﻿using LicensesBusinessLayer;
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
    public partial class frmDriverLIcenseInfo : Form
    {
        private int  _LDLAid;
        public frmDriverLIcenseInfo(int LDLAid)
        {
            InitializeComponent();
            _LDLAid = LDLAid;
            urDriverLIcenseInfo1.LoadInfo(_LDLAid);
        }

        

        

       
    }
}
