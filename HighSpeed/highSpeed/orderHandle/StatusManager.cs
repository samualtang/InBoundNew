﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using InBound;
using InBound.Business;
using System.Configuration;
using InBound.Model;

namespace highSpeed
{
    public partial class StatusManager : Form
    {
      
        AutoSizeFormClass asc = new AutoSizeFormClass();
        public StatusManager()
        {
            InitializeComponent();
            
             task_data.DoubleBufferedDataGirdView(true);
           // button1_Click(null, null);
             CmbBind();
            AutoSizeColumn(task_data);
            //Text = "第" + sortgroupno1 + "组和第" + sortgroupno2 + "组,预分拣状态管理";
          
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Bind();
            }
            catch (Exception ex )
            { 
                MessageBox.Show("错误信息:" + ex.Message);
            }

        }

        private void Bind()
        {

            List<TaskDetail> list=new List<TaskDetail>();

            decimal beginSortnum = 0;
            decimal endSortnum = 0;
            decimal sortstate = 10;
            if (cbGroup.SelectedIndex == -1)
            {
                MessageBox.Show("请选择组号!");
                return;

            }
            if (cmb_mainbelt.SelectedIndex == -1)
            {
                MessageBox.Show("请选择主皮带号!");
                return;

            }

            if (tbBegin.Text != "")
            {
                
                 decimal.TryParse(tbBegin.Text,out beginSortnum);
            }
            if (tbEnd.Text != "")
            {
                decimal.TryParse(tbEnd.Text, out endSortnum);
            }
            if (beginSortnum == 0)
            {
                if (endSortnum == 0)
                {
                    endSortnum = decimal.MaxValue;
                }
            }
            else
            {
                if (endSortnum == 0)
                {
                    endSortnum = decimal.MaxValue;
                }
                else
                {
                    if (endSortnum < beginSortnum)
                    {
                        MessageBox.Show("开始任务号必须大于结束任务号!");
                        return;
                    }
                }
                 
            }
            if (rb1.Checked)
            {
                sortstate = 10;
            }
            else if (rb2.Checked)
            {
                sortstate = 12;
            }
            else if (rb3.Checked)
            {
                sortstate = 15;
            }
            else if (rb4.Checked)
            {
                sortstate = 20;
            }
            list = TaskService.getFJDataByGroupMainBelt(cbGroup.SelectedIndex+1, this.cmb_mainbelt.SelectedIndex+1,beginSortnum,endSortnum,sortstate);
            task_data.Rows.Clear();
            try
            {
                String status = "";
                foreach (var item in list)
                {
                    
                    status =item.SortState+"";
                    string groupline = "";
                    DataGridViewCellStyle dgvStyle = new DataGridViewCellStyle();
                    dgvStyle.BackColor = Color.LightGreen;
                    int index = this.task_data.Rows.Add();
                    this.task_data.Rows[index].Cells[0].Value = item.SortNum;//预分拣任务号
                    this.task_data.Rows[index].Cells[1].Value = item.MainBelt;//主皮带
                    this.task_data.Rows[index].Cells[2].Value = item.GroupNO;//组号
                    this.task_data.Rows[index].Cells[3].Value = item.Billcode;//订单号
                    this.task_data.Rows[index].Cells[4].Value = item.CIGARETTDECODE;//香烟编号
                                     this.task_data.Rows[index].Cells[5].Value = item.CIGARETTDENAME;//香烟名称
                    this.task_data.Rows[index].Cells[6].Value = item.Machineseq;//机械手号
                    this.task_data.Rows[index].Cells[7].Value = item.POKENUM;//抓烟数量
                    this.task_data.Rows[index].Cells[8].Value = item.POCKPLACE;//放烟位置
                    this.task_data.Rows[index].Cells[9].Value = item.uniontasknum;//机械手任务号
                    this.task_data.Rows[index].Cells[10].Value = item.meragenum;//吸烟数量
                   
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
                    this.task_data.Rows[index].Cells[11].Value = status;//状态位
                    if (status == "完成")
                    {
                        this.task_data.Rows[index].Cells[11].Style = dgvStyle;
                    }
                }
           

            }
            finally
            {
               
            }

        }
        void CmbBind()
        {
            //cmbSelect.Items.Add("分拣任务号");
            //cmbSelect.Items.Add("设备号");
            ////cmbSelect.Items.Add("任务号");
            //cmbSelect.Items.Add("分拣任务号设备号");
            //cmbSelect.SelectedIndex = 0;

        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                decimal beginSortnum = 0;
                decimal endSortnum = 0;
                decimal sortstate = 10;
                 decimal updatestate = 10;
                 String updatedesc = "";
                if (cbGroup.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择组号!");
                    return;

                }
                if (cmb_mainbelt.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择主皮带号!");
                    return;

                }

                if (tbBegin.Text != "")
                {

                    decimal.TryParse(tbBegin.Text, out beginSortnum);
                }
                if (tbEnd.Text != "")
                {
                    decimal.TryParse(tbBegin.Text, out endSortnum);
                }
                if (beginSortnum == 0)
                {
                    if (endSortnum == 0)
                    {
                        endSortnum = decimal.MaxValue;
                    }
                }
                else
                {
                    if (endSortnum == 0)
                    {
                        endSortnum = decimal.MaxValue;
                    }
                    else
                    {
                        if (endSortnum < beginSortnum)
                        {
                            MessageBox.Show("开始任务号必须大于结束任务号!");
                            return;
                        }
                    }

                }
                if (rb1.Checked)
                {
                    sortstate = 10;
                }
                else if (rb2.Checked)
                {
                    sortstate = 12;
                }
                else if (rb3.Checked)
                {
                    sortstate = 15;
                }
                else if (rb4.Checked)
                {
                    sortstate = 20;
                }
                if (rbg1.Checked)
                {
                    updatestate = 10;
                    updatedesc = "新增";
                }
                else if (rbg2.Checked)
                {
                    updatestate = 20;
                    updatedesc = "已完成";
                    
                }
                DialogResult MsgBoxResult = MessageBox.Show("确定要更新组号:" + (cbGroup.SelectedIndex + 1) + "; 主皮带号:" + (this.cmb_mainbelt.SelectedIndex + 1) + "任务号从" + beginSortnum + "-" + endSortnum + "状态为:" + updatedesc + "?",//对话框的显示内容 
                                                          "操作提示",//对话框的标题  
                                                          MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                          MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                          MessageBoxDefaultButton.Button2);//
                if (MsgBoxResult == DialogResult.Yes)
                {
                    TaskService.UpdateDataByGroupMainBelt(cbGroup.SelectedIndex + 1, this.cmb_mainbelt.SelectedIndex + 1, beginSortnum, endSortnum, sortstate, updatestate);
                    Bind();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            { 
                MessageBox.Show("错误信息:" + ex.Message);
            }
        }
        /// <summary>
        /// 获取主皮带信息
        /// </summary>
        /// <returns></returns>
        public static decimal[] getmainbelt()
        {
            using (Entities data = new Entities())
            {
                var query = (from items in data.T_PRODUCE_SORTTROUGH where items.TROUGHTYPE == 30 orderby items.MACHINESEQ select items.MACHINESEQ).ToList();
                decimal[] seq = new decimal[query.Count];
                int i = 0;
                foreach (var item in query)
                {
                    seq[i] = Convert.ToInt32(item);
                    i++;
                }
                return seq;
            }
        }

      /// <summary>
        /// 更新预分拣任务状态--用于状态管理
      /// </summary>
      /// <param name="groupno">组号</param>
      /// <param name="stage">预分拣状态</param>
      /// <param name="sortnum">开始分拣任务号</param>
      /// <param name="mainbelt">主皮带</param>
        public void UpdateStatus(decimal groupno, int stage, decimal sortnum, decimal mainbelt)
        {
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_PRODUCE_POKE where item.GROUPNO == groupno && item.SORTNUM == sortnum && item.MAINBELT == mainbelt select item).ToList();
                if (query != null && query.Count > 0)
                {
                    //若重置状态为未发送 则更新机械手任务号、放烟位置、机械手抓烟数为0，机械手任务状态、分拣状态为10
                    if (stage==10)
                    {
                        foreach (var item in query)
                        {
                            item.SORTSTATE = stage;
                            item.MACHINESTATE = 10;
                            item.MERAGENUM = 0;
                            item.POKEPLACE = 0;
                            item.UNIONTASKNUM = 0;
                        }
                    }
                    else
                    {
                        foreach (var item in query)
                        {
                            item.SORTSTATE = stage; 
                        }
                    } 
                    entity.SaveChanges();
                }
            }

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
           // dgViewFiles.Columns[1].Frozen = true;
        }
        private void StatusManager_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.CenterScreen;
            asc.controllInitializeSize(this);

            //取出主皮带总数 
            decimal[] seq = getmainbelt();
            for (int i = 0; i < seq.Length; i++)
            {
                cmb_mainbelt.Items.Add(seq[i]);
            }
        }

        private void StatusManager_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        private void cmbSelect_SelectedIndexChanged(object sender, EventArgs e)
        { 
            //switch (cmbSelect.SelectedIndex )
            //{
            //    case 0:
            //        lblsortnum.Visible = true;
            //        txtsortnum.Visible = true;
            //        txtsortnum.Focus(); 
            //        lblmachine.Visible = false;
            //        txtMachine.Visible = false;
            //        //lbltasknum.Visible = false;
            //        //txtTasknum.Visible = false;
            //        txtMachine.Text = "";
            //        //txtTasknum.Text = "";
            //        break; 
            //    case 1:
            //        lblmachine.Visible = true;
            //        txtMachine.Visible = true;
            //        txtMachine.Focus();
            //        lblsortnum.Visible = false;
            //        txtsortnum.Visible = false;
            //       // lbltasknum.Visible = false;
            //       // txtTasknum.Visible = false;
            //        txtsortnum.Text = "";
            //       // txtTasknum.Text = "";
            //        break;
            //    //case 2:
            //    //    lblmachine.Visible = false;
            //    //    txtMachine.Visible = false;
            //    //    lblsortnum.Visible = false;
            //    //    txtsortnum.Visible = false;
            //    //    txtMachine.Text = "";
            //    //    txtsortnum.Text = "";
            //    //    lbltasknum.Visible = true;
            //    //    txtTasknum.Visible = true;
            //    //    txtTasknum.Focus();
            //    //    break;
            //    case 2:
            //        lblmachine.Visible = true;
            //        txtMachine.Visible = true; 
            //        lblsortnum.Visible = true;
            //        txtsortnum.Visible = true;
            //        //lbltasknum.Visible = false;
            //       // txtTasknum.Visible = false;
            //        txtsortnum.Focus();
            //        break;
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult MsgBoxResult = MessageBox.Show("确定要初始化所有数据?",//对话框的显示内容 
                                                          "操作提示",//对话框的标题  
                                                          MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                          MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                          MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
            //Console.WriteLine(MsgBoxResult);
            if (MsgBoxResult == DialogResult.Yes)
            {

                TaskService.UpdateAll();
                MessageBox.Show("初始化成功");
            }
            else
            {
                return;
            }
        }
    }
}
