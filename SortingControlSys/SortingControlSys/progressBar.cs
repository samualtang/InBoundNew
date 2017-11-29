using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SortingControlSys
{
    public partial class progressBar : Form
    {
        public progressBar()
        {
            InitializeComponent();
            int rcounts = 100;
            for (int i = 0; i < rcounts; i++)
            {
                // progressBar1.Value = ((i + 1) * 100 / rcounts);
                Application.DoEvents();
                progressBar1.Increment(1);


                //Application.DoEvents();
                label1.Text = ((i / rcounts) * 100).ToString() + "%";
            }

        }
    }
}
