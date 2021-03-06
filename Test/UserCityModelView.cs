﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Test
{
    class UserCityModelView : ViewModelBase
    {

        public UserCityModelView(int X,Window T)
        {
            ThisWindow = T;
            ADDNEWCITY = new RelayCommand(addCity);
            MODIFYCITY = new RelayCommand(modifyCity);
            DELETECITY = new RelayCommand(deleteCity);
            EMPTYCITY = new RelayCommand(EmptyCity);
            EMPTYUSER = new RelayCommand(emptyUser);
            ADDNEWUSER = new RelayCommand(newUSer);
            MODIFYUSER = new RelayCommand(modifyUser);
            DELETEUSER = new RelayCommand(deleteUser);
            Disconnect = new RelayCommand(disconnect);
            switch (X)
            {
                case 2: IsSec = "Visible"; break;
                case 4: IsDoctor = "Visible"; IsSec = "Visible"; break;
                case 3: IsPatient = "Visible"; break;
                default: IsAdmin = "Visible"; IsDoctor = "Visible"; IsSec = "Visible"; break;
            }
        }

        private List<CitySet> Cities;
        private List<UserSet> Users;
        private CitySet SelectedCity;
        private UserSet SelectedUser;
        private String CityName;
        private String CityPostalCode;
        private String Login;
        private String Pass;
        private Boolean Sadmin;
        private String Profile;
        HealthCareEntities3 ctx = new HealthCareEntities3();
        public List<CitySet> Cities1
        {
            get
            {
                Cities = ctx.CitySets.ToList();
                return Cities;
            }

            set
            {
                Cities = value;
                RaisePropertyChanged("Cities1");
            }
        }

        public List<UserSet> Users1
        {
            get
            {
                Users = ctx.UserSets.ToList();
                return Users;
            }

            set
            {
                Users = value;
                RaisePropertyChanged("Users1");
            }
        }

        public CitySet SelectedCity1
        {
            get
            {
                return SelectedCity;
            }

            set
            {
                CityName1 = value.name;
                CityPostalCode1 = value.postalCode;
                
                SelectedCity = value;
                RaisePropertyChanged("SelectedCity1");
            }
        }

        private String isDoctor = "Hidden";
        private String isSec = "Hidden";
        private String isAdmin = "Hidden";
        private String isPatient = "Hidden";
        public string IsDoctor
        {
            get
            {
                return isDoctor;
            }

            set
            {
                isDoctor = value;
            }
        }

        public string IsSec
        {
            get
            {
                return isSec;
            }

            set
            {
                isSec = value;
            }
        }

        public string IsAdmin
        {
            get
            {
                return isAdmin;
            }

            set
            {
                isAdmin = value;
            }
        }

        public string IsPatient
        {
            get
            {
                return isPatient;
            }

            set
            {
                isPatient = value;
            }
        }


        public UserSet SelectedUser1
        {
            get
            {
                return SelectedUser;
            }

            set
            {
                SelectedUser = value;
                if (value != null)
                {
                    Login1 = value.login;
                    Pass1 = value.password;
                    Sadmin1 = value.IsSuperUser;
                    Profile1 = UTOS(value.profile);
                }
                RaisePropertyChanged("SelectedUser1");
            }
        }

        public string CityName1
        {
            get
            {
                return CityName;
            }

            set
            {
                CityName = value;
                RaisePropertyChanged("CityName1");
            }
        }

        public string CityPostalCode1
        {
            get
            {
                return CityPostalCode;
            }

            set
            {
                CityPostalCode = value;
                RaisePropertyChanged("CityPostalCode1");
            }
        }

        public string Login1
        {
            get
            {
                return Login;
            }

            set
            {
                Login = value;
                RaisePropertyChanged("Login1");
            }
        }

        public string Pass1
        {
            get
            {
                return Pass;
            }

            set
            {
                Pass = value;
                RaisePropertyChanged("Pass1");
            }
        }

        public bool Sadmin1
        {
            get
            {
                return Sadmin;
            }

            set
            {
                Sadmin = value;
                RaisePropertyChanged("Sadmin1");
            }
        }

        public RelayCommand ADDNEWCITY { private set; get; }

        public void addCity()
        {
            CitySet city = new CitySet();
            if (CityName1!=null && CityPostalCode1 != null)
            {
                city.name = CityName1;
                city.postalCode = CityPostalCode1;
                city.PatientSets = new List<PatientSet>();

                try
                {
                    ctx.CitySets.Add(city);
                    ctx.SaveChanges();
                    ((MahApps.Metro.Controls.MetroWindow)ThisWindow).ShowMessageAsync("City : " + CityName1, " Adding of new City was successful ");
                    RaisePropertyChanged("Cities1");
                }
                catch (Exception e)
                {

                    ((MahApps.Metro.Controls.MetroWindow)ThisWindow).ShowMessageAsync("City : " + CityName1, " Error while adding a City ");
                }
            }
            else
            {
                ((MahApps.Metro.Controls.MetroWindow)ThisWindow).ShowMessageAsync("Adding a new City ", "Please Verify that all entries were filled");
            }
        }
        public RelayCommand MODIFYCITY { private set; get; }

        public void modifyCity()
        {
            if (SelectedCity1 != null)
            {
                SelectedCity1.name = CityName1;
                SelectedCity1.postalCode = CityPostalCode1;
                try
                {
                    ctx.SaveChanges();
                    ((MahApps.Metro.Controls.MetroWindow)ThisWindow).ShowMessageAsync("City : " + CityName1, " City details modified succesfully ");
                }
                catch (Exception e )
                {

                    ((MahApps.Metro.Controls.MetroWindow)ThisWindow).ShowMessageAsync("City : " + CityName1, " Error while modifying a City ");
                }
            }
            else
            {
                ((MahApps.Metro.Controls.MetroWindow)ThisWindow).ShowMessageAsync("Modifying a  City ", "Please select a city to modify");
            }
        }
        public RelayCommand DELETECITY { private set; get; }
        public void deleteCity()
        {
            if (SelectedCity1 != null)
            {
                try
                {
                    SelectedCity1.PatientSets = null;
                    ctx.CitySets.Remove(SelectedCity1);
                    ctx.SaveChanges();
                    MessageBox.Show("City Deleted");
                }
                catch (Exception e )
                {

                    ((MahApps.Metro.Controls.MetroWindow)ThisWindow).ShowMessageAsync("City : " + CityName1, " Error while deleting a City ");
                }
            }
        }
        public RelayCommand EMPTYCITY { private set; get; }
        public void EmptyCity()
        {
            CityName = null;
            CityPostalCode1 = null;
        }

        public int UTOI(String x)
        {
            switch (x)
            {

                case "Secretary": return 2;
                case "Doctor": return 4;
                case "Patient": return 3;
                default: return 1;
                    
            }


        }
        public String UTOS(int x)
        {
            switch (x)
            {

                case 2 : return  "Secretary";
                case 4: return "Doctor";
                case 3 : return "Patient";
                default: return "admin";

            }


        }

        public RelayCommand ADDNEWUSER { private set; get; }
        public void newUSer()
        {
            if (Login1 !=null && Pass1!= null)
            {
                UserSet user = new UserSet();
                user.login = Login1;
                user.password = Pass1;
                user.IsSuperUser = Sadmin1;
                user.SUperMdp = Pass1;
                user.profile = UTOI(Profile1);
                try
                {
                    ctx.UserSets.Add(user);
                    ctx.SaveChanges();
                    RaisePropertyChanged("Uers1");
                    MessageBox.Show("User "+user.login+"Create");
                }
                catch (Exception e)
                {

                    MessageBox.Show("DataBase Connection Error");
                }
            }
            else
            {
                MessageBox.Show("All entries must be filled");
            }
        }
        
       

        public String Profile1
        {
            get
            {
                return Profile;
            }

            set
            {
                Profile = value;
                RaisePropertyChanged("Profile1");
            }
        }
        public RelayCommand MODIFYUSER { private set; get; }
        public void modifyUser()
        {
            if (SelectedUser1 != null)
            {
                 SelectedUser1.login= Login1 ;
                 SelectedUser1.password = Pass1 ;
                 SelectedUser1.IsSuperUser = Sadmin1 ;
                 SelectedUser1.profile = UTOI(Profile1) ;

                try
                {
                    ctx.SaveChanges();
                    RaisePropertyChanged("SelectedUser1");
                    MessageBox.Show("User Modified");

                }
                catch (Exception e)
                {

                    MessageBox.Show("DataBase Connection Error");
                }
            }
            else
            {
                MessageBox.Show("Select a User to Modify");
            }
        }
        public RelayCommand DELETEUSER { private set; get; }
        public void deleteUser()
        {
            if (SelectedUser1 != null)
            {
                ctx.UserSets.Remove(SelectedUser1);
                try
                {
                    ctx.SaveChanges();
                    RaisePropertyChanged("Users1");
                }
                catch (Exception e)
                {

                    MessageBox.Show("DataBase Connection Error");
                }
            }
            else
            {
                MessageBox.Show("Select a User to Delete");
            }
        }

        public RelayCommand EMPTYUSER { private set; get; }
        public void emptyUser()
        {
            Login1 = null;
            Pass1 = null;
            Sadmin1 = false;
            Profile1 = UTOS(3);
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
                RaisePropertyChanged("Today1");
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
