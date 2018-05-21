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
             sortgroupno1 = decimal.Parse(ConfigurationManager.AppSettings["Group1"].ToString());
             sortgroupno2 = decimal.Parse(ConfigurationManager.AppSettings["Group2"].ToString());
             task_data.DoubleBufferedDataGirdView(true);
            button1_Click(null, null);
             CmbBind();
            AutoSizeColumn(task_data);
            Text = "第" + sortgroupno1 + "组和第" + sortgroupno2 + "组,预分拣状态管理";
          
           
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

            List<TaskDetail> list;
            if (!string.IsNullOrWhiteSpace(txtsortnum.Text) && string.IsNullOrWhiteSpace(txtMachine.Text)  )//分拣任务号
            { 
               list = TaskService.getFJData(decimal.Parse(txtsortnum.Text), sortgroupno1, sortgroupno2); 
            }
            else if (!string.IsNullOrWhiteSpace(txtMachine.Text) && string.IsNullOrWhiteSpace(txtsortnum.Text) )//机械手号
            {
                list = TaskService.getFJData(Convert.ToInt32(txtMachine.Text), sortgroupno1, sortgroupno2); 
            } 
            //else if (!string.IsNullOrWhiteSpace(txtTasknum.Text) && cmbSelect.SelectedIndex == 2)//任务号
            //{
            //    list = TaskService.getFJDataByTasknum(Convert.ToInt32(txtTasknum.Text), sortgroupno1, sortgroupno2);
            //}
            else if (!string.IsNullOrWhiteSpace(txtMachine.Text) && !string.IsNullOrWhiteSpace(txtsortnum.Text)  )//分拣任务号 机械手号
            {
                list = TaskService.getFJData(Convert.ToInt32(txtMachine.Text), decimal.Parse(txtsortnum.Text), sortgroupno1, sortgroupno2);
            } 
            else
            {
                list = TaskService.getFJDataAll(sortgroupno1, sortgroupno2);
            }
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
                    this.task_data.Rows[index].Cells[1].Value = item.UnionTasknum;//机械手任务号
                    this.task_data.Rows[index].Cells[2].Value = item.Billcode;//订单号
                    this.task_data.Rows[index].Cells[3].Value = item.CIGARETTDECODE;//香烟编号
                    this.task_data.Rows[index].Cells[4].Value = item.CIGARETTDENAME;//香烟名称
                    if (item.GroupNO % 2 != 0)
                    {
                        groupline = "A线";
                    }
                    else
                    {
                        groupline = "B线";
                    }
                    this.task_data.Rows[index].Cells[5].Value = groupline;//组号
                    this.task_data.Rows[index].Cells[6].Value = item.Machineseq;//机械手号
                    this.task_data.Rows[index].Cells[7].Value = item.POKENUM;//抓烟数量
                    this.task_data.Rows[index].Cells[8].Value = item.POCKPLACE;//放烟位置
                   
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
                    this.task_data.Rows[index].Cells[9].Value = status;//状态位
                    if (status == "完成")
                    {
                        this.task_data.Rows[index].Cells[9].Style = dgvStyle;
                    }
                }
           

            }
            finally
            {
               
            }

        }
        void CmbBind()
        {
            cmbSelect.Items.Add("分拣任务号");
            cmbSelect.Items.Add("设备号");
            //cmbSelect.Items.Add("任务号");
            cmbSelect.Items.Add("分拣任务号设备号");
            cmbSelect.SelectedIndex = 0;

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
                            taskState = 15;

                        }
                        else if (radioButton3.Checked)
                        {

                            taskState = 20;
                        }
                        TaskService.updateTask(decimal.Parse(from), decimal.Parse(to), taskState);
                        decimal dFrom = decimal.Parse(from);
                        decimal tFrom = decimal.Parse(to);
                        for (decimal i = dFrom; i <= tFrom; i++)
                        {
                            if (cbLineA.Checked)
                            {
                                //if (taskState == 20)//转到机械手负责
                                //{
                                //    InBoundService.UpdateInOut(i, sortgroupno1);
                                //}
                                TaskService.UpdateStatus(sortgroupno1, taskState, i);
                            }
                            if (cbLineB.Checked)
                            {
                                //if (taskState == 20)
                                //{
                                //    InBoundService.UpdateInOut(i, sortgroupno2);
                                //}
                                TaskService.UpdateStatus(sortgroupno2, taskState, i);
                            }
                        }
                        button1_Click(null, null);

                    }


                }
            }
            catch (Exception ex)
            { 
                MessageBox.Show("错误信息:" + ex.Message);
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
            dgViewFiles.Columns[1].Frozen = true;
        }
        private void StatusManager_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.CenterScreen;
            asc.controllInitializeSize(this);
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
    }
}
