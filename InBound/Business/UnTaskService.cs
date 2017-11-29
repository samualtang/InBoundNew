using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Model;

namespace InBound.Business
{
    public class UnTaskService:BaseService
    {
        public static List<TaskDetail> GetUnTask()
        {
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_UN_TASKLINE
                            group item by new { item.CIGARETTECODE, item.CIGARETTENAME } into g
                            select new TaskDetail() {  CIGARETTDECODE = g.Key.CIGARETTECODE,  CIGARETTDENAME = g.Key.CIGARETTENAME,  qty = g.Sum(item => item.QUANTITY) ?? 0 }).ToList();
                return query;
            }
           


        }
        public static void PreInOut()
        {
            List<TaskDetail> list = GetUnTask();
            if (list != null && list.Count > 0)
            {
                
                using (Entities entity = new Entities())
                {
                    foreach (var detail in list)
                    {
                        INF_JOBDOWNLOAD load = new INF_JOBDOWNLOAD();
                        load.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                        load.JOBID = load.ID;
                        load.JOBTYPE = 55;
                        load.BRANDID = ItemService.GetItemByCode(detail.CIGARETTDECODE).BIGBOX_BAR;
                        load.CREATEDATE = DateTime.Now;
                        load.PLANQTY = detail.qty;
                        load.PRIORITY = 50;
                        load.SOURCE = AtsCellOutService.getCellNo(detail.CIGARETTDECODE, (int)detail.qty);//out cell
                        load.TARGET = InfJobDownLoadService.GetTargetOutAddress(load.SOURCE, load.PLANQTY ?? 0);//立库出口
                        load.STATUS = 0;
                        entity.INF_JOBDOWNLOAD.AddObject(load);

                        INF_JOBDOWNLOAD load1= new INF_JOBDOWNLOAD();
                        load1.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                        load1.JOBID = load1.ID;
                        load1.JOBTYPE = 70;
                        load1.BRANDID = load.BRANDID;
                        load1.CREATEDATE = DateTime.Now;
                        load1.PLANQTY = detail.qty;
                        load1.PRIORITY = 50;
                        load1.SOURCE = load.TARGET;//out cell
                        load1.TARGET = "";//立库出口
                        load1.STATUS = 2;//拆垛完成下返库
                        entity.INF_JOBDOWNLOAD.AddObject(load1);

                        T_WMS_STORAGEAREA_INOUT outTask1 = new T_WMS_STORAGEAREA_INOUT();
                        outTask1.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                        outTask1.AREAID = 6;//异型烟分拣区
                        outTask1.TASKNO = load.JOBID;
                        outTask1.CELLNO = "";// querySource.TROUGHNUM;
                        outTask1.CIGARETTECODE = detail.CIGARETTDECODE;
                        outTask1.CIGARETTENAME = detail.CIGARETTDENAME;
                        outTask1.BARCODE = load.BRANDID + "";
                        outTask1.INOUTTYPE = 20;//入
                        outTask1.QTY = detail.qty * ItemService.GetItemByCode(detail.CIGARETTDECODE).JT_SIZE;
                        outTask1.STATUS = 10;
                        outTask1.CREATETIME = DateTime.Now;
                        entity.AddToT_WMS_STORAGEAREA_INOUT(outTask1);
                        
                    }
                    entity.SaveChanges();
                }
            }
        }
    }
}
