using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using System.Windows;

namespace Test 
{
    class acceuilViewModel : ViewModelBase
    {

        HealthCareEntities3 ctx = new HealthCareEntities3();

        public acceuilViewModel()
        {
            Searching = new RelayCommand(Search);
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
                RaisePropertyChanged("Patients");
            }
        }


        private List<FileSet> _Files;

        public List<FileSet> Files
        {
            get
            {
                List<FileSet> Listf = new List<FileSet>();
                Listf = ctx.FileSets.ToList();
                _Files = Listf;
                return _Files;

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
        public void Search()
        {
            if (SearchDate != null || searChkey != null)
            {
                List<PatientSet> UList = new List<PatientSet>();
                Patients = ctx.PatientSets.Where(u => u.LastVisit > SearchDate || u.FirstName == searChkey).ToList();
                List<FileSet> Listf = new List<FileSet>();
                Files = ctx.FileSets.Where(u => u.CreationDate >= SearchDate).ToList();
                MessageBox.Show("Number of files found" +Files.Count+"  Number of Patients : "+Patients.Count  );

            }
        }

    }
}
