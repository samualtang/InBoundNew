using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;
namespace InBound.Business
{
    public class PreSortInfoService : BaseService
    {
        public static decimal GetSeq()
        {
            using (Entities context = new Entities())
            {
                return context.ExecuteStoreQuery<decimal>("select SEQ_PRESORTINOID.Nextval from dual").First();
            }

        }
      public static void Add(decimal sortNum, decimal groupNo)
      {
          using (Entities dataentity = new Entities())
          {
              var query = (from item in dataentity.T_PRODUCE_POKE
                           where item.GROUPNO == groupNo && item.SORTNUM == sortNum
                           select item).ToList();
              if (query != null && query.Count > 0)
              {
                  T_PRODUCE_PRESORTINFO info = new T_PRODUCE_PRESORTINFO();
                  info.ID = GetSeq();
                  info.SORTNUM = sortNum;
                  info.BILLCODE = query[0].BILLCODE;
                  info.POKENUM = query.Sum(x => x.POKENUM);
                  info.FINISHTIME = DateTime.Now;
                  info.GROUPNO = groupNo;
                  info.CTYPE = 1;
                  dataentity.T_PRODUCE_PRESORTINFO.AddObject(info);
                  dataentity.SaveChanges();
              }
          }
      }
    }
}
