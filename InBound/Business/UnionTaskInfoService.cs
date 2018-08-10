using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Model;

namespace InBound.Business
{
    public class UnionTaskInfoService : BaseService
    {

        public static decimal getNextSortNum(decimal sortnum)
        {
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_PRODUCE_POKE where item.SORTNUM == sortnum select item).FirstOrDefault();
                if (query == null)
                {
                    return 0;
                }
                else
                {
                    var querynext = (from item in entity.T_PRODUCE_POKE where item.SORTNUM > sortnum && item.MAINBELT==query.MAINBELT orderby item.SORTNUM select item).FirstOrDefault();
                    if (querynext
                        != null)
                    {
                        return querynext.SORTNUM??0;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
        public static decimal getBeforeSortNum(decimal sortnum)
        {
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_PRODUCE_POKE where item.SORTNUM == sortnum select item).FirstOrDefault();
                if (query == null)
                {
                    return 0;
                }
                else
                {
                    var querynext = (from item in entity.T_PRODUCE_POKE where item.SORTNUM < sortnum && item.MAINBELT == query.MAINBELT orderby item.SORTNUM descending select item).FirstOrDefault();
                    if (querynext
                        != null)
                    {
                        return querynext.SORTNUM ?? 0;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
        public static int GetPokeCount()
        {
            using (Entities entity = new Entities())
            {
                var count = (from item in entity.T_PRODUCE_POKE
                             select item).Sum(a => a.POKENUM);
                return Convert.ToInt32(count);
            }
        }

        public static int GetPokeSEQCount()
        {
            using (Entities entity = new Entities())
            {
                var count = (from item in entity.T_PRODUCE_POKESEQ
                             select item).Sum(a => a.POKENUM);
                return Convert.ToInt32(count);
            }
        }
        /// <summary>
        /// 条烟识别
        /// </summary>
        public static void InsertPokeseqInfo()
        {
            using (Entities entity = new Entities())
            {
              
               
                var listsortnum = GetAllSortnum();
                foreach (var item in listsortnum)
                {

                    List<UnionTaskInfo> info = GetUnionTaskInfo(item).ToList();
                    foreach (var union in info)
                    {
                        T_PRODUCE_POKESEQ poke = new T_PRODUCE_POKESEQ();
                        poke.POKEID = GetSeq("select t_produce_pokeseq_pokeid.Nextval from dual");
                        poke.TROUGHNUM = union.TROUGHNUM;
                        poke.POKENUM = union.POKENUM;
                        poke.SORTSTATE = 20;
                        poke.TASKQTY = union.TASKQTY;
                        poke.TASKNUM = union.TASKNUM;
                        poke.MACHINESEQ = union.machineseq;
                        poke.MAINBELT = union.MainBelt;
                        poke.GROUPNO = union.groupno;
                        poke.PACKAGEMACHINE = union.PACKAGEMACHINE;
                        poke.SORTNUM = union.SortNum;
                        poke.BILLCODE = union.BILLCODE;
                        poke.CIGARETTECODE = union.CIGARETTDECODE;
                        poke.CIGARETTENAME = union.CIGARETTDENAME;
                        entity.AddToT_PRODUCE_POKESEQ(poke); 
                    }
                    entity.SaveChanges();
                }
               
            }
        }
        public static List<UnionTaskInfo> GetUnionAllTaskInfo()
        {
            List<UnionTaskInfo> list = new List<UnionTaskInfo>();
            using (Entities entity = new Entities())
            { 
                var listsortnum = GetAllSortnum();
                foreach (var item in listsortnum)
                { 
                    List<UnionTaskInfo> info = GetUnionTaskInfo(item); 
                    list.AddRange(info); 
                }
                if (list != null)
                {
                    return list;
                }
                else
                {
                    return null;
                }
            }
        }
        
        /// <summary>
        /// 获取所有任务号
        /// </summary>
        /// <returns></returns>
        public static List<decimal> GetAllSortnum()
        {
            List<decimal> sortnum = new List<decimal>();
            using (Entities entity = new Entities())
            {
                var sortquery = (from item in entity.T_PRODUCE_POKE
                                 orderby item.SORTNUM
                                 select item).Select(a=>  new {SORTNUM = a.SORTNUM ??0}).Distinct().OrderBy(x=> x.SORTNUM ).ToList();
                var sortpokeseq = (from item in entity.T_PRODUCE_POKESEQ
                                  orderby item.SORTNUM
                                   select item).Select(a => new { SORTNUM = a.SORTNUM ?? 0 }).Distinct().OrderBy(x => x.SORTNUM).ToList();
                var fnailySortpoke = sortquery.Except(sortpokeseq).ToList();
                foreach (var item in fnailySortpoke)
                {
                    sortnum.Add(item.SORTNUM);
                }
            }
            return sortnum;
        }
        /// <summary>
        /// 获取当前任务号卷烟进入包装机的顺序
        /// </summary>
        /// <param name="sortnum">任务号</param>
        /// <returns>卷烟顺序</returns>
        public static List<UnionTaskInfo> GetUnionTaskInfo(decimal sortnum)
        {
            using (Entities entity = new Entities())
            {
                List<UnionTaskInfo> info = new List<UnionTaskInfo>();

                var query = (from item in entity.T_PRODUCE_POKE where item.SORTNUM == sortnum select item).FirstOrDefault();

                if (query != null)
                {
                    //foreach (var queryitem in query)
                    //{
                        var task = (from item in entity.T_PRODUCE_POKE
                                    join item2 in entity.T_PRODUCE_SORTTROUGH
                                        on item.TROUGHNUM equals item2.TROUGHNUM
                                        join item3 in entity.T_PRODUCE_TASK on item.BILLCODE equals item3.BILLCODE
                                    where item.SORTNUM == query.SORTNUM && item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20
                                    orderby item.MACHINESEQ
                                    select
                                        new TaskDetail()
                                        {
                                             Billcode=item3.BILLCODE,
                                             CUSTOMERNAME=item3.CUSTOMERNAME,
                                             SORTSEQ=item3.SORTSEQ??0,
                                            CIGARETTDECODE = item2.CIGARETTECODE,
                                            CIGARETTDENAME = item2.CIGARETTENAME,
                                            GroupNO = item.GROUPNO ?? 0,
                                            Machineseq = item.MACHINESEQ ?? 0,
                                            MainBelt = item.MAINBELT ?? 0,
                                            SortNum= item.SORTNUM ?? 0,
                                            POKENUM = item.POKENUM ?? 0,
                                            MachineState = item.MACHINESTATE ?? 0,
                                             PACKAGEMACHINE = item.PACKAGEMACHINE ?? 0,
                                             TROUGHNUM = item.TROUGHNUM,
                                             TaskNum = item.TASKNUM ?? 0,
                                             TASKQTY = item.TASKQTY ?? 0,
                                        }).ToList();

                        var exitLoop = false;
                        while (!exitLoop)
                        {
                            for (int i = 1; i <= 8; i++)
                            {
                                if (i == 1)
                                {
                                    exitLoop = true;
                                }
                                int tempgroupno = i;
                                if (i == 3)
                                {
                                    tempgroupno = 4;
                                }
                                else if (i == 4)
                                {
                                    tempgroupno = 3;
                                }
                                else if (i == 7)
                                {
                                    tempgroupno = 8;
                                }
                                else if (i == 8)
                                {
                                    tempgroupno = 7;
                                }

                                var temptask = task.Where(x => x.GroupNO == tempgroupno && x.MachineState != 30).OrderBy(y => y.Machineseq).ToList();
                                if (temptask != null && temptask.Count > 0)
                                {
                                    exitLoop = false;
                                    decimal tempcount = 0;
                                    foreach (var titem in temptask)
                                    {
                                        if (tempcount + titem.POKENUM <= 10)
                                        {
                                            info.Add(new UnionTaskInfo()
                                            {
                                                CUSTOMERNAME = titem.CUSTOMERNAME,
                                                BILLCODE = titem.Billcode,
                                                SORTSEQ = titem.SORTSEQ,
                                                CIGARETTDECODE = titem.CIGARETTDECODE,
                                                CIGARETTDENAME = titem.CIGARETTDENAME,
                                                MainBelt = titem.MainBelt,
                                                SortNum = titem.SortNum,
                                                POKENUM = titem.POKENUM,
                                                groupno = titem.GroupNO,
                                                machineseq = titem.Machineseq,
                                                IsOnMainBelt = 0,
                                                PACKAGEMACHINE = titem.PACKAGEMACHINE,
                                                TASKNUM = titem.TaskNum,
                                                TASKQTY = titem.TASKQTY,
                                                TROUGHNUM = titem.TROUGHNUM,
                                            });
                                            titem.MachineState = 30;
                                            tempcount += titem.POKENUM;
                                        }
                                        else
                                        {
                                            if (tempcount < 10)
                                            {
                                                info.Add(new UnionTaskInfo()
                                                {
                                                    CUSTOMERNAME = titem.CUSTOMERNAME,
                                                    BILLCODE = titem.Billcode,
                                                    SORTSEQ = titem.SORTSEQ,
                                                    CIGARETTDECODE = titem.CIGARETTDECODE,
                                                    CIGARETTDENAME = titem.CIGARETTDENAME,
                                                    MainBelt = titem.MainBelt,
                                                    SortNum = titem.SortNum,
                                                    POKENUM = 10 - tempcount,
                                                    groupno = titem.GroupNO,
                                                    machineseq = titem.Machineseq,
                                                    IsOnMainBelt = 0,
                                                    PACKAGEMACHINE = titem.PACKAGEMACHINE,
                                                    TASKNUM = titem.TaskNum,
                                                    TASKQTY = titem.TASKQTY,
                                                    TROUGHNUM = titem.TROUGHNUM,
                                                });
                                                titem.POKENUM = titem.POKENUM - (10 - tempcount);
                                            }
                                            //else
                                            //{
                                            break;
                                            //}
                                        }
                                    }
                                }
                            }

                        }

                    //}
             
                }
                return info;
            }
 
        }

        public static List<UnionTaskInfo> GetUnionTaskInfoAfter(int mainbelt, int groupno, decimal sortnum, decimal xyNum, int orderNum)
        {
            List<UnionTaskInfo> info = new List<UnionTaskInfo>();

            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_PRODUCE_POKE where item.SORTNUM < sortnum && item.MAINBELT == mainbelt orderby item.SORTNUM descending select item).Take(orderNum).OrderBy(x=>x.SORTNUM).ToList();

                if (query != null)
                {
                    foreach (var queryitem in query)
                    {
                        var task = (from item in entity.T_PRODUCE_POKE
                                    join item2 in entity.T_PRODUCE_SORTTROUGH
                                        on item.TROUGHNUM equals item2.TROUGHNUM
                                    where item.SORTNUM == queryitem.SORTNUM && item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20
                                    orderby item.MACHINESEQ
                                    select
                                        new TaskDetail()
                                        {
                                            CIGARETTDECODE = item2.CIGARETTECODE,
                                            CIGARETTDENAME = item2
                                                .CIGARETTENAME,
                                            GroupNO = item.GROUPNO ?? 0,
                                            Machineseq = item.MACHINESEQ ?? 0,
                                            MainBelt = item.MAINBELT ?? 0,
                                            SortNum
                                                = item.SORTNUM ?? 0,
                                            POKENUM = item.POKENUM ?? 0,
                                            MachineState = item.MACHINESTATE ?? 0
                                        }).ToList();

                        var exitLoop = false;
                        while (!exitLoop)
                        {
                            for (int i = 1; i <= 8; i++)
                            {
                                if (i == 1)
                                {
                                    exitLoop = true;
                                }
                                int tempgroupno = i;
                                if (i == 3)
                                {
                                    tempgroupno = 4;
                                }
                                else if (i == 4)
                                {
                                    tempgroupno = 3;
                                }
                                else if (i == 7)
                                {
                                    tempgroupno = 8;
                                }
                                else if (i == 8)
                                {
                                    tempgroupno = 7;
                                }

                                var temptask = task.Where(x => x.GroupNO == tempgroupno && x.MachineState != 30).OrderBy(y => y.Machineseq).ToList();
                                if (temptask != null && temptask.Count > 0)
                                {
                                    exitLoop = false;
                                    decimal tempcount = 0;
                                    foreach (var titem in temptask)
                                    {
                                        if (tempcount + titem.POKENUM <= 10)
                                        {
                                            info.Insert(0, new UnionTaskInfo()
                                            {
                                                CIGARETTDECODE = titem.CIGARETTDECODE,
                                                CIGARETTDENAME = titem.CIGARETTDENAME,
                                                MainBelt = titem.MainBelt,
                                                SortNum = titem.SortNum,
                                                POKENUM = titem.POKENUM,
                                                 groupno=titem.GroupNO,
                                                  machineseq=titem.Machineseq
                                            });
                                            titem.MachineState = 30;
                                            tempcount += titem.POKENUM;
                                        }
                                        else
                                        {
                                            if (tempcount < 10)
                                            {
                                                info.Insert(0, new UnionTaskInfo()
                                                {
                                                    CIGARETTDECODE = titem.CIGARETTDECODE,
                                                    CIGARETTDENAME = titem.CIGARETTDENAME,
                                                    MainBelt = titem.MainBelt,
                                                    SortNum = titem.SortNum,
                                                    POKENUM = 10 - tempcount,
                                                    groupno = titem.GroupNO,
                                                    machineseq = titem.Machineseq
                                                });
                                                titem.POKENUM = titem.POKENUM - (10 - tempcount);
                                            }
                                            //else
                                            //{
                                                break;
                                            //}
                                        }
                                    }
                                }
                            }

                        }

                    }
                    //计算下一波

                    var taskCurrent = (from item in entity.T_PRODUCE_POKE
                                       join item2 in entity.T_PRODUCE_SORTTROUGH
                                           on item.TROUGHNUM equals item2.TROUGHNUM
                                       where item.SORTNUM == sortnum && item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20
                                       orderby item.MACHINESEQ
                                       select
                                           new TaskDetail()
                                           {
                                               CIGARETTDECODE = item2.CIGARETTECODE,
                                               CIGARETTDENAME = item2
                                                   .CIGARETTENAME,
                                               GroupNO = item.GROUPNO ?? 0,
                                               Machineseq = item.MACHINESEQ ?? 0,
                                               MainBelt = item.MAINBELT ?? 0,
                                               SortNum
                                                   = item.SORTNUM ?? 0,
                                               POKENUM = item.POKENUM ?? 0,
                                               MachineState = item.MACHINESTATE ?? 0
                                           }).ToList();
                    decimal totalCount = Math.Ceiling(xyNum / 10);
                    if (totalCount == 0)
                    {
                        totalCount = 1;
                    }
                    var exitLoopCurrent = false;
                    decimal currentCount = 0;
                    while (!exitLoopCurrent)
                    {
                        currentCount += 1;
                        for (int i = 1; i <= 8; i++)
                        {
                            int tempgroupno = i;
                            if (i == 3)
                            {
                                tempgroupno = 4;
                            }
                            else if (i == 4)
                            {
                                tempgroupno = 3;
                            }
                            else if (i == 7)
                            {
                                tempgroupno = 8;
                            }
                            else if (i == 8)
                            {
                                tempgroupno = 7;
                            }
                            var temptask = taskCurrent.Where(x => x.GroupNO == tempgroupno && x.MachineState != 30).OrderBy(y => y.Machineseq).ToList();
                            if (temptask != null && temptask.Count > 0)
                            {

                                decimal tempcount = 0;
                                foreach (var titem in temptask)
                                {
                                    if (tempcount + titem.POKENUM <= 10)
                                    {
                                        info.Insert(0, new UnionTaskInfo()
                                        {
                                            CIGARETTDECODE = titem.CIGARETTDECODE,
                                            CIGARETTDENAME = titem.CIGARETTDENAME,
                                            MainBelt = titem.MainBelt,
                                            SortNum = titem.SortNum,
                                            POKENUM = titem.POKENUM,
                                            groupno = titem.GroupNO,
                                            machineseq = titem.Machineseq
                                        });
                                        titem.MachineState = 30;
                                        tempcount += titem.POKENUM;
                                    }
                                    else
                                    {
                                        if (tempcount < 10)
                                        {
                                            info.Insert(0, new UnionTaskInfo()
                                            {
                                                CIGARETTDECODE = titem.CIGARETTDECODE,
                                                CIGARETTDENAME = titem.CIGARETTDENAME,
                                                MainBelt = titem.MainBelt,
                                                SortNum = titem.SortNum,
                                                POKENUM = 10 - tempcount,
                                                groupno = titem.GroupNO,
                                                machineseq = titem.Machineseq
                                            });
                                            titem.POKENUM = titem.POKENUM - (10 - tempcount);
                                        }
                                        //else
                                        //{
                                            break;
                                        //}
                                    }
                                }
                            }
                            if (currentCount == totalCount && tempgroupno == groupno)
                            {
                                exitLoopCurrent = true;
                                break;
                            }
                        }

                    }


                }
            }


            return info;

        }


        public static List<UnionTaskInfo> GetUnionTaskInfoAfter(int mainbelt, int groupno, decimal sortnum, decimal xyNum)
        {
            List<UnionTaskInfo> info = new List<UnionTaskInfo>();

            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_PRODUCE_POKE where item.SORTNUM < sortnum && item.MAINBELT==mainbelt orderby item.SORTNUM  descending select item).FirstOrDefault();

                if (query != null)
                {
                    var task = (from item in entity.T_PRODUCE_POKE join item2 in entity.T_PRODUCE_SORTTROUGH
                                on item.TROUGHNUM equals item2.TROUGHNUM
                                where item.SORTNUM == query.SORTNUM && item2.TROUGHTYPE==10 && item2.CIGARETTETYPE==20 orderby  item.MACHINESEQ select 
                                new TaskDetail(){ CIGARETTDECODE=item2.CIGARETTECODE, CIGARETTDENAME=item2
                                .CIGARETTENAME, GroupNO=item.GROUPNO??0, Machineseq=item.MACHINESEQ??0, MainBelt=item.MAINBELT??0, SortNum
                                =item.SORTNUM??0, POKENUM=item.POKENUM??0, MachineState=item.MACHINESTATE??0}).ToList();

                    var exitLoop = false;
                    while (!exitLoop)
                    {
                        for (int i = 1; i <= 8; i++)
                        {
                            if (i == 1)
                            {
                                exitLoop = true;
                            }
                            int tempgroupno = i;
                            if (i == 3)
                            {
                                tempgroupno = 4;
                            }
                            else if (i == 4)
                            {
                                tempgroupno = 3;
                            }
                            else if (i == 7)
                            {
                                tempgroupno = 8;
                            }
                            else if (i == 8)
                            {
                                tempgroupno = 7;
                            }

                            var temptask = task.Where(x => x.GroupNO == tempgroupno && x.MachineState != 30).OrderBy(y => y.Machineseq).ToList();
                            if (temptask != null && temptask.Count > 0)
                            {
                                exitLoop = false;
                                decimal tempcount = 0;
                                foreach (var titem in temptask)
                                {
                                    if (tempcount + titem.POKENUM <= 10)
                                    {
                                        info.Insert(0, new UnionTaskInfo()
                                        {
                                            CIGARETTDECODE = titem.CIGARETTDECODE,
                                            CIGARETTDENAME = titem.CIGARETTDENAME,
                                            MainBelt = titem.MainBelt,
                                            SortNum = titem.SortNum,
                                            POKENUM = titem.POKENUM,
                                            groupno = titem.GroupNO,
                                            machineseq = titem.Machineseq
                                        });
                                        titem.MachineState = 30;
                                        tempcount += titem.POKENUM;
                                    }
                                    else
                                    {
                                        if (tempcount < 10)
                                        {
                                            info.Insert(0, new UnionTaskInfo()
                                            {
                                                CIGARETTDECODE = titem.CIGARETTDECODE,
                                                CIGARETTDENAME = titem.CIGARETTDENAME,
                                                MainBelt = titem.MainBelt,
                                                SortNum = titem.SortNum,
                                                POKENUM = 10 - tempcount,
                                                groupno = titem.GroupNO,
                                                machineseq = titem.Machineseq
                                            });
                                            titem.POKENUM = titem.POKENUM - (10 - tempcount);
                                        }
                                        //else
                                        //{
                                            break;
                                        //}
                                    }
                                }
                            }
                        }

                    }

                    //计算下一波

                    var taskCurrent = (from item in entity.T_PRODUCE_POKE
                                join item2 in entity.T_PRODUCE_SORTTROUGH
                                    on item.TROUGHNUM equals item2.TROUGHNUM
                                where item.SORTNUM == sortnum && item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20
                                orderby item.MACHINESEQ
                                select
                                    new TaskDetail()
                                    {
                                        CIGARETTDECODE = item2.CIGARETTECODE,
                                        CIGARETTDENAME = item2
                                            .CIGARETTENAME,
                                        GroupNO = item.GROUPNO ?? 0,
                                        Machineseq = item.MACHINESEQ ?? 0,
                                        MainBelt = item.MAINBELT ?? 0,
                                        SortNum
                                            = item.SORTNUM ?? 0,
                                        POKENUM = item.POKENUM ?? 0,
                                        MachineState = item.MACHINESTATE ?? 0
                                    }).ToList();
                    decimal totalCount = Math.Ceiling(xyNum / 10);
                    if (totalCount == 0)
                    {
                        totalCount = 1;
                    }
                    var exitLoopCurrent = false;
                    decimal currentCount = 0;
                    while (!exitLoopCurrent)
                    {
                        currentCount += 1;
                        for (int i = 1; i <= 8; i++)
                        {
                            int tempgroupno = i;
                            if (i == 3)
                            {
                                tempgroupno = 4;
                            }
                            else if (i == 4)
                            {
                                tempgroupno = 3;
                            }
                            else if (i == 7)
                            {
                                tempgroupno = 8;
                            }
                            else if (i == 8)
                            {
                                tempgroupno = 7;
                            }
                            var temptask = taskCurrent.Where(x => x.GroupNO == tempgroupno && x.MachineState != 30).OrderBy(y => y.Machineseq).ToList();
                            if (temptask != null && temptask.Count > 0)
                            {
                               
                                decimal tempcount = 0;
                                foreach (var titem in temptask)
                                {
                                    if (tempcount + titem.POKENUM <= 10)
                                    {
                                        info.Insert(0, new UnionTaskInfo()
                                        {
                                            CIGARETTDECODE = titem.CIGARETTDECODE,
                                            CIGARETTDENAME = titem.CIGARETTDENAME,
                                            MainBelt = titem.MainBelt,
                                            SortNum = titem.SortNum,
                                            POKENUM = titem.POKENUM,
                                            groupno = titem.GroupNO,
                                            machineseq = titem.Machineseq
                                        });
                                        titem.MachineState = 30;
                                        tempcount += titem.POKENUM;
                                    }
                                    else
                                    {
                                        if (tempcount < 10)
                                        {
                                            info.Insert(0, new UnionTaskInfo()
                                            {
                                                CIGARETTDECODE = titem.CIGARETTDECODE,
                                                CIGARETTDENAME = titem.CIGARETTDENAME,
                                                MainBelt = titem.MainBelt,
                                                SortNum = titem.SortNum,
                                                POKENUM = 10 - tempcount,
                                                groupno = titem.GroupNO,
                                                machineseq = titem.Machineseq
                                            });
                                            titem.POKENUM = titem.POKENUM - (10 - tempcount);
                                        }
                                        //else
                                        //{
                                            break;
                                        //}
                                    }
                                }
                            }
                            if (currentCount == totalCount && tempgroupno == groupno)
                            {
                                exitLoopCurrent = true;
                                break;
                            }
                        }

                    }


                }
            }


            return info;
 
        }


