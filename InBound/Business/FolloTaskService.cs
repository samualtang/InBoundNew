using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Model;

namespace InBound.Business
{
    /// <summary>
    /// 任务跟踪
    /// </summary>
    public class FolloTaskService
    {
        public void test()
        { }
        public static decimal FullCount = 17;
        public static List<T_PRODUCE_TASK> getAllTask()
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_PRODUCE_TASK orderby item.SORTNUM select item;
                return query.ToList();
            }
        }
        public static List<T_PRODUCE_TASK> getAllTask(decimal tasknum)
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_PRODUCE_TASK where item.SORTNUM == tasknum select item;
                return query.ToList();
            }
        }

        /// <summary>
        /// 获取机械手任务
        /// </summary>
        /// <param name="machineseq"></param>
        /// <returns></returns>
        public static List<FollowTaskDeail> getAllMachineTask(decimal machineseq)
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_PRODUCE_POKE
                            join item2 in dataentity.T_PRODUCE_SORTTROUGH
                            on item.TROUGHNUM equals item2.TROUGHNUM
                            where item.MACHINESEQ == machineseq
                            orderby item.UNIONTASKNUM
                            select new FollowTaskDeail() { CIGARETTDECODE = item2.CIGARETTECODE, CIGARETTDENAME = item2.CIGARETTENAME, Machineseq = item.MACHINESEQ ?? 0, UnionTasknum = item.UNIONTASKNUM ?? 0, MERAGENUM = item.MERAGENUM ?? 0, tNum = item.MERAGENUM ?? 0, Billcode = item.BILLCODE, MachineState = item.MACHINESTATE ?? 0 };
                if (query != null)
                    return query.Distinct().OrderBy(x => x.UnionTasknum).ToList();
                else return null;

            }
        }
        /// <summary>
        /// 获取合流机械手任务
        /// </summary>
        /// <param name="machineseq"></param>
        /// <param name="uniontaskNum"></param>
        /// <returns></returns>
        public static List<FollowTaskDeail> getMachineTask(decimal machineseq, decimal uniontaskNum)
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_PRODUCE_POKE
                            join item2 in dataentity.T_PRODUCE_SORTTROUGH
                            on item.TROUGHNUM equals item2.TROUGHNUM
                            where item.MACHINESEQ == machineseq && item.UNIONTASKNUM == uniontaskNum
                            select new FollowTaskDeail() { CIGARETTDECODE = item2.CIGARETTECODE, CIGARETTDENAME = item2.CIGARETTENAME, Machineseq = item.MACHINESEQ ?? 0, UnionTasknum = item.UNIONTASKNUM ?? 0, tNum = item.MERAGENUM ?? 0, Billcode = item.BILLCODE, MachineState = item.MACHINESTATE ?? 0 };
                if (query != null)
                    return query.Distinct().OrderBy(x => x.UnionTasknum).ToList();
                else return null;

            }
        }
        /// <summary>
        /// 所有分拣任务
        /// </summary>
        /// <param name="groupNo1">组1</param>
        /// <param name="groupNo2">组2 </param>
        /// <returns></returns>
        public static List<FollowTaskDeail> getFJDataAll(decimal groupNo1, decimal groupNo2)
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_PRODUCE_POKE
                            join item2 in dataentity.T_PRODUCE_SORTTROUGH
                                on item.TROUGHNUM equals item2.TROUGHNUM
                            where (item.GROUPNO == groupNo1 || item.GROUPNO == groupNo2) && item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20
                            orderby item.SORTNUM
                            select new FollowTaskDeail() { 
                                CIGARETTDECODE = item2.CIGARETTECODE,
                                CIGARETTDENAME = item2.CIGARETTENAME,
                                Machineseq = item.MACHINESEQ ?? 0, 
                                SortNum = item.SORTNUM ?? 0, 
                                tNum = item.POKENUM ?? 0, 
                                Billcode = item.BILLCODE, 
                                SortState = item.SORTSTATE ?? 0 };
                if (query != null)
                    return query.OrderBy(x => x.SortNum).ToList();
                else return null;
            }
        }
        /// <summary>
        /// 根据sortnum查询分拣任务
        /// </summary>
        /// <param name="sortnum"></param>
        /// <param name="groupNo1"></param>
        /// <param name="groupNo2"></param>
        /// <returns></returns>
        public static List<FollowTaskDeail> getFJData(decimal sortnum, decimal groupNo1, decimal groupNo2)
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_PRODUCE_POKE
                            join item2 in dataentity.T_PRODUCE_SORTTROUGH
                            on item.TROUGHNUM equals item2.TROUGHNUM
                            where item.SORTNUM == sortnum && (item.GROUPNO == groupNo1 || item.GROUPNO == groupNo2) && item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20
                            orderby item.SORTNUM
                            select new FollowTaskDeail() { CIGARETTDECODE = item2.CIGARETTECODE, CIGARETTDENAME = item2.CIGARETTENAME, Machineseq = item.MACHINESEQ ?? 0, SortNum = item.SORTNUM ?? 0, tNum = item.POKENUM ?? 0, Billcode = item.BILLCODE, SortState = item.SORTSTATE ?? 0 };
                if (query != null)
                    return query.OrderBy(x => x.SortNum).ToList() ;
                else return null;
            }
        }
        /// <summary>
        /// 获取合流数据
        /// </summary>
        /// <returns></returns>
        public static List<FollowTaskDeail> getUnionDataAll()
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_PRODUCE_POKE
                            orderby item.SORTNUM
                            group item by new { item.BILLCODE, item.SORTNUM, item.UNIONSTATE } into g
                            select new FollowTaskDeail() { SortNum = g.Key.SORTNUM ?? 0, tNum = g.Sum(x => x.POKENUM ?? 0), Billcode = g.Key.BILLCODE, UnionState = g.Key.UNIONSTATE ?? 0 };
                if (query != null)
                    return query.OrderBy(x => x.SortNum).ToList();
                else return null;
            }
        }

        public static List<FollowTaskDeail> getUnionData(decimal sortnum)
        {
            using (Entities dataentity = new Entities())
            {
            var query = from item in dataentity.T_PRODUCE_POKE
                            where item.SORTNUM == sortnum
                            orderby item.SORTNUM
                            group item by new { item.BILLCODE, item.SORTNUM, item.UNIONSTATE } into g
                            select new FollowTaskDeail() { SortNum = g.Key.SORTNUM ?? 0, tNum = g.Sum(x => x.POKENUM ?? 0), Billcode = g.Key.BILLCODE, UnionState = g.Key.UNIONSTATE ?? 0 };
                if (query != null)
                    return query.OrderBy(x => x.SortNum).ToList();
                  
                else return null;
            }
        }
        /// <summary>
        /// 获取机械手前皮带信息
        /// </summary>
        /// <returns></returns>
        public static List<FollowTaskDeail> GetMachineBeltInfp()
        { 
            using (Entities dataentity = new Entities())
            {


            }

        }
        /// <summary>
        /// 查询机械手任务
        /// </summary>
        /// <param name="groupno">组号</param>
        /// <param name="machineno">机械手号</param>
        /// <param name="uniontasknum">合单任务号</param>
        /// <returns></returns>
        //public static List<FollowTaskDeail> GetMachineTask(decimal groupno, decimal machineseq, decimal uniontasknum)
        //{
        //    using (Entities dataentity = new Entities())
        //    {
        //        var query = from item in dataentity.T_PRODUCE_POKE
        //                    where item.GROUPNO == groupno && item.MACHINESEQ == machineseq && item.UNIONTASKNUM  == uniontasknum
        //                    orderby item.SORTNUM
        //    }
        //}
     
        /// <summary>
        /// 获取缓存区 包括正在执行抓条烟数量
        /// </summary>
        /// <param name="groupno">组号</param>
        /// <param name="mainbelt">主皮带号</param>
        ///  <param name="machineTaskExcuting">合流机械手当前执行任务号</param>
        ///   <param name="machinePokeNum">合流机械手当前执行抓数 </param>
        /// <returns></returns>
        public static List<FollowTaskDeail> getUnionCache(decimal groupno, decimal mainbelt, decimal machineTaskExcuting, decimal machinePokeNum)
        { 
            try
            {
                using (Entities dataentity = new Entities())
                {

                    var query =( from p in dataentity.T_PRODUCE_POKE
                                join t in dataentity.T_PRODUCE_SORTTROUGH
                                on p.MACHINESEQ equals t.MACHINESEQ
                                where t.GROUPNO == groupno && p.MAINBELT == mainbelt && t.CIGARETTETYPE == 20 && t.TROUGHTYPE == 10 && p.SORTNUM >= machineTaskExcuting
                                orderby p.SORTNUM, p.MACHINESEQ
                                select new FollowTaskDeail(){CIGARETTDECODE = t.CIGARETTECODE, CIGARETTDENAME = t.CIGARETTENAME,POKENUM = p.POKENUM ?? 0, Machineseq = p.MACHINESEQ ?? 0,POKEID = p.POKEID,MainBelt = p.MAINBELT ?? 0,SortNum = p.SORTNUM ?? 0,GroupNO = t.GROUPNO ?? 0}).ToList();
                    if (query != null)
                    {
                        //获取当前抓烟任务的订单总烟数
                            decimal pokenumTotail = query.Where(a => a.SortNum == machineTaskExcuting && a.GroupNO == groupno && a.MainBelt == mainbelt).Select(z => z.POKENUM).Sum();//注意:数量可能为空(null) 原因:订单数据异常

                            return GetUnionCacheByPokenum(query.ToList(), machineTaskExcuting, machinePokeNum, pokenumTotail);
                    }
                    else { return null; }

                }
            }
            catch (Exception e)
            { 
                throw e;
            }
        }
       
        /// <summary>
        /// 合流缓存区算法
        /// </summary>
        /// <param name="list">当前任务集合</param>
        /// <param name="machineTaskExcuting">当前抓取排序号(sortnum)</param>
        /// <param name="machinePokeNum">当前抓数</param>
        ///  <param name="pokenumTotail">抓烟总数</param>
        /// <returns></returns>
        private static List<FollowTaskDeail> GetUnionCacheByPokenum(List<FollowTaskDeail> list, decimal machineTaskExcuting, decimal machinePokeNum, decimal pokenumTotail)
        {
            decimal TotailmachinePokeNum = Math.Ceiling(pokenumTotail / 10);//总数抓数 
            if (pokenumTotail < 10 * machinePokeNum || TotailmachinePokeNum == machinePokeNum )//如果当前抓烟任务的订单总烟数 小于十   去掉前抓烟任务 (包括最后一个任务)
            {
                   list.RemoveAll(a => a.SortNum == machineTaskExcuting); 
            }
            else
            {
                for (int Count = 1; Count <= machinePokeNum; Count++)//根据当前抓数抓数
                { 
                    var pokenum = list.Where(c=> c.POKEID == list[0].POKEID).Select(a => new { pokeid = a.POKEID, pokenum = a.POKENUM }).FirstOrDefault();//获取当前任务抓烟数  
                    if (pokenum.pokenum == 10)//如果有一抓等于10  直接去掉 
                    {
                        list.RemoveAll(a => a.SortNum == machineTaskExcuting && a.POKEID == list[0].POKEID);//
                    }
                    else if (pokenum.pokenum < 10)//当第一个任务pokenum小于10 就和下一个任务的pokenum相加 直到大于等于10
                    {
                        decimal[] Listmachineseq = new decimal[10];//存放品牌 和 对应品牌的抓取数小于10得品牌   --- //存放pokeid
                        decimal sum = 0;//和 
                        decimal lastNum = 0;//上一次 
                        decimal endNum = 0;//最后一次
                        for (int i = 0; i <= Listmachineseq.Length; i++) 
                        {
                            sum += list[i].POKENUM; 
                            if (sum == 10)
                            {
                                Listmachineseq[i] = list[i].POKEID;
                                for (int j = i; j >= 0; j--)
                                {
                                    list.RemoveAll(a => a.POKEID == Listmachineseq[j]);
                                }
                                break;
                            }
                            else if (sum > 10) 
                            {
                                endNum = list.Find(x => x.POKEID == list[i - 1].POKEID).POKENUM -= (list[i - 1].POKENUM - (10 - lastNum)); //对相加大于10最后一个pokenum的值 所取的数量 相减
                                list.Find(z => z.POKEID == list[i].POKEID).POKENUM -= endNum;
                                for (int j = i; j >= 0; j--)
                                {
                                    list.RemoveAll(a => a.POKEID == Listmachineseq[j]);
                                }
                                //return list;
                                 break;
                            }
                            lastNum = sum; 
                            Listmachineseq[i] = list[i].POKEID;//存入当前pokeid
                        } 
                    }
                }
            } 
            return list; 
        }

        /// <summary>
        /// 获取摇摆前任务信息
        /// </summary>
        /// <param name="mainbelt">主皮带通道</param>
        /// <param name="groupno">组号</param>
        /// <returns></returns>
        public static List<FollowTaskDeail> GetSortingBeltTask(decimal mainbelt,decimal groupno)
        {
            using (Entities dataentity = new Entities())
            {
                if (mainbelt != 5)
                {
                    var query1 = (from p in dataentity.T_PRODUCE_POKE
                                  join t in dataentity.T_PRODUCE_SORTTROUGH
                                  on p.MACHINESEQ equals t.MACHINESEQ
                                  where p.MAINBELT == mainbelt &&  t.GROUPNO == groupno &&p.SORTSTATE == 15  && t.TROUGHTYPE == 10 && t.CIGARETTETYPE == 20
                                  orderby p.SORTNUM, p.MACHINESEQ
                                  select new FollowTaskDeail
                                  {
                                      CIGARETTDECODE = t.CIGARETTECODE,
                                      CIGARETTDENAME = t.CIGARETTENAME,
                                      POKENUM = p.POKENUM ?? 0,
                                      Machineseq = p.MACHINESEQ ?? 0,
                                      POKEID = p.POKEID,
                                      MainBelt = p.MAINBELT ?? 0,
                                      UnionTasknum = p.UNIONTASKNUM ?? 0,
                                      SortNum = p.SORTNUM ?? 0,
                                      GroupNO = t.GROUPNO ?? 0,
                                      Billcode = p.BILLCODE
                                  }).ToList();
                    if (query1 != null)
                    {
                        return query1;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    var query1 = (from p in dataentity.T_PRODUCE_POKE
                                  join t in dataentity.T_PRODUCE_SORTTROUGH
                                  on p.MACHINESEQ equals t.MACHINESEQ
                                  where t.GROUPNO == groupno && p.SORTSTATE == 10  && t.TROUGHTYPE == 10 && t.CIGARETTETYPE == 20
                                  orderby p.SORTNUM, p.MACHINESEQ
                                  select new FollowTaskDeail
                                  {
                                      CIGARETTDECODE = t.CIGARETTECODE,
                                      CIGARETTDENAME = t.CIGARETTENAME,
                                      POKENUM = p.POKENUM ?? 0,
                                      Machineseq = p.MACHINESEQ ?? 0,
                                      POKEID = p.POKEID,
                                      MainBelt = p.MAINBELT ?? 0,
                                      UnionTasknum = p.UNIONTASKNUM ?? 0,
                                      SortNum = p.SORTNUM ?? 0,
                                      GroupNO = t.GROUPNO ?? 0,
                                      Billcode = p.BILLCODE
                                  }).ToList();
                    if (query1 != null)
                    {
                        return query1;
                    }
                    else
                    {
                        return null;
                    }
                }
               
            }
        }
    }
}
