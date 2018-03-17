using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace InBound.Business
{
  public  class BaseService
    {


      public static decimal GetSeq(String sql)
      {
          using (Entities context = new Entities())
          {
              return context.ExecuteStoreQuery<decimal>(sql).First();
          }

      }
      public static decimal GetSeq()
      {
          using (Entities context = new Entities())
          {
              return context.ExecuteStoreQuery<decimal>("select SEQ_PalletNO.Nextval from dual").First();
          }

      }
      public static DataTable Query(string sqlString, params object[] dbParams)
      {
         
          using (Entities context = new Entities())
          {
              context.ExecuteStoreQuery<int>(sqlString);
              DataSet _ds = new DataSet();
              using (DbCommand cmd = context.Connection.CreateCommand())
              {
                  if (cmd.Connection.State == System.Data.ConnectionState.Closed)
                  {
                      cmd.Connection.Open();
                  }
                  cmd.CommandType = System.Data.CommandType.Text;
                  if(dbParams!=null)
                  cmd.Parameters.AddRange(dbParams);
                  cmd.CommandText = sqlString;
                  SqlDataAdapter sda = new SqlDataAdapter();
                  sda.SelectCommand = (SqlCommand)cmd;
                  sda.Fill(_ds);
                  if (cmd.Connection.State != System.Data.ConnectionState.Closed)
                  {
                      cmd.Connection.Close();
                  }
              }
              return _ds.Tables.Count > 0 ? _ds.Tables[0] : null;
          }
      }
    }
}
