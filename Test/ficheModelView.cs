using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Data.Entity;
using GalaSoft.MvvmLight;
using System.Globalization;
using MahApps.Metro.Controls.Dialogs;

namespace Test
{
    class ficheModelView : ViewModelBase
    {
        
        public ficheModelView(int X, Window T)
        {
            ThisWindow = T;
            Searching = new RelayCommand(Search);
            ADDNEW = new RelayCommand(newFile);
            MODIFY = new RelayCommand(updateFile);
            DELETE = new RelayCommand(deleteFile);
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
        

        private DateTime SearchDate = DateTime.Today;
        private string searChkey;
        private Boolean X = false;
        public List<PatientSet> _Patients;
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


        private FileSet selectedFile;



        private PatientSet selectedPatient;
        public PatientSet SelectedPatient1
        {
            get
            {

                
                return selectedPatient;


            }


            set { selectedPatient = value;
                RaisePropertyChanged("SelectedPatient1");
                    }
        }
        private int fileid;
        public int Fileid
        {
            get
            {
                if (selectedFile != null)
                {
                    return selectedFile.Id;
                }
                else return 0;
            }

            set
            {

                fileid = value;
            }

        }

        private DateTime filedate;

        public DateTime SearchDate1
        {
            get
            {
                if (selectedFile != null)
                {
                    return selectedFile.CreationDate;

                }
                else return DateTime.Today;
            }

            set
            {
                SearchDate = value;
            }
        }


        public RelayCommand Searching { private set; get; }
        public void Search()
        {


            if (X == true)
            {
                try
                {

                    Patients = ctx.PatientSets.ToList();
                    Files = ctx.FileSets.ToList();
                    RaisePropertyChanged("RDVS1");
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

                        Patients = ctx.PatientSets.Where(u => u.LastVisit >= SearchDate || u.FirstName == searChkey).ToList();
                        Files = ctx.FileSets.Where(u => u.CreationDate >= SearchDate).ToList(); 
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
            
            _Files = ctx.FileSets.ToList();
            RaisePropertyChanged("Files");
            
        }
        public RelayCommand ADDNEW { private set; get; }

        public FileSet SelectedFile1
        {
            get
            {
                return selectedFile;
            }

            set
            {
                if (value != null)
                {
                    
                    selectedFile = value;
                    Fileid = value.Id;
                    FileDate = value.CreationDate;
                    SelectedPatient1 = value.PatientSet;
                }
                
            }
        }

        public DateTime FileDate
        {
            get
            {
                return filedate;
            }

            set
            {
                filedate = value; RaisePropertyChanged("FileDate");
            }
        }

        public string SearChkey
        {
            get
            {
                return searChkey;
            }

            set
            {
                searChkey = value;
            }
        }

        public int FileID1
        {
            get
            {
                return FileID;
            }

            set
            {
                FileID = value;
            }
        }

        private int FileID;

        public void newFile()
        {
            Exception Xe= null;
            if (filedate != null && SelectedPatient1 != null)
            {
                FileSet NEW = new FileSet();
                NEW.CreationDate = filedate;

                try
                {
                    try
                    {
                        if (SelectedPatient1.FileSet == null)
                        {
                            NEW.PatientSet = SelectedPatient1;
                        }
                        else
                        {
                            throw new Exception("Patient Already has a File");
                        }
                    }
                    catch (Exception e)
                    {
                        Xe = e;
                        throw e;
                    
                    }
                    SelectedPatient1.FileSet = NEW;
                    ctx.Entry(SelectedPatient1).State = EntityState.Modified;

                    try
                    {
                        ctx.FileSets.Add(NEW);
                        ctx.SaveChanges();
                        this.X = true;
                        Search();
                        this.X = false;
                        MahApps.Metro.Controls.MetroWindow wd = Window.GetWindow(ThisWindow) as MahApps.Metro.Controls.MetroWindow;
                        if (wd != null)
                        {
                            wd.ShowMessageAsync("File Added for " + SelectedPatient1.FirstName,"");
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                catch (Exception e)
                {
                    if (Xe == null)
                    {
                        MahApps.Metro.Controls.MetroWindow wd = Window.GetWindow(ThisWindow) as MahApps.Metro.Controls.MetroWindow;
                        if (wd != null)
                        {
                            wd.ShowMessageAsync("Error While adding file", "Please Verify your Entries \n Error :"+e.ToString());
                        }
                    }
                    else
                    {
                        MahApps.Metro.Controls.MetroWindow wd = Window.GetWindow(ThisWindow) as MahApps.Metro.Controls.MetroWindow;
                        if (wd != null)
                        {
                            wd.ShowMessageAsync("Error While adding file for " + SelectedPatient1.FirstName, Xe.Message);
                        }
                    }

                }
            }
            else
            {
                MahApps.Metro.Controls.MetroWindow wd = Window.GetWindow(ThisWindow) as MahApps.Metro.Controls.MetroWindow;
                if (wd != null)
                {
                    wd.ShowMessageAsync("Error While adding file", "Please Fill out all entries");
                }
            }
        }
        public RelayCommand MODIFY { private set; get; }
        public void updateFile()
        {
            if (SelectedFile1 != null)
            {
            
                SelectedFile1.CreationDate = filedate;
                SelectedFile1.PatientSet = SelectedPatient1;

                try
                {
                    ctx.SaveChanges();
                    this.X = true;
                    Search();
                    this.X = false;
                    RaisePropertyChanged("SelectedFile1");
                    RaisePropertyChanged("Files");
                    MahApps.Metro.Controls.MetroWindow wd = Window.GetWindow(ThisWindow) as MahApps.Metro.Controls.MetroWindow;
                    if (wd != null)
                    {
                        wd.ShowMessageAsync("File of" + SelectedPatient1.FirstName+" Modified", "");
                    }
                }
                catch (Exception e)
                {

                    MahApps.Metro.Controls.MetroWindow wd = Window.GetWindow(ThisWindow) as MahApps.Metro.Controls.MetroWindow;
                    if (wd != null)
                    {
                        wd.ShowMessageAsync("Error While Modifying file for " + SelectedPatient1.FirstName, "");
                    }
                }

            }
            else
            {
                MahApps.Metro.Controls.MetroWindow wd = Window.GetWindow(ThisWindow) as MahApps.Metro.Controls.MetroWindow;
                if (wd != null)
                {
                    wd.ShowMessageAsync("Error While Updating file " , "Please Choose a File to modify first");
                }
            }
        }

        public RelayCommand DELETE { private set; get; }

        public void deleteFile()
        {
            if (SelectedFile1 !=null)
            {
                ctx.FileSets.Remove(SelectedFile1);
                ctx.SaveChanges();
                this.X = true;
                Search();
                this.X = false;
                RaisePropertyChanged("Files");
                MahApps.Metro.Controls.MetroWindow wd = Window.GetWindow(ThisWindow) as MahApps.Metro.Controls.MetroWindow;
                if (wd != null)
                {
                    wd.ShowMessageAsync("File deleted successfully","");
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
