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
    public partial class w_schedulereport : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public w_schedulereport()
        {
            InitializeComponent();
            seek();
        }

        private void seek()
        {

            String strsql = @"select a.regioncode,a.cigarettecode,a.cigarettename,a.quantity,b.pokenum from(
select o.regioncode, line.cigarettecode,line.cigarettename,sum(quantity) quantity from t_produce_order o,t_produce_orderline line where o.state='排程'
group by o.regioncode, line.cigarettecode,line.cigarettename
) a left join
(
select cigarettecode,sum(pokenum) pokenum,regioncode
from(
select s.cigarettecode,p.pokenum,t.regioncode from t_produce_poke p,t_produce_sorttrough s,t_produce_task t where t.billcode=p.billcode and p.troughnum=s.troughnum 
and s.troughtype=10 and s.cigarettetype=20
union all
select p.cigarettecode,p.pokenum,t.regioncode  from t_un_poke p,t_produce_task t where p.billcode=t.billcode
)
group by  cigarettecode,regioncode
) b on a.regioncode=b.regioncode and a.cigarettecode=b.cigarettecode"; 
                            
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
            dgVprint2.MainTitle = "排程报表";
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
            dgVprint2.ExportDGVToExcel2(this.orderdata, "排程报表", "schedulereport.xls", true);
        }
    }
}
