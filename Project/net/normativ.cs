using Project.net.Class;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Project.net
{
    public class normativ
    {
        /*[DllImport("User32.dll")]
        static extern bool MoveWindow(IntPtr handle, int x, int y, int width, int height, bool redraw);

        internal delegate int WindowEnumProc(IntPtr hwnd, IntPtr lparam);
        [DllImport("user32.dll")]
        internal static extern bool EnumChildWindows(IntPtr hwnd, WindowEnumProc func, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private static Process process;
        private IntPtr unityHWND = IntPtr.Zero;

        private const int WM_ACTIVATE = 0x0006;
        private readonly IntPtr WA_ACTIVE = new IntPtr(1);
        private readonly IntPtr WA_INACTIVE = new IntPtr(0);*/
        private static Process process;
        private static int idTest;
        public static int grady;
        public static string timeNorm;
        public static void StartProcess(int tempIdTest)
        {
            idTest = tempIdTest;
            process = new Process();
            Proc();
        }

       static void Proc()
        {
            string tempObjectUri = MainWindow._context.Normativ
                .Where(i => i.idTest == idTest).Select(lit => lit.uri).FirstOrDefault();

            process.StartInfo.FileName = $"{tempObjectUri}";
            //process.StartInfo.Arguments = "-parentHWND " + panel1.Handle.ToInt32() + " " + Environment.CommandLine;
            process.StartInfo.UseShellExecute = true;
            //process.StartInfo.CreateNoWindow = true;

            process.Start();

            // process.WaitForInputIdle();
            // Doesn't work for some reason ?!
            //unityHWND = process.MainWindowHandle;
            // EnumChildWindows(panel1.Handle, WindowEnum, IntPtr.Zero);
            new Thread(() => DisplayProcessStatus(process)).Start();
        }
        private void panel1_Resize(object sender, EventArgs e)
        {
            //MoveWindow(unityHWND, 0, 0, panel1.Width, panel1.Height, true);
        }

       /* private int WindowEnum(IntPtr hwnd, IntPtr lparam)
        {
            unityHWND = hwnd;
            return 0;
        }*/

        private static void ExitG()
        {
            string nameTest = MainWindow._context.Normativ
                .Where(i => i.idTest == idTest).Select(lit => lit.name).FirstOrDefault();
            DateTime date = DateTime.Now;
            MainWindow._context.StateTest.Add(new StateTest()
            {
                data = date.ToString("dd.MM.yyyy"),
                nameTest = nameTest,
                grade = grady,
                error = 0,
                familiy = MainWindow.nameUser,
                discipline = "0",
                speciality = MainWindow.special,
                namePK = Environment.MachineName,
                nomVopr = "-",
                time = timeNorm,
                vzdov = MainWindow.vzvod,
                rota = MainWindow.rota,
                idUser = MainWindow.idUser,

            });
            MainWindow._context.SaveChanges();
        }

        public static void DisplayProcessStatus(Process process)
        {
            bool activ = true;
            while (activ)
            {
                process.Refresh();


                if (process.HasExited)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ExitG();
                        new MainWindow().Show();
                        activ = false;
                        try
                        {
                            process.CloseMainWindow();

                            Thread.Sleep(1000);
                            while (!process.HasExited)
                                process.Kill();
                        }
                        catch (Exception)
                        {

                        }
                    });


                }
            }

        }
    }
}
