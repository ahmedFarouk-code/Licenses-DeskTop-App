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

        enum enTestType {Vision = 1 , Written = 3 ,Street = 3 }
        enTestType _TesType = enTestType.Vision;

        private clsTestAppointments _TestAppointments;
        private clsLDLA _LDLA;
        private clsApplications _Application;
        private clsPeople _Person;

        public static int TestAppoID {  get; set; }
        public static int TesTypeID { get; set; }
        public static int LDLAid { get; set; }
        

        public UrScheduleTestAndRetake()
        {
            InitializeComponent();
            if(TesTypeID == 1)
            {
                _TesType = enTestType.Vision;
            }
           else if(TesTypeID == 2)
           {
                _TesType = enTestType.Written;
            }
           else
           {
                _TesType = enTestType.Street;
           }

            if(TestAppoID == -1)
            { _Mode = enMode.AddNew; }
            else { _Mode = enMode.Update; }

           

        }
       private void _Load()
        {
            if (_Mode == enMode.AddNew)
            {
                _TestAppointments = new clsTestAppointments();
                _LDLA = clsLDLA.Find(LDLAid);
                if (_LDLA == null)
                {
                    return;
                }
            }

            else
            {
                _TestAppointments = clsTestAppointments.FindByID(TestAppoID);
                if (_TestAppointments == null)
                {
                    return;
                }
                _LDLA = clsLDLA.Find(_TestAppointments.LocalDrivingLicenseApplicationID);
                if (_LDLA == null)
                {
                    return;
                }
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
            

            if (_TesType == enTestType.Vision)
            {
                pictureBox1.Image = Image.FromFile("D:\\Licenses-DeskTop-App\\licenses\\Images\\eye.png");
                groupBox1.Text = "Vision Test";
            }
            else if(_TesType == enTestType.Written)
            {
                pictureBox1.Image = Image.FromFile("D:\\Licenses-DeskTop-App\\licenses\\Images\\pen.png");
                groupBox1.Text = "Written Test";
            }
            else
            {
                pictureBox1.Image = Image.FromFile("D:\\Licenses-DeskTop-App\\licenses\\Images\\road.png");
                groupBox1.Text = "Street Test";
            }


            lblDLAid.Text = _LDLA.LocalDrivingLicenseApplicationID.ToString();
            lblDClass.Text = clsLicenseClasses.Find (_LDLA.LicenseClassID).LicenseClasseName;
            lblPersonName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;
           
            
            DataTable dtAppointment = clsTestAppointments.GetAllTestAppointments();
            int Trial = 0;
            foreach (DataRow rowAppointment in dtAppointment.Rows)
            {
                if((int)rowAppointment["TestTypeID"] == TesTypeID &&
                    (int)rowAppointment["LocalDrivingLicenseApplicationID"] == _LDLA.LocalDrivingLicenseApplicationID &&
                    (int)rowAppointment["LocalDrivingLicenseApplicationID"] >0)
                {
                    Trial++;
                }
            }
           
            lblTrail.Text = Trial.ToString();

            dateTimePicker1.Value = DateTime.Now;
            lblFees.Text = clsTestTypes.Find(TesTypeID).TestTypeFees.ToString();

            if (lblTrail.Text == "0")
            {
                gbRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Schedule Test";
                lblRFees.Text = "5";
                //lblTotalFees.Text = clsTestTypes.Find(TesTypeID).TestTypeFees.ToString();
            }
            else
            {
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRFees.Text = "5";
                lblTotalFees.Text = (Convert.ToInt32(lblRFees.Text) + Convert.ToInt32(lblFees.Text)).ToString();
                //lblRTestAAppID.Text = _TestAppointments.RetakeTestApplicationID.ToString();
            }

            



        }

        private void UrScheduleTestAndRetake_Load(object sender, EventArgs e)
        {
            _Load();
        }
    }
}
