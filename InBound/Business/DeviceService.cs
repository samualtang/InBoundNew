using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Business
{
  public  class DeviceService
    {
      public static List<T_WMS_DEVICESTATUS> GetList()
      {
          using (Entities entity = new Entities())
          {
              var query = from item in entity.T_WMS_DEVICESTATUS orderby item.ID select item;
              return query.ToList();
          }
      }
      public static List<T_WMS_DEVICESTATUS> GetList(decimal status ,decimal type)
      {
          using (Entities entity = new Entities())
          {
              var query = from item in entity.T_WMS_DEVICESTATUS join  item2 in entity.INF_EQUIPMENTSTATUS
                                                                 on item.DEVICENO equals item2.EQUIPMENTID
                          where item.STATUS==status && item.TYPE==type && item2.EQUIPMENTSTATUS=="0" orderby item.ID select item;
              return query.ToList();
          }
      }
      public static void UpdateEntity(String code, int status)
      {
          using (Entities entity = new Entities())
          {
              var query = (from item in entity.T_WMS_DEVICESTATUS where item.DEVICENO == code select item).First();
              query.STATUS = status;
              entity.SaveChanges();
          }
      }

       
    /// 开箱机对应烟柜通道
    /// </summary>
    /// <param name="DEVICENO">设备编号</param>
    /// <param name="troughnum">通道编号</param>
    /// <param name="MaxTaskNum">最大任务数</param>
      public static void UpdateDevice(String DEVICENO, String troughnum, int MaxTaskNum  )
      {
          using (Entities entity = new Entities())
          {
              var query = (from item in entity.T_WMS_DEVICESTATUS where item.DEVICENO == DEVICENO select item).First();
              query.TROUGHNUM = troughnum;
              query.MAXTASKNUM = MaxTaskNum;
              entity.SaveChanges();
          }
      }
      /// <summary>
      /// 堆垛机最大任务数
      /// </summary>
      /// <param name="DEVICENO">设备编号</param>
      /// <param name="OutMaxTaskNum">出库最大任务数</param>
      /// <param name="InMaxTaskNum">入库最大任务数</param>
      public static void UpdateDevice(String DEVICENO, String OutMaxTaskNum, String InMaxTaskNum)
      {
          using (Entities entity = new Entities())
          {
              var query = (from item in entity.T_WMS_DEVICESTATUS where item.DEVICENO == DEVICENO select item).First();
              query.MAXTASKNUM =Convert.ToDecimal( OutMaxTaskNum );
              query.TROUGHNUM = InMaxTaskNum.ToString();
              entity.SaveChanges();
          }
      }




      public static void AddEntity()
      {
          using (Entities entity = new Entities())
          {
              for (int i = 0; i < 3; i++)
              {
                  T_WMS_LANEWAY way = new T_WMS_LANEWAY();
                  way.LANEWAYNO = (20 + i)+"";
                  way.LANEWAYNAME = "";
                  way.STATUS = 10;
                  entity.T_WMS_LANEWAY.AddObject(way);
                int k= GetList().Count;
                int count = k;
                  ///  
                entity.SaveChanges(); 
              }
              
          }
      }
    }
}
