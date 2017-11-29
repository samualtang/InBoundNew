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
    public partial class QueryForm : Form
    {
        public QueryForm()
        {
            InitializeComponent();
        }
        public void search()
        {
            dataGridView1.AutoGenerateColumns = false;
            if (cbtype.SelectedIndex == -1)
            {
                dataGridView1.DataSource = InfJobDownLoadService.Query(-1);
            }
            else
            {
                int type = 10;
                if (cbtype.SelectedIndex <= 2)
                {
                    type = (cbtype.SelectedIndex + 1) * 10;
                }
                else
                {
                    type=55;
                }
                dataGridView1.DataSource = InfJobDownLoadService.Query(type);
            }
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
