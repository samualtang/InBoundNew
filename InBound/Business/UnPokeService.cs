using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Model;

namespace InBound.Business
{
    public class UnPokeService : BaseService
    {


        public static void test()
        {

            using (Entities data = new Entities())
            {
                data.ExecuteStoreCommand("update t_un_poke set sortmachine=10");
            }
        }
        public static object[] getTask(int takeSize, string lineNum, out List<T_UN_POKE> outlist)
        {

            object[] values = new object[226];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            using (Entities data = new Entities())
            {
                List<T_UN_POKE> list = new List<T_UN_POKE>();
                var query = from item in data.T_UN_POKE where item.LINENUM == lineNum && item.STATUS == 10 orderby item.SORTNUM, item.SECSORTNUM,item.MACHINESEQ,item.TROUGHNUM select item;
                if (query != null)
                    list = query.Take(takeSize).ToList();
                outlist = list;
                if (list != null)
                {
                    int j = 0;
                    decimal machineseq = 0;
                    foreach (var item in list)
                    {
                        values[j * 9] = item.POKEID;//流水号
                        machineseq = (item.MACHINESEQ??0);
                        if (item.MACHINESEQ > 1000 && item.MACHINESEQ < 2000)
                        {
                            machineseq = (item.MACHINESEQ??0) - 1000;
                        }
                        else if (item.MACHINESEQ > 2000 && item.MACHINESEQ < 3000)
                        {
                            machineseq = (item.MACHINESEQ??0) - 2000;
                        }

                        values[j * 9 + 1] = machineseq;//烟道地址
                        values[j * 9 + 2] = 21;//尾数标志 >20
                        values[j * 9 + 3] = item.SORTNUM;//任务号
                        values[j * 9 + 4] = 0;//包装号
                        values[j * 9 + 5] = 0;//备用
                        values[j * 9 + 6] = item.PACKAGEMACHINE;//包装机号
                        values[j * 9 + 7] = 0;//备用
                        values[j * 9 + 8] = item.CIGARETTECODE;//条烟条码
                        j++;
                    }
                    values[values.Length - 1] = 1;
                }
                return values;
            }
        }

        public static object[] getCode()
        {
            object[] values = new object[200];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            using (Entities data = new Entities())
            {

                var query = (from item in data.T_PRODUCE_SORTTROUGH where item.TROUGHTYPE == 10 && item.CIGARETTETYPE != 20 select item.CIGARETTECODE).Distinct().ToList();

                if (query != null)
                {
                    int j = 0;
                    foreach (var code in query)
                    {
                        if (j < 200)
                        {
                            values[j] = code;
                            j++;
                        }
                    }
                }
                return values;
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
            unFullFirst = false;//不管散盘优先
            List<T_PRODUCE_SORTTROUGH> listNormal = SortTroughService.GetTroughNotINCigaretteType(10, 20);//分拣通道

