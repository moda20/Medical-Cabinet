using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Test
{
    class UserCityModelView : ViewModelBase
    {

        public UserCityModelView()
        {
            ADDNEWCITY = new RelayCommand(addCity);
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

        public UserSet SelectedUser1
        {
            get
            {
                return SelectedUser;
            }

            set
            {
                SelectedUser = value;
                Login1 = value.login;
                Pass1 = value.password;
                Sadmin1 = value.IsSuperUser;
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
                }
                catch (Exception e)
                {

                    MessageBox.Show("Database Connection Error");
                }
            }
            else
            {
                MessageBox.Show("All Entries must be filled");
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
                }
                catch (Exception e )
                {

                    MessageBox.Show("Error in Connecting to Database");
                }
            }
            else
            {
                MessageBox.Show("Please Select a City to Modify");
            }
        }
    }
}
