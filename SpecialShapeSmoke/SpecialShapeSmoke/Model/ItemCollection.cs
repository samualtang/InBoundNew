using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecialShapeSmoke.Model
{
    public static class ItemCollection
    {
        /// <summary>
        /// 混合烟道条烟完成,用于混合道的补烟提示
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTaskStatusByShapeItem()// 混合烟道
        {
            List<string> list = new List<string>();

            list.Add("S7:[UnnormalConnection]DB33,DINT12");//第一组1号烟仓 1001
            list.Add("S7:[UnnormalConnection]DB33,DINT16");//第一组2号烟仓 1002
            list.Add("S7:[UnnormalConnection]DB33,DINT20");//第一组59号烟仓
            list.Add("S7:[UnnormalConnection]DB33,DINT24");//第一组60号烟仓
            list.Add("S7:[UnnormalConnection]DB33,DINT28");//第一组61号烟仓
            list.Add("S7:[UnnormalConnection]DB33,DINT32");//第二组1号烟仓
            list.Add("S7:[UnnormalConnection]DB33,DINT36");//第二组2号烟仓
            list.Add("S7:[UnnormalConnection]DB33,DINT40");//第二组59号烟仓
            list.Add("S7:[UnnormalConnection]DB33,DINT44");//第二组60号烟仓
            list.Add("S7:[UnnormalConnection]DB33,DINT48");//第二组61号烟仓          
            return list;
        }
    }
}
