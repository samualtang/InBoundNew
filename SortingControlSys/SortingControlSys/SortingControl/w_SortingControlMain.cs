using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SortingControlSys.PubFunc;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using System.Collections;
using System.Configuration;
using OpcRcw.Da;
using OpcRcw.Comn;
using System.Runtime.InteropServices;
using SortingControlSys.Model;
using InBound.Model;
using InBound.Business;
using InBound;


namespace SortingControlSys.SortingControl
{
    public partial class w_SortingControlMain : Form
    {


        /* Constants */
        internal const string SERVER_NAME = "OPC.SimaticNET";       // local server name

        internal const string GROUP_NAME = "grp1";                  // Group name
        internal const int LOCALE_ID = 0x409;                       // LOCALE FOR ENGLISH.

        /* Global variables */
        IOPCServer pIOPCServer;  //定义opcServer对象
        decimal groupNo = 1;
        Decimal sortgroupno1 = 1;//定义组次
        Decimal sortgroupno2 = 2;
        public InBound.WriteLog writeLog = InBound.WriteLog.GetLog();
        //ClearCmd clearACmd, clearBCmd;
        private bool AlineStopFlag, BlineStopFlag = false;
        DeviceStateManager stateManager = new DeviceStateManager();
        public w_SortingControlMain()
        {
            InitializeComponent();
            updateListBox("应用程序启动");
            writeLog.Write(" 应用程序启动");
            groupNo = decimal.Parse(ConfigurationManager.AppSettings["GroupNO"].ToString());
             sortgroupno2 = decimal.Parse(ConfigurationManager.AppSettings["Group2"].ToString());
             this.Text = "长株潭烟草公司分拣系统-组" + sortgroupno2 / 2 + "      Version:" + ConfigurationManager.AppSettings["Version"].ToString();
            try
            {
                sortgroupno1 = decimal.Parse(ConfigurationManager.AppSettings["Group1"].ToString());
                sortgroupno2 = decimal.Parse(ConfigurationManager.AppSettings["Group2"].ToString());
                ItemCollection.OpcPresortServer = ConfigurationManager.AppSettings["OpcPresortServer"].ToString();
                ItemCollection.UnionOpcServer = ConfigurationManager.AppSettings["UnionOpcServer"].ToString();
                MachineItemCollection.OpcMachineServer = ItemCollection.OpcPresortServer;
            }
            catch (Exception e)
            {
                MessageBox.Show("请检查一下数据网络,在重新打开系统");
                this.Close();
            }
            //stateManager.WriteErrWithCheck("1", 3, "111011000");

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            stateManager.AlarmsHandler += (obj) =>
            {
                updateListBox(string.Format("{0}号设备发生故障，故障名称：{1}", obj.DeviceNo, obj.ErrInfo),listError);
            };
            stateManager.OnGetErr += (i,t) =>
            {
                return getErrMsg(i,t);
            };
            tempList = TaskService.initTask1();

            //this.task_data.BeginInvoke(new Action(() => { ; }));
            if (tempList == null)
                tempList = new List<KeyValuePair<int, int>>();
            AutoSizeColumn(task_data);//DataGridView自适应
            initdata();
            Timerinitdata.Start();//10秒刷新
         


        }
        private delegate void SendTaskDelegate();
        private delegate void HandleDelegate1(string info, Label label);
        public void updateLabel(string info, Label label)
        {
            String time = DateTime.Now.ToLongTimeString();
            if (label.InvokeRequired)
            {
                //   this.txtreceive.BeginInvoke(new ShowDelegate(Show), strshow);//这个也可以

                label.Invoke(new HandleDelegate1(updateLabel), new Object[] { info, label });
            }
            else
            {
                label.Text = info;

            }
        }
        private delegate void HandleDelegate2(Boolean visible, Control control);
        public void updateControlVisible(Boolean visible, Control control)
        {
            if (control.InvokeRequired)
            {
                //   this.txtreceive.BeginInvoke(new ShowDelegate(Show), strshow);//这个也可以

                control.Invoke(new HandleDelegate2(updateControlVisible), new Object[] { visible, control });
            }
            else
            {
                control.Visible = visible;

            }
        }
        public void updateControlEnable(Boolean enable, Control control)
        {
            if (control.InvokeRequired)
            {
                //   this.txtreceive.BeginInvoke(new ShowDelegate(Show), strshow);//这个也可以

                control.Invoke(new HandleDelegate2(updateControlEnable), new Object[] { enable, control });
            }
            else
            {
                control.Enabled = enable;

            }
        }
        delegate void AysncFinish(object sender, EventArgs e);
        public void startFenJian()
        {


            updateListBox("正在尝试连接服务器......");

            Connect();
        }
        Group taskgroup1, statusGroup1, FinishStateGroup1, taskgroup2, statusGroup4, FinishStateGroup2;
        Group errGroup1, errGroup2, errGroup3, errGroup4;

        Group ClearCacheGroup,UnionGroup;


