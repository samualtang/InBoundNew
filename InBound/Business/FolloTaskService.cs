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
        /// 获取缓存区 包括正在执行抓条烟数量
        /// </summary>
        /// <param name="groupno">组号</param>
        /// <param name="mainbelt">主皮带号</param>
        ///  <param name="machineTaskExcuting">合流机械手当前执行任务号</param>
        ///   <param name="machinePokeNum">合流机械手当前执行抓数 </param>
        /// <returns></returns>
        public static List<FollowTaskDeail> getUnionCache(decimal groupno, decimal mainbelt, decimal machineTaskExcuting, decimal machinePokeNum)
        {
            
//select b1.sortnum,b1.tasknum,b2.cigarettename,b1.machineseq,b1.pokenum from 
//(select sortnum,tasknum,machineseq,pokenum from t_produce_poke  where groupno=1 and mainbelt=2 order by sortnum) b1 left join
//(select machineseq,cigarettecode,cigarettename from t_produce_sorttrough  where cigarettetype=20 and troughtype=10 and cigarettecode is not null) b2
//on b1.machineseq=b2.machineseq order by b1.sortnum,b1.machineseq;
            try
            {


                using (Entities dataentity = new Entities())
                {

                    var query = from p in dataentity.T_PRODUCE_POKE
                                join t in dataentity.T_PRODUCE_SORTTROUGH
                                on p.MACHINESEQ equals t.MACHINESEQ
                                where t.GROUPNO == groupno && p.MAINBELT == mainbelt && t.CIGARETTETYPE == 20 && t.TROUGHTYPE == 10 && p.SORTNUM >= machineTaskExcuting
                                orderby p.SORTNUM, p.MACHINESEQ
                                select new FollowTaskDeail() { 
                                    CIGARETTDECODE = t.CIGARETTECODE, 
                                    CIGARETTDENAME = t.CIGARETTENAME, 
                                    POKENUM = p.POKENUM ?? 0, 
                                    Machineseq = p.MACHINESEQ ?? 0, 
                                    POKEID = p.POKEID , 
                                    MainBelt = p.MAINBELT ?? 0,
                                    SortNum = p.SORTNUM ?? 0 ,
                                    GroupNO = t.GROUPNO ?? 0,
                                    mainBelt  =p.MAINBELT ?? 0
                                };

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
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="machineTaskExcuting">当前抓取任务号(sortnum)</param>
        /// <param name="machinePokeNum">当前抓数</param>
        /// <returns></returns>
        private static List<FollowTaskDeail> GetUnionCacheByPokenum(List<FollowTaskDeail> list, decimal machineTaskExcuting, decimal machinePokeNum, decimal pokenumTotail)
        {
            //300 groupno 7 pokenum 21  
            decimal TotailmachinePokeNum = Math.Ceiling(pokenumTotail / 10);//总数几抓 3
          
            if (pokenumTotail < 10 || TotailmachinePokeNum == machinePokeNum)//如果当前抓烟任务的订单总烟数 小于十   去掉前抓烟任务 (包括最后一个任务)
            {
                   list.RemoveAll(a => a.SortNum == machineTaskExcuting);
                
            }
            else
            {  
                var pokenum = list.Select(a => new { machineseq = a.Machineseq, pokenum = a.POKENUM }).FirstOrDefault();//获取当前任务抓烟数

                if (pokenum.pokenum == 10)//如果有一抓等于10  直接去掉
                {
                    list.RemoveAll(a => a.SortNum == machineTaskExcuting);

                }
                else  if (pokenum.pokenum > 10)//如果有一抓大于10 直接去掉一抓
                {
                    list.Find(x => x.Machineseq == pokenum.machineseq && x.SortNum == machineTaskExcuting).POKENUM -= 10;// 减去一抓
                } 
                else if (pokenum.pokenum < 10)//当第一个任务pokenum小于10 就和下一个任务的pokenum相加 直到大于等于10
                {
                    decimal[] Listmachineseq = new decimal[10];//存放品牌 和 对应品牌的抓取数小于10得品牌   --- //存放pokeid
                    decimal sum = 0;//和 
                    decimal lastNum = 0;//上一次
                    decimal z = 0;//
                    
                    for (int i = 0; i <= Listmachineseq.Length; i++)//0 1 2
                    {
                         sum += list[i].POKENUM;//4 + 4 = 8 + 5 =13
                         if (sum == 10)
                         {
                             Listmachineseq[i] = list[i].POKEID;
                             break;
                         }
                         else if (sum > 10)//true
                         {
                             list.RemoveAll(a => a.SortNum == machineTaskExcuting);
                             list.Find(x => x.POKEID ==  list[i].POKEID ).POKENUM -= (list[i].POKENUM - (10 - lastNum)); //对相加大于10最后一个pokenum的值 所取的数量 相减
                             break;
                         }
                         lastNum = sum;//4 8
                         Listmachineseq[i] = list[i].POKEID;//存入当前pokeid 0 1
                    }
               

                    //while (sum == 10)
                    //{
                    //    sum = list[index].POKENUM + list[index + 1].POKENUM;
                    //    index++;
                    //}
               
                }  
            } 
            return list;
            //    //先算合流机械手正在执行的任务有多少条 ( 有几抓  >  machinePokeNum * 10   ) % 10 

            //    //再算第几抓   
            //    //几种情况:
            //    //  抓烟数量 小于十   去掉当前条数  
            //    // 

        }

    }
}
