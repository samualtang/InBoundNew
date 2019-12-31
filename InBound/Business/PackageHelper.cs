using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Business
{
    public static class PackageHelper
    {
        private static readonly object obj = new object();
        private static bool FirstInFalg = true;
        private static decimal id = 0;

        public static decimal GetPackageID(Entities entity)
        {
            lock (obj)
            {
                if (FirstInFalg)
                {
                    id = entity.T_PACKAGE_TASK.Count() > 0 ? entity.T_PACKAGE_TASK.Max(x => x.PTID) + 1 : 1;
                    FirstInFalg = false;
                }
                else
                {
                    id++;
                }
                return id;
            }
        }
    }
}
