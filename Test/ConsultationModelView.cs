using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Test
{
    class ConsultationModelView : ViewModelBase
    {

        public ConsultationModelView(int X, Window T)
        {
            ADDNEW = new RelayCommand(NewConsultation);
            MODIFY = new RelayCommand(ModifyConsultation);
            DELETE = new RelayCommand(deleteConsultation);
            EMPTY = new RelayCommand(emptyConsultation);
            SEARCH = new RelayCommand(searching);
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

        HealthCareEntities3 ctx = new HealthCareEntities3();

        private DateTime CtDate = DateTime.Today;
        private float Cost;
        private String Act;
        private ConsultationSet SelectedConsultation;
        private FileSet SelectedFile;
        private List<ConsultationSet> Consultations;
        private DateTime SearchDate = DateTime.Today;
        private Boolean X;


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

        public DateTime CtDate1
        {
            get
            {
                return CtDate;
            }

            set
            {
                CtDate = value;

                RaisePropertyChanged("CtDate1");
            }
        }

        public float Cost1
        {
            get
            {
                return Cost;
            }

            set
            {
                Cost = value;
                RaisePropertyChanged("Cost1");
            }
        }

        public string Act1
        {
            get
            {
                return Act;
            }

            set
            {
                Act = value;
                RaisePropertyChanged("Act1");
            }
        }

        public ConsultationSet SelectedConsultation1
        {
            get
            {
                return SelectedConsultation;
            }

            set
            {
                SelectedConsultation = value;
                if (SelectedConsultation !=null)
                {
                    Act1 = value.actNature;
                    CtDate1 = value.date;
                    Cost1 = (float)value.cost;
                    SelectedFile1 = value.FileSet;
                }
                RaisePropertyChanged("SelectedConsultation1");
            }
        }



        public RelayCommand ADDNEW { private set; get; }

        public void NewConsultation()
        {

            ConsultationSet cts = new ConsultationSet();
            if (Act1 != null && CtDate1 != null && Cost1 != 0 && SelectedFile1 != null)
            {
                cts.actNature = Act1;
                cts.cost = Cost1;
                cts.date = CtDate1;
                cts.FileSet = SelectedFile1;
                try
                {
                    ctx.ConsultationSets.Add(cts);
                    ctx.SaveChanges();
                    this.X = true;
                    searching();
                    this.X = false;
                    MessageBox.Show("Consultation Added"+ Files.Count);
                    
                    RaisePropertyChanged("Files");
                   
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
           eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Entries can not be empty");
            }
                
            
        }

        public RelayCommand MODIFY { private set; get; }
        public void ModifyConsultation()
        {
            if (SelectedConsultation1 != null)
            {

                SelectedConsultation1.actNature = Act1;
                SelectedConsultation1.cost = Cost1;
                SelectedConsultation1.date = CtDate1;
                SelectedConsultation1.FileSet = SelectedFile1;

                try
                {

                    ctx.SaveChanges();
                    this.X = true;
                    searching();
                    this.X = false;
                }
                catch (Exception e)
                {

                    MessageBox.Show("Data Error ", e.ToString());
                }
                MessageBox.Show("Consultation Modified");
                RaisePropertyChanged("Consultations1");
            }
            else
            {
                MessageBox.Show("Please select a Consultation To modify");
            }
        }

        public RelayCommand DELETE { private set; get; }
        public void deleteConsultation()
        {
            if (SelectedConsultation1 != null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        SelectedConsultation1.FileSet = null;
                        ctx.ConsultationSets.Remove(SelectedConsultation1);
                        ctx.SaveChanges();
                        this.X = true;
                        searching();
                        this.X = false;
                        RaisePropertyChanged("SelectedConsultation1");
                        RaisePropertyChanged("Consultations1");
                        MessageBox.Show("Consultation Deleted");
                    }
                    catch (Exception e)
                    {

                        MessageBox.Show("Error in Deleting the Consultation  ", e.ToString());
                    }
                }
                else
                {

                }
            }
        }

        public RelayCommand EMPTY { private set; get; }

        public FileSet SelectedFile1
        {
            get
            {
                return SelectedFile;
            }

            set
            {
                SelectedFile = value;
                RaisePropertyChanged("SelectedFile1");
            }
        }

        public List<ConsultationSet> Consultations1
        {
            get
            {
                Consultations = ctx.ConsultationSets.ToList();
                return Consultations;
            }

            set
            {
                Consultations = value;
                RaisePropertyChanged("Consultations1");
            }
        }

        public void emptyConsultation()
        {
            SelectedConsultation = null;
            Act1 = null;
            CtDate1 = DateTime.Today;
            Cost1 = 0;
            RaisePropertyChanged("SelectedConsultation1");
            
        }
        

        public DateTime SearchDate1
        {
            get
            {
                return SearchDate;
            }

            set
            {
                SearchDate = value;
                RaisePropertyChanged("SearchDate1");
            }
        }
        public RelayCommand SEARCH { private set; get; }
        public void searching()
        {
           
            if (X == true)
            {
                try
                {

                    Consultations1 = ctx.ConsultationSets.ToList();
                    RaisePropertyChanged("SearchDate1");
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error in Database Connexion, Please Try again.", e.Message);
                }
            }
            else
            {
                if (SearchDate1 != null)
                {
                    try
                    {

                        Consultations1 = ctx.ConsultationSets.Where(u => u.date == SearchDate1).ToList();
                        RaisePropertyChanged("SearchDate1");
                        RaisePropertyChanged("RDVS1");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error in Database Connexion, Please Try again.", e.Message);
                    }
                }
            }

        }
        public RelayCommand Refresh { get; set; }

        public void REFRESH()
        {
            Consultations = ctx.ConsultationSets.ToList();
            
            RaisePropertyChanged("Consultations1");
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
