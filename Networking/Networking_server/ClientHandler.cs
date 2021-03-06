﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WebLibrary;
using static Networking_server.Program;

namespace Networking_server
{
    public class ClientHandler
    {
        public TcpClient tcpclient;
        private Server myServer;
        public string UserName { get; set; }
        public ClientHandler(TcpClient c, Server server)
        {
            tcpclient = c;
            this.myServer = server;
        }

        public void Run()
        {

            Message message = new Message();
            message.UserMessage = "";
            while (!message.UserMessage.Equals("quit"))
            {
                try
                {
                    NetworkStream n = tcpclient.GetStream();
                    message = JsonConvert.DeserializeObject<Message>(new BinaryReader(n).ReadString());
                    Action.CheckAction(myServer, this, message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Request failed");
                    break;
                }

            }

            myServer.DisconnectClient(this);
            tcpclient.Close();

        }
    }

}
