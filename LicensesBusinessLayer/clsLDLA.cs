using LicensesDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicensesBusinessLayer
{
    public class clsLDLA
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { get; set; }
        public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }

        private clsLDLA(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.LicenseClassID = LicenseClassID;

            Mode = enMode.Update;
        }

        public clsLDLA()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.ApplicationID = -1;
            this.LicenseClassID = -1;

            Mode = enMode.AddNew;
        }


        public static DataTable GetALL_LDLA()
        {
            return clsLDLAData.GetALL_LDLA();
        }

        private bool _AddNewLDLA()
        {
            this.LocalDrivingLicenseApplicationID = clsLDLAData.AddNewLDLA(this.ApplicationID, this.LicenseClassID);

            return (this.LocalDrivingLicenseApplicationID != -1);
        }


        private bool _UpdateUser()
        {
            return clsLDLAData.UpdateLDLA(this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
        }

        
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewLDLA())
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


        public static clsLDLA Find(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;

            if (clsLDLAData.GetLDLAByID(  LocalDrivingLicenseApplicationID,
                                             ref  ApplicationID, ref  LicenseClassID))

                return new clsLDLA( LocalDrivingLicenseApplicationID,
                                               ApplicationID,  LicenseClassID);

            else
                return null;

        }

    }
}
