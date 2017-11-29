﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Business
{
  public  class Inf_EquipmentStatusService
    {
      public static List<INF_EQUIPMENTSTATUS> GetList()
      {
          using (Entities entity = new Entities())
          {
              var query = from item in entity.INF_EQUIPMENTSTATUS orderby item.EQUIPMENTID select item;
              return query.ToList();
          }
      }

   
      
    }
}
