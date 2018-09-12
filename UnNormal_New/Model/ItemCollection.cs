using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnNormal_New.Model
{
   public static class ItemCollection
   {
       #region
       /// <summary>
       /// 异形烟1线DB地址
       /// </summary>
       /// <returns></returns>
       public static List<string> GetTask1ALineItem()
       {
           List<string> list = new List<string>();

           list.Add("S7:[UnnormalConnection]DB1,DINT0");
           for (int i = 0; i < 130; i = i + 2)
           {
               list.Add("S7:[UnnormalConnection]DB1,W" + (i + 4));
           } 
           return list;

       }

       /// <summary>
       /// 一线任务结束回应
       /// </summary>
       /// <returns></returns>
       public static List<string> GetFinishTaskStatusItem1()
       {
           List<string> list = new List<string>();
           for (int i = 0; i < 20; i++)
           {
               list.Add("S7:[UnnormalConnection]DB50,DINT" + (i * 4));
           }

           return list;
       }

       /// <summary>
       /// 异形烟2线DB地址
       /// </summary>
       /// <returns></returns>
       public static List<string> GetTask2ALineItem()
       {
           List<string> list = new List<string>();

           list.Add("S7:[UnnormalConnection]DB101,DINT0");
           for (int i = 0; i < 130; i = i + 2)
           {
               list.Add("S7:[UnnormalConnection]DB101,W" + (i + 4));
           }
           return list;
       }

       /// <summary>
       /// 二线任务结束回应
       /// </summary>
       /// <returns></returns>
       public static List<string> GetFinishTaskStatusItem2()
       {
           List<string> list = new List<string>();
           for (int i = 0; i < 20; i++)
           {
               list.Add("S7:[UnnormalConnection]DB150,DINT" + (i * 4));
           }  
           return list;
       }
      
       /// <summary>
       /// 烟柜任务DB地址
       /// </summary>
       /// <returns></returns>
       public static List<string> GetCabinetTaskItem()
       {
           List<string> list = new List<string>();
           list.Add("S7:[UnnormalConnection]DB910,DINT0");
           for (int i = 0; i < 18; i = i + 2)
           {
               list.Add("S7:[UnnormalConnection]DB910,W" + (i + 4));
           }
           list.Add("S7:[UnnormalConnection]DB901,W");
           return list;
       }


       /// <summary>
       /// 烟柜任务结束回应
       /// </summary>
       /// <returns></returns>
       public static List<string> GetCabinetTaskFinishStatusItem()
       {
           List<string> list = new List<string>();
           for (int i = 0; i < 20; i++)
           {
               list.Add("S7:[UnnormalConnection]DB950,DINT" + (i * 4));
           }
           return list;
       }


       /// <summary>
       /// 监控标志位
       /// </summary>
       /// <returns></returns>
       public static List<string> GetSpyDbChangeItem()
       {
           List<string> list = new List<string>();
           list.Add("S7:[UnnormalConnection]DB1,W132");//一线任务发送标志位  1上位写入 0电控取走 
           list.Add("S7:[UnnormalConnection]DB101,W132");//二线任务发送标志位
           list.Add("S7:[UnnormalConnection]DB901,W22");//烟柜任务发送标志位
           list.Add("S7:[UnnormalConnection]DB501,W16");//合流处任务发送标志位
           return list;
       }
       /// <summary>
       /// 合流处通讯DB地址
       /// </summary>
       /// <returns></returns>
       public static List<string> GetUnUnionItem()
       {
           List<string> list = new List<string>();
           list.Add("S7:[UnnormalConnection]DB501,DINT0");
           for (int i = 0; i < 11; i = i + 2)
           {
               list.Add("S7:[UnnormalConnection]DB501,W" + (i + 4));
           }
           list.Add("S7:[UnnormalConnection]DB501,W16");
           return list; 
       }


       /// <summary>
       /// 合流处任务结束回应
       /// </summary>
       /// <returns></returns>
       public static List<string> GetUnUnionTaskFinishStatusItem()
       {
           List<string> list = new List<string>();
           for (int i = 0; i < 20; i++)
           {
               list.Add("S7:[UnnormalConnection]DB550,DINT" + (i * 4));
           }
           return list;
       }



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
       #endregion

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
       /// 一个DB区包含特异形烟交互
       /// </summary>
       /// <returns></returns>
       public static List<string> GetOnlyDBItem()
       {
           List<string> list = new List<string>();

           list.Add("S7:[UnnormalConnection]DB30,DINT0");
           list.Add("S7:[UnnormalConnection]DB30,DINT4");
           list.Add("S7:[UnnormalConnection]DB30,W8");
           for (int i = 2; i < 142; i = i + 2)
           {
               list.Add("S7:[UnnormalConnection]DB30,W" + (i + 8));
           }
           for (int i = 0; i < 7; i++)
           {
               list.Add("S7:[UnnormalConnection]DB30,DINT" + (152 + (i * 8)));//条烟编码
               list.Add("S7:[UnnormalConnection]DB30,W" + (154 + (i * 8)));//长度
               list.Add("S7:[UnnormalConnection]DB30,W" + (156 + (i * 8)));//宽度
           }
           list.Add("S7:[UnnormalConnection]DB30,W206");
           return list;
       }
       /// <summary>
       /// 监控标志位
       /// </summary>
       /// <returns></returns>
       public static List<string> GetSpyOnlyLineItem()
       {
           List<string> list = new List<string>();

           list.Add("S7:[UnnormalConnection]DB30,W206");//异形烟交互标志 0
           //list.Add("S7:[UnnormalConnection]DB101,W108");//特异形烟该发送皮带 1
           //list.Add("S7:[UnnormalConnection]DB103,W108");//特异形烟该发送皮带 2
           //list.Add("S7:[UnnormalConnection]DB101,W92");//特异形烟该发送皮带 1
           //list.Add("S7:[UnnormalConnection]DB103,W92");//特异形烟该发送皮带 2
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
               list.Add("S7:[UnnormalConnection]DB550,DINT" + (i * 4));
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
           list.Add("S7:[UnnormalConnection]DB101,DINT0");//顺序号
           list.Add("S7:[UnnormalConnection]DB101,DINT4");//任务号
           list.Add("S7:[UnnormalConnection]DB101,W8");//烟仓号
           list.Add("S7:[UnnormalConnection]DB101,W10");//订单数量
           //for (int i = 0; i < 10; i++)
           for (int i = 0; i < 12; i++)
           {
               list.Add("S7:[UnnormalConnection]DB101,DINT" + (12 + (i * 8)));//条烟编码
               list.Add("S7:[UnnormalConnection]DB101,W" + (16 + (i * 8)));//长度
               list.Add("S7:[UnnormalConnection]DB101,W" + (18 + (i * 8)));//宽度
           }
           //list.Add("S7:[UnnormalConnection]DB101,W92");//标志位 
           list.Add("S7:[UnnormalConnection]DB101,W108");//标志位 
           return list;
       }


       /// <summary>
       /// 特异性烟交互区63，64道
       /// </summary>
       /// <returns></returns>
       public static List<string> GetSpecialSmokeItem2()
       {
           List<string> list = new List<string>();
           list.Add("S7:[UnnormalConnection]DB103,DINT0");//顺序号
           list.Add("S7:[UnnormalConnection]DB103,DINT4");//任务号
           list.Add("S7:[UnnormalConnection]DB103,W8");//烟仓号
           list.Add("S7:[UnnormalConnection]DB103,W10");//订单数量
           //  for (int i = 0; i < 12; i++)
           for (int i = 0; i < 12; i++)
           {
               list.Add("S7:[UnnormalConnection]DB103,DINT" + (12 + (i * 8)));//条烟编码
               list.Add("S7:[UnnormalConnection]DB103,W" + (16 + (i * 8)));//长度
               list.Add("S7:[UnnormalConnection]DB103,W" + (18 + (i * 8)));//宽度
           }
           list.Add("S7:[UnnormalConnection]DB103,W108");//标志位 
           //list.Add("S7:[UnnormalConnection]DB103,W92");//标志位 
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
