using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using highSpeed.PubFunc;
using System.Data.SqlClient;

namespace highSpeed.baseData
{
    public partial class win_sortline : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public win_sortline()
        {
            InitializeComponent(); 
            seek();
        }
        
        private void seek()
        {
            String strsql = "SELECT rownum,linenum,linedesc,troughcount FROM t_produce_sortline t";

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

                this.sortlinedata.DataSource = ds.Tables[0];
                this.sortlinedata.AutoGenerateColumns = false;

                string columnwidths = pub.IniReadValue(this.Name, this.sortlinedata.Name);
                if (columnwidths != "")
                {
                    string[] columns = columnwidths.Split(',');
                    int j = 0;
                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (sortlinedata.Columns[i].Visible == true)
                        {
                            sortlinedata.Columns[j].Width = Convert.ToInt32(columns[i]);
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

        private void btn_close_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                Db.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void win_sortline_Load(object sender, EventArgs e)
        {
            sortlinedata.ClearSelection();
        }
    }
}
