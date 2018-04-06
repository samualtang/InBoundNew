using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace InBound.Business
{
   public class StroageInOutService:BaseService
    {
       public static void InsertEntity(T_WMS_STORAGEAREA_INOUT entity)
       {
           using (Entities dataEntity = new Entities())
           {

               decimal id = 0;
              // DataTable dt = Query("select S_wms_storagearea_inout.nextval from dual", null);
               id = dataEntity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First(); //decimal.Parse(dt.Rows[0][0].ToString());
               entity.TASKNO = id+"";
               entity.ID = id;
               entity.CREATETIME = DateTime.Now;
               entity.STATUS = 10;
               dataEntity.T_WMS_STORAGEAREA_INOUT.AddObject(entity);
               dataEntity.SaveChanges();
            }
       }

       public static void UpdateInOut(String taskno, decimal qty, Entities dataEntity)
       {
          
               var query = (from item in dataEntity.T_WMS_STORAGEAREA_INOUT where item.TASKNO == taskno select item).FirstOrDefault();
               if (query != null)
               {
                   query.BOXQTY = qty;
                  // dataEntity.SaveChanges();
               }

          
       }
       public static void RollBackOrder(string billcode)
       {
           using (Entities dataEntity = new Entities())
           {
               var query = (from item in dataEntity.T_WMS_STORAGEAREA_INOUT where item.TASKNO == billcode select item).ToList();
               if (query != null)
               {
                   foreach (var item in query)
                   {
                       dataEntity.T_WMS_STORAGEAREA_INOUT.DeleteObject(item);

                   }
                   dataEntity.SaveChanges();
               }
           }
 
       }
       public static void RollBackOrder(string billcode,decimal groupNo)
       {
           using (Entities dataEntity = new Entities())
           {
               var query = (from item in dataEntity.T_WMS_STORAGEAREA_INOUT where item.TASKNO == billcode && item.GROUPNO==groupNo select item).ToList();
               if (query != null)
               {
                   foreach (var item in query)
                   {
                       dataEntity.T_WMS_STORAGEAREA_INOUT.DeleteObject(item);

                   }
                   dataEntity.SaveChanges();
               }
           }

       }
       public static List<T_WMS_STORAGEAREA_INOUT> GetDetail(int type, int areaId, int status)
       {
           using (Entities dataEntity = new Entities())
           {
               var query = from item in dataEntity.T_WMS_STORAGEAREA_INOUT where item.INOUTTYPE == type && item.AREAID == areaId && item.STATUS == status select item;
               return query.ToList();
           }
       }
    }
}
