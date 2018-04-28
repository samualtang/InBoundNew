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
  public  class AtsCellInService
    {
        public static String getCellNo(String cigarettecode)
        {
            using (Entities et = new Entities())
            {
                //var paras=new sqlp
                //et.ExecuteStoreCommand("proc_in_cellNO", null);

                ObjectParameter parameter1 = new ObjectParameter("p_code", cigarettecode);
                ObjectParameter parameter2 = new ObjectParameter("p_ErrCode","");
                ObjectParameter parameter3 = new ObjectParameter("p_ErrMsg","");
                parameter1.Value = cigarettecode;
               
                ObjectParameter[] o = new ObjectParameter[3];
                o[0] = parameter1;
                o[1] = parameter2;
                o[2] = parameter3;
                et.ExecuteFunction("PROC_IN_CELLNO", o);
                if (parameter2.Value.ToString() == "10")
                {
                    return parameter3.Value.ToString();
                }
                else
                {
                    return "";
                }
              
            }
        }

        public static Object lockFlag = new Object();

        public static List<String> getLaneWayTaskCount()
        {
            using (Entities et = new Entities())
            {

                var taskqy = (from item in et.INF_JOBDOWNLOAD
                              where (item.JOBTYPE == 20 || item.JOBTYPE==30)
                                  && item.STATUS != 10
                              select new LaneWayTaskCount() { layewayno = "3" + item.TARGET.Substring(0, 3), taskCount = 1 }).ToList();
                if (taskqy != null && taskqy.Count > 0)
                {
                    taskqy.Add(new LaneWayTaskCount() { layewayno = "3001", taskCount = 1 });
                    taskqy.Add(new LaneWayTaskCount() { layewayno = "3002", taskCount = 1 });
                    taskqy.Add(new LaneWayTaskCount() { layewayno = "3003", taskCount = 1 });
                    taskqy.Add(new LaneWayTaskCount() { layewayno = "3004", taskCount = 1 });
                    taskqy.Add(new LaneWayTaskCount() { layewayno = "3005", taskCount = 1 });
                    taskqy = taskqy.GroupBy(x => x.layewayno).Select(group => new LaneWayTaskCount() { layewayno = group.Key, taskCount = group.Count() - 1 }).ToList();

                    var query = (from item in et.T_WMS_DEVICESTATUS where item.TYPE == 30 select item).ToList();
                    var query1 = (from item in taskqy
                                  join item2 in query
                                      on item.layewayno equals item2.DEVICENO
                                  select new LaneWayTaskCount() { layewayno = item.layewayno.Substring(3, 1), taskCount = item.taskCount, maxCount = Decimal.Parse(item2.TROUGHNUM) }).ToList().Where(x => x.taskCount < x.maxCount).Select(x => x.layewayno).ToList();


                    return query1;
                }
                else
                {
                    return new List<string>() { "1", "2", "3", "4", "5" };
                }
            }

        }
        public static String getCellNoCode(String barcode)
        {
            using (Entities et = new Entities())
            {
                lock(lockFlag)
                {
                    var list = getLaneWayTaskCount();
                    var query = (from item in et.T_WMS_ATSCELL
                                 join item4 in et.T_WMS_LANEWAY
                                 on item.LANEWAYNO equals item4.LANEWAYNO
                                 join item5 in et.INF_EQUIPMENTSTATUS
                                 on item4.LANEWAYNO equals item5.EQUIPMENTID
                                 where (item.STATUS == 10 || item.STATUS == 30) && item.WORKSTATUS == 10//储位空闲状态正常
                                 && (item4.STATUS == 10 || item4.STATUS == 30)  //巷道状态正常
                                 && item5.EQUIPMENTSTATUS=="1" //堆垛机正常
                                 && list.Contains(item4.LANEWAYNO) orderby item.LANEWAYNO descending
                                 select item.LANEWAYNO).Distinct().ToList();
                    if (query != null && query.Count>0)
                    {
                        var query1 = (from item in et.T_WMS_ATSCELLINFO_DETAIL
                                      join item2 in et.T_WMS_ATSCELL
                                      on item.CELLNO equals item2.CELLNO
                                      where item.BARCODE == barcode && query.Contains(item2.LANEWAYNO)
                                      select new InBoundTask { LANEWAYNO = item2.LANEWAYNO, CIGARETTECODE = item.CIGARETTECODE, QTY = item.QTY ?? 0 }).GroupBy(x => x.LANEWAYNO).Select(g => new InBoundTask { LANEWAYNO = g.Key, CIGARETTECODE = barcode, QTY = g.Sum(item => item.QTY) }).ToList();
                        string tempno = "";
                        decimal ctCount = 99999999;
                        foreach (var item in query)
                        {

                            InBoundTask temp = query1.Find(x => x.LANEWAYNO == item);
                            if (temp != null)
                            {
                                if (temp.QTY < ctCount)
                                {
                                    ctCount = temp.QTY;
                                    tempno = item;
                                }
                            }
                            else
                            {
                                tempno = item;
                                break;
                            }
                        }
                        var query3 = (from item in et.T_WMS_ATSCELL where item.LANEWAYNO == tempno && (item.STATUS == 10 || item.STATUS == 30) && item.WORKSTATUS == 10 orderby item.DISTANCE select item).FirstOrDefault();
                        
                        query3.WORKSTATUS = 30;
                        var query4 = (from item in et.T_WMS_ATSCELLINFO_DETAIL where item.CELLNO == query3.CELLNO select item).FirstOrDefault();
                        et.SaveChanges();
                        WriteLog.GetLog().Write("修改储位号:" + query3.CELLNO + "状态为30");
                        return query3.CELLNO;
                    }
                    else
                    {
                        WriteLog.GetLog().Write("没有可用的堆垛机");
                        return "";

                    }
                    }
                    
                
            }
        }

        public void InsertObject(T_WMS_ATSCELL_IN cellin)
        {
            using (Entities entity = new Entities())
            {
                entity.T_WMS_ATSCELL_IN.AddObject(cellin);
                entity.SaveChanges();
            }
        }
       public static String getCellNo(String cellno ,String laneyno)
        {
            using (Entities et = new Entities())
            {
                //var paras=new sqlp
                //et.ExecuteStoreCommand("proc_in_cellNO", null);

                ObjectParameter parameter1 = new ObjectParameter("p_code", cellno);
                ObjectParameter parameter2 = new ObjectParameter("p_lanewayno", laneyno);
                
                ObjectParameter parameter3 = new ObjectParameter("p_ErrCode","");
                ObjectParameter parameter4 = new ObjectParameter("p_ErrMsg","");
              //  parameter1.Value = cigarettecode;
               
                ObjectParameter[] o = new ObjectParameter[4];
                o[0] = parameter1;
                o[1] = parameter2;
                o[2] = parameter3;
                o[3] = parameter4;
                et.ExecuteFunction("PROC_IN_FIXLANEWAY", o);
                if (parameter2.Value.ToString() == "10")
                {
                    return parameter3.Value.ToString();
                }
                else
                {
                    return "";
                }
              
            }
        }

      
    }
}
