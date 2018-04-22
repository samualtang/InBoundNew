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
    }
}
