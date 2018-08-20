﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpcRcw.Da;
using OpcRcw.Comn;
using System.Runtime.InteropServices;
using FollowTask.Modle;
using InBound.Model;
using InBound.Business;

using InBound;
using FollowTask.DataModle;

namespace FollowTask.ErrorStart
{
    public class AllSystemStart
    {
        internal const string SERVER_NAME = "OPC.SimaticNET";       // local server name
        internal const string GROUP_NAME = "grp1";                  // Group name
        internal const int LOCALE_ID = 0x409;                       // LOCALE FOR ENGLISH.


        /* Global variables */
        static IOPCServer pIOPCServer;  //定义opcServer对象
        public  WriteLog writeLog = WriteLog.GetLog();
        DeviceStateManager stateManager = new DeviceStateManager();
        List<List<KeyValuePair<int, int>>> UnionList = new List<List<KeyValuePair<int, int>>>(4);

        //区域plc名称
        public static String FJOpcServer1Name = "S7:[FJConnectionGroup1]";
        public static String FJOpcServer2Name = "S7:[FJConnectionGroup2]";
        public static String FJOpcServer3Name = "S7:[FJConnectionGroup3]";
        public static String FJOpcServer4Name = "S7:[FJConnectionGroup4]";
        public static String InOutOpcServerName = "S7:[InOutConnection]";
        List<Group> ListSort = new List<Group>();

        List<string> str1 = new List<string>();
        List<string> str2 = new List<string>();
        List<string> str3 = new List<string>();
        List<string> str4 = new List<string>();

        Group FJOpcServer1, FJOpcServer2, FJOpcServer3, FJOpcServer4;
        Group InOutcServer;

        public void Connction()
        {
            //txtMainInfo.Text = "连接服务器中.....";


            Type svrComponenttyp;
            Guid iidRequiredInterface = typeof(IOPCItemMgt).GUID;
            svrComponenttyp = Type.GetTypeFromProgID(SERVER_NAME);
            try
            {
                pIOPCServer = (IOPCServer)Activator.CreateInstance(svrComponenttyp);//连接本地服务器 
            }
            catch (Exception ex)
            {
                //MessageBox.Show("错误异常：" + ex.Message);
                //txtMainInfo.Text = "错误异常:请检查环境配置！，连接失败!!!";
                //writeLog.Write("错误异常：" + ex.Message);
            }
            FJOpcServer1 = new Group(pIOPCServer, 1, "group1", 1, LOCALE_ID);
            FJOpcServer2 = new Group(pIOPCServer, 2, "group2", 1, LOCALE_ID);
            FJOpcServer3 = new Group(pIOPCServer, 3, "group3", 1, LOCALE_ID);
            FJOpcServer4 = new Group(pIOPCServer, 4, "group4", 1, LOCALE_ID);
            InOutcServer = new Group(pIOPCServer, 5, "group5", 1, LOCALE_ID);
        }

        /// <summary>
        /// 预分拣读取PLC
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public List<string> ReadDBinfo(int no)
        {
            List<string> str = new List<string>();
            Connction(); 
            FJOpcServer1.addItem(GetFJPlcAdress(no));

            for (int i = 0; i < GetFJOpcServerItem().Count(); i++)//从电控读取数据 
            {
                try
                {
                    string k = FJOpcServer1.ReadD(i).ToString();
                    str.Add(k);
                    switch (no)
                    {
                        case 0:
                            AllPlcState.FJState1 = 1;
                            str1 = str;
                            break;
                        case 1:
                            AllPlcState.FJState2 = 1;
                            str2 = str;
                            break;
                        case 2:
                            AllPlcState.FJState3 = 1;
                            str3 = str;
                            break;
                        case 3:
                            AllPlcState.FJState4 = 1;
                            str4 = str;
                            break;
                    }
                }
                catch (Exception)
                {
                    switch (no)
                    {
                        case 0 :
                            AllPlcState.FJState1 = 0;
                            break;
                        case 1 :
                            AllPlcState.FJState2 = 0;
                            break;
                        case 2 :
                            AllPlcState.FJState3 = 0;
                            break;
                        case 3:
                            AllPlcState.FJState4 = 0;
                            break; 
                    } 
                } 
            }
            return str; 
        }

