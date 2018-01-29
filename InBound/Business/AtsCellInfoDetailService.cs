using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Business
{
    public class AtsCellInfoDetailService : BaseService
    {

        public static void InsertAtsCellInfo(T_WMS_ATSCELLINFO_DETAIL info)
        {
            using (Entities data = new Entities())
            {
                decimal id = 0;
                // DataTable dt = Query("select S_wms_storagearea_inout.nextval from dual", null);
                id = data.ExecuteStoreQuery<decimal>("select S_t_wms_atscellinfodetail.nextval from dual").First();
                info.ID = id;
                data.AddToT_WMS_ATSCELLINFO_DETAIL(info);
                data.SaveChanges();

            }
        }
        public static void InsertAtsCellInfo(T_WMS_ATSCELLINFO_DETAIL info, Entities data)
        {
            
                decimal id = 0;
                // DataTable dt = Query("select S_wms_storagearea_inout.nextval from dual", null);
                id = data.ExecuteStoreQuery<decimal>("select S_t_wms_atscellinfodetail.nextval from dual").First();
                info.ID = id;
                data.AddToT_WMS_ATSCELLINFO_DETAIL(info);
                

            
        }
        public static List<OutBound> GetDetail(String text, String name)
        {
            using (Entities entity = new Entities())
            {
                var query = from item in entity.T_WMS_ATSCELLINFO_DETAIL
                            join item2 in entity.T_WMS_ATSCELLINFO
                            on item.CELLNO equals
                            item2.CELLNO
                            join item3 in entity.T_WMS_ATSCELL
                             on item2.CELLNO equals item3.CELLNO
                             where item.CIGARETTECODE.Contains(text) && item.CIGARETTENAME.Contains(name) && item2.STATUS == 30 && item3.WORKSTATUS==20
                            select new OutBound() { CIGARETTECODE = item.CIGARETTECODE, CIGARETTENAME = item.CIGARETTENAME, QTY = item.QTY ?? 0, CELLNO = item2.CELLNO , CREATETIME = item2.CREATETIME };
                return query.ToList();
            }
        }
        public static List<OutBound> GetGroupDetail(String text, String name)
        {
      
                using (Entities entity = new Entities())
                {
                    var query = from item in entity.T_WMS_ATSCELLINFO_DETAIL
                                join item2 in entity.T_WMS_ATSCELLINFO
                                on item.CELLNO equals
                                item2.CELLNO
                                where item2.STATUS == 30 && item.CIGARETTECODE.Contains(text) && item.CIGARETTENAME.Contains(name)
                                group item by new { item.CIGARETTECODE, item.CIGARETTENAME,item.BARCODE } into g
                                select new OutBound() { CIGARETTECODE = g.Key.CIGARETTECODE, CIGARETTENAME = g.Key.CIGARETTENAME, BarCode=g.Key.BARCODE, QTY = g.Sum(item => item.QTY) ?? 0 };
                    return query.ToList();
                }
                
           
        }
        public static T_WMS_ATSCELLINFO_DETAIL GetDetail(String cellno)
        {
            using (Entities entity = new Entities())
            {
                var query=(from item in entity.T_WMS_ATSCELLINFO_DETAIL where item.CELLNO.Equals(cellno) select item).FirstOrDefault();
                          return query;
            }
        }
        public static T_WMS_ATSCELLINFO_DETAIL GetDetail(String cellno, Entities entity)
        {
            
                var query = (from item in entity.T_WMS_ATSCELLINFO_DETAIL where item.CELLNO.Equals(cellno) select item).FirstOrDefault();
                return query;
            
        }
        public static List<OutBound> GetReport()
        {
            using (Entities entity = new Entities())
            {
                var query = from item in entity.T_WMS_ATSCELLINFO_DETAIL
                            join item2 in entity.T_WMS_ATSCELLINFO
                            on item.CELLNO equals
                            item2.CELLNO
                            where item2.STATUS == 30
                            group item by new { item.CIGARETTECODE, item.CIGARETTENAME } into g
                            select new OutBound() { CIGARETTECODE = g.Key.CIGARETTECODE, CIGARETTENAME = g.Key.CIGARETTENAME, QTY = g.Sum(item => item.QTY)??0 };
                return query.ToList();
            }
        }
        public static List<OutBound> GetReportDetail(String cigarettename, String code)
        {
            using (Entities entity = new Entities())
            {
                var query = from item in entity.T_WMS_ATSCELLINFO_DETAIL
                            join item2 in entity.T_WMS_ATSCELLINFO
                            on item.CELLNO equals
                            item2.CELLNO
                            where item2.STATUS == 30 && item.CIGARETTENAME.Contains(cigarettename) && item.CIGARETTECODE.Contains(code)
                           // group item by new { item.CIGARETTECODE, item.CIGARETTENAME } into g
                            select new OutBound() { CIGARETTECODE = item.CIGARETTECODE, CIGARETTENAME = item.CIGARETTENAME, QTY = item.QTY??0, CELLNO=item2.CELLNO };
                return query.ToList();
            }
        }
        public static List<OutBound> GetReport(String cigarettename,String code)
        {
            using (Entities entity = new Entities())
            {
                var query = from item in entity.T_WMS_ATSCELLINFO_DETAIL
                            join item2 in entity.T_WMS_ATSCELLINFO
                            on item.CELLNO equals
                            item2.CELLNO
                            where item2.STATUS == 30 && item.CIGARETTENAME.Contains(cigarettename) && item.CIGARETTECODE.Contains(code)
                            group item by new { item.CIGARETTECODE, item.CIGARETTENAME } into g
                            select new OutBound() { CIGARETTECODE = g.Key.CIGARETTECODE, CIGARETTENAME = g.Key.CIGARETTENAME, QTY = g.Sum(item => item.QTY) ?? 0 };
                return query.ToList();
            }
        }
        public static void delete(String cellno)
        {
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_WMS_ATSCELLINFO_DETAIL where item.CELLNO == cellno select item).FirstOrDefault();
                if(query!=null)
                {
                 entity.T_WMS_ATSCELLINFO_DETAIL.DeleteObject(query);
                }
                entity.SaveChanges();
            }
        }
        public static void delete(String cellno, Entities entity)
        {
           
                var query = (from item in entity.T_WMS_ATSCELLINFO_DETAIL where item.CELLNO == cellno select item).FirstOrDefault();
                if (query != null)
                {
                    entity.T_WMS_ATSCELLINFO_DETAIL.DeleteObject(query);
                }
               
           
        }
    }
}
