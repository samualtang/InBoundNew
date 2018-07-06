using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Model;
namespace InBound.Business
{
  public  class MainBeltInfoService:BaseService
    {



      public static void GetSortMainBeltInfo(List<MainBeltInfo> infolist)
      {
          if (infolist != null && infolist.Count != 0)
          {

              foreach (var info in infolist)
              {
                  if (info.Quantity > 0 && info.SortNum > 0)
                  {
                      List<UnionTaskInfo> taskList = new List<UnionTaskInfo>();
                      info.taskInfo = taskList;
                      using (Entities entity = new Entities())
                      {
                          var task = (from item in entity.T_PRODUCE_POKE
                                      join item2 in entity.T_PRODUCE_SORTTROUGH
                                          on item.TROUGHNUM equals item2.TROUGHNUM
                                      where item.GROUPNO==info.GroupNO && item.SORTNUM == info.SortNum && item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20

                                      orderby item.MACHINESEQ
                                      select
                                          new TaskDetail()
                                          {
                                              CIGARETTDECODE = item2.CIGARETTECODE,
                                              CIGARETTDENAME = item2
                                                  .CIGARETTENAME,
                                              GroupNO = item.GROUPNO ?? 0,
                                              Machineseq = item.MACHINESEQ ?? 0,
                                              MainBelt = item.MAINBELT ?? 0,
                                              SortNum  = item.SORTNUM ?? 0,
                                              POKENUM = item.POKENUM ?? 0,
                                              MachineState = item.MACHINESTATE ?? 0
                                          }).ToList();
                          if (task != null)
                          {
                              decimal tempcount = 0;
                              foreach (var titem in task)
                              {
                                  if (tempcount + titem.POKENUM <= info.Quantity)
                                  {
                                      taskList.Insert(0, new UnionTaskInfo()
                                      {
                                          CIGARETTDECODE = titem.CIGARETTDECODE,
                                          CIGARETTDENAME = titem.CIGARETTDENAME,
                                          MainBelt = titem.MainBelt,
                                          SortNum = titem.SortNum,
                                          qty = titem.POKENUM,
                                          groupno = titem.GroupNO,
                                          machineseq = titem.Machineseq
                                      });
                                      tempcount += titem.POKENUM;
                                      if (tempcount == info.Quantity)
                                      {
                                          break;
                                      }
                                  }
                                  else
                                  {
                                      info.MsgCode = "-1";
                                      info.ErrorMsg = "读取数量有误,当前任务号:" + info.SortNum + " 读取数量:" + info.Quantity;
                                      
                                      break;
                                  }
                              }
                          }
                      }
                  }
              }
          }
      }

