﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using System.Data.Entity.Validation;
using System.Windows;

namespace Test
{
    public class NewPatientsModelView : ViewModelBase
    {

        public NewPatientsModelView()
        {
            ADDNEW = new RelayCommand(NewPatient);
            MODIFY = new RelayCommand(modifyPatient);
            DELETE = new RelayCommand(deletePatient);
            EMPTY = new RelayCommand(emptyPatient);
            SEARCH = new RelayCommand(Searching);
        }


        private String PName;
        private String PLastName;
        private DateTime PDateOfBirth;
        private DateTime PLASTVISIT;
        private String POccupation;
        private String PCellPhone;
        private String PAddress;
        private FileSet PFile;
        private CitySet PCity;
        private PatientSet SelectedPatient;
        private DateTime SearchDate;

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
            user.FileSet = null;


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
            if (SearchDate1 != null)
            {
                try
                {
                    MessageBox.Show(SearchDate1.ToString());
                    Patients=ctx.PatientSets.Where(u => u.LastVisit == SearchDate1).ToList();
                    RaisePropertyChanged("Patients");
                }
                catch (Exception e )
                {
                    MessageBox.Show("Error in Database Connexion, Please Try again.",e.Message);
                }
            }
        }
    }
}
