using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using SortingControlSys.PubFunc;
using InBound;
using InBound.Business;

namespace SortingControlSys.SortingControl
{
    public partial class StatusManager : Form
    {
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
   
            List<T_PRODUCE_TASK> list;
            if (!textBox3.Text.Equals(""))
            {
                list = TaskService.getAllTask(decimal.Parse(textBox3.Text));
            }
            else
            {
                list = TaskService.getAllTask();
            }
            task_data.Rows.Clear();
            try
            {
                String status = "";
                foreach (var item in list)
                {
                    
                    status =item.STATE+"";

                    int index = this.task_data.Rows.Add();
                    this.task_data.Rows[index].Cells[0].Value = item.TASKNUM;
                    this.task_data.Rows[index].Cells[1].Value = item.CUSTOMERNAME;
                    this.task_data.Rows[index].Cells[2].Value = item.ORDERQUANTITY;
                    if (status == "10")
                    {
                        status = "新增";
                    }
                    else if (status == "15")
                    {
                        status = "已发送";
                    }
                    else
                    {
                        status="完成";
                    }
                    this.task_data.Rows[index].Cells[3].Value = status;

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
                if (textBox1.Text.Equals(""))
                {
                    MessageBox.Show("请输入任务号");
                    return;
                }
                else
                {
                    String from = textBox1.Text;
                    String to = textBox2.Text;
                    decimal taskState = 10;
                    if (textBox2.Text == "")
                    {
                        to = from;
                    }
                     if (radioButton2.Checked)
                    {
                        taskState = 20;

                    }
                     else if (radioButton3.Checked)
                     {
                   
                         taskState = 30;
                     }
                     TaskService.updateTask1(decimal.Parse(from), decimal.Parse(to), taskState);

                     button1_Click(null, null);
                 
                }
             

            }
        }
    }
}
