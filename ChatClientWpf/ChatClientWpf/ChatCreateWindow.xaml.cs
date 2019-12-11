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
    /// Interaction logic for ChatCreateWindow.xaml
    /// </summary>
    public partial class ChatCreateWindow : Window
    {
        Client c;
        public ChatCreateWindow(Client client)
        {
            InitializeComponent();
            c = client;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();

            MainWindow mainWindow = new MainWindow(c);
            mainWindow.Show();
            mainWindow.TextBoxSendTo.Text = SearchTermTextBox.Text;
        }

        private void SearchTermTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
