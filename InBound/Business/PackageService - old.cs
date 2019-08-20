using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Pub;
using InBound.Model;
using System.Reflection;
using System.Globalization;

namespace InBound.Business
{
   public class PackageService1:BaseService
    {

        public void GetAllOrder(decimal packageNo)
        {

            int allCount=0;
            using (Entities entity = new Entities())
            {

                DateTime TIEM = new DateTime();
                DateTime TIEM2= new DateTime();
                DateTimeFormatInfo format = new DateTimeFormatInfo();
                format.ShortDatePattern = "yyyy-MM-dd";
                TIEM = Convert.ToDateTime("2019-03-21", format);
                TIEM2 = Convert.ToDateTime("2019-03-21", format);
 
                //var query = (from item in entity.T_UN_TASK_H
                //             where item.BILLCODE =="CS10429225" && item.PACKAGEMACHINE == packageNo //&& item.ORDERDATE >= TIEM && item.ORDERDATE <= TIEM2 //&& item.TASKNUM == 799261
                //             orderby item.TASKNUM 
                //             select item).ToList();

                var query = (from item in entity.V_PRODUCE_PACKAGEINFO
                             where 
                             //item.EXPORT == packageNo && 
                             item.BILLCODE == "CS10429225"
                             //&& item.ALLOWSORT=="非标"
                             // && (item.TASKNUM == 664692 || item.TASKNUM == 663143)
                             group item by new { item.BILLCODE, item.TASKNUM } into allcode
                             select new { allcode.Key.BILLCODE, allcode.Key.TASKNUM }).OrderBy(x => x.TASKNUM).ToList();
                ptid = entity.T_PACKAGE_TASK.Count() > 0 ? entity.T_PACKAGE_TASK.Max(x => x.PTID) + 1 : 1;
                if (query != null)
                {
                    int i = 0;
                    //decimal maxSeq = GetSeq("select decode(max(ptid),null,0,max(ptid)) from t_package_task ");

                    foreach (var v in query)
                    {
                        i++;
                       
                        int pcount=0;
                        List<T_PACKAGE_TASK> task = new List<T_PACKAGE_TASK>();
                        var query2 = (from item2 in entity.V_PRODUCE_PACKAGEINFO where item2.BILLCODE == v.BILLCODE && item2.ALLOWSORT == "非标" orderby item2.SENDTASKNUM, item2.MACHINESEQ, item2.TROUGHNUM select item2).ToList();
                        if (query2 != null)
                        {
                            foreach (var v2 in query2)
                            {
                                
                                allCount = allCount + 1;
                                pcount = pcount + 1;
                                T_PACKAGE_TASK temp = new T_PACKAGE_TASK();
                                temp.PTID = ptid;
                                temp.CIGARETTECODE = v2.CIGARETTECODE;
                                T_WMS_ITEM tempItem = ItemService.GetItemByCode(v2.CIGARETTECODE);
                                temp.CIGARETTENAME = tempItem.ITEMNAME;
                                temp.CIGHIGH = tempItem.IHEIGHT;
                                temp.CIGWIDTH = tempItem.IWIDTH;
                                temp.CIGWIDTH = tempItem.IWIDTH;
                                temp.CIGLENGTH = tempItem.ILENGTH;
                                temp.BILLCODE = v2.BILLCODE;
                                temp.SORTNUM = v2.TASKNUM;
                                temp.CIGNUM = allCount;
                                temp.CIGSEQ = pcount;
                                //temp.PACKAGESEQ = v2.EXPORT;
                                temp.ALLPACKAGESEQ = 0;
                                //temp.PACKAGENO = v2.PACKAGEMACHINE;
                                temp.PACKAGENO = 1;//v2.EXPORT;
                                //temp.CIGTYPE = "2";
                                temp.CIGTYPE = v2.ALLOWSORT == "非标" ? "2" : "1";
                                temp.STATE = 0;//0 新增  10 确定
                                temp.NORMAILSTATE = 0;//0 新增  10 确定
                                temp.NORMALQTY = 1;
                                temp.UNIONPACKAGETAG = 0;
                                temp.DOUBLETAKE = "0";
                                ////temp.ORDERSEQ = v2.PRIORITY;
                                ////temp.ORDERQTY = v2.ORDERQUANTITY;
                                temp.CIGSTATE = 10;
                                task.Add(temp);
                                ptid++;
                            }
                            allpackagenum++;
                            GenPackageInfo(task,entity); 
                            foreach (var item in task)
                            {
                                item.PACKTASKNUM = item.ALLPACKAGESEQ;
                                entity.T_PACKAGE_TASK.AddObject(item);
                            }
                            if (i == 5)
                            {
                                entity.SaveChanges();
                                i = 0;
                            }

                        }

                    }
                    entity.SaveChanges();
                }
            }
        }
        decimal allhight = 294;
        decimal normalhight = 49;
        decimal ptid;
        int packageWidth = 530;//宽
        int packageHeight = 200 ;//20浮动
        int jx = 5;
        decimal deviation = 3;//高度误差

