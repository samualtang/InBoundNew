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
    }
}
