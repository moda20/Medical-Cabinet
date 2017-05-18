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
    /// Interaction logic for Consultation.xaml
    /// </summary>
    public partial class Consultation : Window
    {
        public Consultation()
        {
            InitializeComponent();
            var ViewModel = new ConsultationModelView();
            DataContext = ViewModel;
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
