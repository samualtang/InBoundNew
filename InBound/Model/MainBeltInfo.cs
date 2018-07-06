using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Model
{
    public class MainBeltInfo
    {
        public decimal SortNum { get; set; }
        public decimal Quantity { get; set; }
        public decimal Place { get; set; }
        public string mainbelt { get; set; }
        public decimal GroupNO { get; set; }
        public string MsgCode { get; set; }
        public string ErrorMsg { get; set; }
        public List<UnionTaskInfo> taskInfo { get; set; }
    }
}
