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
    public partial class UserControlAppDetails : UserControl
    {
        public static int LDLid {  get; set; }
        public static int PassedTests { get; set; }

        private clsLDLA _LDLA;
        private clsApplications _Application;
        private clsPeople _Person;
        public UserControlAppDetails()
        {
            InitializeComponent();

        }

        private void _Load()
        {
            _LDLA = clsLDLA.Find(LDLid);
            if(_LDLA == null )
            {
                
                return;
            }

            _Application = clsApplications.Find(_LDLA.ApplicationID);
            if (_LDLA == null)
            {
                return;
            }
            _Person = clsPeople.Find(_Application.ApplicantPersonID);
            if (_LDLA == null)
            {
                return;
            }

            //Driving License Application Info :

            lblDLAid.Text = _LDLA.LocalDrivingLicenseApplicationID.ToString();
            lblAppLiadForLicens.Text = clsLicenseClasses.Find(_LDLA.LicenseClassID).LicenseClasseName;
            lblPassedTests.Text = PassedTests + "/3";

            //Application Basic Info:

            lblID.Text = _Application.ApplicationID.ToString();
            lblStatuse.Text = _Application.ApplicationStatus.ToString();
            lblFees.Text = _Application.PaidFees.ToString();
            lblApplicant.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;
            lblDate.Text = _Application.ApplicationDate.ToString();
            lblStatussDate.Text = _Application.LastStatusDate.ToString();
            lblCreatedBy.Text = clsUsers.FindByUserID(_Application.CreatedByUserID).UserName;
            lblType.Text = clsApplicationTypes.Find(_Application.ApplicationTypeID).ApplicationTypeTitle;
        }

        private void UserControlAppDetails_Load(object sender, EventArgs e)
        {
            _Load();
        }

        private void llblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmPersonDetails(_Person.ID);
            frm.ShowDialog();
        }
    }
}
