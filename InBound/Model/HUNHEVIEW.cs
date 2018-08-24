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
   }
}
