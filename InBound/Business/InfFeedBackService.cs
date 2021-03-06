﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using InBound;

namespace InBound.Business
{
  public  class InfFeedBackService:BaseService
    {
      public static void InsertEntity(INF_JOBFEEDBACK entity)
      {
        
          using (Entities dataEntity = new Entities())
          {

            //  decimal id = 0;
              //id = dataEntity.ExecuteStoreQuery<decimal>("select s_inf_jobfeedback.nextval from dual").First(); //decimal.Parse(dt.Rows[0][0].ToString());
             // entity.ID = id+"";
              entity.ID = Guid.NewGuid().ToString("N");
              entity.STATUS = 0;
              dataEntity.INF_JOBFEEDBACK.AddObject(entity);
              dataEntity.SaveChanges();
          }
      }

      public static INF_JOBFEEDBACK GetFeedBack(String jobid)
      {

          using (Entities dataEntity = new Entities())
          {

              var query = (from item in dataEntity.INF_JOBFEEDBACK where item.JOBID == jobid && item.FEEDBACKSTATUS==99 select item).FirstOrDefault();
              return query;
          }
      }
      public static INF_JOBFEEDBACK GetFeedBack(String jobid,decimal status)
      {

          using (Entities dataEntity = new Entities())
          {

              var query = (from item in dataEntity.INF_JOBFEEDBACK where item.JOBID == jobid && item.FEEDBACKSTATUS == status select item).FirstOrDefault();
              return query;
          }
      }
      public static void UpdateErrorJob(String jobid)
      {

          using (Entities dataEntity = new Entities())
          {

              var query = (from item in dataEntity.INF_JOBFEEDBACK where item.JOBID == jobid && item.FEEDBACKSTATUS==98  select item).FirstOrDefault();
              if (query != null)
              {
                  query.STATUS = 10;
                  dataEntity.SaveChanges();
              }
          }
      }
      public static WriteLog log = WriteLog.GetLog();
      public static void AutoWriteFinishTask()
      {
          //10 码垛任务 20  入库单入库任务 30 成品入库 40 返库任务 50 出库任务
          //55补货出库 60 自动拆垛补货任务 70人工拆垛补货任务 80 开箱任务 
          //90 托盘条码下达（1194 源地址） 91指定拆垛机械手  97任务取消 100 空托盘回收任务
          
              using (Entities dataEntity = new Entities())
              {
                  
                  var query = (from item in dataEntity.INF_JOBFEEDBACK
                               join item2 in dataEntity.INF_JOBDOWNLOAD
                                   on item.JOBID equals item2.JOBID
                               where item.STATUS == 0 && item.FEEDBACKSTATUS == 99
                              // && item2.INPUTTYPE == 10 
                               select item2).ToList();//执行完的任务
                   var query2 = (from item in dataEntity.INF_EQUIPMENTREQUEST 
                                where item.STATUS == 0 
                                select item).ToList();//wcs申请的任务

                   var query3 =( from item in dataEntity.INF_JOBDOWNLOAD
                                join item2 in dataEntity.INF_JOBFEEDBACK
                                on item.JOBID equals item2.JOBID 
                                where (item.JOBTYPE == 60 || item.JOBTYPE == 70)
                                && item2.FEEDBACKSTATUS == 1 && item2.STATUS==0
                                 select item2).ToList();
                   var query4 = (from item in dataEntity.INF_JOBDOWNLOAD
                                 join item2 in dataEntity.INF_JOBFEEDBACK
                                 on item.JOBID equals item2.JOBID
                                 where (item.JOBTYPE == 80)
                                 && item2.FEEDBACKSTATUS == 100 && item.STATUS==1//出重力式货架完成
                                 select item).ToList();

                   var query5 = (from item in dataEntity.INF_JOBDOWNLOAD
                                 join item2 in dataEntity.INF_JOBFEEDBACK
                                 on item.JOBID equals item2.JOBID
                                 where  item2.FEEDBACKSTATUS == 97 && item.STATUS == 1//wcs 任务取消
                                 select item).ToList();
                   if (query5 != null)
                   {
                       foreach (var item in query5)
                       {
                           try
                           {
                               if (item.JOBTYPE == 20 || item.JOBTYPE == 30 || item.JOBTYPE == 40 || item.JOBTYPE == 42)//42的返库后续有可能要特殊处理
                               {
                                   using (TransactionScope ts = new TransactionScope())
                                   {
                                       item.STATUS = 97;
                                       AtsCellInfoService.delete(item.TARGET);
                                       AtsCellInfoDetailService.delete(item.TARGET);
                                       AtsCellService.UpdateAtsCell(item.TARGET, 10);//更新为空闲
                                       if (item.JOBTYPE == 20)//更新锁定数量
                                       {
                                           var inboundLine = (from line in dataEntity.T_WMS_INBOUND_LINE where line.INBOUNDDETAILID == item.INBOUNDNO && line.BARCODE == item.BRANDID select line).FirstOrDefault();
                                           if (inboundLine == null)
                                           {
                                               WriteLog.GetLog().Write("找不到入库单号:" + item.INBOUNDNO);
                                               continue;
                                           }
                                           else
                                           {
                                               inboundLine.LOCKQTY -= item.PLANQTY;
                                           }
                                       }
                                       dataEntity.SaveChanges();
                                       ts.Complete();
                                   }
                               }
                               else if (item.JOBTYPE == 50 || item.JOBTYPE == 52 || item.JOBTYPE == 55)
                               {
                                   using (TransactionScope ts = new TransactionScope())
                                   {
                                       item.STATUS = 97;
                                       AtsCellInfoService.GetCellInfo(item.SOURCE, dataEntity).STATUS = 30;//
                                       AtsCellService.UpdateAtsCell(item.SOURCE, 10);
                                       AtsCellInfoDetailService.GetDetail(item.SOURCE, dataEntity).REQUESTQTY = 0;
                                       if (item.JOBTYPE == 55)
                                       {
                                           var cd = (from cditem in dataEntity.INF_JOBDOWNLOAD where cditem.EXTATTR2 == item.JOBID select cditem).ToList();
                                           if (cd != null)
                                           {
                                               foreach (var v in cd)
                                               {
                                                   v.STATUS = 97;//取消
                                                   StroageInOutService.Del(v.JOBID + "", dataEntity);
                                               }
                                           }
                                       }
                                       dataEntity.SaveChanges();
                                       ts.Complete();
                                   }

                               }
                               else if (item.JOBTYPE == 80)
                               {
                                   item.STATUS = 97;
                                   StroageInOutService.Del(item.JOBID + "", dataEntity);
                                   dataEntity.SaveChanges();
                               }
                           }
                           catch (Exception e)
                           {
                               log.Write("错误信息:" + e.Message);
                           }
                       }
                   }
                   if (query4 != null)
                   {
                       foreach (var task in query4)
                       {
                           InfJobDownLoadService.UpdateJopDownLoad(task.JOBID, 10,dataEntity);
                           //var report = (from reportitem in dataEntity.T_WMS_STORAGEAREA_INOUT where reportitem.TASKNO == task.JOBID select reportitem).ToList();
                           //if (report != null && report.Count > 0)
                           //{
                           //    report.ForEach(x => x.STATUS = 20);
                           //}
                       }
                   }
                   if (query3 != null)
                   {
                       foreach (var task in query3)
                       {
                           if (task.PLANQTY != null)
                           {
                               StroageInOutService.UpdateInOut(task.JOBID, task.ACTQTY ?? 0, dataEntity);
                               task.STATUS = 1;
                               dataEntity.SaveChanges();
                           }
                       }
                   }
                  if (query2 != null && query2.Count > 0)
                  {
                      
                      foreach (var temptask in query2)
                      {

                          try{
                          //using (TransactionScope ts = new TransactionScope())
                          //{
                      INF_JOBDOWNLOAD task = new INF_JOBDOWNLOAD();
                      //T_WMS_ATSCELLINFO cellInfo = AtsCellInfoService.CheckPalletExist(temptask.BARCODE);//检查托盘号是否存在
                      //if (temptask.BARCODE != null && temptask.BARCODE != "" && (cellInfo==null))//储位中不存在该托盘 || cellInfo.PALLETNO==temptask.BARCODE))
                      //{
                      task.JOBID = dataEntity.ExecuteStoreQuery<decimal>("select s_inf_jobdownload.nextval from dual").First() + "";
                      task.ID = task.JOBID;
                      task.CREATEDATE = DateTime.Now;
                      task.BRANDID = temptask.BRANDID;
                      task.PLANQTY = temptask.REQUESTQTY;
                      task.INPUTTYPE = 10;
                      temptask.RESPONDDATE = DateTime.Now;
                      if (temptask.REQUESTTYPE != 3)
                      {
                         
                          if (temptask.REQUESTTYPE == 1)//入库请求
                          {
                              if (temptask.INBOUNDNO == null || temptask.INBOUNDNO == 0) //返库
                              {
                                  if (temptask.EQUIPMENTID == "1422")//空托盘返库
                                  {

                                      task.JOBID = dataEntity.ExecuteStoreQuery<decimal>("select s_inf_jobdownload.nextval from dual").First() + "";

                                      task.BRANDID = "1111111";
                                      task.BARCODE = RefRFIDPalletService.GetSeq() + "";
                                      task.SOURCE = temptask.EQUIPMENTID;
                                      task.PLANQTY = 10;

                                      task.JOBTYPE = 100;//空托盘回收任务
                                      task.TARGET = AtsCellInService.getCellNoCode(task.BRANDID + ""); //空托盘指定地址
                                      task.TUTYPE = 3;

                                      task.PRIORITY = 50;


                                      try
                                      {
                                          if (task.TARGET != null && task.TARGET != "")
                                          {
                                              using (TransactionScope ts = new TransactionScope())
                                              {
                                                  T_WMS_ATSCELLINFO info = new T_WMS_ATSCELLINFO();
                                                  // info.PALLETNO = RefRFIDPalletService.GetSeq() + "";// task.BARCODE;

                                                  task.BARCODE = RefRFIDPalletService.GetSeq() + "";// AtsCellInfoService.GetCellInfo(task.TARGET).PALLETNO;
                                                  info.CELLNO = task.TARGET;
                                                  info.STATUS = 10;//组盘
                                                  info.CREATETIME = DateTime.Now;
                                                  info.INBOUNDID = task.INBOUNDNO;
                                                  info.PALLETNO = task.BARCODE;
                                                  info.DISMANTLE = 10;

                                                  AtsCellInfoService.InsertAtsCellInfo(info);

                                                  T_WMS_ATSCELLINFO_DETAIL detail = new T_WMS_ATSCELLINFO_DETAIL();
                                                  detail.BARCODE = task.BRANDID + "";
                                                  T_WMS_ITEM item = ItemService.GetItemByBarCode(detail.BARCODE);

                                                  detail.CIGARETTECODE = item.ITEMNO;
                                                  detail.CIGARETTENAME = item.ITEMNAME;
                                                  detail.QTY = task.PLANQTY;
                                                  detail.CELLNO = info.CELLNO;
                                                  AtsCellInfoDetailService.InsertAtsCellInfo(detail);
                                                  task.CREATEDATE = DateTime.Now;
                                                  task.STATUS = 0;
                                                  dataEntity.INF_JOBDOWNLOAD.AddObject(task);
                                                  temptask.STATUS = 1;
                                                  dataEntity.SaveChanges();
                                                  ts.Complete();
                                              }
                                          }
                                          else
                                          {

                                              WriteLog.GetLog().Write("品牌" + task.BRANDID + "暂无库存");
                                          }
                                      }
                                      catch (Exception ex)
                                      {
                                          if (task.TARGET != "")
                                          {
                                              InBoundService.RollBack(task.TARGET); //回滚
                                          }
                                      }
                                  }
                                  else
                                  {
                                      T_WMS_ATSCELLINFO cellInfo = AtsCellInfoService.CheckPalletExist(temptask.BARCODE);//检查托盘号是否存在
                                      if (cellInfo == null)
                                      {
                                          continue;
                                      }
                                      T_WMS_ATSCELLINFO_DETAIL detail = AtsCellInfoDetailService.GetDetail(cellInfo.CELLNO);
                                     
                                      if ( (detail.REQUESTQTY??0) != detail.QTY)
                                      {

                                          INF_JOBDOWNLOAD task1 = new INF_JOBDOWNLOAD();
                                          task1.JOBID = dataEntity.ExecuteStoreQuery<decimal>("select s_inf_jobdownload.nextval from dual").First() + "";
                                          task1.ID = task.JOBID;
                                          task1.BRANDID = detail.BARCODE;
                                          task1.BARCODE = cellInfo.PALLETNO;
                                          task1.SOURCE = temptask.EQUIPMENTID;
                                          task1.PLANQTY = detail.QTY - detail.REQUESTQTY;
                                          task1.INPUTTYPE = 10;
                                          task1.JOBTYPE = 40;//返库任务
                                          task1.TARGET = AtsCellInService.getCellNoCode(task.BRANDID + "");
                                          task1.TUTYPE = 4;
                                          task1.PRIORITY = 50;
                                          task1.STATUS = 0;
                                          task1.CREATEDATE = DateTime.Now;
                                          dataEntity.INF_JOBDOWNLOAD.AddObject(task1);

                                          T_WMS_ATSCELLINFO info = new T_WMS_ATSCELLINFO();
                                          info.PALLETNO = cellInfo.PALLETNO;
                                          // info.DISMANTLE = 1;
                                          info.CELLNO = task1.TARGET;
                                          info.STATUS = 10;//组盘
                                          info.CREATETIME = cellInfo.CREATETIME;
                                          info.INBOUNDID = task.INBOUNDNO;
                                          task.TUTYPE = 4;
                                          info.DISMANTLE = 10;
                                          try
                                          {
                                              using (TransactionScope ts = new TransactionScope())
                                              {
                                                  AtsCellInfoService.InsertAtsCellInfo(info);

                                                  T_WMS_ATSCELLINFO_DETAIL details = new T_WMS_ATSCELLINFO_DETAIL();
                                                  details.BARCODE = detail.BARCODE;
                                                  T_WMS_ITEM item = ItemService.GetItemByBarCode(details.BARCODE);
                                                  details.CIGARETTECODE = item.ITEMNO;
                                                  details.CIGARETTENAME = item.ITEMNAME;
                                                  details.QTY = task1.PLANQTY;
                                                  details.CELLNO = info.CELLNO;
                                                  AtsCellInfoDetailService.InsertAtsCellInfo(details);
                                                  AtsCellService.UpdateAtsCell(detail.CELLNO, 10);
                                                  AtsCellInfoService.delete(detail.CELLNO);
                                                  AtsCellInfoDetailService.delete(detail.CELLNO);
                                                  temptask.STATUS = 1;
                                                  dataEntity.SaveChanges();
                                                  ts.Complete();
                                              }
                                          }
                                          catch (Exception ex)
                                          {
                                              if (task1.TARGET != "")
                                              {
                                                  InBoundService.RollBack(task1.TARGET); //回滚
                                              }
                                          }
                                      }
                                      else
                                      {
                                          using (TransactionScope ts = new TransactionScope())
                                          {
                                              AtsCellService.UpdateAtsCell(detail.CELLNO, 10);
                                              AtsCellInfoService.delete(detail.CELLNO);
                                              AtsCellInfoDetailService.delete(detail.CELLNO);
                                              temptask.STATUS = 1;
                                              dataEntity.SaveChanges();
                                              ts.Complete();
                                          }
                                      }
                                  }
                              }
                              else//入库单入库
                              {

                                  task.SOURCE = temptask.EQUIPMENTID;
                                  task.JOBTYPE = 20;
                                  task.INBOUNDNO = temptask.INBOUNDNO;
                                  // task.BARCODE = RefRFIDPalletService.GetSeq() + ""; //temptask.BARCODE;
                                  task.PRIORITY = 50;
                                  //task.BARCODE=
                                  task.TARGET = AtsCellInService.getCellNoCode(task.BRANDID + "");
                                  try
                                  {
                                      using (TransactionScope ts = new TransactionScope())
                                      {
                                          task.BARCODE = RefRFIDPalletService.GetSeq() + "";
                                          var inboundLine = (from line in dataEntity.T_WMS_INBOUND_LINE where line.INBOUNDDETAILID == task.INBOUNDNO select line).FirstOrDefault();
                                          
                                          if (inboundLine != null)
                                          {
                                              var tq = (from record in dataEntity.T_WMS_INBOUND_LINE where record.INBOUNDID == inboundLine.INBOUNDID select record).Sum(x => x.LOCKQTY) ?? 0;
                                              var inbound = (from record in dataEntity.T_WMS_INBOUND where record.INBOUNDID == inboundLine.INBOUNDID select record).FirstOrDefault();

                                              if (tq < task.PLANQTY)
                                              {
                                                  inbound.STARTTIME = DateTime.Now;
                                              }
                                              //else if (tq + task.PLANQTY == inbound.QTY)
                                              //{
                                              //    inbound.FINISHTIME = DateTime.Now;
                                              //}
                                              inboundLine.LOCKQTY += task.PLANQTY;
                                          }
                                          if (temptask.TUTYPE == 3 || temptask.TUTYPE == 2)//空托盘、空托盘组
                                          {
                                              task.BRANDID = "1111111";
                                          }

                                          T_WMS_ATSCELLINFO info = new T_WMS_ATSCELLINFO();
                                          //info.PALLETNO = RefRFIDPalletService.GetSeq() + "";// task.BARCODE;

                                          // AtsCellInfoService.GetCellInfo(task.TARGET).PALLETNO;
                                          info.CELLNO = task.TARGET;
                                          info.PALLETNO = task.BARCODE;
                                          info.STATUS = 10;//组盘
                                          info.CREATETIME = DateTime.Now;
                                          info.INBOUNDID = task.INBOUNDNO;
                                          if (inboundLine != null)
                                          {
                                              info.CONSIGNOR = inboundLine.CONSIGNSOR;
                                          }
                                          info.DISMANTLE = 10;
                                          task.TUTYPE = 4;
                                          AtsCellInfoService.InsertAtsCellInfo(info);

                                          T_WMS_ATSCELLINFO_DETAIL detail = new T_WMS_ATSCELLINFO_DETAIL();
                                          detail.BARCODE = task.BRANDID + "";
                                         T_WMS_ITEM item = ItemService.GetItemByBarCode(detail.BARCODE);

                                          detail.CIGARETTECODE = item.ITEMNO;
                                          detail.CIGARETTENAME = item.ITEMNAME;
                                          detail.QTY = task.PLANQTY;
                                          detail.CELLNO = info.CELLNO;
                                          AtsCellInfoDetailService.InsertAtsCellInfo(detail);


                                          task.CREATEDATE = DateTime.Now;
                                          task.STATUS = 0;
                                          if (task.SOURCE != null && task.SOURCE != "" && task.TARGET != null && task.TARGET != "")//根据地址判断是否下任务
                                          {

                                              dataEntity.INF_JOBDOWNLOAD.AddObject(task);
                                          }
                                          else
                                          {
                                              
                                              WriteLog.GetLog().Write("品牌" + task.BRANDID + "暂无可用货架");
                                              continue;
                                          }
                                          //}
                                          //else //储位中存在该托盘  肯定就是返库任务 人工站台拆垛完成触发的返库任务
                                          //{

                                          //}
                                          temptask.STATUS = 1;
                                          dataEntity.SaveChanges();
                                          ts.Complete();
                                      }
                                  }
                                  catch (Exception ex)
                                  {
                                      if (task.TARGET != "")
                                      {
                                          InBoundService.RollBack(task.TARGET); //回滚
                                      }
                                  }
                              }
                              
                          }
                        
                          else if (temptask.REQUESTTYPE == 2)//出库请求
                          {
                              task.TARGET = temptask.EQUIPMENTID;
                              if (temptask.TUTYPE == 3)//空托盘组
                              {
                                  task.PRIORITY = 99;
                                  task.JOBTYPE = 50;
                                  task.BRANDID = "1111111";

                                  task.SOURCE = AtsCellOutService.getCellNoBig("1111111", 1);//托盘组任务
                                  if (task.SOURCE == "")
                                  {
                                      //WriteLog.GetLog().Write("暂无出库的");
                                      //continue;
                                  }
                                  else
                                  {
                                      task.BARCODE = AtsCellInfoService.GetCellInfo(task.SOURCE).PALLETNO;
                                  }

                                  task.TUTYPE = temptask.TUTYPE;
                                  task.CREATEDATE = DateTime.Now;
                                  task.STATUS = 0;
                                  if (task.SOURCE != null && task.SOURCE != "" && task.TARGET != null && task.TARGET != "")//根据地址判断是否下任务
                                  {

                                      dataEntity.INF_JOBDOWNLOAD.AddObject(task);
                                  }
                                  else
                                  {
                                      WriteLog.GetLog().Write("品牌" + task.BRANDID + "暂无库存");
                                  }
                                
                                  temptask.STATUS = 1;
                                  dataEntity.SaveChanges();
                              }
                              //else if (temptask.TUTYPE == 2)//空托盘
                              //{
                              //    task.JOBTYPE = 90;
                              //    task.BARCODE = "";//获取托盘号 读取RFID
                              //}
                              //else if (temptask.TUTYPE == 4)//实托盘
                              //{
                              //    task.JOBTYPE = 50;
                              //}
                             
                          }
                         
                          // task.TUTYPE = temptask.TUTYPE;


                          // ts.Complete();
                      }
                      else//下达拆垛任务
                      {
                         
                          //新建指定拆垛机械手任务
                          using (TransactionScope ts = new TransactionScope())
                          {
                              INF_JOBDOWNLOAD inf = InfJobDownLoadService.GetDetail(temptask.JOBID,dataEntity);

                              inf.STATUS = 10;//出库任务完成

                              if (temptask.EQUIPMENTID == "1415" && inf.JOBTYPE != 55)
                              {


                                  temptask.STATUS = 1;

                              }
                              else
                              {


                                  INF_JOBDOWNLOAD load = new INF_JOBDOWNLOAD();
                                  load.ID = dataEntity.ExecuteStoreQuery<decimal>("select S_INF_JOBDOWNLOAD.nextval from dual").First() + "";
                                  load.JOBID = load.ID;
                                  load.JOBTYPE = 91;// 指定拆垛机械手任务

                                  load.CREATEDATE = DateTime.Now;
                                  INF_JOBDOWNLOAD item = InfJobDownLoadService.GetDetail(temptask.JOBID);
                                  load.BRANDID = item.BRANDID;
                                  String cellNo = item.SOURCE;
                                  decimal jobType = item.JOBTYPE ?? 0;
                                  load.PILETYPE = item.PILETYPE;
                                  T_WMS_ATSCELLINFO_DETAIL detail = AtsCellInfoDetailService.GetDetail(cellNo);
                                  load.EXTATTR1 = (detail.QTY ?? 0) + "";//实际数量
                                  load.PLANQTY = detail.REQUESTQTY ?? 0;//拆垛数量
                                  load.BARCODE = AtsCellInfoService.GetCellInfo(cellNo).PALLETNO;
                                  load.PRIORITY = 50;
                                  //  load.SOURCE = querySource.TROUGHNUM;


                                  load.TARGET = temptask.EQUIPMENTID;
                                  load.SOURCE = load.TARGET;
                                  load.PILETYPE = item.PILETYPE;
                                  load.STATUS = 0;
                                  dataEntity.AddToINF_JOBDOWNLOAD(load);
                                  dataEntity.SaveChanges();


                                  var cdtask = (from taskitem in dataEntity.INF_JOBDOWNLOAD where taskitem.EXTATTR2 == temptask.JOBID && taskitem.STATUS == 2 select taskitem).ToList();
                                  if (cdtask != null && cdtask.Count > 0)
                                  {
                                      cdtask.ForEach(x => x.STATUS = 0);
                                      cdtask.ForEach(x => x.SOURCE = temptask.EQUIPMENTID);
                                      cdtask.ForEach(x => x.CREATEDATE = DateTime.Now);
                                  }
                                  if (jobType == 55 && detail.QTY == detail.REQUESTQTY && temptask.EQUIPMENTID != "1415")
                                  {
                                      
                                      AtsCellService.UpdateAtsCell(item.SOURCE, 10);//任务置空闲
                                      //AtsCellOutService.UpdateObject(item.TASKNO ?? 0, AtsCellInfoDetailService.GetDetail(item.SOURCE).QTY ?? 0);
                                      AtsCellInfoService.delete(item.SOURCE);
                                      AtsCellInfoDetailService.delete(item.SOURCE);
                                  }
                                  temptask.STATUS = 1;


                              }
                        
                            

                              dataEntity.SaveChanges();
                               ts.Complete();

                          }
                      }
                      }
                      catch(Exception ex)
                      {
                          WriteLog.GetLog().Write("错误信息:" + ex.Message);
                      }
                      }
                     
                  }
                  if (query != null && query.Count > 0)
                  {
                      foreach (var item in query)
                      {
                        
                          using (TransactionScope ts = new TransactionScope())
                              {
                              try{
                                  var feedback = (from feed in dataEntity.INF_JOBFEEDBACK where feed.JOBID == item.JOBID && feed.FEEDBACKSTATUS == 99 select feed).FirstOrDefault();
                                  feedback.STATUS = 10;
                                  if (item.JOBTYPE == 20 || item.JOBTYPE == 30 || item.JOBTYPE == 40 || item.JOBTYPE == 42 || item.JOBTYPE==100)//入库单入库任务
                                  {
                                      InfJobDownLoadService.UpdateJopDownLoad(item.JOBID, 10, dataEntity);
                                      if (item.JOBTYPE == 20 )
                                      {
                                         
                                          String code = item.BRANDID + "";
                                          var inboundLine = (from line in dataEntity.T_WMS_INBOUND_LINE where line.INBOUNDDETAILID == item.INBOUNDNO && line.BARCODE == code select line).FirstOrDefault();
                                          if (inboundLine == null)
                                          {
                                              WriteLog.GetLog().Write("找不到入库单号:"+item.INBOUNDNO);
                                              continue;
                                          }
                                          inboundLine.ABOXQTY += item.PLANQTY;
                                          if (inboundLine.ABOXQTY + inboundLine.OTHERQTY == inboundLine.BOXQTY)
                                          {
                                              inboundLine.STATUS = "30";
                                          }
                                          var inbound = (from main in dataEntity.T_WMS_INBOUND where main.INBOUNDID == inboundLine.INBOUNDID select main).FirstOrDefault();
                                          var inBoundList = (from line in dataEntity.T_WMS_INBOUND_LINE where line.INBOUNDID == inboundLine.INBOUNDID select line).ToList();
                                          var needUpdate = true;
                                          foreach (var l in inBoundList)
                                          {
                                              if (l.ABOXQTY + l.OTHERQTY != l.BOXQTY)
                                              {
                                                  needUpdate = false;
                                                  break;
                                              }
                                          }
                                          if (needUpdate)
                                          {
                                              inbound.STATUS = "30"; //入库完成
                                              inbound.ENDTIME = DateTime.Now;
                                              inbound.FINISHTIME = DateTime.Now;
                                              
                                          }
                                      }
                                      if (item.JOBTYPE == 42)//抽检入库、盘点入库、补货入库、调拨返库、其它
                                      {
                                          AtsCellOutService.UpdateObjectSec(item.TASKNO ?? 0, AtsCellInfoDetailService.GetDetail(item.SOURCE).QTY ?? 0);
                                      }
                                      AtsCellService.UpdateAtsCell(item.TARGET, 20,dataEntity);//更新cellno状态 载货

                                      var info = (from cellinfo in dataEntity.T_WMS_ATSCELLINFO where cellinfo.CELLNO == item.TARGET select cellinfo).FirstOrDefault();
                                      if (info != null)
                                      {
                                          info.STATUS = 30;
                                          info.INBOUNDTIME = DateTime.Now;
                                      }


                                  }
                                  else if (item.JOBTYPE == 50 || item.JOBTYPE == 55 || item.JOBTYPE == 52)//出库任务
                                  {
                                     
                                      if (item.JOBTYPE == 50)
                                      {
                                          AtsCellService.UpdateAtsCell(item.SOURCE, 10);//任务置空闲
                                          AtsCellInfoService.delete(item.SOURCE);
                                          AtsCellInfoDetailService.delete(item.SOURCE);
                                          InfJobDownLoadService.UpdateJopDownLoad(item.JOBID, 10, dataEntity);
                                      }
                                      else if (item.JOBTYPE == 52)
                                      {
                                          AtsCellService.UpdateAtsCell(item.SOURCE, 10);//任务置空闲
                                          AtsCellOutService.UpdateObject(item.TASKNO ?? 0, AtsCellInfoDetailService.GetDetail(item.SOURCE).QTY ?? 0);
                                          AtsCellInfoService.delete(item.SOURCE);
                                          AtsCellInfoDetailService.delete(item.SOURCE);
                                      }
                                      else if(item.JOBTYPE==55)
                                      {
                                          InfJobDownLoadService.UpdateJopDownLoad(item.JOBID, 10, dataEntity);
                                          //下达拆垛任务
                                          //var task = (from taskitem in dataEntity.INF_JOBDOWNLOAD where taskitem.EXTATTR2 == item.JOBID && taskitem.STATUS == 2 select taskitem).ToList();
                                          //if (task != null && task.Count > 0)
                                          //{
                                          //    task.ForEach(x => x.STATUS = 0);
                                          //}
                                      }
                                  }
                                  else if (item.JOBTYPE == 60 || item.JOBTYPE == 70) //拆垛任务完成
                                  {
                                      //下达返库任务
                                      INF_JOBDOWNLOAD load = InfJobDownLoadService.GetDetail(item.EXTATTR2,dataEntity);
                                      //INF_JOBDOWNLOAD load2 = InfJobDownLoadService.GetDetail(item.EXTATTR3, dataEntity);
                                    //  T_WMS_ATSCELLINFO_DETAIL detail = AtsCellInfoDetailService.GetDetail(load.SOURCE, dataEntity);
                                    //  T_WMS_ATSCELLINFO cellInfo = AtsCellInfoService.GetCellInfo(load.SOURCE,dataEntity);//检查托盘号是否存在

                                      //if (load!=null && detail!=null && load.BRANDID != detail.BARCODE)
                                      //{
                                      //    log.Write("load.BRANDID:" + load.BRANDID + ";detail.BARCODE:" + detail.BARCODE + ";item.EXTATTR2:" + item.EXTATTR2);
                                      //}
                                      //if (detail != null && load.BRANDID==detail.BARCODE)
                                      //{
                                      InfJobDownLoadService.UpdateJopDownLoad(item.JOBID, 10,dataEntity);

                                          var report = (from reportitem in dataEntity.T_WMS_STORAGEAREA_INOUT where reportitem.TASKNO == item.JOBID select reportitem).ToList();
                                          if (report != null && report.Count > 0)
                                          {
                                              report.ForEach(x => x.STATUS = 20);
                                              report.ForEach(x => x.FINISHTIME = DateTime.Now);
                                          }
                                          //if (detail.REQUESTQTY == detail.QTY) //&& (cellInfo.DISMANTLE==0)
                                          //{
                                          //    INF_JOBDOWNLOAD task1 = new INF_JOBDOWNLOAD();
                                          //    task1.JOBID = dataEntity.ExecuteStoreQuery<decimal>("select s_inf_jobdownload.nextval from dual").First() + "";
                                          //    task1.ID = task1.JOBID;
                                          //    task1.BRANDID = "111111";// detail.BARCODE;
                                          //    task1.SOURCE = item.SOURCE;
                                          //    task1.PLANQTY = 1;
                                          //    task1.INPUTTYPE = 10;
                                          //    task1.JOBTYPE = 100;//空托盘回收任务
                                          //    task1.TARGET = "1422";//空托盘指定地址
                                          //    task1.TUTYPE = 2;
                                          //    task1.PRIORITY = 50;
                                          //    task1.STATUS = 0;
                                          //    task1.CREATEDATE = DateTime.Now;
                                          //    task1.TASKNO = decimal.Parse(item.EXTATTR2);
                                          //    dataEntity.INF_JOBDOWNLOAD.AddObject(task1);

                                          //}
                                          //if (detail.REQUESTQTY != detail.QTY)
                                          //{

                                          //    INF_JOBDOWNLOAD task1 = new INF_JOBDOWNLOAD();
                                          //    task1.JOBID = dataEntity.ExecuteStoreQuery<decimal>("select s_inf_jobdownload.nextval from dual").First() + "";
                                          //    task1.ID = task1.JOBID;
                                          //    task1.BRANDID = detail.BARCODE;
                                          //    task1.SOURCE = item.SOURCE;
                                          //    task1.PLANQTY = detail.QTY - detail.REQUESTQTY;
                                          //    task1.INPUTTYPE = 10;
                                          //    task1.JOBTYPE = 40;//返库任务
                                          //    task1.TARGET = AtsCellInService.getCellNoCode(detail.BARCODE + "");
                                          //    task1.TUTYPE = 4;
                                          //    task1.PRIORITY = 50;
                                          //    task1.STATUS = 0;
                                          //    task1.CREATEDATE = DateTime.Now;
                                          //    task1.TASKNO = decimal.Parse(item.EXTATTR2);
                                          //    dataEntity.INF_JOBDOWNLOAD.AddObject(task1);

                                          //    T_WMS_ATSCELLINFO info = new T_WMS_ATSCELLINFO();
                                          //    info.PALLETNO = cellInfo.PALLETNO;
                                          //    // info.DISMANTLE = 1;
                                          //    info.CELLNO = task1.TARGET;
                                          //    info.STATUS = 10;//组盘
                                          //    info.CREATETIME = cellInfo.CREATETIME;
                                          //    // info.INBOUNDID = task1.INBOUNDNO;

                                          //    info.DISMANTLE = 10;

                                          //    AtsCellInfoService.InsertAtsCellInfo(info,dataEntity);

                                          //    T_WMS_ATSCELLINFO_DETAIL details = new T_WMS_ATSCELLINFO_DETAIL();
                                          //    details.BARCODE = task1.BRANDID + "";
                                          //    T_WMS_ITEM items = ItemService.GetItemByBarCode(details.BARCODE);
                                          //    details.CIGARETTECODE = items.ITEMNO;
                                          //    details.CIGARETTENAME = items.ITEMNAME;
                                          //    details.QTY = task1.PLANQTY;
                                          //    details.CELLNO = info.CELLNO;
                                          //    details.REQUESTQTY = 0;
                                          //    AtsCellInfoDetailService.InsertAtsCellInfo(details);
                                          //}
                                          //AtsCellService.UpdateAtsCell(detail.CELLNO, 10);//更新储位为空闲状态
                                          //AtsCellInfoService.delete(detail.CELLNO,dataEntity);
                                          //AtsCellInfoDetailService.delete(detail.CELLNO, dataEntity);
                                      //}

                                  }
                                  else if (item.JOBTYPE == 80)
                                  {
                                      var report = (from reportitem in dataEntity.T_WMS_STORAGEAREA_INOUT where reportitem.TASKNO == item.JOBID select reportitem).ToList();
                                      if (report != null && report.Count > 0)
                                      {
                                          report.ForEach(x => x.STATUS = 20);
                                          report.ForEach(x => x.FINISHTIME = DateTime.Now);
                                      }

                                      INF_JOBDOWNLOAD kxLoad = InfJobDownLoadService.GetDetail(item.JOBID, dataEntity);
                                      kxLoad.STATUS = 20;//开箱完成
                                  }
                                  else if (item.JOBTYPE == 120)
                                  {
                                      AtsCellCJService.Del(item.EXTATTR1);
                                  }
                                  dataEntity.SaveChanges();
                                  ts.Complete();
                              }
                              catch (Exception ex)
                              {
                                  WriteLog.GetLog().Write("错误信息:" + ex.Message);
                              }
                              }
                        
                      }
                  }
                
                 
           
             
          }
      }

     
    }
}
