using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortingControlSys.PubFunc
{
    class SortingPub
    {
        public SortingPub()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //

        }

        /*
         * 格式化字符串，结果长度len，不够长度的字符串前面添加0
         */
        public static string formatData(string str,int len)
        {
            string retStr = str;
            for (int i = 0; i < len; i++)
            {
                retStr = "0" + retStr;
            }
            retStr = retStr.Substring(retStr.Length-len);
                return retStr;
        }

        public static string unformatData(String str)
        {
            string retStr = str;
            string r = @"[1-9]\d+";
            str = System.Text.RegularExpressions.Regex.Match(str, r).ToString();
            retStr = str;
            //Console.WriteLine("str:" + str);
            return retStr;
        }
    }
}
