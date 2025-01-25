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
    public partial class frmManageTestTypes : Form
    {
        DataTable TableAllTestTypes = clsTestTypes.GetAllTestTypes();
        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        private void _RefreshTestTypesList()
        {
            dgvTestTypes.DataSource = clsTestTypes.GetAllTestTypes();
            lblRecordes.Text = TableAllTestTypes.Rows.Count.ToString();
        }

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmEditTestType((int)dgvTestTypes.CurrentRow.Cells[0].Value);
               frm.ShowDialog();
              _RefreshTestTypesList();
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshTestTypesList();
        }
    }
}
