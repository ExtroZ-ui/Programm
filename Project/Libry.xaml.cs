using Project.net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Xml;

namespace Project
{
    /// <summary>
    /// Логика взаимодействия для Libry.xaml
    /// </summary>
    public partial class Libry : Window
    {
        public Libry()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.DBConnect();
            GeneratButtonAll(0);
            GetSpecal();
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            new UserMenu(MainWindow.nameUser).Show();
            Close();

        }

        public void GeneratButtonAll(int temp)
        {
            stackPanel.Children.Clear();
            switch (temp)
            {
                case 0:
                    string[] tempConten = MainWindow._context.Library.Select(s => s.inscription).ToArray();
                    int tempB = 0;
                    for (int i = 0; i <= tempConten.Count() - 1; i++)
                    {

                        Button b = new Button
                        {

                            Height = 25,
                            FontSize = 13,
                            Name = "Button_" + tempB,
                            Content = $"{tempConten[i]}",
                        };
                        tempB++;
                        b.Click += new RoutedEventHandler(ButtonRollClick);
                        stackPanel.Children.Add(b);
                    }
                    break;
                case 1:
                    stackPanel.Children.Clear();
                    List<string> tempContenTwo = MainWindow._context.Library.Where(s => s.specialization == comboboxSpec.SelectedItem.ToString()).Select(s => s.inscription).ToList();


                    int tempBu = 0;
                    for (int i = 0; i <= tempContenTwo.Count() - 1; i++)
                    {


                        Button b = new Button
                        {

                            Height = 25,
                            FontSize = 13,
                            Name = "Button_" + tempBu,
                            Content = $"{tempContenTwo[i]}",
                        };
                        tempBu++;
                        b.Click += new RoutedEventHandler(ButtonRollClick);
                        stackPanel.Children.Add(b);
                    }
                    break;
            }

        }

        void ButtonRollClick(object sender, RoutedEventArgs e)
        {
            Button buttonThatWasClicked = (Button)sender;
             string temp = $"{MainWindow._context.Library.Where(ins => ins.inscription == buttonThatWasClicked.Content.ToString()).Select(ad => ad.adress).FirstOrDefault()}";

            var dir = GetExeDirectory();
            var uri = new Uri(System.IO.Path.Combine(dir,temp));

            webView.LoadUrl(uri.ToString());
        }

        static private string GetExeDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            path = System.IO.Path.GetDirectoryName(path);
            return path;
        }

        public void GetSpecal()
        {
            comboboxSpec.ItemsSource = MainWindow._context.Library.Select(s => s.specialization).Distinct().ToList();
        }

        private void comboboxSpec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboboxSpec.SelectedItem == null && comboboxSpec.IsDropDownOpen == false)
            {
                LabSpec.Visibility = Visibility.Visible;
            }
            else
            {
                LabSpec.Visibility = Visibility.Collapsed;
            }

            MessageBox.Show(comboboxSpec.SelectedItem.ToString());

            if (comboboxSpec.SelectedItem.ToString() == "")
            {

                GeneratButtonAll(0);
            }
            else
            {
                GeneratButtonAll(1);
            }
        }
    }
}
