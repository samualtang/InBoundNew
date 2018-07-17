using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using InBound;
using OpcRcw.Da;
using FollowTask.Modle;
using System.Threading;
using System.Runtime.InteropServices;
 

namespace FollowTask
{
    public partial class Fm_Mian : Form
    {
        /// <summary>
        /// treeV隐藏标志
        /// </summary>
        bool click = true;
        System.Resources.ResourceManager rm;
        internal const string SERVER_NAME = "OPC.SimaticNET";       // local server name

      //  internal const string GROUP_NAME = "grp1";                  // Group name
        internal const int LOCALE_ID = 0x409;                       // LOCALE FOR ENGLISH.
        AutoSizeFormClass asc = new AutoSizeFormClass();
        /* Global variables */
        IOPCServer pIOPCServer;  //定义opcServer对象
      
      
        public WriteLog writeLog = WriteLog.GetLog();
        /// <summary>
        /// 合流
        /// </summary>
        List<Group> listUnionTaskGroup = new List<Group>();
        /// <summary>
        /// 机械手
        /// </summary>
        List<Group> listMachieTaskGroup = new List<Group>();
        /// <summary>
        /// 预分拣
        /// </summary>
        List<Group> listSortTaskGroup = new List<Group>();
        public delegate void HandleUnion(string text, List<Group> listgroup, bool inonline);//合流委托
        HandleUnion Union;
        private delegate void HandleClosing(bool guan);//窗体关闭委托
        HandleClosing hc;
        public delegate void HandleSorting(string text, List<Group> list, bool isonline);//预分拣委托
        HandleSorting Sorting;

        Group UnionTaskGroup1, UnionTaskGroup2, UnionTaskGroup3, UnionTaskGroup4, UnionMachineTaskGroup, UnionMachineNowTaskGroup,UnionMachineDropGroup ;
        Group SortingTaskGroupA, SortingTaskGroupB;
        public Fm_Mian()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            CheckForIllegalCrossThreadCalls = false;
            treeV.Enabled = false;
            fm_sorting = new Fm_FollowTaskSorting();
            fm_union = new Fm_FollowTaskUnion();

          
            Delge();
            this.StartPosition = FormStartPosition.CenterScreen;
            Thread th = new Thread(Connction);
            th.Start();
        }
     
        void Connction()
        {
            txtMainInfo.Text = "连接服务器中.....";
          
            Type svrComponenttyp;
            Guid iidRequiredInterface = typeof(IOPCItemMgt).GUID;
            svrComponenttyp = Type.GetTypeFromProgID(SERVER_NAME);
            try
            {

                pIOPCServer = (IOPCServer)Activator.CreateInstance(svrComponenttyp);//连接本地服务器
                AddUnionTaskGroup();//合流
                AddSortingTaskGroup();//预分拣
                CheckConnection();
               
            }
            catch (Exception ex)
            {
                txtMainInfo.Text = "错误异常:请检查环境配置！，连接失败!!!";
                writeLog.Write("错误异常：" + ex.Message);
            }
        }
        Fm_FollowTaskSorting fm_sorting;
        Fm_FollowTaskUnion fm_union;
        /// <summary>
        /// 
        /// </summary>
        void  Delge()
        { 
            Union += fm_union.GetMainInfo;
            Sorting += fm_sorting.GetSoringBeltInfo; 
        }
        /// <summary>
        /// SortingShow方法
        /// </summary>
        /// <param name="text">第几组</param>
        void ShowSortingForm(string text)
        {
            Sorting(text, listSortTaskGroup, IsOnLine);
            if (CheckExist(fm_sorting) == true)
            {
                fm_sorting.Show();
                fm_sorting. Location = new Point(0, 0);
                fm_sorting.MdiParent = this;
               // fm_sorting.WindowState = FormWindowState.Maximized;
                return;
            }
            fm_sorting.MdiParent = this;
            fm_sorting.Location = new Point(0, 0);
            //fm_sorting.WindowState = FormWindowState.Maximized;
            fm_sorting.Show();
        }
       
