using licensesApp;
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
    public partial class frmAppApointments : Form
    {
        private int _LDLAi;
        private int _PassedTests;
        private int _TestTypeid;
        private int _UserID;
        DataTable dt = new DataTable();
        enum enAppointMentType { Vision = 1 ,Written = 2 , Street = 3}
        enAppointMentType _TestType;
       
        public frmAppApointments(int lDLAi, int passedTests , int TestType ,int UserID)
        {
            InitializeComponent();
            _LDLAi = lDLAi;
            _PassedTests = passedTests;
            _TestTypeid = TestType;
            _UserID = UserID;
            UserControlAppDetails.LDLid = _LDLAi;
            UserControlAppDetails.PassedTests = _PassedTests;
            if (_TestTypeid == 1)
                _TestType = enAppointMentType.Vision;

            else if (_TestTypeid == 2)
                _TestType = enAppointMentType.Written;

            else
                _TestType = enAppointMentType.Street;

        }

        private void _Load()
        {
            if(_TestType == enAppointMentType.Vision)
            {
                pictureBox1.Image = Image.FromFile("D:\\Licenses-DeskTop-App\\licenses\\Images\\eye.png");
                lblTitle.Text = "Vision Test Appointments";
            }
            else if(_TestType == enAppointMentType.Written)
            {
                pictureBox1.Image = Image.FromFile("D:\\Licenses-DeskTop-App\\licenses\\Images\\pen.png");
                lblTitle.Text = "written Test Appointments";
            }
            else
            {
                pictureBox1.Image = Image.FromFile("D:\\Licenses-DeskTop-App\\licenses\\Images\\road.png");
                lblTitle.Text = "Street Test Appointments";
            }
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
                        (int)AppointMentsRecordRow["TestTypeID"] == 1)
                {
                    dt.Rows.Add(AppointMentsRecordRow["TestAppointmentID"], AppointMentsRecordRow["AppointmentDate"], AppointMentsRecordRow["PaidFees"], AppointMentsRecordRow["IsLocked"]);

                }
            }
            dgvAppointmentList.DataSource = dt;
            lblRecords.Text = dt.Rows.Count.ToString();

        }

        private void frmAppVisionApointments_Load(object sender, EventArgs e)
        {
            _Load();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            foreach(DataRow dtRecordRow in dt.Rows)
            {
                if ((bool)dtRecordRow["Is Locked"] == false)
                {
                    MessageBox.Show("Person already have an active appointment for this test , you connot Add new appointment" , "Not Allowed" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            Form frm = new frmScheduleTest(_LDLAi, -1, _TestTypeid , _UserID);
            frm.ShowDialog();

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmScheduleTest(_LDLAi, (int)dgvAppointmentList.CurrentRow.Cells[0].Value, _TestTypeid, _UserID);
            frm.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
