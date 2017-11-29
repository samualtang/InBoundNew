using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using highSpeed.PubFunc;
using System.Data.OracleClient;

namespace highSpeed.orderHandle
{
    public partial class w_tuopanbuhuoplan : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public w_tuopanbuhuoplan()
        {
            InitializeComponent();
            //seek();
        }

        private void seek()
        {
            //string time = this.orderdate.Text;
            //time=DateTime.Parse(time,"yyyy-MM-dd");

            String strsql = "select * from t_produce_palletreplenishplan";
            this.taskdata.AutoGenerateColumns = false;
            //MessageBox.Show(strsql);
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
                Db = new DataBase();
                ds.Clear();
                //da.SelectCommand = new OracleCommand(sql, cn);
                //da.Fill(ds, "TB_Inpatient_info");


                ds = Db.QueryDs(sql);

                //进度条显示


              
                this.taskdata.DataSource = ds.Tables[0];
                this.taskdata.AutoGenerateColumns = false;

                string columnwidths = pub.IniReadValue(this.Name, this.taskdata.Name);
                if (columnwidths != "")
                {
                    string[] columns = columnwidths.Split(',');
                    int j = 0;
                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (taskdata.Columns[i].Visible == true)
                        {
                            taskdata.Columns[j].Width = Convert.ToInt32(columns[i]);
                            j = j + 1;
                        }
                    }
                }
                taskdata.ClearSelection();
                Db.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void btn_poke_Click(object sender, EventArgs e)
        {
            Db.Open();     
            Db.ExecuteNonQueryWithProc("autoreplenish_pallet", null);
            Db.Close();
            seek();
        }

        private void win_pokeplan_Load(object sender, EventArgs e)
        {
            this.taskdata.ClearSelection();
        }

    }
}
