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
        public WriteLog writeLog = new WriteLog();
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
                this.Close();
            }
         }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
         
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
        Group taskgroup,taskGroup1,statusGroup2,statusGroup3,errgroup,SixCabinetGroup;
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
                taskGroup1 = new Group(pIOPCServer, 2, "group1", 1, LOCALE_ID);
                statusGroup2 = new Group(pIOPCServer, 3, "group2", 1, LOCALE_ID);
                statusGroup3 = new Group(pIOPCServer, 4, "group3", 1, LOCALE_ID);
                errgroup = new Group(pIOPCServer, 5, "group5", 1, LOCALE_ID);

                //异形烟6个烟柜
                SixCabinetGroup = new Group(pIOPCServer, 6, "group6", 1, LOCALE_ID);
                SixCabinetGroup.addItem(ItemCollection.GetSixCabinetTaskItem());
                SixCabinetGroup.callback += OnDataChange;


                taskgroup.addItem(ItemCollection.GetTaskItem());
                taskgroup.callback += OnDataChange;
                errgroup.addItem(ItemCollection.GetTaskError());
                errgroup.callback += OnDataChange;


                taskGroup1.addItem(ItemCollection.GetTaskItem1());
                taskGroup1.callback += OnDataChange;
                statusGroup2.addItem(ItemCollection.GetTaskItem2());
                //statusGroup2.callback += OnDataChange;
                statusGroup3.addItem(ItemCollection.GetTaskItem3());
                //statusGroup3.callback += OnDataChange;
                checkConnection();
            
            }
            catch (Exception e)
            {
                updateListBox("连接服务器失败:"+e.Message);
            }
        }
        public void checkConnection()
        {
            int flag = taskgroup.Read(11).CastTo<int>(-1);
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
        List<T_UN_POKE> list = new List<T_UN_POKE>();
        List<T_UN_POKE> list1 = new List<T_UN_POKE>();
        List<T_UN_POKE> listSix = new List<T_UN_POKE>();//六组烟柜

        void sendTask1()
        {

            try
            {
                int flag = taskGroup1.Read(225).CastTo<int>(-1);
                writeLog.Write("二线发送数据前读标志位：" + flag);
                if (flag == 0)
                {
                    object[] datas = UnPokeService.getTask(25, "2", out list1);
                    if (int.Parse(datas[0].ToString())== 0)
                    {
                        updateListBox("二线分拣数据发送完毕");
                        return;
                    }
                    taskGroup1.SyncWrite(datas);
                    string logstr = "";
                    for (int i = 0; i < datas.Length; i++)
                    {
                        logstr += i + ":" + datas[i] + ";";
                    }
                    writeLog.Write("分拣线2:" + logstr);
                    updateListBox(logstr);
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
                int flag = taskgroup.Read(225).CastTo<int>(-1);
                writeLog.Write("一线发送数据前读标志位：" + flag);
                if (flag == 0)
                {
                    object[] datas = UnPokeService.getTask(25,"1",out list);
                    if (int.Parse(datas[0].ToString()) == 0)
                    {
                        updateListBox("一线分拣数据发送完毕");
                        return;
                    }
                 taskgroup.SyncWrite(datas);
                 string logstr = "";
                 for (int i = 0; i < datas.Length; i++)
                 {
                   logstr += i + ":" + datas[i] + ";";
                 }
                 writeLog.Write("分拣线一:"+logstr);
                 updateListBox(logstr);
                 }
            }
            catch(Exception ex)
            {
                writeLog.Write(ex.Message);
            }
        }
        /// <summary>
        /// 六组烟柜
        /// </summary>
        void sendSixCabinetTask()
        { 
            try
            {
                int flag = SixCabinetGroup.Read(225).CastTo<int>(-1);
                writeLog.Write("烟柜发送数据前读标志位：" + flag);
                if (flag == 0)
                {
                    object[] datas = UnPokeService.getSixCabinetTask(25, "1", out listSix);
                    if (int.Parse(datas[0].ToString()) == 0)
                    {
                        updateListBox("烟柜分拣数据发送完毕");
                        return;
                    }
                    SixCabinetGroup.SyncWrite(datas);
                    string logstr = "";
                    for (int i = 0; i < datas.Length; i++)
                    {
                        logstr += i + ":" + datas[i] + ";";
                    }
                    writeLog.Write("烟柜分拣发送数据:" + logstr);
                    updateListBox(logstr);
                }
            }
            catch (Exception ex)
            {
                writeLog.Write(ex.Message);
            }
        }
        public static Object lockFlag = new Object();
        public void OnDataChange(int group, int[] clientId, object[] values)
        {
            if (group == 1)
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    if (clientId[i] == 226)
                    {

                        if (values[i] != null && int.Parse(values[i].ToString()) == 0)
                        {

                            String logstr = "";
                            foreach (var item in list)
                            {
                                logstr += item.POKEID + ";";
                            }
                            writeLog.Write("第一组任务号:" + logstr + "已接收");
                            updateListBox("第一组任务号:" + logstr + "已接收");
                            UnPokeService.UpdateTask(list, 15);
                            UnPokeService.UpdateStroageInout(list);
                            sendTask();
                        }
                        break;
                    }
                }
            }
            else if (group == 2)
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    if (clientId[i] == 226)
                    {
                        if (values[i] != null && int.Parse(values[i].ToString()) == 0)
                        {
                            String logstr = "";
                            foreach (var item in list1)
                            {
                                logstr += item.POKEID + ";";
                            }
                            writeLog.Write("第二组任务号:" + logstr + "已接收");
                            updateListBox("第二组任务号:" + logstr + "已接收");
                            UnPokeService.UpdateTask(list1, 15);
                            UnPokeService.UpdateStroageInout(list1);
                            sendTask1();
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
            else if (group == 6)//六组烟柜
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    if (clientId[i] == 226)
                    {
                        if (values[i] != null && int.Parse(values[i].ToString()) == 0)
                        {
                            String logstr = "";
                            foreach (var item in listSix)
                            {
                                logstr += item.POKEID + ";";
                            }
                            writeLog.Write("烟柜任务号:" + logstr + "已接收");
                            updateListBox("烟柜任务号:" + logstr + "已接收");
                            UnPokeService.UpdateTask(listSix, 15);
                           // UnPokeService.UpdateStroageInout(listSix);
                            sendSixCabinetTask();
                        }
                        break;
                    }
                }
            }

        }


        public DeviceStateManager stateManager = new DeviceStateManager();
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
            if (taskGroup1 != null)
            {
                taskGroup1.Release();
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
        private void button10_Click(object sender, EventArgs e)
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
       public void initdata() {
           writeLog.Write("initdata");
           task_data.Rows.Clear();
           try
           {
               List<TaskInfo> list =  TaskService.GetUNCustomer();
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
                       this.task_data.Rows[index].Cells[6].Value = row.LineNum;
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

       }
      
     
    }
}
