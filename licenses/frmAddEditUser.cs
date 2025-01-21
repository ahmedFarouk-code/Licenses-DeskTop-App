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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace licensesApp
{
    public partial class frmAddEditUser : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        DataTable peopleTable = clsPeople.GetAllPeople();
        clsPeople _Person;
        int _PersonID;

        clsUsers _User;
        int _UserID;




        public frmAddEditUser(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            if (_PersonID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ApplyFilter()
        {
            if (peopleTable == null || txtFilterbyval.Text == "") return;

            string selectedColumn = cbFilterBy.SelectedItem.ToString(); // اسم العمود المختار
            string filterValue = txtFilterbyval.Text; // النص المراد الفلترة عليه

            try
            {
                // الحصول على نوع العمود من DataTable
                Type columnType = peopleTable.Columns[selectedColumn].DataType;

                DataRow[] ResultRows;

                if (!string.IsNullOrWhiteSpace(filterValue))
                {
                    // إذا كان نوع العمود نصيًا
                    if (columnType == typeof(string))
                    {
                        ResultRows = peopleTable.Select(selectedColumn+"='"+ filterValue+"'");
                        foreach (DataRow RecordRow in ResultRows)
                        {

                            _Person = clsPeople.Find(Convert.ToInt32(RecordRow["PersonID"]));

                        }
                    }
                    // إذا كان نوع العمود رقميًا
                    else if (columnType == typeof(int) || columnType == typeof(decimal) || columnType == typeof(double))
                    {
                        if (int.TryParse(filterValue, out int intValue))
                        {
                            ResultRows = peopleTable.Select(selectedColumn + "='" + intValue + "'");
                            foreach (DataRow RecordRow in ResultRows)
                            {
                                
                                _Person = clsPeople.Find(Convert.ToInt32(RecordRow["PersonID"]));
                                
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid numerical value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    // التعامل مع أنواع أخرى إذا لزم الأمر
                    else
                    {
                        MessageBox.Show("Column type is not supported for filtering.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
                   if(_Person != null)
                    userControlPersonDetails1.PersonID = _Person.ID;

               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while filtering: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FindPerson()
        {
            ApplyFilter();
            if (_Person != null)
            {
                userControlPersonDetails1.ReloadData();
                if (clsUsers.isExistUser(_Person.ID) && _PersonID == -1)
                {
                    MessageBox.Show("Selected Person already has,choose another one", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _Person = null;
                    return;
                }

                _PersonID = _Person.ID;
                _User = clsUsers.Find(_Person.ID);
            }
            else
            {
                MessageBox.Show("this Person is Not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnFindPerson_Click(object sender, EventArgs e)
        {
            FindPerson();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditPerson(-1);
            frm.ShowDialog();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            
                if (_Person == null)
                {
                    MessageBox.Show("Select a person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TabControl1.SelectedTab = tpPersonInfo;
                }
                else
                {
                    TabControl1.SelectedTab = tpLoginInfo;
                }
           
        }



        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                e.Cancel = true;
                txtUserName.Focus();
                errorProvider1.SetError(txtUserName, "UserName should have a value!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName, "");
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                e.Cancel = true;
                txtPassword.Focus();
                errorProvider1.SetError(txtPassword, "Password should have a value!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtPassword, "");
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text != txtPassword.Text)
            {

                txtConfirmPassword.Focus();
                errorProvider1.SetError(txtConfirmPassword, "Password should have a value!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtConfirmPassword, "");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if(_Person != null )
            {
                if (_Mode == enMode.AddNew)
                {
                    _User = new clsUsers();

                    
                }
                _User.PersonID = _Person.ID;
                _User.UserName = txtUserName.Text;
                _User.Password = txtPassword.Text;
                if(cbIsActive.Checked)
                {
                    _User.IsActive = true;
                }
                else
                {
                    _User.IsActive = true;
                }
               
                if (_User.Save())
                MessageBox.Show("Data Saved Successfully.");
                  else
                  MessageBox.Show("Error: Data Is not Saved Successfully.");


                _Mode = enMode.Update;

            }
            else
            {
                MessageBox.Show("Data Saved Faild !!! (Select a Person)." ,"Warrning" ,MessageBoxButtons.OK);
            }

        }

        private void _Load()
        {
            if (_PersonID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;


            if (_Mode == enMode.AddNew)
            {
                _User = new clsUsers();

                return;
            }
           

           

            txtTitleName.Text = "Update User Info";
                cbFilterBy.SelectedIndex = 0;
                txtFilterbyval.Text = _PersonID.ToString();
                FindPerson();
                gbFilter.Enabled = false;

              
                if (_User == null)
                {
                    //MessageBox.Show("This form will be closed because No Person with ID = " + PersonID);
                    return;
                }
                lblUserID.Text = _User.UserID.ToString();
                txtUserName.Text = _User.UserName;
                txtPassword.Text = _User.Password;
                txtConfirmPassword.Text = _User.Password;
                if (_User.IsActive == true)
                {
                    cbIsActive.Checked = true;
                }
            
            _Mode = enMode.Update;
        }

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _Load();
        }




    }
}
