using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary;

namespace Networking_server
{
    class Action
    {
        public static void CheckAction(Server server, ClientHandler client,  Message message)
        {
            switch (message.Action)
            {
                case null:
                    server.Broadcast(client, message);//Beta version
                    break;
                case "login":
                    bool responsFromDB = DataBaseConnection.LoginDB(message.UserName, message.UserMessage);
                    message.UserMessage = responsFromDB.ToString();
                    Server.LoginVerification(client, message);
                    break;


                default:
                    break;


            }
        }
    }
}
