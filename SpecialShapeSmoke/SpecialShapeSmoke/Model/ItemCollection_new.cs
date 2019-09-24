using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecialShapeSmoke.Model
{
    public static class ItemCollection_new
    { 


        public static List<string> GetTaskStatusByShapeItem()// 混合烟道61
        {
            List<string> list = new List<string>();
            list.Add("S7:[UnnormalConnection1]DB100,DINT0");//第一组1061 12包装机 已推包号
            list.Add("S7:[UnnormalConnection1]DB100,W4");//第一组1号烟仓 1061 12包装机 出烟条数

            list.Add("S7:[UnnormalConnection1]DB100,DINT6");//第二组2061 34包装机 已推包号
            list.Add("S7:[UnnormalConnection1]DB100,W10");//第二组2061 34包装机 出烟条数

            list.Add("S7:[UnnormalConnection3]DB100,DINT0");//第三组1号烟仓 3061 56包装机 
            list.Add("S7:[UnnormalConnection3]DB100,W4");

            list.Add("S7:[UnnormalConnection4]DB100,DINT0");//第四组1号烟仓 4061 78包装机 
            list.Add("S7:[UnnormalConnection4]DB100,W4");
             
            return list;
        }

        public static List<string> GetYCTaskStatusByShapeItem()// 混合烟道1和60
        {
            List<string> list = new List<string>();
            list.Add("S7:[UnnormalConnection1]DB8,DINT0");//1线 1号混合烟仓  任务号  拨烟数量
            list.Add("S7:[UnnormalConnection1]DB17,W0");

            list.Add("S7:[UnnormalConnection1]DB8,DINT236");//1线 60号混合烟仓  任务号  拨烟数量
            list.Add("S7:[UnnormalConnection1]DB17,W118");

            list.Add("S7:[UnnormalConnection1]DB8,DINT0");//2线 1号混合烟仓  任务号  拨烟数量
            list.Add("S7:[UnnormalConnection1]DB17,W0");

            list.Add("S7:[UnnormalConnection1]DB8,DINT236");//2线 60号混合烟仓  任务号  拨烟数量
            list.Add("S7:[UnnormalConnection1]DB17,W118");

            list.Add("S7:[UnnormalConnection3]DB8,DINT0");//3线 1号混合烟仓  任务号  拨烟数量
            list.Add("S7:[UnnormalConnection3]DB17,W0");

            list.Add("S7:[UnnormalConnection3]DB8,DINT236");//3线 60号混合烟仓  任务号  拨烟数量
            list.Add("S7:[UnnormalConnection3]DB17,W118"); 

            list.Add("S7:[UnnormalConnection4]DB8,DINT0");//4线 1号混合烟仓  任务号  拨烟数量
            list.Add("S7:[UnnormalConnection4]DB17,W0");

            list.Add("S7:[UnnormalConnection4]DB8,DINT236");//4线 60号混合烟仓  任务号  拨烟数量
            list.Add("S7:[UnnormalConnection4]DB17,W118"); 

            return list;
        }

        public static List<string> GetSortBeltByShapeItem(string plcid)// 混合烟道白皮带 
        {
            List<string> list = new List<string>();
            switch (plcid)
            {
                case "1":
                    #region  第一组PLC DB块
                list.Add("S7:[UnnormalConnection1]DB100,W24");
                list.Add("S7:[UnnormalConnection1]DB100,DINT26");
                list.Add("S7:[UnnormalConnection1]DB100,DINT30");
                list.Add("S7:[UnnormalConnection1]DB100,DINT34");
                list.Add("S7:[UnnormalConnection1]DB100,DINT38");
                list.Add("S7:[UnnormalConnection1]DB100,DINT42");
                list.Add("S7:[UnnormalConnection1]DB100,DINT46");
                list.Add("S7:[UnnormalConnection1]DB100,DINT50");
                list.Add("S7:[UnnormalConnection1]DB100,DINT54");
                list.Add("S7:[UnnormalConnection1]DB100,W58");
                list.Add("S7:[UnnormalConnection1]DB100,DINT60");
                list.Add("S7:[UnnormalConnection1]DB100,DINT64");
                list.Add("S7:[UnnormalConnection1]DB100,DINT68");
                list.Add("S7:[UnnormalConnection1]DB100,DINT72");
                list.Add("S7:[UnnormalConnection1]DB100,DINT76");
                list.Add("S7:[UnnormalConnection1]DB100,DINT80");
                list.Add("S7:[UnnormalConnection1]DB100,DINT84");
                list.Add("S7:[UnnormalConnection1]DB100,DINT88");
                list.Add("S7:[UnnormalConnection1]DB100,W92");
                list.Add("S7:[UnnormalConnection1]DB100,DINT94");
                list.Add("S7:[UnnormalConnection1]DB100,DINT98");
                list.Add("S7:[UnnormalConnection1]DB100,DINT102");
                list.Add("S7:[UnnormalConnection1]DB100,DINT106");
                list.Add("S7:[UnnormalConnection1]DB100,DINT110");
                list.Add("S7:[UnnormalConnection1]DB100,DINT114");
                list.Add("S7:[UnnormalConnection1]DB100,DINT118");
                list.Add("S7:[UnnormalConnection1]DB100,DINT122");
                list.Add("S7:[UnnormalConnection1]DB100,W126");
                list.Add("S7:[UnnormalConnection1]DB100,DINT128");
                list.Add("S7:[UnnormalConnection1]DB100,DINT132");
                list.Add("S7:[UnnormalConnection1]DB100,DINT136");
                list.Add("S7:[UnnormalConnection1]DB100,DINT140");
                list.Add("S7:[UnnormalConnection1]DB100,DINT144");
                list.Add("S7:[UnnormalConnection1]DB100,DINT148");
                list.Add("S7:[UnnormalConnection1]DB100,DINT152");
                list.Add("S7:[UnnormalConnection1]DB100,DINT156");
                list.Add("S7:[UnnormalConnection1]DB100,W160");
                list.Add("S7:[UnnormalConnection1]DB100,DINT162");
                list.Add("S7:[UnnormalConnection1]DB100,DINT166");
                list.Add("S7:[UnnormalConnection1]DB100,DINT170");
                list.Add("S7:[UnnormalConnection1]DB100,DINT174");
                list.Add("S7:[UnnormalConnection1]DB100,DINT178");
                list.Add("S7:[UnnormalConnection1]DB100,DINT182");
                list.Add("S7:[UnnormalConnection1]DB100,DINT186");
                list.Add("S7:[UnnormalConnection1]DB100,DINT190");
                list.Add("S7:[UnnormalConnection1]DB100,DINT364");
                list.Add("S7:[UnnormalConnection1]DB100,W372");

                #endregion 
                break;
                case "2":
                    #region  第二组PLC DB块
            list.Add("S7:[UnnormalConnection1]DB100,W194");
            list.Add("S7:[UnnormalConnection1]DB100,DINT196");
            list.Add("S7:[UnnormalConnection1]DB100,DINT200");
            list.Add("S7:[UnnormalConnection1]DB100,DINT204");
            list.Add("S7:[UnnormalConnection1]DB100,DINT208");
            list.Add("S7:[UnnormalConnection1]DB100,DINT212");
            list.Add("S7:[UnnormalConnection1]DB100,DINT216");
            list.Add("S7:[UnnormalConnection1]DB100,DINT220");
            list.Add("S7:[UnnormalConnection1]DB100,DINT224");
            list.Add("S7:[UnnormalConnection1]DB100,W228");
            list.Add("S7:[UnnormalConnection1]DB100,DINT230");
            list.Add("S7:[UnnormalConnection1]DB100,DINT234");
            list.Add("S7:[UnnormalConnection1]DB100,DINT238");
            list.Add("S7:[UnnormalConnection1]DB100,DINT242");
            list.Add("S7:[UnnormalConnection1]DB100,DINT246");
            list.Add("S7:[UnnormalConnection1]DB100,DINT250");
            list.Add("S7:[UnnormalConnection1]DB100,DINT254");
            list.Add("S7:[UnnormalConnection1]DB100,DINT258");
            list.Add("S7:[UnnormalConnection1]DB100,W262");
            list.Add("S7:[UnnormalConnection1]DB100,DINT264");
            list.Add("S7:[UnnormalConnection1]DB100,DINT268");
            list.Add("S7:[UnnormalConnection1]DB100,DINT272");
            list.Add("S7:[UnnormalConnection1]DB100,DINT276");
            list.Add("S7:[UnnormalConnection1]DB100,DINT280");
            list.Add("S7:[UnnormalConnection1]DB100,DINT284");
            list.Add("S7:[UnnormalConnection1]DB100,DINT288");
            list.Add("S7:[UnnormalConnection1]DB100,DINT292");
            list.Add("S7:[UnnormalConnection1]DB100,W296");
            list.Add("S7:[UnnormalConnection1]DB100,DINT298");
            list.Add("S7:[UnnormalConnection1]DB100,DINT302");
            list.Add("S7:[UnnormalConnection1]DB100,DINT306");
            list.Add("S7:[UnnormalConnection1]DB100,DINT310");
            list.Add("S7:[UnnormalConnection1]DB100,DINT314");
            list.Add("S7:[UnnormalConnection1]DB100,DINT318");
            list.Add("S7:[UnnormalConnection1]DB100,DINT322");
            list.Add("S7:[UnnormalConnection1]DB100,DINT326");
            list.Add("S7:[UnnormalConnection1]DB100,W330");
            list.Add("S7:[UnnormalConnection1]DB100,DINT332");
            list.Add("S7:[UnnormalConnection1]DB100,DINT336");
            list.Add("S7:[UnnormalConnection1]DB100,DINT340");
            list.Add("S7:[UnnormalConnection1]DB100,DINT344");
            list.Add("S7:[UnnormalConnection1]DB100,DINT348");
            list.Add("S7:[UnnormalConnection1]DB100,DINT352");
            list.Add("S7:[UnnormalConnection1]DB100,DINT356");
            list.Add("S7:[UnnormalConnection1]DB100,DINT360");
            list.Add("S7:[UnnormalConnection1]DB100,DINT368");
            list.Add("S7:[UnnormalConnection1]DB100,W374");

            #endregion
                break;
                case "3":
                    #region  第三组PLC DB块
            list.Add("S7:[UnnormalConnection3]DB100,W24");
            list.Add("S7:[UnnormalConnection3]DB100,DINT26");
            list.Add("S7:[UnnormalConnection3]DB100,DINT30");
            list.Add("S7:[UnnormalConnection3]DB100,DINT34");
            list.Add("S7:[UnnormalConnection3]DB100,DINT38");
            list.Add("S7:[UnnormalConnection3]DB100,DINT42");
            list.Add("S7:[UnnormalConnection3]DB100,DINT46");
            list.Add("S7:[UnnormalConnection3]DB100,DINT50");
            list.Add("S7:[UnnormalConnection3]DB100,DINT54");
            list.Add("S7:[UnnormalConnection3]DB100,W58");
            list.Add("S7:[UnnormalConnection3]DB100,DINT60");
            list.Add("S7:[UnnormalConnection3]DB100,DINT64");
            list.Add("S7:[UnnormalConnection3]DB100,DINT68");
            list.Add("S7:[UnnormalConnection3]DB100,DINT72");
            list.Add("S7:[UnnormalConnection3]DB100,DINT76");
            list.Add("S7:[UnnormalConnection3]DB100,DINT80");
            list.Add("S7:[UnnormalConnection3]DB100,DINT84");
            list.Add("S7:[UnnormalConnection3]DB100,DINT88");
            list.Add("S7:[UnnormalConnection3]DB100,W92");
            list.Add("S7:[UnnormalConnection3]DB100,DINT94");
            list.Add("S7:[UnnormalConnection3]DB100,DINT98");
            list.Add("S7:[UnnormalConnection3]DB100,DINT102");
            list.Add("S7:[UnnormalConnection3]DB100,DINT106");
            list.Add("S7:[UnnormalConnection3]DB100,DINT110");
            list.Add("S7:[UnnormalConnection3]DB100,DINT114");
            list.Add("S7:[UnnormalConnection3]DB100,DINT118");
            list.Add("S7:[UnnormalConnection3]DB100,DINT122");
            list.Add("S7:[UnnormalConnection3]DB100,W126");
            list.Add("S7:[UnnormalConnection3]DB100,DINT128");
            list.Add("S7:[UnnormalConnection3]DB100,DINT132");
            list.Add("S7:[UnnormalConnection3]DB100,DINT136");
            list.Add("S7:[UnnormalConnection3]DB100,DINT140");
            list.Add("S7:[UnnormalConnection3]DB100,DINT144");
            list.Add("S7:[UnnormalConnection3]DB100,DINT148");
            list.Add("S7:[UnnormalConnection3]DB100,DINT152");
            list.Add("S7:[UnnormalConnection3]DB100,DINT156");
            list.Add("S7:[UnnormalConnection3]DB100,W160");
            list.Add("S7:[UnnormalConnection3]DB100,DINT162");
            list.Add("S7:[UnnormalConnection3]DB100,DINT166");
            list.Add("S7:[UnnormalConnection3]DB100,DINT170");
            list.Add("S7:[UnnormalConnection3]DB100,DINT174");
            list.Add("S7:[UnnormalConnection3]DB100,DINT178");
            list.Add("S7:[UnnormalConnection3]DB100,DINT182");
            list.Add("S7:[UnnormalConnection3]DB100,DINT186");
            list.Add("S7:[UnnormalConnection3]DB100,DINT190");
            list.Add("S7:[UnnormalConnection3]DB100,DINT194");
            list.Add("S7:[UnnormalConnection3]DB100,W202");

            #endregion
                break;
                case "4":
                    #region  第四组PLC DB块
            list.Add("S7:[UnnormalConnection4]DB100,W24");
            list.Add("S7:[UnnormalConnection4]DB100,DINT26");
            list.Add("S7:[UnnormalConnection4]DB100,DINT30");
            list.Add("S7:[UnnormalConnection4]DB100,DINT34");
            list.Add("S7:[UnnormalConnection4]DB100,DINT38");
            list.Add("S7:[UnnormalConnection4]DB100,DINT42");
            list.Add("S7:[UnnormalConnection4]DB100,DINT46");
            list.Add("S7:[UnnormalConnection4]DB100,DINT50");
            list.Add("S7:[UnnormalConnection4]DB100,DINT54");
            list.Add("S7:[UnnormalConnection4]DB100,W58");
            list.Add("S7:[UnnormalConnection4]DB100,DINT60");
            list.Add("S7:[UnnormalConnection4]DB100,DINT64");
            list.Add("S7:[UnnormalConnection4]DB100,DINT68");
            list.Add("S7:[UnnormalConnection4]DB100,DINT72");
            list.Add("S7:[UnnormalConnection4]DB100,DINT76");
            list.Add("S7:[UnnormalConnection4]DB100,DINT80");
            list.Add("S7:[UnnormalConnection4]DB100,DINT84");
            list.Add("S7:[UnnormalConnection4]DB100,DINT88");
            list.Add("S7:[UnnormalConnection4]DB100,W92");
            list.Add("S7:[UnnormalConnection4]DB100,DINT94");
            list.Add("S7:[UnnormalConnection4]DB100,DINT98");
            list.Add("S7:[UnnormalConnection4]DB100,DINT102");
            list.Add("S7:[UnnormalConnection4]DB100,DINT106");
            list.Add("S7:[UnnormalConnection4]DB100,DINT110");
            list.Add("S7:[UnnormalConnection4]DB100,DINT114");
            list.Add("S7:[UnnormalConnection4]DB100,DINT118");
            list.Add("S7:[UnnormalConnection4]DB100,DINT122");
            list.Add("S7:[UnnormalConnection4]DB100,W126");
            list.Add("S7:[UnnormalConnection4]DB100,DINT128");
            list.Add("S7:[UnnormalConnection4]DB100,DINT132");
            list.Add("S7:[UnnormalConnection4]DB100,DINT136");
            list.Add("S7:[UnnormalConnection4]DB100,DINT140");
            list.Add("S7:[UnnormalConnection4]DB100,DINT144");
            list.Add("S7:[UnnormalConnection4]DB100,DINT148");
            list.Add("S7:[UnnormalConnection4]DB100,DINT152");
            list.Add("S7:[UnnormalConnection4]DB100,DINT156");
            list.Add("S7:[UnnormalConnection4]DB100,W160");
            list.Add("S7:[UnnormalConnection4]DB100,DINT162");
            list.Add("S7:[UnnormalConnection4]DB100,DINT166");
            list.Add("S7:[UnnormalConnection4]DB100,DINT170");
            list.Add("S7:[UnnormalConnection4]DB100,DINT174");
            list.Add("S7:[UnnormalConnection4]DB100,DINT178");
            list.Add("S7:[UnnormalConnection4]DB100,DINT182");
            list.Add("S7:[UnnormalConnection4]DB100,DINT186");
            list.Add("S7:[UnnormalConnection4]DB100,DINT190");
            list.Add("S7:[UnnormalConnection4]DB100,DINT194");
            list.Add("S7:[UnnormalConnection4]DB100,W202");

            #endregion
                break; 
            }
            return list;
        }
      
    }
}
