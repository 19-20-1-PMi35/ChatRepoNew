using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ChatClientWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Client c;
        IMReceivedEventHandler receivedHandler;
        public MainWindow(Client client)
        {
            InitializeComponent();
            receivedHandler = new IMReceivedEventHandler(im_MessageReceived);
            c = client;
            c.MessageReceived += receivedHandler;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            c.CloseConn();
        }

        void im_MessageReceived(object sender, IMReceivedEventArgs e)
        {
            Dispatcher.Invoke(() => ListBoxMess.Items.Add(String.Format("{0}[{2}] {1}\r\n", e.From, e.Message, DateTime.Now)));
            //    Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(() =>
            //    {

            //    }));


            //    this.BeginInvoke(new MethodInvoker(delegate
            //    {

            //    }));
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            c.SendMessage(TextBoxSendTo.Text, TextBoxMess.Text);
        }
        private void CreateChatButton_Click(object sender, RoutedEventArgs e)
        {
            ChatCreateWindow ccw = new ChatCreateWindow();
            ccw.ShowDialog();
        }

        private void TextBoxSendTo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
