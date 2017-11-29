using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Business
{
  public  class LaneWayService
    {
      public static List<T_WMS_LANEWAY> GetList()
      {
          using (Entities entity = new Entities())
          {
              var query = from item in entity.T_WMS_LANEWAY select item;
              return query.ToList();
          }
      }

      public static void UpdateEntity(String code, int status)
      {
          using (Entities entity = new Entities())
          {
              var query = (from item in entity.T_WMS_LANEWAY where item.LANEWAYNO == code select item).First();
              query.STATUS = status;
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
