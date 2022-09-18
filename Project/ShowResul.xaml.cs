using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для ShowResul.xaml
    /// </summary>
    public partial class ShowResul : Window
    {
        public ShowResul()
        {
            InitializeComponent();
        }

        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.DBConnect();
            textGrady.Content = $"Оценка: {TestLesson.gradeTes}";
            textError.Content = $"Количество ошибок: {TestLesson.idQustError.Count()}";
            textTime.Content = $"Время выполнения: {TestLesson.timeDb.Minutes}:{TestLesson.timeDb.Seconds}";
        }

        private void nextErrorTable_Click(object sender, RoutedEventArgs e)
        {
            new ErrorTest(TestLesson.idQustError).Show();
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }
    }
}
