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
                case "sendMessage":
                    server.Broadcast(client, message);//Beta version
                    break;
                case "login":
                    Console.WriteLine("Någon försöker logga in");
                    bool responsFromDB = DataBaseConnection.LoginDB(message.UserName, message.UserMessage);
                    message.UserMessage = responsFromDB.ToString();
                    Server.Verification(client, message);
                    break;
                case "createUser":
                    Console.WriteLine("Någon försöker skapa en användare");
                    responsFromDB = DataBaseConnection.CreateUserDB(message.UserName, message.UserMessage);
                    message.UserMessage = responsFromDB.ToString();
                    Server.Verification(client, message);
                    break;
                default:
                    message.UserMessage = "No server action";
                    Server.Verification(client, message);
                    break;


            }
        }
    }
}
