using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.OracleClient;

using System.IO;
using System.Runtime.InteropServices;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Data.SqlClient;
using System.Security;
using System.Security.Cryptography;
using System.Management;


namespace highSpeed.PubFunc
{
    #region PublicFun类
    /// <summary>
    /// PublicFun 的摘要说明。
    /// </summary>
    public class PublicFun
    {
        public static int collection_time;
        private const string sSecretKey = "Password";
        public static string IpAddress, MachineName, ls_flag_days, ls_flag_days_cs, ls_pl, ls_pl_cs, ls_start_date1, ls_start_date2, ls_end_date1, ls_end_date2, ls_start_date1_cs, ls_start_date2_cs, ls_end_date1_cs, ls_end_date2_cs;
        public static string sPath, ls_flag_sql, ls_sql_clinicinfo = "", ls_sql_clinicdetail = "", ls_sql_inpatientturn = "", ls_sql_inpatientinfo = "", ls_sql_inpatientdetail = "", ls_sql_cancel, lss_sql_clinicinfo = "", lss_sql_clinicdetail = "", lss_sql_inpatientinfo = "", lss_sql_inpatientdetail = "", lss_sql_tb_hisoffice = "", ls_sql_tb_patcharge = "", lss_sql_tb_patcharge = "";
        public static string ServerName, userId, sPwd, DataName, hisServerName, hisuserId, hissPwd, hisDataName;
        public static string connect, hisconnect, ls_sql_icd, ls_sql_item, his_sql_type;//连接服务器;
        public static string PubStrusername,/*登录用户账号*/ PubStruserid/*传用户管理参数*/, PubStruserempname;
        public static string PubStrrizhiid, PubStrrizhiname, PubStrrizhiremark, PubStrrizhitime;//传日志参数;
        public static string PubStrcollateid, PubStrcollatename, PubStrcollatespec, PubStrcollateunit, PubStrcollatejx, PubStrcollatezt, PubStrcollateshangchuanflag, PubStrcollatesheheflag;//HIS药品项目匹配参数
        public static string PubStrcollatenongheunits, PubStrcollatenongheid, PubStrcollatenonghename, PubStrcollatenonghespec, PubStrcollatenongheclass, PubStrcollatenonghestate, PubStrcollatenonghejixing, PubStrcollatenongheshangpm;//农合药品项目匹配参数
        public static string PubStrcollateJbid, PubStrcollateJbname, PubStrcollateJbshangchuanflag, PubStrcollateJbshenheflag, PubStrcollateJbtypeid, PubStrcollateJbitemcode;//HIS疾病参数;
        public static string PubStrcollateJbnongheid, PubStrcollateJbnonghename, PubStrcollateJbnonghemxbflag, PubStrcollateJbnonghelx, PubStrcollateJbnonghetypeid, PubStrcollateJbnongheitemcode;//农合疾病参数;
        public static string PubStrsetusername, PubStrsetuserpass, PubStrsetcenterno, PubStrsethospitalno, PubStrsetipaddress, PubStrsetport, PubStrsetremark;//传输参数设置
        public static string PubStrimportusername, PubStrimportuserpass, PubStrimportcenterno, PubStrimporthospitalno, PubStrimportipaddress, PubStrimportport, PubStrimportremark;//传输参数选择
        public static string PubStricdfindno, PubStricdfindfname, PubStricdfindname, PubStricdfindupdatedate, PubStricdfindmxbflag, PubStricdfindflag, PubStricdfindleibie;//传疾病查询参数
        
        /// <summary>
        /// 文件INI名称
        /// </summary>
        public string Path;

