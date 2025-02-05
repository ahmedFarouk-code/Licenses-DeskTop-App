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
            urScheduleTestAndRetake1.LoadInfo(TestAppoID, LDLAid, UserID, TestType);




        }

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
