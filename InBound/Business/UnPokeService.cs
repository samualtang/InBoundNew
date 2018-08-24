using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Model;

namespace InBound.Business
{
    public class UnPokeService : BaseService
    {


        public static void test()
        {

            using (Entities data = new Entities())
            {
                data.ExecuteStoreCommand("update t_un_poke set sortmachine=10");
            }
        }
        //public static object[] GetTask(List<decimal> listsortnum,List<decimal> listpokenum,int[] packagemachineNo)
        //{

        //}
        /// <summary>
        /// 二次优先级（包装机前五订单的异形烟所需要的量）带任务号
        /// </summary>
        /// <param name="sortNum">任务集合</param>
        /// /// <param name="PackacgeMachine">可用包装机集合</param>
        /// <param name="Takesize">获取订单数量（默认是5）</param>
        /// <returns>优先级最高的包装机ss</returns>
        public static int GetSortPackageMachine(List<decimal> sortNum,List<int> PackacgeMachine, int Takesize = 5)
        {
            Dictionary<int, decimal> sortTop = new Dictionary<int, decimal>();
            if (sortNum.Count > 0)
            {
                for (int i = 1; i <= sortNum.Count; i++)
                {
                    if (sortNum[i] == -1)//等于-1是不允许排序的
                    {
                        continue;
                    }
                    sortTop.Add(PackacgeMachine[i - 1], GetTaksQuantity(sortNum[i - 1], PackacgeMachine[i - 1], Takesize));  //获取当前包装机前五订单的异形烟所需要的量 
                }
                return sortTop.Max(a => a.Key);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 获取当前包装机当前任务的总数和异型烟数量
        /// </summary>
        /// <param name="sortnum">当前任务号</param>
        ///  <param name="takesize">获取订单数量（默认是5）</param>
        /// <returns>5个订单异型烟总数</returns>
        public static decimal GetTaksQuantity(decimal sortnum,int packacgemachine, int takesize = 5)
        {
            decimal OqtyAndTqty = 0 ;
            using (Entities data = new Entities())
            {
                var sort = (from item in data.T_UN_TASK
                            where item.SORTNUM > sortnum && item.PACKAGEMACHINE == packacgemachine
                            orderby item.SORTNUM
                            select item).Take(takesize).ToList();
                foreach (var sortitem in sort)//循环读取五个单的异型烟量
                {
                    var query = (from item in data.T_UN_TASK
                                 where item.SORTNUM == sortitem.SORTNUM && item.PACKAGEMACHINE == packacgemachine
                                 select item).Select(a => new { TaskQuantity = a.TASKQUANTITY }).FirstOrDefault();
                    OqtyAndTqty  += query.TaskQuantity ?? 0;//异型烟数量
                }
            }
            return OqtyAndTqty;
        }
        /// <summary>
        /// 获取异型烟缓存区剩余量
        /// </summary>
        /// <param name="sortnum">当前任务号</param>
        /// <param name="xynum">以抓烟数量</param>
        /// <returns>剩余量</returns>
        public static decimal GetCacheCount(decimal packagemachine,decimal sortnum , decimal xynum,decimal maxCount)
        {
            using (Entities data = new Entities())
            {
                var query = (from item in data.T_UN_POKE
                             where item.SORTNUM > sortnum && item.STATUS >= 15 && item.PACKAGEMACHINE == packagemachine
                             select item).Sum(x => x.POKENUM??0);
                if (query != null)
                {
                    return maxCount - (query + xynum);
                }
                else
                {
                    return query;
                }
            }  
        }
        /// <summary>
        /// 是否有未完成任务
        /// </summary>
        /// <param name="packagemachine">包装机号</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public static bool CheckExistPreSendTask(string lienum, decimal ctype, decimal state)
        {
            using (Entities data = new Entities())
            {
                List<T_UN_POKE> list = new List<T_UN_POKE>();
                if (ctype == 1)
                {
                      list = (from item in data.T_UN_POKE
                                 where item.LINENUM == lienum && item.STATUS == state && item.CTYPE == ctype
                                 select item).ToList();
                }
                else
                {
                    list = (from item in data.T_UN_POKE
                            where  item.STATUS == state && item.CTYPE == ctype
                            select item).ToList();
                }
                if (list != null && list.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                } 
            } 
        }
        /// <summary>
        /// 获取可发任务的包装机
        /// </summary>
        /// <param name="sortNum">四个包装机的任务号</param>
        /// <param name="xyNum">四个包装机的抓眼数量</param>
        /// <param name="DISPATCHESIZE">下任务阀值</param>
        /// <returns>包装机号</returns>
        public static int GetSendPackageMachine( List<decimal> sortNum, List<decimal> xyNum, out decimal DISPATCHESIZE)
        {
            int packagemachine = 0;//包装几号
            int maxOrder = 10;//最大订单数
            decimal leftnum = 0;
            List<decimal> listNum = new List<decimal>();//缓存量任务号
            List<int> packmachi = new List<int>();//缓存量任务号

            List<decimal> Orderlist = new List<decimal>();//订单量任务号
            List<int> Orderpcmlist  = new List<int>();//订单量任务号
            int j =1;
            bool flage = true;
            DISPATCHESIZE = 0;//空出多少开始下任务    补烟数
            for (int i = 1; i <= 8; i++)//在八个包装机找可以发送任务的包装机
            {
                if (!CheckExistCanSendPackeMachine(i))//当前包装机无任务跳到下个包装机
                {
                    continue;
                }
                T_UN_CACHE cache = ProduceCacheService.GetUnCache(i);//获取包装机缓存
                decimal currentNum = GetCacheCount(i, sortNum[i - 1], xyNum[i - 1], cache.CACHESIZE ?? 0);//获取缓存剩余量
                //WriteLog.GetLog().Write("包装机号:" + i + "剩余空间:" + currentNum + "当前任务号:" + sortNum[i - 1] + " 已抓烟数量:" + xyNum[i - 1]);  
                for ( j = j+0; j <= 8; j++)//第一波的时候  缓存量少于10 这个次循环只会执行一次
                {
                    cache = ProduceCacheService.GetUnCache(i);//获取包装机缓存
                     currentNum = GetCacheCount(i, sortNum[i - 1], xyNum[i - 1], cache.CACHESIZE ?? 0);//获取缓存剩余量
                     if (((cache.CACHESIZE ?? 0) - currentNum) < 10)//缓存量少于10 
                     {
                         flage = false;
                         break;
                     }
                }
                if (!flage)
                {
                    if (((cache.CACHESIZE ?? 0) - currentNum) < 10)//缓存量少于10 
                    {
                        DISPATCHESIZE = cache.DISPATCHESIZE ?? 0;
                        listNum.Add(sortNum[i - 1]);// 任务号
                        packmachi.Add(i);// 包装机 
                        continue;
                    }
                }
               else  //如果没有缓存量少于10的 接下来的包装机就进行订单排级
                {
                    Orderlist.Add(sortNum[i-1]);
                    if (currentNum >= (cache.DISPATCHENUM ?? 0))//缓存数量比过补烟数
                    {
                        int tempOrderCount = GetLeftOrderCount(i, Orderlist[i - 1]);//获取当前包装机剩余订单数量
                        if (tempOrderCount < maxOrder)
                        {
                            maxOrder = tempOrderCount;
                            Orderlist.Add(sortNum[i - 1]); // 任务号
                            Orderpcmlist.Add(i); // 包装机
                            DISPATCHESIZE = cache.DISPATCHESIZE ?? 0;
                            leftnum = (cache.CACHESIZE ?? 0) - currentNum;
                        }
                        else
                        {
                            if (leftnum > ((cache.CACHESIZE ?? 0) - currentNum))
                            {
                                Orderlist.Add(sortNum[i - 1]);// 任务号
                                Orderpcmlist.Add(i);// 包装机
                                DISPATCHESIZE = cache.DISPATCHESIZE ?? 0;
                                leftnum = (cache.CACHESIZE ?? 0) - currentNum;
                            }
                        }
                    }
                }
            } 
            if (listNum.Count > 0)//取当前缓存量最少的包装机 做二次取级
            {
                if (listNum.Count == 1)
                {
                    WriteLog.GetLog().Write("当前发送包装机:" + packmachi[0]);
                    return packmachi[0];
                }
                else
                { 
                    packagemachine = GetSortPackageMachine(listNum, packmachi);//二次优先级获取(包装机缓存量最少但是需要异形烟量最大的包装机)
                    WriteLog.GetLog().Write("当前发送包装机:" + packagemachine);
                    return packagemachine; 
                }
            }
            else
            {
                if (Orderlist.Count == 0 && Orderpcmlist.Count == 0)
                {
                    return packagemachine;//0
                }
                else
                {
                    packagemachine = GetSortPackageMachine(Orderlist,Orderpcmlist);//如果没有缓存小于阈值的 则取包装机需要量的优先级
                    WriteLog.GetLog().Write( "当前发送包装机:" + packagemachine);
                    return packagemachine;
                }
            }
            //WriteLog.GetLog().Write( "当前发送主皮带:" + packagemachine);
           // return packagemachine;
        }

        /// <summary>
        /// 获取可发任务的包装机 烟仓
        /// </summary>
        /// <param name="sortNum">四个包装机的任务号</param>
        /// <param name="xyNum">四个包装机的抓眼数量</param>
        /// <param name="DISPATCHESIZE">下任务阀值</param>
        /// <returns>包装机号</returns>
        public static int GetSendPackageMachineYC(int linenum,List<decimal> sortNum, List<decimal> xyNum, out decimal DISPATCHESIZE)
        {
            int packagemachine = 0;//包装几号
            int maxOrder = 10;//最大订单数
            decimal leftnum = 0;
            List<decimal> listNum = new List<decimal>();//缓存量任务号
            List<int> packmachi = new List<int>();//缓存量任务号

            List<decimal> Orderlist = new List<decimal>();//订单量任务号
            List<int> Orderpcmlist = new List<int>();//订单量任务号
            
            bool flage = true;
            DISPATCHESIZE = 0;//空出多少开始下任务    补烟数
            int start = 0;//起点
            int end = 0;//终点
            DISPATCHESIZE = 0;//空出多少开始下任务 （阈值）
            if (linenum == 1)//一线对应 1-4号包装机
            {
                start = 1;
                end = 4;
            }
            else//二线对应 5-8包装机
            {
                start = 5;
                end = 8;
            }
            int j = start;
            for (int i = start; i <= end; i++)//在四个包装机找可以发送任务的包装机
            {
                if (!CheckExistCanSendPackeMachine(i))//当前包装机无任务跳到下个包装机
                {
                    continue;
                }
                T_UN_CACHE cache = ProduceCacheService.GetUnCache(i);//获取包装机缓存
                decimal currentNum = GetCacheCount(i, sortNum[i - 1], xyNum[i - 1], cache.CACHESIZE ?? 0);//获取缓存剩余量
                //WriteLog.GetLog().Write("包装机号:" + i + "剩余空间:" + currentNum + "当前任务号:" + sortNum[i - 1] + " 已抓烟数量:" + xyNum[i - 1]);  
                for (j = j + 0; j <= end; j++)//第一波的时候  缓存量少于10 这个次循环只会执行一次
                {
                    cache = ProduceCacheService.GetUnCache(i);//获取包装机缓存
                    currentNum = GetCacheCount(i, sortNum[i - 1], xyNum[i - 1], cache.CACHESIZE ?? 0);//获取缓存剩余量
                    if (((cache.CACHESIZE ?? 0) - currentNum) < 10)//缓存量少于10 
                    {
                        flage = false;
                        break;
                    }
                }
                if (!flage)
                {
                    if (((cache.CACHESIZE ?? 0) - currentNum) < 10)//缓存量少于10 
                    {
                        DISPATCHESIZE = cache.DISPATCHESIZE ?? 0;
                        listNum.Add(sortNum[i - 1]);// 任务号
                        packmachi.Add(i);// 包装机 
                        continue;
                    }
                }
                else  //如果没有缓存量少于10的 接下来的包装机就进行订单排级
                {
                    Orderlist.Add(sortNum[i - 1]);
                    if (currentNum >= (cache.DISPATCHENUM ?? 0))//缓存数量比过补烟数
                    {
                        int tempOrderCount = GetLeftOrderCount(i, Orderlist[i - 1]);//获取当前包装机剩余订单数量
                        if (tempOrderCount < maxOrder)
                        {
                            maxOrder = tempOrderCount;
                            Orderlist.Add(sortNum[i - 1]); // 任务号
                            Orderpcmlist.Add(i); // 包装机
                            DISPATCHESIZE = cache.DISPATCHESIZE ?? 0;
                            leftnum = (cache.CACHESIZE ?? 0) - currentNum;
                        }
                        else
                        {
                            if (leftnum > ((cache.CACHESIZE ?? 0) - currentNum))
                            {
                                Orderlist.Add(sortNum[i - 1]);// 任务号
                                Orderpcmlist.Add(i);// 包装机
                                DISPATCHESIZE = cache.DISPATCHESIZE ?? 0;
                                leftnum = (cache.CACHESIZE ?? 0) - currentNum;
                            }
                        }
                    }
                }
            }
            if (listNum.Count > 0)//取当前缓存量最少的包装机 做二次取级
            {
                if (listNum.Count == 1)
                {
                    WriteLog.GetLog().Write("当前发送包装机:" + packmachi[0]);
                    return packmachi[0];
                }
                else
                {
                    packagemachine = GetSortPackageMachine(listNum, packmachi);//二次优先级获取(包装机缓存量最少但是需要异形烟量最大的包装机)
                    WriteLog.GetLog().Write("当前发送包装机:" + packagemachine);
                    return packagemachine;
                }
            }
            else
            {
                if (Orderlist.Count == 0 && Orderpcmlist.Count == 0)
                {
                    return packagemachine;//0
                }
                else
                {
                    packagemachine = GetSortPackageMachine(Orderlist, Orderpcmlist);//如果没有缓存小于阈值的 则取包装机需要量的优先级
                    WriteLog.GetLog().Write("当前发送包装机:" + packagemachine);
                    return packagemachine;
                }
            }
            //WriteLog.GetLog().Write( "当前发送主皮带:" + packagemachine);
            // return packagemachine;
        }

        /// <summary>
        /// 计算Sendtasknum可以用于发送任务
        /// </summary>
        /// <param name="packagemachine">包装机号</param>
        /// <param name="OrdermaxCount">订单总计算量</param>
        public static void UpdateSendtasknumByPM(int packagemachine, int OrdermaxCount)
        {
            using (Entities entity = new Entities())
            {
               
                List<decimal> sortnum = new List<decimal>();//任务号集合
                List<T_UN_POKE> pokelist=new List<T_UN_POKE>();
                pokelist=(from item in entity.T_UN_POKE where item.STATUS == 10 && item.PACKAGEMACHINE == packagemachine orderby item.SORTNUM,item.CTYPE descending select item).Take(OrdermaxCount).ToList();
                if (pokelist != null && pokelist.Count > 0)
                {
                    sortnum = pokelist.Select(s => s.SORTNUM ?? 0).Distinct().ToList();
                     int tempcount=0;
                    foreach (var item in sortnum)
                    {
                        tempcount++;
                        if (tempcount == 1 || tempcount < sortnum.Count)
                        {
                            var packageno = GetSeq("select T_UN_POKE_SENDTASKNUM.Nextval from dual");
                            var templist = pokelist.Where(x => x.SORTNUM == item).ToList();
                            templist.ForEach(f => { f.STATUS = 12; f.SENDTASKNUM = packageno; });
                        }
                        else
                        {
                            var partTotalCount = pokelist.Where(x => x.SORTNUM == item).ToList().Count;
                            var TotalCount = (from record in entity.T_UN_POKE where record.STATUS == 10 && record.PACKAGEMACHINE == packagemachine && record.SORTNUM == item select record).Count();
                            if (TotalCount == partTotalCount)
                            {
                                var packageno = GetSeq("select T_UN_POKE_SENDTASKNUM.Nextval from dual");
                                var templist = pokelist.Where(x => x.SORTNUM == item).ToList();
                                templist.ForEach(f => { f.STATUS = 12; f.SENDTASKNUM = packageno; });
                            }
                        }
                       

                    }



                    entity.SaveChanges();
                }
            }

        }

      
        public static List<InBound.Model.UnTaskInfo> GetUnTaskInfo(int packagemachine, decimal sortnum,decimal  state)
        {
            using (Entities data = new Entities())
            {
                var query = (from item in data.T_UN_POKE
                             join item2 in data.T_PRODUCE_SORTTROUGH
                              on item.TROUGHNUM equals item2.TROUGHNUM
                              join item3 in data.T_UN_TASK
                              on item.BILLCODE equals item3.BILLCODE
                             where item.PACKAGEMACHINE == packagemachine && item.SORTNUM == sortnum && item.STATUS == state
                             select new UnTaskInfo
                             {
                                 CIGARETTDENAME = item2.CIGARETTENAME,
                                 CIGARETTDECODE = item2.CIGARETTECODE,
                                 machineseq = item.MACHINESEQ ?? 0,
                                 PACKAGEMACHINE = item.PACKAGEMACHINE ?? 0,
                                 SortNum = item.SORTNUM ?? 0,
                                 STATUS = item.STATUS ??0,
                                 CUSTOMERNAME = item3.CUSTOMERNAME,
                                 REGIONCODE = item3.REGIONCODE,
                                 SORTSEQ = item3.SORTSEQ??0,
                             }).ToList();
                if (query != null)
                {
                    return query;
                }
                else
                {
                    return null;
                }
            }

        }
        public static Boolean CheckExistTaskNo(decimal taskno)
        {
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_UN_POKE where item.SORTNUM == taskno select item).FirstOrDefault();
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
        /// <summary>
        /// 获取合流数量
        /// </summary>
        /// <param name="packagemachine"></param>
        /// <param name="sortnum"></param>
        /// <returns></returns> 
        public static object[] GetUnionTask( out List<T_UN_POKE> list)
        {
            object[] values = new object[8];
            for (int i = 0; i < values.Length; i++)
			{
                values[i] = 0;
			}
            using (Entities data = new Entities())
            {
                var fristSortnum = (from tiem in data.T_UN_POKE where  tiem.GRIDNUM == 10 orderby tiem.SORTNUM select tiem).FirstOrDefault();
                var query = (from item in data.T_UN_POKE where item.SORTNUM == fristSortnum.SORTNUM orderby item.SORTNUM, item.MACHINESEQ descending select item).Take(25).ToList();
                if (query != null)
                {
                    list = query.ToList(); 
                    var one = query.FirstOrDefault();
                    values[0] = one.SENDTASKNUM;
                    values[1] = 1;
                    values[2] = one.PACKAGEMACHINE;
                    values[3] = query.Sum(a => a.POKENUM);//总出烟数量
                    values[4] = query.Where(a => a.CTYPE == 1 && (a.MACHINESEQ != 1061 || a.MACHINESEQ != 2061)).Sum(a => a.POKENUM);//烟仓出烟数量
                    values[5] = query.Where(a => a.CTYPE == 2).Sum(a => a.POKENUM);//烟柜出烟数量
                    values[6] = query.Where(a => a.CTYPE == 1 && (a.MACHINESEQ == 1061 || a.MACHINESEQ == 2061)).Sum(a => a.POKENUM);//特异性烟出烟数量
                    values[7] = 1;  
                }
                else
                {
                    list = null;
                }
            }
            return values;
        }
        /// <summary>
        /// 烟仓数据
        /// </summary>
        /// <param name="takeSize"></param>
        /// <param name="lineNum"></param>
        /// <param name="outlist"></param>
        /// <returns></returns>
        public static object[] getYCTask(int packagemachine,string linenum,decimal CTYP, out List<T_UN_POKE> outlist, out string outStr)
        {
            object[] values = new object[66];
            String needDatas = "";
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            using (Entities data = new Entities())
            {
                List<T_UN_POKE> list = new List<T_UN_POKE>();
                var Sendtasknum = (from item in data.T_UN_POKE where item.PACKAGEMACHINE ==packagemachine &&   item.STATUS == 10 orderby item.SENDTASKNUM select item).FirstOrDefault();//取出第一行的sendtasknum(最新的客户)
                var query = (from item in data.T_UN_POKE where  item.SENDTASKNUM == Sendtasknum.SENDTASKNUM orderby item.SORTNUM select item).FirstOrDefault();//取出可以发送的客户)
                if (query == null)
                {
                    outlist = new List<T_UN_POKE>();
                    outStr = null;
                    return values;
                }
                decimal machineseq = 0;
                var query1 = (from task in data.T_UN_POKE where task.SORTNUM == query.SORTNUM && task.STATUS == 10 && task.LINENUM == linenum  && task.CTYPE == CTYP orderby task.SORTNUM select task).Take(25).ToList();
                values[0] = query.SENDTASKNUM;
                values[1] = 1;
                values[2] = query.PACKAGEMACHINE;
                values[3] = query1.Sum(a => a.POKENUM);
                outlist = query1;
                foreach (var item in query1.GroupBy(a => a.MACHINESEQ).Select(g => new { MACHINESEQ = g.Key, QTY = g.Sum(a => a.POKENUM) }).ToList())
                {
                    machineseq = (item.MACHINESEQ ?? 0);
                    if (item.MACHINESEQ > 1000 && item.MACHINESEQ < 2000)
                    {
                        machineseq = (item.MACHINESEQ ?? 0) - 1000;
                    }
                    else if (item.MACHINESEQ > 2000 && item.MACHINESEQ < 3000)
                    {
                        machineseq = (item.MACHINESEQ ?? 0) - 2000;
                    }
                    values[((int)machineseq + 3)] = item.QTY;
                    //values[((int)machineseq + 3)] = query1.Where(a => a.MACHINESEQ == item.MACHINESEQ).GroupBy(a => a.MACHINESEQ).Select(g => new { MACHINESEQ = g.Key, QTY = g.Sum(a => a.POKENUM) }).FirstOrDefault().QTY;
                }
                values[65] = 1;
                for (int i = 0; i < values.Length; i++)
                { 
                    needDatas += i+":"+ values[i] +","; 
                }  
                outStr = needDatas;
                return values;
            }
        }

        /// <summary>
        /// 异形烟数据
        /// </summary>
        /// <param name="takeSize"></param>
        /// <param name="lineNum"></param>
        /// <param name="outlist"></param>
        /// <returns></returns>
        public static object[] getAllLineTask( out List<T_UN_POKE> outlist, out string outStr)
        {
            object[] values = new object[74];
            String needDatas = "";
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            using (Entities data = new Entities())
            {
                List<T_UN_POKE> list = new List<T_UN_POKE>();
                //var Sendtasknum = (from item in data.T_UN_POKE where  item.STATUS == 10 orderby item.SENDTASKNUM select item).FirstOrDefault();//取出第一行的sendtasknum(最新的客户)
                var query = (from item in data.T_UN_POKE where item.STATUS == 10  orderby item.SORTNUM select item).FirstOrDefault();//取出可以发送的客户)
                if (query == null)//如果没有则 无任务s
                {
                    outlist = new List<T_UN_POKE>();
                    outStr = null;
                    return values;
                }
                decimal machineseq = 0;
                //获取任务
                var query1 = (from task in data.T_UN_POKE where task.SORTNUM == query.SORTNUM && task.STATUS == 10 orderby task.SORTNUM select task).Take(12).ToList();
                //获取整个任务有多少包数
                //var sendtasklist = (from item in data.T_UN_POKE select item).Where(a => a.SORTNUM == query.SORTNUM).GroupBy(a => a.SENDTASKNUM).Select(a => new { Count = a.Key }).ToList();
               // var sendtasknum = GetSeq("select T_UN_POKE_SENDTASKNUM.Nextval from dual");
                outlist = query1;
                values[0] = query.SORTNUM;//任务号
                values[1] = query.SENDTASKNUM;//包号
                values[2] = 1;//出口号
                values[3] =query.PACKAGEMACHINE; //包装机号
                foreach (var item in query1.GroupBy(a => a.MACHINESEQ).Select(g => new { MACHINESEQ = g.Key, QTY = g.Sum(a => a.POKENUM) }).ToList())//1-60烟仓 + 6 烟柜
                {
                    machineseq = (item.MACHINESEQ ?? 0);
                    if (machineseq != 1061 && machineseq != 2061)//不包括特异形烟道
                    {
                        if (item.MACHINESEQ > 1000 && item.MACHINESEQ < 2000)//一线烟仓
                        {
                            machineseq = (item.MACHINESEQ ?? 0) - 1000;
                        }
                        else if (item.MACHINESEQ > 2000 && item.MACHINESEQ < 3000)//二线烟仓
                        {
                            machineseq = (item.MACHINESEQ ?? 0) - 2000;
                        }
                        else if (item.MACHINESEQ > 3000)
                        {
                            machineseq = ((item.MACHINESEQ ?? 0) - 3000) + 60;
                        }
                        values[((int)machineseq + 3)] = item.QTY;
                    }
                    //values[((int)machineseq + 3)] = query1.Where(a => a.MACHINESEQ == item.MACHINESEQ).GroupBy(a => a.MACHINESEQ).Select(g => new { MACHINESEQ = g.Key, QTY = g.Sum(a => a.POKENUM) }).FirstOrDefault().QTY;
                }
                values[70] = query1.Where(a => a.CTYPE == 1 && (a.MACHINESEQ != 1061 && a.MACHINESEQ != 2061)).Sum(a => a.POKENUM).CastTo<decimal>(-1);//烟仓出烟数量
                values[71] = query1.Where(a => a.CTYPE == 2).Sum(a => a.POKENUM).CastTo<decimal>(-1);//烟柜出烟数量 
                values[72] = query1.Where(a => a.CTYPE == 1 && (a.MACHINESEQ == 1061 || a.MACHINESEQ == 2061)).Sum(a => a.POKENUM).CastTo<decimal>(-1);//特异性烟出烟数量 
                //if (sendtasklist.Count > 1)//拆包的情况下 第二次或者第三。。特异形烟数量 给0
                //{
                //    if ((query1[0].SENDTASKNUM ?? 0) == Convert.ToDecimal(sendtasklist[0]))
                //    {
                //        values[72] = query1.Where(a => a.CTYPE == 1 && (a.MACHINESEQ == 1061 || a.MACHINESEQ == 2061)).Sum(a => a.POKENUM);//特异性烟出烟数量 
                //    }
                //    else
                //    { 
                //        values[72] = 0;
                //    }
                //}
                //else
                //{
                //    values[72] = query1.Where(a => a.CTYPE == 1 && (a.MACHINESEQ == 1061 || a.MACHINESEQ == 2061)).Sum(a => a.POKENUM);//特异性烟出烟数量
                //}
                values[73] = 1;//标志位
                object[] date = values.Where(a => Convert.ToInt32(a) != 0).ToArray();
                for (int i = 0; i < date.Length; i++)
                {
                    needDatas += i + ":" + date[i] + ",";
                }
                outStr = needDatas;
                return values;
            }
        }
        /// <summary>
        /// 特异形烟数据
        /// </summary>
        /// <param name="outStr"></param>
        /// <returns></returns>
        public static object[] GetSpecialSmokeData(decimal Export, out List<T_UN_POKE> outlist, out string outStr)
        {
            object[] values = new object[36];
            decimal[] pckmch = GetPackageMachine(Export);
            String needDatas = "";
            values.ToList().ForEach(a => a = 0);
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            using (Entities data = new Entities())
            {
                var sendtasknum = (from item in data.T_UN_POKE where item.STATUS == 10 && item.GRIDNUM == 10 && (item.MACHINESEQ == 1061 || item.MACHINESEQ == 2061) && (item.PACKAGEMACHINE==pckmch[0] || item.PACKAGEMACHINE == pckmch[1]) orderby item.SENDTASKNUM select item).FirstOrDefault();//获取特异形烟任务
                if (sendtasknum == null || pckmch .Sum() < 0)
                {
                    outlist = new List<T_UN_POKE>();
                    outStr = null; 
                    return values;
                }
                var query = (from item in data.T_UN_POKE
                             join item2 in data.T_WMS_ITEM on item.CIGARETTECODE equals item2.ITEMNO
                             where item.SENDTASKNUM == sendtasknum.SENDTASKNUM && (item.MACHINESEQ == 1061 || item.MACHINESEQ == 2061)
                             orderby item.SORTNUM, item.MACHINESEQ descending
                             select new { POKEID = item.POKEID, CIGARETTECODE = item.CIGARETTECODE, MACHINESEQ = item.MACHINESEQ, SORTNUM = item.SORTNUM, SENDTASKNUM = item.SENDTASKNUM, PACKAGEMACHINE = item.PACKAGEMACHINE, POKENUM = item.POKENUM, LENGHT = item2.ILENGTH, WIDTH = item2.IWIDTH }).Take(10).ToList();
                outlist = (from it in data.T_UN_POKE where it.SENDTASKNUM == sendtasknum.SENDTASKNUM && (it.MACHINESEQ == 1061 || it.MACHINESEQ == 2061) select it).ToList();
                int index =4;//索引
                values[0] = query.FirstOrDefault().SENDTASKNUM;//顺序号累加
                values[1] = query.FirstOrDefault().SORTNUM;//任务号
                values[2] = Export; // GetLineNum((query.FirstOrDefault().PACKAGEMACHINE ?? 0));//获取烟柜号
                values[3] = query.Where(a=> (a.MACHINESEQ == 1061 || a.MACHINESEQ == 2061)).Sum(a => a.POKENUM);//订单数量
                foreach (var item in query)
                {
                    if (index <= 34)
                    {
                        values[index] = item.CIGARETTECODE;//卷烟编码
                        values[index + 1] = item.LENGHT;//条烟长度
                        values[index + 2] = item.WIDTH;//条烟宽度
                        index = index + 3;
                    }
                }
                values[34] = 1;//特异形烟出口号
                values[35] = 1;//标志位
             
            }
            object[] date = values.Where(a => Convert.ToInt32(a) != 0).ToArray();
            for (int i = 0; i < date.Length; i++)
            {
                needDatas += i + ":" + date[i] + ",";
            }
            outStr = needDatas;
            return values;

        }
        /// <summary>
        /// 获取烟柜号
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
       static int GetLineNum(decimal index)
        {
            if (index == 1 || index == 2)
            {
                return 61;
            }
            if (index == 3 || index == 4)
            {
                return 62;
            }
            if (index == 5 || index == 6)
            {
                return 63;
            }
            if (index == 7 || index == 8)
            {
                return 64;
            }
            return 1;
        }

       static decimal[] GetPackageMachine(decimal Export)
       {
           decimal[] PackageMachine = new decimal[2];
           if (Export == 61)
           {
               PackageMachine[0] = 1;
               PackageMachine[1] = 2;
               return PackageMachine;
           }
           if (Export == 62)
           {
               PackageMachine[0] = 3;
               PackageMachine[1] = 4;
               return PackageMachine;
           }
           if (Export == 63)
           {
               PackageMachine[0] = 5;
               PackageMachine[1] = 6;
               return PackageMachine;
           }
           if (Export == 64)
           {
               PackageMachine[0] = 7;
               PackageMachine[1] = 8;
               return PackageMachine;
           }
           PackageMachine[0] = -1;
           PackageMachine[1] = -1;
           return PackageMachine; 
       }
        /// <summary>
        /// 烟柜数据
        /// </summary>
        /// <param name="takeSize"></param>
        /// <param name="lineNum"></param>
        /// <param name="outlist"></param>
        /// <returns></returns>
        public static object[] getYGTask(int packagemachine, decimal CTYP, out List<T_UN_POKE> outlist, out string outStr)
        {
            object[] values = new object[11];
            String needDatas = "";
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            using (Entities data = new Entities())
            {
                List<T_UN_POKE> list = new List<T_UN_POKE>();
                var Sendtasknum = (from item in data.T_UN_POKE where item.PACKAGEMACHINE == packagemachine && item.STATUS == 10 &&item.CTYPE ==CTYP orderby item.SENDTASKNUM select item).FirstOrDefault();//取出第一行的sendtasknum(最新的客户)
                var query = (from item in data.T_UN_POKE where item.SENDTASKNUM == Sendtasknum.SENDTASKNUM orderby item.SORTNUM select item).FirstOrDefault();//取出可以发送的客户)
                if (query == null)
                {
                    outlist = new List<T_UN_POKE>();
                    outStr = null;
                    return values;
                }
                decimal machineseq = 0;
                var query1 = (from task in data.T_UN_POKE where task.SORTNUM == query.SORTNUM && task.STATUS == 10 && task.CTYPE == CTYP orderby task.SORTNUM select task).Take(25).ToList();
                values[0] = query.SENDTASKNUM;
                values[1] = 1;
                values[2] = query.PACKAGEMACHINE;
                values[3] = query1.Sum(a => a.POKENUM);
                outlist = query1;
                foreach (var item in query1.GroupBy(a => a.MACHINESEQ).Select(g => new { MACHINESEQ = g.Key, QTY = g.Sum(a => a.POKENUM) }).ToList())
                {
                    machineseq = ((item.MACHINESEQ ?? 0) - 3000);
                    values[(int)machineseq + 3] = item.QTY;
                   // values[((int)machineseq + 3)] = query1.Where(a => a.MACHINESEQ == item.MACHINESEQ).GroupBy(a => a.MACHINESEQ).Select(g => new { MACHINESEQ = g.Key, QTY = g.Sum(a => a.POKENUM) }).FirstOrDefault().QTY;
                }
                values[10] = 1;
                for (int i = 0; i < values.Length; i++)
                {
                    needDatas += i + ":" + values[i] + ",";
                }
                outStr = needDatas;
                return values;
            }
        }

      
        /// <summary>
        /// 获取当前包装机剩余订单数量
        /// </summary>
        /// <param name="packagemachine"></param>
        /// <param name="sortnum"></param>
        /// <returns></returns>
        public static int GetLeftOrderCount(int packagemachine, decimal sortnum)
        {
            using (Entities data = new Entities())
            {
                var query = (from item in data.T_UN_POKE
                             where item.SORTNUM >= sortnum && item.PACKAGEMACHINE == packagemachine
                             select item).Distinct().Count();
                if (query != null && query != 0)
                {
                    return query;
                }
                else
                {
                    return 0;
                }

            }
        }
        /// <summary>
        /// 获取可发任务的包装机
        /// </summary>
        /// <param name="lineNum">线路</param>
        /// <param name="sortNum">四个包装机的任务号</param>
        /// <param name="xyNum">四个包装机的抓眼数量</param>
        /// <param name="DISPATCHESIZE">下任务阀值</param>
        /// <returns>包装机号</returns>
        public static int GetSendPackageMachine(decimal lineNum,List<decimal> sortNum, List<decimal> xyNum,out decimal DISPATCHESIZE)
        {
            int packagemachine = 0;//包装几号
            int start = 0;//起点
            int end = 0;//终点
            DISPATCHESIZE = 0;//空出多少开始下任务 （阈值）
            if (lineNum == 1)//一线对应 1-4号包装机
            {
                start = 1;
                end = 4;
            }
            else//二线对应 5-8包装机
            {
                start = 5;
                end = 8;
            }
            for (int i = start; i < end; i++)//在四个包装机找可以发送任务的包装机
            {
                int index = 0;
                if (!CheckExistCanSendPackeMachine(i))//当前包装机无任务跳到下个包装机
                {
                    index++;
                    continue;
                }
                T_UN_CACHE cache = ProduceCacheService.GetUnCache(i);
                decimal currentNum = GetCacheCount(i, sortNum[index], xyNum[index],cache.CACHESIZE??0);//获取缓存剩余量
                WriteLog.GetLog().Write("包装机号:" + i + "剩余空间:" + currentNum + "当前任务号:" + sortNum[index] + " 已抓烟数量:" + xyNum[index]);
                if ((cache.CACHESIZE ?? 0) - currentNum < 10)//缓存量少于10 
                {
                    WriteLog.GetLog().Write("包装机号:" + i + "当前发送主皮带:" + i);
                    index++;
                    return i; 
                }

                index++;
            }
            return packagemachine;
        }
        public static bool CheckExistCanSendPackeMachine(decimal packmachine)
        {
            using (Entities data = new Entities())
            {
                var query = (from item in data.T_UN_POKE
                             where item.PACKAGEMACHINE == packmachine && item.STATUS == 10
                             select item).FirstOrDefault();
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
        /// <summary>
        /// 查看改线还有没有任务
        /// </summary>
        /// <param name="linenum"></param>
        /// <param name="ctype"></param>
        /// <returns></returns>
        public static bool CheckExistCanSendPackeMachine(string linenum , decimal ctype)
        {
            using (Entities data = new Entities())
            {
                T_UN_POKE t = new T_UN_POKE();
                if (ctype == 1)
                {
                    t = (from item in data.T_UN_POKE
                                 where item.LINENUM == linenum && item.STATUS == 10 && item.CTYPE == ctype
                                 select item).FirstOrDefault();
                }
                else
                {
                   t = (from item in data.T_UN_POKE
                                 where  item.STATUS == 10 && item.CTYPE == ctype
                                 select item).FirstOrDefault();
                }
                if (t != null  )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        //public static void UpdateTaskNum(List<T_UN_POKE> task, decimal sendtaskNum)
        //{
        //    using (Entities data = new Entities())
        //    {
        //        if (task != null)
        //        {
        //            foreach (var item in task)
        //            {
        //                var query = (from items in data.T_UN_POKE where items.POKEID == item.POKEID select items).FirstOrDefault();
        //                query.SENDTASKNUM = sendtaskNum;
        //            }
        //        }
        //        data.SaveChanges();
        //    }
        //}
        //在原来的包号上加1
        //public static decimal getPackageNum(decimal ctype, String lineNum)
        //{

        //    decimal packgenum = BaseService.GetSeq("select s_produce_un_sendtasknum.nextval from dual");//1423 1424+1

        //    return packgenum;
        //    //using (Entities data = new Entities())
        //    //{
        //    //    if (lineNum != null)
        //    //    {
        //    //        //item.LINENUM == lineNum && item.CTYPE == ctype descending 
        //    //        var query = (from item in data.T_UN_POKE    orderby item.SENDTASKNUM select item.SENDTASKNUM).FirstOrDefault();
        //    //        if (query != null)
        //    //        {
        //    //            return (query ?? 0)+1;
        //    //         }
        //    //        else
        //    //            return 1; 
        //    //    }
        //    //    else
        //    //    {
        //    //        var query = (from item in data.T_UN_POKE where item.CTYPE == ctype orderby item.SENDTASKNUM  select item.SENDTASKNUM).FirstOrDefault();
        //    //        if (query != null)
        //    //        {
        //    //            return (query ?? 0) + 1;
        //    //        }
        //    //        else
        //    //            return 1;

        //    //    }
        //    //}
        //}

        public static decimal getLeftQty(String lineNum, decimal machineseq)
        {
            using (Entities data = new Entities())
            {
                var query = (from item in data.T_UN_POKE
                             where item.STATUS == 10 && item.MACHINESEQ == machineseq && item.LINENUM == lineNum
                             select item).Sum(x=>x.POKENUM??0);
                return query;
            }
        }
        /// <summary>
        /// 异形烟数据
        /// </summary>
        /// <param name="takeSize"></param>
        /// <param name="lineNum"></param>
        /// <param name="outlist"></param>
        /// <returns></returns>
        public static object[] getTask(int takeSize, string lineNum, out List<T_UN_POKE> outlist,out string outStr)
        {
            object[] values = new object[227];
            String needDatas = "";
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            using (Entities data = new Entities())
            {
                List<T_UN_POKE> list = new List<T_UN_POKE>();
                var query = (from item in data.T_UN_POKE 
                            where item.LINENUM == lineNum  && item.STATUS == 10 && item.CTYPE==1
                             orderby item.SENDTASKNUM
                             select item).FirstOrDefault();//取出第一行的sendtasknum(最新的客户)
                if (query == null)
                {
                    outlist = new List<T_UN_POKE>();
                    outStr  =    null;
                    return values;
                }

                var query1 = (from item in data.T_UN_POKE
                              where item.SENDTASKNUM == query.SENDTASKNUM && item.STATUS == 10 && item.CTYPE == 1 && item.LINENUM == lineNum
                              orderby item.MACHINESEQ, item.TROUGHNUM
                              select item).Take(takeSize).ToList();
                if (query1 != null)
                    //list = query1.Take(takeSize).ToList();
                    list = query1;
                outlist = list;
               // decimal packageNum = getPackageNum(1, lineNum);
               // pNum = packageNum;
                decimal checkcode = 0;//校验码,为流水号之和
                if (list != null)
                {
                    int j = 0;
                    decimal machineseq = 0;
                 
                    foreach (var item in list)
                    {
                        values[j * 9] = item.POKEID;//流水号
                        //customercode = item.CUSTOMERCODE;//12位的客户专卖证号电控只能最大接收9位
                        //if (customercode.Length > 9)
                        //{
                        //    customercode = customercode.Substring(customercode.Length - 9, 9);
                        //}
                        machineseq = (item.MACHINESEQ??0);
                        if (item.MACHINESEQ > 1000 && item.MACHINESEQ < 2000)
                        {
                            machineseq = (item.MACHINESEQ??0) - 1000;
                        }
                        else if (item.MACHINESEQ > 2000 && item.MACHINESEQ < 3000)
                        {
                            machineseq = (item.MACHINESEQ??0) - 2000;
                        }

                        values[j * 9 + 1] = machineseq;//烟道地址
                        values[j * 9 + 2] = getLeftQty(lineNum,item.MACHINESEQ??0);//尾数标志 >20
                        //values[j * 9 + 3] = customercode;//客户号
                        values[j * 9 + 3] = item.SENDTASKNUM;// item.SORTNUM;//客户号,这里的客户号并不是客户专卖证号,而是任务号
                        values[j * 9 + 4] = item.STORENUM; //前一客户顺序号
                        values[j * 9 + 5] = 0;//备用 0
                        values[j * 9 + 6] = item.PACKAGEMACHINE;//包装机号
                        values[j * 9 + 7] = 0;//备用0
                        values[j * 9 + 8] = item.SORTNUM;//客户号
                       
                        checkcode += item.POKEID;
                        needDatas += j * 9 + ":" + values[j * 9].ToString() + "," + (j * 9 + 1) + ":" + values[j * 9 + 1].ToString() + "," + (j * 9 + 2) + ":" + values[j * 9 + 2].ToString() + ","
                          + (j * 9 + 3) + ":" + values[j * 9 + 3].ToString() + "," + (j * 9 + 4) + ":" + values[j * 9 + 4].ToString() + "," + (j * 9 + 5) + ":" + values[j * 9 + 5].ToString() + ","
                          + (j * 9 + 6) + ":" + values[j * 9 + 6].ToString() + "," + (j * 9 + 7) + ":" + values[j * 9 + 7].ToString() + "," + (j * 9 + 8) + ":" + values[j * 9 + 8].ToString() + ";";
                        j++;
                    }
                    values[225] = 1;//完成信号
                    values[226] = checkcode;//校验码,为流水号之和 
                    needDatas += "225:" + values[225].ToString() + ",226:" + values[226].ToString();
                }
                outStr = needDatas;
                return values;
            }
        }
        /// <summary>
        /// 异形烟烟柜数据
        /// </summary>
        /// <param name="takeSize"></param>
        /// <param name="lineNum"></param>
        /// <param name="outlist"></param>
        /// <returns></returns>
        public static object[] getSixCabinetTask(int takeSize, string lineNum, out List<T_UN_POKE> outlist,out string outStr)
        {
           
            object[] values = new object[227];//一个任务
            String needDatas = "";
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            using (Entities data = new Entities())
            {
                List<T_UN_POKE> list = new List<T_UN_POKE>();
                var query =( from item in data.T_UN_POKE 
                            where item.LINENUM == lineNum && item.STATUS == 10  && item.CTYPE ==2  
                            orderby  item.SENDTASKNUM select item).FirstOrDefault(); //取出第一行的sendtasknum
                if (query == null)
                {
                    outlist = new List<T_UN_POKE>();
                    outStr = null;
                    return values;
                    
                }
               // packageNum = getPackageNum(2, lineNum);//取包号 前期需要 w
                var query1 = (from  item in data.T_UN_POKE
                              where item.SENDTASKNUM == query.SENDTASKNUM && item.STATUS == 10 && item.CTYPE == 2 && item.LINENUM == lineNum 
                               orderby item.MACHINESEQ,item.TROUGHNUM
                                  select item).Take(takeSize).ToList();
                              
                if (query1 != null)
                    list = query1;
                outlist = list;
                decimal checkcode = 0;//校验码,为流水号之和
                if (list != null)
                {
                    int j = 0;
                    decimal machineseq = 0;//物理通道号
                    //String customercode = "";
                    
                    foreach (var item in list)
                    {
                        values[j * 9] = item.POKEID;//流水号
                        machineseq = (item.MACHINESEQ ?? 0);
                        //customercode = item.CUSTOMERCODE;//12位的客户专卖证号电控只能最大接收9位
                        //if (customercode.Length>9) {
                        //    customercode = customercode.Substring(customercode.Length-9  ,9);
                        //}

                        values[j * 9 + 1] = machineseq;//烟道地址
                        values[j * 9 + 2] = 21;//尾数标志 >20
                        values[j * 9 + 3] = item.SENDTASKNUM;//客户号,这里的客户号并不是客户专卖证号,而是任务号
                        values[j * 9 + 4] = item.STORENUM;//前一客户顺序号
                        values[j * 9 + 5] = 0;//备用 0
                        values[j * 9 + 6] = item.PACKAGEMACHINE;//包装机号
                        values[j * 9 + 7] = 0;//备用 0
                        values[j * 9 + 8] = item.SORTNUM;//客户号
                        needDatas += j * 9 + ":" + values[j * 9].ToString() + "," + (j * 9 + 1) + ":" + values[j * 9 + 1].ToString() + "," + (j * 9 + 2) + ":" + values[j * 9 + 2].ToString() + ","
                           + (j * 9 + 3) + ":" + values[j * 9 + 3].ToString() + "," + (j * 9 + 4) + ":" + values[j * 9 + 4].ToString() + "," + (j * 9 + 5) + ":" + values[j * 9 + 5].ToString() + ","
                           + (j * 9 + 6) + ":" + values[j * 9 + 6].ToString() + "," + (j * 9 + 7) + ":" + values[j * 9 + 7].ToString() + "," + (j * 9 + 8) + ":" + values[j * 9 + 8].ToString() + ";";
                        j++;
                        checkcode += item.POKEID;
                    }
                    values[225] = 1;//完成信号
                    values[226] = checkcode;//校验码,为流水号之和
                    needDatas += "225:" + values[225].ToString() + ",226:" + values[226].ToString();
                }
                outStr = needDatas;
                return values;
            }
        }

        /// <summary>
        /// 获取烟柜分拣线
        /// </summary>
        /// <returns>烟柜分拣线</returns>
        public static String getSixCabinetLineNum()
        {
            string lineNum ="";
            using(Entities data = new Entities ())
            {
             var    query = (from item in data.T_UN_POKE
                                where item.STATUS == 10 && item.CTYPE == 2
                                orderby item.SORTNUM, item.SECSORTNUM, item.MACHINESEQ, item.TROUGHNUM
                                select item).FirstOrDefault();//分拣线  
             if (query != null)
             {
                 lineNum = query.LINENUM;
             }
             else
             {
                 lineNum = "3";//没有数据
             }
           
            }
            return lineNum;
         }

       
  
       
        public static object[] getCode()
        {
            object[] values = new object[200];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            using (Entities data = new Entities())
            {

                var query = (from item in data.T_PRODUCE_SORTTROUGH where item.TROUGHTYPE == 10 && item.CIGARETTETYPE != 20 select item.CIGARETTECODE).Distinct().ToList();

                if (query != null)
                {
                    int j = 0;
                    foreach (var code in query)
                    {
                        if (j < 200)
                        {
                            values[j] = code;
                            j++;
                        }
                    }
                }
                return values;
            }

        }
        public static void RollBack(string cellno)
        {
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_WMS_ATSCELL where item.CELLNO == cellno select item).FirstOrDefault();
                query.WORKSTATUS = 20;
                var query1 = (from item in entity.T_WMS_ATSCELLINFO where item.CELLNO == cellno select item).FirstOrDefault();
                query1.STATUS = 30;
                var query3 = (from item in entity.T_WMS_ATSCELLINFO_DETAIL where item.CELLNO == cellno select item).FirstOrDefault();
                query3.REQUESTQTY = 0;
                entity.SaveChanges();
            }
        }
        public static String getTarget(String cellNo)
        {
            if (cellNo == "")
                return cellNo;
            String layewayno = cellNo.Substring(2, 1);
            if (layewayno == "1" || layewayno == "2")
            {
                String id = Inf_EquipmentStatusService.GetINFEQUIPMENTSTATUS("1341").EQUIPMENTSTATUS;
                if (id == "1")
                {
                    int taskcount = InfJobDownLoadService.GetMiddleUnFinishTask();
                    if (taskcount == 0)
                    {
                        return "1341";
                    }
                    else
                        return "1355";
                }
                else
                {
                    return "1355";
                }
            }
            else
            {
                return "1355";
            }
        }
        public static void PreUpdateInOut(bool unFullFirst)
        {
           // unFullFirst = false;//不管散盘优先
            List<T_PRODUCE_SORTTROUGH> listNormal = SortTroughService.GetTroughNotINCigaretteType(10, 20);//分拣通道

            using (Entities entity = new Entities())
            {
                foreach (var task in listNormal)
                {
                    String cellno = "";
                    try
                    {
                    
                    var query = (from item in entity.T_WMS_STORAGEAREA_INOUT where item.AREAID == 3 && item.CELLNO == task.TROUGHNUM select item).Sum(x => x.QTY).GetValueOrDefault();

                    var itemDetail = ItemService.GetItemByCode(task.CIGARETTECODE);
                    var leftCount = (task.TRANSPORTATIONLINE??0) - (query + task.MANTISSA);//容量值-理论尾数值
                    int leftBox = int.Parse((leftCount) / (itemDetail.JT_SIZE ?? 0) + "");//可补件数
                    List<T_WMS_ATSCELLINFO_DETAIL> list = AtsCellInfoService.GetDetail(task.CIGARETTECODE, leftBox);//立库是否有数量等于可补数量的托盘

                    if (list != null && list.Count > 0 && unFullFirst)//散盘优先
                    {


                        if (itemDetail.OUTTYPE == 2)
                        {
                            INF_JOBDOWNLOAD load1 = new INF_JOBDOWNLOAD();
                            load1.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";

                            load1.JOBID = load1.ID;
                            load1.BRANDID = itemDetail.BIGBOX_BAR;
                            load1.CREATEDATE = DateTime.Now;
                            load1.PLANQTY = leftBox;
                            load1.PRIORITY = 50;
                            load1.PILETYPE = decimal.Parse(itemDetail.DXTYPE);
                            load1.SOURCE = AtsCellOutService.getCellNoEqual(task.CIGARETTECODE, leftBox);//out cell
                            load1.TARGET = getTarget(load1.SOURCE); //InfJobDownLoadService.GetTargetOutAddress(load1.SOURCE, leftBox);//异型烟人工出口
                            cellno = load1.SOURCE;
                            load1.STATUS = 0;

                            load1.JOBTYPE = 55;//补货出库
                            if (load1.SOURCE != "" && load1.TARGET != "")
                            {
                                T_WMS_ATSCELLINFO_DETAIL dl = AtsCellInfoDetailService.GetDetail(load1.SOURCE);
                                T_WMS_ATSCELLINFO dinfo = AtsCellInfoService.GetCellInfo(load1.SOURCE);
                                if (dinfo.DISMANTLE == 10)
                                {
                                    if (dl.REQUESTQTY != dl.QTY)
                                    {
                                        load1.TUTYPE = 2;//返库
                                    }
                                    else
                                    {
                                        load1.TUTYPE = 1;//不返库
                                    }
                                }
                                else
                                {
                                    load1.TUTYPE = 3;
                                }
                                load1.BARCODE = AtsCellInfoService.GetCellInfo(load1.SOURCE).PALLETNO;
                                entity.INF_JOBDOWNLOAD.AddObject(load1);
                            }
                            else
                            {
                                if (load1.SOURCE != "")
                                {
                                    RollBack(load1.SOURCE);
                                }
                            }
                            T_WMS_STORAGEAREA_INOUT outTask2 = new T_WMS_STORAGEAREA_INOUT();
                            outTask2.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                            outTask2.AREAID = 3;
                            outTask2.TASKNO = load1.JOBID;
                            outTask2.CELLNO = task.TROUGHNUM;
                            outTask2.CIGARETTECODE = itemDetail.ITEMNO;
                            outTask2.BARCODE = itemDetail.BIGBOX_BAR;
                            outTask2.CIGARETTENAME = task.CIGARETTENAME;
                            outTask2.INOUTTYPE = 20;//入
                            outTask2.QTY = leftBox * itemDetail.JT_SIZE;
                            outTask2.GROUPNO = task.GROUPNO;
                            outTask2.CREATETIME = DateTime.Now;
                            outTask2.STATUS = 10;
                            entity.AddToT_WMS_STORAGEAREA_INOUT(outTask2);


                            INF_JOBDOWNLOAD load2 = new INF_JOBDOWNLOAD();
                            load2.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                            load2.JOBID = load2.ID;
                            load2.BRANDID = itemDetail.BIGBOX_BAR;
                            load2.CREATEDATE = DateTime.Now;
                            load2.STATUS = 2;//出库完成置0
                            decimal planQty = leftBox;
                            if (AtsCellInfoService.GetCellInfo(load1.SOURCE).DISMANTLE == 10)
                            {
                                load2.JOBTYPE = 60;
                            }
                            else
                            {
                                load2.JOBTYPE = 70;//
                            }

                            load2.EXTATTR2 = load1.JOBID + "";
                            load2.PRIORITY = 50;
                            load2.SOURCE = load1.TARGET;//out cell立库出口
                            load2.TARGET ="11020";//异型烟货架通道
                            load2.STATUS = 0;
                            entity.INF_JOBDOWNLOAD.AddObject(load2);

                            entity.SaveChanges();
                        }
                        else
                        {

                            T_WMS_ATSCELL_OUT outcell = new T_WMS_ATSCELL_OUT();
                            outcell.REQUESTQTY = leftBox;
                            outcell.OUTTARGET = AtsCellOutService.getCellNoEqual(task.CIGARETTECODE, leftBox);
                            //10 调拨出库 20 抽检出库 30 补货出库 40 盘点出库 100 其他

                            outcell.OUTTYPE = 50;//自动补货出库

                            outcell.CREATETIME = DateTime.Now;
                            outcell.BARCODE = itemDetail.BIGBOX_BAR;
                            outcell.CIGARETTECODE = itemDetail.ITEMNO;
                            outcell.CIGARETTENAME = itemDetail.ITEMNAME;
                            decimal id = BaseService.GetSeq("select S_ATSCELL_OUT.nextval from dual");
                            outcell.ID = id;
                            if (outcell.OUTTARGET != "")
                            {

                                AtsCellOutService.InsertObject(outcell);
                            }



                        }

                    }
                    else
                    {
                        if (query + task.MANTISSA < task.THRESHOLD)
                        {
                            if (itemDetail.OUTTYPE == 2)
                            {

                                INF_JOBDOWNLOAD load1 = new INF_JOBDOWNLOAD();
                                load1.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                                load1.JOBID = load1.ID;
                                load1.BRANDID = itemDetail.BIGBOX_BAR;
                                load1.CREATEDATE = DateTime.Now;
                                load1.PLANQTY = task.BOXCOUNT;
                                load1.PRIORITY = 50;
                                load1.PILETYPE = decimal.Parse(itemDetail.DXTYPE);
                                load1.JOBTYPE = 55;//补货出库
                                load1.SOURCE = AtsCellOutService.getCellNoAll(task.CIGARETTECODE, int.Parse((task.BOXCOUNT ?? 0) + ""));//out cell
                                load1.TARGET = getTarget(load1.SOURCE);// InfJobDownLoadService.GetTargetOutAddress(load1.SOURCE, task.BOXCOUNT ?? 0);//异型烟人工出口
                                cellno = load1.SOURCE;
                                load1.STATUS = 0;
                                if (load1.SOURCE != "" && load1.TARGET != "")
                                {
                                    T_WMS_ATSCELLINFO_DETAIL dl = AtsCellInfoDetailService.GetDetail(load1.SOURCE);
                                    T_WMS_ATSCELLINFO dinfo = AtsCellInfoService.GetCellInfo(load1.SOURCE);
                                    if (dinfo.DISMANTLE == 10)
                                    {
                                        if (dl.REQUESTQTY != dl.QTY)
                                        {
                                            load1.TUTYPE = 2;//返库
                                        }
                                        else
                                        {
                                            load1.TUTYPE = 1;//不返库
                                        }
                                    }
                                    else
                                    {
                                        load1.TUTYPE = 3;
                                    }
                                    load1.BARCODE = AtsCellInfoService.GetCellInfo(load1.SOURCE).PALLETNO;
                                    entity.INF_JOBDOWNLOAD.AddObject(load1);
                                }
                                else
                                {
                                    if (load1.SOURCE != "")
                                    {
                                        RollBack(load1.SOURCE);
                                    }
                                    continue;
                                }
                                T_WMS_STORAGEAREA_INOUT outTask2 = new T_WMS_STORAGEAREA_INOUT();
                                outTask2.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                                outTask2.AREAID = 3;//烟柜
                                outTask2.TASKNO = load1.JOBID;
                                outTask2.CELLNO = task.TROUGHNUM;
                                outTask2.CIGARETTECODE = task.CIGARETTECODE;
                                outTask2.BARCODE = load1.BRANDID + "";
                                outTask2.CIGARETTENAME = task.CIGARETTENAME;
                                outTask2.INOUTTYPE = 20;//入
                                outTask2.QTY = task.BOXCOUNT * itemDetail.JT_SIZE;
                                outTask2.CREATETIME = DateTime.Now;
                                outTask2.GROUPNO = task.GROUPNO;
                                outTask2.STATUS = 10;
                                entity.AddToT_WMS_STORAGEAREA_INOUT(outTask2);


                                INF_JOBDOWNLOAD load2 = new INF_JOBDOWNLOAD();
                                load2.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                                load2.JOBID = load2.ID;
                                load2.BRANDID = load1.BRANDID;
                                load2.PLANQTY = task.BOXCOUNT;
                                load2.CREATEDATE = DateTime.Now;
                                load2.STATUS = 2;//出库完成置0
                                load2.EXTATTR2 = load1.JOBID + "";
                                //decimal planQty = leftBox;
                                if (AtsCellInfoService.GetCellInfo(load1.SOURCE).DISMANTLE == 10)
                                {
                                    load2.JOBTYPE = 60;
                                }
                                else
                                {
                                    load2.JOBTYPE = 70;//人工拆垛
                                }


                                load2.PRIORITY = 50;
                                load2.SOURCE = load1.TARGET;//out cell立库出口
                                load2.TARGET = "11020";//异型烟货架通道
                                //load2.STATUS = 0;
                                entity.INF_JOBDOWNLOAD.AddObject(load2);

                                entity.SaveChanges();
                            }
                            else
                            {



                                T_WMS_ATSCELL_OUT outcell = new T_WMS_ATSCELL_OUT();
                                outcell.REQUESTQTY = task.BOXCOUNT;
                                //outcell.OUTTARGET = AtsCellOutService.getCellNoEqual(task.CIGARETTECODE, leftBox);
                                //10 调拨出库 20 抽检出库 30 补货出库 40 盘点出库 100 其他
                                outcell.STATUS = 10;
                                outcell.OUTTYPE = 50;//自动补货出库
                                outcell.CREATETIME = DateTime.Now;
                                outcell.BARCODE = itemDetail.BIGBOX_BAR;
                                outcell.CIGARETTECODE = itemDetail.ITEMNO;
                                outcell.CIGARETTENAME = itemDetail.ITEMNAME;
                                decimal id = BaseService.GetSeq("select S_ATSCELL_OUT.nextval from dual");
                                outcell.ID = id;


                                T_WMS_STORAGEAREA_INOUT outTask2 = new T_WMS_STORAGEAREA_INOUT();
                                outTask2.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                                outTask2.AREAID = 3;//烟柜
                                outTask2.TASKNO = id + "";
                                outTask2.CELLNO = task.TROUGHNUM;
                                outTask2.CIGARETTECODE = task.CIGARETTECODE;
                                outTask2.BARCODE = itemDetail.BIGBOX_BAR;
                                outTask2.CIGARETTENAME = task.CIGARETTENAME;
                                outTask2.INOUTTYPE = 20;//入
                                outTask2.QTY = task.BOXCOUNT * itemDetail.JT_SIZE;
                                outTask2.CREATETIME = DateTime.Now;
                                outTask2.STATUS = 10;
                                entity.AddToT_WMS_STORAGEAREA_INOUT(outTask2);
                                entity.SaveChanges();
                                // if (outcell.OUTTARGET != "")
                                //{

                                AtsCellOutService.InsertObject(outcell);
                                //}
                            }


                        }
                    }
                }
                    catch(Exception ex)
                    {
                        if(cellno!=null &&cellno!="")
                        {
                            RollBack(cellno);
                        }
                        if (ex != null && ex.Message != null)
                        {
                            WriteLog.GetLog().Write("异常:"+ex.Message);
                        }
                    }
                }
            }


        }
        public static void UpdateStroageInout(List<T_UN_POKE> list)
        {
            if (list != null)
            {

                List<String> troughNum = new List<String>();

                foreach (var item in list)
                {
                    if (!troughNum.Contains(item.TROUGHNUM))
                    {
                        troughNum.Add(item.TROUGHNUM);
                    }
                }
                using (Entities entity = new Entities())
                {
                    foreach (var num in troughNum)
                    {
                        List<T_UN_POKE> tempList = list.FindAll(x => x.TROUGHNUM == num);
                       // decimal totalQty = tempList.Sum(x => x.TASKQTY) ?? 0;
                        T_UN_POKE poke = tempList[0];
                        
                        var query = (from itemlist in entity.T_WMS_STORAGEAREA_INOUT 
                                     where itemlist.TASKNO == poke.BILLCODE && itemlist.AREAID==3 && itemlist.CELLNO==num  && itemlist.QTY<0 select itemlist).FirstOrDefault();
                        if (query != null)
                            break;
                        decimal totalQty = (from itemlist1 in entity.T_UN_POKE
                                      where itemlist1.BILLCODE == poke.BILLCODE && itemlist1.TROUGHNUM == num
                                      select itemlist1).Sum(x => x.TASKQTY??0);
                        T_WMS_STORAGEAREA_INOUT outTask2 = new T_WMS_STORAGEAREA_INOUT();
                        outTask2.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                        outTask2.AREAID = 3;//烟柜 分拣
                        outTask2.TASKNO = poke.BILLCODE;
                        outTask2.CELLNO = poke.TROUGHNUM;
                        outTask2.CIGARETTECODE = poke.CIGARETTECODE;
                        T_WMS_ITEM item = ItemService.GetItemByCode(poke.CIGARETTECODE);
                        outTask2.BARCODE = item.BIGBOX_BAR;
                        outTask2.CIGARETTENAME = item.ITEMNAME;
                        outTask2.INOUTTYPE = 10;//出
                        outTask2.QTY = -totalQty;   
                        outTask2.CREATETIME = DateTime.Now;
                        outTask2.STATUS = 10;
                        entity.AddToT_WMS_STORAGEAREA_INOUT(outTask2);
                    }
                    entity.SaveChanges();
                }
            }
        }
        public static object[] getName()
        {
            object[] values = new object[6000];

            using (Entities data = new Entities())
            {

                var query = (from item in data.T_PRODUCE_SORTTROUGH where item.TROUGHTYPE == 10 && item.CIGARETTETYPE != 20 && item.CIGARETTENAME!=null select item.CIGARETTENAME).Distinct().ToList();

                if (query != null)
                {
                    String cname = "";
                    int j = 0;
                    int m = 0;
                    foreach (var code in query)
                    {
                        if (j < 200)
                        {
                          
                            byte[] b=initStr(code);
                            string s = Encoding.UTF8.GetString(b);
                            for (int i = 0; i < code.Length; i++)
                            {
                                if (i < 30)
                                {
                                    values[m] = b[i];

                                    m++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            j++;
                        }
                    }
                }
                return values;
            }
        }
        public static byte[] initStr(string str)
        {
            if (Encoding.UTF8.GetBytes(str).Length < 30)
            {
                int i = 30 - Encoding.UTF8.GetBytes(str).Length;
               // Encoding.ASCII.GetBytes(str) 转成数字
                for (int j = 0; j < i; j++)
                {
                    str += "0";
                }
                return Encoding.UTF8.GetBytes(str);
            }
            else
            {
                return Encoding.UTF8.GetBytes(str);
            }
        }
        public static void UpdateTask(List<T_UN_POKE> task, int status)
        {
            using (Entities data = new Entities())
            {
                if (task != null)
                {
                    foreach (var item in task)
                    {
                        var query = (from items in data.T_UN_POKE where items.POKEID == item.POKEID select items).FirstOrDefault();
                        query.STATUS = status;
                    } 
                }
                data.ExecuteStoreCommand("update t_un_task set state=30 where  tasknum not in (select tasknum from t_un_poke where status!=20)");
                data.SaveChanges();
            }
        }

         public static void UpdateSSTask( List<T_UN_POKE> list, int status)
        {
            using (Entities data = new Entities())
            {

                foreach (var item in list)
                {
                    var isSendS = (from pokeid in data.T_UN_POKE where pokeid.POKEID == item.POKEID select pokeid).FirstOrDefault();
                    if (isSendS.SORTNUM == 10)
                    {
                        isSendS.SORTNUM = 15;
                    }
                } 
                data.SaveChanges();
            } 
        }
          
        /// <summary>
        /// 更新合流任务状态
        /// </summary>
        /// <param name="task"></param>
        /// <param name="status"></param>
        public static void UpdateUnionTask(List<T_UN_POKE> task, int status)
        {
            using (Entities data = new Entities())
            {
                if (task != null)
                {
                    foreach (var item in task)
                    {
                        var query = (from items in data.T_UN_POKE where items.POKEID == item.POKEID select items).FirstOrDefault();
                        query.STATUS = status;
                    } 


                }
               
                data.SaveChanges();
            }
        }
        /// <summary>
        /// 混合烟道
        /// </summary>
        /// <param name="task"></param>
        /// <param name="status"></param>
        /// <param name="tasknum"></param>
        public static void UpdateTask(List<T_UN_POKE> task, int status, decimal tasknum)
        {
            using (Entities data = new Entities())
            {
                List<T_UN_POKE> list = new List<T_UN_POKE>();
                var query1=  from item in data.T_UN_POKE
                             where item.TASKNUM == tasknum && item.STATUS == 10 && item.TASKNUM > tasknum  
                            select item ;
                if (query1 != null)
                {
                    list = query1.ToList(); 
                    foreach (var item in list)
                    {
                        var query = (from items in data.T_UN_POKE where items.TASKNUM == tasknum select items).FirstOrDefault();
                        query.STATUS = status;
                    }
                }
                data.ExecuteStoreCommand("update t_un_task set state=30 where  tasknum not in (select tasknum from t_un_poke where status!=20)");
                data.SaveChanges();
            }
        }
        
        // 根据异形烟整包任务号更新poke表中状态
        public static void UpdateunTask(decimal sendtasknum, int status)
        {
            
            using (Entities data = new Entities())
            {
                try
                { 
                    var query = (from items in data.T_UN_POKE where items.SENDTASKNUM == sendtasknum select items).ToList();//暂时更新sortnum 本应该是sendtasknum
                    foreach (var item in query)
                    {
                        if (item.STATUS == 15)//必须等于15才能更新已完成
                        {
                            item.STATUS = status;
                        }
                    }
                    data.SaveChanges();
                    data.ExecuteStoreCommand("update t_un_task set state=30 where  tasknum not in (select tasknum from t_un_poke where status!=20)");
                    data.SaveChanges();
                }
                catch (Exception e)
                {

                    throw e;
                }
                
            }
        }

        public static List<T_UN_POKE> GetListByBillCode(String billcode)
        {
            using (Entities data = new Entities())
            {
                var query = (from items in data.T_UN_POKE where items.BILLCODE == billcode select items).ToList();

                return query;
            }
        }
        /// <summary>
        /// 任务号区间查询
        /// </summary>
        /// <param name="sortnumFrom">起始任务号</param>
        /// <param name="sortnumTo">结束任务号</param>
        /// <returns></returns>
        public static List<T_UN_POKE> GetListByBillCode(decimal sortnumFrom,decimal sortnumTo)
        {
            using (Entities data = new Entities())
            {
                var query = (from items in data.T_UN_POKE where items.SORTNUM >= sortnumFrom && items.SORTNUM <= sortnumTo select items).ToList();

                return query;
            }
        }
        public static void UpdateTask(String billcode,decimal status)
        {
            using (Entities data = new Entities())
            {
                var query = (from items in data.T_UN_POKE where items.BILLCODE == billcode select items).ToList();

                foreach (var item in query)
                {
                    item.STATUS = status;
                }
                var query2 = (from item in data.T_UN_TASK where item.BILLCODE == billcode select item).FirstOrDefault();
                if (status == 15)
                {
                    query2.STATE = "30";
                }
                else
                {
                    query2.STATE = "20";
                }
               
               // data.ExecuteStoreCommand("update t_un_task set state=30 where  tasknum not in (select tasknum from t_un_poke where status!=15)");
                data.SaveChanges();
            }
        }
        /// <summary>
        /// 更新任务(任务号区间查询) 
        /// </summary>
        /// <param name="sortnumFrom">起始任务号</param>
        /// <param name="sortnumTo">结束任务号</param>
        /// <param name="status"></param>
        public static void UpdateTask(decimal sortnumFrom, decimal sortnumTo, decimal status)
        {
            using (Entities data = new Entities())
            {
                var query = (from items in data.T_UN_POKE where items.SORTNUM >= sortnumFrom && items.SORTNUM <= sortnumTo select items).ToList();

                foreach (var item in query)
                {
                    item.STATUS = status;
                }
                var query2 = (from item in data.T_UN_TASK where item.SORTNUM >= sortnumFrom && item.SORTNUM <= sortnumTo select item).ToList() ;
                foreach (var item in query2)
                {
                    if (status == 20)
                    {
                        item.STATE = "30";
                    }
                    else
                    {
                        item.STATE = "20";
                    }
                }
             

                 data.ExecuteStoreCommand("update t_un_task set state=30 where  tasknum not in (select tasknum from t_un_poke where status!=15)");
                data.SaveChanges();
            }
        }

        public static List<TaskDetail> getData(decimal sortnum)
        {
            using (Entities dataentity = new Entities())
            {
                //var query = from item in dataentity.T_UN_TASK
                //            where item.SORTNUM == sortnum
                //            orderby item.SORTNUM
                //            group item by new { item.BILLCODE, item.SORTNUM, item.SECSORTNUM,item.STATE } into g
                //            select new TaskDetail() { SortNum = g.Key.SORTNUM ?? 0, SecSortNum = g.Key.SECSORTNUM ?? 0, tNum = g.Sum(x => x.ORDERQUANTITY ?? 0), Billcode = g.Key.BILLCODE, CIGARETTDECODE = g.Key.STATE };
                var query = from item in dataentity.T_UN_POKE
                            join item2 in dataentity.T_PRODUCE_SORTTROUGH
                            on item.TROUGHNUM equals item2.TROUGHNUM
                            where (item2.CIGARETTETYPE == 30 || item2.CIGARETTETYPE == 40) && item2.TROUGHTYPE == 10&& item.SORTNUM == sortnum
                            orderby item.SORTNUM, item2.SEQ, item.MACHINESEQ, item.SENDTASKNUM
                            select new TaskDetail() { POKENUM = item.POKENUM ?? 0, STATUS = item.STATUS ?? 0, SortNum = item.SORTNUM ?? 0, SENDTASKNUM = item.SENDTASKNUM ?? 0, Billcode = item.BILLCODE, CIGARETTDECODE = item2.CIGARETTECODE, CIGARETTDENAME = item2.CIGARETTENAME, LINENUM = item.LINENUM, PACKAGEMACHINE = item.PACKAGEMACHINE ?? 0 };

                if (query != null)
                    return query.OrderBy(x => x.SortNum).ToList();
                else return null;
            }
        }
        public static List<TaskDetail> getDataAll()
        {
            using (Entities dataentity = new Entities())
            {
                //var query = from item in dataentity.T_UN_TASK
                //            orderby item.SORTNUM
                //            group item by new { item.BILLCODE, item.SORTNUM, item.SECSORTNUM, item.STATE } into g
                //            select new TaskDetail() { SortNum = g.Key.SORTNUM ?? 0, SecSortNum = g.Key.SECSORTNUM ?? 0, tNum = g.Sum(x => x.ORDERQUANTITY ?? 0), Billcode = g.Key.BILLCODE,   CIGARETTDECODE = g.Key.STATE };
                

                //var query = from item in dataentity.T_UN_POKE
                //            orderby item.SORTNUM
                //            group item by new { item.BILLCODE, item.SORTNUM, item.STATUS, item.SENDTASKNUM } into g
                //            select new TaskDetail() { SortNum = g.Key.SORTNUM ?? 0, SENDTASKNUM = g.Key.SENDTASKNUM ?? 0, tNum = g.Sum(x => x.POKENUM ?? 0), Billcode = g.Key.BILLCODE, STATUS = g.Key.STATUS ?? 0 };
                var query = from item in dataentity.T_UN_POKE
                            join item2 in dataentity.T_PRODUCE_SORTTROUGH
                            on item.TROUGHNUM equals item2.TROUGHNUM
                            where (item2.CIGARETTETYPE == 30 || item2.CIGARETTETYPE == 40) && item2.TROUGHTYPE == 10
                            orderby item.SORTNUM, item2.SEQ, item.MACHINESEQ, item.SENDTASKNUM
                            select new TaskDetail() {POKENUM = item.POKENUM ??0 , STATUS = item.STATUS ?? 0, SortNum = item.SORTNUM??0,SENDTASKNUM = item.SENDTASKNUM??0, Billcode = item.BILLCODE , CIGARETTDECODE = item2.CIGARETTECODE, CIGARETTDENAME = item2.CIGARETTENAME,LINENUM = item.LINENUM ,PACKAGEMACHINE= item.PACKAGEMACHINE??0 };

                if (query != null)
                    return query.OrderBy(x => x.SortNum).ToList();
                else return null;
            }
        }
        public static List<T_UN_POKE> GetLinenum()
        {
            List<T_UN_POKE> list = new List<T_UN_POKE>();
            using (Entities entity = new Entities())
            {
                var query = from item in entity.T_UN_POKE
                            group item by new { item.LINENUM } into lst
                            select new { LINENUM = lst.Key.LINENUM };
                foreach (var item in query)
                {
                    list.Add(new T_UN_POKE
                    {
                        LINENUM = item.LINENUM,
                    });
                }
                return list;
            }
        }

        public static List<T_UN_POKE> FetchUnPokeList(string lineNum)
        {
            /**
            SELECT a.tasknum,a.customercode,a.customername,b.cigarettecode,b.cigarettename,c.pokenum,a.regioncode,to_char(a.orderdate,'yyyy-mm-dd') AS enterdate 
                         FROM t_un_task a,t_produce_sorttrough b,t_un_poke c 
                         WHERE a.tasknum=c.tasknum  and b.troughnum=c.troughnum and b.troughtype=10 and b.cigarettetype in (30,40) and b.state='10' 
                         and a.synseq='1' and c.linenum=2 
                         ORDER BY c.sortnum,c.secsortnum;
             * 
             */
            using (Entities entity = new Entities())
            {
                var query = from item in entity.T_UN_POKE
                            join task in entity.T_UN_TASK on item.TASKNUM equals task.TASKNUM
                            join trough in entity.T_PRODUCE_SORTTROUGH on item.TROUGHNUM equals trough.TROUGHNUM
                            where
                            (trough.CIGARETTETYPE == 30 || trough.CIGARETTETYPE == 40) && trough.TROUGHTYPE == 10 && trough.STATE == "10"
                            && task.SYNSEQ == 1 && item.LINENUM == lineNum
                            select item;
                return query.ToList();
            }
        }

    }
}
