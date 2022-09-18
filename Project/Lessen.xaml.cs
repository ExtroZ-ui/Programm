using Project.net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.OleDb;
using System.Linq;
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
using System.Windows.Shapes;

namespace Project
{
    /// <summary>
    /// Логика взаимодействия для Lessen.xaml
    /// </summary>
    public partial class Lessen : Window
    {
        public static int tempID;  // глобальное id кнопки на которое происходит событие
        public static int numburTema;  // глобальный номер темы
        public static string nameTema;  // глобальный номер темы
        public static int numburLess;  // глобальный номер занятия
        public static string nameLess;  // глобальный номер занятия
        public Lessen()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.DBConnect();
            CreatLesson();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new UserMenu(null).Show();
            Close();
        }


        void ButtonRollClick(object sender, RoutedEventArgs e)
        {
            Button buttonThatWasClicked = (Button)sender;
            string[] digits = Regex.Split(buttonThatWasClicked.Name, @"\D+");
            tempID = Convert.ToInt32(digits[1]);    // запоминает id кнопки на которую пришлось событие

            Descript.Text = MainWindow._context.ButtonGen.Where(s => s.id == int.Parse(digits[1])).Select(n => n.description).FirstOrDefault();

        }


        int temp = 0;
        int gridTemp = 0;
        public void CreatLesson()
        {
                    int colum = MainWindow._context.ButtonGen.Select(p => p.column).Distinct().Count();

                for (int i = 0; i <= colum; i++)
                {
                    LessenGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(200) });

                        int row = MainWindow._context.ButtonGen.Local.Where(s => s.column == i).Select(p => p.row).Count();

                            for (int c = 0; c < row; c++)
                            {
                                string bdNameTem = MainWindow._context.ButtonGen.Where(colRow => colRow.column == i && colRow.row == c).Select(n => n.nameTema).FirstOrDefault();
                                string bdNameLesson = MainWindow._context.ButtonGen.Where(colRow => colRow.column == i && colRow.row == c).Select(n => n.nameLesson).FirstOrDefault();
                                int bdId = MainWindow._context.ButtonGen.Where(colRow => colRow.column == i && colRow.row == c).Select(n => n.id).FirstOrDefault();

                                LessenGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });

                                    if (i <= colum)
                                    {
                                        Button b = new Button
                                        {
                                            Width = 100,
                                            Height = 35,
                                            FontSize = 14,
                                            Name = "Button_" + bdId.ToString(),
                                            Content = $"Т:{bdNameTem} З:{bdNameLesson}",
                                        };
                                        b.Click += new RoutedEventHandler(ButtonRollClick);
                                        temp++;
                                        LessenGrid.Children.Add(b);
                                        Grid.SetRow(b, c);
                                        Grid.SetColumn(b, i);
                                    }
                                    else
                                    {

                                    }
                            }
                    }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void GoLesson_Click(object sender, RoutedEventArgs e)
        {
            numburTema = MainWindow._context.ButtonGen.Where(z => z.id == tempID).Select(n => n.numburTema).FirstOrDefault();
            numburLess = MainWindow._context.ButtonGen.Where(z => z.id == tempID).Select(n => n.numburLesson).FirstOrDefault();

            string bdNameTem = MainWindow._context.ButtonGen.Where(colRow => colRow.id == tempID).Select(n => n.nameTema).ToString();
            string bdNameLesson = MainWindow._context.ButtonGen.Where(colRow => colRow.id == tempID).Select(n => n.nameLesson).ToString();

                nameTema = bdNameTem;
                nameLess = bdNameLesson;

                new Performance(bdNameTem, bdNameLesson).Show();
                Close();
        }
    } 

    }
