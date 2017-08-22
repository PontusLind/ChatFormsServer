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

namespace Networking_client
{
    class Program
    {
        static void Main(string[] args)
        {
            Client myClient = new Client();

            Thread clientThread = new Thread(myClient.Start);
            clientThread.Start();
            clientThread.Join();
        }

        public class Client
        {
            private TcpClient client;

            public void Start()
            {

                client = new TcpClient("192.168.25.76", 5000);

                Thread listenerThread = new Thread(Send);
                listenerThread.Start();

                Thread senderThread = new Thread(Listen);
                senderThread.Start();

                senderThread.Join();
                listenerThread.Join();
            }

            public void Listen()
            {
                Message message = new Message();

                try
                {           
                    while (true)
                    {
                        NetworkStream n = client.GetStream();
                        message = JsonConvert.DeserializeObject<Message>(new BinaryReader(n).ReadString());
                        Console.WriteLine($"{message.UserName}: {message.UserMessage}");                       
                    }                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            public void Send()
            {
                Message message = new Message();
                Console.Write("Namn: ");
                message.UserName = Console.ReadLine();
                message.Version = "1.0";
                message.UserMessage = $"{message.UserName} joined the chat";

                try
                {
                    while (!message.UserMessage.Equals("quit"))
                    {
                        NetworkStream n = client.GetStream();
                        message.UserMessage = Console.ReadLine();
                        BinaryWriter w = new BinaryWriter(n);
                        string output = JsonConvert.SerializeObject(message);
                        w.Write(output);
                        w.Flush();
                    }

                    client.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
