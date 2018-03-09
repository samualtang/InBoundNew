using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Model
{
    public class OrderGroupDetail
    {
        public String CustomerNO { set; get; }
        public String CustomerName { set; get; }
        public String CigaretteCode { set; get; }
        public String CigaretteName { set; get; }
        public String CustomerCode { set; get; }
        public Decimal? TaskNum { set; get; }
        public Decimal? PokeNum { set; get; }
        public DateTime? OrderDate { set; get; }
        public String FJDate { set; get; }
        public String Seq { set; get; }
        public String RegionCode { set; get; }
        public String RegionName { set; get; }
    }
}
