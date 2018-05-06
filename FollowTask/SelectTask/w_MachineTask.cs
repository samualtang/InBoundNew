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
using System.Reflection;

namespace FollowTask
{
    public partial class w_MachineTask : Form
    {
        public w_MachineTask()
        {
            InitializeComponent();
        }
        List<T_PRODUCE_TASK> list =FolloTaskService.getAllTask();
        public w_MachineTask(string HeadText )
        {  
            InitializeComponent();
            this.Text = HeadText;
            dgvTask.DoubleBufferedDataGirdView(true);
        }
        

        decimal TsakNum  ;//任务号
        decimal Machineseq;//设备号
        decimal UnionTsakNum;//合单任务号
        decimal GroupNo1;//烟柜 组号
        decimal GroupNo2;//烟柜 
        decimal SortNum;//排序号
        private void w_FollowTask_Load(object sender, EventArgs e)
        {
            BindSelectCmb(); 
            dgvTask.DataSource = list;
        }
        /// <summary>
        /// 下拉框绑定
        /// </summary>
        void BindSelectCmb()
        {
            cmbSelectC.Items.Add("所有任务");//0
            cmbSelectC.Items.Add("设备号"); //1
            cmbSelectC.Items.Add("合流任务号");//2
            //cmbSelectC.Items.Add("品牌");//3
            //cmbSelectC.Items.Add("数量");//4
            //cmbSelectC.Items.Add("组号");//5
            //cmbSelectC.Items.Add("机械手"); //6
            cmbSelectC.SelectedIndex = 0;
        }
       
        private void btnOk_Click(object sender, EventArgs e)
        {
            
                switch (cmbSelectC.SelectedItem.ToString())
                {
                    case "所有任务": 
                        if (!string.IsNullOrWhiteSpace(txtInfo.Text))
                        {
                            TsakNum = Convert.ToDecimal(txtInfo.Text.Replace(" ",""));
                            dgvTask.DataSource = FolloTaskService.getAllTask(TsakNum); 
                        }
                        else
                        {
                            dgvTask.DataSource = list;
                        }
                        break;
                    case "设备号":
                        if (!string.IsNullOrWhiteSpace(txtInfo.Text))
                        {
                            Machineseq = Convert.ToDecimal(txtInfo.Text.Replace(" ",""));
                            dgvTask.DataSource = FolloTaskService.getAllMachineTask(Machineseq).Select(a => new
                            {
                                CIGARETTECODE = a.CIGARETTDECODE,
                                CIGARETTNAME = a.CIGARETTDENAME,
                                MACHINESEQ = a.Machineseq,
                                UNIONTASKNUM = a.UnionTasknum,
                                MERAGENUM = a.MERAGENUM,
                                BILLCODE = a.Billcode,
                                MACHINESTATE = a.MachineState
                            }).ToList();
                            DgvBind();
                        }
                        break;
                    case "合流任务号":
                        if (!string.IsNullOrWhiteSpace(txtInfo.Text) && !string.IsNullOrWhiteSpace(txtinfo2.Text))
                        {
                            Machineseq = Convert.ToDecimal(txtInfo.Text.Replace(" ", ""));
                            UnionTsakNum = Convert.ToDecimal(txtinfo2.Text.Replace(" ", ""));
                            dgvTask.DataSource = FolloTaskService.getMachineTask(Machineseq, UnionTsakNum).Select(a => new
                            {
                                CIGARETTECODE = a.CIGARETTDECODE,
                                CIGARETTNAME = a.CIGARETTDENAME,
                                MACHINESEQ = a.Machineseq,
                                UNIONTASKNUM = a.UnionTasknum,
                                MERAGENUM = a.MERAGENUM,
                                BILLCODE = a.Billcode,
                                MACHINESTATE = a.MachineState
                            }).ToList();
                            DgvBind();
                        }
                        break;
                    case "品牌":

                        break;

                    case "数量":

                        break;
                    case "组号":

                        break;
                    case "机械手":

                        break;
                }
            
        }
        void DgvBind()
        {
            //CIGARETTDECODE = item2.CIGARETTECODE, 
            //                    CIGARETTDENAME = item2.CIGARETTENAME, 
            //                    Machineseq = item.MACHINESEQ ?? 0, 
            //                    UnionTasknum = item.UNIONTASKNUM ?? 0, 
            //                    tNum = item.MERAGENUM ?? 0, 
            //                    Billcode = item.BILLCODE,
            //                    MachineState = item.MACHINESTATE ?? 0 }; 
            if (cmbSelectC.SelectedItem.ToString() == "设备号")//设备号
            {
                dgvTask.Columns[0].HeaderCell.Value = "香烟编号";
                dgvTask.Columns[1].HeaderCell.Value = "香烟名称";
                dgvTask.Columns[2].HeaderCell.Value = "设备号";
                dgvTask.Columns[3].HeaderCell.Value = "合流任务号";
                dgvTask.Columns[4].HeaderCell.Value = "每次抓烟数量";
                dgvTask.Columns[5].HeaderCell.Value = "订单号";
                dgvTask.Columns[6].HeaderCell.Value = "抓烟状态"; 
            }
            else if (cmbSelectC.SelectedItem.ToString() == "合流任务号")
            {
                dgvTask.Columns[0].HeaderCell.Value = "香烟编号";
                dgvTask.Columns[1].HeaderCell.Value = "香烟名称";
                dgvTask.Columns[2].HeaderCell.Value = "设备号";
                dgvTask.Columns[3].HeaderCell.Value = "合流任务号";
                dgvTask.Columns[4].HeaderCell.Value = "每次抓烟数量";
                dgvTask.Columns[5].HeaderCell.Value = "订单号";
                dgvTask.Columns[6].HeaderCell.Value = "抓烟状态"; 
            }
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
                txtinfo2.Visible = false;
            }
            else
            {
                lblNo2.Visible = true;
                txtinfo2.Visible = true;
            }
        }


        private void cmbSelectC_SelectedIndexChanged(object sender, EventArgs e)
        { 
            if (cmbSelectC.SelectedItem.ToString() == "设备号")
            {
                lblNo1.Text = "设备号:";
                lblVisbleFalseOrTrue(1);
            }
            if (cmbSelectC.SelectedItem.ToString() == "合流任务号")
            {
                lblNo1.Text = "设备号:";
                lblNo2.Text = "合流任务号:";
                lblVisbleFalseOrTrue(0);
            } 
            if (cmbSelectC.SelectedItem.ToString() == "所有任务")
            {
                lblNo1.Text = "任务号:"; 
                lblVisbleFalseOrTrue(1);
            }
        
        }
      
    }

    ///// <summary>
    ///// 双缓冲，解决闪烁问题   扩展方法，使用反射 将此类定义给DataGirdView或ListView所在的窗体类外面即可 
    ///// </summary>
    //public static class DoubleBufferDataGridView
    //{
    //    /// <summary>
    //    /// 双缓冲，解决闪烁问题  
    //    /// </summary>
    //    /// <param name="dgv">DataGridView</param>
    //    /// <param name="flag">默认True</param>
    //    public static void DoubleBufferedDataGirdView(this DataGridView dgv, bool flag)
    //    {
    //        Type dgvType = dgv.GetType();
    //        PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
    //        pi.SetValue(dgv, flag, null);
    //    }

    //    public static void w(this DataGridView dgv)
    //    {
    //        bool flag=true;
    //        Type dgvType = dgv.GetType();
    //        PropertyInfo pi = dgvType.GetProperty("DoubleSS", BindingFlags.Instance | BindingFlags.NonPublic);
    //        pi.SetValue(dgv, flag, null);
    //    }
    //}

}
