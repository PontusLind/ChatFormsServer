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
        public static void CheckAction(Server server, ClientHandler client, Message message)
        {

            Console.WriteLine($"{message.UserName} request {message.Action}");

            switch (message.Action)
            {
                case "sendMessage":
                    if (server.clients.Exists(c => c.UserName == message.UserName))
                    {
                        server.Broadcast(client, message);
                    }
                    break;
                case "login":
                    bool responsFromDB = DataBaseConnection.LoginDB(message.UserName, message.UserMessage);
                    message.UserMessage = responsFromDB.ToString();

                    if (responsFromDB && !server.clients.Exists(c => c.UserName == message.UserName))
                    {
                        Console.WriteLine("login succeeded");
                        server.clients.Add(client);
                        client.UserName = message.UserName;
                    }

                    Server.Verification(client, message);

                    if (responsFromDB)
                    {
                        message.UserMessage = "";
                        message.Action = "usersOnline";
                        server.UpdateContactBox(message);
                    }
                    break;
                case "createUser":
                    responsFromDB = DataBaseConnection.CreateUserDB(message.UserName, message.UserMessage);
                    message.UserMessage = responsFromDB.ToString();
                    Server.Verification(client, message);
                    break;
                case "sendPrivateMessage":
                    message.Action = "sendMessage";
                    server.PrivateBroadcast(client, message);
                    break;
                default:
                    server.DisconnectClient(client);
                    Console.WriteLine($"{message.UserMessage} was kicked");
                    break;


            }
        }
    }
}
