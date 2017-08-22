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
    class Program
    {


        static void Main(string[] args)
        {
            Server myServer = new Server();
            Thread serverThread = new Thread(myServer.Run);
            serverThread.Start();
            serverThread.Join();
        }

        #region Bortkommenterad kod
        //public class Server
        //{
        //    List<ClientHandler> clients = new List<ClientHandler>();
        //    public void Run()
        //    {
        //        TcpListener listener = new TcpListener(IPAddress.Any, 5000);
        //        Console.WriteLine("Server up and running, waiting for messages...");

        //        try
        //        {
        //            listener.Start();

        //            while (true)
        //            {
        //                TcpClient c = listener.AcceptTcpClient();
        //                ClientHandler newClient = new ClientHandler(c, this);
        //                clients.Add(newClient);

        //                Thread clientThread = new Thread(newClient.Run);
        //                clientThread.Start();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //        finally
        //        {
        //            if (listener != null)
        //                listener.Stop();
        //        }
        //    }

        //    public void Broadcast(ClientHandler client, Message message)
        //    {
        //        foreach (ClientHandler tmpClient in clients)
        //        {

        //            NetworkStream n = tmpClient.tcpclient.GetStream();
        //            BinaryWriter w = new BinaryWriter(n);
        //            string output = JsonConvert.SerializeObject(message);
        //            w.Write(output);

        //            if (clients.Count() == 1)
        //            {
        //                NetworkStream m = tmpClient.tcpclient.GetStream();
        //                BinaryWriter v = new BinaryWriter(m);
        //                message.UserName = "Server";
        //                message.UserMessage = "Sorry, you are alone...";
        //                output = JsonConvert.SerializeObject(message);
        //                v.Write(output);
        //            }
        //        }
        //    }

        //    public void DisconnectClient(ClientHandler client)
        //    {
        //        clients.Remove(client);
        //        Console.WriteLine("Client X has left the building...");
        //        //Broadcast(client, );
        //    }
        //}

        //public class ClientHandler
        //{
        //    public TcpClient tcpclient;
        //    private Server myServer;
        //    public ClientHandler(TcpClient c, Server server)
        //    {
        //        tcpclient = c;
        //        this.myServer = server;
        //    }

        //    public void Run()
        //    {

        //        Message message = new Message();
        //        message.UserMessage = "";
        //        while (!message.UserMessage.Equals("quit"))
        //        {
        //            try
        //            {
        //                NetworkStream n = tcpclient.GetStream();
        //                message = JsonConvert.DeserializeObject<Message>(new BinaryReader(n).ReadString());
        //                //Action.CheckAction(this, message);
        //                myServer.Broadcast(this, message);
        //                Console.WriteLine($"{message.UserName} sent a message: {message.UserMessage}");
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(ex.Message);
        //                myServer.DisconnectClient(this);
        //                tcpclient.Close();
        //                break;
        //            }

        //        }

        //        myServer.DisconnectClient(this);
        //        tcpclient.Close();

        //    }
        //}
        #endregion
    }
}
