using Newtonsoft.Json;
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
        public List<ClientHandler> clients = new List<ClientHandler>();
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
                    //clients.Add(newClient);
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

        internal void UpdateContactBox(Message message)
        {
            foreach (var client in clients)
            {
                message.UserMessage += client.UserName + ";";
            }

            foreach (var client in clients)
            {
                NetworkStream n = client.tcpclient.GetStream();
                BinaryWriter w = new BinaryWriter(n);
                string output = JsonConvert.SerializeObject(message);
                w.Write(output);
            }
            Console.WriteLine("Sent list " + message.UserMessage);
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
                    message.UserName = "Server";
                    message.UserMessage = "Sorry, you are alone...";
                    output = JsonConvert.SerializeObject(message);
                    w.Write(output);
                }
            }
        }

        internal void PrivateBroadcast(ClientHandler client, Message message)//Ej testad
        {
            try
            {
                string[] resiver = message.UserMessage.Split(';');
                message.UserMessage = resiver[1];
                NetworkStream n = client.tcpclient.GetStream();
                BinaryWriter w = new BinaryWriter(n);
                string output = JsonConvert.SerializeObject(message);
                w.Write(output);

                message.UserName = resiver[0];
                client = clients.SingleOrDefault(c => c.UserName == message.UserName);
                n = client.tcpclient.GetStream();
                w = new BinaryWriter(n);
                output = JsonConvert.SerializeObject(message);
                w.Write(output);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void Verification(ClientHandler client, Message message)
        {

            NetworkStream n = client.tcpclient.GetStream();
            BinaryWriter w = new BinaryWriter(n);

            string output = JsonConvert.SerializeObject(message);
            w.Write(output);
        }

        public void DisconnectClient(ClientHandler client)
        {
            Message message = new Message();
            message.Action = "usersOnline";
            clients.Remove(client);
            UpdateContactBox(message);

            Console.WriteLine($"{client.UserName} has left the building...");
        }
    }

}
