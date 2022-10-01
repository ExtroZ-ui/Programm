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
                client = new TcpClient("192.168.0.1", 8888);
                br = new BinaryReader(client.GetStream());
                bw = new BinaryWriter(client.GetStream());
                unityName = $"{Environment.MachineName} Unity";
                bw.Write(Environment.MachineName);
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

                        string msg = br.ReadString();
                        string[] msgParams = msg.Split('|');
                        if (msgParams[0] == "")
                        {
                                if (msgParams[1] == Environment.MachineName.ToString())
                                {
                                NormativForm.nameTeast = msgParams[2];
                                NormativForm.grady = int.Parse(msgParams[3]);
                                NormativForm.timeNorm = msgParams[4];
                                NormativForm.ExitG();
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
