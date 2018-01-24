using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using InBound.Model;
namespace InBound.Business
{
    public class InBoundService
    {
        public static decimal FullCount = 18;
        public static List<InBound.T_WMS_INBOUND> GetItem(DateTime start, DateTime end)
        {
            using (Entities entity = new Entities())
            {
                var query = from item in entity.T_WMS_INBOUND where item.CREATETIME >= start && item.CREATETIME <= end && item.STATUS == "10" && item.INTYPE == 10 select item;
                return query.ToList();
            }
        }
        public static void Update(decimal inboundid, string status)
        {
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_WMS_INBOUND where item.INBOUNDID == inboundid select item).FirstOrDefault();
                if (query != null)
                {
                    query.STATUS = status;
                    entity.SaveChanges();
                }
            }
        }
        public static void RollBack(string cellno)
        {


            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_WMS_ATSCELL where item.CELLNO == cellno select item).FirstOrDefault();
                query.WORKSTATUS = 20;
                var query1 = (from item in entity.T_WMS_ATSCELLINFO where item.CELLNO == cellno select item).FirstOrDefault();
                query1.STATUS = 30;
                var query3 = (from item in entity.T_WMS_ATSCELLINFO_DETAIL where item.CELLNO == cellno select item).FirstOrDefault();
                query3.REQUESTQTY = 0;
                entity.SaveChanges();
            }
        }


        public static void PreUpdateInOut(bool unFullFirst)
        {
            List<T_PRODUCE_SORTTROUGH> listNormal = SortTroughService.GetTrough(10, 20);//分拣通道
            List<T_PRODUCE_SORTTROUGH> listHj = SortTroughService.GetTrough(20, 20);//重力式货架
            try
            {
                //using (TransactionScope ts = new TransactionScope())
                //{
                using (Entities entity = new Entities())
                {
                    foreach (var task in listNormal)//遍历分拣通道
                    {
                        var query = (from item in entity.T_WMS_STORAGEAREA_INOUT where item.AREAID == 3 && item.CELLNO == task.TROUGHNUM select item).Sum(x => x.QTY).GetValueOrDefault();//获取通道调拨数量

                        decimal THRESHOLD = 0;
                        if (task.CLEARUP == 10)
                        {
                            THRESHOLD = task.CLEARTHRESHOLD ?? 0;
                            query = query - ProducePokeService.GetTroughFirstUnFinished(task.TROUGHNUM);
                        }
                        else
                        {
                            THRESHOLD = task.THRESHOLD??0;
                        }
                        if (task.MAINTISSALESS == 10)
                        {
                            decimal leftOrderQty = ProducePokeService.GetTroughUnFinished(task.TROUGHNUM);
                            if (query + task.MANTISSA > leftOrderQty)
                            {
                                continue;
                            }
                        }
                        if (query + task.MANTISSA < THRESHOLD)//烟柜需要补货
                        {
                            //decimal groupno = 1;
                            //if (task.GROUPNO <= 2)
                            //{
                            //    groupno = 1;
                            //}
                            //else if (task.GROUPNO <= 4)
                            //{
                            //    groupno = 2;
                            //}
                            //else if (task.GROUPNO <= 6)
                            //{
                            //    groupno = 3;
                            //}
                            //else if (task.GROUPNO <= 8)
                            //{
                            //    groupno = 4;
                            //}
                            var querySourcetemp = (from item in entity.T_PRODUCE_SORTTROUGH where item.TROUGHTYPE == 20 && item.CIGARETTECODE == task.CIGARETTECODE && item.STATE == "10" && item.GROUPNO == task.REPLENISHLINE select item).ToList();//找出对应重力式货架通道
                            var querySource = querySourcetemp[0];
                            List<OutBound> search = null;
                            decimal totalCount = 0;
                            decimal totalMantissa = 0;
                            decimal troughQty = 0;//通道数量
                            troughQty = querySourcetemp.Count;
                            foreach (var itemsource in querySourcetemp)
                            {
                                totalMantissa += itemsource.MANTISSA ?? 0;
                            }

                            int tempCount = 0;
                            while (query + tempCount + task.MANTISSA < THRESHOLD)//循环补烟柜
                            {

                                if (task.CLEARUP == 10)
                                {
                                    SortTroughService.updateTroughClearUp(10, 20, task.TROUGHNUM);
                                }
                                totalCount = 0;
                                if (querySourcetemp != null && querySourcetemp.Count > 0)//从库存大的先出
                                {

                                    var list1 = querySourcetemp.Select(x => x.TROUGHNUM).ToList();
                                    search = (from item in entity.T_WMS_STORAGEAREA_INOUT
                                              where list1.Contains(item.CELLNO) && item.AREAID == 2
                                              group item by new { item.CELLNO } into g
                                              select new OutBound() { CELLNO = g.Key.CELLNO, QTY = g.Sum(item => item.QTY) ?? 0 }).ToList();
                                    if (search != null && search.Count > 0)
                                    {
                                        decimal qtyTemp = 0;
                                        int maxIndex = 0;
                                        for (int j = 0; j < search.Count; j++)
                                        {
                                            totalCount += search[j].QTY;

                                            if (qtyTemp <= search[j].QTY)
                                            {
                                                qtyTemp = search[j].QTY;
                                                maxIndex = j;
                                            }

                                        }
                                        querySource = querySourcetemp.Find(x => x.TROUGHNUM == search[maxIndex].CELLNO);

                                    }
                                }

                                //生成开箱计划
                                INF_JOBDOWNLOAD load = new INF_JOBDOWNLOAD();
                                load.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                                load.JOBID = load.ID;
                                load.JOBTYPE = 80;
                                load.BRANDID = ItemService.GetItemByCode(task.CIGARETTECODE).BIGBOX_BAR;
                                load.CREATEDATE = DateTime.Now;
                                load.PLANQTY = 1;
                                load.PRIORITY = 50;
                                load.SOURCE = querySource.TROUGHNUM;
                                load.TARGET = task.TROUGHNUM + "";
                                load.STATUS = 0;


                                T_WMS_STORAGEAREA_INOUT outTask1 = new T_WMS_STORAGEAREA_INOUT();

                                outTask1.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                                outTask1.AREAID = 2;//重力式货架
                                outTask1.TASKNO = load.JOBID;
                                outTask1.CELLNO = querySource.TROUGHNUM;
                                outTask1.CIGARETTECODE = task.CIGARETTECODE;
                                outTask1.CIGARETTENAME = task.CIGARETTENAME.Trim();
                                outTask1.BARCODE = load.BRANDID + "";
                                outTask1.INOUTTYPE = 10;//出
                                outTask1.QTY = -1;
                                outTask1.GROUPNO = querySource.GROUPNO;
                                outTask1.STATUS = 10;
                                outTask1.CREATETIME = DateTime.Now;


                                T_WMS_STORAGEAREA_INOUT outTask2 = new T_WMS_STORAGEAREA_INOUT();
                                outTask2.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                                outTask2.AREAID = 3;//烟柜
                                outTask2.TASKNO = load.JOBID;
                                outTask2.CELLNO = task.TROUGHNUM;
                                outTask2.CIGARETTECODE = task.CIGARETTECODE;
                                outTask2.BARCODE = load.BRANDID + "";
                                outTask2.CIGARETTENAME = task.CIGARETTENAME.Trim();
                                outTask2.INOUTTYPE = 20;//入
                                outTask2.QTY = 50;
                                outTask2.GROUPNO = task.GROUPNO;
                                outTask2.CREATETIME = DateTime.Now;
                                outTask2.STATUS = 10;


                                decimal leftCount = troughQty * FullCount - (totalCount + totalMantissa);//重力式货架尾数  是否要减一
                                //List<T_WMS_ATSCELLINFO_DETAIL> list = AtsCellInfoService.GetAllUnFullPallet();
                                T_WMS_ATSCELLINFO_DETAIL detail = AtsCellInfoService.GetDetail(task.CIGARETTECODE, leftCount).FirstOrDefault();// list.Find(x => x.QTY == leftCount && x.CIGARETTECODE == task.CIGARETTECODE);
                                if (detail != null && unFullFirst)
                                {
                                    INF_JOBDOWNLOAD load1 = new INF_JOBDOWNLOAD();
                                    load1.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                                    load1.JOBID = load1.ID;
                                    load1.BRANDID = task.CIGARETTECODE;
                                    load1.CREATEDATE = DateTime.Now;
                                    load1.PLANQTY = detail.QTY;
                                    load1.PRIORITY = 50;

                                    load1.JOBTYPE = 55;//补货出库
                                    load1.SOURCE = AtsCellOutService.getCellNoBig(task.CIGARETTECODE, (int)detail.QTY);//out cell
                                    load1.TARGET = InfJobDownLoadService.GetTargetOutAddress(load1.SOURCE, detail.QTY ?? 0);//出口
                                    load1.STATUS = 0;
                                    if (load1.SOURCE != "" && load1.TARGET != "")
                                    {
                                        load1.BARCODE = AtsCellInfoDetailService.GetDetail(load1.SOURCE).BARCODE;
                                        entity.INF_JOBDOWNLOAD.AddObject(load1);
                                    }
                                    else
                                    {
                                        if (load1.SOURCE != "")
                                        {
                                            RollBack(load1.SOURCE);
                                        }
                                        break;
                                    }
                                    totalCount += detail.QTY ?? 0;
                                    //下达重力式货架补货计划
                                    decimal tempPlanQty = detail.QTY ?? 0;
                                    foreach (var item in querySourcetemp)
                                    {
                                        if (tempPlanQty <= 0)
                                        {
                                            break;
                                        }
                                        INF_JOBDOWNLOAD load2 = new INF_JOBDOWNLOAD();
                                        load2.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                                        load2.JOBID = load2.ID;
                                        load2.BRANDID = load.BRANDID;
                                        load2.CREATEDATE = DateTime.Now;
                                        load2.STATUS = 2;//出库完成置0
                                        load2.EXTATTR2 = load1.JOBID + "";//用来关联出库任务
                                        decimal planQty = item.MANTISSA ?? 0;
                                        if (AtsCellInfoService.GetCellInfo(load1.SOURCE).DISMANTLE == 10)
                                        {
                                            load2.JOBTYPE = 60;
                                        }
                                        else
                                        {
                                            load2.JOBTYPE = 70;//人工码垛
                                        }
                                        if (search.Find(x => x.CELLNO + "" == item.TROUGHNUM) != null)
                                        {
                                            planQty += search.Find(x => x.CELLNO + "" == item.TROUGHNUM).QTY;
                                        }
                                        if (tempPlanQty >= FullCount - planQty)
                                        {
                                            load2.PLANQTY = FullCount - planQty;
                                        }
                                        else
                                        {
                                            load2.PLANQTY = tempPlanQty;

                                        }

                                        load2.PRIORITY = 50;
                                        load2.SOURCE = load1.TARGET;//out cell立库出口
                                        load2.TARGET = querySource.TROUGHNUM;
                                        load2.STATUS = 0;
                                        entity.INF_JOBDOWNLOAD.AddObject(load2);

                                        T_WMS_STORAGEAREA_INOUT outTask4 = new T_WMS_STORAGEAREA_INOUT();
                                        outTask4.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                                        outTask4.AREAID = 2;//重力式货架
                                        outTask4.CELLNO = item.TROUGHNUM;
                                        outTask4.TASKNO = load2.JOBID;
                                        outTask4.CIGARETTECODE = item.CIGARETTECODE;
                                        outTask4.CIGARETTENAME = item.CIGARETTENAME.Trim();
                                        outTask4.BARCODE = load.BRANDID + "";
                                        outTask4.INOUTTYPE = 20;//入库
                                        outTask4.QTY = load2.PLANQTY;
                                        outTask4.STATUS = 10;
                                        outTask4.GROUPNO = item.GROUPNO;
                                        outTask4.CREATETIME = DateTime.Now;
                                        entity.AddToT_WMS_STORAGEAREA_INOUT(outTask4);
                                        tempPlanQty = tempPlanQty - load2.PLANQTY ?? 0;
                                        entity.SaveChanges();
                                    }
                                }
                                else
                                {
                                    if (totalCount + totalMantissa <= (querySource.THRESHOLD) * troughQty)//小于阀值数 乘以通道数量
                                    {
                                        //var query2 = (from item in entity.T_WMS_STORAGEAREA_INOUT where item.AREAID == 2 && item.CELLNO == outTask1.CELLNO select item).Sum(x => x.QTY);
                                        //if (query2 != null)
                                        //{


                                        //    if (query2 + querySource.MANTISSA <= querySource.THRESHOLD)
                                        //    {

                                        //下达立库补货计划
                                        INF_JOBDOWNLOAD load1 = new INF_JOBDOWNLOAD();
                                        load1.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                                        load1.JOBID = load1.ID;
                                        load1.JOBTYPE = 55;
                                        load1.BRANDID = load.BRANDID;
                                        load1.CREATEDATE = DateTime.Now;
                                        if (querySource.BOXCOUNT > 15)
                                        {
                                            if (troughQty < 2)
                                            {
                                                load1.PLANQTY = 15;
                                            }
                                            else
                                            {
                                                load1.PLANQTY = (int)querySource.BOXCOUNT;
                                            }
                                        }
                                        else
                                        {
                                            load1.PLANQTY = (int)querySource.BOXCOUNT;
                                        }
                                        load1.PRIORITY = 50;
                                        load1.SOURCE = AtsCellOutService.getCellNoBig(task.CIGARETTECODE, (int)querySource.BOXCOUNT);//out cell
                                        load1.TARGET = InfJobDownLoadService.GetTargetOutAddress(load1.SOURCE, load1.PLANQTY ?? 0);//立库出口
                                        load1.STATUS = 0;
                                        if (load1.SOURCE != "" && load1.TARGET != "")
                                        {
                                            load1.BARCODE = AtsCellInfoDetailService.GetDetail(load1.SOURCE).BARCODE;
                                            entity.INF_JOBDOWNLOAD.AddObject(load1);
                                        }
                                        else
                                        {
                                            if (load1.SOURCE != "")
                                            {
                                                RollBack(load1.SOURCE);
                                            }
                                            break;
                                        }

                                        totalCount += load1.PLANQTY ?? 0;
                                        decimal tempPlanQty = load1.PLANQTY ?? 0;


                                        //下达重力式货架补货计划
                                        foreach (var item in querySourcetemp)
                                        {
                                            if (tempPlanQty <= 0)
                                            {
                                                break;
                                            }
                                            INF_JOBDOWNLOAD load2 = new INF_JOBDOWNLOAD();
                                            load2.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                                            load2.JOBID = load2.ID;
                                            load2.BRANDID = ItemService.GetItemByCode(task.CIGARETTECODE).BIGBOX_BAR;
                                            load2.CREATEDATE = DateTime.Now;
                                            load2.STATUS = 2;//出库完成置0
                                            load2.EXTATTR2 = load1.JOBID + "";//用来关联出库任务
                                            decimal planQty = item.MANTISSA ?? 0;
                                            if (AtsCellInfoService.GetCellInfo(load1.SOURCE).DISMANTLE == 10)
                                            {
                                                load2.JOBTYPE = 60;
                                            }
                                            else
                                            {
                                                load2.JOBTYPE = 70;//人工码垛
                                            }
                                            if (search.Find(x => x.CELLNO + "" == item.TROUGHNUM) != null)
                                            {
                                                planQty += search.Find(x => x.CELLNO + "" == item.TROUGHNUM).QTY;
                                            }
                                            if (tempPlanQty >= FullCount - planQty)
                                            {
                                                load2.PLANQTY = FullCount - planQty;
                                            }
                                            else
                                            {
                                                load2.PLANQTY = tempPlanQty;

                                            }

                                            load2.PRIORITY = 50;
                                            load2.SOURCE = load1.TARGET;//out cell立库出口
                                            load2.TARGET = querySource.TROUGHNUM;
                                            //load2.STATUS = 0;
                                            entity.INF_JOBDOWNLOAD.AddObject(load2);

                                            T_WMS_STORAGEAREA_INOUT outTask4 = new T_WMS_STORAGEAREA_INOUT();
                                            outTask4.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                                            outTask4.AREAID = 2;//重力式货架
                                            outTask4.CELLNO = item.TROUGHNUM;
                                            outTask4.TASKNO = load2.JOBID;
                                            outTask4.CIGARETTECODE = item.CIGARETTECODE;
                                            outTask4.CIGARETTENAME = item.CIGARETTENAME.Trim();
                                            outTask4.BARCODE = load2.BRANDID + "";
                                            outTask4.INOUTTYPE = 20;//入库
                                            outTask4.QTY = load2.PLANQTY;
                                            outTask4.STATUS = 10;
                                            outTask4.GROUPNO = item.GROUPNO;
                                            outTask4.CREATETIME = DateTime.Now;
                                            entity.AddToT_WMS_STORAGEAREA_INOUT(outTask4);
                                            tempPlanQty = tempPlanQty - load2.PLANQTY ?? 0;
                                            entity.SaveChanges();
                                        }
                                        entity.SaveChanges();
                                    }
                                }
                                if (totalCount + totalMantissa > 0)//重力式货架有货 才下开箱任务
                                {
                                    entity.INF_JOBDOWNLOAD.AddObject(load);
                                    entity.AddToT_WMS_STORAGEAREA_INOUT(outTask1);
                                    totalCount -= 1;
                                    entity.AddToT_WMS_STORAGEAREA_INOUT(outTask2);
                                    tempCount += 50;
                                }
                                entity.SaveChanges();

                            }

                        }

                    }

                    //foreach (var item2 in listHj)
                    //{
                    //    var querySourcetemp = (from item in entity.T_PRODUCE_SORTTROUGH where item.TROUGHTYPE == 20 && item.CIGARETTECODE == item2.CIGARETTECODE && item.STATE == "10" && item.GROUPNO == item2.GROUPNO select item).ToList();
                    //    var querySource = querySourcetemp[0];
                    //    List<OutBound> search = null;
                    //    decimal totalCount = 0;
                    //    decimal totalMantissa = 0;
                    //    decimal troughQty = 0;//通道数量
                    //    troughQty = querySourcetemp.Count;
                    //    foreach (var itemsource in querySourcetemp)
                    //    {
                    //        totalMantissa += itemsource.MANTISSA ?? 0;
                    //    }
                    //    if (querySourcetemp != null )
                    //    {


                    //        var list = querySourcetemp.Select(x => x.TROUGHNUM).ToList();
                    //        search = (from item in entity.T_WMS_STORAGEAREA_INOUT
                    //                  where list.Contains(item.CELLNO) && item.AREAID == 2
                    //                  group item by new { item.CELLNO } into g
                    //                  select new OutBound() { CELLNO = g.Key.CELLNO, QTY = g.Sum(item => item.QTY) ?? 0 }).ToList();
                    //        if (search != null && search.Count > 0)
                    //        {
                    //            decimal qtyTemp = 0;
                    //            int maxIndex = 0;
                    //            for (int j = 0; j < search.Count; j++)
                    //            {
                    //                totalCount += search[j].QTY;

                    //                if (qtyTemp <= search[j].QTY)
                    //                {
                    //                    qtyTemp = search[j].QTY;
                    //                    maxIndex = j;
                    //                }

                    //            }
                    //            //querySource = querySourcetemp[maxIndex];

                    //        }
                    //    }
                    //        decimal leftCount = troughQty * FullCount - (totalCount - 1 + totalMantissa);
                    //        List<T_WMS_ATSCELLINFO_DETAIL> list1 = AtsCellInfoService.GetAllUnFullPallet();
                    //        T_WMS_ATSCELLINFO_DETAIL detail = list1.Find(x => x.QTY == leftCount && x.CIGARETTECODE == item2.CIGARETTECODE);
                    //        if (detail != null && unFullFirst)
                    //        {
                    //            INF_JOBDOWNLOAD load1 = new INF_JOBDOWNLOAD();
                    //            load1.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                    //            load1.JOBID = load1.ID;
                    //            load1.BRANDID =item2.CIGARETTECODE;
                    //            load1.CREATEDATE = DateTime.Now;
                    //            load1.PLANQTY = detail.QTY;// (int)querySource.BOXCOUNT;
                    //            load1.PRIORITY = 50;
                    //            load1.JOBTYPE = 55;
                    //            load1.SOURCE = AtsCellOutService.getCellNo(item2.CIGARETTECODE, (int)detail.QTY);//out cell
                    //            load1.TARGET = InfJobDownLoadService.GetTargetOutAddress(load1.SOURCE, load1.PLANQTY??0);
                    //            load1.STATUS = 0;
                    //            entity.INF_JOBDOWNLOAD.AddObject(load1);

                    //            totalCount += detail.QTY ?? 0;
                    //            decimal tempPlanQty = load1.PLANQTY ?? 0;


                    //            //下达重力式货架补货计划
                    //            foreach (var item in querySourcetemp)
                    //            {
                    //                if (tempPlanQty <= 0)
                    //                {
                    //                    break;
                    //                }
                    //                INF_JOBDOWNLOAD load2 = new INF_JOBDOWNLOAD();
                    //                load2.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                    //                load2.JOBID = load2.ID;
                    //                load2.BRANDID = load1.BRANDID;
                    //                load2.CREATEDATE = DateTime.Now;
                    //                decimal planQty = item.MANTISSA ?? 0;
                    //                load2.STATUS = 2;//出库完成置0
                    //                if (search.Find(x => x.CELLNO + "" == item.TROUGHNUM) != null)
                    //                {
                    //                    planQty += search.Find(x => x.CELLNO + "" == item.TROUGHNUM).QTY;
                    //                }
                    //                if (tempPlanQty >= FullCount - planQty)
                    //                {
                    //                    load2.PLANQTY = FullCount - planQty;
                    //                }
                    //                else
                    //                {
                    //                    load2.PLANQTY = tempPlanQty;

                    //                }

                    //                load2.PRIORITY = 50;
                    //                load2.SOURCE = load1.TARGET;//out cell立库出口
                    //                load2.TARGET = querySource.TROUGHNUM;
                    //                load2.STATUS = 0;
                    //                if (AtsCellInfoService.GetCellInfo(load1.SOURCE).DISMANTLE == 10)
                    //                {
                    //                    load2.JOBTYPE = 60;
                    //                }
                    //                else
                    //                {
                    //                    load2.JOBTYPE = 70;//人工码垛
                    //                }
                    //                entity.INF_JOBDOWNLOAD.AddObject(load2);

                    //                T_WMS_STORAGEAREA_INOUT outTask4 = new T_WMS_STORAGEAREA_INOUT();
                    //                outTask4.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                    //                outTask4.AREAID = 2;//重力式货架
                    //                outTask4.TASKNO = load2.JOBID;
                    //                outTask4.CELLNO = item.TROUGHNUM;
                    //                outTask4.CIGARETTECODE = item.CIGARETTECODE;
                    //                outTask4.CIGARETTENAME = item.CIGARETTENAME;
                    //                outTask4.BARCODE=load2.BRANDID+"";
                    //                outTask4.INOUTTYPE = 20;//入库
                    //                outTask4.QTY = load2.PLANQTY;
                    //                outTask4.STATUS = 10;
                    //                outTask4.CREATETIME = DateTime.Now;
                    //                entity.AddToT_WMS_STORAGEAREA_INOUT(outTask4);
                    //                tempPlanQty = tempPlanQty - load2.PLANQTY ?? 0;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (totalCount  + totalMantissa <= querySource.THRESHOLD)
                    //            {
                    //                //var query2 = (from item in entity.T_WMS_STORAGEAREA_INOUT where item.AREAID == 2 && item.CELLNO == outTask1.CELLNO select item).Sum(x => x.QTY);
                    //                //if (query2 != null)
                    //                //{


                    //                //    if (query2 + querySource.MANTISSA <= querySource.THRESHOLD)
                    //                //    {

                    //                //下达立库补货计划
                    //                INF_JOBDOWNLOAD load1 = new INF_JOBDOWNLOAD();
                    //                load1.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                    //                load1.JOBID = load1.ID;
                    //                load1.BRANDID = item2.CIGARETTECODE;
                    //                load1.JOBTYPE=55;
                    //                load1.CREATEDATE = DateTime.Now;
                    //                load1.PLANQTY = (int)querySource.BOXCOUNT;
                    //                load1.PRIORITY = 50;
                    //                load1.SOURCE = AtsCellOutService.getCellNo(item2.CIGARETTECODE, (int)querySource.BOXCOUNT);//out cell
                    //                load1.TARGET = InfJobDownLoadService.GetTargetOutAddress(load1.SOURCE, load1.PLANQTY ?? 0);
                    //                load1.STATUS = 0;
                    //                entity.INF_JOBDOWNLOAD.AddObject(load1);
                    //                totalCount += load1.PLANQTY ?? 0;

                    //                decimal tempPlanQty = load1.PLANQTY ?? 0;


                    //                //下达重力式货架补货计划
                    //                foreach (var item in querySourcetemp)
                    //                {
                    //                    if (tempPlanQty <= 0)
                    //                    {
                    //                        break;
                    //                    }
                    //                    INF_JOBDOWNLOAD load2 = new INF_JOBDOWNLOAD();
                    //                    load2.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                    //                    load2.JOBID = load2.ID;
                    //                    load2.BRANDID = ItemService.GetItemByCode(item2.CIGARETTECODE).BIGBOX_BAR;
                    //                    load2.CREATEDATE = DateTime.Now;
                    //                    decimal planQty = item.MANTISSA ?? 0;
                    //                    load2.STATUS = 2;//出库完成置0
                    //                    if (search.Find(x => x.CELLNO + "" == item.TROUGHNUM) != null)
                    //                    {
                    //                        planQty += search.Find(x => x.CELLNO + "" == item.TROUGHNUM).QTY;
                    //                    }
                    //                    if (tempPlanQty >= FullCount - planQty)
                    //                    {
                    //                        load2.PLANQTY = FullCount - planQty;
                    //                    }
                    //                    else
                    //                    {
                    //                        load2.PLANQTY = tempPlanQty;

                    //                    }

                    //                    load2.PRIORITY = 50;
                    //                    load2.SOURCE = load1.TARGET; //out cell立库出口
                    //                    load2.TARGET = querySource.TROUGHNUM;
                    //                    load2.STATUS = 0;
                    //                    if(AtsCellInfoService.GetCellInfo(load1.SOURCE).DISMANTLE==10)
                    //                    {
                    //                        load2.JOBTYPE = 60;
                    //                    }
                    //                    else
                    //                    {
                    //                        load2.JOBTYPE = 70;//人工码垛
                    //                    }
                    //                    entity.INF_JOBDOWNLOAD.AddObject(load2);

                    //                    T_WMS_STORAGEAREA_INOUT outTask4 = new T_WMS_STORAGEAREA_INOUT();
                    //                    outTask4.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                    //                    outTask4.AREAID = 2;//重力式货架
                    //                    outTask4.CELLNO = item.TROUGHNUM;
                    //                    outTask4.CIGARETTECODE = item.CIGARETTECODE;
                    //                    outTask4.CIGARETTENAME = item.CIGARETTENAME;
                    //                    outTask4.BARCODE=load2.BRANDID+"";
                    //                    outTask4.TASKNO = load2.JOBID;
                    //                    outTask4.INOUTTYPE = 20;//入库
                    //                    outTask4.QTY = load2.PLANQTY;
                    //                    outTask4.STATUS = 10;
                    //                    outTask4.CREATETIME = DateTime.Now;
                    //                    entity.AddToT_WMS_STORAGEAREA_INOUT(outTask4);
                    //                    tempPlanQty = tempPlanQty - load2.PLANQTY ?? 0;
                    //                }

                    //            }
                    //        }

                    //} //end foreach

                    //     entity.SaveChanges();
                }
                //    ts.Complete();
                //}
            }
            catch (Exception ex)
            {
                String error = ex.Message;
            }
        }//预补结束
        public static void test(decimal groupno)
        {
            using (Entities entity = new Entities())
            {
                var query = (from item in entity.T_PRODUCE_POKE where item.SORTSTATE == 10 && item.GROUPNO == groupno orderby item.SORTNUM select item).FirstOrDefault();
                if (query != null)
                {
                    UpdateInOut(query.SORTNUM ?? 0, groupno);
                    TaskService.UpdateStatus(groupno, 30, query.SORTNUM ?? 0);
                }
            }
        }
        public static void UpdateInOut(decimal sortNo, decimal groupno)
        {


            using (TransactionScope ts = new TransactionScope())
            {
                using (Entities entity = new Entities())
                {
                    var taskList = (from item in entity.T_PRODUCE_POKE where item.SORTNUM == sortNo && item.GROUPNO == groupno select item).ToList();
                    foreach (var task in taskList)
                    {
                        var query = (from item in entity.T_WMS_STORAGEAREA_INOUT where item.TASKNO == task.BILLCODE && item.GROUPNO == groupno select item).FirstOrDefault();
                        if (query != null)
                            break;
                        T_WMS_STORAGEAREA_INOUT outTask = new T_WMS_STORAGEAREA_INOUT();
                        decimal id = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                        outTask.ID = id;
                        outTask.AREAID = 3;//
                        outTask.CELLNO = task.TROUGHNUM;
                        T_PRODUCE_SORTTROUGH s = SortTroughService.GetFJTroughInfo(10, task.TROUGHNUM, 20);
                        outTask.CIGARETTECODE = s.CIGARETTECODE;
                        //实际情况不会有卷烟代码位空的情况   这样写防止程序报错
                        outTask.BARCODE = string.IsNullOrEmpty(outTask.CIGARETTECODE) ? "" : ItemService.GetItemByCode(outTask.CIGARETTECODE).BIGBOX_BAR;
                        outTask.INOUTTYPE = 10;//出
                        outTask.QTY = -task.POKENUM;
                        outTask.STATUS = 20;
                        outTask.CIGARETTENAME = s.CIGARETTENAME;
                        outTask.CREATETIME = DateTime.Now;
                        outTask.TASKNO = task.BILLCODE;
                        outTask.GROUPNO = groupno;
                        entity.AddToT_WMS_STORAGEAREA_INOUT(outTask);
                    }
                    entity.SaveChanges();
                    ts.Complete();
                }
            }


        }
        public static void UpdateInOut(int taskno, int begin, int end, int troughtype, int cigaretteType, bool unFullFirst)
        {
            //    List<TaskDetail> taskList = TaskService.GetCigarette(begin, end, troughtype, cigaretteType, taskno);
            //    if (taskList == null || taskList.Count == 0)
            //    {
            //        return;
            //    }
            //    try
            //    {
            //        using (TransactionScope ts = new TransactionScope())
            //        {
            //            using (Entities entity = new Entities())
            //            {
            //                foreach (var task in taskList)
            //                {
            //                    T_WMS_STORAGEAREA_INOUT outTask = new T_WMS_STORAGEAREA_INOUT();
            //                    decimal id = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
            //                    outTask.ID = id;
            //                    outTask.AREAID = 3;//烟柜
            //                    outTask.CELLNO = task.SortTroughNum;
            //                    outTask.CIGARETTECODE = task.CIGARETTDECODE;
            //                    outTask.BARCODE = ItemService.GetItemByCode(task.CIGARETTDECODE).BIGBOX_BAR;
            //                    outTask.INOUTTYPE = 10;//出
            //                    outTask.QTY = -task.qty;
            //                    outTask.STATUS = 20;
            //                    outTask.CIGARETTENAME = task.CIGARETTDENAME;
            //                    outTask.CREATETIME = DateTime.Now;

            //                    entity.AddToT_WMS_STORAGEAREA_INOUT(outTask);
            //                    //此通道是否需要补烟
            //                    var query = (from item in entity.T_WMS_STORAGEAREA_INOUT where item.AREAID == 3 && item.CELLNO == task.SortTroughNum select item).Sum(x => x.QTY).GetValueOrDefault();

            //                    if (query + task.MANTISSA < task.THRESHOLD)
            //                    {
            //                        decimal groupno = 1;
            //                        if (task.GroupNO <= 2)
            //                        {
            //                            groupno = 1;
            //                        }
            //                        else if (task.GroupNO <= 4)
            //                        {
            //                            groupno = 2;
            //                        }
            //                        else if (task.GroupNO <= 6)
            //                        {
            //                            groupno = 3;
            //                        }
            //                        else if (task.GroupNO <= 8)
            //                        {
            //                            groupno = 4;
            //                        }
            //                        //此处要解决有品牌占用多个通道的问题
            //                        var querySourcetemp = (from item in entity.T_PRODUCE_SORTTROUGH where item.TROUGHTYPE == 20 && item.CIGARETTECODE == task.CIGARETTDECODE && item.STATE == "10" && item.GROUPNO == groupno select item).ToList();
            //                        var querySource = querySourcetemp[0];
            //                        List<OutBound> search = null;
            //                        decimal totalCount = 0;
            //                        decimal totalMantissa = 0;
            //                        decimal troughQty = 0;//通道数量
            //                        troughQty = querySourcetemp.Count;
            //                        foreach (var itemsource in querySourcetemp)
            //                        {
            //                            totalMantissa += itemsource.MANTISSA ?? 0;
            //                        }


            //                        int tempCount = 0;
            //                        while (query + tempCount + task.MANTISSA < task.THRESHOLD)//循环补烟柜
            //                        {
            //                            totalCount = 0;

            //                            if (querySourcetemp != null && querySourcetemp.Count > 0)//从库存大的先出
            //                            {
            //                                var list1 = querySourcetemp.Select(x => x.TROUGHNUM).ToList();
            //                                search = (from item in entity.T_WMS_STORAGEAREA_INOUT
            //                                          where list1.Contains(item.CELLNO) && item.AREAID == 2
            //                                          group item by new { item.CELLNO } into g
            //                                          select new OutBound() { CELLNO = g.Key.CELLNO, QTY = g.Sum(item => item.QTY) ?? 0 }).ToList();
            //                                if (search != null && search.Count > 0)
            //                                {
            //                                    decimal qtyTemp = 0;
            //                                    int maxIndex = 0;
            //                                    for (int j = 0; j < search.Count; j++)
            //                                    {
            //                                        totalCount += search[j].QTY;
            //                                        if (qtyTemp <= search[j].QTY)
            //                                        {
            //                                            qtyTemp = search[j].QTY;
            //                                            maxIndex = j;
            //                                        }
            //                                    }
            //                                    querySource = querySourcetemp.Find(x => x.MACHINESEQ == decimal.Parse(search[maxIndex]._CELLNO));

            //                                }
            //                            }
            //                            //生成开箱计划
            //                            INF_JOBDOWNLOAD load = new INF_JOBDOWNLOAD();
            //                            load.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
            //                            load.JOBID = load.ID;
            //                            load.JOBTYPE = 80;
            //                            load.BRANDID = ItemService.GetItemByCode(task.CIGARETTDECODE).BIGBOX_BAR;
            //                            load.CREATEDATE = DateTime.Now;
            //                            load.PLANQTY = 1;
            //                            load.PRIORITY = 50;
            //                            load.SOURCE = querySource.TROUGHNUM;
            //                            load.TARGET = task.SortTroughNum + "";
            //                            load.STATUS = 0;
            //                            entity.INF_JOBDOWNLOAD.AddObject(load);
            //                            totalCount -= 1;
            //                            T_WMS_STORAGEAREA_INOUT outTask1 = new T_WMS_STORAGEAREA_INOUT();
            //                            outTask1.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
            //                            outTask1.AREAID = 2;//重力式货架
            //                            outTask1.TASKNO = load.JOBID;
            //                            outTask1.CELLNO = querySource.TROUGHNUM;
            //                            outTask1.CIGARETTECODE = task.CIGARETTDECODE;
            //                            outTask1.CIGARETTENAME = task.CIGARETTDENAME;
            //                            outTask1.BARCODE = load.BRANDID + "";
            //                            outTask1.INOUTTYPE = 10;//出
            //                            outTask1.QTY = -1;
            //                            outTask1.STATUS = 10;
            //                            entity.AddToT_WMS_STORAGEAREA_INOUT(outTask1);



            //                            T_WMS_STORAGEAREA_INOUT outTask2 = new T_WMS_STORAGEAREA_INOUT();

            //                            outTask2.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
            //                            outTask2.AREAID = 3;//烟柜
            //                            outTask2.CELLNO = task.SortTroughNum;
            //                            outTask2.TASKNO = load.JOBID;
            //                            outTask2.CIGARETTECODE = task.CIGARETTDECODE;
            //                            outTask2.CIGARETTENAME = task.CIGARETTDENAME;
            //                            outTask2.BARCODE = load.BRANDID + "";
            //                            outTask2.INOUTTYPE = 20;//入
            //                            outTask2.QTY = 50;
            //                            outTask2.CREATETIME = DateTime.Now;
            //                            outTask2.STATUS = 10;
            //                            entity.AddToT_WMS_STORAGEAREA_INOUT(outTask2);

            //                            decimal leftCount = troughQty * FullCount - (totalCount - 1 + totalMantissa);
            //                            List<T_WMS_ATSCELLINFO_DETAIL> list = AtsCellInfoService.GetAllUnFullPallet();
            //                            T_WMS_ATSCELLINFO_DETAIL detail = list.Find(x => x.QTY == leftCount && x.CIGARETTECODE == task.CIGARETTDECODE);
            //                            if (detail != null && unFullFirst)
            //                            {
            //                                INF_JOBDOWNLOAD load1 = new INF_JOBDOWNLOAD();
            //                                load1.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
            //                                load1.JOBID = load1.ID;
            //                                load1.JOBTYPE = 55;
            //                                load1.BRANDID = load.BRANDID;
            //                                load1.CREATEDATE = DateTime.Now;
            //                                load1.PLANQTY = detail.QTY; // (int)querySource.BOXCOUNT;
            //                                load1.PRIORITY = 50;
            //                                load1.SOURCE = AtsCellOutService.getCellNo(task.CIGARETTDECODE, (int)detail.QTY);//out cell
            //                                load1.TARGET = InfJobDownLoadService.GetTargetOutAddress(load1.SOURCE, load1.PLANQTY ?? 0);
            //                                load1.STATUS = 0;
            //                                entity.INF_JOBDOWNLOAD.AddObject(load1);
            //                                totalCount += load1.PLANQTY ?? 0;
            //                                decimal tempPlanQty = detail.QTY ?? 0;
            //                                //下达重力式货架补货计划
            //                                foreach (var item in querySourcetemp)
            //                                {
            //                                    if (tempPlanQty <= 0)
            //                                    {
            //                                        break;
            //                                    }
            //                                    INF_JOBDOWNLOAD load2 = new INF_JOBDOWNLOAD();
            //                                    load2.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
            //                                    load2.JOBID = load2.ID;
            //                                    load2.BRANDID = load.BRANDID;
            //                                    load2.CREATEDATE = DateTime.Now;
            //                                    decimal planQty = item.MANTISSA ?? 0;
            //                                    load2.STATUS = 2;//出库完成置0
            //                                    if (search.Find(x => x.CELLNO + "" == item.TROUGHNUM) != null)
            //                                    {
            //                                        planQty += search.Find(x => x.CELLNO + "" == item.TROUGHNUM).QTY;
            //                                    }
            //                                    if (tempPlanQty >= FullCount - planQty)
            //                                    {
            //                                        load2.PLANQTY = FullCount - planQty;
            //                                    }
            //                                    else
            //                                    {
            //                                        load2.PLANQTY = tempPlanQty;

            //                                    }

            //                                    load2.PRIORITY = 50;
            //                                    load2.SOURCE = load1.TARGET;//out cell立库出口
            //                                    load2.TARGET = querySource.TROUGHNUM;
            //                                    load2.STATUS = 0;
            //                                    if (AtsCellInfoService.GetCellInfo(load1.SOURCE).DISMANTLE == 10)
            //                                    {
            //                                        load2.JOBTYPE = 60;
            //                                    }
            //                                    else
            //                                    {
            //                                        load2.JOBTYPE = 70;//人工码垛
            //                                    }
            //                                    entity.INF_JOBDOWNLOAD.AddObject(load2);

            //                                    T_WMS_STORAGEAREA_INOUT outTask4 = new T_WMS_STORAGEAREA_INOUT();
            //                                    outTask4.AREAID = 2;//重力式货架
            //                                    outTask4.TASKNO = load2.JOBID;
            //                                    outTask4.CELLNO = item.TROUGHNUM;
            //                                    outTask4.CIGARETTECODE = item.CIGARETTECODE;
            //                                    outTask4.CIGARETTENAME = item.CIGARETTENAME;
            //                                    outTask4.BARCODE = load.BRANDID + "";
            //                                    outTask4.INOUTTYPE = 20;//入库
            //                                    outTask4.QTY = load2.PLANQTY;
            //                                    outTask4.STATUS = 10;
            //                                    outTask4.CREATETIME = DateTime.Now;
            //                                    entity.AddToT_WMS_STORAGEAREA_INOUT(outTask4);
            //                                    tempPlanQty = tempPlanQty - load2.PLANQTY ?? 0;
            //                                    entity.SaveChanges();
            //                                }
            //                            }
            //                            else
            //                            {
            //                                if (totalCount + totalMantissa < querySource.THRESHOLD)
            //                                {
            //                                    //var query2 = (from item in entity.T_WMS_STORAGEAREA_INOUT where item.AREAID == 2 && item.CELLNO == outTask1.CELLNO select item).Sum(x => x.QTY);
            //                                    //if (query2 != null)
            //                                    //{


            //                                    //    if (query2 + querySource.MANTISSA <= querySource.THRESHOLD)
            //                                    //    {

            //                                    //下达立库补货计划
            //                                    INF_JOBDOWNLOAD load1 = new INF_JOBDOWNLOAD();
            //                                    load1.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
            //                                    load1.JOBID = load1.ID;
            //                                    load1.JOBTYPE = 55;
            //                                    load1.BRANDID = ItemService.GetItemByCode(task.CIGARETTDECODE).BIGBOX_BAR;
            //                                    load1.CREATEDATE = DateTime.Now;
            //                                    load1.PLANQTY = (int)querySource.BOXCOUNT;
            //                                    load1.PRIORITY = 50;
            //                                    load1.SOURCE = AtsCellOutService.getCellNo(task.CIGARETTDECODE, (int)querySource.BOXCOUNT);//out cell
            //                                    load1.TARGET = InfJobDownLoadService.GetTargetOutAddress(load1.SOURCE, load1.PLANQTY ?? 0);
            //                                    load1.STATUS = 0;
            //                                    entity.INF_JOBDOWNLOAD.AddObject(load1);

            //                                    totalCount += load1.PLANQTY ?? 0;
            //                                    decimal tempPlanQty = load1.PLANQTY ?? 0;
            //                                    //下达重力式货架补货计划
            //                                    foreach (var item in querySourcetemp)
            //                                    {
            //                                        if (tempPlanQty <= 0)
            //                                        {

            //                                            break;
            //                                        }
            //                                        INF_JOBDOWNLOAD load2 = new INF_JOBDOWNLOAD();
            //                                        load2.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
            //                                        load2.JOBID = load2.ID;
            //                                        load2.BRANDID = load1.BRANDID;
            //                                        load2.CREATEDATE = DateTime.Now;
            //                                        decimal planQty = item.MANTISSA ?? 0;
            //                                        load2.STATUS = 2;//出库完成置0
            //                                        if (search.Find(x => x.CELLNO + "" == item.TROUGHNUM) != null)
            //                                        {
            //                                            planQty += search.Find(x => x.CELLNO + "" == item.TROUGHNUM).QTY;
            //                                        }
            //                                        if (tempPlanQty >= FullCount - planQty)
            //                                        {
            //                                            load2.PLANQTY = FullCount - planQty;
            //                                        }
            //                                        else
            //                                        {
            //                                            load2.PLANQTY = tempPlanQty;

            //                                        }

            //                                        load2.PRIORITY = 50;
            //                                        load2.SOURCE = load1.TARGET;//out cell立库出口
            //                                        load2.TARGET = querySource.TROUGHNUM;
            //                                        load2.STATUS = 0;
            //                                        if (AtsCellInfoService.GetCellInfo(load1.SOURCE).DISMANTLE == 10)
            //                                        {
            //                                            load2.JOBTYPE = 60;
            //                                        }
            //                                        else
            //                                        {
            //                                            load2.JOBTYPE = 70;//人工码垛
            //                                        }
            //                                        entity.INF_JOBDOWNLOAD.AddObject(load2);

            //                                        T_WMS_STORAGEAREA_INOUT outTask4 = new T_WMS_STORAGEAREA_INOUT();
            //                                        outTask4.TASKNO = load2.JOBID;
            //                                        outTask4.AREAID = 2;//重力式货架
            //                                        outTask4.CELLNO = item.TROUGHNUM;
            //                                        outTask4.CIGARETTECODE = item.CIGARETTECODE;
            //                                        outTask4.CIGARETTENAME = item.CIGARETTENAME;
            //                                        outTask4.BARCODE = load1.BRANDID + "";
            //                                        outTask4.INOUTTYPE = 20;//入库
            //                                        outTask4.QTY = load2.PLANQTY;
            //                                        outTask4.STATUS = 10;
            //                                        outTask4.CREATETIME = DateTime.Now;
            //                                        entity.AddToT_WMS_STORAGEAREA_INOUT(outTask4);
            //                                        tempPlanQty = tempPlanQty - load2.PLANQTY ?? 0;
            //                                        entity.SaveChanges();
            //                                    }
            //                                    //反库 wcs生成 
            //                                    //

            //                                    //    }

            //                                    //}
            //                                }
            //                            }
            //                            tempCount += 50;
            //                        }
            //                    }

            //                }
            //                entity.SaveChanges();
            //            }
            //            ts.Complete();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        String error = ex.Message;
            //    }
        }
    }
}
