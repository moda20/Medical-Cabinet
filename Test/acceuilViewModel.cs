using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace Test
{
    class acceuilViewModel 
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
            }
        }
        private DateTime SearchDate;
        private String searChkey;
        public DateTime SearchDate1 { get { return SearchDate; } set { SearchDate = value; } }
        public string SearChkey { get { return searChkey; } set { searChkey = value; } }





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

    }
}
