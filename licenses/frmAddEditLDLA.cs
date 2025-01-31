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
using static System.Net.Mime.MediaTypeNames;

namespace licensesApp
{
    public partial class frmAddEditLDLA : Form
    {
        public enum enMode { AddNewL = 0, Update = 1 };
        private enMode _Mode = enMode.AddNewL;

       
        int _UserID;

        int _ApplicationID;
        clsApplications _Application;
        bool _Enabled = false;

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
                UserControl2PersonDetailsWithFilter.PersonID = -1;
            }
            
            else
            {
                _Mode = enMode.Update;
                _LDLA = clsLDLA.Find(_LDLAid);
                _ApplicationID = Convert.ToInt32(_LDLA.ApplicationID);
                _Application = clsApplications.Find(_ApplicationID);
                UserControl2PersonDetailsWithFilter.PersonID = _Application.ApplicantPersonID;
            }


           
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
           
            else
                _Mode = enMode.Update;

            _FillCountry();
            if (_Mode == enMode.AddNewL)
            {
                _LDLA = new clsLDLA();
                _Application = new clsApplications();

                lblAPPDate.Text = DateTime.Now.ToString();
                lblCreatedBy.Text =  (clsUsers.FindByUserID(_UserID) .UserName);
                lblAppFees.Text = 15.ToString();
                cbLicenseClass.SelectedIndex = 0;

                return;
            }

           //_LDLA = clsLDLA.Find(_LDLAid);
           // _ApplicationID = Convert.ToInt32 (_LDLA.ApplicationID);
           // _Application = clsApplications.Find(_ApplicationID);
           // UserControl2PersonDetailsWithFilter.PersonID = _Application.ApplicantPersonID;
           

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
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(clsLicenseClasses.Find(_LDLA.LicenseClassID).LicenseClasseName);

            _Mode = enMode.Update;

        }


        private void frmAddEditLDLA_Load(object sender, EventArgs e)
        {
            _Load();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNewL)
            {
                _LDLA = new clsLDLA();
                _Application = new clsApplications();

                int LDLALicenseClassID = clsLicenseClasses.Find(cbLicenseClass.Text).ID;
             
                if (clsApplications.IsApplicationExist(UserControl2PersonDetailsWithFilter.PersonID) && clsLDLA.IsLDLAExist(LDLALicenseClassID) )
                {
                    MessageBox.Show("Choose another License Class , the sellected person already have an active application for the selected class with id = " + LDLALicenseClassID);

                    return;
                }
                
                        
            }

           
            _Application.ApplicantPersonID = UserControl2PersonDetailsWithFilter.PersonID;
            _Application.ApplicationDate = Convert.ToDateTime (lblAPPDate.Text);
            _Application.ApplicationTypeID = 1;

            if (_Mode == enMode.AddNewL)
            {
                _Application.ApplicationStatus = 1;
            }
            else if (_Mode == enMode.Update)
            {
                _Application.ApplicationStatus = 2;
            }
            _Application.LastStatusDate = DateTime.Now;
            _Application.PaidFees = cbLicenseClass.SelectedIndex;
            _Application.CreatedByUserID = Convert.ToInt32 (lblCreatedBy.Text);

            


            if(_Application.Save())
            {
                MessageBox.Show("Data Saved Successfully*.");
            _LDLA.ApplicationID = _Application.ApplicationID;
            _LDLA.LicenseClassID = clsLicenseClasses.Find(cbLicenseClass.Text).ID;



                if (_LDLA.Save())
                    MessageBox.Show("Data Saved Successfully+.");
                
            }
            else
            {
                 MessageBox.Show("Error: Data Is not Saved Successfully+.");
            }

            _Mode = enMode.Update;
        }

        
    
    }
}
