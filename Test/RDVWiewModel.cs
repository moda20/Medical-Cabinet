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
    class RDVWiewModel : ViewModelBase
    {



        public RDVWiewModel()
        {
            MODIFY = new RelayCommand(ModifyEdit);
            DELETE = new RelayCommand(deleteRdv);
            EMPTY = new RelayCommand(emptyRDV);
        }
        private PatientSet SelectedPatient;
        private DateTime RDVDate;
        private List<RDVSet> RDVS;
        private RDVSet SelectedRDV;
        private String State;

        HealthCareEntities3 ctx = new HealthCareEntities3();

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

        public PatientSet SelectedPatient1
        {
            get
            {
                return SelectedPatient;
            }

            set
            {
                SelectedPatient = value;
            }
        }

        public DateTime RDVDate1
        {
            get
            {
                return RDVDate;
            }

            set
            {
                RDVDate = value;
            }
        }

        public List<RDVSet> RDVS1
        {
            get
            {
                RDVS = ctx.RDVSets.ToList();
                return RDVS;
            }

            set
            {
                RDVS = value;
            }
        }

        public RDVSet SelectedRDV1
        {
            get
            {
                return SelectedRDV;
            }

            set
            {
                SelectedRDV = value;
                RDVDate1 = value.date;
                SelectedPatient1 = ctx.PatientSets.Find(value.Patient_Id);
                State1 = value.state;
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

        public RelayCommand MODIFY { private set; get; }
        public void ModifyEdit()
        {
            if (SelectedRDV1 != null)
            {
                SelectedRDV1.state = State1;
                SelectedRDV1.Patient_Id = SelectedPatient1.Id;
                SelectedRDV1.date = RDVDate1;
                try
                {
                    ctx.SaveChanges();
                    RaisePropertyChanged("RDVS1");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error when connecting to Database");
                }
            }
        }

        public RelayCommand DELETE { private set; get; }
        public void deleteRdv()
        {
            if (SelectedRDV1 != null)
            {
                ctx.RDVSets.Remove(SelectedRDV1);

                try
                {
                    ctx.SaveChanges();
                    RaisePropertyChanged("RDVS1");
                }
                catch (Exception e)
                {
                    
                    MessageBox.Show("Error when connecting to Database");
                }
            }
        }
        public RelayCommand EMPTY { private set; get; }
        public void emptyRDV()
        {
            State1 = null;
            SelectedPatient1 = null;
            RDVDate1 = DateTime.Today;
        }
    }
}
