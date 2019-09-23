using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound;

namespace InBound.Business
{
    // //将分配到两个包装机的任务，重置包装机包数，订单内包序等信息
    public static class UpPackageData
    {
        public static string UpPackageSeq()
        {
            using (Entities et = new Entities())
            {
                var _T_PACKAGE_CALLBACK = et.T_PACKAGE_CALLBACK.ToList();
                //获取两边都存在的订单
                var list = _T_PACKAGE_CALLBACK.Select(x => new { x.BILLCODE, x.EXPORT }).Distinct().GroupBy(x => x.BILLCODE).Select(x => new { billcode = x.Key, ct = x.Count() }).Where(x => x.ct > 1).Select(x => x.billcode).ToList(); ;
                //找到两个包装机，以号码小的为1起
                foreach (var item in list)
                {
                    var packagenolist = _T_PACKAGE_CALLBACK.Where(x => x.BILLCODE == item).Select(x => new { packno = x.EXPORT }).Distinct().ToList();
                    decimal minpackageno = packagenolist.Min(x => x.packno);
                    decimal maxpackageno = packagenolist.Max(x => x.packno);

                    //取号码小的包装机 最大的包序号
                    decimal minpackseq = _T_PACKAGE_CALLBACK.Where(x => x.EXPORT == minpackageno && x.BILLCODE == item).Max(x => x.PACKAGESEQ) ?? 0;
                    decimal maxpackseq = _T_PACKAGE_CALLBACK.Where(x => x.EXPORT == maxpackageno && x.BILLCODE == item).Max(x => x.PACKAGESEQ) ?? 0;
                    decimal allpackseq = minpackseq + maxpackseq;
                    //将号码大的包装机的包序号更新
                    var data1 = _T_PACKAGE_CALLBACK.Where(x => x.EXPORT == maxpackageno && x.BILLCODE == item).ToList();
                    foreach (var it in data1)
                    {
                        it.PACKAGESEQ += minpackseq;
                        it.ORDERPACKAGENUM = allpackseq;
                    }
                    //将号码小的包装机的包序号更新
                    var data2 = _T_PACKAGE_CALLBACK.Where(x => x.EXPORT == minpackageno && x.BILLCODE == item).ToList();
                    foreach (var it in data2)
                    {
                        it.ORDERPACKAGENUM = allpackseq;
                    }
                }
                et.SaveChanges();

                return "";
            }
        }
    }
}
