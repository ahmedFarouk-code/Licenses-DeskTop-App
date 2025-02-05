using LicensesBusinessLayer;
using System;
using System.CodeDom;
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
    public partial class frmIssueDriverLicenseForTheFirstTime : Form
    {
        private int _LDLid;
        private int _PassedTests;
        private int _UserID;

        private clsLDLA _LDLA;
        private clsApplications _Applications;
        private clsLicenseClasses _LicenseClasses;
        public frmIssueDriverLicenseForTheFirstTime(int LDLid, int PassedTests, int UserID)
        {
            InitializeComponent();
            _LDLid = LDLid;
            _PassedTests = PassedTests;
            _UserID = UserID;
            UserControlAppDetails.LDLid = _LDLid;
            UserControlAppDetails.PassedTests = _PassedTests;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            _LDLA = clsLDLA.Find(_LDLid);
            if (_LDLA == null)
            {
                return;
            }
            _Applications = clsApplications.Find(_LDLA.ApplicationID);
            if (_Applications == null)
            {
                return;
            }

            _LicenseClasses = clsLicenseClasses.Find(_LDLA.LicenseClassID);
            if (_LicenseClasses == null)
            {
                return;
            }

            _Applications.ApplicationStatus = 3;
            if (_Applications.Save())
            {
                // MessageBox.Show("Data save successful" , "successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // MessageBox.Show("Data save Faild", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            clsDrivers Driver = new clsDrivers();
            Driver.PersonID = _Applications.ApplicantPersonID;
            Driver.CreatedByUserID = _UserID;
            Driver.CreatedDate = DateTime.Now;

           

            if (Driver.Save())
            {
               // MessageBox.Show("Data save successful" , "successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
               // MessageBox.Show("Data save Faild", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            clsLicenses Licenses = new clsLicenses();
            Licenses.ApplicationID = _Applications.ApplicationID;
            Licenses.DriverID = clsDrivers.Find(_Applications.ApplicantPersonID).DriverID;
            Licenses.LicenseClass = _LDLA.LicenseClassID;
            Licenses.IssueDate = DateTime.Now;
            Licenses.ExpirationDate = DateTime.Now.AddDays(_LicenseClasses.DefaultValidityLength);
            Licenses.Notes = txtNotes.Text;
            Licenses.PaidFees = _LicenseClasses.ClassFees;
            Licenses.IsActive = true;
            Licenses.IssueReason = 1;
            Licenses.CreatedByUserID = _UserID;

            _Applications.ApplicationStatus = 3;


            if (Licenses.Save() && _Applications.Save())
            {
                MessageBox.Show("Data save successful", "successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data save Faild", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
