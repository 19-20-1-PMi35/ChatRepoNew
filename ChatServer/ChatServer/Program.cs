using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Sockets;
using ChatServer;

namespace InstantMessengerServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            Console.WriteLine();
            Console.WriteLine("Press enter to close program.");
            Console.ReadLine();
        }

        // IP of this computer. If you are running all clients at the same computer you can use 127.0.0.1 (localhost). 
        public IPAddress ip = IPAddress.Parse("127.0.0.1");
        public int port = 2000;
        public TcpListener server;

        public Dictionary<string, Client> users = new Dictionary<string, Client>();

        public List<User> all_users = new List<User>();

        public Program()
        {
            Console.Title = "InstantMessenger Server";
            Console.WriteLine("----- InstantMessenger Server -----");
            Console.WriteLine("[{0}] Starting server...", DateTime.Now);

            using (SqlConnection connection = new SqlConnection
                (@"data source=DESKTOP-NV23DLC;Initial Catalog=chatdb;integrated security=True;connect timeout=30;MultipleActiveResultSets=True;"))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand
                    (@"
                           SELECT [Id]
                          ,[Login]
                          ,[Password]
                          ,[Email]
                          ,[Image]
                      FROM [chatdb].[dbo].[Users]", connection);


                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User();
                        user.Id = reader.GetInt32(0);
                        user.Login = reader.GetString(1);
                        user.Password = reader.GetString(2);
                        user.Email = reader.GetString(3);
                        user.Image = reader.GetString(4);
                        all_users.Add(user);
                    }
                }

                server = new TcpListener(ip, port);
                server.Start();

                Console.WriteLine("[{0}] Server is running properly!", DateTime.Now);

                Listen();
            }
        }
        void Listen()  // Listen to incoming connections.
        {

            while (true)
            {
                TcpClient tcpClient = server.AcceptTcpClient();  // Accept incoming connection.
                Client client = new Client(this, tcpClient);     // Handle in another thread.
            }
        }

        public void AddUser(string login, string email, string password, string image)
        {
            using (ChatEntities db = new ChatEntities())
            {
                db.Users.Add(new User { Login = login, Email = email, Password = password, Image = image });
                db.SaveChanges();
            }
        }

        public void SaveMessage(int idFrom, int idTo, DateTime date, string message)
        {
            using (ChatEntities db = new ChatEntities())
            {
                db.Messages.Add(new Message { Id_user_from = idFrom, Id_user_to = idTo, Created_at = date, Message1 = message });
                db.SaveChanges();
            }
        }
    }
}
