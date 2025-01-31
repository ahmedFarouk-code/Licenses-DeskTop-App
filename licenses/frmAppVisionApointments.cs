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
    public partial class frmAppVisionApointments : Form
    {
        private int _LDLAi;
        private int _PassedTests;
        public frmAppVisionApointments(int lDLAi, int passedTests)
        {
            InitializeComponent();
            _LDLAi = lDLAi;
            _PassedTests = passedTests;
            UserControlAppDetails.LDLid = _LDLAi;
            UserControlAppDetails.PassedTests = _PassedTests;

        }

        private void _Load()
        {
           DataTable dt = new DataTable();

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

        }

        private void frmAppVisionApointments_Load(object sender, EventArgs e)
        {
            _Load();
        }
    }
}
