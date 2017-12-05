using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public static List<T_PRODUCE_POKE> FetchProducePokeList(decimal groupno)
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
                var query = from poke in entity.T_PRODUCE_POKE
                            //join task in entity.T_PRODUCE_TASK on poke.TASKNUM equals task.TASKNUM
                            //join sortgh in entity.T_PRODUCE_SORTTROUGH on poke.MACHINESEQ equals sortgh.MACHINESEQ
                            where
                                //sortgh.TROUGHTYPE == 10 && sortgh.CIGARETTETYPE == 20 && sortgh.STATE == "10" && 
                            poke.GROUPNO == groupno
                            //&& task.SYNSEQ == 1 
                            select poke;
                return query.ToList();
            }
        }
    }
}
