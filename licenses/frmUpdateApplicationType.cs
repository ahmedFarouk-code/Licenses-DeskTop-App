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
    public partial class frmUpdateApplicationType : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        clsApplicationTypes _ApplicationTypes;
        private int _ApplicationTypeID = -1;

        public frmUpdateApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
            
        }

        private  void _Load()
        {
            _ApplicationTypes = clsApplicationTypes.Find(_ApplicationTypeID);
            lblID.Text = _ApplicationTypes.ApplicationTypeID.ToString();
            txtTitle.Text = _ApplicationTypes.ApplicationTypeTitle.ToString();
            txtFees.Text = _ApplicationTypes.ApplicationFees.ToString();

        }

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _Load();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            _ApplicationTypes.ApplicationTypeTitle = txtTitle.Text;
            _ApplicationTypes.ApplicationFees = Convert.ToDecimal (txtFees.Text);

           if(_ApplicationTypes.Save())
            

                MessageBox.Show("Data Saved Successfully.");
            else
                    MessageBox.Show("Error: Data Is not Saved Successfully.");
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
