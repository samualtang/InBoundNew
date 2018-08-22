using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound;
using InBound.Business;
using System.Configuration;
using InBound.Model;

namespace SortingControlSys.SortingControl
{
    public partial class StatusManager : Form
    {
        decimal sortgroupno1;
        decimal sortgroupno2;
        public StatusManager()
        {
            InitializeComponent();
            button1_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Bind();

        }

        private void Bind()
        {

            List<TaskDetail> list;
            if (!string .IsNullOrWhiteSpace( textBox3.Text))
            {
             
                list = UnPokeService.getData(decimal.Parse(textBox3.Text));
            }
            else
            {
                list = UnPokeService.getDataAll();
            }
            task_data.Rows.Clear();
            try
            {
                String status = "";
                foreach (var item in list)
                {
                    DataGridViewCellStyle dgvStyle = new DataGridViewCellStyle();
                    dgvStyle.BackColor = Color.LightGreen;
                    // 存了状态值  

                    int index = this.task_data.Rows.Add();
                    this.task_data.Rows[index].Cells[0].Value = item.SortNum;//分拣任务号
                    this.task_data.Rows[index].Cells[1].Value = item.SENDTASKNUM; //发送包号
                    this.task_data.Rows[index].Cells[2].Value = item.Billcode;//订单号
                    this.task_data.Rows[index].Cells[3].Value = item.CIGARETTDECODE;//香烟编号
                    this.task_data.Rows[index].Cells[4].Value = item.CIGARETTDENAME;//香烟名称
                    this.task_data.Rows[index].Cells[5].Value = item.LINENUM;//分拣线
                    this.task_data.Rows[index].Cells[6].Value = item.POKENUM;//抓烟数量
                    this.task_data.Rows[index].Cells[7].Value = item.PACKAGEMACHINE;//包装机
                   // this.task_data.Rows[index].Cells[8].Value = item.STATUS;
                  
                    if (item.STATUS == 10)
                    {
                        status = "新增";
                    }
                    else if (item.STATUS ==  15)
                    {
                        status = "已发送";
                    }
                    else
                    {
                        status="完成";
                    }
                    this.task_data.Rows[index].Cells[8].Value = status;//状态位
                    if (status == "完成")
                    {
                        this.task_data.Rows[index].Cells[8].Style = dgvStyle;
                    }
                }
           

            }
            finally
            {
               
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult MsgBoxResult = MessageBox.Show("确定要更新任务?",//对话框的显示内容 
                                                            "操作提示",//对话框的标题 
                                                            MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                            MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                            MessageBoxDefaultButton.Button2);//定义对话框的按钮式样


            if (MsgBoxResult == DialogResult.Yes)
            {

                if (string.IsNullOrWhiteSpace(txtFrom.Text)  )
                {
                    MessageBox.Show("请输入分拣任务号");
                    return;
                }
                else
                {
                    string from = txtFrom.Text;
                    string to = txtTo.Text;
                    int status = 10;
                    if (string.IsNullOrWhiteSpace(txtTo.Text))//如果只输入第一个任号务 则其修改
                    {
                        to = from;
                    }
                    if (Convert.ToDecimal(txtFrom.Text) < Convert.ToDecimal(txtTo.Text)) //防止任务号输反
                    {
                        from = to;
                    }
                    if (radioButton2.Checked)//已发送
                    {
                        status = 15;

                    }
                    else if (radioButton3.Checked)//完成
                    {
                        status = 20;
                    }
                    if (status == 20)
                    {
                        UnPokeService.UpdateStroageInout(UnPokeService.GetListByBillCode(decimal.Parse(from), decimal.Parse(to)));
                    }

                    UnPokeService.UpdateTask(decimal.Parse(from), decimal.Parse(to), status);
                   
                }
                /////////////////////////////////////////////////////////////////之前机制
                //if (task_data.SelectedRows == null || task_data.SelectedRows.Count <= 0)
                //{
                //    MessageBox.Show("请选择一行更新");
                //    return;
                //}
                //else
                //{
                //    decimal taskState = 10;

                //    if (radioButton2.Checked)
                //    {
                //        taskState = 15;

                //    }
                //    else if (radioButton3.Checked)
                //    {

                //        taskState = 20;
                //    }
                //    if (taskState != 10)
                //    {
                //        UnPokeService.UpdateStroageInout(UnPokeService.GetListByBillCode(decimal.Parse(txtFrom.Text),decimal.Parse(txtTo.Text)));
                //    }

                //    UnPokeService.UpdateTask(decimal.Parse(txtFrom.Text), decimal.Parse(txtTo.Text), taskState);
                //}
                ////////////////////////////////////////////////////////////////////

                button1_Click(null, null);




            }
        }


    }
}
