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
        public frmScheduleTest(int LDLAid, int TestAppoID, int TesTypeID)
        {
            InitializeComponent();
            _TestAppoID = TestAppoID;
            _TesTypeID = TesTypeID;
            _LDLAid = LDLAid;
            UrScheduleTestAndRetake.TestAppoID = _TestAppoID;
            UrScheduleTestAndRetake.TesTypeID = _TesTypeID;
            UrScheduleTestAndRetake.LDLAid = _LDLAid;
            
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            UrScheduleTestAndRetake.TestAppoID = _TestAppoID;
            UrScheduleTestAndRetake.TesTypeID = _TesTypeID;
            UrScheduleTestAndRetake.LDLAid = _TesTypeID;
        }
    }
}
