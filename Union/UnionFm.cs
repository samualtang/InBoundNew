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
using Union;
using InBound;
using Machine;

namespace SortingControlSys.SortingControl
{
    public partial class UnionFm : Form
    {

       
        /* Constants */
        internal const string SERVER_NAME = "OPC.SimaticNET";       // local server name
      
        internal const string GROUP_NAME = "grp1";                  // Group name
        internal const int LOCALE_ID = 0x409;                       // LOCALE FOR ENGLISH.

        /* Global variables */
        IOPCServer pIOPCServer;  //定义opcServer对象
        public WriteLog writeLog = new WriteLog();
        DeviceStateManager stateManager = new DeviceStateManager();
        Alarms alarms = new Alarms();
        public UnionFm()
        {
            InitializeComponent();
            updateListBox("应用程序启动");
            writeLog.Write("应用程序启动");
           // TaskService.GetUnionTask();
            this.Text = "长株潭烟草公司合流信息系统      Version:" + ConfigurationManager.AppSettings["Version"].ToString(); 
           
            alarms.AlarmsHandler += (obj) =>
            {
                updateListBox(string.Format("{0}号设备发生故障，故障名称：{1}", obj.DeviceNo, obj.ErrInfo), listError);
            };
            try
            {
              // initdata();
                ItemCollection.OpcUnionServer = ConfigurationManager.AppSettings["OpcUnionServer"].ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show("请检查一下数据网络,在重新打开系统");
                this.Close();
            }
            stateManager.AlarmsHandler += (obj) =>
            {
                updateListBox(string.Format("{0}号设备发生故障，故障名称：{1}", obj.DeviceNo, obj.ErrInfo), listError);
            };
            stateManager.OnGetErr += (i) =>
            {
                return getErrMsg(i);
            };
            
        }
        private delegate void HandleDelegateError(string strshow, ListBox list);
        public void updateListBox(string info, ListBox list)
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
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            tempList = TaskService.initunionTask();
           // TaskService.GetUnionTask(0);
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
            //TaskService.GetUnionTask();
            Connect();
        }
        Group taskgroup,statusGroup1,statusGroup2,statusGroup3,statusGroup4,statusGroup5,errorGroup,SendTaskStatesGroup;
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            DialogResult MsgBoxResult = MessageBox.Show("确定要退出程序?",//对话框的显示内容 
                                                             "操作提示",//对话框的标题  
                                                             MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                             MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                             MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
            //Console.WriteLine(MsgBoxResult);
            if (MsgBoxResult == DialogResult.Yes)
            {
                Disconnect();


                Disconnect();
                updateListBox("system exit.......");
                writeLog.Write("退出程序。。。。。。");
                this.Dispose();
                this.Close();
                System.Environment.Exit(System.Environment.ExitCode);
            }
            else
            {
                e.Cancel = true;
            }


        }
        public void Connect()
        {
            Type svrComponenttyp;
            Guid iidRequiredInterface = typeof(IOPCItemMgt).GUID;
            svrComponenttyp = Type.GetTypeFromProgID(SERVER_NAME);
            try
            {
                // Connect to the local server.
                pIOPCServer = (IOPCServer)Activator.CreateInstance(svrComponenttyp);
                taskgroup = new Group(pIOPCServer, 1, "group", 1, LOCALE_ID);
                statusGroup1 = new Group(pIOPCServer, 2, "group1", 1, LOCALE_ID);
                statusGroup2 = new Group(pIOPCServer, 3, "group2", 1, LOCALE_ID);
                statusGroup3 = new Group(pIOPCServer, 4, "group4", 1, LOCALE_ID);
                statusGroup4 = new Group(pIOPCServer, 5, "group5", 1, LOCALE_ID);
                errorGroup = new Group(pIOPCServer, 6, "group6", 1, LOCALE_ID);

                SendTaskStatesGroup = new Group(pIOPCServer,7, "group7", 1, LOCALE_ID);//监控标志位   2 为可接收  
                SendTaskStatesGroup.addItem(ItemCollection.GetSendTaskStatesItem());

                errorGroup.addItem(ItemCollection.GetTaskErrStatusItem());
              
                taskgroup.addItem(ItemCollection.GetTaskStatusItem10());
               
                statusGroup1.addItem(ItemCollection.GetTaskStatusItem11());
             
                statusGroup2.addItem(ItemCollection.GetTaskStatusItem12());
              
                statusGroup3.addItem(ItemCollection.GetTaskStatusItem3());
             
                statusGroup4.addItem(ItemCollection.GetTaskStatusItem4());
               
                checkConnection();
            
            }
            catch (Exception e)
            {
                updateListBox("连接服务器失败:"+e.Message);
                writeLog.Write("连接服务器失败:" + e.Message);
            }
        }

