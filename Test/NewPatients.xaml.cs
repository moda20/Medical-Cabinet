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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class NewPatients : Window
    {
        public NewPatients()
        {
            InitializeComponent();
            var ViewModel = new NewPatientsModelView();
            DataContext = ViewModel;
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            acceuil x = new acceuil(2);
            x.Show();
            this.Close();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            rdv x = new rdv();
            x.Show();
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            fiches x = new fiches();
            x.Show();
            this.Close();
        }
    }
}