        /// <summary>
        /// UinionShow方法
        /// </summary>
        /// <param name="text">第几根</param>
        void ShowUinionFrom(string text)
        { 
            Union("合流", listUnionTaskGroup, IsOnLine);
            if (CheckExist(fm_union) == true)
            {
                fm_union.Show();
                fm_union.Location = new Point(0, 0);
                fm_union.MdiParent = this;
                //fm_union.WindowState = FormWindowState.Maximized;
                //fm_union = null;
                return;
            }
            fm_union.MdiParent = this;
            fm_union.Location = new Point(0, 0);
            //fm_union.WindowState = FormWindowState.Maximized;
            fm_union.Show();

        }
        #region 添加opc组
        /// <summary>
        /// 合流
        /// </summary>
        void AddUnionTaskGroup()
        {
            UnionTaskGroup1 = new Group(pIOPCServer, 1, "group1", 1, LOCALE_ID);//一号主皮带
            UnionTaskGroup2 = new Group(pIOPCServer, 2, "group2", 1, LOCALE_ID);//二号主皮带
            UnionTaskGroup3 = new Group(pIOPCServer, 3, "group3", 1, LOCALE_ID);//三号主皮带
            UnionTaskGroup4 = new Group(pIOPCServer, 4, "group4", 1, LOCALE_ID);//四号主皮带
            UnionMachineTaskGroup = new Group(pIOPCServer, 5, "group5", 1, LOCALE_ID);//合流机械手任务号抓数
            UnionMachineNowTaskGroup = new Group(pIOPCServer, 6, "group6", 1, LOCALE_ID);//合流机械手当前任务号和抓数
            UnionMachineDropGroup = new Group(pIOPCServer, 9, "group9", 1, LOCALE_ID);//合流机械手任务号 放烟信号

            UnionTaskGroup1.addItem(ItemCollection.GetTaskGroupItem1());
            UnionTaskGroup2.addItem(ItemCollection.GetTaskGroupItem2());
            UnionTaskGroup3.addItem(ItemCollection.GetTaskGroupItem3());
            UnionTaskGroup4.addItem(ItemCollection.GetTaskGroupItem4());
            UnionMachineTaskGroup.addItem(ItemCollection.getUnionTaskItem());
            UnionMachineNowTaskGroup.addItem(ItemCollection.GetUnionMachinNowTaskeItem());
            UnionMachineDropGroup.addItem(ItemCollection.GetMachineGroup());

          //  UnionMachineTaskGroup.callback += OnDataChange;
            //UnionMachineNowTaskGroup.callback += OnDataChange;
          


            listUnionTaskGroup.Add(UnionTaskGroup1);//0
            listUnionTaskGroup.Add(UnionTaskGroup2);//1
            listUnionTaskGroup.Add(UnionTaskGroup3);//2
            listUnionTaskGroup.Add(UnionTaskGroup4);//3
            listUnionTaskGroup.Add(UnionMachineTaskGroup);//4
            listUnionTaskGroup.Add(UnionMachineNowTaskGroup);//5
            listUnionTaskGroup.Add(UnionMachineDropGroup);//6 
          
         
        }
        /// <summary>
        /// 预分拣
        /// </summary>
        void AddSortingTaskGroup()
        {
            SortingTaskGroupA =  new Group(pIOPCServer, 7, "group7", 1, LOCALE_ID);//预分拣组A
            SortingTaskGroupB = new Group(pIOPCServer, 8, "group8", 1, LOCALE_ID);//预分拣组A

            //SortingTaskGroupB.addItem(ItemCollection.GetASortingItem(""));

            listSortTaskGroup.Add(SortingTaskGroupA);//预分拣A线
            listSortTaskGroup.Add(SortingTaskGroupB);//预分拣B线
        }

        #endregion

        public void OnDataChange(int group, int[] clientId, object[] values)
        {
            if (group == 1)//一号主皮带八个机械手
            {
                 
            }
            if (group == 2)//二号主皮带八个机械手
            {

            }
            if (group == 3)//三号主皮带八个机械手
            {

            }
            if (group == 4)//四号主皮带八个机械手
            {

            }
        }
        /// <summary>
        /// 连接标识符
        /// </summary>
        bool IsOnLine = false;
        /// <summary>
        /// 检查是否断线
        /// </summary>
        bool CheckBrek = false;
        /// <summary>
        /// 检查连接
        /// </summary>
        void CheckConnection()
        {
            int flag = UnionTaskGroup1.ReadD(12).CastTo<int>(-1);
            if (flag == -1)
            {
                txtMainInfo.Text = "服务器连接失败!";
                writeLog.Write("服务器连接失败");
                CheckBrek = true;
                treeV.Enabled = true;
                goto breaks;
            }
            else
            {
                txtMainInfo.Text = "服务器连接成功!";
                treeV.Enabled = true;
                IsOnLine = true;//连接成功
            }
        cheack: while (true)//检查连接是否正常
            {
                int flaged = UnionTaskGroup1.ReadD(12).CastTo<int>(-1);
                if (flaged == -1)
                {
                    IsOnLine = false;
                    Union("合流", null, IsOnLine);//重新绑定合流 
                    Sorting("1预分拣", null, IsOnLine);//重新绑定预分拣
                    writeLog.Write("服务器断开连接");
                    txtMainInfo.Text = "服务器断开连接!!";
                    CheckBrek = true;
                    break;
                }

            }
        breaks: while (CheckBrek)//已经断线 检查是否能重新连接
            {
                txtMainInfo.Text = "服务器断开连接!!正在尝试重新连接......";
                int flaged = UnionTaskGroup1.ReadD(12).CastTo<int>(-1);
                if (flaged != -1)
                {
                    IsOnLine = true;
                    Union("合流", listUnionTaskGroup, IsOnLine);//重新绑定合流 
                    Sorting("1预分拣", listSortTaskGroup, IsOnLine);//重新绑定预分拣
                    writeLog.Write("服务器连接正常");
                    txtMainInfo.Text = "服务器连接正常!!";
                    CheckBrek = false;
                    goto cheack;
                }

            }
        }

