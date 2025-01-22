using LicensesDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LicensesBusinessLayer
{
    public class clsApplicationTypes
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ApplicationTypeID {  get; set; }
        public string ApplicationTypeTitle { get; set; }
        public decimal ApplicationFees { get; set; }

        private clsApplicationTypes(int ApplicationTypeID, string ApplicationTypeTitle, decimal ApplicationFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
            Mode = enMode.Update;
        }

        public clsApplicationTypes()
        {
            this.ApplicationTypeID = -1;
            this.ApplicationTypeTitle = "";
            this.ApplicationFees = 0;
            Mode = enMode.AddNew;
        }

        private  bool _UpdateApplicationTypes()
        {
            return clsApplicationTypesData.UpdateApplicationType(this.ApplicationTypeID ,
                this.ApplicationTypeTitle, this.ApplicationFees);
        }

        public bool Save()
        {
            switch (Mode)
            {



                case enMode.Update:

                    return _UpdateApplicationTypes();
            }

            return false;
        }


        public static clsApplicationTypes Find(int ApplicationTypeID)
        {
            string ApplicationTypeTitle = ""; decimal ApplicationFees = 0;
            

            if (clsApplicationTypesData.GetApplicationTypesID(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees))

                return new clsApplicationTypes(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);

            else
                return null;

        }

        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypesData.GetAllApplicationTypes();
        }


    }
}
