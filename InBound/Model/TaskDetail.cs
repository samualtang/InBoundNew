using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Model
{
   public class TaskDetail
    {
       String _TaskNum;
       String _CIGARETTDECODE;
       String _CIGARETTDENAME;
       decimal _MANTISSA;
       decimal _THRESHOLD;
      
      
       String _SortTroughNum;
      
       decimal _GroupNO;
       decimal _qty;
       public decimal MANTISSA { get; set; }
       public decimal GroupNO { get; set; }
       public decimal THRESHOLD { get; set; }
       public String CIGARETTDENAME { get; set; }
       public String CIGARETTDECODE { get; set; }
       public decimal SORTMACHINE { get; set; }
       public decimal MainBelt { get; set; }
       public String ExportNum { get; set; }
       public String TaskNum { get; set; }
       public Decimal DTaskNum { get; set; }
       public String SortTroughNum
       {
           get;
           set;

       }
       public decimal tNum { get; set; }
       public decimal qty { get; set; }
    }
}
