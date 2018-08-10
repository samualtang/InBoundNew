using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound.Business;
using InBound;

namespace FollowTask
{
    public partial class w_UnionTask : Form
    {
        public w_UnionTask()
        {
            InitializeComponent();
            dgvTask.DoubleBufferedDataGirdView(true);
            this.Text = "合流";
        }
        decimal sortnum;//排序号
        private void w_UnionTask_Load(object sender, EventArgs e)
        {
            asc.controllInitializeSize(this);
            BindSelectCmb();
            //dgvTask.DataSource = FolloTaskService.getUnionDataAll(); 
        }

        /// <summary>
        /// 文本和文本框是否显示 
        /// </summary>
        /// <param name="a">1为隐藏</param>
        void lblVisbleFalseOrTrue(int a)
        {
            if (a == 1)
            {

                lblNo1.Visible = true;
                txtinfo1.Visible = true;
                //lblNo2.Visible = false;
                //txtInfo2.Visible = false;
            }
            else if (a == 2)
            {
                lblNo1.Visible = false;
                txtinfo1.Visible = false;
                //lblNo2.Visible = false;
                //txtInfo2.Visible = false;
            }
            else
            {
                //lblNo2.Visible = true;
                //txtInfo2.Visible = true;
            }
        }

        void BindSelectCmb()
        {
            cmbSelectC.Items.Add("所有合流任务");//0
            cmbSelectC.Items.Add("任务号"); //1
            //cmbSelectC.Items.Add("合流任务号");//2
            //cmbSelectC.Items.Add("品牌");//3
            //cmbSelectC.Items.Add("数量");//4
            //cmbSelectC.Items.Add("组号");//5
            //cmbSelectC.Items.Add("机械手"); //6
            cmbSelectC.SelectedIndex = 1;
        }
     
        private void cmbSelectC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSelectC.SelectedIndex == 0)
            { 
                lblVisbleFalseOrTrue(2);
            }
            else if (cmbSelectC.SelectedIndex == 1)
            {
                lblNo1.Text = "任务号:";
                lblVisbleFalseOrTrue(1);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            switch (cmbSelectC.SelectedItem.ToString())
            {
                case "所有合流任务":
                          dgvTask.DataSource =UnionTaskInfoService.GetUnionAllTaskInfo().Select(x => new
                        {
                            CIGARETTECODE = x.CIGARETTDECODE,
                            CIGARETTNAME = x.CIGARETTDENAME,
                            QTY = x.POKENUM,
                            MAINBELT = x.MainBelt,
                            SORTNUM = x.SortNum,
                            IsOnBelt = x.IsOnMainBelt, 
                        }).ToList();//根据索引读取相对应数据   
                          DgvBind();
                    break;
                case "任务号":
                    if (!string.IsNullOrWhiteSpace(txtinfo1.Text))
                    {
                        dgvTask.DataSource = InBound.Business.UnionTaskInfoService.GetUnionTaskInfo(Convert.ToDecimal(txtinfo1.Text)).Select(x => new
                        {
                            CIGARETTECODE = x.CIGARETTDECODE,
                            CIGARETTNAME = x.CIGARETTDENAME,
                            QTY = x.POKENUM,
                            MAINBELT = x.MainBelt,
                            SORTNUM = x.SortNum,
                            IsOnBelt = x.IsOnMainBelt, 
                        }).ToList();//根据索引读取相对应数据   
                        DgvBind();
                    }
                    else
                    {
                        MessageBox.Show("请输入任务号");
                    } 
                    break;
            }
        }
        /// <summary>
        /// DataGridView列头绑定
        /// </summary>
        void DgvBind()
        {
            if (cmbSelectC.SelectedIndex == 0)//设备号
            {
                dgvTask.Columns[0].HeaderCell.Value = "卷烟编码";
                dgvTask.Columns[1].HeaderCell.Value = "卷烟名称";
                //dgvTask.Columns[2].HeaderCell.Value = "订单号";
                dgvTask.Columns[2].HeaderCell.Value = "数量";
                dgvTask.Columns[3].HeaderCell.Value = "主皮带号";
                dgvTask.Columns[4].HeaderCell.Value = "任务号";
                dgvTask.Columns[5].HeaderCell.Value = "是否在皮带上";
                //dgvTask.Columns[6].HeaderCell.Value = "烟柜号";
                //dgvTask.Columns[4].HeaderCell.Value = "每次抓烟数量";
                //dgvTask.Columns[5].HeaderCell.Value = "订单号";
                //dgvTask.Columns[6].HeaderCell.Value = "抓烟状态";
            }
            else if (cmbSelectC.SelectedIndex == 1)
            {
                dgvTask.Columns[0].HeaderCell.Value = "卷烟编码";
                dgvTask.Columns[1].HeaderCell.Value = "卷烟名称";
                //dgvTask.Columns[2].HeaderCell.Value = "订单号";
                dgvTask.Columns[2].HeaderCell.Value = "数量";
                dgvTask.Columns[3].HeaderCell.Value = "主皮带号";
                dgvTask.Columns[4].HeaderCell.Value = "任务号";
                dgvTask.Columns[5].HeaderCell.Value = "是否在皮带上";
                //dgvTask.Columns[4].HeaderCell.Value = "每次抓烟数量";
                //dgvTask.Columns[5].HeaderCell.Value = "订单号";
                //dgvTask.Columns[6].HeaderCell.Value = "抓烟状态";
            }
        }

        AutoSizeFormClass asc = new AutoSizeFormClass();
        private void w_UnionTask_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        private void dgvTask_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                String statusText = "";
                switch (e.Value.ToString())
                {
                    case "1":
                        statusText = "是";
                        break;
                    case "0":
                        statusText = "否";
                        break;

                }
                e.Value = statusText;
            }
        }
    }
}
