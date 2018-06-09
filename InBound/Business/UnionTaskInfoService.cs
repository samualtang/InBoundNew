﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Model;

namespace InBound.Business
{
    public class UnionTaskInfoService : BaseService
    {

        public static List<UnionTaskInfo> GetUnionTaskInfoAfter(int mainbelt, int groupno, decimal sortnum, decimal xyNum)
        {
            List<UnionTaskInfo> info = new List<UnionTaskInfo>();

            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_PRODUCE_POKE where item.SORTNUM < sortnum orderby item.SORTNUM  descending select item).FirstOrDefault();
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
                                            qty = titem.POKENUM
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
                                                qty = 10 - tempcount
                                            });
                                            titem.POKENUM = titem.POKENUM - (10 - tempcount);
                                        }
                                        else
                                        {
                                            break;
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
                                            qty = titem.POKENUM
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
                                                qty = 10 - tempcount
                                            });
                                            titem.POKENUM = titem.POKENUM - (10 - tempcount);
                                        }
                                        else
                                        {
                                            break;
                                        }
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


        public static List<UnionTaskInfo> GetUnionTaskInfoBefore(int mainbelt, int groupnoCurrent, decimal sortnumCurrent, decimal xyNum,decimal sortNumFirst,decimal xyNumFirst)
        {
            List<UnionTaskInfo> info = new List<UnionTaskInfo>();
            using(Entities entity=new Entities())
            {
                var query = (from item in entity.T_PRODUCE_POKE where item.SORTNUM == sortnumCurrent orderby item.MACHINESEQ select item).ToList();

                decimal totalCount = Math.Ceiling(xyNum / 10);//当前第几波
            }
            return info;
        }
    }
}