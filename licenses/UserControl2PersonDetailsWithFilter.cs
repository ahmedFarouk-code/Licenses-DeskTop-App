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
    public partial class UserControl2PersonDetailsWithFilter : UserControl
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        DataTable peopleTable = clsPeople.GetAllPeople();
        clsPeople _Person;
        public static int PersonID { get; set; }

        public  UserControl2PersonDetailsWithFilter()
        {
            InitializeComponent();
            if (PersonID == -1)
                _Mode = enMode.AddNew;

            else
            {
                _Mode = enMode.Update;
                gbFilter.Enabled = false;
            }
        }

        private void ApplyFilter()
        {
            if (peopleTable == null ) return;
            
            if (_Mode == enMode.Update)
            {
                cbFilterBy.SelectedIndex = 0;
                txtFilterbyval.Text = PersonID.ToString();
            }
            
           
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
                        ResultRows = peopleTable.Select(selectedColumn + "='" + filterValue + "'");
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
                if (_Person != null)

                    userControlPersonDetails1.PersonID = _Person.ID;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while filtering: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
       


        public  void FindPerson()
        {
           

            ApplyFilter();
            if (_Person != null)
            {
                
                userControlPersonDetails1.ReloadData();

                PersonID = _Person.ID;
            }
           
        }


        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditPerson(-1);
            frm.ShowDialog();
        }

        private void btnFindPerson_Click(object sender, EventArgs e)
        {
            FindPerson();
        }

        private void UserControl2PersonDetailsWithFilter_Load(object sender, EventArgs e)
        {
           if (PersonID == -1)
           {
                _Mode = enMode.AddNew;
                gbFilter.Enabled = true;
           }
            else
            {
                _Mode = enMode.Update;
            gbFilter.Enabled = false;
                FindPerson();
            }

           

        }
    }
}
