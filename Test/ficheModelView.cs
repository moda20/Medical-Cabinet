using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Data.Entity;
using GalaSoft.MvvmLight;

namespace Test
{
    class ficheModelView : ViewModelBase
    {

        public ficheModelView(int X)
        {
            Searching = new RelayCommand(Search);
            ADDNEW = new RelayCommand(newFile);
            MODIFY = new RelayCommand(updateFile);
            DELETE = new RelayCommand(deleteFile);
            switch (X)
            {
                case 2: IsSec = "Visible"; break;
                case 4: IsDoctor = "Visible"; IsSec = "Visible"; break;
                case 3: IsPatient = "Visible"; break;
                default: IsAdmin = "Visible"; IsDoctor = "Visible"; IsSec = "Visible"; break;
            }

        }

        HealthCareEntities3 ctx = new HealthCareEntities3();
        

        private DateTime SearchDate;
        private string searChkey;
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


        private FileSet selectedFile;



        private PatientSet selectedPatient;
        public PatientSet SelectedPatient
        {
            get
            {

                if(selectedFile != null)
                {
                    return selectedFile.PatientSet;
                } else return selectedPatient;


            }


            set { selectedPatient = value; }
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
            if (SearchDate != null && searChkey != null)
            {
                List<PatientSet> UList = new List<PatientSet>();
                UList = ctx.PatientSets.Where(u => u.LastVisit >= SearchDate || u.FirstName == searChkey).ToList();
                List<FileSet> Listf = new List<FileSet>();
                Listf = ctx.FileSets.Where(u => u.CreationDate >= SearchDate).ToList();

            }
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
                selectedFile = value;
                Fileid = selectedFile.Id;
                FileDate = selectedFile.CreationDate;
                selectedPatient = selectedFile.PatientSet;
                MessageBox.Show(selectedFile.Id.ToString());
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
                filedate = value;
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
            if (filedate != null && selectedPatient != null)
            {
                FileSet NEW = new FileSet();
                NEW.CreationDate = filedate;
               
                NEW.PatientSet = selectedPatient;
                selectedPatient.FileSet = NEW;
                ctx.Entry(selectedPatient).State = EntityState.Modified;

                try
                {
                    ctx.FileSets.Add(NEW);
                    ctx.SaveChanges();
                    MessageBox.Show("File Added for "+ selectedPatient.FirstName);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error , Please verify your Entries", e.ToString());
                   
                }
            }
            else
            {
                MessageBox.Show("Please Fill the corresponding Entries before Adding");
            }
        }
        public RelayCommand MODIFY { private set; get; }
        public void updateFile()
        {
            if (SelectedFile1 != null)
            {
            
                SelectedFile1.CreationDate = filedate;
                SelectedFile1.PatientSet = selectedPatient;

                try
                {
                    ctx.SaveChanges();
                    RaisePropertyChanged("SelectedFile1");
                    RaisePropertyChanged("Files");
                    MessageBox.Show("File Modified");
                }
                catch (Exception e)
                {

                    MessageBox.Show("Error , Please Retry again");
                }

            }
            else
            {
                MessageBox.Show("You need to select a File To modify first.");
            }
        }

        public RelayCommand DELETE { private set; get; }

        public void deleteFile()
        {
            if (SelectedFile1 !=null)
            {
                ctx.FileSets.Remove(SelectedFile1);
                ctx.SaveChanges();
                RaisePropertyChanged("Files");
                MessageBox.Show("File Deleted");
            }
        }
    }
}
