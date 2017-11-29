using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Business
{
    public class ProduceTaskService : BaseService
    {
        public static List<ProduceTask> GetItem()
        {
            using (Entities entity = new Entities())
            {
                var query = from item in entity.T_PRODUCE_TASK
                            group item by new { item.BATCHCODE,item.SYNSEQ } into lst
                            select new ProduceTask()
                            {
                                batchcode = lst.Key.BATCHCODE,
                                qty = lst.Sum(sum => sum.TASKQUANTITY).Value,
                                cuscount = lst.Count(),
                                synseq = lst.Key.SYNSEQ
                            };
                return query.ToList();
            }
        } 
    }

    public class ProduceTask
    {
        public string batchcode { get; set; }
        public decimal qty { get; set; }
        public int cuscount { get; set; }
        public decimal? synseq { get; set; }
    }
}