        public static List<UnionTaskInfo> GetUnionTaskInfoBefore(int mainbelt, int groupnoCurrent, decimal sortnumCurrent, decimal xyNum)
        {
            List<UnionTaskInfo> info = new List<UnionTaskInfo>();
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_PRODUCE_POKE where item.SORTNUM > sortnumCurrent && item.MAINBELT == mainbelt orderby item.SORTNUM select item).FirstOrDefault();

                decimal totalCount = Math.Ceiling(xyNum / 10);//当前第几波
                //先初始化
                
                    var task = (from item in entity.T_PRODUCE_POKE
                                join item2 in entity.T_PRODUCE_SORTTROUGH
                                    on item.TROUGHNUM equals item2.TROUGHNUM
                                where item.SORTNUM == sortnumCurrent && item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20
                                orderby item.MACHINESEQ
                                select
                                    new TaskDetail()
                                    {
                                        CIGARETTDECODE = item2.CIGARETTECODE,
                                        CIGARETTDENAME = item2
                                            .CIGARETTENAME,
                                        GroupNO = item.GROUPNO ?? 0,
                                        Machineseq = item.MACHINESEQ ?? 0,
                                        MainBelt = item.MAINBELT ?? 0,
                                        SortNum
                                            = item.SORTNUM ?? 0,
                                        POKENUM = item.POKENUM ?? 0,
                                        MachineState = item.MACHINESTATE ?? 0
                                    }).ToList();
                    for (int i = 1; i < groupnoCurrent; i++)
                    {
                        int tempgroupno = i;
                        if (i == 3)
                        {
                            tempgroupno = 4;
                        }
                        else if (i == 4)
                        {
                            tempgroupno = 3;
                        }
                        else if (i == 7)
                        {
                            tempgroupno = 8;
                        }
                        else if (i == 8)
                        {
                            tempgroupno = 7;
                        }
                        var temptask = task.Where(x => x.GroupNO == tempgroupno && x.MachineState != 30).OrderBy(y => y.Machineseq).ToList();
                        if (temptask != null && temptask.Count > 0)
                        {

                            decimal tempcount = 0;
                            foreach (var titem in temptask)
                            {
                                if (tempcount + titem.POKENUM <= totalCount*10)
                                {
                                  
                                    titem.MachineState = 30;
                                    tempcount += titem.POKENUM;
                                }
                                else
                                {
                                    if (tempcount < totalCount * 10)
                                    {

                                        titem.POKENUM = titem.POKENUM - (totalCount * 10 - tempcount);
                                    }
                                    //else
                                    //{
                                        break;
                                   // }
                                }
                            }
                        }
                    }

