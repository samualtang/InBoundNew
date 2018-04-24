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
    public partial class CheckIngForm : Form
    {
        public CheckIngForm()
        {
            InitializeComponent();
        }
        public void search()
        {
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.DataSource = AtsCellInfoDetailService.GetCheckIng();
           
        }
        public void searchQuery()
        {
            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.DataSource = AtsCellInfoDetailService.GetCheckIng(tbName.Text.Trim(), tbCode.Text.Trim());

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

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                if (e.Value.ToString() == "10")
                {
                    e.Value = "入库中";
                }
                else if (e.Value.ToString() == "20")
                {
                    e.Value = "出库中";
                }
            }
        }



    }
}
