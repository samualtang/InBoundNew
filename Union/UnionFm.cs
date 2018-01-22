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
        public UnionFm()
        {
            InitializeComponent();
            updateListBox("应用程序启动");
           // TaskService.GetUnionTask();
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
                updateListBox(string.Format("{0}号设备发生故障，故障名称：{1}", obj.DeviceNo, obj.ErrInfo));
            };
            stateManager.OnGetErr += (i) =>
            {
                return getErrMsg(i);
            };
            
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            tempList = TaskService.initunionTask();
            TaskService.GetUnionTask();
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
        Group taskgroup,statusGroup1,statusGroup2,statusGroup3,statusGroup4,statusGroup5;
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
                taskgroup.addItem(ItemCollection.GetTaskStatusItem10());
                taskgroup.callback += OnDataChange;
                statusGroup1.addItem(ItemCollection.GetTaskStatusItem11());
                statusGroup1.callback += OnDataChange;
                statusGroup2.addItem(ItemCollection.GetTaskStatusItem12());
                statusGroup2.callback += OnDataChange;
                statusGroup3.addItem(ItemCollection.GetTaskStatusItem3());
                statusGroup3.callback += OnDataChange;
                statusGroup4.addItem(ItemCollection.GetTaskStatusItem4());
                statusGroup4.callback += OnDataChange;
                checkConnection();
            
            }
            catch (Exception e)
            {
                updateListBox("连接服务器失败:"+e.Message);
            }
        }
        public void checkConnection()
        {
            int flag = taskgroup.Read(12).CastTo<int>(-1);
            if (flag == -1)
            {
                updateListBox("连接服务器失败,请检查网络.");
            }
            else
            {
                updateListBox("连接服务器成功......");
                updateControlEnable(false, button10);
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
        void sendTask()
        {
            
            try
            {
                int flag = taskgroup.Read(12).CastTo<int>(-1);
                writeLog.Write("标志位：" + flag);
                if (flag == 0)
                {
                   
                    object[] datas = TaskService.GetUnionTask();
                   
                    if (int.Parse(datas[0].ToString()) == 0)
                    {
                        updateListBox("合流数据发送完毕");
                        return;
                    }
                    int export = int.Parse(datas[1].ToString());
                    //if (CheckCanSend(export))
                    //{
                       
                        taskgroup.SyncWrite(datas);
                        string logstr = "";
                        for (int i = 0; i < datas.Length; i++)
                        {
                            logstr += i + ":" + datas[i] + ";";
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
        String[] errMsgList = { "", "", "", "", "", "编码器故障", "手动选中", "反转", "单台电机故障", "空开故障", "接触器/变频器故障", "急停（SF9）", "立烟", "气缸升超时", "气缸降超时", "运行信号" };
        public string getErrMsg(int len)
        {
            return errMsgList[len];
        }
        public void OnDataChange(int group,int[] clientId, object[] values)
        {
            if (group == 1)
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    if (clientId[i] == 13)
                    {
                        if (values[i] != null && int.Parse(values[i].ToString()) == 0)
                        {
                            
                            if (tempList.Count > 0)
                            {
                                updateListBox("任务:" + tempList.ElementAt(tempList.Count - 1).Value + "已接收");
                                TaskService.UpdateUnionStatus( 15, tempList.ElementAt(tempList.Count - 1).Value);
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
                            writeLog.Write("出口号：" + clientId[i] + ";任务号:" + tempvalue);

                            TaskService.UpdateUnionStatus( 20, tempvalue);

                            if (tempvalue != 0)
                            {
                                updateListBox("任务:" + tempvalue + "已完成");
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
       private void w_SortingControlMain_FormClosing(object sender, FormClosingEventArgs e)
       {
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
       public void exitFunc() { 
       
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
         
       }
       
     
    }
}
