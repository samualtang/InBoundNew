using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Model
{
   public class T_UN_SpecialSmoke
    {
       //{ POKEID = item.POKEID, CIGARETTECODE = item.CIGARETTECODE, MACHINESEQ = item.MACHINESEQ, SORTNUM = item.SORTNUM, SENDTASKNUM = item.SENDTASKNUM, PACKAGEMACHINE = item.PACKAGEMACHINE, POKENUM = item.POKENUM, LENGHT = item2.ILENGTH, WIDTH = item2.IWIDTH }).Take(10).ToList();
      public decimal POKEID { get; set; }
      public string CIGARETTECODE { get; set; }
      public decimal MACHINESEQ { get; set; }
      public decimal SORTNUM { get; set; }
      public decimal SENDTASKNUM { get; set; }
      public decimal POKENUM { get; set; }
      public decimal LENGHT { get; set; }
      public decimal WIDTH { get; set; }
      public decimal PACKAGEMACHINE { get; set; }
    
    }
}
