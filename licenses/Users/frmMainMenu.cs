using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LicensesBusinessLayer;
namespace licensesApp
{
    public partial class frmMainMenu : Form
    {
        private int _UserID;
        public frmMainMenu(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmManagePeople();
            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmManageUsers();
            frm.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmUserDetails(_UserID);
            frm.ShowDialog();
            
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmChangePassword(_UserID);
            frm.ShowDialog();
            
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmManageApplicationTypes();
            frm.ShowDialog();
        }

        private void manageTestTyoesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmManageTestTypes();
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListLocalDLA(_UserID);
             frm.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditLDLA(_UserID ,-1);
            frm.ShowDialog();
        }

       
    }
}
