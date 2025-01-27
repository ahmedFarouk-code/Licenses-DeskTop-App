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
    public partial class frmAddEditLDLA : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        int _PersonID;
        int _UserID;
        int _ApplicationID;
        public frmAddEditLDLA(int PersonID ,int UserID , int ApplicationID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            _UserID = UserID;
            if(_ApplicationID == -1)
            {
                Mode = enMode.AddNew;
            }
            else
            {
                Mode = enMode.Update;
            }
            UserControl2PersonDetailsWithFilter.PersonID = _PersonID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddEditLDLA_Load(object sender, EventArgs e)
        {

        }
    }
}
