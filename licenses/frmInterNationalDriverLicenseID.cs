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
    public partial class frmInterNationalDriverLicenseID : Form
    {
        int _LDLAid;
        private clsPeople _Person;
        private clsInternationalLicenses _InternationalLicenses;
        public frmInterNationalDriverLicenseID(int LDLAid)
        {
            InitializeComponent();
            _LDLAid = LDLAid;
        }




        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _Load()
        {
            _InternationalLicenses = clsInternationalLicenses.FindByLDLid(_LDLAid);
           
            lblIDLid.Text = _InternationalLicenses.InternationalLicenseID.ToString();
            lblLDLA.Text = _LDLAid.ToString();
            lblIssueDate.Text = _InternationalLicenses.IssueDate.ToString();
            lblApplicationID.Text = _InternationalLicenses.ApplicationID.ToString();
            lblIsActive.Text = _InternationalLicenses.IsActive.ToString();
            lblDriverID.Text = _InternationalLicenses.DriverID.ToString();
            lblEpirationDate.Text = _InternationalLicenses.ExpirationDate.ToString();

            int PersonID = clsDrivers.FindDriverID(_InternationalLicenses.DriverID).PersonID;
             _Person = clsPeople.Find(PersonID);
            lblName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;
            if(_Person.Gendor == 0)
            {
                lblGendor.Text = "Male";
            }
            else
            {
                lblGendor.Text = "Female";
            }
            
            lblDataOfBirth.Text = _Person.DateOfBirth.ToString();
            lblNationalNo.Text = _Person.NationalNo.ToString();

            if (_Person.ImagePath != "")
            {
                pbPerson.Load(_Person.ImagePath);
            }

            else
            {
                if (_Person.Gendor == 0)
                {

                    pbPerson.Load("D:\\Licenses-DeskTop-App\\licenses\\Images\\person_man.PNG");
                }
                else
                {
                    pbPerson.Load("D:\\Licenses-DeskTop-App\\licenses\\Images\\person_woman.PNG");
                }
            }



        }

        private void frmInterNationalDriverLicenseID_Load(object sender, EventArgs e)
        {
            _Load();
        }
    }
}
