using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Model
{
    public class UnionTaskInfo
    {
        public String CIGARETTDENAME { get; set; }
        public String CIGARETTDECODE { get; set; }
        public decimal MainBelt { get; set; }
        public decimal SortNum { get; set; }
        public decimal qty { get; set; }
        public decimal groupno { get; set; }
        public decimal machineseq { get; set; }
        public String CUSTOMERNAME { get; set; }
        public String BILLCODE { get; set; }
        public decimal SORTSEQ { get; set; }
        public decimal IsOnMainBelt { get; set; }//0 在机械手  1 在皮带
        public decimal Place { get; set; }
    }
}
