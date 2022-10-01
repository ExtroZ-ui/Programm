using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для ErrorTest.xaml
    /// </summary>
    public partial class ErrorTest : Window
    {
        private int[] idQustErrors;
        public ErrorTest(int[] idQustError)
        {
            InitializeComponent();
            idQustErrors = idQustError;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void ErrorDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow._context.Question.Load();
            foreach (var item in idQustErrors)
            {
                ErrorDataGrid.Items.Add(MainWindow._context.Question.Local.Where(t => t.id == item));
            }

        }
    }
}
