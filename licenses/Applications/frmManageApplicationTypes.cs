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
    public partial class frmManageApplicationTypes : Form
    {
        DataTable TableApplicationTypes = clsApplicationTypes.GetAllApplicationTypes();
        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }

        private  void _RefreshApplicationsList()
        {
            dgvApplicationTypes.DataSource = clsApplicationTypes.GetAllApplicationTypes();
            lblRecordes.Text = TableApplicationTypes.Rows.Count.ToString();
        }

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _RefreshApplicationsList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmUpdateApplicationType((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshApplicationsList();
        }
    }
}
