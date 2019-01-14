using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Model; 
 

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
               
                var synseq = (from item in ent.T_PRODUCE_SYNSEQ select item);//获取批次表
                var tasksynseq = (from item in ent.T_PRODUCE_TASK select item);//获取TASK表
                var date = tasksynseq.GroupBy(a => a.ORDERDATE).Select(a => new { orderdate = a.Key }).FirstOrDefault();//获取排程日期
                if (date != null)
                {
                    //获取 TASK表和批次表的差集 批次表中没有的批次取TASK表中的批次插入
                    var lastsynseq = tasksynseq.GroupBy(a => new { a.SYNSEQ, a.PACKAGEMACHINE }).Select(a => new { synseq = a.Key.SYNSEQ, packagemachine = a.Key.PACKAGEMACHINE }).ToList()
                        .Except( synseq.Where(a=> a.ORDERDATE == date.orderdate).GroupBy( a=> new {a.SYNSEQ,a.PACKAGENO}).Select(a=> new { synseq = a.Key.SYNSEQ , packagemachine = a.Key.PACKAGENO}).ToList());
                         
                    
                    
                    foreach (var item in lastsynseq)
                    {
                        T_PRODUCE_SYNSEQ T_synseq = new T_PRODUCE_SYNSEQ();
                        T_synseq.ID = GetSeq("select t_produce_pokeseq_pokeid.Nextval from dual");
                        T_synseq.SYNSEQ = item.synseq;
                        T_synseq.PMSTATE = pmState;
                        T_synseq.TBJSTATE = "0";
                        T_synseq.PACKAGENO = item.packagemachine;
                        T_synseq.QUANTITY = GetSeq("select sum(quantity) from  kesheng.v_produce_packageinfo where export = "+  item.packagemachine + " and  synseq  = "+item.synseq);
                        T_synseq.ORDERDATE = date.orderdate;
                        ent.AddToT_PRODUCE_SYNSEQ(T_synseq);
                    }
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
        /// 异形烟单独异型
        /// </summary>
        /// <returns></returns>
        public static bool InsertSynseqInfo_UnNormalAlone()
        {
            using (Entities ent = new Entities())
            {
                var synseq = (from item in ent.T_PRODUCE_SYNSEQ select item);//获取批次表
                var tasksynseq = (from item in ent.T_UN_TASK select item);//获取TASK表
                var date = tasksynseq.GroupBy(a => a.ORDERDATE).Select(a => new { orderdate = a.Key }).FirstOrDefault();//获取排程日期
                if (date != null)
                {
                    //获取 TASK表和批次表的差集 批次表中没有的批次取TASK表中的批次插入
                    var lastsynseq = tasksynseq.GroupBy(a => new { a.SYNSEQ, a.PACKAGEMACHINE }).Select(a => new { synseq = a.Key.SYNSEQ, packagemachine = a.Key.PACKAGEMACHINE }).ToList()
                        .Except(synseq.GroupBy(a => new { a.SYNSEQ, a.PACKAGENO }).Select(a => new { synseq = a.Key.SYNSEQ, packagemachine = a.Key.PACKAGENO }).ToList()); 
                    foreach (var item in lastsynseq)
                    {
                        T_PRODUCE_SYNSEQ T_synseq = new T_PRODUCE_SYNSEQ();
                        T_synseq.ID = GetSeq("select t_produce_pokeseq_pokeid.Nextval from dual");
                        T_synseq.SYNSEQ = item.synseq;
                        T_synseq.PMSTATE = "1";
                        T_synseq.TBJSTATE = "0";
                        T_synseq.PACKAGENO = item.packagemachine;
                        T_synseq.QUANTITY = GetSeq("select sum(quantity) from  kesheng.v_produce_packageinfo where export = " + item.packagemachine + " and  synseq  = " + item.synseq);
                        T_synseq.ORDERDATE = date.orderdate;
                        ent.AddToT_PRODUCE_SYNSEQ(T_synseq);
                    }
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

        public static void ScheduleVildata(List<int> ListSum)
        {
            using (Entities ent = new Entities())
            {
                var TaskSum = (from item in ent.T_PRODUCE_TASK select item).Sum(a => a.ORDERQUANTITY);
                var UnTaskSum = (from item in ent.T_UN_TASK select item).Sum(a => a.ORDERQUANTITY);
                var pokeSum = (from item in ent.T_PRODUCE_POKE select item).Sum(a => a.POKENUM); 
                var UnpokeSum = (from item in ent.T_UN_POKE select item).Sum(a => a.POKENUM); 
                var pokeSeqSum = (from item in ent.T_PRODUCE_POKESEQ select item).Sum(a => a.POKENUM);
            } 
        }
        /// <summary>
        /// 获取异形烟订单数量
        /// </summary>
        /// <param name="cutName">客户名称</param>
        /// <param name="ordernum">订单数量</param>
        /// <returns></returns>
        public static List<T_UN_TASK> GetUnnormalOrderInfo() 
        {
            using (Entities ent = new Entities())
            {
                 var synseq = (from item in ent.T_PRODUCE_SYNSEQ select item);//获取批次表
                var tasksynseq = (from item in ent.T_UN_TASK select item);//获取TASK表
                var date = tasksynseq.GroupBy(a => a.ORDERDATE).Select(a => new { orderdate = a.Key }).FirstOrDefault();//获取排程日期 
                if (date != null)
                {
                    //获取 TASK表和批次表的差集 批次表中没有的批次取TASK表中的批次插入
                    var lastsynseq = tasksynseq.GroupBy(a => a.SYNSEQ).Select(a => new { synseq = a.Key }).ToList().Except(synseq.Where(a => a.ORDERDATE == date.orderdate).GroupBy(a => a.SYNSEQ).Select(a => new { synseq = a.Key }).ToList()).FirstOrDefault();
                    if (lastsynseq != null)
                    {
                        var alredyorder = (from item in ent.T_UN_POKE
                                           group item by new { package = item.PACKAGEMACHINE, billcode = item.BILLCODE }
                                               into g
                                               select new { g.Key.billcode, g.Key.package }).ToList();//先给订单分组 group by 
                        var count = (from item in alredyorder
                                     group item by new { item.package, item.billcode, count = alredyorder.Count(a => a.billcode == item.billcode) }
                                         into c
                                         where c.Key.count > 1
                                         select new { c.Key.billcode }).Distinct().ToList();//取出一个订单在两个包装机的订单号 相当于 having    Count( distinct  packagemachine) > 1)


                        var order = (from item in ent.T_UN_TASK where item.SYNSEQ == lastsynseq.synseq select item).ToList();
                        foreach (var item in count)//移除已经拆单的订单号
                        {
                            order.RemoveAll(a => a.BILLCODE == item.billcode);
                        }
                        return order;
                    }
                    else
                        return new List<T_UN_TASK>();
                }
                else
                    return new List<T_UN_TASK>();
            }
        }
    }
}
