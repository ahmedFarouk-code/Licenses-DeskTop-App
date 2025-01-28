using LicensesDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicensesBusinessLayer
{
    public class clsLicenseClasses
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ID { get; set; }
        public string LicenseClasseName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinimumAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }
        public decimal ClassFees { get; set; }


        private clsLicenseClasses( int ID, string LicenseClasseName,  string ClassDescription,  byte MinimumAllowedAge,
             byte DefaultValidityLength,  decimal ClassFees)
        {
            this.ID = ID;
            this.LicenseClasseName = LicenseClasseName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;


            Mode = enMode.Update;
        }


        public clsLicenseClasses()
        {
            this.ID = -1;
            this.LicenseClasseName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = 0;
            this.DefaultValidityLength = 0;
            this.ClassFees = 0;

            Mode = enMode.AddNew;
        }

        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassesData.GetAllLicenseClasses();
        }

        public static clsLicenseClasses Find(string LicenseClasseName)
        {
            int ID = -1;
            string ClassDescription = "";
            byte MinimumAllowedAge = 0 , DefaultValidityLength = 0 ;
            decimal ClassFees = 0 ;




            if (clsLicenseClassesData.GetLicenseClassesByName(ref  ID, LicenseClasseName, ref  ClassDescription, ref  MinimumAllowedAge,
            ref  DefaultValidityLength, ref  ClassFees))

                return new clsLicenseClasses( ID, LicenseClasseName,  ClassDescription,  MinimumAllowedAge,
             DefaultValidityLength,  ClassFees);

            else
                return null;

        }

        public static clsLicenseClasses Find(int ID)
        {
           
            string ClassDescription = "" , LicenseClasseName = "";
            byte MinimumAllowedAge = 0, DefaultValidityLength = 0;
            decimal ClassFees = 0;




            if (clsLicenseClassesData.GetLicenseClassesByID( ID, ref LicenseClasseName, ref ClassDescription, ref MinimumAllowedAge,
            ref DefaultValidityLength, ref ClassFees))

                return new clsLicenseClasses(ID, LicenseClasseName, ClassDescription, MinimumAllowedAge,
             DefaultValidityLength, ClassFees);

            else
                return null;

        }

    }
}
