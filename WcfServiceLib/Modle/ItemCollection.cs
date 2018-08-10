using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebService.Modle
{
    public static class ItemCollection
    {
        public static String OpcUnionServer = "S7:[UnionConnection]";


        /// <summary>
        ///   一号主皮带任务位置烟数
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTaskGroupItem1()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 40; i++)
            {
                list.Add(OpcUnionServer + "DB30,DINT"+(850 + (i * 10)));
                list.Add(OpcUnionServer + "DB30,DINT"+(854 + (i * 10)));
                list.Add(OpcUnionServer + "DB30,INT"+(858 + (i * 10))); 
            }
            return list;
        }

        
        public static List<string> GetMachineGroup()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 32; i++)
            {
                list.Add(OpcUnionServer + "DB30,DINT" + (i) * 6);//任务号
               
                
            }
            for (int i = 0; i < 32; i++)
            {
                
                list.Add(OpcUnionServer + "DB30,INT" + (2450 + (i * 2)));//已放烟数量

            }
            return list;
        }

        /// <summary>
        ///   二号主皮带任务位置烟数
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTaskGroupItem2()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 40; i++)
            {
                //list.Add(OpcUnionServer + "DB30,DINT1650" + i * 10);
                //list.Add(OpcUnionServer + "DB30,DINT1654" + i * 10);
                //list.Add(OpcUnionServer + "DB30,INT1658" + i * 10);
                list.Add(OpcUnionServer + "DB30,DINT" + (1250 + (i * 10)));
                list.Add(OpcUnionServer + "DB30,DINT" + (1254 + (i * 10)));
                list.Add(OpcUnionServer + "DB30,INT" + (1258 + (i * 10))); 
            }
            return list;
        }

        /// <summary>
        ///   三号主皮带任务位置烟数
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTaskGroupItem3()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 40;  i++)
            {
                //list.Add(OpcUnionServer + "DB30,DINT2050" + i * 10);
                //list.Add(OpcUnionServer + "DB30,DINT2054" + i * 10);
                //list.Add(OpcUnionServer + "DB30,INT2058" + i * 10);
                list.Add(OpcUnionServer + "DB30,DINT" + (1650 + (i * 10)));
                list.Add(OpcUnionServer + "DB30,DINT" + (1654 + (i * 10)));
                list.Add(OpcUnionServer + "DB30,INT" + (1658 + (i * 10))); 
            }
            return list;
        }

        /// <summary>
        ///   4号主皮带任务位置烟数
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTaskGroupItem4()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 40; i++)
            {
                //list.Add(OpcUnionServer + "DB1,DINT850" + i);
                //list.Add(OpcUnionServer + "DB1,DINT854" + i);
                //list.Add(OpcUnionServer + "DB1,DINT858" + i);
                list.Add(OpcUnionServer + "DB30,DINT" + (2050 + (i * 10)));
                list.Add(OpcUnionServer + "DB30,DINT" + (2054 + (i * 10)));
                list.Add(OpcUnionServer + "DB30,INT" + (2058 + (i * 10))); 
            }
            return list;
        }
        /// <summary>
        /// A组预分拣任务 位置 数量 主皮带号
        /// </summary>
        /// <returns></returns>
        public static List<string> GetASortingItem(string opcSortingServer)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 40; i++)
            {
                list.Add(opcSortingServer + "DB25,DINT" + (0 + (i * 12)));
                list.Add(opcSortingServer + "DB25,REAL" +( 4 + (i * 12)));
                list.Add(opcSortingServer + "DB25,INT" + (8 + (i * 12)));
                list.Add(opcSortingServer + "DB25,INT" + (10 + (i * 12)));
            }
            return list;
        }

        /// <summary>
        /// B组预分拣任务 位置 数量 主皮带号
        /// </summary>
        /// <returns></returns>
        public static List<string> GetBSortingItem(string opcSortingServer)
        { 
            List<string> list = new List<string>();
            for (int i = 0; i < 40; i++)
            {
                list.Add(opcSortingServer + "DB125,DINT" + (0 + (i * 12)));
                list.Add(opcSortingServer + "DB125,REAL" + (4 + (i * 12)));
                list.Add(opcSortingServer + "DB125,INT" + (8 + (i * 12)));
                list.Add(opcSortingServer + "DB125,INT" + (10 + (i * 12)));
            }
           
          
            return list;
        }

        /// <summary>
        ///  读取合流任务和抓数
        /// </summary>
        /// <returns></returns>
        public static List<string> getUnionTaskItem()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 64; i++)
            {
                list.Add(OpcUnionServer + "DB30,DINT" + (i) * 6);//任务号  
                list.Add(OpcUnionServer + "DB30,W" + (4 + (i * 6)));//抓烟条数  
            }
            return list;
        }
        /// <summary>
        /// 获取合流机械手当前任务号和抓数
        /// </summary>
        /// <returns></returns>
        public static List<string> GetUnionMachinNowTaskeItem()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 64; i++ )
            {
                list.Add(OpcUnionServer + "DB20,DINT" + (256 + (i * 10)));//当前任务号
                list.Add(OpcUnionServer + "DB20,W" +( 260 + (i * 10)));//当前抓烟条数

            }
            return list;
        }
      
     
    }
}
