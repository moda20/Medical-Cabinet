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
        public int Role;
        public rdv( int X)
        {
            Role = X;
            InitializeComponent();
            var ViewModel = new RDVWiewModel(X);
            DataContext = ViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            
            AddRdv rdv_add = new AddRdv(Role);
            rdv_add.ShowDialog();

        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            NewPatients x = new NewPatients(Role);
            x.Show();
            this.Close();
        }


        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            NewPatients x = new NewPatients(Role);
            x.Show();
            
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Consultation X = new Consultation(Role);
            X.Show();
            this.Close();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
             fiches X = new fiches(Role);
            X.Show();
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


            }
            else if (headername == "CitySetId")
            {
                e.Cancel = true;
            }
            else if (headername == "FileId")
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
            if (headername == "state")
            {
                e.Column.Header = "State";
                
                (e.Column as DataGridTextColumn).Binding = new Binding("state");
            }
            else if (headername == "CitySet")
            {
                e.Column.Header = "City Name";
                (e.Column as DataGridTextColumn).Binding = new Binding("CitySet.name");
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
                e.Cancel = true;
                e.Column.Header = "File Id";

            }

        }
    }
}
