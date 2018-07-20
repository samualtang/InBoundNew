using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Model
{
    public class HUNHENOWVIEW
    {
        public decimal? tasknum { get; set; }
        public decimal? sortnum { get; set; }
        public string customername { get; set; }
        public string regioncode { get; set; }
        public decimal? TROUGHNUM { get; set; }
        public string CIGARETTENAME { get; set; }
        public decimal? pokenum { get; set; }
        public decimal? status { get; set; }
        public decimal? pokeid { get; set; }
        public decimal? tasksort { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class HUNHENOWVIEW1
    {
        public decimal? tasknum { get; set; }
        public decimal? sortnum { get; set; }
        public string customername { get; set; }
        public string regioncode { get; set; }
        public decimal? TROUGHNUM { get; set; }
        public string CIGARETTECODE { get; set; }
        public string CIGARETTENAME { get; set; }
        public decimal? pokenum { get; set; }
        public decimal? status { get; set; }
        public decimal? pokeid { get; set; } 
    }
    /// <summary>
    /// 混合道条码
    /// </summary>
    public class ALLTIAOMA
    {  
        public string ITEMNO { get; set; }
        public string ITEM_NAME { get; set; } 
        public string PACK_BAR { get; set; }

    }

    /// <summary>
    /// 混合道条码
    /// </summary>
    public class HUNHETROUGH
    {
        public string troughnum { get; set; }
        public decimal? machineseq { get; set; }
        public string cigarettecode { get; set; }
        public string cigarettename { get; set; } 

        public string tiaoma { get; set; }
        
    }
}