            using (Entities entity = new Entities())
            {
                foreach (var task in listNormal)
                {
                    var query = (from item in entity.T_WMS_STORAGEAREA_INOUT where item.AREAID == 3 && item.CELLNO == task.TROUGHNUM select item).Sum(x => x.QTY).GetValueOrDefault();

                    var itemDetail = ItemService.GetItemByCode(task.CIGARETTECODE);
                    var leftCount = 0;// task.TRANSPORTATIONLINE - (query + task.MANTISSA);//容量值-理论尾数值
                    int leftBox = int.Parse((leftCount) / (itemDetail.JT_SIZE ?? 0) + "");//可补件数
                    List<T_WMS_ATSCELLINFO_DETAIL> list = AtsCellInfoService.GetDetail(task.CIGARETTECODE, leftBox);//立库是否有数量等于可补数量的托盘

                    if (list != null && list.Count > 0 && unFullFirst)//散盘优先
                    {


                        if (itemDetail.OUTTYPE == 2)
                        {
                            INF_JOBDOWNLOAD load1 = new INF_JOBDOWNLOAD();
                            load1.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";

                            load1.JOBID = load1.ID;
                            load1.BRANDID = itemDetail.BIGBOX_BAR;
                            load1.CREATEDATE = DateTime.Now;
                            load1.PLANQTY = leftBox;
                            load1.PRIORITY = 50;

                            load1.SOURCE = AtsCellOutService.getCellNoEqual(task.CIGARETTECODE, leftBox);//out cell
                            load1.TARGET = InfJobDownLoadService.GetTargetOutAddress(load1.SOURCE, leftBox);//异型烟人工出口

                            load1.STATUS = 0;

                            load1.JOBTYPE = 55;//补货出库
                            if (load1.SOURCE != "" && load1.TARGET != "")
                            {
                                load1.BARCODE = AtsCellInfoService.GetCellInfo(load1.SOURCE).PALLETNO;
                                entity.INF_JOBDOWNLOAD.AddObject(load1);
                            }
                            else
                            {
                                if (load1.SOURCE != "")
                                {
                                    RollBack(load1.SOURCE);
                                }
                            }
                            T_WMS_STORAGEAREA_INOUT outTask2 = new T_WMS_STORAGEAREA_INOUT();
                            outTask2.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                            outTask2.AREAID = 3;
                            outTask2.TASKNO = load1.JOBID;
                            outTask2.CELLNO = task.TROUGHNUM;
                            outTask2.CIGARETTECODE = itemDetail.ITEMNO;
                            outTask2.BARCODE = itemDetail.BIGBOX_BAR;
                            outTask2.CIGARETTENAME = task.CIGARETTENAME;
                            outTask2.INOUTTYPE = 20;//入
                            outTask2.QTY = leftBox * itemDetail.JT_SIZE;
                            outTask2.GROUPNO = task.GROUPNO;
                            outTask2.CREATETIME = DateTime.Now;
                            outTask2.STATUS = 10;
                            entity.AddToT_WMS_STORAGEAREA_INOUT(outTask2);


                            INF_JOBDOWNLOAD load2 = new INF_JOBDOWNLOAD();
                            load2.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                            load2.JOBID = load2.ID;
                            load2.BRANDID = itemDetail.BIGBOX_BAR;
                            load2.CREATEDATE = DateTime.Now;
                            load2.STATUS = 2;//出库完成置0
                            decimal planQty = leftBox;
                            if (AtsCellInfoService.GetCellInfo(load1.SOURCE).DISMANTLE == 10)
                            {
                                load2.JOBTYPE = 60;
                            }
                            else
                            {
                                load2.JOBTYPE = 70;//
                            }

                            load2.EXTATTR2 = load1.JOBID + "";
                            load2.PRIORITY = 50;
                            load2.SOURCE = load1.TARGET;//out cell立库出口
                            load2.TARGET = task.TROUGHNUM;//异型烟货架通道
                            load2.STATUS = 0;
                            entity.INF_JOBDOWNLOAD.AddObject(load2);

                            entity.SaveChanges();
                        }
                        else
                        {

                            T_WMS_ATSCELL_OUT outcell = new T_WMS_ATSCELL_OUT();
                            outcell.REQUESTQTY = leftBox;
                            outcell.OUTTARGET = AtsCellOutService.getCellNoEqual(task.CIGARETTECODE, leftBox);
                            //10 调拨出库 20 抽检出库 30 补货出库 40 盘点出库 100 其他

                            outcell.OUTTYPE = 50;//自动补货出库

                            outcell.CREATETIME = DateTime.Now;
                            outcell.BARCODE = itemDetail.BIGBOX_BAR;
                            outcell.CIGARETTECODE = itemDetail.ITEMNO;
                            outcell.CIGARETTENAME = itemDetail.ITEMNAME;
                            decimal id = BaseService.GetSeq("select S_ATSCELL_OUT.nextval from dual");
                            outcell.ID = id;
                            if (outcell.OUTTARGET != "")
                            {

                                AtsCellOutService.InsertObject(outcell);
                            }



                        }

                    }
                    else
                    {
                        if (query + task.MANTISSA < task.THRESHOLD)
                        {
                            if (itemDetail.OUTTYPE == 2)
                            {

                                INF_JOBDOWNLOAD load1 = new INF_JOBDOWNLOAD();
                                load1.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                                load1.JOBID = load1.ID;
                                load1.BRANDID = itemDetail.BIGBOX_BAR;
                                load1.CREATEDATE = DateTime.Now;
                                load1.PLANQTY = task.BOXCOUNT;
                                load1.PRIORITY = 50;

                                load1.JOBTYPE = 55;//补货出库
                                load1.SOURCE = AtsCellOutService.getCellNoBig(task.CIGARETTECODE, int.Parse((task.BOXCOUNT ?? 0) + ""));//out cell
                                load1.TARGET = InfJobDownLoadService.GetTargetOutAddress(load1.SOURCE, task.BOXCOUNT ?? 0);//异型烟人工出口

                                load1.STATUS = 0;
                                if (load1.SOURCE != "" && load1.TARGET != "")
                                {
                                    load1.BARCODE = AtsCellInfoService.GetCellInfo(load1.SOURCE).PALLETNO;
                                    entity.INF_JOBDOWNLOAD.AddObject(load1);
                                }
                                else
                                {
                                    if (load1.SOURCE != "")
                                    {
                                        RollBack(load1.SOURCE);
                                    }
                                    continue;
                                }
                                T_WMS_STORAGEAREA_INOUT outTask2 = new T_WMS_STORAGEAREA_INOUT();
                                outTask2.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                                outTask2.AREAID = 3;//烟柜
                                outTask2.TASKNO = load1.JOBID;
                                outTask2.CELLNO = task.TROUGHNUM;
                                outTask2.CIGARETTECODE = task.CIGARETTECODE;
                                outTask2.BARCODE = load1.BRANDID + "";
                                outTask2.CIGARETTENAME = task.CIGARETTENAME;
                                outTask2.INOUTTYPE = 20;//入
                                outTask2.QTY = task.BOXCOUNT * itemDetail.JT_SIZE;
                                outTask2.CREATETIME = DateTime.Now;
                                outTask2.GROUPNO = task.GROUPNO;
                                outTask2.STATUS = 10;
                                entity.AddToT_WMS_STORAGEAREA_INOUT(outTask2);


                                INF_JOBDOWNLOAD load2 = new INF_JOBDOWNLOAD();
                                load2.ID = entity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                                load2.JOBID = load2.ID;
                                load2.BRANDID = load1.BRANDID;
                                load2.PLANQTY = task.BOXCOUNT;
                                load2.CREATEDATE = DateTime.Now;
                                load2.STATUS = 2;//出库完成置0
                                load2.EXTATTR2 = load1.JOBID + "";
                                //decimal planQty = leftBox;
                                if (AtsCellInfoService.GetCellInfo(load1.SOURCE).DISMANTLE == 10)
                                {
                                    load2.JOBTYPE = 60;
                                }
                                else
                                {
                                    load2.JOBTYPE = 70;//人工拆垛
                                }


                                load2.PRIORITY = 50;
                                load2.SOURCE = load1.TARGET;//out cell立库出口
                                load2.TARGET = task.TROUGHNUM;//异型烟货架通道
                                //load2.STATUS = 0;
                                entity.INF_JOBDOWNLOAD.AddObject(load2);

                                entity.SaveChanges();
                            }
                            else
                            {



                                T_WMS_ATSCELL_OUT outcell = new T_WMS_ATSCELL_OUT();
                                outcell.REQUESTQTY = task.BOXCOUNT;
                                //outcell.OUTTARGET = AtsCellOutService.getCellNoEqual(task.CIGARETTECODE, leftBox);
                                //10 调拨出库 20 抽检出库 30 补货出库 40 盘点出库 100 其他
                                outcell.STATUS = 10;
                                outcell.OUTTYPE = 50;//自动补货出库
                                outcell.CREATETIME = DateTime.Now;
                                outcell.BARCODE = itemDetail.BIGBOX_BAR;
                                outcell.CIGARETTECODE = itemDetail.ITEMNO;
                                outcell.CIGARETTENAME = itemDetail.ITEMNAME;
                                decimal id = BaseService.GetSeq("select S_ATSCELL_OUT.nextval from dual");
                                outcell.ID = id;


                                T_WMS_STORAGEAREA_INOUT outTask2 = new T_WMS_STORAGEAREA_INOUT();
                                outTask2.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                                outTask2.AREAID = 3;//烟柜
                                outTask2.TASKNO = id + "";
                                outTask2.CELLNO = task.TROUGHNUM;
                                outTask2.CIGARETTECODE = task.CIGARETTECODE;
                                outTask2.BARCODE = itemDetail.BIGBOX_BAR;
                                outTask2.CIGARETTENAME = task.CIGARETTENAME;
                                outTask2.INOUTTYPE = 20;//入
                                outTask2.QTY = task.BOXCOUNT * itemDetail.JT_SIZE;
                                outTask2.CREATETIME = DateTime.Now;
                                outTask2.STATUS = 10;
                                entity.AddToT_WMS_STORAGEAREA_INOUT(outTask2);
                                entity.SaveChanges();
                                // if (outcell.OUTTARGET != "")
                                //{

                                AtsCellOutService.InsertObject(outcell);
                                //}
                            }


                        }
                    }
                }
            }


        }
        public static void UpdateStroageInout(List<T_UN_POKE> list)
        {
            if (list != null)
            {

                List<String> troughNum = new List<String>();

                foreach (var item in list)
                {
                    if (!troughNum.Contains(item.TROUGHNUM))
                    {
                        troughNum.Add(item.TROUGHNUM);
                    }
                }
                using (Entities entity = new Entities())
                {
                    foreach (var num in troughNum)
                    {
                        List<T_UN_POKE> tempList = list.FindAll(x => x.TROUGHNUM == num);
                       // decimal totalQty = tempList.Sum(x => x.TASKQTY) ?? 0;
                        T_UN_POKE poke = tempList[0];
                        
                        var query = (from itemlist in entity.T_WMS_STORAGEAREA_INOUT where itemlist.TASKNO == poke.BILLCODE && itemlist.AREAID==3 && itemlist.CELLNO==num  && itemlist.QTY<0 select itemlist).FirstOrDefault();
                        if (query != null)
                            break;
                        decimal totalQty = (from itemlist1 in entity.T_UN_POKE
                                      where itemlist1.BILLCODE == poke.BILLCODE && itemlist1.TROUGHNUM == num
                                      select itemlist1).Sum(x => x.TASKQTY??0);
                        T_WMS_STORAGEAREA_INOUT outTask2 = new T_WMS_STORAGEAREA_INOUT();
                        outTask2.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                        outTask2.AREAID = 3;//烟柜
                        outTask2.TASKNO = poke.BILLCODE;
                        outTask2.CELLNO = poke.TROUGHNUM;
                        outTask2.CIGARETTECODE = poke.CIGARETTECODE;
                        T_WMS_ITEM item = ItemService.GetItemByCode(poke.CIGARETTECODE);
                        outTask2.BARCODE = item.BIGBOX_BAR;
                        outTask2.CIGARETTENAME = item.ITEMNAME;
                        outTask2.INOUTTYPE = 10;//出
                        outTask2.QTY = -totalQty;
                        outTask2.CREATETIME = DateTime.Now;
                        outTask2.STATUS = 10;
                        entity.AddToT_WMS_STORAGEAREA_INOUT(outTask2);
                    }
                    entity.SaveChanges();
                }
            }
        }
        public static object[] getName()
        {
            object[] values = new object[6000];

