using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecialShapeSmoke.Model
{
    public static class ItemCollection
    { 


        public static List<string> GetTaskStatusByShape3Item()// 混合烟道
        {
            List<string> list = new List<string>();
            list.Add("S7:[UnnormalConnection]DB33,DINT12");//第一组1号烟仓 1001 0 
            list.Add("S7:[UnnormalConnection]DB33,DINT16");//第一组2号烟仓 1002 1 
            list.Add("S7:[UnnormalConnection]DB33,DINT20");//第一组59号烟仓  1059 2 
            list.Add("S7:[UnnormalConnection]DB33,DINT24");//第一组60号烟仓  1060 3  
            list.Add("S7:[UnnormalConnection]DB33,DINT28");//第一组61号烟仓  1061 4 
            list.Add("S7:[UnnormalConnection]DB33,DINT32");//第二组1号烟仓  2001  5  
            list.Add("S7:[UnnormalConnection]DB33,DINT36");//第二组2号烟仓   2002 6 
            list.Add("S7:[UnnormalConnection]DB33,DINT40");//第二组59号烟仓  2059 7  
            list.Add("S7:[UnnormalConnection]DB33,DINT44");//第二组60号烟仓  2060 8
            list.Add("S7:[UnnormalConnection]DB33,DINT48");//第二组61号烟仓  2061  9       

            return list;
        }

      
    }
}
