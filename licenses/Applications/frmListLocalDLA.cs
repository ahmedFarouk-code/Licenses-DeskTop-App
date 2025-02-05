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
            LDLAList.Columns.Clear();
            LDLAList.Rows.Clear();
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

        private void FilterDataGridView()
        {
            if (LDLAList.Rows.Count == 0) return; // التأكد من وجود بيانات

            string columnName = cbFilterby.SelectedItem.ToString(); // اسم العمود المحدد
            string filterValue = txtFilterByValue.Text.Trim(); // قيمة البحث

            if (string.IsNullOrEmpty(filterValue))
            {
                LDLAList.DefaultView.RowFilter = ""; // إزالة الفلترة إذا كان الحقل فارغًا
            }
            else
            {
                // تطبيق الفلترة بناءً على نوع البيانات
                if (LDLAList.Columns[columnName].DataType == typeof(int) || LDLAList.Columns[columnName].DataType == typeof(byte))
                {
                    // البحث الرقمي
                    LDLAList.DefaultView.RowFilter = $"[{columnName}] = {filterValue}";
                }
                else if (LDLAList.Columns[columnName].DataType == typeof(string))
                {
                    // البحث النصي (باستخدام LIKE)
                    LDLAList.DefaultView.RowFilter = $"[{columnName}] LIKE '%{filterValue}%'";
                }
                else if (LDLAList.Columns[columnName].DataType == typeof(DateTime))
                {
                    // البحث بالتواريخ
                    DateTime dt;
                    if (DateTime.TryParse(filterValue, out dt))
                    {
                        LDLAList.DefaultView.RowFilter = $"[{columnName}] = #{dt:MM/dd/yyyy}#";
                    }
                    else
                    {
                        MessageBox.Show("Invalid date format. Please enter a valid date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
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
            GetLDLAInfo();
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
                    GetLDLAInfo();
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
                    GetLDLAInfo();
                }
                else
                {
                    MessageBox.Show("Error: Cancel App not Saved Successfully.");
                }
              


            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if ((string)dgvLocalApplicationList.CurrentRow.Cells[6].Value == "Canceld")
            {
                editApplicationToolStripMenuItem.Enabled = false;
                deleteApplicationToolStripMenuItem.Enabled = true;
                cancelApplicationToolStripMenuItem.Enabled = false;
                issueDrivingLicenseToolStripMenuItem.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = false;
                sechduleTestsToolStripMenuItem.Enabled = false;
                return;
            }

                if ((int)dgvLocalApplicationList.CurrentRow.Cells[5].Value == 0)
            {
                editApplicationToolStripMenuItem.Enabled = true;
                deleteApplicationToolStripMenuItem.Enabled = true;
                cancelApplicationToolStripMenuItem.Enabled = true;
                issueDrivingLicenseToolStripMenuItem.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = false;
                sechduleTestsToolStripMenuItem.DropDownItems["scheduleVisionTestToolStripMenuItem"].Enabled = true;
                sechduleTestsToolStripMenuItem.DropDownItems["scheduleWrittenTestToolStripMenuItem"].Enabled = false;
                sechduleTestsToolStripMenuItem.DropDownItems["scheduleStreetTestToolStripMenuItem"].Enabled = false;
                sechduleTestsToolStripMenuItem.Enabled = true;
               
            }

            else if ((int)dgvLocalApplicationList.CurrentRow.Cells[5].Value == 1)
            {
                editApplicationToolStripMenuItem.Enabled = true;
                deleteApplicationToolStripMenuItem.Enabled = true;
                cancelApplicationToolStripMenuItem.Enabled = true;
                issueDrivingLicenseToolStripMenuItem.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = false;
                sechduleTestsToolStripMenuItem.DropDownItems["scheduleVisionTestToolStripMenuItem"].Enabled = false;
                sechduleTestsToolStripMenuItem.DropDownItems["scheduleWrittenTestToolStripMenuItem"].Enabled = true;
                sechduleTestsToolStripMenuItem.DropDownItems["scheduleStreetTestToolStripMenuItem"].Enabled = false;
                sechduleTestsToolStripMenuItem.Enabled = true;
                
            }

            else if ((int)dgvLocalApplicationList.CurrentRow.Cells[5].Value == 2)
            {
                editApplicationToolStripMenuItem.Enabled = true;
                deleteApplicationToolStripMenuItem.Enabled = true;
                cancelApplicationToolStripMenuItem.Enabled = true;
                issueDrivingLicenseToolStripMenuItem.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = false;
                sechduleTestsToolStripMenuItem.DropDownItems["scheduleVisionTestToolStripMenuItem"].Enabled = false;
                sechduleTestsToolStripMenuItem.DropDownItems["scheduleWrittenTestToolStripMenuItem"].Enabled = false;
                sechduleTestsToolStripMenuItem.DropDownItems["scheduleStreetTestToolStripMenuItem"].Enabled = true;
                sechduleTestsToolStripMenuItem.Enabled = true;
                
            }

            else if((int)dgvLocalApplicationList.CurrentRow.Cells[5].Value == 3)
            {
               
                

                clsLDLA LDLA1 = clsLDLA.Find((int)dgvLocalApplicationList.CurrentRow.Cells[0].Value);
                clsApplications Application1 = clsApplications.Find(LDLA1.ApplicationID);
                
                if (Application1.ApplicationStatus == 3)
                {
                    editApplicationToolStripMenuItem.Enabled = false;
                    deleteApplicationToolStripMenuItem.Enabled = false;
                    cancelApplicationToolStripMenuItem.Enabled = false;
                    sechduleTestsToolStripMenuItem.Enabled = false;
                    issueDrivingLicenseToolStripMenuItem.Enabled = false;
                    showLicenseToolStripMenuItem.Enabled = true;
                }
                else
                {
                    editApplicationToolStripMenuItem.Enabled = true;
                    deleteApplicationToolStripMenuItem.Enabled = true;
                    cancelApplicationToolStripMenuItem.Enabled = true;
                    sechduleTestsToolStripMenuItem.Enabled = true;
                    issueDrivingLicenseToolStripMenuItem.Enabled = true;
                    showLicenseToolStripMenuItem.Enabled = false;
                }
            }



           
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAppVisionApointments((int)dgvLocalApplicationList.CurrentRow.Cells[0].Value,
                (int)dgvLocalApplicationList.CurrentRow.Cells[5].Value , _UserID);
            frm.ShowDialog();
            GetLDLAInfo();
        }

        private void btnAddNewApplication_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditLDLA(_UserID, -1);
            frm.ShowDialog();
            GetLDLAInfo();
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmWrittenAppointment((int)dgvLocalApplicationList.CurrentRow.Cells[0].Value,
                (int)dgvLocalApplicationList.CurrentRow.Cells[5].Value, _UserID);
            frm.ShowDialog();
            GetLDLAInfo();
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmstreetAppointments((int)dgvLocalApplicationList.CurrentRow.Cells[0].Value,
                (int)dgvLocalApplicationList.CurrentRow.Cells[5].Value, _UserID);
            frm.ShowDialog();
            GetLDLAInfo();
        }

        private void issueDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmIssueDriverLicenseForTheFirstTime((int)dgvLocalApplicationList.CurrentRow.Cells[0].Value,
                (int)dgvLocalApplicationList.CurrentRow.Cells[5].Value, _UserID);
            frm.ShowDialog();
            GetLDLAInfo();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLDLA Ldla = clsLDLA.Find((int)dgvLocalApplicationList.CurrentRow.Cells[0].Value);
            clsApplications App = clsApplications.Find(Ldla.ApplicationID);
         
            Form frm = new frmLicenseHistory(App.ApplicantPersonID);

            frm.ShowDialog();
            GetLDLAInfo();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmDriverLIcenseInfo((int)dgvLocalApplicationList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            GetLDLAInfo();
        }

        private void txtFilterByValue_TextChanged(object sender, EventArgs e)
        {
            FilterDataGridView();
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonId = (clsPeople.FindByNationalNo((string)dgvLocalApplicationList.CurrentRow.Cells[2].Value).ID);
            Form frm = new frmPersonDetails(PersonId);
            frm.ShowDialog();
            GetLDLAInfo();
        }
    }
}
