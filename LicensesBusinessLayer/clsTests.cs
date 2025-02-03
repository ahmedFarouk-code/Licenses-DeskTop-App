using LicensesDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicensesBusinessLayer
{
    public class clsTests
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }


        public static DataTable GetAllTests()
        {
            return clsTestsData.GetAllTests();
        }

        public static bool isPassed(int AppointmentID)
        {
            return clsTestsData.IsPassed(AppointmentID);
        }


        private bool _AddNewTest()
        {
            this.TestID = clsTestsData.AddNewTest(this.TestAppointmentID, this.TestResult, this.Notes,
                this.CreatedByUserID);

            return (this.TestID != -1);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewTest())
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

    }
}
