using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FollowTask.Modle
{
    public static class ItemCollection
    {
        public static String OpcUnionServer = "S7:[UnionConnection]";

        /// <summary>
        ///   一号主皮带任务交互区 
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTaskGroupItem1()
        {
            List<string> list = new List<string>();
            list.Add(OpcUnionServer + "DB1,DWORD58");  //任务号 0 d58 4位
            list.Add(OpcUnionServer + "DB1,W62");   //出口号 1
            list.Add(OpcUnionServer + "DB1,W64");   //包装机号 2
            list.Add(OpcUnionServer + "DB1,W66");   //总条数 3
            list.Add(OpcUnionServer + "DB1,W68");   //1号机械手抓烟数 4
            list.Add(OpcUnionServer + "DB1,W70");   //2号机械手抓烟数 5
            list.Add(OpcUnionServer + "DB1,W72");   //3号机械手抓烟数 6 
            list.Add(OpcUnionServer + "DB1,W74");   //4号机械手抓烟数 7 
            list.Add(OpcUnionServer + "DB1,W76");   //5号机械手抓烟数 8
            list.Add(OpcUnionServer + "DB1,W78");   //6号机械手抓烟数 9
            list.Add(OpcUnionServer + "DB1,W80");   //7号机械手抓烟数 10
            list.Add(OpcUnionServer + "DB1,W82");   //8号机械手抓烟数  11 
            list.Add(OpcUnionServer + "DB1,W56");//状态,1为上位写,2为电控接收12
            return list;
        }

        /// <summary>
        ///   二号主皮带任务交互区 
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTaskGroupItem2()
        {
            List<string> list = new List<string>();
            list.Add(OpcUnionServer + "DB1,DWORD86");  //任务号 0
            list.Add(OpcUnionServer + "DB1,W90");   //出口号 1
            list.Add(OpcUnionServer + "DB1,W92");   //包装机号 2
            list.Add(OpcUnionServer + "DB1,W94");   //总条数 3
            list.Add(OpcUnionServer + "DB1,W96");   //9号机械手抓烟数 4
            list.Add(OpcUnionServer + "DB1,W98");   //10号机械手抓烟数 5
            list.Add(OpcUnionServer + "DB1,W100");   //11号机械手抓烟数 6 
            list.Add(OpcUnionServer + "DB1,W102");   //12号机械手抓烟数 7 
            list.Add(OpcUnionServer + "DB1,W104");   //13号机械手抓烟数 8
            list.Add(OpcUnionServer + "DB1,W106");   //14号机械手抓烟数 9
            list.Add(OpcUnionServer + "DB1,W108");   //15号机械手抓烟数 10
            list.Add(OpcUnionServer + "DB1,W110");   //16号机械手抓烟数  11
            list.Add(OpcUnionServer + "DB1,W84");//状态,为上位写,2为电控接收12
            return list;
        }

        /// <summary>
        ///   三号主皮带任务交互区 
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTaskGroupItem3()
        {
            List<string> list = new List<string>();
            list.Add(OpcUnionServer + "DB1,DWORD114");  //任务号 0
            list.Add(OpcUnionServer + "DB1,W118");   //出口号 1
            list.Add(OpcUnionServer + "DB1,W120");   //包装机号 2
            list.Add(OpcUnionServer + "DB1,W122");   //总条数 3
            list.Add(OpcUnionServer + "DB1,W124");   //17号机械手抓烟数 4
            list.Add(OpcUnionServer + "DB1,W126");   //18号机械手抓烟数 5
            list.Add(OpcUnionServer + "DB1,W128");   //19号机械手抓烟数 6 
            list.Add(OpcUnionServer + "DB1,W130");   //20号机械手抓烟数 7 
            list.Add(OpcUnionServer + "DB1,W132");   //21号机械手抓烟数 8
            list.Add(OpcUnionServer + "DB1,W134");   //22号机械手抓烟数 9
            list.Add(OpcUnionServer + "DB1,W136");   //23号机械手抓烟数 10
            list.Add(OpcUnionServer + "DB1,W138");   //24号机械手抓烟数  11
            list.Add(OpcUnionServer + "DB1,W112");//状态,1为上位写,2为电控接收12
            return list;
        }

        /// <summary>
        ///   四号主皮带任务交互区 
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTaskGroupItem4()
        {
            List<string> list = new List<string>();
            list.Add(OpcUnionServer + "DB1,DWORD142");  //任务号 0
            list.Add(OpcUnionServer + "DB1,W146");   //出口号 1
            list.Add(OpcUnionServer + "DB1,W148");   //包装机号 2
            list.Add(OpcUnionServer + "DB1,W150");   //总条数 3
            list.Add(OpcUnionServer + "DB1,W152");   //25号机械手抓烟数 4
            list.Add(OpcUnionServer + "DB1,W154");   //26号机械手抓烟数 5
            list.Add(OpcUnionServer + "DB1,W156");   //27号机械手抓烟数 6 
            list.Add(OpcUnionServer + "DB1,W158");   //28号机械手抓烟数 7 
            list.Add(OpcUnionServer + "DB1,W160");   //29号机械手抓烟数 8
            list.Add(OpcUnionServer + "DB1,W162");   //30号机械手抓烟数 9
            list.Add(OpcUnionServer + "DB1,W164");   //31号机械手抓烟数 10
            list.Add(OpcUnionServer + "DB1,W166");   //32号机械手抓烟数  11
            list.Add(OpcUnionServer + "DB1,W140");//状态,1为上位写,2为电控接收12
            return list;
        }

        /// <summary>
        ///  读取合流任务
        /// </summary>
        /// <returns></returns
        public static List<string> getUnionTaskItem()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < 32; i++)
            {
                list.Add(OpcUnionServer + "DB30,DINT" + (i) * 6);//标志位 26
                list.Add(OpcUnionServer + "DB30,INT" + (4 + (i) * 6));//标志位 26
            }
            return list;
        }

        public static List<string> MachineItemNo1()
        {
            List<string> list = new List<string>();
            return list;
        }
        public static List<string> MachineItemNo2()
        {
            List<string> list = new List<string>();
            return list;
        }
        public static List<string> MachineItemNo3()
        {
            List<string> list = new List<string>();
            return list;
        }
        public static List<string> MachineItemNo4()
        {
            List<string> list = new List<string>();
            return list;
        }
        public static List<string> MachineItemNo5()
        {
            List<string> list = new List<string>();
            return list;
        }
        public static List<string> MachineItemNo6()
        {
            List<string> list = new List<string>();
            return list;
        }
        public static List<string> MachineItemNo7()
        {
            List<string> list = new List<string>();
            return list;
        }
        public static List<string> MachineItemNo8()
        {
            List<string> list = new List<string>();
            return list;
        }
    }
}
