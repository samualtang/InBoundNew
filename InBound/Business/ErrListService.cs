using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Business
{
  public  class ErrListService:BaseService
    {


      public static void Add(String deviceno,decimal groupno,int type,string errMsg)
      {

          using (Entities dataEntity = new Entities())
          {
              T_WMS_DEVICEERRLIST entity = new T_WMS_DEVICEERRLIST();
              entity.ID = GetSeq("select seq_deviceerrlist.nextval from dual");
              entity.DECICENO = deviceno;
              entity.DEVICENAME = deviceno;
              entity.GROUPNO = groupno;
              entity.ERRTIME = DateTime.Now;
              entity.ERRORMSG = errMsg;
              entity.ERRORTYPE = type;
              dataEntity.T_WMS_DEVICEERRLIST.AddObject(entity);
              dataEntity.SaveChanges();
          }
      }

    
    }
}
