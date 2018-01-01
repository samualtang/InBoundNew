using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InBound.Business;
using System.IO;
using System.Windows.Forms;

namespace Machine
{
    /// <summary>
    /// 机械手报警处理
    /// </summary>
    public class Alarms
    {

        //public RecodeAlarmsToFile fileOper;
        public AlarmsInfoXml xml;
        public Alarms()
        {
            //fileOper = new RecodeAlarmsToFile();
            xml = new AlarmsInfoXml();
            // fileOper.ReadFileToList();
        }
        /// <summary>
        /// 宕机处理
        /// </summary>
        public Action<AlarmsInfo, List<AlarmsInfo>> DowntimeHandler;
        /// <summary>
        /// 警告处理
        /// </summary>
        public Action<AlarmsInfo> AlarmsHandler;

        private List<AlarmsInfo> _marchineList = new List<AlarmsInfo>();

        /// <summary>
        /// 设备报警信息
        /// </summary>
        String[] errMsgList = { "机械手急停", "机械手报警", "外部急停报警", "吸盘监控异常", "烟条初始位校验异常", "子站故障", "线体PLC异常", "机器人宕机", "", "", "", "", "", "", "", "", };




        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="type"></param>
        /// <param name="len"></param>
        /// <param name="temp"></param>
        /// <param name="GroupNo"></param>
        public void WriteErrToDB(int type, int len, String temp, decimal GroupNo)
        {
            String deviceNo = "" + len;
            for (int i = 1; i <= temp.Length; i++)
            {
                if (temp.ElementAt(i - 1) == '1')
                {
                    String errMsg = getErrMsg(temp.Length - i);
                    ErrListService.Add(deviceNo, GroupNo, 10, errMsg, temp.ElementAt(i - 1).ToString());
                    AlarmsInfo info = new AlarmsInfo { DeviceNo = len, DeviceName = deviceNo, ErrInfo = errMsg };
                    AlarmsHandler(info);
                    Downtime(info, errMsg, temp);
                }
            }
        }

        /// <summary>
        /// 写报警记录，包括异常清除写入
        /// </summary>
        /// <param name="type"></param>
        /// <param name="len"></param>
        /// <param name="temp"></param>
        /// <param name="GroupNo"></param>
        public void WriteErrWithCheck(int type, int len, String temp, decimal GroupNo)
        {
            String deviceNo = "" + len;
            string lastInfo = xml.ReadLastInfo(deviceNo);// fileOper.ReadLastInfo(deviceNo).Trim();
            string compStrs;
            string rese = string.Empty;
            //高低位反转
            temp.Reverse().ToList().ForEach(s => { rese += s; });
            List<OperationChar> lst = UnionBit.Union(lastInfo, rese, out compStrs);
            List<OperationChar> lstWhere = lst.Where(w => w.op != Oper.None).ToList();

            foreach (OperationChar item in lstWhere)
            {
                String errMsg = item.val == "0" ? string.Format("消除{0}", getErrMsg(item.bit)) : getErrMsg(item.bit);
                ErrListService.Add(deviceNo, GroupNo, 10, errMsg, item.val);
                AlarmsInfo info = new AlarmsInfo { DeviceNo = len, DeviceName = deviceNo, ErrInfo = errMsg };
                AlarmsHandler(info);
                Downtime(info, errMsg, temp);
            }
            //fileOper.Write(compStrs, deviceNo);
            xml.write(new AlarmsFileModel { DeviceNo = deviceNo, AlarmsValue = temp });
        }



        /// <summary>
        /// 处理宕机异常
        /// </summary>
        /// <param name="info"></param>
        /// <param name="errMsg"></param>
        /// <param name="temp"></param>
        private void Downtime(AlarmsInfo info, string errMsg, string temp)
        {
            char[] ReverseArray = temp.ToArray();
            Array.Reverse(ReverseArray);
            if (ReverseArray.Count() > 7 && ReverseArray[7] == '1')//报警信息中的第七位宕机异常，另外对宕机异常处理。
            {
                _marchineList.Add(new AlarmsInfo
                {
                    DeviceName = info.DeviceName,
                    DeviceNo = info.DeviceNo,
                    ErrInfo = errMsg
                });
                DowntimeHandler(info, _marchineList);
            }
        }

        private string getErrMsg(int len)
        {
            return errMsgList[len];
        }
    }