        #region 图片
        Bitmap btmpathLeft = (Bitmap)Properties.Resources.ResourceManager.GetObject("41");
        Bitmap btmpathRight = (Bitmap)Properties.Resources.ResourceManager.GetObject("71");
        #endregion
       
        private void Fm_Mian_Load(object sender, EventArgs e)
        {
            //InBound.Business.TaskService.UpdateMachineFinished(22540, "84");
        
            btnLeft.Location = new Point(166, Height / 2);
            //BitmapRegion.CreateControlRegion(btnLeft, btmpathLeft);//创建Button图片
        }


        private void treeV_AfterSelect(object sender, TreeViewEventArgs e)
        {

            string nodeselect = treeV.SelectedNode.Name;//获取选择name 
            switch (nodeselect)
            {
                #region 机械手
                //case "MachineGroup1":
                //    ShowMchineForm("机械手,第1组");
                //    txtMainInfo.Clear();
                //    txtMainInfo.Text = "第1组的机械手信息";
                //    break;
                //case "MachineGroup2":
                //    ShowMchineForm("机械手,第2组");
                //    txtMainInfo.Clear();
                //    txtMainInfo.Text = "第2组的机械手信息";
                //    break;
                //case "MachineGroup3":
                //    ShowMchineForm("机械手,第3组");
                //    txtMainInfo.Clear();
                //    txtMainInfo.Text = "第3组的机械手信息";
                //    break;
                //case "MachineGroup4":
                //    ShowMchineForm("机械手,第4组");
                //    txtMainInfo.Clear();
                //    txtMainInfo.Text = "第4组的机械手信息";
                //    break;
                //case "MachineGroup5":
                //    ShowMchineForm("机械手,第5组");
                //    txtMainInfo.Clear();
                //    txtMainInfo.Text = "第5组的机械手信息";
                //    break;
                //case "MachineGroup6":
                //    ShowMchineForm("机械手,第6组");
                //    txtMainInfo.Clear();
                //    txtMainInfo.Text = "第6组的机械手信息";
                //    break;
                //case "MachineGroup7":
                //    ShowMchineForm("机械手,第7组");
                //    txtMainInfo.Clear();
                //    txtMainInfo.Text = "第7组的机械手信息";
                //    break;
                //case "MachineGroup8":
                //    ShowMchineForm("机械手,第8组");
                //    txtMainInfo.Clear();
                //    txtMainInfo.Text = "第8组的机械手信息";
                //    break;
                #endregion
                #region 预分拣
                case "fjBigGroup1":
                    ShowSortingForm("预分拣,第1组");
                    break;
                case "fjBigGroup2":
                    ShowSortingForm("预分拣,第2组");
                    break;
                case "fjBigGroup3":
                    ShowSortingForm("预分拣,第3组");
                    break;
                case "fjBigGroup4":
                    ShowSortingForm("预分拣,第4组");
                    break;
                case "fjBigGroup5":
                    ShowSortingForm("预分拣,第5组");
                    break;
                case "fjBigGroup6":
                    ShowSortingForm("预分拣,第6组");
                    break;
                case "fjBigGroup7":
                    ShowSortingForm("预分拣,第7组");
                    break;
                case "fjBigGroup8":
                    ShowSortingForm("预分拣,第8组");
                    break;
                #endregion
                #region 合流
                case "UinonTask":
                    ShowUinionFrom("合流"); 
                    break;
                //case "UinonBelt2":
                //    ShowUinionFrom("合流,第2根");
                //    break;
                //case "UinonBelt3":
                //    ShowUinionFrom("合流,第3根");
                //    break;
                //case "UinonBelt4":
                //    ShowUinionFrom("合流,第4根");
                //    break;
                #endregion
            }

        }
       
        

