using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Collections.Generic;

namespace Server
{
    class ClientObject
    {
        protected internal string Id { get; private set; }
        private TcpClient tcpClient;
        public string clientMachineName;
        public BinaryReader br;
        public BinaryWriter bw;

        public ClientObject(TcpClient tcpClient)
        {
            Id = Guid.NewGuid().ToString();
            this.tcpClient = tcpClient;   
            br = new BinaryReader(tcpClient.GetStream());
            bw = new BinaryWriter(tcpClient.GetStream());
            clientMachineName = br.ReadString();
        }

        public void SendMessage(string msg)
        {
            bw.Write(msg);
        }

        public string RecvMessage()
        {
            return br.ReadString();
        }
    }

    class Program
    {
        List<ClientObject> clients;
        // Мой 192.168.43.200
        // Сервак в 1 корп 192.168.0.1
        Program()
        {
            try
            {
            clients = new List<ClientObject>();
            var tcpListener = new TcpListener(IPAddress.Parse("0.0.0.0"), 8888);
            tcpListener.Start();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Ожидание соединений...");
            Console.ResetColor();

            for (; ; )
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                ClientObject clientObject = new ClientObject(tcpClient);
                    clients.Add(clientObject);
                new Thread(() => ClientThread(clientObject)).Start();
            }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();

            }
            
        }

        void ClientThread(ClientObject clientObject)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Клиент подключён {clientObject.clientMachineName}");
            Console.ResetColor();
            try
            {
                for (; ; )
                {
                    string msg = clientObject.RecvMessage();
                    Console.WriteLine($"{clientObject.clientMachineName}: " + msg);
                    string[] msgParams = msg.Split('|');
                    if (msgParams[0] == "")
                    {
                        BroadcastMessage(msg, clientObject);
                    }
                }
            }
            catch
            {
                clients.Remove(clientObject);
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"Клиент отключён {clientObject.clientMachineName}");
            Console.ResetColor();
        }

        void BroadcastMessage(string msg, ClientObject excludedClient)
        {
            foreach (var client in clients)
                if (client != excludedClient)
                    client.SendMessage(msg);
        }

        static void Main(string[] args)
        {
            Console.Title = "Сервер v1.1";
            new Program();
        }
    }
}