        /// <summary>
        /// 出入库读取PLC
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public List<ErrorInfo> ReadDBinfo_inout()
        {

            List<ErrorInfo> str = new List<ErrorInfo>();
            List<ErrorInfo> AdressAndMsg = GetInOutPlcAdress();
            List<string> DBlist = AdressAndMsg.Select(X=>X.DBAdress).ToList();
            Connction();

            InOutcServer.addItem(DBlist);

            try
            {
                for (int i = 0; i < DBlist.Count(); i++)//从电控读取数据 
                { 
                    ErrorInfo info = new ErrorInfo();
                    info.DBAdress = DBlist[i];
                    info.ErrorMsg = AdressAndMsg[i].ErrorMsg;
                    info.Value=InOutcServer.ReadD(i).ToString();
                    str.Add(info);
                }
                AllPlcState.InOutState = 1;
            }
            catch (Exception)
            {
                AllPlcState.InOutState = 0; 
            }
          
            return str;
        }


      
        /// <summary>
        /// 数据库获取 分拣 故障信息地址
        /// </summary>
        /// <returns>分拣 地址集合</returns>
        public List<Abnormallists> GetFJOpcServerItem()
        {
            using (Entities et=new Entities())
            {
                var list = et.T_WMS_ABNORMALLIST.Where(x => x.AREAPLC == "S7:[FJConnectionGroup-]").OrderBy(X=>X.ID).Select(x => new Abnormallists{ DECICENO = x.DECICENO, AREANAME = x.AREANAME, ERRORMSG = x.ERRORMSG }).ToList();
                return list;
            }
        }

        /// <summary>
        /// 数据库获取 出入库 故障信息地址
        /// </summary>
        /// <returns>出入库 地址集合</returns>
        public List<Abnormallists> GetInOutOpcServerItem()
        {
            using (Entities et = new Entities())
            {
                int num = et.T_WMS_ABNORMALLIST.Count();

                var list = et.T_WMS_ABNORMALLIST.Where(x => x.AREANAME == "出入库" && x.AREAPLC == "S7:[InOutConnection]").Select(x => new Abnormallists
                {
                    AREANAME = x.AREANAME,
                    ERRORMSG = x.ERRORMSG,
                    DECICENO = x.DECICENO,
                    OFFSET = x.OFFSET,
                    MACHINESEQ = x.MACHINESEQ,
                    TYPE = x.TYPE
                }).ToList();


                List<Abnormallists> content = list.Where(x => x.TYPE == "1").Select(x => x).ToList();
                List<Abnormallists> head = list.Where(x => x.TYPE == "2").Select(x => x).ToList();
                List<Abnormallists> DBList = new List<Abnormallists>();

                foreach (var item in head)
                {
                    foreach (var it in content)
                    {
                        Abnormallists data = new Abnormallists();
                        string DB = ((Convert.ToInt32(item.MACHINESEQ) - 100) * 2).ToString();
                        data.DECICENO = it.DECICENO + "," + DB;
                        data.ERRORMSG = item.ERRORMSG + "" + it.ERRORMSG;
                        DBList.Add(data);
                    }
                }
                return DBList;
            }
        }  

        /// <summary>
        /// 预分拣 每组plc的故障地址
        /// </summary>
        /// <param name="i">分拣组号（4大组）</param>
        /// <returns>plc 地址集合</returns>
        public List<string> GetFJPlcAdress(int i)
        {
            List<string> list = new List<string>();
            switch (i)
            {
                case 1 :
                    foreach (var item in GetFJOpcServerItem())
                    {
                        list.Add("S7:[FJConnectionGroup1]" + item.DECICENO);//第一组  
                    }
                    break;
                case 2:
                    foreach (var item in GetFJOpcServerItem())
                    {
                        list.Add("S7:[FJConnectionGroup2]" + item.DECICENO);//第二组 
                    }
                    break;
                case 3:
                    foreach (var item in GetFJOpcServerItem())
                    {
                        list.Add("S7:[FJConnectionGroup3]" + item.DECICENO);//第三组   
                    }
                    break;
                case 4:
                    foreach (var item in GetFJOpcServerItem())
                    {
                        list.Add("S7:[FJConnectionGroup4]" + item.DECICENO);//第四组  
                    } 
                    break; 
            }
            return list;
        }

        public List<ErrorInfo> GetInOutPlcAdress()
        {
            List<ErrorInfo> list = new List<ErrorInfo>();
            foreach (var item in GetFJOpcServerItem())
            {
                ErrorInfo info = new ErrorInfo();
                info.DBAdress = item.DECICENO;
                info.ErrorMsg = item.ERRORMSG;
                list.Add(info);
            }
            return list;
        }
      
    }
}
