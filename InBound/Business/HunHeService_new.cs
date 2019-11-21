using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Model;
using System.Configuration;
using System.Diagnostics;

namespace InBound.Business
{
    public class HunHeService_new
    {
       
        /// <summary>
        /// 读取混合道数据
        /// </summary>
        /// <param name="seq">通道号</param>
        /// <param name="finishno">当前完成的pokeid</param>
        /// <param name="qty">前*行记录</param>
        /// <param name="tag">false 为已放烟未出烟，(true为点击版本使用的)</param>
        /// <returns>通道烟数据</returns>
        public List<HUNHEVIEW> GetTroughCigarette(decimal seq, decimal[] finishno, int qty,decimal[] packmachineseq, bool tag = false,string cigarettesort = "0" )
        {
            decimal packmachine1 = packmachineseq[0];
            

            decimal packmachine2 = packmachineseq[1];
            decimal? finishno1 = finishno[0];
            //decimal? finishno1 = 119601;
            int finishno2 = Convert.ToInt32(finishno[1] < 0 ? 0 : finishno[1]);
           
            using (Entities entity = new Entities())
            {
                try
                {
                    if (seq != 1061 && seq != 2061 && seq != 3061 && seq != 4061)
                    {
                        //处理前的数据表
                            var query = (from item1 in entity.T_PRODUCE_SORTTROUGH
                                         join item2 in entity.T_UN_POKE_HUNHE 
                                         on item1.TROUGHNUM equals item2.TROUGHNUM
                                         where item1.TROUGHTYPE == 10 && item1.CIGARETTETYPE == 40 //&& item2.SENDTASKNUM > finishno1 //>= finishno1//finishno  
                                             && item1.MACHINESEQ == seq
                                             && item2.PULLSTATUS == 1 
                                             && (item2.PACKMACHINESEQ == packmachine1
                                             || item2.PACKMACHINESEQ == packmachine2)
                                         orderby item2.SORTNUM, item1.MACHINESEQ, item1.TROUGHNUM, item2.POKEID
                                         select new HUNHEVIEW()
                                         {
                                             POKEID = item2.POKEID,
                                             CIGARETTECODE = item2.CIGARETTECODE,
                                             CIGARETTENAME = item1.CIGARETTENAME,
                                             MACHINESEQ = item2.MACHINESEQ,
                                             QUANTITY = 1,
                                             SORTNUM = item2.SORTNUM,
                                             SENDTASKNUM = item2.SENDTASKNUM,
                                             PULLSTATUS = item2.PULLSTATUS
                                         }).ToList().Where(x => x.SENDTASKNUM > finishno1).Skip(finishno2)
                                         .Take(qty)
                                         .ToList();
                                return query; 
                    }
                    if (tag)
                    {
                        
                     
                        if (cigarettesort=="0")
                        {
                            //处理前的数据表
                            var query = (from item1 in entity.T_PRODUCE_SORTTROUGH
                                         join item2 in entity.T_UN_POKE_HUNHE 
                                         on item1.TROUGHNUM equals item2.TROUGHNUM
                                         where item1.TROUGHTYPE == 10 && item1.CIGARETTETYPE == 40 //&& item2.SENDTASKNUM > finishno1 //>= finishno1//finishno  
                                             && item1.MACHINESEQ == seq
                                             && item2.PULLSTATUS == 1 
                                             && (item2.PACKMACHINESEQ == packmachine1
                                             || item2.PACKMACHINESEQ == packmachine2)
                                         orderby item2.SORTNUM, item1.MACHINESEQ, item1.TROUGHNUM, item2.POKEID
                                         select new HUNHEVIEW()
                                         {
                                             POKEID = item2.POKEID,
                                             CIGARETTECODE = item2.CIGARETTECODE,
                                             CIGARETTENAME = item1.CIGARETTENAME,
                                             MACHINESEQ = item2.MACHINESEQ,
                                             QUANTITY = 1,
                                             SORTNUM = item2.SORTNUM,
                                             SENDTASKNUM = item2.SENDTASKNUM,
                                             PULLSTATUS = item2.PULLSTATUS
                                         }).ToList().Where(x => x.SENDTASKNUM >= finishno1)
                                         .Take(qty)
                                         .ToList();
                                return query; 
                        }
                        else
                        {
                            //处理前的数据表
                            var query = (from item1 in entity.T_PRODUCE_SORTTROUGH
                                         join item2 in entity.T_UN_POKE_HUNHE on item1.TROUGHNUM equals item2.TROUGHNUM
                                         where item1.TROUGHTYPE == 10 && item1.CIGARETTETYPE == 40 //&& item2.SENDTASKNUM > finishno1//finishno  
                                             && item2.MACHINESEQ == seq
                                             && item2.PULLSTATUS == 1// 
                                             && (item2.PACKMACHINESEQ == packmachine1
                                             || item2.PACKMACHINESEQ == packmachine2)
                                         orderby item2.SORTNUM,item2.MACHINESEQ, item2.TROUGHNUM, item2.POKEID
                                         select new HUNHEVIEW()
                                         {
                                             POKEID = item2.POKEID,
                                             CIGARETTECODE = item2.CIGARETTECODE,
                                             CIGARETTENAME = item1.CIGARETTENAME,
                                             MACHINESEQ = item2.MACHINESEQ,
                                             QUANTITY = 1,
                                             SORTNUM = item2.SORTNUM,
                                             SENDTASKNUM = item2.SENDTASKNUM,
                                             PULLSTATUS = item2.PULLSTATUS
                                         }).ToList().Where(x => x.SENDTASKNUM > finishno1)
                                         .Take(qty)
                                         .ToList();
                            return updown_new(query, 0);
                            //没有经过排程处理的数据使用的方法 不需要过滤放烟
                            //return updown(query, cigarettesort,2);
                        }      
                       
                    }
                    else
                    {
                        var query = (from item2 in entity.T_PRODUCE_SORTTROUGH
                                     join item3 in entity.T_UN_POKE_HUNHE
                                         on item2.TROUGHNUM equals item3.TROUGHNUM
                                     where item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 40 //&& item3.SENDTASKNUM >= finishno1//finishno  
                                     && (item3.PACKMACHINESEQ == packmachine1
                                         || item3.PACKMACHINESEQ == packmachine2)
                                        && item3.PULLSTATUS == 1 
                                     orderby item3.SORTNUM, item2.MACHINESEQ, item2.TROUGHNUM, item3.POKEID
                                     select new HUNHEVIEW()
                                     {
                                         POKEID = item3.POKEID,
                                         CIGARETTECODE = item3.CIGARETTECODE,
                                         CIGARETTENAME = item2.CIGARETTENAME,                                        
                                         MACHINESEQ = item3.MACHINESEQ,
                                         QUANTITY = 1,
                                         SENDTASKNUM=item3.SENDTASKNUM,
                                          SORTNUM=item3.SORTNUM
                                     }).ToList().Where(x => x.SENDTASKNUM > finishno1)
                                     .Take(qty)
                                     .ToList();
                        if (cigarettesort == "0")
                        {
                            return query;
                        }
                        else
                        {
                            return updown_new(query, 0);
                            //没有经过排程处理的数据使用的方法 不需要过滤放烟
                            //return updown(query, cigarettesort);
                        }      
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        /// <summary>
        /// 包内顺序切换（不计算烟宽度）
        /// </summary>
        /// <param name="query">原数据(过滤放烟)</param>
        /// <returns>重新排序后的数据</returns>
        public List<HUNHEVIEW> updown_new(List<HUNHEVIEW> query, int tag = 0)
        {
            //重新排序后的数据表
            List<HUNHEVIEW> table = new List<HUNHEVIEW>();
            using (Entities entity = new Entities())
            {
                WriteLog writeLog = WriteLog.GetLog();
                //包号顺序表 
                List<HUNHEVIEW> table1 = query.GroupBy(x => x.SENDTASKNUM).Select(x => new HUNHEVIEW { SENDTASKNUM = x.Key }).OrderBy(x=>x.SENDTASKNUM).ToList();
                //每包内顺序表（未修改顺序—>修改顺序）
                foreach (var item in table1)
                {
                    //当前包号数据表
                    List<HUNHEVIEW> table2 = query.Where(x => x.SENDTASKNUM == item.SENDTASKNUM).Select(x => x).ToList();
                    //重新排序后的包
                    List<HUNHEVIEW> table3 = new List<HUNHEVIEW>(); 
                    int index = table2.Count;
                    //遍历每包内数据
                    foreach (var item2 in table2)
                    {
                        index--;
                        table3.Add(table2[index]); 
                    }
                    table.AddRange(table3);
                }
            }
            if (tag == 1)
            {
                table.RemoveAll(x => x.PULLSTATUS == 1);
            }
            if (tag == 2)
            {
                table.RemoveAll(x => x.PULLSTATUS == 0);
            }
            return table;
        }

        /// <summary>
        /// 包内顺序切换（不计算烟宽度  定位）
        /// </summary>
        /// <param name="query">原数据(过滤放烟)</param>
        /// <returns>重新排序后的数据</returns>
        public List<HUNHENOWVIEW1> updown_now_new(List<HUNHENOWVIEW1> query, int tag = 0)
        {
            //重新排序后的数据表
            List<HUNHENOWVIEW1> table = new List<HUNHENOWVIEW1>();
            using (Entities entity = new Entities())
            {
                WriteLog writeLog = WriteLog.GetLog();
                //包号顺序表 
                List<HUNHENOWVIEW1> table1 = query.GroupBy(x => x.sendtasknum).Select(x => new HUNHENOWVIEW1 { sendtasknum = x.Key }).OrderBy(x => x.sendtasknum).ToList();
                //每包内顺序表（未修改顺序—>修改顺序）
                foreach (var item in table1)
                {
                    //当前包号数据表
                    List<HUNHENOWVIEW1> table2 = query.Where(x => x.sendtasknum == item.sendtasknum).Select(x => x).ToList();
                    //重新排序后的包
                    List<HUNHENOWVIEW1> table3 = new List<HUNHENOWVIEW1>();
                    int index = table2.Count;
                    //遍历每包内数据
                    foreach (var item2 in table2)
                    {
                        index--;
                        table3.Add(table2[index]);
                    }
                    table.AddRange(table3);
                }
            } 
            return table;
        }


        #region  烟序处理算法
        /// <summary>
        /// 包内顺序切换（烟宽度）
        /// </summary>
        /// <param name="query">原数据</param>
        /// <param name="cigarettesort">最大宽度</param>
        /// <returns>重新排序后的数据</returns>
        public List<HUNHEVIEW> updown(List<HUNHEVIEW> query, string cigarettesort,int tag = 0)
        {
            //重新排序后的数据表
            List<HUNHEVIEW> table = new List<HUNHEVIEW>();
            using (Entities entity= new Entities())
            {
                WriteLog writeLog = WriteLog.GetLog();
                //包号顺序表 
                List<HUNHEVIEW> table1 = query.GroupBy(x => x.SENDTASKNUM).Select(x => new HUNHEVIEW { SENDTASKNUM = x.Key }).ToList();
                //每包内顺序表（未修改顺序—>修改顺序）
                foreach (var item in table1)
                {
                    //包号表
                    List<HUNHEVIEW> table2 = query.Where(x => x.SENDTASKNUM == item.SENDTASKNUM).Select(x => x).ToList();
                    //最大宽度
                    decimal maxwidth = 0;
                    //当前计算的宽度
                    decimal nowwidth = 0;
                    try
                    {
                        maxwidth = Convert.ToDecimal(cigarettesort);
                    }
                    catch (Exception)
                    {
                        writeLog.Write("最大出烟宽度转化失败！");
                    }
                    //重新排序后的包
                    List<HUNHEVIEW> table3 = new List<HUNHEVIEW>();
                    //重新排序后的临时包（反转前的一节烟数据）
                    List<HUNHEVIEW> table4 = new List<HUNHEVIEW>();
                    int countindex = 0;
                    int index = 0;
                    //遍历每包内数据
                    foreach (var item2 in table2)
                    {
                        countindex++;
                        index++;
                        //条烟宽度
                        decimal width = Convert.ToDecimal(entity.T_WMS_ITEM.Where(x => x.ITEMNO == item2.CIGARETTECODE).Select(x => x.IWIDTH).FirstOrDefault());
                        //计算宽度小于最大宽度
                        if (nowwidth + width < maxwidth)
                        {
                            table4.Add(item2);
                            nowwidth = nowwidth + width;
                            if (table2.Count == countindex)
                            {
                                //反转后的一节烟数据
                                List<HUNHEVIEW> table5 = new List<HUNHEVIEW>();
                                for (int i = table4.Count; i > 0; i--)
                                {
                                    table5.Add(table4[i - 1]);
                                }
                                table3.AddRange(table5);
                                table4.Clear();
                            }
                        }
                        else
                        {
                            //反转后的一节烟数据
                            List<HUNHEVIEW> table5 = new List<HUNHEVIEW>();
                            for (int i = index -1; i > 0; i--)
                            {
                                table5.Add(table4[i - 1]);
                            }
                            table3.AddRange(table5);
                            table4.Clear();

                            nowwidth = width;
                            table4.Add(item2);
                            index = 1;
                            if (table2.Count == countindex)
                            {
                                //反转后的一节烟数据
                                List<HUNHEVIEW> table6 = new List<HUNHEVIEW>();
                                for (int i = table4.Count; i > 0; i--)
                                {
                                    table6.Add(table4[i - 1]);
                                }
                                table3.AddRange(table6);
                                table4.Clear();
                            }
                        }  
                    }
                    table.AddRange(table3); 
                }
            }
            if (tag==1)
            {
                table.RemoveAll(x => x.PULLSTATUS == 1);
            }
            if (tag == 2)
            {
                table.RemoveAll(x => x.PULLSTATUS == 0);
            }
            return table;
        }
        #endregion
        #region  定位
        /// <summary>
        /// 包内顺序切换（烟宽度）
        /// </summary>
        /// <param name="query">原数据</param>
        /// <param name="cigarettesort">最大宽度</param>
        /// <returns>重新排序后的数据</returns>
        public List<HUNHENOWVIEW1> updown_now(List<HUNHENOWVIEW1> query, string cigarettesort)
        {
            //重新排序后的数据表
            List<HUNHENOWVIEW1> table = new List<HUNHENOWVIEW1>();
            using (Entities entity = new Entities())
            {
                WriteLog writeLog = WriteLog.GetLog();
                //包号顺序表 
                List<HUNHENOWVIEW1> table1 = query.GroupBy(x => x.sendtasknum).Select(x => new HUNHENOWVIEW1 { sendtasknum = x.Key }).ToList();
                //每包内顺序表（未修改顺序—>修改顺序）
                foreach (var item in table1)
                {
                    //包号表
                    List<HUNHENOWVIEW1> table2 = query.Where(x => x.sendtasknum == item.sendtasknum).Select(x => x).ToList();
                    //最大宽度
                    decimal maxwidth = 0;
                    //当前计算的宽度
                    decimal nowwidth = 0;
                    try
                    {
                        maxwidth = Convert.ToDecimal(cigarettesort);
                    }
                    catch (Exception)
                    {
                        writeLog.Write("最大出烟宽度转化失败！");
                    }
                    //重新排序后的包
                    List<HUNHENOWVIEW1> table3 = new List<HUNHENOWVIEW1>();
                    //重新排序后的临时包（反转前的一节烟数据）
                    List<HUNHENOWVIEW1> table4 = new List<HUNHENOWVIEW1>();
                    int countindex = 0;
                    int index = 0;
                    //遍历每包内数据
                    foreach (var item2 in table2)
                    {
                        countindex++;
                        index++;
                        //条烟宽度
                        decimal width = Convert.ToDecimal(entity.T_WMS_ITEM.Where(x => x.ITEMNO == item2.CIGARETTECODE).Select(x => x.IWIDTH).FirstOrDefault());
                        //计算宽度小于最大宽度
                        if (nowwidth + width < maxwidth)
                        {
                            table4.Add(item2);
                            nowwidth = nowwidth + width;
                            if (table2.Count == countindex)
                            {
                                //反转后的一节烟数据
                                List<HUNHENOWVIEW1> table5 = new List<HUNHENOWVIEW1>();
                                for (int i = table4.Count; i > 0; i--)
                                {
                                    table5.Add(table4[i - 1]);
                                }
                                table3.AddRange(table5);
                                table4 = null;
                            }
                        }
                        else
                        {
                            //反转后的一节烟数据
                            List<HUNHENOWVIEW1> table5 = new List<HUNHENOWVIEW1>();
                            for (int i = index - 1; i > 0; i--)
                            {
                                table5.Add(table4[i - 1]);
                            }
                            table3.AddRange(table5);
                            table4.Clear();

                            nowwidth = width;
                            table4.Add(item2);
                            index = 1;
                            if (table2.Count == countindex)
                            {
                                //反转后的一节烟数据
                                List<HUNHENOWVIEW1> table6 = new List<HUNHENOWVIEW1>();
                                for (int i = table4.Count; i > 0; i--)
                                {
                                    table6.Add(table4[i - 1]);
                                }
                                table3.AddRange(table6);
                                table4 = null;
                            }
                        }
                    }
                    table.AddRange(table3);
                }
            }
            return table;
        }
#endregion 

        /// <summary>
        /// 读取混合道的待放烟品脾
        /// </summary>
        /// <param name="seq"></param>
        /// <returns></returns>
        public List<HUNHEVIEW> GetUnPullCigarette(decimal seq,decimal[] finishno,decimal[] packmachineseq,int qty,string cigarettesort = "0")
        {
            decimal packmachine1 = packmachineseq[0];
            decimal packmachine2 = packmachineseq[1];
            decimal finishno1 = finishno[0]; 
            int finishno2 = Convert.ToInt32(finishno[1] < 0 ? 0 : finishno[1]);
            using (Entities entity = new Entities())
            {
                try
                {
                    if (seq != 1061 && seq != 2061 && seq != 3061 && seq != 4061)
                    {
                        var query = (from item2 in entity.T_PRODUCE_SORTTROUGH
                                     join item4 in entity.T_UN_POKE_HUNHE on item2.TROUGHNUM equals item4.TROUGHNUM
                                     where item2.CIGARETTETYPE == 40 && item2.MACHINESEQ == seq && (item4.PACKMACHINESEQ == packmachine1 || item4.PACKMACHINESEQ == packmachine2)
                                         //&& item4.SENDTASKNUM >= finishno1
                                     && item4.PULLSTATUS == 0
                                     orderby item4.SORTNUM, item2.MACHINESEQ, item2.TROUGHNUM, item4.POKEID
                                     select new HUNHEVIEW() { PULLSTATUS = item4.PULLSTATUS, POKEID = item4.POKEID, CIGARETTECODE = item4.CIGARETTECODE, CIGARETTENAME = item2.CIGARETTENAME, MACHINESEQ = item2.MACHINESEQ, QUANTITY = 1, SENDTASKNUM = item4.SENDTASKNUM }).ToList().Where(x => x.SENDTASKNUM >= finishno1).Skip(finishno2).Take(qty).ToList();

                        return query;
                    }
                     if (cigarettesort == "0")
                    {
                        var query = (from item2 in entity.T_PRODUCE_SORTTROUGH
                                     join item4 in entity.T_UN_POKE_HUNHE on item2.TROUGHNUM equals item4.TROUGHNUM
                                     where item2.CIGARETTETYPE == 40 && item2.MACHINESEQ == seq && (item4.PACKMACHINESEQ == packmachine1 || item4.PACKMACHINESEQ == packmachine2)
                                         //&& item4.SENDTASKNUM >= finishno1
                                     && item4.PULLSTATUS == 0
                                     orderby item4.SORTNUM, item2.MACHINESEQ, item2.TROUGHNUM, item4.POKEID
                                     select new HUNHEVIEW() { PULLSTATUS = item4.PULLSTATUS, POKEID = item4.POKEID, CIGARETTECODE = item4.CIGARETTECODE, CIGARETTENAME = item2.CIGARETTENAME, MACHINESEQ = item2.MACHINESEQ, QUANTITY = 1, SENDTASKNUM = item4.SENDTASKNUM }).ToList().Where(x => x.SENDTASKNUM >= finishno1).Take(qty).ToList();
                   
                        return query;
                    }
                    else
                    {
                        var query = (from item2 in entity.T_PRODUCE_SORTTROUGH
                                     join item4 in entity.T_UN_POKE_HUNHE  on item2.TROUGHNUM equals item4.TROUGHNUM
                                     where item2.CIGARETTETYPE == 40 && item2.MACHINESEQ == seq && (item4.PACKMACHINESEQ == packmachine1 || item4.PACKMACHINESEQ == packmachine2)
                                     //&& item4.SENDTASKNUM >= finishno1
                                     && item4.PULLSTATUS == 0
                                     orderby item4.SORTNUM, item2.MACHINESEQ, item2.TROUGHNUM, item4.POKEID
                                     select new HUNHEVIEW() { PULLSTATUS = item4.PULLSTATUS, POKEID = item4.POKEID, CIGARETTECODE = item4.CIGARETTECODE, CIGARETTENAME = item2.CIGARETTENAME, MACHINESEQ = item2.MACHINESEQ, QUANTITY = 1, SENDTASKNUM = item4.SENDTASKNUM }).ToList().Where(x => x.SENDTASKNUM >= finishno1).Take(qty).ToList();
                          
                        return updown_new(query, 0);
                        //没有经过排程处理的数据使用的方法  不需要过滤放烟
                        //return updown(query, cigarettesort,1);
                    }
                    
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }


        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <param name="seq">通道号</param>
        /// <returns></returns>
        public List<HUNHENOWVIEW1> GetALLCigarette(decimal seq, decimal[] packmachineseq, string cigarettesort="0")
        {
            decimal pack1 = packmachineseq[0];
            decimal pack2 = packmachineseq[1];
            using (Entities entity = new Entities())
            {
                try
                {
                    if (cigarettesort=="0")
                    {
                        var query = (from item in entity.T_UN_POKE
                                 join item2 in entity.T_PRODUCE_SORTTROUGH
                                 on item.TROUGHNUM equals item2.TROUGHNUM
                                 join item3 in entity.T_UN_TASK on item.TASKNUM equals item3.TASKNUM
                                 join item4 in entity.T_UN_POKE_HUNHE on item.POKEID equals item4.POKEID
                                 where item2.CIGARETTETYPE == 40 && item.MACHINESEQ == seq &&
                                 (item.PACKAGEMACHINE == pack1 || item.PACKAGEMACHINE == pack2)
                                     orderby item.SORTNUM, item2.MACHINESEQ, item2.TROUGHNUM, item.POKEID
                                 select new HUNHENOWVIEW1() { PULLSTATUS = item4.PULLSTATUS, tasknum = item.TASKNUM, sortnum = item.SORTNUM, customername = item3.CUSTOMERNAME, regioncode = item3.REGIONCODE, TROUGHNUM = item.MACHINESEQ, CIGARETTECODE = item2.CIGARETTECODE, CIGARETTENAME = item2.CIGARETTENAME, pokenum = item.POKENUM, status = item.STATUS, pokeid = item.POKEID, packmachineseq=item.PACKAGEMACHINE, sendtasknum=item.SENDTASKNUM }).ToList();
                        return query;
                    }
                    else
                    {
                        var query = (from item in entity.T_UN_POKE
                                     join item2 in entity.T_PRODUCE_SORTTROUGH
                                     on item.TROUGHNUM equals item2.TROUGHNUM
                                     join item3 in entity.T_UN_TASK on item.TASKNUM equals item3.TASKNUM
                                     join item4 in entity.T_UN_POKE_HUNHE on item.POKEID equals item4.POKEID
                                     where item2.CIGARETTETYPE == 40 && item.MACHINESEQ == seq &&
                                     (item.PACKAGEMACHINE == pack1 || item.PACKAGEMACHINE == pack2)
                                     orderby item.SORTNUM, item2.MACHINESEQ, item2.TROUGHNUM, item.POKEID
                                     select new HUNHENOWVIEW1() { PULLSTATUS = item4.PULLSTATUS, tasknum = item.TASKNUM, sortnum = item.SORTNUM, customername = item3.CUSTOMERNAME, regioncode = item3.REGIONCODE, TROUGHNUM = item.MACHINESEQ, CIGARETTECODE = item2.CIGARETTECODE, CIGARETTENAME = item2.CIGARETTENAME, pokenum = item.POKENUM, status = item.STATUS, pokeid = item.POKEID, packmachineseq = item.PACKAGEMACHINE, sendtasknum = item.SENDTASKNUM }).ToList();
                        if (cigarettesort == "0")
                        {
                            return query;
                        }
                        else
                        {
                            return updown_now_new(query,0);
                            //没有经过排程处理的数据使用的方法 不需要过滤放烟
                            //return updown_now(query, cigarettesort);
                        }    
                    }
                   
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public static int linkstate()
        {
            int num ;
            using (Entities et = new Entities())
            { 
                num = et.T_UN_POKE.Where(x => x.STATUS != 20).Count();
            }
            return num;
        }


        /// <summary>
        /// 置数据库为已放烟（当前接连的集合）
        /// </summary>
        /// <param name="pokelist">pokeid的集合</param>
        /// <returns>是否成功</returns>
        public static bool PullTag(List<string> pokelist, decimal machineseq)
        {
            using (Entities entity = new Entities())
            {
                try
                {
                    //先取第一个pokeid 读取它前面的是否放烟
                    decimal firstpokeid = Convert.ToDecimal(pokelist.First());
                    var qu = entity.T_UN_POKE_HUNHE.Where(x => x.POKEID < firstpokeid && x.PULLSTATUS == 0 && x.MACHINESEQ == machineseq).Count();
                    if (qu > 0)
                    {
                        return false;
                    }
                    foreach (var item in pokelist)
                    {
                        decimal pokeid = Convert.ToDecimal(item);
                        var query = entity.T_UN_POKE_HUNHE.Where(x => x.POKEID == pokeid).FirstOrDefault();
                        query.PULLSTATUS = 1;
                        entity.SaveChanges();
                    }
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                    // throw e;
                }
            }
        }

        /// <summary>
        /// 置数据库为已放烟(单条)
        /// </summary>
        /// <param name="pokelist">pokeid的集合</param>
        /// <returns>是否成功</returns>
        public static bool PullTag(decimal pokeid, decimal machineseq)
        {
            using (Entities entity = new Entities())
            {
                try
                {
                    var query = entity.T_UN_POKE_HUNHE.Where(x => x.POKEID == pokeid).FirstOrDefault();
                    query.PULLSTATUS = 1;
                    entity.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                    // throw e;
                }
            }
        }
        /// <summary>
        /// 置数据库为未放烟
        /// </summary>
        /// <param name="pokelist">pokeid的集合</param>
        /// <returns></returns>
        public static bool CancelTag(List<string> pokelist, decimal machineseq)
        {
            using (Entities entity = new Entities())
            {
                try
                {
                    //先取最后一个pokeid 读取它后面的是否放烟
                    decimal firstpokeid = Convert.ToDecimal(pokelist.Last());
                    var qu = entity.T_UN_POKE_HUNHE.Where(x => x.POKEID > firstpokeid && x.PULLSTATUS == 1 && x.MACHINESEQ == machineseq).Count();
                    if (qu > 0)
                    {
                        return false;
                    }
                    foreach (var item in pokelist)
                    {
                        decimal pokeid = Convert.ToDecimal(item);
                        var query = entity.T_UN_POKE_HUNHE.Where(x => x.POKEID == pokeid).FirstOrDefault();
                        query.PULLSTATUS = 0;
                        entity.SaveChanges();
                    }
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 放烟标志位
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns> 
        public static bool GetTag(string[] str)
        {
            using (Entities entity = new Entities())
            {
                try
                {
                    int tagnum = 0;
                    foreach (var item in str)
                    {
                        decimal pokeid = Convert.ToDecimal(item);
                        var query = entity.T_UN_POKE_HUNHE.Where(x => x.POKEID == pokeid && x.PULLSTATUS == 1).Count();
                        if (query == 1)
                        {
                            tagnum++;
                        }
                    }
                    if (tagnum == str.Count())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
      
        /// <summary>
        /// 获取将要放下的烟信息
        /// </summary>
        /// <param name="pokeid"></param>
        /// <returns></returns>
        public static HUNHEVIEW GetNextCigarette(decimal pokeid)
        {
            using (Entities entity = new Entities())
            {
                try
                {
                    var query = (from item in entity.T_UN_POKE
                                 join item2 in entity.T_PRODUCE_SORTTROUGH
                                 on item.TROUGHNUM equals item2.TROUGHNUM
                                 join item3 in entity.T_UN_TASK on item.TASKNUM equals item3.TASKNUM
                                 join item4 in entity.T_UN_POKE_HUNHE on item.POKEID equals item4.POKEID
                                 where item2.CIGARETTETYPE == 40 && item.POKEID == pokeid
                                 && item4.PULLSTATUS == 0
                                 orderby item.SORTNUM, item2.MACHINESEQ, item2.TROUGHNUM, item.POKEID
                                 select new HUNHEVIEW() { POKEID = item.POKEID, CIGARETTECODE = item.CIGARETTECODE, CIGARETTENAME = item2.CIGARETTENAME, MACHINESEQ = item2.MACHINESEQ, QUANTITY = item.POKENUM }).FirstOrDefault();
                    return query;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        /// <summary>
        /// 获取已放下的烟信息
        /// </summary>
        /// <param name="pokeid"></param>
        /// <returns></returns>
        public static HUNHEVIEW GetHaveCigarette(decimal pokeid)
        {
            using (Entities entity = new Entities())
            {
                try
                {
                    var query = (from item in entity.T_UN_POKE
                                 join item2 in entity.T_PRODUCE_SORTTROUGH
                                 on item.TROUGHNUM equals item2.TROUGHNUM
                                 join item3 in entity.T_UN_TASK on item.TASKNUM equals item3.TASKNUM
                                 join item4 in entity.T_UN_POKE_HUNHE on item.POKEID equals item4.POKEID
                                 where item2.CIGARETTETYPE == 40 && item.POKEID == pokeid
                                 && item4.PULLSTATUS == 1
                                 orderby item.SORTNUM, item2.MACHINESEQ, item2.TROUGHNUM, item.POKEID
                                 select new HUNHEVIEW() { POKEID = item.POKEID, CIGARETTECODE = item.CIGARETTECODE, CIGARETTENAME = item2.CIGARETTENAME, MACHINESEQ = item2.MACHINESEQ, QUANTITY = item.POKENUM }).FirstOrDefault();
                    return query;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }


        /// <summary>
        /// 获取将要放下的烟信息（条码）
        /// </summary>
        /// <param name="pokeid"></param>
        /// <returns></returns>
        public static HUNHEVIEW1 GetNextCigarette_bar(decimal pokeid)
        {
            using (Entities entity = new Entities())
            {
                try
                {
                    var query = (from item in entity.T_UN_POKE
                                 join item2 in entity.T_WMS_ITEM
                                 on item.CIGARETTECODE equals item2.ITEMNO
                                 join item3 in entity.T_UN_TASK on item.TASKNUM equals item3.TASKNUM
                                 join item4 in entity.T_UN_POKE_HUNHE on item.POKEID equals item4.POKEID
                                 where item.POKEID == pokeid
                                 && item4.PULLSTATUS == 0 
                                 select new HUNHEVIEW1() { POKEID = item.POKEID, CIGARETTECODE = item.CIGARETTECODE, CIGARETTENAME = item2.ITEMNAME, MACHINESEQ = item.MACHINESEQ, QUANTITY = item.POKENUM, PACK_BAR = item2.PACK_BAR }).FirstOrDefault();
                    return query;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        /// <summary>
        /// 获取已放下的烟信息（条码）
        /// </summary>
        /// <param name="pokeid"></param>
        /// <returns></returns>
        public static HUNHEVIEW1 GetHaveCigarette_BAR(decimal pokeid)
        {
            using (Entities entity = new Entities())
            {
                try
                {
                    var query = (from item in entity.T_UN_POKE
                                 join item2 in entity.T_WMS_ITEM
                                 on item.CIGARETTECODE equals item2.ITEMNO
                                 join item3 in entity.T_UN_TASK on item.TASKNUM equals item3.TASKNUM
                                 join item4 in entity.T_UN_POKE_HUNHE on item.POKEID equals item4.POKEID
                                 where item.POKEID == pokeid
                                 && item4.PULLSTATUS == 1 
                                 select new HUNHEVIEW1() { POKEID = item.POKEID, CIGARETTECODE = item.CIGARETTECODE, CIGARETTENAME = item2.ITEMNO, MACHINESEQ = item.MACHINESEQ, QUANTITY = item.POKENUM, PACK_BAR = item2.PACK_BAR }).FirstOrDefault();
                    return query;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public static List<HUNHENOWVIEW> GetSearchCigarette(string cname, int type)
        {
            using (Entities entity = new Entities())
            {
                try
                {
                    if (type == 1)
                    {
                        var query = (from item in entity.T_UN_POKE
                                     join item2 in entity.T_PRODUCE_SORTTROUGH
                                     on item.TROUGHNUM equals item2.TROUGHNUM
                                     join item3 in entity.T_UN_TASK on item.TASKNUM equals item3.TASKNUM
                                     where item2.CIGARETTETYPE == 40 && (item2.CIGARETTENAME.Contains(cname))
                                     orderby item.SORTNUM, item2.MACHINESEQ, item2.TROUGHNUM, item.POKEID
                                     select new HUNHENOWVIEW() {  packmachineseq=item.PACKAGEMACHINE,tasksort = item3.SORTSEQ, tasknum = item.TASKNUM, CIGARETTECODE=item.CIGARETTECODE, sortnum = item.SORTNUM, customername = item3.CUSTOMERNAME, regioncode = item3.REGIONCODE, TROUGHNUM = item.MACHINESEQ, CIGARETTENAME = item2.CIGARETTENAME, pokenum = item.POKENUM, status = item.STATUS, pokeid = item.POKEID }).ToList();
                        return query;
                    }
                    else
                    {
                        var query = (from item in entity.T_UN_POKE
                                     join item2 in entity.T_PRODUCE_SORTTROUGH
                                     on item.TROUGHNUM equals item2.TROUGHNUM
                                     join item3 in entity.T_UN_TASK on item.TASKNUM equals item3.TASKNUM
                                     where item2.CIGARETTETYPE == 40 && (item3.CUSTOMERNAME.Contains(cname))
                                     orderby item.SORTNUM, item2.MACHINESEQ, item2.TROUGHNUM
                                     select new HUNHENOWVIEW() { packmachineseq = item.PACKAGEMACHINE,tasksort = item3.SORTSEQ, tasknum = item.TASKNUM, CIGARETTECODE = item.CIGARETTECODE, sortnum = item.SORTNUM, customername = item3.CUSTOMERNAME, regioncode = item3.REGIONCODE, TROUGHNUM = item.MACHINESEQ, CIGARETTENAME = item2.CIGARETTENAME, pokenum = item.POKENUM, status = item.STATUS, pokeid = item.POKEID }).ToList();
                        return query;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        /// <summary>
        /// 获取 item表 条码
        /// </summary>
        /// <param name="cname">卷烟名称</param> 
        /// <returns></returns> 
        public static List<ALLTIAOMA> GetAllPack_bar(string cname = null)
        {
            using (Entities entity = new Entities())
            {
                if (cname == null)
                {
                    var query = (from item in entity.T_WMS_ITEM
                                 join item1 in entity.T_PRODUCE_SORTTROUGH
                                 on item.ITEMNO equals item1.CIGARETTECODE
                                 where (item1.CIGARETTETYPE == 40
                                  || item1.CIGARETTETYPE == 30)
                                 && item1.STATE == "10"
                                 select new ALLTIAOMA() { ITEMNO = item.ITEMNO, ITEM_NAME = item.ITEMNAME, PACK_BAR = item.PACK_BAR }).ToList();
                    return query;
                }
                else
                {
                    var query = (from item in entity.T_WMS_ITEM
                                 join item1 in entity.T_PRODUCE_SORTTROUGH
                                 on item.ITEMNO equals item1.CIGARETTECODE
                                 where (item1.CIGARETTETYPE == 40
                                  || item1.CIGARETTETYPE == 30)
                                 && item1.STATE == "10"
                                 && item.ITEMNAME.Contains(cname)
                                 select new ALLTIAOMA() { ITEMNO = item.ITEMNO, ITEM_NAME = item.ITEMNAME, PACK_BAR = item.PACK_BAR }).ToList();
                    return query;
                }

            }
        }

        // 获取混合道烟条码信息
        #region
        /*
        public static List<HUNHETROUGH> GetHunhe(string cname=null,decimal? machineseq=null)
        {
            using (Entities entity = new Entities())
            {
                if (cname == null && machineseq == null)
                {
                    var query = (from item in entity.T_PRODUCE_SORTTROUGH
                                 join item2 in entity.T_WMS_ITEM
                                 on item.CIGARETTECODE equals item2.ITEMNO
                                 where (item.MACHINESEQ == 1001
                                     || item.MACHINESEQ == 1002
                                     || item.MACHINESEQ == 1059
                                     || item.MACHINESEQ == 1060
                                     || item.MACHINESEQ == 1061)
                                     && item.CIGARETTETYPE == 40
                                     && item.STATE == "10"
                                 select new HUNHETROUGH()
                                 {
                                     troughnum = item.TROUGHNUM,
                                     machineseq = item.MACHINESEQ,
                                     cigarettecode = item.CIGARETTECODE,
                                     cigarettename = item.CIGARETTENAME,
                                     tiaoma = item2.PACK_BAR
                                 }).ToList();
                    return query;
                }
                else if (cname != null)
                {
                    var query = (from item in entity.T_PRODUCE_SORTTROUGH
                                 join item2 in entity.T_WMS_ITEM
                                 on item.CIGARETTECODE equals item2.ITEMNO
                                 where (item.MACHINESEQ == 1001
                                     || item.MACHINESEQ == 1002
                                     || item.MACHINESEQ == 1059
                                     || item.MACHINESEQ == 1060
                                     || item.MACHINESEQ == 1061)
                                     && item.CIGARETTETYPE == 40
                                     && item.STATE == "10"
                                     && item.CIGARETTENAME.Contains(cname)
                                 select new HUNHETROUGH()
                                 {
                                     troughnum = item.TROUGHNUM,
                                     machineseq = item.MACHINESEQ,
                                     cigarettecode = item.CIGARETTECODE,
                                     cigarettename = item.CIGARETTENAME,
                                     tiaoma = item2.PACK_BAR
                                 }).ToList();
                    return query;
                } 
                else
                {
                    var query = (from item in entity.T_PRODUCE_SORTTROUGH
                                 join item2 in entity.T_WMS_ITEM
                                 on item.CIGARETTECODE equals item2.ITEMNO
                                 where (item.MACHINESEQ == 1001
                                     || item.MACHINESEQ == 1002
                                     || item.MACHINESEQ == 1059
                                     || item.MACHINESEQ == 1060
                                     || item.MACHINESEQ == 1061)
                                     && item.CIGARETTETYPE == 40
                                     && item.STATE == "10"
                                     && item.CIGARETTENAME.Contains(cname)
                                     && item.MACHINESEQ == machineseq
                                 select new HUNHETROUGH()
                                 {
                                     troughnum = item.TROUGHNUM,
                                     machineseq = item.MACHINESEQ,
                                     cigarettecode = item.CIGARETTECODE,
                                     cigarettename = item.CIGARETTENAME,
                                     tiaoma = item2.PACK_BAR
                                 }).ToList();
                    return query;
                }
            }
        }*/
        #endregion

        //录入条码
        public static bool InsertAllPack_bar(string str, string packbar)
        {
            try
            {
                using (Entities entity = new Entities())
                {
                    var query = entity.T_WMS_ITEM.Where(x => x.ITEMNO == str).Select(x => x).FirstOrDefault();

                    query.PACK_BAR = packbar;
                    entity.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }


        }


        /// <summary>
        /// 获取白皮带烟序
        /// </summary>
        public static List<SPSortBeltInfo> GetCigBeltSort(decimal sendtasknum)
        {
            List<SPSortBeltInfo> sp=new List<SPSortBeltInfo>();
            using (Entities et=new Entities())
            {
                var result = (from item in et.T_UN_POKE
                              join item2 in et.T_PRODUCE_SORTTROUGH
                              on item.TROUGHNUM equals item2.TROUGHNUM
                              join item3 in et.T_UN_TASK
                              on item.BILLCODE equals item3.BILLCODE
                              where item2.CIGARETTETYPE==40 && item2.STATE == "10" && item.SENDTASKNUM == sendtasknum
                              orderby item.SENDTASKNUM,item.POKEID
                              select new SPSortBeltInfo { CIGARETTECODE = item.CIGARETTECODE, CIGARETTENAME = item2.CIGARETTENAME, CUSTOMERCODE = item3.CUSTOMERCODE, CUSTOMERNAME = item3.CUSTOMERNAME, PACKAGENO = item.PACKAGEMACHINE, SENDTASKNUM = item.SENDTASKNUM, SORTNUM = item.SORTNUM, SORTSEQ = item3.SORTSEQ }).ToList();
                return result;
            }
           
        }
    }
}