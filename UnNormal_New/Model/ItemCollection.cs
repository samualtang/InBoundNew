using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace UnNormal_New.Model
{
   public static class ItemCollection
   {
    
       public static string OPCserverStr =  "S7:[UnnormalConnection1]";//线路
       public static string PLCDBTaskposition = "DB30";//db块位置
       public static string PLCDBSpecialposition = "DB101";//db块位置
       public static string PLCDBFinshposition = "DB550";//db块位置

       /// <summary>
       /// 一线DB交互
       /// </summary>
       /// <returns></returns>
       public static List<string> GetOnlyDBItem1()
       {
           List<string> list = new List<string>();

           list.Add(OPCserverStr+PLCDBTaskposition+",DINT0");
           list.Add(OPCserverStr + PLCDBTaskposition + ",DINT4");
           list.Add(OPCserverStr + PLCDBTaskposition + ",W8");
           for (int i = 2; i < 142; i = i + 2)
           {
               list.Add(OPCserverStr + PLCDBTaskposition + ",W" + (i + 8));
           }
           for (int i = 0; i < 10; i++)
           {
               list.Add(OPCserverStr + PLCDBTaskposition + ",DINT" + (150 + (i * 8)));//条烟编码
               list.Add(OPCserverStr + PLCDBTaskposition + ",W" + (154 + (i * 8)));//长度
               list.Add(OPCserverStr + PLCDBTaskposition + ",W" + (156 + (i * 8)));//宽度
           }
           list.Add(OPCserverStr + PLCDBTaskposition + ",W230");
           return list;
       }
 

       /// <summary>
       /// 监控标志位
       /// </summary>
       /// <returns></returns>
       public static List<string> GetSpyOnlyLineItem()
       {
           List<string> list = new List<string>();

           list.Add(OPCserverStr + PLCDBTaskposition + ",W230");// 交互标志 0
           return list;
       }

       /// <summary>
       /// 一线DB任务结束回应
       /// </summary>
       /// <returns></returns>
       public static List<string> GetOnlyLineFinishTaskItem1()
       {
           List<string> list = new List<string>();
           for (int i = 0; i < 20; i++)
           {
               list.Add(OPCserverStr + PLCDBFinshposition + ",DINT" + (i * 4));
           }

           return list;
       }
 
      

       /// <summary>
       /// 特异性烟交互区61，62道
       /// </summary>
       /// <returns></returns>
       public static List<string> GetSpecialSmokeItem1()
       {
           List<string> list = new List<string>();
           list.Add(OPCserverStr+PLCDBSpecialposition+",DINT0");//顺序号
           list.Add(OPCserverStr + PLCDBSpecialposition + ",DINT4");//任务号
           list.Add(OPCserverStr + PLCDBSpecialposition + ",W8");//烟仓号
           list.Add(OPCserverStr + PLCDBSpecialposition + ",W10");//订单数量
           //for (int i = 0; i < 10; i++)
           for (int i = 0; i < 12; i++)
           {
               list.Add(OPCserverStr + PLCDBSpecialposition + ",DINT" + (12 + (i * 8)));//条烟编码
               list.Add(OPCserverStr + PLCDBSpecialposition + ",W" + (16 + (i * 8)));//长度
               list.Add(OPCserverStr + PLCDBSpecialposition + ",W" + (18 + (i * 8)));//宽度
           }
           //list.Add(OPCserverStr+"DB101,W92");//标志位 
           list.Add(OPCserverStr + PLCDBSpecialposition + ",W108");//标志位 
           return list;
       }

 
       /// <summary>
       /// 包装机
       /// </summary>
       /// <param name="no">包装机号</param>
       /// <returns></returns>
       public static List<string> GetPackageMachineItem(int no)
       {
           List<string> list = new List<string>();
           list.Add("S7:[PackageMachine" + no + "]DB1,D500");//常规烟顺序号
           list.Add("S7:[PackageMachine" + no + "]DB1,W504");//常规烟以包数量
           list.Add("S7:[PackageMachine" + no + "]DB1,D506");//异形烟顺序号
           list.Add("S7:[PackageMachine" + no + "]DB1,W510");//异形烟以抓数量
           return list;
       }
   }
}
