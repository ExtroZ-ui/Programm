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
            new Thread(() => DisplayProcessStatus(process)).Start();
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

        private void ComboBoxNormativ_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxNormativ.ItemsSource = MainWindow._context.Normativ.Select(n => n.name).ToList();
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            string nameObjec = MainWindow._context.Normativ
    .Where(i => i.idTest == idTest).Select(lit => lit.name).FirstOrDefault();
            if (ComboBoxNormativ.SelectedItem.ToString() == nameObjec)
            {
                string tempObjectUri = MainWindow._context.Normativ
    .Where(i => i.name == nameObjec).Select(lit => lit.uri).FirstOrDefault();
                Proc(tempObjectUri);
            }
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridStat.ItemsSource = MainWindow._context.StateTest
    .Where(i => i.familiy == MainWindow.nameUser).ToList();
        }
    }
}
