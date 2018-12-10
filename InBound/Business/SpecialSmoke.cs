using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Model;

namespace InBound.Business
{
    public static class SpecialSmoke
    {
        /// <summary>
        /// 获取当前异型烟分拣线
        /// </summary>
        /// <returns>分拣线集合</returns>
        public static decimal[] getalllinenum()
        { 
            Entities et=new Entities();
            var list =et.T_PRODUCE_SORTTROUGH.Where(x=>x.CIGARETTETYPE==30 && x.TROUGHTYPE==10 &&x.SEQ == 2).GroupBy(x=>x.GROUPNO).Select(x=>x.Key).ToList();
            decimal[] items=new decimal[list.Count];
            for (int i = 0; i < list.Count; i++)
			{
			 items[i]=Convert.ToDecimal(list[i]);
			}
            return items;
        }

        /// <summary>
        /// 获取异型烟线的通道设置
        /// </summary>
        /// <param name="linenum">异型烟分拣线</param>
        /// <returns>异型烟通道、物理通道号、品牌的集合</returns>
        public static List<HUNHETROUGH2> getalltrough(decimal linenum)
        {
            Entities et = new Entities();
            var list = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 30 && x.TROUGHTYPE == 10 && x.GROUPNO == linenum).OrderBy(x => x.MACHINESEQ).Select(x => new InBound.Model.HUNHETROUGH2 { machineseq = x.MACHINESEQ ?? 0, cigarettename = x.CIGARETTENAME, troughnum = x.TROUGHNUM, cigarettecode = x.CIGARETTECODE, status = x.STATE }).ToList();
            return list;
        }

        /// <summary>
        /// 获取异型烟线混合道的通道设置
        /// </summary>
        /// <param name="linenum">异型烟分拣线</param>
        /// <returns>异型烟混合道、物理通道号、品牌的集合</returns>
        public static List<HUNHETROUGH2> spcialgetalltrough(decimal linenum)
        {
            Entities et = new Entities();
            var list = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 40 && x.TROUGHTYPE == 10 && x.GROUPNO == linenum && x.STATE =="10").OrderBy(x => x.TROUGHNUM).Select(x => new InBound.Model.HUNHETROUGH2 { machineseq = x.MACHINESEQ ?? 0, cigarettename = x.CIGARETTENAME, troughnum = x.TROUGHNUM, cigarettecode = x.CIGARETTECODE , status=x.STATE}).ToList();
            return list;
        }

        /// <summary>
        /// 互换烟仓
        /// </summary>
        /// <param name="obj1">烟仓1</param>
        /// <param name="obj2">烟仓2</param>
        /// <returns>互换结果</returns>
        public static bool HuHuanYc(HUNHETROUGH2 obj1,HUNHETROUGH2 obj2)
        {
            Entities et = new Entities();
            var list = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 30 && x.TROUGHTYPE == 10 && x.GROUPNO == 1 ).Select(x=>x).ToList();
            foreach (var item in list)
            {
                if(item.TROUGHNUM == obj2.troughnum)
                {
                    item.CIGARETTECODE = obj1.cigarettecode;
                    item.CIGARETTENAME = obj1.cigarettename;
                }
                if (item.TROUGHNUM == obj1.troughnum)
                {
                    item.CIGARETTECODE = obj2.cigarettecode;
                    item.CIGARETTENAME = obj2.cigarettename;
                }
            }
            if (et.SaveChanges()> 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <returns></returns>
        public static string StatusChange(HUNHETROUGH2 obj)
        {
            Entities et = new Entities();
            var list = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 30 && x.TROUGHTYPE == 10 && x.GROUPNO == 1 ).Select(x => x).ToList();
            foreach (var item in list)
            {
                if (item.TROUGHNUM == obj.troughnum && item.STATE != obj.status)
                {
                    item.STATE = obj.status; 
                } 
            } 
            if (et.SaveChanges()>0)
            {
                return "修改成功！";
            }
            else
            {
                return "修改失败！";
            }
        }


        /// <summary>
        /// 烟仓与混合道互换品牌
        /// </summary>
        /// <param name="obj1">混合道</param>
        /// <param name="obj2">烟仓</param>
        /// <returns>互换结果</returns>
        public static bool HuHuanHunHeDao(HUNHETROUGH2 hunhe, HUNHETROUGH2 yc)
        {
            Entities et = new Entities();
            var list = et.T_PRODUCE_SORTTROUGH.Where(x => ( x.CIGARETTETYPE == 30 || x.CIGARETTETYPE == 40 ) && x.TROUGHTYPE == 10 && x.GROUPNO == 1).Select(x => x).ToList();
            foreach (var item in list)
            {
                if (item.TROUGHNUM == hunhe.troughnum)
                {
                    item.CIGARETTECODE = yc.cigarettecode;
                    item.CIGARETTENAME = yc.cigarettename;
                }
                if (item.TROUGHNUM == yc.troughnum)
                {
                    item.CIGARETTECODE = hunhe.cigarettecode;
                    item.CIGARETTENAME = hunhe.cigarettename;
                }
            }
            if (et.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 获取异型烟线混合道的查询品牌
        /// </summary>
        /// <param name="cigarettename">异型烟名称</param>
        /// <returns>异型烟混合道、物理通道号、品牌的集合</returns>
        public static List<HUNHETROUGH2> getspeacildata(string cigarettename)
        {
            Entities et = new Entities();
            var list = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 40 && x.TROUGHTYPE == 10 && x.GROUPNO == 1 && x.STATE == "10" &&
                x.CIGARETTENAME.Contains(cigarettename)).OrderBy(x => x.TROUGHNUM).Select(x => new InBound.Model.HUNHETROUGH2 
                { machineseq = x.MACHINESEQ ?? 0, cigarettename = x.CIGARETTENAME, troughnum = x.TROUGHNUM, cigarettecode = x.CIGARETTECODE, status = x.STATE }).ToList();
            return list;
        }
    }
}
