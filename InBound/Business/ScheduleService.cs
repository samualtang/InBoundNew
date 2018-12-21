using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Business
{
    public class ScheduleService : BaseService
    {
        /// <summary>
        /// 异标合一
        /// </summary>
        /// <param name="pmState">状态</param>
        /// <returns></returns>
        public static bool InsertSynseqInfo(string pmState)
        {
            using (Entities ent = new Entities())
            {
                var synseq = (from item in ent.T_PRODUCE_SYNSEQ select item);
                var tasksynseq = (from item in ent.T_PRODUCE_TASK select item);
              
                //获取 TASK表和批次表的补集
                var lastsynseq = tasksynseq.GroupBy(a => a.SYNSEQ).Select(a => new { synseq = a.Key }).ToList().Except(synseq.GroupBy(a=> a.SYNSEQ).Select(a=> new {synseq = a.Key}).ToList()).ToList();
                foreach (var item in lastsynseq)
                {
                    T_PRODUCE_SYNSEQ T_synseq = new T_PRODUCE_SYNSEQ();
                    T_synseq.ID = GetSeq("select t_produce_pokeseq_pokeid.Nextval from dual");
                    T_synseq.SYNSEQ = item.synseq;
                    T_synseq.PMSTATE = pmState;
                    T_synseq.TBJSTATE = "0";
                    T_synseq.QUANTITY = tasksynseq.Where(a => a.SYNSEQ == item.synseq).Sum(a => a.ORDERQUANTITY);
                    T_synseq.ORDERDATE = tasksynseq.Where(a => a.SYNSEQ == item.synseq).Select(a => new { orderdata = a.ORDERDATE }).FirstOrDefault().orderdata;
                    ent.AddToT_PRODUCE_SYNSEQ(T_synseq);
                }
                if (ent.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                } 
            }
        }

        /// <summary>
        /// 单独常规
        /// </summary>
        /// <returns></returns>
        public static bool InsertSynseqInfo_NormalAlone( )
        {
            using (Entities ent = new Entities())
            {
                var synseq = (from item in ent.T_PRODUCE_SYNSEQ select item);
                var tasksynseq = (from item in ent.T_PRODUCE_TASK select item);

                var lastsynseq = tasksynseq.GroupBy(a => a.SYNSEQ).Select(a => new { synseq = a.Key }).ToList().Except(synseq.GroupBy(a => a.SYNSEQ).Select(a => new { synseq = a.Key }).ToList()).ToList();
                foreach (var item in lastsynseq)
                {
                    T_PRODUCE_SYNSEQ T_synseq = new T_PRODUCE_SYNSEQ();
                    T_synseq.ID = GetSeq("select t_produce_pokeseq_pokeid.Nextval from dual");
                    T_synseq.SYNSEQ = item.synseq;
                    T_synseq.PMSTATE = "1";
                    T_synseq.TBJSTATE = "0";
                    T_synseq.QUANTITY = tasksynseq.Where(a => a.SYNSEQ == item.synseq).Sum(a => a.ORDERQUANTITY);
                    T_synseq.ORDERDATE = tasksynseq.Where(a => a.SYNSEQ == item.synseq).Select(a => new { orderdata = a.ORDERDATE }).FirstOrDefault().orderdata;
                    ent.AddToT_PRODUCE_SYNSEQ(T_synseq);
                }
                if (ent.SaveChanges() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 单独异型
        /// </summary>
        /// <returns></returns>
        public static bool InsertSynseqInfo_UnNormalAlone()
        {
            using (Entities ent = new Entities())
            {
                var synseq = (from item in ent.T_PRODUCE_SYNSEQ select item);
                var tasksynseq = (from item in ent.T_UN_TASK select item);

                var lastsynseq = tasksynseq.GroupBy(a => a.SYNSEQ).Select(a => new { synseq = a.Key }).ToList().Except(synseq.GroupBy(a => a.SYNSEQ).Select(a => new { synseq = a.Key }).ToList()).ToList();
                foreach (var item in lastsynseq)
                {
                    T_PRODUCE_SYNSEQ T_synseq = new T_PRODUCE_SYNSEQ();
                    T_synseq.ID = GetSeq("select t_produce_pokeseq_pokeid.Nextval from dual");
                    T_synseq.SYNSEQ = item.synseq;
                    T_synseq.PMSTATE = "1";
                    T_synseq.TBJSTATE = "0";
                    T_synseq.QUANTITY = tasksynseq.Where(a => a.SYNSEQ == item.synseq).Sum(a => a.ORDERQUANTITY);
                    T_synseq.ORDERDATE = tasksynseq.Where(a => a.SYNSEQ == item.synseq).Select(a => new { orderdata = a.ORDERDATE }).FirstOrDefault().orderdata;
                    ent.AddToT_PRODUCE_SYNSEQ(T_synseq);
                }
                if (ent.SaveChanges() > 0)
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
