using LicensesBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace licensesApp
{
    public partial class UrScheduleTestAndRetake : UserControl
    {
        enum enMode { AddNew =0 , Update = 1 }
        enMode _Mode = enMode.AddNew;

        private clsTestAppointments _TestAppointments;
        private clsLDLA _LDLA;
        private clsApplications _Application;
        private clsPeople _Person;
        private clsTestAppointments _Appointments;

        int _TestAppoID; int _LDLAid; int _UserID; int _TestType;

        public UrScheduleTestAndRetake()
        {
            InitializeComponent();

        }
       public void LoadInfo(int TestAppoID , int LDLAid , int UserID , int TestType)
        {
             _TestAppoID = TestAppoID ;  _LDLAid = LDLAid;  _UserID = UserID;  _TestType = TestType;
            if (TestAppoID == -1)
            { _Mode = enMode.AddNew; }
            else { _Mode = enMode.Update; }

            if (_TestType == 1)
            {
                groupBox1.Text = "Vision Test";
            }
            if (_TestType == 2)
            {
                groupBox1.Text = "written Test";
            }
            if (_TestType == 3)
            {
                groupBox1.Text = "Street Test";
            }




            if (_Mode == enMode.AddNew)
            {
                _TestAppointments = new clsTestAppointments();
               
            }

            else
            {
                _TestAppointments = clsTestAppointments.FindByID(_TestAppoID);
                if (_TestAppointments == null)
                {
                    return;
                }
                if(_TestAppointments.IsLocked == true)
                {
                    dateTimePicker1.Enabled = false;
                    btnSave.Enabled = false;
                    lblAlreadySat.Visible = true;
                    lblAlreadySat.Text = "Person already sat for the test, appointment locked";

                }
            }

            _LDLA = clsLDLA.Find(_LDLAid);
            if (_LDLA == null)
            {
                return;
            }

            _Application = clsApplications.Find(_LDLA.ApplicationID);
            if (_Application == null)
            {
                return;
            }
            _Person = clsPeople.Find(_Application.ApplicantPersonID);
                if (_Person == null)
                {
                    return;
                }
            

            

            lblDLAid.Text = _LDLA.LocalDrivingLicenseApplicationID.ToString();
            lblDClass.Text = clsLicenseClasses.Find (_LDLA.LicenseClassID).LicenseClasseName;
            lblPersonName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;
           
            
            DataTable dtAppointment = clsTestAppointments.GetAllTestAppointments();
            int Trial = 0;
            foreach (DataRow rowAppointment in dtAppointment.Rows)
            {
                if((int)rowAppointment["LocalDrivingLicenseApplicationID"] == _LDLA.LocalDrivingLicenseApplicationID &&
                    rowAppointment["RetakeTestApplicationID"] != null)
                {
                    Trial++;
                }
            }
           
            lblTrail.Text = Trial.ToString();
           

            dateTimePicker1.Value = DateTime.Now;
            lblFees.Text = clsTestTypes.Find(_TestType).TestTypeFees.ToString();

            if (lblTrail.Text == "0")
            {
                gbRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Schedule Test";
                lblRFees.Text = "5";
                lblTotalFees.Text = clsTestTypes.Find(_TestType).TestTypeFees.ToString();
            }
            else
            {
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRFees.Text = "5";
                lblTotalFees.Text = (5 + clsTestTypes.Find(_TestType).TestTypeFees).ToString();
                lblRTestAAppID.Text = _TestAppointments.RetakeTestApplicationID.ToString();
            }

            



        }

       

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                _Appointments = new clsTestAppointments();

            }
           else
            {
                _Appointments = clsTestAppointments.FindByID(_TestAppoID);
            }

           

            _Appointments.TestTypeID = _TestType;
            _Appointments.LocalDrivingLicenseApplicationID =  Convert.ToInt32(lblDLAid.Text);
            _Appointments.AppointmentDate = dateTimePicker1.Value;
            _Appointments.PaidFees = Convert.ToDecimal(lblFees.Text);
            _Appointments.CreatedByUserID = _UserID;
            _Appointments.IsLocked = false;
            _Appointments.RetakeTestApplicationID = 0;

            if (_Appointments.Save())

                MessageBox.Show("Data Saved Successfully.");
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.");

            _Mode = enMode.Update;
        }
       

    }

}
