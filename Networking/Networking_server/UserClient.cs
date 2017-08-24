using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Networking_server
{
    public class UserClient
    {
        public UserClient(TcpClient userConnection, string userName)
        {
            UserConnection = userConnection;
            UserName = userName;
        }
        public TcpClient UserConnection { get; set; }
        public string UserName { get; set; }
    }
}
