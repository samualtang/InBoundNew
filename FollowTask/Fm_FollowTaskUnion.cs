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

using FollowTask.Modle;
 

namespace FollowTask
{
    public partial class Fm_FollowTaskUnion : Form
    {
        public Fm_FollowTaskUnion()
        {
            InitializeComponent();
        }

        internal const string SERVER_NAME = "OPC.SimaticNET";       // local server name

        internal const string GROUP_NAME = "grp1";                  // Group name
        internal const int LOCALE_ID = 0x409;                       // LOCALE FOR ENGLISH.
        AutoSizeFormClass asc = new AutoSizeFormClass();
        /* Global variables */
        IOPCServer pIOPCServer;  //定义opcServer对象
        public WriteLog writeLog = WriteLog.GetLog();
        DeviceStateManager stateManager = new DeviceStateManager();
        Alarms alarms = new Alarms();

        Group machineGroup1, machineGroup2, machineGroup3, machineGroup4, machineGroup5, machineGroup6 , machineGroup7, machineGroup8;//合流一条皮带上的机械手
        List<Group> listgroup = new List<Group>();
        public Fm_FollowTaskUnion(string text)
        {
            InitializeComponent();
            asc.controllInitializeSize(this);
            this.listViewUnion.DoubleBufferedListView(true); 
            this.Text = text;
            updateListBox(text + "主皮带,应用程序启动");
            writeLog.Write(text + "主皮带,应用程序启动");
        } 
        private void Fm_FollowTaskUnion_Load(object sender, EventArgs e)
        {
            lblGourpText.Text = this.Text+"主皮带";
            BtnText(this.Text);
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

        private void Machine1_Click(object sender, EventArgs e)
        {
            Button btn = ((Button)sender);//获取当前单击按钮的所有实例
            Fm_FollowTaskMachineDetail ftmd = new Fm_FollowTaskMachineDetail("第" + Text + "皮带" + btn.Text, listgroup);
            ftmd.Show();
        }
        void GroupAdd()
        {
            listgroup.Add(machineGroup1);
            listgroup.Add(machineGroup2);
            listgroup.Add(machineGroup3);
            listgroup.Add(machineGroup4);
            listgroup.Add(machineGroup5);
            listgroup.Add(machineGroup6);
            listgroup.Add(machineGroup7);
            listgroup.Add(machineGroup8);  
        }

        void Connction()
        {
            Type svrComponenttyp;
            Guid iidRequiredInterface = typeof(IOPCItemMgt).GUID;
            svrComponenttyp = Type.GetTypeFromProgID(SERVER_NAME);
            try
            {


                pIOPCServer = (IOPCServer)Activator.CreateInstance(svrComponenttyp);//连接本地服务器

                machineGroup1  = new Group(pIOPCServer, 1, "group1", 1, LOCALE_ID);//第一根
                machineGroup2 = new Group(pIOPCServer, 2, "group2", 1, LOCALE_ID); 
                machineGroup3 = new Group(pIOPCServer, 3, "group3", 1, LOCALE_ID); 
                machineGroup4 = new Group(pIOPCServer, 4, "group4", 1, LOCALE_ID); 
                machineGroup5 = new Group(pIOPCServer, 5, "group5", 1, LOCALE_ID); 
                machineGroup6 = new Group(pIOPCServer, 6, "group6", 1, LOCALE_ID); 
                machineGroup7 = new Group(pIOPCServer, 7, "group7", 1, LOCALE_ID); 
                machineGroup8 = new Group(pIOPCServer, 8, "group8", 1, LOCALE_ID); 

                machineGroup1.addItem(ItemCollection.MachineItemNo1());
                machineGroup2.addItem(ItemCollection.MachineItemNo2());
                machineGroup3.addItem(ItemCollection.MachineItemNo3());
                machineGroup4.addItem(ItemCollection.MachineItemNo4());
                machineGroup5.addItem(ItemCollection.MachineItemNo5());
                machineGroup6.addItem(ItemCollection.MachineItemNo6());
                machineGroup7.addItem(ItemCollection.MachineItemNo7());
                machineGroup8.addItem(ItemCollection.MachineItemNo8());
                GroupAdd();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 机械手根据组变更名 
        /// </summary>
        /// <param name="groupText">组名 </param>
        public void BtnText(string groupText)
        { 
            if (groupText.Contains("1") )
            {
                int j = 1;
                for (int i = 1; i <= 8; i++)
                {
                    string btnNmae = "Machine" + j;
                    Control control = Controls.Find(btnNmae, true)[0];
                    control.Text = i + "号";
                    j++;
                }
            }
           if (groupText.Contains("2")) // || groupText.Contains("五") || groupText.Contains("七"
            {
                int j = 1;
                for (int i = 9; i <= 16; i++)
                {
                    string btnNmae = "Machine" + j;
                    Control control = Controls.Find(btnNmae, true)[0];
                    control.Text = i + "号";
                    j++;
                }
            }
            if (groupText.Contains("3"))
            {
                int j = 1;
                for (int i = 17; i <= 24; i++)
                {
                    string btnNmae = "Machine" + j;
                    Control control = Controls.Find(btnNmae, true)[0];
                    control.Text = i + "号";
                    j++;
                }
            }
            if (groupText.Contains("4"))
            {
                int j = 1;
                for (int i = 25; i <= 32; i++)
                {
                    string btnNmae = "Machine" + j;
                    Control control = Controls.Find(btnNmae, true)[0];
                    control.Text = i + "号";
                    j++;
                }
            }

        }

        private void btnhuancun1_Click(object sender, EventArgs e)
        {
            Button btn = ((Button)sender);//获取当前单击按钮的所有实例
            string btnNmae = "Machine" + btn.Name.Substring(10); 
            Control control = Controls.Find(btnNmae, true)[0];
            String machineno = System.Text.RegularExpressions.Regex.Replace(control.Text, @"[^0-9]+", "");
            Fm_UinonCache uc = new Fm_UinonCache(Text + control.Text, machineno);//二号 主皮带  四号机械手
            uc.Show();
        }

        private void Machine1_MouseEnter(object sender, EventArgs e)
        {
            Button btn = ((Button)sender);
            ToolTip p = new ToolTip();
            p.ShowAlways = true;
            p.SetToolTip(btn, btn.Text + "机械详细信息");
        }

        private void btnhuancun8_MouseEnter(object sender, EventArgs e)
        {
            Button btn = ((Button)sender);//获取当前单击按钮的所有实例
            ToolTip p = new ToolTip();
            p.ShowAlways = true;
            p.SetToolTip(btn, btn.Text + "缓存详细信息");
        }

        private void Fm_FollowTaskUnion_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        private void listViewUnion_SizeChanged(object sender, EventArgs e)
        {

            int _Count = listViewUnion.Columns.Count;
            int _Width = listViewUnion.Width;
            foreach (ColumnHeader ch in listViewUnion.Columns)
            {
                ch.Width = _Width / _Count - 1;
            }
        }
    }
}