        Group taskMachineGroup1, taskMachineGroup2, taskMachineGroup3, taskMachineGroup4, taskMachineGroup5, taskMachineGroup6, taskMachineGroup7, taskMachineGroup8, taskMachineGroup9, taskMachineGroup10;
        Group taskMachineGroup11, taskMachineGroup12, taskMachineGroup13, taskMachineGroup14, taskMachineGroup15, taskMachineGroup16, taskMachineGroup17, taskMachineGroup18, taskMachineGroup19, taskMachineGroup20;
        Group taskMachineGroup21, taskMachineGroup22;
        /// <summary>
        /// 监控标志位
        /// </summary>
        Group SendTaskStatesGroup;
        public void Connect()
        {
            Type svrComponenttyp;
            Guid iidRequiredInterface = typeof(IOPCItemMgt).GUID;
            svrComponenttyp = Type.GetTypeFromProgID(SERVER_NAME);
            try
            {
                // Connect to the local server.
                pIOPCServer = (IOPCServer)Activator.CreateInstance(svrComponenttyp);
                taskgroup1 = new Group(pIOPCServer, 1, "group", 1, LOCALE_ID);//组号由这里定义 //第一组数据
                statusGroup1 = new Group(pIOPCServer, 2, "group1", 1, LOCALE_ID);
                FinishStateGroup1 = new Group(pIOPCServer, 3, "group2", 1, LOCALE_ID);//第一组完成信息
                taskgroup2 = new Group(pIOPCServer, 4, "group3", 1, LOCALE_ID);//第二组数据
                statusGroup4 = new Group(pIOPCServer, 5, "group4", 1, LOCALE_ID);
                UnionGroup = new Group(pIOPCServer, 13, "group13", 1, LOCALE_ID);
                FinishStateGroup2 = new Group(pIOPCServer, 6, "group5", 1, LOCALE_ID);//第二组完成信息
                errGroup1 = new Group(pIOPCServer, 7, "group7", 1, LOCALE_ID);
                errGroup2 = new Group(pIOPCServer, 8, "group8", 1, LOCALE_ID);
                errGroup3 = new Group(pIOPCServer, 9, "group9", 1, LOCALE_ID);
                errGroup4 = new Group(pIOPCServer, 10, "group10", 1, LOCALE_ID);
                ClearCacheGroup = new Group(pIOPCServer, 11, "group11", 1, LOCALE_ID);


                taskMachineGroup1 = new Group(pIOPCServer, 100, "group100", 1, LOCALE_ID);
                taskMachineGroup2 = new Group(pIOPCServer, 101, "group101", 1, LOCALE_ID);
                taskMachineGroup3 = new Group(pIOPCServer, 102, "group102", 1, LOCALE_ID);
                taskMachineGroup4 = new Group(pIOPCServer, 103, "group103", 1, LOCALE_ID);
                taskMachineGroup5 = new Group(pIOPCServer, 104, "group104", 1, LOCALE_ID);
                taskMachineGroup6 = new Group(pIOPCServer, 105, "group105", 1, LOCALE_ID);
                taskMachineGroup7 = new Group(pIOPCServer, 106, "group106", 1, LOCALE_ID);
                taskMachineGroup8 = new Group(pIOPCServer, 107, "group107", 1, LOCALE_ID);
                taskMachineGroup9 = new Group(pIOPCServer, 108, "group108", 1, LOCALE_ID);
                taskMachineGroup10 = new Group(pIOPCServer, 109, "group109", 1, LOCALE_ID);
                taskMachineGroup11 = new Group(pIOPCServer, 110, "group110", 1, LOCALE_ID);
                taskMachineGroup12 = new Group(pIOPCServer, 111, "group111", 1, LOCALE_ID);
                taskMachineGroup13 = new Group(pIOPCServer, 112, "group112", 1, LOCALE_ID);
                taskMachineGroup14 = new Group(pIOPCServer, 113, "group113", 1, LOCALE_ID);
                taskMachineGroup15 = new Group(pIOPCServer, 114, "group114", 1, LOCALE_ID);
                taskMachineGroup16 = new Group(pIOPCServer, 115, "group115", 1, LOCALE_ID);
                taskMachineGroup17 = new Group(pIOPCServer, 116, "group116", 1, LOCALE_ID);
                taskMachineGroup18 = new Group(pIOPCServer, 117, "group117", 1, LOCALE_ID);
                taskMachineGroup19 = new Group(pIOPCServer, 118, "group118", 1, LOCALE_ID);
                taskMachineGroup20 = new Group(pIOPCServer, 119, "group119", 1, LOCALE_ID);
                taskMachineGroup21 = new Group(pIOPCServer, 120, "group120", 1, LOCALE_ID);
                taskMachineGroup22 = new Group(pIOPCServer, 121, "group121", 1, LOCALE_ID);

                taskMachineGroup1.addItem(MachineItemCollection.GetTaskStatusItem1());
                taskMachineGroup2.addItem(MachineItemCollection.GetTaskStatusItem2());
                taskMachineGroup3.addItem(MachineItemCollection.GetTaskStatusItem3());
                taskMachineGroup4.addItem(MachineItemCollection.GetTaskStatusItem4());
                taskMachineGroup5.addItem(MachineItemCollection.GetTaskStatusItem5());
                taskMachineGroup6.addItem(MachineItemCollection.GetTaskStatusItem6());
                taskMachineGroup7.addItem(MachineItemCollection.GetTaskStatusItem7());
                taskMachineGroup8.addItem(MachineItemCollection.GetTaskStatusItem8());
                taskMachineGroup9.addItem(MachineItemCollection.GetTaskStatusItem9());
                taskMachineGroup10.addItem(MachineItemCollection.GetTaskStatusItem10());
                taskMachineGroup11.addItem(MachineItemCollection.GetTaskStatusItem11());
                taskMachineGroup12.addItem(MachineItemCollection.GetTaskStatusItem12());
                taskMachineGroup13.addItem(MachineItemCollection.GetTaskStatusItem13());
                taskMachineGroup14.addItem(MachineItemCollection.GetTaskStatusItem14());
                taskMachineGroup15.addItem(MachineItemCollection.GetTaskStatusItem15());
                taskMachineGroup16.addItem(MachineItemCollection.GetTaskStatusItem16());
                taskMachineGroup17.addItem(MachineItemCollection.GetTaskStatusItem17());
                taskMachineGroup18.addItem(MachineItemCollection.GetTaskStatusItem18());
                taskMachineGroup19.addItem(MachineItemCollection.GetTaskStatusItem19());
                taskMachineGroup20.addItem(MachineItemCollection.GetTaskStatusItem20());
                taskMachineGroup21.addItem(MachineItemCollection.GetTaskStatusItem21());
                taskMachineGroup22.addItem(MachineItemCollection.GetTaskStatusItem22());

                SendTaskStatesGroup =new Group(pIOPCServer, 12, "group12", 1, LOCALE_ID);//监控标志位

                taskgroup1.addItem(ItemCollection.GetGroup1TaskItem());//第一组数据
              
                statusGroup1.addItem(ItemCollection.GetTaskStatusItem1());
                UnionGroup.addItem(ItemCollection.getUnionTaskItem());
                FinishStateGroup1.addItem(ItemCollection.GetFinishTaskStatusItem1());//第一组完成信息
             
                taskgroup2.addItem(ItemCollection.GetGroup2TaskItem());//第二组数据
              

                statusGroup4.addItem(ItemCollection.GetTaskStatusSECItem1());

                FinishStateGroup2.addItem(ItemCollection.GetFinishTaskStatusItem2());//第二组完成信息

                SendTaskStatesGroup.addItem(ItemCollection.GetSendTaskStateItem());//监控标志位
               
                errGroup1.addItem(ItemCollection.GetTaskStatusItem3());
               
                errGroup2.addItem(ItemCollection.GetTaskStatusItem4());
               
                errGroup3.addItem(ItemCollection.GetTaskStatusSECItem3());
             
                ClearCacheGroup.addItem(ItemCollection.GetClearTaskItem());
                // ClearCacheGroup.callback += OnDataChange;
                //errGroup4.addItem(ItemCollection.GetTaskStatusSECItem4());
                //errGroup4.callback += OnDataChange;
                regDataChange();
                checkConnection();
                // sendTask();

            }
            catch (Exception e)
            {
                updateListBox("连接服务器失败:" + e.Message);
                writeLog.Write("连接服务器失败:" + e.Message);
                
            }
        }

