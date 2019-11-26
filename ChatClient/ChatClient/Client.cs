﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace ChatClient
{
    public class Client
    {
        Thread tcpThread;      // Receiver
        bool _conn = false;    // Is connected/connecting?
        string _user;          // Username

        public string Server { get { return "localhost"; } }  // Address of server. In this case - local IP address.
        public int Port { get { return 2000; } }

        public string UserName { get { return _user; } }

        // Start connection thread and login or register.
        public void connect(string user)
        {
            if (!_conn)
            {
                _conn = true;
                _user = user;
                tcpThread = new Thread(new ThreadStart(SetupConn));
                tcpThread.Start();
            }
        }
        public void Disconnect()
        {
            if (_conn)
                CloseConn();
        }
        public void SendMessage(string to, string msg)
        {
            if (_conn)
            {
                bw.Write(to);
                bw.Write(msg);
                bw.Flush();
            }
        }

        TcpClient client;
        NetworkStream netStream;
        BinaryReader br;
        BinaryWriter bw;

        void SetupConn()  // Setup connection and login
        {
            client = new TcpClient(Server, Port);  // Connect to the server.
            netStream = client.GetStream();
            // Now we have encrypted connection.

            br = new BinaryReader(netStream, Encoding.UTF8);
            bw = new BinaryWriter(netStream, Encoding.UTF8);

            bw.Write(UserName);
            bw.Flush();

            Receiver(); // Time for listening for incoming messages.
            if (_conn)
                CloseConn();
        }
        void CloseConn() // Close connection.
        {
            br.Close();
            bw.Close();
            netStream.Close();
            client.Close();
            _conn = false;
        }
        void Receiver()  // Receive all incoming packets.
        {
            try
            {
                while (client.Connected)  // While we are connected.
                {
                    string from = br.ReadString();
                    string msg = br.ReadString();
                    Console.WriteLine("[{0}] ({1} -> {2}) Message sent!", DateTime.Now, from, msg);
                }
            }
            catch (IOException) { }
        }

    }
}
