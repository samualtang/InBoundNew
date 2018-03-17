using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;
namespace InBound.Business
{
  public  class ItemService
    {
      public static List<InBound.T_WMS_ITEM> GetItem(String code, String name)
      {
          using (Entities entity = new Entities())
          {
              var query = from item in entity.T_WMS_ITEM where   item.BIGBOX_BAR.Contains(code) &&  item.ITEMNAME.Contains(name) && item.ITEMNO.Length==7 select item;
              return query.ToList();
          }
      }

      public static T_WMS_ITEM GetItemByCode(String code)
      {
          using (Entities entity = new Entities())
          {
              var query = (from item in entity.T_WMS_ITEM where item.ITEMNO==code && item.ITEMNO.Length == 7 select item).FirstOrDefault();
              return query;
          }
      }
      public static T_WMS_ITEM GetItemByBarCode(String barcode)
      {
          using (Entities entity = new Entities())
          {
              var query = (from item in entity.T_WMS_ITEM where item.BIGBOX_BAR == barcode && item.ITEMNO.Length == 7  select item).FirstOrDefault();
              return query;
          }
      }
    }
}