        public void regDataChange()
        {
            //taskgroup.callback += OnDataChange;//第一组数据 //被监控标志取代
            //statusGroup1.callback += OnDataChange;
            FinishStateGroup1.callback += OnDataChange;//第一组完成信息
            //statusGroup3.callback += OnDataChange;//第二组数据//被监控标志取代
            //statusGroup4.callback += OnDataChange;
            FinishStateGroup2.callback += OnDataChange;//第二组完成信息
            SendTaskStatesGroup.callback += OnDataChange;//监控第一组和第二组标志位
            errGroup1.callback += OnDataChange;
            errGroup2.callback += OnDataChange;
            errGroup3.callback += OnDataChange;


            taskMachineGroup1.callback += OnDataChange;
            taskMachineGroup2.callback += OnDataChange;
            taskMachineGroup3.callback += OnDataChange;
            taskMachineGroup4.callback += OnDataChange;
            taskMachineGroup5.callback += OnDataChange;
            taskMachineGroup6.callback += OnDataChange;
            taskMachineGroup7.callback += OnDataChange;
            taskMachineGroup8.callback += OnDataChange;
            taskMachineGroup9.callback += OnDataChange;
            taskMachineGroup10.callback += OnDataChange;
            taskMachineGroup11.callback += OnDataChange;
            taskMachineGroup12.callback += OnDataChange;
            taskMachineGroup13.callback += OnDataChange;
            taskMachineGroup14.callback += OnDataChange;
            taskMachineGroup15.callback += OnDataChange;
            taskMachineGroup16.callback += OnDataChange;
            taskMachineGroup17.callback += OnDataChange;
            taskMachineGroup18.callback += OnDataChange;
            taskMachineGroup19.callback += OnDataChange;
            taskMachineGroup20.callback += OnDataChange;
            taskMachineGroup21.callback += OnDataChange;
            taskMachineGroup22.callback += OnDataChange;
          
        }
        public void checkConnection()
        {
            int flag = SendTaskStatesGroup.Read(0).CastTo<int>(-1);
            if (flag == -1)
            {
                updateListBox("连接服务器失败,请检查网络.");
                writeLog.Write("连接服务器失败,请检查网络.");
            }
            else
            {
                //if (taskgroup.Read(26).ToString() != "1")
                //{
                //    taskgroup.Write(2, 26);
                //}
                //if (statusGroup3.Read(26).ToString() != "1")
                //{
                //    statusGroup3.Write(2, 26);
                //}
                if (SendTaskStatesGroup.Read(0).ToString() != "1")//监控标志位第一组 产生跳变
                {
                    decimal tasknum = decimal.Parse(taskgroup1.ReadD(0).ToString());

                    //if (tempList.Count > 0)
                    //{
                    // TaskService.UpdateFJSendStatus(sortgroupno1,  tempList.ElementAt(tempList.Count - 1).Value);//状态改为已发送
                    if (tasknum != 0)
                    {
                        TaskService.UpdateTaskStatus(sortgroupno1, 15, tasknum);//状态改为已发送

                        updateListBox("组" + sortgroupno1 + "---任务:" + tasknum + "已接收");
                        writeLog.Write(sortgroupno1 + "组:" + tasknum + "号任务已接收");
                    }
                    SendTaskStatesGroup.Write(2, 0);
                }
                if (SendTaskStatesGroup.Read(1).ToString() != "1")//监控标志位第二组 
                {
                    decimal tasknum = decimal.Parse(taskgroup1.ReadD(0).ToString());

                    //if (tempList.Count > 0)
                    //{
                    // TaskService.UpdateFJSendStatus(sortgroupno1,  tempList.ElementAt(tempList.Count - 1).Value);//状态改为已发送
                    if (tasknum != 0)
                    {
                        TaskService.UpdateTaskStatus(sortgroupno2, 15, tasknum);//状态改为已发送

                        updateListBox("组" + sortgroupno1 + "---任务:" + tasknum + "已接收");
                        writeLog.Write(sortgroupno1 + "组:" + tasknum + "号任务已接收");
                    }
                    SendTaskStatesGroup.Write(2, 1);
                }
                updateListBox("连接服务器成功......");
                writeLog.Write("连接服务器成功......");
                updateControlEnable(false, button10);
                isInit = true;
            }
          
        }
        Boolean CheckCanSend(int targetPort)
        {
            int value = taskgroup2.Read(targetPort - 1).CastTo<int>(-1);
            writeLog.Write("出口号：" + targetPort+" 值：" + value);
            if (value == 1)
            {
                return true;
            }
            else
                return false;
        }
        List<KeyValuePair<int, int>> tempList = new List<KeyValuePair<int, int>>();
        List<KeyValuePair<int, int>> tempList1 = new List<KeyValuePair<int, int>>();
        public static Object lockFlg = new Object();
        public void removeKey(List<KeyValuePair<int, int>> list, int export)
        {
            int i = 0;
            if (list != null)
            {
                lock (lockFlg)
                {
                    foreach (var item in list)
                    {
                        i++;
                        if (item.Key == export)
                        {

                            if (i != list.Count)
                            {
                                list.Remove(item);
                            }
                            break;
                        }
                    }
                }
            }
        }

