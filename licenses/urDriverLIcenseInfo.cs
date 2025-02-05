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
    public partial class urDriverLIcenseInfo : UserControl
    {
        clsLDLA _LDLA;
        clsApplications _applications;
        clsPeople _Person;
        clsLicenses _licenses;
        clsLicenseClasses _classes;

        public urDriverLIcenseInfo()
        {
            InitializeComponent();
        }


        public void LoadInfo(int LDLAid)
        {
             _LDLA = clsLDLA.Find(LDLAid);
             _applications = clsApplications.Find(_LDLA.ApplicationID);
             _Person = clsPeople.Find(_applications.ApplicantPersonID);
             _licenses = clsLicenses.FindByAppID(_LDLA.ApplicationID);
             _classes = clsLicenseClasses.Find(_licenses.LicenseClass);

            lblName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;
            lblNationalNo.Text = _Person.NationalNo;
            if (_Person.Gendor == 0)
                lblGendor.Text = "Male";
            else
            {
                lblGendor.Text = "Female";
            }

            lblDateOfBirth.Text = _Person.DateOfBirth.ToString();
            lblLicenseID.Text = _licenses.LicenseID.ToString();
            lblDriverID.Text = _licenses.DriverID.ToString();
            lblIssueDate.Text = _licenses.IssueDate.ToString();
            if (_licenses.Notes != "")
                lblNotes.Text = _licenses.Notes.ToString();
            else
            {
                lblNotes.Text = "No Notes";
            }
            lblEpirationDate.Text = _licenses.ExpirationDate.ToString();
            lblIssueReason.Text = "New";
            lblIsActive.Text = _licenses.IsActive.ToString();
            lblClass.Text = _classes.LicenseClasseName;
            lblisDetained.Text = "No";
        }

        private void urDriverLIcenseInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
