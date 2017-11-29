using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Business
{
  public  class AtsCellCJService:BaseService
    {


      public static List<InBound.T_WMS_ATSCELL_CJ_HIS> GetItem(String code, String name)
      {
          using (Entities entity = new Entities())
          {
              var query = from item in entity.T_WMS_ATSCELL_CJ_HIS where item.CIGARETTECODE.Contains(code) && item.CIGARETTENAME.Contains(name) select item;
              return query.ToList();
          }
      }
      public static void Del(String cellno)
      {
          using (Entities entity = new Entities())
          {
              var query = (from item in entity.T_WMS_ATSCELL_CJ_HIS where item.CELLNO == cellno select item).FirstOrDefault();
              entity.T_WMS_ATSCELL_CJ_HIS.DeleteObject(query);
              entity.SaveChanges();
          }

 
      }
      public static void InsertEntity(T_WMS_ATSCELL_CJ_HIS entity)
      {
          using (Entities dataEntity = new Entities())
          {

              decimal id = 0;
              // DataTable dt = Query("select S_wms_storagearea_inout.nextval from dual", null);
              id = dataEntity.ExecuteStoreQuery<decimal>("select S_wms_atscell_cj_his.nextval from dual").First(); //decimal.Parse(dt.Rows[0][0].ToString());
              //entity = id + "";
              entity.ID = id;
              entity.CREATETIME = DateTime.Now;
             // entity.STATUS = 10;
              dataEntity.T_WMS_ATSCELL_CJ_HIS.AddObject(entity);
              dataEntity.SaveChanges();
          }
      }
    }
}
