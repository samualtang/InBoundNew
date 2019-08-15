using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using highSpeed.baseData;
using highSpeed.orderHandle;
using highSpeed.PubFunc;
using highSpeed.statement;
using InBound.Business;
using System.Configuration;


namespace highSpeed
{
    public partial class w_main : Form
    {
        public w_main()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "长株潭烟草物流";
            toolStripStatusLabel2.Text = "当前用户：" + PublicFun.PubStruserempname;
            toolStripStatusLabel3.Text = "登录时间："+System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            toolStripStatusLabel4.Text = "版本日期：" + ConfigurationManager.AppSettings["Version"].ToString();
        }

        private void 数据库设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_database_set w_database_set = new w_database_set();
            w_database_set.ShowDialog();
            if (w_database_set.isCancel)
            {

            }
            else
            {
                return;
            }
        }

        private void 订单信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_cigaretteInfo frm = new w_cigaretteInfo();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        #region 查找是否已经打开
        /// <summary>
        /// 查找是否已经打开
        /// </summary>
        /// <param name="frm"></param>
        /// <returns></returns>
        private bool CheckExist(Form frm)
        {
            bool blResult = false;
            for (int i = 0; i < MdiChildren.Length; i++)
            {
                if (MdiChildren[i].GetType().Name == frm.GetType().Name)
                {
                    Form tmpFrm = MdiChildren[i];
                    if (tmpFrm.Text == frm.Text)
                    {
                        blResult = true;
                        tmpFrm.Activate();
                    }
                    else if (frm.Text == "")
                    {
                        blResult = true;
                        tmpFrm.Activate();
                    }
                    else if (frm.GetType().Name.ToLower() == "w_export_new")
                    {
                        blResult = true;
                        tmpFrm.Activate();
                    }
                }
            }
            return blResult;
        }
        #endregion

        private void 零售户信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            win_customer frm = new win_customer();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 车组信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            win_region frm = new win_region();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 分拣批次管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            win_batch frm = new win_batch();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 分拣线信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            win_sortline frm = new win_sortline();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 分拣通道管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            win_trough frm = new win_trough();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 包装类型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            win_package frm = new win_package();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 品牌条码信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            win_brandrelative frm = new win_brandrelative();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 补货通道信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            win_replenish frm = new win_replenish();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 出口信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            win_exportline frm = new win_exportline();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 订单接收ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            win_order_recieve frm = new win_order_recieve();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 预排程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            win_schedule frm = new win_schedule();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 任务排程ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            w_SortFm frm = new w_SortFm();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 任务导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            win_export frm = new win_export();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 装箱汇总ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SumInfo frm = new SumInfo();
            //win_binningSummary frm = new win_binningSummary();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 拨烟计划ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            win_pokeplan frm = new win_pokeplan();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 分拣情况ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            //win_sorting frm = new win_sorting();
            //if (CheckExist(frm) == true)
            //{
            //    frm.Dispose();
            //    frm = null;
            //    return;
            //}
            //frm.MdiParent = this;
            //frm.WindowState = FormWindowState.Maximized;
            //frm.Show();
             
        }

        private void 今日订单汇总ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           win_orderdata frm = new win_orderdata();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 今日异型烟汇总ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            win_yxyreplenish frm = new win_yxyreplenish();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 品牌尾数维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_cigarette_leftcount frm = new w_cigarette_leftcount();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 右贴标机文本导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            win_export_right frm = new win_export_right();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();

        }

        private void 左贴标机文本导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            win_export_left frm = new win_export_left();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();

        }

        private void 混合道补货计划ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_hunhesort frm = new w_hunhesort();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 异形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            win_yxytrough frm = new win_yxytrough();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 任务报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_reportprint frm = new w_reportprint();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 混合道补货顺序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_hunhereportprint frm = new w_hunhereportprint();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();

        }

        private void 件烟补货顺序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_jianyanreportprint frm = new w_jianyanreportprint();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 分拣进度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SortingProcess frm = new SortingProcess();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 混合分拣通道管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_hunhetrough frm = new w_hunhetrough();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 托盘补货计划ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_tuopanbuhuoplan frm = new w_tuopanbuhuoplan();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();

        }

        private void 混合道尾数维护ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_cigarette_hunheleftcount frm = new w_cigarette_hunheleftcount();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void excel测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcelForm frm = new ExcelForm();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 异型烟订单接收ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            win_un_order_recieve frm = new win_un_order_recieve();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 异型烟排程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_un_schedule_alone frm = new w_un_schedule_alone();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 异型烟批次管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            win_un_batch frm = new win_un_batch();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void yiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_unhunhetrough frm = new w_unhunhetrough();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 通道转移ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            troughmove frm = new troughmove();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();

        }

        private void tsmi_dataSend_Click(object sender, EventArgs e)
        {
            w_senddata frm = new w_senddata();
            if (CheckExist(frm)==true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void tsmi_enablestandby_Click(object sender, EventArgs e)
        {
            w_enableStandby frm = new w_enableStandby();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 排程报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_schedulereport frm = new w_schedulereport();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 混合道补烟顺序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_hunhereportprint frm = new w_hunhereportprint();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 机械手ToolStripMenuItem_Click(object sender, EventArgs e)
        { 
             
        }

        private void 预分拣ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void 合流ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }

        private void 异形烟ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
       
        /// <summary>
        /// 订单接收
        /// </summary>
        static bool isRecieveing = false;
        /// <summary>
        /// 预排程
        /// </summary>
        static bool isScheduleing = false;

        /// <summary>
        /// 排程
        /// </summary>
        static bool isSorting = false;
        /// <summary>
        /// 判断子窗体是否正在执行任务
        /// </summary>
        /// <param name="index">1 订单接收，2 预排程 ，3排程</param>
        public void  GetSonFormState(int  index,bool isOrNot)
        { 
            if (index == 1)//订单接收
            {
                isRecieveing = isOrNot;
            }
            if (index == 2)//预排程
            {
                isScheduleing = isOrNot;
            }
            if (index == 3)//排程
            {
                isSorting = isOrNot;
            } 
        }
        private void w_main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isRecieveing)
            {
                e.Cancel = true; 
                return;
            }
            if (isScheduleing)
            {
                e.Cancel = true; 
                return;
            }
            if (isSorting)
            {
                e.Cancel = true; 
                return;
            }
            else
            {
                DialogResult MsgBoxResult = MessageBox.Show("确定要退出程序?",//对话框的显示内容 
                                                              "操作提示",//对话框的标题 
                                                              MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                              MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                              MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
                if (MsgBoxResult == DialogResult.Yes)
                {
                    System.Environment.Exit(System.Environment.ExitCode);
                    this.Dispose();
                    this.Close();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void 合流任务查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_uniondata frm = new w_uniondata();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 任务状态修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatusManager frm = new StatusManager();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 基础数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 今日订单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            win_order_Union frm = new win_order_Union();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 异型烟预排程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_un_schedule_alone frm = new w_un_schedule_alone();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 异型烟排程ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            w_un_SortFm_alone frm = new w_un_SortFm_alone();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 常规烟预排程ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            w_reschedule_alone frm = new w_reschedule_alone();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 常规烟排程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_SortFm_alone frm = new w_SortFm_alone();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 任务导出异型烟ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_export_CGY frm = new w_export_CGY();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 任务导出异型烟ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            w_export_YXY frm = new w_export_YXY();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 车组重排ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReHihgSpeedForm frm = new ReHihgSpeedForm();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
              
        }

        private void 异型烟手工线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_un_schedule_diy frm = new w_un_schedule_diy();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 任务导出异型烟手工线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_export_DIY frm = new w_export_DIY();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 任务导出批量退货ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            w_export_deletelist frm = new w_export_deletelist();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 订单拆分ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_order_Split frm = new w_order_Split();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 六三六手工线任务排程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_schedule_six frm = new w_schedule_six();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void 包装机数据生成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_PackageMachineFm frm = new w_PackageMachineFm();
            if (CheckExist(frm) == true)
            {
                frm.Dispose();
                frm = null;
                return;
            }
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }
 

    



      
       


       
    }
}
