using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SortingControlSys.PubFunc;
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
            if (!textBox3.Text.Equals(""))
            {
             
                list = TaskService.getUnionData(decimal.Parse(textBox3.Text));
            }
            else
            {
                list = TaskService.getUnionDataAll();
            }
            task_data.Rows.Clear();
            try
            {
                String status = "";
                foreach (var item in list)
                {
                    
                    status =item.UnionState+"";

                    int index = this.task_data.Rows.Add();
                    this.task_data.Rows[index].Cells[0].Value = item.SortNum;
                    this.task_data.Rows[index].Cells[1].Value = item.Billcode;
                 
                    this.task_data.Rows[index].Cells[2].Value = item.tNum;
                   
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
                    int taskState = 10;
                    if (textBox2.Text == "")
                    {
                        to = from;
                    }
                     if (radioButton2.Checked)
                    {
                        taskState =15;

                    }
                     else if (radioButton3.Checked)
                     {
                   
                         taskState = 20;
                     }
                     TaskService.updateTask(decimal.Parse(from), decimal.Parse(to), taskState);
                    int dFrom=int.Parse(from);
                    int tFrom=int.Parse(to);
                    for (int i = dFrom; i <= tFrom; i++)
                    {
                        
                            //InBoundService.UpdateInOut(i, sortgroupno1);
                            TaskService.UpdateUnionStatus( taskState, i);
                       
                    }
                     button1_Click(null, null);
                 
                }
             

            }
        }
    }
}
