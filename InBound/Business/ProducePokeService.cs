using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Model;

namespace InBound.Business
{
    public class ProducePokeService : BaseService
    {
        public static List<T_PRODUCE_POKE> GetGroupNo()
        {

            List<T_PRODUCE_POKE> list = new List<T_PRODUCE_POKE>();
            using (Entities entity = new Entities())
            {
                var query =
                from item in entity.T_PRODUCE_POKE
                group item by new { item.GROUPNO } into lst
                select new { GROUPNO = lst.Key.GROUPNO };

                foreach (var item in query)
                {
                    list.Add(new T_PRODUCE_POKE
                    {
                        GROUPNO = item.GROUPNO,
                    });
                }
            }
            return list;
        }
        public static decimal GetTroughUnFinished(string throughno)
        {
            decimal total = 0;
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_PRODUCE_POKE
                            where item.TROUGHNUM == throughno
                                && item.SORTSTATE != 20
                                group item by new {item.TROUGHNUM} into g

                             select new TaskInfo(){ FinishQTY = g.Sum(t => t.POKENUM??0) }).FirstOrDefault();
                                if (query != null)
                               {
                                   total = query.FinishQTY;
                               }

                               return total;
            }
 
        }
        public static decimal GetTroughFirstUnFinished(string throughno)
        {
            decimal total = 0;
            using (Entities entity = new Entities())
            {

               T_PRODUCE_SORTTROUGH info= SortTroughService.GetFJTroughInfo(10, throughno, 20);
                var query = (from item in entity.T_PRODUCE_POKE
                            // where item.TROUGHNUM == throughno
                                 where item.SORTSTATE != 20  && item.GROUPNO==info.GROUPNO orderby item.SORTNUM
                              select item ).FirstOrDefault();
                if (query != null)
                {
                    var query2 = (from item in entity.T_PRODUCE_POKE where item.SORTNUM == query.SORTNUM && item.TROUGHNUM == throughno select item).FirstOrDefault();
                    if (query2 != null)
                    {
                        total = query2.POKENUM ?? 0;
                    }
                }

                return total;
            }

        }
        public static List<T_PRODUCE_POKE> GetGroupNoByRegionCode(string regionCode)
        {
            List<T_PRODUCE_POKE> list = new List<T_PRODUCE_POKE>();
            using (Entities entity = new Entities())
            {
                var query =
                from item in entity.T_PRODUCE_POKE
                join task in entity.T_PRODUCE_TASK on item.TASKNUM equals task.TASKNUM
                where task.REGIONCODE == regionCode
                group item by new { item.GROUPNO } into lst
                select new { GROUPNO = lst.Key.GROUPNO };

                foreach (var item in query)
                {
                    list.Add(new T_PRODUCE_POKE
                    {
                        GROUPNO = item.GROUPNO,
                    });
                }
            }
            return list;
        }

        public static List<OrderGroupDetail> FetchProducePokeList(decimal groupno)
        {
            /**
           SELECT a.tasknum,a.customercode,a.customername,b.cigarettecode,b.cigarettename,c.pokenum,a.regioncode,to_char(a.orderdate,'yyyy-mm-dd') AS enterdate 
                         FROM t_produce_task a,t_produce_sorttrough b,t_produce_poke c 
                         WHERE a.tasknum=c.tasknum  
             * and b.machineseq=c.machineseq 
             * and b.troughtype=10 and b.cigarettetype=20 and b.state='10' 
                         and a.synseq='1' and c.groupno=1 ORDER BY c.sortnum;
             * 
             */
            using (Entities entity = new Entities())
            {
                var query = (from poke in entity.T_PRODUCE_POKE
                            join task in entity.T_PRODUCE_TASK on poke.TASKNUM equals task.TASKNUM
                            join sortgh in entity.T_PRODUCE_SORTTROUGH on poke.TROUGHNUM equals sortgh.TROUGHNUM
                            where
                                sortgh.TROUGHTYPE == 10 && sortgh.CIGARETTETYPE == 20 && sortgh.STATE == "10" && 
                            poke.GROUPNO == groupno
                            //&& task.SYNSEQ == 1 
                            select new OrderGroupDetail() {  CigaretteCode=sortgh.CIGARETTECODE, CigaretteName=sortgh.CIGARETTENAME,
                             CustomerCode=task.COMPANYCODE, CustomerName=task.CUSTOMERNAME, OrderDate=task.ORDERDATE, PokeNum=poke.POKENUM, RegionCode=task.REGIONCODE, RegionName=task.REGIONDESC,
                            TaskNum=task.SORTNUM});
                return query.ToList();
            }
        }

        public static void FetchTaskByTroughNo(string troughNo, string standbyNo)
        {
            using (Entities entity = new Entities())
            {

                var query = (from poke in entity.T_PRODUCE_POKE
                             where poke.TROUGHNUM == troughNo
                             select poke).ToList();
                //更换通道编码
                query.ForEach(f =>
                {
                    f.TROUGHNUM = standbyNo;
                    //f.MACHINESTATE = 20;
                });

                //获取最近完成的该通道任务
                var task = query.Where(w => w.SORTSTATE == 20 && w.TROUGHNUM==troughNo).OrderByDescending(w=>w.SORTNUM).FirstOrDefault();
                var query1 = (from poke in entity.T_PRODUCE_POKE
                             where poke.TROUGHNUM == troughNo && poke.SORTNUM<=task.SORTNUM
                             select poke).ToList();
    
                query1.ForEach(f =>
                {
                   // f.TROUGHNUM = standbyNo;
                    f.MACHINESTATE = 20;//更新预分拣已完成的订单对应机械手的状态为已完成
                });
                if (task != null)
                {
                 var queryList=(from item in entity.T_PRODUCE_POKE where item.UNIONTASKNUM==task.UNIONTASKNUM select item).ToList();

                    if(queryList!=null)
                    {
                        var unfinished = queryList.Where(w => w.SORTSTATE != 20).Sum(w=>w.POKENUM);
                        if (unfinished != null && unfinished != 0)
                        {
                            decimal place = 1;
                         foreach(var item in queryList)
                         {
                             if (item.SORTSTATE != 20)
                             {
                                 item.POKEPLACE = place;
                                 place += (item.POKENUM??0);
                             }
                             item.MERAGENUM = unfinished;
                         }
                        }
                    }
                }
                //foreach (var item in allTask)
                //{
                //    var taskquy = 0M;
                //    var CompletNot = query.Where(w => w.UNIONTASKNUM == item && w.MACHINESTATE != 20).ToList();
                //    taskquy = CompletNot.Sum(s => s.POKENUM.Value);
                //    decimal nextPlace = 0;
                //    // decimal nextLocal = 0;//下一个位置=前位置+当前数量
                //    decimal lastPlace = 0;
                //    decimal lastSortnum = 0;
                //    CompletNot.ForEach(f =>
                //    {
                //        f.MERAGENUM = taskquy;
                //        f.MACHINESTATE = 20;
                //        f.POKEPLACE = nextPlace == 0 ? 1 : lastSortnum + lastPlace;
                //        lastPlace = f.POKEPLACE.Value;
                //        lastSortnum = f.POKENUM.Value;
                //        nextPlace = f.POKEPLACE.Value;
                //    });
                //    query.Where(w => w.UNIONTASKNUM == item).OrderBy(o => o.SORTNUM).ToList().ForEach(f =>
                //    {
                //        f.MERAGENUM = taskquy;
                //        f.MACHINESTATE = 10;
                //    });

                //}
                entity.SaveChanges();
            }
        }
    }
}
