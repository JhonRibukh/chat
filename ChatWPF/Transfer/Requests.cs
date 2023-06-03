using ChatWPF.Transfer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChatWPF
{
    internal class Requests
    {
        public static string LIST_USERS_IN_CHAT = "/clients/get_all";
        public static string CHECK_EXIST_USERNAME = "/clients/check_exist_username";
        public static string ADD_NICKNAME = "/clients/add_nickname";
        public static string SEND_MESSAGE = "clients/send_message";
        public static string LOG_OUT_CHAT = "log_out_chat";

        public static void ConnectToServer()
        {

        }

        /// <summary>
        /// Отправка сообщения конкретному клиенту
        /// </summary>
        /// <param name="reciever">Получатель сообщения</param>
        /// <param name="message">Сообщение</param>
        public static async Task SendMessage(Socket reciever, Message message)
        {
            string jsonMessage = JsonConvert.SerializeObject(message);
            byte[] bytes = Encoding.UTF8.GetBytes(jsonMessage);
            await reciever.SendAsync(new ArraySegment<byte>(bytes), SocketFlags.None);
        }

        /// <summary>
        /// Отправка сообщения клиентам
        /// </summary>
        /// <param name="clients">Список клиентов</param>
        /// <param name="message">Сообщение</param>
        public static async Task SendMessage(List<Socket> clients, Message message)
        {
            foreach (var item in clients)
            {
                SendMessage(item, message);
            }
        }
    }
}
