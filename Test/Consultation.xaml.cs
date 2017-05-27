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
    /// Interaction logic for Consultation.xaml
    /// </summary>
    public partial class Consultation : MetroWindow
    {
        public int Role;
        public Consultation(int X)
        {
            Role = X;
            InitializeComponent();
            var ViewModel = new ConsultationModelView(X,Window.GetWindow(this));
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
            fiches x = new fiches(Role);
            x.Show();
            this.Close();
        }


        private void DG2_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();

            ////Cancel the column you don't want to generate
            //if (headername == "MiddleName")
            //{
            //    e.Cancel = true;
            //}

            //update column details when generating
            if (headername == "FileSet")
            {
                e.Column.Header = "File";

                (e.Column as DataGridTextColumn).Binding = new Binding("FileSet.Id");
            }
            else if (headername == "CitySet")
            {
                e.Column.Header = "City Name";
                (e.Column as DataGridTextColumn).Binding = new Binding("PCity.name");
            }
            else if (headername == "RDVSets")
            {
                e.Cancel = true;


            }
            else if (headername == "CitySetId")
            {
                e.Cancel = true;
            }
            else if (headername == "FileId")
            {
                e.Column.Header = "File Id";
                e.Cancel = true;
            }
            else if (headername == "ConsultationSets")
            {
                e.Column.Header = "Consultations";
                e.Cancel = true;
            }

        }

        private void Admin_Manage_CityUser_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
