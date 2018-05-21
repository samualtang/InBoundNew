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

        //public static void UpdateTaskNum(List<T_UN_POKE> task, decimal sendtaskNum)
        //{
        //    using (Entities data = new Entities())
        //    {
        //        if (task != null)
        //        {
        //            foreach (var item in task)
        //            {
        //                var query = (from items in data.T_UN_POKE where items.POKEID == item.POKEID select items).FirstOrDefault();
        //                query.SENDTASKNUM = sendtaskNum;
        //            }
        //        }
        //        data.SaveChanges();
        //    }
        //}
        //在原来的包号上加1
        //public static decimal getPackageNum(decimal ctype, String lineNum)
        //{

        //    decimal packgenum = BaseService.GetSeq("select s_produce_un_sendtasknum.nextval from dual");//1423 1424+1

        //    return packgenum;
        //    //using (Entities data = new Entities())
        //    //{
        //    //    if (lineNum != null)
        //    //    {
        //    //        //item.LINENUM == lineNum && item.CTYPE == ctype descending 
        //    //        var query = (from item in data.T_UN_POKE    orderby item.SENDTASKNUM select item.SENDTASKNUM).FirstOrDefault();
        //    //        if (query != null)
        //    //        {
        //    //            return (query ?? 0)+1;
        //    //         }
        //    //        else
        //    //            return 1; 
        //    //    }
        //    //    else
        //    //    {
        //    //        var query = (from item in data.T_UN_POKE where item.CTYPE == ctype orderby item.SENDTASKNUM  select item.SENDTASKNUM).FirstOrDefault();
        //    //        if (query != null)
        //    //        {
        //    //            return (query ?? 0) + 1;
        //    //        }
        //    //        else
        //    //            return 1;

        //    //    }
        //    //}
        //}
        /// <summary>
        /// 烟道
        /// </summary>
        /// <param name="takeSize"></param>
        /// <param name="lineNum"></param>
        /// <param name="outlist"></param>
        /// <returns></returns>
        public static object[] getTask(int takeSize, string lineNum, out List<T_UN_POKE> outlist)
        {
            object[] values = new object[227];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            using (Entities data = new Entities())
            {
                List<T_UN_POKE> list = new List<T_UN_POKE>();
                var query = (from item in data.T_UN_POKE 
                            where item.LINENUM == lineNum  && item.STATUS == 10 && item.CTYPE==1
                             orderby item.SENDTASKNUM
                             select item).FirstOrDefault();//取出第一行的sendtasknum(最新的客户)
                if (query == null)
                {
                    outlist = new List<T_UN_POKE>();
                    return values;
                }
                var query1 = (from item in data.T_UN_POKE
                              where item.SENDTASKNUM == query.SENDTASKNUM && item.STATUS == 10 && item.CTYPE == 1 && item.LINENUM == lineNum
                              orderby item.MACHINESEQ, item.TROUGHNUM
                              select item).ToList();
                if (query1 != null)
                    //list = query1.Take(takeSize).ToList();
                    list = query1;
                outlist = list;
               // decimal packageNum = getPackageNum(1, lineNum);
               // pNum = packageNum;
                decimal checkcode = 0;//校验码,为流水号之和
                if (list != null)
                {
                    int j = 0;
                    decimal machineseq = 0;
                    //String customercode = "";
                    foreach (var item in list)
                    {
                        values[j * 9] = item.POKEID;//流水号
                        //customercode = item.CUSTOMERCODE;//12位的客户专卖证号电控只能最大接收9位
                        //if (customercode.Length > 9)
                        //{
                        //    customercode = customercode.Substring(customercode.Length - 9, 9);
                        //}
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
                        //values[j * 9 + 3] = customercode;//客户号
                        values[j * 9 + 3] = item.SORTNUM;//客户号,这里的客户号并不是客户专卖证号,而是任务号
                        values[j * 9 + 4] = item.SENDTASKNUM;//包装号
                        values[j * 9 + 5] = item.SENDTASKNUM;//发送任务号 25条为一个任务 
                        values[j * 9 + 6] = item.PACKAGEMACHINE;//包装机号
                        values[j * 9 + 7] = item.SORTNUM;//备用:排序号
                        values[j * 9 + 8] = item.CIGARETTECODE;//条烟条码 
                        j++;
                        checkcode += item.POKEID;
                        
                    } 
                    values[225] = 1;//完成信号
                    values[226] = checkcode;//校验码,为流水号之和
                }
                return values;
            }
        }
        /// <summary>
        /// 六组烟柜
        /// </summary>
        /// <param name="takeSize"></param>
        /// <param name="lineNum"></param>
        /// <param name="outlist"></param>
        /// <returns></returns>
        public static object[] getSixCabinetTask(int takeSize, string lineNum, out List<T_UN_POKE> outlist)
        {
           
            object[] values = new object[227];//一个任务
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            using (Entities data = new Entities())
            {
                List<T_UN_POKE> list = new List<T_UN_POKE>();
                var query =( from item in data.T_UN_POKE 
                            where item.STATUS == 10  && item.CTYPE ==2  
                            orderby  item.SENDTASKNUM select item).FirstOrDefault(); //取出第一行的sendtasknum
                if (query == null)
                {
                    outlist = new List<T_UN_POKE>();
                    return values;
                }
               // packageNum = getPackageNum(2, lineNum);//取包号 前期需要 
                var query1 = (from  item in data.T_UN_POKE
                             where item.SENDTASKNUM == query.SENDTASKNUM  && item.STATUS  ==  10  && item.CTYPE == 2
                               orderby item.MACHINESEQ,item.TROUGHNUM
                                  select item).ToList();
                              
                if (query1 != null)
                    list = query1;
                outlist = list;
                decimal checkcode = 0;//校验码,为流水号之和
                if (list != null)
                {
                    int j = 0;
                    decimal machineseq = 0;//物理通道号
                    //String customercode = "";
                    
                    foreach (var item in list)
                    {
                        values[j * 9] = item.POKEID;//流水号
                        machineseq = (item.MACHINESEQ ?? 0);
                        //customercode = item.CUSTOMERCODE;//12位的客户专卖证号电控只能最大接收9位
                        //if (customercode.Length>9) {
                        //    customercode = customercode.Substring(customercode.Length-9  ,9);
                        //}

                        values[j * 9 + 1] = machineseq;//烟道地址
                        values[j * 9 + 2] = 21;//尾数标志 >20
                        values[j * 9 + 3] = item.SORTNUM;//客户号,这里的客户号并不是客户专卖证号,而是任务号
                        values[j * 9 + 4] = item.SENDTASKNUM;//包装号 item.SENDTASKNUM 取最新一个客户
                        values[j * 9 + 5] = item.SENDTASKNUM;//发送任务号 25条为一个任务 
                        values[j * 9 + 6] = item.PACKAGEMACHINE;//包装机号
                        values[j * 9 + 7] = item.SORTNUM;//备用:排序号
                        values[j * 9 + 8] = item.CIGARETTECODE;//条烟条码
                        j++;
                        checkcode += item.POKEID;
                    }

                    values[225] = 1;//完成信号
                    values[226] = checkcode;//校验码,为流水号之和
                }
                return values;
            }
        }

        /// <summary>
        /// 获取烟柜分拣线
        /// </summary>
        /// <returns>烟柜分拣线</returns>
        public static String getSixCabinetLineNum()
        {
            string lineNum ="";
            using(Entities data = new Entities ())
            {
             var    query = (from item in data.T_UN_POKE
                                where item.STATUS == 10 && item.CTYPE == 2
                                orderby item.SORTNUM, item.SECSORTNUM, item.MACHINESEQ, item.TROUGHNUM
                                select item).FirstOrDefault();//分拣线  
             if (query != null)
             {
                 lineNum = query.LINENUM;
             }
             else
             {
                 lineNum = "3";//没有数据
             }
           
            }
            return lineNum;
         }

        /// <summary>
        /// 混合烟道
        /// </summary>
        /// <param name="takeSize"></param>
        /// <param name="lineNum"></param>
        /// <param name="outlist"></param>
        /// <param name="packageNum"></param>
        /// <returns></returns>
        public static object[] getShapeSmokeTask(int takeSize, string lineNum, out List<T_UN_POKE> outlist)
        {
            object[] values = new object[227];//一个任务
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            using (Entities data = new Entities())
            {
                List<T_UN_POKE> list = new List<T_UN_POKE>();
                var query = (from item in data.T_UN_POKE
                            where item.STATUS == 10 && item.CTYPE == 1
                            orderby item.SENDTASKNUM
                            select item).FirstOrDefault();

                var query1 = (from item in data.T_UN_POKE
                              where item.SENDTASKNUM == query.SENDTASKNUM && item.STATUS == 10 && item.CTYPE == 1
                              orderby item.MACHINESEQ, item.TROUGHNUM
                              select item).ToList();
                //packageNum = getPackageNum(2, null);
                if (query1 != null)
                    list = query1.ToList();
                outlist = list;
                decimal checkcode = 0;//校验码,为流水号之和
                if (list != null)
                {
                    int j = 0;
                    decimal machineseq = 0;//物理通道号
                    //String customercode = "";

                    foreach (var item in list)
                    {
                        values[j * 9] = item.POKEID;//流水号
                        machineseq = (item.MACHINESEQ ?? 0);
                        //customercode = item.CUSTOMERCODE;//12位的客户专卖证号电控只能最大接收9位
                        //if (customercode.Length > 9)
                        //{
                        //    customercode = customercode.Substring(customercode.Length - 9, 9);
                        //} 
                        values[j * 9 + 1] = machineseq;//烟道地址
                        values[j * 9 + 2] = 21;//尾数标志 >20
                        values[j * 9 + 3] = item.SORTNUM;//客户号
                        values[j * 9 + 4] = item.SENDTASKNUM;//包装号
                        values[j * 9 + 5] = item.SENDTASKNUM;//发送任务号 25条为一个任务 
                        values[j * 9 + 6] = item.PACKAGEMACHINE;//包装机号
                        values[j * 9 + 7] = item.SORTNUM;//备用:排序号
                        values[j * 9 + 8] = item.CIGARETTECODE;//条烟条码
                        j++;
                        checkcode += item.POKEID;
                    }

                    values[225] = 1;//完成信号
                    values[226] = checkcode;//校验码,为流水号之和
                }
                return values;
            }
        }
  
        /// <summary>
        /// 获取完成信号
        /// </summary>
        /// <param name="takeSize"></param>
        /// <param name="lineNum"></param>
        /// <param name="outlist"></param>
        /// <returns></returns>
        public static object[] GetFinishSignalTask(int takeSize, string lineNum, out List<T_UN_POKE> outlist)
        {  
            object[] values = new object[227];//一个任务
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = 0;
            }
            using (Entities data = new Entities())
            {
                List<T_UN_POKE> list = new List<T_UN_POKE>();
                var query = from item in data.T_UN_POKE
                            where item.STATUS == 10 && item.CTYPE == 2
                            orderby item.SORTNUM, item.SECSORTNUM, item.MACHINESEQ, item.TROUGHNUM
                            select item; 
                if (query != null)
                    list = query.Take(takeSize).ToList();
                outlist = list;
                decimal checkcode = 0;//校验码,为流水号之和
                if (list != null)
                {
                    int j = 0;
                    decimal machineseq = 0;//物理通道号
                    String customercode = "";
                    foreach (var item in list)
                    {
                        values[j * 9] = item.POKEID;//流水号
                        machineseq = (item.MACHINESEQ ?? 0);
                        customercode = item.CUSTOMERCODE;//12位的客户专卖证号电控只能最大接收9位
                        if (customercode.Length > 9)
                        {
                            customercode = customercode.Substring(customercode.Length - 9, 9);
                        } 
                        values[j * 9 + 1] = machineseq;//烟道地址
                        values[j * 9 + 2] = 21;//尾数标志 >20
                        values[j * 9 + 3] = customercode;//客户号
                        values[j * 9 + 4] = 0;//包装号
                        values[j * 9 + 5] = item.SENDTASKNUM;//发送任务号 25条为一个任务 
                        values[j * 9 + 6] = item.PACKAGEMACHINE;//包装机号
                        values[j * 9 + 7] = item.SORTNUM;//备用:排序号
                        values[j * 9 + 8] = item.CIGARETTECODE;//条烟条码
                        j++;
                        checkcode += item.POKEID;
                    }
                    values[225] = 1;//完成信号
                    values[226] = checkcode;//校验码,为流水号之和
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
        public static String getTarget(String cellNo)
        {
            if (cellNo == "")
                return cellNo;
            String layewayno = cellNo.Substring(2, 1);
            if (layewayno == "1" || layewayno == "2")
            {
                String id = Inf_EquipmentStatusService.GetINFEQUIPMENTSTATUS("1341").EQUIPMENTSTATUS;
                if (id == "1")
                {
                    int taskcount = InfJobDownLoadService.GetMiddleUnFinishTask();
                    if (taskcount == 0)
                    {
                        return "1341";
                    }
                    else
                        return "1355";
                }
                else
                {
                    return "1355";
                }
            }
            else
            {
                return "1355";
            }
        }
        public static void PreUpdateInOut(bool unFullFirst)
        {
           // unFullFirst = false;//不管散盘优先
            List<T_PRODUCE_SORTTROUGH> listNormal = SortTroughService.GetTroughNotINCigaretteType(10, 20);//分拣通道

            using (Entities entity = new Entities())
            {
                foreach (var task in listNormal)
                {
                    String cellno = "";
                    try
                    {
                    
                    var query = (from item in entity.T_WMS_STORAGEAREA_INOUT where item.AREAID == 3 && item.CELLNO == task.TROUGHNUM select item).Sum(x => x.QTY).GetValueOrDefault();

                    var itemDetail = ItemService.GetItemByCode(task.CIGARETTECODE);
                    var leftCount = (task.TRANSPORTATIONLINE??0) - (query + task.MANTISSA);//容量值-理论尾数值
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
                            load1.PILETYPE = decimal.Parse(itemDetail.DXTYPE);
                            load1.SOURCE = AtsCellOutService.getCellNoEqual(task.CIGARETTECODE, leftBox);//out cell
                            load1.TARGET = getTarget(load1.SOURCE); //InfJobDownLoadService.GetTargetOutAddress(load1.SOURCE, leftBox);//异型烟人工出口
                            cellno = load1.SOURCE;
                            load1.STATUS = 0;

                            load1.JOBTYPE = 55;//补货出库
                            if (load1.SOURCE != "" && load1.TARGET != "")
                            {
                                T_WMS_ATSCELLINFO_DETAIL dl = AtsCellInfoDetailService.GetDetail(load1.SOURCE);
                                T_WMS_ATSCELLINFO dinfo = AtsCellInfoService.GetCellInfo(load1.SOURCE);
                                if (dinfo.DISMANTLE == 10)
                                {
                                    if (dl.REQUESTQTY != dl.QTY)
                                    {
                                        load1.TUTYPE = 2;//返库
                                    }
                                    else
                                    {
                                        load1.TUTYPE = 1;//不返库
                                    }
                                }
                                else
                                {
                                    load1.TUTYPE = 3;
                                }
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
                            load2.TARGET ="11020";//异型烟货架通道
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
                                load1.PILETYPE = decimal.Parse(itemDetail.DXTYPE);
                                load1.JOBTYPE = 55;//补货出库
                                load1.SOURCE = AtsCellOutService.getCellNoAll(task.CIGARETTECODE, int.Parse((task.BOXCOUNT ?? 0) + ""));//out cell
                                load1.TARGET = getTarget(load1.SOURCE);// InfJobDownLoadService.GetTargetOutAddress(load1.SOURCE, task.BOXCOUNT ?? 0);//异型烟人工出口
                                cellno = load1.SOURCE;
                                load1.STATUS = 0;
                                if (load1.SOURCE != "" && load1.TARGET != "")
                                {
                                    T_WMS_ATSCELLINFO_DETAIL dl = AtsCellInfoDetailService.GetDetail(load1.SOURCE);
                                    T_WMS_ATSCELLINFO dinfo = AtsCellInfoService.GetCellInfo(load1.SOURCE);
                                    if (dinfo.DISMANTLE == 10)
                                    {
                                        if (dl.REQUESTQTY != dl.QTY)
                                        {
                                            load1.TUTYPE = 2;//返库
                                        }
                                        else
                                        {
                                            load1.TUTYPE = 1;//不返库
                                        }
                                    }
                                    else
                                    {
                                        load1.TUTYPE = 3;
                                    }
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
                                load2.TARGET = "11020";//异型烟货架通道
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
                    catch(Exception ex)
                    {
                        if(cellno!=null &&cellno!="")
                        {
                            RollBack(cellno);
                        }
                        if (ex != null && ex.Message != null)
                        {
                            WriteLog.GetLog().Write("异常:"+ex.Message);
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
                        
                        var query = (from itemlist in entity.T_WMS_STORAGEAREA_INOUT 
                                     where itemlist.TASKNO == poke.BILLCODE && itemlist.AREAID==3 && itemlist.CELLNO==num  && itemlist.QTY<0 select itemlist).FirstOrDefault();
                        if (query != null)
                            break;
                        decimal totalQty = (from itemlist1 in entity.T_UN_POKE
                                      where itemlist1.BILLCODE == poke.BILLCODE && itemlist1.TROUGHNUM == num
                                      select itemlist1).Sum(x => x.TASKQTY??0);
                        T_WMS_STORAGEAREA_INOUT outTask2 = new T_WMS_STORAGEAREA_INOUT();
                        outTask2.ID = entity.ExecuteStoreQuery<decimal>("select S_wms_storagearea_inout.nextval from dual").First();
                        outTask2.AREAID = 3;//烟柜 分拣
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
        /// <summary>
        /// 混合烟道
        /// </summary>
        /// <param name="task"></param>
        /// <param name="status"></param>
        /// <param name="tasknum"></param>
        public static void UpdateTask(List<T_UN_POKE> task, int status, decimal tasknum)
        {
            using (Entities data = new Entities())
            {
                List<T_UN_POKE> list = new List<T_UN_POKE>();
                var query1=  from item in data.T_UN_POKE
                             where item.TASKNUM == tasknum && item.STATUS == 10 && item.TASKNUM > tasknum  
                            select item ;
                if (query1 != null)
                {
                    list = query1.ToList(); 
                    foreach (var item in list)
                    {
                        var query = (from items in data.T_UN_POKE where items.TASKNUM == tasknum select items).FirstOrDefault();
                        query.STATUS = status;
                    }
                }
                data.ExecuteStoreCommand("update t_un_task set state=30 where  tasknum not in (select tasknum from t_un_poke where status!=20)");
                data.SaveChanges();
            }
        }
        
        // 根据异形烟整包任务号更新poke表中状态
        public static void UpdateunTask(decimal sendtasknum, int status)
        {
            
            using (Entities data = new Entities())
            {
                try
                { 
                    var query = (from items in data.T_UN_POKE where items.SENDTASKNUM == sendtasknum select items).ToList();
                    foreach (var item in query)
                    {
                        item.STATUS = status;
                    }
                    data.ExecuteStoreCommand("update t_un_task set state=20 where  tasknum not in (select tasknum from t_un_poke where status!=20)");
                    data.SaveChanges();
                }
                catch (Exception e)
                {

                    throw e;
                }
                
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
        /// <summary>
        /// 任务号区间查询
        /// </summary>
        /// <param name="sortnumFrom">起始任务号</param>
        /// <param name="sortnumTo">结束任务号</param>
        /// <returns></returns>
        public static List<T_UN_POKE> GetListByBillCode(decimal sortnumFrom,decimal sortnumTo)
        {
            using (Entities data = new Entities())
            {
                var query = (from items in data.T_UN_POKE where items.SORTNUM >= sortnumFrom && items.SORTNUM <= sortnumTo select items).ToList();

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
        /// <summary>
        /// 更新任务(任务号区间查询) 
        /// </summary>
        /// <param name="sortnumFrom">起始任务号</param>
        /// <param name="sortnumTo">结束任务号</param>
        /// <param name="status"></param>
        public static void UpdateTask(decimal sortnumFrom, decimal sortnumTo, decimal status)
        {
            using (Entities data = new Entities())
            {
                var query = (from items in data.T_UN_POKE where items.SORTNUM >= sortnumFrom && items.SORTNUM <= sortnumTo select items).ToList();

                foreach (var item in query)
                {
                    item.STATUS = status;
                }
                var query2 = (from item in data.T_UN_TASK where item.SORTNUM >= sortnumFrom && item.SORTNUM <= sortnumTo select item).FirstOrDefault();
                if (status == 15)
                {
                    query2.STATE = "30";
                }
                else
                {
                    query2.STATE = "20";
                }

                 data.ExecuteStoreCommand("update t_un_task set state=30 where  tasknum not in (select tasknum from t_un_poke where status!=15)");
                data.SaveChanges();
            }
        }

        public static List<TaskDetail> getData(decimal sortnum)
        {
            using (Entities dataentity = new Entities())
            {
                //var query = from item in dataentity.T_UN_TASK
                //            where item.SORTNUM == sortnum
                //            orderby item.SORTNUM
                //            group item by new { item.BILLCODE, item.SORTNUM, item.SECSORTNUM,item.STATE } into g
                //            select new TaskDetail() { SortNum = g.Key.SORTNUM ?? 0, SecSortNum = g.Key.SECSORTNUM ?? 0, tNum = g.Sum(x => x.ORDERQUANTITY ?? 0), Billcode = g.Key.BILLCODE, CIGARETTDECODE = g.Key.STATE };
                var query = from item in dataentity.T_UN_POKE
                            join item2 in dataentity.T_PRODUCE_SORTTROUGH
                            on item.TROUGHNUM equals item2.TROUGHNUM
                            where (item2.CIGARETTETYPE == 30 || item2.CIGARETTETYPE == 40) && item2.TROUGHTYPE == 10&& item.SORTNUM == sortnum
                            orderby item.SORTNUM, item2.SEQ, item.MACHINESEQ, item.SENDTASKNUM
                            select new TaskDetail() { POKENUM = item.POKENUM ?? 0, STATUS = item.STATUS ?? 0, SortNum = item.SORTNUM ?? 0, SENDTASKNUM = item.SENDTASKNUM ?? 0, Billcode = item.BILLCODE, CIGARETTDECODE = item2.CIGARETTECODE, CIGARETTDENAME = item2.CIGARETTENAME, LINENUM = item.LINENUM, PACKAGEMACHINE = item.PACKAGEMACHINE ?? 0 };

                if (query != null)
                    return query.OrderBy(x => x.SortNum).ToList();
                else return null;
            }
        }
        public static List<TaskDetail> getDataAll()
        {
            using (Entities dataentity = new Entities())
            {
                //var query = from item in dataentity.T_UN_TASK
                //            orderby item.SORTNUM
                //            group item by new { item.BILLCODE, item.SORTNUM, item.SECSORTNUM, item.STATE } into g
                //            select new TaskDetail() { SortNum = g.Key.SORTNUM ?? 0, SecSortNum = g.Key.SECSORTNUM ?? 0, tNum = g.Sum(x => x.ORDERQUANTITY ?? 0), Billcode = g.Key.BILLCODE,   CIGARETTDECODE = g.Key.STATE };
                

                //var query = from item in dataentity.T_UN_POKE
                //            orderby item.SORTNUM
                //            group item by new { item.BILLCODE, item.SORTNUM, item.STATUS, item.SENDTASKNUM } into g
                //            select new TaskDetail() { SortNum = g.Key.SORTNUM ?? 0, SENDTASKNUM = g.Key.SENDTASKNUM ?? 0, tNum = g.Sum(x => x.POKENUM ?? 0), Billcode = g.Key.BILLCODE, STATUS = g.Key.STATUS ?? 0 };
                var query = from item in dataentity.T_UN_POKE
                            join item2 in dataentity.T_PRODUCE_SORTTROUGH
                            on item.TROUGHNUM equals item2.TROUGHNUM
                            where (item2.CIGARETTETYPE == 30 || item2.CIGARETTETYPE == 40) && item2.TROUGHTYPE == 10
                            orderby item.SORTNUM, item2.SEQ, item.MACHINESEQ, item.SENDTASKNUM
                            select new TaskDetail() {POKENUM = item.POKENUM ??0 , STATUS = item.STATUS ?? 0, SortNum = item.SORTNUM??0,SENDTASKNUM = item.SENDTASKNUM??0, Billcode = item.BILLCODE , CIGARETTDECODE = item2.CIGARETTECODE, CIGARETTDENAME = item2.CIGARETTENAME,LINENUM = item.LINENUM ,PACKAGEMACHINE= item.PACKAGEMACHINE??0 };

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
