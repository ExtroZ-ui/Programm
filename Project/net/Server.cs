using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Project.net
{
    public class Server
    {
        static TcpClient client;
        static BinaryReader br;
        static BinaryWriter bw;
        static string unityName;

        public static void Connect()
        {
            // Мой 192.168.43.200 169.254.240.128
            // Сервак в 1 корп 192.168.0.1
            try
            {
                client = new TcpClient("192.168.43.200", 8888);
                br = new BinaryReader(client.GetStream());
                bw = new BinaryWriter(client.GetStream());
                bw.Write(Environment.MachineName);
                unityName = $"{Environment.MachineName} Unity";
                new Thread(RecvThread).Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось установить соединение с сервером.",
                    "Ошибка", MessageBoxButton.OK);
            }
        }

        public static void ExitConnect()
        {
            client.Close();
        }

        public static void RecvThread()
        {
            try
            {
                for (; ; )
                {
                    if(unityName == $"{Environment.MachineName} Unity")
                    {
                        string msg = br.ReadString();
                        string[] msgParams = msg.Split('|');
                        if (msgParams[0] == "")
                        {
                            NormativForm.grady = int.Parse(msgParams[1]);
                            NormativForm.timeNorm = msgParams[2];
                            //AddChatMessage(msgParams[1], msgParams[2]);
                        }
                    }
                    
                }
            }
            catch
            {
                return;
            }
        }



    }
}
