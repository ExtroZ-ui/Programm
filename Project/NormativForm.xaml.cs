using Project.net.Class;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Application;

namespace Project
{
    /// <summary>
    /// Логика взаимодействия для NormativForm.xaml
    /// </summary>
    /// 

    public partial class NormativForm : Window
    {

        //[DllImport("User32.dll")]
        //static extern bool MoveWindow(IntPtr handle, int x, int y, double width, double height, bool redraw);

        //internal delegate int WindowEnumProc(IntPtr hwnd, IntPtr lparam);
        //[DllImport("user32.dll")]
        //internal static extern bool EnumChildWindows(IntPtr hwnd, WindowEnumProc func, IntPtr lParam);

        //[DllImport("user32.dll")]
        //static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private Process process;
        private IntPtr unityHWND = IntPtr.Zero;

        private const int WM_ACTIVATE = 0x0006;
        private readonly IntPtr WA_ACTIVE = new IntPtr(1);
        private readonly IntPtr WA_INACTIVE = new IntPtr(0);


        private int idTest;
        public static int grady;
        public static string nameTeast;
        public static string timeNorm;
        public NormativForm(int tempIdTest)
        {
            InitializeComponent();
            idTest = tempIdTest;
            string tempObjectUri = MainWindow._context.Normativ
.Where(i => i.idTest == idTest).Single().uri;
            Proc(tempObjectUri);
        }

        void Proc(string uri)
        {
            process = new Process();
            process.StartInfo.FileName = $"{Properties.Settings.Default.DirectNormativ}{uri}";
            //process.StartInfo.Arguments = "-parentHWND " + panel1.Handle.ToInt32() + " " + Environment.CommandLine;
            //process.StartInfo.UseShellExecute = true;
            //process.StartInfo.CreateNoWindow = true;
            //Size();
            process.Start();

            // process.WaitForInputIdle();
            // Doesn't work for some reason ?!
            //unityHWND = process.MainWindowHandle;
            // EnumChildWindows(panel1.Handle, WindowEnum, IntPtr.Zero);
            new Thread(() => DisplayProcessStatus(process, uri)).Start();
        }



        // System.Windows.SystemParameters.PrimaryScreenWidth
        private void Size()
        {
            //MoveWindow(unityHWND, 0, 0, SystemParameters.PrimaryScreenWidth - this.Width, SystemParameters.WorkArea.Bottom - this.Height, true);
        }

        private int WindowEnum(IntPtr hwnd, IntPtr lparam)
        {
            unityHWND = hwnd;
            return 0;
        }

        public static void ExitG()
        {
            DateTime date = DateTime.Now;
            MainWindow._context.StateTest.Add(new StateTest()
            {
                data = date.ToString("dd.MM.yyyy"),
                nameTest = nameTeast,
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

        public void DisplayProcessStatus(Process process,string lern)
        {
            bool activ = true;
            while (activ)
            {
                process.Refresh();

                if (process.HasExited)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        activ = false;
                        Close();
                        try
                        {
                            process.CloseMainWindow();
                            Thread.Sleep(1000);
                            while (!process.HasExited)
                            {
                                activ = false;
                                process.Kill();
                            }
                                
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
