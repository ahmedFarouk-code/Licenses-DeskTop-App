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
    public partial class frmEditTestType : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        clsTestTypes _TestType;
        private int _TestTypeID = -1;
        public frmEditTestType(int TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
        }

        private void _Load()
        {
            _TestType = clsTestTypes.Find(_TestTypeID);
            lblID.Text = _TestType.TestTypeID.ToString();
            txtTitle.Text = _TestType.TestTypeTitle.ToString();
            txtDescription.Text = _TestType.TestTypeDescription.ToString();
            txtFees.Text = _TestType.TestTypeFees.ToString();


        }

        private void femEditTestType_Load(object sender, EventArgs e)
        {
            _Load();
        }

        private void lblSave_Click(object sender, EventArgs e)
        {
            _TestType.TestTypeTitle = txtTitle.Text;
            _TestType.TestTypeDescription = txtTitle.Text;
            _TestType.TestTypeFees = Convert.ToDecimal(txtFees.Text);

            if (_TestType.Save())


                MessageBox.Show("Data Saved Successfully.");
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.");
        }

        private void clsClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
