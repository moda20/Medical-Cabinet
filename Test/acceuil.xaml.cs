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
    /// Logique d'interaction pour acceuil.xaml
    /// </summary>
    public partial class acceuil : Window
    {
        public acceuil(int X)
        {
            InitializeComponent();
            var ViewModel = new acceuilViewModel(X);
            DataContext = ViewModel;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            fiches X =new fiches();
            X.Show();
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            NewPatients X = new NewPatients();
            X.Show();
            this.Close();
        }

        //Access and update columns during autogeneration
        private void DG1_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
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
                e.Column.Header = "File Id";
                
                (e.Column as DataGridTextColumn).Binding = new Binding("FileSet.Id");
            }
            else if (headername == "CitySet")
            {
                e.Column.Header = "City Name";
                (e.Column as DataGridTextColumn).Binding = new Binding("CitySet.name");
            }
            else if (headername == "RDVSets")
            {
                e.Cancel = true;
               

            }else if (headername == "CitySetId")
            {
                e.Cancel = true;
            }else if (headername == "FileId")
            {
                e.Cancel = true;
                e.Column.Header = "File Id";
                
            } 

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
            if (headername == "PatientSet")
            {
                e.Column.Header = "Patient";
                
                (e.Column as DataGridTextColumn).Binding = new Binding("PatientSet.FirstName");
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
            }
            else if (headername == "ConsultationSets")
            {
                e.Column.Header = "Consultations";
                e.Cancel = true;
            }

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
            rdv rdv = new rdv();
            rdv.ShowDialog();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
            UserCity x = new UserCity();
            x.Show();
        }
    }
}
