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
using InBound;

namespace highSpeed.orderHandle
{
    public partial class win_order_Union : Form
    {
       
        w_main wm = new w_main();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        public delegate void HandleRecieveing(int falge,bool isOrnot);
      
        HandleRecieveing handlerecieve;
       
        bool isRecieve = false;
        public win_order_Union()
        {
            InitializeComponent();
            handlerecieve += wm.GetSonFormState;
            string weekstr = DateTime.Now.DayOfWeek.ToString();
            this.datePick.Value = DateTime.Today;
            
        }

        private void seek()
        {
            string time = this.datePick.Text;
            this.txt_codestr.Text = "";
            String strsql = "with lst as (SELECT routecode,SUM(totalqty)as order_qty,count(1) as count_hs FROM t_wms_shiporder " +
                            "WHERE   orderdate=to_date('" + time + "','yyyy-mm-dd') GROUP BY routecode) select rownum,lst.* from lst";

          

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
                DataSet ds = new DataSet();
                DataBase Db = new DataBase();
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


        private void Bind2(string sql)
        {
            try
            {
                DataSet ds = new DataSet();
                DataBase dbbase = new DataBase();
                ds.Clear();
                //da.SelectCommand = new OracleCommand(sql, cn);
                //da.Fill(ds, "TB_Inpatient_info");


                ds = dbbase.QueryDs(sql);

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

                this.dgvUnionOrderINfo.DataSource = ds.Tables[0];
                this.dgvUnionOrderINfo.AutoGenerateColumns = false;

                //string columnwidths = pub.IniReadValue(this.Name, this.orderdata.Name);
                //if (columnwidths != "")
                //{
                //    string[] columns = columnwidths.Split(',');
                //    int j = 0;
                //    for (int i = 0; i < columns.Length; i++)
                //    {
                //        if (orderdata.Columns[i].Visible == true)
                //        {
                //            orderdata.Columns[j].Width = Convert.ToInt32(columns[i]);
                //            j = j + 1;
                //        }
                //    }
                //}
                dgvUnionOrderINfo.ClearSelection();
                dbbase.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void btn_search_Click(object sender, EventArgs e)
        {
            seek();
       
            //btn_recieve.Enabled = true;
        }

        private void btn_recieve_Click(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(txt_codestr.Text))
                {
                     String codestr = this.txt_codestr.Text.Trim().Substring(1); //获取选中的车组
                 

          
                 

                    string time = this.datePick.Text;//获取选中的时间
                    string strsql = "select  count( ship.orderno) 户数, sum(qty ) 销量 ,shiline.itemname 卷烟名称,sorts.machineseq 物理通道号, sorts.troughnum 通道号  from t_wms_shiporder ship , t_wms_shiporderline shiline , t_produce_sorttrough sorts " +
     " where ship.orderno = shiline.orderno and shiline.item_id = sorts.cigarettecode and ship.orderdate = to_date('" + time + "','yyyy-mm-dd') and routecode in(" + codestr + ") " +
     " and sorts.troughtype  = 10 and sorts.cigarettetype  in(" + cigerType + ") and sorts.groupno in (1,2,3,4,5,6,7,8) and sorts.state = 10  " +
     " and (sorts.machineseq > 3000 or sorts.machineseq <2000) " +
    " group by shiline.itemname, sorts.troughnum ,sorts.machineseq,sorts.cigarettetype " +
    " order by 销量 desc ,sorts.machineseq ";
                    Bind2(strsql);
                    lab_showinfo.Text = "查询了  " + codestr + "    车组";
                }
                else
                {
                    MessageBox.Show("请至少选择一个要查询的车组!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            catch (Exception ex)
            {
                writeLog.Write("查询的车组异常" + ex.Message);

            }

        }

        public WriteLog writeLog = WriteLog.GetLog();
        private void orderdata_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                bool obj = (bool)this.orderdata.CurrentRow.Cells[0].EditedFormattedValue;
             
                String czcode = this.orderdata.CurrentRow.Cells[2].Value + "";//modify by tjl
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
                    czcodestr = czcodestr.Replace( czcode+",", "");
                }
                this.txt_codestr.Text = czcodestr;
            }
        }

        private void btn_all_Click(object sender, EventArgs e)
        {
            if (btn_all.Text == "全选")
            {
                String czcodestr = "";
                for (int i = 0; i < this.orderdata.RowCount; i++)
                {
                    orderdata.Rows[i].Cells[0].Value = "true";
                    czcodestr = czcodestr + "," + orderdata.Rows[i].Cells[2].Value + "";
                }
                this.txt_codestr.Text = czcodestr;
                btn_all.Text = "反选";
            }
            else
            {
                for (int i = 0; i < this.orderdata.RowCount; i++)
                {
                    orderdata.Rows[i].Cells[0].Value = "false"; 
                }
                this.txt_codestr.Text = "";
                btn_all.Text = "全选";

            }
        }

        private void win_order_recieve_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
      string cigerType = "20,30,40";//卷烟类型
        private void cmbCigerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCigerType.SelectedIndex == 0)
            {
                cigerType = "20,30,40";
            }
            else  if (cmbCigerType.SelectedIndex == 1)
            {
                cigerType = "20";
            }
            else if (cmbCigerType.SelectedIndex == 2)
            {
                cigerType = "30,40";
            }
            else
            {
                cigerType = "20,30,40";
            }
        }

        private void win_order_Union_Load(object sender, EventArgs e)
        {
            cmbCigerType.SelectedIndex = 0;
        }
    }
}
