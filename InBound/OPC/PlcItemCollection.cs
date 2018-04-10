using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound
{
    public static class PlcItemCollection
    {
        public static String OpcPresortServer1 = "S7:[FJConnectionGroup1]";
        public static String OpcPresortServer2 = "S7:[FJConnectionGroup2]";
        public static String OpcPresortServer3 = "S7:[FJConnectionGroup3]";
        public static String OpcPresortServer4 = "S7:[FJConnectionGroup4]";
               
        public static List<string> GetTaskCountItem()//烟柜
        {
            List<string> list = new List<string>();
            for (int i = 1; i <= 22; i++)
            {
             list.Add(OpcPresortServer1+"DB40,W" + ((i - 1) * 20 + 18));//为烟柜内部皮带的条烟总数
            }
            for (int i = 1; i <= 22; i++)
            {
                list.Add(OpcPresortServer2 + "DB40,W" + ((i - 1) * 20 + 18));//为烟柜内部皮带的条烟总数
            }
            for (int i = 1; i <= 22; i++)
            {
                list.Add(OpcPresortServer3 + "DB40,W" + ((i - 1) * 20 + 18));//为烟柜内部皮带的条烟总数
            }
            for (int i = 1; i <= 22; i++)
            {
                list.Add(OpcPresortServer4 + "DB40,W" + ((i - 1) * 20 + 18));//为烟柜内部皮带的条烟总数
            }

            return list;
        }

       

    }
}
