using ChatWPF.Transfer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
using System.Windows.Shapes;

namespace ChatWPF
{

    public partial class Client : Window
    {
        private Socket server;
        private string userName;
        private CancellationTokenSource cancellationTokenSource;
        private bool isDisconnect = false;

        public Client(string userName, Socket server)
        {
            InitializeComponent();

            this.Title = userName;
            this.userName = userName;
            cancellationTokenSource = new CancellationTokenSource();

            this.server = server;
            startListenServer();

            Message request = new Message(Requests.ADD_NICKNAME, userName, "");
            Requests.SendMessage(server, request);

        }

        private async void startListenServer()
        {
            try
            {
                Task task = RecieveMessage(cancellationTokenSource.Token);
                await task;
            }
            catch (OperationCanceledException ex)
            {
                isDisconnect = true;
                this.Close();
            }
        }

        private async Task RecieveMessage(CancellationToken token)
        {
            while (!cancellationTokenSource.IsCancellationRequested)
            {
                byte[] bytes = new byte[1024];
                await server.ReceiveAsync(new ArraySegment<byte>(bytes), SocketFlags.None);

                string messageString = Encoding.UTF8.GetString(bytes);
                Message message = JsonConvert.DeserializeObject<Message>(messageString);

             
                if (message.requestName.Equals(Requests.LIST_USERS_IN_CHAT))
                {
                    ClientsLbx.Items.Clear();
                    string[] clients = message.content.Split('\n');
                    foreach (string client in clients)
                    {
                        ClientsLbx.Items.Add(client);
                    }
                }

                if (message.requestName.Equals(Requests.LOG_OUT_CHAT))
                {
                    token.ThrowIfCancellationRequested();
                }

                if (message.requestName.Equals(Requests.ADD_NICKNAME))
                {
                    Message res = new Message(Requests.ADD_NICKNAME, userName, "");
                    Requests.SendMessage(server, res);
                }

                if (message.requestName.Equals(Requests.SEND_MESSAGE))
                {
                    if (message.owner.Equals(userName))
                    {
                        MessagesLbx.Items.Add($"{message.dateSend}   {message.content}");
                    }
                    else
                    {
                        MessagesLbx.Items.Add($"{message.dateSend}   [{message.owner}]: {message.content}");
                    }
                }
            }
        }

        private void Send_User_Message(object sender, RoutedEventArgs e)
        {
            string text = MessageTxt.Text.Trim();
            if (text.Equals("/disconnect"))
            {
                LogOutChat();
                return;
            }
            Message message = new Message(Requests.SEND_MESSAGE, userName, MessageTxt.Text);
            MessageTxt.Text = "";
            Requests.SendMessage(server, message);
        }

        private void LogOutChat_Button_Click(object sender, RoutedEventArgs e)
        {
            LogOutChat();
        }

      
        private void LogOutChat()
        {
            Message request = new Message(Requests.LOG_OUT_CHAT, userName, "");
            Requests.SendMessage(server, request);
            cancellationTokenSource.Cancel();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isDisconnect) LogOutChat();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
