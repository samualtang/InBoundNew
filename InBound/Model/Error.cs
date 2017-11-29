using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound;

namespace InBound.Model
{
  public  class Error
    {
        
       public string BRANDID { get; set; }
       public string JOBID { get; set; }
       public decimal? PLANQTY { get; set; }
       public string SOURCE { get; set; }
       public string TARGET { get; set; }
       public decimal? JOBTYPE { get; set; }
       public string ERRORCODE { get; set; }
       public DateTime? RESDATE { get; set; }
    }
}
