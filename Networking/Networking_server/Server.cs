﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebLibrary;

namespace Networking_server
{
    public class Server
    {
        List<ClientHandler> clients = new List<ClientHandler>();
        public void Run()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 5000);
            Console.WriteLine("Server up and running, waiting for messages...");
            try
            {
                listener.Start();

                while (true)
                {
                    TcpClient c = listener.AcceptTcpClient();
                    ClientHandler newClient = new ClientHandler(c, this);
                    clients.Add(newClient);

                    Thread clientThread = new Thread(newClient.Run);
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (listener != null)
                    listener.Stop();
            }
        }

        public void Broadcast(ClientHandler client, Message message)
        {
            foreach (ClientHandler tmpClient in clients)
            {

                NetworkStream n = tmpClient.tcpclient.GetStream();
                BinaryWriter w = new BinaryWriter(n);
                string output = JsonConvert.SerializeObject(message);
                w.Write(output);

                if (clients.Count() == 1)
                {
                    NetworkStream m = tmpClient.tcpclient.GetStream();
                    BinaryWriter v = new BinaryWriter(m);
                    message.UserName = "Server";
                    message.UserMessage = "Sorry, you are alone...";
                    output = JsonConvert.SerializeObject(message);
                    v.Write(output);
                }
            }
        }

        public void DisconnectClient(ClientHandler client)
        {
            clients.Remove(client);
            Console.WriteLine("Client X has left the building...");
            //Broadcast(client, );
        }
    }

}