using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections;

namespace SortingControlSys.PubFunc
{
    class SortingFun
    {
        DataBase Db = new DataBase();
        DataSet ds = new DataSet();
        SortingPub sortingPub = new SortingPub();
        public static Hashtable ht = new Hashtable();
        public static Hashtable ht_taskinfo = new Hashtable();
        public int htSize = 0;


        public static List<KeyValuePair<int, int>> initTask()
        {
            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
            string sql = "select distinct tasknum,sortmachine from  t_produce_poke where status=15 order by tasknum";
            OracleDataReader myread = DataPublic.ReadDb(sql);
            while (myread.Read())
            {
               
                list.Add(new KeyValuePair<int,int>(int.Parse(myread["sortmachine"].ToString()), int.Parse(myread["tasknum"].ToString())));
            }
            return list;
        }
        public static void updateStatus(int tasknum)
        {
            string sql = "update t_produce_poke set status=15 where tasknum=" + tasknum;
            DataPublic.ExeDb(sql);
        }

        public static void updateCompleteStatus(int tasknum)
        {
            string sql = "update t_produce_poke set status=20 where tasknum=" + tasknum;
            DataPublic.ExeDb(sql);
           // sql = "select count(1) from t_produce_poke where  status<>20  and tasknum= " + tasknum;// sortnum.ToString().Substring(1);
         // int exist=  (int)DataPublic.ExecuteScalar(sql);
          //if (exist == 0)
          //{
            sql = "update t_produce_task set state=30 ,finishtime=sysdate where tasknum=" + tasknum;// sortnum.ToString().Substring(1);
              DataPublic.ExeDb(sql);
          //}
        }
        static WriteLog writelog = new WriteLog();
        public static Hashtable GetHashTable()
        {
            //1.
            Hashtable ht = new Hashtable();
            //init  hashtable   取数据库车组在分拣中的  
            OracleDataReader myread1 = DataPublic.ReadDb("select  regioncode,mainbelt from t_produce_task where state=20 group by regioncode,mainbelt  ");
            if (myread1 != null)
            {
                while (myread1.Read())
                {
                    ht.Add(myread1["mainbelt"].ToString(), myread1["regioncode"].ToString());
                }
            }
         
                String sql = "select * from ( select min(tasknum),regioncode,mainbelt from t_produce_task where state=10  group by regioncode,mainbelt order by min(tasknum)) where rownum<5";
                OracleDataReader myread2 = DataPublic.ReadDb(sql);
                if (myread2 != null)
                {
                    while (myread2.Read())
                    {
                        if (ht.ContainsKey(myread1["mainbelt"].ToString()))
                        {
                        }
                        else
                        {
                            ht.Add(myread2["mainbelt"].ToString(), myread2["regioncode"].ToString());
                        }
                     }
                }
            

            return ht;
        }

        public void finishOrder(String taskno)
        {
            String sql = "update t_produce_task set state=30 where tasknum=" + taskno;
            DataPublic.ExecuteScalar(sql);
            //更新移库命令

            //计算品牌库存  确定是否发送开箱 以及出库命令
            //取订单里面的所有品牌
            String sql2 = "select itemno from t_produce_taskline where tasknum=" + taskno;
            //取库存 
            String sql3 = "select sum(itemqty) from t_wms_storagearea_inoutline where cigarettecode=''";
            //取烟仓阀值
            String  sql4="select THRESHOLD from t_procuce_sorttrough where CIGARETTECODE='' and troughtype=10";
            //取重力式货架阀值 
            String  sql5="select THRESHOLD from t_procuce_sorttrough where CIGARETTECODE='' and troughtype=20";
            //对比库存与阀值  确定是否发送开箱以及立库出库
        }
        
