using MahApps.Metro.Controls;
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
    /// Logique d'interaction pour fiches.xaml
    /// </summary>
    public partial class fiches : MetroWindow
    {
        public int Role;
        public fiches(int X)
        {
            Role = X;
            InitializeComponent();
            var ViewModel = new ficheModelView(X, Window.GetWindow(this));
            DataContext = ViewModel;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            acceuil x = new acceuil(Role);
            x.Show();
            this.Close();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            rdv x = new rdv(Role);
            x.Show();
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            NewPatients x = new NewPatients(Role);
            x.Show();
            this.Close();
        }
        private void DG1_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();

            ////Cancel the column you don't want to generate
            //if (headername == "MiddleName")
            //{
            //    e.Cancel = true;
            //}

            //update column details when generating
            if (headername == "PatientSet")
            {
                e.Column.Header = "Patient";
                (e.Column as DataGridTextColumn).Binding = new Binding("PatientSet.FirstName");

            }
        }
    }
}
