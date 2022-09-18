using System;
using System.Data.OleDb;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.IO;
using System.Linq;
using Project.net;
using Project.net.Class;
using System.Data.Entity;
using System.Runtime.InteropServices;
using System.Threading;

namespace Project
{
    /// <summary>
    /// Логика взаимодействия для Performance.xaml
    /// </summary>
    public partial class Performance : Window
    {

        [DllImport("User32.dll")]
        static extern bool MoveWindow(IntPtr handle, int x, int y, int width, int height, bool redraw);

        internal delegate int WindowEnumProc(IntPtr hwnd, IntPtr lparam);
        [DllImport("user32.dll")]
        internal static extern bool EnumChildWindows(IntPtr hwnd, WindowEnumProc func, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        private Process process;
        private IntPtr unityHWND = IntPtr.Zero;

        private const int WM_ACTIVATE = 0x0006;
        private readonly IntPtr WA_ACTIVE = new IntPtr(1);
        private readonly IntPtr WA_INACTIVE = new IntPtr(0);

        public Performance(string nameTema, string nameLesson)
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.DBConnect();

            string uri = "C:/Users/Extro/source/repos/Project/Project/Resources/Literature/";
            webView.LoadUrl($"{uri}{MainWindow._context.ButtonGen.Where(i => i.id == Lessen.tempID).Select(lit => lit.literature).FirstOrDefault()}");
            process = new Process();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string tempObjectUri = MainWindow._context.ButtonGen
                .Where(i => i.id == Lessen.tempID).Select(lit => lit.objectUri).FirstOrDefault();

            if (tempObjectUri != "")
            {
                tab2.Visibility = Visibility.Visible;
                if (tab2.IsSelected == true)
                {
                   
                    process.StartInfo.FileName = $"{tempObjectUri}";
                    process.StartInfo.Arguments = "-parentHWND " + panel1.Handle.ToInt32() + " " + Environment.CommandLine;
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.CreateNoWindow = true;

                    process.Start();

                    process.WaitForInputIdle();
                    // Doesn't work for some reason ?!
                    //unityHWND = process.MainWindowHandle;
                    EnumChildWindows(panel1.Handle, WindowEnum, IntPtr.Zero);
                }
            }
            else
            {
                tab2.Visibility = Visibility.Collapsed;
            }

        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            MoveWindow(unityHWND, 0, 0, panel1.Width, panel1.Height, true);
        }

        private int WindowEnum(IntPtr hwnd, IntPtr lparam)
        {
            unityHWND = hwnd;
            return 0;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            new Lessen().Show();
            Close();
        }

        private void GoLessButton_Click(object sender, RoutedEventArgs e)
        {
            string tempText = MainWindow._context.Exercises.Where(tm => tm.tema == Lessen.numburTema && tm.zanytie == Lessen.numburLess).Select(p => p.type_lesson).FirstOrDefault();

            if (tempText == "печать")
            {
                new Print().Show();
            }
            else
            {
                new TestLesson(86).Show();
            }
            Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
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
