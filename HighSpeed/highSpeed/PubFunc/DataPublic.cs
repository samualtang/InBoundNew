using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Collections;
using System.Configuration;
using System.Windows.Forms;

namespace highSpeed.PubFunc
{
    public delegate DataTable getCustomerHandler();
    /// <summary>
    /// DataPublic 的摘要说明。

   
    /// </summary>
    public class DataPublic
    {
        public static DataTable dataTable = new DataTable();
        public DataPublic()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //

        }

        public static DataTable getCustomer()
        {
            DataBase Db = new DataBase();
            try
            {
                Db.Open();
                dataTable = Db.Query("SELECT remarks,code,NAME,contactaddress,customerlevel,contact,contactphone FROM t_produce_customer");
            }
            finally
            {
                Db.Close();
            }
            return dataTable;
        }

        public static OracleDataReader ReadDb(string Sqlstr)
        {
            //***************************************************************************************
            //
            // 开发日期:   2005.03.05
            // 函数功能:   读取数据库函数


            // 参数说明:   Sqlstr：SQL语句
            // 函数返回值: 记录集


            //***************************************************************************************
            if (!ISSQL(Sqlstr))
            {
                return null;
            }

            DataBase Db = new DataBase();
            // try
            //{
            Db.Open();
            return (OracleDataReader)Db.ReadDb(Sqlstr);
            /*}
            finally
            {
                Db.Close();

            }*/

        }

        public static bool ExeDb(string Sqlstr)
        {
            //***************************************************************************************
            //
            // 开发日期:   2005.03.05
            // 函数功能:   执行SQL语句函数
            // 参数说明:   Sqlstr：SQL语句
            // 函数返回值: bool
            //***************************************************************************************
            if (!ISSQL(Sqlstr))
            {
                return false;
            }

            DataBase Db = new DataBase();
            try
            {
                Db.Open();
                return Db.ExecuteNonQuery(Sqlstr) > 0 ? true : false;

            }
            finally
            {
                Db.Close();

            }

        }

        public static bool ExeDbLog(string Sqlstr)
        {
            //***************************************************************************************
            //
            // 开发日期:   2005.03.05
            // 函数功能:   执行SQL语句函数，专为日志服务的
            // 参数说明:   Sqlstr：SQL语句
            // 函数返回值: bool
            //***************************************************************************************
            if (!ISSQL(Sqlstr))
            {
                return false;
            }

            DataBase Db = new DataBase();
            try
            {
                Db.Open();
                return Db.ExecuteNonQuery(Sqlstr) > 0 ? true : false;

            }
            finally
            {
                Db.Close();
            }

        }

        public static DataTable Query(string sql)
        {
            if (!ISSQL(sql))
            {
                return null;
            }

            if (sql == "")
                return null;

            DataBase Db = new DataBase();
            try
            {
                Db.Open();
                return Db.Query(sql);
            }
            catch
            {
                return null;
            }
            finally
            {
                Db.Close();
            }

        }

        public static object ExecuteScalar(string sql)
        {
            if (!ISSQL(sql))
            {
                return null;
            }

            DataBase Db = new DataBase();
            try
            {
                Db.Open();
                return Db.ExecuteScalar(sql);
            }
            catch
            {
                return null;
            }
            finally
            {
                Db.Close();
            }
        }


        //***************************************************************************************
        //
        // 开发日期:   2008.04.18
        // 函数功能:   调用数据库函数

        // 参数说明:   funname：函数名称，paramname:参数名称 instr：输入参数



        // 函数返回值: "-1"：出错

        public string getSqlFuncStr(string funname, string paramname, string instr)
        {
            string outstr;
            System.Data.SqlClient.SqlConnection myConnection = new System.Data.SqlClient.SqlConnection(ConfigurationSettings.AppSettings["ConnectionString"].ToString());
            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
            try
            {
                System.Data.SqlClient.SqlCommand sql = new SqlCommand(funname, myConnection);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.Add("@" + paramname, SqlDbType.VarChar);

                sql.Parameters["@" + paramname].Value = instr;

                //添加输出参数
                sql.Parameters.Add("@str", SqlDbType.VarChar);
                sql.Parameters["@str"].Direction = ParameterDirection.ReturnValue;


                sql.ExecuteNonQuery();
                //得到存储过程输出参数
                outstr = sql.Parameters["@str"].Value.ToString();
                sql.Dispose();
            }
            catch
            {
                outstr = "-1";
            }
            return outstr;



        }

        //***************************************************************************************
        //
        // 开发日期:   2005.03.05
        // 函数功能:   数据绑定DataGrid控件
        // 参数说明:   str_sql：SQL语句，table_name：数据表名，my_DataGrid：DataGrid控件名


        // 函数返回值: 无


        //***************************************************************************************
        public static void BindData(string str_sql, string table_name, DataGrid my_DataGrid)
        {
            if (!ISSQL(str_sql))
            {
                return;
            }

            DataBase Db = new DataBase();
            try
            {
                Db.Open();
                my_DataGrid.DataSource = Db.Query(str_sql).DefaultView;
               //my_DataGrid.DataBind();
            }
            catch (Exception exc)
            {//异常处理，当也面的页数不对时，到最后一页（当前也面在最后一页，或倒数第二页时，会出异常）
                throw exc;
            }
            finally
            {
                Db.Close();
                my_DataGrid.Dispose();
            }
        }

