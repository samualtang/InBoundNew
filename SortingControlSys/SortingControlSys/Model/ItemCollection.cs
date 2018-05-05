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
        /// 获取任务item 第一组任务
        /// </summary>
        /// <returns></returns>
        public static List<string> GetGroup1TaskItem()
        {
            List<string> list = new List<string>();
            //for (var i = 2; i <= 52; i++, i++)
            //{
            //    list.Add(OpcPresortServer+"DB1,W" + i);
            //}
            list.Add(OpcPresortServer + "DB1,DWORD2");//任务号 0
            list.Add(OpcPresortServer + "DB1,W6");//虚拟出口号 1
            list.Add(OpcPresortServer + "DB1,W8");//主皮带号 2
            list.Add(OpcPresortServer + "DB1,W10");//条烟总数 3
            list.Add(OpcPresortServer + "DB1,W12");//烟柜1条数 4
            list.Add(OpcPresortServer + "DB1,W14");//起始位置 5
            list.Add(OpcPresortServer + "DB1,W16");//烟柜2条数 6 
            list.Add(OpcPresortServer + "DB1,W18");//起始位置 7
            list.Add(OpcPresortServer + "DB1,W20");//烟柜3条数 8
            list.Add(OpcPresortServer + "DB1,W22");//起始位置 9
            list.Add(OpcPresortServer + "DB1,W24");//烟柜4条数 10
            list.Add(OpcPresortServer + "DB1,W26");//起始位置 11
            list.Add(OpcPresortServer + "DB1,W28");//烟柜5条数 12
            list.Add(OpcPresortServer + "DB1,W30");//起始位置 13
            list.Add(OpcPresortServer + "DB1,W32");//烟柜6条数 14
            list.Add(OpcPresortServer + "DB1,W34");//起始位置 15
            list.Add(OpcPresortServer + "DB1,W36");//烟柜7条数 16
            list.Add(OpcPresortServer + "DB1,W38");//起始位置 17
            list.Add(OpcPresortServer + "DB1,W40");//烟柜8条数 18
            list.Add(OpcPresortServer + "DB1,W42");//起始位置 19
            list.Add(OpcPresortServer + "DB1,W44");//烟柜9条数 20
            list.Add(OpcPresortServer + "DB1,W46");//起始位置 21
            list.Add(OpcPresortServer + "DB1,W48");//烟柜10条数 22 
            list.Add(OpcPresortServer + "DB1,W50");//起始位置 23
            list.Add(OpcPresortServer + "DB1,W52");//烟柜11条数 24
            list.Add(OpcPresortServer + "DB1,W54");//起始位置 25 

            list.Add(OpcPresortServer + "DB1,W0");//标志位 26
            return list;
        }

        /// <summary>
        /// 第二组任务
        /// </summary>
        /// <returns></returns>
        public static List<string> GetGroup2TaskItem()
        {
            List<string> list = new List<string>();
            //for (var i = 2; i <= 52; i++, i++)
            //{
            //    list.Add(OpcPresortServer + "DB101,W" + i);
            //}
            list.Add(OpcPresortServer + "DB101,DWORD2");//任务号 0
            list.Add(OpcPresortServer + "DB101,W6");//虚拟出口号 1
            list.Add(OpcPresortServer + "DB101,W8");//主皮带号 2
            list.Add(OpcPresortServer + "DB101,W10");//条烟总数 3
            list.Add(OpcPresortServer + "DB101,W12");//烟柜1条数 4
            list.Add(OpcPresortServer + "DB101,W14");//起始位置 5
            list.Add(OpcPresortServer + "DB101,W16");//烟柜2条数 6 
            list.Add(OpcPresortServer + "DB101,W18");//起始位置 7
            list.Add(OpcPresortServer + "DB101,W20");//烟柜3条数 8
            list.Add(OpcPresortServer + "DB101,W22");//起始位置 9
            list.Add(OpcPresortServer + "DB101,W24");//烟柜4条数 10
            list.Add(OpcPresortServer + "DB101,W26");//起始位置 11
            list.Add(OpcPresortServer + "DB101,W28");//烟柜5条数 12
            list.Add(OpcPresortServer + "DB101,W30");//起始位置 13
            list.Add(OpcPresortServer + "DB101,W32");//烟柜6条数 14
            list.Add(OpcPresortServer + "DB101,W34");//起始位置 15
            list.Add(OpcPresortServer + "DB101,W36");//烟柜7条数 16
            list.Add(OpcPresortServer + "DB101,W38");//起始位置 17
            list.Add(OpcPresortServer + "DB101,W40");//烟柜8条数 18
            list.Add(OpcPresortServer + "DB101,W42");//起始位置 19
            list.Add(OpcPresortServer + "DB101,W44");//烟柜9条数 20
            list.Add(OpcPresortServer + "DB101,W46");//起始位置 21
            list.Add(OpcPresortServer + "DB101,W48");//烟柜10条数 22 
            list.Add(OpcPresortServer + "DB101,W50");//起始位置 23
            list.Add(OpcPresortServer + "DB101,W52");//烟柜11条数 24
            list.Add(OpcPresortServer + "DB101,W54");//起始位置 25 

            list.Add(OpcPresortServer + "DB101,W0");//标志位 26
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
        public static List<string> GetFinishTaskStatusItem1()
        {
            List<string> list = new List<string>();
            for (int i = 100; i <= 256; i = i + 4) 
            {
                list.Add(OpcPresortServer + "DB30,DWORD" + i);
            }
            //list.Add(OpcPresortServer + "DB30,DWORD100");
            //list.Add(OpcPresortServer + "DB30,DWORD104");
            //list.Add(OpcPresortServer + "DB30,DWORD108");
            //list.Add(OpcPresortServer + "DB30,DWORD112");
            //list.Add(OpcPresortServer + "DB30,DWORD116");
            //list.Add(OpcPresortServer + "DB30,DWORD124");
            //list.Add(OpcPresortServer + "DB30,DWORD128");
            //list.Add(OpcPresortServer + "DB30,DWORD132");
            //list.Add(OpcPresortServer + "DB30,DWORD136");
            //list.Add(OpcPresortServer + "DB30,DWORD140");
            //list.Add(OpcPresortServer + "DB30,DWORD144");
            //list.Add(OpcPresortServer + "DB30,DWORD148");
            //list.Add(OpcPresortServer + "DB30,DWORD152");
            //list.Add(OpcPresortServer + "DB30,DWORD156");
            //list.Add(OpcPresortServer + "DB30,DWORD160");
            //list.Add(OpcPresortServer + "DB30,DWORD164");
            //list.Add(OpcPresortServer + "DB30,DWORD168");
            //list.Add(OpcPresortServer + "DB30,DWORD172");
            //list.Add(OpcPresortServer + "DB30,DWORD176");
            //list.Add(OpcPresortServer + "DB30,DWORD178");
         
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
        public static List<string> GetFinishTaskStatusItem2()
        {
            List<string> list = new List<string>();
            for (int i = 100; i <= 256; i = i + 4) 
            {
                list.Add(OpcPresortServer + "DB130,DWORD" + i);
            }
            //list.Add(OpcPresortServer + "DB130,DWORD100");
            //list.Add(OpcPresortServer + "DB130,DWORD104");
            //list.Add(OpcPresortServer + "DB130,DWORD108");
            //list.Add(OpcPresortServer + "DB130,DWORD112");
            //list.Add(OpcPresortServer + "DB130,DWORD116");
            //list.Add(OpcPresortServer + "DB130,DWORD124");
            //list.Add(OpcPresortServer + "DB130,DWORD128");
            //list.Add(OpcPresortServer + "DB130,DWORD132");
            //list.Add(OpcPresortServer + "DB130,DWORD136");
            //list.Add(OpcPresortServer + "DB130,DWORD140");
            //list.Add(OpcPresortServer + "DB130,DWORD144");
            //list.Add(OpcPresortServer + "DB130,DWORD148");
            //list.Add(OpcPresortServer + "DB130,DWORD152");
            //list.Add(OpcPresortServer + "DB130,DWORD156");
            //list.Add(OpcPresortServer + "DB130,DWORD160");
            //list.Add(OpcPresortServer + "DB130,DWORD164");
            //list.Add(OpcPresortServer + "DB130,DWORD168");
            //list.Add(OpcPresortServer + "DB130,DWORD172");
            //list.Add(OpcPresortServer + "DB130,DWORD176");
            //list.Add(OpcPresortServer + "DB130,DWORD178");
          
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
