using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWPF.Transfer
{
    internal class Message
    {
        public string requestName { get; set; }
        public string owner { get; set; }
        public string content { get; set; }
        public DateTime dateSend { get; set; }    

        public Message() { }

        public Message(string requestName, string owner, string content)
        {
            this.requestName = requestName;
            this.owner = owner;
            this.content = content;
            dateSend = DateTime.Now;
        }
    }
}
