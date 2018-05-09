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
    public partial class W_FenJTask : Form
    {
        public W_FenJTask()
        {
            InitializeComponent();
            dgvTask.DoubleBufferedDataGirdView(true);
            this.Text = "分拣";
        }
        AutoSizeFormClass asc = new AutoSizeFormClass();
        decimal groupNo1;//组1
        decimal groupNo2;//组2 
        decimal SortNum;//排序号
        private void W_FenJTask_Load(object sender, EventArgs e)
        {
            asc.controllInitializeSize(this);
            dgvTask.DataSource = FolloTaskService.getFJDataAll(1, 1);
             
            BindSelectCmb();
            DgvBind();
        }
        /// <summary>
        /// 下拉框绑定
        /// </summary>
        void BindSelectCmb()
        {
            cmbSelectC.Items.Add("所有分拣任务");//0
            cmbSelectC.Items.Add("排序号"); //1
            //cmbSelectC.Items.Add("合流任务号");//2
            //cmbSelectC.Items.Add("品牌");//3
            //cmbSelectC.Items.Add("数量");//4
            //cmbSelectC.Items.Add("组号");//5
            //cmbSelectC.Items.Add("机械手"); //6
            cmbSelectC.SelectedIndex = 0;
        }

        /// <summary>
        /// 文本和文本框是否显示 
        /// </summary>
        /// <param name="a">1为隐藏</param>
        void lblVisbleFalseOrTrue(int a)
        {
            if (a == 1)
            {
                lblNo3.Visible = false;
                txtinfo3.Visible = false;
            }
            else
            {
                lblNo3.Visible = true;
                txtinfo3.Visible = true;
            }
        }
        void DgvBind()
        {
            //CIGARETTDECODE = a.CIGARETTDECODE,
            //                CIGARETTDENAME = a.CIGARETTDENAME,
            //                Machineseq = a.Machineseq,
            //                SortNum = a.SortNum,
            //                tNum = a.tNum,
            //                Billcode = a.Billcode,
            //                SortState = a.SortState 
            if (cmbSelectC.SelectedIndex == 0)//设备号
            {
                dgvTask.Columns[0].HeaderCell.Value = "香烟编号";
                dgvTask.Columns[1].HeaderCell.Value = "香烟名称";
                dgvTask.Columns[2].HeaderCell.Value = "设备号";
                dgvTask.Columns[3].HeaderCell.Value = "排序号";
                dgvTask.Columns[4].HeaderCell.Value = "抓取数量";
                dgvTask.Columns[5].HeaderCell.Value = "排序状态";
                dgvTask.Columns[6].HeaderCell.Value = "抓烟状态";
                dgvTask.Columns[7].HeaderCell.Value = "抓烟状态";
            }
            else if (cmbSelectC.SelectedIndex == 1) 
            {
                dgvTask.Columns[0].HeaderCell.Value = "香烟编号";
                dgvTask.Columns[1].HeaderCell.Value = "香烟名称";
                dgvTask.Columns[2].HeaderCell.Value = "设备号";
                dgvTask.Columns[3].HeaderCell.Value = "排序号";
                dgvTask.Columns[4].HeaderCell.Value = "抓取数量";
                dgvTask.Columns[5].HeaderCell.Value = "排序状态";
            }
        }
        private void cmbSelectC_SelectedIndexChanged(object sender, EventArgs e)
        { 
            if (cmbSelectC.SelectedIndex ==0)
            {
                lblNo1.Text = "组号:";
                lblNo2.Text = "组号:";
                lblVisbleFalseOrTrue(1);
            }
            if (cmbSelectC.SelectedIndex == 1)
            {
                lblNo1.Text = "组号:";
                lblNo2.Text = "组号:";
                lblNo3.Text = "序号:";
                lblVisbleFalseOrTrue(0);
            }
            if (cmbSelectC.SelectedIndex == 2)
            {
                lblNo1.Text = " ";
                lblVisbleFalseOrTrue(0);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            switch (cmbSelectC.SelectedItem.ToString())
            {
                case "所有分拣任务":
                    if (!string.IsNullOrWhiteSpace(txtInfo.Text) && !string.IsNullOrWhiteSpace(txtinfo2.Text))
                    {

                        groupNo1 = Convert.ToDecimal(txtInfo.Text.Replace(" ", ""));
                        groupNo2 = Convert.ToDecimal(txtinfo2.Text.Replace(" ", ""));
                        dgvTask.DataSource = FolloTaskService.getFJDataAll(groupNo1, groupNo2).Select(a => new 
                        {
                            CIGARETTDECODE = a.CIGARETTDECODE,
                            CIGARETTDENAME = a.CIGARETTDENAME,
                            Machineseq = a.Machineseq,
                            SortNum = a.SortNum,
                            tNum = a.tNum,
                            Billcode = a.Billcode,
                            SortState = a.SortState
                        }).ToList();
                        DgvBind();
                    }
                    else
                    {
                        MessageBox.Show("请输入组号");
                        return;
                    }
                    break;
                case "排序号":
                    if (!string.IsNullOrWhiteSpace(txtInfo.Text) && !string.IsNullOrWhiteSpace(txtinfo2.Text) && !string.IsNullOrWhiteSpace(txtinfo3.Text))
                    {
                        groupNo1 = Convert.ToDecimal(txtInfo.Text.Replace(" ", ""));
                        groupNo2 = Convert.ToDecimal(txtinfo2.Text.Replace(" ", ""));
                        SortNum = Convert.ToDecimal(txtinfo3.Text.Replace(" ", ""));
                        dgvTask.DataSource = FolloTaskService.getFJData(SortNum, groupNo1, groupNo2).Select(a => new
                        {
                            CIGARETTDECODE = a.CIGARETTDECODE,
                            CIGARETTDENAME = a.CIGARETTDENAME,
                            Machineseq = a.Machineseq,
                            SortNum = a.SortNum,
                            tNum = a.tNum,
                            Billcode = a.Billcode,
                            SortState = a.SortState
                        }).ToList(); ;
                        DgvBind();
                    }
                    else
                    {
                        MessageBox.Show("请输入完整");
                        return;
                    }
                    break;

            }
        }

        private void W_FenJTask_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }


    }
}
