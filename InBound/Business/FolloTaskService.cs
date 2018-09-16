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
            try
            { 
                using (Entities dataentity = new Entities())
                {
                    var query = from item in dataentity.T_PRODUCE_TASK orderby item.SORTNUM select item;
                    return query.ToList();
                } 
            }
            catch (Exception ex)
            { 
                throw  ex;
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
                            select new FollowTaskDeail()
                            {
                                CIGARETTDECODE = item2.CIGARETTECODE,
                                CIGARETTDENAME = item2.CIGARETTENAME,
                                Machineseq = item.MACHINESEQ ?? 0,
                                SortNum = item.SORTNUM ?? 0,
                                tNum = item.POKENUM ?? 0,
                                Billcode = item.BILLCODE,
                                SortState = item.SORTSTATE ?? 0
                            };
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
                    return query.OrderBy(x => x.SortNum).ToList();
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
                            join item2 in dataentity.T_PRODUCE_SORTTROUGH
                            on item.TROUGHNUM equals item2.TROUGHNUM 
                            orderby item.SORTNUM
                            select new FollowTaskDeail()
                            {
                                SortNum = item.SORTNUM ?? 0,
                                MERAGENUM = item.MERAGENUM ?? 0,
                                POKENUM = item.POKENUM ?? 0,
                                MainBelt = item.MAINBELT ?? 0,
                                CIGARETTDECODE = item2.CIGARETTECODE,
                                CIGARETTDENAME = item2.CIGARETTENAME,
                                GroupNO = item.GROUPNO ?? 0
                            };
                if (query != null)
                    return query .ToList();
                else return null;
            }
        }

        public static List<FollowTaskDeail> getUnionData(decimal sortnum)
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_PRODUCE_POKE
                            join item2 in dataentity.T_PRODUCE_SORTTROUGH
                            on item.TROUGHNUM  equals item2.TROUGHNUM
                            where item.SORTNUM == sortnum
                            orderby item.SORTNUM
                            select new FollowTaskDeail() { SortNum = item.SORTNUM ?? 0, MERAGENUM = item.MERAGENUM ??0, POKENUM = item.POKENUM ??0 ,MainBelt = item.MAINBELT ??0,CIGARETTDECODE = item2.CIGARETTECODE,
                                CIGARETTDENAME = item2.CIGARETTENAME,
                                GroupNO = item.GROUPNO??0,
                                Machineseq = item.MACHINESEQ ??0
                            };
                            //group item by new { item.BILLCODE, item.SORTNUM, item.UNIONSTATE } into g
                            //select new FollowTaskDeail() { SortNum = g.Key.SORTNUM ?? 0, tNum = g.Sum(x => x.POKENUM ?? 0), Billcode = g.Key.BILLCODE, UnionState = g.Key.UNIONSTATE ?? 0 };
                if (query != null)
                    return query .ToList();

                else return null;
            }
        } 
      static decimal  yjZqNum = 0;
      static decimal lastZqNum = 0;
      static decimal lastSortnum = 0;
        /// <summary>
        /// 获取合流机械手信息
        /// </summary>
        /// <param name="sortnum">任务号</param>
        /// <param name="mainbelt">主皮带</param>
        /// <param name="groupno">组号</param>
        /// <param name="Allxynum">吸烟数量</param>
        /// <returns></returns>
        public static List<FollowTaskDeail> GetUnionMachineInfo(decimal sortnum, int mainbelt,int groupno,decimal Allxynum,decimal yjxyNum)
        {
            using (Entities dataentity  = new Entities())
            {
                 
                if (groupno == 4) { groupno = 3; } else if (groupno == 3) { groupno = 4; }
                //由于机械手第7组分出来的烟对应的是合流第八组机械手，这里7和8组对应有个对调
                if (groupno == 8) { groupno = 7; } else if (groupno == 7) { groupno = 8; }
                //由于机械手第7组分出来的烟对应的是合流第八组机械手，这里7和8组对应有个对调
                var query = (from item in dataentity.T_PRODUCE_POKE
                            join item2 in dataentity.T_PRODUCE_SORTTROUGH
                            on item.TROUGHNUM equals item2.TROUGHNUM
                             where item.SORTNUM == sortnum && item.MAINBELT == mainbelt && item.GROUPNO == groupno
                            orderby item.TROUGHNUM
                            select new FollowTaskDeail()
                            {
                                SortNum = item.SORTNUM ?? 0,
                                MERAGENUM = item.MERAGENUM ?? 0,
                                POKENUM = item.POKENUM ?? 0,
                                MainBelt = item.MAINBELT ?? 0,
                                CIGARETTDECODE = item2.CIGARETTECODE,
                                CIGARETTDENAME = item2.CIGARETTENAME,
                                GroupNO = item.GROUPNO ?? 0,
                                Machineseq = item.MACHINESEQ ?? 0
                            }).ToList();

                Allxynum = Allxynum - yjxyNum;
                if (query != null)
                {
                    return ChaiFenList(query, Allxynum);
                }
                else
                {
                    return null;
                }
            }

        }
        public static List<FollowTaskDeail> ChaiFenList(List<FollowTaskDeail> list, decimal xynum)
        {
            List<FollowTaskDeail> newlist = new List<FollowTaskDeail>();
            foreach (var item in list)
            {
                if (item.POKENUM > 1)
                {
                    for (int i = 0; i < item.POKENUM; i++)
                    {
                        newlist.Add(item);
                    }
                }
                else
                {
                    newlist.Add(item);
                }
            }
            newlist = newlist.Skip((int)xynum).ToList(); 
            foreach (var item in newlist)
            {
                item.POKENUM = 1;
            }
            return newlist.OrderBy(a => a.SortNum).Take(10).ToList();


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
                    //由于机械手第三组分出来的烟对应的是合流第四组机械手，这里3和4组对应有个对调
                    if (groupno == 4) { groupno = 3; } else if (groupno == 3) { groupno = 4; }
                    //由于机械手第7组分出来的烟对应的是合流第八组机械手，这里7和8组对应有个对调
                    if (groupno == 8) { groupno = 7; } else if (groupno == 7) { groupno = 8; }
                    var query = (from p in dataentity.T_PRODUCE_POKE
                                 join t in dataentity.T_PRODUCE_SORTTROUGH
                                 on p.TROUGHNUM equals t.TROUGHNUM
                                 where t.GROUPNO == groupno && p.MAINBELT == mainbelt && t.CIGARETTETYPE == 20 && t.TROUGHTYPE == 10 && p.SORTNUM >= machineTaskExcuting && p.SORTSTATE == 20
                                 orderby p.SORTNUM, p.MACHINESEQ
                                 select new FollowTaskDeail() { CIGARETTDECODE = t.CIGARETTECODE, CIGARETTDENAME = t.CIGARETTENAME, POKENUM = p.POKENUM ?? 0, Machineseq = p.MACHINESEQ ?? 0, POKEID = p.POKEID, MainBelt = p.MAINBELT ?? 0, SortNum = p.SORTNUM ?? 0, GroupNO = t.GROUPNO ?? 0 }).ToList();
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
        /// 合流缓存区
        /// </summary>
        /// <param name="list">当前任务集合</param>
        /// <param name="machineTaskExcuting">当前抓取排序号(sortnum)</param>
        /// <param name="machinePokeNum">当前抓数</param>
        ///  <param name="pokenumTotail">抓烟总数</param>
        /// <returns></returns>
        private static List<FollowTaskDeail> GetUnionCacheByPokenum(List<FollowTaskDeail> list, decimal machineTaskExcuting, decimal machinePokeNum, decimal pokenumTotail)
        {
            decimal TotailmachinePokeNum = Math.Ceiling(pokenumTotail / 10);//总数抓数 
            if (pokenumTotail < 10 * machinePokeNum || TotailmachinePokeNum == machinePokeNum)//如果当前抓烟任务的订单总烟数 小于十   去掉前抓烟任务 (包括最后一个任务)
            {
                list.RemoveAll(a => a.SortNum == machineTaskExcuting);
            }
            else
            {
                for (int Count = 1; Count <= machinePokeNum; Count++)//根据当前抓数抓数
                {
                    var pokenum = list.Where(c => c.POKEID == list[0].POKEID).Select(a => new { pokeid = a.POKEID, pokenum = a.POKENUM }).FirstOrDefault();//获取当前任务抓烟数  
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
        /// 获取机械手皮带信息
        /// </summary>
        /// <param name="groupNo">组号</param>
        /// <returns></returns>
        public static List<FollowTaskDeail> GetMachineBeltInfo(decimal groupNo)
        {
            using (Entities dataentity = new Entities())
            {
                var query1 = (from t in dataentity.T_PRODUCE_POKE
                              join p in dataentity.T_PRODUCE_SORTTROUGH
                              on t.TROUGHNUM equals p.TROUGHNUM
                              where t.MACHINESTATE == 20 && t.SORTSTATE == 15 && t.GROUPNO == groupNo
                              orderby t.SORTNUM
                              select new FollowTaskDeail
                              {
                                  Billcode = t.BILLCODE,
                                  MainBelt = t.MAINBELT ?? 0,
                                  CIGARETTDENAME = p.CIGARETTENAME,
                                  CIGARETTDECODE = p.CIGARETTECODE,
                                  SortNum = t.SORTNUM ?? 0,
                                  UnionTasknum = t.UNIONTASKNUM ?? 0,
                                  Machineseq = t.MACHINESEQ ?? 0,
                              }).ToList();
                if (query1 != null)
                {
                    return query1;
                }
                else
                    return null;
            }
        }


        /// <summary>
        /// 获取摇摆前任务信息
        /// </summary>
        /// <param name="mainbelt">主皮带通道</param>
        /// <param name="groupno">组号</param>
        /// <returns></returns>
        public static List<FollowTaskDeail> GetSortingBeltTask(decimal mainbelt, decimal groupno)
        {
            using (Entities dataentity = new Entities())
            {
                if (mainbelt != 5)//不同区域
                {
                    var query1 = (from p in dataentity.T_PRODUCE_POKE
                                  join t in dataentity.T_PRODUCE_SORTTROUGH
                                  on p.TROUGHNUM equals t.TROUGHNUM
                                  where p.MAINBELT == mainbelt && t.GROUPNO == groupno && p.SORTSTATE == 15 && p.MACHINESTATE == 20
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
                else//查出所有
                {
                    var query1 = (from p in dataentity.T_PRODUCE_POKE
                                  join t in dataentity.T_PRODUCE_SORTTROUGH
                                  on p.TROUGHNUM equals t.TROUGHNUM
                                  where t.GROUPNO == groupno && p.SORTSTATE == 15 && p.MACHINESTATE == 20
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
        /// <summary>
        /// 查询件烟信息
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<RestockingData> Restocking(string str)
        {
            using (Entities et = new Entities())
            {
                var query = et.T_WMS_ITEM.Where(i => i.BIGBOX_BAR != "222222" && i.BIGBOX_BAR != "111111" && (i.ITEMNAME.StartsWith(str) || i.ITEMNO.StartsWith(str))).Select(x => new RestockingData { cid = x.ITEMNO, cname = x.ITEMNAME, bigbox_bar = x.BIGBOX_BAR, dxtype = x.DXTYPE }).ToList();
                return query;
            }
        }
        /// <summary>
        /// 获取烟柜列表
        /// </summary>
        /// <returns></returns>
        public static List<TroughNumList> GetYGTroughNum()
        {
            using (Entities et = new Entities())
            {
                var query = et.T_PRODUCE_SORTTROUGH.Where(i => i.TROUGHTYPE == 10 && i.CIGARETTETYPE == 20).OrderBy(t => t.TROUGHNUM).Select(x => new TroughNumList { Cid = x.CIGARETTECODE, cname = x.CIGARETTENAME, troughnun = x.TROUGHNUM }).ToList();
                return query;
            }
        }
        /// <summary>
        /// 根据品牌编码查询件烟码垛形等数据
        /// </summary>
        /// <param name="str">卷烟编码</param>
        /// <returns></returns>
        public static RestockingData RestockingOrDefult(string str)
        {
            using (Entities et = new Entities())
            {
                if (str == null)
                {
                    return null;

                }
                var query = et.T_WMS_ITEM.Where(i => i.ITEMNO == str).Select(x => new RestockingData { cid = x.ITEMNO, cname = x.ITEMNAME, bigbox_bar = x.BIGBOX_BAR, dxtype = x.DXTYPE }).FirstOrDefault();
                return query;
            }
        }
        /// <summary>
        /// 根据卷烟件码查询烟垛形等信息
        /// </summary>
        /// <param name="str">件烟码</param>
        /// <returns></returns>
        public static RestockingData RestockingByDx(string str)
        {
            using (Entities et = new Entities())
            {
                if (str == null)
                {
                    return null;

                }
                var query = et.T_WMS_ITEM.Where(i => i.BIGBOX_BAR == str).Select(x => new RestockingData { cid = x.ITEMNO, cname = x.ITEMNAME, bigbox_bar = x.BIGBOX_BAR, dxtype = x.DXTYPE }).FirstOrDefault();
                return query;
            }
        }
        public static List<TroughNumList> GetHJTroughNum()
        {
            using (Entities et = new Entities())
            {
                var query = et.T_PRODUCE_SORTTROUGH.Where(i => i.TROUGHTYPE == 20 && i.CIGARETTETYPE == 20).OrderBy(t => t.TROUGHNUM).Select(x => new TroughNumList { Cid = x.CIGARETTECODE, cname = x.CIGARETTENAME, troughnun = x.TROUGHNUM }).ToList();
                return query;
            }
        }
        /// <summary>
        /// 插入补货任务并减少重力式货架的尾数
        /// </summary>
        /// <param name="startNum">重力式货架编号</param>
        /// <param name="endNum">烟柜编号</param>
        /// <param name="bigbox_Bar">件烟码</param>
        /// <param name="cid">卷烟编码</param>
        /// <param name="num">任务数量</param>
        /// <param name="dxtype">垛形</param>
        /// <returns>成功/失败</returns>
        public static bool InsertRestocking(string startNum, string endNum, string bigbox_Bar, string cid, int num, decimal dxtype)
        {
            using (Entities et = new Entities())
            {
                try
                {
                    for (int i = num; i > 0; i--)
                    {
                        INF_JOBDOWNLOAD inf_iobdownload = new INF_JOBDOWNLOAD();
                        string id = BaseService.GetSeq("select S_INF_JOBDOWNLOAD.nextval from dual").ToString();

                        inf_iobdownload.ID = id;
                        inf_iobdownload.JOBID = id;
                        inf_iobdownload.JOBTYPE = 80;
                        inf_iobdownload.SOURCE = startNum;
                        inf_iobdownload.TARGET = endNum;
                        inf_iobdownload.BRANDID = bigbox_Bar;
                        inf_iobdownload.PLANQTY = 1;
                        inf_iobdownload.PILETYPE = dxtype;
                        inf_iobdownload.PRIORITY = 50;
                        inf_iobdownload.BARCODE = cid;
                        inf_iobdownload.TUTYPE = 1;
                        inf_iobdownload.CREATEDATE = DateTime.Now;
                        inf_iobdownload.STATUS = 0;

                        et.INF_JOBDOWNLOAD.AddObject(inf_iobdownload); 
                    }
                     
                    var queryhj = (from item in et.T_PRODUCE_SORTTROUGH
                                 where item.CIGARETTETYPE == 20 && item.TROUGHTYPE == 20 && item.TROUGHNUM == startNum
                                 select item).FirstOrDefault();
                    var queryyg = (from item in et.T_PRODUCE_SORTTROUGH
                                   where item.CIGARETTETYPE == 20 && item.TROUGHTYPE == 10 && item.TROUGHNUM == endNum
                                   select item).FirstOrDefault();

                    if (queryhj == null && queryyg==null)
                    {
                        return false;
                    } 
                    else
                    {
                        queryhj.MANTISSA = queryhj.MANTISSA - num;
                        queryyg.MANTISSA = queryyg.MANTISSA + (num * 50);
                        et.SaveChanges();
                        return true;
                    }
                   

                }
                catch (Exception)
                {
                    return false;
                }

            }
        }
        /// <summary>
        /// 手动下开箱机任务重力式货架通道位数减少
        /// </summary>
        /// <param name="torughnum">重力式货架通道号</param>
        /// <param name="mantissanum">开箱任务数</param>
        /// <returns></returns>
        public static bool TroughMantissaChange(string torughnum, decimal mantissanum)
        {

            using (Entities et = new Entities())
            {
                var query = (from item in et.T_PRODUCE_SORTTROUGH
                             where item.CIGARETTETYPE == 20 && item.TROUGHTYPE == 20 && item.TROUGHNUM == torughnum
                             select item).FirstOrDefault();
                if (query != null)
                {
                    query.MANTISSA = query.MANTISSA - mantissanum;
                    et.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

                
            }
           
        }
    }
}
