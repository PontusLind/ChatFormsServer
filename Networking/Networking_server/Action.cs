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
                case "sendMessage":
                    server.Broadcast(client, message);
                    break;
                case "login":
                    Console.WriteLine("Någon försöker logga in");
                    bool responsFromDB = DataBaseConnection.LoginDB(message.UserName, message.UserMessage);
                    message.UserMessage = responsFromDB.ToString();

                    if (responsFromDB && !server.clients.Exists(c => c.UserName == message.UserName))
                    {
                        server.clients.Add(client);
                        client.UserName = message.UserName;
                    }

                    Server.Verification(client, message);

                    if(responsFromDB)
                    {
                        message.UserMessage = "";
                        message.Action = "usersOnline";
                        server.UpdateContactBox(message);
                    }
                    break;
                case "createUser":
                    Console.WriteLine("Någon försöker skapa en användare");
                    responsFromDB = DataBaseConnection.CreateUserDB(message.UserName, message.UserMessage);
                    message.UserMessage = responsFromDB.ToString();
                    Server.Verification(client, message);
                    break;
                case "sendPrivateMessage":
                    server.PrivateBroadcast(client, message);
                    break;
                default:
                    message.Action = "NonValidAction";
                    message.UserMessage = "No server action";
                    Server.Verification(client, message);
                    break;


            }
        }
    }
}
