using LicensesDataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicensesBusinessLayer
{
    public class clsTestAppointments
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }


        public static DataTable GetAllTestAppointments()
        {
            return clsTestAppointmentsData.GetAllTestAppointments();
        }

        private clsTestAppointments( int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID,
             DateTime AppointmentDate,  decimal PaidFees,  int CreatedByUserID,  bool IsLocked,  int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;

            Mode = enMode.Update;
        }

        public clsTestAppointments()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = -1;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.IsLocked = false;
            this.RetakeTestApplicationID = RetakeTestApplicationID;

            Mode = enMode.AddNew;
        }
        
        public static clsTestAppointments FindByLDLAidAndTestType(int TestTypeID, int LocalDrivingLicenseApplicationID)
        {
            int TestAppointmentID = -1, RetakeTestApplicationID = -1, CreatedByUserID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = 0;
            bool IsLocked = false;



            if (clsTestAppointmentsData.GetAppointmentByLDLAidAndTestType(ref TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID,
                ref AppointmentDate,ref PaidFees, ref CreatedByUserID,ref IsLocked, ref RetakeTestApplicationID))

                return new clsTestAppointments( TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID,
                 AppointmentDate,  PaidFees ,CreatedByUserID,  IsLocked,  RetakeTestApplicationID);

            else
                return null;

        }


        public static clsTestAppointments FindByID(int TestAppointmentID)
        {
            int LocalDrivingLicenseApplicationID = -1, TestTypeID = -1, RetakeTestApplicationID = -1, CreatedByUserID = -1;
            DateTime AppointmentDate = DateTime.Now;
            decimal PaidFees = 0;
            bool IsLocked = false;



            if (clsTestAppointmentsData.GetAppointmentByID( TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID,
                ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))

                return new clsTestAppointments(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID,
                 AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);

            else
                return null;

        }
    }
}
