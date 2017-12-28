using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Machine
{
    public class RecodeAlarmsToFile
    {
        List<AlarmsFileModel> _infos = new List<AlarmsFileModel>();
        private string fileLogPath;
        private string dataFile;

        public List<AlarmsFileModel> InfoList
        {
            get
            {
                return _infos;
            }
        }
        public RecodeAlarmsToFile()
        {
            this.fileLogPath = Application.StartupPath + "\\alarms\\";
            this.dataFile = "data.txt";
        }

        public void ReadFileToList()
        {
            try
            {
                if (!File.Exists(Path.Combine(fileLogPath, dataFile)))
                {
                    return;
                }

                var file = Path.Combine(fileLogPath, dataFile);
                string content = File.ReadAllText(file).Trim();
                if (string.IsNullOrWhiteSpace(content))
                {
                    return;
                }
                var rows = content.Split(';');
                int i = 1;
                foreach (var item in rows)
                {
                    if (string.IsNullOrWhiteSpace(item))
                    {
                        return;
                    }
                    string[] ss = item.Split(',');

                    AlarmsFileModel obj = new AlarmsFileModel
                    {
                        DeviceNo = ss[0].Split(':')[1],
                        DeviceName = ss[1].Split(':')[1],
                        AlarmsBit = int.Parse(ss[2].Split(':')[1]),
                        AlarmsValue = int.Parse(ss[3].Split(':')[1]),
                        InfoTime = DateTime.Parse(ss[4].Trim().Substring(9, ss[4].Trim().Length - 9)),
                        RowLine = i
                    };
                    i++;
                    _infos.Add(obj);
                }
            }
            catch (FileLoadException e)
            {
                throw e;
            }
        }


        public string ReadLastInfo()
        {
            if (!File.Exists(Path.Combine(fileLogPath + dataFile)))
                return "";

            FileStream fs = new FileStream(Path.Combine(fileLogPath + dataFile), FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);//解决写入文件乱码   

            string line = string.Empty;
            StringBuilder sb = new StringBuilder();
            line = sr.ReadToEnd();
            sb.Append(line);
            sr.Close();
            fs.Close();
            return sb.ToString();
        }
        public void Write(string content,AlarmsFileModel obj=null)
        {
            if (!Directory.Exists(fileLogPath))
            {
                Directory.CreateDirectory(fileLogPath);//.CreateDirectory(path);
            }
            AlarmsFileModel mode = _infos.Where(w => w.DeviceNo == obj.DeviceNo).FirstOrDefault();
            if (mode != null)
            {
                //修改
                EditFile(mode.RowLine, obj.ToString(), fileLogPath + dataFile);
            }
            else
            {
                FileStream fs = new FileStream(fileLogPath + dataFile, FileMode.Append);
                StreamWriter strwriter = new StreamWriter(fs);
                try
                {
                    strwriter.WriteLine(content);
                    strwriter.Flush();
                }
                catch (Exception ee)
                {
                    //Console.WriteLine("写入信息失败:" + ee.ToString());
                    throw ee;
                }
                finally
                {
                    strwriter.Close();
                    strwriter = null;
                    fs.Close();
                    fs = null;
                }
            }
            //ReadFileToList();
        }



        public static void EditFile(int curLine, string newLineValue, string patch)
        {
            FileStream fs = new FileStream(patch, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);//解决写入文件乱码  


            string line = string.Empty;
            StringBuilder sb = new StringBuilder();
            for (int i = 1; line != null; i++)
            {
                sb.Append(line);
                if (i != curLine)
                    line = sr.ReadLine();
                else
                {
                    sr.ReadLine();
                    line = newLineValue + "\n";
                }
            }
            sr.Close();
            fs.Close();
            FileStream fs1 = new FileStream(patch, FileMode.Open, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs1);
            sw.Write(sb.ToString());
            sw.Close();
            fs.Close();
        }
    }

    public class ResoleStringToModel
    {
        public static T Resole<T>(string content)
        {
            //Type type = typeof(T);
            var getPropertyVal = content.Split(',').ToList();

            T result = Activator.CreateInstance<T>();
            PropertyInfo[] t = result.GetType().GetProperties();

            foreach (var item in t)
            {
                item.SetValue(item.Name, getPropertyVal.Where(w => item.Name.Contains(w)).First().Split(':')[1], null);
            }
            return result;
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

        public int AlarmsValue { get; set; }

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
