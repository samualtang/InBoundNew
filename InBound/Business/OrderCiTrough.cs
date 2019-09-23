using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Model;

namespace InBound.Business
{
    public  class OrderCiTrough
    {
        /// <summary>
        /// 检验是否有已排程数据
        /// </summary>
        /// <returns>true是、false否</returns>
        public bool CheckTrough()
        {
            using (Entities et = new Entities())
            {
                bool num = et.T_UN_TASK.Count() > 0 ? false : true;
                return num;
            }
        }

        /// <summary>
        /// 更新特异型烟道
        /// </summary>
        /// <returns>结果</returns>
        public int UpdateCiTrough()
        {
            using (Entities et = new Entities())
            {
                int[] gual = new int[4];
                for (int i = 1; i <= 4; i++)
                {
                    List<TroughNumList> ListTroughNum = new List<TroughNumList>();//排序后的通道号 （分拣线）

                    List<CigaretteSortList> ListCigarette = new List<CigaretteSortList>();//排序后的品牌列表（分拣线）
                    ListCigarette = (from item in et.T_PRODUCE_SORTTROUGH
                                     join item2 in et.T_WMS_ITEM on item.CIGARETTECODE equals item2.ITEMNO
                                     where item.CIGARETTETYPE == 40 && item.TROUGHTYPE == 10 && item.STATE == "10" && item.GROUPNO == i && (item.MACHINESEQ == 1061 ||item.MACHINESEQ == 2061 ||item.MACHINESEQ == 3061 ||item.MACHINESEQ == 4061)
                                     select new CigaretteSortList() { troughnun = item.TROUGHNUM, Cid = item2.ITEMNO, cname = item2.ITEMNAME, iheight = item2.IHEIGHT ?? 0, ilength = item2.ILENGTH ?? 0, iwidth = item2.IWIDTH ?? 0 , cdtype = item2.CDTYPE??0})
                                    .OrderBy(x=>x.cdtype).ThenBy(x => x.iheight).ThenByDescending(x => x.ilength).ThenByDescending(x => x.iwidth).ToList();//低到高(长到短，宽到窄)，中支烟最后（cdtype=1的）

                    //完成重新排序后的数据√ (分拣线)
                    var result = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 40 && x.TROUGHTYPE == 10 && x.STATE == "10" && x.GROUPNO == i).Select(x => x).OrderBy(x => x.TROUGHNUM).ToList();
                    int index = 0;
                    foreach (var item in result)
                    {
                        item.CIGARETTECODE = ListCigarette[index].Cid;
                        item.CIGARETTENAME = ListCigarette[index].cname;
                        index++;
                    }
                    gual[i-1] = et.SaveChanges();
                } 
                /************************************************************************************************************/ 
                if ( gual[0]>0 && gual[1]>0 && gual[2]>0 && gual[3]>0 )
                {
                    return 3;
                } 
                else
                {
                    return 0;
                } 
            }
        }
    }
}
