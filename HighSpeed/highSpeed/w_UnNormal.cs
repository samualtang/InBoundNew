﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound;


namespace highSpeed
{
    public partial class w_UnNormal : Form
    {
        public w_UnNormal()
        {
            InitializeComponent();
            dgvTask.DoubleBufferedDataGirdView(true);
            this.Text = "异形烟";
        }
    }
}
