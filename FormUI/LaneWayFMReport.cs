using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound.Business;

namespace FormUI
{
    public partial class LaneWayFMReport : Form
    {
        public LaneWayFMReport()
        {
            InitializeComponent();
        }
        public void search()
        {
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.DataSource = AtsCellService.GetUseRate();
           
        }
       
        private void QueryForm_Load(object sender, EventArgs e)
        {
            search();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            search();
        }

       



    }
}
