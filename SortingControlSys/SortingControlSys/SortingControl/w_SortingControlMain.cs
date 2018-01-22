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

        Decimal sortgroupno1 = 1;//定义组次
        Decimal sortgroupno2 = 2;
        public WriteLog writeLog = new WriteLog();
        //ClearCmd clearACmd, clearBCmd;
        private bool AlineStopFlag, BlineStopFlag = false;
        DeviceStateManager stateManager = new DeviceStateManager();
        public w_SortingControlMain()
        {
            InitializeComponent();
            updateListBox("应用程序启动");
            try
            {
                sortgroupno1 = decimal.Parse(ConfigurationManager.AppSettings["Group1"].ToString());
                sortgroupno2 = decimal.Parse(ConfigurationManager.AppSettings["Group2"].ToString());
                ItemCollection.OpcPresortServer = ConfigurationManager.AppSettings["OpcPresortServer"].ToString();

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

            this.task_data.BeginInvoke(new Action(() => { initdata(); }));
            if (tempList == null)
                tempList = new List<KeyValuePair<int, int>>();

        }
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
        Group taskgroup, statusGroup1, statusGroup2, statusGroup3, statusGroup4, statusGroup5;
        Group errGroup1, errGroup2, errGroup3, errGroup4;
        Group ClearCacheGroup;
        public void Connect()
        {
            Type svrComponenttyp;
            Guid iidRequiredInterface = typeof(IOPCItemMgt).GUID;
            svrComponenttyp = Type.GetTypeFromProgID(SERVER_NAME);
            try
            {
                // Connect to the local server.
                pIOPCServer = (IOPCServer)Activator.CreateInstance(svrComponenttyp);
                taskgroup = new Group(pIOPCServer, 1, "group", 1, LOCALE_ID);//组号由这里定义
                statusGroup1 = new Group(pIOPCServer, 2, "group1", 1, LOCALE_ID);
                statusGroup2 = new Group(pIOPCServer, 3, "group2", 1, LOCALE_ID);
                statusGroup3 = new Group(pIOPCServer, 4, "group3", 1, LOCALE_ID);
                statusGroup4 = new Group(pIOPCServer, 5, "group4", 1, LOCALE_ID);
                statusGroup5 = new Group(pIOPCServer, 6, "group5", 1, LOCALE_ID);
                errGroup1 = new Group(pIOPCServer, 7, "group7", 1, LOCALE_ID);
                errGroup2 = new Group(pIOPCServer, 8, "group8", 1, LOCALE_ID);
                errGroup3 = new Group(pIOPCServer, 9, "group9", 1, LOCALE_ID);
                errGroup4 = new Group(pIOPCServer, 10, "group10", 1, LOCALE_ID);
                ClearCacheGroup = new Group(pIOPCServer, 11, "group11", 1, LOCALE_ID);
                taskgroup.addItem(ItemCollection.GetTaskItem());
                taskgroup.callback += OnDataChange;
                statusGroup1.addItem(ItemCollection.GetTaskStatusItem1());
                statusGroup1.callback += OnDataChange;
                statusGroup2.addItem(ItemCollection.GetTaskStatusItem2());//拨烟完成
                statusGroup2.callback += OnDataChange;
                statusGroup3.addItem(ItemCollection.GetTaskStausItemGroup());
                statusGroup3.callback += OnDataChange;

                statusGroup4.addItem(ItemCollection.GetTaskStatusSECItem1());
                statusGroup4.callback += OnDataChange;
                statusGroup5.addItem(ItemCollection.GetTaskStatusSECItem2());
                statusGroup5.callback += OnDataChange;
                errGroup1.addItem(ItemCollection.GetTaskStatusItem3());
                errGroup1.callback += OnDataChange;
                errGroup2.addItem(ItemCollection.GetTaskStatusItem4());
                errGroup2.callback += OnDataChange;
                errGroup3.addItem(ItemCollection.GetTaskStatusSECItem3());
                errGroup3.callback += OnDataChange;
                ClearCacheGroup.addItem(ItemCollection.GetClearTaskItem());
                // ClearCacheGroup.callback += OnDataChange;
                //errGroup4.addItem(ItemCollection.GetTaskStatusSECItem4());
                //errGroup4.callback += OnDataChange;

                checkConnection();
                // sendTask();

            }
            catch (Exception e)
            {
                updateListBox("连接服务器失败:" + e.Message);
            }
        }
        public void checkConnection()
        {
            int flag = taskgroup.Read(25).CastTo<int>(-1);
            if (flag == -1)
            {
                updateListBox("连接服务器失败,请检查网络.");
            }
            else
            {
                taskgroup.Write(2, 26);
                statusGroup3.Write(2, 26);
                updateListBox("连接服务器成功......");
                updateControlEnable(false, button10);
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
                foreach (var item in list)
                {
                    if (item.Key == export)
                    {
                        i = item.Value;
                        break;
                    }
                }
            }
            return i;
        }
        delegate void delSendTask();
        void sendTask1()
        {
            try
            {
                int flag = statusGroup3.Read(26).CastTo<int>(-1);//读任务写入标志
                writeLog.Write("标志位：" + flag);
                if (flag == 2)//0：已取走， 1：已写入
                {

                    object[] datas = TaskService.GetSortTask(sortgroupno2);//数据

                    if (int.Parse(datas[0].ToString()) == 0)//已经没有数据可发送了，datas[0]是任务号
                    {
                        updateListBox(sortgroupno2 + "组分拣数据发送完毕");
                        return;
                    }
                    int export = int.Parse(datas[1].ToString());//取虚拟出口号
                    //if (CheckCanSend(export))//和电控交互该出口号是否能用
                    //{

                    statusGroup3.SyncWrite(datas);//写任务
                    string logstr = "";
                    for (int i = 0; i < datas.Length; i++)
                    {
                        logstr += i + ":" + datas[i] + ";";
                    }
                    writeLog.Write(logstr);
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
            }
        }
        void sendTask()
        {
            try
            {
                int flag = taskgroup.Read(26).CastTo<int>(-1);//读任务写入标志
                writeLog.Write("标志位：" + flag);
                if (flag == 2)//0：已取走， 1：已写入
                {

                    object[] datas = TaskService.GetSortTask(sortgroupno1);//数据

                    if (int.Parse(datas[0].ToString()) == 0)//已经没有数据可发送了，datas[0]是任务号
                    {
                        updateListBox(sortgroupno1 + "组分拣数据发送完毕");
                        return;
                    }
                    int export = int.Parse(datas[1].ToString());//取虚拟出口号
                    //if (CheckCanSend(export))//和电控交互该出口号是否能用
                    //{

                    taskgroup.SyncWrite(datas);//写任务

                    //String p2 = taskgroup.Read(1).ToString();
                    //String p3 = taskgroup.Read(2).ToString();
                    //if (p2 == datas[1].ToString())
                    //{

                    //}
                    //else
                    //{

                    //}
                    string logstr = "";
                    for (int i = 0; i < datas.Length; i++)
                    {
                        logstr += i + ":" + datas[i] + ";";
                    }
                    writeLog.Write(logstr);
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
            if (group == 1)//发送任务组
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    if (clientId[i] == 27)//监控写入标识位
                    {
                        if (values[i] != null && int.Parse(values[i].ToString()) == 2)//0是电控已经接收
                        {

                            if (tempList.Count > 0)
                            {

                                TaskService.UpdateStatus(sortgroupno1, 15, tempList.ElementAt(tempList.Count - 1).Value);//状态改为已发送
                                updateListBox("组" + sortgroupno1 + "---任务:" + tempList.ElementAt(tempList.Count - 1).Value + "已接收");
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
            else if (group == 3)//监控分拣任务完成信号
            {
                for (int i = 0; i < clientId.Length; i++)//"出口号：" + clientId[i] + ";任务号:" + taskno
                {
                    int tempvalue = int.Parse((values[i].ToString()));
                    if (tempvalue >= 1)//分拣完成
                    {
                        statusGroup1.Write(1, clientId[i] - 1);
                        if (getKey(tempList, clientId[i]) != -1)
                        {
                            // int taskno = getKey(tempList, clientId[i]);
                            writeLog.Write("出口号：" + clientId[i] + ";任务号:" + tempvalue);
                            InBoundService.UpdateInOut(tempvalue, sortgroupno1);
                            TaskService.UpdateStatus(sortgroupno1, 20, tempvalue);//将第一组分拣任务改为完成完成

                            if (tempvalue != 0)
                            {
                                updateListBox("任务:" + tempvalue + "已完成");
                            }

                            removeKey(tempList, clientId[i]);
                            this.task_data.BeginInvoke(new Action(() => { initdata(); }));//异步调用，刷新分拣页面的分拣进度

                        }

                    }
                    else
                    {
                        statusGroup1.Write(0, clientId[i] - 1);
                    }
                }
            }
            else if (group == 4)
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    if (clientId[i] == 27)//监控写入标识位
                    {
                        if (values[i] != null && int.Parse(values[i].ToString()) == 2)//0是电控已经接收
                        {

                            if (tempList1.Count > 0)
                            {

                                TaskService.UpdateStatus(sortgroupno2, 20, tempList1.ElementAt(tempList1.Count - 1).Value);//状态改为已发送
                                updateListBox("组" + sortgroupno2 + "---任务:" + tempList1.ElementAt(tempList1.Count - 1).Value + "已接收");
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
                        if (getKey(tempList1, clientId[i]) != -1)
                        {
                            // int taskno = getKey(tempList1, clientId[i]);
                            writeLog.Write("出口号：" + clientId[i] + ";任务号:" + tempvalue);
                            InBoundService.UpdateInOut(tempvalue, sortgroupno2);
                            TaskService.UpdateStatus(sortgroupno2, 30, tempvalue);//将第一组分拣任务改为完成完成


                            if (tempvalue != 0)
                            {
                                updateListBox("任务:" + tempvalue + "已完成");
                            }

                            removeKey(tempList1, clientId[i]);
                            this.task_data.BeginInvoke(new Action(() => { initdata(); }));//异步调用，刷新分拣页面的分拣进度

                        }

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
            { }
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
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //TaskService.GetSortTask(1);

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
        int i = 1;
        public void initdata()
        {//刷新分拣进度等
            writeLog.Write("initdata");

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
                e.Cancel = true;
            }
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            Disconnect();

            this.Dispose();
            this.Close();
            System.Environment.Exit(System.Environment.ExitCode);
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
            w_SortingControlMainTest w_SortingControlMain = new w_SortingControlMainTest();

            //w_SortingControlMain.MdiParent = this;
            w_SortingControlMain.WindowState = FormWindowState.Maximized;
            w_SortingControlMain.Show();
            //this.Close();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            w_pass pass = new w_pass();


            pass.Show();


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            AlineStopFlag = ChangeFalg(!AlineStopFlag, 0, "A线", btnClear) == true ? true : false;
        }

        private void btnClearB_Click(object sender, EventArgs e)
        {
            BlineStopFlag = ChangeFalg(!BlineStopFlag, 1, "B线", btnClearB) == true ? true : false;
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
                btn.Text = string.Format("{0}恢复", lineName);
            }
            else
            {
                ClearCacheGroup.Write(0, index);
                isStop = false;
                btn.Text = string.Format("{0}停止", lineName);
            }
            return isStop;
        }
    }
}
