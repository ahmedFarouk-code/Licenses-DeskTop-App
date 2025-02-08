using LicensesDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace LicensesBusinessLayer
{
    public class clsApplications
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public int ApplicationID {  get; set; }
        public int ApplicantPersonID  { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public byte ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }


        private clsApplications(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
            byte ApplicationStatus, DateTime LastStatusDate, decimal PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            

            Mode = enMode.Update;
        }


        public clsApplications()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = 1;
            this.ApplicationStatus = 1;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }


        public static DataTable GetAllApplications()
        {
            return clsApplicationsData.GetAllApplications();
        }

        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationsData.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID,
            this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            return (this.ApplicationID != -1);
        }

        public bool AddNewApplication(ref int ApplicationID)
        {
            this.ApplicationID = clsApplicationsData.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID,
            this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            ApplicationID = this.ApplicationID;
            return (this.ApplicationID != -1);
        }

        private bool _UpdateApplication()
        {
            return clsApplicationsData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID,
            this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewApplication())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    case enMode.Update:

                    return _UpdateApplication();


            }
            return false;
        }


        public static clsApplications Find(int ApplicationID)
        {
            int ApplicantPersonID = -1, ApplicationTypeID = 1, CreatedByUserID = -1;
            DateTime ApplicationDate = DateTime.Now , LastStatusDate = DateTime.Now ;
            byte ApplicationStatus = 0;
            decimal PaidFees = 0;
            if (clsApplicationsData.GetApplicationByID( ApplicationID, ref  ApplicantPersonID, ref  ApplicationDate, ref  ApplicationTypeID,
           ref  ApplicationStatus, ref  LastStatusDate, ref  PaidFees, ref  CreatedByUserID))

                return new clsApplications( ApplicationID,  ApplicantPersonID,  ApplicationDate,  ApplicationTypeID,
             ApplicationStatus,  LastStatusDate,  PaidFees,  CreatedByUserID);

            else
                return null;

        }
        


            public static clsApplications FindPerIDAndTypeID(int ApplicantPersonID , int ApplicationTypeID)
        {
            int ApplicationID = -1, CreatedByUserID = -1;
            DateTime ApplicationDate = DateTime.Now, LastStatusDate = DateTime.Now;
            byte ApplicationStatus = 0;
            decimal PaidFees = 0;
            if (clsApplicationsData.PerIDAndTypeID(ref ApplicationID,  ApplicantPersonID, ref ApplicationDate,  ApplicationTypeID,
           ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID))

                return new clsApplications(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID,
             ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);

            else
                return null;

        }

        public static clsApplications FindPerIDAndAppDate(int ApplicantPersonID, DateTime ApplicationDate)
        {
            int ApplicationID = -1, CreatedByUserID = -1, ApplicationTypeID = -1;
            DateTime  LastStatusDate = DateTime.Now;
            byte ApplicationStatus = 0;
            decimal PaidFees = 0;
            if (clsApplicationsData.FindPerIDAndAppDate(ref ApplicationID, ApplicantPersonID,  ApplicationDate, ref ApplicationTypeID,
           ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID))

                return new clsApplications(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID,
             ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);

            else
                return null;

        }



        public static bool IsApplicationExist(int PersonID)
        {
            return clsApplicationsData.IsApplicationExist(PersonID);
        }

        public static bool DeleteApplication(int ApplicationID)
        {
            return clsApplicationsData.DeleteApplication(ApplicationID);
        }
    }
}
