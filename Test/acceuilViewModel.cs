using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using System.Windows;
using System.Globalization;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Test 
{
    class acceuilViewModel : ViewModelBase
    {

        HealthCareEntities3 ctx = new HealthCareEntities3();
        
        public Window ThisWindow ;
        public acceuilViewModel(int X, Window T)
        {
            ThisWindow = T;
            ProfilePassed1 = UTOS(X);
            Searching = new RelayCommand(Search);
            Disconnect = new RelayCommand(disconnect);
            Refresh = new RelayCommand(REFRESH);
            switch (X)
            {
                case 2: IsSec = "Visible"; break;
                case 4: IsDoctor = "Visible"; IsSec = "Visible"; break;
                case 3: IsPatient = "Visible"; break;
                default: IsAdmin = "Visible"; IsDoctor = "Visible"; IsSec = "Visible"; break;  
            }
        }
        public String UTOS(int x)
        {
            switch (x)
            {

                case 2: return "Secretary";
                case 4: return "Doctor";
                case 3: return "Patient";
                default: return "admin";

            }
           
        }
        private String ProfilePassed;
        public List<PatientSet> _Patients;
        public List<PatientSet> Patients
        {
            get
            {
                if (_Patients != null)
                {
                    return _Patients;
                }
                else
                {
                    List<PatientSet> UList = new List<PatientSet>();
                    UList = ctx.PatientSets.ToList();
                    _Patients = UList;
                    return _Patients;
                }

            }
            set
            {
                _Patients = value;
                RaisePropertyChanged("Patients");
            }
        }


        private List<FileSet> _Files;

        public List<FileSet> Files
        {
            get
            {
                if (_Files != null)
                {
                    return _Files;
                }
                else
                {
                    List<FileSet> Listf = new List<FileSet>();
                    Listf = ctx.FileSets.ToList();
                    _Files = Listf;
                    return _Files;
                }

            }
            set
            {
                _Files = value;
                RaisePropertyChanged("Files");
            }
        }
        private DateTime SearchDate = DateTime.Today;
        private String searChkey;
        public DateTime SearchDate1 { get { return SearchDate; } set { SearchDate = value;RaisePropertyChanged("SearchDate1"); } }
        public string SearChkey { get { return searChkey; } set { searChkey = value; RaisePropertyChanged("SearChkey"); } }





        public RelayCommand Searching { private set; get; }

        public string ProfilePassed1
        {
            get
            {
                return ProfilePassed;
            }

            set
            {
                ProfilePassed = value;
            }
        }
        private String isDoctor="Hidden";
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

        public void Search()
        {
            if (SearchDate1 != null || searChkey != null)
            {
                
                Patients = ctx.PatientSets.Where(u => u.LastVisit == SearchDate || u.FirstName == searChkey).ToList();
                List<FileSet> Listf = new List<FileSet>();
                Files = ctx.FileSets.Where(u => u.CreationDate == SearchDate).ToList();
                RaisePropertyChanged("Patients"); RaisePropertyChanged("Files");
                //MessageBox.Show("Number of files found : " +Files.Count+" /  Number of Patients : "+Patients.Count,"Search Results");

            }
        }
        public RelayCommand Refresh { get; set; }

        public void REFRESH()
        {
            _Patients = ctx.PatientSets.ToList();
            _Files = ctx.FileSets.ToList();
            RaisePropertyChanged("Files");
            RaisePropertyChanged("Patients");
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
        public void disconnect() 
        {
            ThisWindow.Close();
            MainWindow x = new MainWindow();
            x.Show();
            MahApps.Metro.Controls.MetroWindow wd = Window.GetWindow(x) as MahApps.Metro.Controls.MetroWindow;
            if (wd != null)
            {
                wd.ShowMessageAsync("You were Disconnected "," You were disconnected and sent back to login Window");
            }
        }

    }
}
