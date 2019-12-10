using System;
using System.Collections.Generic;
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
        Client c = new Client();
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Text.Length > 3)
            {
                if (LoginBox.Text != "")
                {
                    login = LoginBox.Text;
                    password = PasswordBox.Text;
                    email = "";
                    image = "";

                    c.connect(login, password, email, image);

                    MainWindow mw = new MainWindow(c);
                    Close();
                    mw.Show();
                }
                else
                {
                    MessageBox.Show("Fill all fields");
                }
            }
            else
            {
                MessageBox.Show("Password is short, minimum 4 characters!!!");
                PasswordBox.Text = "";
            }

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

        }
    }
}
