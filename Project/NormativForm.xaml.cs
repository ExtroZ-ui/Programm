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
        public static string timeNorm;
        public NormativForm(int tempIdTest)
        {
            InitializeComponent();
            idTest = tempIdTest;
            process = new Process();
            Proc();


        }

       async void Proc()
        {
            string tempObjectUri = MainWindow._context.Normativ
                .Where(i => i.idTest == idTest).Select(lit => lit.uri).FirstOrDefault();
            process.StartInfo.FileName = $"{Properties.Settings.Default.DirectNormativ}{tempObjectUri}";           
            //process.StartInfo.Arguments = "-parentHWND " + panel1.Handle.ToInt32() + " " + Environment.CommandLine;
            //process.StartInfo.UseShellExecute = true;
            //process.StartInfo.CreateNoWindow = true;
            //Size();
            process.Start(); 
            
            // process.WaitForInputIdle();
            // Doesn't work for some reason ?!
            //unityHWND = process.MainWindowHandle;
            // EnumChildWindows(panel1.Handle, WindowEnum, IntPtr.Zero);
            new Thread(() => DisplayProcessStatus(process)).Start();
            await DoStuff();
        }

        private async Task DoStuff()
        {
            var progress = new Progress<double>(ReportProgress);
            string result = await Task<string>.Factory.StartNew(() => LongMethod(progress));
            Debug.WriteLine(result);
        }

        private string LongMethod(IProgress<double> progress)
        {
            Random random = new Random();
            var list = new List<string>();
            int cap = 20000;

            for (int i = 0; i < cap; i++)
            {
                int randomNumber = random.Next(0, cap);
                list.Add(randomNumber.ToString());

                // Overload operations here
                var hashSet = new HashSet<string>(list);
                list = hashSet.ToList<string>();

                progress.Report(ConvertToPercentage(i, cap));
            }

            int randomIndex = random.Next(0, list.Count);
            return list[randomIndex];
        }

        private void ReportProgress(double value)
        {
            progress.Value = value;
        }

        private double ConvertToPercentage(int value, int count)
        {
            return (double)value * 100 / count;
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

        private void ExitG()
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

        public void DisplayProcessStatus(Process process)
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
                        Close();
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ExitG();
            new MainWindow().Show();
            Close();
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

        }
    }
}
