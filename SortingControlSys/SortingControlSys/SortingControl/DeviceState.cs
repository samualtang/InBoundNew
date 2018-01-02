using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using InBound.Business;
using InBound;

namespace SortingControlSys.SortingControl
{
    public class DeviceStateManager
    {
        public SaveFileHandler fileOper;

        public DeviceStateManager()
        {
            fileOper = new SaveFileHandler();
            // fileOper.ReadFileToList();
        }


        public Func<int, string> OnGetErr;


        /// <summary>
        /// 警告处理
        /// </summary>
        public Action<DeviceStateInfoModel> AlarmsHandler;

        /// <summary>
        /// 写报警记录，包括异常清除写入
        /// </summary>
        /// <param name="type"></param>
        /// <param name="len"></param>
        /// <param name="temp"></param>
        /// <param name="GroupNo"></param>
        public void WriteErrWithCheck(string len, int GroupNo, String temp)
        {
            String deviceNo = "" + len;
            string lastInfo = fileOper.ReadLastInfo(deviceNo).Trim();
            string compStrs;
            List<OperationChar> lst = UnionBit.Union(lastInfo, temp, out compStrs);
            List<OperationChar> lstWhere = lst.Where(w => w.op != Oper.None).ToList();

            foreach (OperationChar item in lstWhere)
            {
                String errMsg = item.val == "0" ? string.Format("消除{0}", OnGetErr(item.bit)) : OnGetErr(item.bit);
                ErrListService.Add(deviceNo, GroupNo, 10, errMsg, item.val);
                DeviceStateInfoModel info = new DeviceStateInfoModel { DeviceNo = len, DeviceName = deviceNo, ErrInfo = errMsg };
                if (AlarmsHandler!=null)
                {
                    AlarmsHandler(info);
                }
            }
            fileOper.write(new DeviceStateInfoModel
            {
                DeviceNo = deviceNo,
                AlarmsValue = temp
            });
        }
    }

    public class DeviceStateInfoModel
    {
        public string DeviceNo { get; set; }
        public string DeviceName { get; set; }
        public string ErrInfo { get; set; }
        public string AlarmsValue { get; set; }
    }


    public class SaveFileHandler
    {
        private string fileLogPath;
        private string dataFile;


        public SaveFileHandler()
        {
            this.fileLogPath = Application.StartupPath + "\\alarms\\";
            this.dataFile = "data.xml";
        }

        public string ReadLastInfo(string deviceNo = null)
        {
            if (!File.Exists(Path.Combine(fileLogPath + dataFile)))
                return "";
            var list = XmlOper.XmlDeserializeFromFile<List<DeviceStateInfoModel>>(Path.Combine(fileLogPath + dataFile), Encoding.UTF8);
            if (list == null)
            {
                return "";
            }
            var model = list.Where(w => w.DeviceNo == deviceNo).FirstOrDefault();
            if (model == null)
            {
                return "";
            }
            else
            {
                return model.AlarmsValue.ToString();
            }
        }

        public void write(DeviceStateInfoModel obj)
        {
            List<DeviceStateInfoModel> list = new List<DeviceStateInfoModel>();
            if (File.Exists(Path.Combine(fileLogPath + dataFile)))
            {
                list = XmlOper.XmlDeserializeFromFile<List<DeviceStateInfoModel>>(Path.Combine(fileLogPath + dataFile), Encoding.UTF8);
                if(list==null)
                    list = new List<DeviceStateInfoModel>();
            }

            if (list.Count == 0)
            {
                list.Add(obj);
                XmlOper.XmlSerializeToFile(list, fileLogPath, dataFile, Encoding.UTF8);
                return;
            }

            var model = list.Where(w => w.DeviceNo == obj.DeviceNo).FirstOrDefault();
            if (model != null)
            {
                model.AlarmsValue = obj.AlarmsValue;
            }
            else
            {
                list.Add(obj);
            }
            XmlOper.XmlSerializeToFile(list, fileLogPath, dataFile, Encoding.UTF8);
        }
    } 
}
