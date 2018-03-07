using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using InBound;

namespace Machine
{ 

    public class AlarmsInfoXml
    {

        private string fileLogPath;
        private string dataFile;


        public AlarmsInfoXml()
        {
            this.fileLogPath = Application.StartupPath + "\\alarms\\";
            this.dataFile = "data.xml";
        }

        public string ReadLastInfo(string deviceNo = null)
        {
            if (!File.Exists(Path.Combine(fileLogPath + dataFile)))
                return "";
            var list = XmlOper.XmlDeserializeFromFile<List<AlarmsFileModel>>(Path.Combine(fileLogPath + dataFile), Encoding.UTF8);
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

        public void write(AlarmsFileModel obj)
        {
            List<AlarmsFileModel> list = new List<AlarmsFileModel>();
            if (File.Exists(Path.Combine(fileLogPath + dataFile)))
            {
                list = XmlOper.XmlDeserializeFromFile<List<AlarmsFileModel>>(Path.Combine(fileLogPath + dataFile), Encoding.UTF8);
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


    /// <summary>
    /// 
    /// </summary>
    public class AlarmsFileModel
    {
        public string DeviceNo { get; set; }

        public string DeviceName { get; set; }

        public int AlarmsBit { get; set; }

        public string AlarmsValue { get; set; }

        public DateTime InfoTime { get; set; }

        public int RowLine { get; set; }

        public override string ToString()
        {
            return string.Format("DeviceNo:{0},DeviceName:{1},AlarmsBit:{2},AlarmsValue:{3},InfoTime:{4};",
                DeviceNo,
                DeviceName,
                AlarmsBit,
                AlarmsValue,
                InfoTime);
        }
    }
}
