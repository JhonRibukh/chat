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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using ChatWPF.Transfer;
using Newtonsoft.Json;

namespace ChatWPF
{
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        private void ServerConect_Click(object sender, RoutedEventArgs e)
        {
            string UserName = UserNameTextBox.Text.Trim();
            if (UserName.Length == 0)
            {

                MessageBox.Show("Не указано имя пользователя!");
                return;

            }

            string serverIp1 = ServerIpTextBox1.Text.Trim();
            string serverIp2 = ServerIpTextBox2.Text.Trim();
            string serverIp3 = ServerIpTextBox3.Text.Trim();
            string serverIp4 = ServerIpTextBox4.Text.Trim();
            if (serverIp1.Length == 0 || serverIp2.Length == 0 || serverIp3.Length == 0 || serverIp4.Length == 0)
            {
                MessageBox.Show("Не указано ip!");
                return;
            }

            if (!isNumber(serverIp1) || !isNumber(serverIp2) || !isNumber(serverIp3) || !isNumber(serverIp4))
            {
                MessageBox.Show("Некорректный ip!");
                return;
            }

            if (int.Parse(serverIp1) < 0 || int.Parse(serverIp1) > 255 || int.Parse(serverIp2) < 0 || int.Parse(serverIp2) > 255
                || int.Parse(serverIp3) < 0 || int.Parse(serverIp3) > 255 || int.Parse(serverIp4) < 0 || int.Parse(serverIp4) > 255)
            {
                MessageBox.Show("Некорректный адрес! Каждый узел должен находиться в диапазоне [0..255]");
                return;
            }

            string serverIp = serverIp1 + "." + serverIp2 + "." + serverIp3 + "." + serverIp4;

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                var tsk = server.ConnectAsync(serverIp, 8888);
                tsk.Wait(3000);
                RecieveMessage(server);
            }
            catch (AggregateException)
            {
                MessageBox.Show( $"Не удалось устоановить подключение к {serverIp}");
            }
        }

        private async Task RecieveMessage(Socket server)
        {
            while (true)
            {
                byte[] bytes = new byte[1024];
                await server.ReceiveAsync(new ArraySegment<byte>(bytes), SocketFlags.None);

                string messageString = Encoding.UTF8.GetString(bytes);
                Message message = JsonConvert.DeserializeObject<Message>(messageString);

                if (message.requestName.Equals(Requests.ADD_NICKNAME))
                {
                    Requests.SendMessage(server, new Message(Requests.CHECK_EXIST_USERNAME, "", UserNameTextBox.Text.Trim()));
                }

                if (message.requestName.Equals(Requests.CHECK_EXIST_USERNAME))
                {
                    if (message.content.Equals(""))
                    {
                        Client client = new Client(UserNameTextBox.Text.Trim(), server);
                        client.Show();
                        this.Close();
                        break;
                    }

                    MessageBox.Show(message.content);
                }
            }
        }

        private void ServerCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Server server = new Server();
                server.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Адрес сервера уже занят!");
            }
        }

        private bool isNumber(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9') return false;
            }
            return true;
        }
    }
}
