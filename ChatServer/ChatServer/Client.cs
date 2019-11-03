﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.IO;
using System.Threading;
using ChatServer;

namespace InstantMessengerServer
{
    public class Client
    {
        public Client(Program p, TcpClient c)
        {
            prog = p;
            client = c;

            // Handle client in another thread.
            (new Thread(new ThreadStart(SetupConn))).Start();
        }

        Program prog;
        public TcpClient client;
        public NetworkStream netStream;  // Raw-data stream of connection.
        public BinaryReader br;
        public BinaryWriter bw;

        string currentUser;  // Information about current user.

        void SetupConn()  // Setup connection and login or register.
        {
            try
            {
                Console.WriteLine("[{0}] New connection!", DateTime.Now);
                netStream = client.GetStream();

                br = new BinaryReader(netStream, Encoding.UTF8);
                bw = new BinaryWriter(netStream, Encoding.UTF8);

                currentUser = br.ReadString();
                string password = br.ReadString();

                prog.AddUser(currentUser, "", password, "");

                prog.users.Add(currentUser, this);  // Add new user
                Receiver();  // Listen to client in loop.
                CloseConn();
            }
            catch { CloseConn(); }
        }
        void CloseConn() // Close connection.
        {
            try
            {
                prog.users.Remove(currentUser);
                br.Close();
                bw.Close();
                netStream.Close();
                client.Close();
                Console.WriteLine("[{0}] End of connection!", DateTime.Now);
            }
            catch { }
        }
        void Receiver()  // Receive all incoming packets.
        {
            Console.WriteLine("[{0}] ({1}) User logged in", DateTime.Now, currentUser);
            try
            {
                while (client.Client.Connected)  // While we are connected.
                {
                    int idFrom = -1, idTo = -1;

                    string to = br.ReadString();
                    string msg = br.ReadString();

                    using (ChatEntities db = new ChatEntities())
                    {
                        foreach (var user in db.Users)
                        {
                            if (user.Login == currentUser)
                                idFrom = user.Id;

                            if (user.Login == to)
                                idTo = user.Id;
                        }
                    }

                    Client recipient;
                    if (prog.users.TryGetValue(to, out recipient))
                    {
                        // Write received packet to recipient
                        recipient.bw.Write(currentUser);  // From
                        recipient.bw.Write(msg);
                        recipient.bw.Flush();
                        Console.WriteLine("[{0}] ({1} -> {2}) Message sent!", DateTime.Now, currentUser, to);
                        if(idFrom != -1 && idTo != -1)
                            prog.SaveMessage(idFrom, idTo, DateTime.Now, msg);
                        
                    }
                    else
                        Console.WriteLine("client does not exist");
                }
            }
            catch (IOException) { }

            Console.WriteLine("[{0}] ({1}) User logged out", DateTime.Now, currentUser);
        }

    }
}
