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
    public partial class frmScheduleTest : Form
    {
        private int _TestAppoID;
        private int _LDLAid;
        private int _UserID;
        private int _TestType;
        public frmScheduleTest(int LDLAid, int TestAppoID , int UserID ,int TestType)
        {
            InitializeComponent();
            _TestAppoID = TestAppoID;
            _LDLAid = LDLAid;
            _UserID = UserID;
            _TestType = TestType;
            UrScheduleTestAndRetake.TestAppoID = _TestAppoID;
            UrScheduleTestAndRetake.LDLAid = _LDLAid;
            UrScheduleTestAndRetake.UserID = _UserID;
            UrScheduleTestAndRetake.TestType = _TestType;


        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            UrScheduleTestAndRetake.TestAppoID = _TestAppoID;
            UrScheduleTestAndRetake.LDLAid = _LDLAid;
            UrScheduleTestAndRetake.UserID = _UserID;
            UrScheduleTestAndRetake.TestType = _TestType;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
