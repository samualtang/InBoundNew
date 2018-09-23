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
       /// <summary>
       /// 一线DB交互
       /// </summary>
       /// <returns></returns>
       public static List<string> GetOnlyDBItem1()
       {
           List<string> list = new List<string>();

           list.Add(OPCserverStr+"DB30,DINT0");
           list.Add(OPCserverStr + "DB30,DINT4");
           list.Add(OPCserverStr + "DB30,W8");
           for (int i = 2; i < 142; i = i + 2)
           {
               list.Add(OPCserverStr+"DB30,W" + (i + 8));
           }
           for (int i = 0; i < 7; i++)
           {
               list.Add(OPCserverStr+"DB30,DINT" + (152 + (i * 8)));//条烟编码
               list.Add(OPCserverStr+"DB30,W" + (154 + (i * 8)));//长度
               list.Add(OPCserverStr+"DB30,W" + (156 + (i * 8)));//宽度
           }
           list.Add(OPCserverStr+"DB30,W230");
           return list;
       }
       /// <summary>
       /// 二线DB交互
       /// </summary>
       /// <returns></returns>
       public static List<string> GetOnlyDBItem2()
       {
           List<string> list = new List<string>();

           list.Add(OPCserverStr+"DB31,DINT0");
           list.Add(OPCserverStr+"DB31,DINT4");
           list.Add(OPCserverStr+"DB31,W8");
           for (int i = 2; i < 142; i = i + 2)
           {
               list.Add(OPCserverStr+"DB31,W" + (i + 8));
           }
           for (int i = 0; i < 7; i++)
           {
               list.Add(OPCserverStr+"DB31,DINT" + (152 + (i * 8)));//条烟编码
               list.Add(OPCserverStr+"DB31,W" + (154 + (i * 8)));//长度
               list.Add(OPCserverStr+"DB31,W" + (156 + (i * 8)));//宽度
           }
           list.Add(OPCserverStr+"DB31,W230");
           return list;
       }

       /// <summary>
       /// 监控标志位
       /// </summary>
       /// <returns></returns>
       public static List<string> GetSpyOnlyLineItem()
       {
           List<string> list = new List<string>();

           list.Add(OPCserverStr+"DB30,W230");//一线异形烟交互标志 0
           list.Add(OPCserverStr+"DB31,W230");//二线异形烟交互标志 1
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
               list.Add(OPCserverStr+"DB550,DINT" + (i * 4));
           }

           return list;
       }
       /// <summary>
       /// 二线DB任务结束回应
       /// </summary>
       /// <returns></returns>
       public static List<string> GetOnlyLineFinishTaskItem2()
       {
           List<string> list = new List<string>();
           for (int i = 0; i < 20; i++)
           {
               list.Add(OPCserverStr+"DB551,DINT" + (i * 4));
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
           list.Add(OPCserverStr+"DB101,DINT0");//顺序号
           list.Add(OPCserverStr+"DB101,DINT4");//任务号
           list.Add(OPCserverStr+"DB101,W8");//烟仓号
           list.Add(OPCserverStr+"DB101,W10");//订单数量
           //for (int i = 0; i < 10; i++)
           for (int i = 0; i < 12; i++)
           {
               list.Add(OPCserverStr+"DB101,DINT" + (12 + (i * 8)));//条烟编码
               list.Add(OPCserverStr+"DB101,W" + (16 + (i * 8)));//长度
               list.Add(OPCserverStr+"DB101,W" + (18 + (i * 8)));//宽度
           }
           //list.Add(OPCserverStr+"DB101,W92");//标志位 
           list.Add(OPCserverStr+"DB101,W108");//标志位 
           return list;
       }


       /// <summary>
       /// 特异性烟交互区63，64道
       /// </summary>
       /// <returns></returns>
       public static List<string> GetSpecialSmokeItem2()
       {
           List<string> list = new List<string>();
           list.Add(OPCserverStr+"DB103,DINT0");//顺序号
           list.Add(OPCserverStr+"DB103,DINT4");//任务号
           list.Add(OPCserverStr+"DB103,W8");//烟仓号
           list.Add(OPCserverStr+"DB103,W10");//订单数量
           //  for (int i = 0; i < 12; i++)
           for (int i = 0; i < 12; i++)
           {
               list.Add(OPCserverStr+"DB103,DINT" + (12 + (i * 8)));//条烟编码
               list.Add(OPCserverStr+"DB103,W" + (16 + (i * 8)));//长度
               list.Add(OPCserverStr+"DB103,W" + (18 + (i * 8)));//宽度
           }
           list.Add(OPCserverStr+"DB103,W108");//标志位 
           //list.Add(OPCserverStr+"DB103,W92");//标志位 
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
