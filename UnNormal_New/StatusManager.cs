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

namespace UnNormal_New
{
    public partial class StatusManager : Form
    {
        decimal sortgroupno1;
        decimal sortgroupno2;
        public StatusManager()
        {
            InitializeComponent();
            button1_Click(null, null);
            WriteLog.GetLog().Write("进入状态修改");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Bind();
            }
            catch (Exception ex)
            {
                WriteLog.GetLog().Write("状态修改异常"+ex.Message);
            }
           
    
     
        }

        private void Bind()
        {

            List<TaskDetail> list;
            if (!string.IsNullOrWhiteSpace(textBox3.Text) && !string.IsNullOrWhiteSpace(txtregioncode.Text))
            {
                list = UnPokeService.getDataAll().Where(a => a.REGIONCODE == txtregioncode.Text && a.SortNum == decimal.Parse(textBox3.Text)).OrderBy(a => a.SortNum).ToList();
                WriteLog.GetLog().Write("进行分拣任务号和车组号查询"  );
            }
            else if (!string .IsNullOrWhiteSpace( textBox3.Text))
            { 
                list = UnPokeService.getDataAll().Where(a => a.SortNum == decimal.Parse(textBox3.Text)).ToList();
                WriteLog.GetLog().Write("进行分拣任务号查询");
            }
            else if (!string.IsNullOrWhiteSpace(txtregioncode.Text))
            {
                list =UnPokeService.getDataAll().Where(a=> a.REGIONCODE == txtregioncode.Text).OrderBy(a=> a.SortNum).ToList();
                WriteLog.GetLog().Write("进行车组号查询");
            }
            else
            {
                list = UnPokeService.getDataAll();
                WriteLog.GetLog().Write("进行所有任务查询");
            }
            task_data.Rows.Clear();
            try
            {
                String status = "";
                string ssStatus = "";
                foreach (var item in list)
                {
                    DataGridViewCellStyle dgvStyle = new DataGridViewCellStyle();
                    dgvStyle.BackColor = Color.LightGreen;
                    // 存了状态值  

                    int index = this.task_data.Rows.Add();
                    this.task_data.Rows[index].Cells[0].Value = item.SORTSEQ;//户序
                    this.task_data.Rows[index].Cells[1].Value = item.SortNum; //分拣任务号
                    this.task_data.Rows[index].Cells[2].Value = item.SENDTASKNUM;//发送包号
                    this.task_data.Rows[index].Cells[3].Value = item.REGIONCODE;//香烟编号
                    this.task_data.Rows[index].Cells[4].Value = item.CUSTOMERNAME;//客户名称
                    this.task_data.Rows[index].Cells[5].Value = item.CIGARETTDECODE;//香烟编号
                    this.task_data.Rows[index].Cells[6].Value = item.CIGARETTDENAME;//香烟名称
                    this.task_data.Rows[index].Cells[7].Value = item.LINENUM;//分拣线
                    this.task_data.Rows[index].Cells[8].Value = item.POKENUM;//抓烟数量
                    this.task_data.Rows[index].Cells[9].Value = item.STATUS;//状态位
                    this.task_data.Rows[index].Cells[10].Value = item.GRIDNUM;//特异性烟标志位
                    this.task_data.Rows[index].Cells[11].Value = item.Machineseq;//物理通道号
                    this.task_data.Rows[index].Cells[12].Value = item.PACKAGEMACHINE;//包装机
                    this.task_data.Rows[index].Cells[13].Value = item.Billcode;//订单号
                  
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
                    if (item.Machineseq == 1061 || item.Machineseq == 2061)
                    {
                        if (item.GRIDNUM == 10)
                        {
                            ssStatus = "新增";
                        }
                        else if (item.GRIDNUM == 15)
                        {
                            ssStatus = "完成";
                        }
                        else
                        {
                            ssStatus = "";
                        }
                        this.task_data.Rows[index].Cells[10].Value = ssStatus;//特异性烟标志位
                        if (ssStatus == "完成")
                        {
                            this.task_data.Rows[index].Cells[10].Style = dgvStyle;//特异性烟标志位
                        }
                    }
                    else
                    {
                        this.task_data.Rows[index].Cells[10].Value = "";
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {


                if (string.IsNullOrWhiteSpace(txtFrom.Text) && string.IsNullOrWhiteSpace(txtTo.Text))
                {
                    MessageBox.Show("请输入分拣任务号");
                    return;
                }
                DialogResult MsgBoxResult = MessageBox.Show("确定要更新任务?",//对话框的显示内容 
                                                                "操作提示",//对话框的标题 
                                                                MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                                MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                                MessageBoxDefaultButton.Button2);//定义对话框的按钮式样


                if (MsgBoxResult == DialogResult.Yes)
                { 
                    string from = txtFrom.Text;
                    string to = txtTo.Text;
                    int status = 10;
                    if (string.IsNullOrWhiteSpace(txtTo.Text))//如果只输入第一个任号务 则其修改
                    {
                        to = from;

                    }
                    else if (string.IsNullOrWhiteSpace(txtFrom.Text))//如果只输入第一个任号务 则其修改
                    {
                        from = to;
                    }
                    else if (Convert.ToDecimal(txtFrom.Text) > Convert.ToDecimal(txtTo.Text)) //防止任务号输反
                    {
                        from = to;
                    }
                    if (radioButton1.Checked)//新增
                    {
                        status = 10;
                    }
                    else if (radioButton2.Checked)//已发送
                    {
                        status = 15;

                    }
                    else if (radioButton3.Checked)//完成
                    {
                        status = 20;
                    }

                    //if (status == 20)
                    //{
                    //    UnPokeService.UpdateStroageInout(UnPokeService.GetListByBillCode(decimal.Parse(from), decimal.Parse(to)));
                    //} 
                    UnPokeService.UpdateTask(decimal.Parse(from), decimal.Parse(to), status);

                    WriteLog.GetLog().Write("任务号从：" + from + "任务号到：" + to + "，修改状态为：" + status + "，任务更新完成!");
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
            catch (Exception ex)
            { 
                MessageBox.Show("任务更新失败，详情请看日志：" + ex.Message);
                WriteLog.GetLog().Write("任务更新失败：" + ex.Message);
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
            dgViewFiles.Columns[0].Width = 45;
            dgViewFiles.Columns[0].Frozen = true;
        }
        private void StatusManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteLog.GetLog().Write("状态修改关闭");
        }

        private void StatusManager_SizeChanged(object sender, EventArgs e)
        {
            AutoSizeColumn(task_data);
           
        } 
    }
}
