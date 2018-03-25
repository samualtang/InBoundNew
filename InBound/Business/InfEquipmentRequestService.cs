using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Business
{
   public class InfEquipmentRequestService:BaseService
    {

        public static void insert(INF_EQUIPMENTREQUEST entity)
        {
            using (Entities dataentity = new Entities())
            {

                entity.REQUESTID = Guid.NewGuid().ToString("N");
                dataentity.AddToINF_EQUIPMENTREQUEST(entity);
            }

        }
        public static INF_EQUIPMENTREQUEST GetEquipMentRequest(String  jobid)
        {
            using (Entities dataentity = new Entities())
            {
                var query = (from item in dataentity.INF_EQUIPMENTREQUEST
                             where item.STATUS == 0 && item.JOBID == jobid && item.REQUESTTYPE == 3 && item.EQUIPMENTID == "1415"
                             select item).FirstOrDefault();
                return query;
            }

        }

        public static void UpdateEquipMentRequest(String jobid, decimal status)
        {
            using (Entities data = new Entities())
            {
                var query = (from item in data.INF_EQUIPMENTREQUEST
                             where item.STATUS == 0 && item.JOBID == jobid && item.REQUESTTYPE == 3 && item.EQUIPMENTID == "1415"
                             select item).FirstOrDefault();
                query.STATUS = status;
                data.SaveChanges();
            }
        }
    }
}
