using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для Statist.xaml
    /// </summary>
    public partial class Statist : Window
    {
        public Statist()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StatDataGrid.ItemsSource = MainWindow._context.StateTest.ToList();
            sortDate.ItemsSource = MainWindow._context.StateTest.Select(s => s.data).Distinct().ToList();
            sortSpec.ItemsSource = MainWindow._context.StateTest.Select(s => s.speciality).Distinct().ToList();
            sortNameTest.ItemsSource = MainWindow._context.StateTest.Select(s => s.nameTest).Distinct().ToList();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            //new MainWindow().Show();
            Close();
        }


        private void sortDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {         
            StatDataGrid.ItemsSource = MainWindow._context.StateTest.Where(s => s.data == sortDate.SelectedItem.ToString()).ToList();
        }

        private void sortSpec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {           
            StatDataGrid.ItemsSource = MainWindow._context.StateTest.Where(s => s.speciality == sortSpec.SelectedItem.ToString()).ToList();          
        }

        private void sortNameTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StatDataGrid.ItemsSource = MainWindow._context.StateTest.Where(s => s.nameTest == sortNameTest.SelectedItem.ToString()).ToList();           
        }

        private void clearButt_Click(object sender, RoutedEventArgs e)
        {
            StatDataGrid.ItemsSource = MainWindow._context.StateTest.ToList();
        }
    }
}

