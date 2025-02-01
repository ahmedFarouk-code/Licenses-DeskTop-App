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
        private int _TesTypeID;
        private int _LDLAid;
        private int _UserID;
        public frmScheduleTest(int LDLAid, int TestAppoID, int TesTypeID , int UserID)
        {
            InitializeComponent();
            _TestAppoID = TestAppoID;
            _TesTypeID = TesTypeID;
            _LDLAid = LDLAid;
            _UserID = UserID;
            UrScheduleTestAndRetake.TestAppoID = _TestAppoID;
            UrScheduleTestAndRetake.TesTypeID = _TesTypeID;
            UrScheduleTestAndRetake.LDLAid = _LDLAid;
            UrScheduleTestAndRetake.UserID = _UserID;



        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            UrScheduleTestAndRetake.TestAppoID = _TestAppoID;
            UrScheduleTestAndRetake.TesTypeID = _TesTypeID;
            UrScheduleTestAndRetake.LDLAid = _TesTypeID;
            UrScheduleTestAndRetake.UserID = _UserID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
