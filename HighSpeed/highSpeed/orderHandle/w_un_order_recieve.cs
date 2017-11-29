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
using System.Data.OracleClient;

namespace highSpeed.orderHandle
{
    public partial class win_un_order_recieve : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public win_un_order_recieve()
        {
            InitializeComponent();
            string weekstr = DateTime.Now.DayOfWeek.ToString();
            //if (weekstr == "Monday") this.datePick.Value = DateTime.Today.AddDays(-3);
            //else if (weekstr == "Sunday") this.datePick.Value = DateTime.Today.AddDays(-2);
            //else this.datePick.Value = DateTime.Today.AddDays(-1);
            this.datePick.Value = DateTime.Today;
            
        }

        private void seek()
        {
            string time = this.datePick.Text;
            //time=DateTime.Parse(time,"yyyy-MM-dd");
            this.txt_codestr.Text = "";
            String strsql = "SELECT tmp1.dpid,count_hs,order_qty FROM "+
                            "(SELECT a.dpid,SUM(b.orderqty)as order_qty FROM dbm.twms_excpo a ,dbm.twms_excpoline b "+
                            "WHERE a.extnum=b.extnum  AND a.enterdate=to_date('" + time + "','yyyy-mm-dd') GROUP BY a.dpid)tmp1," +
                            "(SELECT a.dpid,count(*)as count_hs FROM dbm.twms_excpo a "+
                            "WHERE a.enterdate=to_date('" + time + "','yyyy-mm-dd') GROUP BY a.dpid)tmp2 " +
                            "WHERE tmp1.dpid=tmp2.dpid  order by tmp1.dpid";
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
                Db.Close();
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
            String codestr = this.txt_codestr.Text.Trim();
            DateTime time = DateTime.Parse(this.datePick.Value.ToString());
            String date = string.Format("{0:d}", time);
            OracleParameter[] sqlpara;
            string errcode = "", errmsg = "";

            //取是第几次接受数据

            String synseq = "1";
            String sql="select decode(max(synseq),'','0',max(synseq))+1 from highspeed.t_un_order";
            DataTable dbq = new DataTable();
            dbq = Db.Query(sql);
            if (dbq.Rows.Count > 0) {
                synseq=dbq.Rows[0][0].ToString();
            }


            if (codestr != "")
            {
                Db.Open();
                String[] code = codestr.Substring(1).Split(',');
                int len=code.Length;

                string indexstr = "";//记录已完成接收的车组code串



                for (int i = 0; i < len; i++ )
                {
                    panel2.Visible = true;
                    label2.Visible = true;
                    progressBar1.Visible = true;
                    int rcounts = ds.Tables[0].Rows.Count;
                    progressBar1.Value = 0;
                    Application.DoEvents();
                    if (i == 0) label2.Text = "正在接收" + code[i] + "车组订单数据...";
                    //MessageBox.Show(label2.Text);
                    sqlpara = new OracleParameter[5];
                    sqlpara[0] = new OracleParameter("p_time", date);
                    sqlpara[1] = new OracleParameter("p_routestr", code[i]);
                    sqlpara[2] = new OracleParameter("p_synseq", System.Int32.Parse(synseq));
                    sqlpara[3] = new OracleParameter("p_ErrCode", OracleType.VarChar,30);
                    sqlpara[4] = new OracleParameter("p_ErrMsg", OracleType.VarChar,500);

                    sqlpara[3].Direction = ParameterDirection.Output;
                    sqlpara[4].Direction = ParameterDirection.Output;

                    Db.ExecuteNonQueryWithProc("P_UN_ORDERRECEIVE", sqlpara);
                    //MessageBox.Show(date);
                    //MessageBox.Show(code[i]+"订单数据接收完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    errcode = sqlpara[3].Value.ToString();
                    errmsg = sqlpara[4].Value.ToString();
                    //进度条显示




                    progressBar1.Value = ((i + 1) * 100 / len);
                    progressBar1.Refresh();
                    String tmpstr="";

                    if (errcode == "1")
                    {
                        if (i + 1 < len) tmpstr = "正在接收" + code[i + 1] + "车组订单数据...";
                        else tmpstr = "";
                        label2.Text = code[i] + "车组订单数据接收完毕..." + tmpstr;
                        label2.Refresh();
                        indexstr = indexstr + "," + code[i];
                    }
                    else 
                    {
                        label2.Text = errmsg;
                        label2.Refresh();
                        break;
                    }
                    //MessageBox.Show(label2.Text);
                }

                //string msg = "订单";

                panel2.Visible = false;
                label2.Visible = false;
                progressBar1.Visible = false;
                this.lab_showinfo.Text = errmsg;
                MessageBox.Show(errmsg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.txt_codestr.Text = "";

                /*for (int i = 0; i < orderdata.RowCount;i++ )
                {
                    bool obj = (bool)this.orderdata.Rows[i].Cells[0].EditedFormattedValue;
                    if (obj) indexstr=indexstr +","+ i;
                }*/
                
                if (indexstr != "")
                {
                    indexstr = indexstr.Substring(1);
                    String[] indexArr = indexstr.Split(',');
                    DataTable dt_new = ds.Tables[0];
                    DataRowCollection drc = dt_new.Rows;
                    for (int j = 0; j < indexArr.Length; j++)
                    {
                        Console.Write(indexArr[indexArr.Length - 1 - j]);

                        drc.RemoveAt(Convert.ToInt32(indexArr.Length - 1 - j));
                        
                    }
                    this.orderdata.DataSource = dt_new;
                    this.orderdata.AutoGenerateColumns = false;
                }

                seek();
                
            }
            else 
            {
                MessageBox.Show("请至少选择一个要接收订单的车组!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void orderdata_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                bool obj = (bool)this.orderdata.CurrentRow.Cells[0].EditedFormattedValue;
                //MessageBox.Show(obj);

                String czcode = this.orderdata.CurrentRow.Cells[1].Value + "";
                String czcodestr = this.txt_codestr.Text;
                if (obj)
                {
                    if (!czcodestr.Contains(czcode))
                    {
                        czcodestr = czcodestr + "," + czcode;
                    }
                }
                else
                {
                    czcodestr = czcodestr.Replace("," + czcode, "");
                }
                this.txt_codestr.Text = czcodestr;
            }
        }

        private void btn_all_Click(object sender, EventArgs e)
        {
            String czcodestr = "";
            for (int i = 0; i < this.orderdata.RowCount;i++ )
            {
                orderdata.Rows[i].Cells[0].Value = "true";
                czcodestr = czcodestr + "," + orderdata.Rows[i].Cells[1].Value + "";
            }
            this.txt_codestr.Text = czcodestr;
        }
    }
}
