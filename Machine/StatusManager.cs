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
        decimal groupNo = 0;
        public StatusManager()
        {
            InitializeComponent();
            groupNo = decimal.Parse(ConfigurationManager.AppSettings["GroupNO"].ToString());
            initTroughnum();
            cbTroughNum.SelectedIndex = 0;
            button1_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Bind();

        }
        public void initTroughnum()
        {
            for (int i = 1; i < 23; i++)
            {
                cbTroughNum.Items.Add(i);
            }
        }

        private void Bind()
        {

            List<TaskDetail> list;
            if (!textBox3.Text.Equals(""))
            {
                list = TaskService.getMachineTask((groupNo-1)*22+cbTroughNum.SelectedIndex+1,decimal.Parse(textBox3.Text));
            }
            else
            {
                list = TaskService.getAllMachineTask((groupNo - 1) * 22 + cbTroughNum.SelectedIndex + 1);
            }
            task_data.Rows.Clear();
            try
            {
                String status = "";
                foreach (var item in list)
                {
                    
                    status =item.MachineState+"";

                    int index = this.task_data.Rows.Add();
                    this.task_data.Rows[index].Cells[0].Value = item.UnionTasknum;
                    this.task_data.Rows[index].Cells[1].Value = item.Billcode;
                    this.task_data.Rows[index].Cells[2].Value = item.CIGARETTDECODE;
                    this.task_data.Rows[index].Cells[3].Value = item.CIGARETTDENAME;
                    this.task_data.Rows[index].Cells[4].Value = item.Machineseq;
                    this.task_data.Rows[index].Cells[5].Value = item.tNum;
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
                    this.task_data.Rows[index].Cells[6].Value = status;

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

          //decimal  sortgroupno1 = decimal.Parse(ConfigurationManager.AppSettings["Group1"].ToString());
          //decimal sortgroupno2 = decimal.Parse(ConfigurationManager.AppSettings["Group2"].ToString());
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
                        taskState =15;

                    }
                     else if (radioButton3.Checked)
                     {
                   
                         taskState = 20;
                     }
                    
                    decimal dFrom=decimal.Parse(from);
                    decimal tFrom=decimal.Parse(to);
                    for (decimal i = dFrom; i <= tFrom; i++)
                    {
                        
                            
                            TaskService.UpdateMachine(  i,(groupNo - 1) * 22 + cbTroughNum.SelectedIndex + 1,taskState);
                       
                    }
                     button1_Click(null, null);
                 
                }
             

            }
        }
    }
}
