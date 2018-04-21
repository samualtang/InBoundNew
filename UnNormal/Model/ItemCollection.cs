using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortingControlSys.Model
{
   public static class ItemCollection
    {
       /// <summary>
       /// 获取任务item
       /// </summary>
       /// <returns></returns>
       public static List<string> GetTaskItem()//第一组任务
       {
           List<string> list = new List<string>();
           for (int i = 0; i <= 904; i++)
           {
               list.Add("S7:[UnnormalConnection]DB30,DINT"+i);
               i += 3;
           }
   
           return list;
       }

       public static List<string> GetTaskItem1()//第二组任务
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
       /// 六组烟柜任务
       /// </summary>
       /// <returns></returns>
       public static List<string> GetSixCabinetTaskItem()//六个烟柜任务
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
       /// 监控三个发送标志位
       /// </summary>
       /// <returns></returns>
       public static List<string> GetSendStatesItem() 
       {
           List<string> list = new List<string>();
           list.Add("S7:[UnnormalConnection]DB30,DINT900");//第一组
           list.Add("S7:[UnnormalConnection]DB31,DINT900");//第二组
           list.Add("S7:[UnnormalConnection]DB32,DINT900");//烟柜

           return list;
       }
       /// <summary>
       /// 获取完成信号
       /// </summary>
       /// <returns></returns>
       public static List<string> GetFinishSignalTaskItem() 
       {
           List<string> list = new List<string>(); 
           list.Add("S7:[UnnormalConnection]DB33,DINT0");//烟柜
           list.Add("S7:[UnnormalConnection]DB33,DINT4");//烟仓
           list.Add("S7:[UnnormalConnection]DB33,DINT8");//混合道

           return list;
       }
       public static List<string> GetTaskError()
       {
           List<string> list = new List<string>();
           double place0=0d, place1=0d;
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
               list.Add("S7:[UnnormalConnection]DB400,DINT"+i);
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
