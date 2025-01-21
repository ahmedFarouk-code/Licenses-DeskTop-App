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
    public partial class frmLoginScreen : Form
    {
        private clsUsers _User;
       
        public frmLoginScreen()
        {
            InitializeComponent();
        }

        private  void isHaveAccesse()
        {
            _User = clsUsers.Find(txtUserName.Text, txtPassword.Text);
           
                if (_User != null)
                {
                  if(_User.IsActive)
                  {
                    Form frm = new frmMainMenu(_User.UserID);
                    frm.ShowDialog();
                  }
                  else
                  {
                    MessageBox.Show("Your Account Is Not Active (Contact Your Admin)", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                }

                else
                {
                    MessageBox.Show("User Name Or Password Is Incorrect", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            isHaveAccesse();
        }
    }
}
