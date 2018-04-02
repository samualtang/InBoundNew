using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Model;

namespace InBound.Business
{
   public class HunHeService
    {
       public  List<HUNHEVIEW> GetTroughCigarette(decimal seq,int qty)
       {
           
           using (Entities entity = new Entities())
           {
               //List<HUNHEVIEW> list = null;
               var query = (from item in entity.T_UN_POKE
                            join item2 in entity.T_PRODUCE_SORTTROUGH
                                on item.TROUGHNUM equals item2.TROUGHNUM
                            where item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 30 && item.STATUS==15
                                && item.MACHINESEQ == seq orderby item.SORTNUM
                            select new HUNHEVIEW() { CIGARETTECODE = item2.CIGARETTECODE, CIGARETTENAME = item2.CIGARETTENAME, QUANTITY = item.POKENUM }).Take(qty).ToList();
               return query;
           }


       }
    }
}
