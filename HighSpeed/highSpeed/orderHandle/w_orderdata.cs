using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using highSpeed.PubFunc;

namespace highSpeed.orderHandle
{
    public partial class win_orderdata : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public win_orderdata()
        {
            InitializeComponent();
            seek();
        }

        private void seek()
        {

            String strsql = "select rownum as num  , cigarettecode,cigarettename,  ccount,  orderqty  from (SELECT  cigarettecode,cigarettename,count(*) as ccount,SUM(quantity) AS orderqty    FROM t_produce_orderline GROUP BY cigarettecode,cigarettename ORDER BY orderqty   desc)"; 
                            
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
                Db.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void btn_print_Click(object sender, EventArgs e)
        {
            dgVprint2.MainTitle = "今日订单汇总";
            //dgVprint1.SubTitle = "这是子标题，当然也可以不设的";
            //dgVprint1.PageHeaderLeft = "白沙物流";
            //dgVprint1.PageHeaderRight = "分拣日期：" + DateTime.Now.Date.ToShortDateString();
            dgVprint2.TableHeaderLeft = "长株潭烟草物流配送中心";
            dgVprint2.TableHeaderRight = "分拣日期：" + DateTime.Now.Date.ToShortDateString();
            //dgVprint2.PaperLandscape = true;//用横向打印，默认是纵向哦
            dgVprint2.Print(orderdata);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dgVprint2.ExportDGVToExcel2(this.orderdata, "今日订单汇总", "orderinfo.xls", true);
        }
    }
}
