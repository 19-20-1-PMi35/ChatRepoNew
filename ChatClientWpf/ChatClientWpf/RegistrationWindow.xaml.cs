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
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        string login;
        string email;
        string password;
        string confPassword;
        string image;

        public RegistrationWindow()
        {
            InitializeComponent();
            login = "";
            email = "";
            password = "";
            confPassword = "";
            image = "";
        }

        public string Login { get => login; set => login = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Image { get => image; set => image = value; }

        private void RegisttrationButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            if (PasswordBox.Text.Length > 3)
            {
                if (LoginBox.Text != "" && EmailBox.Text != "")
                {
                    login = LoginBox.Text;
                    email = EmailBox.Text;
                    password = PasswordBox.Text;
                    confPassword = PasswordBoxCheck.Text;
                    if (password != confPassword)
                    {
                        MessageBox.Show("Passwords do not match");
                        PasswordBoxCheck.Text = "";
                    }
                    else
                    {
                        Close();
                        this.DialogResult = true;
                    }
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
                PasswordBoxCheck.Text = "";
            }
        }
    }
}
