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
    public partial class frmManageUsers : Form
    {
        DataTable UsersTable = clsUsers.GetAllUsers();
        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void _RefreshYsersList()
        {
            DataTable UsersTable = clsUsers.GetAllUsers();
            dgvUsersList.DataSource = UsersTable;
            cbFilterBy.SelectedIndex = 0;
            txtFilterbyval.Visible = true;
          

        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _RefreshYsersList();
        }

        private void ApplyFilter()
        {
            if (UsersTable == null || cbFilterBy.SelectedIndex == 0) return;
            
            string selectedColumn = cbFilterBy.SelectedItem.ToString(); // اسم العمود المختار
            string filterValue = txtFilterbyval.Text; // النص المراد الفلترة عليه

            try
            {
               
                    // الحصول على نوع العمود من DataTable
                Type columnType = UsersTable.Columns[selectedColumn].DataType;

                // إنشاء DataView لتطبيق الفلترة
                DataView dv = UsersTable.DefaultView;

                
                    if (!string.IsNullOrWhiteSpace(filterValue))
                    {
                        // إذا كان نوع العمود نصيًا
                        if (columnType == typeof(string))
                        {
                            dv.RowFilter = $"{selectedColumn} LIKE '{filterValue}%'";
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
                                MessageBox.Show("Please enter a valid numerical value.", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }

                        // التعامل مع أنواع أخرى إذا لزم الأمر
                        else
                        {
                            MessageBox.Show("Column type is not supported for filtering.", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    else
                    {
                        dv.RowFilter = ""; // إظهار جميع البيانات إذا كان النص فارغاً
                    }
                    
                
                
                dgvUsersList.DataSource = dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ أثناء الفلترة: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFilterbyval_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditUser(-1);
            frm.ShowDialog();
            _RefreshYsersList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            Form frm = new frmAddEditUser((int)dgvUsersList.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
            _RefreshYsersList();
        }

        
    }
}
