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
        int[] types = { -1,10, 20, 30, 40, 42, 50, 52, 55, 60, 70, 80, 90, 97, 100 };
        public QueryForm()
        {

            InitializeComponent();
            cbtype.SelectedIndex = 0;
        }
        public void search()
        {
            dataGridView1.AutoGenerateColumns = false;
            DateTime begin =DateTime.Parse( dateTimePicker1.Text);
            DateTime end = DateTime.Parse(dateTimePicker2.Text).AddDays(1);
            if (end <=begin)
            {
                MessageBox.Show("结束时间不能小于开始时间");
                return;
            }
            if (cbtype.SelectedIndex == 0)
            {
                dataGridView1.DataSource = InfJobDownLoadService.Query(-1, begin,end, txtCode.Text, txtCellNO.Text, txtPlace.Text);
            }
            else
            {
                int type = types[cbtype.SelectedIndex];
                
                dataGridView1.DataSource = InfJobDownLoadService.Query(type, begin,end, txtCode.Text, txtCellNO.Text, txtPlace.Text);
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

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
                {
                if (e.Value.ToString() == "10")
                {
                    e.Value = "码垛任务";
                }
                else if (e.Value.ToString() == "20")
                {
                    e.Value = "入库单入库任务";
                }
                else if (e.Value.ToString() == "30")
                {
                    e.Value = "成品入库";
                }
                else if (e.Value.ToString() == "40")
                {
                    e.Value = "返库任务";
                }
                else if (e.Value.ToString() == "42")
                {
                    e.Value = "一楼返库";
                }
                else if (e.Value.ToString() == "50")
                {
                    e.Value = "出库任务";
                }
                 else if (e.Value.ToString() == "52")
                {
                    e.Value = "一楼出库";
                }
                 else if (e.Value.ToString() == "55")
                {
                    e.Value = "补货出库";
                }
                 else if (e.Value.ToString() == "60")
                {
                    e.Value = "自动拆垛补货任务";
                }
                  else if (e.Value.ToString() == "70")
                {
                    e.Value = "人工拆垛补货任务";
                }
                  else if (e.Value.ToString() == "80")
                {
                    e.Value = "开箱任务";
                }
                else if (e.Value.ToString() == "90")
                {
                    e.Value = "托盘条码下达";
                }
                 else if (e.Value.ToString() == "97")
                {
                    e.Value = "任务取消";
                }
                else if (e.Value.ToString() == "100")
                {
                    e.Value = "空托盘回收任务";
                }
            }
        }



    }
}
