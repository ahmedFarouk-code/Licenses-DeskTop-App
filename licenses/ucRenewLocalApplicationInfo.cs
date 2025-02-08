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
    public partial class ucRenewLocalApplicationInfo : UserControl
    {
       
        clsLicenses _OldLicenses;
        clsLicenses _NewLicenses;
        clsLicenseClasses _LicenseClasses;

        private int _UserID;
        private int _OldLDLid = -1;
        private int _NewLicenseID = -1;
        public ucRenewLocalApplicationInfo()
        {
            InitializeComponent();
        }

        public void LoadRenewLocalApplicationInfo(int UserID, int OldLicensesID ,int NewLicenseID)
        {
            _UserID = UserID;
            _OldLDLid = OldLicensesID;
            _NewLicenseID = NewLicenseID;

            lblAppDate.Text = DateTime.Now.ToString();
            lblIssueDate.Text = DateTime.Now.ToString();
            lblAppFees.Text = 7.ToString();
            lblCreatedBy.Text = clsUsers.FindByUserID(_UserID).UserName;
         
            if(OldLicensesID != -1)
            {
                _OldLicenses = clsLicenses.FindByLicenseID(_OldLDLid);
                _LicenseClasses = clsLicenseClasses.Find(_OldLicenses.LicenseClass);

                lblOldLicenseID.Text = _OldLDLid.ToString();
                lblLicenseFees.Text = _LicenseClasses.ClassFees.ToString();
                lblTotalFees.Text = ((int)_LicenseClasses.ClassFees + 7).ToString();
                lblExpirationDate.Text = DateTime.Now.AddDays(_LicenseClasses.DefaultValidityLength).ToString(); 
            }

            if(NewLicenseID != -1)
            {
                _NewLicenses = clsLicenses.FindByLicenseID(NewLicenseID);
                lblRenewedLicenseID.Text = _NewLicenses.LicenseID.ToString();
                clsApplications app = clsApplications.Find(_NewLicenses.ApplicationID);
               lblAppDate.Text = app.ApplicationDate.ToString();
                lblRLocalApplicationID.Text = _NewLicenses.ApplicationID.ToString();
            }

            
        }




    }
}
