using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using highSpeed.PubFunc;

namespace highSpeed.baseData
{
    public partial class win_region : Form
    {
        DataSet ds = new DataSet();
        DataSet detail_ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public win_region()
        {
            InitializeComponent();
            seek();
        }

        private void seek()
        {
            String strsql = "select rownum,routecode,routename,deptname from t_sys_routeinfo info ,t_sys_dept dept where info.deptid=dept.id";
            
            Bind(strsql);
        }
        #region 查询
        /// <summary>
        /// 绑定DataGridView1
        /// </summary>
        /// <param name="sql">要查询的sql</param>
        private void Bind(string sql)
        {
            try
            {
                ds.Clear();
                //da.SelectCommand = new OracleCommand(sql, cn);
                //da.Fill(ds, "TB_Inpatient_info");


                ds = Db.QueryDs(sql);

                this.regiondata.DataSource = ds.Tables[0];
                this.regiondata.AutoGenerateColumns = false;

                string columnwidths = pub.IniReadValue(this.Name, this.regiondata.Name);
                if (columnwidths != "")
                {
                    string[] columns = columnwidths.Split(',');
                    int j = 0;
                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (regiondata.Columns[i].Visible == true)
                        {
                            regiondata.Columns[j].Width = Convert.ToInt32(columns[i]);
                            j = j + 1;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
