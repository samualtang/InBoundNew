using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Model;
using System.Configuration;

namespace InBound.Business
{
   public class HunHeService
    {
       public  List<HUNHEVIEW> GetTroughCigarette(decimal seq,decimal finishno,int qty)
       {
           
           using (Entities entity = new Entities())
           {
               try
               {
                 
                   //List<HUNHEVIEW> list = null;
                   var query = (from item in entity.T_UN_POKE
                                join item2 in entity.T_PRODUCE_SORTTROUGH
                                    on item.TROUGHNUM equals item2.TROUGHNUM
                                where item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 40 && item.TASKNUM > finishno  
                                    && item.MACHINESEQ == seq
                                orderby item.SORTNUM, item.SECSORTNUM, item2.SEQ, item2.MACHINESEQ, item2.TROUGHNUM
                                select new HUNHEVIEW() { CIGARETTECODE = item.CIGARETTECODE, CIGARETTENAME = item2.CIGARETTENAME, QUANTITY = item.POKENUM }).Take(qty).ToList();

                   //var query = (from item in entity.T_PRODUCE_SORTTROUGH
                   //             where item.TROUGHTYPE == 20 && item.CIGARETTECODE != null && item.GROUPNO == seq
                   //             orderby item.TROUGHNUM
                   //             select new HUNHEVIEW() { CIGARETTECODE = item.CIGARETTECODE, CIGARETTENAME = item.CIGARETTENAME, TROUGHNUM = item.TROUGHNUM }).ToList();


                   return query;
               }
               catch (Exception e )
               {

                   throw e ;
               }
           }


       }
    }
}
