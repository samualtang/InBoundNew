using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using InBound.Business;
using InBound;

namespace UnNormal_New
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
        public void WriteErrWithCheck(string val, string index, string lineNum)
        {
            String deviceNo = "" + index;
            string lastInfo = fileOper.ReadLastInfo(deviceNo).Trim();
            if (lastInfo == "")
                lastInfo = "0";
            if (lastInfo != val)
            {
                String errMsg = "";//取数据库取对应的错误信息
                if (val == "0")
                {
                     errMsg = "消除将来从数据库获取";//取数据库取对应的错误信息
                }
                else
                {
                    errMsg = "消除将来从数据库获取another one";
                }
                ErrListService.Add(deviceNo, decimal.Parse(lineNum), 50, errMsg, val);
            }
                
            fileOper.write(new DeviceStateInfoModel
            {
                DeviceNo = deviceNo,
                AlarmsValue = val
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
