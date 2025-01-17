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
    public partial class frmManagePeople : Form
    {
       
        public frmManagePeople()
        {
            InitializeComponent();
        }

       private void _RefreshPeopleList()
        {
                dgvPeoplelist.DataSource = clsPeople.GetAllPeople();
            _GetRecords();
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _RefreshPeopleList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditPerson((int)dgvPeoplelist.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void _GetRecords()
        {
            lblRecords.Text = dgvPeoplelist.Rows.Count.ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditPerson(-1);
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditPerson(-1);
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsPeople.DeletePerson((int)dgvPeoplelist.CurrentRow.Cells[0].Value);
        }

        
    }
}
