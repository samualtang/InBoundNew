using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InBound.Pub
{
    public class DateTimeService
    {

        /// <summary>
        /// 获取下一个日期（周日至周四 加一天，周五之周六 加三天）
        /// </summary>
        /// <param name="DateContrl">时间控件</param>
        /// <param name="DateFort">时间格式</param>
        /// <returns></returns>
        public string GetNextDate(System.Windows.Forms.DateTimePicker DateContrl, string DateFort)
        {
            int Dayofweek;
            switch (DateTime.Now.AddDays(1).DayOfWeek)
            {
                case DayOfWeek.Friday:
                    Dayofweek = 5;
                    break;
                case DayOfWeek.Monday:
                    Dayofweek = 1;
                    break;
                case DayOfWeek.Saturday:
                    Dayofweek = 6;
                    break;
                case DayOfWeek.Sunday:
                    Dayofweek = 7;
                    break;
                case DayOfWeek.Thursday:
                    Dayofweek = 4;
                    break;
                case DayOfWeek.Tuesday:
                    Dayofweek = 2;
                    break;
                case DayOfWeek.Wednesday:
                    Dayofweek = 3;
                    break;
                default:
                    Dayofweek = 1;
                    break;
            }
            if (Dayofweek == 5 || Dayofweek == 6)
            {
                DateContrl.Value = DateTime.Now.AddDays(3);
            }
            else
            {
                DateContrl.Value = DateTime.Now.AddDays(1);
            }
            return DateContrl.Value.ToString(DateFort);
        }
        /// <summary>
        /// 获取当前日期
        /// </summary>
        /// <param name="DateContrl"></param>
        /// <param name="DateFort"></param>
        /// <returns></returns>
        public string GetDate(System.Windows.Forms.DateTimePicker DateContrl, string DateFort)
        {
            return DateContrl.Value.ToString(DateContrl.Value.ToString());
        }
    }
}
