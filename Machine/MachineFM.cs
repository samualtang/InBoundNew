using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using Machine;


namespace SortingControlSys.SortingControl
{
    public partial class MachineFM : Form
    {


        /* Constants */
        internal const string SERVER_NAME = "OPC.SimaticNET";       // local server name

        internal const string Group_NAME = "grp1";                  // Group name
        internal const int LOCALE_ID = 0x409;                       // LOCALE FOR ENGLISH.

        /* Global variables */
        IOPCServer pIOPCServer;  //定义opcServer对象



        int writeCount = 2;

        decimal groupNo = 1;
        public WriteLog writeLog = new WriteLog();
        Dictionary<string, string> dicList = new Dictionary<string, string>();
        Alarms alarms = new Alarms();
        public MachineFM()
        {
            InitializeComponent();
            alarms.DowntimeHandler = OnDowntime;
            alarms.AlarmsHandler += (obj) =>
            {
                updateListBox(string.Format("{0}号设备发生故障，故障名称：{1}", obj.DeviceNo, obj.ErrInfo));
            };
            updateListBox("应用程序启动");
            groupNo = decimal.Parse(ConfigurationManager.AppSettings["GroupNO"].ToString());
            try
            {
                // initdata();

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
            //tempList = SortingFun.initTask();
            // TaskService.GetCigarette1(0, 22, 10, 20);
            // TaskService.UpdateInOut(347,0,22,10,20);
            //TaskService.GetUnionTask();

            alarms.WriteErrWithCheck(10, 9, "111010101010", 2);


            this.task_data.BeginInvoke(new Action(() => { initdata(); }));
            if (tempList == null)
                tempList = new List<KeyValuePair<String, List<String>>>();

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
        Group taskGroup1, taskGroup2, taskGroup3, taskGroup4, taskGroup5, taskGroup6, taskGroup7, taskGroup8, taskGroup9, taskGroup10;
        Group taskGroup11, taskGroup12, taskGroup13, taskGroup14, taskGroup15, taskGroup16, taskGroup17, taskGroup18, taskGroup19, taskGroup20;
        Group taskGroup21, taskGroup22;
        Group taskErrGroup;
        //taskGroup23, taskGroup24, taskGroup25, taskGroup26, taskGroup27, taskGroup28, taskGroup29, taskGroup30;
        //Group taskGroup31,  taskGroup32, taskGroup33, taskGroup34, taskGroup35, taskGroup36, taskGroup37, taskGroup38, taskGroup39, taskGroup40;
        //Group taskGroup41,  taskGroup42, taskGroup43, taskGroup44, taskGroup45, taskGroup46, taskGroup47, taskGroup48, taskGroup49, taskGroup50;
        //Group taskGroup51,  taskGroup52, taskGroup53, taskGroup54, taskGroup55, taskGroup56, taskGroup57, taskGroup58, taskGroup59, taskGroup60;
        //Group taskGroup61,  taskGroup62, taskGroup63, taskGroup64, taskGroup65, taskGroup66, taskGroup67, taskGroup68, taskGroup69, taskGroup70;
        //Group taskGroup71,  taskGroup72, taskGroup73, taskGroup74, taskGroup75, taskGroup76, taskGroup77, taskGroup78, taskGroup79, taskGroup80;
        //Group taskGroup81,  taskGroup82, taskGroup83, taskGroup84, taskGroup85, taskGroup86, taskGroup87, taskGroup88;
        List<Group> groupList = new List<Group>();
        public void Connect()
        {
            Type svrComponenttyp;
            Guid iidRequiredInterface = typeof(IOPCItemMgt).GUID;
            svrComponenttyp = Type.GetTypeFromProgID(SERVER_NAME);
            try
            {
                // Connect to the local server.
                pIOPCServer = (IOPCServer)Activator.CreateInstance(svrComponenttyp);
                taskErrGroup = new Group(pIOPCServer, 23, "Group0", 1, LOCALE_ID);
                taskErrGroup.addItem(ItemCollection.GetTaskErrStatusItem());
                taskGroup1 = new Group(pIOPCServer, 1, "Group1", 1, LOCALE_ID);
                taskGroup1.addItem(ItemCollection.GetTaskStatusItem1());

                taskGroup2 = new Group(pIOPCServer, 2, "Group2", 1, LOCALE_ID);
                taskGroup2.addItem(ItemCollection.GetTaskStatusItem2());

                taskGroup3 = new Group(pIOPCServer, 3, "Group3", 1, LOCALE_ID);
                taskGroup3.addItem(ItemCollection.GetTaskStatusItem3());

                taskGroup4 = new Group(pIOPCServer, 4, "Group4", 1, LOCALE_ID);
                taskGroup4.addItem(ItemCollection.GetTaskStatusItem4());

                taskGroup5 = new Group(pIOPCServer, 5, "Group5", 1, LOCALE_ID);
                taskGroup5.addItem(ItemCollection.GetTaskStatusItem5());

                taskGroup6 = new Group(pIOPCServer, 6, "Group6", 1, LOCALE_ID);
                taskGroup6.addItem(ItemCollection.GetTaskStatusItem6());

                taskGroup7 = new Group(pIOPCServer, 7, "Group7", 1, LOCALE_ID);
                taskGroup7.addItem(ItemCollection.GetTaskStatusItem7());

                taskGroup8 = new Group(pIOPCServer, 8, "Group8", 1, LOCALE_ID);
                taskGroup8.addItem(ItemCollection.GetTaskStatusItem8());

                taskGroup9 = new Group(pIOPCServer, 9, "Group9", 1, LOCALE_ID);
                taskGroup9.addItem(ItemCollection.GetTaskStatusItem9());

                taskGroup10 = new Group(pIOPCServer, 10, "Group10", 1, LOCALE_ID);
                taskGroup10.addItem(ItemCollection.GetTaskStatusItem10());

                taskGroup11 = new Group(pIOPCServer, 11, "Group11", 1, LOCALE_ID);
                taskGroup11.addItem(ItemCollection.GetTaskStatusItem11());

                taskGroup12 = new Group(pIOPCServer, 12, "Group12", 1, LOCALE_ID);
                taskGroup12.addItem(ItemCollection.GetTaskStatusItem12());

                taskGroup13 = new Group(pIOPCServer, 13, "Group13", 1, LOCALE_ID);
                taskGroup13.addItem(ItemCollection.GetTaskStatusItem13());

                taskGroup14 = new Group(pIOPCServer, 14, "Group14", 1, LOCALE_ID);
                taskGroup14.addItem(ItemCollection.GetTaskStatusItem14());

                taskGroup15 = new Group(pIOPCServer, 15, "Group15", 1, LOCALE_ID);
                taskGroup15.addItem(ItemCollection.GetTaskStatusItem15());

                taskGroup16 = new Group(pIOPCServer, 16, "Group16", 1, LOCALE_ID);
                taskGroup16.addItem(ItemCollection.GetTaskStatusItem16());

                taskGroup17 = new Group(pIOPCServer, 17, "Group17", 1, LOCALE_ID);
                taskGroup17.addItem(ItemCollection.GetTaskStatusItem17());

                taskGroup18 = new Group(pIOPCServer, 18, "Group18", 1, LOCALE_ID);
                taskGroup18.addItem(ItemCollection.GetTaskStatusItem18());

                taskGroup19 = new Group(pIOPCServer, 19, "Group19", 1, LOCALE_ID);
                taskGroup19.addItem(ItemCollection.GetTaskStatusItem19());

                taskGroup20 = new Group(pIOPCServer, 20, "Group20", 1, LOCALE_ID);
                taskGroup20.addItem(ItemCollection.GetTaskStatusItem20());

                taskGroup21 = new Group(pIOPCServer, 21, "Group21", 1, LOCALE_ID);
                taskGroup21.addItem(ItemCollection.GetTaskStatusItem21());

                taskGroup22 = new Group(pIOPCServer, 22, "Group22", 1, LOCALE_ID);
                taskGroup22.addItem(ItemCollection.GetTaskStatusItem22());
                groupList.Add(taskGroup1);
                groupList.Add(taskGroup2);
                groupList.Add(taskGroup3);
                groupList.Add(taskGroup4);
                groupList.Add(taskGroup5);
                groupList.Add(taskGroup6);
                groupList.Add(taskGroup7);
                groupList.Add(taskGroup8);
                groupList.Add(taskGroup9);
                groupList.Add(taskGroup10);
                groupList.Add(taskGroup11);
                groupList.Add(taskGroup12);
                groupList.Add(taskGroup13);
                groupList.Add(taskGroup14);
                groupList.Add(taskGroup15);
                groupList.Add(taskGroup16);
                groupList.Add(taskGroup17);
                groupList.Add(taskGroup18);
                groupList.Add(taskGroup19);
                groupList.Add(taskGroup20);
                groupList.Add(taskGroup21);
                groupList.Add(taskGroup22);
                taskGroup22.callback += OnDataChange;
                taskGroup21.callback += OnDataChange;
                taskGroup20.callback += OnDataChange;
                taskGroup19.callback += OnDataChange;
                taskGroup18.callback += OnDataChange;
                taskGroup17.callback += OnDataChange;
                taskGroup16.callback += OnDataChange;
                taskGroup15.callback += OnDataChange;
                taskGroup14.callback += OnDataChange;
                taskGroup13.callback += OnDataChange;
                taskGroup12.callback += OnDataChange;
                taskGroup11.callback += OnDataChange;
                taskGroup10.callback += OnDataChange;
                taskGroup9.callback += OnDataChange;
                taskGroup8.callback += OnDataChange;
                taskGroup7.callback += OnDataChange;
                taskGroup6.callback += OnDataChange;
                taskGroup5.callback += OnDataChange;
                taskGroup4.callback += OnDataChange;
                taskGroup3.callback += OnDataChange;
                taskGroup2.callback += OnDataChange;
                taskGroup1.callback += OnDataChange;
                taskErrGroup.callback += OnDataChange;
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
            int flag = taskGroup1.Read(0).CastTo<int>(-1);
            if (flag == -1)
            {
                updateListBox("连接服务器失败,请检查网络.");
            }
            else
            {
                updateListBox("连接服务器成功......");
                InitPlcData();
                updateControlEnable(false, button10);
            }
        }


        public void InitPlcData()
        {
            int i = 0;
            foreach (var item in groupList)
            {
                item.Write(2, 3);
                updateListBox("通道号:" + i + ";初始值:" + item.Read(3));
                i++;
            }
        }
        Boolean CheckCanSend(int targetPort)
        {
            writeLog.Write("出口号：" + targetPort);
            int value = taskGroup3.Read(targetPort - 1).CastTo<int>(-1);
            writeLog.Write(" value=" + value);
            if (value == 1)
            {
                return true;
            }
            else
                return false;
        }
        List<KeyValuePair<String, List<String>>> tempList = new List<KeyValuePair<String, List<String>>>();
        public static Object lockFlag = new Object();
        public void removeKey(String export)
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

                            // if (i != tempList.Count)
                            // {
                            tempList.Remove(item);
                            //  }
                            break;
                        }
                    }
                }
            }
        }
        public void removeKey(List<KeyValuePair<String, List<String>>> list, String export)
        {
            int i = 0;
            if (list != null)
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
        public List<String> getKey(String export)
        {
            List<String> i = null;
            if (tempList != null)
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
            return i;
        }
        delegate void delSendTask();
        void sendTask(String exportnum, Group group)
        {


            try
            {
                int flag = group.Read(3).CastTo<int>(-1);
                writeLog.Write("exportnum:" + exportnum + ";标志位：" + flag);
                updateListBox("exportnum:" + exportnum + ";标志位：" + flag);
                if (flag == 2)
                {
                    List<String> temp = new List<string>();
                    object[] datas = TaskService.GetTroughValue(exportnum, temp);


                    if (int.Parse(datas[1].ToString()) == 0)
                    {
                        writeLog.Write("通道" + exportnum + ":机械手数据发送完毕");
                        updateListBox("通道" + exportnum + ":机械手数据发送完毕");
                        return;
                    }
                    //int export = int.Parse(datas[1].ToString());
                    //if (CheckCanSend(export))
                    //{
                    // datas[0] = 9999;
                    bool iserror = false;
                    string errorInfo = string.Empty;
                    int j = 0;
                    //while (j < writeCount)
                    //{

                    group.WriteR(datas[1], 1);
                    group.WriteR(datas[2], 2);
                    //   group.WriteR(datas[3], 1);


                    String p2 = group.Read(1).ToString();
                    String p3 = group.Read(2).ToString();
                    String p4 = group.Read(3).ToString();
                    //int count = 1;

                    if (p2 == datas[1].ToString() && p3 == datas[2].ToString())
                    {

                        group.WriteR(1, 3);

                        //String p5 = group.Read(3).ToString();

                        //while (p5 != "1" && j < writeCount)
                        //{
                        //    updateListBox("重新写入");
                        //    writeLog.Write("任务号:" + p2 + ";重新写入");
                        //    group.WriteR(1, 3);
                        //    p5 = group.Read(3).ToString();
                        //    j++;
                        //}
                        if (!string.IsNullOrWhiteSpace(errorInfo))
                        {
                            updateListBox(errorInfo);
                        }
                        //  j = 1000;
                        string logstr = "通道:" + exportnum + "   ";
                        for (int i = 0; i < datas.Length; i++)
                        {
                            logstr += i + ":" + datas[i] + ";";
                        }
                        writeLog.Write(p2 + ":" + p3);
                        writeLog.Write(logstr);
                        updateListBox(logstr);
                        updateListBox(":" + p2 + ":" + p3);
                        // break;
                        //}
                    }
                    else
                    {
                        //j++;

                    }

                    CheckExport(exportnum);
                    tempList.Add(new KeyValuePair<String, List<String>>(exportnum, temp));

                }
            }
            catch (Exception ex)
            {
                writeLog.Write(ex.Message);
            }
        }
        public void CheckExport(String exportnum)
        {
            if (tempList != null)
            {

                foreach (var item in tempList)
                {
                    if (item.Key == exportnum)
                    {
                        // isExist = true;
                        tempList.Remove(item);
                        break;
                    }
                }
            }
            //  return true;
        }

        public void OnDataChange(int Group, int[] clientId, object[] values)
        {
            if (Group != 23)
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    //第四位 读写标志位
                    if (clientId[i] == 4)
                    {
                        if (int.Parse(values[i].ToString()) == 2)
                        {

                            if (tempList.Count > 0)
                            {
                                List<String> temp = getKey(Group + "");
                                if (temp != null)
                                {
                                    foreach (var item in temp)
                                    {
                                        updateListBox(item + ":" + ((groupNo - 1) * 22 + Group) + " 已接收");
                                        writeLog.Write(item + ":" + ((groupNo - 1) * 22 + Group) + " 已接收");

                                        TaskService.UpdateMachine(decimal.Parse(item), ((groupNo - 1) * 22 + Group) + "", 15);
                                        removeKey(((groupNo - 1) * 22 + Group) + "");
                                    }
                                }

                                // removeKey(Group + "");
                            }

                            sendTask(Group + "", groupList[Group - 1]);
                        }
                        break;
                    }
                    else if (clientId[i] == 1)//第一位：任务完成标志位
                    {
                        if (decimal.Parse(values[i].ToString()) != 0)
                        {
                            writeLog.Write((decimal.Parse(values[i].ToString()) + ":" + ((groupNo - 1) * 22 + Group) + " 已完成"));
                            updateListBox((decimal.Parse(values[i].ToString()) + ":" + ((groupNo - 1) * 22 + Group) + " 已完成"));
                            TaskService.UpdateMachine(decimal.Parse(values[i].ToString()), ((groupNo - 1) * 22 + Group) + "", 20);
                            //object[] datas = new object[4];

                            //groupList[Group - 1].SyncWrite(0,1);
                            //groupList[Group - 1].SyncWrite(0, 2);
                            //groupList[Group - 1].SyncWrite(0, 3);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    //if (clientId[i] == 1)
                    //{
                    if (int.Parse(values[i].ToString()) != 0)
                    {
                        String temp = Convert.ToString(int.Parse(values[i].ToString()), 2);
                        //WriteErr(1, clientId[i], temp, groupNo);
                        //alarms.fileOper.Write(temp, new AlarmsFileModel { DeviceNo = clientId[i].ToString() });
                        alarms.WriteErrWithCheck(1, clientId[i], temp, groupNo);
                    }
                    //}
                }
            }
        }

        private void OnDowntime(AlarmsInfo obj, List<AlarmsInfo> list)
        {
            if (this.cmbMachines.InvokeRequired)
            {
                this.cmbMachines.Invoke(new Action<AlarmsInfo, List<AlarmsInfo>>(OnDowntime), obj, list);
            }
            else
            {
                cmbMachines.DataSource = null;
                cmbMachines.DataSource = list;
                cmbMachines.DisplayMember = "DeviceNo";
                cmbMachines.ValueMember = "DeviceNo";
            }
        }

        public void Disconnect()
        {


            if (pIOPCServer != null)
            {
                Marshal.ReleaseComObject(pIOPCServer);
                pIOPCServer = null;
            }

            if (taskGroup1 != null)
            {
                taskGroup1.Release();
            }
            if (taskGroup2 != null)
            {
                taskGroup2.Release();
            }
            if (taskGroup3 != null)
            {
                taskGroup3.Release();
            }
            if (taskGroup4 != null)
            {
                taskGroup4.Release();
            }
            if (taskGroup5 != null)
            {
                taskGroup5.Release();
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            //List<String> temp = new List<string>();
            //object[] datas = TaskService.GetTroughValue("48", temp);
            Thread thread = new Thread(new ThreadStart(startFenJian));
            thread.Start();

        }

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
                //   this.txtreceive.BeginInvoke(new ShowDelegate(Show), strshow);//这个也可以

                this.list_data.Invoke(new HandleDelegate(updateListBox), info);
            }
            else
            {
                this.list_data.Items.Insert(0, time + "    " + info);

            }
        }
        int i = 1;
        public void initdata()
        {
            writeLog.Write("initdata");
            task_data.Rows.Clear();
            try
            {
                //String regioncode = "", cuscount = "", qty = "", finishcuscount = "", finishqty="",percent="";
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
                    //}
                }

            }
            finally
            {
                //if (myread != null)
                //{
                //    myread.Close();
                //    myread.Dispose();
                //}
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

        public void exitFunc()
        {

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
            //w_SortingControlMainTest w_SortingControlMain = new w_SortingControlMainTest();

            ////w_SortingControlMain.MdiParent = this;
            //w_SortingControlMain.WindowState = FormWindowState.Maximized;
            //w_SortingControlMain.Show();
            //this.Close();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            //w_pass pass = new w_pass();


            //pass.Show();


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            AlarmsInfo mode = cmbMachines.SelectedItem as AlarmsInfo;
            if (mode == null)
            {
                MessageBox.Show("请选择设备号");
            }
            try
            {
                Group no = groupList[mode.DeviceNo - 1];
                no.Write(3, 3);
                MessageBox.Show("操作完成！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


    }
}
