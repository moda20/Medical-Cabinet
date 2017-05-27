using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Navigation;
using System.Data.Entity.Validation;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Globalization;

namespace Test
{
    class MainWindowsViewModel : MetroWindow
    {

        
        public MainWindowsViewModel( Window x)
        {
            ThisWindow = x;
            Connect = new RelayCommand(connect);


        }
        HealthCareEntities3 ctx = new HealthCareEntities3();

        private PatientSet _selectedPatient;

        public PatientSet selectedPatient
        {
            get
            {
                return _selectedPatient;
            }
            set
            {
                _selectedPatient = value;

            }

        }
        private String username;

        public String name
        {
            get { return username; }
            set { username = value; }
        }
        private String password;

        public String pass
        {
            get { return password; }
            set { password = value; }
        }
        public RelayCommand Connect { private set; get; }


        public int Roles(UserSet u)
        {
            switch (u.profile)
            {

                case 1: return 1;
                case 2: return 2;
                case 3: return 3;
                default: return 4;

            }

        }

        public void connect()
        {


            if (pass != null && name != null)
            {
                UserSet USER = new UserSet();
                USER = ctx.UserSets.SingleOrDefault(u => u.login == name);
                if(USER != null)
                {
                    if (USER.password == pass && ( Roles(USER)==1 || Roles(USER) == 2 || Roles(USER) == 3))
                    {
                        acceuil acceuil = new acceuil(USER.profile);
                        acceuil.Show();
                        ThisWindow.Close();
                        MahApps.Metro.Controls.MetroWindow window = Window.GetWindow(acceuil) as MahApps.Metro.Controls.MetroWindow;
                        if (window != null)
                        {
                            DateTime dt = DateTime.Today;
                            string mt = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dt.Month);
                            window.ShowMessageAsync("Hello "+USER.login, "It is ' "+dt.DayOfWeek+" the "+dt.Day+" of "+mt+" '. ");
                            
                        }
                        
                        
                        
                    }
                    else
                    {
                        MessageBox.Show("Wrong Pass! Please verify");
                    }

                }
                else { MessageBox.Show("User Doesn't Exist ! Please verify"); }
            }
            else
            {
                MessageBox.Show("Wrong UserName! Please verify");
            }

        }

        static DateTime dt = DateTime.Today;
        static string mt = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dt.Month);
        private String Today = "" + dt.DayOfWeek + " the " + dt.Day + " of " + mt + "";
        public string Today1
        {
            get
            {
                return Today;
            }

            set
            {
                Today = value;
                
            }
        }
        public RelayCommand Disconnect { private set; get; }
        public Window ThisWindow { get; private set; }

        public void disconnect()
        {
            ThisWindow.Close();
            MainWindow x = new MainWindow();
            x.Show();
            MahApps.Metro.Controls.MetroWindow wd = Window.GetWindow(x) as MahApps.Metro.Controls.MetroWindow;
            if (wd != null)
            {
                wd.ShowMessageAsync("You were Disconnected ", " You were disconnected and sent back to login Window");
            }
        }
    }
    }


