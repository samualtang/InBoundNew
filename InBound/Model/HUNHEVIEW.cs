using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Model
{
   public class HUNHEVIEW
    {
       /// <summary>
       /// pokeid集合
       /// </summary>
       public List<string> POKEIDLIST { get; set; }
       public decimal POKEID { get; set; }
       /// <summary>
       /// 烟名称
       /// </summary>
       public string CIGARETTENAME { get; set; }
       /// <summary>
       /// 烟数量
       /// </summary>
       public decimal? QUANTITY { get; set; }
       public string CIGARETTECODE { get; set; }
       public decimal? MACHINESEQ { get; set; }

       public decimal? SORTNUM { get; set; }

       public decimal? SENDTASKNUM { get; set; }
       public decimal? PULLSTATUS { get; set; }
       
    }

   public class HUNHEVIEW1
   {
       /// <summary>
       /// pokeid集合
       /// </summary>
       public List<string> POKEIDLIST { get; set; }
       public decimal POKEID { get; set; }
       /// <summary>
       /// 烟名称
       /// </summary>
       public string CIGARETTENAME { get; set; }
       /// <summary>
       /// 烟数量
       /// </summary>
       public decimal? QUANTITY { get; set; }
       public string CIGARETTECODE { get; set; }
       public decimal? MACHINESEQ { get; set; }
       public string PACK_BAR { get; set; }

       public decimal? SENDTASKNUM { get; set; }
   }

   public class SPSortBeltInfo
   {
       //任务包号、分拣任务号、条烟名称、条烟编码、户名、专卖证号、户序、包装机
    
       /// <summary>
       /// 条烟名称
       /// </summary>
       public string CIGARETTENAME { get; set; }
       /// <summary>
       /// 条烟编码
       /// </summary>
       public string CIGARETTECODE { get; set; }
       /// <summary>
       /// 任务包号
       /// </summary>
       public decimal? SENDTASKNUM { get; set; }
       /// <summary>
       /// 分拣任务号
       /// </summary>
       public decimal? SORTNUM { get; set; }
       /// <summary>
       /// 户名
       /// </summary>
       public string CUSTOMERNAME { get; set; }
       /// <summary>
       /// 专卖证号
       /// </summary>
       public string CUSTOMERCODE { get; set; } 
       /// <summary>
       /// 户序
       /// </summary>
       public decimal? SORTSEQ { get; set; }
       /// <summary>
       /// 包装机
       /// </summary>
       public decimal? PACKAGENO { get; set; }
       
   }

}
