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
    public partial class frmAppApointments : Form
    {
        private int _LDLAi;
        private int _PassedTests;
        public frmAppApointments(int lDLAi, int passedTests)
        {
            InitializeComponent();
            _LDLAi = lDLAi;
            _PassedTests = passedTests;
            UserControlAppDetails.LDLid = _LDLAi;
            UserControlAppDetails.PassedTests = _PassedTests;
        }
    }
}