            using (Entities data = new Entities())
            {

                var query = (from item in data.T_PRODUCE_SORTTROUGH where item.TROUGHTYPE == 10 && item.CIGARETTETYPE != 20 && item.CIGARETTENAME!=null select item.CIGARETTENAME).Distinct().ToList();

                if (query != null)
                {
                    String cname = "";
                    int j = 0;
                    int m = 0;
                    foreach (var code in query)
                    {
                        if (j < 200)
                        {
                          
                            byte[] b=initStr(code);
                            string s = Encoding.UTF8.GetString(b);
                            for (int i = 0; i < code.Length; i++)
                            {
                                if (i < 30)
                                {
                                    values[m] = b[i];

                                    m++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            j++;
                        }
                    }
                }
                return values;
            }
        }
        public static byte[] initStr(string str)
        {
            if (Encoding.UTF8.GetBytes(str).Length < 30)
            {
                int i = 30 - Encoding.UTF8.GetBytes(str).Length;
               // Encoding.ASCII.GetBytes(str) 转成数字
                for (int j = 0; j < i; j++)
                {
                    str += "0";
                }
                return Encoding.UTF8.GetBytes(str);
            }
            else
            {
                return Encoding.UTF8.GetBytes(str);
            }
        }
        public static void UpdateTask(List<T_UN_POKE> task, int status)
        {
            using (Entities data = new Entities())
            {
                if (task != null)
                {
                    foreach (var item in task)
                    {
                        var query = (from items in data.T_UN_POKE where items.POKEID == item.POKEID select items).FirstOrDefault();
                        query.STATUS = status;
                    }

                }
                data.ExecuteStoreCommand("update t_un_task set state=30 where  tasknum not in (select tasknum from t_un_poke where status!=20)");
                data.SaveChanges();
            }
        }

        public static List<T_UN_POKE> GetListByBillCode(String billcode)
        {
            using (Entities data = new Entities())
            {
                var query = (from items in data.T_UN_POKE where items.BILLCODE == billcode select items).ToList();

                return query;
            }
        }
        public static void UpdateTask(String billcode,decimal status)
        {
            using (Entities data = new Entities())
            {
                var query = (from items in data.T_UN_POKE where items.BILLCODE == billcode select items).ToList();
              
                foreach (var item in query)
                    {
                        item.STATUS = status;
                    }
                var query2 = (from item in data.T_UN_TASK where item.BILLCODE == billcode select item).FirstOrDefault();
                if (status == 15)
                {
                    query2.STATE = "30";
                }
                else
                {
                    query2.STATE = "20";
                }
               
               // data.ExecuteStoreCommand("update t_un_task set state=30 where  tasknum not in (select tasknum from t_un_poke where status!=15)");
                data.SaveChanges();
            }
        }

        public static List<TaskDetail> getData(decimal sortnum)
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_UN_TASK
                            where item.SORTNUM == sortnum
                            orderby item.SORTNUM
                            group item by new { item.BILLCODE, item.SORTNUM, item.SECSORTNUM,item.STATE } into g
                            select new TaskDetail() { SortNum = g.Key.SORTNUM ?? 0, SecSortNum = g.Key.SECSORTNUM ?? 0, tNum = g.Sum(x => x.ORDERQUANTITY ?? 0), Billcode = g.Key.BILLCODE, CIGARETTDECODE = g.Key.STATE };
                if (query != null)
                    return query.OrderBy(x => x.SortNum).ToList();
                else return null;
            }
        }
        public static List<TaskDetail> getDataAll()
        {
            using (Entities dataentity = new Entities())
            {
                var query = from item in dataentity.T_UN_TASK
                            orderby item.SORTNUM
                            group item by new { item.BILLCODE, item.SORTNUM, item.SECSORTNUM, item.STATE } into g
                            select new TaskDetail() { SortNum = g.Key.SORTNUM ?? 0, SecSortNum = g.Key.SECSORTNUM ?? 0, tNum = g.Sum(x => x.ORDERQUANTITY ?? 0), Billcode = g.Key.BILLCODE,   CIGARETTDECODE = g.Key.STATE };
                if (query != null)
                    return query.OrderBy(x => x.SortNum).ToList();
                else return null;
            }
        }
        public static List<T_UN_POKE> GetLinenum()
        {
            List<T_UN_POKE> list = new List<T_UN_POKE>();
            using (Entities entity = new Entities())
            {
                var query = from item in entity.T_UN_POKE
                            group item by new { item.LINENUM } into lst
                            select new { LINENUM = lst.Key.LINENUM };
                foreach (var item in query)
                {
                    list.Add(new T_UN_POKE
                    {
                        LINENUM = item.LINENUM,
                    });
                }
                return list;
            }
        }