        #region 读写INI文件
        ////声明读写INI文件的API函数 
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);


        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static void EncryptFile(string sInputFilename, string sOutputFilename, string sKey)
        {
            try
            {
                FileStream fsInput = new FileStream(sInputFilename,
                                                    FileMode.Open,
                                                    FileAccess.Read);
                FileStream fsEncrypted = new FileStream(sOutputFilename,
                                                        FileMode.Create,
                                                        FileAccess.Write);
                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                ICryptoTransform desencrypt = DES.CreateEncryptor();
                CryptoStream cryptostream = new CryptoStream(fsEncrypted,
                                                             desencrypt,
                                                             CryptoStreamMode.Write);

                byte[] bytearrayinput = new byte[fsInput.Length];
                fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
                cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
                cryptostream.Close();
                fsInput.Close();
                fsEncrypted.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        public static void DecryptFile(string sInputFilename, string sOutputFilename, string sKey)
        {
            try
            {
                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                FileStream fsread = new FileStream(sInputFilename,
                                                   FileMode.Open,
                                                   FileAccess.Read);
                ICryptoTransform desdecrypt = DES.CreateDecryptor();
                CryptoStream cryptostreamDecr = new CryptoStream(fsread,
                                                                 desdecrypt,
                                                              CryptoStreamMode.Read);
                StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
                fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
                fsDecrypted.Flush();
                fsread.Close();
                fsDecrypted.Close();
                fsDecrypted = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //写INI文件
        public void IniWriteValue(string Section, string Key, string Value)
        {
            try
            {
                WritePrivateProfileString(Section, Key, Value, this.Path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //读取INI文件指定
        public string IniReadValue(string Section, string Key)
        {
            try
            {
                StringBuilder temp = new StringBuilder(2000);
                int i = GetPrivateProfileString(Section, Key, "", temp, 2000, this.Path);
                return temp.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 读写INI文件
        //写INI文件
        public static void IniWriteValue(string fileName, string Section, string Key, string Value)
        {
            try
            {
                WritePrivateProfileString(Section, Key, Value, fileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //读取INI文件
        public static string IniReadValue(string fileName, string Section, string Key)
        {
            try
            {
                StringBuilder temp = new StringBuilder(2000);
                int i = GetPrivateProfileString(Section, Key, "", temp, 2000, fileName);
                return temp.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        [System.Runtime.InteropServices.DllImport("NH_Interface")]
        public static extern bool ReadIC(out string ic_code, out string sf_code, out string man_code, out string man_name
            , out string hz_code, out string hz_name, out string my_money, out string family_money, out string my_bc_money
            , out string center_no, out string state);

        //类的构造函数，传递INI文件名
        public PublicFun(string inipath)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            Path = inipath;

        }

        #region 初始化
        public static Boolean start()
        {
            try
            {
                if (File.Exists(System.IO.Directory.GetCurrentDirectory().ToString() + "\\hospital_encrypt.ini"))
                {
                    DecryptFile(System.IO.Directory.GetCurrentDirectory().ToString() + "\\hospital_encrypt.ini", System.IO.Directory.GetCurrentDirectory().ToString() + "\\hospital.ini", sSecretKey);
                }
                else if (File.Exists(System.IO.Directory.GetCurrentDirectory().ToString() + "\\hospital.ini"))
                {
                    EncryptFile(System.IO.Directory.GetCurrentDirectory().ToString() + "\\hospital.ini", System.IO.Directory.GetCurrentDirectory().ToString() + "\\hospital_encrypt.ini", sSecretKey);
                }
                sPath = System.IO.Directory.GetCurrentDirectory().ToString() + "\\hospital.ini";
                PublicFun ini = new PublicFun(@sPath);
                ServerName = ini.IniReadValue("Database", "Data Source");
                userId = ini.IniReadValue("Database", "User ID");
                sPwd = ini.IniReadValue("Database", "Password");
                DataName = ini.IniReadValue("Database", "Initial Catalog");
                try
                {

                    connect = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + ServerName + ")(PORT=10001))(CONNECT_DATA=(SERVICE_NAME=" + DataName + ")));User Id=" + userId + ";Password=" + sPwd + ";";
                    Console.WriteLine(connect);

                    OracleConnection cn = new OracleConnection();
                    cn.ConnectionString = PublicFun.connect;
                  
                    cn.Open();
                   
                   
                    if (cn != null) { if (cn.State != ConnectionState.Closed) { cn.Close(); } }

                   
                }
                catch
                {
                    if (File.Exists(System.IO.Directory.GetCurrentDirectory().ToString() + "\\hospital.ini"))
                    {
                        //File.Delete(System.IO.Directory.GetCurrentDirectory().ToString() + "\\hospital.ini");
                    }
                    return false;
                }
                if (File.Exists(System.IO.Directory.GetCurrentDirectory().ToString() + "\\hospital.ini"))
                {
                    //File.Delete(System.IO.Directory.GetCurrentDirectory().ToString() + "\\hospital.ini");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        //判断表是否被修改
        public static int GetDtState(DataTable VarDt)
        {
            try
            {
                DataRow[] DrDel = VarDt.Select(null, null, DataViewRowState.Deleted);
                DataRow[] DrAdd = VarDt.Select(null, null, DataViewRowState.Added);
                DataRow[] DrMod = VarDt.Select(null, null, DataViewRowState.ModifiedCurrent);
                if (DrDel.Length > 0 || DrAdd.Length > 0 || DrMod.Length > 0)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void AddSQLStr(ref string sqlstr, string addstr)
        {
            try
            {
                if (addstr.Trim() != "")
                {
                    if (sqlstr.IndexOf(" where") == -1)
                    {
                        //加上where条件
                        sqlstr += " where " + addstr;
                    }
                    else
                    {
                        sqlstr += " and " + addstr;

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //判断文本框输入为数字
        //返回0表示通过，-1表示未通过
        //public static int VTextDecimal(System.Windows.Forms.TextBox VarText)
        //{
        //    decimal DcTemp;
        //    try
        //    {
        //        DcTemp = decimal.Parse(VarText.Text.Trim());
        //        return 0;
        //    }
        //    catch
        //    {
        //        return -1;
        //    }
        //}
        //验证文本框输入不为空
        //返回0表示通过，-1表示未通过
        //public static int VTextNoNull(System.Windows.Forms.TextBox VarText)
        //{
        //    if(VarText.Text.Length>0)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return -1;
        //    }
        //}




        #region 获取网卡
        /// <summary>
        /// 获取网卡
        /// </summary>
        /// <returns></returns>
        public static string GetNetCardMacAddress()
        {
            string str = "";
            /*
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            
            foreach (ManagementObject mo in moc)
            {
                if ((bool)mo["IPEnabled"] == true)
                    str = mo["MacAddress"].ToString();
            }
             */
            return str;
        }
        #endregion

        #region MD5
        /// <summary>
        /// MD5　16位加密 
        /// </summary>
        /// <param name="ConvertString"></param>
        /// <returns></returns>
        public static string GetMd5Str(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }

        /// <summary>
        /// MD5　32位加密 
        /// </summary> 
        /// <param name="str"></param> 
        /// <returns></returns> 
        public static string GetMd5Str32(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            for (int i = 0; i < s.Length; i++)
            {
                pwd = pwd + s[i].ToString("x").PadLeft(2, '0');
            }
            return pwd;
        }
        #endregion


        #region 效验注册码
        /// <summary>
        /// 效验注册码
        /// </summary>
        /// <param name="markCode">机器码</param>
        /// <param name="regCode">注册码</param>
        /// <returns></returns>
        public static bool CheckRegCode(string markCode, string regCode)
        {
            bool blResult = false;

            if (BuilderRegCode(markCode, sSecretKey) == regCode)
                blResult = true;

            return blResult;
        }
        #endregion

        #region 生成注册码
        /// <summary>
        /// 生成注册码
        /// </summary>
        /// <param name="markCode">机器码</param>
        /// <param name="keyCode">密钥</param>
        /// <returns></returns>
        public static string BuilderRegCode(string markCode, string keyCode)
        {
            string sResult = "";

            string keycode = keyCode;
            string[] mark = markCode.Split(':');
            if (keycode.Length < mark.Length)
                keycode = keycode + "KEYU_DENG";

            string tmp = markCode.Replace(":", "");
            int add = 0;
            for (int i = 0; i < tmp.Length; i++)
            {
                int value = Convert.ToInt32(char.Parse(tmp.Substring(tmp.Length - i - 1, 1)));
                if (value >= 48 && value <= 57)
                {
                    if (i % 2 == 0)
                        value = value + 18;
                    else
                        value = value + 19;
                }
                else if (value >= 65 && value <= 90)
                {
                    if (i % 2 == 0)
                    {
                        if (value <= 77)
                        {
                            value = value + 45;
                        }
                        else
                        {
                            value = value + 20;
                        }
                    }
                    else
                    {
                        if (value <= 77)
                        {
                            value = value + 13;
                        }
                        else
                        {
                            value = value + 25;
                        }
                    }
                }
                else if (value >= 97 && value <= 122)
                {
                    if (value <= 108)
                    {
                        if (i % 2 == 0)
                            value = value - 49;
                        else
                            value = value + 10;
                    }
                    else
                    {
                        if (i % 2 == 0)
                            value = value - 11;
                        else
                            value = value - 8;
                    }
                }
                if (i % 2 == 0)
                {
                    if (add < 4)
                    {
                        int addvalue = Convert.ToInt32(char.Parse(keycode.Substring(add, 1)));
                        if (add % 2 == 0)
                            sResult = Convert.ToChar(addvalue) + sResult + Convert.ToChar(value);
                        else
                            sResult = sResult + Convert.ToChar(value) + Convert.ToChar(addvalue);
                    }
                    else
                    {
                        sResult = sResult + Convert.ToChar(value);
                    }
                    add = add + 1;
                }
                else
                    sResult = sResult + Convert.ToChar(value);
            }

            tmp = sResult;
            sResult = "";
            for (int i = 0; i < tmp.Length; i++)
            {
                if ((i + 1) % 4 == 0)
                {
                    sResult = sResult + tmp.Substring(i, 1) + "-";
                }
                else
                {
                    sResult = sResult + tmp.Substring(i, 1);
                }
            }
            sResult = sResult.Substring(0, sResult.Length - 1);

            return sResult;
        }
        #endregion


    }
    #endregion


}