        public void regDataChange()
        {
            SendTaskStatesGroup.callback += OnDataChange;//监控标志位
            errorGroup.callback += OnDataChange;
            //taskgroup.callback += OnDataChange;//合流信息
            statusGroup1.callback += OnDataChange;
            statusGroup2.callback += OnDataChange; 
            statusGroup3.callback += OnDataChange;
            statusGroup4.callback += OnDataChange;
        }
        /// <summary>
        /// 给PLC写入 主皮带 初始值
        /// </summary>
        public void FristTimeToPLC()
        {
            string log = ""; 
            object[] datas = TaskService.FristTime();
            taskgroup.SyncWrite(datas[0],17);//给PLC写入 主皮带 初始值
            taskgroup.SyncWrite(datas[1], 18);
            taskgroup.SyncWrite(datas[2], 19);
            taskgroup.SyncWrite(datas[3], 20);
            for (int i = 0; i < 4; i++)
            {
                log += i + "号" + datas[i] + " 状态 ,";
            }
            writeLog.Write("主皮带初始值为:" + log); 
        }
        public void checkConnection()
        {
          
            int flag = SendTaskStatesGroup.Read(0).CastTo<int>(-1);
            if (flag == 0 || flag == 2)
            {
                FristTimeToPLC();//初始值在跳变之前
                SendTaskStatesGroup.Write(5, 0);//先写5,为了产生跳变
                SendTaskStatesGroup.Write(2,0);//初始化状态为可接收状态 
            }
            if (flag == -1)
            {
                updateListBox("连接服务器失败,请检查网络.");
            }
            else
            {
                updateListBox("连接服务器成功......");
                writeLog.Write("连接服务器成功......数据初始化成功!"  );
               
                updateControlEnable(false, button10);
            }

            regDataChange();
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
                lock (lockFlag)
                {
                    foreach (var item in tempList)
                    {
                        i++;
                        if (item.Key == export)
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
        }

        public int getKey(int export)
        {
            int i = -1;
            if (tempList != null)
            {
                lock (lockFlag)
                {
                    foreach (var item in tempList)
                    {
                        if (item.Key == export)
                        {
                            i = item.Value;
                            break;
                        }
                    }
                }
            }
            return i;
        }
        delegate void delSendTask();
      //  int count =1;//记数
        /// <summary>
        /// 合流数据
        /// </summary>
        void sendTask()
        {
            try
            { 
                //Thread.Sleep(500);//0.5
                int flag = SendTaskStatesGroup.Read(0).CastTo<int>(-1);
                
                if (flag == -1)
                {
                    writeLog.Write("与PLC连接异常,请检查网络");
                    updateListBox("与PLC连接异常,请检查网络");
                }
                int mainbelt = 0;
                int tempmainbelte =0;
                if (flag == 2)//接收信号   现在改为 2 
                {
                    /////////////////////////////////////////////////////////////////////////
                    //先找出上位禁用的主皮带
                    //找到哪根主皮带可以接收任务,为0表示可以接收任务
                    string banbelte = TaskService.GetBanMainBelt();//  任务为0被禁用主皮带 和 已禁用主皮带 
                        for (int i = 1; i < 5; i++)
                        {
                            if (banbelte.Contains(i.ToString())) 
                            { 
                                continue;
                            }
                            tempmainbelte = taskgroup.Read(12 + i).CastTo<int>(-1); 
                            if (tempmainbelte == 1)//1 为主皮带启用  0 为为主皮带禁止
                            {
                                mainbelt = i;
                                break;
                            } 
                        }
                    object[] datas = TaskService.GetUnionTask(mainbelt); 
                    /////////////////////////////////////////////// 
                    if (int.Parse(datas[0].ToString()) == 0)
                    {
                        updateListBox("合流数据发送完毕");
                        writeLog.Write("合流数据发送完毕");

                        return;
                    }
                    int export = int.Parse(datas[1].ToString());
                    //if (CheckCanSend(export))
                    //{
                       
                        taskgroup.SyncWrite(datas);
                        string logstr = "任务信息：";// +count++;
                        string f = "";
                        for (int i = 0; i < datas.Length; i++)
                        {
                            if (i == 0) f = "任务号";
                            else if (i == 1) f = "出口号";
                            else if (i == 2) f = "包装机号";
                            else if (i == 3) f = "总条数";
                            else if (i == 5) f = "2号机械手抓烟数";
                            else if (i == 4) f = "1号机械手抓烟数";
                            else if (i == 6) f = "3号机械手抓烟数";
                            else if (i == 7) f = "4号机械手抓烟数";
                            else if (i == 8) f = "5号机械手抓烟数";
                            else if (i == 9) f = "6号机械手抓烟数";
                            else if (i == 10) f = "7号机械手抓烟数";
                            else if (i == 11) f = "8号机械手抓烟数";
                            else if (i == 12) f = "标志位";
                            else if (i == 13) f = "电控一号主皮带是否可接受任务状态";
                            else if (i == 14) f = "电控二号主皮带是否可接受任务状态";
                            else if (i == 15) f = "电控三号主皮带是否可接受任务状态";
                            else if (i == 16) f = "电控四号主皮带是否可接受任务状态";
                            else if (i == 17) f = "上位一号主皮带禁用状态";
                            else if (i == 18) f = "上位二号主皮带禁用状态";
                            else if (i == 19) f = "上位三号主皮带禁用状态";
                            else if (i == 20) f = "上位四号主皮带禁用状态";
                            logstr += f + ":" + datas[i] + ";";
                        }
                        writeLog.Write(logstr);
                        updateListBox(logstr);
                        removeKey(export);
                        tempList.Add(new KeyValuePair<int, int>(export, int.Parse(datas[0].ToString())));
                    //}
                   // else
                    //{
                    //    Thread.Sleep(1000);
                    //    sendTask();
                   // }
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
                sendTask();//异常后重新发送
            }
        }
        public static Object lockFlag = new Object();
        public void WriteErr(int type, int len, String temp, decimal GroupNo)
        {
            String deviceNo = "";
            if (type == 1)
            {
                deviceNo = "C" + len;
            }
            else
            {
                deviceNo = "E" + len;
            }
            lock (lockFlag)
            {
                stateManager.WriteErrWithCheck(deviceNo, Convert.ToInt32(GroupNo), temp.Length > 16 ? temp.Substring(0, 15) : temp, 2);
            }
            //for (int i = 0; i < temp.Length; i++)
            //{
            //    if (temp.ElementAt(i) == '1')
            //    {
            //        String errMsg = getErrMsg(i);
            //        ErrListService.Add(deviceNo, GroupNo, 20, errMsg);

            //    }
            //}


        }
        String[] errMsgList = {"", "", "", "", "", "编码器故障", "手动选中", "反转","单台电机故障", "空开故障", "接触器/变频器故障", "急停（SF9）", "立烟", "气缸升超时", "气缸降超时", "运行信号"  };
        public string getErrMsg(int len)
        {
            return errMsgList[len];
        }
        public void OnDataChange(int group,int[] clientId, object[] values)
        {
            if (group == 1)//暂时没用
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    if (clientId[i] == 13)
                    {
                        if (values[i] != null && int.Parse(values[i].ToString()) == 0)
                        {
                            
                            if (tempList.Count > 0)
                            {                                                               
                                TaskService.UpdateUnionStatus( 15, tempList.ElementAt(tempList.Count - 1).Value);
                                updateListBox("任务:" + tempList.ElementAt(tempList.Count - 1).Value + "已接收");
                                writeLog.Write("任务号:" + tempList.ElementAt(tempList.Count - 1).Value + "已接收");
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
            else if (group == 3)
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    int tempvalue=int.Parse((values[i].ToString()));
                    if (tempvalue >= 1)
                    {

                        // if (getKey(clientId[i])!=-1)
                        // {
                        //  int taskno = getKey(clientId[i]);
                        writeLog.Write("从电控出口号：" + clientId[i] + "获取到任务号:" + tempvalue + "完成信号 ");
                        try
                        {
                            TaskService.UpdateUnionStatus(20, tempvalue);
                        }
                        catch (Exception ex)
                        {
                            writeLog.Write("数据库更新合流状态位失败: " + ex.Message);
                            updateListBox("数据库更新合流状态位失败: " + ex.Message);
                        }

                        if (tempvalue != 0)
                        {
                            updateListBox("任务:" + tempvalue + "数据库状态已置完成");
                            writeLog.Write("合流任务号:" + tempvalue + "数据库状态已置完成");
                        }
                        statusGroup2.Write(0, clientId[i] - 1);
                        removeKey(clientId[i]);
                        this.task_data.BeginInvoke(new Action(() => { initdata(); }));

                        //}

                    }
                }
            }
            else if (group == 4)
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    //if (clientId[i] == 1)
                    //{
                    if (values[i]!=null && int.Parse(values[i].ToString()) != 0)
                    {
                        String temp = Convert.ToString(int.Parse(values[i].ToString()), 2);
                        WriteErr(1, clientId[i], temp, 0);
                    }
                    //}
                }
            }
            else if (group == 5)
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    //if (clientId[i] == 1)
                    //{
                    if (values[i] != null && int.Parse(values[i].ToString()) != 0)
                    {
                        String temp = Convert.ToString(int.Parse(values[i].ToString()), 2);
                        WriteErr(2, clientId[i], temp, 0);
                    }
                    //}
                }
            }
            else if (group == 6)
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                  
                    String temp = Convert.ToString(int.Parse(values[i].ToString()), 2);
                    
                    alarms.WriteErrWithCheck(1, clientId[i], temp, 1);
                 
                }
            }
            else if (group == 7)//监控标志位
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    if (clientId[i] == 1)
                    {
                        if (values[i] != null && int.Parse(values[i].ToString()) == 2)//接收
                        {

                            if (tempList.Count > 0)
                            {
                                TaskService.UpdateUnionStatus(15, tempList.ElementAt(tempList.Count - 1).Value);
                                updateListBox("任务:" + tempList.ElementAt(tempList.Count - 1).Value + "已接收");
                                writeLog.Write("任务号:" + tempList.ElementAt(tempList.Count - 1).Value + "已接收");
                            } 
                            sendTask();
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
            if (taskgroup != null)
            {
                taskgroup.Release();
            }
            if (statusGroup1 != null)
            {
                statusGroup1.Release();
            }
            if (statusGroup2 != null)
            {
                statusGroup2.Release();
            }
            if (statusGroup3 != null)
            {
                statusGroup3.Release();
            }
            if (statusGroup4 != null)
            {
                statusGroup4.Release();
            }
            if (statusGroup5 != null)
            {
                statusGroup5.Release();
            }
            if (SendTaskStatesGroup != null)
            {
                SendTaskStatesGroup.Release();
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
           // TaskService.GetUnionTask();
            //LaneWayService.AddEntity();
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
       int i = 1;
       public void initdata() {
           //writeLog.Write("启动程序。。。。。。");
           task_data.Rows.Clear();
           try
           {
              List<TaskInfo> list = TaskService.GetCustomer();
               if (list != null)
               {

                   foreach (var row in list)
                   {
                       int index = this.task_data.Rows.Add();

                       this.task_data.Rows[index].Cells[0].Value = row.REGIONCODE;
                       this.task_data.Rows[index].Cells[1].Value = row.REGIONCODE;
                       this.task_data.Rows[index].Cells[2].Value = row.FinishCount + "/" + row.Count;
                       this.task_data.Rows[index].Cells[3].Value = row.FinishCount + "/" + row.Count;
                       this.task_data.Rows[index].Cells[4].Value = row.FinishQTY + "/" + row.QTY;
                       this.task_data.Rows[index].Cells[5].Value = row.Rate;
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
                    this.task_data.Rows[indexj].Cells[5].Value = percent+"%";

                    Console.WriteLine(boxc + "/" + boxcount[1]);
                    Console.WriteLine(cusc + "/" + cuscount[1]);
                    Console.WriteLine(finish + "/" + finishqty[1]);
                    Console.WriteLine(percent + "%");
                }
                
            }
       }
       
       public void exitFunc() { 
       
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

       private void button6_Click_1(object sender, EventArgs e)
       {
           w_pass pass = new w_pass();


           pass.Show();
       }
       
     
    }
}
