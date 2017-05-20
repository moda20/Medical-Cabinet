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

namespace Test
{
    class MainWindowsViewModel
    {


        public MainWindowsViewModel()
        {
           
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
                        
                        MessageBox.Show("Connected");
                        acceuil acceuil = new acceuil(USER.profile);
                        acceuil.Show();
  
                        this.Close();
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

        private void Close()
        {

        }
    }
    }


