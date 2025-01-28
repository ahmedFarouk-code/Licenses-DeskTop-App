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
    public partial class frmAddEditLDLA : Form
    {
        public enum enMode { AddNewL = 0, Update = 1 ,Cancel = 2  };
        private enMode _Mode = enMode.AddNewL;

       
        int _UserID;

        int _ApplicationID;
        clsApplications _Application;

        int _LDLAid;
        clsLDLA _LDLA;
        



        public frmAddEditLDLA(int UserID , int LDLAid)
        {
            InitializeComponent();
            
            _UserID = UserID;
            _LDLAid = LDLAid;
            if (_LDLAid == -1)
            {
                _Mode = enMode.AddNewL;
               
            }
            else if(_LDLAid == -2)
            {
                _Mode = enMode.Cancel;
                
            }
            else
            {
                _Mode = enMode.Update;
               
            }


            //UserControl2PersonDetailsWithFilter.PersonID = _PersonID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void _FillCountry()
        {
            DataTable dt = clsLicenseClasses.GetAllLicenseClasses();
            foreach (DataRow row in dt.Rows)
            {
                cbLicenseClass.Items.Add(row["ClassName"]);
            }
        }

        private void _Load()
        {
            if (_LDLAid == -1)
                _Mode = enMode.AddNewL;
            else if (_LDLAid == -2)
                _Mode = enMode.Cancel;
            else
                _Mode = enMode.Update;

            _FillCountry();
            if (_Mode == enMode.AddNewL)
            {
                _LDLA = new clsLDLA();
                _Application = new clsApplications();

                lblAPPDate.Text = DateTime.Now.ToString();
                lblCreatedBy.Text = _UserID.ToString();
                lblAppFees.Text = 15.ToString();
                cbLicenseClass.SelectedIndex = 0;

                return;
            }

           _LDLA = clsLDLA.Find(_LDLAid);
            _ApplicationID = Convert.ToInt32 (_LDLA.ApplicationID);
            _Application = clsApplications.Find(_ApplicationID);
            UserControl2PersonDetailsWithFilter.PersonID = _Application.ApplicantPersonID;
           

            lblTitle.Text = "UpdateApplication Info";





            if (_LDLA == null || _Application == null)
            {
                MessageBox.Show("This form will be closed because No Application with this ID");
                return;
            }
            lblDLApplicationID.Text = _LDLA.LocalDrivingLicenseApplicationID.ToString();
            lblAPPDate.Text = _Application.ApplicationDate.ToString();
            lblCreatedBy.Text = _Application.CreatedByUserID.ToString();
            lblAppFees.Text = _Application.PaidFees.ToString();
            //cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(clsLicenseClasses.Find(_LDLA.LicenseClassID).LicenseClasseName);

            _Mode = enMode.Update;

        }


        private void frmAddEditLDLA_Load(object sender, EventArgs e)
        {
            _Load();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
        }
    }
}
