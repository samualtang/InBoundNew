using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Remoting.Messaging;
using System.Collections;
using System.Configuration;
using OpcRcw.Da;
using OpcRcw.Comn;
using System.Runtime.InteropServices;
using UnNormal_New.Model;
using InBound.Model;
using InBound.Business;
using InBound;
using UnNormal_New;


namespace UnNormal_New 
{
    public partial class UnNormalFm : Form
    {

       
        /* Constants */
        internal const string SERVER_NAME = "OPC.SimaticNET";       // local server name
      
        internal const string GROUP_NAME = "grp1";                  // Group name
        internal const int LOCALE_ID = 0x409;                       // LOCALE FOR ENGLISH.
        String lineNum = "";
        /* Global variables */
        IOPCServer pIOPCServer;  //定义opcServer对象
        public WriteLog writeLog =  WriteLog.GetLog();
        public DeviceStateManager stateManager = new DeviceStateManager();
        public UnNormalFm()
        {
            InitializeComponent();
            updateListBox("应用程序启动");
            writeLog.Write("应用程序启动");
            t1.Tick += new EventHandler(t1_Tick);
            try
            {
                lineNum = ConfigurationManager.AppSettings["LineNum"].ToString();
               // UnPokeService.getTask(25, lineNum, out list);
               initdata();
               t1.Start();//定时刷新
            }
            catch (Exception e)
            {
                MessageBox.Show("请检查一下数据网络,在重新打开系统");
                writeLog.Write("请检查一下数据网络,在重新打开系统");
                writeLog.Write(e.Message);
                this.Close();
            }
            
         }

        void t1_Tick(object sender, EventArgs e)
        {
            initdata();
            t1.Interval = 20000;//二十秒刷新
        }
       System.Windows.Forms. Timer t1 = new System.Windows.Forms.Timer();
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            stateManager.AlarmsHandler += (obj) =>
            {
                updateListBox(string.Format("{0}号设备发生故障,故障名称{1}", obj.DeviceNo, obj.ErrInfo), listError);
            };

