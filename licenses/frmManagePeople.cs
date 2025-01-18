using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LicensesBusinessLayer;
namespace licensesApp
{
    public partial class frmManagePeople : Form
    {
        DataTable peopleTable = clsPeople.GetAllPeople();
        public frmManagePeople()
        {
            InitializeComponent();
        }


        private void ApplyFilter()
        {
            if (peopleTable == null || cbFilterBy.SelectedIndex == 0) return;

            string selectedColumn = cbFilterBy.SelectedItem.ToString(); // اسم العمود المختار
            string filterValue = txtFilterbyval.Text; // النص المراد الفلترة عليه

            try
            {
                // الحصول على نوع العمود من DataTable
                Type columnType = peopleTable.Columns[selectedColumn].DataType;

                // إنشاء DataView لتطبيق الفلترة
                DataView dv = peopleTable.DefaultView;

                if (!string.IsNullOrWhiteSpace(filterValue))
                {
                    // إذا كان نوع العمود نصيًا
                    if (columnType == typeof(string))
                    {
                        dv.RowFilter = $"{selectedColumn} LIKE '%{filterValue}%'";
                    }
                    // إذا كان نوع العمود رقميًا
                    else if (columnType == typeof(int) || columnType == typeof(decimal) || columnType == typeof(double))
                    {
                        if (int.TryParse(filterValue, out int intValue))
                        {
                            dv.RowFilter = $"{selectedColumn} = {intValue}";
                        }
                        else
                        {
                            MessageBox.Show("الرجاء إدخال قيمة رقمية صحيحة.", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    // التعامل مع أنواع أخرى إذا لزم الأمر
                    else
                    {
                        MessageBox.Show("نوع العمود غير مدعوم للفلترة.", "تحذير", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    dv.RowFilter = ""; // إظهار جميع البيانات إذا كان النص فارغاً
                }

                dgvPeoplelist.DataSource = dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ أثناء الفلترة: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _RefreshPeopleList()
        {
                dgvPeoplelist.DataSource = peopleTable;
            cbFilterBy.SelectedIndex = 0;
        }

       
        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _RefreshPeopleList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditPerson((int)dgvPeoplelist.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void _GetRecords()
        {
            lblRecords.Text = dgvPeoplelist.Rows.Count.ToString();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditPerson(-1);
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditPerson(-1);
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if(clsPeople.DeletePerson((int)dgvPeoplelist.CurrentRow.Cells[0].Value));
            MessageBox.Show("Data Saved Successfully.");
            _RefreshPeopleList();
        }

        private void txtFilterbyval_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void personDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmPersonDetails((int)dgvPeoplelist.CurrentRow.Cells[0].Value);
             frm.ShowDialog();
            _RefreshPeopleList();
        }
    }
}
