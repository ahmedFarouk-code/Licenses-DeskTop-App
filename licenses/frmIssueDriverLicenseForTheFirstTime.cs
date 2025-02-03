using System;
using System.CodeDom;
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
    public partial class frmIssueDriverLicenseForTheFirstTime : Form
    {
        private int _LDLid;
        private int _PassedTests;
        private int _UserID;
        public frmIssueDriverLicenseForTheFirstTime(int LDLid , int PassedTests, int UserID)
        {
            InitializeComponent();
            _LDLid = LDLid;
            _PassedTests = PassedTests;
            _UserID = UserID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