    public class AlarmsInfo
    {
        public string ErrInfo { get; set; }
        public int DeviceNo { get; set; }
        public string DeviceName { get; set; }
    }


    public class UnionBit
    {
        public static List<OperationChar> Union(string oldVal, string newVal, out string compStrs)
        {
            List<OperationChar> list = new List<OperationChar>();
            list = Compent(oldVal, newVal);
            var compStr = string.Empty;
            list.ForEach(f => compStr += f.val);
            compStrs = compStr;
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldVal"></param>
        /// <param name="newVal"></param>
        /// <returns></returns>
        private static List<OperationChar> Compent(string oldVal, string newVal)
        {
            if (string.IsNullOrWhiteSpace(newVal))
            {
                throw new Exception("设备报警数据错误,设备报警信息不能为空！");
            }

            List<OperationChar> list = new List<OperationChar>();
            OperationChar obj;

            if (string.IsNullOrWhiteSpace(oldVal))
            {
                for (int i = 0; i < newVal.Length; i++)
                {
                    obj = new OperationChar
                    {
                        bit = i,
                        op = newVal[i].ToString() == "0" ? Oper.None : Oper.Add,
                        val = newVal[i].ToString()
                    };
                    list.Add(obj);
                }
                return list;
            }

            int spilt = oldVal.Length - newVal.Length;
            if (spilt > 0)
            {
                for (int i = 0; i < newVal.Length; i++)
                {
                    obj = new OperationChar();
                    obj.bit = i;
                    if (oldVal[i].ToString() != newVal[i].ToString())
                    {
                        obj.val = newVal[i].ToString(); ;
                        obj.op = Oper.Update;
                    }
                    else
                    {
                        obj.val = newVal[i].ToString(); ;
                        obj.op = Oper.None;
                    }
                    list.Add(obj);
                }
                var skip = oldVal.Skip(newVal.Length).ToList();
                for (int i = 0; i < skip.Count; i++)
                {
                    if (skip[i].ToString() == "1")
                    {
                        obj = new OperationChar
                        {
                            val = "0",
                            bit = newVal.Length + i,
                            op = Oper.Update
                        };
                        list.Add(obj);
                    }
                    else
                    {
                        obj = new OperationChar
                        {
                            val = oldVal[newVal.Length + i].ToString(),
                            bit = newVal.Length + i,
                            op = Oper.None
                        };
                        list.Add(obj);
                    }
                }
            }
            else if (spilt < 0)
            {
                for (int i = 0; i < oldVal.Length; i++)
                {
                    obj = new OperationChar();
                    obj.bit = i;
                    if (oldVal[i].ToString() != newVal[i].ToString())
                    {

                        //comp += newVal[i].ToString();
                        obj.val = newVal[i].ToString();
                        obj.op = Oper.Update;
                    }
                    else
                    {
                        obj.val = newVal[i].ToString(); ;
                        obj.op = Oper.None;
                    }
                    list.Add(obj);
                }
                var skip = newVal.Skip(oldVal.Length).ToList();
                for (int i = 0; i < skip.Count; i++)
                {
                    if (skip[i].ToString() == "1")
                    {
                        obj = new OperationChar
                        {
                            val = "1",
                            bit = oldVal.Length + i,
                            op = Oper.Add
                        };
                        list.Add(obj);
                    }
                    else
                    {
                        obj = new OperationChar
                        {
                            val = newVal[oldVal.Length + i].ToString(),
                            bit = newVal.Length + i,
                            op = Oper.None
                        };
                        list.Add(obj);
                    }
                }
            }
            else
            {
                for (int i = 0; i < oldVal.Length; i++)
                {
                    obj = new OperationChar();
                    obj.bit = i;
                    if (oldVal[i].ToString() != newVal[i].ToString())
                    {

                        //comp += newVal[i].ToString();
                        obj.val = newVal[i].ToString(); ;
                        obj.op = Oper.Update;
                    }
                    else
                    {
                        obj.val = newVal[i].ToString(); ;
                        obj.op = Oper.None;
                    }
                    list.Add(obj);
                }
            }
            return list;
        }

    }

    public class OperationChar
    {
        public Oper op { get; set; }
        public int bit { get; set; }
        public string val { get; set; }
    }

    public enum Oper
    {
        /// <summary>
        /// 不变化
        /// </summary>
        None,
        /// <summary>
        /// 更新报警状态
        /// </summary>
        Update,
        /// <summary>
        /// 新增报警信息
        /// </summary>
        Add
    }
}
