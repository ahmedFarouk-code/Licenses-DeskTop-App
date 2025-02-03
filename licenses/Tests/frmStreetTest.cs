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
    public partial class frmStreetTest : Form
    {
        private clsTestAppointments _TestAppointments;
        private clsLDLA _LDLA;
        private clsApplications _Application;
        private clsPeople _Person;
        private clsTests _Test;

        private int _TestAppoID;
        private int _LDLAid;
        private int _UserID;

        public frmStreetTest(int LDLAid, int TestAppoID, int UserID)
        {
            InitializeComponent();
            _TestAppoID = TestAppoID;
            _LDLAid = LDLAid;
            _UserID = UserID;

        }

        private void _Load()
        {


            _Test = new clsTests();



            _TestAppointments = clsTestAppointments.FindByID(_TestAppoID);
            if (_TestAppointments == null)
            {
                return;
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

            if (clsTests.isPassed(_TestAppointments.TestAppointmentID))
            {
                btnSave.Enabled = false;
                lblWasExamed.Visible = true;

            }

            lblDLAid.Text = _LDLA.LocalDrivingLicenseApplicationID.ToString();
            lblDClass.Text = clsLicenseClasses.Find(_LDLA.LicenseClassID).LicenseClasseName;
            lblPersonName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;


            DataTable dtAppointment = clsTestAppointments.GetAllTestAppointments();
            int Trial = 0;
            foreach (DataRow rowAppointment in dtAppointment.Rows)
            {
                if ((int)rowAppointment["TestTypeID"] == 3 &&
                    (int)rowAppointment["LocalDrivingLicenseApplicationID"] == _LDLA.LocalDrivingLicenseApplicationID &&
                    rowAppointment["RetakeTestApplicationID"] != null)
                {
                    Trial++;
                }
            }

            lblTrail.Text = Trial.ToString();


            lblDate.Text = DateTime.Now.ToString();
            lblFees.Text = clsTestTypes.Find(3).TestTypeFees.ToString();

            if (lblTrail.Text == "0")
            {
                lblTitle.Text = "Schedule Test";
            }
            else
            {
                lblTitle.Text = "Schedule Retake Test";
            }
        }

        private void frmStreetTest_Load(object sender, EventArgs e)
        {
            _Load();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Test = new clsTests();
            _TestAppointments = clsTestAppointments.FindByID(_TestAppoID);
            _Test.TestAppointmentID = Convert.ToInt32(_TestAppoID);
            if (rbPass.Checked)
            {
                _Test.TestResult = true;
            }
            else
            {
                _Test.TestResult = false;
            }
            if (txtNotes.Text != "")
                _Test.Notes = txtNotes.Text;
            else
            {
                _Test.Notes = "";
            }
            _Test.CreatedByUserID = _UserID;
            _TestAppointments.IsLocked = true;

            if (_TestAppointments.Save() && _Test.Save())
            {

                MessageBox.Show("Data Saved Successfully.");
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.");


        }
    
    }
}
