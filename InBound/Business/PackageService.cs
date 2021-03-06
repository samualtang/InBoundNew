﻿using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using InBound;
using InBound.Model;
using InBound.Pub;
using System.Data.SqlClient;
using System.Data.Common;


namespace InBound.Business
{

    public class PackageService
    {
        //WriteLog log = WriteLog.GetLog();
        /// <summary>
        /// 获取数据 计算
        /// </summary>
        /// <param name="packageNo"></param>
        public void GetAllOrder(decimal packageNo, decimal synseq)
        {


            int allCount = 0;
            using (Entities entity = new Entities())
            {
                //System.Data.EntityClient.EntityConnection entityConnection = (System.Data.EntityClient.EntityConnection)entity.Connection;
                //entityConnection.Open();
                //System.Data.Common.DbConnection storeConnection = entityConnection.StoreConnection;
                //System.Data.Common.DbCommand cmd = storeConnection.CreateCommand();
                //cmd.CommandType = System.Data.CommandType.Text;
                //cmd.CommandText = "select ZOOMTEL.S_PACKAGE_TASK.NEXTVAL from dual";

                //var data = entity.V_PRODUCE_PACKAGEINFO
                //    .Where(x => x.EXPORT == packageNo && x.SYNSEQ == synseq)
                //    .ToList();
                var data = GetPackageInfoView(entity, packageNo, synseq);
                //所有订单明细
                var query = (from item in data
                             
                             group item by new { item.BILLCODE, item.TASKNUM } into allcode
                             select new { allcode.Key.BILLCODE, allcode.Key.TASKNUM }).OrderBy(x => x.TASKNUM).ToList();


                query1 = entity.T_WMS_ITEM.Select(x => x).ToList();

                //最大包数
                allpackagenum = entity.T_PACKAGE_TASK.Where(x => x.PACKAGENO == packageNo).Count() > 0 ? (int)entity.T_PACKAGE_TASK.Where(x => x.PACKAGENO == packageNo).Max(x => x.ALLPACKAGESEQ).Value : 0;
                //任务号
                temptask = GetPackageNum(entity, packageNo);

                List<T_WMS_ITEM> templist = ItemService.GetItemByCode();
                T_WMS_ITEM tempItem = new T_WMS_ITEM();
                if (query != null)
                {
                    int i = 0;
                    foreach (var v in query)
                    {
                        i++;
                        int pcount = 0;
                        List<T_PACKAGE_TASK> task = new List<T_PACKAGE_TASK>();
                        //当期订单明细
                        var query2 = (from item2 in entity.V_PRODUCE_PACKAGEINFO
                                      where item2.BILLCODE == v.BILLCODE && item2.EXPORT==packageNo //&& item2.ALLOWSORT == "非标"
                                      orderby item2.SENDTASKNUM, item2.MACHINESEQ, item2.TROUGHNUM, item2.SEQ
                                      select item2).ToList();
                        if (query2 != null)
                        {
                            //遍历订单数据存入集合
                            foreach (var v2 in query2)
                            {
                                allCount = allCount + 1;
                                pcount = pcount + 1;
                                T_PACKAGE_TASK temp = new T_PACKAGE_TASK();
                                temp.CIGARETTECODE = v2.CIGARETTECODE;
                                tempItem = templist.Where(x => x.ITEMNO == v2.CIGARETTECODE).FirstOrDefault();
                                temp.CIGARETTENAME = tempItem.ITEMNAME;
                                temp.CIGHIGH = tempItem.IHEIGHT;
                                if (tempItem.CDTYPE == 1)//标记为转向的品牌 长宽对换
                                {
                                    temp.CIGWIDTH = tempItem.ILENGTH + HFWidth;
                                    temp.CIGLENGTH = tempItem.IWIDTH;
                                }
                                else
                                {
                                    temp.CIGWIDTH = tempItem.IWIDTH;
                                    temp.CIGLENGTH = tempItem.ILENGTH;
                                }

                                temp.BILLCODE = v2.BILLCODE;
                                temp.SORTNUM = v2.TASKNUM;
                                temp.CIGNUM = allCount;
                                temp.CIGSEQ = pcount;
                                temp.PACKAGESEQ = 0;
                                temp.ALLPACKAGESEQ = 0;
                                temp.PACKAGENO = v2.EXPORT; ;////////////
                                temp.CIGTYPE = v2.ALLOWSORT == "非标" ? "2" : "1";
                                temp.STATE = 0;//0 新增  10 确定
                                temp.NORMAILSTATE = 0;//0 新增  10 确定
                                temp.NORMALQTY = v2.QUANTITY;
                                temp.UNIONPACKAGETAG = 0;
                                temp.DOUBLETAKE = "0";
                                temp.ORDERSEQ = v2.SORTSEQ;
                                temp.ORDERQTY = v2.ORDERQUANTITY;
                                temp.CIGSTATE = 10;
                                temp.ORDERDATE = v2.ORDERDATE;
                                temp.MIANBELT = Math.Ceiling((v2.EXPORT ?? 0) / 2);
                                temp.SYNSEQ = v2.SYNSEQ;
                                temp.REGIONCODE = v2.REGIONCODE;
                                task.Add(temp);

                            }
                            allpackagenum++;
                            //log.Write("开始计算");
                            GenPackageInfo(task, entity, query1);
                            //task.AddRange(task);
                            //log.Write("计算完成");
                            decimal orderpackageqty = task.GroupBy(x => x.PACKAGESEQ ?? 0).Count();
                            decimal tempseq = 0;//上一个包号 

                            foreach (var item in task.OrderBy(x => x.ALLPACKAGESEQ).ThenBy(x => x.CIGTYPE).ThenBy(x => x.CIGSEQ).ToList())
                            {
                                T_PACKAGE_TASK ts = new T_PACKAGE_TASK();
                                DataCopy.CopyToT(item, ts);
                                ts.PTID = PackageHelper.GetPackageID(entity); ;
                                if (tempseq != ts.ALLPACKAGESEQ)//包号与上一个不等时  任务号+1
                                {
                                    temptask = temptask + 1;
                                }
                                tempItem = templist.Where(x => x.ITEMNO == item.CIGARETTECODE).FirstOrDefault();
                                if (tempItem.CDTYPE == 1)//标记为转向的品牌 长宽重新赋值
                                {
                                    ts.CIGWIDTH = tempItem.ILENGTH;
                                    ts.CIGLENGTH = tempItem.IWIDTH;
                                }
                                else if (tempItem.CDTYPE != 1 && ts.CIGTYPE == "2")
                                {
                                    ts.CIGZ = 183;
                                }
                                ts.PACKTASKNUM = temptask;
                                ts.STATE = 10;
                                ts.NORMAILSTATE = 10;
                                ts.ORDERPACKAGEQTY = orderpackageqty;
                                ts.PACKAGEQTY = task.Where(x => x.ALLPACKAGESEQ == ts.ALLPACKAGESEQ).Sum(X => X.NORMALQTY);

                                entity.T_PACKAGE_TASK.AddObject(ts);

                                tempseq = ts.ALLPACKAGESEQ ?? 0;
                                //log.Write("entity.Add");
                            }

                            if (i == 1)
                            {
                                //log.Write("entity.SaveChanges 开始");
                                entity.SaveChanges();
                                i = 0;
                                //log.Write("entity.SaveChanges 结束");
                            }
                            task.Clear();
                            Normaldata.Clear();
                            //log.Write("数据库存储完成");
                        }
                        query2 = null;
                    }
                    entity.SaveChanges();
                }
                //entityConnection.Close();
                var date = data.Max(x => x.ORDERDATE);
                var seqtemp = entity.T_PRODUCE_SYNSEQ.Where(x => x.SYNSEQ == synseq && x.PACKAGENO == packageNo && x.ORDERDATE == date).FirstOrDefault();
                seqtemp.PMSTATE = "2";
                seqtemp.TBJSTATE = "2";
                entity.SaveChanges();
                data = null; ;
                query = null;
            }
        }
        /// <summary>
        /// 高度定值
        /// </summary>
        public int packageTHeight { get; set; }

        decimal temptask;
        List<T_WMS_ITEM> query1;
        decimal ptid;
        int packageWidth = 540;//宽
        int packageLenghth = 366; //长
        int packageCtHeight = 50;//横放烟高度限值
        int packageHeight = 140;//高


        int qzhbqty = 3;
        int jx = 4;//间隙
        int lc = 20;//长度差  不允许短烟上放置长烟
        decimal deviation = 3;//高度误差
        decimal Widthdeviation = 3;//宽度误差
        decimal HFWidth = 60;//共60  两边30
        /// <summary>
        /// 常规烟高
        /// </summary>
        decimal normalhight = 49;
        /// <summary>
        /// 常规烟宽
        /// </summary>
        decimal normalwidth = 90;
        /// <summary>
        /// 合包常规烟高度 总限高
        /// </summary>
        decimal allhight = 300;
        /// <summary>
        /// Z轴最大值
        /// </summary>
        decimal maxLength = 300;
        /// <summary>
        /// 合包常规烟总层数
        /// </summary>
        public decimal MaxnormalHight { get; set; }
        int taskCount = 6;//一次参与计算的条数
        int allpackagenum = 0;
        int NormalCount = 36;//常规烟整包条烟数
        int NorCount = 6;//一层常规烟的条数
        decimal beginy = 0;
        /// <summary>
        /// 计算包装机数据
        /// </summary>
        /// <param name="task">条烟集合</param>
        /// <param name="entity">数据库实体</param>
        public void GenPackageInfo(List<T_PACKAGE_TASK> task, Entities entity, List<T_WMS_ITEM> query1)//
        {
            diclist.Clear();//清空平面
            List<PackageArea> list = new List<PackageArea>();//平面集合
            PackageArea area = new PackageArea();//创建平面
            area.width = packageWidth;//平面宽（初始）
            area.height = 0;//平面高（初始）
            area.length = packageLenghth; ;


            //序号，最小X坐标,最大X坐标，平面宽度
            area.cigaretteList = new List<Cigarette>() { new Cigarette() { CigaretteNo = 0, fromx = 0, tox = packageWidth, width = packageWidth } };//平面集合， 算烟
            //插入初始平面
            list.Add(area);

            List<PackageArea> list1 = new List<PackageArea>(list);
            diclist.Push(list1);//插入初始平面到临时平面集合
            //log.Write("开始计算平面");
            CalcPackage(task, list, query1);


        }
        /// <summary>
        /// 重计平面
        /// </summary>
        /// <param name="list">平面集合</param>
        /// <param name="area"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="cigseq"></param>
        /// <param name="unit"></param>
        public void CalcArea(List<PackageArea> list, PackageArea area, decimal width, decimal height, decimal cigseq, AreaUnit unit, decimal length)
        {
            list.Remove(area);

            PackageArea areal = new PackageArea();
            PackageArea areaC = new PackageArea();
            PackageArea arear = new PackageArea();

            areal.left = area.left;
            areal.right = areaC;
            areal.beginx = area.beginx;
            areal.width = unit.beginx;
            areal.length = area.length;
            areal.height = area.height;
            List<Cigarette> temp = area.cigaretteList.Where(x => x.index < unit.begin).ToList();
            areal.cigaretteList = new List<Cigarette>();
            areal.cigaretteList.AddRange(temp);
            #region
            //if (area.left != null)
            //{
            //    area.left.right = areal;
            //    if (Math.Abs(area.left.height - areal.height) <= deviation)
            //    {
            //        areal.left = area.left.left;
            //        areal.beginx = area.left.beginx;
            //        areal.cigaretteList = area.left.cigaretteList;
            //        areal.cigaretteList.AddRange(areal.cigaretteList);
            //            //(new Cigarette() { CigaretteNo = cigseq, fromx = area.left.width, tox = area.left.width + width, width = width });
            //        areal.width = area.left.width + areal.width;
            //        if (areal.height < area.left.height)
            //        {
            //            areal.height = area.left.height;
            //        }
            //        list.Remove(area.left);
            //    }
            //}
            #endregion
            areaC.left = areal;
            areaC.beginx = area.beginx + unit.beginx;
            areaC.height = height;
            areaC.length = length;
            areaC.width = width;
            areaC.cigaretteList = new List<Cigarette> { new Cigarette() { CigaretteNo = cigseq, fromx = 0, tox = width, width = width } };
            areaC.right = arear;
            arear.left = areaC;
            arear.beginx = areaC.beginx + width;
            arear.width = area.width - width - unit.beginx;
            arear.height = area.height;
            arear.length = area.length;
            arear.right = area.right;
            Cigarette tempC = area.cigaretteList.Where(x => x.index == unit.begin).FirstOrDefault();
            if (tempC.width < width)
            {

                arear.cigaretteList = area.cigaretteList.Where(x => x.index > unit.begin).ToList();
                //arear.cigaretteList[0].width -= (width - tempC.width);
                if (arear.cigaretteList.Count > 0)
                {
                    arear.cigaretteList[0].width -= (width - tempC.width);
                }
            }
            else
            {
                arear.cigaretteList = area.cigaretteList.Where(x => x.index >= unit.begin).ToList();
                // arear.cigaretteList[0].width -= width;
                if (arear.cigaretteList.Count > 0)
                {
                    arear.cigaretteList[0].width -= width;
                }
            }
            //arear.cigaretteList[0].tox = arear.width;
            //arear.cigaretteList[0].width = arear.width;
            if (area.right != null)
            {
                area.right.left = arear;
            }
            list.Add(areal);
            list.Add(areaC);
            list.Add(arear);
        }
        /// <summary>
        /// 重新计算平面
        /// </summary>
        /// <param name="list"></param>
        /// <param name="area"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="cigseq"></param>
        public void calcArea(List<PackageArea> list, PackageArea area, decimal width, decimal height, decimal cigseq, decimal length)
        {
            list.Remove(area);

            PackageArea areal = new PackageArea();//左边的平面
            PackageArea arear = new PackageArea();//右边的平面

            areal.left = area.left;
            areal.right = arear;
            areal.beginx = area.beginx;//加间隙 +jx  如果平面高为0 则设间隙，否则不设
            areal.width = width;//加间隙+jx
            areal.height = height;
            areal.length = length;

            areal.cigaretteList = new List<Cigarette> { new Cigarette() { CigaretteNo = cigseq, fromx = 0, tox = width, width = width } };//
            if (area.left != null && list.Contains(area.left))
            {
                area.left.right = areal;
                if (Math.Abs(area.left.height - areal.height) <= deviation && Math.Abs(area.left.length - areal.length) <= lc)
                {


                    areal.beginx = area.left.beginx;
                    areal.cigaretteList = CopyCigaretteList(area.left.cigaretteList);
                    areal.cigaretteList.Add(new Cigarette() { CigaretteNo = cigseq, fromx = area.left.width, tox = area.left.width + width, width = width });
                    areal.width = area.left.width + areal.width;//加间隙  待加
                    if (areal.height < area.left.height)
                    {
                        areal.height = area.left.height;
                    }
                    if (areal.beginx == 0)
                    {
                        areal.left = null;
                    }
                    list.Remove(area.left);
                }
            }
            arear.left = areal;
            arear.beginx = areal.beginx + areal.width;
            arear.width = area.width - width;//+jx * 2
            arear.height = area.height;
            arear.length = area.length;
            arear.right = area.right;
            arear.cigaretteList = CopyCigaretteList(area.cigaretteList);
            //if (arear.cigaretteList.Count > 1)
            //{

            if (width > area.cigaretteList[0].width + jx * 2)
            {
                arear.cigaretteList.RemoveAt(0);
                // arear.cigaretteList[0].width -= (width - area.cigaretteList[0].width);

                if ((width - area.cigaretteList[0].width) > arear.cigaretteList[0].width)
                {
                    arear.cigaretteList.RemoveAt(0);
                    arear.cigaretteList[0].width -= (width - area.cigaretteList[0].width - area.cigaretteList[1].width);
                }
                else
                {
                    arear.cigaretteList[0].width -= (width - area.cigaretteList[0].width);
                    if (arear.cigaretteList[0].width < 0)
                    {
                        arear.cigaretteList[0].width = 0;
                    }
                }
            }
            else
            {

                arear.cigaretteList[0].width = (area.cigaretteList[0].width - width);//-间隙*2 
            }
            //}
            //else
            //{
            //    arear.cigaretteList[0].width = arear.cigaretteList[0].width - width;
            //}
            if (packageWidth - (areal.beginx + areal.width) <= minWidth)
            {

                decimal tempwidth = (packageWidth - (areal.beginx + areal.width)) - 4;
                if (tempwidth > 20)
                {
                    tempwidth = 20;
                }

                areal.cigaretteList[areal.cigaretteList.Count - 1].width += tempwidth;
                areal.cigaretteList[areal.cigaretteList.Count - 1].tox += tempwidth;
                areal.width += (packageWidth - (areal.beginx + areal.width));
                areal.right = null;
                list.Add(areal);
            }
            else
            {
                list.Add(areal);
                list.Add(arear);
            }
            //list.Add(areal);
            //list.Add(arear);
        }
        /// <summary>
        /// 临时平面集合
        /// </summary>
        public Stack<List<PackageArea>> diclist = new Stack<List<PackageArea>>();
        /// <summary>
        /// 回滚平面数据
        /// </summary>
        /// <param name="list"></param>
        /// <param name="bigList"></param>
        /// <returns></returns>
        public List<PackageArea> RollBackList(List<PackageArea> list, List<T_PACKAGE_TASK> bigList)
        {
            packageHeight = packageTHeight;
            var tempCode = "";
            var doubleTake = "0";
            decimal cdtype = 0;
            decimal cigseq = 0;
            foreach (var item in bigList)
            {
                var t = query1.Where(x => x.ITEMNO == item.CIGARETTECODE).FirstOrDefault().CDTYPE ?? 0;
                if (t == 1 && cdtype == 1 && (item.CIGSEQ - cigseq == 1)
                    )
                {
                    cigseq = item.CIGSEQ ?? 0;
                    continue;
                }
                else
                {
                    cigseq = item.CIGSEQ ?? 0;
                    cdtype = t;
                }
                //if (item.CIGARETTECODE != tempCode)
                //{

                //    list = diclist.Pop();

                //    tempCode = item.CIGARETTECODE;
                //    doubleTake = item.DOUBLETAKE;
                //}
                if (item.DOUBLETAKE != "1")
                {
                    list = diclist.Pop();
                    doubleTake = "0";
                }
                else if (item.DOUBLETAKE == "1" && doubleTake == "0")
                {
                    list = diclist.Pop();
                    doubleTake = "1";//一次双抓后重新计算
                }
                else
                {
                    doubleTake = "0";
                }

            }
            return diclist.Peek();
        }
        //最小条烟宽度
        decimal minWidth = 75;
        /// <summary>
        /// 临时卷烟集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<Cigarette> CopyCigaretteList(List<Cigarette> list)
        {
            List<Cigarette> clist = new List<Cigarette>();
            foreach (var it in list)
            {
                Cigarette ct = new Cigarette();
                ct.CigaretteNo = it.CigaretteNo;
                ct.fromx = it.fromx;
                ct.tox = it.tox;
                ct.width = it.width;
                ct.index = it.index;
                clist.Add(ct);
            }
            return clist;

        }