        public int getKey(List<KeyValuePair<int, int>> list, int export)
        {
            int i = -1;
            if (list != null)
            {
                //lock (lockFlg)
                //{
                    foreach (var item in list)
                    {
                        if (item.Key == export)
                        {
                            i = item.Value;
                            break;
                        }
                   // }
                }
            }
            return i;
        }
        delegate void delSendTask();
        int mainbeltNum2 = 4;
        /// <summary>
        /// 第二组数据
        /// </summary>
        void sendTask1()
        {
            try
            {
                int flag = SendTaskStatesGroup.Read(1).CastTo<int>(-1);//读任务写入标志
                writeLog.Write(sortgroupno2 + "组写任务前电控标志位：" + flag);
                if (flag == -1)
                {
                    writeLog.Write("与PLC连接异常,请检查网络");
                    updateListBox("与PLC连接异常,请检查网络");
                }
                if (flag == 2)//0：已取走， 1：已写入
                {
                    while (!ProducePokeService.CheckExistPreSendTask(sortgroupno2, 12) && ProducePokeService.CheckExistPreSendTask(sortgroupno2, 10))
                    {
                        decimal taskno, zqqty;
                        decimal sortgroup = sortgroupno2;
                        if (sortgroup == 4)
                        {
                            sortgroup = 3;
                        }
                        else if (sortgroup == 8)
                        {
                            sortgroup = 7;
                        }

                        try
                        {
                            taskno = decimal.Parse(UnionGroup.ReadD((int)((mainbeltNum2 - 1) * 16 + 2 * (sortgroup - 1))).ToString());

                            zqqty = decimal.Parse(UnionGroup.ReadD((int)((mainbeltNum2 - 1) * 16 + 2 * (sortgroup - 1) - 1)).ToString());
                        }
                        catch
                        {
                            taskno = 0;
                            zqqty = 0;
                        }
                        writeLog.Write("plc地址:" + plclist[(int)((mainbeltNum2 - 1) * 16 + 2 * (sortgroup - 1))] + "读取组:" + sortgroupno2 + " 主皮带:" + mainbeltNum2 + " 合流任务号:" + taskno + " 抓取数量:" + zqqty);
                        updateListBox("plc地址:" + plclist[(int)((mainbeltNum2 - 1) * 16 + 2 * (sortgroup - 1))] + "读取组:" + sortgroupno2 + " 主皮带:" + mainbeltNum2 + " 合流任务号:" + taskno + " 抓取数量:" + zqqty);
                        if (!ProducePokeService.CheckExistTaskNo(taskno))
                        {
                            zqqty = 0;
                        }
                        T_PRODUCE_CACHE cache = ProduceCacheService.GetCache(sortgroupno2, mainbeltNum2);
                        decimal currentNum = ProducePokeService.LeftCount(sortgroupno2, mainbeltNum2, taskno, zqqty, cache.CACHESIZE ?? 0);
                        writeLog.Write("当前剩余量:" + currentNum + " 组号:" + sortgroupno2 + " 主皮带:" + mainbeltNum2);
                        updateListBox("当前剩余量:" + currentNum + " 组号:" + sortgroupno2 + " 主皮带:" + mainbeltNum2);
                       
                        if (currentNum >= (cache.DISPATCHENUM??0))
                        {
                            ProducePokeService.UpdatePokeByGroupNo(sortgroupno2, (int)(cache.DISPATCHESIZE ?? 0), mainbeltNum2);
                        }
                        if (mainbeltNum2 - 1 > 0)
                        {
                            mainbeltNum2 -= 1;
                        }
                        else
                        {
                            mainbeltNum2 = 4;
                        }
                        Thread.Sleep(1000);
                    }

                    object[] datas = ProducePokeService.GetSortTask(sortgroupno2);//数据

                    if (int.Parse(datas[0].ToString()) == 0)//已经没有数据可发送了，datas[0]是任务号
                    {
                        updateListBox(sortgroupno2 + "组分拣数据发送完毕");
                        writeLog.Write(sortgroupno2 + "组分拣数据发送完毕");
                        return;
                    }
                    int export = int.Parse(datas[1].ToString());//取虚拟出口号
                    //if (CheckCanSend(export))//和电控交互该出口号是否能用
                    //{

                    taskgroup2.SyncWrite(datas);//写任务
                    string logstr = "";
                    string logcolumnname = "";
                    for (int i = 0; i < datas.Length; i++)
                    {
                        if (i == 0) logcolumnname = "第" + sortgroupno2 + "任务号";
                        else if (i == 1) logcolumnname = "虚拟出口号";
                        else if (i == 2) logcolumnname = "主皮带号";
                        else if (i == 3) logcolumnname = "条烟总数";
                        else if (i == 4) logcolumnname = "烟柜1条数";
                        else if (i == 5) logcolumnname = "起始位置";
                        else if (i == 6) logcolumnname = "烟柜2条数";
                        else if (i == 7) logcolumnname = "起始位置";
                        else if (i == 8) logcolumnname = "烟柜3条数";
                        else if (i == 9) logcolumnname = "起始位置";
                        else if (i == 10) logcolumnname = "烟柜4条数";
                        else if (i == 11) logcolumnname = "起始位置";
                        else if (i == 12) logcolumnname = "烟柜5条数";
                        else if (i == 13) logcolumnname = "起始位置";
                        else if (i == 14) logcolumnname = "烟柜6条数";
                        else if (i == 15) logcolumnname = "起始位置";
                        else if (i == 16) logcolumnname = "烟柜7条数";
                        else if (i == 17) logcolumnname = "起始位置";
                        else if (i == 18) logcolumnname = "烟柜8条数";
                        else if (i == 19) logcolumnname = "起始位置";
                        else if (i == 20) logcolumnname = "烟柜9条数";
                        else if (i == 21) logcolumnname = "起始位置";
                        else if (i == 22) logcolumnname = "烟柜10条数";
                        else if (i == 23) logcolumnname = "起始位置";
                        else if (i == 24) logcolumnname = "烟柜11条数";
                        else if (i == 25) logcolumnname = "起始位置";                      
                        
                        else if (i==47) logcolumnname = "写入标志位";

                        logstr += logcolumnname + ":" + datas[i] + ";";
                        //logstr += i + ":" + datas[i] + ";";
                    }
                    writeLog.Write("组:" + sortgroupno2 + "----" + logstr);
                    updateListBox("组:" + sortgroupno2 + "----" + logstr);
                    removeKey(tempList1, export);
                    tempList1.Add(new KeyValuePair<int, int>(export, int.Parse(datas[0].ToString())));//任务号和出口号对应起来
                    //}
                    //else
                    //{
                    //    Thread.Sleep(1000);//毫秒
                    //    sendTask();
                    //}
                }
            }
            catch (Exception ex)
            {
                writeLog.Write(ex.Message);
                updateListBox(ex.Message);
                Thread.Sleep(10000);
                if (ex.InnerException != null && ex.InnerException.Message != null)
                {
                    writeLog.Write(ex.InnerException.Message);
                    updateListBox(ex.InnerException.Message);
                }
                sendTask1();//异常后重新发送
            }
        }
        int mainbeltNum = 4;
        int maxCacheNum = 160;
        int minCahceNum =50;
        List<string> plclist = ItemCollection.getUnionTaskItem();
        /// <summary>
        /// 第一组数据
        /// </summary>
        void sendTask()
        {
            try
            {
                int flag = SendTaskStatesGroup.Read(0).CastTo<int>(-1);//读任务写入标志 第一组
                writeLog.Write(sortgroupno1 + "组写任务前电控标志位：" + flag);
                if (flag == -1)
                {
                    writeLog.Write("与PLC连接异常,请检查网络");
                    updateListBox("与PLC连接异常,请检查网络");
                }
                if (flag == 2)//0：已取走， 1：已写入
                {

                    while (!ProducePokeService.CheckExistPreSendTask(sortgroupno1, 12) && ProducePokeService.CheckExistPreSendTask(sortgroupno1, 10))
                    {
                        decimal taskno, zqqty;
                        decimal sortgroup = sortgroupno1;
                        if (sortgroup == 3)
                        {
                            sortgroup = 4;
                        }
                        else if (sortgroup == 7)
                        {
                            sortgroup = 8;
                        }
                        try
                        {
                            taskno = decimal.Parse(UnionGroup.ReadD((int)((mainbeltNum - 1) * 16 + 2 * (sortgroup - 1))).ToString());

                            zqqty = decimal.Parse(UnionGroup.ReadD((int)((mainbeltNum - 1) * 16 + 2 * (sortgroup) - 1)).ToString());
                        }
                        catch
                        {
                            taskno = 0;
                            zqqty = 0;
                        }
                        writeLog.Write("plc地址:"+plclist[(int)((mainbeltNum - 1) * 16 + 2 * (sortgroup - 1))]+"读取组:" + sortgroupno1 + " 主皮带:" + mainbeltNum + " 合流任务号:" + taskno + " 抓取数量:" + zqqty);
                        updateListBox("plc地址:" + plclist[(int)((mainbeltNum - 1) * 16 + 2 * (sortgroup - 1))] + "读取组:" + sortgroupno1 + " 主皮带:" + mainbeltNum + " 合流任务号:" + taskno + " 抓取数量:" + zqqty);
                        if (!ProducePokeService.CheckExistTaskNo(taskno))
                        {
                            zqqty = 0;
                        }
                        T_PRODUCE_CACHE cache = ProduceCacheService.GetCache(sortgroupno1, mainbeltNum);
                        decimal currentNum = ProducePokeService.LeftCount(sortgroupno1, mainbeltNum, taskno, zqqty, cache.CACHESIZE??0);
                        writeLog.Write("当前剩余量:" + currentNum + " 组号:" + sortgroupno1 +" 主皮带:"+mainbeltNum);
                        updateListBox("当前剩余量:" + currentNum + " 组号:" + sortgroupno1 + " 主皮带:" + mainbeltNum);
                        if (currentNum>= (cache.DISPATCHENUM??0))
                        {
                            ProducePokeService.UpdatePokeByGroupNo(sortgroupno1, (int)(cache.DISPATCHESIZE ?? 0), mainbeltNum);
                        }
                        if (mainbeltNum - 1 > 0)
                        {
                            mainbeltNum -= 1;
                        }
                        else
                        {
                            mainbeltNum = 4;
                        }
                        Thread.Sleep(1000);
                    }

                    object[] datas = ProducePokeService.GetSortTask(sortgroupno1);//数据

                    if (int.Parse(datas[0].ToString()) == 0)//已经没有数据可发送了，datas[0]是任务号
                    {
                        updateListBox(sortgroupno1 + "组分拣数据发送完毕");
                        writeLog.Write(sortgroupno1 + "组分拣数据发送完毕");
                        return;
                    }
                    int export = int.Parse(datas[1].ToString());//取虚拟出口号
                    //if (CheckCanSend(export))//和电控交互该出口号是否能用
                    //{

                    taskgroup1.SyncWrite(datas);//写任务

                    //String p2 = taskgroup.Read(1).ToString();
                    //String p3 = taskgroup.Read(2).ToString();
                    //if (p2 == datas[1].ToString())
                    //{

                    //}
                    //else
                    //{

                    //}
                    string logstr = "";
                    string logcolumnname = "";
                    for (int i = 0; i < datas.Length; i++)
                    {
                        if (i == 0) logcolumnname = "第" + sortgroupno1 + "任务号";
                        else if (i == 1) logcolumnname = "虚拟出口号";
                        else if (i == 2) logcolumnname = "主皮带号";
                        else if (i == 3) logcolumnname = "条烟总数";
                        else if (i == 4) logcolumnname = "烟柜1条数";
                        else if (i == 5) logcolumnname = "起始位置";
                        else if (i == 6) logcolumnname = "烟柜2条数";
                        else if (i == 7) logcolumnname = "起始位置";
                        else if (i == 8) logcolumnname = "烟柜3条数";
                        else if (i == 9) logcolumnname = "起始位置";
                        else if (i == 10) logcolumnname = "烟柜4条数";
                        else if (i == 11) logcolumnname = "起始位置";
                        else if (i == 12) logcolumnname = "烟柜5条数";
                        else if (i == 13) logcolumnname = "起始位置";
                        else if (i == 14) logcolumnname = "烟柜6条数";
                        else if (i == 15) logcolumnname = "起始位置";
                        else if (i == 16) logcolumnname = "烟柜7条数";
                        else if (i == 17) logcolumnname = "起始位置";
                        else if (i == 18) logcolumnname = "烟柜8条数";
                        else if (i == 19) logcolumnname = "起始位置";
                        else if (i == 20) logcolumnname = "烟柜9条数";
                        else if (i == 21) logcolumnname = "起始位置";
                        else if (i == 22) logcolumnname = "烟柜10条数";
                        else if (i == 23) logcolumnname = "起始位置";
                        else if (i == 24) logcolumnname = "烟柜11条数";
                        else if (i == 25) logcolumnname = "起始位置";

                        else if (i == 47) logcolumnname = "写入标志位";

                        logstr += logcolumnname + ":" + datas[i] + ";";
                       // logstr += i + ":" + datas[i] + ";";
                    }
                    writeLog.Write("组:" + sortgroupno1 + "----" +logstr);
                    updateListBox("组:" + sortgroupno1 + "----" + logstr);
                    removeKey(tempList, export);
                    tempList.Add(new KeyValuePair<int, int>(export, int.Parse(datas[0].ToString())));//任务号和出口号对应起来
                    //}
                    //else
                    //{
                    //    Thread.Sleep(1000);//毫秒
                    //    sendTask();
                    //}
                }
            }
            catch (Exception ex)
            {
                writeLog.Write(ex.Message);
                updateListBox(ex.Message);
                Thread.Sleep(10000);
                if (ex.InnerException != null && ex.InnerException.Message != null)
                {
                    writeLog.Write(ex.InnerException.Message);
                    updateListBox(ex.InnerException.Message);
                }
                sendTask();//异常后重新发送
            }
        }
        public void WriteErr(int type, int len, String temp, decimal GroupNo)
        {
            if (string.IsNullOrEmpty(temp))
                return;
            String deviceNo = "";
            if (type == 1)
            {
                deviceNo = "A" + len;
            }
            else
            {
                deviceNo = "B" + len;
            }
            lock (lockFlag)
            {
                stateManager.WriteErrWithCheck(deviceNo, Convert.ToInt32(GroupNo), temp.Length > 16 ? temp.Substring(0, 15) : temp, 1);
            }

            //for (int i = 0; i < temp.Length; i++)
            //{
            //    if (temp.ElementAt(i) == '1')
            //    {
            //        if (i < 16)
            //        {
            //            String errMsg = getErrMsg(i);
            //            ErrListService.Add(deviceNo, GroupNo, 10, errMsg);
            //        }

            //    }
            //}


        }
        public static Object lockFlag = new Object();
        public void WriteErrG(int type, int len, String temp)
        {
            if (string.IsNullOrEmpty(temp))
                return;
            String deviceNo = Math.Ceiling(((Decimal)len / 10)) + "M" + type;
            decimal GroupNo = 0;
            if ((len / 10) > 11)
                GroupNo = sortgroupno2;
            else
                GroupNo = sortgroupno1;
            lock (lockFlag)
            {
                stateManager.WriteErrWithCheck(deviceNo, Convert.ToInt32(GroupNo), temp.Length > 16 ? temp.Substring(0, 15) : temp, 2);
            }
            //for (int i = 0; i < temp.Length; i++)
            //{
            //    if (temp.ElementAt(i) == '1')
            //    {
            //        if (i < 16)
            //        {
            //            String errMsg = getErrMainbeltList(i);
            //            ErrListService.Add(deviceNo, GroupNo, 10, errMsg);
            //        }

            //    }
            //}


        }

