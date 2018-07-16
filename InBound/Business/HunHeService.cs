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
       /// <summary>
       /// 读取混合道数据
       /// </summary>
       /// <param name="seq">通道号</param>
       /// <param name="finishno">当前完成的pokeid</param>
       /// <param name="qty">前*行记录</param>
        /// <param name="tag">false 为已放烟未出烟，(true为点击版本使用的)</param>
       /// <returns>通道烟数据</returns>
       public  List<HUNHEVIEW> GetTroughCigarette(decimal seq,decimal finishno,int qty,bool tag =false)
       { 
           using (Entities entity = new Entities())
           {
               try
               { 
                   if (tag)
                   {
                       var query = (from item in entity.T_UN_POKE
                                join item2 in entity.T_PRODUCE_SORTTROUGH
                                    on item.TROUGHNUM equals item2.TROUGHNUM
                                    join item3 in entity.T_UN_POKE_HUNHE on  item.POKEID equals item3.POKEID
                                where item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 40 && item.POKEID > finishno//finishno  
                                    && item2.MACHINESEQ == seq && item3.PULLSTATUS==1
                                orderby   item.SORTNUM, item.POKEID, item2.SEQ, item2.MACHINESEQ, item2.TROUGHNUM
                                select new HUNHEVIEW() { POKEID=item.POKEID,CIGARETTECODE = item.CIGARETTECODE, CIGARETTENAME = item2.CIGARETTENAME
                                    ,MACHINESEQ=item.MACHINESEQ, QUANTITY = item.POKENUM }).Take(qty).ToList();
                       return query;
                   }
                   else
                   {
                        var query = (from item in entity.T_UN_POKE
                                join item2 in entity.T_PRODUCE_SORTTROUGH
                                    on item.TROUGHNUM equals item2.TROUGHNUM
                                    join item3 in entity.T_UN_POKE_HUNHE on  item.POKEID equals item3.POKEID
                                where item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 40 && item.POKEID > finishno//finishno  
                                orderby   item.SORTNUM, item.POKEID, item2.SEQ, item2.MACHINESEQ, item2.TROUGHNUM
                                select new HUNHEVIEW() { POKEID=item.POKEID,CIGARETTECODE = item.CIGARETTECODE, CIGARETTENAME = item2.CIGARETTENAME
                                    ,MACHINESEQ=item.MACHINESEQ, QUANTITY = item.POKENUM }).Take(qty).ToList();
                        return query;
                   } 
               }
               catch (Exception e )
               { 
                   throw e ;
               }
           }
       }

       /// <summary>
       /// 获取所有信息
       /// </summary>
       /// <param name="seq">通道号</param>
       /// <returns></returns>
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
                                select new HUNHENOWVIEW() { tasknum = item.TASKNUM, sortnum = item.SORTNUM, customername = item3.CUSTOMERNAME, regioncode = item3.REGIONCODE, TROUGHNUM = item.MACHINESEQ, CIGARETTENAME = item2.CIGARETTENAME, pokenum = item.POKENUM, status = item.STATUS, pokeid = item.POKEID }).ToList();
                   return query; 
               }
               catch (Exception e)
               {
                   throw e;
               }
           } 
       }
      
       /// <summary>
       /// 置数据库为已放烟（当前接连的集合）
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
       /// 置数据库为已放烟(单条)
       /// </summary>
       /// <param name="pokelist">pokeid的集合</param>
       /// <returns>是否成功</returns>
       public static bool PullTag(decimal pokeid, decimal machineseq)
       {
           using (Entities entity = new Entities())
           {
               try
               { 
                       var query = entity.T_UN_POKE_HUNHE.Where(x => x.POKEID == pokeid).FirstOrDefault();
                       query.PULLSTATUS = 1;
                       entity.SaveChanges(); 
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

       /// <summary>
       /// 放烟标志位
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns> 
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
       /// <summary>
       /// 读取混合道的待放烟品脾
       /// </summary>
       /// <param name="seq"></param>
       /// <returns></returns>
       public List<HUNHEVIEW> GetUnPullCigarette(decimal seq)
       {
           using (Entities entity = new Entities())
           {
               try
               {
                   var query = (from item in entity.T_UN_POKE
                                join item2 in entity.T_PRODUCE_SORTTROUGH
                                on item.TROUGHNUM equals item2.TROUGHNUM
                                join item3 in entity.T_UN_TASK on item.TASKNUM equals item3.TASKNUM
                                join item4 in entity.T_UN_POKE_HUNHE on item.POKEID equals item4.POKEID
                                where item2.CIGARETTETYPE == 40 && item.MACHINESEQ == seq
                                && item4.PULLSTATUS == 0
                                orderby item.SORTNUM, item.POKEID
                                select new HUNHEVIEW() { POKEID = item.POKEID, CIGARETTECODE = item.CIGARETTECODE, CIGARETTENAME = item2.CIGARETTENAME, MACHINESEQ = item2.MACHINESEQ, QUANTITY = item.POKENUM }).ToList();
                   return query;
               }
               catch (Exception e)
               {
                   throw e;
               }
           }
       }
       /// <summary>
       /// 获取将要放下的烟信息
       /// </summary>
       /// <param name="pokeid"></param>
       /// <returns></returns>
       public static HUNHEVIEW GetNextCigarette(decimal pokeid)
       {
           using (Entities entity = new Entities())
           {
               try
               {
                   var query = (from item in entity.T_UN_POKE
                                join item2 in entity.T_PRODUCE_SORTTROUGH
                                on item.TROUGHNUM equals item2.TROUGHNUM
                                join item3 in entity.T_UN_TASK on item.TASKNUM equals item3.TASKNUM
                                join item4 in entity.T_UN_POKE_HUNHE on item.POKEID equals item4.POKEID
                                where item2.CIGARETTETYPE == 40 && item.POKEID == pokeid
                                && item4.PULLSTATUS == 0
                                orderby item.SORTNUM, item.POKEID
                                select new HUNHEVIEW() { POKEID = item.POKEID, CIGARETTECODE = item.CIGARETTECODE, CIGARETTENAME = item2.CIGARETTENAME, MACHINESEQ = item2.MACHINESEQ, QUANTITY = item.POKENUM }).FirstOrDefault();
                   return query;
               }
               catch (Exception e)
               {
                   throw e;
               }
           }
       }
       /// <summary>
       /// 获取已放下的烟信息
       /// </summary>
       /// <param name="pokeid"></param>
       /// <returns></returns>
       public static HUNHEVIEW GetHaveCigarette(decimal pokeid)
       {
           using (Entities entity = new Entities())
           {
               try
               {
                   var query = (from item in entity.T_UN_POKE
                                join item2 in entity.T_PRODUCE_SORTTROUGH
                                on item.TROUGHNUM equals item2.TROUGHNUM
                                join item3 in entity.T_UN_TASK on item.TASKNUM equals item3.TASKNUM
                                join item4 in entity.T_UN_POKE_HUNHE on item.POKEID equals item4.POKEID
                                where item2.CIGARETTETYPE == 40 && item.POKEID == pokeid
                                && item4.PULLSTATUS == 1
                                orderby item.SORTNUM, item.POKEID
                                select new HUNHEVIEW() { POKEID = item.POKEID, CIGARETTECODE = item.CIGARETTECODE, CIGARETTENAME = item2.CIGARETTENAME, MACHINESEQ = item2.MACHINESEQ, QUANTITY = item.POKENUM }).FirstOrDefault();
                   return query;
               }
               catch (Exception e)
               {
                   throw e;
               }
           }
       }


       public static List<HUNHENOWVIEW> GetSearchCigarette(string cname,int type)
       {
           using (Entities entity = new Entities())
           {
               try
               {
                   if (type==1)
                   {
                       var query = (from item in entity.T_UN_POKE
                                    join item2 in entity.T_PRODUCE_SORTTROUGH
                                    on item.TROUGHNUM equals item2.TROUGHNUM
                                    join item3 in entity.T_UN_TASK on item.TASKNUM equals item3.TASKNUM
                                    where item2.CIGARETTETYPE == 40 && (item2.CIGARETTENAME.Contains(cname))
                                    orderby item.SORTNUM, item.POKEID
                                    select new HUNHENOWVIEW() { tasknum = item.TASKNUM, sortnum = item.SORTNUM, customername = item3.CUSTOMERNAME, regioncode = item3.REGIONCODE, TROUGHNUM = item.MACHINESEQ, CIGARETTENAME = item2.CIGARETTENAME, pokenum = item.POKENUM, status = item.STATUS, pokeid = item.POKEID }).ToList();
                       return query;
                   }
                  else
                   {
                       var query = (from item in entity.T_UN_POKE
                                    join item2 in entity.T_PRODUCE_SORTTROUGH
                                    on item.TROUGHNUM equals item2.TROUGHNUM
                                    join item3 in entity.T_UN_TASK on item.TASKNUM equals item3.TASKNUM
                                    where item2.CIGARETTETYPE == 40 && (item3.CUSTOMERNAME.Contains(cname))
                                    orderby item.SORTNUM, item.POKEID
                                    select new HUNHENOWVIEW() { tasknum = item.TASKNUM, sortnum = item.SORTNUM, customername = item3.CUSTOMERNAME, regioncode = item3.REGIONCODE, TROUGHNUM = item.MACHINESEQ, CIGARETTENAME = item2.CIGARETTENAME, pokenum = item.POKENUM, status = item.STATUS, pokeid = item.POKEID }).ToList();
                       return query;
                   }
               }
               catch (Exception e)
               {
                   throw e;
               }
           }
       }
    }
}
