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
    public partial class ReportDetailForm : Form
    {
        public ReportDetailForm()
        {
            InitializeComponent();
        }
        public void search()
        {
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.DataSource = AtsCellInfoDetailService.GetReport();
           
        }
        public void searchQuery()
        {
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.DataSource = AtsCellInfoDetailService.GetReportDetail(tbName.Text.Trim(),tbCode.Text.Trim());

        }
        private void QueryForm_Load(object sender, EventArgs e)
        {
            searchQuery();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            searchQuery();
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            searchQuery();
        }



    }
}
