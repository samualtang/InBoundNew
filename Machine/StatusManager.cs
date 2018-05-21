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
        decimal groupNo = 0;
        AutoSizeFormClass asc = new AutoSizeFormClass();
        public StatusManager()
        {
            InitializeComponent();
            AutoSizeColumn(task_data);
            task_data.DoubleBufferedDataGirdView(true);
            groupNo = decimal.Parse(ConfigurationManager.AppSettings["GroupNO"].ToString());
            initTroughnum();
            initTroughnum2();
            Text ="第"+ groupNo +"组机械手状态管理";
            cbTroughNum.SelectedIndex = 0;
            button1_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Bind();
            }
            catch (Exception ex)
            { 
                MessageBox.Show("错误信息:" + ex.Message+"\r\n"+"请输入正确的任务号");
            }
           

        }
        public void initTroughnum()
        {
            for (int i = 1; i < 23; i++)
            {
                cbTroughNum.Items.Add(i);
            }
            cbTroughNum.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTroughNum.SelectedIndex = 0;
        }

        public void initTroughnum2()
        {
            for (int i = 1; i < 23; i++)
            {
                cbTroughNum2.Items.Add(i);
            }
            cbTroughNum2.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTroughNum2.SelectedIndex = 0;
        }

        private void Bind()
        {

            List<TaskDetail> list;
            if (!string.IsNullOrWhiteSpace(txtUnionTasknum.Text))
            {
                list = TaskService.getMachineTask((groupNo-1)*22+cbTroughNum2.SelectedIndex+1,decimal.Parse(txtUnionTasknum.Text));
            } 
            else
            {
                list = TaskService.getAllMachineTask((groupNo - 1) * 22 + cbTroughNum2.SelectedIndex + 1);
            }
            task_data.Rows.Clear();
            try
            {
                String status = "";
                foreach (var item in list)
                {
                    
                    status =item.MachineState+"";

                    int index = this.task_data.Rows.Add();
                    this.task_data.Rows[index].Cells[0].Value = item.SortNum;//任务号
                    this.task_data.Rows[index].Cells[1].Value = item.UnionTasknum;//合流任务号
                    this.task_data.Rows[index].Cells[2].Value = item.Billcode;//订单号
                    this.task_data.Rows[index].Cells[3].Value = item.CIGARETTDECODE;//香烟编号
                    this.task_data.Rows[index].Cells[4].Value = item.CIGARETTDENAME;//香烟名称
                    this.task_data.Rows[index].Cells[5].Value = item.Machineseq;//通道号
                    this.task_data.Rows[index].Cells[6].Value = item.tNum;//吸烟数量
                    this.task_data.Rows[index].Cells[7].Value = item.POKENUM;//放烟数量
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
                    this.task_data.Rows[index].Cells[9].Value = status;//状态

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

                //decimal  sortgroupno1 = decimal.Parse(ConfigurationManager.AppSettings["Group1"].ToString());
                //decimal sortgroupno2 = decimal.Parse(ConfigurationManager.AppSettings["Group2"].ToString());
                if (MsgBoxResult == DialogResult.Yes)
                {
                    if (textBox1.Text.Equals(""))
                    {
                        MessageBox.Show("请输入机械手任务号");
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
                            taskState = 15;

                        }
                        else if (radioButton3.Checked)
                        {

                            taskState = 20;
                        }

                        decimal dFrom = decimal.Parse(from);
                        decimal tFrom = decimal.Parse(to);
                        for (decimal i = dFrom; i <= tFrom; i++)//i 机械手任务号
                        {
                            if (taskState == 20)
                            {
                                InBoundService.UpdateMachineInOut(i, (groupNo - 1) * 22 + cbTroughNum.SelectedIndex + 1);
                            }

                            TaskService.UpdateMachine(i, (groupNo - 1) * 22 + cbTroughNum.SelectedIndex + 1, taskState);

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

        private void StatusManager_Load(object sender, EventArgs e)
        {
            asc.controllInitializeSize(this);
        }

        private void StatusManager_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
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

        private void cbTroughNum2_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                Bind();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误信息:" + ex.Message + "\r\n" + "请输入正确的任务号");
            }
        }
           

 
    }
}
