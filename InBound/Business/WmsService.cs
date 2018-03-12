using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Business
{
   public class WmsService
    {
        public T_WMS_ITEM GetItemByCode(String code)
        {
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_WMS_ITEM where item.ITEMNO == code select item).FirstOrDefault();
                return query;
            }
        }

        public List<T_WMS_ITEM> GetItemByCode()
        {
            using (Entities entity = new Entities())
            {
                var query = from item in entity.T_WMS_ITEM select item;
                return query.ToList();
            }
        }

        public List<T_WMS_ITEM> GetItemPageList(String name,String code,int currentPage,int pageSize,out int total)
        {
            using (Entities entity = new Entities())
            {
                var query = from item in entity.T_WMS_ITEM  where item.ITEMTYPE_ID=="1" select item;
                if (name != "")
                {
                    query = query.Where(item => item.ITEMNAME.Contains(name));
                }
                if (code != "")
                {
                    query = query.Where(item => item.ITEMNO.Contains(code));
                }
              
                total = query.Count();
                return query.OrderBy(item => item.ITEMNAME).Skip(currentPage*pageSize).Take(pageSize).ToList();
            }
        }
        //根据件烟码取商品名称
        public T_WMS_ITEM GetItemByBarCode(String barcode)
        {
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_WMS_ITEM where item.BIGBOX_BAR == barcode select item).FirstOrDefault();
                return query;
            }
        }
    }
}
