using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Model;

namespace InBound.Business
{
    public class ProducePokeService : BaseService
    {


        public static bool CheckExistCanSendBelt(int mainBelt, decimal groupno)
        {
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_PRODUCE_POKE where item.MAINBELT == mainBelt && item.GROUPNO == groupno && item.SORTSTATE == 10 select item).FirstOrDefault();
                if (query != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public static int GetSendMainbelt(decimal groupno, List<decimal> sortNum, List<decimal> xynum, out decimal DISPATCHESIZE)
        {
            int Mainbeltno = 0;
            int maxOrder = 100;
            DISPATCHESIZE = 0;
            decimal leftnum = 0;
            for (int i = 1; i <= 4; i++)
            {
                if (!CheckExistCanSendBelt(i, groupno))
                {
                    continue;
                   
                }
                T_PRODUCE_CACHE cache = ProduceCacheService.GetCache(groupno, i);
                decimal currentNum = ProducePokeService.LeftCount(groupno, i, sortNum[i - 1], xynum[i-1], cache.CACHESIZE ?? 0);
                if (currentNum == (cache.CACHESIZE ?? 0))//如果缓层是空的
                {
                    return i;
                                     
                }
                if (currentNum >= (cache.DISPATCHENUM ?? 0))
                {
                    //if (CheckExistCanSendBelt(i, groupno))
                    //{
                        int tempOrderCount = getUnionOrderCount(groupno, sortNum[i - 1],  i);
                        if (tempOrderCount <= maxOrder)
                        {
                            if (tempOrderCount < maxOrder)
                            {
                                maxOrder = tempOrderCount;
                                Mainbeltno = i;
                                DISPATCHESIZE = cache.DISPATCHESIZE ?? 0;
                                leftnum = (cache.CACHESIZE ?? 0) - currentNum;
                            }
                            else
                            {
                                if (leftnum > ((cache.CACHESIZE ?? 0) - currentNum))
                                {
                                    Mainbeltno = i;
                                    DISPATCHESIZE = cache.DISPATCHESIZE ?? 0;
                                    leftnum = (cache.CACHESIZE ?? 0) - currentNum;
                                }
                            }
                        }
                   // }
                }
            }
            return Mainbeltno;
        }

        public static int getUnionOrderCount(decimal groupno,decimal beginnum,int mainbelt)
        {
            List<decimal> listsortNum = new List<decimal>();
            decimal beginsortNum=beginnum;
            int orderCount=0;
           // decimal orderNum=0;
            using (Entities entity = new Entities())
            {

                 var query = (from item in entity.T_PRODUCE_POKE where item.SORTSTATE >= 15 && item.SORTNUM >= beginsortNum && item.GROUPNO == groupno && item.MAINBELT == mainbelt select item.SORTNUM).ToList();
                 if (query != null)
                 {
                     orderCount = query.Distinct().Count();
                 }
                
            }
            return orderCount;
        }
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
        public static Boolean CheckExistTaskNo(decimal taskno)
        {
            using (Entities entity = new Entities())
            {
                  var query =( from item in entity.T_PRODUCE_POKE where item.SORTNUM==taskno select item).FirstOrDefault();
                if(query!=null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
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
        public static decimal GetTroughNextUnFinished(string throughno,int orderCount)
        {
            decimal total = 0;
            using (Entities entity = new Entities())
            {

                T_PRODUCE_SORTTROUGH info = SortTroughService.GetFJTroughInfo(10, throughno, 20);
                var query = (from item in entity.T_PRODUCE_POKE
                             // where item.TROUGHNUM == throughno
                             where item.SORTSTATE != 20 && item.GROUPNO == info.GROUPNO
                             orderby item.SORTNUM
                             select item).ToList();
                if (query != null)
                {
                    total = query.Take(orderCount).Sum(p => p.POKENUM) ?? 0;
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
        public static decimal LeftCount(decimal groupno, int mainbelt, decimal sortnum, decimal zycount,decimal maxCount)
        {
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_PRODUCE_POKE
                             where item.SORTNUM >= sortnum && item.SORTSTATE >= 15 && item.UNIONSTATE!=20 && item.GROUPNO == groupno
                                 && item.MAINBELT == mainbelt
                             select item).Sum(x => x.POKENUM)??0;
                if (query != null)
                {
                    return maxCount - query + zycount;
                }
                else
                {
                    return maxCount;
                }
            }
        }
        public static Boolean CheckExistPreSendTask(decimal groupno,decimal state)
        {
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_PRODUCE_POKE where item.SORTSTATE == state && item.GROUPNO==groupno select item).ToList();
                if (query != null && query.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
 
        }
        static Object lockFlag = new Object();
        public static List<TaskDetail> GetCigaretteSendTask(decimal troughtype, decimal cigarettetype, decimal groupno)
        {
            using (Entities entity = new Entities())
            {   //找出待发送给plc的订单
                var query = (from item in entity.T_PRODUCE_POKE where item.SORTSTATE == 12 && item.GROUPNO == groupno orderby item.SORTNUM select item).FirstOrDefault();
                if (query != null)
                {   //该订单的烟柜等分布情况
                    var query2 = (from item in entity.T_PRODUCE_POKE
                                  where item.SORTNUM == query.SORTNUM && item.GROUPNO == groupno && item.SORTSTATE == 12
                                  orderby item.TROUGHNUM
                                  select new TaskDetail
                                  {
                                      MainBelt = item.MAINBELT ?? 0,
                                      tNum = item.SORTNUM ?? 0,
                                      qty = item.POKENUM ?? 0,
                                      Machineseq = item.MACHINESEQ ?? 0,  //烟柜通道                                      
                                      ExportNum = item.EXPORTNUM,//分拣出口号
                                      GroupNO = item.GROUPNO ?? 0,     //组号
                                      POCKPLACE = item.POKEPLACE.Value,   //放烟位置
                                      meragenum= item.MERAGENUM??0,
                                      UnionTasknum=item.UNIONTASKNUM??0,
                                      SortTroughNum=item.TROUGHNUM

                                  }
                             ).ToList();

                    if (query2 != null && query2.Count > 0)
                    {
                        return query2;
                    }
                    else//没找到数据,理论上不会有这种情况
                    {
                        return null;

                    }
                }
                else//已经没有分拣任务了
                {
                    return null;
                }
            }
        }

        public static object[] GetSortTask(decimal sortgroupno)
        {
            WriteLog writeLog = WriteLog.GetLog();

            object[] values = new object[49];
            for (int i = 0; i < values.Length; i++)//初始化一个数组
            {
                values[i] = 0;
            }
            List<TaskDetail> list;
            lock (lockFlag)
            {
                list = GetCigaretteSendTask(10, 20, sortgroupno);
            }
            if (list != null)
            {
                int i = 0;
                int totalCount = 0;
                foreach (var item in list)//组装所需要的信息
                {
                    if (item.qty != 0)
                    {
                        if (i == 0)
                        {
                            values[0] = item.tNum;
                            values[1] = int.Parse(item.ExportNum);//虚拟出口号
                            values[2] = item.MainBelt;
                            values[3] = 0;
                            values[48] = 1;//标志位  ||
                        }
                        using (Entities entity = new Entities())
                        {
                            var query = (from record in entity.T_PRODUCE_POKE
                                         where record.UNIONTASKNUM == item.UnionTasknum && record.TROUGHNUM == item.SortTroughNum
                                             && record.SORTNUM < item.SortNum
                                         select record).ToList();
                            if (query == null || query.Count==0)
                            {
                                values[(int)((item.Machineseq - (sortgroupno - 1) * 11 - 1) * 2 + 26)] = item.UnionTasknum;
                                values[(int)((item.Machineseq - (sortgroupno - 1) * 11 - 1) * 2 + 27)] = item.meragenum;

                            }
                        }
                        //if(item.UnionTasknum)
                        item.SortTroughNum = item.Machineseq + "";
                        double tempNum = double.Parse(item.SortTroughNum);
                        double ws = Math.Ceiling((tempNum) / 11) - 1;
                        tempNum = tempNum - (ws * 11);
                        item.SortTroughNum = tempNum + "";
                        values[4 + 2 * (int.Parse(item.SortTroughNum + "") - 1)] = int.Parse(item.qty + "");
                        //如果机械手吸烟需要多个订单一起吸，则需要计算放烟位置，目前先不考虑这么复杂，一次吸一个订单
                        //values[4 + 2 * (int.Parse(item.SortTroughNum + "") - 1)] = getPlace(item.CIGARETTDECODE, item.SortTroughNum, item.tNum);
                        values[5 + 2 * (int.Parse(item.SortTroughNum + "") - 1)] = item.POCKPLACE;
                        totalCount += int.Parse(item.qty + "");
                        i++;
                    }
                }
                values[3] = totalCount;
            }

            return values;
        }
         
        public static void UpdatePokeByGroupNo(decimal groupno, int orderAmount,int mainbelt)
        {
            using (Entities entity = new Entities())
            {
               // int count = 0;

                
                decimal beginSortnum = 0;
                decimal totalCount = 0;
                int j = 0;
                List<Decimal> sortnum = new List<decimal>();
                while (totalCount < orderAmount)
                {
                    var query = (from item in entity.T_PRODUCE_POKE where item.GROUPNO == groupno && item.SORTSTATE == 10 && item.MAINBELT==mainbelt && item.SORTNUM > beginSortnum orderby item.SORTNUM select item).FirstOrDefault();
                    if (query != null)
                    {
                        var query2 = (from item in entity.T_PRODUCE_POKE where item.GROUPNO == groupno && item.SORTNUM == query.SORTNUM  && item.MAINBELT==mainbelt select item).Sum(x => x.POKENUM )??0;
                        totalCount += query2;
                        if (totalCount <= orderAmount )
                        {
                            sortnum.Add(query.SORTNUM ?? 0);
                            beginSortnum = query.SORTNUM ?? 0;
                        }
                        else
                        {
                            if (j == 0)
                            {
                                sortnum.Add(query.SORTNUM ?? 0);
                                beginSortnum = query.SORTNUM ?? 0;
                            }
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                    j++;
                }
                //开始算合单 生成合单号
                var query3 = (from item1 in entity.T_PRODUCE_POKE where item1.GROUPNO == groupno && item1.MAINBELT == mainbelt && sortnum.Contains(item1.SORTNUM??0) select item1).ToList();
                var troughnums = query3.Select(x => x.TROUGHNUM).Distinct().ToList();
                var sendOrder = ((from task in entity.T_PRODUCE_POKE  select task ).Max(x => x.SECSORTNUM)??0)+1;
                foreach (var troughnum in troughnums)
                {
                    var templist = query3.Where(x => x.TROUGHNUM == troughnum).OrderBy(x=>x.SORTNUM).ToList();
                    decimal tempCount = 0;
                    int size = 0;

                    foreach (var record in templist)
                    {
                        size++;
                        record.SORTSTATE = 12;
                        if (tempCount + record.POKENUM < 10)
                        {
                           
                            tempCount += (record.POKENUM ?? 0);
                            record.POKEPLACE =tempCount;
                            record.SECSORTNUM = sendOrder;
                            sendOrder += 1;
                            if (size == templist.Count)
                            {
                                var temp = templist.Where(x => x.SORTNUM <= record.SORTNUM && x.UNIONTASKNUM == 0).OrderBy(x => x.SORTNUM).ToList();//.ForEach(x => { x.MERAGENUM = tempCount; x.UNIONTASKNUM = GetSeq("select S_produce_uniontasknum..Nextval from dual");  });
                              var unionnum = GetSeq("select S_produce_uniontasknum.Nextval from dual");
                                var pnum=tempCount;
                               foreach (var t in temp)
                              {
                                  t.MERAGENUM = tempCount;
                                  t.UNIONTASKNUM = unionnum;
                                  t.POKEPLACE = pnum;
                                  pnum -= (t.POKENUM??0);
                              }
                                //uniontasknum += 1;
                            }
                        }
                        else
                        {
                            if (tempCount == 0)
                            {
                                tempCount = record.POKENUM ?? 0;
                                if (tempCount <= 10)
                                {
                                    record.POKEPLACE =  tempCount;
                                }
                                else
                                {
                                    record.POKEPLACE = tempCount % 10;
                                }
                                record.SECSORTNUM = sendOrder;
                                sendOrder += 1;
                                record.MERAGENUM = tempCount;
                                record.UNIONTASKNUM = GetSeq("select S_produce_uniontasknum.Nextval from dual"); 
                               // uniontasknum += 1;
                            }
                            else
                            {
                               var temp= templist.Where(x => x.SORTNUM < record.SORTNUM && x.UNIONTASKNUM==0 ).OrderBy(x=>x.SORTNUM).ToList();
                              // temp.ForEach(x => { x.MERAGENUM = tempCount; x.UNIONTASKNUM = GetSeq("select S_produce_uniontasknum..Nextval from dual");  });
                                //uniontasknum += 1;
                               var unionnum = GetSeq("select S_produce_uniontasknum.Nextval from dual");
                               var pnum = tempCount;
                               foreach (var t in temp)
                               {
                                   t.MERAGENUM = tempCount;
                                   t.UNIONTASKNUM = unionnum;
                                   t.POKEPLACE = pnum;
                                   pnum -= (t.POKENUM ?? 0);
                               }
                                
                                tempCount = record.POKENUM??0;
                                record.SECSORTNUM = sendOrder;
                                sendOrder += 1;
                                if (tempCount <= 10)
                                {
                                    record.POKEPLACE = tempCount;
                                }
                                else
                                {
                                    record.POKEPLACE =tempCount%10;
                                }
                                
                            }

                            if (size == templist.Count)
                            {
                                var temp = templist.Where(x => x.SORTNUM <= record.SORTNUM && x.UNIONTASKNUM == 0).OrderBy(x => x.SORTNUM).ToList();//.ForEach(x => { x.MERAGENUM = tempCount;  x.UNIONTASKNUM = GetSeq("select S_produce_uniontasknum..Nextval from dual");  });
                               var unionnum = GetSeq("select S_produce_uniontasknum.Nextval from dual");
                               var pnum = tempCount;
                               foreach (var t in temp)
                               {
                                   t.MERAGENUM = tempCount;
                                   t.UNIONTASKNUM = unionnum;
                                   t.POKEPLACE = pnum;
                                   pnum -= (t.POKENUM ?? 0);
                               }
                                // uniontasknum += 1;
                            }
                          
                        }
                    }
                    
                }
                entity.SaveChanges();
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
