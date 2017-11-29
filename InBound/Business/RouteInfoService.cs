using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Business
{
  public  class RouteInfoService
    {
      public static List<T_SYS_ROUTEINFO> GetList()
      {
          using (Entities entity = new Entities())
          {
              var query = from item in entity.T_SYS_ROUTEINFO select item;
              return query.ToList();
          }
      }

     
    }
}
