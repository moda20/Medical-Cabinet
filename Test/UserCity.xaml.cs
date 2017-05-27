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
    /// Interaction logic for UserCity.xaml
    /// </summary>
    public partial class UserCity : MetroWindow
    {
        public int Role;
        public UserCity(int X)
        {
            Role = X;
            InitializeComponent();
            var ViewModel = new UserCityModelView(X,Window.GetWindow(this));
            DataContext = ViewModel;
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            acceuil X = new acceuil(Role);
            X.Show();
            this.Close();
        }
    }
}
