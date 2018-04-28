using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FormUI.TooL
{
   public enum TaskType
    {
      
        码垛任务=10 ,//10
        入库单入库任务=20,//20
        成品入库=30,//30
        返库任务=40,//40
        一楼返库=42,//42
        出库任务=50,//50
        一楼出库=52,//52
        补货出库=55,//55
        自动拆垛补货任务=60,//60
        人工拆垛补货任务=70,//70
        开箱任务=80,//80
        托盘条码下达=90,//90
        任务取消=97,//97
        空托盘回收任务 =100//100
    }
}
