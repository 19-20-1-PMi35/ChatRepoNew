using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.Data;
using System.Data.SqlClient;
=======
>>>>>>> 6ca16eb674649a1dba8c9a59a1146192217f0063
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChatClientWpf
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
<<<<<<< HEAD
        Client c = new Client();

=======
>>>>>>> 6ca16eb674649a1dba8c9a59a1146192217f0063
        string login;
        string email;
        string password;
        string image;

        public LoginWindow()
        {
            InitializeComponent();
            login = "";
            email = "";
            password = "";
            image = "";
        }
<<<<<<< HEAD
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
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
                List<int> ids = new List<int>();
                List<string> logins = new List<string>();
                List<string> passwords = new List<string>();
                List<string> emails = new List<string>();
                List<string> images = new List<string>();

                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ids.Add(reader.GetInt32(0));
                        logins.Add(reader.GetString(1));
                        passwords.Add(reader.GetString(2));
                        emails.Add(reader.GetString(3));
                        images.Add(reader.GetString(4));
                    }
                }

                login = LoginBox.Text;
                password = PasswordBox.Text;
                for (int i = 0; i < logins.Count; i++)
                {
                    if (logins[i++] == login)
                    {
                        if (passwords[i++] == password)
                        {
                            //MessageBox.Show("There is such a person");
                            email = "";
                            image = ""; 
                            MainWindow mw = new MainWindow(c);
                            Close();
                            mw.Show();
                        }
                    }
                }
                //MessageBox.Show("Can't find such a person");
            }
=======
        Client c = new Client();
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            login = LoginBox.Text;
            password = PasswordBox.Text;
            email = "";
            image = "";
            c.connect(login, password, email, image);
            MainWindow mw = new MainWindow(c);
            Close();
            mw.Show();
>>>>>>> 6ca16eb674649a1dba8c9a59a1146192217f0063
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow mw = new RegistrationWindow();
            mw.ShowDialog();
            login = LoginBox.Text = mw.Login;
            password = PasswordBox.Text = mw.Password;
            email = mw.Email;
            image = mw.Image;
            if ((bool)mw.DialogResult)
            {
                c.connect(login, password, email, image);
            }
<<<<<<< HEAD

        }

        private void LoginBox_TextChanged(object sender, TextChangedEventArgs e)
        {

=======
            
>>>>>>> 6ca16eb674649a1dba8c9a59a1146192217f0063
        }
    }
}
