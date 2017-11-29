using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound.Business;
using InBound;
namespace FormUI
{
    public partial class DubiousForm : Form
    {
        public DubiousForm()
        {
            InitializeComponent();
        }
      
        public void search()
        {

           
            dataGridView1.AutoGenerateColumns = false;
            
            dataGridView1.DataSource = AtsCellService.GetAtsCellList(50);
            
        }
        private void QueryForm_Load(object sender, EventArgs e)
        {
            search();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            search();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

       
        String jobid = "";
        String errorCode = "";
        String source = "";
        String target="";
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count > 0)
            {

                jobid=dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                errorCode = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                source = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                target = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            }
        }

    }
}
