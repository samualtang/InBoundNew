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
    public partial class win_yxyreplenish : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public win_yxyreplenish()
        {
            InitializeComponent();
            seek();
        }

        private void seek()
        {





            String strsql = "  SELECT ROWNUM AS num, cigarettecode,cigarettename,ccount ,orderqty  FROM" +
                            "(SELECT line.cigarettecode,line.cigarettename,count(*) as ccount,SUM(line.quantity)AS orderqty FROM t_produce_orderline line " +
                           " WHERE line.allowsort='非标' GROUP BY line.cigarettecode,line.cigarettename ORDER BY orderqty desc)";

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
            dgVprint1.MainTitle = "今日异型烟汇总";
            //dgVprint1.SubTitle = "这是子标题，当然也可以不设的";

            dgVprint1.TableHeaderLeft = "长株潭烟草物流配送中心";
            dgVprint1.TableHeaderRight = "分拣日期：" + DateTime.Now.Date.ToShortDateString();
            dgVprint1.PaperLandscape = true;//用横向打印，默认是纵向哦
            dgVprint1.Print(orderdata);
        }

       
    }
}
