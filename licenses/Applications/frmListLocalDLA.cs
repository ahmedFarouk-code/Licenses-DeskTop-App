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
using static System.Net.Mime.MediaTypeNames;

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

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure to Cancel this application" ,"Cancel" ,MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                clsLDLA LDLA = clsLDLA.Find(((int)dgvLocalApplicationList.CurrentRow.Cells[0].Value));
                clsApplications App = clsApplications.Find(LDLA.ApplicationID);

                App.ApplicationStatus = 2;
                App.Mode = clsApplications.enMode.Update;
                if(App.Save())
                {
                    MessageBox.Show("Cancel App Successfully.");
                }
                else
                {
                    MessageBox.Show("Error: Cancel App not Saved Successfully.");
                }



            }
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to Delete this application", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                clsLDLA LDLA = clsLDLA.Find(((int)dgvLocalApplicationList.CurrentRow.Cells[0].Value));
                clsApplications App = clsApplications.Find(LDLA.ApplicationID);

                
                if (clsLDLA.DeletLDLA(LDLA.LocalDrivingLicenseApplicationID)&&
                    clsApplications.DeleteApplication(App.ApplicationID))
                {
                    MessageBox.Show("Cancel App Successfully.");
                }
                else
                {
                    MessageBox.Show("Error: Cancel App not Saved Successfully.");
                }
              


            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

             if ((int)dgvLocalApplicationList.CurrentRow.Cells[5].Value == 0)
            {
                sechduleTestsToolStripMenuItem.DropDownItems["scheduleVisionTestToolStripMenuItem"].Enabled = true;
                sechduleTestsToolStripMenuItem.DropDownItems["scheduleWrittenTestToolStripMenuItem"].Enabled = false;
                sechduleTestsToolStripMenuItem.DropDownItems["scheduleStreetTestToolStripMenuItem"].Enabled = false;
                sechduleTestsToolStripMenuItem.Enabled = true;
                issueDrivingLicenseToolStripMenuItem.Enabled=false;
            }

            else if ((int)dgvLocalApplicationList.CurrentRow.Cells[5].Value == 1)
            {
                sechduleTestsToolStripMenuItem.DropDownItems["scheduleVisionTestToolStripMenuItem"].Enabled = false;
                sechduleTestsToolStripMenuItem.DropDownItems["scheduleWrittenTestToolStripMenuItem"].Enabled = true;
                sechduleTestsToolStripMenuItem.DropDownItems["scheduleStreetTestToolStripMenuItem"].Enabled = false;
                sechduleTestsToolStripMenuItem.Enabled = true;
                issueDrivingLicenseToolStripMenuItem.Enabled = false;
            }

            else if ((int)dgvLocalApplicationList.CurrentRow.Cells[5].Value == 2)
            {
                sechduleTestsToolStripMenuItem.DropDownItems["scheduleVisionTestToolStripMenuItem"].Enabled = false;
                sechduleTestsToolStripMenuItem.DropDownItems["scheduleWrittenTestToolStripMenuItem"].Enabled = false;
                sechduleTestsToolStripMenuItem.DropDownItems["scheduleStreetTestToolStripMenuItem"].Enabled = true;
                sechduleTestsToolStripMenuItem.Enabled = true;
                issueDrivingLicenseToolStripMenuItem.Enabled = false;
            }

            else if((int)dgvLocalApplicationList.CurrentRow.Cells[5].Value == 3)
            {
                sechduleTestsToolStripMenuItem.DropDownItems["scheduleVisionTestToolStripMenuItem"].Enabled = false;
                sechduleTestsToolStripMenuItem.DropDownItems["scheduleWrittenTestToolStripMenuItem"].Enabled = false;
                sechduleTestsToolStripMenuItem.DropDownItems["scheduleStreetTestToolStripMenuItem"].Enabled = false;
                sechduleTestsToolStripMenuItem.Enabled = false;
                issueDrivingLicenseToolStripMenuItem.Enabled = true;
            }
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAppVisionApointments((int)dgvLocalApplicationList.CurrentRow.Cells[0].Value,
                (int)dgvLocalApplicationList.CurrentRow.Cells[5].Value , _UserID);
            frm.ShowDialog();
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditLDLA(_UserID, -1);
            frm.ShowDialog();
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmWrittenAppointment((int)dgvLocalApplicationList.CurrentRow.Cells[0].Value,
                (int)dgvLocalApplicationList.CurrentRow.Cells[5].Value, _UserID);
            frm.ShowDialog();
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmstreetAppointments((int)dgvLocalApplicationList.CurrentRow.Cells[0].Value,
                (int)dgvLocalApplicationList.CurrentRow.Cells[5].Value, _UserID);
            frm.ShowDialog();
        }
    }
}
