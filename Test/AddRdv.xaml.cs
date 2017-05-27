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
    /// Interaction logic for AddRdv.xaml
    /// </summary>
    public partial class AddRdv : MetroWindow
    {
        public int Role;
        public AddRdv(int X)
        {
            Role = X;
            InitializeComponent();
            var ViewModel = new AddRDVViewModel(X,Window.GetWindow(this));
            DataContext = ViewModel;
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
