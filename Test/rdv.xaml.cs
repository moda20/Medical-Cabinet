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

namespace Test
{
    /// <summary>
    /// Interaction logic for rdv.xaml
    /// </summary>
    public partial class rdv : Window
    {
        public rdv()
        {
            InitializeComponent();
            var ViewModel = new RDVWiewModel();
            DataContext = ViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            
            AddRdv rdv_add = new AddRdv();
            rdv_add.ShowDialog();

        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            acceuil x = new acceuil();
            x.Show();
            this.Close();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            rdv x = new rdv();
            x.Show();
            
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            NewPatients x = new NewPatients();
            x.Show();
            
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {

        }
    }
}
