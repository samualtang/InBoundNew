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
    public partial class CellForm : Form
    {
        public CellForm()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            dataGridView1.AutoGenerateColumns = false;
            for (int i = 1; i <= 82; i++)
            {
                comboBox3.Items.Add(i);
            }
            comboBox3.SelectedIndex = 0;
            bindData();
        }
        public void bindData()
        {
            dataGridView1.DataSource = AtsCellService.GetAtsCellList(comboBox1.SelectedIndex + 1 + "", comboBox2.SelectedIndex + 1, comboBox3.SelectedIndex + 1);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bindData();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {

                if (e.Value.ToString() == "10")
                {

                    e.Value = "正常";
                }
                else if (e.Value.ToString() == "20")
                {

                    e.Value = "禁入";
                }
                else if (e.Value.ToString() == "30")
                {

                    e.Value = "禁出";
                }
                else if (e.Value.ToString() == "40")
                {

                    e.Value = "禁用";
                }
                else if (e.Value.ToString() == "50")
                {

                    e.Value = "可疑";
                }

            }
            else if (e.ColumnIndex == 4)
            {
                if (e.Value.ToString() == "10")
                {

                    e.Value = "空闲";
                }
                else if (e.Value.ToString() == "20")
                {

                    e.Value = "卷烟载货";
                }
                else if (e.Value.ToString() == "30")
                {

                    e.Value = "预定";
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
            {
                MessageBox.Show("请选择一行进行操作");
            }
            else
            {

                String code = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                AtsCellService.UpdateAtsCell1(code, 10);
                bindData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
            {
                MessageBox.Show("请选择一行进行操作");
            }
            else
            {

                String code = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                AtsCellService.UpdateAtsCell1(code, 40);
                bindData();
            }
        }
    }
}