      public static void GetMainBeltInfo(List<MainBeltInfo> infolist)
      {
          if (infolist != null && infolist.Count != 0)
          {

              foreach (var info in infolist)
              {
                  if (info.Quantity > 0 && info.SortNum > 0)
                  {
                      List<UnionTaskInfo> taskList = new List<UnionTaskInfo>();
                      info.taskInfo = taskList;
                      using (Entities entity = new Entities())
                      {
                          var task = (from item in entity.T_PRODUCE_POKE
                                      join item2 in entity.T_PRODUCE_SORTTROUGH
                                          on item.TROUGHNUM equals item2.TROUGHNUM
                                      where item.SORTNUM == info.SortNum && item2.TROUGHTYPE == 10 && item2.CIGARETTETYPE == 20
                                      orderby item.MACHINESEQ
                                      select
                                          new TaskDetail()
                                          {
                                              CIGARETTDECODE = item2.CIGARETTECODE,
                                              CIGARETTDENAME = item2
                                                  .CIGARETTENAME,
                                              GroupNO = item.GROUPNO ?? 0,
                                              Machineseq = item.MACHINESEQ ?? 0,
                                              MainBelt = item.MAINBELT ?? 0,
                                              SortNum
                                                  = item.SORTNUM ?? 0,
                                              POKENUM = item.POKENUM ?? 0,
                                              MachineState = item.MACHINESTATE ?? 0
                                          }).ToList();

                          var exitLoop = false;
                          decimal uniontaskquantity = 0;
                          while (!exitLoop)
                          {
                              var isrun = false;
                              for (int i = 1; i <= 8; i++)
                              {
                                  int tempgroupno = i;
                                  if (i == 3)
                                  {
                                      tempgroupno = 4;
                                  }
                                  else if (i == 4)
                                  {
                                      tempgroupno = 3;
                                  }
                                  else if (i == 7)
                                  {
                                      tempgroupno = 8;
                                  }
                                  else if (i == 8)
                                  {
                                      tempgroupno = 7;
                                  }

                                  var temptask = task.Where(x => x.GroupNO == tempgroupno && x.MachineState != 30).OrderBy(y => y.Machineseq).ToList();
                                  if (temptask != null && temptask.Count > 0)
                                  {
                                      isrun = true;
                                     //var groupquantity=temptask.Sum(x=)
                                      decimal tempcount = 0;
                                      foreach (var titem in temptask)
                                      {
                                          if (tempcount + titem.POKENUM <= 10 && uniontaskquantity + titem.POKENUM <= info.Quantity)
                                          {
                                              taskList.Insert(0, new UnionTaskInfo()
                                              {
                                                  CIGARETTDECODE = titem.CIGARETTDECODE,
                                                  CIGARETTDENAME = titem.CIGARETTDENAME,
                                                  MainBelt = titem.MainBelt,
                                                  SortNum = titem.SortNum,
                                                  qty = titem.POKENUM,
                                                  groupno = titem.GroupNO,
                                                  machineseq = titem.Machineseq
                                              });
                                              titem.MachineState = 30;
                                              tempcount += titem.POKENUM;
                                              uniontaskquantity += titem.POKENUM;
                                              if (uniontaskquantity > info.Quantity)
                                              {
                                                  info.MsgCode = "-1";
                                                  info.ErrorMsg = "读取数量有误,当前任务号:"+info.SortNum+" 读取数量:"+info.Quantity;
                                                  exitLoop = true;
                                                  i = 9;
                                                  break;
                                              }
                                              if (uniontaskquantity == info.Quantity)
                                              {
                                                  var left=10-tempcount;
                                                  var check = task.Where(x => x.GroupNO == tempgroupno && x.MachineState != 30 && x.POKENUM<=left).OrderBy(y => y.Machineseq).ToList();
                                                  if (check != null && check.Count > 0)
                                                  {
                                                      info.MsgCode = "-1";
                                                      info.ErrorMsg = "读取数量有误,当前任务号:" + info.SortNum + " 读取数量:" + info.Quantity;
                                                  }

                                                  exitLoop = true;
                                                  i = 9;
                                                  break;
                                              }
                                          }
                                          else
                                          {
                                              if (tempcount + titem.POKENUM > 10)
                                              {
                                                  if (tempcount < 10)
                                                  {
                                                      taskList.Insert(0, new UnionTaskInfo()
                                                      {
                                                          CIGARETTDECODE = titem.CIGARETTDECODE,
                                                          CIGARETTDENAME = titem.CIGARETTDENAME,
                                                          MainBelt = titem.MainBelt,
                                                          SortNum = titem.SortNum,
                                                          qty = 10 - tempcount,
                                                          groupno = titem.GroupNO,
                                                          machineseq = titem.Machineseq
                                                      });
                                                      titem.POKENUM = titem.POKENUM - (10 - tempcount);

                                                      uniontaskquantity += (10 - tempcount);
                                                      if (uniontaskquantity > info.Quantity)
                                                      {
                                                          info.MsgCode = "-1";
                                                          info.ErrorMsg = "读取数量有误,当前任务号:" + info.SortNum + " 读取数量:" + info.Quantity;
                                                          exitLoop = true;
                                                          i = 9;
                                                          break;
                                                      }
                                                      if (uniontaskquantity == info.Quantity)
                                                      {
                                                          exitLoop = true;
                                                          i = 9;
                                                          break;
                                                      }
                                                  }
                                              }
                                              else 
                                              {
                                                  info.MsgCode = "-1";
                                                  info.ErrorMsg = "读取数量有误,当前任务号:" + info.SortNum + " 读取数量:" + info.Quantity;
                                                  exitLoop = true;
                                                  i = 9;
                                                  break;
                                              }
                                             
                                              break;
                                              
                                          }
                                      }
                                  }
                              }
                              if (!isrun)
                              {
                                  if (info.Quantity > uniontaskquantity)
                                  {
                                      info.MsgCode = "-1";
                                      info.ErrorMsg = "读取数量有误,当前任务号:" + info.SortNum + " 读取数量:" + info.Quantity;
                                      
                                  }
                                  exitLoop = true;
                              }
                          }
                      }
                  }
              }
 
          }
      }

    
    }
}
