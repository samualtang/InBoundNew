using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound.Business;

namespace highSpeed
{
    public partial class w_UnionTask : Form
    {
        public w_UnionTask()
        {
            InitializeComponent();
            this.Text = "合流";
        }

        private void w_UnionTask_Load(object sender, EventArgs e)
        {
            dgvTask.DataSource = FolloTaskService.getUnionDataAll();
        }

        /// <summary>
        /// 文本和文本框是否显示 
        /// </summary>
        /// <param name="a">1为隐藏</param>
        void lblVisbleFalseOrTrue(int a)
        {
            if (a == 1)
            {
                lblNo2.Visible = false;
                txtInfo2.Visible = false;
            }
            else
            {
                lblNo2.Visible = true;
                txtInfo2.Visible = true;
            }
        }

        void BindSelectCmb()
        {
            cmbSelectC.Items.Add("所有合流任务");//0
            cmbSelectC.Items.Add("排序号"); //1
            //cmbSelectC.Items.Add("合流任务号");//2
            //cmbSelectC.Items.Add("品牌");//3
            //cmbSelectC.Items.Add("数量");//4
            //cmbSelectC.Items.Add("组号");//5
            //cmbSelectC.Items.Add("机械手"); //6
            cmbSelectC.SelectedIndex = 0;
        }
    }
}
