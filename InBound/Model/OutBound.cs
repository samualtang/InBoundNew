using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound
{
   public class OutBound
    {
        public string _CIGARETTECODE;
        public string _CIGARETTENAME;
        public decimal _QTY;
        public string _CELLNO;
        public DateTime? _CREATETIME;
        public string CIGARETTECODE { get; set; }
        public string BarCode { get; set; }
        public string CIGARETTENAME { get; set; }
        public decimal QTY { get; set; }
        public string CELLNO { get; set; }
        public DateTime? CREATETIME { get; set; }
    }
}
