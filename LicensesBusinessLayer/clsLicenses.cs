using LicensesDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicensesBusinessLayer
{

    public class clsLicenses
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClass { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool IsActive { get; set; }
        public byte IssueReason { get; set; }
        public int CreatedByUserID { get; set; }

        private clsLicenses(int LicenseID, int ApplicationID, int DriverID, int LicenseClass,
             DateTime IssueDate, DateTime ExpirationDate, string Notes,
             decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;
        }


        public clsLicenses()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = false;
            this.IssueReason = 0;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }


        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicensesData.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass,
                this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive,
                this.IssueReason, this.CreatedByUserID);

            return (this.LicenseID != -1);
        }


        private bool _UpdateLicense()
        {
            return clsLicensesData.UpdateLicense(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClass,
                this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive,
                this.IssueReason, this.CreatedByUserID);
        }


        public static DataTable GetAllLicense()
        {
            return clsLicensesData.GetAllLicense();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewLicense())
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

                    return _UpdateLicense();

            }

            return false;
        }

        public static clsLicenses FindByAppID(int ApplicationID)
        {
            int LicenseID = 0, DriverID = -1, LicenseClass = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = 0;
            bool IsActive = false;
            byte IssueReason = 1;

            if (clsLicensesData.GetLicenseByAppID(ref LicenseID,  ApplicationID, ref  DriverID, ref  LicenseClass,
            ref  IssueDate, ref  ExpirationDate, ref  Notes,
            ref  PaidFees, ref  IsActive, ref  IssueReason, ref  CreatedByUserID))

                return new clsLicenses(  LicenseID, ApplicationID,  DriverID,  LicenseClass,
             IssueDate,  ExpirationDate,  Notes,
             PaidFees,  IsActive,  IssueReason,  CreatedByUserID);

            else
                return null;

        }

        public static clsLicenses FindByLicenseID(int LicenseID)
        {
            int ApplicationID = 0, DriverID = -1, LicenseClass = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            decimal PaidFees = 0;
            bool IsActive = false;
            byte IssueReason = 1;

            if (clsLicensesData.GetLicenseID( LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass,
            ref IssueDate, ref ExpirationDate, ref Notes,
            ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))

                return new clsLicenses(LicenseID, ApplicationID, DriverID, LicenseClass,
             IssueDate, ExpirationDate, Notes,
             PaidFees, IsActive, IssueReason, CreatedByUserID);

            else
                return null;

        }

        public static bool isExistSameLicenseforDriver(int DriverID ,int LicenseClass)
        {
            return clsLicensesData.isExistSameLicenseforDriver(DriverID , LicenseClass);
        }

    }
}
