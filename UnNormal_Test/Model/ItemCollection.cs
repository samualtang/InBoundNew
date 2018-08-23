﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnNormal_Test.Model
{
   public static class ItemCollection
   {


       public static List<string> GetTaskError()
       {
           List<string> list = new List<string>();
           double place0 = 0d, place1 = 0d;
           for (var i = 0; i < 1500; i++)
           {
               place0 = Math.Floor((double)(i / 8));
               place1 = i % 8;
               list.Add("S7:[UnnormalConnection]DB101,x" + place0 + "." + place1);//根据协议设置DB块

           }

           return list;
       }
       public static List<string> GetTaskItem2()
       {
           List<string> list = new List<string>();
           for (var i = 0; i < 800; i++)
           {
               list.Add("S7:[UnnormalConnection]DB400,DINT" + i);
               i += 3;
           }

           return list;
       }
       public static List<string> GetTaskItem3()
       {
           List<string> list = new List<string>();

           for (int i = 0; i < 6000; i++)
           {
               list.Add("S7:[UnnormalConnection]DB401,byte" + i);
           } 
           return list;
       }

       /// <summary>
       /// 一个DB交互区
       /// </summary>
       /// <returns></returns>
       public static List<string> GetOnlyLineItem()
       {
           List<string> list = new List<string>();

           list.Add("S7:[UnnormalConnection]DB30,DINT0");
           list.Add("S7:[UnnormalConnection]DB30,DINT4");
           list.Add("S7:[UnnormalConnection]DB30,W8");
           for (int i = 2; i < 143; i = i + 2)
           {
               list.Add("S7:[UnnormalConnection]DB30,W" + (i + 8));
           }
           return list;
       }
       /// <summary>
       /// 监控标志位
       /// </summary>
       /// <returns></returns>
       public static List<string> GetSpyOnlyLineItem()
       {
           List<string> list = new List<string>();

           list.Add("S7:[UnnormalConnection]DB30,W150");//异形烟交互标志
           list.Add("S7:[UnnormalConnection]DB11,W52");//特异形烟交互标志
           return list;
       }

       /// <summary>
       /// 一个DB任务结束回应
       /// </summary>
       /// <returns></returns>
       public static List<string> GetOnlyLineFinishTaskItem()
       {
           List<string> list = new List<string>();
           for (int i = 0; i < 20; i++)
           {
               list.Add("S7:[UnnormalConnection]DB50,DINT" + (i * 4));
           }

           return list;
       }

       /// <summary>
       /// 特异性烟交互区
       /// </summary>
       /// <returns></returns>
       public static List<string> GetSpecialSmokeItem()
       {
           List<string> list = new List<string>();
           list.Add("S7:[UnnormalConnection]DB11,DINT0");
           list.Add("S7:[UnnormalConnection]DB11,DINT4");
           list.Add("S7:[UnnormalConnection]DB11,W8");//烟仓号
           list.Add("S7:[UnnormalConnection]DB11,W10");//订单数量
           for (int i = 0; i < 10; i++)
           {
               list.Add("S7:[UnnormalConnection]DB11,DINT" + (12 + (i * 8)));//条烟编码
               list.Add("S7:[UnnormalConnection]DB11,W" + (16 + (i * 8)));//长度
               list.Add("S7:[UnnormalConnection]DB11,W" + (18 + (i * 8)));//宽度
           }
           list.Add("S7:[UnnormalConnection]DB11,W92");//标志位
           return list;
       }
   }
}