        public static List<T_UN_POKE> FetchUnPokeList(string lineNum)
        {
            /**
            SELECT a.tasknum,a.customercode,a.customername,b.cigarettecode,b.cigarettename,c.pokenum,a.regioncode,to_char(a.orderdate,'yyyy-mm-dd') AS enterdate 
                         FROM t_un_task a,t_produce_sorttrough b,t_un_poke c 
                         WHERE a.tasknum=c.tasknum  and b.troughnum=c.troughnum and b.troughtype=10 and b.cigarettetype in (30,40) and b.state='10' 
                         and a.synseq='1' and c.linenum=2 
                         ORDER BY c.sortnum,c.secsortnum;
             * 
             */
            using (Entities entity = new Entities())
            {
                var query = from item in entity.T_UN_POKE
                            join task in entity.T_UN_TASK on item.TASKNUM equals task.TASKNUM
                            join trough in entity.T_PRODUCE_SORTTROUGH on item.TROUGHNUM equals trough.TROUGHNUM
                            where
                            (trough.CIGARETTETYPE == 30 || trough.CIGARETTETYPE == 40) && trough.TROUGHTYPE == 10 && trough.STATE == "10"
                            && task.SYNSEQ == 1 && item.LINENUM == lineNum
                            select item;
                return query.ToList();
            }
        }

    }
}
