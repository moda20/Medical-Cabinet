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
    class AddRDVViewModel : ViewModelBase
    {
        HealthCareEntities3 ctx = new HealthCareEntities3();
        public AddRDVViewModel(int X)
        {
            ADDRDV = new RelayCommand(NewRDV);
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
        private DateTime RDVdate;
        private String State;

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

        public string State1
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
            if ( State1 != null && RDVdate1 != null && SelectedPatient1 != null)
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
                    MessageBox.Show("RDV for : " + SelectedPatient1.FirstName + " added.");
                    
                }
                catch (Exception e)
                {
                
                    MessageBox.Show("Databse Connetion Error, PLease Try again  :  "+e.Message);
                }
            }
        }
    }
}
