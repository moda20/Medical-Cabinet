using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using System.Data.Entity.Validation;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using System.Globalization;

namespace Test
{
    public class NewPatientsModelView : ViewModelBase
    {
        
        public NewPatientsModelView(int X, Window T)
        {
            ThisWindow = T;
            ADDNEW = new RelayCommand(NewPatient);
            MODIFY = new RelayCommand(modifyPatient);
            DELETE = new RelayCommand(deletePatient);
            EMPTY = new RelayCommand(emptyPatient);
            SEARCH = new RelayCommand(Searching);
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


        private String PName;
        private String PLastName;
        private DateTime PDateOfBirth=DateTime.Today;
        private DateTime PLASTVISIT = DateTime.Today;
        private String POccupation;
        private String PCellPhone;
        private String PAddress;
        private FileSet PFile;
        private CitySet PCity;
        private PatientSet SelectedPatient;
        private DateTime SearchDate = DateTime.Today;
        private Boolean X = false;

        HealthCareEntities3 ctx = new HealthCareEntities3();

        private List<PatientSet> _Patients;

        public List<PatientSet> Patients
        {
            get
            {
                if (_Patients !=null)
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
                    Listf = ctx.FileSets.Where(u =>u.PatientSet==null).ToList();
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

        private List<CitySet> _Cities;
        private int PatientId;

        public List<CitySet> Cities
        {
            get
            {
                List<CitySet> Listf = new List<CitySet>();
                Listf = ctx.CitySets.ToList();
                _Cities = Listf;
                return _Cities;

            }
            set
            {
                _Cities = value;
            }
        }

        public string PName1 { get { return PName; } set { PName = value; RaisePropertyChanged("PName1"); } }
        public string PLastName1 { get { return PLastName; } set { PLastName = value; RaisePropertyChanged("PLastName1"); } }
        public DateTime PDateOfBirth1 { get { return PDateOfBirth; } set { PDateOfBirth = value; RaisePropertyChanged("PDateOfBirth1"); } }
        public DateTime PLASTVISIT1 { get { return PLASTVISIT; } set { PLASTVISIT = value; RaisePropertyChanged("PLASTVISIT1"); } }
        public string POccupation1 { get { return POccupation; } set { POccupation = value; RaisePropertyChanged("POccupation1"); } }
        public string PCellPhone1
        {
            get { return PCellPhone; }
            set { PCellPhone = value; RaisePropertyChanged("PCellPhone1"); }
        }
        public string PAddress1
        {
            get { return PAddress; }
            set { PAddress = value; RaisePropertyChanged("PAddress1"); }
        }


        
        public FileSet PFile1 { get { return PFile; } set { PFile = value; RaisePropertyChanged("PFile1"); } }
        public CitySet PCity1 { get { return PCity; } set { PCity = value;
                RaisePropertyChanged("PCity1");
            } }
        public PatientSet SelectedPatient1
        {
            get { return SelectedPatient; }
            set
            {

      
                if ( value!=null)
                {
                    PName1 = value.FirstName;
                    PLastName1 = value.LastName;
                    PLASTVISIT1 = (DateTime)value.LastVisit;
                    PDateOfBirth1 = value.BirthDate;
                    _Files = null;
                    RaisePropertyChanged("Files");
                    _Files.Add(value.FileSet);
                    RaisePropertyChanged("Files");
                    PFile1 = value.FileSet;
                    PCity1 = value.CitySet;
                    PCellPhone1 = value.CellPhone;
                    PAddress1 = value.Address;
                    POccupation1 = value.Occupation;
                    PatientId1 = value.Id;
                    
                }
                SelectedPatient = value;
                RaisePropertyChanged("SelectedPatient1");
                
            }
        }

        public int PatientId1
        {
            get
            {
                return PatientId;
            }

            set
            {
                PatientId = value;
                RaisePropertyChanged("PatientId1");
            }
        }
        public RelayCommand ADDNEW { private set; get; }

        public void NewPatient()
        {
            PatientSet user = new PatientSet();
            user.Address = PAddress1;
            user.BirthDate = PDateOfBirth1;
            user.LastName = PLastName1;
            user.FirstName = PName1;
            user.Occupation = POccupation1;
            user.CellPhone = PCellPhone1;
            user.CitySet = PCity1;
            user.FileSet = PFile1;
            user.LastVisit = PLASTVISIT1;
            user.FileSet = PFile1;


            user.RDVSets = new List<RDVSet>(); 

            if (user.Address != null &&
            user.BirthDate != null &&
            user.LastName != null &&
            user.FirstName != null &&
            user.Occupation != null &&
            user.CellPhone != null &&

            user.RDVSets != null)
            {
                try
                {
                    ctx.PatientSets.Add(user);
                    ctx.SaveChanges();
                    this.X = true;
                    Searching();
                    this.X = false;
                    MessageBox.Show("Patient Added");
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
        }

        public RelayCommand MODIFY { private set; get; }
        public void modifyPatient()
        {
            if (SelectedPatient1 != null)
            {
            
                SelectedPatient1.RDVSets = SelectedPatient.RDVSets;
                SelectedPatient1.Address = PAddress1;
                SelectedPatient1.BirthDate = PDateOfBirth1;
                SelectedPatient1.LastName = PLastName1;
                SelectedPatient1.FirstName = PName1;
                SelectedPatient1.Occupation = POccupation1;
                SelectedPatient1.CellPhone = PCellPhone1;
                SelectedPatient1.CitySet = PCity1;
                SelectedPatient1.FileSet = PFile1;
                SelectedPatient1.LastVisit = PLASTVISIT1;
                SelectedPatient1.Id = PatientId1;
               
                try
                {
                    ctx.SaveChanges();
                    this.X = true;
                    Searching();
                    this.X = false;
                }
                catch (Exception e)
                {

                    MessageBox.Show("Data Error ", e.ToString());
                }
                MessageBox.Show("Patient Modified");
                RaisePropertyChanged("Patients");
            }
            else
            {
                MessageBox.Show("Please select a Patient To modify");
            }
        }

        public RelayCommand DELETE { private set; get; }
        public void deletePatient()
        {
            if (SelectedPatient1 != null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    try
                    {

                        if (SelectedPatient1.FileSet !=  null)
                        {
                            ctx.FileSets.Remove(SelectedPatient1.FileSet);
                        }
                        ctx.RDVSets.RemoveRange(SelectedPatient1.RDVSets);
                        ctx.PatientSets.Remove(SelectedPatient);
                        ctx.SaveChanges();
                        this.X = true;
                        Searching();
                        this.X = false;
                        RaisePropertyChanged("SelectedPatient1");
                        RaisePropertyChanged("Patients");
                        MessageBox.Show("Patient Deleted/ Patient's File Deleted");
                }
                    catch (Exception e)
                {

                    MessageBox.Show("Error in Deleting the Patient  "+ e.ToString());
                }
            }
                else
                {
                    
                }
            }
        }

        public RelayCommand EMPTY { private set; get; }


        public void emptyPatient()
        {
            PName1 = null;
            PLastName1 = null;
            PLASTVISIT1 = DateTime.Today;
            PDateOfBirth1 = DateTime.Today;
            PFile1 = null;
            PCity1 = null;
            PCellPhone1 = null;
            PAddress1 = null;
            POccupation1 = null;
            SelectedPatient = null;
            _Files = null;
            RaisePropertyChanged("Files");
            RaisePropertyChanged("SelectedPatient1");
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
        public void Searching()
        {
            if (X == true)
            {
                try
                {

                    Patients = ctx.PatientSets.ToList();
                    RaisePropertyChanged("Patients");
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

                        Patients = ctx.PatientSets.Where(u => u.LastVisit == SearchDate1).ToList();
                        RaisePropertyChanged("Patients");
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
            _Patients = ctx.PatientSets.ToList();
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
