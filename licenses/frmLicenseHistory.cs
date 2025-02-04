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
    public partial class frmLicenseHistory : Form
    {

        private int _PersonID;
        public frmLicenseHistory(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            UserControl2PersonDetailsWithFilter.PersonID = _PersonID;

        }


        DataTable LocalLicensesList = new DataTable();
        private void _DriversTableColumns()
        {
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "Lic.ID";
            LocalLicensesList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "App.ID";
            LocalLicensesList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "Class Name";
            LocalLicensesList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "Issue Date";
            LocalLicensesList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "Epiration Date";
            LocalLicensesList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(bool);
            dtColumn.ColumnName = "Is Active";
            LocalLicensesList.Columns.Add(dtColumn);
        }


        private void GetAllLDLA_info_ForPerson()
        {
            _DriversTableColumns();
            DataTable ApplicationsTabl = new DataTable();
            ApplicationsTabl = clsApplications.GetAllApplications();

            DataTable LDLAtable = new DataTable();
            LDLAtable = clsLDLA.GetALL_LDLA();

            DataTable LicensesTable = new DataTable();
            LicensesTable = clsLicenses.GetAllLicense();

            DataTable LicenseClassesTable = new DataTable();
            LicenseClassesTable = clsLicenseClasses.GetAllLicenseClasses();


            DataRow[] ApplicationsTablRestults = ApplicationsTabl.Select("ApplicantPersonID =" + _PersonID);
            foreach (var ApplicationsRecordRow in ApplicationsTablRestults)
            {

                LocalLicensesList.Rows.Add(null, ApplicationsRecordRow["ApplicationID"], null, null, null, null);
            }
            foreach (DataRow RecordRow in LocalLicensesList.Rows)
            {
                DataRow[] LDLARRestults = LDLAtable.Select("ApplicationID =" + (int)RecordRow["App.ID"]);
                foreach (var LDLARRecordRow in LDLARRestults)
                {
                    RecordRow["Lic.ID"] = LDLARRecordRow["LocalDrivingLicenseApplicationID"];
                   
                }


                DataRow[] LicensesRestults = LicensesTable.Select("ApplicationID =" + (int)RecordRow["App.ID"]);
                foreach (var LicensesRecordRow in LicensesRestults)
                {
                    RecordRow["Is Active"] = LicensesRecordRow["IsActive"];

                    DataRow[] LicenseClassesRestults = LicenseClassesTable.Select("LicenseClassID =" + (int)LicensesRecordRow["LicenseClass"]);
                    foreach (var LicenseClassesRecordRow in LicenseClassesRestults)
                    {

                        DateTime TheDate = (DateTime)LicensesRecordRow["IssueDate"];
                        DateTime EpirationDate = TheDate.AddDays((byte)LicenseClassesRecordRow["DefaultValidityLength"]);
                        RecordRow["Class Name"] = LicenseClassesRecordRow["ClassName"];
                        RecordRow["Issue Date"] = TheDate;
                        RecordRow["Epiration Date"] = EpirationDate;
                      
                    }


                }


                dgvLocalLicense.DataSource = LocalLicensesList  ; 

            }
        }

            private void _Load()
            {

            }

        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            GetAllLDLA_info_ForPerson();
            dgvLocalLicense.Columns[0].Width = 75;
            dgvLocalLicense.Columns[1].Width = 75;
            dgvLocalLicense.Columns[2].Width = 200;
            dgvLocalLicense.Columns[3].Width = 200;
            dgvLocalLicense.Columns[4].Width = 200;
            dgvLocalLicense.Columns[5].Width = 75;
        }
    }
}