        int taskCount = 6;//一次参与计算的条数
        int allpackagenum = 0;
        int NormalCount = 36;//常规烟整包条烟数
        public static T DeepCopyByReflect<T>(T obj)
        {
            //如果是字符串或值类型则直接返回
            if (obj is string || obj.GetType().IsValueType) return obj;
            object retval = Activator.CreateInstance(obj.GetType());
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                try { field.SetValue(retval, DeepCopyByReflect(field.GetValue(obj))); }
                catch { }
            }
            return (T)retval;
        }
        public void GenPackageInfo(List<T_PACKAGE_TASK> task,Entities entity)//
        {
            diclist.Clear();
            List<PackageArea> list = new List<PackageArea>();
            PackageArea area = new PackageArea();
            area.width = packageWidth;
            area.height = 0;
            area.cigaretteList = new List<Cigarette>() { new Cigarette(){ CigaretteNo=0, fromx=0, tox=packageWidth, width=packageWidth} };
            list.Add(area);
            List<PackageArea> list1 = new List<PackageArea>(list);
            diclist.Push(list1);
            calcPackage(task, list);


        }
       
        public void calcArea(List<PackageArea> list, PackageArea area, decimal width, decimal height, decimal cigseq,AreaUnit unit)
        {
            list.Remove(area);
           // list.RemoveAll(x => x.beginx == area.beginx);
            PackageArea areal = new PackageArea();
            PackageArea areaC = new PackageArea();
            PackageArea arear = new PackageArea();

            areal.left = area.left;
            areal.right = areaC;
            areal.beginx = area.beginx;
            areal.width = unit.beginx;
            areal.height = area.height;
            List<Cigarette> temp = area.cigaretteList.Where(x => x.index < unit.begin).ToList();
            areal.cigaretteList = new List<Cigarette>() ;
            areal.cigaretteList.AddRange(temp);
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

            areaC.left = areal;
            areaC.beginx = area.beginx + unit.beginx;
            areaC.height = height;
            areaC.width = width;
            areaC.cigaretteList = new List<Cigarette> { new Cigarette() { CigaretteNo = cigseq, fromx = 0, tox = width, width = width } };
            areaC.right = arear;
            arear.left = areaC;
            arear.beginx = areaC.beginx + width;
            arear.width = area.width - width-unit.beginx;
            arear.height = area.height;
            arear.right = area.right;
            Cigarette tempC = area.cigaretteList.Where(x => x.index == unit.begin).FirstOrDefault();
            if (tempC.width < width)
            {

                arear.cigaretteList = area.cigaretteList.Where(x => x.index > unit.begin).ToList();
                arear.cigaretteList[0].width-= (width - tempC.width);
            }
            else
            {
                arear.cigaretteList = area.cigaretteList.Where(x => x.index >= unit.begin).ToList();
                arear.cigaretteList[0].width -= width;
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
        public void calcArea(List<PackageArea> list, PackageArea area,decimal width,decimal height,decimal cigseq)
        {
            list.Remove(area);
           // list.RemoveAll(x => x.beginx == area.beginx);
            PackageArea areal = new PackageArea();
            PackageArea arear = new PackageArea();
            
            areal.left = area.left;
            areal.right = arear;
            areal.beginx = area.beginx;
            areal.width = width;
            areal.height = height;

            areal.cigaretteList = new List<Cigarette> { new Cigarette(){ CigaretteNo=cigseq, fromx=0, tox=width, width=width}  };
            if (area.left != null && list.Contains(area.left))
            {
                area.left.right = areal;
               if (Math.Abs(area.left.height - areal.height) <= deviation)
                {
                   
                   
                    areal.beginx = area.left.beginx;
                    //if (areal.beginx == 0)
                  //  {
                  //      areal.left = null;
                  //      area.left.cigaretteList.Clear();
                 //   }
                    areal.cigaretteList = CopyCigaretteList(area.left.cigaretteList);
                    areal.cigaretteList.Add(new Cigarette() { CigaretteNo = cigseq, fromx = area.left.width, tox = area.left.width+width, width = width });
                    areal.width = area.left.width + areal.width;
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
            arear.width = area.width - width;
            arear.height = area.height;
            arear.right = area.right;
            arear.cigaretteList =CopyCigaretteList(area.cigaretteList);
            //if (arear.cigaretteList.Count > 1)
            //{

            if (width > area.cigaretteList[0].width + jx * 2)
                {
                    arear.cigaretteList.RemoveAt(0);
                    arear.cigaretteList[0].width -= (width - area.cigaretteList[0].width);
                }
                else
                {

                    arear.cigaretteList[0].width = (area.cigaretteList[0].width - width);
                }
            //}
            //else
            //{
            //    arear.cigaretteList[0].width = arear.cigaretteList[0].width - width;
            //}


                if ( arear.beginx + arear.width == packageWidth && arear.width <= minWidth)
                {
                    areal.width += (arear.width/2);
                    areal.right = null;
                    list.Add(areal);
                }
                else
                {
                    list.Add(areal);
                    list.Add(arear);
                }
        }
        public Stack<List<PackageArea>> diclist = new Stack< List<PackageArea>>();
        public List<PackageArea> RollBackList(List<PackageArea> list, List<T_PACKAGE_TASK> bigList)
        {
            var tempCode = "";
            var doubleTake = "0";
            foreach (var item in bigList)
            {
                if (item.CIGARETTECODE != tempCode)
                {
                    
                        list = diclist.Pop();
                  
                    tempCode = item.CIGARETTECODE;
                    doubleTake = item.DOUBLETAKE;
                }
                else if (item.DOUBLETAKE != "1" || (item.DOUBLETAKE == "1" && doubleTake != "1"))
                {
                    list = diclist.Pop();
                    doubleTake = item.DOUBLETAKE;
                }
                else
                {
                    tempCode = "";//一次双抓后重新计算
                }
               
            }
            return diclist.Peek();
        }
        decimal minWidth = 75;
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
        public void calcPackage(List<T_PACKAGE_TASK> task, List<PackageArea> list)
        {
            var unnormaltask = task.Where(x => x.CIGTYPE == "2").ToList();
            var normaltask = task.Where(x => x.CIGTYPE == "1").ToList();
            decimal startpackagenum = allpackagenum;

            int packageNO = 1;
            //var templist = task.Where(x => x.STATE != 10).ToList().Take(taskCount).ToList();
            var templist = unnormaltask.Where(x => x.STATE == 0).ToList().Take(taskCount).ToList();

            if (templist != null && templist.Count > 0)
            {

                while (templist.Where(x => x.STATE == 10 ) != null && templist.Where(x => x.STATE != 10).Count()>0)
                {
                   // templist = templist.Where(x => x.STATE != 10).ToList();
                    decimal minHeight = 0;
                    if (list.Where(x => x.isscan == 0 && x.width > minWidth) != null && list.Where(x => x.isscan == 0 && x.width > minWidth).Count() > 0)
                    {
                        minHeight = list.Where(x => x.isscan == 0 && x.width > minWidth).Min(x => x.height);
                    }
                    else
                    {
                      
                        decimal sciseq = templist.Where(x=>x.STATE!=10).Min(x => x.CIGSEQ)??0;
                        List<T_PACKAGE_TASK> bigList = templist.Where(x => x.STATE == 10 && x.CIGSEQ > sciseq ).OrderBy(x=>x.CIGSEQ).ToList();//有大于当前序号已排好的烟
                        if (bigList != null && bigList.Count>0)
                        {
                          //  bigList = templist.Where(x => x.STATE == 10).OrderBy(x => x.CIGSEQ).ToList();
                          
                            //list.Clear();
                           
                           list= RollBackList(list, bigList);
                            list.ForEach(x => x.isscan = 0);
                            templist.ForEach(x => { x.PACKAGESEQ = 0; x.STATE = 0; x.DOUBLETAKE = "0"; });
                            templist = templist.Where(x => x.CIGSEQ <= sciseq).ToList();
                            minHeight = list.Where(x => x.isscan == 0 && x.width > minWidth).Min(x => x.height);
                            //List<PackageArea> list1 = new List<PackageArea>(list);
                            //diclist.Push(list1);
                        }
                        else
                        {
                            NormalCig(task, normaltask, 1);

                            packageNO += 1;
                            allpackagenum += 1;
                            list.Clear();
                            diclist.Clear();
                            PackageArea areainit = new PackageArea();
                            areainit.width = packageWidth;
                            areainit.height = 0;
                            areainit.cigaretteList = new List<Cigarette>() { new Cigarette() { CigaretteNo = 0, fromx = 0, tox = packageWidth, width = packageWidth } };
                            list.Add(areainit);
                           
                            diclist.Push(CopyList(list));
                        }
                          
                    }
                    PackageArea area = list.Find(x => x.height == minHeight && x.isscan == 0 && x.width > minWidth);
                    area = list.FindAll(x => x.beginx == area.beginx && x.isscan == 0 && x.width > minWidth ).OrderByDescending(x=>x.height).FirstOrDefault();
                    //是否有相同品牌的烟
                    List<ItemGroup> allGroupList = templist.Where(x=>x.STATE!=10).GroupBy(x => x.CIGARETTECODE).Select(x => new ItemGroup() { CigaretteCode = x.Key, Total = x.Count() }).ToList();

                    List<ItemGroup> groupList = allGroupList.FindAll(x => x.Total > 1);

                    foreach (var item in groupList)
                    {
                       var doubleList= templist.Where(x => x.STATE != 10 && x.CIGARETTECODE == item.CigaretteCode).Take(2).ToList();
                       if (Math.Abs((doubleList[0].CIGSEQ ?? 0) - (doubleList[1].CIGSEQ ?? 0)) != 1)
                       {
                           item.Total = 100;
                       }
                    }
                    groupList.RemoveAll(x => x.Total == 100);
                  //  List<ItemGroup> smallGroupList = allGroupList;

                   
                    String tempcode = "";
                    decimal tempWidth = 0;
                    decimal gdc = 0;//高度差
                    List<AreaUnit> unit = new List<AreaUnit>();
                    AreaUnit tempunit=null;
                    if (groupList != null && groupList.Count > 0)//优先双抓 而且是宽度大的双抓
                    {
                      //  List<Cigarette> cigList = area.cigaretteList;
                        foreach (var v in groupList)
                        {
                            unit.Clear();
                            T_PACKAGE_TASK temptask = templist.Find(x => x.CIGARETTECODE == v.CigaretteCode);
                            decimal cgiseq = templist.Where(x => x.CIGARETTECODE == v.CigaretteCode && x.STATE!=10).FirstOrDefault().CIGSEQ ?? 0;
                            if ((temptask.CIGWIDTH +jx) * 2 <= area.width && area.height + temptask.CIGHIGH < packageHeight)//小于区域宽度,同时小于整包高度
                            {
                               
                             
                              
                                int i = 0;

                                decimal flag = 1;
                                decimal lastflag = 0;
                                decimal beginx = 0;
                                foreach (var item in area.cigaretteList)
                                {
                                    item.index = i;
                                    if (cgiseq < item.CigaretteNo)
                                    {
                                        flag = 0;
                                    }
                                    else
                                    {
                                        flag = 1;
                                    }
                                    if (lastflag == 1 && flag == 1)
                                    {
                                        
                                            AreaUnit u = unit.ElementAt(unit.Count - 1);
                                            u.width += item.width;
                                            u.end = i;
                                        

                                    }
                                    else if (lastflag == 0 && flag == 1)
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
                                foreach (var cell in unit)
                                {
                                    if ((temptask.CIGWIDTH+jx) * 2 <=cell.width) //后面的seq必须大于已放的才能放
                                    {
                                        if (tempWidth <= temptask.CIGWIDTH)
                                        {
                                            if (tempWidth == temptask.CIGWIDTH)
                                            {

                                                if (area.left != null)
                                                {


                                                    //看左边高度差 取相差小的
                                                    if (Math.Abs(area.height + (temptask.CIGHIGH ?? 0) - area.left.height) - Math.Abs(gdc) < 0)
                                                    {
                                                        tempWidth = temptask.CIGWIDTH ?? 0;
                                                        tempcode = v.CigaretteCode;
                                                        gdc = area.height + (temptask.CIGHIGH ?? 0) - area.left.height;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                tempWidth = temptask.CIGWIDTH ?? 0;
                                                tempcode = v.CigaretteCode;
                                                if (area.left != null)
                                                {
                                                    gdc = area.height + (temptask.CIGHIGH ?? 0) - area.left.height;
                                                }
                                            }
                                        }
                                        tempunit = cell;
                                        break;
                                    }
                                }

                            }
                        }
                    }
                    if (tempcode != "" && tempunit!=null)
                    {
                        var chooseItem = templist.FindAll(x => x.CIGARETTECODE == tempcode && x.STATE!=10).OrderBy(x => x.CIGSEQ).Take(2).ToList();
                        decimal width = 0;
                        decimal height = 0;
                        decimal cigseq = 0;

                        foreach (var v in chooseItem)
                        {
                            v.PACKAGESEQ = packageNO;
                            v.CIGWIDTHX = area.beginx+tempunit.beginx + v.CIGWIDTH +jx;//两条当做一条
                            v.CIGHIGHY = area.height + v.CIGHIGH;
                            v.STATE = 10;
                            v.DOUBLETAKE = "1";
                            v.ALLPACKAGESEQ = allpackagenum;
                            width += (v.CIGWIDTH ?? 0) +jx;
                            height = area.height + (v.CIGHIGH ?? 0);
                            cigseq = v.CIGSEQ??0;
                        }
                        //更新area
                        if (tempunit.begin == 0)
                        {
                            calcArea(list, area, width, height, cigseq);
                        }
                        else
                        {
                            calcArea(list, area, width, height, cigseq,tempunit);
                        }

                        diclist.Push(CopyList(list));
                    }
                    else
                    {

                        tempWidth = 0;
                        gdc = 0;//高度差
                        unit.Clear();
                        foreach (var v in allGroupList)
                        {
                            T_PACKAGE_TASK temptask = templist.Find(x => x.CIGARETTECODE == v.CigaretteCode && x.STATE!=10);
                            int i = 0;
                            unit.Clear();
                            decimal flag = 1;
                            decimal lastflag = 0;
                            decimal beginx = 0;
                            foreach (var item in area.cigaretteList)
                            {
                                item.index = i;
                                if (temptask.CIGSEQ < item.CigaretteNo)
                                {
                                    flag = 0;
                                }
                                else
                                {
                                    flag = 1;
                                }
                                if (lastflag == 1 && flag == 1)
                                {

                                    AreaUnit u = unit.ElementAt(unit.Count - 1);
                                    u.width += item.width;
                                    u.end = i;


                                }
                                else if (lastflag == 0 && flag == 1)
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
                            if (temptask.CIGWIDTH +jx*2 <= area.width && area.height + temptask.CIGHIGH < packageHeight)
                            {
                            foreach (var cell in unit)
                            {
                                if (temptask.CIGWIDTH +jx*2 <= cell.width) //后面的seq必须大于已放的才能放
                                {
                                    
                                        if (tempWidth <= temptask.CIGWIDTH+jx*2)
                                        {
                                            if (tempWidth == temptask.CIGWIDTH+jx*2)
                                            {

                                                if (area.left != null)
                                                {


                                                    //看左边高度差 取相差小的
                                                    if (Math.Abs(area.height + (temptask.CIGHIGH ?? 0) - area.left.height) - Math.Abs(gdc) < 0)
                                                    {
                                                        tempWidth = (temptask.CIGWIDTH ?? 0)+jx*2;
                                                        tempcode = v.CigaretteCode;
                                                        gdc = area.height + (temptask.CIGHIGH ?? 0) - area.left.height;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                tempWidth = (temptask.CIGWIDTH ?? 0)+jx*2;
                                                tempcode = v.CigaretteCode;
                                                if (area.left != null)
                                                {
                                                    gdc = area.height + (temptask.CIGHIGH ?? 0) - area.left.height;
                                                }
                                            }

                                        }
                                        tempunit = cell;
                                        break;
                                    }
                                }
                            }



                        }
                        if (tempcode != "" && tempunit!=null)
                        {
                            var chooseItem = templist.FindAll(x => x.CIGARETTECODE == tempcode && x.STATE!=10).OrderBy(x => x.CIGSEQ).FirstOrDefault();
                            decimal width = 0;
                            decimal height = 0;
                            decimal cigseq = 0;

                            
                                chooseItem.PACKAGESEQ = packageNO;
                                chooseItem.CIGWIDTHX = area.beginx+tempunit.beginx +chooseItem.CIGWIDTH / 2 +jx ;
                                chooseItem.CIGHIGHY = area.height + chooseItem.CIGHIGH;
                                chooseItem.STATE = 10;
                                chooseItem.ALLPACKAGESEQ = allpackagenum;
                                width += (chooseItem.CIGWIDTH ?? 0) +jx*2;
                                height = area.height + (chooseItem.CIGHIGH ?? 0);
                                cigseq = chooseItem.CIGSEQ ?? 0;
                                //更新area
                                //更新area
                                if (tempunit.begin == 0)
                                {
                                    calcArea(list, area, width, height, cigseq);
                                }
                                else
                                {
                                    calcArea(list, area, width, height, cigseq, tempunit);
                                }
                                diclist.Push(CopyList(list));
                            
                            
                        }
                        else
                        {
                            area.isscan = 1;
                        }

                    }


                    if (templist.Where(x => x.STATE != 10) == null || templist.Where(x => x.STATE != 10).Count() == 0)

                    {
                        list.ForEach(x => x.isscan = 0);
                        templist = task.Where(x => x.STATE != 10).ToList().Take(taskCount).ToList();
                    }


                }
            }
          
        }


        public void NormalCig(List<T_PACKAGE_TASK> task, List<T_PACKAGE_TASK> normaltask, int tag)
        {
            //常规烟合包：有常规烟的订单
            //2.如果常规烟大于36  且除6有余数，将组30 + 余数为一包，剩余用于搭配异型烟
            //1.如果小于36 且除6有余数 直接一包 

            var datalist = task.Where(x => x.ALLPACKAGESEQ == allpackagenum && x.STATE == 10).ToList();
            //未计算的总常规烟 条烟数
            decimal normalnum = normaltask.Where(x => x.NORMAILSTATE != 10).Sum(x => x.NORMALQTY) ?? 0;
            decimal Remainder = normalnum % 6;//余数
            //如果常规烟大于36 且除6有余数
            if (normalnum > 30 + Remainder && Remainder != 0)
            {
                decimal count = 0;
                foreach (var item in normaltask.Where(x => x.NORMAILSTATE != 10).ToList())
                {
                    count += item.NORMALQTY ?? 0;
                    //恰好一条记录 需要分割为两条记录
                    if (count > 30 + Remainder)
                    {
                        decimal itemnum = item.NORMALQTY ?? 0;
                        var temp = normaltask.Where(x => x.PTID == item.PTID).ToList();
                        decimal surpnum = (item.NORMALQTY ?? 0) - Math.Abs(count - NormalCount);//多出的条数
                        item.NORMALQTY -= surpnum;
                        T_PACKAGE_TASK _PACKAGE_TASK = new T_PACKAGE_TASK();

                        Pub.DataCopy.CopyToT(item, _PACKAGE_TASK);

                        _PACKAGE_TASK.NORMALQTY = surpnum;
                        _PACKAGE_TASK.PTID = ptid;
                        normaltask.Add(_PACKAGE_TASK);
                        ptid++;
                    }
                    item.ALLPACKAGESEQ = allpackagenum;
                    item.PUSHSPACE = 0;
                    item.NORMAILSTATE = 10;
                    item.PACKAGESEQ = datalist.Select(x => x.PACKAGESEQ).LastOrDefault();
                    if (count == 30 + Remainder)
                    {
                        break;
                    }
                }
                allpackagenum += 1;
            }
            //如果常规烟小于36 且除6有余数
            if (normalnum < NormalCount && Remainder != 0)
            {
                foreach (var item in normaltask.Where(x => x.NORMAILSTATE != 10).ToList())
                {
                    item.ALLPACKAGESEQ = allpackagenum;
                    item.PUSHSPACE = 0;
                    item.NORMAILSTATE = 10;
                    item.PACKAGESEQ = datalist.Select(x => x.PACKAGESEQ).LastOrDefault();
                }
            }

            //获取上一个包 最高坐标  可匹配常规烟层数
            decimal PackHight = Math.Floor((allhight - task.Where(x => x.ALLPACKAGESEQ == allpackagenum - 1).Max(x => x.CIGHIGHY) ?? 0) / normalhight);
            decimal tempnum = 0;
            decimal maxnum = PackHight * 6;//可匹配常规烟 条数
            bool unnormaltag = true;
            if (PackHight > 0)
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

                //未计算常规烟还有几层 
                decimal uncompute = normaltask.Where(x => x.NORMAILSTATE == 0).Sum(x => x.NORMALQTY).Value;
                maxnum = uncompute > maxnum ? maxnum : uncompute;
                PackHight = uncompute > maxnum ? PackHight : uncompute / 6;
                foreach (var item in task.Where(x => x.CIGTYPE == "1" && x.NORMAILSTATE == 0).ToList())
                {

                    if (tempnum < maxnum)
                    {
                        tempnum += item.NORMALQTY ?? 0;//5
                        //恰好一条记录 需要分割为两条记录
                        if (tempnum > maxnum)
                        {
                            decimal tmp = item.NORMALQTY ?? 0;//5
                            decimal itemnum = item.NORMALQTY ?? 0;//5
                            var temp = normaltask.Where(x => x.PTID == item.PTID).ToList();
                            decimal surpnum = Convert.ToDecimal(item.NORMALQTY) - Math.Abs(tempnum - maxnum);//多出的条数//5-4  1

                            T_PACKAGE_TASK _PACKAGE_TASK = new T_PACKAGE_TASK();

                            Pub.DataCopy.CopyToT(item, _PACKAGE_TASK);

                            _PACKAGE_TASK.NORMALQTY = tmp - surpnum;//1
                            _PACKAGE_TASK.PTID = ptid;

                            normaltask.Add(_PACKAGE_TASK);//
                            task.Add(_PACKAGE_TASK);//
                            ptid++;

                            item.NORMALQTY = surpnum;//4
                        }
                        item.ALLPACKAGESEQ = allpackagenum;
                        item.PUSHSPACE = PackHight;
                        item.NORMAILSTATE = 10;
                        item.PACKAGESEQ = datalist.Select(x => x.PACKAGESEQ).LastOrDefault();
                        if (unnormaltag)
                        {
                            foreach (var it in datalist)
                            {
                                it.PUSHSPACE = PackHight;
                                it.ALLPACKAGESEQ = allpackagenum;
                            }
                            unnormaltag = false;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                //}                                
            }

        }

        
    }
}
