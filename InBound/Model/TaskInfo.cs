using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Model
{
    public class TaskInfo
    {
        public string _REGIONCODE;
        public decimal _QTY;
        public decimal _FinishQTY;
        public decimal _FinishCount;
        public String _Rate;
        public decimal _Count;
        public string REGIONCODE { get; set; }
        public string LineNum { get; set; }
        public decimal QTY { get; set; }
        public decimal FinishQTY{ get;set;}
        public decimal Count { get; set; }
        public decimal FinishCount { get; set; }
        public String Rate { get; set; }

        
        /////////////////////////////////
        public decimal MACHINESEQ { get; set; }
        public decimal GROUPNO { get; set; }
        public decimal POKENUM { get; set; }
        public decimal UNIONTASKNUM { get; set; }
        public string TROUGHNUM { get; set; }
        public decimal SYNSEQ { get; set; }//批次号 优先取
        public Decimal BATCHODE { get; set; }//批次号
        public string CUSTOMERNAME { get; set; }//货主
        public DateTime? ORDERDATE { get; set; }//订单日期
        public decimal? MIANBELT { get; set; }//主皮带号
        public decimal PACKAGEMACHINE { get; set; }//包装机号 
    }
}
