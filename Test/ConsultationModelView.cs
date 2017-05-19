using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Test
{
    class ConsultationModelView : ViewModelBase
    {

        public ConsultationModelView()
        {
            ADDNEW = new RelayCommand(NewConsultation);
            MODIFY = new RelayCommand(ModifyConsultation);
            DELETE = new RelayCommand(deleteConsultation);
            EMPTY = new RelayCommand(emptyConsultation);
            SEARCH = new RelayCommand(searching);

        }

        HealthCareEntities3 ctx = new HealthCareEntities3();

        private DateTime CtDate = DateTime.Today;
        private float Cost;
        private String Act;
        private ConsultationSet SelectedConsultation;
        private FileSet SelectedFile;
        private List<ConsultationSet> Consultations;
        private DateTime SearchDate;


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
                    CtDate1 = DateTime.Parse(value.date);
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
                cts.date = (CtDate1).ToShortDateString();
                cts.FileSet = SelectedFile1;
                try
                {
                    ctx.ConsultationSets.Add(cts);
                    ctx.SaveChanges();
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
                SelectedConsultation1.date = (CtDate1).ToShortDateString();
                SelectedConsultation1.FileSet = SelectedFile1;

                try
                {
                    ctx.SaveChanges();
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
            if (SearchDate1 != null)
            {
                Consultations1 = ctx.ConsultationSets.Where(u => DateTime.Parse(u.date) > SearchDate1).ToList();
                RaisePropertyChanged("SearchDate1");
                MessageBox.Show("Found : " + Consultations1.Count + " Consultations");
            }
        }

}
}