        public List<PackageArea> CopyList(List<PackageArea> list)
        {
            List<PackageArea> list1 = new List<PackageArea>();
            foreach (var item in list)
            {
                PackageArea t = new PackageArea();
                t.height = item.height;
                t.width = item.width;
                t.isscan = item.isscan;
                t.left = item.left;
                t.right = item.right;
                if (item.cigaretteList != null)
                {
                    List<Cigarette> clist = new List<Cigarette>();
                    foreach (var it in item.cigaretteList)
                    {
                        Cigarette ct = new Cigarette();
                        ct.CigaretteNo = it.CigaretteNo;
                        ct.fromx = it.fromx;
                        ct.tox = it.tox;
                        ct.width = it.width;
                        ct.index = it.index;
                        clist.Add(ct);
                    }
                    t.cigaretteList = clist;
                }
                list1.Add(item);
            }
            return list1;
        }

        decimal packageseq = 0;
        decimal unpackageseq = 0;
        /// <summary>
        /// 常规烟与异型烟同时分配
        /// </summary>
        /// <param name="task"></param>
        /// <param name="Normaldata"></param>
        /// <param name="tag">1正常换包，0最后一包异型烟或纯异型烟，2纯常规烟或只剩常规烟</param>
        public void NormalCig(List<T_PACKAGE_TASK> task, List<T_PACKAGE_TASK> Normaldata, int tag)
        {
            //packageseq = (Normaldata.Max(x => x.PACKAGESEQ) ?? 0) == 0 ? 1 : Normaldata.Max(x => x.PACKAGESEQ) ?? 0;
            unpackageseq = task.Max(x => x.PACKAGESEQ) ?? 1;
            packageseq = unpackageseq;
            bool reflag = false;//只有常规烟的未分配
            bool normalfalg = false;
            bool falgtag = false;
            bool first = true;//第一次进入方法标志
            bool allnormalfalg = false;

            decimal normalnum = Normaldata.Where(x => x.NORMAILSTATE != 10).Sum(x => x.NORMALQTY) ?? 0;
            decimal Remainder = 0;
            //包内顺序
            int cigseq = 1;
            //常规烟合包：有常规烟的订单
            //2.如果常规烟大于36  且除6有余数，将组30 + 余数为一包，剩余用于搭配异型烟
            //1.如果小于36 且除6有余数 直接一包 
            var datalist = task.Where(x => x.ALLPACKAGESEQ == allpackagenum && x.STATE == 10).ToList();

            //分配的常规烟数量
            decimal packagenor;
            Remainder = normalnum % 6;
            //常规烟层数大于=4层，所有异型烟小于等于限宽，常规烟包设为3层+余数，剩余常规烟分给异型烟	
            if (Math.Ceiling(normalnum / 6) >= 4 && datalist.Sum(x => x.CIGWIDTH) <= packageWidth && first)
            {
                //算出常规烟共可以多少层  将第二包分满的情况下，第二包可以分多少层
                decimal allhigh = Math.Ceiling(normalnum / NorCount);
                //如果在第五层的余数空隙的2/3放得下异型烟 跳出
                if (allhigh == 4 && ((6 - Remainder) * normalwidth) > datalist.Sum(x => x.CIGWIDTH))
                {
                    packagenor = NorCount * 5 + Remainder;
                    goto a1;
                }

                if (allhigh - 6 <= 0)
                {
                    packagenor = NorCount * 3 + Remainder;
                }
                else
                {
                    packagenor = NorCount * 5 + Remainder;
                }
                first = false;
            }
            else
            {
                if (Remainder != 0)
                {
                    packagenor = NorCount * 5 + Remainder;
                }
                else
                {
                    packagenor = NorCount * 6;
                }
            }

        a1:
            //未计算的总常规烟 条烟数
            normalnum = Normaldata.Where(x => x.NORMAILSTATE != 10).Sum(x => x.NORMALQTY) ?? 0;
            Remainder = normalnum % 6;//余数
            //如果常规烟大于36 且除6有余数   或 纯常规烟订单大于36条-没有余数？
            if ((normalnum > packagenor && Remainder != 0) || tag == 2 || reflag)
            {

            a2:
                if (tag == 2 || reflag)
                {
                    Remainder = 6;
                }
                decimal count = 0;

                foreach (var item in Normaldata.Where(x => x.NORMAILSTATE != 10).ToList())
                {
                    count += item.NORMALQTY ?? 0;
                    item.CIGSEQ = cigseq;
                    item.ALLPACKAGESEQ = allpackagenum;
                    item.PUSHSPACE = 0;
                    item.NORMAILSTATE = 10;

                    item.PACKAGESEQ = packageseq;
                    //恰好一条记录 需要分割为两条记录
                    if (count > packagenor)
                    {
                        decimal itemnum = item.NORMALQTY ?? 0;
                        decimal surpnum = Math.Abs(count - (packagenor));//一垛多出的条数
                        decimal addpnum = itemnum - surpnum;//分配的数量
                        T_PACKAGE_TASK _PACKAGE_TASK = new T_PACKAGE_TASK();

                        DataCopy.CopyToT(item, _PACKAGE_TASK);
                        _PACKAGE_TASK.NORMALQTY = addpnum;
                        item.NORMALQTY = surpnum;
                        _PACKAGE_TASK.NORMAILSTATE = 10;
                        item.NORMAILSTATE = 0;
                        task.Add(_PACKAGE_TASK);
                    }
                    else
                    {
                        task.Add(item);
                    }
                    cigseq++;
                    if (count >= packagenor)
                    {
                        cigseq = 1;
                        break;
                    }
                }
                Normaldata.RemoveAll(x => x.NORMAILSTATE == 10);
                packageseq++;
                unpackageseq = packageseq;
                first = false;
                if (task.Where(x => x.CIGTYPE == "2").Count() != 0 && Normaldata.Where(x => x.NORMAILSTATE != 10).Sum(x => x.NORMALQTY) != 0)
                {
                    allpackagenum += 1;
                }
                falgtag = true;
                normalfalg = true;
                Remainder = 0;
                //log.Write(allpackagenum + "，  单独常规烟包计算完成");
                if (tag == 2 && Normaldata.Where(x => x.NORMAILSTATE != 10).Sum(x => x.NORMALQTY) > 0)
                {
                    allpackagenum += 1;
                    goto a2;
                }
            }
            normalnum = Normaldata.Where(x => x.NORMAILSTATE != 10).Sum(x => x.NORMALQTY) ?? 0;
            //如果常规烟小于36  且（有余数 或 纯常规烟）
            if (normalnum < NormalCount && (Remainder != 0 || task.Where(x => x.CIGTYPE == "2").Count() == 0) && normalnum != 0)
            {
                if (first && task.Where(x => x.UNIONPACKAGETAG == 1).Count() != 0 && task.Where(x => x.CIGTYPE == "1").Count() != 0)
                {
                    unpackageseq++;
                    packageseq++;
                }
                //常规烟有缺时合上异型烟
                T_PACKAGE_TASK pack = task.Where(x => x.CIGTYPE == "2" && x.PACKAGESEQ == unpackageseq).OrderByDescending(x => x.CIGWIDTHX).FirstOrDefault();
                if ((packageWidth - Remainder * normalwidth) > (pack.CIGWIDTHX + pack.CIGWIDTH / 2 + jx) && normalnum < (MaxnormalHight + 1) * 6)//计算常规烟余下宽度：限宽 减去 余数*常规烟宽度后的宽度  如果大于当前异型烟包(平面最大的X轴坐标+最大条烟的一半+间隙)，放置   且常规烟的烟数小于等于5层
                {
                    //判断可以匹配几层常规烟
                    decimal hight = Math.Floor(normalnum / 6);
                    //将异型烟的层数加上，并合包
                    foreach (var it in datalist)
                    {
                        it.PUSHSPACE = hight + 2;// + 1;
                        it.ALLPACKAGESEQ = allpackagenum;
                        it.UNIONPACKAGETAG = 1;
                        it.CIGSEQ = cigseq;
                        it.PACKAGESEQ = unpackageseq;
                        cigseq++;
                    }
                    cigseq = 1;
                    foreach (var item in Normaldata.Where(x => x.NORMAILSTATE != 10).ToList())
                    {

                        item.ALLPACKAGESEQ = allpackagenum;
                        item.PUSHSPACE = hight + 2;
                        item.NORMAILSTATE = 10;
                        item.UNIONPACKAGETAG = 1;
                        item.CIGSEQ = cigseq;
                        item.PACKAGESEQ = packageseq;

                        T_PACKAGE_TASK _PACKAGE_TASK = new T_PACKAGE_TASK();

                        DataCopy.CopyToT(item, _PACKAGE_TASK);
                        task.Add(_PACKAGE_TASK);
                        cigseq++;
                    }
                    Normaldata.RemoveAll(x => x.NORMAILSTATE == 10);
                    packageseq++;
                    unpackageseq = packageseq;
                    //跳过异型烟单包判断和常规烟分配
                    first = false;
                    goto a3;
                }

                //若常规烟有余数，且层数向上取整小于等于4
                if (Remainder != 0 && Math.Ceiling(normalnum / 6) <= 4)
                {
                    //在异型烟宽度小于最高层的常规烟宽度
                    if ((pack.CIGWIDTHX + pack.CIGWIDTH / 2 + jx) < Remainder * normalwidth)
                    {
                        decimal PackHighta = Math.Floor((allhight - task.Where(x => x.ALLPACKAGESEQ == allpackagenum && x.CIGTYPE == "2" && x.STATE == 10).Max(x => x.CIGHIGHY) ?? 0) / normalhight);
                        //异型烟最大高度可补充的常规烟层数 大于等于 现有的常规烟层数 可放烟
                        if (PackHighta >= Math.Ceiling(normalnum / 6))
                        {
                            //判断可以匹配几层常规烟
                            decimal hight = Math.Ceiling(normalnum / 6);
                            decimal space = (6 - Remainder) * normalwidth;
                            //将异型烟的层数加上，并合包
                            foreach (var it in datalist)
                            {
                                it.PUSHSPACE = hight + 1;// + 1;
                                it.ALLPACKAGESEQ = allpackagenum;
                                it.UNIONPACKAGETAG = 1;
                                it.CIGSEQ = cigseq;
                                it.CIGWIDTHX += space;
                                cigseq++;
                            }
                            cigseq = 1;
                            foreach (var item in Normaldata.Where(x => x.NORMAILSTATE != 10).ToList())
                            {
                                item.ALLPACKAGESEQ = allpackagenum;
                                item.PUSHSPACE = hight + 1;
                                item.NORMAILSTATE = 10;
                                item.UNIONPACKAGETAG = 1;
                                item.CIGSEQ = cigseq;
                                item.PACKAGESEQ = packageseq;

                                T_PACKAGE_TASK _PACKAGE_TASK = new T_PACKAGE_TASK();

                                DataCopy.CopyToT(item, _PACKAGE_TASK);
                                task.Add(_PACKAGE_TASK);
                                cigseq++;
                            }
                            Normaldata.RemoveAll(x => x.NORMAILSTATE == 10);
                            //跳过异型烟单包判断和常规烟分配
                            first = false;
                            goto a3;
                        }
                    }


                }

                foreach (var item in Normaldata.Where(x => x.NORMAILSTATE != 10).ToList())
                {
                    item.ALLPACKAGESEQ = allpackagenum;
                    item.PUSHSPACE = 0;
                    item.NORMAILSTATE = 10;
                    item.PACKAGESEQ = packageseq;
                    T_PACKAGE_TASK _PACKAGE_TASK = new T_PACKAGE_TASK();

                    DataCopy.CopyToT(item, _PACKAGE_TASK);
                    task.Add(_PACKAGE_TASK);
                    Normaldata.RemoveAll(x => x.NORMAILSTATE == 10);
                }
                unpackageseq++;
                if (tag == 0)
                {
                    allpackagenum++;
                    foreach (var it in datalist)
                    {
                        it.PUSHSPACE = 1;// + 1;
                        it.ALLPACKAGESEQ = allpackagenum;
                        it.PACKAGESEQ = unpackageseq;
                        it.UNIONPACKAGETAG = 1;
                        it.CIGSEQ = cigseq;
                        cigseq++;
                    }
                    cigseq = 1;
                }
            a3: ;                //log.Write(allpackagenum + "，  该订单常规烟单一包");
            }

            //获取上一个包 最高坐标  可匹配常规烟层数  
            decimal PackHight;
            //如果上一包是异型烟且已分配常规烟后，无异型烟但还存在常规烟未分配
            if (task.Where(x => x.ALLPACKAGESEQ == allpackagenum && x.CIGTYPE == "2" && x.UNIONPACKAGETAG == 1).Count() > 0
                && task.Where(x => x.ALLPACKAGESEQ == allpackagenum && x.CIGTYPE == "2" && x.STATE == 10).Count() > 0
                && task.Where(x => x.CIGTYPE == "2" && x.STATE == 0).Count() <= 0
                && Normaldata.Where(x => x.CIGTYPE == "1" && x.NORMAILSTATE == 0).Count() > 0)
            {
                allpackagenum++;

                packagenor = 36;
                reflag = true;
                goto a1;
            }
            //如果第一包是纯常规烟包
            if (falgtag)
            {
                PackHight = Math.Floor((allhight - task.Where(x => x.ALLPACKAGESEQ == allpackagenum - 1 && x.CIGTYPE == "2" && x.STATE == 10).Max(x => x.CIGHIGHY) ?? 0) / normalhight);
                datalist = task.Where(x => x.ALLPACKAGESEQ == allpackagenum - 1 && x.STATE == 10).ToList();
            }
            else
            {
                PackHight = Math.Floor((allhight - task.Where(x => x.ALLPACKAGESEQ == allpackagenum && x.CIGTYPE == "2" && x.STATE == 10).Max(x => x.CIGHIGHY) ?? 0) / normalhight);
            }
            PackHight = PackHight > MaxnormalHight ? MaxnormalHight : PackHight;//限定匹配的常规烟层数  小于等于4

            decimal tempnum = 0;
            decimal maxnum = PackHight * 6;//可匹配常规烟 条数
            bool unnormaltag = true;
            normalnum = Normaldata.Where(x => x.NORMAILSTATE != 10).Sum(x => x.NORMALQTY) ?? 0;

            //如果异型烟可匹配的常规烟大于0 且(是异型烟包 或(常规烟除数为0且未分配的条数大于0)）
            if (PackHight > 0 && (tag == 1 || (Remainder == 0 && normalnum > 0)))
            {
                #region
                //if (normalnum < maxnum && normalnum % 6 > 0)//如果未计算的条烟数小于可配置条烟数， 且无法提供6条烟的平面
                //{
                //    //上包异型烟数据还原未计算
                //    foreach (var item in task.Where(x => x.ALLPACKAGESEQ == allpackagenum))
                //    {
                //        item.STATE = 0;
                //    }

                //    //计算初始平面
                //    list.Clear();
                //    diclist.Clear();
                //    PackageArea areainit1 = new PackageArea();
                //    areainit1.width = packageWidth - normalnum % 6 * 90;
                //    areainit1.height = normalhight;
                //    areainit1.cigaretteList = new List<Cigarette>() { new Cigarette() { CigaretteNo = 0, fromx = 0, tox = packageWidth - normalnum % 6 * 90, width = packageWidth - normalnum % 6 * 90 } };
                //    list.Add(areainit1);

                //    area = list.Find(x => x.height == normalhight && x.isscan == 0 && x.width > minWidth);
                //    goto aa;
                //}
                //else
                //{
                #endregion
                if (!allnormalfalg && task.Where(x => x.UNIONPACKAGETAG == 1).Count() != 0 && task.Where(x => x.CIGTYPE == "1").Count() != 0 && task.Where(x => x.PACKAGESEQ == 1 && x.UNIONPACKAGETAG == 0).Count() > 0)
                {
                    unpackageseq++;
                    packageseq++;
                }
                //未计算常规烟还有几层 
                decimal uncompute = Normaldata.Where(x => x.NORMAILSTATE == 0).Sum(x => x.NORMALQTY).Value;
                maxnum = uncompute > maxnum ? maxnum : uncompute;
                PackHight = uncompute > maxnum ? PackHight : uncompute / 6;
                foreach (var item in Normaldata.Where(x => x.CIGTYPE == "1" && x.NORMAILSTATE == 0).ToList())
                {
                    if (tempnum < maxnum)
                    {
                        if (unnormaltag)
                        {
                            decimal addcount = 1;
                            decimal statetag = task.Where(x => x.CIGTYPE == "2").GroupBy(x => x.STATE).Select(x => x.Key).Count();
                            decimal packagetag = task.Where(x => x.CIGTYPE == "2").GroupBy(x => new { x.ALLPACKAGESEQ, x.UNIONPACKAGETAG }).Select(x => x).Count();
                            decimal nortag = task.Where(x => x.CIGTYPE == "1" && x.NORMAILSTATE == 10).Count();
                            //是否存在纯常规烟包
                            bool norflag = task.Where(x => x.NORMAILSTATE == 10 || x.STATE == 10).GroupBy(x => new { x.ALLPACKAGESEQ, x.CIGTYPE }).Count() == 1 ? false : true;
                            //（如果常规烟的所有条数等于这次的合包常规烟数 且异型烟是第一包）或 共一包 或常规烟都还没有分配
                            if ((normalnum == maxnum && datalist.Max(x => x.PACKAGESEQ == 1)) || (statetag == 1 && packagetag == 1) || nortag == 0 || norflag)
                            {
                                //if (!normalfalg && task.Where(x=>x.ALLPACKAGESEQ == allpackagenum && x.UNIONPACKAGETAG == 0).Count()==0)//重新调用合包 且不是异型烟最后一包
                                //{
                                //    addcount = 0;
                                //}
                                //else 
                                if (datalist.Where(x => x.UNIONPACKAGETAG == 1).Max(x => x.PACKAGESEQ) != 1 && task.Where(x => x.CIGTYPE == "1").Count() > 0)//如果异型烟不是第一包
                                {
                                    addcount = 1;
                                    if (!normalfalg)
                                    { addcount = 0; }
                                }
                                else //if (datalist.Max(x => x.PACKAGESEQ == 1))//如果异型烟不是第一包
                                {
                                    addcount = 0;
                                }
                            }
                            foreach (var it in datalist)
                            {
                                it.PUSHSPACE = PackHight + 1;// + 1;
                                it.ALLPACKAGESEQ = allpackagenum;
                                it.PACKAGESEQ = unpackageseq;
                                it.UNIONPACKAGETAG = 1;
                                it.CIGSEQ = cigseq;
                                cigseq++;
                            }
                            cigseq = 1;
                            unnormaltag = false;
                        }
                        tempnum += item.NORMALQTY ?? 0;//5

                        item.CIGSEQ = cigseq;
                        item.ALLPACKAGESEQ = allpackagenum;
                        item.PUSHSPACE = PackHight + 1;// + 1;
                        item.NORMAILSTATE = 10;
                        item.UNIONPACKAGETAG = 1;
                        item.PACKAGESEQ = packageseq;
                        cigseq++;
                        if (tempnum > maxnum)
                        {
                            decimal itemnum = item.NORMALQTY ?? 0;
                            decimal surpnum = Convert.ToDecimal(item.NORMALQTY) - Math.Abs(tempnum - maxnum);//多出的条数
                            decimal addpnum = itemnum - surpnum;//分配的数量
                            T_PACKAGE_TASK _PACKAGE_TASK = new T_PACKAGE_TASK();

                            DataCopy.CopyToT(item, _PACKAGE_TASK);
                            _PACKAGE_TASK.NORMALQTY = surpnum;
                            item.NORMALQTY = addpnum;
                            _PACKAGE_TASK.NORMAILSTATE = 10;
                            item.NORMAILSTATE = 0;
                            task.Add(_PACKAGE_TASK);
                        }
                        else
                        {
                            task.Add(item);
                        }
                    }
                    else
                    {
                        cigseq = 1;
                        break;
                    }
                }
                Normaldata.RemoveAll(x => x.NORMAILSTATE == 10);
                packageseq++;
                unpackageseq = packageseq;
                first = false;
                //log.Write(allpackagenum + "，  常规烟合包");

            }
            if (tag == 0)//异型烟只有一包 或没有异型烟？
            {
                if (first)
                {
                    unpackageseq++;
                    packageseq++;
                }
                //常规烟未分配的不止一包
                if (Normaldata.Where(x => x.NORMAILSTATE == 0).Sum(x => x.NORMALQTY) > 0)
                {
                    if (Normaldata.Where(x => x.NORMAILSTATE == 0).Sum(x => x.NORMALQTY) <= 36)
                    {
                        tag = 1;
                        falgtag = true;
                    }
                    goto a1;
                }
                if (tag == 0 && Normaldata.Where(x => x.NORMAILSTATE == 0).Count() > 0)
                //&& task.Where(x => x.CIGTYPE == "2" && x.STATE == 0).Count() > 0
                //是最后一包异型烟包 且 存在常规烟未分配完成 
                {
                    if (Normaldata.Where(x => x.NORMAILSTATE == 0).Sum(x => x.NORMALQTY) <= 36)//如果有常规烟 且小于36条
                    {
                        allpackagenum++;
                    }
                    foreach (var it in datalist)
                    {
                        it.PUSHSPACE = 1;
                        it.ALLPACKAGESEQ = allpackagenum;
                        it.PACKAGESEQ = unpackageseq;
                        it.UNIONPACKAGETAG = 1;
                        it.CIGSEQ = cigseq;
                        cigseq++;
                    }
                    unpackageseq++;
                    first = false;
                }

            }

        }
        /// <summary>
        /// 常规烟临时集合  分配完成后移除
        /// </summary>
        List<T_PACKAGE_TASK> Normaldata = new List<T_PACKAGE_TASK>();
        /// <summary>
        /// 常规烟分配的数据集合
        /// </summary>
        List<T_PACKAGE_TASK> task = new List<T_PACKAGE_TASK>();
        /// <summary>
        /// 计算平面
        /// </summary>
        /// <param name="task">条烟集合</param>
        /// <param name="list">平面集合</param>
        public void CalcPackage(List<T_PACKAGE_TASK> task, List<PackageArea> list, List<T_WMS_ITEM> query1)
        {
            foreach (var item in task.Where(x => x.CIGTYPE == "1").ToList())
            {
                T_PACKAGE_TASK tk = new T_PACKAGE_TASK();
                DataCopy.CopyToT(item, tk);
                Normaldata.Add(tk);
                tk = null;
            }
            task.RemoveAll(x => x.CIGTYPE == "1");
            packageHeight = packageTHeight;
            var unnormaltask = task.Where(x => x.CIGTYPE == "2").ToList();
            decimal startpackagenum = allpackagenum;

            int packageNO = 1;
            var templist = unnormaltask.Where(x => x.STATE == 0).ToList().Take(taskCount).ToList();  //为0的未计算数据 暂每次取6条


            if (templist != null && templist.Count > 0)
            {
                //不为空 
                while (templist.Where(x => x.STATE != 10).Count() > 0)
                {
                    // templist = templist.Where(x => x.STATE != 10).ToList();
                    decimal minHeight = 0;
                    PackageArea area;
                    //平面集合内未标记删除且大于75最宽度的平面，且数量大于0
                    if (list.Where(x => x.isscan == 0 && x.width > minWidth).Count() > 0)
                    {
                        //最小高度 = 标记未删除的最低平面高度
                        minHeight = list.Where(x => x.isscan == 0 && x.width > minWidth).Min(x => x.height);
                    }
                    else
                    {

                        decimal sciseq = templist.Where(x => x.STATE != 10).Min(x => x.CIGSEQ) ?? 0;

                        List<T_PACKAGE_TASK> bigList = templist.Where(x => x.STATE == 10 && x.CIGSEQ > sciseq).OrderBy(x => x.CIGSEQ).ToList();//有大于当前序号已排好的烟
                        if (bigList != null && bigList.Count > 0)
                        {
                            decimal maxallpackageseq = templist.Max(x => x.ALLPACKAGESEQ).Value;
                            bigList = templist.Where(x => x.STATE == 10 && x.ALLPACKAGESEQ == maxallpackageseq).OrderBy(x => x.CIGSEQ).ToList();

                            list = RollBackList(list, bigList);
                            list = CopyList(list);
                            list.ForEach(x => x.isscan = 0);

                            templist = templist.Where(x => x.PACKAGESEQ == packageNO || x.PACKAGESEQ == 0).ToList();
                            templist.ForEach(x => { x.STATE = 0; x.DOUBLETAKE = "0"; });
                            templist.Where(x => x.CIGSEQ > sciseq).ToList().ForEach(x => x.PACKAGESEQ = 0);

                            templist = templist.Where(x => x.CIGSEQ <= sciseq && (x.PACKAGESEQ == packageNO || x.PACKAGESEQ == 0)).ToList();
                            templist.ForEach(x => { x.PACKAGESEQ = 0; });
                            minHeight = list.Where(x => x.isscan == 0 && x.width > minWidth).Min(x => x.height);
                            //List<PackageArea> list1 = new List<PackageArea>(list);
                            //diclist.Push(list1);
                        }
                        else//换包
                        {
                            packageHeight = packageTHeight;
                            DiversionCoordinates(task.Where(x => x.ALLPACKAGESEQ == allpackagenum && x.STATE == 10).ToList());
                            //if (Normaldata.Where(x => x.NORMAILSTATE == 0).Count() > 0)//存在常规烟未分配
                            //{
                            //    //log.Write("计算常规烟开始");
                            //    NormalCig(task, Normaldata, 1);
                            //    //log.Write("计算常规烟完成");
                            //}
                            //else//常规烟都分配完成了，只剩纯异型烟
                            //{
                            //    int cigseq = 1;
                            //    var datalist = task.Where(x => x.ALLPACKAGESEQ == allpackagenum && x.STATE == 10).ToList();
                            //    //如果订单内有常规烟且不是第一包的纯异型烟 且没有纯常规烟包
                            //    //var packageseq = (Normaldata.Where(x => x.NORMAILSTATE == 0).Count() == 0 && datalist.Select(x => x.PACKAGESEQ).FirstOrDefault() != 1
                            //    //    &&  task.Where(x => x.NORMAILSTATE == 10 || x.STATE == 10).GroupBy(x => new { x.ALLPACKAGESEQ, x.CIGTYPE }).Count() == 1 ) ?
                            //    //    datalist.Max(x => x.PACKAGESEQ) + 1 : datalist.Max(x => x.PACKAGESEQ);
                            //    var packageseq = datalist.Max(x => x.PACKAGESEQ) + 1;
                            //    foreach (var item in datalist)
                            //    {
                            //        item.CIGSEQ = cigseq;
                            //        item.PUSHSPACE = 1;
                            //        item.UNIONPACKAGETAG = 1;
                            //        cigseq++;
                            //        item.PACKAGESEQ = packageseq;
                            //    }
                            //}



                            //初始化异型烟包平面 
                            packageNO += 1;
                            allpackagenum += 1;
                            list.Clear();
                            diclist.Clear();
                            PackageArea areainit = new PackageArea();
                            areainit.width = packageWidth;
                            areainit.height = 0;
                            areainit.length = packageLenghth;
                            areainit.cigaretteList = new List<Cigarette>() { new Cigarette() { CigaretteNo = 0, fromx = 0, tox = packageWidth, width = packageWidth } };
                            list.Add(areainit);
                            diclist.Push(CopyList(list));
                            //log.Write("一包计算完成，开始下一包");
                        }

                    }
                    //找到最底平面
                    area = list.Find(x => x.height == minHeight && x.isscan == 0 && x.width > minWidth);
                    area = list.FindAll(x => x.beginx == area.beginx && x.isscan == 0 && x.width > minWidth).OrderByDescending(x => x.height).FirstOrDefault();
                    //是否有连续的相同品牌的烟存入集合（即获取双抓数据） 原*   --没有判断不同品牌？
                    List<ItemGroup> allGroupList = templist.Where(x => x.STATE != 10).GroupBy(x => x.CIGARETTECODE).Select(x => new ItemGroup() { CigaretteCode = x.Key, Total = x.Count() }).ToList();

                    #region  双抓 单抓判断
                    List<ItemGroup1> allGroupList1 = null;
                    //从数据库获取烟的信息
                    allGroupList1 = (from item in templist
                                     join item2 in query1 on item.CIGARETTECODE equals item2.ITEMNO
                                     where item.STATE != 10
                                     select new ItemGroup1 { Cigindex = item.PACKAGESEQ ?? 0, CigaretteCode = item.CIGARETTECODE, Total = item.NORMALQTY ?? 0, Length = item2.ILENGTH ?? 0, Width = item2.IWIDTH ?? 0, Hight = item2.IHEIGHT ?? 0, CDTYPE = item2.CDTYPE ?? 0, CigaretteSeq = item.CIGSEQ ?? 0, DoubleTake = item2.DOUBLETAKE }).ToList();
                    //2:遍历集合，连续同高度、且数据库内标记双抓的烟 加入集合 
                    int Indexfag = 1;//双抓组序号
                    int cigindex = 0;//条烟顺序（6条中）
                    decimal LastHight = 0;
                    decimal LastWidth = 0;
                    string LastDoubletask = "0";
                    decimal LastSeq = 0;
                    List<ItemGroups> allGroupLists = null;
                    allGroupLists = new List<ItemGroups>();
                    List<ItemGroup1> itemGroups = new List<ItemGroup1>();//条烟集合
                    List<ItemGroup1> itemGroupSave = new List<ItemGroup1>();
                    foreach (var item in allGroupList1)//遍历组合双抓
                    {
                        if (item.CDTYPE == 1)//标记为转向的品牌 长宽对换
                        {
                            item.Width = item.Length + HFWidth;
                            item.Length = item.Width;
                        }
                        else
                        {
                            item.Width = item.Width;
                            item.Length = item.Length;
                        }
                        if ((Math.Abs(item.Hight - LastHight) < deviation || LastHight == 0) && LastDoubletask == "1" && item.DoubleTake == "1" && Math.Abs(LastWidth - item.Width) <= Widthdeviation && Math.Abs(LastSeq - item.CigaretteSeq) == 1)//如果当前条烟与上条烟 高度相差在偏差范围内且能双抓   或是第一条烟  暂时宽度要求相等
                        {
                            cigindex += 1;
                            item.Cigindex = cigindex;
                            itemGroups.Add(item);
                            LastHight = item.Hight;
                            LastWidth = item.Width;
                            LastSeq = item.CigaretteSeq;
                            LastDoubletask = item.DoubleTake;
                        }
                        else
                        {
                            if (LastHight == 0 && LastWidth == 0)
                            {
                                cigindex += 1;
                                item.Cigindex = cigindex;


                                Indexfag++;
                                itemGroups.Add(item);
                                LastHight = item.Hight;
                                LastWidth = item.Width;
                                LastSeq = item.CigaretteSeq;
                                LastDoubletask = item.DoubleTake;
                            }
                            else
                            {
                                ItemGroups itemGroup = new ItemGroups();
                                itemGroupSave = itemGroups;
                                itemGroup.CigaretteNo = Indexfag;
                                itemGroup.Cigarette = itemGroupSave;
                                allGroupLists.Add(itemGroup);
                                itemGroups = new List<ItemGroup1>();
                                itemGroupSave = null;

                                Indexfag++;
                                itemGroups.Add(item);
                                LastHight = item.Hight;
                                LastWidth = item.Width;
                                LastSeq = item.CigaretteSeq;
                                LastDoubletask = item.DoubleTake;
                            }
                        }
                        if (item == allGroupList1.LastOrDefault())//若是最后一条
                        {
                            ItemGroups itemGroup = new ItemGroups();
                            itemGroupSave = itemGroups;
                            itemGroup.CigaretteNo = Indexfag;
                            itemGroup.Cigarette = itemGroupSave;
                            allGroupLists.Add(itemGroup);
                        }

                    }

                    #endregion

                    //存入6条烟中 数量大于1的条烟品牌和数量记录 原*
                    List<ItemGroup> groupList = allGroupList.FindAll(x => x.Total > 1);

                    //读取能双抓的条烟数据
                    var groupsList1 = allGroupLists.FindAll(x => x.Cigarette.Count > 1).Select(x => x.Cigarette).ToList();
                    //读取能单抓的条烟数据
                    var groupsList2 = allGroupLists.FindAll(x => x.Cigarette.Count == 1).Select(x => x.Cigarette).ToList();
                    //如果单抓的集合还剩一条，还有异型烟烟未分配，往后取一条看是否能组成双抓
                    if (groupsList1.Count == 0 && groupsList2.Count > 1 && task.Where(x => x.STATE == 0 && x.CIGTYPE == "2").Count() > groupsList2.Count())
                    {
                        decimal maxcigseq = 0;
                        foreach (var item in groupsList2)
                        {
                            maxcigseq = item[0].CigaretteSeq > maxcigseq ? item[0].CigaretteSeq : maxcigseq;
                        }
                        var lastdata = task.Where(x => x.CIGSEQ == maxcigseq).FirstOrDefault();
                        var nextdata = task.Where(x => x.CIGSEQ == maxcigseq + 1).FirstOrDefault();
                        var doubletakelast = query1.Where(x => x.ITEMNO == lastdata.CIGARETTECODE).Select(x => x.DOUBLETAKE).FirstOrDefault();
                        var doubletakenext = query1.Where(x => x.ITEMNO == nextdata.CIGARETTECODE).Select(x => x.DOUBLETAKE).FirstOrDefault();
                        List<ItemGroup1> l1 = new List<ItemGroup1>();
                        //如果接下来的一条烟可以与上一条双抓
                        if ((Math.Abs((nextdata.CIGHIGH ?? 0) - (lastdata.CIGHIGH ?? 0)) < deviation) && (doubletakelast == "1" && doubletakenext == "1") && Math.Abs((nextdata.CIGWIDTH ?? 0) - (lastdata.CIGWIDTH ?? 0)) <= Widthdeviation)//如果当前条烟与上条烟 高度相差在偏差范围内且能双抓  
                        {

                            ItemGroup1 itemGroup1 = new ItemGroup1();
                            itemGroup1.CigaretteCode = lastdata.CIGARETTECODE;
                            itemGroup1.CigaretteSeq = lastdata.CIGSEQ ?? 0;
                            itemGroup1.Cigindex = 1;
                            itemGroup1.DoubleTake = doubletakelast;
                            itemGroup1.Hight = lastdata.CIGHIGH ?? 0;
                            itemGroup1.Length = lastdata.CIGLENGTH ?? 0;
                            itemGroup1.Total = 1;
                            itemGroup1.Width = lastdata.CIGWIDTH ?? 0;
                            l1.Add(itemGroup1);
                            ItemGroup1 itemGroup2 = new ItemGroup1();
                            itemGroup2.CigaretteCode = nextdata.CIGARETTECODE;
                            itemGroup2.CigaretteSeq = nextdata.CIGSEQ ?? 0;
                            itemGroup2.Cigindex = 2;
                            itemGroup2.DoubleTake = doubletakenext;
                            itemGroup2.Hight = nextdata.CIGHIGH ?? 0;
                            itemGroup2.Length = nextdata.CIGLENGTH ?? 0;
                            itemGroup2.Total = 1;
                            itemGroup2.Width = nextdata.CIGWIDTH ?? 0;
                            l1.Add(itemGroup2);
                            groupsList1.Add(l1);
                            groupsList2.Remove(groupsList2.Where(x => x[0].CigaretteSeq == lastdata.CIGSEQ).FirstOrDefault());
                            templist.Add(nextdata);
                        }
                    }

                    decimal[] tempcode = new decimal[2];
                    decimal tempWidth = 0;
                    decimal gdc = 0;//高度差
                    List<AreaUnit> unit = new List<AreaUnit>();//双抓平面
                    AreaUnit tempunit = null;
                    if (groupsList1 != null && groupsList1.Count > 0)//优先双抓 
                    {
                        foreach (var v in groupsList1)
                        {
                            unit.Clear();
                            T_PACKAGE_TASK tempunnormaltask1 = templist.Find(x => x.CIGARETTECODE == v[0].CigaretteCode);//在 所取得的6条烟中，找到第一条烟
                            T_PACKAGE_TASK tempunnormaltask2 = templist.Find(x => x.CIGARETTECODE == v[1].CigaretteCode);
                            decimal cgiseq1 = templist.Where(x => x.CIGARETTECODE == v[0].CigaretteCode && x.STATE != 10).FirstOrDefault().CIGSEQ ?? 0;//获取条烟序号 该品牌的第一条烟
                            decimal cgiseq2 = templist.Where(x => x.CIGARETTECODE == v[1].CigaretteCode && x.STATE != 10).FirstOrDefault().CIGSEQ ?? 0;//获取条烟序号 该品牌的第一条烟

                            if ((tempunnormaltask1.CIGWIDTH + tempunnormaltask2.CIGWIDTH + jx * 2) <= area.width && area.height + tempunnormaltask1.CIGHIGH < packageHeight && (tempunnormaltask1.CIGLENGTH < area.length || tempunnormaltask1.CIGLENGTH - area.length < lc))//双抓小于最低平面宽度,同时小于整包高度  +2个间隙   且烟长度小于底平面或 比底平面宽度差小于90
                            {
                                int i = 0;

                                decimal flag = 1;
                                decimal lastflag = 0;
                                decimal beginx = 0;
                                foreach (var item in area.cigaretteList)//遍历平面得卷烟集合，条烟不能放在序号比当前大得条烟上
                                {
                                    item.index = i;
                                    if (cgiseq1 < item.CigaretteNo || cgiseq2 < item.CigaretteNo)
                                    {
                                        flag = 0;//如果大，标记不可放
                                    }
                                    else
                                    {
                                        flag = 1;
                                    }
                                    if (lastflag == 1 && flag == 1)//若上一个卷烟平面和当前找到的平面上都可放
                                    {

                                        AreaUnit u = unit.ElementAt(unit.Count - 1);
                                        u.width += item.width;
                                        u.end = i;


                                    }
                                    else if (lastflag == 0 && flag == 1)//若上一个卷烟平面不可放，但当前找到的平面上可放
                                    {
                                        AreaUnit cell = new AreaUnit();
                                        cell.width = item.width;
                                        cell.begin = i;
                                        cell.end = i;
                                        cell.beginx = beginx;
                                        unit.Add(cell);
                                    }

                                    lastflag = flag;

                                    beginx += item.width;//平面起始=原X+宽度

                                    i++;
                                }
                                foreach (var cell in unit)
                                {
                                    if ((tempunnormaltask1.CIGWIDTH + tempunnormaltask2.CIGWIDTH + jx * 2) <= cell.width)
                                    {
                                        if (tempWidth <= tempunnormaltask1.CIGWIDTH)//
                                        {
                                            if (tempWidth == tempunnormaltask1.CIGWIDTH)//
                                            {

                                                if (area.left != null)
                                                {


                                                    //看左边高度差 取相差小的
                                                    if (Math.Abs(area.height + (tempunnormaltask1.CIGHIGH ?? 0) - area.left.height) - Math.Abs(gdc) < 0)
                                                    {
                                                        tempWidth = tempunnormaltask1.CIGWIDTH ?? 0;
                                                        tempcode[0] = v[0].CigaretteSeq;
                                                        tempcode[1] = v[1].CigaretteSeq;
                                                        tempunit = cell;
                                                        gdc = area.height + (tempunnormaltask1.CIGHIGH ?? 0) - area.left.height;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                tempWidth = tempunnormaltask1.CIGWIDTH ?? 0;
                                                tempcode[0] = v[0].CigaretteSeq;
                                                tempcode[1] = v[1].CigaretteSeq;
                                                tempunit = cell;
                                                if (area.left != null)
                                                {
                                                    gdc = area.height + (tempunnormaltask1.CIGHIGH ?? 0) - area.left.height;
                                                }
                                            }
                                        }

                                        break;
                                    }
                                }

                            }
                            else
                            {
                                groupsList2.Add(v.Where(x => x.CigaretteSeq == v[0].CigaretteSeq).ToList());
                                groupsList1 = null;
                                groupsList2.Remove(v.Where(x => x.CigaretteSeq >= v[1].CigaretteSeq).ToList());

                            }
                        }
                    }
                    if (tempcode[0] + tempcode[1] > 0 && tempunit != null)
                    {
                        var chooseItem1 = templist.FindAll(x => x.CIGSEQ == tempcode[0] || x.CIGSEQ == tempcode[1]).ToList();//找到标记双抓的两条烟序号

                        decimal width = 0;
                        decimal height = 0;
                        decimal length = 0;
                        decimal cigseq = 0;
                        decimal tmphight = 0;

                        foreach (var v in chooseItem1)
                        {
                            tmphight = (v.CIGHIGH ?? 0) > tmphight ? (v.CIGHIGH ?? 0) : tmphight;//双抓取两条烟最高的
                            v.PACKAGESEQ = packageNO;
                            v.CIGWIDTHX = area.beginx + tempunit.beginx + chooseItem1[0].CIGWIDTH + jx;//两条当做一条 +jx
                            v.CIGHIGHY = area.height + tmphight;
                            v.STATE = 10;
                            v.DOUBLETAKE = "1";
                            v.ALLPACKAGESEQ = allpackagenum;
                            width += (v.CIGWIDTH ?? 0) + jx;//+jx
                            length = v.CIGLENGTH ?? 0;
                            height = area.height + tmphight;
                            cigseq = v.CIGSEQ ?? 0;
                        }
                        //更新area
                        if (tempunit.begin == 0)
                        {
                            calcArea(list, area, width, height, cigseq, length);
                        }
                        else
                        {
                            CalcArea(list, area, width, height, cigseq, tempunit, length);
                        }

                        diclist.Push(CopyList(list));
                    }
                    else
                    {

                        tempWidth = 0;
                        gdc = 0;//高度差
                        unit.Clear();
                        foreach (var v in groupsList2)//循环6条烟
                        {
                            T_PACKAGE_TASK tempunnormaltask = templist.Find(x => x.CIGSEQ == v[0].CigaretteSeq && x.STATE != 10);
                            int i = 0;
                            unit.Clear();
                            decimal flag = 1;
                            decimal lastflag = 0;
                            decimal beginx = 0;

                            foreach (var item in area.cigaretteList)//平面上的每个子平面
                            {
                                item.index = i;
                                if (tempunnormaltask.CIGSEQ < item.CigaretteNo)//如果当前条烟的序号小于平面的条烟序号 不可放
                                {
                                    flag = 0;
                                }
                                else
                                {
                                    flag = 1;
                                }
                                if (lastflag == 1 && flag == 1)//上一条烟的序号和当前条烟的序号 都大于当前条烟序号
                                {

                                    AreaUnit u = unit.ElementAt(unit.Count - 1);
                                    u.width += item.width;
                                    u.end = i;


                                }
                                else if (lastflag == 0 && flag == 1)//如果上条烟序号小于当前平面条烟序号  新增初始平面
                                {
                                    AreaUnit cell = new AreaUnit();
                                    cell.width = item.width;
                                    cell.begin = i;
                                    cell.end = i;
                                    cell.beginx = beginx;
                                    unit.Add(cell);
                                }

                                lastflag = flag;

                                beginx += item.width;

                                i++;
                            }

                            if (tempunnormaltask.CIGWIDTH + jx * 2 <= area.width && area.height + tempunnormaltask.CIGHIGH < packageHeight && (tempunnormaltask.CIGLENGTH < area.length || tempunnormaltask.CIGLENGTH - area.length < lc))
                            {
                                foreach (var cell in unit)
                                {
                                    if (tempunnormaltask.CIGWIDTH + jx * 2 <= cell.width)
                                    {

                                        if (tempWidth <= tempunnormaltask.CIGWIDTH + jx * 2)
                                        {
                                            if (tempWidth == tempunnormaltask.CIGWIDTH + jx * 2)
                                            {

                                                if (area.left != null)
                                                {


                                                    //看左边高度差 取相差小的
                                                    if (Math.Abs(area.height + (tempunnormaltask.CIGHIGH ?? 0) - area.left.height) - Math.Abs(gdc) < 0)
                                                    {
                                                        tempWidth = (tempunnormaltask.CIGWIDTH ?? 0) + jx * 2;

                                                        tempcode[0] = v[0].CigaretteSeq;
                                                        tempunit = cell;
                                                        gdc = area.height + (tempunnormaltask.CIGHIGH ?? 0) - area.left.height;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                tempWidth = (tempunnormaltask.CIGWIDTH ?? 0) + jx * 2;

                                                tempcode[0] = v[0].CigaretteSeq;
                                                tempunit = cell;
                                                if (area.left != null)
                                                {
                                                    gdc = area.height + (tempunnormaltask.CIGHIGH ?? 0) - area.left.height;
                                                }
                                            }

                                        }


                                        break;
                                    }
                                }
                            }



                        }
                        if (tempcode[0] > 0 && tempunit != null)//切换平面后新增平面
                        {
                            var chooseItem = templist.FindAll(x => x.CIGSEQ == tempcode[0] && x.STATE != 10).OrderBy(x => x.CIGSEQ).FirstOrDefault();
                            decimal width = 0;
                            decimal height = 0;
                            decimal length = 0;
                            decimal cigseq = 0;


                            chooseItem.PACKAGESEQ = packageNO;
                            chooseItem.CIGWIDTHX = area.beginx + tempunit.beginx + chooseItem.CIGWIDTH / 2 + jx;


                            chooseItem.CIGHIGHY = area.height + chooseItem.CIGHIGH;
                            chooseItem.STATE = 10;
                            chooseItem.ALLPACKAGESEQ = allpackagenum;
                            width += (chooseItem.CIGWIDTH ?? 0) + jx * 2;
                            height = area.height + (chooseItem.CIGHIGH ?? 0);
                            cigseq = chooseItem.CIGSEQ ?? 0;
                            length = chooseItem.CIGLENGTH ?? 0;

                            T_WMS_ITEM item = query1.Where(x => x.ITEMNO == chooseItem.CIGARETTECODE).FirstOrDefault();

                            var cditem = (from d in unnormaltask
                                          join d2 in query1 on d.CIGARETTECODE equals d2.ITEMNO
                                          where d2.CDTYPE == 1 && d.PACKAGESEQ == packageNO
                                              && d.STATE == 10
                                          select d).FirstOrDefault();
                            if (cditem != null)
                            {
                                packageHeight = packageCtHeight;
                            }

                          
                            //if (item.CDTYPE == 1)
                            //{
                            //    packageHeight = packageCtHeight;
                            //    decimal cigseqN = (chooseItem.CIGSEQ ?? 0) + 1;
                            //    // decimal cigseqT =( chooseItem.CIGSEQ??0) + 2;
                            //    var chooseItem2 = unnormaltask.FindAll(x => x.CIGSEQ == cigseqN && x.STATE != 10).OrderBy(x => x.CIGSEQ).FirstOrDefault();
                            //    // var chooseItem3 = unnormaltask.FindAll(x => x.CIGSEQ == cigseqT && x.STATE != 10).OrderBy(x => x.CIGSEQ).FirstOrDefault();

                            //    if (chooseItem2 != null)
                            //    {

                            //        item = query1.Where(x => x.ITEMNO == chooseItem2.CIGARETTECODE).FirstOrDefault();
                            //        if (item.CDTYPE == 1)
                            //        {
                            //            if (!templist.Contains(chooseItem2))
                            //            {
                            //                templist.Add(chooseItem2);
                            //            }
                            //            chooseItem.CIGZ = 10 + jx + chooseItem.CIGLENGTH / 2;
                            //            chooseItem2.PACKAGESEQ = packageNO;
                            //            chooseItem2.CIGWIDTHX = area.beginx + tempunit.beginx + chooseItem.CIGWIDTH / 2 + jx;
                            //            length = chooseItem2.CIGLENGTH ?? 0;

                            //            chooseItem2.CIGHIGHY = area.height + chooseItem2.CIGHIGH;
                            //            chooseItem2.STATE = 10;
                            //            chooseItem2.ALLPACKAGESEQ = allpackagenum;
                            //            chooseItem2.CIGZ = chooseItem.CIGLENGTH + jx * 2 + chooseItem2.CIGLENGTH / 2 + jx + 10;



                            //            #region modify by tjl  取消放三条
                            //            //if (chooseItem3 != null)
                            //            //{


                            //            //    item = query1.Where(x => x.ITEMNO == chooseItem3.CIGARETTECODE).FirstOrDefault();
                            //            //    decimal maxL = 0;
                            //            //    if (area.cigaretteList != null && area.cigaretteList.Count > 0)
                            //            //    {
                            //            //        maxL = (from t in unnormaltask
                            //            //                join d in area.cigaretteList on t.CIGSEQ equals d.CigaretteNo
                            //            //                where t.CIGLENGTH>=280
                            //            //                select t).ToList().Count();
                            //            //    }
                            //            //    if (item.CDTYPE == 1 && (maxL>=2 ||maxL==0))
                            //            //    {
                            //            //        if (maxLength - chooseItem.CIGLENGTH - chooseItem2.CIGLENGTH - jx * 2 >= chooseItem3.CIGLENGTH)
                            //            //        {
                            //            //            if (!templist.Contains(chooseItem3))
                            //            //            {
                            //            //                templist.Add(chooseItem3);
                            //            //            }
                            //            //            chooseItem3.PACKAGESEQ = packageNO;
                            //            //            chooseItem3.CIGWIDTHX = area.beginx + tempunit.beginx + chooseItem.CIGWIDTH / 2 + jx * 2;


                            //            //            chooseItem3.CIGHIGHY = area.height + chooseItem3.CIGHIGH;
                            //            //            chooseItem3.STATE = 10;
                            //            //            chooseItem3.ALLPACKAGESEQ = allpackagenum;
                            //            //            chooseItem3.CIGZ = chooseItem.CIGLENGTH + jx * 2 + chooseItem2.CIGLENGTH + jx * 2 + chooseItem3.CIGLENGTH / 2 + jx;

                            //            //        }
                            //            //    }
                            //            //}
                            //            #endregion
                            //        }
                            //    }


                            //}
                           
                            //更新area
                            //更新area
                            if (tempunit.begin == 0)
                            {
                                calcArea(list, area, width, height, cigseq, length);
                            }
                            else
                            {
                                CalcArea(list, area, width, height, cigseq, tempunit, length);
                            }
                            diclist.Push(CopyList(list));//临时平面集合


                        }
                        else
                        {
                            area.isscan = 1;
                        }

                    }


                    if (templist.Where(x => x.STATE != 10) == null || templist.Where(x => x.STATE != 10).Count() == 0)
                    {
                        list.ForEach(x => x.isscan = 0);
                        templist = unnormaltask.Where(x => x.STATE != 10).ToList().Take(taskCount).ToList();
                    }


                }

            }

            DiversionCoordinates(task.Where(x => x.ALLPACKAGESEQ == allpackagenum && x.STATE == 10).ToList());


            if (Normaldata.Where(x => x.NORMAILSTATE == 0).Count() > 0)
            {
                //log.Write("计算常规烟开始");
                if (unnormaltask.Count > 0)//存在异型烟
                {
                    //NormalCig(task, Normaldata, 0);
                }
                else
                {
                    if (task.Where(x => x.CIGTYPE == "2").Count() == 0)//纯常规烟
                    {
                        //NormalCig(task, Normaldata, 2);
                    }
                    else
                    {
                        //NormalCig(task, Normaldata, 1);
                    }
                }
                //log.Write("计算常规烟完成");
            }
            else
            {
                int cigseq = 1;
                var datalist = task.Where(x => x.ALLPACKAGESEQ == allpackagenum && x.STATE == 10).ToList();
                if (datalist.Count > 0)
                {
                    //有常规烟 已经没有未分配常规烟 且 不是第一包烟
                    //var packageseq = ( Normaldata.Count() > 0 && Normaldata.Where(x => x.NORMAILSTATE == 0).Count() == 0 && datalist.Select(x => x.PACKAGESEQ).FirstOrDefault() != 1) ? datalist.Max(x => x.PACKAGESEQ) +1: datalist.Max(x => x.PACKAGESEQ);
                    //if (Normaldata.Count() <= 0)
                    //{
                    //    packageseq = (datalist.Max(x => x.PACKAGESEQ) ?? 0) + 1;
                    //}
                    packageseq = (datalist.Max(x => x.PACKAGESEQ) ?? 0);
                    foreach (var item in datalist)
                    {
                        item.CIGSEQ = cigseq;
                        item.PUSHSPACE = 1;
                        item.UNIONPACKAGETAG = 1;
                        cigseq++;
                        item.PACKAGESEQ = packageseq;
                    }
                }
                else//既没有常规烟  也没有异型烟
                {
                    allpackagenum -= 1;
                }
            }
            NormalCig(task, Normaldata);
        }
        //反转坐标
        public void DiversionCoordinates(List<T_PACKAGE_TASK> datalist)
        {
            //if (datalist.Sum(x => x.CIGWIDTH) < packageWidth)
            //{
            //    return;
            //}
            //平面计算后，将超过异型烟工位限宽的异型烟包（即多层包）坐标反转
            //反转方式：
            //单抓的烟，工位的限宽 - 坐标位置
            //双抓的烟，
            //第一种情况 - 两条宽度相同，工位的限宽 - 第一条烟的坐标位置
            //第二种情况 - 第一条烟宽度长，将第一条烟的宽度减去两条烟宽度的一半，得出双抓的中心点偏移量，移到右边后，加上偏移量
            //第三种情况 - 第二条烟宽度长，将第二条烟的宽度减去两条烟宽度的一半，得出双抓的中心点偏移量，移到右边后，减去偏移量
            //--若是3 4 7 8包装机，不做反转坐标
            if (datalist.Where(x => x.PACKAGENO == 1 || x.PACKAGENO == 2 || x.PACKAGENO == 5 || x.PACKAGENO == 6).Count() > 0)
            {
                decimal LastX = 0;
                decimal LastCigseq = 0;
                decimal LastWidth = 0;
                foreach (var item in datalist)
                {
                    if (item.DOUBLETAKE == "0")
                    {
                        item.CIGWIDTHX = packageWidth - item.CIGWIDTHX - jx;
                        LastX = 0;
                        LastWidth = 0;
                        LastCigseq = 0;
                    }
                    else
                    {
                        if (LastX == item.CIGWIDTHX)
                        {
                            if (LastWidth == item.CIGWIDTH)
                            {
                                item.CIGWIDTHX = packageWidth - item.CIGWIDTHX - jx;
                            }
                            else if (LastWidth > item.CIGWIDTH)
                            {
                                item.CIGWIDTHX = packageWidth - item.CIGWIDTHX + (LastWidth - item.CIGWIDTH) - jx;
                            }
                            else if (LastWidth < item.CIGWIDTH)
                            {
                                item.CIGWIDTHX = packageWidth - item.CIGWIDTHX - (item.CIGWIDTH - LastWidth) - jx;
                            }
                            datalist.Where(x => x.CIGSEQ == LastCigseq).FirstOrDefault().CIGWIDTHX = item.CIGWIDTHX;
                        }
                        else
                        {
                            LastCigseq = item.CIGSEQ ?? 0;
                            LastWidth = item.CIGWIDTH ?? 0;
                            LastX = item.CIGWIDTHX ?? 0;
                        }
                    }
                }
            }
        }
        List<T_PACKAGE_TASK> _TASKS = new List<T_PACKAGE_TASK>();
        /// <summary>
        /// 异型烟包序集合
        /// </summary>
        List<decimal[]> unnormallist = new List<decimal[]>();
        List<decimal[]> normallist = new List<decimal[]>();
        /// <summary>
        /// 常规烟合包算法2，异型烟运算完成后合包
        /// </summary>
        /// <param name="task"></param>
        /// <param name="Normaldata"></param>
        public void NormalCig2(List<T_PACKAGE_TASK> task, List<T_PACKAGE_TASK> Normaldata)
        {
            decimal norpackage = 1;
            foreach (var item in task)
            {
                T_PACKAGE_TASK _task = new T_PACKAGE_TASK();
                DataCopy.CopyToT(item, _task);
                _TASKS.Add(_task);
                _task = null;
            }
            //异型烟现有的整体包序  定义每个包序号的初始常规烟数0
            foreach (var item in task.GroupBy(x => x.ALLPACKAGESEQ).Select(x => x.Key ?? 0).ToList())
            {
                decimal[] de = new decimal[2];
                de[0] = item;
                de[1] = 0;
                unnormallist.Add(de);
            }
            //常规烟数量
            decimal AllNormalQty = Normaldata.Sum(x => x.NORMALQTY) ?? 0;
            //获取常规烟总层数
            decimal AllNormalLevel = Math.Ceiling(AllNormalQty / NorCount);
            //获取常规烟余数
            decimal Remainder = AllNormalQty % NorCount;

            //异型烟包数
            decimal AllUnnormalCount = task.GroupBy(x => x.ALLPACKAGESEQ).Count();
            if (AllUnnormalCount != 0)//存在异型烟
            {
                //排序异型烟包：按包的总高度排序数量、单包高度、总条烟数升序排序
                var sortdata = task.GroupBy(x => new { x.ALLPACKAGESEQ, x.PACKAGESEQ }).Select(x => new { allpackageq = x.Key.ALLPACKAGESEQ, packageq = x.Key.PACKAGESEQ, yy = x.Max(t => t.CIGHIGHY), ww = x.Sum(t => t.CIGWIDTH), qty = x.Sum(t => t.NORMALQTY) }).OrderBy(x => x.qty).ThenBy(x => x.yy).ThenBy(x => x.ww);
                //找出只有一层的异型烟包(6条常规烟宽度*0.8)
                var oneleveldata = sortdata.Where(x => x.ww <= (normalwidth * NorCount));//* (decimal)0.8));
                //如果常规烟有余数，总层数减 
                if (Remainder > 0 && AllNormalLevel > 2)
                {
                    if (!(oneleveldata.Count() == 1 && AllNormalLevel <= 4))//如果不满一层的异型烟只有1包，且常规烟总层数小于等于4
                    {
                        AllNormalLevel -= 2;
                    }
                }
                if (AllNormalQty > 0)//存在常规烟
                {
                    decimal dddd = oneleveldata.Count();
                    foreach (var item in oneleveldata)//只有一层的异型烟包
                    {
                        if (AllNormalLevel > 0)//存在常规烟
                        {
                            if (AllNormalLevel <= 4)//小于4层 全部合包
                            {
                                var tmp = unnormallist.Where(x => x[0] == item.allpackageq).ToList();
                                tmp[0][1] = AllNormalLevel;
                                AllNormalLevel = 0;
                            }
                            else if (sortdata.Count() == 1 && AllNormalLevel > 4)
                            {
                                decimal templevel = 0;
                                if (AllNormalLevel - 4 > 2)
                                {
                                    templevel = 4;
                                }
                                else
                                {
                                    templevel = AllNormalLevel - 2 > 4 ? 4 : AllNormalLevel - 2;
                                }
                                var tmp = unnormallist.Where(x => x[0] == item.allpackageq).ToList();
                                tmp[0][1] = templevel;
                                AllNormalLevel -= templevel;
                            }
                            else if (AllNormalLevel / AllUnnormalCount >= 4)//如果异型烟平均可以匹配的常规烟层数大于2
                            {
                                var tmp = unnormallist.Where(x => x[0] == item.allpackageq).ToList();
                                tmp[0][1] = 4;
                                AllNormalLevel -= 4;
                            }
                            else if (AllNormalLevel / AllUnnormalCount >= 2)//如果异型烟平均可以匹配的常规烟层数大于2
                            {
                                var tmp = unnormallist.Where(x => x[0] == item.allpackageq).ToList();
                                tmp[0][1] = 3;
                                AllNormalLevel -= 3;
                            }
                            else if (AllNormalLevel / AllUnnormalCount >= 2)//如果异型烟平均可以匹配的常规烟层数大于2
                            {
                                var tmp = unnormallist.Where(x => x[0] == item.allpackageq).ToList();
                                tmp[0][1] = 2;
                                AllNormalLevel -= 2;
                            }
                            else
                            {
                                if (AllNormalLevel == 0)
                                {
                                    break;
                                }
                                var tmp = unnormallist.Where(x => x[0] == item.allpackageq).ToList();
                                tmp[0][1] = 1;
                                AllNormalLevel -= 1;
                            }
                        }
                        else
                        {
                            var datalist = task.Where(x => x.ALLPACKAGESEQ == item.allpackageq).ToList();
                            foreach (var it in datalist)
                            {
                                it.PUSHSPACE = 1;
                            }
                        }
                    }

                    //分配其他异型烟包
                    var moreleveldata = sortdata.Where(x => x.ww > (normalwidth * NorCount));//* (decimal)0.8));
                    if (moreleveldata.Count() > 0)
                    {
                        norpackage = Math.Ceiling(AllNormalLevel / moreleveldata.Count()) >= 2 ? 2 : 1;
                        decimal tmpAllNormalLevel = AllNormalLevel;
                        foreach (var it in moreleveldata)
                        {
                            var tmplist = unnormallist.Where(x => x[0] == it.allpackageq).ToList();
                            //如果可以每包分配2层

                            if (moreleveldata.Count() * norpackage <= AllNormalLevel)
                            {
                                tmplist[0][1] = norpackage;
                            }
                            else
                            {
                                if (tmpAllNormalLevel >= norpackage)
                                {
                                    tmplist[0][1] = norpackage;
                                    tmpAllNormalLevel -= norpackage;
                                }
                                else
                                {
                                    tmplist[0][1] = tmpAllNormalLevel;
                                    tmpAllNormalLevel = 0;
                                }
                            }

                        }
                        if (AllNormalLevel / moreleveldata.Count() >= norpackage)//如果异型烟平均可以匹配的常规烟层数大于2
                        {
                            //每包2层后剩余的常规烟层数
                            decimal surp = AllNormalLevel - norpackage * moreleveldata.Count();
                            decimal maxlevel = norpackage;
                            if (surp > 0)
                            {
                                //if (surp <= 4)
                                //{
                                //    surp = 0;
                                //}
                                if (surp > 0)
                                {
                                    //看是否存在每包2层后还存在常规烟，若存在 取最小的异型烟包 可方的最大层数，将其补充到最大
                                    foreach (var item in moreleveldata)
                                    {
                                        maxlevel = Math.Floor((allhight - (item.yy ?? 0)) / normalhight) > MaxnormalHight ? MaxnormalHight : Math.Floor((allhight - (item.yy ?? 0)) / normalhight);
                                        if (item.yy > 90)
                                        {
                                            maxlevel = maxlevel > 3 ? 3 : maxlevel;
                                        }
                                        if (surp - (maxlevel - norpackage) >= 0)
                                        {
                                            surp = surp - (maxlevel - norpackage);
                                        }
                                        else
                                        {
                                            maxlevel = surp + norpackage;
                                            surp = 0;
                                        }
                                        var tmp = unnormallist.Where(x => x[0] == item.allpackageq).ToList();
                                        tmp[0][1] = maxlevel;
                                        AllNormalLevel = surp;
                                    }
                                }
                                var it = unnormallist.OrderByDescending(x => x[1]).ToList();
                                //如果剩余常规烟的层数分包后小于2 , 从合包异型烟最多的包取一层常规烟
                                if (AllNormalLevel > 0 && (AllNormalLevel - it[0].FirstOrDefault()) % 6 < 2)
                                {
                                    foreach (var item in it)
                                    {
                                        if (item[1] > 1)
                                        {
                                            item[1] -= 1;
                                            break;
                                        }
                                    }
                                }
                            }
                            AllNormalLevel -= unnormallist.Sum(x => x[1]);
                        }
                        else//若不能平均匹配指定层数
                        {
                            foreach (var item in moreleveldata)
                            {
                                if (AllNormalLevel - norpackage < 0)
                                {
                                    if (AllNormalLevel > 0)
                                    {
                                        var tmp1 = unnormallist.Where(x => x[0] == item.allpackageq).ToList();
                                        tmp1[0][1] = AllNormalLevel;
                                    }
                                    break;
                                }
                                var tmp = unnormallist.Where(x => x[0] == item.allpackageq).ToList();
                                tmp[0][1] = norpackage;
                                AllNormalLevel -= norpackage;
                            }
                        }
                    }
                    if (Math.Ceiling(AllNormalQty / NorCount) - unnormallist.Sum(x => x[1]) > 0)
                    {
                        allpackagenum++;
                    }

                    //按上面的异型烟合包方式，分配常规烟,插入集合
                    foreach (var item in sortdata.OrderBy(x => x.allpackageq).ToList())
                    {
                        decimal cenum = unnormallist.Where(x => x[0] == item.allpackageq).Select(x => x[1]).FirstOrDefault();
                        //该包异型烟需要的常规烟条数
                        decimal nornum = cenum * NorCount;
                        decimal nownum = 0;
                        decimal cigseq = 1;
                        List<T_PACKAGE_TASK> tasklist = new List<T_PACKAGE_TASK>();
                        var datalist = task.Where(x => x.ALLPACKAGESEQ == item.allpackageq && x.STATE == 10).ToList();
                        if (nornum > 0)
                        {
                            foreach (var it in datalist)
                            {
                                it.PUSHSPACE = cenum + 1;
                                it.ALLPACKAGESEQ = item.allpackageq;
                                it.UNIONPACKAGETAG = 1;
                                it.CIGSEQ = cigseq;
                                cigseq++;
                            }
                            cigseq = 1;
                            foreach (var it in Normaldata)
                            {
                                nownum += it.NORMALQTY ?? 0;
                                it.CIGSEQ = cigseq;
                                it.ALLPACKAGESEQ = item.allpackageq;
                                it.PUSHSPACE = cenum + 1;
                                it.NORMAILSTATE = 10;
                                it.UNIONPACKAGETAG = 1;
                                it.PACKAGESEQ = item.packageq;
                                //恰好一条记录 需要分割为两条记录
                                if (nownum > nornum)
                                {
                                    decimal itemnum = it.NORMALQTY ?? 0;
                                    decimal surpnum = Math.Abs(nownum - (nornum));//一垛多出的条数
                                    decimal addpnum = itemnum - surpnum;//分配的数量
                                    T_PACKAGE_TASK _PACKAGE_TASK = new T_PACKAGE_TASK();

                                    DataCopy.CopyToT(it, _PACKAGE_TASK);
                                    _PACKAGE_TASK.NORMALQTY = addpnum;
                                    it.NORMALQTY = surpnum;
                                    _PACKAGE_TASK.NORMAILSTATE = 10;
                                    it.UNIONPACKAGETAG = 0;
                                    it.NORMAILSTATE = 0;
                                    task.Add(_PACKAGE_TASK);
                                }
                                else
                                {
                                    task.Add(it);
                                }
                                cigseq++;
                                if (nownum >= nornum)
                                {
                                    cigseq = 1;
                                    break;
                                }
                            }
                            Normaldata.RemoveAll(x => x.NORMAILSTATE == 10);
                        }
                        else//纯异型烟
                        {
                            var data1 = task.Where(x => x.STATE == 10 && x.ALLPACKAGESEQ == item.allpackageq).ToList();
                            if (data1.Count > 0)
                            {
                                foreach (var it in data1)
                                {
                                    it.PUSHSPACE = 1;
                                    it.UNIONPACKAGETAG = 1;
                                    it.CIGSEQ = cigseq;
                                    cigseq++;
                                }
                                cigseq = 1;
                            }
                        }
                    }

                    //将剩下的常规烟分配
                    while (Normaldata.Count != 0)
                    {
                        decimal allnornum = Normaldata.Sum(x => x.NORMALQTY) ?? 0;
                        decimal nornum = allnornum;
                        decimal nownum = 0;
                        decimal cigseq = 1;
                        decimal packageseq = (task.Max(x => x.PACKAGESEQ) ?? 0) + 1;
                        if (Remainder != 0 && nornum > 36)
                        {
                            nornum = 30 + Remainder;
                            Remainder = 0;
                        }
                        else if (Remainder == 0 && nornum > 36)
                        {
                            nornum = 36;
                        }
                        //计算接下来还有多少没有分配，如果等于7层，接下来5+2分配
                        if (Math.Ceiling(allnornum / 6) == 7)
                        {
                            nornum = 30;
                        }

                        foreach (var it in Normaldata)
                        {
                            nownum += it.NORMALQTY ?? 0;
                            it.CIGSEQ = cigseq;
                            it.ALLPACKAGESEQ = allpackagenum;
                            it.PUSHSPACE = 0;
                            it.NORMAILSTATE = 10;

                            it.PACKAGESEQ = packageseq;
                            //恰好一条记录 需要分割为两条记录
                            if (nownum > nornum)
                            {
                                decimal itemnum = it.NORMALQTY ?? 0;
                                decimal surpnum = Math.Abs(nownum - (nornum));//一垛多出的条数
                                decimal addpnum = itemnum - surpnum;//分配的数量
                                T_PACKAGE_TASK _PACKAGE_TASK = new T_PACKAGE_TASK();

                                DataCopy.CopyToT(it, _PACKAGE_TASK);
                                _PACKAGE_TASK.NORMALQTY = addpnum;
                                it.NORMALQTY = surpnum;
                                _PACKAGE_TASK.NORMAILSTATE = 10;
                                it.NORMAILSTATE = 0;
                                task.Add(_PACKAGE_TASK);
                            }
                            else
                            {
                                task.Add(it);
                            }
                            cigseq++;
                            if (nownum >= nornum)
                            {
                                cigseq = 1;
                                break;
                            }
                        }
                        Normaldata.RemoveAll(x => x.NORMAILSTATE == 10);
                        if (Normaldata.Count > 0)
                        {
                            allpackagenum++;
                        }
                    }
                    var ddddd = Normaldata;
                    var ddd1 = unnormallist.Sum(x => x[1]);
                    var ddd2 = unnormallist.Sum(x => x[1]);

                    decimal ddd = 1;
                }
                else//纯异型烟
                {
                    decimal packageseq = 0;
                    decimal tempallpackageseq = 0;
                    decimal cigseq = 1;
                    var datalist = task.Where(x => x.STATE == 10).ToList();
                    if (datalist.Count > 0)
                    {
                        foreach (var it in datalist)
                        {
                            if (tempallpackageseq != it.ALLPACKAGESEQ)
                            {
                                tempallpackageseq = (it.ALLPACKAGESEQ ?? 0);
                                packageseq++;
                                cigseq = 1;
                            }
                            it.CIGSEQ = cigseq;
                            it.PACKAGESEQ = packageseq;
                            it.PUSHSPACE = 1;
                            it.UNIONPACKAGETAG = 0;
                            cigseq++;
                        }
                        packageseq = 0;
                        tempallpackageseq = 0;
                    }
                }
            }
            else//纯常规烟订单
            {
                while (Normaldata.Count != 0)
                {

                    decimal allnornum = Normaldata.Sum(x => x.NORMALQTY) ?? 0;
                    decimal nornum = allnornum;
                    decimal nownum = 0;
                    decimal cigseq = 1;
                    decimal packageseq = (task.Max(x => x.PACKAGESEQ) ?? 0) + 1;
                    if (Remainder != 0 && nornum > 36)
                    {
                        nornum = 30 + Remainder;
                        Remainder = 0;
                    }
                    else if (Remainder == 0 && nornum > 36)
                    {
                        nornum = 36;
                    }
                    //计算接下来还有多少没有分配，如果等于7层，接下来5+2分配
                    if (Math.Ceiling(allnornum / 6) == 7)
                    {
                        nornum = 30;
                    }

                    foreach (var it in Normaldata)
                    {
                        nownum += it.NORMALQTY ?? 0;
                        it.CIGSEQ = cigseq;
                        it.ALLPACKAGESEQ = allpackagenum;
                        it.PUSHSPACE = 0;
                        it.NORMAILSTATE = 10;

                        it.PACKAGESEQ = packageseq;
                        //恰好一条记录 需要分割为两条记录
                        if (nownum > nornum)
                        {
                            decimal itemnum = it.NORMALQTY ?? 0;
                            decimal surpnum = Math.Abs(nownum - (nornum));//一垛多出的条数
                            decimal addpnum = itemnum - surpnum;//分配的数量
                            T_PACKAGE_TASK _PACKAGE_TASK = new T_PACKAGE_TASK();

                            DataCopy.CopyToT(it, _PACKAGE_TASK);
                            _PACKAGE_TASK.NORMALQTY = addpnum;
                            it.NORMALQTY = surpnum;
                            _PACKAGE_TASK.NORMAILSTATE = 10;
                            it.NORMAILSTATE = 0;
                            task.Add(_PACKAGE_TASK);
                        }
                        else
                        {
                            task.Add(it);
                        }
                        cigseq++;
                        if (nownum >= nornum)
                        {
                            cigseq = 1;
                            break;
                        }
                    }
                    Normaldata.RemoveAll(x => x.NORMAILSTATE == 10);
                    if (Normaldata.Count > 0)
                    {
                        allpackagenum++;
                    }
                }
                var ddddd = Normaldata;
                var ddd1 = unnormallist.Sum(x => x[1]);
                var ddd2 = unnormallist.Sum(x => x[1]);

                decimal ddd = 1;

            }
            unnormallist.Clear();
            normallist.Clear();
        }

        decimal NormalTemp = 0.8m;

        public void NormalCig(List<T_PACKAGE_TASK> task, List<T_PACKAGE_TASK> Normaldata)
        {
            foreach (var item in task)
            {
                T_PACKAGE_TASK _task = new T_PACKAGE_TASK();
                DataCopy.CopyToT(item, _task);
                _TASKS.Add(_task);
                _task = null;
            }
            //异型烟现有的整体包序  定义每个包序号的初始常规烟数0
            foreach (var item in task.GroupBy(x => x.ALLPACKAGESEQ).Select(x => x.Key ?? 0).ToList())
            {
                decimal maxcell = Math.Floor((allhight - (task.Where(x => x.ALLPACKAGESEQ == item).Max(x => x.CIGHIGHY).Value)) / normalhight);
                if (maxcell > MaxnormalHight)
                {
                    maxcell = MaxnormalHight;
                }
                decimal[] de = new decimal[4];
                de[0] = item;//包号
                de[1] = 0;//匹配层数
                de[2] = maxcell;//最大层数
                de[3] = 0;//强制和包标志
                unnormallist.Add(de);
            }
            //常规烟数量
            decimal AllNormalQty = Normaldata.Sum(x => x.NORMALQTY) ?? 0;
            //获取常规烟总层数
            decimal AllNormalLevel = Math.Ceiling(AllNormalQty / NorCount);
            //获取常规烟余数
            decimal Remainder = AllNormalQty % NorCount;

            //异型烟包数
            decimal AllUnnormalCount = task.GroupBy(x => x.ALLPACKAGESEQ).Count();
            if (AllUnnormalCount != 0)//存在异型烟
            {
                //排序异型烟包：按包的总高度排序数量、单包高度、总条烟数升序排序
                var sortdata = task.GroupBy(x => new { x.ALLPACKAGESEQ, x.PACKAGESEQ }).Select(x => new { allpackageq = x.Key.ALLPACKAGESEQ, packageq = x.Key.PACKAGESEQ, yy = x.Max(t => t.CIGHIGHY), ww = x.Sum(t => t.CIGWIDTH), qty = x.Sum(t => t.NORMALQTY), qh = 0 }).OrderBy(x => x.qty).ThenBy(x => x.yy).ThenBy(x => x.ww);
                //找出只有一层的异型烟包(6条常规烟宽度*0.8)
                //var oneleveldata = sortdata.Where(x => x.ww <= (normalwidth * NorCount) * NormalTemp).ToList();
                //获取品牌特性:1中支横放烟
                List<T_WMS_ITEM> itemlist = new List<T_WMS_ITEM>();
                using (Entities et = new Entities())
                {
                    itemlist = et.T_WMS_ITEM.Where(x => x.CDTYPE == 1).ToList();
                }
                //找出只有3条及一下的异型烟包(用于强制合包)
                var oneleveldata = sortdata.Where(x => x.qty <= qzhbqty).ToList();
                List<decimal> _ALLPACKAGESEQ = new List<decimal>();
                if (itemlist.Count > 0)
                {
                    foreach (var item in itemlist)
                    {
                        _ALLPACKAGESEQ.AddRange(task.Where(x => x.CIGARETTECODE.Contains(item.ITEMNO)).Select(x => x.ALLPACKAGESEQ ?? 0).Distinct().ToList());
                    }
                    if (_ALLPACKAGESEQ.Count > 0)
                    {
                        foreach (var item in _ALLPACKAGESEQ)
                        {
                            oneleveldata = oneleveldata.Where(x => x.allpackageq != item).ToList();
                        }
                    }
                }

                if (AllNormalQty > 0)//存在常规烟
                {
                    //异型烟可匹配的最大层数
                    decimal allFloor = unnormallist.Sum(x => x[2]);
                    //异型烟可匹配数量多
                    if (allFloor >= AllNormalLevel)
                    {
                        //如果有余数  量最少的来强制合成一包
                        if (Remainder != 0)
                        {
                            //存在只有一层的异型烟
                            if (oneleveldata != null && oneleveldata.Count > 0)
                            {
                                var tmpdatas = unnormallist.Where(x => x[0] == oneleveldata.FirstOrDefault().allpackageq).Select(x => x).FirstOrDefault();
                                tmpdatas[3] = 1;//强制合包
                                tmpdatas[1] = tmpdatas[2];
                                if (AllNormalLevel < tmpdatas[2])
                                {
                                    tmpdatas[1] = AllNormalLevel;
                                    AllNormalLevel = 0;
                                }
                                else
                                {
                                    tmpdatas[1] = tmpdatas[2];
                                    AllNormalLevel -= tmpdatas[1];
                                }
                            }
                            else
                            {
                                AllNormalLevel -= 2;//不存在一层的 不强制合成一包 另起一包
                            }
                        }
                        //按最大数量来搭配，优先搭配异型烟数量少的
                        //平均分配
                        foreach (var item in sortdata)
                        {
                            var tmpdatas = unnormallist.Where(x => x[0] == item.allpackageq).Select(x => x).FirstOrDefault();
                            if (tmpdatas[1] > 0)
                            {
                                continue;
                            }
                            if (AllNormalLevel < tmpdatas[2])
                            {
                                tmpdatas[1] = AllNormalLevel;
                                AllNormalLevel = 0;
                                break;
                            }
                            else
                            {
                                tmpdatas[1] = tmpdatas[2];
                                AllNormalLevel -= tmpdatas[1];
                            }
                        }

                    }
                    else //常规烟数量多
                    {
                        //按预定的层数分配
                        foreach (var item in sortdata)
                        {
                            var tmpdatas = unnormallist.Where(x => x[0] == item.allpackageq).Select(x => x).FirstOrDefault();
                            tmpdatas[1] = tmpdatas[2];
                            AllNormalLevel -= tmpdatas[1];
                        }
                        if (AllNormalLevel == 1)//常规烟多出1层 抽最大的合包的一层重组 
                        {
                            var tmpcgy = unnormallist.OrderByDescending(X => X[1]).FirstOrDefault();
                            tmpcgy[1] -= 1;
                        }

                    }
                    //按上面的异型烟合包方式，分配常规烟,插入集合
                    foreach (var item in sortdata.OrderBy(x => x.allpackageq).ToList())
                    {
                        var datatmpcenum = unnormallist.Where(x => x[0] == item.allpackageq).FirstOrDefault();
                        decimal cenum = datatmpcenum[1];
                        //该包异型烟需要的常规烟条数
                        decimal nornum = cenum * NorCount;

                        if (datatmpcenum[3] == 1)//强制合包
                        {
                            nornum = ((cenum - 1) * NorCount) + Remainder;
                        }
                        decimal nownum = 0;
                        decimal cigseq = 1;
                        List<T_PACKAGE_TASK> tasklist = new List<T_PACKAGE_TASK>();
                        var datalist = task.Where(x => x.ALLPACKAGESEQ == item.allpackageq && x.STATE == 10).ToList();
                        if (nornum > 0)
                        {
                            foreach (var it in datalist)
                            {
                                it.PUSHSPACE = cenum + 1;
                                it.ALLPACKAGESEQ = item.allpackageq;
                                it.UNIONPACKAGETAG = 1;
                                it.CIGSEQ = cigseq;
                                cigseq++;
                            }
                            cigseq = 1;
                            foreach (var it in Normaldata)
                            {
                                nownum += it.NORMALQTY ?? 0;
                                it.CIGSEQ = cigseq;
                                it.ALLPACKAGESEQ = item.allpackageq;
                                it.PUSHSPACE = cenum + 1;
                                it.NORMAILSTATE = 10;
                                it.UNIONPACKAGETAG = 1;
                                it.PACKAGESEQ = item.packageq;
                                //恰好一条记录 需要分割为两条记录
                                if (nownum > nornum)
                                {
                                    decimal itemnum = it.NORMALQTY ?? 0;
                                    decimal surpnum = Math.Abs(nownum - (nornum));//一垛多出的条数
                                    decimal addpnum = itemnum - surpnum;//分配的数量
                                    T_PACKAGE_TASK _PACKAGE_TASK = new T_PACKAGE_TASK();

                                    DataCopy.CopyToT(it, _PACKAGE_TASK);
                                    _PACKAGE_TASK.NORMALQTY = addpnum;
                                    it.NORMALQTY = surpnum;
                                    _PACKAGE_TASK.NORMAILSTATE = 10;
                                    it.UNIONPACKAGETAG = 0;
                                    it.NORMAILSTATE = 0;
                                    task.Add(_PACKAGE_TASK);
                                }
                                else
                                {
                                    task.Add(it);
                                }
                                cigseq++;
                                if (nownum >= nornum)
                                {
                                    cigseq = 1;
                                    break;
                                }
                            }
                            Normaldata.RemoveAll(x => x.NORMAILSTATE == 10);
                        }
                        else//纯异型烟
                        {
                            var data1 = task.Where(x => x.STATE == 10 && x.ALLPACKAGESEQ == item.allpackageq).ToList();
                            if (data1.Count > 0)
                            {
                                foreach (var it in data1)
                                {
                                    it.PUSHSPACE = 1;
                                    it.UNIONPACKAGETAG = 1;
                                    it.CIGSEQ = cigseq;
                                    cigseq++;
                                }
                                cigseq = 1;
                            }
                        }
                    }
                }
                else//纯异型烟
                {
                    decimal packageseq = 0;
                    decimal tempallpackageseq = 0;
                    decimal cigseq = 1;
                    var datalist = task.Where(x => x.STATE == 10).ToList();
                    if (datalist.Count > 0)
                    {
                        foreach (var it in datalist)
                        {
                            if (tempallpackageseq != it.ALLPACKAGESEQ)
                            {
                                tempallpackageseq = (it.ALLPACKAGESEQ ?? 0);
                                packageseq++;
                                cigseq = 1;
                            }
                            it.CIGSEQ = cigseq;
                            it.PACKAGESEQ = packageseq;
                            it.PUSHSPACE = 1;
                            it.UNIONPACKAGETAG = 0;
                            cigseq++;
                        }
                        packageseq = 0;
                        tempallpackageseq = 0;
                    }
                }
                //将剩下的常规烟分配
                while (Normaldata.Count != 0)
                {
                    if (Normaldata.Count > 0)
                    {
                        allpackagenum++;
                    }
                    decimal allnornum = Normaldata.Sum(x => x.NORMALQTY) ?? 0;
                    decimal nornum = allnornum;
                    decimal nownum = 0;
                    decimal cigseq = 1;
                    decimal packageseq = (task.Max(x => x.PACKAGESEQ) ?? 0) + 1;
                    if (Remainder != 0 && nornum > 36)
                    {
                        nornum = 30 + Remainder;
                        Remainder = 0;
                    }
                    else if (Remainder == 0 && nornum > 36)
                    {
                        nornum = 36;
                    }
                    //计算接下来还有多少没有分配，如果等于7层，接下来5+2分配
                    if (Math.Ceiling(allnornum / 6) == 7)
                    {
                        nornum = 30;
                    }
                    foreach (var it in Normaldata)
                    {
                        nownum += it.NORMALQTY ?? 0;
                        it.CIGSEQ = cigseq;
                        it.ALLPACKAGESEQ = allpackagenum;
                        it.PUSHSPACE = 0;
                        it.NORMAILSTATE = 10;

                        it.PACKAGESEQ = packageseq;
                        //恰好一条记录 需要分割为两条记录
                        if (nownum > nornum)
                        {
                            decimal itemnum = it.NORMALQTY ?? 0;
                            decimal surpnum = Math.Abs(nownum - (nornum));//一垛多出的条数
                            decimal addpnum = itemnum - surpnum;//分配的数量
                            T_PACKAGE_TASK _PACKAGE_TASK = new T_PACKAGE_TASK();

                            DataCopy.CopyToT(it, _PACKAGE_TASK);
                            _PACKAGE_TASK.NORMALQTY = addpnum;
                            it.NORMALQTY = surpnum;
                            _PACKAGE_TASK.NORMAILSTATE = 10;
                            it.NORMAILSTATE = 0;
                            task.Add(_PACKAGE_TASK);
                        }
                        else
                        {
                            task.Add(it);
                        }
                        cigseq++;
                        if (nownum >= nornum)
                        {
                            cigseq = 1;
                            break;
                        }
                    }
                    Normaldata.RemoveAll(x => x.NORMAILSTATE == 10);
                }
            }
            else//纯常规烟订单
            {
                while (Normaldata.Count != 0)
                {

                    decimal allnornum = Normaldata.Sum(x => x.NORMALQTY) ?? 0;
                    decimal nornum = allnornum;
                    decimal nownum = 0;
                    decimal cigseq = 1;
                    decimal packageseq = (task.Max(x => x.PACKAGESEQ) ?? 0) + 1;
                    if (Remainder != 0 && nornum > 36)
                    {
                        nornum = 30 + Remainder;
                        Remainder = 0;
                    }
                    else if (Remainder == 0 && nornum > 36)
                    {
                        nornum = 36;
                    }
                    //计算接下来还有多少没有分配，如果等于7层，接下来5+2分配
                    if (Math.Ceiling(allnornum / 6) == 7)
                    {
                        nornum = 30;
                    }

                    foreach (var it in Normaldata)
                    {
                        nownum += it.NORMALQTY ?? 0;
                        it.CIGSEQ = cigseq;
                        it.ALLPACKAGESEQ = allpackagenum;
                        it.PUSHSPACE = 0;
                        it.NORMAILSTATE = 10;

                        it.PACKAGESEQ = packageseq;
                        //恰好一条记录 需要分割为两条记录
                        if (nownum > nornum)
                        {
                            decimal itemnum = it.NORMALQTY ?? 0;
                            decimal surpnum = Math.Abs(nownum - (nornum));//一垛多出的条数
                            decimal addpnum = itemnum - surpnum;//分配的数量
                            T_PACKAGE_TASK _PACKAGE_TASK = new T_PACKAGE_TASK();

                            DataCopy.CopyToT(it, _PACKAGE_TASK);
                            _PACKAGE_TASK.NORMALQTY = addpnum;
                            it.NORMALQTY = surpnum;
                            _PACKAGE_TASK.NORMAILSTATE = 10;
                            it.NORMAILSTATE = 0;
                            task.Add(_PACKAGE_TASK);
                        }
                        else
                        {
                            task.Add(it);
                        }
                        cigseq++;
                        if (nownum >= nornum)
                        {
                            cigseq = 1;
                            break;
                        }
                    }
                    Normaldata.RemoveAll(x => x.NORMAILSTATE == 10);
                    if (Normaldata.Count > 0)
                    {
                        allpackagenum++;
                    }
                }
            }
            unnormallist.Clear();
            normallist.Clear();
        }
        /// <summary>
        /// 获取包装机未生成的订单
        /// </summary>
        /// <param name="et"></param>
        /// <returns></returns>
        public List<V_PRODUCE_PACKAGEINFO> GetPackageInfoView(Entities entity, decimal packageNo, decimal synseq)
        {
            //获取当前包装机 已经生成的最大分拣任务号
            decimal sortnum = entity.T_PACKAGE_TASK.Where(x => x.PACKAGENO == packageNo && x.SYNSEQ == synseq).ToList().Count > 0 ? entity.T_PACKAGE_TASK.Where(x => x.PACKAGENO == packageNo && x.SYNSEQ == synseq).ToList().Max(x => x.SORTNUM).Value : 0;
            //只查询大于最大分拣任务号的订单
            var PackageInfoView = entity.V_PRODUCE_PACKAGEINFO.Where(x => x.EXPORT == packageNo && x.SYNSEQ == synseq && x.TASKNUM > sortnum).ToList();
            return PackageInfoView;
        }
        /// <summary>
        /// 获取当前包装机的 当前包装机任务号
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="packageNo"></param>
        /// <returns></returns>
        public decimal GetPackageNum(Entities entity, decimal packageNo)
        {
            decimal StartNum = 0;
            switch ((int)packageNo)
            {
                case 1:
                    StartNum = 0;
                    break;
                case 2:
                    StartNum = 100000;
                    break;
                case 3:
                    StartNum = 200000;
                    break;
                case 4:
                    StartNum = 300000;
                    break;
                case 5:
                    StartNum = 400000;
                    break;
                case 6:
                    StartNum = 500000;
                    break;
                case 7:
                    StartNum = 600000;
                    break;
                case 8:
                    StartNum = 700000;
                    break;
            }
            decimal maxpackagenum = entity.T_PACKAGE_TASK.Where(x => x.PACKAGENO == packageNo).Count() > 0 ? (int)entity.T_PACKAGE_TASK.Where(x => x.PACKAGENO == packageNo).Max(x => x.PACKTASKNUM).Value : StartNum;
            if (maxpackagenum < StartNum)
            {
                maxpackagenum = StartNum;
            }
            return maxpackagenum;
        }
    }
}
