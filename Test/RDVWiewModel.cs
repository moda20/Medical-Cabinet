using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class RDVWiewModel : ViewModelBase
    {

        private PatientSet SelectedPatient;
        private DateTime RDVDate;

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
    }
}