        public void GetData(Hashtable ht)
        {
            if (ht != null && ht.Count > 0)
            {
                foreach (var item in ht.Values)
                {
                   // 获取要发送的订单信息 
                    String sql = "select * from  t_produce_taskline where tasknum in(select min(tasknum) from t_produce_task where state=10 and regioncode ='" + item + "' group by regioncode)";
                }
            }
            else//分拣完成
            { 

            }
            //values = values.Substring(1);
          
            
            //计算品牌库存 有低于阈值的就发送开箱命令
        }
        public static object[] GetTask()
        {
            object[] values=new object[73];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }

          
            //按排序先取4个车组 排除主皮带号相同的车组
            // 
            //  通过 hashtable 过滤重复值
            //Hashtable ht = new Hashtable();
            //for (int i = 0; i < result.length; i++)
            //{
            //    if (ht.ContainsKey(""))
            //    { }
            //    else
            //    { ht.Add("mainbelt", "regioncode");
            //     更新对应车组状态为分拣中
            //    }
            //}
             //根据车组取订单发送  同时更新订单状态
            string sql =  "select tasknum from(select distinct tasknum from t_produce_poke where status=10  order by tasknum) where rownum=1";
            //writelog.Write(sql);
            int tasknum =  int.Parse(DataPublic.ExecuteScalar(sql).ToString());
            //OracleDataReader reader = DataPublic.ReadDb(sql);
            //writelog.Write("search sql" + sql);
            //if (reader != null)
            //{
            //    if (reader.Read())
            //    {
            //        tasknum = int.Parse(reader[tasknum].ToString());
            //    }
            //    reader.Close();
            //    reader.Dispose();
            //}
            if (tasknum != 0)
            {
                //sql = "select decode(min(sortnum),'',0,min(sortnum)) from t_produce_poke where status=10 and tasknum=" + tasknum;
                //int sortnum = int.Parse(DataPublic.ExecuteScalar(sql).ToString());
                //if (sortnum != 0)
                //{
                sql = "select sortnum,sortmachine, sum(pokenum) pokenum,machineseq,sortmachine,tasknum from t_produce_poke  where tasknum=" + tasknum + " group by tasknum, sortnum,sortmachine,machineseq";
               // writelog.Write(sql);878777
               
                OracleDataReader myread = DataPublic.ReadDb(sql);
                    try
                    {
                        int i = 0;
                        int totalcount = 0;
                        while (myread.Read())
                        {
                            if (i == 0)
                            {
                                values[0] = int.Parse(myread["tasknum"].ToString());
                                values[1] = int.Parse(myread["sortmachine"].ToString());
                                values[3] = 0;
                                values[72] = 1;
                            }
                            totalcount += int.Parse(myread["pokenum"].ToString());
                            int troughnum = int.Parse(myread["machineseq"].ToString());
                            values[3 + troughnum] = int.Parse(myread["pokenum"].ToString());
                            //if ((int)values[1] == 1)
                            //{
                            //    values[4] = 0;
                            //    values[5] = 0;
                            //    values[6] = 0;
                            //    values[7] = 0;
                            //    values[8] = 0;
                            //    values[9] = 0;
                            //    values[10] = 0;
                            //    values[11] = 0;
                            //    values[12] = 0;
                            //    values[13] = 0;
                            //    values[14] = 0;
                            //    values[15] = 0;
                            //    values[16] = 0;
                            //    values[17] = 0;
                            //    values[18] = 0;
                            //    values[19] = 0;//4-19 分拣机一
                            //}
                            //else if ((int)values[1] == 2)
                            //{
                            //    values[20] = 0;//20-49分拣机二
                            //    values[21] = 0;
                            //    values[22] = 0;
                            //    values[23] = 0;
                            //    values[24] = 0;
                            //    values[25] = 0;
                            //    values[26] = 0;
                            //    values[27] = 0;
                            //    values[28] = 0;
                            //    values[29] = 0;
                            //    values[30] = 0;
                            //    values[31] = 0;
                            //    values[32] = 0;
                            //    values[33] = 0;
                            //    values[34] = 0;
                            //    values[35] = 0;
                            //    values[36] = 0;
                            //    values[37] = 0;
                            //    values[38] = 0;
                            //    values[39] = 0;
                            //    values[40] = 0;
                            //    values[41] = 0;
                            //    values[42] = 0;
                            //    values[43] = 0;
                            //    values[44] = 0;
                            //    values[45] = 0;
                            //    values[46] = 0;
                            //    values[47] = 0;
                            //    values[48] = 0;
                            //    values[49] = 0;
                            //}
                            //else
                            //{
                            //    values[50] = 0;//分拣三
                            //    values[51] = 0;
                            //    values[52] = 0;
                            //    values[53] = 0;
                            //    values[54] = 0;
                            //    values[55] = 0;
                            //    values[56] = 0;
                            //    values[57] = 0;
                            //    values[58] = 0;
                            //    values[59] = 0;
                            //    values[60] = 0;
                            //    values[61] = 0;
                            //    values[62] = 0;
                            //    values[63] = 0;
                            //    values[64] = 0;
                            //    values[65] = 0;
                            //    values[66] = 0;
                            //    values[67] = 0;
                            //    values[68] = 0;
                            //    values[69] = 0;
                            //    values[70] = 0;
                            //    values[71] = 0;
                            //}
                        }
                        values[2] = totalcount;

                    }
                    catch (Exception e)
                    {
                        writelog.Write(e.Message);
                        if (e.InnerException != null)
                        {
                            writelog.Write(e.InnerException.Message);
                        }
                    }
                    finally
                    {
                        myread.Close();
                        myread.Dispose();
                    }
               // }
            }
           return values;
        }
        public static int getTaskNum(int sortmachine)
        {
            string sql = "select distinct tasknum from t_produce_poke where status=15 and sortmachine=" + sortmachine;
            OracleDataReader myread = DataPublic.ReadDb(sql);
            if (myread != null && myread.Read())
            {
                int tasknum = int.Parse(myread["tasknum"].ToString());
                return tasknum;

            }
            return 0;
        }
        public SortingFun()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //

        }
        /*
         *生成拨烟数据：101|总长度（四位）任务号（八位）订单总数量（四位）烟仓号A（两位）打几下（三位）烟仓号B（两位）打几下（三位）;下一个任务号。。。

         */
        public string allocateCigarStr(out String tasknums)
        {
            string retStr = "101|";
            String cigarDataStr = getAllocateCigarDataFromDB(out tasknums);
            //计算字符串总长度

            int StrLen = 4 + 4 + cigarDataStr.Length;
            //拼接长度串

            String StrLenStr = SortingPub.formatData(StrLen.ToString(), 4);
            retStr = retStr + StrLenStr + cigarDataStr;
            return retStr;
        }

        public static void initMemoryData()
        {
            String sql = "SELECT a.*,b.regioncode,b.taskquantity  FROM t_produce_poke a,t_produce_task b WHERE a.tasknum=b.tasknum AND a.tasknum IN( SELECT tasknum  FROM (select DISTINCT tasknum from t_produce_poke t  WHERE T.status='15' order by tasknum) WHERE ROWNUM <10 ) and status='15' ORDER BY b.regioncode,a.tasknum";
            OracleDataReader myread = DataPublic.ReadDb(sql);

            try
            {
                String taskNum = "", taskQty = "", troughNum = "", pokeNum = "", regioncode = "";

                while (myread.Read())
                {
                     taskNum = myread["tasknum"].ToString().Trim();
                  
                    taskQty = myread["taskquantity"].ToString().Trim();
                    regioncode = myread["regioncode"].ToString().Trim();
                   
                        if (!ht_taskinfo.ContainsKey(taskNum))
                        {
                           // ht.Add(SortingPub.formatData(taskNum, 8), "0");//hashtable 存放任务号和0，表示未完成分拣
                            ht_taskinfo.Add(taskNum, regioncode + "-" + taskQty);
                        }  //存放 任务号对应的车组和任务数量  用
                                  
                }
            }
            catch (Exception e)
            {
 
            }
            finally
            {
                myread.Close();
                myread.Dispose();
            }
        }
        public void updateStatus(String taskNum, string status)
        {
            string upSql = "update t_produce_poke set status='"+status+"' where tasknum='" + taskNum + "' ";
            DataPublic.ExeDb(upSql);
        }
        /*
         * 从数据库取拨烟数据,任务号（八位）订单总数量（四位）烟仓号A（两位）打几下（三位）烟仓号B（两位）打几下（三位）;下一个任务号。。。

         */
        public string getAllocateCigarDataFromDB(out String tasknums)
        {
            string retStr = "";
            tasknums = "";
            ht.Clear();
            //String sql = "SELECT *  FROM ( SELECT rownum seq, a.*  FROM (select * from t_produce_poke t  WHERE T.status='10' order by to_number(tasknum)) a )WHERE seq  >=1	AND seq <200";
            String sql = "SELECT a.*,b.regioncode,b.taskquantity  FROM t_produce_poke a,t_produce_task b WHERE a.tasknum=b.tasknum AND a.tasknum IN( SELECT tasknum  FROM (select DISTINCT tasknum from t_produce_poke t  WHERE T.status='10' order by tasknum) WHERE ROWNUM <4 ) and status='10' ORDER BY b.regioncode,a.tasknum";
            OracleDataReader myread = DataPublic.ReadDb(sql);
            try
            {
                String taskNum = "", taskQty = "", troughNum = "", pokeNum = "",regioncode="";
                String taskNumTemp = "";
                String export = "";
                //ht = new Hashtable(); 
                while (myread.Read())
                {
                    taskNum = myread["tasknum"].ToString().Trim();
                  
                    taskQty = myread["taskquantity"].ToString().Trim();
                    regioncode = myread["regioncode"].ToString().Trim();
                    export = myread["export"].ToString().Trim();
                    if (export.Equals("L"))
                    {
                        export = "1";
                    }
                    else
                    {
                        export = "2";
                    }
                    if (!taskNumTemp.Equals(taskNum))
                    {
                        tasknums += "," + taskNum;

                        //增加一个新任务号前，判断组装的字符串长度，所有拼接后的长度不能超过4000字节。

                        if (retStr.Length > 3500)
                        {
                            break;
                        }
                        if (!ht_taskinfo.ContainsKey(taskNum))
                        {
                            ht.Add(SortingPub.formatData(taskNum, 8), "0");//hashtable 存放任务号和0，表示未完成分拣
                            ht_taskinfo.Add(taskNum, regioncode + "-" + taskQty);
                        }//存放 任务号对应的车组和任务数量  用于更新分拣数据
                        retStr += ";"+SortingPub.formatData(taskNum, 8)+export;
                        retStr += SortingPub.formatData(taskQty, 4);
                        //已经取过的更新status为15
                       

                        /*临时
                        troughNum = myread["troughNum"].ToString().Trim();
                        pokeNum = myread["pokeNum"].ToString().Trim();

                        retStr += SortingPub.formatData(troughNum, 2);
                        retStr += SortingPub.formatData(pokeNum, 3);
                        taskNumTemp = taskNum;*/
                    }
                    troughNum = myread["machineseq"].ToString().Trim();
                    pokeNum = myread["pokeNum"].ToString().Trim();

                    retStr += SortingPub.formatData(troughNum, 2);
                    retStr += SortingPub.formatData(pokeNum, 3);
                    taskNumTemp = taskNum;
                }
            }
            finally
            {
                myread.Close();
                myread.Dispose();
            }
            if (ht != null)
                htSize += ht.Count;
            if (retStr.Length>0)
            retStr = retStr.Substring(1) + ".";
            return retStr;
        }

        /*
         * 检查hash表中分拣数据状态，如果只有num个任务号未分拣，则开始新取拨烟数据。

         */
        public int getTaskFinishNum()
        {

            String sql = "select count(1) num from (select distinct tasknum from t_produce_poke where status=15)";
            int num = int.Parse(DataPublic.ExecuteScalar(sql).ToString());
            return num;
            //int retVal = 0;
            //retVal = this.htSize - hasFinish;
            //return retVal;
        }

        public static void WriteErrorLog(String errorCode)
        {
            String sql = "insert into t_produce_errorlog(Errorcode,createtime) values('" + errorCode + "',sysdate)";
            DataPublic.ExeDbLog(sql);
        }
        static WriteLog log = new WriteLog();
        public static void ResetAllTask()
        {
           
            ArrayList arrList = new ArrayList();
            //String upOrderTable = "update t_produce_order set state='完成' where billcode in (select billcode from t_produce_task where tasknum='" + taskNum + "')";
            String upTaskTable = "update t_produce_task set state='20'";
            log.Write(upTaskTable);
            String upPokeTable = "update t_produce_poke set status='10' ";
            //arrList.Add(upOrderTable);
            arrList.Add(upTaskTable);
            arrList.Add(upPokeTable);
            try
            {
                DataPublic.ExecuteSqlTrans(arrList);
            }
            catch (Exception e)
            {
                Console.WriteLine("拨烟事物提交信息报错：" + e.StackTrace);
                //e.Message();
            }
           
        }
        /*
         * 
         */
        public bool updateTaskState(String taskNum)
        {
            bool retVal = false;
            ArrayList arrList = new ArrayList();
            //String upOrderTable = "update t_produce_order set state='完成' where billcode in (select billcode from t_produce_task where tasknum='" + taskNum + "')";
            String upTaskTable = "update t_produce_task set state='30',finishtime=sysdate where tasknum='" + taskNum + "'";
            String upPokeTable = "update t_produce_poke set status='20' where tasknum='" + taskNum + "'";
            Console.WriteLine("拨烟事物提交更新报告：" + taskNum);
            //arrList.Add(upOrderTable);
            arrList.Add(upTaskTable);
            arrList.Add(upPokeTable);
            try
            {
                DataPublic.ExecuteSqlTrans(arrList);
            }
            catch (Exception e)
            {
                Console.WriteLine("拨烟事物提交信息报错："+e.StackTrace);
                //e.Message();
            }
            return retVal;
        }

        public static bool updateTaskState(String taskNumfrom ,String taskTo,String pokeStatus,String taskStatus)
        {
            bool retVal = false;
            ArrayList arrList = new ArrayList();
            //String upOrderTable = "update t_produce_order set state='完成' where billcode in (select billcode from t_produce_task where tasknum='" + taskNum + "')";
            String upTaskTable = "update t_produce_task set state='"+taskStatus+"' where tasknum>=" + taskNumfrom + " and taskNum<="+taskTo;
            String upPokeTable = "update t_produce_poke set status='"+pokeStatus+"' where tasknum>=" + taskNumfrom + " and taskNum<=" + taskTo;
            //Console.WriteLine("拨烟事物提交更新报告：" + taskNum);
            //arrList.Add(upOrderTable);
            arrList.Add(upTaskTable);
            arrList.Add(upPokeTable);
            try
            {
                DataPublic.ExecuteSqlTrans(arrList);
            }
            catch (Exception e)
            {
                Console.WriteLine("拨烟事物提交信息报错：" + e.StackTrace);
                //e.Message();
            }
            return retVal;
        }

        public string getTaskinfo(String tasknum) 
        {
            string czcode = "";
            if (ht_taskinfo.ContainsKey(tasknum)) 
            {
                czcode = ht_taskinfo[tasknum].ToString();
            }
            return czcode;
        }
    }
}