        public void WriteErrM(int len, String temp)
        {
            if (string.IsNullOrEmpty(temp))
                return;
            String deviceNo = Math.Ceiling(((Decimal)len / 10)) + "";
            decimal GroupNo = 0;
            if ((len / 10) > 11)
                GroupNo = sortgroupno2;
            else
                GroupNo = sortgroupno1;
            lock (lockFlag)
            {
                stateManager.WriteErrWithCheck(deviceNo, Convert.ToInt32(GroupNo), temp.Length > 16 ? temp.Substring(0, 15) : temp, 3);
            }
            //for (int i = 0; i < temp.Length; i++)
            //{
            //    if (temp.ElementAt(i) == '1')
            //    {
            //        if (i < 8)
            //        {
            //            String errMsg = getErrMachinesMsg(i);
            //            ErrListService.Add(deviceNo, GroupNo, 10, errMsg);
            //        }

            //    }
            //}


        }
        //String[] errMsgList = { "", "", "", "", "", "编码器故障", "手动选中", "反转", "单台电机故障", "空开故障", "接触器/变频器故障", "急停（SF9）", "立烟", "气缸升超时", "气缸降超时", "运行信号" };
        String[] errMsgList = {  "单台电机故障", "空开故障", "运行故障", "急停（SF9）", "立烟", "气缸升超时", "气缸降超时", "运行信号","", "", "", "气缸降按钮", "气缸升按钮", "编码器故障", "手动选中", "烟条滞后"};
        public string getErrMsg(int len,int type)
        {
            if (type == 1)
            {
                return errMsgList[len];
            }
            else if (type == 2)
            {
               //errMsgMainbeltList= (String[])errMsgMainbeltList.Reverse();
                return errMsgMainbeltList[len];
            }
            else if (type == 3)
            {
                return errMachinesMsgList[len];
            }
            else
            {
                return "";
            }
        }
        //String[] errMsgList1 = { "", "", "", "", "", "", "后气缸升降按钮", "前气缸升降按钮", "电机正常", "空开故障", "接触器/变频器故障", "上翻超时", "下翻超时", "后气缸超时", "前气缸超时", "皮带手动选中" };
        String[] errMsgMainbeltList = { "", "", "", "", "", "", "后气缸升降按钮", "前气缸升降按钮", "电机正常/1", "空开故障", "接触器变频器故障", "上翻超时", "下翻超时", "后气缸超时", "前气缸超时", "皮带手动选中"};
        public string getErrMainbeltList(int len)
        {
            return errMsgMainbeltList[len];
        }
        //String[] errMsgList2 = { "机械手正常/1", "机械手故障", "", "", "", "", "烟柜烟条未准备好", "机械手抓烟超时", "", "", "", "", "", "", "", "" };
        String[] errMachinesMsgList = { "", "", "", "", "", "", "", "",  "机械手正常/1", "机械手故障", "机械手急停", "机械手手体报警", "机械手掉烟", "机械手空开", "烟柜烟条未准备好", "机械手抓烟超时" };
        public string getErrMachinesMsg(int len)
        {
            return errMachinesMsgList[len];
        }
        public void OnDataChange(int group, int[] clientId, object[] values)//plc对应db块字节值发生变化
        {
            if (group == 1)//发送任务组 //被监控标志取代
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    if (clientId[i] == 27)//监控写入标识位
                    {
                        if (values[i] != null && int.Parse(values[i].ToString()) == 2)//2是电控已经接收
                        {
                            while (!isInit)
                            {
                                Thread.Sleep(100);
                            }
                            if (tempList.Count > 0)
                            {

                                TaskService.UpdateStatus(sortgroupno1, 15, tempList.ElementAt(tempList.Count - 1).Value);//状态改为已发送
                                updateListBox("组" + sortgroupno1 + "---任务:" + tempList.ElementAt(tempList.Count - 1).Value + "已接收");
                                writeLog.Write(sortgroupno1 + "组:" + tempList.ElementAt(tempList.Count - 1).Value + "号任务已接收");
                            }

                            sendTask();
                        }
                        break;
                    }
                }
            }
            else if (group == 2)
            {

            }
            else if (group == 3)//监控分拣任务完成信号 组一
            {
                for (int i = 0; i < clientId.Length; i++)//"出口号：" + clientId[i] + ";任务号:" + taskno
                {
                    int tempvalue = int.Parse((values[i].ToString()));
                    if (tempvalue >= 1)//分拣完成
                    {
                        statusGroup1.Write(1, clientId[i] - 1);
                        //if (getKey(tempList, clientId[i]) != -1)
                        //{
                            // int taskno = getKey(tempList, clientId[i]);
                            writeLog.Write("从电控读取" + sortgroupno1 + "组出口号：" + clientId[i] + "；任务号:" + tempvalue);
                            //InBoundService.UpdateInOut(tempvalue, sortgroupno1);
                            TaskService.UpdateFJFinishStatus(sortgroupno1,  tempvalue);//将第一组分拣任务改为完成完成
                            
                            if (tempvalue != 0)
                            {
                                try
                                {
                                    PreSortInfoService.Add((decimal)tempvalue, sortgroupno1);
                                }
                                catch (Exception ex)
                                { }
                                updateListBox(sortgroupno1 + "组:" + tempvalue + "号任务已完成");
                                writeLog.Write(sortgroupno1 + "组:" + tempvalue + "号任务已完成");
                            }

                            removeKey(tempList, clientId[i]);
                           // this.task_data.BeginInvoke(new Action(() => { initdata(); }));//异步调用，刷新分拣页面的分拣进度

                        //}
                    }
                    else
                    {
                        statusGroup1.Write(0, clientId[i] - 1);
                    }
                }
            }
            else if (group == 4) //被监控标志取代 组二
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    if (clientId[i] == 27)//监控写入标识位
                    {
                        if (values[i] != null && int.Parse(values[i].ToString()) == 2)//2是电控已经接收
                        {
                            while (!isInit)
                            {
                                Thread.Sleep(100);
                            }
                            if (tempList1.Count > 0)
                            {

                                TaskService.UpdateStatus(sortgroupno2, 15, tempList1.ElementAt(tempList1.Count - 1).Value);//状态改为已发送
                                updateListBox("组" + sortgroupno2 + "---任务:" + tempList1.ElementAt(tempList1.Count - 1).Value + "已接收");
                                writeLog.Write(sortgroupno2 + "组:" + tempList1.ElementAt(tempList1.Count - 1).Value + "号任务已接收");
                            }
                            sendTask1();
                        }
                        break;
                    }
                }
            }
            else if (group == 5)
            {

            }
            else if (group == 6)
            {
                for (int i = 0; i < clientId.Length; i++)//"出口号：" + clientId[i] + ";任务号:" + taskno
                {
                    int tempvalue = int.Parse((values[i].ToString()));
                    if (tempvalue >= 1)//分拣完成
                    {
                        statusGroup4.Write(1, clientId[i] - 1);
                        //if (getKey(tempList1, clientId[i]) != -1)
                        //{
                            // int taskno = getKey(tempList1, clientId[i]);
                            writeLog.Write("从电控读取" + sortgroupno2 + "组出口号：" + clientId[i] + ";任务号:" + tempvalue);
                            //InBoundService.UpdateInOut(tempvalue, sortgroupno2); 
                            TaskService.UpdateFJFinishStatus(sortgroupno2, tempvalue);//将第一组分拣任务改为完成完成


                            if (tempvalue != 0)
                            {
                                try
                                {
                                    PreSortInfoService.Add((decimal)tempvalue, sortgroupno2);
                                }
                                catch (Exception ex)
                                { }
                                updateListBox(sortgroupno2 + "组:" + tempvalue + "号任务已完成");
                                writeLog.Write(sortgroupno2 + "组:" + tempvalue + "号任务已完成");
                            }

                            removeKey(tempList1, clientId[i]);
                            //this.task_data.BeginInvoke(new Action(() => { initdata(); }));//异步调用，刷新分拣页面的分拣进度 
                        //} 
                    }
                    else
                    {
                        statusGroup4.Write(0, clientId[i] - 1);
                    }
                }

            }
            else if (group == 7)
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    //if (clientId[i] == 1)
                    //{ 
                    if (values[i] != null)
                    {
                        String temp = Convert.ToString(int.Parse(values[i].ToString()), 2);
                        WriteErr(1, clientId[i], temp, sortgroupno1);
                    }
                    //}
                }
            }
            else if (group == 8)
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    String temp = Convert.ToString(int.Parse(values[i].ToString()), 2);
                    if (temp.Length > 16)
                    {
                        temp = temp.Substring(temp.Length - 16);
                    }
                    if (clientId[i] % 10 == 1)//M1  这里应该是机电
                    {
                        WriteErrG(1, clientId[i], temp);
                    }
                    else if (clientId[i] % 10 == 2)//M2 皮带1
                    {
                        WriteErrG(2, clientId[i], temp);
                    }
                    else if (clientId[i] % 10 == 3)////M3   皮带2
                    {
                        WriteErrG(3, clientId[i], temp);
                    }
                    else if (clientId[i] % 10 == 4)//M4   皮带3
                    {
                        WriteErrG(4, clientId[i], temp);
                    }
                    else if (clientId[i] % 10 == 5)//机械手
                    {
                        WriteErrM(clientId[i], temp);
                    }
                }
            }
            else if (group == 9)
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    //if (clientId[i] == 1)
                    //{
                    if (values[i] != null)
                    {
                        String temp = Convert.ToString(int.Parse(values[i].ToString()), 2);
                        WriteErr(2, clientId[i], temp, sortgroupno2);
                    }
                    //}
                }
            }
            else if (group == 10)
            {

            }

            else if (group == 12)//监控标志位 第一组 和第二组
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    if (clientId[i] == 1)//第一组 监控标志位
                    {
                        if (values[i] != null && int.Parse(values[i].ToString()) == 2)//2是电控已经接收
                        {
                            while (!isInit)
                            {
                                Thread.Sleep(100);
                            }
                          decimal tasknum=  decimal.Parse(taskgroup1.ReadD(0).ToString());

                            //if (tempList.Count > 0)
                            //{
                               // TaskService.UpdateFJSendStatus(sortgroupno1,  tempList.ElementAt(tempList.Count - 1).Value);//状态改为已发送
                          if (tasknum != 0)
                          {
                              TaskService.UpdateTaskStatus(sortgroupno1, 15, tasknum);//状态改为已发送
                              updateListBox("组" + sortgroupno1 + "---任务:" + tasknum + "已接收");
                              writeLog.Write(sortgroupno1 + "组:" + tasknum + "号任务已接收");
                          }
                           // }
                            delSendTask task=sendTask;

                            task.BeginInvoke( null, null);
                          //  this.BeginInvoke( new delSendTask(sendTask));
                        }

                    }
                    if (clientId[i] == 2)//第二组 监控标志位
                    {
                        if (values[i] != null && int.Parse(values[i].ToString()) == 2)//2是电控已经接收
                        {
                            while (!isInit)
                            {
                                Thread.Sleep(100);
                            }
                            //if (tempList1.Count > 0)
                            //{
                                //TaskService.UpdateStatus(sortgroupno2, 15, tempList1.ElementAt(tempList1.Count - 1).Value);//状态改为已发送
                            decimal tasknum = decimal.Parse(taskgroup2.ReadD(0).ToString());
                            //if (tempList.Count > 0)
                            //{
                            // TaskService.UpdateFJSendStatus(sortgroupno1,  tempList.ElementAt(tempList.Count - 1).Value);//状态改为已发送
                            if (tasknum != 0)
                            {
                                TaskService.UpdateTaskStatus(sortgroupno2, 15, tasknum);//状态改为已发送
                                updateListBox("组" + sortgroupno2 + "---任务:" + tasknum + "已接收");
                                writeLog.Write(sortgroupno2 + "组:" + tasknum + "号任务已接收");
                            }
                            //} 
                            delSendTask task = sendTask1;

                            task.BeginInvoke(null, null);
                        }
                    }
                }
            }
            else if (group > 99)
            {
                group = group - 99;
                for (int i = 0; i < clientId.Length; i++)
                {
                    if (clientId[i] == 1)//第一位：任务完成标志位
                    {
                        if (values[i]!=null && decimal.Parse(values[i].ToString()) != 0)
                        {
                            writeLog.Write((((groupNo - 1) * 22 + group) + "号机械手已完成：" + decimal.Parse(values[i].ToString()) + "号任务"));
                            updateListBox((((groupNo - 1) * 22 + group) + "号机械手已完成：" + decimal.Parse(values[i].ToString()) + "号任务"));
                            InBoundService.UpdateMachineInOut(decimal.Parse(values[i].ToString()), ((groupNo - 1) * 22 + group));
                            TaskService.UpdateMachineFinished(decimal.Parse(values[i].ToString()), ((groupNo - 1) * 22 + group) + "");

                        }
                    }
                }
            }
        }
        public void Disconnect()
        { 
            if (pIOPCServer != null)
            {
                Marshal.ReleaseComObject(pIOPCServer);
                pIOPCServer = null;
            }
            if (taskgroup1 != null)
            {
                taskgroup1.Release();
            }
            if (statusGroup1 != null)
            {
                statusGroup1.Release();
            }
            if (FinishStateGroup1 != null)
            {
                FinishStateGroup1.Release();
            }
            if (taskgroup2 != null)
            {
                taskgroup2.Release();
            }
            if (statusGroup4 != null)
            {
                statusGroup4.Release();
            }
            if (FinishStateGroup2 != null)
            {
                FinishStateGroup2.Release();
            }
            if (SendTaskStatesGroup != null)
            {
                SendTaskStatesGroup.Release();
            }
        }
        static Boolean isInit=false;
        private void button10_Click(object sender, EventArgs e)
        {
            //TaskService.GetSortTask(1);
           // List<String> list = ItemCollection.getUnionTaskItem();
            Thread thread = new Thread(new ThreadStart(startFenJian));
            thread.Start();

        }

        public void writeListBox(string info)
        {
            String time = DateTime.Now.ToLongTimeString();
            this.list_data.Items.Add(time + "    " + info);
        }

        private delegate void HandleDelegate(string strshow);
        private delegate void HandleDelegateError(string strshow, ListBox list);
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
        public void updateListBox(string info,ListBox list)
        {
            String time = DateTime.Now.ToLongTimeString();
            if (list.InvokeRequired)
            {


                list.Invoke(new HandleDelegateError(updateListBox), info, list);
            }
            else
            {
               
                list.Items.Insert(0, time + "    " + info);

            }
        }

        public void initdata()
        {//刷新分拣进度等
            //writeLog.Write("启动程序。。。。。。");

            task_data.Rows.Clear();
            try
            { 
            //writeLog.Write("启动程序。。。。。。");
               task_data.Rows.Clear();
            
              
               
                 
                DataGridViewCellStyle dgvStyle = new DataGridViewCellStyle();
                dgvStyle.BackColor = Color.LightGreen;
                int Nums = 1;//序号
                List<TaskInfo> list = TaskService.GetSortingCustomer((int)sortgroupno1,(int) sortgroupno2);
                if (list != null)
                {
                     
                    foreach (var row in list)
                    {
                        int index = this.task_data.Rows.Add();

                        this.task_data.Rows[index].Cells[0].Value = Nums++; //序号
                        this.task_data.Rows[index].Cells[1].Value ="第"+ row.GROUPNO+"组";//组号
                        this.task_data.Rows[index].Cells[2].Value = 11;//机械手
                        this.task_data.Rows[index].Cells[3].Value = 40;//出口号
                        this.task_data.Rows[index].Cells[4].Value ="主皮带"+ row.MIANBELT+"号";//主皮带
                        this.task_data.Rows[index].Cells[5].Value = "包装机"+row.PACKAGEMACHINE +"号";//包装机
                        this.task_data.Rows[index].Cells[6].Value = row.FinishQTY + "/" + row.UNIONTASKNUM;//客户数
                        this.task_data.Rows[index].Cells[7].Value = row.FinishCount +"/"+ row.Count;//完成量
                        this.task_data.Rows[index].Cells[8].Value = row.Rate;//完成百分比
                        if (row.Rate == "100%")
                        {
                            this.task_data.Rows[index].Cells[8].Style = dgvStyle;
                        }
                        //this.task_data.Rows[index].Cells[0].Value = Nums ++; //序号
                        //this.task_data.Rows[index].Cells[1].Value = row.REGIONCODE;
                        //this.task_data.Rows[index].Cells[2].Value = row.FinishCount + "/" + row.Count;
                        //this.task_data.Rows[index].Cells[3].Value = row.FinishCount + "/" + row.Count;
                        //this.task_data.Rows[index].Cells[4].Value = row.FinishQTY + "/" + row.QTY;
                        //this.task_data.Rows[index].Cells[5].Value = row.Rate;
                        //this.task_data.Rows[index].Cells[6].Value = row.Rate;
                    }

                }

            }
            finally
            {

            }



        }
        delegate void UpdateDataGridView(string data);
        public void updateTaskInfo(string taskinfo)
        {
            Console.WriteLine("进入方法updateTaskInfo");
            if (taskinfo != null && taskinfo.Length > 0)
            {
                string[] info = taskinfo.Split('-');

                int len = task_data.RowCount;
                int indexj = 0;
                //取要修改分拣数据的行标

                if (len > 0)
                {
                    for (int i = 0; i < len; i++)
                    {
                        if (this.task_data.Rows[i].Cells[0].Value.ToString() == info[0].ToString())
                        {
                            indexj = i;
                            break;
                        }
                    }
                    Console.WriteLine(this.task_data.Rows[indexj].Cells[0].Value.ToString());
                    string[] boxcount = this.task_data.Rows[indexj].Cells[2].Value.ToString().Split('/');
                    string[] cuscount = this.task_data.Rows[indexj].Cells[3].Value.ToString().Split('/');
                    string[] finishqty = this.task_data.Rows[indexj].Cells[4].Value.ToString().Split('/');

                    //修改完成箱数
                    int boxc = int.Parse(boxcount[0].ToString());
                    boxc = boxc + 1;
                    this.task_data.Rows[indexj].Cells[2].Value = boxc + "/" + boxcount[1];

                    //修改完成客户数

                    int cusc = int.Parse(cuscount[0].ToString());
                    cusc = cusc + 1;
                    this.task_data.Rows[indexj].Cells[3].Value = cusc + "/" + cuscount[1];

                    //修改完成分拣量

                    int finish = int.Parse(finishqty[0].ToString());
                    finish = finish + int.Parse(info[1].ToString());
                    this.task_data.Rows[indexj].Cells[4].Value = finish + "/" + finishqty[1];

                    //修改分拣完成百分比

                    double percent = Math.Round(double.Parse(finish + "") / double.Parse(finishqty[1].ToString()) * 100, 2);
                    this.task_data.Rows[indexj].Cells[5].Value = percent + "%";

                    Console.WriteLine(boxc + "/" + boxcount[1]);
                    Console.WriteLine(cusc + "/" + cuscount[1]);
                    Console.WriteLine(finish + "/" + finishqty[1]);
                    Console.WriteLine(percent + "%");
                }

            }
        }

        private void w_SortingControlMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DialogResult MsgBoxResult = MessageBox.Show("确定要退出程序?",//对话框的显示内容 
            //                                                 "操作提示",//对话框的标题 
            //                                                 MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
            //                                                 MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
            //                                                 MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
            ////Console.WriteLine(MsgBoxResult);
            //if (MsgBoxResult == DialogResult.Yes)
            //{
            //    System.Environment.Exit(System.Environment.ExitCode);
            //    this.Dispose();
            //    this.Close();
            //}
            //else
            //{
            //    e.Cancel = true;
            //}
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            //base.OnClosing(e);
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
        private void button11_Click(object sender, EventArgs e)
        {
            initdata();
            //this.task_data.BeginInvoke(new Action(() => { initdata(); }));
        }




        private void button12_Click(object sender, EventArgs e)
        {

            updateControlEnable(true, button10);
            //Application.Exit();
            /*if (MessageBox.Show("是否确实关闭窗口？", "询问", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //System.Environment.Exit(System.Environment.ExitCode);
                
                this.Dispose();
                this.Close();
            }*/
        }

        private void button6_Click(object sender, EventArgs e)
        {
            w_SortingControlMainTest w_SortingControlMain = new w_SortingControlMainTest();

            //w_SortingControlMain.MdiParent = this;
            w_SortingControlMain.WindowState = FormWindowState.Maximized;
            w_SortingControlMain.Show();
            //this.Close();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            w_pass pass = new w_pass();

            pass.StartPosition = FormStartPosition.CenterScreen;
            pass.Show();


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (pIOPCServer != null)
            {
                AlineStopFlag = ChangeFalg(!AlineStopFlag, 0, "A线", btnClear) == true ? true : false;
            }
        }

        private void btnClearB_Click(object sender, EventArgs e)
        {
            if (pIOPCServer != null)
            {
                BlineStopFlag = ChangeFalg(!BlineStopFlag, 1, "B线", btnClearB) == true ? true : false;
            }
        }

        /// <summary>
        /// 状态切换
        /// </summary>
        /// <param name="isStop">状态</param>
        /// <param name="index">分拣线</param>
        /// <param name="lineName">分拣线名称</param>
        /// <param name="btn">对应按钮</param>
        /// <returns></returns>
        private bool ChangeFalg(bool isStop, int index, string lineName, Button btn)
        {

            if (isStop)
            {
                ClearCacheGroup.Write(1, index);
                isStop = true;
               // btn.Text = string.Format("{0}恢复", lineName);
            }
            else
            {
                ClearCacheGroup.Write(0, index);
                isStop = false;
              //  btn.Text = string.Format("{0}停止", lineName);
            }
            return isStop;
        }

        private void w_SortingControlMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //MessageBox.Show("close.....");
        }

        private void w_SortingControlMain_SizeChanged(object sender, EventArgs e)
        {
            task_data.Height = this.Size.Height - list_data.Size.Height ;
            task_data.Width = this.Size.Width - groupBoxErr.Size.Width-20; // listError.Size.Width -15;
        }
        /// <summary>
        /// 使DataGridView的列自适应宽度
        /// </summary>
        /// <param name="dgViewFiles"></param>
        private void AutoSizeColumn(DataGridView dgViewFiles)
        {
            int width = 0;
            //使列自使用宽度
            //对于DataGridView的每一个列都调整
            for (int i = 0; i < dgViewFiles.Columns.Count; i++)
            {

                //将每一列都调整为自动适应模式
                dgViewFiles.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                //记录整个DataGridView的宽度
                width += dgViewFiles.Columns[i].Width;

            }
            //判断调整后的宽度与原来设定的宽度的关系，如果是调整后的宽度大于原来设定的宽度，
            //则将DataGridView的列自动调整模式设置为显示的列即可，
            //如果是小于原来设定的宽度，将模式改为填充。
            if (width > dgViewFiles.Size.Width)
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            //冻结某列 从左开始 0，1，2
            dgViewFiles.Columns[0].Width = 45;
            dgViewFiles.Columns[1].Frozen = true;
        }

        private void Timerinitdata_Tick(object sender, EventArgs e)
        {
            initdata();
            Timerinitdata.Interval = 20000;//10秒刷新
        }

        private void listError_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(e.BackColor), e.Bounds);
            if (e.Index >= 0)
            {
                StringFormat sStringFormat = new StringFormat();
                sStringFormat.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds, sStringFormat);
                StringFormat strFmt = new System.Drawing.StringFormat();
                strFmt.Alignment = StringAlignment.Center; //文本垂直居中
                strFmt.LineAlignment = StringAlignment.Center; //文本水平居中
            }
            e.DrawFocusRectangle();
        }

        private void listError_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            ListBox lb = ((ListBox)sender);
            if (lb.Items[e.Index].ToString().Contains("-"))
            { 
                e.ItemHeight = e.ItemHeight + 13;
            }
            else
            {
                e.ItemHeight = e.ItemHeight + (lb.Items[e.Index].ToString().Length);//行间距
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Fm_SelectedInfoEX fs = new Fm_SelectedInfoEX();
            fs.StartPosition = FormStartPosition.CenterScreen;
            fs.Show();
        }


    }
}
