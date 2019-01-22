using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Model;

namespace InBound.Business
{
    public static class SixSchedule
    {

        /// <summary>
        /// 置六三六订单为排程状态
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool sixorderschedule(string[] str)
        {
            using (Entities et= new Entities())
            { 
                decimal seq = Convert.ToDecimal(et.T_UN_DIY_TASK.Max(x => x.SYNSEQ)) + 1;
                foreach (var item in str)
                {
                    //修改排程状态和批次号
                    var etstr = et.T_UN_DIY_TASK.Where(x => x.REGIONCODE == item).ToList();
                    foreach (var it in etstr)
                    {
                        it.STATE = "15";
                        it.SYNSEQ = seq;
                        var etstate = et.T_UN_DIY_DETAIL.Where(t => t.SORTNUM == it.SORTNUM).ToList();
                        foreach (var its in etstate)
                        {
                            its.STATE = 2;
                        }
                    }
                    


                } 
                if ( et.SaveChanges() > 0 )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } 
        }



    }
}