        //绑定特殊的DataGrid表（实现对DataGrid的控制，实现上一页、下一页等功能）


        public static void BindData(string str_sql, string table_name, DataGrid my_DataGrid, int StartIndex, int PageSize)
        {
            if (!ISSQL(str_sql))
            {
                return;
            }

            DataBase Db = new DataBase();
            DataSet myDataSet = new DataSet();
            try
            {
                Db.Open();
                Db.FullDataSet(str_sql, ref myDataSet, StartIndex, PageSize);
                my_DataGrid.DataSource = myDataSet.Tables[0].DefaultView;
               // my_DataGrid.DataBind();
            }
            catch (Exception err)
            {
                //throw err;
                SaveErrLog(" [" + str_sql + "] " + err.ToString());//保存错误日志

            }
            finally
            {
                Db.Close();
                myDataSet.Dispose();
            }
        }

        /*
        public static void BindData(string str_sql, string table_name, DataList my_DataList)
        {
            if (!ISSQL(str_sql))
            {
                return;
            }

            DataBase Db = new DataBase();
            try
            {
                Db.Open();
                my_DataList.DataSource = Db.Query(str_sql).DefaultView;
                my_DataList.DataBind();
            }
            catch (Exception err)
            {
                //throw err;
                SaveErrLog(" [" + str_sql + "] " + err.ToString());//保存错误日志

            }
            finally
            {
                Db.Close();
                my_DataList.Dispose();
            }
        }
         

        //绑定特殊的DATALIST表（实现对DATALIST的控制，实现上一页、下一页等功能）


        public static void BindData(string str_sql, string table_name, DataList my_DataList, int StartIndex, int PageSize)
        {
            if (!ISSQL(str_sql))
            {
                return;
            }

            DataBase Db = new DataBase();
            DataSet myDataSet = new DataSet();
            try
            {
                Db.Open();
                Db.FullDataSet(str_sql, ref myDataSet, StartIndex, PageSize);
                my_DataList.DataSource = myDataSet.Tables[0].DefaultView;
                my_DataList.DataBind();
            }
            catch (Exception err)
            {
                //throw err;
                SaveErrLog(" [" + str_sql + "] " + err.ToString());//保存错误日志

            }
            finally
            {
                Db.Close();
                myDataSet.Dispose();
            }
        }
        */

        // 执行一组 SQL 语句，并作为事务处理。		
        public static void ExecuteSqlTrans(ArrayList sqlList)
        {
            DataBase Db = new DataBase();
            try
            {
                Db.Open();
                Db.ExecuteSqlTrans(sqlList);
            }
            finally
            {
                Db.Close();
            }
        }

        // 执行一组 SQL 语句，并作为事务处理,true表示执行成功,false表示执行失败		
        public static bool ExecuteSqlTrans_Bool(ArrayList sqlList)
        {
            DataBase Db = new DataBase();
            try
            {
                Db.Open();
                if (Db.ExecuteSqlTrans(sqlList))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                Db.Close();
            }
        }

        //保存错误信息，到日志中


        public static void SaveErrLog(string err)
        {
            DataBase Db = new DataBase();
            Db.SaveErrLog(err);
        }

        /// <summary>
        /// SQL判断，是否为SQL注入
        /// </summary>
        /// <param name="strsql"></param>
        /// <returns></returns>
        public static bool ISSQL(string strsql)
        {
            /* string str_test = strsql.Substring(0,6).ToLower().Trim();
             //			if(str_test.IndexOf(";")!=-1)
             //			{
             //				return false;
             //			}

             if (str_test.Equals("insert"))
             {
                 return true;
             }

             if (str_test.Equals("delete"))
             {
                 return true;
             }

             if (str_test.Equals("update"))
             {
                 return true;
             }

             if (str_test.Equals("exec") )
             {
                 return true;
             }

             if (str_test.Equals("drop"))
             {
                 return true;
             }

             //			if(str_test.IndexOf("'''")!=-1)
             //			{
             //				return false;
             //			}
             //
             //			string [] str_a_test=str_test.Split('\'');
             //			if(str_a_test.Length%2==0)
             //			{
             //				return false;
             //			}*/

            return true;
        }

        /// <summary>
        /// 输入信息判断，防SQL注入
        /// </summary>
        /// <param name="str_info"></param>
        /// <returns></returns>
        public static bool ISInput(string str_info)
        {
            string str_test = str_info.Substring(6).ToLower();
            if (str_test.IndexOf(";") != -1)
            {
                return false;
            }

            if (str_test.IndexOf("insert") != -1)
            {
                return false;
            }

            if (str_test.IndexOf("delete") != -1)
            {
                return false;
            }

            if (str_test.IndexOf("update") != -1)
            {
                return false;
            }

            if (str_test.IndexOf("exec") != -1)
            {
                return false;
            }

            if (str_test.IndexOf("drop") != -1)
            {
                return false;
            }

            if (str_test.IndexOf("'''") != -1)
            {
                return false;
            }

            string[] str_a_test = str_test.Split('\'');
            if (str_a_test.Length % 2 == 0)
            {
                return false;
            }

            return true;
        }

    }
}
