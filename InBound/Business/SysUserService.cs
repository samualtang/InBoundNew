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

      public static List<int?> getUserMenu(String userName)
      {
          using (Entities entity = new Entities())
          {
              var query = (from item in entity.T_SYS_USER
                           join item2 in entity.T_SYS_MENUROLERELATIVE
                           on item.ROLEID equals item2.SYSROLEID
                           where item.USERCODE == userName
                           select item2.MENUID).ToList();
              
              
                  return query;
              
          
 
          }
      }
    }
}
