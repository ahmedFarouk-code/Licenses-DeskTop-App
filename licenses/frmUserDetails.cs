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
    public partial class frmUserDetails : Form
    {
        private int _UserID;
        private clsUsers _User;
        public frmUserDetails(int UserID)
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

       
    }
}
