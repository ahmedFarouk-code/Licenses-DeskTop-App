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
    public partial class UserControlPersonDetails : UserControl
    {
        public UserControlPersonDetails()
        {
            InitializeComponent();
        }
        public int PersonID { get; set; }
        clsPeople _Person;
        private void llblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmAddEditPerson(PersonID);
            frm.ShowDialog();
        }
        public void ReloadData()
        {
            _Person = clsPeople.Find(PersonID);

            if (_Person == null)
            {
                MessageBox.Show("this person not found");


                return;
            }

            lblPersonID.Text = _Person.ID.ToString();
            lblName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;
            lblNationalNo.Text = _Person.NationalNo;
            lblEmail.Text = _Person.Email;
            lblPhone.Text = _Person.Phone;
            lblAddress.Text = _Person.Address;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString();
            if (_Person.ImagePath != "")
            {

                if (_Person.Gendor == 0)
                {
                    lblGendor.Text = "Male";
                }
                else
                {
                    lblGendor.Text = "Female";
                }
                pictureBox1.Load(_Person.ImagePath);
            }

            else
            {

                if (_Person.Gendor == 0)
                {
                    lblGendor.Text = "Male";
                    pictureBox1.Load("D:\\Licenses-DeskTop-App\\licenses\\Images\\person_man.PNG");
                }
                else
                {
                    lblGendor.Text = "Female";
                    pictureBox1.Load("D:\\Licenses-DeskTop-App\\licenses\\Images\\person_woman.PNG");
                }
            }
        }

        private void UserControlPersonDetails_Load(object sender, EventArgs e)
        {

            ReloadData();
        }
    }
}
