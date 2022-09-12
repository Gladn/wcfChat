using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChatClient.ServiceChat;
using Chat;
using Microsoft.Win32;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

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


        private void RefreshFileList()
        {
            StorageFileInfo[] files = null;

            using (FileRepositoryServiceClient client = new FileRepositoryServiceClient())
            {
                files = client.List(null);
            }

            //FileList.Items.Clear(); 

            int width = 20;
           
            float[] widths = { .2f, .6f, .2f };

            for (int i = 0; i < widths.Length; i++)
                FileData.Columns[i].Width = (int)((float)width * widths[i]);

            foreach (var file in files)
            {
                List<string> filenames = new List<string>();
                string filepath = Path.GetFileName(file.VirtualPath);
                filenames.Add(file.VirtualPath);
                //var item = new ListViewItem(Path.GetFileName(file.VirtualPath));
                // ListViewItem item = new ListViewItem(Path.GetFileName(file.VirtualPath));

                //item.Add(file.VirtualPath);

                float fileSize = (float)file.Size / 1024.0f;
                string suffix = "Kb";

                if (fileSize > 1000.0f)
                {
                    fileSize /= 1024.0f;
                    suffix = "Mb";
                }
                //item.SubItems.Add(string.Format("{0:0.0} {1}", fileSize, suffix));
                FileData.ItemsSource = filenames;
                //FileList.Items.Add(item);
            }
        }

        private void ButtUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Title = "Select a file to upload",
                RestoreDirectory = true,
                CheckFileExists = true
            };

            dlg.ShowDialog();

            if (!string.IsNullOrEmpty(dlg.FileName))
            {
                string virtualPath = Path.GetFileName(dlg.FileName);

                using (Stream uploadStream = new FileStream(dlg.FileName, FileMode.Open))
                {
                    using (FileRepositoryServiceClient client = new FileRepositoryServiceClient())
                    {
                        client.PutFile(new FileUploadMessage() { VirtualPath = virtualPath, DataStream = uploadStream });
                    }
                }

                RefreshFileList();
            }
        }

        private void ButtDelete_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtDownload_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void FileData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
