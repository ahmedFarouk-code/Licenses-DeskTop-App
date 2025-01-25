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
        public frmListLocalDLA()
        {
            InitializeComponent();
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

        private  void GetLDLAInfo()
        {
           DataTable LDLA = new DataTable();
            LDLA = clsLDLA.GetALL_LDLA();
            _LDLA_TableColumns();

            DataTable LicenseClassesTable = new DataTable();
            LicenseClassesTable = clsLicenseClasses.GetAllLicenseClasses();

            foreach (DataRow RecordRow in LDLA.Rows)
            {

                
                
                DataRow[] LicenseClassesRestults = LicenseClassesTable.Select("LicenseClassID =" +  RecordRow["LicenseClassID"]);
                foreach (var LicenseClassesRecordRow in LicenseClassesRestults)
                {

                    LDLAList.Rows.Add(RecordRow["ApplicationID"], LicenseClassesRecordRow["ClassName"], null, null, null, null, null);
                }

            }
        }

        private void GetApplicationInfo()
        {
            GetLDLAInfo();
            DataTable ApplicationsTable = new DataTable();
            ApplicationsTable = clsApplications.GetAllApplications();
            DataTable PersonsTable = new DataTable();
            PersonsTable = clsPeople.GetAllPeople();

           

            foreach (DataRow RecordRow in LDLAList.Rows)
            {
                                                                                       //selectedColumn + "='" + filterValue + "'"

                DataRow[] ApplicationsRestults = ApplicationsTable.Select("ApplicationID =" + (int)RecordRow["L.D.AppID"] );
                foreach (var ApplicationsRecordRow in ApplicationsRestults)
                {

                    RecordRow["Application Date"] = ApplicationsRecordRow["ApplicationDate"];
                    DataRow[] PersonsTableRestults = PersonsTable.Select("PersonID =" + ApplicationsRecordRow["ApplicantPersonID"]);
                   
                    foreach (var PersonsTableRecordRow in PersonsTableRestults)
                    {
                        RecordRow["NationalNo"] = PersonsTableRecordRow["NationalNo"];
                        RecordRow["Full Name"] = PersonsTableRecordRow["FirstName"].ToString() + " "+ PersonsTableRecordRow["SecondName"] + " " + PersonsTableRecordRow["ThirdName"] + " " + PersonsTableRecordRow["LastName"];
                    }
                }


            }
        }


        private void frmListLocalDLA_Load(object sender, EventArgs e)
        {
            GetApplicationInfo();
            dgvLocalApplicationList.DataSource = LDLAList;
            dgvLocalApplicationList.Columns[0].Width = 75;
            dgvLocalApplicationList.Columns[1].Width = 200;
            dgvLocalApplicationList.Columns[2].Width = 75;
            dgvLocalApplicationList.Columns[3].Width = 250;
            dgvLocalApplicationList.Columns[4].Width = 150;
        }


        
    }
}
