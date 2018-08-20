using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FollowTask.DataModle
{
    public static class AllPlcState
    {
        /// <summary>
        /// 0为未连接，1为已连接
        /// </summary>
        public static int FJState1 { get; set; }
        public static int FJState2 { get; set; }
        public static int FJState3 { get; set; }
        public static int FJState4 { get; set; }
        public static int InOutState { get; set; }

    }
}
