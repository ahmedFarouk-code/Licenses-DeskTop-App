using LicensesDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicensesBusinessLayer
{
    public class clsInternationalLicenses
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }


        private clsInternationalLicenses(int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;
        }


        public clsInternationalLicenses()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = false;
            this.CreatedByUserID = -1;


            Mode = enMode.AddNew;
        }


        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicensesData.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
                this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedByUserID);

            return (this.InternationalLicenseID != -1);
        }


        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicensesData.GetAllInternationalLicenses();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewInternationalLicense())
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
               

            }

            return false;
        }

        public static clsInternationalLicenses FindByLDLid(int IssuedUsingLocalLicenseID)
        {
            int InternationalLicenseID = 0; int ApplicationID = -1, DriverID = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now , ExpirationDate = DateTime.Now ;
            bool IsActive = false;

            if (clsInternationalLicensesData.GetInternationalLicensesByLDLid(ref InternationalLicenseID, ref ApplicationID, ref DriverID, IssuedUsingLocalLicenseID,
               ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))

                return new clsInternationalLicenses( InternationalLicenseID,  ApplicationID,  DriverID, IssuedUsingLocalLicenseID,
                IssueDate,  ExpirationDate,  IsActive,  CreatedByUserID);

            else
                return null;

        }






        public static bool IsInternationalLicenseExistByLocalID(int IssuedUsingLocalLicenseID)
        {
            return clsInternationalLicensesData.IsInternationalLicenseExistByLocalID(IssuedUsingLocalLicenseID);
        }

    }
}
