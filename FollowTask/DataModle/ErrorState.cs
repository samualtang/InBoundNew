using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FollowTask.DataModle
{
    public static class ErrorState
    {
        /// <summary>
        /// 0为未使用，1为正在扫描，2为正常，3为异常
        /// </summary>
        public static int SortState { get; set; }
    }
}
