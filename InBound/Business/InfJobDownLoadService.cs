using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Model;
using System.Configuration;

namespace InBound.Business
{
  public  class InfJobDownLoadService:BaseService
    {
      public static void InsertEntity(INF_JOBDOWNLOAD entity)
      {
          using (Entities dataEntity = new Entities())
          {

              decimal id = 0;
       
              id = dataEntity.ExecuteStoreQuery<decimal>("select s_inf_jobdownload.nextval from dual").First(); //decimal.Parse(dt.Rows[0][0].ToString());
              entity.JOBID = id+"";
              entity.ID = id+"";
              entity.STATUS = 0;
              dataEntity.INF_JOBDOWNLOAD.AddObject(entity);
              dataEntity.SaveChanges();
          }
      }
      public static void InsertCancelTask(INF_JOBDOWNLOAD entity)
      {
          using (Entities dataEntity = new Entities())
          {

              decimal id = 0;
              // DataTable dt = Query("select S_wms_storagearea_inout.nextval from dual", null);
              id = dataEntity.ExecuteStoreQuery<decimal>("select s_inf_jobdownload.nextval from dual").First(); //decimal.Parse(dt.Rows[0][0].ToString());
              //entity.JOBID = id + "";
              entity.ID = id + "";
              entity.STATUS = 0;

              dataEntity.INF_JOBDOWNLOAD.AddObject(entity);
              dataEntity.SaveChanges();
          }
      }
      public static List<INF_JOBDOWNLOAD> Query(int jobtype)
      {
          using (Entities dataEntity = new Entities())
          {
              if (jobtype == -1)
              {
                  var query = from item in dataEntity.INF_JOBDOWNLOAD  select item;
                  return query.ToList();
              }
              else
              {
                  var query = from item in dataEntity.INF_JOBDOWNLOAD where item.JOBTYPE == jobtype select item;
                  return query.ToList();
              }
          }
 
      }
      public static INF_JOBDOWNLOAD QueryManual()
      {
          using (Entities entity = new Entities())
          {
              var query = (from item in entity.INF_JOBDOWNLOAD
                           join item2 in entity.INF_JOBFEEDBACK on item.JOBID equals item2.JOBID
                           where  item.JOBTYPE == 55 && item.TARGET == "1415" && item2.FEEDBACKSTATUS == 99 
                           orderby item.RESPONDDATE descending
                           select item).FirstOrDefault();
              return query;
          }
      }
      public static List<Error> QueryError()
      {
          using (Entities entity = new Entities())
          {

              DateTime dt = DateTime.Now.AddDays(-1);
              var query = (from item in entity.INF_JOBDOWNLOAD
                           join item2 in entity.INF_JOBFEEDBACK on item.JOBID equals item2.JOBID
                           where item2.FEEDBACKSTATUS == 98 && item.RESPONDDATE > dt
                           orderby item.RESPONDDATE descending
                           select new Error{ BRANDID=item.BRANDID, RESDATE =item.RESPONDDATE, JOBID=item.JOBID, PLANQTY=item.PLANQTY, JOBTYPE=item.JOBTYPE, SOURCE=item.SOURCE, TARGET=item.TARGET, ERRORCODE=item2.ERRORCODE }).ToList();
              return query ;
          }
      }
      public static Dictionary<string, string> initList = new Dictionary<string, string>();
     public static decimal vc1412 = 0;
     public static decimal vc1409 = 0;
      public static void Init()
      {
          vc1412 = decimal.Parse(ConfigurationManager.AppSettings["1412Virtual"].ToString());
          vc1409 = decimal.Parse(ConfigurationManager.AppSettings["1409Virtual"].ToString());
          if (initList.Count ==0)
          {
            
               initList.Add("1391", ConfigurationManager.AppSettings["1391"].ToString());
               initList.Add("1394", ConfigurationManager.AppSettings["1394"].ToString());
               initList.Add("1397", ConfigurationManager.AppSettings["1397"].ToString());
               initList.Add("1400", ConfigurationManager.AppSettings["1400"].ToString());
               initList.Add("1403", ConfigurationManager.AppSettings["1403"].ToString());
               initList.Add("1406", ConfigurationManager.AppSettings["1406"].ToString());
               initList.Add("1409", ConfigurationManager.AppSettings["1409"].ToString());
               initList.Add("1412", ConfigurationManager.AppSettings["1412"].ToString());
               initList.Add("1415", ConfigurationManager.AppSettings["1415"].ToString());
             

          }
      }

      
      public static String GetTargetOutAddress(string cellno, decimal qty)
      {
          bool istest = false;
          using(Entities entity=new Entities())
          {
              istest = false;
              if (istest)
              {
                  return "1412";
              }

              Init();//阀值
              var query = AtsCellInfoDetailService.GetDetail(cellno);
              var query2 = AtsCellInfoService.GetCellInfo(cellno);
                          //可用拆垛工位列表
              List<String> list = new List<string>();
              var tempList = new List<AddressInfo>();
              var deviceList = new List<String>();
              List<T_WMS_DEVICESTATUS> listdevice = DeviceService.GetList(10, 40);//可用设备
              foreach (var item in listdevice)
              {
                  if (item.DEVICENO == "4001")
                  {
                      list.Add("1391");
                      list.Add("1394");
                      deviceList.Add("4001");
                      tempList.Add(new AddressInfo(){ DeviceNo="4001" , DeviceCount=0, Address="1391", Count=0});
                      tempList.Add(new AddressInfo() { DeviceNo = "4001", DeviceCount = 0, Address = "1394", Count = 0 });

                  }
                  else if (item.DEVICENO == "4002")
                  {
                      list.Add("1397");
                      list.Add("1400");
                      deviceList.Add("4002");
                      tempList.Add(new AddressInfo() { DeviceNo = "4002", DeviceCount = 0, Address = "1397", Count = 0 });
                      tempList.Add(new AddressInfo() { DeviceNo = "4002", DeviceCount = 0, Address = "1400", Count = 0 });
                  }
                  else if (item.DEVICENO == "4003")
                  {
                      list.Add("1403");
                      list.Add("1406");
                      deviceList.Add("4003");
                      tempList.Add(new AddressInfo() { DeviceNo = "4003", DeviceCount = 0, Address = "1403", Count = 0 });
                      tempList.Add(new AddressInfo() { DeviceNo = "4003", DeviceCount = 0, Address = "1406", Count = 0 });
                  }
                  else if (item.DEVICENO == "4004")
                  {
                      list.Add("1409");
                      list.Add("1412");
                      deviceList.Add("4004");
                      tempList.Add(new AddressInfo() { DeviceNo = "4004", DeviceCount = 0, Address = "1409", Count = 0 });
                      tempList.Add(new AddressInfo() { DeviceNo = "4004", DeviceCount = 0, Address = "1412", Count = 0 });
                  }
                  list.Add("1415");
                  tempList.Add(new AddressInfo() { DeviceNo = "4005", DeviceCount = 0, Address = "1415", Count = 0 });
              }

              var addressList = (from item in entity.INF_JOBDOWNLOAD
                                 join item2 in entity.INF_JOBFEEDBACK on item.JOBID equals item2.JOBID
                                 where item.JOBTYPE == 55 && item2.FEEDBACKSTATUS != 99 && list.Contains(item.TARGET)
                                 group item by new { item.TARGET } into g
                                 select new AddressInfo() { Address = g.Key.TARGET, Count = g.Count(t => t.TARGET == g.Key.TARGET) }).ToList();
             
              foreach (var add in addressList)//设置任务数
              {
                  if (tempList.Find(x => x.Address == add.Address) != null)
                  {
                    var temp=  tempList.Find(x => x.Address == add.Address);
                    temp.Count = add.Count;
                    List<AddressInfo> info = tempList.FindAll(x => x.DeviceNo == temp.DeviceNo);
                    foreach (var item in info)
                    {
                        item.DeviceCount += temp.Count;
                    }

                  }
              }
              //tempList = tempList.OrderBy(s => new { s.DeviceCount, s.DeviceNo, s.Count }).ToList();

              if (query2!=null && query2.DISMANTLE == 10)
              {
                 
               
                  //List<INF_EQUIPMENTSTATUS> listEquip = Inf_EquipmentStatusService.GetList();
                  
                    

                  if (query.QTY == qty)
                  {

                      if (tempList != null && tempList.Count > 0)
                       {

                           String tempAdd = "";
                           foreach (var add in list)
                           {
                               //if (addressList.Find(x => x.Address == add) == null)
                               //{
                               //    tempAdd = add;
                               //    break;
                               //}
                               //else//阀值设定
                               //{
                                   if (add == "1412")
                                   {
                                       tempList.Find(x => x.Address == add).Threshold = decimal.Parse(initList[add]) + vc1412;
                                   }
                                   else if (add == "1409")
                                   {
                                       tempList.Find(x => x.Address == add).Threshold = decimal.Parse(initList[add]) + vc1409;
                                   }
                                   else
                                   {
                                       tempList.Find(x => x.Address == add).Threshold = decimal.Parse(initList[add]);
                                   }

                               //}
                           }
                           //if (tempAdd != "")
                           //{
                           //    return tempAdd;
                           //}
                           //else
                           //{
                              List<AddressInfo> tempList1=    tempList.FindAll(x=>x.Count<x.Threshold);

                              if (tempList1 == null || tempList1.Count == 0)
                              {
                                  return "";
                              }
                              else
                              {
                                  decimal maxTask = 0;
                                  foreach (var item in tempList)
                                  {
                                      if ((item.Threshold - item.Count) > maxTask)
                                      {
                                          maxTask = item.Threshold - item.Count;
                                          tempAdd = item.Address;
                                      }

                                  }
                                  return tempAdd;
                              }
                              
                           //}
                       }
                       else
                       {
                           return list[0];
                       }

                  }
                  else
                  {
                      if (list.Contains("1412"))
                      {

                          if (tempList != null && tempList.Count > 0)
                          {
                              var temp = tempList.Find(x => x.Address == "1412");
                              if (temp == null)
                              {
                                  return "1412";//返库站台
                              }
                              else
                              {
                                  if (temp.Count < decimal.Parse(initList["1412"]))
                                  {
                                      return "1412";
                                  }
                                  else
                                  {
                                      return "";
                                  }
                              }
                          }
                          else
                          {
                              return "1412";//
                          }
                      }
                      else
                      {
                          return "";
                      }
                  }
              }
              else
              {
                  if (tempList != null && tempList.Count > 0)
                  {
                      var temp = tempList.Find(x => x.Address == "1415");
                      if (temp == null)
                      {
                          return "1415";//人工站台
                      }
                      else
                      {
                          if (temp.Count < decimal.Parse(initList["1415"]))
                          {
                              return "1415";
                          }
                          else
                          {
                              return "";
                          }
                      }
                  }
                  else
                  {
                      return "1415";//
                  }
              }
          }
          
      }
      public static INF_JOBDOWNLOAD GetDetail(string jobid)
      {
          using (Entities entity = new Entities())
          {
              var query = (from item in entity.INF_JOBDOWNLOAD where item.JOBID == jobid select item).FirstOrDefault();
                  return query;
          }
      }
      public static INF_JOBDOWNLOAD GetDetail(string jobid,Entities entity)
      {
          
              var query = (from item in entity.INF_JOBDOWNLOAD where item.JOBID == jobid select item).FirstOrDefault();
              return query;
          
      }

      public static Boolean CheckJobExist(decimal inboundNO)
      {
          using (Entities entity = new Entities())
          {
              var query = (from item in entity.INF_JOBDOWNLOAD where item.INBOUNDNO == inboundNO select item).FirstOrDefault();
              if (query != null && query.INBOUNDNO == inboundNO)
              {
                  var query2 = (from item in entity.INF_JOBDOWNLOAD where item.JOBID == query.JOBID && item.JOBTYPE == 97 select item).FirstOrDefault();
                  if (query2 != null)
                  {
                      return false;
                  }
                  else
                  {
                      return true;
                  }
              }
              else
              {
                  return false;
              }
          }
         
      }
    }
}