        #region 常用方法
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
                        tmpFrm.Show();
                        tmpFrm.Activate();
                    }
                    else if (frm.Text == "")
                    {
                        blResult = true;
                        tmpFrm.Show();
                        tmpFrm.Activate();
                    }
                    else if (frm.GetType().Name.ToLower() == "w_export_new")
                    {
                        blResult = true;
                        tmpFrm.Show();
                        tmpFrm.Activate();
                    }
                }
            }
            return blResult;
        }
        /// <summary>
        /// MachineShow方法
        /// </summary> 
        /// <param text="fm">第几组</param>
        void ShowMchineForm(string text)
        {
            fm_Machine fm_machine = new fm_Machine();//机械手
            if (CheckExist(fm_machine) == true)
            {
                fm_machine.Dispose();
                //fm_machine = null;
                //return;
            }
            fm_machine.MdiParent = this;
            fm_machine.WindowState = FormWindowState.Maximized;
            fm_machine.Show();
        }
      // Fm_FollowTaskSorting fm_sorting = new Fm_FollowTaskSorting();//预分拣
       
        #endregion


        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (click)
            {
                treeV.Visible = false;//隐藏
                btnLeft.Location = new Point(-13, this.Size.Height / 2);
                //BitmapRegion.CreateControlRegion(btnLeft, new Bitmap(btmpathRight));
                click = false;
            }
            else
            {
                treeV.Visible = true;
                btnLeft.Dock = DockStyle.None;
                btnLeft.Location = new Point(166, this.Size.Height / 2);
                //BitmapRegion.CreateControlRegion(btnLeft, new Bitmap(btmpathLeft));
                click = true;
            }


        }

        private void Fm_Mian_SizeChanged(object sender, EventArgs e)
        {
            if (!click)
            {
                btnLeft.Location = new Point(-13, this.Size.Height / 2);
            }
            else
            {
                btnLeft.Location = new Point(166, this.Size.Height / 2);
            }
        }

        private void btnLeft_MouseMove(object sender, MouseEventArgs e)
        {
            //if (click)
            //{
            //    TxtBoxMianInFo("隐藏树状菜单");
            //}
            //else
            //{
            //    TxtBoxMianInFo("还原树状菜单");
            //}
        }

        private void btnLeft_MouseLeave(object sender, EventArgs e)
        {
            //TxtBoxMianInFo("信息:");
        }
        private void 退出EToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult MsgBoxResult = MessageBox.Show("确定要退出程序?",//对话框的显示内容 
                                                           "操作提示",//对话框的标题  
                                                           MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                           MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                           MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
            //Console.WriteLine(MsgBoxResult);
            if (MsgBoxResult == DialogResult.Yes)
            {
                System.Environment.Exit(System.Environment.ExitCode);

                this.Dispose();
                this.Close();
            }
            else
            {
                return;
            }

        }
        /// <summary>
        /// 主窗体信息提示
        /// </summary>
        /// <param name="text"></param>
        public void TxtBoxMianInFo(string text)
        {
            txtMainInfo.Text = text;

        }

        private void txtMainInfo_TextChanged(object sender, EventArgs e)
        {

        }

        private void 机械手MToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_MachineTask fm = new w_MachineTask("机械手");
            if (CheckExist(fm) == true)
            {
                fm.Dispose();
                fm = null;
                return;
            }
          //  fm.MdiParent = this;
            fm.Show();
        }

        private void 预分拣YToolStripMenuItem_Click(object sender, EventArgs e)
        {
            W_FenJTask fm = new W_FenJTask();
            if (CheckExist(fm) == true)
            {
                fm.Dispose();
                fm = null;
                return;
            }
           // fm.MdiParent = this;
            fm.Show();
        }

        private void 合流UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_UnionTask fm = new w_UnionTask();
            if (CheckExist(fm) == true)
            {
                fm.Dispose();
                fm = null;
                return;
            }
           // fm.MdiParent = this;
            fm.Show();
        }

        private void 异形烟NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_UnNormal fm = new w_UnNormal();
            if (CheckExist(fm) == true)
            {
                fm.Dispose();
                fm = null;
                return;
            }
        //    fm.MdiParent = this;
            fm.Show();
        }

        private void 查询任务sToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        Restocking rt;
        private void 补货任务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rt = new Restocking();
            rt.ShowDialog();


        }
        public void Disconnect()
        {
            if (pIOPCServer != null)
            {
                Marshal.ReleaseComObject(pIOPCServer);
                pIOPCServer = null;
            }
            for (int i = 0; i < listUnionTaskGroup.Count; i++)
            {
                if (listUnionTaskGroup[i] != null)
                {
                    listUnionTaskGroup[i].Release();
                }
            }
        }

        private void Fm_Mian_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            DialogResult MsgBoxResult = MessageBox.Show("确定要退出程序?",//对话框的显示内容 
                                                             "操作提示",//对话框的标题  
                                                             MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                             MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                            MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
            //Console.WriteLine(MsgBoxResult);
            if (MsgBoxResult == DialogResult.Yes)
            {
                Disconnect();

               
                writeLog.Write("退出程序。。。。。。");
                System.Environment.Exit(System.Environment.ExitCode);
                
                this.Dispose();
                this.Close();
            }
            else
            {
                
                e.Cancel = true;
            }
           
        }

        private void treeV_MouseDown(object sender, MouseEventArgs e)
        {

            if ((sender as TreeView) != null)
            {

                treeV.SelectedNode = treeV.GetNodeAt(e.X, e.Y);
                if (treeV.SelectedNode != null)
                {
                    string nodeselect = treeV.SelectedNode.Name;//获取选择name 

                    switch (nodeselect)
                    {
                        #region 机械手
                        //case "MachineGroup1":
                        //    ShowMchineForm("机械手,第1组");
                        //    txtMainInfo.Clear();
                        //    txtMainInfo.Text = "第1组的机械手信息";
                        //    break;
                        //case "MachineGroup2":
                        //    ShowMchineForm("机械手,第2组");
                        //    txtMainInfo.Clear();
                        //    txtMainInfo.Text = "第2组的机械手信息";
                        //    break;
                        //case "MachineGroup3":
                        //    ShowMchineForm("机械手,第3组");
                        //    txtMainInfo.Clear();
                        //    txtMainInfo.Text = "第3组的机械手信息";
                        //    break;
                        //case "MachineGroup4":
                        //    ShowMchineForm("机械手,第4组");
                        //    txtMainInfo.Clear();
                        //    txtMainInfo.Text = "第4组的机械手信息";
                        //    break;
                        //case "MachineGroup5":
                        //    ShowMchineForm("机械手,第5组");
                        //    txtMainInfo.Clear();
                        //    txtMainInfo.Text = "第5组的机械手信息";
                        //    break;
                        //case "MachineGroup6":
                        //    ShowMchineForm("机械手,第6组");
                        //    txtMainInfo.Clear();
                        //    txtMainInfo.Text = "第6组的机械手信息";
                        //    break;
                        //case "MachineGroup7":
                        //    ShowMchineForm("机械手,第7组");
                        //    txtMainInfo.Clear();
                        //    txtMainInfo.Text = "第7组的机械手信息";
                        //    break;
                        //case "MachineGroup8":
                        //    ShowMchineForm("机械手,第8组");
                        //    txtMainInfo.Clear();
                        //    txtMainInfo.Text = "第8组的机械手信息";
                        //    break;
                        #endregion
                        #region 预分拣
                        case "fjBigGroup1":
                            ShowSortingForm("预分拣,第1组");
                            break;
                        case "fjBigGroup2":
                            ShowSortingForm("预分拣,第2组");
                            break;
                        case "fjBigGroup3":
                            ShowSortingForm("预分拣,第3组");
                            break;
                        case "fjBigGroup4":
                            ShowSortingForm("预分拣,第4组");
                            break;
                        case "fjBigGroup5":
                            ShowSortingForm("预分拣,第5组");
                            break;
                        case "fjBigGroup6":
                            ShowSortingForm("预分拣,第6组");
                            break;
                        case "fjBigGroup7":
                            ShowSortingForm("预分拣,第7组");
                            break;
                        case "fjBigGroup8":
                            ShowSortingForm("预分拣,第8组");
                            break;
                        #endregion
                        #region 合流
                        case "UinonTask":
                            ShowUinionFrom("合流");
                            break;
                        //case "UinonBelt2":
                        //    ShowUinionFrom("合流,第2根");
                        //    break;
                        //case "UinonBelt3":
                        //    ShowUinionFrom("合流,第3根");
                        //    break;
                        //case "UinonBelt4":
                        //    ShowUinionFrom("合流,第4根");
                        //    break;
                        #endregion
                    }
                }
            }
        }
    }
}
