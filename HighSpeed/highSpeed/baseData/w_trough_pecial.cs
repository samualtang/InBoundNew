using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound.Model;
using InBound.Business;

namespace highSpeed
{
    public partial class w_trough_pecial : Form
    {
        public string speacilname;
        public string speacilcode;
        public string speaciltrough;
        public List<HUNHETROUGH2> list;
        public w_trough_pecial(string speacilname, string speaciltrough, List<HUNHETROUGH2> list)
        {
            InitializeComponent();
            this.list = list;
            this.speacilname = speacilname;
            this.speaciltrough = speaciltrough;
        }

        private void w_trough_pecial_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = list.OrderBy(x => x.cigarettename).ToList(); ;
        }
         
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                textBox_name.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                label_troughnum.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                label_code.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
 
    }
}
