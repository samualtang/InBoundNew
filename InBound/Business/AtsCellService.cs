using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Business
{
  public  class AtsCellService:BaseService
    {
      public Object GetAtsCell()
      {
          using(Entities data=new Entities())
          {
              var query=from item in data.T_WMS_ATSCELL 
                        join item2 in data.T_WMS_ATSCELLINFO
                        on   item.CELLNO equals item2.CELLNO  
                       where item.STATUS==1 select new {item.CELLNO,item.CELLNAME,item2.DISMANTLE};
               return query.FirstOrDefault();
          }
      }
      public static List<T_WMS_ATSCELL> GetAtsCellList(decimal status)
      {
          using (Entities data = new Entities())
          {
              var query = from item in data.T_WMS_ATSCELL
                                         where item.STATUS == status
                          select item;
              return query.ToList();
          }
      }
      public static void UpdateAtsCell(String cellno,decimal status)
      {
          using(Entities data=new Entities())
          {
              var query=(from item in data.T_WMS_ATSCELL where item.CELLNO==cellno select item).FirstOrDefault();
              if(query!=null)
              {
                  query.WORKSTATUS = status;
                  data.SaveChanges();
              }
          }
      }
      public static void UpdateAtsCell1(String cellno, decimal status)
      {
          using (Entities data = new Entities())
          {
              var query = (from item in data.T_WMS_ATSCELL where item.CELLNO == cellno select item).FirstOrDefault();
              if (query != null)
              {
                  query.STATUS = status;
                  data.SaveChanges();
              }
          }
      }
      public static List<T_WMS_ATSCELL> GetAtsCellList(String lanewayno, decimal floor,decimal col)
      {
          using (Entities entity = new Entities())
          {
              var query = from item in entity.T_WMS_ATSCELL where item.LANEWAYNO == lanewayno && item.FLOOR == floor && item.COL==col select item;
              return query.ToList();
          }
      }
      public static T_WMS_ATSCELL  GetAtsCell(String cellno)
      {
          using (Entities data = new Entities())
          {
              var query = (from item in data.T_WMS_ATSCELL where item.CELLNO == cellno select item).FirstOrDefault();
              return query;
          }
      }
    }
}
