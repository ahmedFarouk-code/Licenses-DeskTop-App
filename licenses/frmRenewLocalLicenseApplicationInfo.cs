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
    public partial class frmRenewLocalLicenseApplicationInfo : Form
    {
        clsLicenses _OldLocalLic;
        clsLDLA _OldLDLA;
        int _OldLicenseID = -1;
        
        clsLicenses _NewLocalLic;
        clsLDLA _NewLDLA;
        int _NewLicenseID = -1;

        int _UserID;
      
        public frmRenewLocalLicenseApplicationInfo(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;

        }

        private void btnFindLicense_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtLicenseID.Text, out _OldLicenseID))
            {
                _OldLocalLic = clsLicenses.FindByLicenseID(_OldLicenseID);
                _OldLDLA = clsLDLA.FindByAppID(_OldLocalLic.ApplicationID);
                if (_OldLDLA == null)
                {
                    MessageBox.Show("This License not found !");
                    return;
                }
                ucRenewLocalApplicationInfo1.LoadRenewLocalApplicationInfo(_UserID, _OldLicenseID, -1);
                urDriverLIcenseInfo1.LoadInfo(_OldLDLA.LocalDrivingLicenseApplicationID);
                llblShowLicenseHistory.Enabled = true;


            }
            else
            {
                MessageBox.Show("License ID Should be a number !", "Warning", MessageBoxButtons.OK);
            }

            

            if (_OldLocalLic.ExpirationDate > DateTime.Now)
            {
                MessageBox.Show("Selected License is not expiared,it will expire on :  " + _OldLocalLic.ExpirationDate, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);

                ucRenewLocalApplicationInfo1.LoadRenewLocalApplicationInfo(_UserID, _OldLicenseID, -1);
                btnRenew.Enabled = false;
                llblShowNewLicense.Enabled = false;
                llblShowLicenseHistory.Enabled = true;
            }
            else
            {
                btnRenew.Enabled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
          clsApplications  _NewApp = new clsApplications();
            clsApplications OldApplication = clsApplications.Find(_OldLDLA.ApplicationID);

            _NewApp.ApplicantPersonID = OldApplication.ApplicantPersonID;
            _NewApp.ApplicationDate = DateTime.Now;
            _NewApp.ApplicationTypeID = OldApplication.ApplicationTypeID;
            _NewApp.ApplicationStatus = OldApplication.ApplicationStatus;
            _NewApp.LastStatusDate = DateTime.Now;
            _NewApp.PaidFees = OldApplication.PaidFees;
            _NewApp.CreatedByUserID = _UserID;

            int appId = -1;
            if(_NewApp.AddNewApplication(ref appId))
            {
                //MessageBox.Show("New Application Save is Done !");

                _NewLDLA = new clsLDLA();
                _NewLDLA.ApplicationID = appId;
                _NewLDLA.LicenseClassID = _OldLDLA.LicenseClassID;

                if(_NewLDLA.Save())
                {
                    //MessageBox.Show("New New LDLA Save is Done !");
                    _NewLocalLic = new clsLicenses();
                    _NewLocalLic.ApplicationID = _NewLDLA.ApplicationID;
                    _NewLocalLic.DriverID = _OldLocalLic.DriverID;
                    _NewLocalLic.LicenseClass = _NewLDLA.LicenseClassID;
                    _NewLocalLic.IssueDate = DateTime.Now;
                    clsLicenseClasses LicenseClasses = clsLicenseClasses.Find(_NewLDLA.LicenseClassID);
                    _NewLocalLic.ExpirationDate = DateTime.Now.AddDays(LicenseClasses.DefaultValidityLength);
                    _NewLocalLic.Notes = "";
                    _NewLocalLic.PaidFees = _OldLocalLic.PaidFees;
                    _NewLocalLic.IsActive = true;
                    _NewLocalLic.IssueReason = 2;
                    _NewLocalLic.CreatedByUserID =  _UserID;
                    _OldLocalLic.IsActive = false;
                    if (_NewLocalLic.Save())
                    {
                        //MessageBox.Show(" New LocalLic Save is Done !");

                        if (_OldLocalLic.Save())
                        {
                            MessageBox.Show(" SAVE is Done !");

                            clsLicenses NewLicForFindIndo = clsLicenses.FindByAppID(_NewLDLA.ApplicationID);
                            ucRenewLocalApplicationInfo1.LoadRenewLocalApplicationInfo(_UserID, _OldLicenseID, NewLicForFindIndo.LicenseID);
                            btnRenew.Enabled = false;
                            llblShowNewLicense.Enabled = true;
                        }
                    }
                }
            }
            


        }

        private void llblShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = clsApplications.Find(_OldLDLA.ApplicationID).ApplicantPersonID;
            int DriverID = clsDrivers.Find(PersonID).DriverID;
            Form frm = new frmLicenseHistory(PersonID, DriverID);
            frm.ShowDialog();
        }

        private void llblShowNewLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmDriverLIcenseInfo(_NewLDLA.LocalDrivingLicenseApplicationID);
            frm.ShowDialog();
            
        }
    }
}
