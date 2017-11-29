using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Business
{
  public  class InBoundLineService
    {
      public static List<InBound.T_WMS_INBOUND_LINE> GetItem(String code, String name)
      {
          using (Entities entity = new Entities())
          {
              var query = from item in entity.T_WMS_INBOUND_LINE where item.CIGARETTECODE.Contains(code) && item.CIGARETTENAME.Contains(name) && (item.BOXQTY - item.ABOXQTY-item.LOCKQTY) > 0 && item.STATUS!="30" select item;
              return query.ToList();
          }
      }
      public static List<T_WMS_ITEM> GetInBoundCigarette(String name, String code)
      {
          using (Entities entity = new Entities())
          {
              var query = from item in entity.T_WMS_ITEM
                          join item2 in entity.T_WMS_INBOUND_LINE
                          on item.ITEMNO equals item2.CIGARETTECODE
                          join item3 in entity.T_WMS_INBOUND
                          on item2.INBOUNDID equals item3.INBOUNDID
                          where item3.STATUS != "30" && item2.BOXQTY!=item2.ABOXQTY
                          select item;
              if (name != "")
              {
                  query = query.Where(item => item.ITEMNAME.Contains(name));
              }
              if (code != "")
              {
                  query = query.Where(item => item.ITEMNO.Contains(code));
              }
              return query.Distinct().ToList();
          }
      }
      public static List<InBound.T_WMS_INBOUND_LINE> GetItem()
      {
          using (Entities entity = new Entities())
          {
              var query = from item in entity.T_WMS_INBOUND_LINE join item2 in entity.T_WMS_INBOUND
                          on item.INBOUNDID equals item2.INBOUNDID
                          where item2.STATUS!="10" && item2.STATUS!="30" orderby item.INBOUNDID select item;
              return query.ToList();
          }
      }
      public static List<InBound.T_WMS_INBOUND_LINE> GetItem(decimal inboundid)
      {
          using (Entities entity = new Entities())
          {
              var query = from item in entity.T_WMS_INBOUND_LINE 
                          //是否加载满托盘 以及垛形
                          where item.INBOUNDID==inboundid //&& item.BOXQTY-item.LOCKQTY>0
                          select item;
              return query.ToList();
          }
      }
      public static InBound.T_WMS_INBOUND_LINE GetItemByID(int id)
      {
          using (Entities entity = new Entities())
          {
              var query = from item in entity.T_WMS_INBOUND_LINE where item.INBOUNDDETAILID==id select item;
              return query.FirstOrDefault();
          }
      }
      public static void Update(decimal id, decimal anum,decimal locknum)
      {
          using (Entities entity = new Entities())
          {
              var query = (from item in entity.T_WMS_INBOUND_LINE 
                           where item.INBOUNDDETAILID == id select item).FirstOrDefault();
              if (query != null)
                  query.ABOXQTY += anum;
              query.LOCKQTY += locknum;
              entity.SaveChanges();
          }
      }
    }
}
