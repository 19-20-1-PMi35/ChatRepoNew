﻿using System;
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
        public LoginWindow()
        {
            InitializeComponent();
        }
        Client c = new Client();
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            c.connect(LoginBox.Text, PasswordBox.Text);
            MainWindow mw = new MainWindow(c);
            Close();
            mw.Show();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow mw = new RegistrationWindow();
            mw.ShowDialog();
        }
    }
}
