

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Model;
using System.Transactions;
using InBound;


namespace InBound.Business
{
    public class TaskService
    {
        //1412,1409,1406,1403,1400,1397,1394,139拆垛位置
        //1415 人工拆垛

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
        #region 机械手任务查询
        /// <summary>
        /// 查询机械手所有任务
        /// </summary>
        /// <param name="machineseq"></param>
        /// <returns></returns>
        public static List<TaskDetail> getAllMachineTask(decimal machineseq)
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_PRODUCE_POKE 
                            join item2 in dataentity.T_PRODUCE_SORTTROUGH
                            on item.TROUGHNUM equals item2.TROUGHNUM
                            where item.MACHINESEQ == machineseq orderby item.UNIONTASKNUM
                            select new TaskDetail() { TaskNum = item.TASKNUM ?? 0, SortNum = item.SORTNUM ?? 0, POCKPLACE = item.POKEPLACE ?? 0, POKENUM = item.POKENUM ?? 0, CIGARETTDECODE = item2.CIGARETTECODE, CIGARETTDENAME = item2.CIGARETTENAME, Machineseq = item.MACHINESEQ ?? 0, UnionTasknum = item.UNIONTASKNUM ?? 0, tNum = item.MERAGENUM ?? 0, Billcode = item.BILLCODE, MachineState = item.MACHINESTATE ?? 0 };
                if (query != null)
                    return query.Distinct().OrderBy(x => x.UnionTasknum).ToList();
                else return null;
             
            }
        }
        /// <summary>
        /// 查询机械手任务
        /// </summary>
        /// <param name="machineseq">机械手号</param>
        /// <param name="uniontaskNum">合流任务号</param>
        /// <returns></returns>
        public static List<TaskDetail> getMachineTask(decimal machineseq,decimal uniontaskNum)
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_PRODUCE_POKE
                            join item2 in dataentity.T_PRODUCE_SORTTROUGH
                            on item.TROUGHNUM equals item2.TROUGHNUM
                            where item.MACHINESEQ == machineseq && item.UNIONTASKNUM==uniontaskNum
                            select new TaskDetail() { TaskNum = item.TASKNUM ?? 0, SortNum = item.SORTNUM ?? 0,POCKPLACE =item.POKEPLACE ??0, POKENUM = item.POKENUM ?? 0, CIGARETTDECODE = item2.CIGARETTECODE, CIGARETTDENAME = item2.CIGARETTENAME, Machineseq = item.MACHINESEQ ?? 0, UnionTasknum = item.UNIONTASKNUM ?? 0, tNum = item.MERAGENUM ?? 0, Billcode = item.BILLCODE, MachineState = item.MACHINESTATE ?? 0 };
                if (query != null)
                    return query.Distinct().OrderBy(x => x.UnionTasknum).ToList();
                else return null;

            }
        }
        /// <summary>
        /// 查询机械手任务
        /// </summary>
        /// <param name="sortnum">分拣任务号</param>
        /// <returns></returns>
        public static List<TaskDetail> getMachineTask(int sortnum )
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_PRODUCE_POKE
                            join item2 in dataentity.T_PRODUCE_SORTTROUGH
                            on item.TROUGHNUM equals item2.TROUGHNUM
                            where item.SORTNUM == sortnum
                            select new TaskDetail() { TaskNum = item.TASKNUM ?? 0, SortNum = item.SORTNUM ?? 0, POCKPLACE = item.POKEPLACE ?? 0, POKENUM = item.POKENUM ?? 0, CIGARETTDECODE = item2.CIGARETTECODE, CIGARETTDENAME = item2.CIGARETTENAME, Machineseq = item.MACHINESEQ ?? 0, UnionTasknum = item.UNIONTASKNUM ?? 0, tNum = item.MERAGENUM ?? 0, Billcode = item.BILLCODE, MachineState = item.MACHINESTATE ?? 0 };
                if (query != null)
                    return query.Distinct().OrderBy(x => x.UnionTasknum).ToList();
                else return null;

            }
        }

      
        /// <summary>
        /// 分拣任务号 合流任务号 查询机械手
        /// </summary>
        /// <param name="sortnum">分拣任务号</param>
        /// <param name="machineseq">设备号</param>
        /// <param name="uniontaskNum">合流任务号</param>
        /// <returns></returns>
        public static List<TaskDetail> getMachineTask(decimal sortnum, decimal machineseq, decimal uniontaskNum)
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_PRODUCE_POKE
                            join item2 in dataentity.T_PRODUCE_SORTTROUGH
                            on item.TROUGHNUM equals item2.TROUGHNUM
                            where item.SORTNUM == sortnum && item.MACHINESEQ == machineseq && item.UNIONTASKNUM == uniontaskNum
                            select new TaskDetail() { TaskNum = item.TASKNUM ?? 0, SortNum = item.SORTNUM ?? 0, POCKPLACE = item.POKEPLACE ?? 0, POKENUM = item.POKENUM ?? 0, CIGARETTDECODE = item2.CIGARETTECODE, CIGARETTDENAME = item2.CIGARETTENAME, Machineseq = item.MACHINESEQ ?? 0, UnionTasknum = item.UNIONTASKNUM ?? 0, tNum = item.MERAGENUM ?? 0, Billcode = item.BILLCODE, MachineState = item.MACHINESTATE ?? 0 };
                if (query != null)
                    return query.Distinct().OrderBy(x => x.UnionTasknum).ToList();
                else return null;

            }
        } 
        #endregion

        public static void UpdateAll()
        {
            using (Entities dataentity = new Entities())
            {
                dataentity.ExecuteStoreCommand(" update t_produce_poke set pokeplace=0,meragenum=0,uniontasknum=0,SECSORTNUM=0,sortstate=10,machinestate=10,unionstate=10");
                dataentity.SaveChanges();
            }
        }
        public static void UpdateDataByGroupMainBelt(decimal groupNo, decimal mainbelt, decimal beginSortnum, decimal endSortnum, decimal sortstate,decimal updatestatus)
        {
            using (Entities dataentity = new Entities())
            {

                var query = (from item in dataentity.T_PRODUCE_POKE
                            join item2 in dataentity.T_PRODUCE_SORTTROUGH
                                on item.TROUGHNUM equals item2.TROUGHNUM
                            where item.GROUPNO == groupNo && item.MAINBELT == mainbelt && item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20 && item.SORTSTATE == sortstate
                            && item.SORTNUM >= beginSortnum && item.SORTNUM <= endSortnum
                            orderby item.SORTNUM
                            select item).ToList();;
                if (query != null)
                {
                    foreach (var item in query)
                    {
                        if (updatestatus == 10)
                        {
                            item.POKEPLACE = 0;
                            item.MACHINESTATE = 10;
                            item.UNIONSTATE = 10;
                            item.MERAGENUM = 0;
                            item.UNIONTASKNUM = 0;
                            item.SECSORTNUM = 0;
                        }

                        item.SORTSTATE = updatestatus;
                       
                    }
                    dataentity.SaveChanges();
                }
                
            }
        }



        public static List<TaskDetail> getFJDataByGroupMainBelt(decimal groupNo, decimal mainbelt,decimal beginSortnum,decimal endSortnum,decimal sortstate)
        {
            using (Entities dataentity = new Entities())
            {

                var query = from item in dataentity.T_PRODUCE_POKE
                            join item2 in dataentity.T_PRODUCE_SORTTROUGH
                                on item.TROUGHNUM equals item2.TROUGHNUM
                            where item.GROUPNO == groupNo && item.MAINBELT == mainbelt && item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20 && item.SORTSTATE == sortstate
                            && item.SORTNUM >= beginSortnum && item.SORTNUM <= endSortnum
                            orderby item.SORTNUM
                            select new TaskDetail() { GroupNO = item.GROUPNO ?? 0, TaskNum = item.TASKNUM ?? 0, UnionTasknum = item.UNIONTASKNUM ?? 0, POKENUM = item.POKENUM ?? 0, CIGARETTDECODE = item2.CIGARETTECODE, POCKPLACE = item.POKEPLACE ?? 0, CIGARETTDENAME = item2.CIGARETTENAME, Machineseq = item.MACHINESEQ ?? 0, SortNum = item.SORTNUM ?? 0, tNum = item.POKENUM ?? 0, Billcode = item.BILLCODE, SortState = item.SORTSTATE ?? 0, MainBelt=item.MAINBELT??0, meragenum=item.MERAGENUM??0 };
                if (query != null)
                    return query.OrderBy(x => x.SortNum).ToList();
                else return null;
            }
        }
        #region 预分拣查询
        public static List<TaskDetail> getFJDataAll(decimal groupNo1, decimal groupNo2)
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_PRODUCE_POKE join item2 in dataentity.T_PRODUCE_SORTTROUGH
                            on item.TROUGHNUM equals item2.TROUGHNUM
                            where   (item.GROUPNO == groupNo1 || item.GROUPNO == groupNo2) && item2.TROUGHTYPE==10 && item2.CIGARETTETYPE==20 orderby item.SORTNUM
                            select new TaskDetail() { GroupNO = item.GROUPNO ?? 0, TaskNum = item.TASKNUM ?? 0, UnionTasknum = item.UNIONTASKNUM ?? 0, POKENUM = item.POKENUM ?? 0, CIGARETTDECODE = item2.CIGARETTECODE, POCKPLACE = item.POKEPLACE ?? 0, CIGARETTDENAME = item2.CIGARETTENAME, Machineseq = item.MACHINESEQ ?? 0, SortNum = item.SORTNUM ?? 0, tNum = item.POKENUM ?? 0, Billcode = item.BILLCODE, SortState = item.SORTSTATE ?? 0 };
                if (query != null)
                    return query.OrderBy(x => x.SortNum).ToList();
                else return null;
            }
        }

        public static List<TaskDetail> getFJDataAll(decimal state,decimal groupNo1, decimal groupNo2)
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_PRODUCE_POKE
                            join item2 in dataentity.T_PRODUCE_SORTTROUGH
                                on item.TROUGHNUM equals item2.TROUGHNUM
                            where (item.GROUPNO == groupNo1 || item.GROUPNO == groupNo2) && item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20 && item.SORTSTATE == state 
                            orderby item.SORTNUM
                            select new TaskDetail() { GroupNO = item.GROUPNO ?? 0, TaskNum = item.TASKNUM ?? 0, UnionTasknum = item.UNIONTASKNUM ?? 0, POKENUM = item.POKENUM ?? 0, CIGARETTDECODE = item2.CIGARETTECODE, POCKPLACE = item.POKEPLACE ?? 0, CIGARETTDENAME = item2.CIGARETTENAME, Machineseq = item.MACHINESEQ ?? 0, SortNum = item.SORTNUM ?? 0, tNum = item.POKENUM ?? 0, Billcode = item.BILLCODE, SortState = item.SORTSTATE ?? 0 };
                if (query != null)
                    return query.OrderBy(x => x.SortNum).ToList();
                else return null;
            }
        }
        /// <summary>
        /// 排序号获取预分拣任务
        /// </summary>
        /// <param name="sortnum">任务号</param>
        /// <param name="groupNo1">A组</param>
        /// <param name="groupNo2">B组</param>
        /// <returns></returns>
        public static List<TaskDetail> getFJData(decimal sortnum, decimal groupNo1, decimal groupNo2)
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_PRODUCE_POKE
                            join item2 in dataentity.T_PRODUCE_SORTTROUGH
                            on item.TROUGHNUM equals item2.TROUGHNUM
                            where item.SORTNUM == sortnum && (item.GROUPNO == groupNo1 || item.GROUPNO == groupNo2) && item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20
                            orderby item.SORTNUM
                            select new TaskDetail() { GroupNO = item.GROUPNO ?? 0, TaskNum = item.TASKNUM ?? 0, UnionTasknum = item.UNIONTASKNUM ?? 0, POKENUM = item.POKENUM ?? 0, CIGARETTDECODE = item2.CIGARETTECODE, POCKPLACE = item.POKEPLACE ?? 0, CIGARETTDENAME = item2.CIGARETTENAME, Machineseq = item.MACHINESEQ ?? 0, SortNum = item.SORTNUM ?? 0, tNum = item.POKENUM ?? 0, Billcode = item.BILLCODE, SortState = item.SORTSTATE ?? 0 };
                if (query != null)
                    return query.OrderBy(x => x.SortNum).ToList();
                else return null;
            }
        }
        /// <summary>
        /// 任务号获取分拣任务
        /// </summary>
        /// <param name="tasknum">任务号</param>
        /// <param name="groupNo1">A组</param>
        /// <param name="groupNo2">B组</param>
        /// <returns></returns>
        public static List<TaskDetail> getFJDataByTasknum(decimal tasknum, decimal groupNo1, decimal groupNo2)
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_PRODUCE_POKE
                            join item2 in dataentity.T_PRODUCE_SORTTROUGH
                            on item.TROUGHNUM equals item2.TROUGHNUM
                            where item.TASKNUM == tasknum && (item.GROUPNO == groupNo1 || item.GROUPNO == groupNo2) && item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20
                            orderby item.SORTNUM
                            select new TaskDetail() { GroupNO = item.GROUPNO ?? 0, TaskNum = item.TASKNUM ?? 0, UnionTasknum = item.UNIONTASKNUM ?? 0, POKENUM = item.POKENUM ?? 0, CIGARETTDECODE = item2.CIGARETTECODE, POCKPLACE = item.POKEPLACE ?? 0, CIGARETTDENAME = item2.CIGARETTENAME, Machineseq = item.MACHINESEQ ?? 0, SortNum = item.SORTNUM ?? 0, tNum = item.POKENUM ?? 0, Billcode = item.BILLCODE, SortState = item.SORTSTATE ?? 0 };
                if (query != null)
                    return query.OrderBy(x => x.SortNum).ToList();
                else return null;
            }
        }
        
        
        /// <summary>
        /// 机械手号获取分拣任务
        /// </summary>
        /// <param name="machineseq">机械手号</param>
        /// <param name="groupNo1">A组</param>
        /// <param name="groupNo2">B组</param>
        /// <returns></returns>
        public static List<TaskDetail> getFJData(int machineseq, decimal groupNo1, decimal groupNo2)
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_PRODUCE_POKE
                            join item2 in dataentity.T_PRODUCE_SORTTROUGH
                            on item.TROUGHNUM equals item2.TROUGHNUM
                            where item.MACHINESEQ == machineseq && (item.GROUPNO == groupNo1 || item.GROUPNO == groupNo2) && item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20
                            orderby item.SORTNUM
                            select new TaskDetail() { GroupNO = item.GROUPNO ?? 0, TaskNum = item.TASKNUM ?? 0, UnionTasknum = item.UNIONTASKNUM ?? 0, POKENUM = item.POKENUM ?? 0, CIGARETTDECODE = item2.CIGARETTECODE, POCKPLACE = item.POKEPLACE ?? 0, CIGARETTDENAME = item2.CIGARETTENAME, Machineseq = item.MACHINESEQ ?? 0, SortNum = item.SORTNUM ?? 0, tNum = item.POKENUM ?? 0, Billcode = item.BILLCODE, SortState = item.SORTSTATE ?? 0 };
                if (query != null)
                    return query.OrderBy(x => x.SortNum).ToList();
                else return null;
            }
        }

        /// <summary>
        /// 任务号和机械手号获取分拣任务
        /// </summary>
        /// <param name="machineseq">机械手号</param>
        /// <param name="sortnum">任务号</param>
        /// <param name="groupNo1">A组</param>
        /// <param name="groupNo2">B组</param>
        /// <returns></returns>
        public static List<TaskDetail> getFJData(int machineseq,decimal sortnum ,decimal groupNo1, decimal groupNo2)
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_PRODUCE_POKE
                            join item2 in dataentity.T_PRODUCE_SORTTROUGH
                            on item.TROUGHNUM equals item2.TROUGHNUM
                            where item.MACHINESEQ == machineseq && item.SORTNUM == sortnum && (item.GROUPNO == groupNo1 || item.GROUPNO == groupNo2) && item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20
                            orderby item.SORTNUM
                            select new TaskDetail() { GroupNO = item.GROUPNO ??0, TaskNum = item.TASKNUM ?? 0, UnionTasknum = item.UNIONTASKNUM ?? 0, POKENUM = item.POKENUM ?? 0, CIGARETTDECODE = item2.CIGARETTECODE, POCKPLACE = item.POKEPLACE ?? 0, CIGARETTDENAME = item2.CIGARETTENAME, Machineseq = item.MACHINESEQ ?? 0, SortNum = item.SORTNUM ?? 0, tNum = item.POKENUM ?? 0, Billcode = item.BILLCODE, SortState = item.SORTSTATE ?? 0 };
                if (query != null)
                    return query.OrderBy(x => x.SortNum).ToList();
                else return null;
            }
        }
        #endregion

        #region  合流任务
        /// <summary>
        /// 获取合流任务
        /// </summary>
        /// <returns></returns>
        public static List<TaskDetail> getUnionDataAll()
        { 
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_PRODUCE_POKE
                             orderby item.SORTNUM
                            group item by new { item.BILLCODE, item.SORTNUM, item.UNIONSTATE ,item.MAINBELT,item.PACKAGEMACHINE,item.GROUPNO } into g
                            select new TaskDetail() { 
                                SortNum = g.Key.SORTNUM ?? 0,
                                tNum = g.Sum(x => x.POKENUM ?? 0), 
                                Billcode = g.Key.BILLCODE, 
                                UnionState = g.Key.UNIONSTATE ?? 0 ,
                                MainBelt = g.Key.MAINBELT ?? 0,
                                PACKAGEMACHINE = g.Key.PACKAGEMACHINE ?? 0, 
                                GroupNO = g.Key.GROUPNO ??0
                            };
                if (query != null)
                    return query.OrderBy(x => x.SortNum).ToList();
                else return null;
            }
        }
        /// <summary>
        /// 获取合流任务
        /// </summary>
        /// <param name="sortnum">任务号</param>
        /// <returns></returns>
        public static List<TaskDetail> getUnionData(decimal sortnum)
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_PRODUCE_POKE
                            where item.SORTNUM == sortnum  
                            orderby item.SORTNUM
                            group item by new { item.BILLCODE, item.SORTNUM, item.UNIONSTATE,  item.MAINBELT, item.PACKAGEMACHINE, item.GROUPNO } into g
                            select new TaskDetail()
                            {
                                SortNum = g.Key.SORTNUM ?? 0,
                                tNum = g.Sum(x => x.POKENUM ?? 0),
                                Billcode = g.Key.BILLCODE,
                                UnionState = g.Key.UNIONSTATE ?? 0,
                                MainBelt = g.Key.MAINBELT ?? 0,
                                PACKAGEMACHINE = g.Key.PACKAGEMACHINE ?? 0,
                                GroupNO = g.Key.GROUPNO ?? 0
                            };
                if (query != null)
                    return query.OrderBy(x => x.SortNum).ToList();
                else return null;
            }
        }

       /// <summary>
       /// 获取合流任务
       /// </summary>
       /// <param name="groupno">机械手号</param>
       /// <returns></returns>
        public static List<TaskDetail> getUnionData(int groupno)
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_PRODUCE_POKE
                            where item.GROUPNO == groupno
                            orderby item.SORTNUM
                            group item by new { item.BILLCODE, item.SORTNUM, item.UNIONSTATE,  item.MAINBELT, item.PACKAGEMACHINE, item.GROUPNO} into g
                            select new TaskDetail()
                            {
                                SortNum = g.Key.SORTNUM ?? 0,
                                tNum = g.Sum(x => x.POKENUM ?? 0),
                               
                                Billcode = g.Key.BILLCODE,
                                UnionState = g.Key.UNIONSTATE ?? 0,
                               
                                MainBelt = g.Key.MAINBELT ?? 0,
                                PACKAGEMACHINE = g.Key.PACKAGEMACHINE ?? 0,
                                GroupNO = g.Key.GROUPNO ?? 0
                            };
                if (query != null)
                    return query.OrderBy(x => x.SortNum).ToList();
                else return null;
            }
        } 

       /// <summary>
       /// 获取合流
       /// </summary>
       /// <param name="sortnum">任务号</param>
       /// <param name="machineseq">机械手号</param>
       /// <returns></returns>
        public static List<TaskDetail> getUnionData(decimal sortnum, decimal groupno)
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_PRODUCE_POKE
                            where item.SORTNUM == sortnum && item.GROUPNO == groupno
                            orderby item.SORTNUM
                            group item by new { item.BILLCODE, item.SORTNUM, item.UNIONSTATE,  item.MAINBELT, item.PACKAGEMACHINE, item.GROUPNO } into g
                            select new TaskDetail()
                            {
                                SortNum = g.Key.SORTNUM ?? 0,
                                tNum = g.Sum(x => x.POKENUM ?? 0),
                                
                                Billcode = g.Key.BILLCODE,
                                UnionState = g.Key.UNIONSTATE ?? 0,
                               
                                MainBelt = g.Key.MAINBELT ?? 0,
                                PACKAGEMACHINE = g.Key.PACKAGEMACHINE ?? 0,
                                GroupNO = g.Key.GROUPNO ?? 0
                            };
                if (query != null)
                    return query.OrderBy(x => x.SortNum  ).ToList();
                else return null;
            }
        }
        #endregion

        /// <summary>
        /// 查询预分拣任务号到预分拣任务号的区间
        /// </summary>
        /// <param name="SortTaskNumFrom">起始预分拣任务号</param>
        /// <param name="SortTaskNumTo">结束预分拣任务号</param>
        /// <returns></returns>
        public static List<T_PRODUCE_POKE> GetListBySortTaskNumToSortTaskNum(decimal SortTaskNumFrom, decimal SortTaskNumTo)
        {
            using (Entities data = new Entities())
            {
                var query = (from items in data.T_PRODUCE_POKE where items.SORTNUM >= SortTaskNumFrom && items.SORTNUM <= SortTaskNumTo select items).ToList();

                return query;
            }
        }
        public static void updateTask(decimal fromtasknum, decimal totasknum, decimal state)
        {
            
        }
        public static void updateTask2(decimal fromtasknum, decimal totasknum, decimal state)
        {
            //using (Entities dataentity = new Entities())
            //{
            //    var query = (from item in dataentity.T_PRODUCE_TASK where item.TASKNUM >= fromtasknum && item.TASKNUM <= totasknum select item).ToList();
            //    if (query != null && query.Count > 0)
            //    {
            //        query.ForEach(x => x.GROUP2STATE = state);
            //        dataentity.SaveChanges();
            //    }
            //}
        }
        public static void updateTask3(decimal fromtasknum, decimal totasknum, decimal state)
        {
            //using (Entities dataentity = new Entities())
            //{
            //    var query = (from item in dataentity.T_PRODUCE_TASK where item.TASKNUM >= fromtasknum && item.TASKNUM <= totasknum select item).ToList();
            //    if (query != null && query.Count > 0)
            //    {
            //        query.ForEach(x => x.GROUP3STATE = state);
            //        dataentity.SaveChanges();
            //    }
            //}
        }
        public static void updateTask4(decimal fromtasknum, decimal totasknum, decimal state)
        {
            //using (Entities dataentity = new Entities())
            //{
            //    var query = (from item in dataentity.T_PRODUCE_TASK where item.TASKNUM >= fromtasknum && item.TASKNUM <= totasknum select item).ToList();
            //    if (query != null && query.Count > 0)
            //    {
            //        query.ForEach(x => x.GROUP4STATE = state);
            //        dataentity.SaveChanges();
            //    }
            //}
        }
        public static void updateTask5(decimal fromtasknum, decimal totasknum, decimal state)
        {
            //using (Entities dataentity = new Entities())
            //{
            //    var query = (from item in dataentity.T_PRODUCE_TASK where item.TASKNUM >= fromtasknum && item.TASKNUM <= totasknum select item).ToList();
            //    if (query != null && query.Count > 0)
            //    {
            //        query.ForEach(x => x.GROUP5STATE = state);
            //        dataentity.SaveChanges();
            //    }
            //}
        }
        public static void updateTask6(decimal fromtasknum, decimal totasknum, decimal state)
        {
            //using (Entities dataentity = new Entities())
            //{
            //    var query = (from item in dataentity.T_PRODUCE_TASK where item.TASKNUM >= fromtasknum && item.TASKNUM <= totasknum select item).ToList();
            //    if (query != null && query.Count > 0)
            //    {
            //        query.ForEach(x => x.GROUP6STATE = state);
            //        dataentity.SaveChanges();
            //    }
            //}
        }
        public static void updateTask7(decimal fromtasknum, decimal totasknum, decimal state)
        {
            //using (Entities dataentity = new Entities())
            //{
            //    var query = (from item in dataentity.T_PRODUCE_TASK where item.TASKNUM >= fromtasknum && item.TASKNUM <= totasknum select item).ToList();
            //    if (query != null && query.Count > 0)
            //    {
            //        query.ForEach(x => x.GROUP7STATE = state);
            //        dataentity.SaveChanges();
            //    }
            //}
        }
        public static void updateTask8(decimal fromtasknum, decimal totasknum, decimal state)
        {
            //using (Entities dataentity = new Entities())
            //{
            //    var query = (from item in dataentity.T_PRODUCE_TASK where item.TASKNUM >= fromtasknum && item.TASKNUM <= totasknum select item).ToList();
            //    if (query != null && query.Count > 0)
            //    {
            //        query.ForEach(x => x.GROUP8STATE = state);
            //        dataentity.SaveChanges();
            //    }
            //}
        }
        public static List<T_UN_POKE> GetUNTaskInfo()
        {
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_UN_POKE
                             select  item  ).ToList();
                return query;
            }
           
        }
        public static List<TaskInfo> GetUNCustomer()
        {
            using (Entities entity = new Entities())
            {
             //总量
                var query = (from item in entity.T_UN_TASK
                             orderby item.SORTNUM
                             group item by new
                             {
                                 item.REGIONCODE,
                                 item.LINENUM,
                                 item.ORDERDATE,
                                 item.SYNSEQ
                                 
                             } into g
                             select new TaskInfo() { REGIONCODE = g.Key.REGIONCODE,SYNSEQ = g.Key.SYNSEQ ??0, LineNum = g.Key.LINENUM, ORDERDATE = g.Key.ORDERDATE, FinishCount = 0, FinishQTY = 0, QTY = g.Sum(t=> t.TASKQUANTITY ) ?? 0, Count = g.Count(t => t.REGIONCODE == g.Key.REGIONCODE) }).ToList();
                //完成 
                var query2 = (from item in entity.T_UN_TASK
                              where item.STATE == "30"
                              group item by new { item.REGIONCODE } into g
                              select new TaskInfo()
                              {
                                  REGIONCODE = g.Key.REGIONCODE,
                                  FinishCount = g.Count(t => t.REGIONCODE == g.Key.REGIONCODE),
                                  FinishQTY = g.Sum(t => t.TASKQUANTITY ?? 0)
                              }).ToList();


                #region
                //var query = (from item in entity.T_UN_TASK
                //             group item by new
                //             {
                //                 item.REGIONCODE,
                //                 item.LINENUM,
                //                 item.ORDERDATE
                //             } into g
                //             select new TaskInfo() { REGIONCODE = g.Key.REGIONCODE, ORDERDATE = g.Key.ORDERDATE, LineNum = g.Key.LINENUM, FinishCount = 0, FinishQTY = 0, QTY = 0, Count = g.Count(t => t.REGIONCODE == g.Key.REGIONCODE) }).ToList();

                //var query2 = (from item in entity.T_UN_TASKLINE
                //              join item2 in entity.T_UN_TASK
                //              on item.TASKNUM equals item2.TASKNUM
                //              group item by new { item2.REGIONCODE, item2.LINENUM } into g
                //              select new TaskInfo() { REGIONCODE = g.Key.REGIONCODE, LineNum = g.Key.LINENUM, QTY = g.Sum(t => t.QUANTITY) ?? 0 }).ToList(); 
                //总量
                //var query2 = from item in entity.T_UN_POKE

                //              orderby item.SORTNUM 
                //               join item2 in entity.T_UN_TASK
                //               on item.TASKNUM equals item2.TASKNUM
                //               group item by  new {
                //                 item2.REGIONCODE,
                //                item2.LINENUM,
                //                item2.ORDERDATE
                //              } into g
                //              select new TaskInfo () { REGIONCODE =  g.Key.REGIONCODE,LineNum = g.Key.LINENUM,QTY
                //var query3 = (from item in entity.T_UN_TASK
                //              where item.STATE == "30"
                //              group item by new { item.REGIONCODE, item.LINENUM } into g
                //              select new TaskInfo() { REGIONCODE = g.Key.REGIONCODE, LineNum = g.Key.LINENUM, FinishCount = g.Count(t => t.REGIONCODE == g.Key.REGIONCODE) }).ToList();
                //var query4 = (from item in entity.T_UN_TASKLINE
                //              join item2 in entity.T_UN_TASK
                //              on item.TASKNUM equals item2.TASKNUM
                //              where item2.STATE == "30"
                //              group item by new { item2.REGIONCODE, item2.LINENUM } into g
                //              select new TaskInfo() { REGIONCODE = g.Key.REGIONCODE, LineNum = g.Key.LINENUM, FinishQTY = g.Sum(t => t.QUANTITY) ?? 0 }).ToList();
                #endregion
                UnionList(query, query2, 2);
                //UnionList(query, query3, 2);
                //UnionList(query, query4, 3);
                CaldList(query);
                return query;
            }
        }
        public static List<TaskInfo> GetUnCutomer(decimal sortnum)
        {
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_UN_POKE
                             where item.SORTNUM == sortnum
                             orderby item.SORTNUM
                             select new TaskInfo() { MACHINESEQ = item.MACHINESEQ ?? 0, POKENUM = item.POKENUM ??0}).ToList();
                return null;
            }

        }
        public static List<TaskInfo> GetCustomer()
        {
            using (Entities entity = new Entities())
            { 
                var query = (from item in entity.T_PRODUCE_TASK//总任务数
                             group item by new { item.REGIONCODE , item.SYNSEQ,item.ORDERDATE,item.MAINBELT } into g
                             select new TaskInfo() { REGIONCODE = g.Key.REGIONCODE,MIANBELT = g.Key.MAINBELT, ORDERDATE = g.Key.ORDERDATE , FinishCount = 0, FinishQTY = 0, SYNSEQ = g.Key.SYNSEQ ?? 0, QTY = g.Sum(t => t.TASKQUANTITY) ?? 0, Count = g.Count(t => t.REGIONCODE == g.Key.REGIONCODE) }).ToList();
                //var query2 = (from item in entity.T_PRODUCE_TASKLINE
                //              join item2 in entity.T_PRODUCE_TASK//总量
                //              on item.TASKNUM equals item2.TASKNUM
                //              group item by new { item2.REGIONCODE } into g
                //              select new TaskInfo() { REGIONCODE = g.Key.REGIONCODE, QTY = g.Sum(t => t.QUANTITY) ?? 0 }).ToList();
                var query2 = (from item in entity.T_PRODUCE_TASK//完成任务数
                              where item.STATE == "30"
                              group item by new { item.REGIONCODE } into g
                              select new TaskInfo() { REGIONCODE = g.Key.REGIONCODE, FinishCount = g.Count(t => t.REGIONCODE == g.Key.REGIONCODE),
                                  FinishQTY = g.Sum(t => t.TASKQUANTITY) ?? 0  }).ToList();
                //var query4 = (from item in entity.T_PRODUCE_TASKLINE//完成量
                //              join item2 in entity.T_PRODUCE_TASK
                //              on item.TASKNUM equals item2.TASKNUM
                //              where item2.STATE == "30"
                //              group item by new { item2.REGIONCODE } into g
                //              select new TaskInfo() { REGIONCODE = g.Key.REGIONCODE, FinishQTY = g.Sum(t => t.QUANTITY) ?? 0 }).ToList();
                //UnionList(query, query2, 1);
                UnionList(query, query2, 2);
                //UnionList(query, query4, 3);
                CaldList(query);
                return query;
            }
        }
        /// <summary>
        /// 预分拣界面数据
        /// </summary>
        /// <returns></returns>
        public static List<TaskInfo> GetSortingCustomer(int groupno1, int groupno2)
        {
 
            using (Entities entity = new Entities())
            {
                //总量
                var query =(from item in entity.T_PRODUCE_POKE
                            where item.GROUPNO == groupno1 || item.GROUPNO == groupno2
                            group item by new { item.GROUPNO, item.PACKAGEMACHINE, item.MAINBELT } into g
                            orderby g.Key.GROUPNO, g.Key.PACKAGEMACHINE
                            select new TaskInfo() { GROUPNO = g.Key.GROUPNO ?? 0, PACKAGEMACHINE = g.Key.PACKAGEMACHINE ?? 0,FinishCount = 0,FinishQTY =0,Count=g.Sum(c=>c.POKENUM ?? 0), MIANBELT = g.Key.MAINBELT, UNIONTASKNUM = g.Select(x => new { UNIONTASKNUM = x.UNIONTASKNUM }).Count() }).ToList();
                //完成
                var query2 = (from item in entity.T_PRODUCE_POKE
                             where (item.GROUPNO == groupno1 || item.GROUPNO == groupno2) && item.SORTSTATE == 20
                             group item by new { item.GROUPNO, item.PACKAGEMACHINE, item.MAINBELT } into g
                             orderby g.Key.GROUPNO, g.Key.PACKAGEMACHINE
                             select new TaskInfo() { GROUPNO = g.Key.GROUPNO ?? 0, PACKAGEMACHINE = g.Key.PACKAGEMACHINE ?? 0, FinishCount  = g.Sum(c => c.POKENUM ?? 0), MIANBELT = g.Key.MAINBELT, FinishQTY = g.Select(x => new { UNIONTASKNUM = x.UNIONTASKNUM }).Count() }).ToList();

               UnionList(query, query2, 5);
              
                CaldList(query);
                return query;
            }
        }




        /// <summary>
        /// 用于机械手发送任务刷新
        /// </summary>
        /// <returns></returns>
        public static List<TaskInfo> GetMachineProgramress(int  groupno1, int groupno2)
        { 
            using (Entities entity = new Entities())
            {
                //总量
                var query1 = (from item in entity.T_PRODUCE_POKE
                              where item.GROUPNO == groupno1 || item.GROUPNO == groupno2
                              group item by new { item.MACHINESEQ, item.GROUPNO ,item.TROUGHNUM} into g
                              orderby g.Key.MACHINESEQ
                              select new TaskInfo() { GROUPNO = g.Key.GROUPNO ?? 0, TROUGHNUM = g.Key.TROUGHNUM, MACHINESEQ = g.Key.MACHINESEQ ?? 0, FinishCount = 0, FinishQTY = 0, Count = g.Sum(c => c.POKENUM ?? 0), UNIONTASKNUM = g.Select(x => new { UNIONTASKNUM = x.UNIONTASKNUM }).Count() }).ToList();
                 //完成
                var query2 = (from item in entity.T_PRODUCE_POKE
                              where (item.GROUPNO == groupno1 || item.GROUPNO == groupno2) && item.MACHINESTATE == 20
                              orderby item.MACHINESEQ
                              group item by new { item.MACHINESEQ, item.GROUPNO } into g
                              select new TaskInfo() { GROUPNO = g.Key.GROUPNO ?? 0, MACHINESEQ = g.Key.MACHINESEQ ?? 0, FinishCount = g.Sum(a => a.POKENUM ?? 0), FinishQTY = g.Select(x => new { UNIONTASKNUM = x.UNIONTASKNUM }).Count() }).ToList(); // g.Select(t => new { pokenum = t.POKENUM }).Count()

                

                if (query1 != null)
                {
                    UnionList(query1, query2, 4);
                    CaldList(query1);
                    return query1 ; 
                }
                else { return null; }
            }
        }
        public static List<KeyValuePair<int, int>> initTask1()
        {
            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
            //using (Entities entity = new Entities())
            //{
            //    var query = (from item in entity.T_PRODUCE_TASK where item.GROUP1STATE == 20 orderby item.TASKNUM select item).ToList();
            //    if (query != null && query.Count > 0)
            //    {
            //        foreach (var item in query)
            //        {


            //            list.Add(new KeyValuePair<int, int>((int)item.SORTMACHINE,(int)item.TASKNUM ));
            //        }
            //    }
            //}
            return list;
        }
        public static List<KeyValuePair<int, int>> initTask2()
        {
            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
            //using (Entities entity = new Entities())
            // {
            //     var query = (from item in entity.T_PRODUCE_TASK where item.GROUP2STATE == 20 orderby item.TASKNUM select item).ToList();
            //     if (query != null && query.Count > 0)
            //     {
            //         foreach (var item in query)
            //         {


            //             list.Add(new KeyValuePair<int, int>((int)item.SORTMACHINE, (int)item.TASKNUM));
            //         }
            //     }
            // }
            return list;
        }
        public static List<KeyValuePair<int, int>> initTask3()
        {
            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
            //using (Entities entity = new Entities())
            //{
            //    var query = (from item in entity.T_PRODUCE_TASK where item.GROUP3STATE == 20 orderby item.TASKNUM select item).ToList();
            //    if (query != null && query.Count > 0)
            //    {
            //        foreach (var item in query)
            //        {


            //            list.Add(new KeyValuePair<int, int>((int)item.SORTMACHINE, (int)item.TASKNUM));
            //        }
            //    }
            //}
            return list;
        }
        public static List<KeyValuePair<int, int>> initTask4()
        {
            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
            //using (Entities entity = new Entities())
            //{
            //    var query = (from item in entity.T_PRODUCE_TASK where item.GROUP4STATE == 20 orderby item.TASKNUM select item).ToList();
            //    if (query != null && query.Count > 0)
            //    {
            //        foreach (var item in query)
            //        {


            //            list.Add(new KeyValuePair<int, int>((int)item.SORTMACHINE, (int)item.TASKNUM));
            //        }
            //    }
            //}
            return list;
        }
        public static List<KeyValuePair<int, int>> initTask5()
        {
            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
            //using (Entities entity = new Entities())
            //{
            //    var query = (from item in entity.T_PRODUCE_TASK where item.GROUP5STATE == 20 orderby item.TASKNUM select item).ToList();
            //    if (query != null && query.Count > 0)
            //    {
            //        foreach (var item in query)
            //        {


            //            list.Add(new KeyValuePair<int, int>((int)item.SORTMACHINE, (int)item.TASKNUM));
            //        }
            //    }
            //}
            return list;
        }
        public static List<KeyValuePair<int, int>> initTask6()
        {
            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
            //using (Entities entity = new Entities())
            //{
            //    var query = (from item in entity.T_PRODUCE_TASK where item.GROUP6STATE == 20 orderby item.TASKNUM select item).ToList();
            //    if (query != null && query.Count > 0)
            //    {
            //        foreach (var item in query)
            //        {
            //            list.Add(new KeyValuePair<int, int>((int)item.SORTMACHINE, (int)item.TASKNUM));
            //        }
            //    }
            //}
            return list;
        }
        public static List<KeyValuePair<int, int>> initTask7()
        {
            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
            using (Entities entity = new Entities())
                //{
                //    var query = (from item in entity.T_PRODUCE_TASK where item.GROUP7STATE == 20 orderby item.TASKNUM select item).ToList();
                //    if (query != null && query.Count > 0)
                //    {
                //        foreach (var item in query)
                //        {


                //            list.Add(new KeyValuePair<int, int>((int)item.SORTMACHINE, (int)item.TASKNUM));
                //        }
                //    }
                //}
                return list;
        }
        public static List<KeyValuePair<int, int>> initTask8()
        {
            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
            //using (Entities entity = new Entities())
            //{
            //    var query = (from item in entity.T_PRODUCE_TASK where item.GROUP8STATE == 20 orderby item.TASKNUM select item).ToList();
            //    if (query != null && query.Count > 0)
            //    {
            //        foreach (var item in query)
            //        {


            //            list.Add(new KeyValuePair<int, int>((int)item.SORTMACHINE, (int)item.TASKNUM));
            //        }
            //    }
            //}
            return list;
        }
        public static List<KeyValuePair<int, int>> initunionTask()
        {
            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
            //using (Entities entity = new Entities())
            //{
            //    var query = (from item in entity.T_PRODUCE_TASK where item.UNIONSTATE == 20 orderby item.TASKNUM select item).ToList();
            //    if (query != null && query.Count > 0)
            //    {
            //        foreach (var item in query)
            //        {


            //            list.Add(new KeyValuePair<int, int>((int)(item.PACKAGEMACHINE??0), (int)item.TASKNUM));
            //        }
            //    }
            //}
            return list;
        }
        public static void CaldList(List<TaskInfo> info)
        {
            if (info != null)
            {
                foreach (var item in info)
                {
                    //item.Rate = Math.Round((item.FinishCount / item.QTY) * 100, 2) + "%"; 
                    item.Rate = Math.Round((item.FinishCount / item.Count) * 100, 2) + "%";
                }
            }
        }
        public static void UnionList(List<TaskInfo> info, List<TaskInfo> info2, int type)
        {
             
            if (info2 == null || info2.Count == 0)
                return;
            else
            {
                if (type == 4)//机械手
                { 
                    foreach (var item in info2)
                    {
                        var entity = info.Find(s => s.MACHINESEQ == item.MACHINESEQ); 
                        entity.FinishCount = item.FinishCount;
                        entity.FinishQTY = item.FinishQTY;
                    }
                }
                else if (type == 5)//预分拣
                {
                    foreach (var item in info2)
                    {
                        var entity = info.Find(s => s.GROUPNO == item.GROUPNO && s.PACKAGEMACHINE  == item.PACKAGEMACHINE);
                        entity.FinishCount = item.FinishCount;
                        entity.FinishQTY = item.FinishQTY;
                    }
                }
                else
                {
                    foreach (var item in info2)
                    {

                        var entity = info.Find(s => s.REGIONCODE == item.REGIONCODE);

                        if (entity != null)
                        {
                            if (type == 1)
                            {
                                entity.QTY = item.QTY;
                            }
                            else if (type == 2)
                            {

                                entity.FinishCount = item.FinishCount;
                                entity.FinishQTY = item.FinishQTY;
                            }
                            else
                            {
                                entity.FinishQTY = item.FinishQTY;
                            }
                        }
                    }
                }

            }

        }


        public static void TestDingDan()
        {
            //decimal troughtype = 10;
            //decimal cigarettetype = 20;
            //using (Entities entity = new Entities())
            //{
            //    var query = (from item in entity.T_PRODUCE_TASK where item.STATE == "10" && item.GROUP1STATE == 10 orderby item.TASKNUM select item).FirstOrDefault();


            //    if (query != null)
            //    {
            //        var query2 = (from item in entity.T_PRODUCE_TASKLINE
            //                      join item2 in entity.T_PRODUCE_TASK on item.TASKNUM equals item2.TASKNUM
            //                      join item3 in entity.T_PRODUCE_SORTTROUGH on item.CIGARETTECODE equals item3.CIGARETTECODE
            //                      where item2.STATE == "10" && item2.GROUP1STATE == 10 && item3.TROUGHTYPE == troughtype && item3.CIGARETTETYPE == cigarettetype && item3.STATE == "10" && item2.TASKNUM == query.TASKNUM
            //                      orderby item.CIGARETTECODE, item3.TROUGHNUM
            //                      select new TaskDetail
            //                      {
            //                          tNum = item.TASKNUM,
            //                          CIGARETTDECODE = item.CIGARETTECODE,
            //                          qty = item.QUANTITY ?? 0,
            //                          SortTroughNum = item3.TROUGHNUM,
            //                          //SortTroughNum=item3.MACHINESEQ??0,
            //                          SORTMACHINE = item2.SORTMACHINE ?? 0,
            //                          CIGARETTDENAME = item.CIGARETTENAME,
            //                          MANTISSA = item3.MANTISSA ?? 0,
            //                          THRESHOLD = item3.THRESHOLD ?? 0,
            //                          GroupNO = item3.GROUPNO ?? 0
            //                      }
            //                 ).ToList();

            //        if (query2 != null && query2.Count > 0)
            //        {
            //            //String tempcode = "";
            //            //TaskDetail task = null;
            //            //foreach (var item in query2)//多通道烟均分
            //            //{
            //            //    if (tempcode == item.CIGARETTDECODE)
            //            //    {
            //            //        item.qty = task.qty;
            //            //        task.qty = Math.Round(task.qty / 2);
            //            //        item.qty = item.qty - task.qty;
            //            //    }

            //            //    task = item;
            //            //    tempcode = item.CIGARETTDECODE;
            //            //}
            //            List<String> listCigarettecode = new List<string>();
            //            foreach (var item in query2)
            //            {
            //                int ct = query2.Count(x => x.CIGARETTDECODE == item.CIGARETTDECODE);
            //                if (ct > 1)
            //                {
            //                    if (!listCigarettecode.Contains(item.CIGARETTDECODE))
            //                        listCigarettecode.Add(item.CIGARETTDECODE);
            //                }
            //            }
            //            if (listCigarettecode.Count > 0)
            //            {
            //                foreach (var code in listCigarettecode)
            //                {
            //                    var tempList = query2.FindAll(x => x.CIGARETTDECODE == code).OrderBy(x=>x.SortTroughNum).ToList();
            //                    if (tempList != null && tempList.Count > 1)
            //                    {
            //                        decimal assignCount = 0;
            //                        foreach (var item in tempList)
            //                        {
            //                            if (item.CIGARETTDECODE == FRW || item.CIGARETTDECODE == JBS)
            //                            {
            //                                item.qty = GetTroughQty(query.TASKNUM, item.SortTroughNum, item.CIGARETTDECODE, item.GroupNO);
            //                            }
            //                            else
            //                            {
            //                                decimal totalCount = item.qty;
            //                                item.qty = Math.Ceiling(item.qty / tempList.Count);
            //                                if (assignCount + item.qty > totalCount)
            //                                {
            //                                    item.qty = totalCount - assignCount;
            //                                }
            //                                assignCount += item.qty;
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //            query.STATE = "30";
            //             decimal id = 0;
            //             id = entity.ExecuteStoreQuery<decimal>("select S_wms_log.nextval from dual").First();
            //             T_WMS_LOG log = new T_WMS_LOG();
            //             log.ID = id;
            //             log.ORDRENO = query.TASKNUM+"";
            //             log.CUSTOMERCODE = query.CUSTOMERCODE;
            //             log.TOTALQTY = query.TASKQUANTITY;
            //             log.CTYPE = 1;
            //            foreach (var item in query2)
            //            {
            //              log.GetType().GetProperty("COL"+item.SortTroughNum).SetValue(log,item.qty, null);
            //            }
            //            entity.T_WMS_LOG.AddObject(log);
            //            entity.SaveChanges();
            //        }
            //    }
            //}
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static List<TaskDetail> GetCigaretteSendTask(decimal troughtype, decimal cigarettetype, decimal groupno)
        {
            using (Entities entity = new Entities())
            {   //找出待发送给plc的订单
                var query = (from item in entity.T_PRODUCE_POKE where item.SORTSTATE == 10 && item.GROUPNO == groupno orderby item.SORTNUM select item).FirstOrDefault();
                if (query != null)
                {   //该订单的烟柜等分布情况
                    var query2 = (from item in entity.T_PRODUCE_POKE
                                  where item.SORTNUM == query.SORTNUM && item.GROUPNO == groupno && item.SORTSTATE == 10
                                  orderby item.TROUGHNUM
                                  select new TaskDetail
                                  {
                                      MainBelt = item.MAINBELT ?? 0,
                                      tNum = item.SORTNUM ?? 0,
                                      qty = item.POKENUM ?? 0,
                                        Machineseq = item.MACHINESEQ??0,  //烟柜通道                                      
                                      ExportNum = item.EXPORTNUM,//分拣出口号
                                      GroupNO = item.GROUPNO ?? 0,     //组号
                                      POCKPLACE = item.POKEPLACE.Value   //放烟位置
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









        //select line.tasknum,line.cigarettecode,line.cigarettename,line.quantity 
        //from t_produce_taskline line 
        //inner join t_produce_task task 
        //on line.tasknum=task.tasknum
        //inner join t_produce_sorttrough sort on line.cigarettecode=sort.cigarettecode
        //and task.state=10 and task.group1state=10 and sort.state=10 and sort.machineseq>0 and sort.machineseq<=22
        //and line.tasknum=347 and sort.troughtype=10 and sort.cigarettetype=20
        public static List<TaskDetail> GetCigarette1(int begin, int end, decimal troughtype, decimal cigarettetype)
        {
            return null;
            //using (Entities entity = new Entities())
            //{   //找出待发送给plc的订单
            //    var query = (from item in entity.T_PRODUCE_TASK where item.STATE == "10"  && item.GROUP1STATE==10 orderby item.TASKNUM select item).FirstOrDefault();


            //        if (query != null)
            //        {   //该订单的烟柜等分布情况
            //            var query2 = (from item in entity.T_PRODUCE_TASKLINE
            //                          join item2 in entity.T_PRODUCE_TASK on item.TASKNUM equals item2.TASKNUM
            //                          join item3 in entity.T_PRODUCE_SORTTROUGH on item.CIGARETTECODE equals item3.CIGARETTECODE
            //                          where item2.STATE == "10" && item2.GROUP1STATE == 10 && item3.TROUGHTYPE == troughtype && item3.CIGARETTETYPE == cigarettetype && item3.STATE == "10" &&  item2.TASKNUM == query.TASKNUM
            //                          orderby item.CIGARETTECODE,item3.TROUGHNUM
            //                          select new TaskDetail
            //                          {
            //                                tNum=item.TASKNUM,
            //                              CIGARETTDECODE = item.CIGARETTECODE,
            //                              qty = item.QUANTITY ?? 0,
            //                              SortTroughNum = item3.TROUGHNUM,  //烟柜通道
            //                              //SortTroughNum=item3.MACHINESEQ??0,
            //                               SORTMACHINE=item2.SORTMACHINE??0,//分拣出口号
            //                               CIGARETTDENAME = item.CIGARETTENAME,
            //                              MANTISSA = item3.MANTISSA ?? 0,  //尾数
            //                              THRESHOLD = item3.THRESHOLD ?? 0,//阈值
            //                              GroupNO = item3.GROUPNO ?? 0     //组号
            //                          }
            //                     ).ToList();

            //            if (query2 != null && query2.Count>0)
            //            {
            //                //String tempcode = "";
            //                //TaskDetail task = null;
            //                //foreach (var item in query2)//多通道烟均分
            //                //{
            //                //    if (tempcode == item.CIGARETTDECODE)
            //                //    {
            //                //        item.qty = task.qty;
            //                //        task.qty = Math.Round(task.qty / 2);
            //                //        item.qty = item.qty - task.qty;
            //                //    }

            //                //    task = item;
            //                //    tempcode = item.CIGARETTDECODE;
            //                //}
            //                List<String> listCigarettecode = new List<string>();
            //                foreach (var item in query2)//列出一个道以上的品牌
            //                {
            //                    int ct = query2.Count(x => x.CIGARETTDECODE == item.CIGARETTDECODE);
            //                    if (ct > 1)
            //                    {
            //                        if (!listCigarettecode.Contains(item.CIGARETTDECODE))
            //                            listCigarettecode.Add(item.CIGARETTDECODE);
            //                    }
            //                }
            //                if (listCigarettecode.Count > 0)//如果存在此订单有品牌对应多个烟柜
            //                {
            //                    foreach (var code in listCigarettecode)
            //                    {
            //                        var tempList = query2.FindAll(x => x.CIGARETTDECODE == code).OrderBy(x=>x.SortTroughNum).ToList();//找出某品牌对应的烟柜号
            //                        if (tempList != null && tempList.Count > 1)//对应多个道的品牌一个个拿出来计算
            //                        {
            //                            decimal assignCount = 0;
            //                            foreach (var item in tempList)
            //                            {
            //                                if (item.CIGARETTDECODE == FRW || item.CIGARETTDECODE == JBS)//芙蓉王或精白沙
            //                                {
            //                                    item.qty = GetTroughQty(query.TASKNUM, item.SortTroughNum, item.CIGARETTDECODE, item.GroupNO);//计算分配到各组上
            //                                }
            //                                else//其他品牌多个烟柜
            //                                {
            //                                    decimal totalCount = item.qty;
            //                                    item.qty = Math.Ceiling(item.qty / tempList.Count);//平均值向上取整，支持2个或两个以上的道拆分，最后一个分配剩下的
            //                                    if (assignCount + item.qty > totalCount)
            //                                    {
            //                                        item.qty = totalCount - assignCount;
            //                                    }
            //                                    assignCount += item.qty;
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //                query2= query2.FindAll(x=> decimal.Parse(x.SortTroughNum)>begin && decimal.Parse(x.SortTroughNum)<=end);//烟柜号在1-11间
            //                if (query2 == null || query2.Count == 0)
            //                {
            //                    query.GROUP1STATE = 30;//如果本组没有需要分拣的卷烟，直接将本组标志置为完成：30
            //                    entity.SaveChanges();
            //                    return GetCigarette1(begin, end, troughtype, cigarettetype);//如果本订单没有该通道，找下一个订单
            //                }
            //                else
            //                {
            //                   // entity.SaveChanges();
            //                    return query2;
            //                }
            //            }
            //            else//订单没有对应的烟柜，理论上是没有这种情况
            //            {
            //                query.GROUP1STATE = 30;
            //                entity.SaveChanges();

            //                return GetCigarette1(begin, end, troughtype, cigarettetype);

            //            }

            //        }
            //        else//已经没有分拣任务了
            //        {
            //            return null;
            //        }

            //}


        }
        public static List<TaskDetail> GetCigarette2(int begin, int end, decimal troughtype, decimal cigarettetype)
        {
            return null;
            //using (Entities entity = new Entities())
            //{
            //    var query = (from item in entity.T_PRODUCE_TASK where item.STATE == "10" && item.GROUP2STATE == 10 orderby item.TASKNUM select item).FirstOrDefault();

            //    if (query != null)
            //    {

            //        //query.GROUP2STATE = 20;
            //        var query2 = (from item in entity.T_PRODUCE_TASKLINE
            //                      join item2 in entity.T_PRODUCE_TASK on item.TASKNUM equals item2.TASKNUM
            //                      join item3 in entity.T_PRODUCE_SORTTROUGH on item.CIGARETTECODE equals item3.CIGARETTECODE
            //                      where item2.STATE == "10" && item2.GROUP2STATE == 10 && item3.TROUGHTYPE == troughtype && item3.CIGARETTETYPE == cigarettetype && item3.STATE == "10" && item2.TASKNUM == query.TASKNUM
            //                      orderby item.CIGARETTECODE, item3.TROUGHNUM
            //                      select new TaskDetail
            //                      {
            //                          tNum = item.TASKNUM,
            //                          CIGARETTDECODE = item.CIGARETTECODE,
            //                          qty = item.QUANTITY ?? 0,
            //                          SortTroughNum = item3.TROUGHNUM,
            //                          CIGARETTDENAME = item.CIGARETTENAME
            //                          ,
            //                          MANTISSA = item3.MANTISSA ?? 0,
            //                          THRESHOLD = item3.THRESHOLD ?? 0,
            //                          GroupNO = item3.GROUPNO ?? 0
            //                      }
            //                     ).ToList();
            //        if (query2 != null && query2.Count>0)
            //        {
            //            //String tempcode = "";
            //            //TaskDetail task = null;
            //            //foreach (var item in query2)//多通道烟均分
            //            //{
            //            //    if (tempcode == item.CIGARETTDECODE)
            //            //    {
            //            //        item.qty = task.qty;
            //            //        task.qty = Math.Round(task.qty / 2);
            //            //        item.qty = item.qty - task.qty;
            //            //    }

            //            //    task = item;

            //            //    tempcode = item.CIGARETTDECODE;
            //            //}

            //            List<String> listCigarettecode = new List<string>();
            //            foreach (var item in query2)
            //            {
            //                int ct = query2.Count(x => x.CIGARETTDECODE == item.CIGARETTDECODE);
            //                if (ct > 1)
            //                {
            //                    if (!listCigarettecode.Contains(item.CIGARETTDECODE))
            //                        listCigarettecode.Add(item.CIGARETTDECODE);
            //                }
            //            }
            //            if (listCigarettecode.Count > 0)
            //            {
            //                foreach (var code in listCigarettecode)
            //                {
            //                    var tempList = query2.FindAll(x => x.CIGARETTDECODE == code).OrderBy(x=>x.SortTroughNum).ToList();
            //                    if (tempList != null && tempList.Count > 1)
            //                    {
            //                        decimal assignCount = 0;
            //                        foreach (var item in tempList)
            //                        {
            //                            if (item.CIGARETTDECODE == FRW || item.CIGARETTDECODE == JBS)
            //                            {

            //                                item.qty = GetTroughQty(query.TASKNUM, item.SortTroughNum, item.CIGARETTDECODE, item.GroupNO);
            //                            }
            //                            else
            //                            {
            //                                decimal totalCount = item.qty;
            //                                item.qty = Math.Ceiling(item.qty / tempList.Count);
            //                                if (assignCount + item.qty > totalCount)
            //                                {
            //                                    item.qty = totalCount - assignCount;
            //                                }
            //                                assignCount += item.qty;
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //            query2 = query2.FindAll(x => decimal.Parse(x.SortTroughNum) > begin && decimal.Parse(x.SortTroughNum) <= end);
            //            if (query2 == null || query2.Count == 0)
            //            {
            //                query.GROUP2STATE = 30;
            //                entity.SaveChanges();
            //                return GetCigarette2(begin, end, troughtype, cigarettetype);
            //            }
            //            else
            //            {
            //                entity.SaveChanges();
            //                return query2;
            //            }
            //        }
            //        else
            //        {
            //            query.GROUP2STATE = 30;
            //            entity.SaveChanges();
            //            return GetCigarette2(begin,end,troughtype,cigarettetype);
            //        }
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}

        }
        public static List<TaskDetail> GetCigarette3(int begin, int end, decimal troughtype, decimal cigarettetype)
        {
            return null;
            //using (Entities entity = new Entities())
            //{
            //    var query = (from item in entity.T_PRODUCE_TASK where item.STATE == "10" && item.GROUP3STATE == 10 orderby item.TASKNUM select item).FirstOrDefault();

            //    if (query != null)
            //    {

            //        //query.GROUP3STATE = 20;
            //        var query2 = (from item in entity.T_PRODUCE_TASKLINE
            //                      join item2 in entity.T_PRODUCE_TASK on item.TASKNUM equals item2.TASKNUM
            //                      join item3 in entity.T_PRODUCE_SORTTROUGH on item.CIGARETTECODE equals item3.CIGARETTECODE
            //                      where item2.STATE == "10" && item2.GROUP3STATE == 10 && item3.TROUGHTYPE == troughtype && item3.CIGARETTETYPE == cigarettetype && item3.STATE == "10" &&  item2.TASKNUM == query.TASKNUM
            //                      orderby item.CIGARETTECODE, item3.TROUGHNUM
            //                      select new TaskDetail
            //                      {
            //                          tNum = item.TASKNUM,
            //                          CIGARETTDECODE = item.CIGARETTECODE,
            //                          qty = item.QUANTITY ?? 0,
            //                          SortTroughNum = item3.TROUGHNUM,
            //                          CIGARETTDENAME = item.CIGARETTENAME
            //                          ,
            //                          MANTISSA = item3.MANTISSA ?? 0,
            //                          THRESHOLD = item3.THRESHOLD ?? 0,
            //                          GroupNO = item3.GROUPNO ?? 0
            //                      }
            //                     ).ToList();
            //        if (query2 != null && query2.Count>0)
            //        {
            //            //String tempcode = "";
            //            //TaskDetail task = null;
            //            //foreach (var item in query2)//多通道烟均分
            //            //{
            //            //    if (tempcode == item.CIGARETTDECODE)
            //            //    {
            //            //        item.qty = task.qty;
            //            //        task.qty = Math.Round(task.qty / 2);
            //            //        item.qty = item.qty - task.qty;
            //            //    }

            //            //    task = item;

            //            //    tempcode = item.CIGARETTDECODE;
            //            //}

            //            List<String> listCigarettecode = new List<string>();
            //            foreach (var item in query2)
            //            {
            //                int ct = query2.Count(x => x.CIGARETTDECODE == item.CIGARETTDECODE);
            //                if (ct > 1)
            //                {
            //                    if (!listCigarettecode.Contains(item.CIGARETTDECODE))
            //                        listCigarettecode.Add(item.CIGARETTDECODE);
            //                }
            //            }
            //            if (listCigarettecode.Count > 0)
            //            {
            //                foreach (var code in listCigarettecode)
            //                {
            //                    var tempList = query2.FindAll(x => x.CIGARETTDECODE == code).OrderBy(x => x.SortTroughNum).ToList(); ;
            //                    if (tempList != null && tempList.Count > 1)
            //                    {
            //                        decimal assignCount = 0;
            //                        foreach (var item in tempList)
            //                        {
            //                            if (item.CIGARETTDECODE == FRW || item.CIGARETTDECODE == JBS)
            //                            {

            //                                item.qty = GetTroughQty(query.TASKNUM, item.SortTroughNum, item.CIGARETTDECODE, item.GroupNO);
            //                            }
            //                            else
            //                            {
            //                                decimal totalCount = item.qty;
            //                                item.qty = Math.Ceiling(item.qty / tempList.Count);
            //                                if (assignCount + item.qty > totalCount)
            //                                {
            //                                    item.qty = totalCount - assignCount;
            //                                }
            //                                assignCount += item.qty;
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //            query2 = query2.FindAll(x => decimal.Parse(x.SortTroughNum) > begin && decimal.Parse(x.SortTroughNum) <= end);
            //            if (query2 == null || query2.Count == 0)
            //            {
            //                query.GROUP3STATE = 30;
            //                entity.SaveChanges();
            //                return GetCigarette3(begin, end, troughtype, cigarettetype);
            //            }
            //            else
            //            {
            //                entity.SaveChanges();
            //                return query2;
            //            }
            //        }
            //        else
            //        {
            //            query.GROUP3STATE = 30;
            //            entity.SaveChanges();
            //            return GetCigarette3(begin, end, troughtype, cigarettetype);
            //        }
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}

        }

        public static void UpdateMachine(decimal tasknum, string troughno, int status)
        {

            using (Entities entity = new Entities())
            {
                decimal machineseq = decimal.Parse(troughno);

                var query = (from item in entity.T_PRODUCE_POKE where item.UNIONTASKNUM == tasknum && item.MACHINESEQ == machineseq select item).ToList();
                if (query != null && query.Count > 0)
                {
                    foreach (var item in query)
                    {
                        item.MACHINESTATE = status;
                        
                    }
                    entity.SaveChanges();
                }

            }
        }
        public static void UpdateMachineSec(decimal tasknum, string troughno)
        {

            using (Entities entity = new Entities())
            {
                decimal machineseq = decimal.Parse(troughno);

               var query1=(from item in entity.T_PRODUCE_POKE where item.MACHINESTATE==10 && item.MACHINESEQ == machineseq && item.UNIONTASKNUM<tasknum select item).ToList();
               if (query1 != null && query1.Count > 0)
               {
                   return;
               }
                var query = (from item in entity.T_PRODUCE_POKE where item.UNIONTASKNUM == tasknum && item.MACHINESEQ == machineseq select item).ToList();
                if (query != null && query.Count > 0)
                {
                    foreach (var item in query)
                    {
                        item.MACHINESTATE = 15;

                    }
                    entity.SaveChanges();
                }

            }
        }
        public static void UpdateMachineFinished(decimal tasknum, string troughno)
        {

            using (Entities entity = new Entities())
            {
                decimal machineseq = decimal.Parse(troughno);              
                var query = (from item in entity.T_PRODUCE_POKE where item.UNIONTASKNUM == tasknum && item.MACHINESEQ == machineseq select item).ToList();
                if (query != null && query.Count > 0)
                {
                    foreach (var item in query)
                    {
                        //if (item.MACHINESTATE == 15)
                        //{
                            item.MACHINESTATE = 20;
                        //}

                    }
                    entity.SaveChanges();
                }

            }
        }
        public static void UpdateMachine(decimal tasknum, decimal machineseq,decimal stage)
        {

            using (Entities entity = new Entities())
            {
                

                var query = (from item in entity.T_PRODUCE_POKE where item.UNIONTASKNUM == tasknum && item.MACHINESEQ == machineseq  select item).ToList();
                if (query != null && query.Count > 0)
                {
                    foreach (var item in query)
                    {
                        item.MACHINESTATE = stage;
                    }
                    entity.SaveChanges();
                }

            }
        }
        public static void UpdateMachine(decimal tasknum, string troughno)
        {

            using (Entities entity = new Entities())
            {
                decimal machineseq = decimal.Parse(troughno);

                var query = (from item in entity.T_PRODUCE_POKE where item.UNIONTASKNUM == tasknum && item.MACHINESEQ == machineseq && item.MACHINESTATE==15 select item).ToList();
                if (query != null && query.Count > 0)
                {
                    foreach (var item in query)
                    {
                        item.MACHINESTATE = 20;
                    }
                    entity.SaveChanges();
                }

            }
        }
        public static void UpdateMachine(List<String> list)
        {

            using (Entities entity = new Entities())
            {
                if (list != null && list.Count > 0)
                {
                    decimal pokeId = decimal.Parse(list[0]);
                    var query = (from item in entity.T_PRODUCE_POKE where item.POKEID == pokeId select item).FirstOrDefault();
                    query.MACHINESTATE = 15;
                    entity.SaveChanges();
                }
            }

            //if (list != null && list.Count > 0)
            //{
            //    using (Entities entity = new Entities())
            //    {
            //        foreach (var item in list)
            //        {
            //            String[] values=item.Split(',');
            //            decimal tasknum=decimal.Parse(values[0]);
            //            decimal qty=decimal.Parse(values[3]);
            //            String cigarettecode = values[2];
            //            //result.Add(item.TaskNum + "," + item.SortTroughNum + "," +item.CIGARETTDECODE+","+ item.qty);2452,48,1430105,1  
            //            var query = (from record in entity.T_PRODUCE_TASKLINE where record.TASKNUM == tasknum && record.CIGARETTECODE == cigarettecode select record).FirstOrDefault();
            //            if (query != null)
            //            {
            //                if (qty != 0)
            //                {
            //                    query.SENDTROUGH += values[1] + ":" + qty + ";";
            //                    query.FINISHQUANTITY += qty;
            //                }
            //                //String[] sendValues = query.SENDTROUGH.Split(';');
            //                //decimal tempqty = 0;
            //                //foreach (var v in sendValues)
            //                //{
            //                //    if (v != null && v != "")
            //                //    {
            //                //        String[] temp = v.Split(':');
            //                //        tempqty += decimal.Parse(temp[1]);
            //                //    }
            //                //}

            //                if (query.QUANTITY <= query.FINISHQUANTITY)
            //                {
            //                    query.HANDLESTATE = 30;
            //                }
            //            }

            //        }
            //        entity.SaveChanges();
            //    }
            //}
        }
        /// <summary>
        /// 合流任务接收
        /// </summary>
        /// <param name="stage"></param>
        /// <param name="sortnum"></param>
        public static void UpdateUnionState(decimal stage, int sortnum)//更新合流状态
        {
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_PRODUCE_POKE where item.SORTNUM == sortnum select item).ToList();
                if (query != null && query.Count > 0)
                {
                    foreach (var item in query)
                    {
                        if (item.UNIONSTATE == 10)//未发送的情况下才会接收
                        {
                            item.UNIONSTATE = stage;
                        }
                    }
                    entity.SaveChanges();
                }
            }

        }
        public static void UpdateUnionStatus(decimal stage, int sortnum)//更新合流状态
        {
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_PRODUCE_POKE where item.SORTNUM == sortnum select item).ToList();
                if (query != null && query.Count > 0)
                {
                    foreach (var item in query)
                    {
                        if (item.UNIONSTATE == 10)
                        {
                            item.UNIONSTATE = stage;
                        }
                    }
                    //如果是合流完成,则将task表状态置为完成
                    if (stage == 20)
                    {
                        entity.ExecuteStoreCommand("update t_produce_task set state=30,finishtime=sysdate where  sortnum=" + sortnum);
                    }
                    entity.SaveChanges();
                }
            }

        }
        public static void UpdateUnionSendStatus( int sortnum)//更新合流状态
        {
            using (Entities entity = new Entities())
            {
                var query1 = (from item in entity.T_PRODUCE_POKE where item.SORTNUM< sortnum && item.UNIONSTATE==10 select item).ToList();
                if (query1 != null && query1.Count > 0)
                {
                    return;
                }
                var query = (from item in entity.T_PRODUCE_POKE where item.SORTNUM == sortnum select item).ToList();
                if (query != null && query.Count > 0)
                {
                    foreach (var item in query)
                    {
                        item.UNIONSTATE = 15;
                    }
                    //如果是合流完成,则将task表状态置为完成
                  
                    entity.SaveChanges();
                }
            }

        }
        public static void UpdateUnionFinishedStatus( int sortnum)//更新合流状态
        {
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_PRODUCE_POKE where item.SORTNUM == sortnum select item).ToList();
                if (query != null && query.Count > 0)
                {
                    foreach (var item in query)
                    {
                        if (item.UNIONSTATE == 15)
                        {
                            item.UNIONSTATE = 20;
                        }
                        else
                        {
                            return;
                        }
                    }
                    //如果是合流完成,则将task表状态置为完成
                   
                        entity.ExecuteStoreCommand("update t_produce_task set state=30,finishtime=sysdate where  sortnum=" + sortnum);
                   
                    entity.SaveChanges();
                }
            }

        }

        public static List<T_PRODUCE_POKE> getList(decimal groupno, decimal sortnum)
        {
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_PRODUCE_POKE where item.GROUPNO == groupno && item.SORTNUM == sortnum select item).ToList();
                return query;
            }
        }
        public static void UpdateFJSendStatus(decimal groupno,  decimal sortnum)//更新预分拣任务状态
        {


            using (Entities entity = new Entities())
            {
                var query1 = (from item in entity.T_PRODUCE_POKE where item.GROUPNO == groupno && item.SORTNUM < sortnum && item.SORTSTATE == 10 select item).ToList();
                if (query1 != null && query1.Count > 0)
                {
                    return;
                }
                var query = (from item in entity.T_PRODUCE_POKE where item.GROUPNO == groupno && item.SORTNUM == sortnum select item).ToList();
                
                if (query != null && query.Count > 0)
                {

                    foreach (var item in query)
                    {
                        item.SORTSTATE = 15;
                    }
                    //entity.ExecuteStoreCommand("update t_produce_task set state=30 where  tasknum not in (select distinct a.tasknum from t_produce_poke a,t_produce_task b where  a.tasknum=b.tasknum and sortstate!=20)");
                    entity.SaveChanges();
                }
            }
        }

        public static void UpdateFJFinishStatus(decimal groupno, decimal sortnum)//更新预分拣任务状态
        {


            using (Entities entity = new Entities())
            {
             
                var query = (from item in entity.T_PRODUCE_POKE where item.GROUPNO == groupno && item.SORTNUM == sortnum select item).ToList();

                if (query != null && query.Count > 0)
                {

                    foreach (var item in query)
                    {
                        if (item.SORTSTATE == 15)//必须等于15才能更新已完成
                        {
                            item.SORTSTATE = 20;
                        }
                    }
                    entity.SaveChanges();
                }
            }
        }

         public static void UpdateTaskStatus(decimal groupno, int stage, decimal sortnum)//更新预分拣任务状态
        {


            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_PRODUCE_POKE where  item.SORTSTATE==12 && item.GROUPNO == groupno && item.SORTNUM == sortnum select item).ToList();
                if (query != null && query.Count > 0)
                {
                    foreach (var item in query)
                    {
                        item.SORTSTATE = stage;
                    }
                    //entity.ExecuteStoreCommand("update t_produce_task set state=30 where  tasknum not in (select distinct a.tasknum from t_produce_poke a,t_produce_task b where  a.tasknum=b.tasknum and sortstate!=20)");
                    entity.SaveChanges();
                }
            }
            
        }
        public static void UpdateStatus(decimal groupno, int stage, decimal sortnum)//更新预分拣任务状态
        {


            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_PRODUCE_POKE where item.GROUPNO == groupno && item.SORTNUM == sortnum select item).ToList();
                if (query != null && query.Count > 0)
                {
                    foreach (var item in query)
                    {
                        item.SORTSTATE = stage;
                    }
                    //entity.ExecuteStoreCommand("update t_produce_task set state=30 where  tasknum not in (select distinct a.tasknum from t_produce_poke a,t_produce_task b where  a.tasknum=b.tasknum and sortstate!=20)");
                    entity.SaveChanges();
                }
            }
            
        }
        public static List<TaskDetail> GetCigarette4(int begin, int end, decimal troughtype, decimal cigarettetype)
        {
            return null;
            //using (Entities entity = new Entities())
            //{
            //    var query = (from item in entity.T_PRODUCE_TASK where item.STATE == "10" && item.GROUP4STATE == 10 orderby item.TASKNUM select item).FirstOrDefault();

            //    if (query != null)
            //    {

            //       // query.GROUP4STATE = 20;
            //        var query2 = (from item in entity.T_PRODUCE_TASKLINE
            //                      join item2 in entity.T_PRODUCE_TASK on item.TASKNUM equals item2.TASKNUM
            //                      join item3 in entity.T_PRODUCE_SORTTROUGH on item.CIGARETTECODE equals item3.CIGARETTECODE
            //                      where item2.STATE == "10" && item2.GROUP4STATE == 10 && item3.TROUGHTYPE == troughtype && item3.CIGARETTETYPE == cigarettetype && item3.STATE == "10" &&  item2.TASKNUM == query.TASKNUM
            //                      orderby item.CIGARETTECODE, item3.TROUGHNUM
            //                      select new TaskDetail
            //                      {
            //                          tNum=item.TASKNUM,
            //                          CIGARETTDECODE = item.CIGARETTECODE,
            //                          qty = item.QUANTITY ?? 0,
            //                          SortTroughNum= item3.TROUGHNUM,
            //                          CIGARETTDENAME = item.CIGARETTENAME
            //                          ,
            //                          MANTISSA = item3.MANTISSA ?? 0,
            //                          THRESHOLD = item3.THRESHOLD ?? 0,
            //                          GroupNO = item3.GROUPNO ?? 0
            //                      }
            //                     ).ToList();
            //        if (query2 != null && query2.Count>0)
            //        {
            //            //String tempcode = "";
            //            //TaskDetail task = null;
            //            //foreach (var item in query2)//多通道烟均分
            //            //{
            //            //    if (tempcode == item.CIGARETTDECODE)
            //            //    {
            //            //        item.qty = task.qty;
            //            //        task.qty = Math.Round(task.qty / 2);
            //            //        item.qty = item.qty - task.qty;
            //            //    }

            //            //    task = item;

            //            //    tempcode = item.CIGARETTDECODE;
            //            //}

            //            List<String> listCigarettecode = new List<string>();
            //            foreach (var item in query2)
            //            {
            //                int ct = query2.Count(x => x.CIGARETTDECODE == item.CIGARETTDECODE);
            //                if (ct > 1)
            //                {
            //                    if (!listCigarettecode.Contains(item.CIGARETTDECODE))
            //                        listCigarettecode.Add(item.CIGARETTDECODE);
            //                }
            //            }
            //            if (listCigarettecode.Count > 0)
            //            {
            //                foreach (var code in listCigarettecode)
            //                {
            //                    var tempList = query2.FindAll(x => x.CIGARETTDECODE == code).OrderBy(x => x.SortTroughNum).ToList();
            //                    if (tempList != null && tempList.Count > 1)
            //                    {
            //                        decimal assignCount = 0;
            //                        foreach (var item in tempList)
            //                        {
            //                            if (item.CIGARETTDECODE == FRW || item.CIGARETTDECODE == JBS)
            //                            {

            //                                item.qty = GetTroughQty(query.TASKNUM, item.SortTroughNum, item.CIGARETTDECODE, item.GroupNO);
            //                            }
            //                            else
            //                            {
            //                                decimal totalCount = item.qty;
            //                                item.qty = Math.Ceiling(item.qty / tempList.Count);
            //                                if (assignCount + item.qty > totalCount)
            //                                {
            //                                    item.qty = totalCount - assignCount;
            //                                }
            //                                assignCount += item.qty;
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //            query2 = query2.FindAll(x => decimal.Parse(x.SortTroughNum) > begin && decimal.Parse(x.SortTroughNum) <= end);
            //            if (query2 == null || query2.Count == 0)
            //            {
            //                query.GROUP4STATE = 30;
            //                entity.SaveChanges();
            //                return GetCigarette4(begin, end, troughtype, cigarettetype);
            //            }
            //            else
            //            {
            //                entity.SaveChanges();
            //                return query2;
            //            }
            //        }
            //        else
            //        {
            //            query.GROUP4STATE = 30;
            //            entity.SaveChanges();
            //            return GetCigarette4(begin, end, troughtype, cigarettetype);
            //        }
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}

        }

        public static void UpdateFjSort()
        {
            List<T_PRODUCE_TASK> one = new List<T_PRODUCE_TASK>();
            List<T_PRODUCE_TASK> two = new List<T_PRODUCE_TASK>();
            List<T_PRODUCE_TASK> three = new List<T_PRODUCE_TASK>();
            List<T_PRODUCE_TASK> four = new List<T_PRODUCE_TASK>();
            decimal sortIndex = 0;
            using (Entities entity = new Entities())
            {
                var maxIndex = (from item in entity.T_PRODUCE_TASK orderby item.SORTNUM descending select item.SORTNUM).First();
                if (maxIndex != null)
                    sortIndex = maxIndex ?? 0;
                one = (from item in entity.T_PRODUCE_TASK where item.MAINBELT == 1 && item.STATE == "10" orderby item.TASKNUM select item).ToList();
                two = (from item in entity.T_PRODUCE_TASK where item.MAINBELT == 2 && item.STATE == "10" orderby item.TASKNUM select item).ToList();
                three = (from item in entity.T_PRODUCE_TASK where item.MAINBELT == 3 && item.STATE == "10" orderby item.TASKNUM select item).ToList();
                four = (from item in entity.T_PRODUCE_TASK where item.MAINBELT == 4 && item.STATE == "10" orderby item.TASKNUM select item).ToList();
                Boolean isrun = true;
                while (isrun)
                {
                    if (one != null && one.Count > 0)
                    {
                        List<T_PRODUCE_TASK> temp = one.Take(10).ToList();
                        if (temp != null && temp.Count > 0)
                        {
                            foreach (var item in temp)
                            {
                                item.SORTNUM = sortIndex;
                                sortIndex++;
                                one.Remove(item);
                            }

                        }
                    }
                    if (two != null && two.Count > 0)
                    {
                        List<T_PRODUCE_TASK> temp = two.Take(10).ToList();
                        if (temp != null && temp.Count > 0)
                        {
                            foreach (var item in temp)
                            {
                                item.SORTNUM = sortIndex;
                                sortIndex++;
                                two.Remove(item);
                            }

                        }
                    }
                    if (three != null && three.Count > 0)
                    {
                        List<T_PRODUCE_TASK> temp = three.Take(10).ToList();
                        if (temp != null && temp.Count > 0)
                        {
                            foreach (var item in temp)
                            {
                                item.SORTNUM = sortIndex;
                                sortIndex++;
                                three.Remove(item);
                            }

                        }
                    }
                    if (four != null && four.Count > 0)
                    {
                        List<T_PRODUCE_TASK> temp = four.Take(10).ToList();
                        if (temp != null && temp.Count > 0)
                        {
                            foreach (var item in temp)
                            {
                                item.SORTNUM = sortIndex;
                                sortIndex++;
                                four.Remove(item);
                            }

                        }
                    }
                    entity.ExecuteStoreCommand("update t_produce_taskline  line set sortnum= (select distinct task.sortnum from t_produce_task task where line.tasknum=task.tasknum )");
                    entity.SaveChanges();
                }
            }
        }
        public static void PreUpdateInOut(bool unFullFirst)
        {
            List<T_PRODUCE_SORTTROUGH> listNormal = SortTroughService.GetTrough(10, 20);//分拣通道
            List<T_PRODUCE_SORTTROUGH> listHj = SortTroughService.GetTrough(20, 20);//重力式货架
            try
            {
                //using (TransactionScope ts = new TransactionScope())
                //{
                using (Entities entity = new Entities())
                {
                    foreach (var task in listNormal)
                    {
                        var query = (from item in entity.T_WMS_STORAGEAREA_INOUT where item.AREAID == 3 && item.CELLNO == task.TROUGHNUM select item).Sum(x => x.QTY).GetValueOrDefault();


                        if (query + task.MANTISSA < task.THRESHOLD)//烟柜需要补货
                        {
                            decimal groupno = 1;
                            if (task.GROUPNO <= 2)
                            {
                                groupno = 1;
                            }
                            else if (task.GROUPNO <= 4)
                            {
                                groupno = 2;
                            }
                            else if (task.GROUPNO <= 6)
                            {
                                groupno = 3;
                            }
                            else if (task.GROUPNO <= 8)
                            {
                                groupno = 4;
                            }
                            var querySourcetemp = (from item in entity.T_PRODUCE_SORTTROUGH where item.TROUGHTYPE == 20 && item.CIGARETTECODE == task.CIGARETTECODE && item.STATE == "10" && item.GROUPNO == groupno select item).ToList();
                            var querySource = querySourcetemp[0];
                            List<OutBound> search = null;
                            decimal totalCount = 0;
                            decimal totalMantissa = 0;
                            decimal troughQty = 0;//通道数量
                            troughQty = querySourcetemp.Count;
                            foreach (var itemsource in querySourcetemp)
                            {
                                totalMantissa += (itemsource.MANTISSA ?? 0);
                            }

                            int tempCount = 0;
                            while (query + tempCount + task.MANTISSA < task.THRESHOLD)//循环补烟柜
                            {
                                totalCount = 0;
                                if (querySourcetemp != null && querySourcetemp.Count > 0)//从库存大的先出
                                {
                                    var list1 = querySourcetemp.Select(x => x.TROUGHNUM).ToList();
                                    search = (from item in entity.T_WMS_STORAGEAREA_INOUT
                                              where list1.Contains(item.CELLNO) && item.AREAID == 2
                                              group item by new { item.CELLNO } into g
                                              select new OutBound() { CELLNO = g.Key.CELLNO, QTY = g.Sum(item => item.QTY) ?? 0 }).ToList();
                                    if (search != null && search.Count > 0)
                                    {
                                        decimal qtyTemp = 0;
                                        int maxIndex = 0;
                                        for (int j = 0; j < search.Count; j++)
                                        {
                                            totalCount += search[j].QTY;

                                            if (qtyTemp <= search[j].QTY)
                                            {
                                                qtyTemp = search[j].QTY;
                                                maxIndex = j;
                                            }

                                        }
                                        querySource = querySourcetemp.Find(x => x.MACHINESEQ == decimal.Parse(search[maxIndex].CELLNO));

                                    }
                                }

                                //生成开箱计划
                                INF_JOBDOWNLOAD load = new INF_JOBDOWNLOAD();
                                load.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                                load.JOBID = load.ID;
                                load.JOBTYPE = 80;
                                load.BRANDID = ItemService.GetItemByCode(task.CIGARETTECODE).BIGBOX_BAR;
                                load.CREATEDATE = DateTime.Now;
                                load.PLANQTY = 1;
                                load.PRIORITY = 50;
                                load.SOURCE = querySource.TROUGHNUM;
                                load.TARGET = task.TROUGHNUM + "";
                                load.STATUS = 0;
                                entity.INF_JOBDOWNLOAD.AddObject(load);
                                totalCount -= 1;
                                T_WMS_STORAGEAREA_INOUT outTask1 = new T_WMS_STORAGEAREA_INOUT();

                                outTask1.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                                outTask1.AREAID = 2;//重力式货架
                                outTask1.TASKNO = load.JOBID;
                                outTask1.CELLNO = querySource.TROUGHNUM;
                                outTask1.CIGARETTECODE = task.CIGARETTECODE;
                                outTask1.CIGARETTENAME = task.CIGARETTENAME;
                                outTask1.BARCODE = load.BRANDID + "";
                                outTask1.INOUTTYPE = 10;//出
                                outTask1.QTY = -1;
                                outTask1.STATUS = 10;
                                outTask1.CREATETIME = DateTime.Now;
                                entity.AddToT_WMS_STORAGEAREA_INOUT(outTask1);

                                T_WMS_STORAGEAREA_INOUT outTask2 = new T_WMS_STORAGEAREA_INOUT();
                                outTask2.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                                outTask2.AREAID = 3;//烟柜
                                outTask2.TASKNO = load.JOBID;
                                outTask2.CELLNO = task.TROUGHNUM;
                                outTask2.CIGARETTECODE = task.CIGARETTECODE;
                                outTask2.BARCODE = load.BRANDID + "";
                                outTask2.CIGARETTENAME = task.CIGARETTENAME;
                                outTask2.INOUTTYPE = 20;//入
                                outTask2.QTY = 50;
                                outTask2.CREATETIME = DateTime.Now;
                                outTask2.STATUS = 10;
                                entity.AddToT_WMS_STORAGEAREA_INOUT(outTask2);

                                decimal leftCount = troughQty * FullCount - (totalCount - 1 + totalMantissa);//重力式货架尾数
                                List<T_WMS_ATSCELLINFO_DETAIL> list = AtsCellInfoService.GetAllUnFullPallet();
                                T_WMS_ATSCELLINFO_DETAIL detail = list.Find(x => x.QTY == leftCount && x.CIGARETTECODE == task.CIGARETTECODE);
                                if (detail != null && unFullFirst)
                                {
                                    INF_JOBDOWNLOAD load1 = new INF_JOBDOWNLOAD();
                                    load1.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                                    load1.JOBID = load1.ID;
                                    load1.BRANDID = task.CIGARETTECODE;
                                    load1.CREATEDATE = DateTime.Now;
                                    load1.PLANQTY = detail.QTY;
                                    load1.PRIORITY = 50;

                                    load1.JOBTYPE = 55;//补货出库
                                    load1.SOURCE = AtsCellOutService.getCellNo(task.CIGARETTECODE, (int)detail.QTY);//out cell
                                    load1.TARGET = InfJobDownLoadService.GetTargetOutAddress(load1.SOURCE, detail.QTY ?? 0);//出口
                                    load1.STATUS = 0;
                                    if (load1.SOURCE != "")
                                    {
                                        load1.BARCODE = AtsCellInfoDetailService.GetDetail(load1.SOURCE).BARCODE;
                                        entity.INF_JOBDOWNLOAD.AddObject(load1);
                                    }
                                    totalCount += (detail.QTY ?? 0);
                                    //下达重力式货架补货计划
                                    decimal tempPlanQty = detail.QTY ?? 0;
                                    foreach (var item in querySourcetemp)
                                    {
                                        if (tempPlanQty <= 0)
                                        {
                                            break;
                                        }
                                        INF_JOBDOWNLOAD load2 = new INF_JOBDOWNLOAD();
                                        load2.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                                        load2.JOBID = load2.ID;
                                        load2.BRANDID = load.BRANDID;
                                        load2.CREATEDATE = DateTime.Now;
                                        load2.STATUS = 2;//出库完成置0
                                        decimal planQty = item.MANTISSA ?? 0;
                                        if (AtsCellInfoService.GetCellInfo(load1.SOURCE).DISMANTLE == 10)
                                        {
                                            load2.JOBTYPE = 60;
                                        }
                                        else
                                        {
                                            load2.JOBTYPE = 70;//人工码垛
                                        }
                                        if (search.Find(x => x.CELLNO + "" == item.TROUGHNUM) != null)
                                        {
                                            planQty += search.Find(x => x.CELLNO + "" == item.TROUGHNUM).QTY;
                                        }
                                        if (tempPlanQty >= FullCount - planQty)
                                        {
                                            load2.PLANQTY = FullCount - planQty;
                                        }
                                        else
                                        {
                                            load2.PLANQTY = tempPlanQty;

                                        }

                                        load2.PRIORITY = 50;
                                        load2.SOURCE = load1.TARGET;//out cell立库出口
                                        load2.TARGET = querySource.TROUGHNUM;
                                        load2.STATUS = 0;
                                        entity.INF_JOBDOWNLOAD.AddObject(load2);

                                        T_WMS_STORAGEAREA_INOUT outTask4 = new T_WMS_STORAGEAREA_INOUT();
                                        outTask4.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                                        outTask4.AREAID = 2;//重力式货架
                                        outTask4.CELLNO = item.TROUGHNUM;
                                        outTask4.TASKNO = load2.JOBID;
                                        outTask4.CIGARETTECODE = item.CIGARETTECODE;
                                        outTask4.CIGARETTENAME = item.CIGARETTENAME;
                                        outTask4.BARCODE = load.BRANDID + "";
                                        outTask4.INOUTTYPE = 20;//入库
                                        outTask4.QTY = load2.PLANQTY;
                                        outTask4.STATUS = 10;
                                        outTask4.CREATETIME = DateTime.Now;
                                        entity.AddToT_WMS_STORAGEAREA_INOUT(outTask4);
                                        tempPlanQty = tempPlanQty - (load2.PLANQTY ?? 0);
                                        entity.SaveChanges();
                                    }
                                }
                                else
                                {
                                    if (totalCount + totalMantissa < querySource.THRESHOLD)
                                    {
                                        //var query2 = (from item in entity.T_WMS_STORAGEAREA_INOUT where item.AREAID == 2 && item.CELLNO == outTask1.CELLNO select item).Sum(x => x.QTY);
                                        //if (query2 != null)
                                        //{


                                        //    if (query2 + querySource.MANTISSA <= querySource.THRESHOLD)
                                        //    {

                                        //下达立库补货计划
                                        INF_JOBDOWNLOAD load1 = new INF_JOBDOWNLOAD();
                                        load1.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                                        load1.JOBID = load1.ID;
                                        load1.JOBTYPE = 55;
                                        load1.BRANDID = load.BRANDID;
                                        load1.CREATEDATE = DateTime.Now;
                                        load1.PLANQTY = (int)querySource.BOXCOUNT;
                                        load1.PRIORITY = 50;
                                        load1.SOURCE = AtsCellOutService.getCellNo(task.CIGARETTECODE, (int)querySource.BOXCOUNT);//out cell
                                        load1.TARGET = InfJobDownLoadService.GetTargetOutAddress(load1.SOURCE, load1.PLANQTY ?? 0);//立库出口
                                        load1.STATUS = 0;
                                        if (load1.SOURCE != "")
                                        {
                                            load1.BARCODE = AtsCellInfoDetailService.GetDetail(load1.SOURCE).BARCODE;
                                            entity.INF_JOBDOWNLOAD.AddObject(load1);
                                        }

                                        totalCount += (load1.PLANQTY ?? 0);
                                        decimal tempPlanQty = load1.PLANQTY ?? 0;


                                        //下达重力式货架补货计划
                                        foreach (var item in querySourcetemp)
                                        {
                                            if (tempPlanQty <= 0)
                                            {
                                                break;
                                            }
                                            INF_JOBDOWNLOAD load2 = new INF_JOBDOWNLOAD();
                                            load2.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                                            load2.JOBID = load2.ID;
                                            load2.BRANDID = ItemService.GetItemByCode(task.CIGARETTECODE).BIGBOX_BAR;
                                            load2.CREATEDATE = DateTime.Now;
                                            load2.STATUS = 2;//出库完成置0
                                            decimal planQty = item.MANTISSA ?? 0;
                                            if (AtsCellInfoService.GetCellInfo(load1.SOURCE).DISMANTLE == 10)
                                            {
                                                load2.JOBTYPE = 60;
                                            }
                                            else
                                            {
                                                load2.JOBTYPE = 70;//人工码垛
                                            }
                                            if (search.Find(x => x.CELLNO + "" == item.TROUGHNUM) != null)
                                            {
                                                planQty += search.Find(x => x.CELLNO + "" == item.TROUGHNUM).QTY;
                                            }
                                            if (tempPlanQty >= FullCount - planQty)
                                            {
                                                load2.PLANQTY = FullCount - planQty;
                                            }
                                            else
                                            {
                                                load2.PLANQTY = tempPlanQty;

                                            }

                                            load2.PRIORITY = 50;
                                            load2.SOURCE = load1.TARGET;//out cell立库出口
                                            load2.TARGET = querySource.TROUGHNUM;
                                            load2.STATUS = 0;
                                            entity.INF_JOBDOWNLOAD.AddObject(load2);

                                            T_WMS_STORAGEAREA_INOUT outTask4 = new T_WMS_STORAGEAREA_INOUT();
                                            outTask4.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                                            outTask4.AREAID = 2;//重力式货架
                                            outTask4.CELLNO = item.TROUGHNUM;
                                            outTask4.TASKNO = load2.JOBID;
                                            outTask4.CIGARETTECODE = item.CIGARETTECODE;
                                            outTask4.CIGARETTENAME = item.CIGARETTENAME;
                                            outTask4.BARCODE = load2.BRANDID + "";
                                            outTask4.INOUTTYPE = 20;//入库
                                            outTask4.QTY = load2.PLANQTY;
                                            outTask4.STATUS = 10;
                                            outTask4.CREATETIME = DateTime.Now;
                                            entity.AddToT_WMS_STORAGEAREA_INOUT(outTask4);
                                            tempPlanQty = tempPlanQty - (load2.PLANQTY ?? 0);
                                            entity.SaveChanges();
                                        }

                                    }
                                }
                                entity.SaveChanges();
                                tempCount += 50;
                            }

                        }

                    }

                    //foreach (var item2 in listHj)
                    //{
                    //    var querySourcetemp = (from item in entity.T_PRODUCE_SORTTROUGH where item.TROUGHTYPE == 20 && item.CIGARETTECODE == item2.CIGARETTECODE && item.STATE == "10" && item.GROUPNO == item2.GROUPNO select item).ToList();
                    //    var querySource = querySourcetemp[0];
                    //    List<OutBound> search = null;
                    //    decimal totalCount = 0;
                    //    decimal totalMantissa = 0;
                    //    decimal troughQty = 0;//通道数量
                    //    troughQty = querySourcetemp.Count;
                    //    foreach (var itemsource in querySourcetemp)
                    //    {
                    //        totalMantissa += itemsource.MANTISSA ?? 0;
                    //    }
                    //    if (querySourcetemp != null )
                    //    {


                    //        var list = querySourcetemp.Select(x => x.TROUGHNUM).ToList();
                    //        search = (from item in entity.T_WMS_STORAGEAREA_INOUT
                    //                  where list.Contains(item.CELLNO) && item.AREAID == 2
                    //                  group item by new { item.CELLNO } into g
                    //                  select new OutBound() { CELLNO = g.Key.CELLNO, QTY = g.Sum(item => item.QTY) ?? 0 }).ToList();
                    //        if (search != null && search.Count > 0)
                    //        {
                    //            decimal qtyTemp = 0;
                    //            int maxIndex = 0;
                    //            for (int j = 0; j < search.Count; j++)
                    //            {
                    //                totalCount += search[j].QTY;

                    //                if (qtyTemp <= search[j].QTY)
                    //                {
                    //                    qtyTemp = search[j].QTY;
                    //                    maxIndex = j;
                    //                }

                    //            }
                    //            //querySource = querySourcetemp[maxIndex];

                    //        }
                    //    }
                    //        decimal leftCount = troughQty * FullCount - (totalCount - 1 + totalMantissa);
                    //        List<T_WMS_ATSCELLINFO_DETAIL> list1 = AtsCellInfoService.GetAllUnFullPallet();
                    //        T_WMS_ATSCELLINFO_DETAIL detail = list1.Find(x => x.QTY == leftCount && x.CIGARETTECODE == item2.CIGARETTECODE);
                    //        if (detail != null && unFullFirst)
                    //        {
                    //            INF_JOBDOWNLOAD load1 = new INF_JOBDOWNLOAD();
                    //            load1.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                    //            load1.JOBID = load1.ID;
                    //            load1.BRANDID =item2.CIGARETTECODE;
                    //            load1.CREATEDATE = DateTime.Now;
                    //            load1.PLANQTY = detail.QTY;// (int)querySource.BOXCOUNT;
                    //            load1.PRIORITY = 50;
                    //            load1.JOBTYPE = 55;
                    //            load1.SOURCE = AtsCellOutService.getCellNo(item2.CIGARETTECODE, (int)detail.QTY);//out cell
                    //            load1.TARGET = InfJobDownLoadService.GetTargetOutAddress(load1.SOURCE, load1.PLANQTY??0);
                    //            load1.STATUS = 0;
                    //            entity.INF_JOBDOWNLOAD.AddObject(load1);

                    //            totalCount += detail.QTY ?? 0;
                    //            decimal tempPlanQty = load1.PLANQTY ?? 0;


                    //            //下达重力式货架补货计划
                    //            foreach (var item in querySourcetemp)
                    //            {
                    //                if (tempPlanQty <= 0)
                    //                {
                    //                    break;
                    //                }
                    //                INF_JOBDOWNLOAD load2 = new INF_JOBDOWNLOAD();
                    //                load2.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                    //                load2.JOBID = load2.ID;
                    //                load2.BRANDID = load1.BRANDID;
                    //                load2.CREATEDATE = DateTime.Now;
                    //                decimal planQty = item.MANTISSA ?? 0;
                    //                load2.STATUS = 2;//出库完成置0
                    //                if (search.Find(x => x.CELLNO + "" == item.TROUGHNUM) != null)
                    //                {
                    //                    planQty += search.Find(x => x.CELLNO + "" == item.TROUGHNUM).QTY;
                    //                }
                    //                if (tempPlanQty >= FullCount - planQty)
                    //                {
                    //                    load2.PLANQTY = FullCount - planQty;
                    //                }
                    //                else
                    //                {
                    //                    load2.PLANQTY = tempPlanQty;

                    //                }

                    //                load2.PRIORITY = 50;
                    //                load2.SOURCE = load1.TARGET;//out cell立库出口
                    //                load2.TARGET = querySource.TROUGHNUM;
                    //                load2.STATUS = 0;
                    //                if (AtsCellInfoService.GetCellInfo(load1.SOURCE).DISMANTLE == 10)
                    //                {
                    //                    load2.JOBTYPE = 60;
                    //                }
                    //                else
                    //                {
                    //                    load2.JOBTYPE = 70;//人工码垛
                    //                }
                    //                entity.INF_JOBDOWNLOAD.AddObject(load2);

                    //                T_WMS_STORAGEAREA_INOUT outTask4 = new T_WMS_STORAGEAREA_INOUT();
                    //                outTask4.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                    //                outTask4.AREAID = 2;//重力式货架
                    //                outTask4.TASKNO = load2.JOBID;
                    //                outTask4.CELLNO = item.TROUGHNUM;
                    //                outTask4.CIGARETTECODE = item.CIGARETTECODE;
                    //                outTask4.CIGARETTENAME = item.CIGARETTENAME;
                    //                outTask4.BARCODE=load2.BRANDID+"";
                    //                outTask4.INOUTTYPE = 20;//入库
                    //                outTask4.QTY = load2.PLANQTY;
                    //                outTask4.STATUS = 10;
                    //                outTask4.CREATETIME = DateTime.Now;
                    //                entity.AddToT_WMS_STORAGEAREA_INOUT(outTask4);
                    //                tempPlanQty = tempPlanQty - load2.PLANQTY ?? 0;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (totalCount  + totalMantissa <= querySource.THRESHOLD)
                    //            {
                    //                //var query2 = (from item in entity.T_WMS_STORAGEAREA_INOUT where item.AREAID == 2 && item.CELLNO == outTask1.CELLNO select item).Sum(x => x.QTY);
                    //                //if (query2 != null)
                    //                //{


                    //                //    if (query2 + querySource.MANTISSA <= querySource.THRESHOLD)
                    //                //    {

                    //                //下达立库补货计划
                    //                INF_JOBDOWNLOAD load1 = new INF_JOBDOWNLOAD();
                    //                load1.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                    //                load1.JOBID = load1.ID;
                    //                load1.BRANDID = item2.CIGARETTECODE;
                    //                load1.JOBTYPE=55;
                    //                load1.CREATEDATE = DateTime.Now;
                    //                load1.PLANQTY = (int)querySource.BOXCOUNT;
                    //                load1.PRIORITY = 50;
                    //                load1.SOURCE = AtsCellOutService.getCellNo(item2.CIGARETTECODE, (int)querySource.BOXCOUNT);//out cell
                    //                load1.TARGET = InfJobDownLoadService.GetTargetOutAddress(load1.SOURCE, load1.PLANQTY ?? 0);
                    //                load1.STATUS = 0;
                    //                entity.INF_JOBDOWNLOAD.AddObject(load1);
                    //                totalCount += load1.PLANQTY ?? 0;

                    //                decimal tempPlanQty = load1.PLANQTY ?? 0;


                    //                //下达重力式货架补货计划
                    //                foreach (var item in querySourcetemp)
                    //                {
                    //                    if (tempPlanQty <= 0)
                    //                    {
                    //                        break;
                    //                    }
                    //                    INF_JOBDOWNLOAD load2 = new INF_JOBDOWNLOAD();
                    //                    load2.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                    //                    load2.JOBID = load2.ID;
                    //                    load2.BRANDID = ItemService.GetItemByCode(item2.CIGARETTECODE).BIGBOX_BAR;
                    //                    load2.CREATEDATE = DateTime.Now;
                    //                    decimal planQty = item.MANTISSA ?? 0;
                    //                    load2.STATUS = 2;//出库完成置0
                    //                    if (search.Find(x => x.CELLNO + "" == item.TROUGHNUM) != null)
                    //                    {
                    //                        planQty += search.Find(x => x.CELLNO + "" == item.TROUGHNUM).QTY;
                    //                    }
                    //                    if (tempPlanQty >= FullCount - planQty)
                    //                    {
                    //                        load2.PLANQTY = FullCount - planQty;
                    //                    }
                    //                    else
                    //                    {
                    //                        load2.PLANQTY = tempPlanQty;

                    //                    }

                    //                    load2.PRIORITY = 50;
                    //                    load2.SOURCE = load1.TARGET; //out cell立库出口
                    //                    load2.TARGET = querySource.TROUGHNUM;
                    //                    load2.STATUS = 0;
                    //                    if(AtsCellInfoService.GetCellInfo(load1.SOURCE).DISMANTLE==10)
                    //                    {
                    //                        load2.JOBTYPE = 60;
                    //                    }
                    //                    else
                    //                    {
                    //                        load2.JOBTYPE = 70;//人工码垛
                    //                    }
                    //                    entity.INF_JOBDOWNLOAD.AddObject(load2);

                    //                    T_WMS_STORAGEAREA_INOUT outTask4 = new T_WMS_STORAGEAREA_INOUT();
                    //                    outTask4.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                    //                    outTask4.AREAID = 2;//重力式货架
                    //                    outTask4.CELLNO = item.TROUGHNUM;
                    //                    outTask4.CIGARETTECODE = item.CIGARETTECODE;
                    //                    outTask4.CIGARETTENAME = item.CIGARETTENAME;
                    //                    outTask4.BARCODE=load2.BRANDID+"";
                    //                    outTask4.TASKNO = load2.JOBID;
                    //                    outTask4.INOUTTYPE = 20;//入库
                    //                    outTask4.QTY = load2.PLANQTY;
                    //                    outTask4.STATUS = 10;
                    //                    outTask4.CREATETIME = DateTime.Now;
                    //                    entity.AddToT_WMS_STORAGEAREA_INOUT(outTask4);
                    //                    tempPlanQty = tempPlanQty - load2.PLANQTY ?? 0;
                    //                }

                    //            }
                    //        }

                    //} //end foreach

                    //     entity.SaveChanges();
                }
                //    ts.Complete();
                //}
            }
            catch (Exception ex)
            {
                String error = ex.Message;
            }
        }//预补结束
        public static void UpdateInOut(int taskno, int begin, int end, int troughtype, int cigaretteType, bool unFullFirst)
        {
            List<TaskDetail> taskList = GetCigarette(begin, end, troughtype, cigaretteType, taskno);
            if (taskList == null || taskList.Count == 0)
            {
                return;
            }
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    using (Entities entity = new Entities())
                    {
                        foreach (var task in taskList)
                        {
                            T_WMS_STORAGEAREA_INOUT outTask = new T_WMS_STORAGEAREA_INOUT();
                            decimal id = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                            outTask.ID = id;
                            outTask.AREAID = 3;//烟柜
                            outTask.CELLNO = task.SortTroughNum;
                            outTask.CIGARETTECODE = task.CIGARETTDECODE;
                            outTask.BARCODE = ItemService.GetItemByCode(task.CIGARETTDECODE).BIGBOX_BAR;
                            outTask.INOUTTYPE = 10;//出
                            outTask.QTY = -task.qty;
                            outTask.STATUS = 20;
                            outTask.CIGARETTENAME = task.CIGARETTDENAME;
                            outTask.CREATETIME = DateTime.Now;

                            entity.AddToT_WMS_STORAGEAREA_INOUT(outTask);
                            //此通道是否需要补烟
                            var query = (from item in entity.T_WMS_STORAGEAREA_INOUT where item.AREAID == 3 && item.CELLNO == task.SortTroughNum select item).Sum(x => x.QTY).GetValueOrDefault();

                            if (query + task.MANTISSA < task.THRESHOLD)
                            {
                                decimal groupno = 1;
                                if (task.GroupNO <= 2)
                                {
                                    groupno = 1;
                                }
                                else if (task.GroupNO <= 4)
                                {
                                    groupno = 2;
                                }
                                else if (task.GroupNO <= 6)
                                {
                                    groupno = 3;
                                }
                                else if (task.GroupNO <= 8)
                                {
                                    groupno = 4;
                                }
                                //此处要解决有品牌占用多个通道的问题
                                var querySourcetemp = (from item in entity.T_PRODUCE_SORTTROUGH where item.TROUGHTYPE == 20 && item.CIGARETTECODE == task.CIGARETTDECODE && item.STATE == "10" && item.GROUPNO == groupno select item).ToList();
                                var querySource = querySourcetemp[0];
                                List<OutBound> search = null;
                                decimal totalCount = 0;
                                decimal totalMantissa = 0;
                                decimal troughQty = 0;//通道数量
                                troughQty = querySourcetemp.Count;
                                foreach (var itemsource in querySourcetemp)
                                {
                                    totalMantissa += (itemsource.MANTISSA ?? 0);
                                }


                                int tempCount = 0;
                                while (query + tempCount + task.MANTISSA < task.THRESHOLD)//循环补烟柜
                                {
                                    totalCount = 0;

                                    if (querySourcetemp != null && querySourcetemp.Count > 0)//从库存大的先出
                                    {


                                        var list1 = querySourcetemp.Select(x => x.TROUGHNUM).ToList();
                                        search = (from item in entity.T_WMS_STORAGEAREA_INOUT
                                                  where list1.Contains(item.CELLNO) && item.AREAID == 2
                                                  group item by new { item.CELLNO } into g
                                                  select new OutBound() { CELLNO = g.Key.CELLNO, QTY = g.Sum(item => item.QTY) ?? 0 }).ToList();
                                        if (search != null && search.Count > 0)
                                        {
                                            decimal qtyTemp = 0;
                                            int maxIndex = 0;
                                            for (int j = 0; j < search.Count; j++)
                                            {
                                                totalCount += search[j].QTY;

                                                if (qtyTemp <= search[j].QTY)
                                                {
                                                    qtyTemp = search[j].QTY;
                                                    maxIndex = j;
                                                }

                                            }
                                            querySource = querySourcetemp.Find(x => x.MACHINESEQ == decimal.Parse(search[maxIndex]._CELLNO));

                                        }
                                    }
                                    //生成开箱计划
                                    INF_JOBDOWNLOAD load = new INF_JOBDOWNLOAD();
                                    load.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                                    load.JOBID = load.ID;
                                    load.JOBTYPE = 80;
                                    load.BRANDID = ItemService.GetItemByCode(task.CIGARETTDECODE).BIGBOX_BAR;
                                    load.CREATEDATE = DateTime.Now;
                                    load.PLANQTY = 1;
                                    load.PRIORITY = 50;
                                    load.SOURCE = querySource.TROUGHNUM;
                                    load.TARGET = task.SortTroughNum + "";
                                    load.STATUS = 0;
                                    entity.INF_JOBDOWNLOAD.AddObject(load);
                                    totalCount -= 1;
                                    T_WMS_STORAGEAREA_INOUT outTask1 = new T_WMS_STORAGEAREA_INOUT();
                                    outTask1.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                                    outTask1.AREAID = 2;//重力式货架
                                    outTask1.TASKNO = load.JOBID;
                                    outTask1.CELLNO = querySource.TROUGHNUM;
                                    outTask1.CIGARETTECODE = task.CIGARETTDECODE;
                                    outTask1.CIGARETTENAME = task.CIGARETTDENAME;
                                    outTask1.BARCODE = load.BRANDID + "";
                                    outTask1.INOUTTYPE = 10;//出
                                    outTask1.QTY = -1;
                                    outTask1.STATUS = 10;
                                    entity.AddToT_WMS_STORAGEAREA_INOUT(outTask1);



                                    T_WMS_STORAGEAREA_INOUT outTask2 = new T_WMS_STORAGEAREA_INOUT();

                                    outTask2.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                                    outTask2.AREAID = 3;//烟柜
                                    outTask2.CELLNO = task.SortTroughNum;
                                    outTask2.TASKNO = load.JOBID;
                                    outTask2.CIGARETTECODE = task.CIGARETTDECODE;
                                    outTask2.CIGARETTENAME = task.CIGARETTDENAME;
                                    outTask2.BARCODE = load.BRANDID + "";
                                    outTask2.INOUTTYPE = 20;//入
                                    outTask2.QTY = 50;
                                    outTask2.CREATETIME = DateTime.Now;
                                    outTask2.STATUS = 10;
                                    entity.AddToT_WMS_STORAGEAREA_INOUT(outTask2);

                                    decimal leftCount = troughQty * FullCount - (totalCount - 1 + totalMantissa);
                                    List<T_WMS_ATSCELLINFO_DETAIL> list = AtsCellInfoService.GetAllUnFullPallet();
                                    T_WMS_ATSCELLINFO_DETAIL detail = list.Find(x => x.QTY == leftCount && x.CIGARETTECODE == task.CIGARETTDECODE);
                                    if (detail != null && unFullFirst)
                                    {
                                        INF_JOBDOWNLOAD load1 = new INF_JOBDOWNLOAD();
                                        load1.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                                        load1.JOBID = load1.ID;
                                        load1.JOBTYPE = 55;
                                        load1.BRANDID = load.BRANDID;
                                        load1.CREATEDATE = DateTime.Now;
                                        load1.PLANQTY = detail.QTY; // (int)querySource.BOXCOUNT;
                                        load1.PRIORITY = 50;
                                        load1.SOURCE = AtsCellOutService.getCellNo(task.CIGARETTDECODE, (int)detail.QTY);//out cell
                                        load1.TARGET = InfJobDownLoadService.GetTargetOutAddress(load1.SOURCE, load1.PLANQTY ?? 0);
                                        load1.STATUS = 0;
                                        entity.INF_JOBDOWNLOAD.AddObject(load1);
                                        totalCount += (load1.PLANQTY ?? 0);
                                        decimal tempPlanQty = detail.QTY ?? 0;
                                        //下达重力式货架补货计划
                                        foreach (var item in querySourcetemp)
                                        {
                                            if (tempPlanQty <= 0)
                                            {
                                                break;
                                            }
                                            INF_JOBDOWNLOAD load2 = new INF_JOBDOWNLOAD();
                                            load2.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                                            load2.JOBID = load2.ID;
                                            load2.BRANDID = load.BRANDID;
                                            load2.CREATEDATE = DateTime.Now;
                                            decimal planQty = item.MANTISSA ?? 0;
                                            load2.STATUS = 2;//出库完成置0
                                            if (search.Find(x => x.CELLNO + "" == item.TROUGHNUM) != null)
                                            {
                                                planQty += search.Find(x => x.CELLNO + "" == item.TROUGHNUM).QTY;
                                            }
                                            if (tempPlanQty >= FullCount - planQty)
                                            {
                                                load2.PLANQTY = FullCount - planQty;
                                            }
                                            else
                                            {
                                                load2.PLANQTY = tempPlanQty;

                                            }

                                            load2.PRIORITY = 50;
                                            load2.SOURCE = load1.TARGET;//out cell立库出口
                                            load2.TARGET = querySource.TROUGHNUM;
                                            load2.STATUS = 0;
                                            if (AtsCellInfoService.GetCellInfo(load1.SOURCE).DISMANTLE == 10)
                                            {
                                                load2.JOBTYPE = 60;
                                            }
                                            else
                                            {
                                                load2.JOBTYPE = 70;//人工码垛
                                            }
                                            entity.INF_JOBDOWNLOAD.AddObject(load2);

                                            T_WMS_STORAGEAREA_INOUT outTask4 = new T_WMS_STORAGEAREA_INOUT();
                                            outTask4.AREAID = 2;//重力式货架
                                            outTask4.TASKNO = load2.JOBID;
                                            outTask4.CELLNO = item.TROUGHNUM;
                                            outTask4.CIGARETTECODE = item.CIGARETTECODE;
                                            outTask4.CIGARETTENAME = item.CIGARETTENAME;
                                            outTask4.BARCODE = load.BRANDID + "";
                                            outTask4.INOUTTYPE = 20;//入库
                                            outTask4.QTY = load2.PLANQTY;
                                            outTask4.STATUS = 10;
                                            outTask4.CREATETIME = DateTime.Now;
                                            entity.AddToT_WMS_STORAGEAREA_INOUT(outTask4);
                                            tempPlanQty = tempPlanQty - (load2.PLANQTY ?? 0);
                                            entity.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        if (totalCount + totalMantissa < querySource.THRESHOLD)
                                        {
                                            //var query2 = (from item in entity.T_WMS_STORAGEAREA_INOUT where item.AREAID == 2 && item.CELLNO == outTask1.CELLNO select item).Sum(x => x.QTY);
                                            //if (query2 != null)
                                            //{


                                            //    if (query2 + querySource.MANTISSA <= querySource.THRESHOLD)
                                            //    {

                                            //下达立库补货计划
                                            INF_JOBDOWNLOAD load1 = new INF_JOBDOWNLOAD();
                                            load1.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                                            load1.JOBID = load1.ID;
                                            load1.JOBTYPE = 55;
                                            load1.BRANDID = ItemService.GetItemByCode(task.CIGARETTDECODE).BIGBOX_BAR;
                                            load1.CREATEDATE = DateTime.Now;
                                            load1.PLANQTY = (int)querySource.BOXCOUNT;
                                            load1.PRIORITY = 50;
                                            load1.SOURCE = AtsCellOutService.getCellNo(task.CIGARETTDECODE, (int)querySource.BOXCOUNT);//out cell
                                            load1.TARGET = InfJobDownLoadService.GetTargetOutAddress(load1.SOURCE, load1.PLANQTY ?? 0);
                                            load1.STATUS = 0;
                                            entity.INF_JOBDOWNLOAD.AddObject(load1);

                                            totalCount += (load1.PLANQTY ?? 0);
                                            decimal tempPlanQty = load1.PLANQTY ?? 0;
                                            //下达重力式货架补货计划
                                            foreach (var item in querySourcetemp)
                                            {
                                                if (tempPlanQty <= 0)
                                                {

                                                    break;
                                                }
                                                INF_JOBDOWNLOAD load2 = new INF_JOBDOWNLOAD();
                                                load2.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                                                load2.JOBID = load2.ID;
                                                load2.BRANDID = load1.BRANDID;
                                                load2.CREATEDATE = DateTime.Now;
                                                decimal planQty = item.MANTISSA ?? 0;
                                                load2.STATUS = 2;//出库完成置0
                                                if (search.Find(x => x.CELLNO + "" == item.TROUGHNUM) != null)
                                                {
                                                    planQty += search.Find(x => x.CELLNO + "" == item.TROUGHNUM).QTY;
                                                }
                                                if (tempPlanQty >= FullCount - planQty)
                                                {
                                                    load2.PLANQTY = FullCount - planQty;
                                                }
                                                else
                                                {
                                                    load2.PLANQTY = tempPlanQty;

                                                }

                                                load2.PRIORITY = 50;
                                                load2.SOURCE = load1.TARGET;//out cell立库出口
                                                load2.TARGET = querySource.TROUGHNUM;
                                                load2.STATUS = 0;
                                                if (AtsCellInfoService.GetCellInfo(load1.SOURCE).DISMANTLE == 10)
                                                {
                                                    load2.JOBTYPE = 60;
                                                }
                                                else
                                                {
                                                    load2.JOBTYPE = 70;//人工码垛
                                                }
                                                entity.INF_JOBDOWNLOAD.AddObject(load2);

                                                T_WMS_STORAGEAREA_INOUT outTask4 = new T_WMS_STORAGEAREA_INOUT();
                                                outTask4.TASKNO = load2.JOBID;
                                                outTask4.AREAID = 2;//重力式货架
                                                outTask4.CELLNO = item.TROUGHNUM;
                                                outTask4.CIGARETTECODE = item.CIGARETTECODE;
                                                outTask4.CIGARETTENAME = item.CIGARETTENAME;
                                                outTask4.BARCODE = load1.BRANDID + "";
                                                outTask4.INOUTTYPE = 20;//入库
                                                outTask4.QTY = load2.PLANQTY;
                                                outTask4.STATUS = 10;
                                                outTask4.CREATETIME = DateTime.Now;
                                                entity.AddToT_WMS_STORAGEAREA_INOUT(outTask4);
                                                tempPlanQty = tempPlanQty - (load2.PLANQTY ?? 0);
                                                entity.SaveChanges();
                                            }
                                            //反库 wcs生成 
                                            //

                                            //    }

                                            //}
                                        }
                                    }
                                    tempCount += 50;
                                }
                            }

                        }
                        entity.SaveChanges();
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                String error = ex.Message;
            }
        }
        public static decimal getPlace(String cigarettecode, string troughnum, decimal tasknum)//获取烟的位置
        {
            using (Entities data = new Entities())
            {
                var query = (from item in data.T_PRODUCE_TASKLINE
                             join item2 in data.T_PRODUCE_SORTTROUGH
                             on item.CIGARETTECODE equals item2.CIGARETTECODE
                             where item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20 && item2.STATE == "10" && item2.CIGARETTECODE == cigarettecode
                                  && item.TASKNUM <= tasknum
                             orderby item.TASKNUM, item2.TROUGHNUM
                             select new TaskDetail() { tNum = item.TASKNUM, CIGARETTDECODE = item.CIGARETTECODE, qty = item.QUANTITY ?? 0, SortTroughNum = item2.TROUGHNUM }).ToList();
                List<decimal> tasknums = new List<decimal>();
                foreach (var item in query)
                {
                    int ct = query.FindAll(x => x.tNum == item.tNum).Count();
                    if (ct > 1)
                    {
                        if (!tasknums.Contains(item.tNum))
                            tasknums.Add(item.tNum);
                    }
                }
                if (tasknums.Count > 0)
                {
                    foreach (var code in tasknums)
                    {
                        var tempList = query.FindAll(x => x.tNum == code);
                        if (tempList != null && tempList.Count > 1)
                        {
                            decimal assignCount = 0;
                            foreach (var item in tempList)
                            {
                                decimal totalCount = item.qty;
                                item.qty = Math.Ceiling(item.qty / tempList.Count);
                                if (assignCount + item.qty > totalCount)
                                {
                                    item.qty = totalCount - assignCount;
                                }
                                assignCount += item.qty;
                            }
                        }
                    }
                }
                decimal place = 0;
                decimal tempCount = 0;
                foreach (var item in query)
                {
                    if (item.SortTroughNum == troughnum)
                    {
                        if (tempCount + item.qty < 10)
                        {
                            place = tempCount + 1;
                            tempCount += item.qty;
                            //  place += 1;
                        }
                        else
                        {
                            tempCount = item.qty;
                            place = 1;
                        }
                    }
                }

                return place;
            }


        }
        static Object lockFlag = new Object();
        //订单完成触发
        public static object[] GetSortTask(decimal sortgroupno)
        {
            WriteLog writeLog =  WriteLog.GetLog();

            object[] values = new object[27];
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
                            values[26] = 1;//标志位  ||27
                        }
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



        //订单完成触发
        public static object[] GetTask1()//任务号/虚拟出口号/总条数/每个道德数量和位置。。。/写入标志，1
        {
            WriteLog writeLog =  WriteLog.GetLog();

            object[] values = new object[26];
            for (int i = 0; i < values.Length; i++)//初始化一个数组
            {
                values[i] = 0;
            }
            List<TaskDetail> list = GetCigarette1(0, 11, 10, 20);
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
                            values[1] = int.Parse(item.SORTMACHINE + "");//虚拟出口号
                            values[2] = 0;
                            values[25] = 1;
                        }
                        //SortTroughNum写死了第一组，其他组用就会有问题
                        values[3 + 2 * (int.Parse(item.SortTroughNum + "") - 1)] = int.Parse(item.qty + "");
                        //如果机械手吸烟需要多个订单一起吸，则需要计算放烟位置
                        values[4 + 2 * (int.Parse(item.SortTroughNum + "") - 1)] = getPlace(item.CIGARETTDECODE, item.SortTroughNum, item.tNum);
                        totalCount += int.Parse(item.qty + "");
                        i++;
                    }
                }
                values[2] = totalCount;
            }

            return values;
        }
        public static object[] GetTask2()
        {
            object[] values = new object[26];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            List<TaskDetail> list = GetCigarette1(11, 22, 10, 20);
            if (list != null)
            {
                int i = 0;
                int totalCount = 0;
                foreach (var item in list)
                {
                    if (item.qty != 0)
                    {
                        if (i == 0)
                        {
                            values[0] = item.tNum;
                            values[1] = int.Parse(item.SORTMACHINE + "");
                            values[2] = 0;
                            values[25] = 1;
                        }
                        values[3 + 2 * (int.Parse(item.SortTroughNum + "") - 1)] = int.Parse(item.qty + "");
                        values[4 + 2 * (int.Parse(item.SortTroughNum + "") - 1)] = getPlace(item.CIGARETTDECODE, item.SortTroughNum, item.tNum);
                        totalCount += int.Parse(item.qty + "");
                        i++;
                    }
                }
                values[2] = totalCount;
            }
            return values;
        }
        public static object[] GetTask3()
        {
            object[] values = new object[26];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            List<TaskDetail> list = GetCigarette1(22, 33, 10, 20);
            if (list != null)
            {
                int i = 0;
                int totalCount = 0;
                foreach (var item in list)
                {
                    if (item.qty != 0)
                    {
                        if (i == 0)
                        {
                            values[0] = item.tNum;
                            values[1] = int.Parse(item.SORTMACHINE + "");
                            values[2] = 0;
                            values[25] = 1;
                        }
                        values[3 + 2 * (int.Parse(item.SortTroughNum + "") - 1)] = int.Parse(item.qty + "");
                        values[4 + 2 * (int.Parse(item.SortTroughNum + "") - 1)] = getPlace(item.CIGARETTDECODE, item.SortTroughNum, item.tNum);
                        totalCount += int.Parse(item.qty + "");
                        i++;
                    }
                }
                values[2] = totalCount;
            }
            return values;
        }
        public static object[] GetTask4()
        {
            object[] values = new object[26];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            List<TaskDetail> list = GetCigarette1(33, 44, 10, 20);
            if (list != null)
            {
                int i = 0;
                int totalCount = 0;
                foreach (var item in list)
                {
                    if (item.qty != 0)
                    {
                        if (i == 0)
                        {
                            values[0] = item.tNum;
                            values[1] = int.Parse(item.SORTMACHINE + "");
                            values[2] = 0;
                            values[25] = 1;
                        }
                        values[3 + 2 * (int.Parse(item.SortTroughNum + "") - 1)] = int.Parse(item.qty + "");
                        values[4 + 2 * (int.Parse(item.SortTroughNum + "") - 1)] = getPlace(item.CIGARETTDECODE, item.SortTroughNum, item.tNum);
                        totalCount += int.Parse(item.qty + "");
                        i++;
                    }
                }
                values[2] = totalCount;
            }
            return values;
        }
        public static object[] GetTask5()
        {
            object[] values = new object[26];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            List<TaskDetail> list = GetCigarette1(44, 55, 10, 20);
            if (list != null)
            {
                int i = 0;
                int totalCount = 0;
                foreach (var item in list)
                {
                    if (item.qty != 0)
                    {
                        if (i == 0)
                        {
                            values[0] = item.tNum;
                            values[1] = int.Parse(item.SORTMACHINE + "");
                            values[2] = 0;
                            values[25] = 1;
                        }
                        values[3 + 2 * (int.Parse(item.SortTroughNum + "") - 1)] = int.Parse(item.qty + "");
                        values[4 + 2 * (int.Parse(item.SortTroughNum + "") - 1)] = getPlace(item.CIGARETTDECODE, item.SortTroughNum, item.tNum);
                        totalCount += int.Parse(item.qty + "");
                        i++;
                    }
                }
                values[2] = totalCount;
            }
            return values;
        }
        public static object[] GetTask6()
        {
            object[] values = new object[26];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            List<TaskDetail> list = GetCigarette1(55, 66, 10, 20);
            if (list != null)
            {
                int i = 0;
                int totalCount = 0;
                foreach (var item in list)
                {
                    if (item.qty != 0)
                    {
                        if (i == 0)
                        {
                            values[0] = item.tNum;
                            values[1] = int.Parse(item.SORTMACHINE + "");
                            values[2] = 0;
                            values[25] = 1;
                        }
                        values[3 + 2 * (int.Parse(item.SortTroughNum + "") - 1)] = int.Parse(item.qty + "");
                        values[4 + 2 * (int.Parse(item.SortTroughNum + "") - 1)] = getPlace(item.CIGARETTDECODE, item.SortTroughNum, item.tNum);
                        totalCount += int.Parse(item.qty + "");
                        i++;
                    }
                }
                values[2] = totalCount;
            }
            return values;
        }
        public static object[] GetTask7()
        {
            object[] values = new object[26];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            List<TaskDetail> list = GetCigarette1(66, 77, 10, 20);
            if (list != null)
            {
                int i = 0;
                int totalCount = 0;
                foreach (var item in list)
                {
                    if (item.qty != 0)
                    {
                        if (i == 0)
                        {
                            values[0] = item.tNum;
                            values[1] = int.Parse(item.SORTMACHINE + "");
                            values[2] = 0;
                            values[25] = 1;
                        }
                        values[3 + 2 * (int.Parse(item.SortTroughNum + "") - 1)] = int.Parse(item.qty + "");
                        values[4 + 2 * (int.Parse(item.SortTroughNum + "") - 1)] = getPlace(item.CIGARETTDECODE, item.SortTroughNum, item.tNum);
                        totalCount += int.Parse(item.qty + "");
                        i++;
                    }
                }
                values[2] = totalCount;
            }
            return values;
        }
        public static object[] GetTask8()
        {
            object[] values = new object[26];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            List<TaskDetail> list = GetCigarette1(77, 88, 10, 20);
            if (list != null)
            {
                int i = 0;
                int totalCount = 0;
                foreach (var item in list)
                {
                    if (item.qty != 0)
                    {
                        if (i == 0)
                        {
                            values[0] = item.tNum;
                            values[1] = int.Parse(item.SORTMACHINE + "");
                            values[2] = 0;
                            values[25] = 1;
                        }
                        values[3 + 2 * (int.Parse(item.SortTroughNum + "") - 1)] = int.Parse(item.qty + "");
                        values[4 + 2 * (int.Parse(item.SortTroughNum + "") - 1)] = getPlace(item.CIGARETTDECODE, item.SortTroughNum, item.tNum);
                        totalCount += int.Parse(item.qty + "");
                        i++;
                    }
                }
                values[2] = totalCount;
            }
            return values;
        }

        public static void TestUnion()
        {
            //using (Entities entity = new Entities())
            //{
            //    var query = (from item in entity.T_PRODUCE_TASK where item.UNIONSTATE == 10 orderby item.TASKNUM select item).FirstOrDefault();
            //    if (query != null)
            //    {
            //        var query2 = (from item in entity.T_PRODUCE_TASKLINE
            //                      join item2 in entity.T_PRODUCE_SORTTROUGH
            //                      on item.CIGARETTECODE equals item2.CIGARETTECODE
            //                      where item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20 && item2.STATE == "10" && item.TASKNUM == query.TASKNUM
            //                      orderby item.CIGARETTECODE
            //                      select new TaskDetail() { CIGARETTDECODE = item.CIGARETTECODE, qty = item.QUANTITY ?? 0, SortTroughNum = item2.TROUGHNUM, GroupNO = item2.GROUPNO ?? 0 }).ToList();


            //        if (query2 != null && query2.Count > 0)
            //        {


            //            decimal qty1 = 0, qty2 = 0, qty3 = 0, qty4 = 0, qty5 = 0, qty6 = 0, qty7 = 0, qty8 = 0;
            //            List<String> listCigarettecode = new List<string>();
            //            foreach (var item in query2)
            //            {
            //                int ct = query2.Count(x => x.CIGARETTDECODE == item.CIGARETTDECODE);
            //                if (ct > 1)
            //                {
            //                    if (!listCigarettecode.Contains(item.CIGARETTDECODE))
            //                        listCigarettecode.Add(item.CIGARETTDECODE);
            //                }
            //            }
            //            if (listCigarettecode.Count > 0)
            //            {
            //                foreach (var code in listCigarettecode)
            //                {
            //                    var tempList = query2.FindAll(x => x.CIGARETTDECODE == code).OrderBy(x=>x.SortTroughNum).ToList();
            //                    if (tempList != null && tempList.Count > 1)
            //                    {
            //                        decimal assignCount = 0;
            //                        foreach (var item in tempList)
            //                        {
            //                            if (item.CIGARETTDECODE == FRW || item.CIGARETTDECODE == JBS)
            //                            {

            //                                item.qty = GetTroughQty(query.TASKNUM, item.SortTroughNum, item.CIGARETTDECODE, item.GroupNO);
            //                            }
            //                            else
            //                            {
            //                                decimal totalCount = item.qty;
            //                                item.qty = Math.Ceiling(item.qty / tempList.Count);
            //                                if (assignCount + item.qty > totalCount)
            //                                {
            //                                    item.qty = totalCount - assignCount;
            //                                }
            //                                assignCount += item.qty;
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //            foreach (var item in query2)
            //            {
            //                decimal trough = decimal.Parse(item.SortTroughNum);
            //                //
            //                if (trough <= 11)
            //                {
            //                    qty1 += item.qty;
            //                }
            //                else if (11 < trough && trough <= 22)
            //                {
            //                    qty2 += item.qty;
            //                }
            //                else if (22 < trough && trough <= 33)
            //                {
            //                    qty3 += item.qty;
            //                }
            //                else if (33 < trough && trough <= 44)
            //                {
            //                    qty4 += item.qty;
            //                }
            //                else if (44 < trough && trough <= 55)
            //                {
            //                    qty5 += item.qty;
            //                }
            //                else if (55 < trough && trough <= 66)
            //                {
            //                    qty6 += item.qty;
            //                }
            //                else if (66 < trough && trough <= 77)
            //                {
            //                    qty7 += item.qty;
            //                }
            //                else if (77 < trough && trough <= 88)
            //                {
            //                    qty8 += item.qty;
            //                }

            //            }

            //            query.UNIONSTATE = 30;
            //            decimal id = 0;
            //            id = entity.ExecuteStoreQuery<decimal>("select S_wms_log.nextval from dual").First();
            //            T_WMS_LOG log = new T_WMS_LOG();
            //            log.ID = id;
            //            log.ORDRENO = query.TASKNUM + "";
            //            log.CUSTOMERCODE = query.CUSTOMERCODE;
            //            log.TOTALQTY = query.TASKQUANTITY;
            //            log.CTYPE = 2;
            //            log.COL1 = qty1;
            //            log.COL2 = qty2;
            //            log.COL3 = qty3;
            //            log.COL4 = qty4;
            //            log.COL5 = qty5;
            //            log.COL6 = qty6;
            //            log.COL7 = qty7;
            //            log.COL8 = qty8;


            //            entity.T_WMS_LOG.AddObject(log);
            //            entity.SaveChanges();
            //        }


            //    }


            //}

        }
        /// <summary>
        /// 查询禁用主皮带
        /// </summary>
        /// <returns>禁用主皮带</returns>
        public static string GetBanMainBelt()
        {
            string banbelt = "";
            using (Entities entity = new Entities())
            { 
                for (int i = 1; i < 5; i++)
                {
                      //数据禁用主皮带 t_produce_poke
                       var queryMainBelt = (from item in entity.T_PRODUCE_POKE   where item.UNIONSTATE == 10 && item.MAINBELT == i   select item.TASKNUM).Distinct().Count();//一号皮带任务总数
                       if (queryMainBelt == 0)
                       {
                           banbelt += i ;
                       } 
                } 
                //物理禁用主皮带 t_produce_sorttrough
                var query =  (from item in entity.T_PRODUCE_SORTTROUGH where item.TROUGHTYPE == 30 && item.STATE == "0" orderby item.MACHINESEQ select item).ToList();
                foreach (var item in query)
                {
                    banbelt += item.MACHINESEQ + ",";
                }
            }
            return banbelt;
        }

        
        /// <summary>
        /// 给PLC赋 皮带状态初始值
        /// </summary>
        public static object[] FristTime()
        { 
            int i = 1;
            object[] values = new object[4];
            string banbelt = GetBanMainBelt();// int BeltState;//主皮带状态 
            for (int index = 0; index < 4; index++)
            {    
                if (!banbelt.Contains(i.ToString()))//如果为启用 置为1 则为 0
                {
                    values[index] = 1;// 皮带为启用   投入 
                }
                else
                {
                    values[index] = 0;//皮带为禁用   挂起
                }
                i++;
            }

            return values;
        }

        /// <summary>
        /// 合流任务
        /// </summary>
        /// <param name="mainbelt">主皮带</param>
        /// <returns></returns>  
        public static object[] GetUnionTask(int mainbelt )//合流任务
        {
            object[] values = new object[13]; 
            //int fg = 1;
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            using (Entities entity = new Entities())
            {
               
                var query = (from item in entity.T_PRODUCE_POKE where item.UNIONSTATE == 10 && item.MAINBELT == mainbelt orderby item.SORTNUM select item).FirstOrDefault();
                if (query != null)
                { 
                    var query2 = (from item in entity.T_PRODUCE_POKE where item.SORTNUM == query.SORTNUM select item).ToList();
                   
                    if (query2 != null && query2.Count > 0)
                    {
                        values[0] = query.SORTNUM;//递增
                        values[1] = query.UNIONEXPORTNUM ;//出口号
                        values[2] = query.PACKAGEMACHINE;//包装机号
                        values[3] = query2.Sum(x => x.POKENUM);
                        values[4] = query2.Where(x => x.GROUPNO == 1).Sum(x => x.POKENUM);
                        values[5] = query2.Where(x => x.GROUPNO == 2).Sum(x => x.POKENUM);
                        //由于机械手第三组分出来的烟对应的是合流第四组机械手，这里3和4组对应有个对调
                        values[6] = query2.Where(x => x.GROUPNO == 4).Sum(x => x.POKENUM);
                        values[7] = query2.Where(x => x.GROUPNO == 3).Sum(x => x.POKENUM);
                        values[8] = query2.Where(x => x.GROUPNO == 5).Sum(x => x.POKENUM);
                        values[9] = query2.Where(x => x.GROUPNO == 6).Sum(x => x.POKENUM);
                        //由于机械手第7组分出来的烟对应的是合流第八组机械手，这里7和8组对应有个对调
                        values[10] = query2.Where(x => x.GROUPNO == 8).Sum(x => x.POKENUM);
                        values[11] = query2.Where(x => x.GROUPNO == 7).Sum(x => x.POKENUM);
                        values[12] = 1;//标志位

                       
                    }
                }
            }
            return values;
            #region
            //object[] values = new object[12];
            //for (int i = 0; i < values.Length; i++)
            //{
            //    values[i] = 0;
            //}

            //using (Entities entity = new Entities())
            //{
            //    var query = (from item in entity.T_PRODUCE_TASK where item.UNIONSTATE==10 orderby item.TASKNUM select item).FirstOrDefault();
            //    if (query != null)
            //    {
            //        var query2 = (from item in entity.T_PRODUCE_TASKLINE
            //                      join item2 in entity.T_PRODUCE_SORTTROUGH
            //                      on item.CIGARETTECODE equals item2.CIGARETTECODE
            //                      where item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20 && item2.STATE == "10" && item.TASKNUM == query.TASKNUM orderby item.CIGARETTECODE
            //                      select new TaskDetail() {   CIGARETTDECODE=item.CIGARETTECODE,  qty=item.QUANTITY??0, SortTroughNum=item2.TROUGHNUM, GroupNO=item2.GROUPNO??0}).ToList();


            //        if (query2 != null && query2.Count > 0)
            //        {
            //            values[0] = query.TASKNUM;
            //            values[1] = query.PACKAGEMACHINE;

            //            decimal qty1=0, qty2=0, qty3=0, qty4=0, qty5=0, qty6=0, qty7=0, qty8=0;
            //            List<String> listCigarettecode = new List<string>();
            //            foreach (var item in query2)
            //            {
            //                int ct=query2.Count(x => x.CIGARETTDECODE == item.CIGARETTDECODE);
            //                if (ct > 1)
            //                {
            //                    if (!listCigarettecode.Contains(item.CIGARETTDECODE))
            //                    listCigarettecode.Add(item.CIGARETTDECODE);
            //                }
            //            }
            //            if (listCigarettecode.Count > 0)
            //            {
            //                foreach (var code in listCigarettecode)
            //                {
            //                    var tempList = query2.FindAll(x => x.CIGARETTDECODE == code).OrderBy(x => x.SortTroughNum).ToList();
            //                   if (tempList != null && tempList.Count>1)
            //                   {
            //                       decimal assignCount=0;
            //                       foreach (var item in tempList)
            //                       {
            //                           if (item.CIGARETTDECODE == FRW || item.CIGARETTDECODE == JBS)
            //                           {

            //                                   item.qty = GetTroughQty(query.TASKNUM, item.SortTroughNum, item.CIGARETTDECODE, item.GroupNO);
            //                           }
            //                           else
            //                           {
            //                               decimal totalCount = item.qty;
            //                               item.qty = Math.Ceiling(item.qty / tempList.Count);
            //                               if (assignCount + item.qty > totalCount)
            //                               {
            //                                   item.qty = totalCount - assignCount;
            //                               }
            //                               assignCount += item.qty;
            //                           }
            //                       }
            //                   }
            //                }
            //            }
            //            foreach (var item in query2)
            //            {
            //                decimal trough=decimal.Parse(item.SortTroughNum);
            //                //
            //                if (trough <= 11)
            //                {
            //                    qty1 += item.qty;
            //                }
            //                else if (12 < trough   && trough <= 22)
            //                {
            //                    qty2 += item.qty;
            //                }
            //                else if (22 < trough && trough <= 33)
            //                {
            //                    qty3 += item.qty;
            //                }
            //                else if (33 < trough && trough <= 44)
            //                {
            //                    qty4 += item.qty;
            //                }
            //                else if (44 < trough && trough <= 55)
            //                {
            //                    qty5 += item.qty;
            //                }
            //                else if (55 < trough && trough <= 66)
            //                {
            //                    qty6 += item.qty;
            //                }
            //                else if (66 < trough && trough <= 77)
            //                {
            //                    qty7 += item.qty;
            //                }
            //                else if (77 < trough && trough <= 88)
            //                {
            //                    qty8 += item.qty;
            //                }
            //            }
            //            values[2] = qty1 + qty2 + qty3 + qty4 + qty5 + qty6 + qty7 + qty8;
            //            values[3] = qty1;
            //            values[4] = qty2;
            //            values[5] = qty3;
            //            values[6] = qty4;
            //            values[7] = qty5;
            //            values[8] = qty6;
            //            values[9] = qty7;
            //            values[10] = qty8;
            //            values[11] = 1;

            //        }
            //        else
            //        {
            //            query.UNIONSTATE = 30;
            //            entity.SaveChanges();
            //        }

            //    }


            //}
            //return values;
            #endregion
        }
        public static String FRW = "1430204";
        public static String JBS = "1430105";
        public static decimal GetTroughQty(decimal tasknum, String troughnum, String cigarettecode, decimal groupNo)
        {


            using (Entities entity = new Entities())
            {

                var query2 = (from item in entity.T_PRODUCE_TASKLINE
                              join item2 in entity.T_PRODUCE_SORTTROUGH
                              on item.CIGARETTECODE equals item2.CIGARETTECODE
                              where item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20 && item2.STATE == "10" && item.TASKNUM == tasknum  //88个分拣烟柜中未发送订单
                              && item.CIGARETTECODE != FRW && item.CIGARETTECODE != JBS //排除芙蓉王 精白沙
                              orderby item.CIGARETTECODE
                              select new TaskDetail() { CIGARETTDECODE = item.CIGARETTECODE, qty = item.QUANTITY ?? 0, SortTroughNum = item2.TROUGHNUM }).Take(300).ToList();

                //找出订单明细信息，除了芙蓉王 精白沙
                if (query2 != null && query2.Count > 0)
                {


                    decimal qty1 = 0, qty2 = 0, qty3 = 0, qty4 = 0, qty5 = 0, qty6 = 0, qty7 = 0, qty8 = 0;  //八组预分拣线
                    List<String> listCigarettecode = new List<string>();
                    foreach (var item in query2)//两大大品牌外的所有品牌
                    {
                        int ct = query2.Count(x => x.CIGARETTDECODE == item.CIGARETTDECODE);
                        // if (ct > 1)
                        // {
                        if (!listCigarettecode.Contains(item.CIGARETTDECODE))
                            listCigarettecode.Add(item.CIGARETTDECODE);
                        // }

                    }
                    if (listCigarettecode.Count > 0)
                    {
                        foreach (var code in listCigarettecode)//循环品牌
                        {
                            var tempList = query2.FindAll(x => x.CIGARETTDECODE == code);
                            if (tempList != null && tempList.Count > 1)
                            {
                                decimal assignCount = 0;
                                foreach (var item in tempList)//将多个烟柜的品牌数量设置好，便于计算各组的卷烟数量
                                {
                                    decimal totalCount = item.qty;
                                    item.qty = Math.Ceiling(item.qty / tempList.Count);
                                    if (assignCount + item.qty > totalCount)
                                    {
                                        item.qty = totalCount - assignCount;
                                    }
                                    assignCount += item.qty;
                                }
                            }
                        }

                        foreach (var item in query2)//计算各组的卷烟数量
                        {
                            decimal trough = decimal.Parse(item.SortTroughNum);
                            //
                            if (trough <= 11)
                            {
                                qty1 += item.qty;
                            }
                            else if (12 < trough && trough <= 22)
                            {
                                qty2 += item.qty;
                            }
                            else if (22 < trough && trough <= 33)
                            {
                                qty3 += item.qty;
                            }
                            else if (33 < trough && trough <= 44)
                            {
                                qty4 += item.qty;
                            }
                            else if (44 < trough && trough <= 55)
                            {
                                qty5 += item.qty;
                            }
                            else if (55 < trough && trough <= 66)
                            {
                                qty6 += item.qty;
                            }
                            else if (66 < trough && trough <= 77)
                            {
                                qty7 += item.qty;
                            }
                            else if (77 < trough && trough <= 88)
                            {
                                qty8 += item.qty;
                            }
                        }

                    }
                    var tCount = (from record in entity.T_PRODUCE_TASK where record.TASKNUM == tasknum select record.TASKQUANTITY).FirstOrDefault();//订单总体数量
                    var fCount = (from record in entity.T_PRODUCE_TASKLINE where record.TASKNUM == tasknum && record.CIGARETTECODE == "1430204" select record.QUANTITY).FirstOrDefault();//芙蓉王数量
                    var jCount = (from record in entity.T_PRODUCE_TASKLINE where record.TASKNUM == tasknum && record.CIGARETTECODE == "1430105" select record.QUANTITY).FirstOrDefault();//经白沙数量
                    if (fCount == null)
                        fCount = 0;
                    if (jCount == null)
                        jCount = 0;
                    var leftFcount = fCount;
                    var leftJcount = jCount;
                    Dictionary<int, decimal> list = new Dictionary<int, decimal>();
                    if (qty1 != 0)
                    {
                        list.Add(1, qty1);
                    }
                    if (qty2 != 0)
                    {
                        list.Add(2, qty2);
                    }
                    if (qty3 != 0)
                    {
                        list.Add(3, qty3);
                    }
                    if (qty4 != 0)
                    {
                        list.Add(4, qty4);
                    }
                    if (qty5 != 0)
                    {
                        list.Add(5, qty5);
                    }
                    if (qty6 != 0)
                    {
                        list.Add(6, qty6);
                    }
                    if (qty7 != 0)
                    {
                        list.Add(7, qty7);
                    }
                    if (qty8 != 0)
                    {
                        list.Add(8, qty8);
                    }
                    list = list.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                    if (list.Count == 0)//说明只有芙蓉王或精白沙，没有其他品牌了
                    {
                        //统计有几个道能用，平均出来，这里没有考虑坏道后平均值的计算不同了。
                        var avgquery = (from item in entity.T_PRODUCE_SORTTROUGH where item.TROUGHTYPE == 10 && item.CIGARETTETYPE == 20 && item.GROUPNO == 1 && item.CIGARETTECODE == cigarettecode orderby item.TROUGHNUM select item).ToList();
                        var avgCount = Math.Ceiling((fCount ?? 0) / avgquery.Count);
                        foreach (var item in avgquery)
                        {
                            if (item.TROUGHNUM == troughnum)
                            {
                                if (avgCount > fCount)
                                {
                                    return fCount ?? 0;
                                }
                                else
                                {
                                    return avgCount;
                                }
                            }
                            else
                            {
                                fCount -= avgCount;
                            }
                        }
                        return 0;


                    }

                    var avg = Math.Ceiling((Decimal)(tCount / list.Count));//看有几组，先算出每组应该分多少烟
                    if (!list.Keys.Contains((int)groupNo))
                    {
                        return 0;
                    }
                    else
                    {

                        foreach (var dic in list)
                        {
                            var needCount = avg - dic.Value;//看还需多少量
                            if (needCount <= 0)
                            {
                                return 0;
                            }
                            if (groupNo == dic.Key)//本分拣组
                            {
                                if (cigarettecode == FRW)
                                {
                                    decimal groupCount = 0;
                                    if (fCount > needCount)//芙蓉王大于需要分配的量
                                    {
                                        fCount = fCount - needCount;
                                        groupCount = needCount;//需要补充的值
                                    }
                                    else//芙蓉王小于需要分配的量
                                    {
                                        groupCount = fCount ?? 0;//芙蓉王的量
                                    }
                                    //看本组中芙蓉王有几个道，支持在同一组中还有多道
                                    var avgquery = (from item in entity.T_PRODUCE_SORTTROUGH where item.TROUGHTYPE == 10 && item.CIGARETTETYPE == 20 && item.GROUPNO == groupNo && item.CIGARETTECODE == cigarettecode orderby item.TROUGHNUM select item).ToList();
                                    var avgCount = Math.Ceiling(groupCount / avgquery.Count);
                                    foreach (var item in avgquery)
                                    {
                                        if (item.TROUGHNUM == troughnum)
                                        {
                                            if (avgCount > groupCount)
                                            {
                                                return groupCount;
                                            }
                                            else
                                            {
                                                return avgCount;
                                            }
                                        }
                                        else
                                        {
                                            groupCount -= avgCount;
                                        }
                                    }
                                }
                                else//精白沙  if (cigarettecode == FRW)
                                {
                                    if (fCount > needCount)//优先芙蓉王，芙蓉王够了就无需精白沙来
                                    {
                                        return 0;
                                    }
                                    else
                                    {
                                        needCount = needCount - (fCount ?? 0);//算出精白沙的需要量
                                        decimal groupCount = 0;
                                        if (jCount > needCount)//完全满足
                                        {
                                            groupCount = needCount;
                                        }
                                        else
                                        {
                                            groupCount = jCount ?? 0;//精白沙也不够
                                        }
                                        var avgquery = (from item in entity.T_PRODUCE_SORTTROUGH where item.TROUGHTYPE == 10 && item.CIGARETTETYPE == 20 && item.GROUPNO == groupNo && item.CIGARETTECODE == cigarettecode orderby item.TROUGHNUM select item).ToList();
                                        var avgCount = Math.Ceiling(groupCount / avgquery.Count);
                                        foreach (var item in avgquery)
                                        {
                                            if (item.TROUGHNUM == troughnum)
                                            {
                                                if (avgCount > groupCount)
                                                {
                                                    return groupCount;
                                                }
                                                else
                                                {
                                                    return avgCount;
                                                }
                                            }
                                            else
                                            {
                                                groupCount -= avgCount;
                                            }
                                        }
                                    }
                                }
                            }
                            else  //if (groupNo == dic.Key)//本分拣组
                            //其他组，芙蓉王和精白沙的数量都要扣除已分配给本组的量
                            {
                                if (fCount > needCount)
                                {
                                    fCount = fCount - needCount;
                                }
                                else
                                {
                                    needCount -= (fCount ?? 0);
                                    fCount = 0;
                                    if (jCount > needCount)
                                    {
                                        jCount = jCount - needCount;

                                    }
                                    else
                                    {
                                        jCount = 0;
                                    }
                                }

                            }

                        }
                    }

                }

                return 0;
            }

        }

        public static void TestTroughValue()
        {

            //using (Entities entity = new Entities())
            //{
            //    String troughno = "";
            //    List<string> result = null;
            //    for (int i = 1; i < 89; i++)
            //    {
            //        result = new List<string>();
            //        troughno = i+"";
            //        //troughno = "76";
            //        var temptroughno = ";" + troughno + ":";
            //        var cigarettecode = (from item in entity.T_PRODUCE_SORTTROUGH where item.TROUGHTYPE == 10 && item.CIGARETTETYPE == 20 && item.TROUGHNUM == troughno select item.CIGARETTECODE).FirstOrDefault();
            //        var query = (from item in entity.T_PRODUCE_TASKLINE
            //                     join item2 in entity.T_PRODUCE_SORTTROUGH on item.CIGARETTECODE equals item2.CIGARETTECODE
            //                     where item.HANDLESTATE == 10 && item2.STATE=="10" && item2.TROUGHNUM == troughno && item.QUANTITY - item.FINISHQUANTITY > 0 && !item.SENDTROUGH.Contains(temptroughno) && item.CIGARETTECODE == cigarettecode && item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20
            //                     orderby item.TASKNUM, item2.TROUGHNUM
            //                     select new TaskDetail { CIGARETTDECODE = item.CIGARETTECODE, DTaskNum = item.TASKNUM, qty = item.QUANTITY??0 , SortTroughNum = item2.TROUGHNUM, GroupNO = item2.GROUPNO ?? 0 }).ToList();
            //        if (query != null && query.Count > 0)
            //        {
            //            List<String> listCigarettecode = new List<string>();
            //            listCigarettecode.Add(cigarettecode);
            //            //foreach (var item in query)
            //            //{
            //            //    int ct = query.Count(x => x.CIGARETTDECODE == item.CIGARETTDECODE);
            //            //    if (ct > 1)
            //            //    {
            //            //        if (!listCigarettecode.Contains(item.CIGARETTDECODE))
            //            //            listCigarettecode.Add(item.CIGARETTDECODE);
            //            //    }
            //            //}
            //            if (listCigarettecode.Count > 0)
            //            {
            //                foreach (var code in listCigarettecode)
            //                {
            //                    var tempList = query.FindAll(x => x.CIGARETTDECODE == code);

            //                    if (tempList != null && tempList.Count >= 1)
            //                    {


            //                        foreach (var item in tempList)
            //                        {
            //                            item.TaskNum = item.DTaskNum + "";
            //                            if (item.CIGARETTDECODE == FRW || item.CIGARETTDECODE == JBS)
            //                            {

            //                                item.qty = GetTroughQty(item.DTaskNum, item.SortTroughNum, item.CIGARETTDECODE, item.GroupNO);

            //                            }
            //                            else
            //                            {
            //                                var q = (from r in entity.T_PRODUCE_SORTTROUGH where r.CIGARETTECODE == cigarettecode && r.TROUGHTYPE == 10  &&r.STATE=="10" && r.CIGARETTETYPE == 20 orderby r.TROUGHNUM select r).ToList();
            //                                decimal totalCount = item.qty;
            //                                if (q.Count> 1)
            //                                {
            //                                    decimal assignCount = 0;
            //                                    var avg  = Math.Ceiling(item.qty / q.Count);
            //                                    foreach (var v in q)
            //                                    {

            //                                        if (v.TROUGHNUM == troughno)
            //                                        {
            //                                            if (assignCount + avg > totalCount)
            //                                            {
            //                                                item.qty = totalCount - assignCount;
            //                                            }
            //                                            else
            //                                            {
            //                                                item.qty = avg;
            //                                            }
            //                                            break;

            //                                        }
            //                                        else
            //                                        {
            //                                            //if (assignCount + avg > totalCount)
            //                                            //{
            //                                            //    item.qty = totalCount - assignCount;
            //                                            //}
            //                                            assignCount += avg;
            //                                        }

            //                                    }
            //                                }

            //                            }
            //                        }
            //                    }

            //                }
            //            }
            //            decimal totalQty = 0;
            //            int j = 0;
            //            foreach (var item in query)
            //            {
            //                if (item.SortTroughNum == troughno)
            //                {
            //                    if (totalQty + item.qty <= 10)
            //                    {
            //                        totalQty += item.qty;
            //                        result.Add(item.TaskNum + "," + item.SortTroughNum + "," + item.CIGARETTDECODE + "," + item.qty);
            //                    }
            //                    else
            //                    {
            //                        //values[0] = totalQty;
            //                        if (j == 0 || totalQty==0)
            //                        {
            //                            result.Add(item.TaskNum + "," + item.SortTroughNum + "," + item.CIGARETTDECODE + "," + item.qty);
            //                            totalQty += item.qty;
            //                        }
            //                        break;
            //                    }
            //                    j++;
            //                }
            //            }
            //            T_WMS_LOG log = new T_WMS_LOG();
            //            decimal id = 0;
            //            id = entity.ExecuteStoreQuery<decimal>("select S_wms_log.nextval from dual").First();

            //            log.ID = id;
            //            //log.ORDRENO = query.TASKNUM + "";
            //            //log.CUSTOMERCODE = query.CUSTOMERCODE;
            //            //log.TOTALQTY = query.TASKQUANTITY;
            //            log.CTYPE = 3;
            //            log.GetType().GetProperty("COL" + troughno).SetValue(log, totalQty, null);


            //            entity.T_WMS_LOG.AddObject(log);
            //            entity.SaveChanges();
            //            TaskService.UpdateMachine(result);

            //        }
            //    }
            //}

        }
        //

        public static List<KeyValuePair<String, List<String>>> InitTask(int groupno1 ,int groupno2)
        {
            List<KeyValuePair<String, List<String>>> tempList = new List<KeyValuePair<String, List<String>>>();
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_PRODUCE_POKE
                             where (item.GROUPNO == groupno1 || item.GROUPNO == groupno2) && item.MACHINESTATE == 12
                             orderby item.MACHINESEQ
                             select item).ToList();
                if (query != null && query.Count > 0)
                {
                    foreach(var item in query)
                    {
                        
                            tempList.Add(new KeyValuePair<String, List<String>>(item.MACHINESEQ + "", new List<String>() { item.SORTNUM + "" }));
                      
                    }
                   tempList= tempList.Distinct().ToList();
                }
            }
            return tempList;
        }
        public static object[] GetTroughValue(String troughno, List<String> result)
        {
            object[] values = new object[5];
            decimal machineseq = decimal.Parse(troughno);
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_PRODUCE_POKE
                             where item.MACHINESEQ == machineseq && item.MACHINESTATE == 10
                             orderby item.SORTNUM
                             select item).FirstOrDefault();
                if (query != null)
                {
                    values[2] = query.MERAGENUM;
                    values[1] = query.UNIONTASKNUM;
                    result.Add(query.UNIONTASKNUM + "");
                }
                else
                {
                    values[1] = 0;
                }
            }
            // values[1] = 1;
            return values;
            //object[] values = new object[2];
            //using (Entities entity = new Entities())
            //{
            //    var temptroughno = ";" + troughno + ":";
            //    var cigarettecode = (from item in entity.T_PRODUCE_SORTTROUGH where item.TROUGHTYPE == 10 && item.CIGARETTETYPE == 20 && item.TROUGHNUM == troughno select item.CIGARETTECODE).FirstOrDefault();
            //    var query = (from item in entity.T_PRODUCE_TASKLINE
            //                 join item2 in entity.T_PRODUCE_SORTTROUGH on item.CIGARETTECODE equals item2.CIGARETTECODE
            //                 where item.HANDLESTATE == 10&& item2.STATE=="10" && item2.TROUGHNUM == troughno && !item.SENDTROUGH.Contains(temptroughno) && item.CIGARETTECODE == cigarettecode && item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20
            //                 orderby item.TASKNUM, item2.TROUGHNUM
            //                 select new TaskDetail { CIGARETTDECODE = item.CIGARETTECODE, DTaskNum = item.TASKNUM, qty = item.QUANTITY ?? 0, SortTroughNum = item2.TROUGHNUM, GroupNO = item2.GROUPNO ?? 0 }).ToList();
            //    if (query != null && query.Count > 0)
            //    {
            //        List<String> listCigarettecode = new List<string>();
            //        listCigarettecode.Add(cigarettecode);
            //        //foreach (var item in query)
            //        //{
            //        //    int ct = query.Count(x => x.CIGARETTDECODE == item.CIGARETTDECODE);
            //        //    if (ct > 1)
            //        //    {
            //        //        if (!listCigarettecode.Contains(item.CIGARETTDECODE))
            //        //            listCigarettecode.Add(item.CIGARETTDECODE);
            //        //    }
            //        //}
            //        if (listCigarettecode.Count > 0)
            //        {
            //            foreach (var code in listCigarettecode)
            //            {
            //                var tempList = query.FindAll(x => x.CIGARETTDECODE == code);

            //                if (tempList != null && tempList.Count >= 1)
            //                {


            //                    foreach (var item in tempList)
            //                    {
            //                        item.TaskNum = item.DTaskNum + "";
            //                        if (item.CIGARETTDECODE == FRW || item.CIGARETTDECODE == JBS)
            //                        {

            //                            item.qty = GetTroughQty(item.DTaskNum, item.SortTroughNum, item.CIGARETTDECODE, item.GroupNO);

            //                        }
            //                        else
            //                        {
            //                            var q = (from r in entity.T_PRODUCE_SORTTROUGH where r.CIGARETTECODE == cigarettecode && r.TROUGHTYPE == 10 && r.STATE == "10" && r.CIGARETTETYPE == 20 orderby r.TROUGHNUM select r).ToList();
            //                            decimal totalCount = item.qty;
            //                            if (q.Count > 1)
            //                            {
            //                                decimal assignCount = 0;
            //                                var avg = Math.Ceiling(item.qty / q.Count);
            //                                foreach (var v in q)
            //                                {

            //                                    if (v.TROUGHNUM == troughno)
            //                                    {
            //                                        if (assignCount + avg > totalCount)
            //                                        {
            //                                            item.qty = totalCount - assignCount;
            //                                        }
            //                                        else
            //                                        {
            //                                            item.qty = avg;
            //                                        }
            //                                        break;

            //                                    }
            //                                    else
            //                                    {
            //                                        //if (assignCount + avg > totalCount)
            //                                        //{
            //                                        //    item.qty = totalCount - assignCount;
            //                                        //}
            //                                        assignCount += avg;
            //                                    }

            //                                }
            //                            }

            //                        }
            //                    }
            //                }

            //            }
            //        }
            //        decimal totalQty = 0;
            //        int j = 0;
            //        foreach (var item in query)
            //        {
            //            if (item.SortTroughNum == troughno)
            //            {
            //                if (totalQty + item.qty <= 10)
            //                {
            //                    totalQty += item.qty;
            //                    result.Add(item.TaskNum + "," + item.SortTroughNum + "," + item.CIGARETTDECODE + "," + item.qty);
            //                }
            //                else
            //                {
            //                    //values[0] = totalQty;
            //                    if (j == 0 || totalQty == 0)
            //                    {
            //                        result.Add(item.TaskNum + "," + item.SortTroughNum + "," + item.CIGARETTDECODE + "," + item.qty);
            //                        totalQty += item.qty;

            //                    }
            //                    values[0] = totalQty;
            //                    break;
            //                }
            //                j++;
            //            }
            //        }

            //    }
            //    else
            //    {
            //        values[0] = 0;
            //    }
            //}

            ////    //    if (listCigarettecode.Count > 0)
            ////    //    {
            ////    //        foreach (var code in listCigarettecode)
            ////    //        {
            ////    //            var tempList = query.FindAll(x => x.CIGARETTDECODE == code);
            ////    //            if (tempList != null && tempList.Count >= 1)
            ////    //            {

            ////    //                decimal assignCount = 0;
            ////    //                foreach (var item in tempList)
            ////    //                {
            ////    //                    item.TaskNum = item.DTaskNum + "";
            ////    //                    if (item.CIGARETTDECODE == FRW || item.CIGARETTDECODE == JBS)
            ////    //                    {

            ////    //                        item.qty = GetTroughQty(item.DTaskNum, item.SortTroughNum, item.CIGARETTDECODE, item.GroupNO);
            ////    //                    }
            ////    //                    else
            ////    //                    {
            ////    //                        decimal totalCount = item.qty;
            ////    //                        item.qty = Math.Ceiling(item.qty / tempList.Count);
            ////    //                        if (assignCount + item.qty > totalCount)
            ////    //                        {
            ////    //                            item.qty = totalCount - assignCount;
            ////    //                        }
            ////    //                        assignCount += item.qty;
            ////    //                    }
            ////    //                }
            ////    //            }
            ////    //        }
            ////    //    }
            ////    //    decimal totalQty = 0;
            ////    //    foreach (var item in query)
            ////    //    {
            ////    //        if (item.SortTroughNum == troughno)
            ////    //        {
            ////    //            if (totalQty + item.qty <= 10)
            ////    //            {
            ////    //                totalQty += item.qty;
            ////    //                result.Add(item.TaskNum + "," + item.SortTroughNum + "," +item.CIGARETTDECODE+","+ item.qty);
            ////    //            }
            ////    //            else
            ////    //            {
            ////    //                values[0] = totalQty;
            ////    //                break;
            ////    //            }
            ////    //        }
            ////    //    }

            ////    //}
            ////    //else
            ////    //{
            ////    //    values[0] = 0;

            ////    //}
            ////}
            //values[1] = 1;
            //return values;

        }
        public static List<TaskDetail> GetCigarette(int begin, int end, decimal troughtype, decimal cigarettetype, int tasknum)
        {
            return null;
            //using (Entities entity = new Entities())
            //{
            //                      var query2 = (from item in entity.T_PRODUCE_TASKLINE
            //                      join item2 in entity.T_PRODUCE_TASK on item.TASKNUM equals item2.TASKNUM
            //                      join item3 in entity.T_PRODUCE_SORTTROUGH on item.CIGARETTECODE equals item3.CIGARETTECODE
            //                      where item3.TROUGHTYPE == troughtype && item3.CIGARETTETYPE == cigarettetype && item3.STATE == "10" && item2.TASKNUM == tasknum
            //                      orderby item.CIGARETTECODE,item3.TROUGHNUM
            //                      select new TaskDetail
            //                      {
            //                          tNum = item.TASKNUM,
            //                          CIGARETTDECODE = item.CIGARETTECODE,
            //                          qty = item.QUANTITY ?? 0,
            //                          SortTroughNum = item3.TROUGHNUM,
            //                          //SortTroughNum=item3.MACHINESEQ??0,
            //                          SORTMACHINE = item2.SORTMACHINE ?? 0,
            //                          CIGARETTDENAME = item.CIGARETTENAME,
            //                          MANTISSA = item3.MANTISSA ?? 0,
            //                          THRESHOLD = item3.THRESHOLD ?? 0,
            //                          GroupNO = item3.GROUPNO ?? 0
            //                      }
            //                 ).ToList();

            //        if (query2 != null)
            //        {
            //            List<String> listCigarettecode = new List<string>();
            //            foreach (var item in query2)
            //            {
            //                int ct = query2.Count(x => x.CIGARETTDECODE == item.CIGARETTDECODE);
            //                if (ct > 1)
            //                {
            //                    if (!listCigarettecode.Contains(item.CIGARETTDECODE))
            //                        listCigarettecode.Add(item.CIGARETTDECODE);
            //                }
            //            }
            //            if (listCigarettecode.Count > 0)
            //            {
            //                foreach (var code in listCigarettecode)
            //                {
            //                    var tempList = query2.FindAll(x => x.CIGARETTDECODE == code);
            //                    if (tempList != null && tempList.Count > 1)
            //                    {
            //                        decimal assignCount = 0;
            //                        foreach (var item in tempList)
            //                        {
            //                            if (item.CIGARETTDECODE == FRW || item.CIGARETTDECODE == JBS)
            //                            {

            //                                item.qty = GetTroughQty( tasknum, item.SortTroughNum, item.CIGARETTDECODE, item.GroupNO);
            //                            }
            //                            else
            //                            {
            //                                decimal totalCount = item.qty;
            //                                item.qty = Math.Ceiling(item.qty / tempList.Count);
            //                                if (assignCount + item.qty > totalCount)
            //                                {
            //                                    item.qty = totalCount - assignCount;
            //                                }
            //                                assignCount += item.qty;
            //                            }
            //                        }
            //                    }
            //                }
            //            }

            //            query2 = query2.FindAll(x => decimal.Parse(x.SortTroughNum) > begin && decimal.Parse(x.SortTroughNum) <= end);
            //            return query2;
            //        }
            //        else
            //        {
            //            return null;
            //        }




            //}


        }



        public static List<T_PRODUCE_TASK> FetchTaskListByRegionCode()
        {
            using (Entities entity = new Entities())
            {
                List<T_PRODUCE_TASK> list = new List<T_PRODUCE_TASK>();
                var query = from item in entity.T_PRODUCE_TASK
                            where item.STATE != "30"
                            group item by new { item.REGIONCODE } into lst
                            select new { REGIONCODE = lst.Key.REGIONCODE };
                foreach (var item in query)
                {
                    list.Add(new T_PRODUCE_TASK
                    {
                        REGIONCODE = item.REGIONCODE,
                    });
                }
                return list.OrderBy(o => o.REGIONCODE).ToList();
            }
        }
    }
}
