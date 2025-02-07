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
    public partial class ucIntarnationalAppInfo : UserControl
    {
        clsLDLA LDLA;
        clsLicenses Licenses;
        clsInternationalLicenses InternationalLicenses;


        private int _UserID;
        private int _LDLid = -1;

        public ucIntarnationalAppInfo()
        {
            InitializeComponent();
        }

        public void LoadIntarnationalAppInfo(int UserID , int LDLid)
        {
            _UserID = UserID;
            _LDLid = LDLid;

            lblAppDate.Text = DateTime.Now.ToString();
            lblIssueDate.Text = DateTime.Now.ToString();
            lblFees.Text = "51";
            
            if (_LDLid != -1)
            {
                lblLocalLicenseID.Text = LDLid.ToString();

                InternationalLicenses = clsInternationalLicenses.FindByLDLid(_LDLid);
                if (InternationalLicenses  != null)
                {
                    lblLicenseID.Text = InternationalLicenses.InternationalLicenseID.ToString();
                    lblInternationalLAPPlicationID.Text = InternationalLicenses.ApplicationID.ToString();
                }
                else
                {
                    lblInternationalLAPPlicationID.Text = "???";
                    lblInternationalLAPPlicationID.Text = "???";
                }   
            }
            lblExpDate.Text = DateTime.Now.AddYears(1).ToString();
            lblCreatedBy.Text = _UserID.ToString();

           


        }
    }
}