                    var exitLoop = false;
                    #region
                    while (!exitLoop && groupnoCurrent != 1)
                    {
                        for (int i = 1; i < groupnoCurrent; i++)
                        {
                            if (i == 1)
                            {
                                exitLoop = true;
                            }
                            int tempgroupno = i;
                            if (i == 3)
                            {
                                tempgroupno = 4;
                            }
                            else if (i == 4)
                            {
                                tempgroupno = 3;
                            }
                            else if (i == 7)
                            {
                                tempgroupno = 8;
                            }
                            else if (i == 8)
                            {
                                tempgroupno = 7;
                            }

                            var temptask = task.Where(x => x.GroupNO == tempgroupno && x.MachineState != 30).OrderBy(y => y.Machineseq).ToList();
                            if (temptask != null && temptask.Count > 0)
                            {
                                exitLoop = false;
                                decimal tempcount = 0;
                                foreach (var titem in temptask)
                                {
                                    if (tempcount + titem.POKENUM <= 10)
                                    {
                                        info.Insert(0, new UnionTaskInfo()
                                        {
                                            CIGARETTDECODE = titem.CIGARETTDECODE,
                                            CIGARETTDENAME = titem.CIGARETTDENAME,
                                            MainBelt = titem.MainBelt,
                                            SortNum = titem.SortNum,
                                            POKENUM = titem.POKENUM,
                                            groupno = titem.GroupNO,
                                            machineseq = titem.Machineseq
                                        });
                                        titem.MachineState = 30;
                                        tempcount += titem.POKENUM;
                                    }
                                    else
                                    {
                                        if (tempcount < 10)
                                        {
                                            info.Insert(0, new UnionTaskInfo()
                                            {
                                                CIGARETTDECODE = titem.CIGARETTDECODE,
                                                CIGARETTDENAME = titem.CIGARETTDENAME,
                                                MainBelt = titem.MainBelt,
                                                SortNum = titem.SortNum,
                                                POKENUM = 10 - tempcount,
                                                groupno = titem.GroupNO,
                                                machineseq = titem.Machineseq
                                            });
                                            titem.POKENUM = titem.POKENUM - (10 - tempcount);
                                        }
                                        //else
                                        //{
                                            break;
                                        //}
                                    }
                                }
                            }
                        }

                    }
#endregion
                    if (query != null)
                    {
                    var taskCurrent = (from item in entity.T_PRODUCE_POKE
                                join item2 in entity.T_PRODUCE_SORTTROUGH
                                    on item.TROUGHNUM equals item2.TROUGHNUM
                                where item.SORTNUM == query.SORTNUM && item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20
                                orderby item.MACHINESEQ
                                select
                                    new TaskDetail()
                                    {
                                        CIGARETTDECODE = item2.CIGARETTECODE,
                                        CIGARETTDENAME = item2
                                            .CIGARETTENAME,
                                        GroupNO = item.GROUPNO ?? 0,
                                        Machineseq = item.MACHINESEQ ?? 0,
                                        MainBelt = item.MAINBELT ?? 0,
                                        SortNum
                                            = item.SORTNUM ?? 0,
                                        POKENUM = item.POKENUM ?? 0,
                                        MachineState = item.MACHINESTATE ?? 0
                                    }).ToList();

                    var exitLoopNext = false;
                    while (!exitLoopNext && groupnoCurrent != 1)
                    {
                        for (int i = 1; i <groupnoCurrent; i++)
                        {
                            if (i == 1)
                            {
                                exitLoopNext = true;
                            }
                            int tempgroupno = i;
                            if (i == 3)
                            {
                                tempgroupno = 4;
                            }
                            else if (i == 4)
                            {
                                tempgroupno = 3;
                            }
                            else if (i == 7)
                            {
                                tempgroupno = 8;
                            }
                            else if (i == 8)
                            {
                                tempgroupno = 7;
                            }

                            var temptask = taskCurrent.Where(x => x.GroupNO == tempgroupno && x.MachineState != 30).OrderBy(y => y.Machineseq).ToList();
                            if (temptask != null && temptask.Count > 0)
                            {
                                exitLoopNext = false;
                                decimal tempcount = 0;
                                foreach (var titem in temptask)
                                {
                                    if (tempcount + titem.POKENUM <= 10)
                                    {
                                        info.Add(new UnionTaskInfo()
                                        {
                                            CIGARETTDECODE = titem.CIGARETTDECODE,
                                            CIGARETTDENAME = titem.CIGARETTDENAME,
                                            MainBelt = titem.MainBelt,
                                            SortNum = titem.SortNum,
                                            POKENUM = titem.POKENUM,
                                            groupno = titem.GroupNO,
                                            machineseq = titem.Machineseq
                                        });
                                        titem.MachineState = 30;
                                        tempcount += titem.POKENUM;
                                    }
                                    else
                                    {
                                        if (tempcount < 10)
                                        {
                                            info.Add(new UnionTaskInfo()
                                            {
                                                CIGARETTDECODE = titem.CIGARETTDECODE,
                                                CIGARETTDENAME = titem.CIGARETTDENAME,
                                                MainBelt = titem.MainBelt,
                                                SortNum = titem.SortNum,
                                                POKENUM = 10 - tempcount,
                                                groupno = titem.GroupNO,
                                                machineseq = titem.Machineseq
                                            });
                                            titem.POKENUM = titem.POKENUM - (10 - tempcount);
                                        }
                                        //else
                                        //{
                                            break;
                                        //}
                                    }
                                }
                            }
                        }

                    }
                }
            }
            return info;
        }
    }
}
