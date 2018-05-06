using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound;
using OpcRcw.Da;
using Machine;

namespace FollowTask
{
    public partial class fm_Machine : Form
    {
        
        public fm_Machine()
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


        public fm_Machine(string text)//窗体初始化
        {
            InitializeComponent(); 
            this.listViewMchineBelt.DoubleBufferedListView(true); //双缓存 减少闪烁
            listViewMchineBelt.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.Text = text;
            updateListBox(text+"应用程序启动");
            writeLog.Write(text+"应用程序启动");
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


        private void fm_Machine_Load(object sender, EventArgs e)
        {  
            lblFormText.Text = this.Text +"烟柜";
            BtnText(this.Text);
            
           
        }
   

        private void Machine1_Click(object sender, EventArgs e)//机械手单击事件
        {
            Button name = ((Button)sender);//获取当前单击按钮的所有实例
            updateListBox("查看" + Text.Substring(4) +"的"+ name.Text + "机械手");
            //writeLog.Write("查看" + Text.Substring(4) +"的"+ name.Text + "机械手");
            Fm_FollowTaskMachineDetail ftmd = new Fm_FollowTaskMachineDetail(Text+ name.Text);
            ftmd.Show();
             
        }

        /// <summary>
        /// 机械手根据组变更
        /// </summary>
        /// <param name="groupText">组名 </param>
        public void BtnText(string groupText)
        {
            int group = 0;
            if (groupText.Contains("1") || groupText.Contains("2") || groupText.Contains("5") || groupText.Contains("7"))
            {
                group = 1;//1-11
            }
            else
            {
                group = 2;//12-22
            }
            if (group == 1)
            {
                int j = 1;
                for (int i = 1; i < 13; i++)
                {
                    string btnNmae = "Machine" + j;
                    Control control = Controls.Find(btnNmae, true)[0];
                    control.Text = j + "号";
                    j++;
                }
            }
            else if (group == 2)
            {
                int j = 1;
                for (int i = 12; i < 23; i++)
                {
                    string btnNmae = "Machine" + j;
                    Control control = Controls.Find(btnNmae, true)[0];
                    control.Text = i + "号";
                    j++;
                }
            }

        }

        private void Machine1_MouseEnter(object sender, EventArgs e)
        {
            Button btn = ((Button)sender);
            ToolTip p = new ToolTip();
            p.ShowAlways = true;
            p.SetToolTip(btn,btn.Text+ "机械详细信息");
        }

        private void btnBelt_Click(object sender, EventArgs e)//皮带单击事件
        {
            Button name = ((Button)sender);//获取当前单击按钮的所有实例
            updateListBox("查看" + Text.Substring(4) + "的皮带");
        }
    }
}
