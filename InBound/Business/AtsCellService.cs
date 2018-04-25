using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Model;

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
      public static void UpdateAtsCell(String cellno, decimal status,Entities data)
      {
          
              var query = (from item in data.T_WMS_ATSCELL where item.CELLNO == cellno select item).FirstOrDefault();
              if (query != null)
              {
                  query.WORKSTATUS = status;

                 // data.SaveChanges();
                  WriteLog.GetLog().Write("修改储位工作状态:" + cellno + ":" + status);
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
                  WriteLog.GetLog().Write("修改储位工作状态:" + cellno + ":" + status);
              }
          }
      }
      public static List<UseRate> GetUseRate()
      {
          using (Entities entity = new Entities())
          {
              var query1 = (from item in entity.T_WMS_ATSCELL
                           where item.WORKSTATUS != 10
                           group item by new { item.LANEWAYNO } into g
                            select new UseRate() {  KeyName1 = g.Key.LANEWAYNO,  UseTotal = g.Count() }).ToList();
              var query2 = (from item in entity.T_WMS_ATSCELL
                            orderby item.LANEWAYNO
                            group item by new { item.LANEWAYNO } into g
                            select new UseRate() { KeyName1 = g.Key.LANEWAYNO, UseTotal = 0, Total = g.Count() }).ToList();
              query2.ForEach(x => x.UseTotal = (query1.Find(y => y.KeyName1 == x.KeyName1)==null ? 0 : query1.Find(y => y.KeyName1 == x.KeyName1).UseTotal));
              query2.ForEach(x => x.Usage =Decimal.Parse(( x.UseTotal*100/ x.Total).ToString("0.00")));
              query2=query2.OrderBy(x => x.KeyName1).ToList();
              return query2;
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
