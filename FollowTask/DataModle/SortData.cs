﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FollowTask.DataModle
{
    public static class SortData
    {
        public static List<ErrorDates> FJList1 { get; set; }
        public static List<ErrorDates> FJList2 { get; set; }
        public static List<ErrorDates> FJList3 { get; set; }
        public static List<ErrorDates> FJList4 { get; set; }

    }
    public class ErrorDates
    {
        public int FJIndex { get; set; }
        public string ErrorTime { get; set; }
        public string FJValue { get; set; }
        public string FJError { get; set; } 

    }
}
