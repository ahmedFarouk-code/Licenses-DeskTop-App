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
    public partial class frmIntarnationlLicenseApplications : Form
    {
        private int _UserID;
        public frmIntarnationlLicenseApplications(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            cbFilterby.SelectedIndex = 0;
        }


        DataTable IDLAList = new DataTable();
        private void _IDLA_TableColumns()
        {
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "int.License.ID";
            IDLAList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "Application.ID";
            IDLAList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "Driver.ID";
            IDLAList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(int);
            dtColumn.ColumnName = "L.License.ID";
            IDLAList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "Issue.Date";
            IDLAList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(DateTime);
            dtColumn.ColumnName = "Expiration.Date";
            IDLAList.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(bool);
            dtColumn.ColumnName = "Is Active";
            IDLAList.Columns.Add(dtColumn);
        }

        private void GetLDLAInfo()
        {
            IDLAList.Columns.Clear();
            IDLAList.Rows.Clear();
            _IDLA_TableColumns();

            DataTable IDLA = new DataTable();
            IDLA = clsInternationalLicenses.GetAllInternationalLicenses();

            foreach (DataRow IDLARecordRow in IDLA.Rows)
            {

                IDLAList.Rows.Add(IDLARecordRow["InternationalLicenseID"], IDLARecordRow["ApplicationID"], IDLARecordRow["DriverID"], IDLARecordRow["IssuedUsingLocalLicenseID"], IDLARecordRow["IssueDate"], IDLARecordRow["ExpirationDate"], IDLARecordRow["IsActive"]);
            }
            dgvintarbationalLicensApps.DataSource = IDLAList;
            lblRecords.Text = dgvintarbationalLicensApps.RowCount.ToString();
        }


        private void FilterDataGridView()
        {
            if (IDLAList.Rows.Count == 0) return; // التأكد من وجود بيانات

            string columnName = cbFilterby.SelectedItem.ToString(); // اسم العمود المحدد
            string filterValue = txtFilterByValue.Text.Trim(); // قيمة البحث

            if (string.IsNullOrEmpty(filterValue))
            {
                IDLAList.DefaultView.RowFilter = ""; // إزالة الفلترة إذا كان الحقل فارغًا
            }
            else
            {
                // تطبيق الفلترة بناءً على نوع البيانات
                if (IDLAList.Columns[columnName].DataType == typeof(int) || IDLAList.Columns[columnName].DataType == typeof(byte))
                {
                    // البحث الرقمي
                    IDLAList.DefaultView.RowFilter = $"[{columnName}] = {filterValue}";
                }
                else if (IDLAList.Columns[columnName].DataType == typeof(string))
                {
                    // البحث النصي (باستخدام LIKE)
                    IDLAList.DefaultView.RowFilter = $"[{columnName}] LIKE '%{filterValue}%'";
                }
                else if (IDLAList.Columns[columnName].DataType == typeof(DateTime))
                {
                    // البحث بالتواريخ
                    DateTime dt;
                    if (DateTime.TryParse(filterValue, out dt))
                    {
                        IDLAList.DefaultView.RowFilter = $"[{columnName}] = #{dt:MM/dd/yyyy}#";
                    }
                    else
                    {
                        MessageBox.Show("Invalid date format. Please enter a valid date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void frmIntarnationlLicenseApplications_Load(object sender, EventArgs e)
        {
            GetLDLAInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
          int  PersonID = clsDrivers.FindDriverID((int)dgvintarbationalLicensApps.CurrentRow.Cells[2].Value).PersonID;

            Form frm = new frmLicenseHistory(PersonID, (int)dgvintarbationalLicensApps.CurrentRow.Cells[2].Value);
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmInterNationalDriverLicenseID((int)dgvintarbationalLicensApps.CurrentRow.Cells[3].Value);
            frm.ShowDialog();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
