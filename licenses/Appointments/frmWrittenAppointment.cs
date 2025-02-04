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
    public partial class frmWrittenAppointment : Form
    {
        private int _LDLAi;
        private int _PassedTests;
        private int _UserID;
        DataTable dt = new DataTable();
        public frmWrittenAppointment(int lDLAi, int passedTests, int UserID)
        {
            InitializeComponent();
            _LDLAi = lDLAi;
            _PassedTests = passedTests;
            _UserID = UserID;
            UserControlAppDetails.LDLid = _LDLAi;
            UserControlAppDetails.PassedTests = _PassedTests;
        }

        private void _Load()
        {
            dgvAppointmentList.Columns.Clear();
             dt = new DataTable();

            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "Appointment ID";
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "Appointment Date";
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(decimal);
            dtColumn.ColumnName = "Paid Feees";
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(bool);
            dtColumn.ColumnName = "Is Locked";
            dt.Columns.Add(dtColumn);

            DataTable AppointMentsTable = clsTestAppointments.GetAllTestAppointments();


            foreach (DataRow AppointMentsRecordRow in AppointMentsTable.Rows)
            {
                if ((int)AppointMentsRecordRow["LocalDrivingLicenseApplicationID"] == _LDLAi &&
                        (int)AppointMentsRecordRow["TestTypeID"] == 2)
                {
                    dt.Rows.Add(AppointMentsRecordRow["TestAppointmentID"], AppointMentsRecordRow["AppointmentDate"], AppointMentsRecordRow["PaidFees"], AppointMentsRecordRow["IsLocked"]);

                }
            }
            dgvAppointmentList.DataSource = dt;
            lblRecords.Text = dt.Rows.Count.ToString();

        }

        private void frmWrittenAppointment_Load(object sender, EventArgs e)
        {
            _Load();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmScheduleTest(_LDLAi, (int)dgvAppointmentList.CurrentRow.Cells[0].Value, _UserID, 2);
            frm.ShowDialog();
            _Load();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            foreach (DataRow dtRecordRow in dt.Rows)
            {
                if ((bool)dtRecordRow["Is Locked"] == false)
                {
                    MessageBox.Show("Person already have an active appointment for this test , you connot Add new appointment", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
            }
            Form frm = new frmScheduleTest(_LDLAi, -1, _UserID, 2);
            frm.ShowDialog();
            _Load();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmWrittenTest (_LDLAi, (int)dgvAppointmentList.CurrentRow.Cells[0].Value, _UserID);
            frm.ShowDialog();
            _Load();
        }

       
    }
}
