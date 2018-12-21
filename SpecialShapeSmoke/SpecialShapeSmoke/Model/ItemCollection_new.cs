using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecialShapeSmoke.Model
{
    public static class ItemCollection_new
    { 


        public static List<string> GetTaskStatusByShapeItem()// 混合烟道
        {
            List<string> list = new List<string>();
            list.Add("S7:[UnnormalConnection1]DB100,DINT0");//第一组1061 12包装机 已推包号
            list.Add("S7:[UnnormalConnection1]DB100,W4");//第一组1号烟仓 1061 12包装机 出烟条数

            list.Add("S7:[UnnormalConnection1]DB100,DINT6");//第二组2061 34包装机 已推包号
            list.Add("S7:[UnnormalConnection1]DB100,W10");//第二组2061 34包装机 出烟条数

            //list.Add("S7:[UnnormalConnection3]DB100,DINT0");//第二组1号烟仓 2061 34包装机 
            //list.Add("S7:[UnnormalConnection3]DB100,W4");

            list.Add("S7:[UnnormalConnection3]DB100,DINT0");//第三组1号烟仓 3061 56包装机 
            list.Add("S7:[UnnormalConnection3]DB100,W4");

            list.Add("S7:[UnnormalConnection4]DB100,DINT0");//第四组1号烟仓 4061 78包装机 
            list.Add("S7:[UnnormalConnection4]DB100,W4");
             
            return list;
        }

      
    }
}
