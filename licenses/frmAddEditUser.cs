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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace licensesApp
{
    public partial class frmAddEditUser : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        DataTable peopleTable = clsPeople.GetAllPeople();
        clsPeople _Person;
        int _PersonID;

        clsUsers _User;
        int _UserID;




        public frmAddEditUser(int PersonID)
        {
            InitializeComponent();
           _PersonID = PersonID;
           if (_PersonID == -1)
               _Mode = enMode.AddNew;

           else
           {
                _Mode = enMode.Update;
               
            }
            UserControl2PersonDetailsWithFilter.PersonID = _PersonID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

       

       

        

        private void btnNext_Click(object sender, EventArgs e)
        {
         TabControl1.SelectedTab = tpLoginInfo;
        }



        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                e.Cancel = true;
                txtUserName.Focus();
                errorProvider1.SetError(txtUserName, "UserName should have a value!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName, "");
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                e.Cancel = true;
                txtPassword.Focus();
                errorProvider1.SetError(txtPassword, "Password should have a value!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPassword, "");
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text != txtPassword.Text)
            {

                txtConfirmPassword.Focus();
                errorProvider1.SetError(txtConfirmPassword, "Password should have a value!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, "");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            
                if (_Mode == enMode.AddNew)
                {
                    _User = new clsUsers();

                    
                }
                _User.PersonID = UserControl2PersonDetailsWithFilter.PersonID;
                _User.UserName = txtUserName.Text;
                _User.Password = txtPassword.Text;
                if(cbIsActive.Checked)
                {
                    _User.IsActive = true;
                }
                else
                {
                    _User.IsActive = true;
                }
               
                if (_User.Save())
                MessageBox.Show("Data Saved Successfully.");
                  else
                  MessageBox.Show("Error: Data Is not Saved Successfully.");


                _Mode = enMode.Update;

           
            

        }

        private  void _Load()
        {
            if (_PersonID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;


            if (_Mode == enMode.AddNew)
            {
                _User = new clsUsers();
               
                return;
            }

           
            UserControl2PersonDetailsWithFilter.PersonID = _PersonID;
            _User = clsUsers.Find( _PersonID );
            
            txtTitleName.Text = "Update User Info";
               
               
                

              
                if (_User == null)
                {
                    MessageBox.Show("This form will be closed because No Person with ID");
                    return;
                }
                lblUserID.Text = _User.UserID.ToString();
                txtUserName.Text = _User.UserName;
                txtPassword.Text = _User.Password;
                txtConfirmPassword.Text = _User.Password;
                if (_User.IsActive == true)
                {
                    cbIsActive.Checked = true;
                }
            
            _Mode = enMode.Update;
        }

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _Load();
        }




    }
}
