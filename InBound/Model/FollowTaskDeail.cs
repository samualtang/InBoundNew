using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Model
{
  public  class FollowTaskDeail
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
        public decimal Machineseq { get; set; }
        public decimal GroupNO { get; set; }
        public decimal THRESHOLD { get; set; }
        public String CIGARETTDENAME { get; set; }
        public String CIGARETTDECODE { get; set; }
        public decimal SORTMACHINE { get; set; }
        public decimal MainBelt { get; set; }
        public decimal SortState { get; set; }
        public decimal UnionState { get; set; }
        public decimal MachineState { get; set; }
        public decimal UnionTasknum { get; set; }
        public decimal SortNum { get; set; }
        public decimal SecSortNum { get; set; }
        public String Billcode { get; set; }
        public String ExportNum { get; set; }
        public String TaskNum { get; set; }
        public Decimal DTaskNum { get; set; }
        public Decimal MERAGENUM { get; set; }
        public String SortTroughNum {get;set;}
        public decimal tNum { get; set; }
        public decimal qty { get; set; }
        public decimal pokePlace { get; set; }
        public decimal POKENUM  { get; set; }
        public decimal POKEID { get; set; }
    
      //  public decimal mainBelt { get; set; }


    }
}
