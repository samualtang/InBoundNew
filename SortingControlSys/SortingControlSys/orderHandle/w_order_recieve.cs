using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SortingControlSys.PubFunc;
using System.Data.SqlClient;

namespace highSpeed.orderHandle
{
    public partial class win_order_recieve : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public win_order_recieve()
        {
            InitializeComponent();
        }

        private void seek()
        {
            string time = this.datePick.Text;
            //time=DateTime.Parse(time,"yyyy-MM-dd");
            
            String strsql = "SELECT tmp1.dpid,count_hs,order_qty FROM "+
                            "(SELECT a.dpid,SUM(b.orderqty)as order_qty FROM dbm.twms_excpo a ,dbm.twms_excpoline b "+
                            "WHERE a.extnum=b.extnum  AND a.enterdate=to_date('" + time + "','yyyy-mm-dd')AND stat='新增' GROUP BY a.dpid)tmp1," +
                            "(SELECT a.dpid,count(*)as count_hs FROM dbm.twms_excpo a "+
                            "WHERE a.enterdate=to_date('" + time + "','yyyy-mm-dd')AND stat='新增' GROUP BY a.dpid)tmp2 " +      
                            "WHERE tmp1.dpid=tmp2.dpid";
            MessageBox.Show(strsql);
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

                //进度条显示

                panel2.Visible = true;
                label2.Visible = true;
                progressBar1.Visible = true;
                int rcounts = ds.Tables[0].Rows.Count;
                progressBar1.Value = 0;
                for (int i = 0; i < rcounts; i++)
                {
                    Application.DoEvents();
                    progressBar1.Value = ((i + 1) * 100 / rcounts);
                    progressBar1.Refresh();
                    label2.Text = "正在读取数据..." + ((i + 1) * 100 / rcounts).ToString() + "%";
                    label2.Refresh();
                }
                panel2.Visible = false;
                label2.Visible = false;
                progressBar1.Visible = false;

                this.orderdata.DataSource = ds.Tables[0];
                this.orderdata.AutoGenerateColumns = false;

                string columnwidths = pub.IniReadValue(this.Name, this.orderdata.Name);
                if (columnwidths != "")
                {
                    string[] columns = columnwidths.Split(',');
                    int j = 0;
                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (orderdata.Columns[i].Visible == true)
                        {
                            orderdata.Columns[j].Width = Convert.ToInt32(columns[i]);
                            j = j + 1;
                        }
                    }
                }
                orderdata.ClearSelection();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void btn_search_Click(object sender, EventArgs e)
        {
            seek();
        }

        private void btn_recieve_Click(object sender, EventArgs e)
        {

        }
    }
}
