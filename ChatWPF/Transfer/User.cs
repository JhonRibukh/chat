using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatWPF.Transfer
{
    internal class User
    {
        private Socket socket { get; set; }
        private string username { get; set; }
    }
}
