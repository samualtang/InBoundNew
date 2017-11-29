using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Model
{
    public class AddressInfo
    {
        public String DeviceNo { get; set; }
      
        public string Address { get; set; }
        public decimal DeviceCount { get; set; }
        
        public decimal Count { get; set; }

        public decimal Threshold { get; set; }
    }
}
