using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Business
{
    public  enum WmsType
    {
        码垛任务=10,
        入库单入库任务=20,
        成品入库=30, 
        返库任务=40,
        出库任务=50,
        补货出库=55,
        自动拆垛补货任务=60,
        人工拆垛补货任务=70,
        开箱任务=80, 
        托盘条码下达=90,//1194 托盘条码下达
        任务取消=97,
        空托盘回收任务=100

    }
    public enum InOutType
    {
        入库=1,
        出库=2
    }
}
