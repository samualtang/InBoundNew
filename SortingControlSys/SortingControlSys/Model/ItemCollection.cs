using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortingControlSys.Model
{
    public static class ItemCollection
    {
        public static String OpcPresortServer = "S7:[FJConnectionGroup1]";

        /// <summary>
        /// 获取任务item
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTaskItem()
        {
            List<string> list = new List<string>();
            for (var i = 2; i <= 52; i++, i++)
            {
                list.Add(OpcPresortServer+"DB1,W" + i);
            }
            //list.Add(OpcPresortServer+"DB1,W54");
            //list.Add(OpcPresortServer+"DB1,W56");
            //list.Add(OpcPresortServer+"DB1,W58");
            //list.Add(OpcPresortServer+"DB1,W60");
            //list.Add(OpcPresortServer+"DB1,W62");
            //list.Add(OpcPresortServer+"DB1,W64");
            //list.Add(OpcPresortServer+"DB1,W66");
            //list.Add(OpcPresortServer+"DB1,W68");
            //list.Add(OpcPresortServer+"DB1,W70");
            //list.Add(OpcPresortServer+"DB1,W72");
            //list.Add(OpcPresortServer+"DB1,W74");
            //list.Add(OpcPresortServer+"DB1,W76");
            //list.Add(OpcPresortServer+"DB1,W78");
            //list.Add(OpcPresortServer+"DB1,W80");
            //list.Add(OpcPresortServer+"DB1,W82");
            //list.Add(OpcPresortServer+"DB1,W84");
            //list.Add(OpcPresortServer"+DB1,W86");
            //list.Add(OpcPresortServer+"DB1,W88");
            //list.Add(OpcPresortServer+"DB1,W90");
            //list.Add(OpcPresortServer+"DB1,W92");
            //list.Add(OpcPresortServer+"DB1,W94");
            list.Add(OpcPresortServer+"DB1,W0"); 
            return list;
        }
        /// <summary>
        /// 监控标志位
        /// </summary>
        /// <returns></returns>
        public static List<string> GetSendTaskStateItem()
        {
            List<string> list = new List<string>();
            list.Add(OpcPresortServer + "DB1,W0");//第一组 标志位
            list.Add(OpcPresortServer + "DB101,W0");//第二组 标志位
            return list;
        }
        /// <summary>
        /// 第二组任务
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTaskStausItemGroup()
        {
            List<string> list = new List<string>();
            for (var i = 2; i <= 52; i++, i++)
            {
                list.Add(OpcPresortServer+"DB101,W" + i);
            }
             list.Add(OpcPresortServer+"DB101,W0"); 
            return list;
        }
        public static List<string> GetTaskStatusItem1()
        {
            List<string> list = new List<string>();
            for (int i = 0; i <= 78; i++, i++)
            {
                list.Add(OpcPresortServer+"DB30,W" + i);
            }

            return list;
        }
        /// <summary>
        /// 第一组完成信息
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTaskStatusItem2()
        {
            List<string> list = new List<string>();
            for (int i = 100; i <= 178; i++, i++)
            {
                list.Add(OpcPresortServer+"DB30,W" + i);
            }
          //  list.Add(OpcPresortServer + "DB1,W0");//第一组 标志位
            return list;
        }
        public static List<string> GetTaskStatusItem3()//A01-A27
        {
            List<string> list = new List<string>();
            for (int i = 1; i <= 27; i++)
            {
                list.Add(OpcPresortServer+"DB4,W" + i * 2);
            }
            return list;
        }

        public static List<string> GetTaskStatusSECItem3()//B01-B27
        {
            List<string> list = new List<string>();
            for (int i = 41; i <= 67; i++)
            {
                list.Add(OpcPresortServer+"DB4,W" + i * 2);
            }
            return list;
        }
        public static List<string> GetTaskStatusItem4()//烟柜
        {
            List<string> list = new List<string>();
            for (int i = 1; i <= 22; i++)
            {
                list.Add(OpcPresortServer+"DB40,W" + ((i - 1) * 20));//M1
                list.Add(OpcPresortServer+"DB40,W" + ((i - 1) * 20 + 2));//M2
                list.Add(OpcPresortServer+"DB40,W" + ((i - 1) * 20 + 4));//M3
                list.Add(OpcPresortServer+"DB40,W" + ((i - 1) * 20 + 6));//M4 (M1-M4 烟柜四个设备)
                list.Add(OpcPresortServer+"DB40,W" + ((i - 1) * 20 + 8));//机械手
                list.Add(OpcPresortServer+"DB40,W" + ((i - 1) * 20 + 10));//为烟柜内的条烟总数
                list.Add(OpcPresortServer+"DB40,W" + ((i - 1) * 20 + 12));//为烟柜前端皮带的条烟总数
                list.Add(OpcPresortServer+"DB40,W" + ((i - 1) * 20 + 14));//为机械手已经分拣的条烟总数
                list.Add(OpcPresortServer+"DB40,W" + ((i - 1) * 20 + 16));//为机械手今天的任务总量
                list.Add(OpcPresortServer+"DB40,W" + ((i - 1) * 20 + 18));//为烟柜内部皮带的条烟总数
            }


            return list;
        }

        //public static List<string> GetTaskStatusSECItem4()//烟柜
        //{
        //    List<string> list = new List<string>();
        //    for (int i = 1; i <= 11; i++)
        //    {
        //        list.Add(OpcPresortServer+"DB140,W" + (i - 1) * 20);//M1
        //        list.Add(OpcPresortServer+"DB140,W" + (i - 1) * 20 + 2);//M2
        //        list.Add(OpcPresortServer+"DB140,W" + (i - 1) * 20 + 4);//M3
        //        list.Add(OpcPresortServer+"DB140,W" + (i - 1) * 20 + 6);//M4 (M1-M4 烟柜四个设备)
        //        list.Add(OpcPresortServer+"DB140,W" + (i - 1) * 20 + 8);//机械手
        //        list.Add(OpcPresortServer+"DB140,W" + (i - 1) * 20 + 10);//为烟柜内的条烟总数
        //        list.Add(OpcPresortServer+"DB140,W" + (i - 1) * 20 + 12);//为烟柜前端皮带的条烟总数
        //        list.Add(OpcPresortServer+"DB140,W" + (i - 1) * 20 + 14);//为机械手已经分拣的条烟总数
        //        list.Add(OpcPresortServer+"DB140,W" + (i - 1) * 20 + 16);//为机械手今天的任务总量
        //        list.Add(OpcPresortServer+"DB140,W" + (i - 1) * 20 + 18);//为烟柜内部皮带的条烟总数
        //    }


        //    return list;
        //}
      
        public static List<string> GetTaskStatusSECItem1()
        {
            List<string> list = new List<string>();
            for (int i = 0; i <= 78; i++, i++)
            {
                list.Add(OpcPresortServer+"DB130,W" + i);
            }

            return list;
        }
        /// <summary>
        /// 第二组完成信息
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTaskStatusSECItem2()
        {
            List<string> list = new List<string>();
            for (int i = 100; i <= 178; i++, i++)
            {
                list.Add(OpcPresortServer+"DB130,W" + i);
            }
           // list.Add(OpcPresortServer + "DB101,W0");//第二组 标志位
            return list;
        }


        public static List<String> GetTaskStatusSecItem5()
        {
            List<string> list = new List<string>();
            for (int i = 200; i <= 278; i++, i++)
            {
                list.Add(OpcPresortServer+"DB10,W" + i);
            }
            return list;
        }

        /// <summary>
        /// 清除任务地址
        /// </summary>
        /// <returns></returns>
        public static List<String> GetClearTaskItem()
        {
            List<string> list = new List<string>(); 
            list.Add(OpcPresortServer+"M40.2");
            list.Add(OpcPresortServer+"M40.3");
            return list;
        }

    }
}
