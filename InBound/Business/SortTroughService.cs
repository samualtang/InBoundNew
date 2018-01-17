using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Business
{
   public class SortTroughService
    {
       public static List<T_PRODUCE_SORTTROUGH> GetTrough(int troughtype, int cigarettetype)
       {
           using (Entities dataentity = new Entities())
           {
               var query = from item in dataentity.T_PRODUCE_SORTTROUGH where item.TROUGHTYPE == troughtype && item.CIGARETTETYPE == cigarettetype && item.STATE == "10" select item;
               return query.ToList();
           }
       }

       public static List<T_PRODUCE_SORTTROUGH> GetTroughNotINCigaretteType(int troughtype, int cigarettetype)
       {
           using (Entities dataentity = new Entities())
           {
               var query = from item in dataentity.T_PRODUCE_SORTTROUGH where item.TROUGHTYPE == troughtype && item.CIGARETTETYPE != cigarettetype && item.STATE == "10" select item;
               return query.ToList();
           }
       }
       public static T_PRODUCE_SORTTROUGH GetFJTroughInfo(int troughtype,string troughnum, int cigarettetype)
       {
           using (Entities dataentity = new Entities())
           {
               var query = from item in dataentity.T_PRODUCE_SORTTROUGH where item.TROUGHTYPE == troughtype && item.TROUGHNUM==troughnum && item.CIGARETTETYPE == cigarettetype && item.STATE == "10" select item;
               return query.FirstOrDefault();
           }
       }

       public static void updateTroughClearUp(decimal type, decimal cigarettetype, String troughnum)
       {
           using (Entities dataentity = new Entities())
           {
               var query = (from item in dataentity.T_PRODUCE_SORTTROUGH where item.TROUGHTYPE == type && item.CIGARETTETYPE == cigarettetype && item.STATE == "10"  && item.TROUGHNUM==troughnum select item).FirstOrDefault();
               if (query != null)
               {
                   query.CLEARUP = 0;
                   dataentity.SaveChanges();
               }
           }
       }
       public static List<T_PRODUCE_SORTTROUGH> GetTrough(int troughtype, int cigarettetype, int begin, int end)
       {
           using (Entities dataentity = new Entities())
           {
               var query = from item in dataentity.T_PRODUCE_SORTTROUGH where item.TROUGHTYPE == troughtype && item.CIGARETTETYPE == cigarettetype && item.STATE == "10" && item.MACHINESEQ>=begin && item.MACHINESEQ<=end select item;
               return query.ToList();
           }
       }
    }
}
