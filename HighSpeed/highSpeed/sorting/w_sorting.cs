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

namespace highSpeed.sorting
{
    public partial class win_sorting : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public win_sorting()
        {
            InitializeComponent();
            seek();
        }

        private void seek()
        {
            //string time = this.orderdate.Text;
            //time=DateTime.Parse(time,"yyyy-MM-dd");

            String strsql = "SELECT xlmc,bzxx FROM bswlsq.t_sq_xlxx WHERE status='1' AND xlmc<0210 ORDER BY xlmc";
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

        private void w_sorting_Load(object sender, EventArgs e)
        {
            taskdata.ClearSelection();
        }

        private void btn_test_Click(object sender, EventArgs e)
        {
            
            String time = DateTime.Now.ToLongTimeString();
            for (int i = 0; i < 10;i++ )
            {
                this.list_data.Items.Add(i+"   "+time+"    ");
            }
            //MessageBox.Show(this.taskdata.Rows[1].Cells.Count.ToString());
            //DataTable dt = this.taskdata.DataSource as DataTable;
            //DataRow dr=dt.Rows[1];
            //MessageBox.Show(this.taskdata.Rows[1].Cells[1].Value.ToString());
            //this.taskdata.Rows[1].Cells["percent"].Value = "66.7%";
            //taskdata["percent", 1].Value = "66.7%";
            //taskdata.Rows[2][2].cellValue = "20";
            //this.taskdata.Refresh();
        }
    }
}
