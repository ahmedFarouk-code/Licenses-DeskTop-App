using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LicensesBusinessLayer;
using static System.Net.Mime.MediaTypeNames;

namespace licensesApp
{
    public partial class UserControl1 : UserControl
    {
      
         public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        
        clsPeople _Person;
        public int PersonID { get; set; }
       
        
        public UserControl1()
        {
            InitializeComponent();
           
        }

        private  void _FillCountry()
        {
            DataTable dt = clsCountry.GetAllCountries();
            foreach (DataRow row in dt.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
        }

        private void _LoadData()
        {
            if (PersonID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            _FillCountry();
            cbCountry.SelectedIndex = cbCountry.FindString("Jordan");
            if (_Mode == enMode.AddNew)
            {
                _Person = new clsPeople();
                return;
            }

            _Person = clsPeople.Find(PersonID);

            if (_Person == null)
            {
                //MessageBox.Show("This form will be closed because No Person with ID = " + PersonID);


                return;
            }

            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text += _Person.LastName;
            txtNationalNo.Text = _Person.NationalNo;
            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;
            txtAddress.Text = _Person.Address;
            cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.Find(_Person.NationalityCountryID).CountryName);
            dtpDateOfBirth.Value=_Person.DateOfBirth;
            if (_Person.ImagePath != "")
            {
               
                if (_Person.Gendor == 0)
                {
                    rbMale.Checked = true;
                }
                else
                {
                    rbFemale.Checked = true;
                }
                pictureBox1.Load(_Person.ImagePath);
            }
            
            else
            {

                if (_Person.Gendor == 0)
                {
                    rbMale.Checked = true;
                    pictureBox1.Load("D:\\Licenses-DeskTop-App\\licenses\\Images\\person_man.PNG");
                }
                else
                {
                    rbFemale.Checked = true;
                    pictureBox1.Load("D:\\Licenses-DeskTop-App\\licenses\\Images\\person_woman.PNG");
                }
            }
            _Mode = enMode.Update;
        }
       


private void UserControl1_Load(object sender, EventArgs e)
        {
            _LoadData();
        }



        private void llblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = false;
            openFileDialog1.InitialDirectory = _Person.ImagePath;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                //MessageBox.Show("Selected Image is:" + selectedFilePath);

                pictureBox1.Load(selectedFilePath);
                // ...
            }
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Load("D:\\Licenses-DeskTop-App\\licenses\\Images\\person_man.PNG");

        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Load("D:\\Licenses-DeskTop-App\\licenses\\Images\\person_woman.PNG");

        }
        public event EventHandler RequestCloseForm;

        private void btnClose_Click(object sender, EventArgs e)
        {
            RequestCloseForm?.Invoke(this, EventArgs.Empty);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int CountryID = clsCountry.Find(cbCountry.Text).ID;

            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;
            _Person.NationalityCountryID = CountryID;
            _Person.Email = txtEmail.Text;
            _Person.Phone = txtPhone.Text;
            _Person.Address = txtAddress.Text;
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.NationalityCountryID = CountryID;
            if(rbFemale.Checked == true)
            {
                _Person.Gendor = 1;
            }
            else
            {
                _Person.Gendor= 0;
            }



            if (pictureBox1.ImageLocation != null)
                _Person.ImagePath = pictureBox1.ImageLocation;
            else
                _Person.ImagePath = "";

            if (_Person.Save())
                MessageBox.Show("Data Saved Successfully.");
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.");

            _Mode = enMode.Update;
           
           
        }
    }
}
