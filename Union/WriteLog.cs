using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Union
{
    public class WriteLog
    {
        private int fileSize;
        private string fileLogPath;
        private string logFileName;

        public WriteLog()
            {
            //初始化大于2M日志文件将自动删除;

            this.fileSize=2048*1024*2;//2M

            //默认路径

            this.fileLogPath = Application.StartupPath + "\\log\\";
            this.logFileName="log.txt";
            }

        public int FileSize
        {
            set
            {
                fileSize = value;
            }
            get
            {
                return fileSize;
            }
        }

        public string FileLogPath
        {
            set
            {
                this.fileLogPath = value;
            }
            get
            {
                return this.fileLogPath;
            }
        }

        public string LogFileName
        {
            set
            {
                this.logFileName = value;
            }
            get
            {
                return this.logFileName;
            }
        }


        public void Write(string Message)
        {
            this.Write(this.logFileName, Message);
        }

        public void Write(string LogFileName,string Message)
            {

            //DirectoryInfo path=new DirectoryInfo(LogFileName);
            //如果日志文件目录不存在,则创建
            if(!Directory.Exists(this.fileLogPath))
            {
            Directory.CreateDirectory(this.fileLogPath);
            }

            FileInfo finfo=new FileInfo(this.fileLogPath+LogFileName);
            if(finfo.Exists&&finfo.Length>fileSize)
            {
            finfo.Delete();
            }
            try
            {
            FileStream fs=new FileStream(this.fileLogPath+LogFileName,FileMode.Append);
            StreamWriter strwriter=new StreamWriter(fs);
            try
            {

            DateTime d=DateTime.Now;
            strwriter.WriteLine("时间:"+d.ToString());
            strwriter.WriteLine(Message);
            strwriter.WriteLine();
            strwriter.Flush();
            }
            catch(Exception ee)
            {
            Console.WriteLine("日志文件写入失败信息:"+ee.ToString()); 
            }
            finally
            {
            strwriter.Close();
            strwriter=null;
            fs.Close();
            fs=null;
            }
            }
            catch(Exception ee)
            {
                Console.WriteLine("日志文件没有打开,详细信息如下:");
            }
        }
 




        /*
        /// <summary>
        /// 写日志文件
        /// </summary>
        /// <param name="sMsg"></param>
        public static void Write(string sMsg)
        {
            if (sMsg != "")
            {
                //Random randObj = new Random(DateTime.Now.Millisecond);
                //int file = randObj.Next() + 1;
                string filename = DateTime.Now.ToString("yyyyMM") + ".log";
                try
                {
                    FileInfo fi = new FileInfo(Application.StartupPath + "\\log\\" + filename);
                    if (!fi.Exists)
                    {
                        using (StreamWriter sw = fi.CreateText())
                        {
                            sw.WriteLine(DateTime.Now + "\n" + sMsg + "\n");
                            sw.Close();
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = fi.AppendText())
                        {
                            sw.WriteLine(DateTime.Now + "\n" + sMsg + "\n");
                            sw.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }*/
    }
}
