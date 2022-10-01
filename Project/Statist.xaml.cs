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

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void sortDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sortDate.SelectedItem != null)
            {
                try
                {
                    StatDataGrid.ItemsSource = MainWindow._context.StateTest
                                    .Where(s => s.data == sortDate.SelectedItem.ToString())
                                    .Where(s => s.speciality == sortSpec.SelectedItem.ToString())
                                    .Where(s => s.nameTest == sortNameTest.SelectedItem.ToString()).ToList();
                }
                catch
                {
                    try
                    {
                        StatDataGrid.ItemsSource = MainWindow._context.StateTest
                            .Where(s => s.data == sortDate.SelectedItem.ToString())
                            .Where(s => s.speciality == sortSpec.SelectedItem.ToString()).ToList();
                    }
                    catch 
                    {
                        try
                        {
                            StatDataGrid.ItemsSource = MainWindow._context.StateTest
                                .Where(s => s.data == sortDate.SelectedItem.ToString())
                                .Where(s => s.nameTest == sortNameTest.SelectedItem.ToString()).ToList();
                        }
                        catch
                        {
                            StatDataGrid.ItemsSource = MainWindow._context.StateTest.Where(s => s.data == sortDate.SelectedItem.ToString()).ToList();
                        }
                        
                    }
                }
            }

        }

        private void sortSpec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sortSpec.SelectedItem != null)
            {
                try
                {
                    StatDataGrid.ItemsSource = MainWindow._context.StateTest
                                    .Where(s => s.data == sortDate.SelectedItem.ToString())
                                    .Where(s => s.speciality == sortSpec.SelectedItem.ToString())
                                    .Where(s => s.nameTest == sortNameTest.SelectedItem.ToString()).ToList();
                }
                catch
                {
                    try
                    {
                        StatDataGrid.ItemsSource = MainWindow._context.StateTest
                            .Where(s => s.data == sortDate.SelectedItem.ToString())
                            .Where(s => s.speciality == sortSpec.SelectedItem.ToString()).ToList();
                    }
                    catch
                    {
                        try
                        {
                            StatDataGrid.ItemsSource = MainWindow._context.StateTest
                                .Where(s => s.data == sortDate.SelectedItem.ToString())
                                .Where(s => s.nameTest == sortNameTest.SelectedItem.ToString()).ToList();
                        }
                        catch
                        {
                            StatDataGrid.ItemsSource = MainWindow._context.StateTest.Where(s => s.speciality == sortSpec.SelectedItem.ToString()).ToList();
                        }

                    }
                }
            }
        }

        private void sortNameTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sortNameTest.SelectedItem != null)
            {
                try
                {
                    StatDataGrid.ItemsSource = MainWindow._context.StateTest
                                    .Where(s => s.data == sortDate.SelectedItem.ToString())
                                    .Where(s => s.speciality == sortSpec.SelectedItem.ToString())
                                    .Where(s => s.nameTest == sortNameTest.SelectedItem.ToString()).ToList();
                }
                catch
                {
                    try
                    {
                        StatDataGrid.ItemsSource = MainWindow._context.StateTest
                            .Where(s => s.data == sortDate.SelectedItem.ToString())
                            .Where(s => s.speciality == sortSpec.SelectedItem.ToString()).ToList();
                    }
                    catch
                    {
                        try
                        {
                            StatDataGrid.ItemsSource = MainWindow._context.StateTest
                                .Where(s => s.data == sortDate.SelectedItem.ToString())
                                .Where(s => s.nameTest == sortNameTest.SelectedItem.ToString()).ToList();
                        }
                        catch
                        {
                            StatDataGrid.ItemsSource = MainWindow._context.StateTest.Where(s => s.nameTest == sortNameTest.SelectedItem.ToString()).ToList();
                        }

                    }
                }
            }
        }

        private void clearButt_Click(object sender, RoutedEventArgs e)
        {
            sortDate.SelectedItem = null;
            sortSpec.SelectedItem = null;
            sortNameTest.SelectedItem = null;
            StatDataGrid.ItemsSource = MainWindow._context.StateTest.ToList();
            lableData.Visibility = Visibility.Visible;
            lableSpec.Visibility = Visibility.Visible;
            lableNameTest.Visibility = Visibility.Visible;
        }

        private void StatDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            StatDataGrid.ItemsSource = MainWindow._context.StateTest.ToList();
        }

        private void sortDate_Loaded(object sender, RoutedEventArgs e)
        {
            sortDate.ItemsSource = MainWindow._context.StateTest.Select(s => s.data).Distinct().ToList();
        }

        private void sortSpec_Loaded(object sender, RoutedEventArgs e)
        {
            sortSpec.ItemsSource = MainWindow._context.StateTest.Select(s => s.speciality).Distinct().ToList();
        }

        private void sortNameTest_Loaded(object sender, RoutedEventArgs e)
        {
            sortNameTest.ItemsSource = MainWindow._context.StateTest.Select(s => s.nameTest).Distinct().ToList();
        }

        private void sortDate_MouseEnter(object sender, MouseEventArgs e)
        {
            lableData.Visibility = Visibility.Collapsed;
        }

        private void sortDate_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sortDate.SelectedItem != null)
            {
                lableData.Visibility = Visibility.Collapsed;
            }
            else
            {
                lableData.Visibility = Visibility.Visible;
            }
        }

        private void sortSpec_MouseEnter(object sender, MouseEventArgs e)
        {
            lableSpec.Visibility = Visibility.Collapsed;
        }

        private void sortSpec_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sortSpec.SelectedItem != null)
            {
                lableSpec.Visibility = Visibility.Collapsed;
            }
            else
            {
                lableSpec.Visibility = Visibility.Visible;
            }
        }

        private void sortNameTest_MouseEnter(object sender, MouseEventArgs e)
        {
            lableNameTest.Visibility = Visibility.Collapsed;
        }

        private void sortNameTest_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sortNameTest.SelectedItem != null)
            {
                lableNameTest.Visibility = Visibility.Collapsed;
            }
            else
            {
                lableNameTest.Visibility = Visibility.Visible;
            }
        }

    }
}

