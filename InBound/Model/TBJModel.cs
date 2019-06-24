using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Model
{
public  class TBJModel
{
        public String billcode{get;set;}
        public String regioncode{get;set;}
        public decimal    PACKTASKNUM{get;set;}
        public decimal normalqty { get; set; }
        public decimal PACKAGESEQ { get; set; }
        public String CIGARETTECODE { get; set; }
        public String CIGARETTENAME { get; set; }
        public String CIGTYPE { get; set; }
        public decimal PACKAGEQTY { get; set; }
        public DateTime ORDERDATE { get; set; }
        public decimal MIANBELT { get; set; }
        public decimal ORDERQTY { get; set; }
        public decimal ALLPACKAGESEQ { get; set; }
        public decimal SORTNUM { get; set; }
        public decimal synseq { get; set; }
        public decimal var_orderPagNum { get; set; }
        public decimal var_shaednum { get; set; }
        public decimal var_ordercount { get; set; }
        public decimal var_NormalTPagNum { get; set; }
        public decimal var_UnnormalTPagNum { get; set; }
        public decimal pCount { get; set; }
        public decimal var_UnionTPagNum { get; set; }
}
}
