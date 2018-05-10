using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound.Model;
using InBound.Business;
using InBound;
using System.Configuration;

namespace SortingControlSys
{
    public partial class Fm_SelectedInfoEX : Form
    {
        decimal sortgroupno1;
        decimal sortgroupno2;
        AutoSizeFormClass asc = new AutoSizeFormClass();
        public Fm_SelectedInfoEX()
        {
            InitializeComponent();
            sortgroupno1 = decimal.Parse(ConfigurationManager.AppSettings["Group1"].ToString());
            sortgroupno2 = decimal.Parse(ConfigurationManager.AppSettings["Group2"].ToString());
            task_data.DoubleBufferedDataGirdView(true);
            button1_Click(null, null);
            CmbBind();
            AutoSizeColumn(task_data); 
            Bind();

        }

        private void Fm_SelectedInfoEX_Load(object sender, EventArgs e)
        {
            asc.controllInitializeSize(this);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Bind();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误信息:" + ex.Message);
            }
            
        }
        decimal sortsate = 0;
        private void cmbSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbSelect.SelectedIndex)
            {
                case 0:
                    lblsortnum.Visible = true;
                    txtsortnum.Visible = true;
                    txtsortnum.Focus();
                    lblsortnum.Location = new Point(600, 29);
                    txtsortnum.Location = new Point(749, 24);
                    lblmachine.Visible = false;
                    txtMachine.Visible = false;
                    txtMachine.Text = "";
                    break;
                case 1:
                    lblmachine.Visible = true;
                    txtMachine.Visible = true;
                    txtMachine.Focus();
                    lblsortnum.Visible = false;
                    txtsortnum.Visible = false;
                    txtsortnum.Text = "";
                    break;
                case 2:
                    lblmachine.Visible = true;
                    txtMachine.Visible = true;
                    lblsortnum.Location = new Point(348, 29);
                    txtsortnum.Location = new Point(486, 24);
                    lblsortnum.Visible = true;
                    txtsortnum.Visible = true;
                    txtsortnum.Focus();
                    break;
                case 3://新增
                    lblAndTextVisible();
                    sortsate = 10;
                    break; 
                case 4://已发送
                    lblAndTextVisible();
                    sortsate = 15;
                    break;
                case 5://完成
                    lblAndTextVisible();
                    sortsate = 20;
                    break;
            }
        }
        void lblAndTextVisible()
        {
            lblmachine.Visible = false;
            txtMachine.Visible = false;
            lblsortnum.Visible = false;
            txtsortnum.Visible = false;
        }
        private void Bind()
        {

            List<TaskDetail> list;
            if (!string.IsNullOrWhiteSpace(txtsortnum.Text) && cmbSelect.SelectedIndex == 0)//任务号
            {
                list = TaskService.getFJData(decimal.Parse(txtsortnum.Text), sortgroupno1, sortgroupno2);
            }
            else if (!string.IsNullOrWhiteSpace(txtMachine.Text) && cmbSelect.SelectedIndex == 1)//机械手号
            {
                list = TaskService.getFJData(Convert.ToInt32(txtMachine.Text), sortgroupno1, sortgroupno2);
            }
            else if (!string.IsNullOrWhiteSpace(txtMachine.Text) && !string.IsNullOrWhiteSpace(txtsortnum.Text) && cmbSelect.SelectedIndex == 2)//任务号 机械手号
            {
                list = TaskService.getFJData(Convert.ToInt32(txtMachine.Text), decimal.Parse(txtsortnum.Text), sortgroupno1, sortgroupno2);
            }
            else if (cmbSelect.SelectedIndex == 3 || cmbSelect.SelectedIndex == 4 || cmbSelect.SelectedIndex == 5)
            {
                list = TaskService.getFJDataAll(sortsate, sortgroupno1, sortgroupno2);
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

                    status = item.SortState + "";
                    DataGridViewCellStyle dgvStyle = new DataGridViewCellStyle();
                    dgvStyle.BackColor = Color.LightGreen;
                    int index = this.task_data.Rows.Add();
                    this.task_data.Rows[index].Cells[0].Value = item.SortNum;//任务号
                    this.task_data.Rows[index].Cells[1].Value = item.Billcode;//订单号
                    this.task_data.Rows[index].Cells[2].Value = item.CIGARETTDECODE;//香烟编号
                    this.task_data.Rows[index].Cells[3].Value = item.CIGARETTDENAME;//香烟名称
                    this.task_data.Rows[index].Cells[4].Value = item.Machineseq;//机械手号
                    this.task_data.Rows[index].Cells[5].Value = item.tNum;//抓烟数量
                    this.task_data.Rows[index].Cells[6].Value = item.POCKPLACE;//放烟位置

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
        void CmbBind()
        {
            cmbSelect.Items.Add("任务号");//0
            cmbSelect.Items.Add("设备号");//1
            cmbSelect.Items.Add("任务号设备号");//2
            cmbSelect.Items.Add("新增");//3
            cmbSelect.Items.Add("已发送");//4
            cmbSelect.Items.Add("完成");//5
            
            cmbSelect.SelectedIndex = 0;

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
        private void Fm_SelectedInfoEX_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

    }
}
