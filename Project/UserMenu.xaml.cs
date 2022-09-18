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
    /// Логика взаимодействия для UserMenu.xaml
    /// </summary>
    public partial class UserMenu : Window
    {
        public UserMenu(string temp)
        {
            InitializeComponent();
            nameLable.Content = temp;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Lessen_Click(object sender, RoutedEventArgs e)
        {
            new Lessen().Show();
            Close();
        }


        private void LibrButt_Click(object sender, RoutedEventArgs e)
        {          
            new Libry().Show();
            Close();
        }
    }
}
