using CefSharp.DevTools.Database;
using LinqToDB.Remote;
using Microsoft.EntityFrameworkCore;
using Project.net;
using Project.net.Class;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project
{
    public partial class MainWindow : Window
    {
        public static readonly MobileContext _context =
         new MobileContext();
        public static string sp;
        public static int idUser;
        public static string nameUser;
        public static string special;
        public static int rota;
        public static int vzvod;


        public MainWindow()
        {
            InitializeComponent();
        }


        string[] digits;

        public void ComboboxFIO()
        {
            if (Pod.SelectedItem != null)
            {
                digits = Regex.Split(Pod.SelectedItem.ToString(), @"\D+");
                rota = int.Parse(digits[0].ToString());
                vzvod = int.Parse(digits[1].ToString());
                FIO.ItemsSource = _context.User.Where(i => i.rota == rota && i.vzvod == vzvod).Select(s => $"{s.famile} {s.name} {s.otch}").ToList();
            }
        }


        public void ComboboxPod()
        {
            string temp = _context.Subdivision.Where(p => p.specAll == Spec.SelectedItem.ToString()).Select(s => s.specShort).Distinct().ToList().FirstOrDefault();
            Pod.ItemsSource = _context.User.Where(i => i.specal == temp).Select(s => $"{s.rota} Рота {s.vzvod} Взвод").Distinct().ToList();
        }

        public void ComboboxNameTest()
        {
            int temp = _context.Subdivision.Where(p => p.specAll == specNoAut.SelectedItem.ToString()).Select(s => s.id).Distinct().ToList().FirstOrDefault();
            testNoAut.ItemsSource = _context.Test.Where(n => n.idSpecl == temp || n.idSpecl == 0).Select(s => s.name).ToList();
        }


        public static void DBConnect()
        {

            try
            {
                /*_context.ActionsAll.Load();
                _context.User.Load();
                _context.ButtonGen.Load();
                _context.Question.Load();
                _context.Test.Load();
                _context.Subdivision.Load();
                _context.Library.Load();
                _context.Exercises.Load();
                _context.StateTest.Load();
                _context.Normativ.Load();*/

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            };
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Server.ExitConnect();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (Spec.SelectedItem != null && Pod.SelectedItem != null && FIO.SelectedItem != null)
            {
                string[] sentences = FIO.SelectedItem.ToString().Split(' ');

                idUser = _context.User.Where(n => n.famile == sentences[0].ToString() && n.name == sentences[1].ToString() && n.famile == sentences[2].ToString()).Select(s => s.id).FirstOrDefault();
                nameUser = FIO.SelectedItem.ToString();
                special = Spec.SelectedItem.ToString();

                new UserMenu(FIO.SelectedItem.ToString()).Show();
                Close();
            }
            else if (specNoAut.SelectedItem != null && testNoAut.SelectedItem != null && familyNoAut.Text != null)
            {
                int tempId = _context.Test
                                                .Where(s => s.name == testNoAut.SelectedItem.ToString())
                                                .Select(v => v.id).FirstOrDefault();

                string temp = _context.Test.Where(s => s.id == tempId).Select(n => n.name).FirstOrDefault();
                string[] subs = temp.Split(' ');
                special = specNoAut.SelectedItem.ToString();
                nameUser = familyNoAut.Text;
                if (subs[0] == "Норматив")
                {
                    new NormativForm(tempId).Show();
                }
                else
                {
                    new TestLesson(tempId).Show();
                    Close();
                }


            }
            else
            {
                MessageBox.Show("Выберите пользователя");
            }
        }

        private void ProjectMenu_Click(object sender, RoutedEventArgs e)
        {
            new ProjectMenu().Show();
            Close();
        }


        private void FIO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (FIO.SelectedItem == null && FIO.IsDropDownOpen == false)
            {
                LabFio.Visibility = Visibility.Visible;
            }
            else
            {
                LabFio.Visibility = Visibility.Collapsed;
            }
            ChckButton();

        }

        private void Pod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Pod.SelectedItem == null && Pod.IsDropDownOpen == false)
                {
                    LabPod.Visibility = Visibility.Visible;
                }
                else
                {
                    LabPod.Visibility = Visibility.Collapsed;
                }
                FIO.IsEnabled = true;

                ComboboxFIO();
                ChckButton();
            }
            catch (Exception)
            {

            }

        }

        private void Spec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Spec.SelectedItem == null && Spec.IsDropDownOpen == false)
            {
                LabSpec.Visibility = Visibility.Visible;
            }
            else
            {
                LabSpec.Visibility = Visibility.Collapsed;
            }
            ComboboxPod();
            Pod.IsEnabled = true;
            ChckButton();
        }



        public void ChckButton()
        {
            if (Spec.SelectedItem != null && Pod.SelectedItem != null && FIO.SelectedItem != null)
            {
                Start.IsEnabled = true;
            }
            else if (specNoAut.SelectedItem != null && testNoAut.SelectedItem != null && familyNoAut.Text != null)
            {
                Start.IsEnabled = true;
            }
            else
            {
                Start.IsEnabled = false;
            }

        }

        private void familyNoAut_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (familyNoAut.Text != null)
            {
                labFamilyNoAut.Visibility = Visibility.Collapsed;
                ChckButton();
            }
            else
            {
                labFamilyNoAut.Visibility = Visibility.Visible;
            }
        }

        private void specNoAut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (specNoAut.SelectedItem != null)
            {
                testNoAut.IsEnabled = true;
                labSpecyNoAut.Visibility = Visibility.Collapsed;
                ComboboxNameTest();
                ChckButton();
            }
            else
            {
                labSpecyNoAut.Visibility = Visibility.Visible;
                testNoAut.IsEnabled = false;
            }
        }

        private void testNoAut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (testNoAut.SelectedItem != null)
            {
                testbPodNoAut.Visibility = Visibility.Collapsed;
                ChckButton();
            }
            else
            {
                testbPodNoAut.Visibility = Visibility.Visible;
            }
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            new Statist().Show();
        }

        private void specNoAut_MouseEnter(object sender, MouseEventArgs e)
        {
            labSpecyNoAut.Visibility = Visibility.Collapsed;
        }


        private void specNoAut_MouseLeave(object sender, MouseEventArgs e)
        {
            if (specNoAut.SelectedItem != null)
            {
                labSpecyNoAut.Visibility = Visibility.Collapsed;
            }
            else
            {
                labSpecyNoAut.Visibility = Visibility.Visible;
            }
        }

        private void familyNoAut_MouseEnter(object sender, MouseEventArgs e)
        {
            labFamilyNoAut.Visibility = Visibility.Collapsed;
        }

        private void familyNoAut_MouseLeave(object sender, MouseEventArgs e)
        {
            if (familyNoAut.Text != null)
            {
                labFamilyNoAut.Visibility = Visibility.Collapsed;
                ChckButton();
            }
            else
            {
                labFamilyNoAut.Visibility = Visibility.Visible;
            }

            if (familyNoAut.IsFocused == false)
            {
                labFamilyNoAut.Visibility = Visibility.Visible;
            }
        }

        private void testNoAut_MouseEnter(object sender, MouseEventArgs e)
        {
            testbPodNoAut.Visibility = Visibility.Collapsed;
        }


        private void testNoAut_MouseLeave(object sender, MouseEventArgs e)
        {
            if (testNoAut.SelectedItem != null)
            {
                testbPodNoAut.Visibility = Visibility.Collapsed;
                ChckButton();
            }
            else
            {
                testbPodNoAut.Visibility = Visibility.Visible;
            }
        }

        private void specNoAut_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                specNoAut.ItemsSource = _context.Subdivision.Where(s => s.rota == 0 && s.vzvod == 0).Select(s => s.specAll).Distinct().ToList();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void Spec_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Spec.ItemsSource = _context.Subdivision.Where(s => s.rota != 0 && s.vzvod != 0).Select(s => s.specAll).Distinct().ToList();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
        }

        private void familyNoAut_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!familyNoAut.IsFocused)
            {
                labFamilyNoAut.Visibility = Visibility.Visible;
            }
        }
    }
}



