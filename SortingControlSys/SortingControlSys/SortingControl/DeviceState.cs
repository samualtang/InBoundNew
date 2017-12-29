using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using InBound.Business;

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

            }
            fileOper.Write(compStrs, deviceNo);
        }
    }

    public class DeviceStateInfoModel
    {
        public string DeviceNo { get; set; }
        public string DeviceName { get; set; }
        public string ErrInfo { get; set; }
    }


    public class SaveFileHandler
    {
        private string fileLogPath;
        private string dataFile;

        public SaveFileHandler()
        {
            this.fileLogPath = Application.StartupPath + "\\DeviceState\\";
            this.dataFile = "data.txt";
        }


        public string ReadLastInfo(string deviceNo = null)
        {
            if (!File.Exists(Path.Combine(fileLogPath + deviceNo + dataFile)))
                return "";

            FileStream fs = new FileStream(Path.Combine(fileLogPath + deviceNo + dataFile), FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);//解决写入文件乱码   

            string line = string.Empty;
            StringBuilder sb = new StringBuilder();
            line = sr.ReadToEnd();
            sb.Append(line);
            sr.Close();
            fs.Close();
            return sb.ToString();
        }

        public void Write(string content, string deviceNo)
        {
            if (!Directory.Exists(fileLogPath))
            {
                Directory.CreateDirectory(fileLogPath);//.CreateDirectory(path);
            }

            FileStream fs = new FileStream(fileLogPath + deviceNo + dataFile, FileMode.Create);
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
