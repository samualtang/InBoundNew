﻿using System;
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
        decimal Status = 0;
        int SendWay = 0;//发送方式 1为顺序发送 2为动态发送
        /* Global variables */
        IOPCServer pIOPCServer;  //定义opcServer对象
        public WriteLog writeLog =  WriteLog.GetLog();
        public DeviceStateManager stateManager = new DeviceStateManager();
        static int Strat = 1,End = 4;//开始和

        public UnNormalFm()
        {
            InitializeComponent();
            updateListBox("应用程序启动");
            writeLog.Write("应用程序启动");
            t1.Tick += new EventHandler(t1_Tick);
            try
            {
                Control.CheckForIllegalCrossThreadCalls = false;
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
            if (SpyBiaozhiGroup.Read(0).ToString() != "0" && !issendone)//监控标志位第一组 产生跳变
            {
                SpyBiaozhiGroup.Write(2, 0);
                SpyBiaozhiGroup.Write(0, 0);
            }
            //if (SpyBiaozhiGroup.Read(1).ToString() != "0" && !issendone)//监控标志位第一组 产生跳变
            //{
            //    SpyBiaozhiGroup.Write(2, 1);
            //    SpyBiaozhiGroup.Write(0, 1);
            //}
            //if (SpyBiaozhiGroup.Read(2).ToString() != "0" && !issendone)//监控标志位第一组 产生跳变
            //{
            //    SpyBiaozhiGroup.Write(2, 2);
            //    SpyBiaozhiGroup.Write(0, 2);
            //}
            timerSendTask.Stop();
        }



        Group OnlyTaskGorup, FinishOnlyGoroup, SpyBiaozhiGroup, SpecialSmokeGroup1, SpecialSmokeGroup2;
        Group PackageMachineGroup1, PackageMachineGroup2, PackageMachineGroup3, PackageMachineGroup4, PackageMachineGroup5, PackageMachineGroup6, PackageMachineGroup7, PackageMachineGroup8;
        List<Group> listPM = new List<Group>();
        public void Connect()
        {
            Type svrComponenttyp;
            Guid iidRequiredInterface = typeof(IOPCItemMgt).GUID;
            svrComponenttyp = Type.GetTypeFromProgID(SERVER_NAME);
            try
            {
                // Connect to the local server.
                pIOPCServer = (IOPCServer)Activator.CreateInstance(svrComponenttyp);

                OnlyTaskGorup = new Group(pIOPCServer, 1, "group1", 1, LOCALE_ID);
                FinishOnlyGoroup = new Group(pIOPCServer, 2, "group2", 1, LOCALE_ID);//完成信号
                SpyBiaozhiGroup = new Group(pIOPCServer, 3, "group3", 1, LOCALE_ID);//监控标志位
                SpecialSmokeGroup1 = new Group(pIOPCServer, 4, "group4", 1, LOCALE_ID);//特异形烟 61,62道
                SpecialSmokeGroup2 = new Group(pIOPCServer, 5, "group5", 1, LOCALE_ID);//特异形烟 63,64道


                PackageMachineGroup1 = new Group(pIOPCServer, 6, "group6", 1, LOCALE_ID);//包装机
                PackageMachineGroup2 = new Group(pIOPCServer, 7, "group7", 1, LOCALE_ID);//包装机
                PackageMachineGroup3 = new Group(pIOPCServer, 8, "group8", 1, LOCALE_ID);//包装机
                PackageMachineGroup4 = new Group(pIOPCServer, 9, "group9", 1, LOCALE_ID);//包装机
                PackageMachineGroup5 = new Group(pIOPCServer, 10, "group10", 1, LOCALE_ID);//包装机
                PackageMachineGroup6 = new Group(pIOPCServer, 11, "group11", 1, LOCALE_ID);//包装机
                PackageMachineGroup7 = new Group(pIOPCServer, 12, "group12", 1, LOCALE_ID);//包装机
                PackageMachineGroup8 = new Group(pIOPCServer, 13, "group13", 1, LOCALE_ID);//包装机
                for (int i = 1; i <= 8; i++)
                {
                    if (i == 1) { PackageMachineGroup1.addItem(ItemCollection.GetPackageMachineItem(i)); }
                    if (i == 2) { PackageMachineGroup2.addItem(ItemCollection.GetPackageMachineItem(i)); }
                    if (i == 3) { PackageMachineGroup3.addItem(ItemCollection.GetPackageMachineItem(i)); }
                    if (i == 4) { PackageMachineGroup4.addItem(ItemCollection.GetPackageMachineItem(i)); }
                    if (i == 5) { PackageMachineGroup5.addItem(ItemCollection.GetPackageMachineItem(i)); }
                    if (i == 6) { PackageMachineGroup6.addItem(ItemCollection.GetPackageMachineItem(i)); }
                    if (i == 7) { PackageMachineGroup7.addItem(ItemCollection.GetPackageMachineItem(i)); }
                    if (i == 8) { PackageMachineGroup8.addItem(ItemCollection.GetPackageMachineItem(i)); }
                }
               
               
                //任务交互区
                OnlyTaskGorup.addItem(ItemCollection.GetOnlyDBItem());//一个交互区
                SpyBiaozhiGroup.addItem(ItemCollection.GetSpyOnlyLineItem());//监控任务标识位
                SpecialSmokeGroup1.addItem(ItemCollection.GetSpecialSmokeItem1());//特异形烟 61,62道
                SpecialSmokeGroup2.addItem(ItemCollection.GetSpecialSmokeItem2());//特异形烟 63,64道
            

                //完成信号交互区 
                FinishOnlyGoroup.addItem(ItemCollection.GetOnlyLineFinishTaskItem());//一个交互区完成信号
              
                //回调 

                SpyBiaozhiGroup.callback += OnDataChange;
                FinishOnlyGoroup.callback += OnDataChange;

                listPM.Add(PackageMachineGroup1);
                listPM.Add(PackageMachineGroup2);
                listPM.Add(PackageMachineGroup3);
                listPM.Add(PackageMachineGroup4);
                listPM.Add(PackageMachineGroup5);
                listPM.Add(PackageMachineGroup6);
                listPM.Add(PackageMachineGroup7);
                listPM.Add(PackageMachineGroup8);
                checkConnection();
            
            }
            catch (Exception e)
            {
                updateListBox("连接服务器失败:"+e.Message);
            }
        }
        public void PackageMachineConnection()
        {
            int[] flagpm = new int[8];
            int[] connfile = new int[8];
            for (int i = 0; i < listPM.Count; i++)
            {
                flagpm[i] = listPM[i].ReadD(0).CastTo<int>(-1);
                if (flagpm[i] == -1)
                {
                    connfile[i] = flagpm[i];
                }
            }
            if (connfile.Length > 0)
            {
                string str = "";
                for (int i = 0; i < connfile.Length; i++)
                {
                    str += i + "号，";
                }
                updateListBox(str+"包装机服务器连接失败,请检查网络.");
                writeLog.Write(str+" 包装机服务器连接失败,请检查网络.");
                strat = false;
                return;
            }
            else
            {
                 updateListBox("包装机服务器连接成功......");
                 writeLog.Write(" 包装机服务器连接成功......");
                 strat = true;
            }
            
           
        }
        public void checkConnection()
        {
            int flag = SpyBiaozhiGroup.ReadD(0).CastTo<int>(-1);
          
            if (flag != -1)
            {
                updateListBox("连接服务器成功......");
                writeLog.Write(" 连接服务器成功......");
                SpyBiaozhiGroup.Write(2, 0);
                SpyBiaozhiGroup.Write(0, 0);
                //SpyBiaozhiGroup.Write(2, 1);
                //SpyBiaozhiGroup.Write(0, 1);
                //SpyBiaozhiGroup.Write(2, 2);
                //SpyBiaozhiGroup.Write(0, 2);
                updateControlEnable(false, button10);
                isInit = true;
            }
            else
            {
                updateListBox("连接服务器失败,请检查网络.");
                writeLog.Write(" 连接服务器失败,请检查网络.");
            
            }
        }


        delegate void delSendTask();
      
      
        List<T_UN_POKE> listOnly = new List<T_UN_POKE>();
        List<T_UN_SpecialSmoke> listSS1B = new List<T_UN_SpecialSmoke>();
        List<T_UN_SpecialSmoke> listSS2A = new List<T_UN_SpecialSmoke>(); 
        bool issendone = false; 
       
        /// <summary>
        /// 一组交互
        /// </summary> 
        void sendOnlyTask()
        {
            try
            {
                issendone = true; 
                List<decimal> sortNumList = new List<decimal>();//当前任务号集合
                List<decimal> xyNumList = new List<decimal>();//当前吸烟数量集合 
                int flag = SpyBiaozhiGroup.ReadD(0).CastTo<int>(-1);
                //object[] pmsss = UnPokeService.GetEnablePackageMachine();
                //Strat = (int)pmsss.Min();
                //End = (int)pmsss.Max(); 
                writeLog.Write("烟仓烟柜发送数据前读标志位：" + flag);
                if (flag == 0)
                { 
                    while (!UnPokeService.CheckExistCanSendTask(12) && UnPokeService.CheckExistCanSendTask(10))
                    {
                        sortNumList.Clear();
                        xyNumList.Clear(); 
                        decimal sortNum, xyNum;
                        decimal packagemachine = 0;
                        for (int i =0; i < 8; i++)
                        {
                            try
                            {
                                if(UnPokeService.checkExist(i+1))
                                {
                                    sortNum =  listPM[i].ReadD(2).CastTo<decimal>(-1);//包装机读取出来的任务号
                                    xyNum =  listPM[i].ReadD(3).CastTo<decimal>(-1);//包装机读取出来的数量
                                }
                                else
                                {
                                 sortNum=0;
                                 xyNum=0;
                                }
                            }
                            catch
                            {
                                sortNum = 0; //读取出错的情况下 默认为0
                                xyNum = 0;
                            }
                            writeLog.Write("plc地址:S7:[PackageMachine" +i + "]DB1,D506" + " ，包装机:" + (i + 1) + "号， 包装机读取任务号:" + sortNum + " ，包装机读取抓取数量:" + xyNum);
                            updateListBox("plc地址:S7:[PackageMachine" + i + "]DB1,D506" + " ，包装机:" + (i + 1) + " 号，包装机读取任务号:" + sortNum + "， 包装机读取抓取数量:" + xyNum);
                            sortNumList.Add(sortNum);
                            xyNumList.Add(xyNum);
                        }
                        // decimal DISPATCHESIZE = 0;
                        packagemachine = UnPokeService.GetSendPackageMachine_New(sortNumList, xyNumList, SendWay); 
                        if (packagemachine > 0)
                        {
                            UnPokeService.UpdateTaskByPackMachine(packagemachine);
                        }
                        Thread.Sleep(100);

                    }
                    string OutStr = "";
                    object[] datas = UnPokeService.getOneDateBaseTask(12,out listOnly, out OutStr);//获取可发送任务
                    if (int.Parse(datas[0].ToString()) == 0)
                    {
                        updateListBox("烟仓烟柜分拣数据发送完毕");
                        return;
                    } 
                    writeLog.Write("烟仓烟柜分拣线:" + OutStr);
                    updateListBox("烟仓烟柜分拣线:" + OutStr); 
                    OnlyTaskGorup.SyncWrite(datas);  
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
                sendOnlyTask();//异常后重新发送

            }
            issendone = false;
        }
        /// <summary>
        ///特异形烟1线
        /// </summary> 
        void sendSSTask1()
        {
            try
            {
                issendone = true; 
                int flag = SpyBiaozhiGroup.ReadD(1).CastTo<int>(-1);//发送数据前读标志位
                writeLog.Write("1线特异形烟发送数据前读标志位：" + flag);
                if (flag == 0)
                {
                    while (!UnPokeService.CheckExistSSCanSendTask(12) && UnPokeService.CheckExistCanSendTask(10))
                    {
                        if (UnPokeService.CheckExistSSCanSendTask(12))
                        {
                            break;
                        }
                        Thread.Sleep(100);
                    }
                    string OutStr = "";
                    object[] datas = UnPokeService.GetSpecialSmokeData(12, "1", out listSS1B, out OutStr);//获取可发送任务
                    if (int.Parse(datas[0].ToString()) == 0)
                    {
                        updateListBox("1线特异形烟分拣数据发送完毕");
                       
                        return;
                    }
                    writeLog.Write("1线特异形烟分拣线:" + OutStr);
                    updateListBox("1线特异形烟分拣线:" + OutStr);
                    SpecialSmokeGroup1.SyncWrite(datas); 
                }
                else
                {
                    writeLog.Write("1线特异形烟标志位读取到异常:" + flag);
                    updateListBox("1线特异形烟标志位读取到异常:" + flag);
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
                sendSSTask1();//异常后重新发送

            }
        }

        /// <summary>
        ///特异形烟2线
        /// </summary> 
        void sendSSTask2( )
        {
            try
            {
                issendone = true;
                int flag = SpyBiaozhiGroup.ReadD(2).CastTo<int>(-1);//发送数据前读标志位
                writeLog.Write("2线特异形烟发送数据前读标志位：" + flag);
                if (flag == 0)
                {
                    while (!UnPokeService.CheckExistSSCanSendTask(12) && UnPokeService.CheckExistCanSendTask(10))
                    {
                        if (UnPokeService.CheckExistSSCanSendTask(12))
                        {
                            break;
                        }
                        Thread.Sleep(100);
                    }
                    string OutStr = "";
                    object[] datas = UnPokeService.GetSpecialSmokeData(12, "2", out listSS2A, out OutStr);//获取可发送任务
                    if (int.Parse(datas[0].ToString()) == 0)
                    {
                        updateListBox("2线特异形烟分拣数据发送完毕"); 
                        return;
                    }
                    writeLog.Write("2线特异形烟分拣线:" + OutStr);
                    updateListBox("2线特异形烟分拣线:" + OutStr);
                    SpecialSmokeGroup2.SyncWrite(datas);
                }
                else
                {
                    writeLog.Write("2线特异形烟标志位读取到异常:" + flag);
                    updateListBox("2线特异形烟标志位读取到异常:" + flag);
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
                sendSSTask2();//异常后重新发送

            }
        }
        public static Object lockFlag = new Object();
        public void OnDataChange(int group, int[] clientId, object[] values)
        {
          
            if (group == 2)//完成信号
            {
                 for (int i = 0; i < clientId.Length; i++)//"出口号：" + clientId[i] + ";任务号:" + taskno
                 {
                     try
                     {
                         int tempvalue = int.Parse((values[i].ToString()));
                         if (tempvalue >= 1)//分拣完成
                         {

                             writeLog.Write("从电控读取出口号：" + clientId[i] + ";任务号:" + tempvalue);
                             UnPokeService.UpdateunTask(tempvalue, 20);//根据异形烟整包任务号更新poke表中状态 
                             writeLog.Write("烟仓烟柜任务号" + tempvalue + "数据库更新完成");
                             if (tempvalue != 0)
                             {
                                 updateListBox(" :" + tempvalue + "号任务已完成");
                                 writeLog.Write(" :" + tempvalue + "号任务已完成");
                             } 
                             FinishOnlyGoroup.Write(0, clientId[i] - 1);
                             // this.task_data.BeginInvoke(new Action(() => { initdata(); }));//异步调用，刷新分拣页面的分拣进度 

                         }
                    
                     }
                     catch (NullReferenceException nullex)
                     {
                         writeLog.Write("空引用异常：" + nullex.Message);
                     }
                     catch (Exception ex)
                     { 
                         writeLog.Write("集成错误 ：" + ex.Message);
                     }
                 }

            }
            else if (group == 3)//接收标志
            {
                for (int i = 0; i < clientId.Length; i++)
                {
                    if (clientId[i] == 1)
                    {
                        try
                        { 
                            if (values[i] != null && int.Parse(values[i].ToString()) == 0)
                            {
                                while (!isInit)
                                {
                                    Thread.Sleep(100);
                                }
                                String logstr = "";
                                //var list = listOnly.FirstOrDefault();
                                 
                                int receivePackage = int.Parse(OnlyTaskGorup.ReadD(1).ToString());
                                //if (list != null)
                                //{
                                //    logstr += list.SORTNUM + ";";
                                //}
                                //if (logstr != null && logstr.Length > 0)
                                //{
                                //    writeLog.Write("烟仓烟柜任务号:" + logstr + "已接收");
                                //    updateListBox("烟仓烟柜任务号:" + logstr + "已接收");
                                //    UnPokeService.UpdateTask(listOnly, 15);
                                //}
                                if (receivePackage != 0)
                                {
                                    writeLog.Write("烟仓烟柜任务号:" + receivePackage + "已接收");
                                    updateListBox("烟仓烟柜任务号:" + receivePackage + "已接收");
                                    UnPokeService.UpdateTask(receivePackage, 15);
                                }
                                if (!issendone)
                                {
                                    //sendOnlyTask();
                                    delSendTask task = sendOnlyTask;
                                    task.BeginInvoke(null, null);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            WriteLog.GetLog().Write("烟仓烟柜异常信息" + ex.Message);
                        }
                        
                    }
                    //if (clientId[i] == 2)//特异形烟 61,62道
                    //{
                    //    try
                    //    {
                    //        if (values[i] != null && int.Parse(values[i].ToString()) == 0)
                    //        {
                    //            while (!isInit)
                    //            {
                    //                Thread.Sleep(100);
                    //            }
                    //            string STR = "";

                    //            foreach (var item in listSS1B)
                    //            {
                    //                STR += item.POKEID + "，";
                    //            }
                    //            if (STR != null && STR.Length > 0)
                    //            {
                    //                writeLog.Write("1线61,62特异形烟任务号:" + STR + "已接收");
                    //                updateListBox("1线61,62特异形烟:" + STR + "已接收");
                    //                UnPokeService.UpdateSSTask(listSS1B, 15);
                    //            }
                    //            //sendSSTask1();
                    //            delSendTask task = sendSSTask1;
                    //            task.BeginInvoke(null, null);
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        WriteLog.GetLog().Write("1线61,62特异形烟异常信息" + ex.Message);
                    //    }
                        
                    //}
                    //if (clientId[i] == 3)//特异形烟 63,64道
                    //{
                    //    try
                    //    {
                    //        if (values[i] != null && int.Parse(values[i].ToString()) == 0)
                    //        {
                    //            while (!isInit)
                    //            {
                    //                Thread.Sleep(100);
                    //            }
                    //            string STR = "";

                    //            foreach (var item in listSS2A)
                    //            {
                    //                STR += item.POKEID + "，";
                    //            }
                    //            if (STR != null && STR.Length > 0)
                    //            {
                    //                writeLog.Write("2线63,64特异形烟任务号:" + STR + "已接收");
                    //                updateListBox("2线63,64特异形烟:" + STR + "已接收");
                    //                UnPokeService.UpdateSSTask(listSS2A, 15);
                    //            }
                    //           // sendSSTask2();
                    //            delSendTask task = sendSSTask2;
                    //            task.BeginInvoke(null, null); 
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        WriteLog.GetLog().Write("2线63,64特异形烟异常信息" + ex.Message);
                    //    }
                        
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
            if (OnlyTaskGorup != null)
            {
                OnlyTaskGorup.Release();
            }
            if (FinishOnlyGoroup != null)
            {
                FinishOnlyGoroup.Release();
            }
            if (SpyBiaozhiGroup != null)
            {
                SpyBiaozhiGroup.Release();
            }
            
        }
        static Boolean isInit = false;
        private void button10_Click(object sender, EventArgs e)//开始
        {
            // UnPokeService.getTask(25);
            timerSendTask.Interval = 1000 * 10;
            timerSendTask.Start();
            updateListBox("启动定时器");
            updateControlEnable(false, button10);
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
           DialogResult MsgBoxResult = MessageBox.Show("确定要停止任务?",//对话框的显示内容 
                                                         "操作提示",//对话框的标题 
                                                         MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                         MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                         MessageBoxDefaultButton.Button2);//定义对话框的按钮式样


           if (MsgBoxResult == DialogResult.Yes)
           {
               if (pIOPCServer != null)
               {
                   Marshal.ReleaseComObject(pIOPCServer);
                   pIOPCServer = null;
               }
               if (OnlyTaskGorup != null)
               {
                   OnlyTaskGorup.Release();
               }
               if (FinishOnlyGoroup != null)
               {
                   FinishOnlyGoroup.Release();
               }
               if (SpyBiaozhiGroup != null)
               {
                   SpyBiaozhiGroup.Release();
               }
               if (SpecialSmokeGroup1 != null)
               {
                   SpecialSmokeGroup1.Release();
               }
               if (SpecialSmokeGroup2 != null)
               {
                   SpecialSmokeGroup2.Release();
               }
               list_data.Items.Clear();
               updateListBox("任务停止发送!");
               writeLog.Write("任务停止发送!");
               updateControlEnable(true, button10);
           }
           else
           {
               return;
           }
         //  updateControlEnable(true, button10);
           
       }
       private void button6_Click(object sender, EventArgs e)
       {
         
       }

       private void button7_Click(object sender, EventArgs e)
       {
           //UnPokeService.getName();
           //SpyBiaozhiGroup.Write(2, 1);
           //SpyBiaozhiGroup.Write(0, 1);
           //SpyBiaozhiGroup.Write(2, 2);
           //SpyBiaozhiGroup.Write(0, 2);
           //if (statusGroup2 != null)
           //{
           //    statusGroup2.SyncWrite(UnPokeService.getCode());
           //    statusGroup3.SyncWrite(UnPokeService.getName());
           //}
           //else
           //{
           //    MessageBox.Show("连接未建立,请稍后同步!");
           //}
       }

       private void button6_Click_1(object sender, EventArgs e)
       {
           //StatusManager sm = new StatusManager();
           //sm.StartPosition = FormStartPosition.CenterScreen;
           //sm.Show();
           w_pass pass = new w_pass();
           pass.StartPosition = FormStartPosition.CenterScreen;

           pass.Show();
       }
      // delegate void HandleGetNewinfo();
       private void UnNormalFm_Load(object sender, EventArgs e)
       {
           try
           {
               lineNum = ConfigurationManager.AppSettings["LineNum"].ToString();//线路

               SendWay = Convert.ToInt32(ConfigurationManager.AppSettings["SendWay"]);//1为顺序 2为动态
               if (SendWay != 1 && SendWay != 2)
               { 
                   MessageBox.Show("配置文件出现错误！\r\n请关闭程序按正常配置再打开！");
                   WriteLog.GetLog().Write("配置文件出现错误！\r\n请关闭程序按正常配置再打开！");
                   button10.Enabled = false; 
               }
               AutoSizeColumn(task_data);
              
               TimeToClike.Start();  
               this.task_data.DoubleBufferedDataGirdView(true);
           }
           catch (NullReferenceException nullre)
           {
               MessageBox.Show("配置文件出现错误！\r\n请关闭程序按正常配置再打开！");
               WriteLog.GetLog().Write("配置文件出现错误！\r\n请关闭程序按正常配置再打开！" + "\r\n" + nullre.Message);
           }
           catch (Exception ex)
           {
               MessageBox.Show("错误提示:"+"\r\n"+ ex.Message);
               WriteLog.GetLog().Write("错误提示:" + "\r\n" + ex.Message);
           }
          
       }
       void GetNewInfo()
       {
           try
           {
               while (true)
               {  
                   List<T_UN_POKE> list2 = TaskService.GetUNTaskInfo();
                   labelALLcount.Text = "任务总数：" +( list2.Sum(a => a.POKENUM)??0);
                   labelFIinshCOunt.Text = "完成数量：" + (list2.Where(a => a.STATUS != 10).Sum(a => a.POKENUM) ??0);
                   labeleftCOunt.Text = "剩余数量：" + (list2.Where(a => a.STATUS == 10).Sum(a => a.POKENUM) ??0);
                   if ((list2.Where(a => a.STATUS == 10).Sum(a => a.POKENUM)??0) == 0)
                   { 
                       break;
                   }
               } 
           }
           catch
           {

           }
       }
       private void UnNormalFm_SizeChanged(object sender, EventArgs e)
       {
           //task_data.Height = this.Size.Height - list_data.Size.Height;
           //task_data.Width = this.Size.Width - groupboxErr.Width;
        
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

       private void TimeToClike_Tick(object sender, EventArgs e)
       {
           List<T_UN_POKE> list2 = TaskService.GetUNTaskInfo();
           labelALLcount.Text = "任务总数：" + (list2.Sum(a => a.POKENUM) ?? 0);
           labelFIinshCOunt.Text = "完成数量：" + (list2.Where(a => a.STATUS != 10).Sum(a => a.POKENUM) ?? 0);
           labeleftCOunt.Text = "剩余数量：" + (list2.Where(a => a.STATUS == 10).Sum(a => a.POKENUM) ?? 0);
           if ((list2.Where(a => a.STATUS == 10).Sum(a => a.POKENUM) ?? 0) == 0)
           {
               TimeToClike.Stop();
           }
       }

       private void btnDynamic_Click(object sender, EventArgs e)
       {
           DialogResult MsgBoxResult = MessageBox.Show("确定要切换调度?",//对话框的显示内容 
                                                          "操作提示",//对话框的标题 
                                                          MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                          MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                          MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
           if (DialogResult.Yes == MsgBoxResult)
           {
               if (btnDynamic.Text == "动态调度")
               {
                   SendWay = 1;//为顺序调度
                   btnDynamic.Text = "顺序调度";
               }
               else if (btnDynamic.Text == "顺序调度")
               {
                   SendWay = 2;//为动态调度
                   btnDynamic.Text = "动态调度";
               }
           }
           else
           {
               return;
           }

       }

       private void TimeToReadPMDB_Tick(object sender, EventArgs e)
       {
          
       } 
        void ThreadFuntion()
       {
           while (true)
           {
               //object[] pmsss = UnPokeService.GetEnablePackageMachine();
               //Strat = (int)pmsss.Min();
               //End = (int)pmsss.Max(); 
               for (int i = 1; i <= 8; i++)
               {
                   T_UN_CACHE cache = ProduceCacheService.GetUnCache(i);
                   string lblName = "lblPm" + i;
                   Control contr = (Label)Controls.Find(lblName, true)[0]; 
                   if (UnPokeService.checkExist(i ))
                   {
                       decimal sortnum = listPM[i - 1].ReadD(2).CastTo<decimal>(-1);//任务号
                       if (sortnum == -1)
                       {
                           string str = i + "号包装机连接异常！";
                           updateLabel(str, (Label)contr);
                       }
                       else
                       {
                           decimal xynum = listPM[i - 1].ReadD(3).CastTo<decimal>(-1);//以包数量
                           decimal? cacheSize = cache.CACHESIZE;//可容纳烟条大小
                           string str = i + "号包装机常规烟任务：" + listPM[i - 1].ReadD(0) + "，常规烟已包数量：" + listPM[i - 1].ReadD(1) + "\r\n" + i + "号包装机异形烟任务号：" + sortnum + "，异形烟已包数量：" + xynum +
                               "\r\n当前缓存量：" + (cacheSize - UnPokeService.GetCacheCount((decimal)i, sortnum, xynum, (cacheSize ?? 0)));
                           updateLabel(str, (Label)contr);
                       }
                   }
                   else
                   { 
                       string str = i + "号包装机未启用！";
                       updateLabel(str, (Label)contr);
                   }
               }
               Thread.Sleep(200);
           }
       }
       bool strat = true;
    
       private void btnStratRead_Click(object sender, EventArgs e)
       {

           Thread thread = new Thread(ThreadFuntion);
           if (strat)
           {
               thread.Start();
               btnStratRead.Text = "停止";
               strat = false;
           }
           else
           {
               thread.Abort();
               btnStratRead.Text = "读取包装机数据";
               strat = true;
           }
       }



    
      
     
    }
}
