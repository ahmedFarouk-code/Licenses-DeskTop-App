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
    public partial class frmDriverLIcenseInfo : Form
    {
        private int  _LDLAid;
        public frmDriverLIcenseInfo(int LDLAid)
        {
            InitializeComponent();
            _LDLAid = LDLAid;
        }

        private void _Load()
        {
            clsLDLA clsLDLA = clsLDLA.Find(_LDLAid);
            clsApplications applications = clsApplications.Find(clsLDLA.ApplicationID);
            clsPeople Person = clsPeople.Find(applications.ApplicantPersonID);
            clsLicenses licenses = clsLicenses.FindByAppID(clsLDLA.ApplicationID);
            clsLicenseClasses classes = clsLicenseClasses.Find(licenses.LicenseClass);

            lblName.Text = Person.FirstName + " " + Person.SecondName + " " + Person.ThirdName + " " + Person.LastName;
            lblNationalNo.Text = Person.NationalNo;
            if (Person.Gendor == 0)
                lblGendor.Text = "Male";
            else
            {
                lblGendor.Text = "Female";
            }

            lblDateOfBirth.Text = Person.DateOfBirth.ToString();
            lblLicenseID.Text = licenses.LicenseID.ToString();
            lblDriverID.Text = licenses.DriverID.ToString();
            lblIssueDate.Text = licenses.IssueDate.ToString();
            if(licenses.Notes != "")
            lblNotes.Text = licenses.Notes.ToString();
            else
            {
                lblNotes.Text = "No Notes";
            }
            lblEpirationDate.Text = licenses.ExpirationDate.ToString();
            lblIssueReason.Text = "New";
            lblIsActive.Text = licenses.IsActive.ToString();
            lblClass.Text = classes.LicenseClasseName;
            lblisDetained.Text = "No";
        }

        private void frmDriverLIcenseInfo_Load(object sender, EventArgs e)
        {
            _Load();
        }

       
    }
}
