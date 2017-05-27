using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Test
{
    class AddRDVViewModel : ViewModelBase
    {
        HealthCareEntities3 ctx = new HealthCareEntities3();
        public AddRDVViewModel(int X,Window T)
        {
            ThisWindow = T;
            ADDRDV = new RelayCommand(NewRDV);
            Disconnect = new RelayCommand(disconnect);
            switch (X)
            {
                case 2: IsSec = "Visible"; break;
                case 4: IsDoctor = "Visible"; IsSec = "Visible"; break;
                case 3: IsPatient = "Visible"; break;
                default: IsAdmin = "Visible"; IsDoctor = "Visible"; IsSec = "Visible"; break;
            }
        }

        private List<PatientSet> _Patients;

        public List<PatientSet> Patients
        {
            get
            {
                List<PatientSet> UList = new List<PatientSet>();
                UList = ctx.PatientSets.ToList();
                _Patients = UList;
                return _Patients;

            }
            set
            {
                _Patients = value;
            }
        }

        private PatientSet SelectedPatient;
        private DateTime RDVdate = DateTime.Today;
        private bool State;

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

        public PatientSet SelectedPatient1
        {
            get
            {
                return SelectedPatient;
            }

            set
            {
                SelectedPatient = value;
                RaisePropertyChanged("SelectedPatient1");
            }
        }

        public DateTime RDVdate1
        {
            get
            {
                return RDVdate;
            }

            set
            {
                RDVdate = value;
                RaisePropertyChanged("RDVdate1");
            }
        }

        public bool State1
        {
            get
            {
                return State;
            }

            set
            {
                State = value;
                RaisePropertyChanged("State1");
            }
        }
        public RelayCommand ADDRDV { private set; get; }
        public void NewRDV()
        {
            if (  RDVdate1 != null && SelectedPatient1 != null)
            {
                RDVSet rdv = new RDVSet();
                rdv.date = RDVdate1;
                rdv.Patient_Id = SelectedPatient1.Id;
                rdv.state = State1;
                SelectedPatient1.RDVSets.Add(rdv);
                try
                {
                    ctx.RDVSets.Add(rdv);
                    ctx.SaveChanges();
                    ((MahApps.Metro.Controls.MetroWindow)ThisWindow).ShowMessageAsync("Rendez-Vous of  " + SelectedPatient1.FirstName, " Adding of new Rendez-Vous was successful ");

                }
                catch (Exception e)
                {

                    ((MahApps.Metro.Controls.MetroWindow)ThisWindow).ShowMessageAsync("Rendez-Vous of  " + SelectedPatient1.FirstName, " Error while adding the Rendez-Vous ");
                }
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
