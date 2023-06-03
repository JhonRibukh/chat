using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
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
using ChatWPF.Transfer;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Threading;

namespace ChatWPF
{
    
    public partial class Server : Window
    {
        private Socket socket;

        private Dictionary<Socket, string> clientsDict = new Dictionary<Socket, string>();
        private IPAddress ip;

        public Server()
        {
            InitializeComponent();

            string Host = Dns.GetHostName();
            string IP = Dns.GetHostByName(Host).AddressList[0].ToString();
            ip = IPAddress.Parse(IP);

            ChatIpTB.Text += ip;

            IPEndPoint ipPoint = new IPEndPoint(ip, 8888);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipPoint);
            socket.Listen(1000);
            ListenToClients();
        }

       
        private async Task ListenToClients()
        {
            while (true)
            {
                var client = await socket.AcceptAsync();
                RecieveMessage(client);

                Message message = new Message(Requests.ADD_NICKNAME, "Администратор чата", "");
                Requests.SendMessage(client, message);
            }
        }

        
        private async Task RecieveMessage(Socket client)
        {
            while (true)
            {

                byte[] bytes = new byte[1024];
                await client.ReceiveAsync(new ArraySegment<byte>(bytes), SocketFlags.None);
                string messageText = Encoding.UTF8.GetString(bytes);
                Message request = JsonConvert.DeserializeObject<Message>(messageText);

                
                if (request.requestName.Equals(Requests.ADD_NICKNAME))
                {
                    clientsDict.Add(client, request.owner);
                    ClientsLbx.Items.Add(request.owner + " (" + client.RemoteEndPoint + ")");            

                    MessagesLbx.Items.Add($"{DateTime.Now}   Пользователь [{request.owner} ({client.RemoteEndPoint})] подключился к чату");

                    string list_users_in_chat = Create_Users_List();
                    Message message = new Message(Requests.LIST_USERS_IN_CHAT, "Администратор чата", list_users_in_chat);
                    Requests.SendMessage(clientsDict.Keys.ToList(), message);                               
                }


                if (request.requestName.Equals(Requests.LIST_USERS_IN_CHAT))
                {

                }


              
                if (request.requestName.Equals(Requests.CHECK_EXIST_USERNAME))
                {
                    string username = request.content;
                    bool isExist = false;
                    foreach (var cl in clientsDict)
                    {
                        if (cl.Value.Equals(username))
                        {
                            isExist = true;
                        }
                    }

                    if (isExist)
                    {
                        Requests.SendMessage(client, new Message(Requests.CHECK_EXIST_USERNAME, "Администратор чата", "Пользователь с таким именем уже существует"));
                    }
                    else
                    {
                        Requests.SendMessage(client, new Message(Requests.CHECK_EXIST_USERNAME, "Администратор чата", ""));
                    }
                }

                
                if (request.requestName.Equals(Requests.LOG_OUT_CHAT))
                {
                    clientsDict.Remove(client);

                    ClientsLbx.Items.Remove(request.owner + " (" + client.RemoteEndPoint + ")");
                    Requests.SendMessage(client, new Message(Requests.LOG_OUT_CHAT, "", ""));                   

                    string list_users_in_chat = Create_Users_List();
                    Message message = new Message(Requests.LIST_USERS_IN_CHAT, "Администратор чата", list_users_in_chat);
                    Requests.SendMessage(clientsDict.Keys.ToList(), message);                                    

                    MessagesLbx.Items.Add($"{DateTime.Now}   Пользователь [{request.owner} ({client.RemoteEndPoint})] вышел из чата");
                }

              
                if (request.requestName.Equals(Requests.SEND_MESSAGE))
                {
                    
                    MessagesLbx.Items.Add($"{request.dateSend}   [{clientsDict[client]}]: {request.content}");
                    Requests.SendMessage(clientsDict.Keys.ToList(), request);
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string messageText = "\n-----------\n" + MessageTb.Text + "\n-----------\n";
            MessageTb.Text = "";
            MessagesLbx.Items.Add(DateTime.Now + messageText);

            Message message = new Message(Requests.SEND_MESSAGE, "Администратор чата", $"\n{messageText}");
            Requests.SendMessage(clientsDict.Keys.ToList(), message);
        }

        private string Create_Users_List()
        {
            string list_users_in_chat = "";
            foreach (var cl in clientsDict)
            {
                list_users_in_chat += cl.Value + " (" + cl.Key.RemoteEndPoint + ")\n";
            }
            return list_users_in_chat;
        }

        private async void Save_Log_Button_Click(object sender, RoutedEventArgs e)
        {
            string fileName = $"Log_chat_{ip.ToString().Replace(".", "_")}_DATE_{DateTime.Now.ToShortDateString().Replace(".", "_")}_TIME_{DateTime.Now.TimeOfDay.ToString().Replace(":", "_")}.txt";
            using (StreamWriter writer = new StreamWriter(fileName, false))
            {
                foreach (var message in MessagesLbx.Items)
                {
                    await writer.WriteLineAsync(message.ToString());
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            socket.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
