using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Model;

namespace InBound.Business
{
    public  class OrderCiTrough
    {
        //更新通道
        public int UpdateCiTrough()
        {
            using (Entities et = new Entities())
            {
                //获取将要查重新排序的数据(1线)
                List<TroughNumList> list = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 40 && x.TROUGHTYPE == 10 && x.STATE == "10" && x.GROUPNO == 1).Select(x => new TroughNumList { troughnun = x.TROUGHNUM, Cid = x.CIGARETTECODE, cname = x.CIGARETTENAME }).ToList();
                List<TroughNumList> ListTroughNum = new List<TroughNumList>();//排序后的通道号 （1线）
                ListTroughNum = list.OrderBy(x => x.troughnun).ToList();

                List<TroughNumList> ListCigarette = new List<TroughNumList>();//排序后的品牌列表（1线）
                ListCigarette = (from item in et.T_PRODUCE_SORTTROUGH
                                join item2 in et.T_WMS_ITEM on item.CIGARETTECODE equals item2.ITEMNO
                                where item.CIGARETTETYPE == 40 && item.TROUGHTYPE == 10 && item.STATE == "10" && item.GROUPNO == 1
                                 orderby item2.IHEIGHT, item2.ILENGTH descending, item2.IWIDTH descending, item2.ITEMNO
                                select new TroughNumList(){ troughnun = item.TROUGHNUM, Cid = item2.ITEMNO, cname = item2.ITEMNAME }).ToList();

                 //完成重新排序后的数据√ (1线)
                var result = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 40 && x.TROUGHTYPE == 10 && x.STATE == "10" && x.GROUPNO == 1).Select(x => x).OrderBy(x => x.TROUGHNUM).ToList();
                int index = 0;
                foreach (var item in result)
                {  
                    item.CIGARETTECODE = ListCigarette[index].Cid;
                    item.CIGARETTENAME = ListCigarette[index].cname;
                    index++; 
                }
                 
                int g1 = et.SaveChanges();
                /***********************************************************************************************************/
                //获取将要查重新排序的数据(2线)
                List<TroughNumList> list2 = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 40 && x.TROUGHTYPE == 10 && x.STATE == "10" && x.GROUPNO == 2).Select(x => new TroughNumList { troughnun = x.TROUGHNUM, Cid = x.CIGARETTECODE, cname = x.CIGARETTENAME }).ToList();
                List<TroughNumList> ListTroughNum2 = new List<TroughNumList>();//排序后的通道号 （2线）
                ListTroughNum = list.OrderBy(x => x.troughnun).ToList();

                List<TroughNumList> ListCigarette2 = new List<TroughNumList>();//排序后的品牌列表（2线）
                ListCigarette2 = (from item in et.T_PRODUCE_SORTTROUGH
                                 join item2 in et.T_WMS_ITEM on item.CIGARETTECODE equals item2.ITEMNO
                                 where item.CIGARETTETYPE == 40 && item.TROUGHTYPE == 10 && item.STATE == "10" && item.GROUPNO == 2
                                 orderby item2.IHEIGHT, item2.ILENGTH descending, item2.IWIDTH descending,item2.ITEMNO
                                 select new TroughNumList() { troughnun = item.TROUGHNUM, Cid = item2.ITEMNO, cname = item2.ITEMNAME }).ToList();

                //完成重新排序后的数据√ (2线)
                var result2 = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 40 && x.TROUGHTYPE == 10 && x.STATE == "10" && x.GROUPNO == 2).Select(x => x).OrderBy(x => x.TROUGHNUM).ToList();
                int index2 = 0;
                foreach (var item in result2)
                {
                    item.CIGARETTECODE = ListCigarette2[index2].Cid;
                    item.CIGARETTENAME = ListCigarette2[index2].cname;
                    index2++;
                }

                int g2 = et.SaveChanges();

                if ( g1 > 0 && g2 > 0 )
                {
                    return 3;
                }
                else if ( g1 > 0 && g2 <= 0)
                {
                    return 1;
                }
                else if ( g2 > 0 && g1 <= 0) 
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
                
            }
        }
    }
}
