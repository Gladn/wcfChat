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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatClient.ServiceChat;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IServiceCallback
    {
        bool IsConnected = false;
        ServiceClient client;
        int ID;
        string name;
        public MainWindow()
        {
            InitializeComponent();
        }

        void ConnectUser()
        {
            if (!IsConnected)
            {
                client = new ServiceClient(new System.ServiceModel.InstanceContext(this));
                ID = client.Connect(UserNameTb.Text);
                
                UserNameTb.IsEnabled = false;
                BtCon.Content = "Disconnect";
                IsConnected = true;
            }
        }
        void DisconnectUser()
        {
            if (IsConnected)
            {
                client.Disconnect(ID);
                client = null;
                UserNameTb.IsEnabled = true;
                BtCon.Content = "Connect";
                IsConnected = false;
            }
        }
        private void ButtClk(object sender, RoutedEventArgs e)
        {
            if (IsConnected)
            {         
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
        }

        public void MsgCallback(string msg)
        {
            lbChat.Items.Add(msg);
            lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void TbChat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (client != null)
                {
                    client.SendMsg(TbChat.Text, ID);
                    TbChat.Text = string.Empty;
                }
                
            }
        }
    }
}
