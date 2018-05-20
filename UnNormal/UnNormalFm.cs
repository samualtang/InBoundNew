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
using SortingControlSys.Model;
using InBound.Model;
using InBound.Business;
using InBound;
using UnNormal;


namespace SortingControlSys.SortingControl
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
            try
            {
                lineNum = ConfigurationManager.AppSettings["LineNum"].ToString();
               // UnPokeService.getTask(25, lineNum, out list);
               initdata();
           
            }
            catch (Exception e)
            {
                MessageBox.Show("请检查一下数据网络,在重新打开系统");
                writeLog.Write("请检查一下数据网络,在重新打开系统");
                writeLog.Write(e.Message);
                this.Close();
            }
         }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            stateManager.AlarmsHandler += (obj) =>
            {
                updateListBox(string.Format("{0}号设备发生故障,故障名称{1}", obj.DeviceNo, obj.ErrInfo), listError);
            };

            this.task_data.BeginInvoke(new Action(() => { initdata(); }));
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
        Group taskgroup1, taskGroup2,SendTaskStatesGroup, statusGroup2, statusGroup3, errgroup, SixCabinetGroup, FinishSignalGroup;
        public void Connect()
        {
            Type svrComponenttyp;
            Guid iidRequiredInterface = typeof(IOPCItemMgt).GUID;
            svrComponenttyp = Type.GetTypeFromProgID(SERVER_NAME);
            try
            {
                // Connect to the local server.
                pIOPCServer = (IOPCServer)Activator.CreateInstance(svrComponenttyp);
                taskgroup1 = new Group(pIOPCServer, 1, "group", 1, LOCALE_ID);//立式一组
                taskGroup2 = new Group(pIOPCServer, 2, "group1", 1, LOCALE_ID);//立式二组
                SixCabinetGroup = new Group(pIOPCServer, 6, "group6", 1, LOCALE_ID);
               //监控发送标志
                SendTaskStatesGroup =  new Group(pIOPCServer , 8 ,"group8",1,LOCALE_ID);


                statusGroup2 = new Group(pIOPCServer, 3, "group2", 1, LOCALE_ID);
                statusGroup3 = new Group(pIOPCServer, 4, "group3", 1, LOCALE_ID);
                errgroup = new Group(pIOPCServer, 5, "group5", 1, LOCALE_ID);

                //异形烟6个烟柜
                taskgroup1.addItem(ItemCollection.GetTaskItem());
                SixCabinetGroup.addItem(ItemCollection.GetSixCabinetTaskItem());
                taskGroup2.addItem(ItemCollection.GetTaskItem1());
                //监控发送标志
                SendTaskStatesGroup.addItem(ItemCollection.GetSendStatesItem());
                SendTaskStatesGroup.callback += OnDataChange;

                //接收完成信号组
                FinishSignalGroup = new Group(pIOPCServer, 7, "group7", 1, LOCALE_ID);
                FinishSignalGroup.addItem(ItemCollection.GetFinishSignalTaskItem()); 
                FinishSignalGroup.callback += OnDataChange;
                  
                errgroup.addItem(ItemCollection.GetTaskError());
                errgroup.callback += OnDataChange;
                 
                statusGroup2.addItem(ItemCollection.GetTaskItem2()); 
                statusGroup3.addItem(ItemCollection.GetTaskItem3());   
                checkConnection();
            
            }
            catch (Exception e)
            {
                updateListBox("连接服务器失败:"+e.Message);
            }
        }
        public void checkConnection()
        {
            int flag = taskgroup1.ReadD(225).CastTo<int>(-1);
            if (flag == -1)
            {
                updateListBox("连接服务器失败,请检查网络.");
                writeLog.Write(" 连接服务器失败,请检查网络." );
            }
            else
            {   //接收位初始化
                //if (taskgroup1.Read(225).ToString() != "1")
                //{
                //    taskgroup1.Write(2, 225);
                //}
                //if (taskGroup2.Read(225).ToString() != "1")
                //{
                //    taskGroup2.Write(2, 225);
                //}
                //if (SixCabinetGroup.Read(225).ToString() != "1")
                //{
                //    SixCabinetGroup.Write(2, 225);
                //}
                if (SendTaskStatesGroup.ReadD(0).ToString() == "0")
                {
                    SendTaskStatesGroup.Write(2, 0);
                }
                if (SendTaskStatesGroup.ReadD(1).ToString() == "0")
                {
                    SendTaskStatesGroup.Write(2, 1);
                }
                if (SendTaskStatesGroup.ReadD(2).ToString() == "0")
                {
                    SendTaskStatesGroup.Write(2, 2);
                }
                updateListBox("连接服务器成功......");
                writeLog.Write(" 连接服务器成功......");
                updateControlEnable(false, button10);
                isInit = true;
            }
        }
        Boolean CheckCanSend(int targetPort)
        {
            writeLog.Write("出口号：" + targetPort);
            int  value= statusGroup3.Read(targetPort-1).CastTo<int>(-1);
            writeLog.Write(" value="+value);
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
        List<T_UN_POKE> list = new List<T_UN_POKE>();
        List<T_UN_POKE> list1 = new List<T_UN_POKE>();
        List<T_UN_POKE> listSix = new List<T_UN_POKE>();//六组烟柜
        List<T_UN_POKE> listFinishSignal = new List<T_UN_POKE>();
        /// <summary>
        /// 第二组
        /// </summary>
        void sendTask2()
        { 
            try
            {
                int flag = SendTaskStatesGroup.ReadD(1).CastTo<int>(-1);
                writeLog.Write("二线发送数据前读标志位：" + flag);
                if (flag == 2)
                {
                   // decimal packageNum = 0;
                    object[] datas = UnPokeService.getTask(25, "2", out list1);
                    if (int.Parse(datas[0].ToString())== 0)
                    {
                        updateListBox("二线分拣数据发送完毕");
                        return;
                    }
                    string logstr = "";
                    for (int i = 0; i < datas.Length; i++)
                    {
                        logstr += i + ":" + datas[i] + ";";
                    }
                    writeLog.Write("分拣线2:" + logstr);
                    updateListBox("分拣线2:" + logstr);
                    taskGroup2.SyncWrite(datas);
                   
                    //写完db块后,再读出来 
                    String p1 = "";
                    for (int i = 0; i <= 225; i = i + 9)
                    {
                        p1 += taskGroup2.ReadD(i).ToString() + ";";//pokeid  
                    }
                    writeLog.Write("读出第二组电控写入值:" + p1);
                    
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
        /// <summary>
        /// 第一组
        /// </summary>
        void sendTask1()
        {
            
            try
            {
                int flag = SendTaskStatesGroup.ReadD(0).CastTo<int>(-1);
                writeLog.Write("一线发送数据前读标志位：" + flag);
                if (flag == 2)
                {
                   
                    object[] datas = UnPokeService.getTask(25, "1", out list);
                    if (int.Parse(datas[0].ToString()) == 0)
                    {
                        updateListBox("一线分拣数据发送完毕");
                        return;
                    } 
                    string logstr = "";
                    for (int i = 0; i < datas.Length; i++)
                    {
                        logstr += i + ":" + datas[i] + ";";
                    }

                    writeLog.Write("分拣线一:" + logstr);
                    updateListBox("分拣线一:" + logstr);

                    taskgroup1.SyncWrite(datas);  
                    //写完db块后,再读出来 
                    String p1 = "";
                    for (int i = 0; i <= 225; i = i + 9)
                    {
                        p1 += taskgroup1.ReadD(i).ToString() + ";";//pokeid   
                    }
                    writeLog.Write("读出第一组电控写入值:" + p1);
                    
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
        /// 六组烟柜
        /// </summary>
        void sendSixCabinetTask()
        { 
            try
            {
                int flag = SendTaskStatesGroup.ReadD(2).CastTo<int>(-1);
                writeLog.Write("烟柜发送数据前读标志位：" + flag);
                if (flag == 2)
                {
                   
                 //   string linenum = UnPokeService.getSixCabinetLineNum();//烟柜分拣线
                    object[] datas = UnPokeService.getSixCabinetTask(25, "1", out listSix);
                    if (int.Parse(datas[0].ToString()) == 0)
                    {
                        updateListBox("烟柜分拣数据发送完毕");
                        return;
                    }
                    string logstr = "";
                    for (int i = 0; i < datas.Length; i++)
                    {
                        logstr += i + ":" + datas[i] + ";";
                    }
                    writeLog.Write("烟柜分拣发送数据:" + logstr);
                    updateListBox("烟柜分拣发送数据:" + logstr);
                    //写电控
                    SixCabinetGroup.SyncWrite(datas); 
                    //读电控
                    String p1 = "";
                    for (int i = 0; i <= 225; i = i + 9)
                    {
                        p1 += SixCabinetGroup.ReadD(i).ToString()+";";//pokeid  
                    }
                    writeLog.Write("读出烟柜电控写入值:" + p1);
                  
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
                sendSixCabinetTask();//异常后重新发送
            }
        }

        public static Object lockFlag = new Object();
        public void OnDataChange(int group, int[] clientId, object[] values)
        {
            if (group == 1)// 暂时没用 被监控标志取代 
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    if (clientId[i] == 226)
                    {

                        if (values[i] != null && int.Parse(values[i].ToString()) == 2)
                        {
                            while (!isInit)
                            {
                                Thread.Sleep(100);
                            }
                            String logstr = "";
                            foreach (var item in list)
                            {
                                logstr += item.POKEID + ";";
                            }
                            if (logstr != null && logstr.Length > 0)
                            {
                                writeLog.Write("第一组任务号:" + logstr + "已接收");
                                updateListBox("第一组任务号:" + logstr + "已接收");
                                UnPokeService.UpdateTask(list, 15);
                                UnPokeService.UpdateStroageInout(list);
                            }
                            sendTask1();
                        }
                        break;
                    }
                }
            }
            else if (group == 2)//  暂时没用 被监控标志取代 
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    if (clientId[i] == 226)
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
                                writeLog.Write("第二组任务号:" + logstr + "已接收");
                                updateListBox("第二组任务号:" + logstr + "已接收");
                                UnPokeService.UpdateTask(list1, 15);
                                UnPokeService.UpdateStroageInout(list1);
                            }
                            sendTask2();
                        }
                        break;
                    }
                }
            }
            else if (group == 5)
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
                        }
                    }

                }
            }
            else if (group == 6)//六组烟柜 暂时没用 被监控标志取代 
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    if (clientId[i] == 226)//一个任务
                    {
                        if (values[i] != null && int.Parse(values[i].ToString()) == 2)
                        {
                            while (!isInit)
                            {
                                Thread.Sleep(100);
                            }
                            String logstr = "";
                            foreach (var item in listSix)
                            {
                                logstr += item.POKEID + ";";
                            }
                            if (logstr != null && logstr.Length > 0)
                            {
                                writeLog.Write("烟柜任务号:" + logstr + "已接收");
                                updateListBox("烟柜任务号:" + logstr + "已接收");
                                UnPokeService.UpdateTask(listSix, 15);
                                // UnPokeService.UpdateStroageInout(listSix);
                            }
                            sendSixCabinetTask();
                        }
                        break;
                    }
                }
            }
            else if (group == 7)//完成任务号处理
            {
                for (int i = 0; i < clientId.Length; i++)
                { 
                    int tempvalue = int.Parse((values[i].ToString()));
                    if (tempvalue >= 1)
                    {
                       writeLog.Write("从异形烟线：" + clientId[i] + "获取到完成任务号:" + tempvalue );
                        try
                        {
                            UnPokeService.UpdateunTask(tempvalue, 20);//根据异形烟整包任务号更新poke表中状态 
                            writeLog.Write("数据库更新完成");
                        }
                        catch (Exception ex)
                        {
                            writeLog.Write("任务号" + tempvalue + ";数据库更新异形烟完成状态位失败: " + ex.Message);
                            updateListBox("任务号" + tempvalue + ";数据库更新异形烟完成状态位失败: " + ex.Message);
                        }
                        if (tempvalue != 0)
                        {
                            updateListBox("异形烟线：" + clientId[i]+" 任务号:" + tempvalue + "数据库状态已置完成");
                            writeLog.Write("异形烟线：" + clientId[i] + " 任务号:" + tempvalue + "数据库状态已置完成");
                        }
                        FinishSignalGroup.Write(0, clientId[i] - 1);
                        //removeKey(clientId[i]);
                        this.task_data.BeginInvoke(new Action(() => { initdata(); }));

                        //}

                    }
                }
            }
            else if (group == 8)//监控发送状态位 1为上位写 2为电控已接收
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    if (clientId[i] == 1)//第一组
                    {

                        if (values[i] != null && int.Parse(values[i].ToString()) == 2)
                        {
                            while (!isInit)
                            {
                                Thread.Sleep(100);
                            }
                            String logstr = "";
                            foreach (var item in list)
                            {
                                logstr += item.POKEID + ";";
                            }
                            if (logstr != null && logstr.Length > 0)
                            {
                                writeLog.Write("第一组任务号:" + logstr + "已接收");
                                updateListBox("第一组任务号:" + logstr + "已接收");
                                UnPokeService.UpdateTask(list, 15);
                                UnPokeService.UpdateStroageInout(list);
                            }
                            sendTask1();
                        } 
                    } 
                    if (clientId[i] == 2)//第二组
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
                                writeLog.Write("第二组任务号:" + logstr + "已接收");
                                updateListBox("第二组任务号:" + logstr + "已接收");
                                UnPokeService.UpdateTask(list1, 15);
                                UnPokeService.UpdateStroageInout(list1);
                            }
                            sendTask2();
                        }

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
                            foreach (var item in listSix)
                            {
                                logstr += item.POKEID + ";";
                            }
                            if (logstr != null && logstr.Length > 0)
                            {
                                writeLog.Write("烟柜任务号:" + logstr + "已接收");
                                updateListBox("烟柜任务号:" + logstr + "已接收");
                                UnPokeService.UpdateTask(listSix, 15);
                                // UnPokeService.UpdateStroageInout(listSix);
                            }
                            sendSixCabinetTask();
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
            if (taskGroup2 != null)
            {
                taskGroup2.Release();
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
                           this.task_data.Rows[index].Cells[8].Style = dgvStyle;
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
           this.task_data.BeginInvoke(new Action(() => { initdata(); }));
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
