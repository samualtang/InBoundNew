﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Model;
using System.Threading;

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
                decimal currentNum = ProducePokeService.LeftCount(groupno, i, sortNum[i - 1], xynum[i - 1], cache.CACHESIZE ?? 0);
                WriteLog.GetLog().Write("主皮带:" + i + "剩余空间:" + currentNum + "当前任务号:" + sortNum[i - 1]+" 已抓烟数量:"+xynum[i-1]);
                if ((cache.CACHESIZE??0)-currentNum<10)//如果缓层小于10
                {
                    DISPATCHESIZE = cache.DISPATCHESIZE ?? 0;
                    WriteLog.GetLog().Write("组号:" + groupno + "当前发送主皮带:" + i);
                    return i;

                }
               
                if (currentNum >= (cache.DISPATCHENUM ?? 0))
                {
                    //if (CheckExistCanSendBelt(i, groupno))
                    //{
                    int tempOrderCount = getUnionOrderCount(groupno, sortNum[i - 1], i);
                    WriteLog.GetLog().Write("主皮带:" + i + "剩余空间:" + currentNum + "当前任务号:" + sortNum[i - 1] + " 已抓烟数量:" + xynum[i - 1] + " 可支撑订单数:" + tempOrderCount);
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
            WriteLog.GetLog().Write("组号:"+groupno+ "当前发送主皮带:" + Mainbeltno);
            return Mainbeltno;
        }
        public static int getUnionOrderCount(decimal groupno, decimal beginnum, int mainbelt)
        {
            List<decimal> listsortNum = new List<decimal>();
            decimal beginsortNum = beginnum;
            int orderCount = 0;
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
                                      SortTroughNum=item.TROUGHNUM,
                                      SortNum=item.SORTNUM??0,
                                      countFlags = item.SENDPOKENUM ?? 0 //(2018/11/20新增 每计算一波的标志)
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

            object[] values = new object[51];
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
                decimal checkNum = 0;
               // decimal check
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
                            values[49] = 1;//标志位 
                            values[50] = item.countFlags;  //(2018/11/20新增 每计算一波的标志)
                        }
                        using (Entities entity = new Entities())
                        {
                            var query = (from record in entity.T_PRODUCE_POKE
                                         where record.UNIONTASKNUM == item.UnionTasknum && record.TROUGHNUM == item.SortTroughNum
                                             && record.SORTNUM < item.SortNum
                                         select record).ToList();
                            if (query == null || query.Count==0)
                            {
                                int seq = ((int)item.Machineseq)%11;
                                if (seq == 0)
                                    seq = 11;
                                seq -= 1;
                                checkNum += (decimal)Math.Pow(2, seq);
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
                values[48] = checkNum;
            }

            return values;
        }
        private static ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
       
        public static void UpdatePokeByGroupNo(decimal groupno, int orderAmount,int mainbelt)
        {

           
            try
            {
                cacheLock.EnterWriteLock();
                #region
                using (Entities entity = new Entities())
                {
                    // int count = 0;


                    decimal beginSortnum = 0;
                    decimal totalCount = 0;
                    List<Decimal> sortnum = new List<decimal>();
                    int countNum = 0;
                    while (totalCount < orderAmount)
                    {
                        countNum += 1;
                        var query = (from item in entity.T_PRODUCE_POKE where item.GROUPNO == groupno && item.SORTSTATE == 10 && item.MAINBELT == mainbelt && item.SORTNUM > beginSortnum orderby item.SORTNUM select item).FirstOrDefault();
                        if (query != null)
                        {
                            var query2 = (from item in entity.T_PRODUCE_POKE where item.GROUPNO == groupno && item.SORTNUM == query.SORTNUM && item.MAINBELT == mainbelt select item).Sum(x => x.POKENUM) ?? 0;
                            totalCount += query2;
                            if (totalCount <= orderAmount)
                            {
                                sortnum.Add(query.SORTNUM ?? 0);
                                beginSortnum = query.SORTNUM ?? 0;
                            }
                            else
                            {
                                if (countNum == 1)
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

                    }
                    if (sortnum.Count == 1)//只有一个单的情况下
                    {
                        var oneSortnum = (from item in entity.T_PRODUCE_POKE where item.GROUPNO == groupno && item.SORTSTATE == 10 && item.SORTNUM == sortnum.Min() select item).ToList();
                        foreach (var item in oneSortnum)
                        {
                            item.SENDPOKENUM = 3;//一个单情况下
                        }
                    }
                    else
                    {
                        //需要计算的最小任务号
                        var mixSortnum = (from item in entity.T_PRODUCE_POKE where item.GROUPNO == groupno && item.SORTSTATE == 10 && item.SORTNUM == sortnum.Min()select item ).ToList();
                        //需要计算的最大任务号
                        var maxSortnum = (from item in entity.T_PRODUCE_POKE where item.GROUPNO == groupno && item.SORTSTATE == 10 && item.SORTNUM == sortnum.Max() select item).ToList();
                        if (mixSortnum != null && maxSortnum != null)
                        {
                            foreach (var item in mixSortnum)
                            {
                                item.SENDPOKENUM = 1;//头
                            }
                            foreach (var item in maxSortnum)
                            {
                                item.SENDPOKENUM = 2;//尾
                            }
                        }
                        else
                        {
                            WriteLog.GetLog().Write("写入标志失败");
                        }
                    }
                    //开始算合单 生成合单号
                    var query3 = (from item1 in entity.T_PRODUCE_POKE where item1.GROUPNO == groupno && item1.MAINBELT == mainbelt && sortnum.Contains(item1.SORTNUM ?? 0) select item1).ToList();
                    var troughnums = query3.Select(x => x.TROUGHNUM).Distinct().ToList();
                    var sendOrder = ((from task in entity.T_PRODUCE_POKE select task).Max(x => x.SECSORTNUM) ?? 0) + 1;
                    foreach (var troughnum in troughnums)
                    {
                        var templist = query3.Where(x => x.TROUGHNUM == troughnum).OrderBy(x => x.SORTNUM).ToList();
                        decimal tempCount = 0;
                        int size = 0;

                        foreach (var record in templist)
                        {
                            size++;
                            record.SORTSTATE = 12;
                            if (tempCount + record.POKENUM < 10)
                            {

                                tempCount += (record.POKENUM ?? 0);
                                record.POKEPLACE = tempCount;
                                record.SECSORTNUM = sendOrder;
                                sendOrder += 1;
                                if (size == templist.Count)
                                {
                                    var temp = templist.Where(x => x.SORTNUM <= record.SORTNUM && x.UNIONTASKNUM == 0).OrderBy(x => x.SORTNUM).ToList();//.ForEach(x => { x.MERAGENUM = tempCount; x.UNIONTASKNUM = GetSeq("select S_produce_uniontasknum..Nextval from dual");  });
                                    var unionnum = GetSeq("select S_produce_uniontasknum.Nextval from dual");
                                    var pnum = tempCount;
                                    WriteLog.GetLog().Write("1机械手任务号:" + unionnum + "meragenum:" + pnum);
                                    foreach (var t in temp)
                                    {

                                        t.MERAGENUM = tempCount;
                                        t.UNIONTASKNUM = unionnum;
                                        if (pnum > 10)
                                        {
                                            t.POKEPLACE = pnum % 10;
                                            if (t.POKEPLACE == 0)
                                            {
                                                t.POKEPLACE = 10;
                                            }
                                        }
                                        else
                                        {
                                            t.POKEPLACE = pnum;
                                        }
                                        pnum -= (t.POKENUM ?? 0);
                                        WriteLog.GetLog().Write("sortnum" + t.SORTNUM + "pokeplace:" + t.POKEPLACE);
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
                                        record.POKEPLACE = tempCount;
                                    }
                                    else
                                    {
                                        record.POKEPLACE = record.POKENUM % 10;
                                        if (record.POKEPLACE == 0)
                                        {
                                            record.POKEPLACE = 10;
                                        }
                                    }
                                    record.SECSORTNUM = sendOrder;
                                    sendOrder += 1;
                                    record.MERAGENUM = tempCount;
                                    record.UNIONTASKNUM = GetSeq("select S_produce_uniontasknum.Nextval from dual");
                                    // uniontasknum += 1;
                                }
                                else
                                {
                                    var temp = templist.Where(x => x.SORTNUM < record.SORTNUM && x.UNIONTASKNUM == 0).OrderBy(x => x.SORTNUM).ToList();
                                    // temp.ForEach(x => { x.MERAGENUM = tempCount; x.UNIONTASKNUM = GetSeq("select S_produce_uniontasknum..Nextval from dual");  });
                                    //uniontasknum += 1;
                                    var unionnum = GetSeq("select S_produce_uniontasknum.Nextval from dual");
                                    var pnum = tempCount;
                                    WriteLog.GetLog().Write("2机械手任务号:" + unionnum + "meragenum:" + pnum);
                                    foreach (var t in temp)
                                    {

                                        t.MERAGENUM = tempCount;
                                        t.UNIONTASKNUM = unionnum;
                                        if (pnum > 10)
                                        {
                                            t.POKEPLACE = pnum % 10;
                                            if (t.POKEPLACE == 0)
                                            {
                                                t.POKEPLACE = 10;
                                            }
                                        }
                                        else
                                        {
                                            t.POKEPLACE = pnum;
                                        }
                                        pnum -= (t.POKENUM ?? 0);
                                        WriteLog.GetLog().Write("sortnum" + t.SORTNUM + "pokeplace:" + t.POKEPLACE);
                                    }

                                    tempCount = record.POKENUM ?? 0;
                                    record.SECSORTNUM = sendOrder;
                                    sendOrder += 1;
                                    if (tempCount <= 10)
                                    {
                                        record.POKEPLACE = tempCount;
                                    }
                                    else
                                    {
                                        record.POKEPLACE = tempCount % 10;
                                        if (record.POKEPLACE == 0)
                                        {
                                            record.POKEPLACE = 10;
                                        }
                                    }

                                }

                                if (size == templist.Count)
                                {
                                    var temp = templist.Where(x => x.SORTNUM <= record.SORTNUM && x.UNIONTASKNUM == 0).OrderBy(x => x.SORTNUM).ToList();//.ForEach(x => { x.MERAGENUM = tempCount;  x.UNIONTASKNUM = GetSeq("select S_produce_uniontasknum..Nextval from dual");  });
                                    var unionnum = GetSeq("select S_produce_uniontasknum.Nextval from dual");
                                    var pnum = tempCount;

                                    WriteLog.GetLog().Write("3机械手任务号:" + unionnum + "meragenum:" + pnum);
                                    foreach (var t in temp)
                                    {

                                        t.MERAGENUM = tempCount;
                                        t.UNIONTASKNUM = unionnum;
                                        if (pnum > 10)
                                        {
                                            t.POKEPLACE = pnum % 10;
                                            if (t.POKEPLACE == 0)
                                            {
                                                t.POKEPLACE = 10;
                                            }
                                        }
                                        else
                                        {
                                            t.POKEPLACE = pnum;
                                        }
                                        pnum -= (t.POKENUM ?? 0);
                                        WriteLog.GetLog().Write("sortnum" + t.SORTNUM + "pokeplace:" + t.POKEPLACE);
                                    }
                                    // uniontasknum += 1;
                                }

                            }

                          
                        }

                    }
                    entity.SaveChanges();
                }
                #endregion
            }
            
            finally
            {
                cacheLock.ExitWriteLock();
            }
        }
        public static void FetchTaskByTroughNo(string troughNo, string standbyNo)
        {
            using (Entities entity = new Entities())
            {

                var query = (from poke in entity.T_PRODUCE_POKE
                             where poke.TROUGHNUM == troughNo
                             select poke).ToList();

                var tempquery = (from poke in entity.T_PRODUCE_SORTTROUGH
                                 where poke.TROUGHNUM == standbyNo
                             select poke).FirstOrDefault();
                //更换通道编码

                var querysource = (from item in entity.T_PRODUCE_SORTTROUGH where item.TROUGHNUM == troughNo select item).FirstOrDefault();
                var querytarget = (from item in entity.T_PRODUCE_SORTTROUGH where item.TROUGHNUM == standbyNo select item).FirstOrDefault();
                var tempcigarettecode = querytarget.CIGARETTECODE;
                var tempCigarettename = querytarget.CIGARETTENAME;
                var tempmachineseq = querytarget.MACHINESEQ;
                var status = querytarget.STATE;

                var tempcigarettecode1 = querysource.CIGARETTECODE;
                var tempCigarettename1 = querysource.CIGARETTENAME;
                var tempmachineseq1 = querysource.MACHINESEQ;
                var status1 = querysource.STATE;

                querysource.CIGARETTENAME = tempCigarettename;
                querysource.CIGARETTECODE = tempcigarettecode;
               // querysource.MACHINESEQ = tempmachineseq;
                querysource.STATE = status;

                querytarget.CIGARETTENAME = tempCigarettename1;
                querytarget.CIGARETTECODE = tempcigarettecode1;
                //querytarget.MACHINESEQ = tempmachineseq1;
                querytarget.STATE = status1;
                //entity.SaveChanges();
               
                decimal groupno=0;
                if (query != null)
                {
                    query.ForEach(f =>
                    {
                        f.TROUGHNUM = standbyNo;
                        f.MACHINESEQ = tempquery.MACHINESEQ;
                    });
                    groupno = query[0].GROUPNO ?? 0;
                    var querytemp=(from item in entity.T_PRODUCE_POKE where item.GROUPNO==groupno && item.SORTSTATE>10 &&item.SORTSTATE<=15 select item).ToList();
                    if(querytemp!=null)
                    {
                         foreach ( var record in querytemp)
                         {
                             record.SORTSTATE = 10;
                             record.POKEPLACE = 0;
                             record.MERAGENUM = 0;
                             record.MACHINESTATE = 10;
                             record.UNIONTASKNUM = 0;
                         }
                    }
                }
                
                //获取最近完成的该通道任务
                //var task = query.Where(w => w.SORTSTATE == 20 && w.TROUGHNUM==troughNo).OrderByDescending(w=>w.SORTNUM).FirstOrDefault();
                //var query1 = (from poke in entity.T_PRODUCE_POKE
                //             where poke.TROUGHNUM == troughNo && poke.SORTNUM<=task.SORTNUM
                //             select poke).ToList();
    
                //query1.ForEach(f =>
                //{
                //   // f.TROUGHNUM = standbyNo;
                //    f.MACHINESTATE = 20;//更新预分拣已完成的订单对应机械手的状态为已完成
                //});
                //if (task != null)
                //{
                // var queryList=(from item in entity.T_PRODUCE_POKE where item.UNIONTASKNUM==task.UNIONTASKNUM select item).ToList();

                //    if(queryList!=null)
                //    {
                //        var unfinished = queryList.Where(w => w.SORTSTATE != 20).Sum(w=>w.POKENUM);
                //        if (unfinished != null && unfinished != 0)
                //        {
                //            decimal place = 1;
                //         foreach(var item in queryList)
                //         {
                //             if (item.SORTSTATE != 20)
                //             {
                //                 item.POKEPLACE = place;
                //                 place += (item.POKENUM??0);
                //             }
                //             item.MERAGENUM = unfinished;
                //         }
                //        }
                //    }
                //}
     
                entity.SaveChanges();
            }
        }
        /// <summary>
        /// 烟柜更换
        /// </summary>
        /// <param name="soucreTrougNum">源烟柜</param>
        /// <param name="tagGroupNo">目标组</param>
        /// <param name="tagTrougNum">目标烟柜</param>
        public static void FetchPokeTroughByTroughNo(string soucreTrougNum,   string tagTrougNum)
        {
            using (Entities entity = new Entities())
            {
                var source = (from item in entity.T_PRODUCE_POKE where item.TROUGHNUM == soucreTrougNum select item).ToList();//获取源烟柜所有任务
                var tag = (from item in entity.T_PRODUCE_SORTTROUGH where item.TROUGHNUM == tagTrougNum select item).FirstOrDefault();//获取目标烟柜通道信息
                if (source != null && source.Count > 0 && tag != null)
                {
                    source.ForEach(f =>
                        {
                            f.TROUGHNUM = tagTrougNum;//将源烟柜通道更换为目标烟柜通道
                            f.MACHINESEQ = tag.MACHINESEQ;// 将源烟柜物理通道号更换为物理通道号
                            f.GROUPNO = tag.GROUPNO;//将源烟柜组更换为目标烟柜通组
                        });
                }
                entity.SaveChanges();
                
            }
        }

        public static void FetchPokeByTroughNo(string troughNo, string standbyNo)
        {
            using (Entities entity = new Entities())
            {

                var query = (from poke in entity.T_PRODUCE_POKE
                             where poke.TROUGHNUM == troughNo
                             select poke).ToList();

                var tempquery = (from poke in entity.T_PRODUCE_SORTTROUGH
                                 where poke.TROUGHNUM == standbyNo
                                 select poke).FirstOrDefault(); 
                decimal groupno = 0;
                if (query != null)
                {
                    query.ForEach(f =>
                    {
                        f.TROUGHNUM = standbyNo;
                        f.MACHINESEQ = tempquery.MACHINESEQ;
                    });
                    groupno = query[0].GROUPNO ?? 0;

                    var querytemp = (from item in entity.T_PRODUCE_POKE where item.GROUPNO == groupno && item.SORTSTATE > 10 && item.SORTSTATE <= 15 select item).ToList();
                    if (querytemp != null)
                    {
                        foreach (var record in querytemp)
                        {
                            record.SORTSTATE = 10;
                            record.POKEPLACE = 0;
                            record.MERAGENUM = 0;
                            record.MACHINESTATE = 10;
                            record.UNIONTASKNUM = 0;
                        }
                    }
                }

                entity.SaveChanges();
            }
        }

        public static List<decimal> GetSortnumByNotCalcu()
        {
            using (Entities entity = new Entities())
            {
                List<decimal> list = new List<decimal>();
                for (int i = 1; i <= 4; i++)
                {
                    var sortnumMax = (from item in entity.T_PRODUCE_POKE
                                      where item.SORTSTATE >= 15 && item.MAINBELT == i
                                      select item).ToList();
                    if (sortnumMax.Count > 0 && sortnumMax != null)
                    {
                        list.Add(sortnumMax.Max(a => a.SORTNUM ?? 0));
                    }
                    else
                    {
                        list.Add(0);
                    }
                    
                }
                return list;
            }
        }
        /// <summary>
        /// 任务号写入数据库
        /// </summary>
        /// <param name="List">任务号集合</param>
        /// <param name="Step">步骤</param>
        public static void WriteSortNumToDb(List<decimal> List , decimal Step)
        {
            using (Entities entity = new Entities())
            {
                int mianbelt = 1;
                foreach (var item in List)
                {
                    var troughTrans = (from item1 in entity.T_PRO_TROUGHTRANSFER where item1.MIANBELT == mianbelt select item1).FirstOrDefault();
                    if (troughTrans != null)
                    {
                        if (Step == 0)//清空数据库任务号
                        {
                            troughTrans.SORTNUM = 0;//任务号
                            troughTrans.STEP = 0;//步骤
                        }
                        else
                        {
                            troughTrans.SORTNUM = item;//任务号
                            troughTrans.STEP = Step;//步骤
                        }
                    }
                    else
                    {
                        T_PRO_TROUGHTRANSFER t_pro_troughransfer = new InBound.T_PRO_TROUGHTRANSFER();
                        if (entity.T_PRO_TROUGHTRANSFER.Count() > 0)
                        {
                            t_pro_troughransfer.ID = entity.T_PRO_TROUGHTRANSFER.Max(z => z.ID) + 1;
                        }
                        else
                        {
                            t_pro_troughransfer.ID = 1;
                        }
                        t_pro_troughransfer.MIANBELT = mianbelt;
                        t_pro_troughransfer.SORTNUM = item;
                        t_pro_troughransfer.STEP = Step;
                        entity.T_PRO_TROUGHTRANSFER.AddObject(t_pro_troughransfer);
                    }
                    mianbelt++;
                    entity.SaveChanges();
                }
            }
        }
        /// <summary>
        /// 从数据库读取写入的任务号
        /// </summary>
        /// <param name="Step">步骤</param>
        public static void ReadSortNumByDb(out List<decimal> list,out decimal Step)
        {
            using (Entities entity = new Entities())
            {
                list = new List<decimal>();
                Step = 0;
                for (int i = 1; i <= 4; i++)
                {
                    var troughTrans = (from item1 in entity.T_PRO_TROUGHTRANSFER where item1.MIANBELT == i select item1).FirstOrDefault();
                    if (troughTrans != null)
                    {
                        list.Add(troughTrans.SORTNUM ?? 0);
                        Step = troughTrans.STEP ?? 0;
                    }
                    else
                    {
                        list.Add(0);
                        Step = 0;
                    }
                }
            }
        }
        public static bool GetExistSortnum( decimal sortnum)
        {
            using (Entities entity = new Entities())
            {
                var isOrNot =( from item in entity.T_PRODUCE_POKE
                              where item.SORTNUM == sortnum
                              select item).ToList().Count();
                if (isOrNot >0)
                    return true;
                else
                    return false;
            }
        }
        public static void RefSortByTiaoyan(List<decimal> list)
        {
          
            using (Entities entity = new Entities())
            {
                for (int i = 1; i <= 4; i++)
                {
                    if (list[i - 1] > 0)
                    {
                        decimal sortnum = list[i - 1];
                        var taskList = (from item in entity.T_PRODUCE_POKESEQ
                                       where item.MAINBELT == i
                                        select item).ToList();//获取总共的任务
                        var DrpoList = (from item in entity.T_PRODUCE_POKESEQ
                                        where item.SORTNUM > sortnum && item.MAINBELT == i
                                        select item).ToList();//获取这个任务之后的所有任务
                        foreach (var item in DrpoList)
                        {
                            entity.T_PRODUCE_POKESEQ.DeleteObject(item);
                        }
                        entity.SaveChanges();
                    }
                }
            }

        }
        /// <summary>
        /// 更新这个任务之后的任务状态
        /// </summary>
        /// <param name="sortnum">这个任务</param>
        /// <param name="status">状态</param>
        public static void UpdateAfterBySortnum(List<decimal> list,decimal status)
        {
            using (Entities entity = new Entities())
            {
                for (int i = 1; i <= 4; i++)
                {
                    if (list[i - 1] > 0)
                    {
                        decimal sortnum = list[i - 1];
                        var taskList = (from item in entity.T_PRODUCE_POKE
                                        where item.SORTNUM > sortnum && item.MAINBELT == i
                                        select item).ToList();//获取这个任务之后的所有任务
                        foreach (var item in taskList)
                        {
                            item.UNIONSTATE = status;
                            item.SORTSTATE = status;
                            if (item.POKEPLACE > 0)
                            {
                                item.POKEPLACE = 0;
                                item.MERAGENUM = 0;
                                item.MACHINESTATE = 10;
                                item.UNIONTASKNUM = 0;
                            }
                        }
                    }
                    entity.SaveChanges();
                }
               
            }
        }
        
        public static List<string> GetYGtroughnum(int groupno1, int groupno2)
        {
            using (Entities en = new Entities())
            {
                List<string> list = new List<string>();
                var ygtrough = (from item in en.T_PRODUCE_SORTTROUGH
                               where item.TROUGHTYPE == 10 && item.CIGARETTETYPE == 20 && (item.GROUPNO == groupno1 || item.GROUPNO == groupno2)
                               orderby item.MACHINESEQ
                               select item).ToList();
                foreach (var item in ygtrough)
                {
                    list.Add(item.TROUGHNUM );
                }
                return list;

            }
        }

        public static decimal GetStepNum()
        {
            using (Entities en = new Entities())
            {
                decimal stepnum = en.T_PRO_TROUGHTRANSFER.Select(x => x.STEP).FirstOrDefault() ?? -1;
                return stepnum;
            }
        }
    }
}
