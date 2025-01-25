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
    public partial class frmChangePassword : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.Update;
        private int _UserID;
        private clsUsers _User;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            
            _User = clsUsers.FindByUserID(UserID);
            userControlPersonDetails1.PersonID = _User.PersonID;
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName;
            if (_User.IsActive == true)
            {
                lblIsActive.Text = "Yes";
                lblIsActive.ForeColor = Color.Green;
            }
            else
            {
                lblIsActive.Text = "No";
                lblIsActive.ForeColor = Color.Red;
            }

            userControlPersonDetails1.ReloadData();
        }

        private void _Save()
        {
            if(_User.Password != txtCurrentPass.Text)
            {
                MessageBox.Show("Current Password is not Correct !!! .", "Warrning", MessageBoxButtons.OK);
                return;
            }
            if (_User != null)
            {

                _User.Password = txtNewPass.Text;

                if (_User.Save())
                    MessageBox.Show("Data Saved Successfully.");
                else
                    MessageBox.Show("Error: Data Is not Saved Successfully.");


                _Mode = enMode.Update;
            }
            else
            {
                MessageBox.Show("Data Saved Faild !!! .", "Warrning", MessageBoxButtons.OK);
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Save();
        }

        private void ConfirmPass_Validating(object sender, CancelEventArgs e)
        {
            if (ConfirmPass.Text != txtNewPass.Text)
            {
                e.Cancel = true;
                ConfirmPass.Focus();
                errorProvider1.SetError(ConfirmPass, "ConfirmPass should have a value!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(ConfirmPass, "");
            }
        }
    }
}