           // this.task_data.BeginInvoke(new Action(() => { initdata(); }));
            if (tempList == null)
                tempList = new List<KeyValuePair<int, int>>();

        }
        private delegate void HandleDelegate1(string info, Label label);
        public void updateLabel(string info,Label label)
        {
            String time = DateTime.Now.ToLongTimeString();
            if (label.InvokeRequired)
            {
              label.Invoke(new HandleDelegate1(updateLabel), new Object[]{info,label});
            }
            else
            {
                label.Text=info;

            }
        }
        private delegate void HandleDelegate2(Boolean visible, Control control);
        public void updateControlVisible(Boolean visible,Control control)
        {
            if (control.InvokeRequired)
            {
           
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
        private void timerSendTask_Tick(object sender, EventArgs e)
        {
            updateListBox("触发定时器");
            if (TaskGroup1.Read(0).ToString() != "0" && !issendone)//监控标志位第一组 产生跳变
            {
                TaskGroup1.Write(1, 0);
                TaskGroup1.Write(0, 0);
            }
            else if (TaskGroup2.Read(1).ToString() != "0" && !issendtwo)
            {
                TaskGroup2.Write(1, 1);
                TaskGroup2.Write(0, 1);
            }
            else if (CabinetTaskGroup.Read(2).ToString() != "0" && !issendsixtwo)
            {
                CabinetTaskGroup.Write(1, 2);
                CabinetTaskGroup.Write(0, 2);
            }
            else if (UnUnionTaskGroup.Read(3).ToString() != "0" && !issendsixone)
            {
                UnUnionTaskGroup.Write(1, 3);
                UnUnionTaskGroup.Write(0, 3);
            }
            timerSendTask.Stop();
        }
 
        Group TaskGroup1, TaskGroup2, CabinetTaskGroup, UnUnionTaskGroup, TaskFinishGroup1, TaskFinishGroup2, CabinetFinishTaskGroup, UnionTaskFinishGroup, SpyChangeGroup;
        //一线烟仓任务组，二线烟仓任务组，烟柜任务组，合流（烟仓，烟柜，特异型烟）任务组，一线烟仓完成任务组，二线烟仓完成任务组，烟柜完成，合流完成，监视三组标志位
        Group errgroup, statusGroup2, statusGroup3;
        public void Connect()
        {
            Type svrComponenttyp;
            Guid iidRequiredInterface = typeof(IOPCItemMgt).GUID;
            svrComponenttyp = Type.GetTypeFromProgID(SERVER_NAME);
            try
            {
                // Connect to the local server.
                pIOPCServer = (IOPCServer)Activator.CreateInstance(svrComponenttyp);
                TaskGroup1 = new Group(pIOPCServer, 1, "group1", 1, LOCALE_ID);//立式一组
                TaskGroup2 = new Group(pIOPCServer, 2, "group2", 1, LOCALE_ID);//立式二组 
                CabinetTaskGroup = new Group(pIOPCServer, 3, "group3", 1, LOCALE_ID);//烟柜
                UnUnionTaskGroup = new Group(pIOPCServer, 4, "group4", 1, LOCALE_ID);//合流

                TaskFinishGroup1 = new Group(pIOPCServer, 4, "group4", 1, LOCALE_ID);//立式一组完成信号
                TaskFinishGroup2 = new Group(pIOPCServer, 5, "group5", 1, LOCALE_ID);//立式一组完成信号
                CabinetFinishTaskGroup = new Group(pIOPCServer, 6, "group6", 1, LOCALE_ID);//烟柜完成信号
                UnionTaskFinishGroup = new Group(pIOPCServer, 7, "group7", 1, LOCALE_ID);//合流 完成信号 
                SpyChangeGroup = new Group(pIOPCServer, 8, "group8", 1, LOCALE_ID);//监控标志位
                errgroup = new Group(pIOPCServer, 9, "group9", 1, LOCALE_ID);
                statusGroup2 = new Group(pIOPCServer, 10, "group10", 1, LOCALE_ID);
                statusGroup3 = new Group(pIOPCServer, 11, "group11", 1, LOCALE_ID); 
                //任务交互区
                TaskGroup1.addItem(ItemCollection.GetTask1ALineItem());
                TaskGroup2.addItem(ItemCollection.GetTask2ALineItem());
                CabinetTaskGroup.addItem(ItemCollection.GetCabinetTaskItem()); 
                UnUnionTaskGroup.addItem(ItemCollection.GetUnUnionItem());
                SpyChangeGroup.addItem(ItemCollection.GetSpyDbChangeItem());
                //完成信号交互区
                TaskFinishGroup1.addItem(ItemCollection.GetFinishTaskStatusItem1()); 
                TaskFinishGroup2.addItem(ItemCollection.GetFinishTaskStatusItem2());
                CabinetTaskGroup.addItem(ItemCollection.GetCabinetTaskFinishStatusItem());
                UnionTaskFinishGroup.addItem(ItemCollection.GetUnUnionTaskFinishStatusItem()); 
                //异常
                errgroup.addItem(ItemCollection.GetTaskError()); 
                statusGroup2.addItem(ItemCollection.GetTaskItem2());
                statusGroup3.addItem(ItemCollection.GetTaskItem3());

                //回调 
                errgroup.callback += OnDataChange;
                SpyChangeGroup.callback += OnDataChange; //监控发送标志位
                TaskFinishGroup1.callback += OnDataChange;
                TaskFinishGroup2.callback += OnDataChange;
                CabinetTaskGroup.callback += OnDataChange;
                UnionTaskFinishGroup.callback += OnDataChange;

                checkConnection();
            
            }
            catch (Exception e)
            {
                updateListBox("连接服务器失败:"+e.Message);
            }
        }
        public void checkConnection()
        {
            int flag = TaskGroup1.ReadD(225).CastTo<int>(-1);
            if (flag == -1)
            {
                updateListBox("连接服务器失败,请检查网络.");
                writeLog.Write(" 连接服务器失败,请检查网络." );
            }
            else
            {   
                updateListBox("连接服务器成功......");
                writeLog.Write(" 连接服务器成功......");
                updateControlEnable(false, button10);
                isInit = true;
            }
        }
        Boolean CheckCanSend(int targetPort)
        {
            writeLog.Write("出口号：" + targetPort);
            int value = statusGroup3.Read(targetPort - 1).CastTo<int>(-1);
            writeLog.Write(" value=" + value);
            if (value == 1)
            {
                return true;
            }
            else
                return false;
        }
        List<KeyValuePair<int, int>> tempList = new List<KeyValuePair<int, int>>();
        public void removeKey(int export)
        {
            int i = 0;
            if (tempList != null)
            {
                foreach (var item in tempList)
                {
                    i++;
                if(item.Key==export)
                {

                    if (i != tempList.Count)
                    {
                        tempList.Remove(item);
                    }
                    break;
                }
                }
               }
        }

        public int getKey(int export)
        {
            int i = -1;
            if (tempList != null)
            {
                foreach (var item in tempList)
                {
                    if (item.Key == export)
                    {
                        i= item.Value;
                        break;
                    }
                }
            }
            return i;
        }
        delegate void delSendTask();
        List<T_UN_POKE> list1 = new List<T_UN_POKE>();
        List<T_UN_POKE> list2 = new List<T_UN_POKE>();
        List<T_UN_POKE> listSix2A = new List<T_UN_POKE>();//烟柜2线（A）订单信息 
        List<T_UN_POKE> listSix1B = new List<T_UN_POKE>();//烟柜1线（B）订单信息
        List<T_UN_POKE> listFinishSignal = new List<T_UN_POKE>();
        /// <summary>
        /// 第二组
        /// </summary>
        void sendTask2()
        { 
            try
            {
                issendtwo = false;
                int flag = SendTaskStatesGroup.ReadD(1).CastTo<int>(-1);
                writeLog.Write("烟仓二线发送数据前读标志位：" + flag);
                if (flag == 2)
                {
                    string OutStr = "";
                   // decimal packageNum = 0;
                    object[] datas = UnPokeService.getTask(25, "2", out list2, out OutStr);
                    if (int.Parse(datas[0].ToString())== 0)
                    {
                        updateListBox("烟仓二线分拣数据发送完毕");
                        return;
                    }
                    //string logstr = "";
                    //for (int i = 0; i < datas.Length; i++)
                    //{
                    //    logstr += i + ":" + datas[i] + ";";
                    //}
                    writeLog.Write("烟仓分拣二线:" + OutStr);
                    updateListBox("烟仓分拣二线:" + OutStr);
                    TaskGroup2.SyncWrite(datas);
                   
                    //写完db块后,再读出来 
                    //String p1 = "";
                    //for (int i = 0; i <= 225; i = i + 9)
                    //{
                    //    p1 += taskGroup2.ReadD(i).ToString() + ";";//pokeid  
                    //}
                    //writeLog.Write("读出烟仓二线电控写入值:" + p1);
                    
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
                sendTask2();//异常后重新发送
            }
        }


        bool issendone = false, issendtwo = false, issendsixone = false, issendsixtwo = false;
        /// <summary>
        /// 第一组
        /// </summary>
        void sendTask1()
        {
            
            try
            {
                issendone = true;
                int flag = SendTaskStatesGroup.ReadD(0).CastTo<int>(-1);
                writeLog.Write("烟仓一线发送数据前读标志位：" + flag);
                if (flag == 2)
                {
                    string OutStr = "";
                    object[] datas = UnPokeService.getTask(25, "1", out list1, out OutStr);
                    if (int.Parse(datas[0].ToString()) == 0)
                    {
                        updateListBox("烟仓一线分拣数据发送完毕");
                        return;
                    } 
                    //string logstr = "";
                    //for (int i = 0; i < datas.Length; i++)
                    //{
                    //    logstr += i + ":" + datas[i] + ";";
                    //}

                    writeLog.Write("分拣烟仓一线:" + OutStr);
                    updateListBox("分拣烟仓一线:" + OutStr);

                    taskgroup1.SyncWrite(datas);  
                    //写完db块后,再读出来 
                    //String p1 = "";
                    //for (int i = 0; i <= 225; i = i + 9)
                    //{
                    //    p1 += taskgroup1.ReadD(i).ToString() + ";";//pokeid   
                    //}
                   //writeLog.Write("读出烟仓一线电控写入值:" + p1);
                    
                }
            }
            catch(Exception ex)
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
        /// <summary>
        /// 烟柜2线（A）订单信息
        /// </summary>
        void sendSixCabine2AtTask()
        { 
            try
            {
                issendsixtwo = false;
                int flag = SendTaskStatesGroup.ReadD(2).CastTo<int>(-1);
                writeLog.Write("烟柜二线发送数据前读标志位：" + flag);
                if (flag == 2)
                {
                    string OutStr = "";
                 //   string linenum = UnPokeService.getSixCabinetLineNum();//烟柜分拣线
                    object[] datas = UnPokeService.getSixCabinetTask(25, "2", out listSix2A, out OutStr);
                    if (int.Parse(datas[0].ToString()) == 0)
                    {
                        updateListBox("烟柜二线分拣数据发送完毕");
                        return;
                    }
                    //string logstr = "";
                    //for (int i = 0; i < datas.Length; i++)
                    //{
                    //    logstr += i + ":" + datas[i] + ";";
                    //}
                    writeLog.Write("烟柜二线分拣发送数据:" + OutStr);
                    updateListBox("烟柜二线分拣发送数据:" + OutStr);
                    //写电控
                    SixCabinetGroup2A.SyncWrite(datas); 
                    //读电控
                    //String p1 = "";
                    //for (int i = 0; i <= 225; i = i + 9)
                    //{
                    //    p1 += SixCabinetGroup2A.ReadD(i).ToString()+";";//pokeid  
                    //}
                    //writeLog.Write("读出烟柜二线电控写入值:" + p1);
                  
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
                sendSixCabine2AtTask();//异常后重新发送
            }
        }

        /// <summary>
        /// 烟柜1线（B）订单信息
        /// </summary>
        void sendSixCabine1BtTask()
        {
            try
            {
                issendsixone = false;
                string OutStr = "";
                int flag = SendTaskStatesGroup.ReadD(3).CastTo<int>(-1);
                writeLog.Write("烟柜一线发送数据前读标志位：" + flag);
                if (flag == 2)
                {

                    //   string linenum = UnPokeService.getSixCabinetLineNum();//烟柜分拣线
                    object[] datas = UnPokeService.getSixCabinetTask(25, "1", out listSix1B, out OutStr);
                    if (int.Parse(datas[0].ToString()) == 0)
                    {
                        updateListBox("烟柜一线分拣数据发送完毕");
                        return;
                    }
                    //string logstr = "";
                    //for (int i = 0; i < datas.Length; i++)
                    //{
                    //    logstr += i + ":" + datas[i] + ";";
                    //}
                    writeLog.Write("烟柜一线分拣发送数据:" + OutStr);
                    updateListBox("烟柜一线分拣发送数据:" + OutStr);
                    //写电控
                    SixCabinetGroup1B.SyncWrite(datas);
                    //读电控
                    //String p1 = "";
                    //for (int i = 0; i <= 225; i = i + 9)
                    //{
                    //    p1 += SixCabinetGroup1B.ReadD(i).ToString() + ";";//pokeid  
                    //}
                    //writeLog.Write("读出烟柜一线电控写入值:" + p1);

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
                sendSixCabine1BtTask();//异常后重新发送
            }
        }

        public static Object lockFlag = new Object();
        public void OnDataChange(int group, int[] clientId, object[] values)
        {
            if (group == 8) //监控标志位
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    if (clientId[i] ==1)//一组烟仓
                    {

                        if (values[i] != null && int.Parse(values[i].ToString()) == 0)
                        {
                            while (!isInit)
                            {
                                Thread.Sleep(100);
                            }
                            String logstr = "";
                            foreach (var item in list1)
                            {
                                logstr += item.POKEID + ";";
                            }
                            if (logstr != null && logstr.Length > 0)
                            {
                                writeLog.Write("第一组任务号:" + logstr + "已接收");
                                updateListBox("第一组任务号:" + logstr + "已接收");
                                UnPokeService.UpdateTask(list1, 15);
                                UnPokeService.UpdateStroageInout(list1);
                            }
                            sendTask1();
                        }
                        break;
                    }
                    if (clientId[i] == 2)//二组烟仓
                    {

                        if (values[i] != null && int.Parse(values[i].ToString()) == 2)
                        {
                            while (!isInit)
                            {
                                Thread.Sleep(100);
                            }
                            String logstr = "";
                            foreach (var item in list1)
                            {
                                logstr += item.POKEID + ";";
                            }
                            if (logstr != null && logstr.Length > 0)
                            {
                                writeLog.Write("第一组任务号:" + logstr + "已接收");
                                updateListBox("第一组任务号:" + logstr + "已接收");
                                UnPokeService.UpdateTask(list1, 15);
                                UnPokeService.UpdateStroageInout(list1);
                            }
                            sendTask1();
                        }
                        break;
                    }
                    if (clientId[i] == 3)//烟柜
                    {

                        if (values[i] != null && int.Parse(values[i].ToString()) == 2)
                        {
                            while (!isInit)
                            {
                                Thread.Sleep(100);
                            }
                            String logstr = "";
                            foreach (var item in list1)
                            {
                                logstr += item.POKEID + ";";
                            }
                            if (logstr != null && logstr.Length > 0)
                            {
                                writeLog.Write("第一组任务号:" + logstr + "已接收");
                                updateListBox("第一组任务号:" + logstr + "已接收");
                                UnPokeService.UpdateTask(list1, 15);
                                UnPokeService.UpdateStroageInout(list1);
                            }
                            sendTask1();
                        }
                        break;
                    }
                    if (clientId[i] == 4)//合流
                    {

                        if (values[i] != null && int.Parse(values[i].ToString()) == 2)
                        {
                            while (!isInit)
                            {
                                Thread.Sleep(100);
                            }
                            String logstr = "";
                            foreach (var item in list1)
                            {
                                logstr += item.POKEID + ";";
                            }
                            if (logstr != null && logstr.Length > 0)
                            {
                                writeLog.Write("第一组任务号:" + logstr + "已接收");
                                updateListBox("第一组任务号:" + logstr + "已接收");
                                UnPokeService.UpdateTask(list1, 15);
                                UnPokeService.UpdateStroageInout(list1);
                            }
                            sendTask1();
                        }
                        break;
                    }
                }
            }
      
            else if (group == 9)
            {
                for (int i = 0; i < clientId.Length; i++)
                {

                    // clientId[i]//序号
                    // values[i]//值

                    lock (lockFlag)
                    {
                        if (values[i] != null)
                        {
                            stateManager.WriteErrWithCheck(Math.Abs(int.Parse(values[i].ToString())).ToString(), clientId[i].ToString(), lineNum);
                            stateManager.AlarmsHandler += (obj) =>
                            {
                                updateListBox(string.Format("{0}号设备发生故障,故障名称{1}", obj.DeviceNo, obj.ErrInfo), listError);
                            };
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
            if (TaskGroup2 != null)
            {
                TaskGroup2.Release();
            }
            if (statusGroup2 != null)
            {
                statusGroup2.Release();
            }
            if (statusGroup3 != null)
            {
                statusGroup3.Release();
            }
            
        }
        static Boolean isInit = false;
        private void button10_Click(object sender, EventArgs e)//开始
        {
            // UnPokeService.getTask(25);
            timerSendTask.Interval = 1000 * 10;
            timerSendTask.Start();
            updateListBox("启动定时器");
            Thread thread = new Thread(new ThreadStart(startFenJian));
            thread.Start();

        }
       
       public void writeListBox(string info) {
           String time = DateTime.Now.ToLongTimeString();
           this.list_data.Items.Add(time + "    "+info);
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
               this.list_data.Items.Insert(0,time + "    " + info);

           }
       }

       public void updateListBox(string info,ListBox list)
       {
           String time = DateTime.Now.ToLongTimeString();
           if (this.list_data.InvokeRequired)
           {

               this.list_data.Invoke(new HandleDelegate(updateListBox), info,list);
           }
           else
           {
               this.list_data.Items.Insert(0, time + "    " + info);

           }
       }
       public void initdata() {
           writeLog.Write("initdata");
           task_data.Rows.Clear(); 
           try
           {
               List<TaskInfo> list =  TaskService.GetUNCustomer();
               if (list != null)
               {
                   DataGridViewCellStyle dgvStyle = new DataGridViewCellStyle();
                   dgvStyle.BackColor = Color.LightGreen;
                   int i =1;
                   foreach (var row in list)
                   {
                       int index = this.task_data.Rows.Add();
                       this.task_data.Rows[index].Cells[0].Value = i++;//序号
                       this.task_data.Rows[index].Cells[1].Value = "长沙市烟草公司";//货主
                       this.task_data.Rows[index].Cells[2].Value = row.ORDERDATE.Value.Date.ToString("D"); //订单日期
                       this.task_data.Rows[index].Cells[3].Value = "批次" + row.SYNSEQ;//批次
                       this.task_data.Rows[index].Cells[4].Value = row.REGIONCODE;//线路编号
                       this.task_data.Rows[index].Cells[5].Value = row.REGIONCODE;//线路名称
                       //this.task_data.Rows[index].Cells[2].Value = row.FinishCount + "/" + row.Count;
                       this.task_data.Rows[index].Cells[6].Value = row.FinishCount + "/" + row.Count;
                       this.task_data.Rows[index].Cells[7].Value = row.FinishQTY + "/" + row.QTY; 
                       this.task_data.Rows[index].Cells[8].Value ="分拣"+ row.LineNum+"线";
                       this.task_data.Rows[index].Cells[9].Value = row.Rate;

                       if (row.Rate == "100%")
                       {
                           this.task_data.Rows[index].Cells[9].Style = dgvStyle;
                       }
                   }
                   task_data.Sort(task_data.Columns[0], ListSortDirection.Ascending); 
               }

           }
           finally
           {

           }
         }
       delegate void UpdateDataGridView(string data);
       public void updateTaskInfo(string taskinfo) 
       {
         
            if(taskinfo!=null&&taskinfo.Length>0){
                string[] info = taskinfo.Split('-');
                int len=task_data.RowCount;
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
                    this.task_data.Rows[indexj].Cells[5].Value = percent+"%";

                 
                }
                
            }
       }
       private void w_SortingControlMain_FormClosing(object sender, FormClosingEventArgs e)
       {
         
       }
       
       protected override void OnClosing(CancelEventArgs e)
       {
           base.OnClosing(e);

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
       private void button11_Click(object sender, EventArgs e)
       {
           initdata();
         //  this.task_data.BeginInvoke(new Action(() => { initdata(); }));
       }
       private void button12_Click(object sender, EventArgs e)
       {
           
           updateControlEnable(true, button10);
           
       }
       private void button6_Click(object sender, EventArgs e)
       {
         
       }

       private void button7_Click(object sender, EventArgs e)
       {
           //UnPokeService.getName();
        
           if (statusGroup2 != null)
           {
               statusGroup2.SyncWrite(UnPokeService.getCode());
               statusGroup3.SyncWrite(UnPokeService.getName());
           }
           else
           {
               MessageBox.Show("连接未建立,请稍后同步!");
           }
       }

       private void button6_Click_1(object sender, EventArgs e)
       {
           w_pass pass = new w_pass();
           pass.StartPosition = FormStartPosition.CenterScreen;

           pass.Show();
       }

       private void UnNormalFm_Load(object sender, EventArgs e)
       {
           AutoSizeColumn(task_data);
           this.task_data.DoubleBufferedDataGirdView(true);
       }

       private void UnNormalFm_SizeChanged(object sender, EventArgs e)
       {
           task_data.Height = this.Size.Height - list_data.Size.Height;
           task_data.Width = this.Size.Width - groupboxErr.Width;
        
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
           dgViewFiles.Columns[0].Frozen = true;
       }

       private void task_data_CellClick(object sender, DataGridViewCellEventArgs e)
       {
           MessageBox.Show(e.RowIndex + "");
       }

    
      
     
    }
}
