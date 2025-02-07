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
using static System.Net.Mime.MediaTypeNames;

namespace licensesApp
{
    public partial class frmNewLicenseInternationalApplication : Form
    {
        clsLicenses _LocalLic;
        clsLDLA _LDLA;
        
        int _UserID;
        int LicenseID = -1;
        public frmNewLicenseInternationalApplication(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            ucIntarnationalAppInfo1.LoadIntarnationalAppInfo(_UserID , LicenseID);
        }

        private void btnFindLicense_Click(object sender, EventArgs e)
        {

           
            if(int.TryParse(txtLicenseID.Text, out LicenseID))
            {
                _LocalLic = clsLicenses.FindByLicenseID(LicenseID);
                _LDLA = clsLDLA.FindByAppID(_LocalLic.ApplicationID);
                if(_LDLA == null)
                {
                    MessageBox.Show("This License not found !");
                    return;
                }

                urDriverLIcenseInfo1.LoadInfo(_LDLA.LocalDrivingLicenseApplicationID);
                llblShowLicenseHistory.Enabled = true;
               
              
            }
           else
            {
                MessageBox.Show("License ID Should be a number !" ,"Warning" ,MessageBoxButtons.OK);
            }

            ucIntarnationalAppInfo1.LoadIntarnationalAppInfo(_UserID, LicenseID);

            if(clsInternationalLicenses.FindByLDLid(LicenseID) != null)
            {
                MessageBox.Show("This person have an Intrnational lisence ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                btnIssue.Enabled = false;
                llblShowLicense.Enabled = true;
                llblShowLicenseHistory.Enabled = true;
            }
            else
            {
                btnIssue.Enabled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            clsApplications applications = new clsApplications();
            applications.ApplicantPersonID = clsApplications.Find(_LocalLic.ApplicationID).ApplicantPersonID;
            applications.ApplicationDate = DateTime.Now;
            applications.ApplicationTypeID = 6; // should edit
            applications.ApplicationStatus = 3;
            applications.LastStatusDate = DateTime.Now;
            applications.PaidFees = 51; // should edit
            applications.CreatedByUserID = _UserID;

            if (applications.Save())
            {
                MessageBox.Show("Data save successful   applications ", "successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Data save Faild  applications", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            clsInternationalLicenses InternationalLicenses = new clsInternationalLicenses();
            
            InternationalLicenses.ApplicationID = clsApplications.FindPerIDAndTypeID(applications.ApplicantPersonID, 6).ApplicationID;
            InternationalLicenses.DriverID = _LocalLic.DriverID;
            InternationalLicenses.IssuedUsingLocalLicenseID = _LocalLic.LicenseID;
            InternationalLicenses.IssueDate = DateTime.Now;
            InternationalLicenses.ExpirationDate = DateTime.Now.AddYears(1);
            InternationalLicenses.IsActive = true;
            InternationalLicenses.CreatedByUserID = _UserID;

            if (InternationalLicenses.Save())
            {
                MessageBox.Show("Data save successful   applications ", "successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnIssue.Enabled = false;
                llblShowLicense.Enabled = true;
                llblShowLicenseHistory.Enabled = true;
            }
            else
            {
                MessageBox.Show("Data save Faild  applications", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            ucIntarnationalAppInfo1.LoadIntarnationalAppInfo(_UserID, LicenseID);

           




        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = clsApplications.Find(_LocalLic.ApplicationID).ApplicantPersonID;
            clsApplications App = clsApplications.Find(_LocalLic.ApplicationID);
            clsLicenses Lic = clsLicenses.FindByAppID(_LocalLic.ApplicationID);
            Form frm = new frmLicenseHistory(PersonID , Lic.DriverID);
            frm.ShowDialog();
            
        }

        private void llblShowLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmInterNationalDriverLicenseID(_LocalLic.LicenseID);
            frm.ShowDialog();
            
        }
    }
}

