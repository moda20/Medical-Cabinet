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



        public RDVWiewModel(int X)
        {
            MODIFY = new RelayCommand(ModifyEdit);
            DELETE = new RelayCommand(deleteRdv);
            EMPTY = new RelayCommand(emptyRDV);

            switch (X)
            {
                case 2: IsSec = "Visible"; break;
                case 4: IsDoctor = "Visible"; IsSec = "Visible"; break;
                case 3: IsPatient = "Visible"; break;
                default: IsAdmin = "Visible"; IsDoctor = "Visible"; IsSec = "Visible"; break;
            }

        }
        private PatientSet SelectedPatient;
        private DateTime RDVDate;
        private List<RDVSet> RDVS;
        private RDVSet SelectedRDV;
        private String State;

        HealthCareEntities3 ctx = new HealthCareEntities3();

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

        public DateTime RDVDate1
        {
            get
            {
                return RDVDate;
            }

            set
            {
                RDVDate = value;
                RaisePropertyChanged("RDVDate1");
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
                RaisePropertyChanged("RDVS1");
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
                RaisePropertyChanged("SelectedRDV1");
                
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
