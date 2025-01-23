using LicensesDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicensesBusinessLayer
{
    public class clsTestTypes
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestTypeFees { get; set; }

        private clsTestTypes(int TestTypeID, string TestTypeTitle,
            string TestTypeDescription, decimal TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
            Mode = enMode.Update;
        }

        public clsTestTypes()
        {
            this.TestTypeID = -1;
            this.TestTypeTitle = "";
            this.TestTypeDescription = "";
            this.TestTypeFees = 0;
            Mode = enMode.AddNew;
        }

        private bool _UpdateTestTypes()
        {
            return clsTestTypesData.UpdateTestTypes(this.TestTypeID,
                this.TestTypeTitle,this.TestTypeDescription, this.TestTypeFees);
        }

        public bool Save()
        {
            switch (Mode)
            {



                case enMode.Update:

                    return _UpdateTestTypes();
            }

            return false;
        }


        public static clsTestTypes Find(int TestTypeID)
        {
            string TestTypeTitle = "" , TestTypeDescription = ""; decimal TestTypeFees = 0;


            if (clsTestTypesData.GetTestTypesByID(TestTypeID, ref TestTypeTitle, ref TestTypeDescription ,ref TestTypeFees))

                return new clsTestTypes(TestTypeID, TestTypeTitle, TestTypeDescription , TestTypeFees);

            else
                return null;

        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesData.GetAllTestTypes();
        }
    }
}
