using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

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
            var list = XmlOper.XmlDeserializeFromFile<List<AlarmsFileModel>>(Path.Combine(fileLogPath + dataFile), Encoding.UTF8);

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


    public class XmlOper
    {
        private static void XmlSerializeInternal(Stream stream, object o, Encoding encoding)
        {
            if (o == null)
                throw new ArgumentNullException("o");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            XmlSerializer serializer = new XmlSerializer(o.GetType());

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineChars = "\r\n";
            settings.Encoding = encoding;
            settings.IndentChars = "    ";

            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, o);
                writer.Close();
            }
        }

        /// <summary>
        /// 将一个对象序列化为XML字符串
        /// </summary>
        /// <param name="o">要序列化的对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>序列化产生的XML字符串</returns>
        public static string XmlSerialize(object o, Encoding encoding)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializeInternal(stream, o, encoding);

                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 将一个对象按XML序列化的方式写入到一个文件
        /// </summary>
        /// <param name="o">要序列化的对象</param>
        /// <param name="path">保存文件路径</param>
        /// <param name="encoding">编码方式</param>
        public static void XmlSerializeToFile(object o, string path, string fileName, Encoding encoding)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);//.CreateDirectory(path);
            }

            using (FileStream file = new FileStream(Path.Combine(path + fileName), FileMode.Create, FileAccess.Write))
            {
                XmlSerializeInternal(file, o, encoding);
            }
        }

        /// <summary>
        /// 从XML字符串中反序列化对象
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="s">包含对象的XML字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>反序列化得到的对象</returns>
        public static T XmlDeserialize<T>(string s, Encoding encoding)
        {
            if (string.IsNullOrEmpty(s))
                throw new ArgumentNullException("s");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(encoding.GetBytes(s)))
            {
                using (StreamReader sr = new StreamReader(ms, encoding))
                {
                    return (T)mySerializer.Deserialize(sr);
                }
            }
        }

        /// <summary>
        /// 读入一个文件，并按XML的方式反序列化对象。
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>反序列化得到的对象</returns>
        public static T XmlDeserializeFromFile<T>(string path, Encoding encoding)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            string xml = File.ReadAllText(path, encoding);
            return XmlDeserialize<T>(xml, encoding);
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
