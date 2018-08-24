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
            list.Add("S7:[UnnormalConnection]DB100,DINT0");//第一组1号烟仓 1061 12包装机 
            list.Add("S7:[UnnormalConnection]DB100,W4");//第一组1号烟仓 1061 12包装机 出烟条数

            list.Add("S7:[UnnormalConnection]DB100,DINT6");//第一组2号烟仓 1061 34包装机
            list.Add("S7:[UnnormalConnection]DB100,W10");//第一组2号烟仓 1061 34包装机 出烟条数

            list.Add("S7:[UnnormalConnection]DB100,DINT12");//第一组1号烟仓 2061 56包装机 
            list.Add("S7:[UnnormalConnection]DB100,W16");//第一组1号烟仓 2061 56包装机 出烟条数

            list.Add("S7:[UnnormalConnection]DB100,DINT18");//第一组2号烟仓 2061 78包装机 
            list.Add("S7:[UnnormalConnection]DB100,W22");//第一组2号烟仓 2061 78包装机 出烟条数
             
            return list;
        }

      
    }
}
