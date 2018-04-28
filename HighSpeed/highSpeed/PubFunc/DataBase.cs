using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.IO;
using System.Collections;
namespace highSpeed.PubFunc
{
    class DataBase
    {
        #region 公有成员
        //数据连接词




        public const string connectionString1 = "server=CHUNFEN;database=hnxyw;uid=sa;pwd=sa;";
        #endregion

        #region 私有成员

        private IDbConnection innerConnection;
        private IDbDataAdapter innerDataAdapter;
        private IDbCommand innerCommand;
        private IDataReader innerDataReader;
        private IDbTransaction innerTransaction;
        //private string connectionString="Provider=MSDAORA.1;Data Source=xmove30;Max Pool Size=100;Connection Lifetime=0;Min Pool Size=6;Pooling=true;user id=webxmovep;password=webxmovep";
        private static string connStr = PublicFun.connect;

        private OracleConnection OraConn=null;
        #endregion

        /// <summary>
        /// 连接字符串



        #region 数据库操作

        private void Conn()
        {
            try
            {
                OraConn = new OracleConnection(PublicFun.connect);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        private void CloseDBConnecion()
        {
            try
            {
                if (OraConn != null) { if (OraConn.State != ConnectionState.Closed) { OraConn.Close(); } }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        /// </summary>
        public string ConnStr
        {
            get
            {
                if (connStr != null)
                    return connStr;
                //return ConfigurationManager.
                return "";// ConfigurationSettings.AppSettings["ConnectionString"].ToString();
            }
        }

        /// <summary>
        /// 数据库操作命令对象




        /// </summary>
        public OracleCommand OracleCommandObject
        {
            get { return (OracleCommand)this.innerCommand; }
        }
        /// <summary>
        /// 数据库连接对象




        /// </summary>
        public OracleConnection OracleConnectionObject
        {
            get { return (OracleConnection)this.innerConnection; }
        }

        #region 构造函数




        public DataBase()
        {
            if (connStr == null)
            {
                connStr = ConnStr;
            }
            CreateConnectionObject();
        }
        #endregion

        #region 创建一系统的连接对象




        /// <summary>
        /// 创建一系统的连接对象




        /// </summary>
        private void CreateConnectionObject()
        {
            //创建一个连接对象




            innerConnection = new OracleConnection(connStr);
            //创建一个命令对象




            innerCommand = innerConnection.CreateCommand();
            innerDataAdapter = new OracleDataAdapter();
            //为数据适配器关联数据命令




            innerDataAdapter.SelectCommand = innerCommand;
        }
        #endregion

        #region 打开数据库连接




        /// <summary>
        /// 打开数据库连接




        /// </summary>
        public void Open()
        {
            if (innerConnection.State != ConnectionState.Open)
                innerConnection.Open();
        }

        #endregion

        #region 关闭数据库连接




        /// <summary>
        /// 关闭数据库连接




        /// </summary>
        public void Close()
        {
            if (innerConnection.State == ConnectionState.Open)
                innerConnection.Close();
            if (innerCommand != null)
            {
                innerCommand.CommandText = "";
                //innerCommand.CommandType = CommandType.;
            }
        }
        #endregion

        #region 开始一个新事务
        /// <summary>
        /// 开始一个新事务
        /// </summary>
        public void BeginTransaction()
        {
            if (innerConnection != null && innerConnection.State == ConnectionState.Open)
            {
                innerTransaction = ((OracleConnection)innerConnection).BeginTransaction();
                ((OracleCommand)innerCommand).Transaction = (OracleTransaction)innerTransaction;
            }
            else
                throw new ArgumentNullException("连接已关闭");
        }
        #endregion

        #region 提交事务
        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            if (innerTransaction != null)
                innerTransaction.Commit();
        }
        #endregion

        #region 回滚事务
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            if (innerTransaction != null)
                innerTransaction.Rollback();
        }
        #endregion


        /// <summary>
        /// 通过执行 SQL 语句获取数据表。




        /// </summary>
        /// <param name="sql"> 要执行的 SQL 语句。</param>
        /// <returns>
        /// 获取的数据表。




        /// </returns>
        public DataTable Query(string sql)
        {
            DataSet ds = null;
            DataTable dt = null;
            try
            {
                ds = new DataSet();
                dt = new DataTable();
                innerCommand.Parameters.Clear();
                innerCommand.CommandType = CommandType.Text;
                innerCommand.CommandText = sql;
                innerDataAdapter.Fill(ds);
            }
            catch (Exception err)
            {
                throw err;
            }
            dt = ds.Tables[0].Copy();
            return dt;
        }

        public DataSet QueryDs(string sql)
        {
            DataSet ds = null;

            try
            {
                ds = new DataSet();
                innerCommand.Parameters.Clear();
                innerCommand.CommandType = CommandType.Text;
                innerCommand.CommandText = sql;
                innerDataAdapter.Fill(ds);
            }
            catch (Exception err)
            {
                throw err;
            }

            return ds;
        }
        /// <summary>
        /// 使用此方法时,最好使用如下语句,以使在忘记关闭DataReader对象时,及时关闭连接及资源




        /// 使用例子:
        /// Db.Open();
        /// SqlDataReader Dr=(SqlDataReader)Db.ReadDb(strCmd);
        /// </summary>
        /// <param name="Sqlstr"></param>
        /// <returns></returns>
        public IDataReader ReadDb(string Sqlstr)
        {
            try
            {
                innerCommand.CommandText = Sqlstr;
                innerDataReader = innerCommand.ExecuteReader(CommandBehavior.CloseConnection);
                return innerDataReader;
            }
            catch (Exception err)
            {
                //throw err;

                SaveErrLog(" [" + Sqlstr + "] " + err.ToString());//保存错误日志

                return null;
            }
        }

        public IDataReader ReadDb_Back(string Sqlstr)
        {//这个函数是后台用
            try
            {
                innerCommand.CommandText = Sqlstr;
                innerDataReader = innerCommand.ExecuteReader(CommandBehavior.CloseConnection);
                return innerDataReader;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数。




        /// </summary>
        /// <param name="sql">需要执行的SQL语句</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql)
        {
            int intRet = 0;
            try
            {
                innerCommand.CommandText = sql;
                intRet = innerCommand.ExecuteNonQuery();
                return intRet;
            }
            catch (Exception err)
            {

                SaveErrLog(" [" + sql + "] " + err.ToString());//保存错误日志

                return 0;
            }
        }
        public int ExecuteNonQuery(string sql,out string errorMsg)
        {
            int intRet = 0;
            try
            {
                innerCommand.CommandText = sql;
                intRet = innerCommand.ExecuteNonQuery();
                errorMsg = "";
                return intRet;
            }
            catch (Exception err)
            {

                SaveErrLog(" [" + sql + "] " + err.ToString());//保存错误日志
                errorMsg = " [" + sql + "] " + err.ToString();
                return 0;
            }
        }
        public int ExecuteNonQuery_Back(string sql)
        {//这个函数是后台用
            int intRet = 0;
            try
            {
                innerCommand.CommandText = sql;
                intRet = innerCommand.ExecuteNonQuery();
                return intRet;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="sqlSpName">储存过程名</param>
        /// <param name="dbParams">参数</param>
        public void ExecuteNonQueryWithProc(string sqlSpName, OracleParameter[] dbParams)
        {
            try
            {
                innerCommand.CommandText = sqlSpName;
                innerCommand.CommandType = CommandType.StoredProcedure;
                innerCommand.Parameters.Clear();
                if (dbParams != null)
                {
                    foreach (OracleParameter dbParam in dbParams)
                    {
                        innerCommand.Parameters.Add(dbParam);
                    }
                }

                innerCommand.ExecuteNonQuery();
               // this.Commit();
            }
            catch (Exception err)
            {
                //throw err;
                //this.Rollback();
                SaveErrLog(" [" + sqlSpName.ToString() + "] " + err.ToString());//保存错误日志
            }
        }

        /// <summary>
        /// 填充数据集,可以指定填充起始位置，及填记录数
        /// </summary>
        /// <param name="sqlstr">sql脚本</param>
        /// <param name="ds">数据集</param>
        /// <param name="StartIndex">起始页</param>
        /// <param name="PageSize">页大小</param>
        public void FullDataSet(string sqlstr, ref DataSet ds, int StartIndex, int PageSize)
        {
            try
            {
                innerCommand.CommandText = sqlstr;
                innerDataAdapter.SelectCommand = innerCommand;
                ((OracleDataAdapter)innerDataAdapter).Fill(ds, StartIndex, PageSize, "MyTable");
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// 执行存储过程填充数据集




        /// </summary>
        /// <param name="ProcName">存储过程名</param>
        /// <param name="dbParams">参数</param>
        /// <returns></returns>
        public DataSet FullDataSetWithPro(string ProcName, SqlParameter[] dbParams)
        {
            DataSet ds = null;
            try
            {
                ds = new DataSet();
                innerCommand.CommandText = ProcName;
                innerCommand.CommandType = CommandType.StoredProcedure;
                innerCommand.Parameters.Clear();
                if (dbParams != null)
                {
                    foreach (SqlParameter dbParam in dbParams)
                    {
                        innerCommand.Parameters.Add(dbParam);
                    }
                }

                innerDataAdapter.Fill(ds);
            }
            catch (Exception err)
            {
                throw err;
            }
            return ds;
        }

        /// <summary>
        /// 执行Sql语句,并返回查询结果中第一行第一列的值,忽略额外的行或列
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <returns>返回Obj对象,在使用时应对其进行拆箱操作</returns>
        public object ExecuteScalar(string sql)
        {
            object obj;
            try
            {
                innerCommand.CommandText = sql;
                obj = innerCommand.ExecuteScalar();
                return obj;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message, err);
            }
        }


        /// <summary>
        /// 执行一组 SQL 语句，并作为事务处理。




        /// </summary>
        /// <param name="sqlList"></param>
        /// <returns></returns>
        public bool ExecuteSqlTrans(ArrayList sqlList)
        {
            //标志位,表示执行是否成功
            bool blResult = false;

            try
            {
                innerTransaction = innerConnection.BeginTransaction();
                innerCommand.Transaction = innerTransaction;
                for (int i = 0; i < sqlList.Count; i++)
                {
                   // if (!util.DataPublic.ISSQL(sqlList[i].ToString()))
                    {
                        //回滚
                        innerTransaction.Rollback();
                        return false;
                    }
                    innerCommand.CommandText = sqlList[i].ToString();
                    innerCommand.ExecuteNonQuery();
                }
                //提交数据库事务




                innerTransaction.Commit();
                blResult = true;
            }
            catch (Exception err)
            {
                blResult = false;
                //回滚
                innerTransaction.Rollback();
                //throw err;

                SaveErrLog(" [" + sqlList[0].ToString() + "] " + err.ToString());//保存错误日志
            }

            return blResult;

        }


        /// <summary>
        /// 使用事务执行存储过程
        /// </summary>
        /// <param name="ProcName">存储过程名</param>
        /// <param name="parameterValues">参数列表</param>
        public void ExecuteProcWithTransaction(string ProcName, params SqlParameter[] parameterValues)
        {
            try
            {
                innerCommand.CommandText = ProcName.Trim();
                if (innerTransaction == null)
                    this.BeginTransaction();
                innerCommand.CommandType = CommandType.StoredProcedure;
                innerCommand.Parameters.Clear();
                foreach (SqlParameter Parameter in parameterValues)
                {
                    innerCommand.Parameters.Add(Parameter);
                }
                innerCommand.ExecuteNonQuery();

                this.Commit();
            }
            catch (Exception err)
            {
                this.Rollback();
                //throw new Exception(err.Message,err);
                SaveErrLog(" [" + ProcName.ToString() + "] " + err.ToString());//保存错误日志
            }
        }

        /// <summary>
        /// 返回输入/输出参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">大小</param>
        /// <param name="objValue">值</param>
        /// <returns></returns>
        /* public static SqlParameter MakeParam(string paramName, SqlType dbType, int size, object objValue)
         {
             SqlParameter param;
             if (size > 0)
                 param = new SqlParameter(paramName, dbType, size);
             else
                 param = new SqlParameter(paramName, dbType);
             param.Value = objValue;
             return param;
         }*/

        //保存错误信息，到本地文件LOG下




        public void SaveErrLog(string err)
        {
            //			CAppGlobal.m_log.error("DataBase", err);
        }

        #region IDisposable 成员

        protected void Disponse(bool disponse)
        {
            if (disponse)
            {
                if (innerConnection.State == ConnectionState.Open)
                    innerConnection.Close();
                if (innerDataReader != null)
                    innerDataReader.Close();
                if (innerCommand != null)
                    innerCommand.Dispose();
            }
            innerCommand = null;
            innerConnection = null;
            innerDataAdapter = null;
            innerDataReader = null;
            innerTransaction = null;
        }

        ~DataBase()
        {
            Disponse(false);
        }

        #endregion

    }

}
