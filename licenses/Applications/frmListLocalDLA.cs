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
    public partial class frmListLocalDLA : Form
    {
        private int _UserID;
        public frmListLocalDLA(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            cbFilterby.SelectedIndex = 0;
        }
        DataTable LDLAList = new DataTable();
        private void _LDLA_TableColumns()
        {
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "L.D.AppID";
            LDLAList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "Driving Class";
            LDLAList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "NationalNo";
            LDLAList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "Full Name";
            LDLAList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "Application Date";
            LDLAList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "Passed Tests";
            LDLAList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "Status";
            LDLAList.Columns.Add(dtColumn);
        }

        private void GetLDLAInfo()
        {
            _LDLA_TableColumns();

            DataTable LDLA = new DataTable();
            LDLA = clsLDLA.GetALL_LDLA();
           

            DataTable LicenseClassesTable = new DataTable();
            LicenseClassesTable = clsLicenseClasses.GetAllLicenseClasses();

            DataTable ApplicationsTable = new DataTable();
            ApplicationsTable = clsApplications.GetAllApplications();

            DataTable PersonsTable = new DataTable();
            PersonsTable = clsPeople.GetAllPeople();

            DataTable TestAppointmentsTable = new DataTable();
            TestAppointmentsTable = clsTestAppointments.GetAllTestAppointments();

            DataTable TestsTable = new DataTable();
            TestsTable = clsTests.GetAllTests();

            foreach (DataRow LDLARecordRow in LDLA.Rows)
            {



                DataRow[] LicenseClassesRestults = LicenseClassesTable.Select("LicenseClassID =" + LDLARecordRow["LicenseClassID"]);
                foreach (var LicenseClassesRecordRow in LicenseClassesRestults)
                {

                    LDLAList.Rows.Add(LDLARecordRow["LocalDrivingLicenseApplicationID"], LicenseClassesRecordRow["ClassName"], null, null, null, 0, null);

                    
                }

            }
            foreach (DataRow RecordRow in LDLAList.Rows)
            {
                DataRow[] LDLARRestults = LDLA.Select("LocalDrivingLicenseApplicationID =" + (int)RecordRow["L.D.AppID"]);
                foreach (var LDLARRecordRow in LDLARRestults)
                {

                    DataRow[] ApplicationsRestults = ApplicationsTable.Select("ApplicationID =" + (int)LDLARRecordRow["ApplicationID"]);
                    foreach (var ApplicationsRecordRow in ApplicationsRestults)
                    {
                        RecordRow["Application Date"] = ApplicationsRecordRow["ApplicationDate"];
                        byte AppStatus = (byte)ApplicationsRecordRow["ApplicationStatus"];

                        if (AppStatus == 1)
                        {
                            RecordRow["Status"] = "New";
                        }
                        else if (AppStatus == 2)
                        {
                            RecordRow["Status"] = "Canceld";
                        }
                        else
                        {
                            RecordRow["Status"] = "Completed";
                        }



                        DataRow[] PersonsTableRestults = PersonsTable.Select("PersonID =" + ApplicationsRecordRow["ApplicantPersonID"]);
                        foreach (var PersonsTableRecordRow in PersonsTableRestults)
                        {
                            RecordRow["NationalNo"] = PersonsTableRecordRow["NationalNo"];
                            RecordRow["Full Name"] = PersonsTableRecordRow["FirstName"].ToString() + " " + PersonsTableRecordRow["SecondName"] + " " + PersonsTableRecordRow["ThirdName"] + " " + PersonsTableRecordRow["LastName"];
                        }

                        DataRow[] TestAppointmentsRestults = TestAppointmentsTable.Select("LocalDrivingLicenseApplicationID =" + RecordRow["L.D.AppID"]);
                        foreach (var TestAppointmentsRecordRow in TestAppointmentsRestults)
                        {

                            DataRow[] TestsRestults = TestsTable.Select("TestAppointmentID =" + TestAppointmentsRecordRow["TestAppointmentID"]);
                           
                            foreach (var TestsRecordRow in TestsRestults)
                            {
                                bool testResult = (bool)TestsRecordRow["TestResult"];
                                int PassedTests = (int)RecordRow["Passed Tests"];
                                if (testResult == true)
                                {
                                   
                                    PassedTests++;
                                    RecordRow["Passed Tests"] = PassedTests;
                                }

                            }

                        }


                    }
                }

            }
            dgvLocalApplicationList.DataSource = LDLAList;


        }

   


        private void frmListLocalDLA_Load(object sender, EventArgs e)
        {
            GetLDLAInfo();
            dgvLocalApplicationList.Columns[0].Width = 75;
            dgvLocalApplicationList.Columns[1].Width = 200;
            dgvLocalApplicationList.Columns[2].Width = 75;
            dgvLocalApplicationList.Columns[3].Width = 250;
            dgvLocalApplicationList.Columns[4].Width = 150;

        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditLDLA(_UserID, (int)dgvLocalApplicationList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

        }
    }
}
