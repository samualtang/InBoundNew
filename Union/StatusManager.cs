﻿using System;
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
        AutoSizeFormClass asc = new AutoSizeFormClass();
        public StatusManager()
        {
            InitializeComponent();
            AutoSizeColumn(task_data);
          // button1_Click(null, null);
            this.StartPosition = FormStartPosition.CenterScreen;
            asc.controllInitializeSize(this);
            this.task_data.DoubleBufferedDataGirdView(true);
            Bind();
            
        }


     
          /// <summary>
        /// 获取主皮带号
        /// </summary>
        /// <param name="MachineNo"></param>
        /// <returns>主皮带</returns>
        int GetMainBeltNo(decimal MachineNo)
        {
            int mainbelt = 0;
            if (MachineNo <= 8)
            {
                mainbelt = 1;
            }
            else if (MachineNo >= 9 && MachineNo <= 16)
            {
                mainbelt = 2;
            }
            else if (MachineNo >= 17 && MachineNo <= 24)
            {
                mainbelt = 3;
            }
            else if (MachineNo >= 25 && MachineNo <= 32)
            {
                mainbelt = 4;
            }
            return mainbelt;
        }
        /// <summary>
        /// 获取组号
        /// </summary>
        /// <param name="machineNo">机械手号</param>
        /// <returns></returns>4   
        decimal GetGroupNo(int machineNo)
        {
            decimal groupno = 0;
            if (machineNo >= 8)
            {
                groupno = machineNo % 8;// Convert.ToDecimal(Math.IEEERemainder(machineNo, 8));//获得组号
            }
            else
            {
                groupno = machineNo;
            }
            if (groupno == 0)
            {
                groupno = 8;
            }
            return groupno;
        }
 
        private void Bind()
        {

            List<TaskDetail> list;
            if (!string.IsNullOrWhiteSpace(textSortNum.Text))
            {
                list = TaskService.getUnionDataAll().Where(a=> a.SortNum  == Convert.ToDecimal( textSortNum.Text)).ToList();
            }
            else if (!string.IsNullOrWhiteSpace(txtMachineseq.Text))
            {  
                list = TaskService.getUnionDataAll().Where(a => a.GroupNO == GetGroupNo(Convert.ToInt32( txtMachineseq.Text))  && a.MainBelt == GetMainBeltNo(Convert.ToInt32( txtMachineseq.Text)) ).ToList();
            }
            else
            {
                list = TaskService.getUnionDataAll().Take(150).ToList();
            }
            task_data.Rows.Clear();
            try
            {
                String status = "";
                foreach (var item in list)
                { 
                    status =item.UnionState+"";
                    DataGridViewCellStyle dgvStyle = new DataGridViewCellStyle();
                    dgvStyle.BackColor = Color.LightGreen;
                    int index = this.task_data.Rows.Add();
                    this.task_data.Rows[index].Cells[0].Value = item.SortNum;//分拣任务号
                    this.task_data.Rows[index].Cells[1].Value = item.Billcode;//订单号   
                    this.task_data.Rows[index].Cells[2].Value = item.MainBelt;//主皮带
                    this.task_data.Rows[index].Cells[3].Value = item.PACKAGEMACHINE;//包装机
                    this.task_data.Rows[index].Cells[4].Value = (item.GroupNO + ((item.MainBelt - 1) * 8));//机械手号
                    this.task_data.Rows[index].Cells[5].Value = item.tNum;//吸烟数量 
                    this.task_data.Rows[index].Cells[6].Value = item.CIGARETTDENAME;//卷烟名称 
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
                        status = "完成";
                        this.task_data.Rows[index].Cells[7].Style = dgvStyle;
                    }
                    this.task_data.Rows[index].Cells[7].Value = status;//状态位
                }
           

            }
            finally
            {
               
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            { 
                DialogResult MsgBoxResult = MessageBox.Show("确定要更新任务?",//对话框的显示内容 
                                                                "操作提示",//对话框的标题 
                                                                MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                                MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                                MessageBoxDefaultButton.Button2);//定义对话框的按钮式样 
                if (MsgBoxResult == DialogResult.Yes)
                {
                    if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(cmbMainbelt.Text))
                    {
                        MessageBox.Show("请输入任务号和主皮带号");
                        return;
                    }
                    else
                    {
                        String from = textBox1.Text;
                        String to = textBox2.Text;
                        int taskState = 10;
                        if (string.IsNullOrWhiteSpace(textBox2.Text) )
                        {
                            to = from;
                        }
                        if (Convert.ToDecimal(textBox1.Text) > Convert.ToDecimal(textBox2.Text)) //防止任务号输反
                        {
                            from = to;
                        }
                        if (radioButton2.Checked)
                        {
                            taskState = 15;

                        }
                        else if (radioButton3.Checked)
                        {

                            taskState = 20;
                        }
                        decimal mainbelt = Convert.ToDecimal(cmbMainbelt.SelectedItem);
                        TaskService.updateUnionTask(decimal.Parse(from), decimal.Parse(to), mainbelt, taskState);
                        //int dFrom = int.Parse(from);
                        //int tFrom = int.Parse(to);
                        //for (int i = dFrom; i <= tFrom; i++)
                        //{

                        //    //InBoundService.UpdateInOut(i, sortgroupno1);
                        //    TaskService.UpdateUnionStatus(taskState, i);

                        //}
                        //button1_Click(null, null);
                        Bind();

                    } 
                }
            }
            catch (Exception ex)
            { 
                MessageBox.Show("错误信息:" + ex.Message);
            }
        }

       
        /// <summary>
        /// 使DataGridView的列自适应宽度
        /// </summary>
        /// <param name="dgViewFiles"></param>
        private void AutoSizeColumn(DataGridView dgViewFiles)
        {
            int width = 0;
            //使列自使用宽度
            //对于DataGridView的每一个列都调整
            for (int i = 0; i < dgViewFiles.Columns.Count; i++)
            {

                //将每一列都调整为自动适应模式
                dgViewFiles.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                //记录整个DataGridView的宽度
                width += dgViewFiles.Columns[i].Width;

            }
            //判断调整后的宽度与原来设定的宽度的关系，如果是调整后的宽度大于原来设定的宽度，
            //则将DataGridView的列自动调整模式设置为显示的列即可，
            //如果是小于原来设定的宽度，将模式改为填充。
            if (width > dgViewFiles.Size.Width)
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            //冻结某列 从左开始 0，1，2
            //dgViewFiles.Columns[0].Width = 50;
            dgViewFiles.Columns[1].Frozen = true;
        }

        private void StatusManager_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void 退出EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult MsgBoxResult = MessageBox.Show("确定要退出程序?",//对话框的显示内容 
                                                           "操作提示",//对话框的标题  
                                                           MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                           MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                           MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
            //Console.WriteLine(MsgBoxResult);
            if (MsgBoxResult == DialogResult.Yes)
            {
                
                this.Close();
                //System.Environment.Exit(System.Environment.ExitCode);
            }
            else
            {
                return;
            }
        }

        private void StatusManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bind();
        }

 

       

 
    }
}
