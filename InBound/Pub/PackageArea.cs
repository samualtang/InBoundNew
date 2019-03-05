using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Pub
{
    public class PackageArea
    {
        public decimal width = 0;
        public decimal height = 0;
        public decimal beginx = 0;
        public PackageArea left;
        public PackageArea right;
        public List<Cigarette> cigaretteList;
        public decimal isscan = 0;
    }
    public class Cigarette
    {
        public decimal CigaretteNo;
        public decimal width;
        public decimal fromx;
        public decimal tox;
        public decimal index;
    }
}
