using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Business
{
   public class UserService
    {
       public static bool GetUserInfo(string account ,string pwd)
       {
           using (Entities date = new Entities())
           {

               var query = (from item in date.T_SYS_USER
                            where item.USERCODE == account && item.USERPSW == pwd
                            select item).FirstOrDefault();
               if (query != null  )
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
