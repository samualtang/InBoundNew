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
   public class PackageService:BaseService
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
                TIEM = Convert.ToDateTime("2019-02-28", format);
                TIEM2 = Convert.ToDateTime("2019-03-01", format);
 
                var query = (from item in entity.T_UN_TASK_H
                             where item.PACKAGEMACHINE == packageNo    && item.ORDERDATE >= TIEM &&  item.ORDERDATE <= TIEM2  
                             orderby item.SORTNUM
                             select item).ToList();
                if (query != null)
                {
                    foreach (var v in query)
                    {
                        int pcount=0;
                        List<T_PACKAGE_TASK> task = new List<T_PACKAGE_TASK>();
                        var query2 = (from item2 in entity.T_UN_POKE_H where item2.BILLCODE == v.BILLCODE orderby item2.POKEID select item2).ToList();
                        if (query2 != null)
                        {
                            foreach (var v2 in query2)
                            {
                                allCount = allCount + 1;
                                pcount = pcount + 1;
                                T_PACKAGE_TASK temp = new T_PACKAGE_TASK();
                                temp.PTID = GetSeq("select s_package_task.nextval from dual");
                                temp.CIGARETTECODE = v2.CIGARETTECODE;
                                T_WMS_ITEM tempItem = ItemService.GetItemByCode(v2.CIGARETTECODE);
                                temp.CIGARETTENAME = tempItem.ITEMNAME;
                                temp.CIGHIGH = tempItem.IHEIGHT;
                                temp.CIGWIDTH = tempItem.IWIDTH;
                                temp.BILLCODE = v2.BILLCODE;
                                temp.SORTNUM = v2.SORTNUM;
                                temp.CIGNUM = allCount;
                                temp.CIGSEQ = pcount;
                                temp.PACKAGESEQ = packageNo;
                                temp.ALLPACKAGESEQ = 0;
                                temp.PACKAGENO = 1;
                                temp.STATE = 0;//0 新增  10 确定
                                temp.CIGZ = decimal.Parse(tempItem.DOUBLETAKE);
                                task.Add(temp);
                            }
                            allpackagenum++;
                            GenPackageInfo(task,entity); 
                            foreach (var item in task)
                            {
                                entity.T_PACKAGE_TASK.AddObject(item);
                            }
                            entity.SaveChanges();
                        }

                    }
                }
            }
        }

        int packageWidth = 540;//宽
        int packageHeight = 460 + 20;//20浮动
        int jx = 3;

        int taskCount = 6;//一次参与计算的条数
        int allpackagenum = 0;
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
        decimal deviation = 2;
        public void calcArea(List<PackageArea> list, PackageArea area,decimal width,decimal height,decimal cigseq)
        {
            list.Remove(area);

            PackageArea areal = new PackageArea();
            PackageArea arear = new PackageArea();
            
            areal.left = area.left;
            areal.right = arear;
            areal.beginx = area.beginx;
            areal.width = width;
            areal.height = height;

            areal.cigaretteList = new List<Cigarette> { new Cigarette(){ CigaretteNo=cigseq, fromx=0, tox=width, width=width}  };
            if (area.left != null)
            {
                area.left.right = areal;
               if (Math.Abs(area.left.height - areal.height) <= deviation)
                {
                    areal.left = area.left.left;
                    areal.beginx = area.left.beginx;
                    areal.cigaretteList = area.left.cigaretteList;
                    areal.cigaretteList.Add(new Cigarette() { CigaretteNo = cigseq, fromx = area.left.width, tox = area.left.width+width, width = width });
                    areal.width = area.left.width + areal.width;
                    if (areal.height < area.left.height)
                    {
                        areal.height = area.left.height;
                    }
                    list.Remove(area.left);
                }
            }
            arear.left = areal;
            arear.beginx = areal.beginx + areal.width;
            arear.width = area.width - width;
            arear.height = area.height;
            arear.right = area.right;
            arear.cigaretteList = area.cigaretteList;
            arear.cigaretteList[0].tox = arear.width;
            arear.cigaretteList[0].width = arear.width;
            if (area.right != null)
            {
                area.right.left = arear;
            }
            list.Add(areal);
            list.Add(arear);
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
               
            }
            return diclist.Peek();
        }
        public void calcPackage(List<T_PACKAGE_TASK> task, List<PackageArea> list)
        {
            int packageNO = 1;
            var templist = task.Where(x => x.STATE != 10).ToList().Take(taskCount).ToList();
         
            if (templist != null && templist.Count > 0)
            {

                while (templist.Where(x => x.STATE == 10) != null && templist.Where(x => x.STATE != 10).Count()>0)
                {
                   // templist = templist.Where(x => x.STATE != 10).ToList();
                    decimal minHeight = 0;
                    if (list.Where(x => x.isscan == 0) != null && list.Where(x => x.isscan == 0).Count()>0)
                    {
                        minHeight = list.Where(x => x.isscan == 0).Min(x => x.height);
                    }
                    else
                    {
                      
                        decimal sciseq = templist.Where(x=>x.STATE!=10).Min(x => x.CIGSEQ)??0;
                        List<T_PACKAGE_TASK> bigList = templist.Where(x => x.STATE == 10 && x.CIGSEQ > sciseq ).OrderBy(x=>x.CIGSEQ).ToList();//有大于当前序号已排好的烟
                        if (bigList != null && bigList.Count>0)
                        {
                            bigList = templist.Where(x => x.STATE == 10).OrderBy(x => x.CIGSEQ).ToList();
                            templist.ForEach(x => { x.PACKAGESEQ = 0; x.STATE = 0; });
                            templist = templist.Where(x => x.CIGSEQ <= sciseq).ToList();
                            list.Clear();
                           
                           list= RollBackList(list, bigList);
                            list.ForEach(x => x.isscan = 0);
                            minHeight = list.Where(x => x.isscan == 0).Min(x => x.height);
                            //List<PackageArea> list1 = new List<PackageArea>(list);
                            //diclist.Push(list1);
                        }
                        else
                        {
                            packageNO += 1;
                            allpackagenum += 1;
                            list.Clear();
                            diclist.Clear();
                            PackageArea areainit = new PackageArea();
                            areainit.width = packageWidth;
                            areainit.height = 0;
                            areainit.cigaretteList = new List<Cigarette>() { new Cigarette() { CigaretteNo = 0, fromx = 0, tox = packageWidth, width = packageWidth } };
                            list.Add(areainit);
                            List<PackageArea> list1 = new List<PackageArea>(list);
                            diclist.Push(list1);
                        }
                          
                    }
                    PackageArea area = list.Find(x => x.height == minHeight && x.isscan==0);
                    //是否有相同品牌的烟
                    List<ItemGroup> allGroupList = templist.Where(x=>x.STATE!=10).GroupBy(x => x.CIGARETTECODE).Select(x => new ItemGroup() { CigaretteCode = x.Key, Total = x.Count() }).ToList();
                    List<ItemGroup> groupList = allGroupList.FindAll(x => x.Total > 1);
                  //  List<ItemGroup> smallGroupList = allGroupList;

                   
                    String tempcode = "";
                    decimal tempWidth = 0;
                    decimal gdc = 0;//高度差
                    if (groupList != null && groupList.Count > 0)//优先双抓 而且是宽度大的双抓
                    {
                        List<Cigarette> cigList = area.cigaretteList;
                        foreach (var v in groupList)
                        {
                            T_PACKAGE_TASK temptask = templist.Find(x => x.CIGARETTECODE == v.CigaretteCode);
                            decimal cgiseq = templist.Where(x => x.CIGARETTECODE == v.CigaretteCode && x.STATE!=10).FirstOrDefault().CIGSEQ ?? 0;
                            if (temptask.CIGWIDTH * 2 <= area.width && area.height + temptask.CIGHIGH < packageHeight)//小于区域宽度,同时小于整包高度
                            {
                                if (cgiseq > area.cigaretteList[0].CigaretteNo) //后面的seq必须大于已放的才能放
                                {
                                    if (tempWidth <= temptask.CIGWIDTH)
                                    {
                                        if (tempWidth == temptask.CIGWIDTH)
                                        {

                                            if (area.left != null)
                                            {


                                                //看左边高度差 取相差小的
                                                if (Math.Abs(area.height + temptask.CIGHIGH ?? 0 - area.left.height) - Math.Abs(gdc) < 0)
                                                {
                                                    tempWidth = temptask.CIGWIDTH ?? 0;
                                                    tempcode = v.CigaretteCode;
                                                    gdc = area.height + temptask.CIGHIGH ?? 0 - area.left.height;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            tempWidth = temptask.CIGWIDTH ?? 0;
                                            tempcode = v.CigaretteCode;
                                            if (area.left != null)
                                            {
                                                gdc = area.height + temptask.CIGHIGH ?? 0 - area.left.height;
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                    if (tempcode != "")
                    {
                        var chooseItem = templist.FindAll(x => x.CIGARETTECODE == tempcode && x.STATE!=10).OrderBy(x => x.CIGSEQ).Take(2).ToList();
                        decimal width = 0;
                        decimal height = 0;
                        decimal cigseq = 0;
                        foreach (var v in chooseItem)
                        {
                            v.PACKAGESEQ = packageNO;
                            v.CIGWIDTHX = area.beginx + v.CIGWIDTH;//两条当做一条
                            v.CIGHIGHY = area.height + v.CIGHIGH;
                            v.STATE = 10;
                            v.DOUBLETAKE = "1";
                            v.ALLPACKAGESEQ = allpackagenum;
                            width += (v.CIGWIDTH ?? 0);
                            height = (area.height + v.CIGHIGH ?? 0);
                            cigseq = v.CIGSEQ??0;
                        }
                        //更新area
                        calcArea(list, area, width, height, cigseq);
                        List<PackageArea> list1 = new List<PackageArea>(list);
                        diclist.Push(list1);
                    }
                    else
                    {

                        tempWidth = 0;
                        gdc = 0;//高度差
                        foreach (var v in allGroupList)
                        {
                            T_PACKAGE_TASK temptask = templist.Find(x => x.CIGARETTECODE == v.CigaretteCode && x.STATE!=10);
                            if ((temptask.CIGSEQ ?? 0) > area.cigaretteList[0].CigaretteNo)//后面的seq必须大于已放的才能放
                            {
                                if (temptask.CIGWIDTH <= area.width && area.height + temptask.CIGHIGH < packageHeight)
                                {
                                    if (tempWidth <= temptask.CIGWIDTH)
                                    {
                                        if (tempWidth == temptask.CIGWIDTH)
                                        {

                                            if (area.left != null)
                                            {


                                                //看左边高度差 取相差小的
                                                if (Math.Abs(area.height + temptask.CIGHIGH ?? 0 - area.left.height) - Math.Abs(gdc) < 0)
                                                {
                                                    tempWidth = temptask.CIGWIDTH ?? 0;
                                                    tempcode = v.CigaretteCode;
                                                    gdc = area.height + temptask.CIGHIGH ?? 0 - area.left.height;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            tempWidth = temptask.CIGWIDTH ?? 0;
                                            tempcode = v.CigaretteCode;
                                            if (area.left != null)
                                            {
                                                gdc = area.height + temptask.CIGHIGH ?? 0 - area.left.height;
                                            }
                                        }
                                    }

                                }
                            }



                        }
                        if (tempcode != "")
                        {
                            var chooseItem = templist.FindAll(x => x.CIGARETTECODE == tempcode && x.STATE!=10).OrderBy(x => x.CIGSEQ).FirstOrDefault();
                            decimal width = 0;
                            decimal height = 0;
                            decimal cigseq = 0;

                            
                                chooseItem.PACKAGESEQ = packageNO;
                                chooseItem.CIGWIDTHX = area.beginx + chooseItem.CIGWIDTH / 2;
                                chooseItem.CIGHIGHY = area.height + chooseItem.CIGHIGH;
                                chooseItem.STATE = 10;
                                chooseItem.ALLPACKAGESEQ = allpackagenum;
                                width += (chooseItem.CIGWIDTH ?? 0);
                                height = (area.height + chooseItem.CIGHIGH ?? 0);
                                cigseq = chooseItem.CIGSEQ ?? 0;
                                //更新area
                                calcArea(list, area, width, height, cigseq);
                                List<PackageArea> list1 = new List<PackageArea>(list);
                                diclist.Push(list1);
                            
                            
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



        
    }
}
