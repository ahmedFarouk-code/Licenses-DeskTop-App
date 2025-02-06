using LicensesBusinessLayer;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.NetworkInformation;

namespace licensesApp
{
    public partial class frmLoginScreen : Form
    {
        private clsUsers _User;

        string filePath = "D:\\Licenses-DeskTop-App\\licenses\\LastUserAndPass.txt";
        public frmLoginScreen()
        {
            InitializeComponent();
            cbRemmberMe.Checked = true;

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath); // قراءة جميع الأسطر

                if (lines.Length > 0) txtUserName.Text = lines[0]; // السطر الأول
                if (lines.Length > 1) txtPassword.Text = lines[1]; // السطر الثاني
             
            }
            else
            {
                MessageBox.Show("[File is not Exist!");
            }
        }

        private void isHaveAccesse()
        {
            _User = clsUsers.Find(txtUserName.Text, txtPassword.Text);

            if (_User != null)
            {
                if (_User.IsActive)
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
           
            if (cbRemmberMe.Checked == true)
            {

                string content = txtUserName.Text + Environment.NewLine +
                                 txtPassword.Text;
                File.WriteAllText(filePath, content);
            }
            else
            {
                string content = "" + Environment.NewLine +
                                 "";
                File.WriteAllText(filePath, content);
            }
            



            isHaveAccesse();
        }
    }
}
