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
    public partial class frmMannageDrivers : Form
    {
        public frmMannageDrivers()
        {
            InitializeComponent();
            cbFilterby.SelectedIndex = 0;
        }
        DataTable DriversList = new DataTable();
        private void _DriversTableColumns()
        {
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "Driver ID";
            DriversList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "Person ID";
            DriversList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "National No";
            DriversList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "Full Name";
            DriversList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "Date";
            DriversList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(bool);
            dtColumn.ColumnName = "Active Licenses";
            DriversList.Columns.Add(dtColumn);
        }

        private void GetDriversInfo()
        {
            _DriversTableColumns();
           
            DataTable DriversTable = new DataTable();
            DriversTable = clsDrivers.GetAllDrivers();
            //  DriversTable to get DriverID and PersonID and CreatedDate

            DataTable PeopleTable = new DataTable();
            PeopleTable = clsPeople.GetAllPeople();
            //  PeopleTable to get National No and FullName

            DataTable LicensesTable = new DataTable();
            LicensesTable = clsLicenses.GetAllLicense();
            //  LicensesTable to get Active Licenses No 


            foreach (DataRow DriversecordRow in DriversTable.Rows)
            {

                DriversList.Rows.Add(DriversecordRow["DriverID"], DriversecordRow["PersonID"], null, null, DriversecordRow["CreatedDate"], 0);
            }


            foreach (DataRow DriversListRecordRow in DriversList.Rows)
            {
                int ActiveLicenses = 0;
                DataRow[] PeopleRestults = PeopleTable.Select("PersonID =" + (int)DriversListRecordRow["Person ID"]);
                foreach (var PeopleRecordRow in PeopleRestults)
                {
                    DriversListRecordRow["National No"] = PeopleRecordRow["NationalNo"];
                    DriversListRecordRow["Full Name"] = PeopleRecordRow["FirstName"].ToString() + " " + PeopleRecordRow["SecondName"] + " " + PeopleRecordRow["ThirdName"] + " " + PeopleRecordRow["LastName"];
                }

                DataRow[] LicensesRestults = LicensesTable.Select("DriverID =" + (int)DriversListRecordRow["Driver ID"] + " AND IsActive = True");
                foreach (var LicensesRecordRow in LicensesRestults)
                {
                    ActiveLicenses++;
                    DriversListRecordRow["Active Licenses"] = ActiveLicenses;

                }
            }
            dgvDriversList.DataSource = DriversList;


        }

        private void frmMannageDrivers_Load(object sender, EventArgs e)
        {
            GetDriversInfo();
            dgvDriversList.Columns[0].Width = 75;
            dgvDriversList.Columns[1].Width = 75;
            dgvDriversList.Columns[2].Width = 75;
            dgvDriversList.Columns[3].Width = 250;
            dgvDriversList.Columns[4].Width = 150;
            dgvDriversList.Columns[5].Width = 75;
            lblRecordes.Text = dgvDriversList.RowCount.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
