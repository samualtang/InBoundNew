using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Business
{
  public  class SysUserService:BaseService
    {
      public static bool Login(String username, string pas)
      {
          using (Entities entity = new Entities())
          {
              var query = (from item in entity.T_SYS_USER where item.USERCODE == username && item.USERPSW == pas select item).FirstOrDefault();
              if (query != null)
              {
                  return true;
              }
              else
              {
                  return false;
              }
          }
      }
    }
}
