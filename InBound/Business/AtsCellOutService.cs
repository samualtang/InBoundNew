using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using System.Data.EntityClient;
using InBound.Model;

namespace InBound.Business
{
  public  class AtsCellOutService
    {
        public static String getCellNo(String cigarettecode,int qty)
        {
            using (Entities et = new Entities())
            {
                //var paras=new sqlp
                //et.ExecuteStoreCommand("proc_in_cellNO", null);

                ObjectParameter parameter1 = new ObjectParameter("p_code", cigarettecode);
                ObjectParameter parameter2 = new ObjectParameter("p_qty", qty);
                ObjectParameter parameter3 = new ObjectParameter("p_ErrCode","");
                ObjectParameter parameter4 = new ObjectParameter("p_ErrMsg","");
                parameter1.Value = cigarettecode;
                parameter2.Value = qty;
                ObjectParameter[] o = new ObjectParameter[4];
                o[0] = parameter1;
                o[1] = parameter2;
                o[2] = parameter3;
                o[3] = parameter4;
                et.ExecuteFunction("PROC_OUT_CELLNO", o);
                if (parameter3.Value.ToString() == "10")
                {
                    return parameter4.Value.ToString();
                }
                else
                {
                    return "";
                }
              
            }
        }

     public static int ly1=5;
     public static int ly2 = 5;
     public static int ly3 = 5;
     public static int ly4 = 5;
     public static int ly5 = 5;
     public static Dictionary<string, string> initList = new Dictionary<string, string>();
     public static void Init()
     {
        
         if (initList.Count == 0)
         {

             initList.Add("1", "5");
             initList.Add("2","5");
             initList.Add("3","5");
             initList.Add("4", "5");
             initList.Add("5", "5");
             

         }
     }
        public static String getOutCellNo(String cigarettecode, int qty)
        {
            using (Entities et = new Entities())
            {
                List<String> laynot = new List<string>();
                var layn=(from item in et.T_WMS_ATSCELL 
                         join item2 in et.INF_JOBDOWNLOAD 
                         on item.CELLNO equals item2.SOURCE
                         join item3 in et.INF_JOBFEEDBACK
                         on item2.JOBID equals item3.JOBID
                         where item2.JOBTYPE==55 && item3.FEEDBACKSTATUS!=99
                          group item by new { item.LANEWAYNO } into g
                         select new LaneWaynoTaskInfo(){  LaneWayno=g.Key.LANEWAYNO,Count=g.Count() }).ToList();
               
                if (layn != null && layn.Count > 0)
                {
                    foreach (var key in initList.Keys)
                    {
                        if (layn.Find(x => x.LaneWayno ==key) != null)
                        {
                            if (layn.Find(x => x.LaneWayno == key).Count >= int.Parse(initList[key]))
                            {
                                laynot.Add(key);
                            }
                        }
                    }
                    
                }
                //可用设备
                var deviceStatus = (from item in et.T_WMS_DEVICESTATUS
                                   join item2 in et.INF_EQUIPMENTSTATUS
                                   on item.DEVICENO equals item2.EQUIPMENTID
                                   where item2.EQUIPMENTSTATUS == "0" && item.TYPE==30
                                   select item).ToList();

                var query = (from item in et.T_WMS_ATSCELL
                             join item2 in et.T_WMS_ATSCELLINFO
                             on item.CELLNO equals item2.CELLNO
                             join item3 in et.T_WMS_ATSCELLINFO_DETAIL
                             on item2.CELLNO equals item3.CELLNO
                             join item4 in et.T_WMS_LANEWAY
                             on item.LANEWAYNO equals item4.LANEWAYNO
                             where (item.STATUS == 10 || item.STATUS == 20) && item.WORKSTATUS == 20//储位状态正常
                             && (item4.STATUS == 10 || item4.STATUS == 20) //巷道状态正常
                             && item2.STATUS == 30//上架
                             && item3.CIGARETTECODE == cigarettecode && item3.QTY == qty
                             && !laynot.Contains(item.LANEWAYNO)
                             orderby item2.INBOUNDTIME, item.DISTANCE
                             select item.CELLNO).FirstOrDefault();

                if (query == null)
                {
                    var query2 = (from item in et.T_WMS_ATSCELL
                                  join item2 in et.T_WMS_ATSCELLINFO
                                 on item.CELLNO equals item2.CELLNO
                                  join item3 in et.T_WMS_ATSCELLINFO_DETAIL
                                  on item2.CELLNO equals item3.CELLNO
                                  join item4 in et.T_WMS_LANEWAY
                                  on item.LANEWAYNO equals item4.LANEWAYNO
                                  where (item.STATUS == 10 || item.STATUS == 20) && item.WORKSTATUS == 20//储位状态正常
                                  && (item4.STATUS == 10 || item4.STATUS == 20) //巷道状态正常
                                  && item2.STATUS == 30//上架
                                   && !laynot.Contains(item.LANEWAYNO)//
                                  && item3.CIGARETTECODE == cigarettecode && item3.QTY > qty
                                  orderby item2.INBOUNDTIME, item.DISTANCE
                                  select item.CELLNO).FirstOrDefault();
                    if (query2 != null)
                    {
                        return query2;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return query;
                }
            }
 
        }
        public static void UpdateCellOutStatus(String cellno,int requestQty)
        {

              using(Entities et=new Entities())
              {
                  var query=(from item in et.T_WMS_ATSCELL where item.CELLNO==cellno select item).FirstOrDefault();
                  query.WORKSTATUS=30;
                     var query1=(from item in et.T_WMS_ATSCELLINFO where item.CELLNO==cellno select item).FirstOrDefault();
                   query1.STATUS=20;
                     var query2=(from item in et.T_WMS_ATSCELLINFO_DETAIL where item.CELLNO==cellno select item).FirstOrDefault();
                  query2.REQUESTQTY=requestQty;
                  et.SaveChanges();
                  WriteLog.GetLog().Write("修改储位:" + cellno + "状态30,储位明细状态为20,出库申请数量:" + requestQty);
              }
        }
        public static String getCellNoByTime(String cigarettecode)
        {
            using (Entities et = new Entities())
            {
                lock (lockFlag)
                {
                    var query = (from item in et.T_WMS_ATSCELL
                                 join item2 in et.T_WMS_ATSCELLINFO
                                on item.CELLNO equals item2.CELLNO
                                 join item3 in et.T_WMS_ATSCELLINFO_DETAIL
                                 on item2.CELLNO equals item3.CELLNO
                                 join item4 in et.T_WMS_LANEWAY
                                 on item.LANEWAYNO equals item4.LANEWAYNO
                                 join item5 in et.INF_EQUIPMENTSTATUS
                                on item4.LANEWAYNO equals item5.EQUIPMENTID
                                 where (item.STATUS == 10 || item.STATUS == 20) && item.WORKSTATUS == 20//储位状态正常
                                 && (item4.STATUS == 10 || item4.STATUS == 20) //巷道状态正常
                                 && item2.STATUS == 30//上架
                                 && item3.CIGARETTECODE == cigarettecode
                                 && item5.EQUIPMENTSTATUS == "1"
                                 orderby item2.INBOUNDTIME
                                 select item.CELLNO).FirstOrDefault();
                    if (query != null)
                    {
                        return query;
                    }
                    else
                    {
                        return "";

                    }
                }
            }
        }

        public static List<T_WMS_ATSCELL_OUT> getList(int type, String cigaretteCode)
        {
            using (Entities et = new Entities())
            {
                var query = from item in et.T_WMS_ATSCELL_OUT
                             where item.CIGARETTECODE .Contains( cigaretteCode)
                             && item.LOCKRETURNQTY != item.RETURNQTY && item.STATUS==20
                             select item;
                if (type != -1)
                {
                    query = query.Where(item => item.OUTTYPE == type);
                }
                return query.ToList(); ;
 
            }
        }
        public static List<T_WMS_ATSCELL_OUT> getAutoList(String cigaretteCode,String cigaretteName )
        {
            using (Entities et = new Entities())
            {
                var query = from item in et.T_WMS_ATSCELL_OUT
                            where item.CIGARETTECODE.Contains(cigaretteCode) && item.CIGARETTENAME.Contains(cigaretteName)
                            && item.STATUS==10
                            select item;
               
                return query.ToList(); ;

            }
        }
        public static T_WMS_ATSCELL_OUT getDetailById(decimal id)
        {
            using (Entities et = new Entities())
            {
                var query = from item in et.T_WMS_ATSCELL_OUT
                            where item.ID==id
                           
                            select item;

                return query.FirstOrDefault() ;

            }
        }
        public static void updateDetailById(decimal id)
        {
            using (Entities et = new Entities())
            {
                var query = (from item in et.T_WMS_ATSCELL_OUT
                            where item.ID == id

                            select item).FirstOrDefault();

                if (query != null)
                {
                    query.STATUS = 20;
                    et.SaveChanges();
                }

            }
        }
        public static void deleteById(decimal id)
        {
            using (Entities et = new Entities())
            {
                var query = (from item in et.T_WMS_ATSCELL_OUT
                             where item.ID == id

                             select item).FirstOrDefault();

                if (query != null)
                {
                    query.STATUS = 0;
                    et.SaveChanges();
                }

            }
        }
        public static String getCellNoMath(String cigarettecode, int qty)
        {
            using (Entities et = new Entities())
            {
                lock (lockFlag)
                {
                    var query = (from item in et.T_WMS_ATSCELL
                                 join item2 in et.T_WMS_ATSCELLINFO
                                on item.CELLNO equals item2.CELLNO
                                 join item3 in et.T_WMS_ATSCELLINFO_DETAIL
                                 on item2.CELLNO equals item3.CELLNO
                                 join item4 in et.T_WMS_LANEWAY
                                 on item.LANEWAYNO equals item4.LANEWAYNO
                                 join item5 in et.INF_EQUIPMENTSTATUS
                                on item4.LANEWAYNO equals item5.EQUIPMENTID
                                 where (item.STATUS == 10 || item.STATUS == 20) && item.WORKSTATUS == 20//储位状态正常
                                 && (item4.STATUS == 10 || item4.STATUS == 20) //巷道状态正常
                                 && item2.STATUS == 30//上架
                                 && item3.CIGARETTECODE == cigarettecode && item3.QTY >= qty
                                 && item5.EQUIPMENTSTATUS == "1"
                                 orderby item2.INBOUNDTIME, item.DISTANCE
                                 select item.CELLNO).ToList();
                    if (query != null && query.Count > 0)
                    {
                        Random rd = new Random();
                        int seed = rd.Next(query.Count);
                        UpdateCellOutStatus(query[seed], qty);
                        return query[seed];
                    }
                    else
                    {
                        return "";

                    }
                }
            }

        }
        public static String getCellNoEqual(String cigarettecode, int qty)
        {
            using (Entities et = new Entities())
            {
                lock (lockFlag)
                {
                    var query = (from item in et.T_WMS_ATSCELL
                                 join item2 in et.T_WMS_ATSCELLINFO
                                on item.CELLNO equals item2.CELLNO
                                 join item3 in et.T_WMS_ATSCELLINFO_DETAIL
                                 on item2.CELLNO equals item3.CELLNO
                                 join item4 in et.T_WMS_LANEWAY
                                 on item.LANEWAYNO equals item4.LANEWAYNO
                                 join item5 in et.INF_EQUIPMENTSTATUS
                                on item4.LANEWAYNO equals item5.EQUIPMENTID
                                 where (item.STATUS == 10 || item.STATUS == 20) && item.WORKSTATUS == 20//储位状态正常
                                 && (item4.STATUS == 10 || item4.STATUS == 20) //巷道状态正常
                                 && item2.STATUS == 30//上架
                                 && item3.CIGARETTECODE == cigarettecode && item3.QTY == qty
                                 && item5.EQUIPMENTSTATUS == "1"
                                 orderby item2.INBOUNDTIME, item.DISTANCE
                                 select item.CELLNO).FirstOrDefault();
                    if (query != null)
                    {
                        UpdateCellOutStatus(query, qty);
                        return query;
                    }
                    else
                    {
                        return "";

                    }
                }
            }

        }
        public static Object lockFlag = new Object();
       public static String getCellNoBig(String cigarettecode, int qty)
        {
            using (Entities et = new Entities())
            {

                String cellno = getCellNoEqual(cigarettecode, qty);

                if (cellno != "")
                {
                    return cellno;
                }
                lock (lockFlag)
                {

                    var query = (from item in et.T_WMS_ATSCELL
                                 join item2 in et.T_WMS_ATSCELLINFO
                                on item.CELLNO equals item2.CELLNO
                                 join item3 in et.T_WMS_ATSCELLINFO_DETAIL
                                 on item2.CELLNO equals item3.CELLNO
                                 join item4 in et.T_WMS_LANEWAY
                                 on item.LANEWAYNO equals item4.LANEWAYNO
                                 join item5 in et.INF_EQUIPMENTSTATUS
                                 on item4.LANEWAYNO equals item5.EQUIPMENTID
                                 where (item.STATUS == 10 || item.STATUS == 20) && item.WORKSTATUS == 20//储位状态正常
                                 && (item4.STATUS == 10 || item4.STATUS == 20) //巷道状态正常
                                 && item2.STATUS == 30//上架
                                 && item3.CIGARETTECODE == cigarettecode && item3.QTY >= qty
                                 && item5.EQUIPMENTSTATUS == "1" //堆垛机正常
                                 orderby item2.INBOUNDTIME, item.DISTANCE
                                 select item.CELLNO).FirstOrDefault();
                    if (query != null)
                    {
                        UpdateCellOutStatus(query, qty);
                        return query;
                    }
                    else
                    {
                        return "";

                    }
                }
            }
           
        }
        
        public static String getCellNoSmall(String cigarettecode, int qty)
        {
            using (Entities et = new Entities())
            {
                String cellno = getCellNoEqual(cigarettecode, qty);

                if (cellno != "")
                {
                    return cellno;
                }
                lock (lockFlag)
                {
                    var query = (from item in et.T_WMS_ATSCELL
                                 join item2 in et.T_WMS_ATSCELLINFO
                                on item.CELLNO equals item2.CELLNO
                                 join item3 in et.T_WMS_ATSCELLINFO_DETAIL
                                 on item2.CELLNO equals item3.CELLNO
                                 join item4 in et.T_WMS_LANEWAY
                                 on item.LANEWAYNO equals item4.LANEWAYNO
                                 join item5 in et.INF_EQUIPMENTSTATUS
                                on item4.LANEWAYNO equals item5.EQUIPMENTID
                                 where (item.STATUS == 10 || item.STATUS == 20) && item.WORKSTATUS == 20//储位状态正常
                                 && (item4.STATUS == 10 || item4.STATUS == 20) //巷道状态正常
                                 && item2.STATUS == 30//上架
                                 && item3.CIGARETTECODE == cigarettecode && item3.QTY < qty
                                 && item5.EQUIPMENTSTATUS == "1"
                                 orderby item2.INBOUNDTIME, item.DISTANCE
                                 select item.CELLNO).FirstOrDefault();
                    if (query != null)
                    {
                        UpdateCellOutStatus(query, qty);
                        return query;
                    }
                    else
                    {
                        return "";

                    }
                }
            }

        }
        public static void UpdateObject(decimal taskno, decimal qty)
        {
            using (Entities et = new Entities())
            {
                var query = (from item in et.T_WMS_ATSCELL_OUT where item.ID == taskno select item).FirstOrDefault();
                query.ACTQTY += qty;
                //出库类型(10 调拨出库 20 抽检出库 30 人工补货出库 40 盘点出库 50自动补货出库 60 指定货位出库  100 其他)
                if (query.OUTTYPE == 40 || query.OUTTYPE == 60 || query.OUTTYPE ==100)
                {
                    query.RETURNQTY = query.ACTQTY;
                }
                else
                {
                    query.RETURNQTY = query.ACTQTY - query.REQUESTQTY;
                }
                et.SaveChanges();
            }
        }
        public static void UpdateObjectSec(decimal taskno, decimal qty)
        {
            using (Entities et = new Entities())
            {
                var query = (from item in et.T_WMS_ATSCELL_OUT where item.ID == taskno select item).FirstOrDefault();
                query.ACTRETURNQTY += qty;
                et.SaveChanges();
            }
        }
        public static void UpdateObjectThd(decimal taskno, decimal qty)
        {
            using (Entities et = new Entities())
            {
                var query = (from item in et.T_WMS_ATSCELL_OUT where item.ID == taskno select item).FirstOrDefault();
                query.LOCKRETURNQTY += qty;
                et.SaveChanges();
            }
        }
        public static void InsertObject(T_WMS_ATSCELL_OUT cellout)
        {
            using (Entities entity = new Entities())
            {
                entity.T_WMS_ATSCELL_OUT.AddObject(cellout);
                entity.SaveChanges();
            }
        }
    }
}
