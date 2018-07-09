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
                   var query = (from item in entity.T_UN_POKE
                                join item2 in entity.T_PRODUCE_SORTTROUGH
                                    on item.TROUGHNUM equals item2.TROUGHNUM
                                where item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 40 && item.POKEID > 48//finishno  
                                    && item2.MACHINESEQ == seq
                                orderby   item.SORTNUM, item.POKEID, item2.SEQ, item2.MACHINESEQ, item2.TROUGHNUM
                                select new HUNHEVIEW() { POKEID=item.POKEID,CIGARETTECODE = item.CIGARETTECODE, CIGARETTENAME = item2.CIGARETTENAME
                                    ,MACHINESEQ=item.MACHINESEQ, QUANTITY = item.POKENUM }).Take(qty).ToList();

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

       public List<HUNHENOWVIEW> GetALLCigarette(decimal seq )
       { 
           using (Entities entity = new Entities())
           { 
               try
               { 
                   var query = (from item in entity.T_UN_POKE
                                join item2 in entity.T_PRODUCE_SORTTROUGH
                                on item.TROUGHNUM equals item2.TROUGHNUM
                                join item3 in entity.T_UN_TASK on item.TASKNUM equals item3.TASKNUM
                                where  item2.CIGARETTETYPE == 40 && item.MACHINESEQ == seq
                                orderby item.SORTNUM, item.POKEID
                                select new HUNHENOWVIEW() { tasknum = item.TASKNUM, sortnum = item.SORTNUM, customername = item3.CUSTOMERNAME, regioncode = item3.REGIONCODE, TROUGHNUM = item.TROUGHNUM, CIGARETTENAME = item2.CIGARETTENAME, pokenum = item.POKENUM, status = item.STATUS, pokeid = item.POKEID }).ToList();
                   return query; 
               }
               catch (Exception e)
               {
                   throw e;
               }
           } 
       }




      
       /// <summary>
       /// 置数据库为已放烟
       /// </summary>
       /// <param name="pokelist">pokeid的集合</param>
       /// <returns>是否成功</returns>
       public static bool PullTag(List<string> pokelist,decimal machineseq)
       { 
           using (Entities entity = new Entities())
           {
               try
               {
                   //先取第一个pokeid 读取它前面的是否放烟
                  decimal firstpokeid = Convert.ToDecimal(pokelist.First());
                  var qu = entity.T_UN_POKE_HUNHE.Where(x => x.POKEID < firstpokeid && x.PULLSTATUS == 0 && x.MACHINESEQ == machineseq).Count();
                  if (qu>0)
                  {
                      return false;
                  }
                   foreach (var item in pokelist)
	                { 
                       decimal pokeid = Convert.ToDecimal(item);
                       var query = entity.T_UN_POKE_HUNHE.Where(x => x.POKEID == pokeid).FirstOrDefault();
                       query.PULLSTATUS = 1;
                       entity.SaveChanges();
	                }
                   return true; 
               }
               catch (Exception e)
               {
                   return false;
                  // throw e;
               }
           } 
       }
       /// <summary>
       /// 置数据库为未放烟
       /// </summary>
       /// <param name="pokelist">pokeid的集合</param>
       /// <returns></returns>
       public static bool CancelTag(List<string> pokelist, decimal machineseq)
       {
           using (Entities entity = new Entities())
           {
               try
               {
                   //先取最后一个pokeid 读取它后面的是否放烟
                   decimal firstpokeid = Convert.ToDecimal(pokelist.Last());
                   var qu = entity.T_UN_POKE_HUNHE.Where(x => x.POKEID > firstpokeid && x.PULLSTATUS == 1 && x.MACHINESEQ == machineseq).Count();
                   if (qu > 0)
                   {
                       return false;
                   }
                   foreach (var item in pokelist)
                   {
                       decimal pokeid = Convert.ToDecimal(item);
                       var query = entity.T_UN_POKE_HUNHE.Where(x => x.POKEID == pokeid).FirstOrDefault();
                       query.PULLSTATUS = 0;
                       entity.SaveChanges();
                   }
                   return true;
               }
               catch (Exception e)
               {
                   return false;
               }
           }
       }


       public static bool GetTag(string[] str)
       {
           using (Entities entity = new Entities())
           {
               try
               {
                   int tagnum = 0;
                   foreach (var item in str)
                   {
                       decimal pokeid = Convert.ToDecimal(item);
                       var query = entity.T_UN_POKE_HUNHE.Where(x => x.POKEID == pokeid && x.PULLSTATUS==1).Count();
                       if (query==1)
                       {  
                            tagnum++;
                       } 
                   }
                   if (tagnum==str.Count())
                   {
                       return true;
                   }
                   else
                   {
                       return false;
                   }
               }
               catch (Exception e)
               {
                   return false;
               }
           }
       }


        
    }
}
