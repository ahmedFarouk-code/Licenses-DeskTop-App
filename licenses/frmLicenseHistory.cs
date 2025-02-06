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
        private int  _DriverID;
        public frmLicenseHistory(int PersonID , int DriverID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            _DriverID = DriverID;

            UserControl2PersonDetailsWithFilter.PersonID = _PersonID;

        }


        DataTable LocalLicensesList = new DataTable();
        DataTable InterNationalLicensesList = new DataTable();
        private void _LDLTableColumns()
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
            dtColumn.ColumnName = "Expiration Date";
            LocalLicensesList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(bool);
            dtColumn.ColumnName = "Is Active";
            LocalLicensesList.Columns.Add(dtColumn);
        }

        private void _IDLTableColumns()
        {
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "Int.License.ID";
            InterNationalLicensesList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "Application ID";
            InterNationalLicensesList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "License ID";
            InterNationalLicensesList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "Issue Date";
            InterNationalLicensesList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "Expiration Date";
            InterNationalLicensesList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(bool);
            dtColumn.ColumnName = "Is Active";
            InterNationalLicensesList.Columns.Add(dtColumn);
        }


        private void GetLDLA_info_ForPerson()
        {
            _LDLTableColumns();
            DataTable ApplicationsTabl = new DataTable();
            ApplicationsTabl = clsApplications.GetAllApplications();

            DataTable LDLAtable = new DataTable();
            LDLAtable = clsLDLA.GetALL_LDLA();

            DataTable LicensesTable = new DataTable();
            LicensesTable = clsLicenses.GetAllLicense();

            DataTable LicenseClassesTable = new DataTable();
            LicenseClassesTable = clsLicenseClasses.GetAllLicenseClasses();


            DataRow[] LicensesRestults = LicensesTable.Select("DriverID =" + _DriverID + "AND IsActive= true");
            foreach (var LicensesRecordRow in LicensesRestults)
            {

                LocalLicensesList.Rows.Add(LicensesRecordRow["LicenseID"], LicensesRecordRow["ApplicationID"], null, LicensesRecordRow["IssueDate"], null, LicensesRecordRow["IsActive"]);
            }
            foreach (DataRow RecordRow in LocalLicensesList.Rows)
            {
                 
                foreach (var LicensesRecordRow in LicensesRestults)
                {

                    DataRow[] LicenseClassesRestults = LicenseClassesTable.Select("LicenseClassID =" + (int)LicensesRecordRow["LicenseClass"]);
                    foreach (var LicenseClassesRecordRow in LicenseClassesRestults)
                    {

                        DateTime TheDate = (DateTime)LicensesRecordRow["IssueDate"];
                        DateTime EpirationDate = TheDate.AddDays((byte)LicenseClassesRecordRow["DefaultValidityLength"]);
                        RecordRow["Class Name"] = LicenseClassesRecordRow["ClassName"];
                        RecordRow["Expiration Date"] = EpirationDate;

                    }
                }
               


            }

               
                  
                
                dgvLocalLicense.DataSource = LocalLicensesList;


        }


        private void GetIDLA_info_ForPerson()
        {
            _IDLTableColumns();
            DataTable ApplicationsTabl = new DataTable();
            ApplicationsTabl = clsApplications.GetAllApplications();

            DataTable LicensesTable = new DataTable();
            LicensesTable = clsLicenses.GetAllLicense();

            DataTable InternationalLicensesTable = new DataTable();
            InternationalLicensesTable = clsInternationalLicenses.GetAllInternationalLicenses();

            DataTable LicenseClassesTable = new DataTable();
            LicenseClassesTable = clsLicenseClasses.GetAllLicenseClasses();


            DataRow[] InternationalLicensesRestults = InternationalLicensesTable.Select("DriverID =" + _DriverID + "AND IsActive= true");
            foreach (var InternationalLicensesRecordRow in InternationalLicensesRestults)
            {

                InterNationalLicensesList.Rows.Add(InternationalLicensesRecordRow["InternationalLicenseID"], InternationalLicensesRecordRow["ApplicationID"], null, InternationalLicensesRecordRow["IssueDate"], InternationalLicensesRecordRow["ExpirationDate"], InternationalLicensesRecordRow["IsActive"]);
            }
            foreach (DataRow RecordRow in InterNationalLicensesList.Rows)
            {
                DataRow[] LicensesRestults = LicensesTable.Select("DriverID =" + _DriverID + "AND IsActive= true");
                foreach (var LicensesRecordRow in LicensesRestults)
                {
                    RecordRow["License ID"] = LicensesRecordRow["LicenseID"];

                }

            }

            dgvIDLforPeson.DataSource = InterNationalLicensesList;


        }

        private void _Load()
            {

            }

        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            GetLDLA_info_ForPerson();
            GetIDLA_info_ForPerson();
            //dgvLocalLicense.Columns[0].Width = 75;
            //dgvLocalLicense.Columns[1].Width = 75;
            //dgvLocalLicense.Columns[2].Width = 200;
            //dgvLocalLicense.Columns[3].Width = 200;
            //dgvLocalLicense.Columns[4].Width = 200;
            //dgvLocalLicense.Columns[5].Width = 75;
        }
    }
}
