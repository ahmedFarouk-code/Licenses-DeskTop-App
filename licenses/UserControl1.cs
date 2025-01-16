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
    public partial class UserControl1 : UserControl
    {
      
         public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        
        clsPeople _Person;
        public int PersonID { get; set; }
       

        public UserControl1()
        {
            InitializeComponent();
           
        }

        private  void _FillCountry()
        {
            DataTable dt = clsCountry.GetAllCountries();
            foreach (DataRow row in dt.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
        }

        private void _LoadData()
        {
            _FillCountry();
            txtAddress.Text = PersonID.ToString();
        }

     

      
        private void UserControl1_Load(object sender, EventArgs e)
        {
            _LoadData();
        }
    }
}
