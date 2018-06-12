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
using InBound.Business;
using InBound.Model;
using System.Threading;
 

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
        /// <summary>
        /// 主皮带
        /// </summary>
        static int mainbelt ;
        /// <summary>
        /// 组号
        /// </summary>
        static int groupno;
         
        
        Group machineGroup1, machineGroup2, machineGroup3, machineGroup4, machineGroup5, machineGroup6 , machineGroup7, machineGroup8;//合流一条皮带上的机械手

        Group UnionTaskGroup1, UnionTaskGroup2, UnionTaskGroup3, UnionTaskGroup4;//合流四条皮带
        List<Group> listgroup = new List<Group>();
        public Fm_FollowTaskUnion(string text)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            asc.controllInitializeSize(this);
            this.listViewUnion.DoubleBufferedListView(true); 
            this.Text = text;
            mainbelt =Convert.ToInt32( System.Text.RegularExpressions.Regex.Replace(text, @"[^0-9]+", ""));//获取主皮带
            updateListBox(text + "主皮带,应用程序启动");
            writeLog.Write(text + "主皮带,应用程序启动");
        }
 
        private delegate void StartBind();
        private void Fm_FollowTaskUnion_Load(object sender, EventArgs e)
        {
            lblGourpText.Text = this.Text+"主皮带";
            Invoke(new  StartBind(BindLabelName));
            Invoke(new StartBind(BtnText)); 
        } 
       
        #region listBox显示
       

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
        /// <summary>
        /// 当前机械手之后的烟
        /// </summary>
        List<UnionTaskInfo> listafter = new List<UnionTaskInfo>();

        /// <summary>
        /// 当前机械手之前的烟
        /// </summary>
        List<UnionTaskInfo> listbefore = new List<UnionTaskInfo>();
        private void Machine1_Click(object sender, EventArgs e)
        {
            PictureBox btn = ((PictureBox)sender);//获取当前单击按钮的所有实例
            //MessageBox.Show(btn.Name);

            Fm_FollowTaskMachineDetail ftmd = new Fm_FollowTaskMachineDetail("第" + Text + "皮带" + btn.Name, listgroup);
            ftmd.Show();
        }

     
        void Connction()
        {
            Type svrComponenttyp;
            Guid iidRequiredInterface = typeof(IOPCItemMgt).GUID;
            svrComponenttyp = Type.GetTypeFromProgID(SERVER_NAME);
            try
            {
 
                pIOPCServer = (IOPCServer)Activator.CreateInstance(svrComponenttyp);//连接本地服务器
                 
                UnionTaskGroup1 = new Group(pIOPCServer, 1, "group1", 1, LOCALE_ID);
                UnionTaskGroup2 = new Group(pIOPCServer, 2, "group2", 1, LOCALE_ID);
                UnionTaskGroup3 = new Group(pIOPCServer, 3, "group3", 1, LOCALE_ID);
                UnionTaskGroup4 = new Group(pIOPCServer, 4, "group4", 1, LOCALE_ID);

                UnionTaskGroup1.addItem(ItemCollection.GetTaskGroupItem1());
                UnionTaskGroup2.addItem(ItemCollection.GetTaskGroupItem2());
                UnionTaskGroup3.addItem(ItemCollection.GetTaskGroupItem3());
                UnionTaskGroup4.addItem(ItemCollection.GetTaskGroupItem4());
                
                GroupAdd();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        void CheckConnection()
        {
            int flag = UnionTaskGroup1.Read(12).CastTo<int>(-1);
            if (flag == -1)
            {
                updateListBox("服务器连接失败");
            }

        }
        /// <summary>
        /// 获取组号
        /// </summary>
        /// <param name="machineNo">机械手号</param>
        /// <returns></returns>4   
        int GetGroupNo(int machineNo)
        {
            if (machineNo >= 8)
            {
                groupno = machineNo % 8;// Convert.ToDecimal(Math.IEEERemainder(machineNo, 8));//获得组号
            }
            else
            {
                groupno = machineNo;
            }
            if (groupno == 0)
            {
                groupno = 8;
            }
            return groupno;
        }

        void GroupAdd()
        {
            listgroup.Add(UnionTaskGroup1);
            listgroup.Add(UnionTaskGroup2);
            listgroup.Add(UnionTaskGroup3);
            listgroup.Add(UnionTaskGroup4);
       
        }
        #region  暂时无用
        ///// <summary>
       ///// 获取当前主皮带机械手号
       ///// </summary>
       ///// <returns></returns>
       // decimal[] GetMachineNos()
       // {
       //     machinenos[0] = mainbelt * 8 - 7;
       //     machinenos[1] = mainbelt * 8 - 6;
       //     machinenos[2] = mainbelt * 8 - 5;
       //     machinenos[3] = mainbelt * 8 - 4;
       //     machinenos[4] = mainbelt * 8 - 3;
       //     machinenos[5] = mainbelt * 8 - 2;
       //     machinenos[6] = mainbelt * 8 - 1;
       //     machinenos[7] = mainbelt * 8 - 0;
       //     return machinenos; 
       // }
       // /// <summary>
       // /// 获取当前皮带组号
       // /// </summary>
       // /// <returns></returns>
       // decimal[] GetGroupNos()
       // {
       //     for (int i = 0; i < machinenos.Length; i++)
       //     {
       //        groupnos[i] =   GetGroupNo(machinenos[i]);
       //     }
       //     return groupnos; 
        // }
        #endregion

        /// <summary>
        /// 机械手根据组变更名 
        /// </summary>
        void BindLabelName()
        { 
            if (Text.Contains("1"))
            {
                int j = 1;
                for (int i = 1; i <= 8; i++)
                {

                    string labelName = "label" + j;
                    Control control2 = Controls.Find(labelName, true)[0];
                    control2.Text = i + ""; 
                    j++;
                }
                //th2.Abort();
            }
            if (Text.Contains("2")) // || groupText.Contains("五") || groupText.Contains("七"
            {
                int j = 1;
                for (int i = 9; i <= 16; i++)
                {
                    string labelName = "label" + j;
                    Control control2 = Controls.Find(labelName, true)[0];
                    control2.Text = i + ""; 
                    j++;
                }
                //th2.Abort();
            }
            if (Text.Contains("3"))
            {
                int j = 1;
                for (int i = 17; i <= 24; i++)
                {
                    string labelName = "label" + j;
                    Control control2 = Controls.Find(labelName, true)[0];
                    control2.Text = i + ""; 
                    j++;
                }
                //th2.Abort();
            }
            if (Text.Contains("4"))
            {
                int j = 1;
                for (int i = 25; i <= 32; i++)
                {
                    string labelName = "label" + j;
                    Control control2 = Controls.Find(labelName, true)[0];
                    control2.Text = i + ""; 
                    j++;
                }
                //th2.Abort();
            }
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        public void BtnText()
        {
            if (Text.Contains("1"))
            {
                int j = 1;
                for (int i = 1; i <= 8; i++)
                { 
                    string labelName = "label" + j;
                    Control control2 = Controls.Find(labelName, true)[0];
                    //control2.Text = i + "";

                    //string btnNmae = "Machine" + j; 
                    //Control control = Controls.Find(btnNmae, true)[0]; 
                    //control.Text = i + "号";
                    bindDate(control2);
                    j++;
                }
               // th.Abort();
            }
            if (Text.Contains("2")) // || groupText.Contains("五") || groupText.Contains("七"
            {
                int j = 1;
                for (int i = 9; i <= 16; i++)
                {
                    string labelName = "label" + j;
                    Control control2 = Controls.Find(labelName, true)[0];
                    //control2.Text = i + "";

                    //string btnNmae = "Machine" + j;
                    //Control control = Controls.Find(btnNmae, true)[0];
                    //control.Text = i + "号";
                    bindDate(control2);
                    j++;
                }
               //th.Abort();
            }
           if (Text.Contains("3"))
            {
                int j = 1;
                for (int i = 17; i <= 24; i++)
                {
                    string labelName = "label" + j;
                    Control control2 = Controls.Find(labelName, true)[0];
                    //control2.Text = i + "";

                    //string btnNmae = "Machine" + j;
                    //Control control = Controls.Find(btnNmae, true)[0];
                    //control.Text = i + "号";
                    bindDate(control2);
                    j++;
                }
               //th.Abort();
            }
            if (Text.Contains("4"))
            {
                int j = 1;
                for (int i = 25; i <= 32; i++)
                {
                    string labelName = "label" + j;
                    Control control2 = Controls.Find(labelName, true)[0];
                    //control2.Text = i + "";

                    //string btnNmae = "Machine" + j;
                    //Control control = Controls.Find(btnNmae, true)[0];
                    //control.Text = i + "号";
                    bindDate(control2);
                    j++;
                }
              // th.Abort();
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
        #region
        ToolTip p = new ToolTip();
        private void Machine1_MouseEnter(object sender, EventArgs e)
        {
            //Button btn = ((Button)sender);
            //int btnmachineno = Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(btn.Text, @"[^0-9]+", "")); //当前选定机械手号
            //int groupno = GetGroupNo(btnmachineno);//获取组号
            //listafter = UnionTaskInfoService.GetUnionTaskInfoAfter(mainbelt, groupno, 42588, 1);
            ////ToolTip p = new ToolTip();
            //p.ShowAlways = true;
            //foreach (var item in listafter)
            //{   
            //    p.SetToolTip(btn, item.CIGARETTDECODE + " " + item.CIGARETTDENAME + " " + item.MainBelt + " " + item.SortNum+ " "   + item.qty +" ");
            //}
            //Thread.Sleep(100);
        }
       // static int index  =  -1;
        //static string nowstr;
       // static string laststr;
        //static int now;
        //static int last;

        List<UnionTaskInfo> listUnion = new List<UnionTaskInfo>();
        /// <summary>
        /// 数据获取
        /// </summary>
        /// <param name="control"></param>
        void bindDate(Control control)
        {
            try
            {
                Label btn = ((Label)control);
                // Random rd = new Random();

                int btnmachineno = Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(btn.Text, @"[^0-9]+", "")); //当前选定机械手号
                int groupno = GetGroupNo(btnmachineno);//获取组号
                listafter = UnionTaskInfoService.GetUnionTaskInfoAfter(mainbelt, groupno, 42627, 10);//机械手之前
                listbefore = UnionTaskInfoService.GetUnionTaskInfoBefore(mainbelt, groupno, 42627, 10);//机械手之后

                var union = listafter.ToList();//两个list合并 //.Union(listbefore)


                for (int i = 0; i < union.Count; i++)
                {
                    ListViewItem lv = new ListViewItem();
                    var mod = union[i];
                    lv.SubItems[0].Text = btn.Text + "机械手";
                    lv.SubItems.Add(mod.SortNum.ToString());
                    lv.SubItems.Add(mod.MainBelt.ToString());
                    lv.SubItems.Add(mod.CIGARETTDECODE.ToString());
                    lv.SubItems.Add(mod.CIGARETTDENAME.ToString());
                    lv.SubItems.Add("");
                    listViewUnion.Items.Add(lv);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("错误信息:" + ex.Message);
            }
        }
        
        
      
      


        private void btnhuancun8_MouseEnter(object sender, EventArgs e)
        {
            Button btn = ((Button)sender);//获取当前单击按钮的所有实例
       
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
        #endregion

        private void listViewUnion_SelectedIndexChanged(object sender, EventArgs e)
        { 
            if (listViewUnion.SelectedIndices.Count > 0)
            {
                ClaerColor();
                string type = listViewUnion.SelectedItems[0].SubItems[0].Text;
             
                for (int i = 0; i < listViewUnion.Items.Count; i++)
                {
                    ListViewItem item = listViewUnion.Items[i];
                    for (int j = 0; j < item.SubItems.Count; j++)
                    {
                        if (type == item.SubItems[j].Text)
                        { 
                            item.ForeColor = Color.Red; 
                        }
                    }
                } 
            }
             
        }
        /// <summary>
        /// 清除listview颜色
        /// </summary>
        void ClaerColor()
        {
            for (int i = 0; i < listViewUnion.Items.Count; i++)
            {
                ListViewItem item = listViewUnion.Items[i];
                for (int j = 0; j < item.SubItems.Count; j++)
                { 
                    item.ForeColor = Color.Black ; 
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
