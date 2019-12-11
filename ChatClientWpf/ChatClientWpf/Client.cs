using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Windows;

namespace ChatClientWpf
{
    public class Client
    {
        Thread tcpThread;      // Receiver
        bool _conn = false;    // Is connected/connecting?

        public event IMReceivedEventHandler MessageReceived;

        public string Server { get { return "localhost"; } }  // Address of server. In this case - local IP address.
        public int Port { get { return 2000; } }

        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public string Image { get; private set; }

        virtual protected void OnMessageReceived(IMReceivedEventArgs e)
        {
            if (MessageReceived != null)
                MessageReceived(this, e);
        }
        /// <summary>
        /// Створює у новому потоці підключення до сервера та зберігає дані користувача
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="image"></param>
        // Start connection thread and login or register.
        public void connect(string user, string password, string email, string image)
        {
            if (!_conn)
            {
                _conn = true;
                UserName = user;
                Password = password;
                Email = email;
                Image = image;

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
        /// <summary>
        /// Ініціалізує потоки та створює підключення до серверу
        /// </summary>
        void SetupConn()  // Setup connection and login
        {
            client = new TcpClient(Server, Port);  // Connect to the server.
            netStream = client.GetStream();

            br = new BinaryReader(netStream, Encoding.UTF8);
            bw = new BinaryWriter(netStream, Encoding.UTF8);

            bw.Write(UserName);
            bw.Write(Password);
            bw.Write(Email);
            bw.Write(Image);
            bw.Flush();

            if (Email == "")
            {
                Receiver(); // Time for listening for incoming messages.
                if (_conn)
                    CloseConn();
            }
            else
            {
                if (_conn)
                    CloseConn();
            }
        }
        /// <summary>
        /// Закриває потоки та звільняє память
        /// </summary>
        public void CloseConn() // Close connection.
        {
            br.Close();
            bw.Close();
            netStream.Close();
            client.Close();
            _conn = false;
        }
        /// <summary>
        /// Слухає сервер
        /// </summary>
        void Receiver()  // Receive all incoming packets.
        {
            try
            {
                while (client.Connected)  // While we are connected.
                {
                    string from = br.ReadString();
                    string msg = br.ReadString();
                    Console.WriteLine("[{0}] ({1} -> {2}) Message sent!", DateTime.Now, from, msg);
                    OnMessageReceived(new IMReceivedEventArgs(from, msg));
                }
            }
            catch (IOException) { }
        }

    }
    /// <summary>
    /// Клас для передачі в подію аргументів user і message
    /// </summary>
    public class IMReceivedEventArgs : EventArgs
    {
        string user;
        string msg;

        public IMReceivedEventArgs(string user, string msg)
        {
            this.user = user;
            this.msg = msg;
        }

        public string From
        {
            get { return user; }
        }
        public string Message
        {
            get { return msg; }
        }
    }

    public delegate void IMReceivedEventHandler(object sender, IMReceivedEventArgs e);
}
