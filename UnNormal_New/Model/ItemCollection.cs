using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnNormal_New.Model
{
   public static class ItemCollection
   {
       #region 之前的协议
       ///// <summary>-------------之前协议
       /// <summary>
       /// 获取任务item
       /// </summary>
       /// <returns></returns>
       //public static List<string> GetTaskItem()//第一组任务
       //{
       //    List<string> list = new List<string>();
       //    for (int i = 0; i <= 904; i++)
       //    {
       //        list.Add("S7:[UnnormalConnection]DB30,DINT"+i);
       //        i += 3;
       //    }
   
       //    return list;
       //}

       //public static List<string> GetTaskItem1()//第二组任务
       //{
       //    List<string> list = new List<string>();
       //    for (int i = 0; i <= 904; i++)
       //    {
       //        list.Add("S7:[UnnormalConnection]DB31,DINT" + i);
       //        i += 3;
       //    }
          
       //    return list;
       //}
       

       ///// <summary>
       ///// 六组烟柜任务
       ///// </summary>
       ///// <returns></returns>
       //public static List<string> GetSixCabinetTaskItem()//六个烟柜任务
       //{
       //    List<string> list = new List<string>();
       //    for (int i = 0; i <= 904; i++)
       //    {
       //        list.Add("S7:[UnnormalConnection]DB32,DINT" + i);
       //        i += 3;
       //    }

       //    return list;
       //}
       ///// <summary> 
       ///// 监控三个发送标志位
       ///// </summary>
       ///// <returns></returns>
       //public static List<string> GetSendStatesItem() 
       //{
       //    List<string> list = new List<string>();
       //    list.Add("S7:[UnnormalConnection]DB30,DINT900");//第一组
       //    list.Add("S7:[UnnormalConnection]DB31,DINT900");//第二组
       //    list.Add("S7:[UnnormalConnection]DB32,DINT900");//烟柜

       //    return list;
       //}
        ///// <summary>-------------之前协议
       //--------------------------------新的协议

        
#endregion

       #region
       /// <summary>
       /// 1线分拣订单信息
       /// </summary>
       /// <returns></returns>
       public static List<string> GetTaskItem()//1线分拣订单信息
       {
           List<string> list = new List<string>();
           for (int i = 0; i <= 904; i++)
           {
               list.Add("S7:[UnnormalConnection]DB30,DINT" + i);
               i += 3;
           }

           return list;
       }
       /// <summary>
       /// //2线分拣订单信息
       /// </summary>
       /// <returns></returns>
       public static List<string> GetTaskItem1()//2线分拣订单信息
       {
           List<string> list = new List<string>();
           for (int i = 0; i <= 904; i++)
           {
               list.Add("S7:[UnnormalConnection]DB31,DINT" + i);
               i += 3;
           }

           return list;
       }

       /// <summary>
       /// 烟柜2线（A）订单信息
       /// </summary>
       /// <returns></returns>
       public static List<string> GetSixCabinetTaskItem2A()//烟柜2线（A）订单信息
       {
           List<string> list = new List<string>();
           for (int i = 0; i <= 904; i++)
           {
               list.Add("S7:[UnnormalConnection]DB32,DINT" + i);
               i += 3;
           }

           return list;
       }

       /// <summary>
       /// 烟柜1线（B）订单信息
       /// </summary>
       /// <returns></returns>
       public static List<string> GetSixCabinetTaskItem1B()//烟柜1线（B）订单信息
       {
           List<string> list = new List<string>();
           for (int i = 0; i <= 904; i++)
           {
               list.Add("S7:[UnnormalConnection]DB34,DINT" + i);
               i += 3;
           }

           return list;
       }
       /// <summary>
       /// 监控四个发送标志位
       /// </summary>
       /// <returns></returns>
       public static List<string> GetSendStatesItem()
       {
           List<string> list = new List<string>();
           list.Add("S7:[UnnormalConnection]DB30,DINT900");//第一组
           list.Add("S7:[UnnormalConnection]DB31,DINT900");//第二组
           list.Add("S7:[UnnormalConnection]DB32,DINT900");// 烟柜2线（A）订单信息
           list.Add("S7:[UnnormalConnection]DB34,DINT900");// 烟柜1线（B）订单信息
           return list;
       }

       /// <summary>
       /// 重新发送标志位
       /// </summary>
       /// <returns></returns>
       public static List<string> GetFristSendItem()
       {
           List<string> list = new List<string>();
           list.Add("S7:[UnnormalConnection]DB100,DINT0");//第一组
           list.Add("S7:[UnnormalConnection]DB100,DINT4");//第二组
           list.Add("S7:[UnnormalConnection]DB100,DINT8");// 烟柜2线（A）订单信息
           list.Add("S7:[UnnormalConnection]DB100,DINT12");// 烟柜1线（B）订单信息
           return list;
       }
       /// <summary>
       /// 获取完成信号
       /// </summary>
       /// <returns></returns>
       public static List<string> GetFinishSignalTaskItem() 
       {
           List<string> list = new List<string>();
           list.Add("S7:[UnnormalConnection]DB33,DINT0");//1线分拣订单完成信息
           list.Add("S7:[UnnormalConnection]DB33,DINT4");//2线分拣订单完成信息
           list.Add("S7:[UnnormalConnection]DB33,DINT8");//烟柜2线（A）完成信息
           list.Add("S7:[UnnormalConnection]DB33,DINT12");// 烟柜1线（B）完成信息
           return list;
       }
       #endregion


       /// <summary>
       /// 异形烟1线DB地址
       /// </summary>
       /// <returns></returns>
       public static List<string> GetTask1ALineItem()
       {
           List<string> list = new List<string>();

           list.Add("S7:[UnnormalConnection]DB1,DBD0");
           for (int i = 0; i < 130; i = i + 2)
           {
               list.Add("S7:[UnnormalConnection]DB1,DBW" + (i + 4));
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
               list.Add("S7:[UnnormalConnection]DB50,DBD" + (i * 4));
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

           list.Add("S7:[UnnormalConnection]DB101,DBD0");
           for (int i = 0; i < 130; i = i + 2)
           {
               list.Add("S7:[UnnormalConnection]DB101,DBW" + (i + 4));
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
               list.Add("S7:[UnnormalConnection]DB150,DBD" + (i * 4));
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
           list.Add("S7:[UnnormalConnection]DB910,DBD0");
           for (int i = 0; i < 18; i = i + 2)
           {
               list.Add("S7:[UnnormalConnection]DB910,DBD" + (i + 4));
           }
           list.Add("S7:[UnnormalConnection]DB901,DBW22");
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
               list.Add("S7:[UnnormalConnection]DB950,DBD" + (i * 4));
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
           list.Add("S7:[UnnormalConnection]DB1,DBD132");//一线任务发送标志位  1上位写入 0电控取走 
           list.Add("S7:[UnnormalConnection]DB101,DBD132");//二线任务发送标志位
           list.Add("S7:[UnnormalConnection]DB901,DBW22");//烟柜任务发送标志位
           list.Add("S7:[UnnormalConnection]DB501,DBW16");//合流处任务发送标志位
           return list;
       }
       /// <summary>
       /// 合流处通讯DB地址
       /// </summary>
       /// <returns></returns>
       public static List<string> GetUnUnionItem()
       {
           List<string> list = new List<string>();
           list.Add("S7:[UnnormalConnection]DB501,DBD0");
           for (int i = 0; i < 11; i = i + 2)
           {
               list.Add("S7:[UnnormalConnection]DB501,DBD" + (i + 4));
           }
           list.Add("S7:[UnnormalConnection]DB501,DBW16");
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
               list.Add("S7:[UnnormalConnection]DB550,DBD" + (i * 4));
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

   }
}
