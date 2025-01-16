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
    public partial class frmAddEditPerson : Form
    {
        private int _PersonID;
        public frmAddEditPerson(int PersonID)
        {
            InitializeComponent();

            _PersonID = PersonID;
            userControl11.PersonID = _PersonID;
        }

        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
            lblPersonID.Text = _PersonID.ToString();
        }
    }
}
