using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Business
{
  public  class RefRFIDPalletService:BaseService
    {

      public static String GetPallet(String rfid)
      {
          using (Entities entity = new Entities())
          {
              var query =( from item in entity.T_WMS_REF_PALLET_RFID where item.RFID == rfid select item).FirstOrDefault();
              if (query != null)
              {
                  var q = (from item in entity.T_WMS_ATSCELLINFO where item.PALLETNO == query.PALLECTNO select item).FirstOrDefault();
                  if (q != null)
                  {
                      return "E";

                  }
                  else
                  {
                      return query.PALLECTNO;

                  }
                                }
              else
              {
                  T_WMS_REF_PALLET_RFID et = new T_WMS_REF_PALLET_RFID();
                  decimal id = 0;
                  // DataTable dt = Query("select S_wms_storagearea_inout.nextval from dual", null);
                  id = entity.ExecuteStoreQuery<decimal>("select S_t_wms_ref_pallet_rfid.nextval from dual").First(); //decimal.Parse(dt.Rows[0][0].ToString());
                
                  et.ID = id ;
                  et.RFID=rfid;
                  et.PALLECTNO = id+"";
                  entity.T_WMS_REF_PALLET_RFID.AddObject(et);
                  entity.SaveChanges();
                 
                  return id+"";
              }
          }
      }
    }
}
