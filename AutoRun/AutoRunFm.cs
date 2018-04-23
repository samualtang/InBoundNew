using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using InBound.Business;
using InBound.Model;
using InBound;
using OpcRcw.Da;

namespace AutoRun
{
    public partial class AutoRunFm : Form
    {

       public static int scanTime = 0;
        public AutoRunFm()
        {
            InitializeComponent();
        }
        List<TaskDetail> detail = new List<TaskDetail>();
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            scanTime = int.Parse(tbScanTime.Text);
            Thread thread = new Thread(new ThreadStart(startAutoInBound));
            thread.IsBackground = true;
            thread.Start();
        }
        delegate void HandleDelegate(string msg);
        public void updateListBox(string info)
        {
            String time = DateTime.Now.ToLongTimeString();
            if (this.lblog.InvokeRequired)
            {
                this.lblog.Invoke(new HandleDelegate(updateListBox), info);
            }
            else
            {
                this.lblog.Items.Insert(0, time + "    " + info);

            }
        }

        bool isRun = true;
        public void startAutoInBound()
        {

           
            // 发送堆垛机命令
            while (isRun)
            {
                            
                ScanDataTable();
                Thread.Sleep(scanTime * 1000);
            }

        }
        bool isRunBuHuo = true;
        IOPCServer pIOPCServer;  //定义opcServer对象
        PlcGroup FJCount;
        internal const string SERVER_NAME = "OPC.SimaticNET";
        internal const int LOCALE_ID = 0x409;    
        public void startAutoBuHuo()
        {

            //Type svrComponenttyp;
            //Guid iidRequiredInterface = typeof(IOPCItemMgt).GUID;
            //svrComponenttyp = Type.GetTypeFromProgID(SERVER_NAME);
            //pIOPCServer = (IOPCServer)Activator.CreateInstance(svrComponenttyp);
            //FJCount = new PlcGroup(pIOPCServer, 1, "group", 1, LOCALE_ID);//组号由这里定义
            //FJCount.addItem(PlcItemCollection.GetTaskCountItem());

            
            while (isRunBuHuo)
            {
                InBoundService.PreUpdateInOut(isSanpan,null);

                Thread.Sleep(scanTime * 1000);
            }

        }

        bool isRunUnnormalBuHuo = true;
        public void startAutoUnNormalBuHuo()
        {



            while (isRunUnnormalBuHuo)
            {


                UnPokeService.PreUpdateInOut(isSanpan);

                Thread.Sleep(scanTime * 1000);
            }

        }
        WriteLog log = new WriteLog();
        public void ScanDataTable()
        {
            try
            {
                InfFeedBackService.AutoWriteFinishTask();
                updateListBox("start.........");
            }
            catch(Exception ex)
            {
                if(ex!=null && ex.Message!=null)
                log.Write(DateTime.Now + ":" + ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {


            //int t = 11;
            //string s = Convert.ToString(t, 2);

            isRun = false;
        }
        bool isSanpan = false;
        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            scanTime = int.Parse(tbScanTime.Text);
            if (cbsanpan.Checked)
            {
                isSanpan = true;
            }
            Thread thread = new Thread(new ThreadStart(startAutoBuHuo));
            thread.IsBackground = true;
            thread.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            scanTime = int.Parse(tbScanTime.Text);
            Thread thread = new Thread(new ThreadStart(startAutoUnNormalBuHuo));
            thread.IsBackground = true;
            thread.Start();
        }

        private void AutoRunFm_FormClosed(object sender, FormClosedEventArgs e)
        {
            isRunUnnormalBuHuo = false;
            isRun = false;
            isRunBuHuo = false;
        }

        private void AutoRunFm_MinimumSizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal && this.Visible == true)
            {
                this.notifyicon.Visible = true;//在通知区显示Form的Icon
                this.WindowState = FormWindowState.Minimized;
                this.Visible = false;
                this.ShowInTaskbar = false;
            }
        }

        private void AutoRunFm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !isClose)
            {
                //取消"关闭窗口"事件
                e.Cancel = true;
                //使关闭时窗口向右下角缩小的效果
                this.WindowState = FormWindowState.Minimized;
                this.notifyicon.Visible = true;
                this.Hide();
                return;
            }
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.Visible)
            {
                this.WindowState = FormWindowState.Minimized;
                this.notifyicon.Visible = true;
                this.Hide();
            }
            else
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }
        bool isClose = false;
        private void menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "menuExit")
            {
                isClose = true;
                this.notifyicon.ShowBalloonTip(30, "注意", "wms后台服务退出", ToolTipIcon.Info);
                this.Close();
            }
            else if (e.ClickedItem.Name == "openMainForm")
            {
                if (!this.Visible)
                {
                    this.Visible = true;
                    this.WindowState = FormWindowState.Normal;
                    this.Activate();
                }
              
            }
        }

    }
}
