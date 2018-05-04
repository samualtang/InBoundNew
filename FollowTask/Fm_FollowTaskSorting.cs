using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text; 
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;
using InBound;
using OpcRcw.Da;
using Machine;


namespace FollowTask
{
    public partial class Fm_FollowTaskSorting : Form
    {
        public Fm_FollowTaskSorting()
        {
            InitializeComponent();
        }


        internal const string SERVER_NAME = "OPC.SimaticNET";       // local server name

        internal const string GROUP_NAME = "grp1";                  // Group name
        internal const int LOCALE_ID = 0x409;                       // LOCALE FOR ENGLISH.

        /* Global variables */
        IOPCServer pIOPCServer;  //定义opcServer对象
        public WriteLog writeLog = WriteLog.GetLog();
        DeviceStateManager stateManager = new DeviceStateManager();
        Alarms alarms = new Alarms();
      //  Fm_Mian fm = new Fm_Mian();
    
        public Fm_FollowTaskSorting(string text)
        { 
            InitializeComponent(); 
            this.Text = text;
            updateListBox(text + "应用程序启动");
            writeLog.Write(text + "应用程序启动");
           
        }
        #region listBox显示
        public void writeListBox(string info)
        {
            String time = DateTime.Now.ToLongTimeString();
            this.list_data.Items.Add(time + "    " + info);
        }

        private delegate void HandleDelegate(string strshow);

        public void updateListBox(string info)
        {
            String time = DateTime.Now.ToLongTimeString();

            if (this.list_data.InvokeRequired)
            {

                this.list_data.Invoke(new HandleDelegate(updateListBox), info);
            }
            else
            {
                this.list_data.Items.Insert(0, time + "    " + info);

            }
        }
        #endregion

        private void Fm_FollowTaskSorting_Load(object sender, EventArgs e)
        {  
            if (Text.Contains("1")||Text.Contains("3")||Text.Contains("5")||Text.Contains("7"))//根据组号分别显示不同组 A组和B组
            {
                GroupBoxA.Visible = true;
                GroupBoxA.Location = new Point(15, 54);
                btnRefreshA.Location = new Point(804, 36);
                GroupBoxB.Visible = false;
                btnRefreshB.Visible = false;
            }
            else
            {
                GroupBoxA.Visible = false; 
                btnRefreshA.Visible = false;
                GroupBoxB.Visible = true; 
            }

            lblSortText.Text = Text; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = ((Button)sender);//获取当前单击按钮的所有实例
            Fm_SortDetails fs = new Fm_SortDetails(btn.Text); 
            fs.Show();
        }

        private void Fm_FollowTaskSorting_MouseMove(object sender, MouseEventArgs e)
        {
            //fm.TxtBoxMianInFo("摇摆前皮带订单详情");
        }
        //鼠标停留按钮Tips
        private void btnA011_MouseEnter(object sender, EventArgs e)
        {
            Button btn = ((Button)sender);//获取当前单击按钮的所有实例
            ToolTip p = new ToolTip();
            p.ShowAlways = true;
            p.SetToolTip(btn, "摇摆前订单在皮带上的详细信息");
        }

        private void btnRefreshB_Click(object sender, EventArgs e)
        {
            MessageBox.Show("这是B区的刷新");
        }

        private void btnRefreshA_Click(object sender, EventArgs e)
        {
            MessageBox.Show("这是A区的刷新");
        }


    }
}
