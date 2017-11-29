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
using System.IO;
using System.IO.Compression;

namespace highSpeed.orderHandle
{
    public partial class win_export_left : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public win_export_left()
        {
            InitializeComponent();
            seek();
        }

        private void seek()
        {
            //string time = this.orderdate.Text;
            //time=DateTime.Parse(time,"yyyy-MM-dd");

            String strsql = "SELECT synseq,sum(t.orderquantity) as qty,COUNT(*)as taskcount from highspeed.t_produce_task t where exportnum='L01' group BY t.synseq";
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

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void win_export_left_Load(object sender, EventArgs e)
        {
            orderdata.ClearSelection();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
            Db.Close();
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            int count = this.orderdata.SelectedRows.Count;
            if (count > 0)
            {
                String synseq = this.orderdata.SelectedRows[0].Cells["synseq"].Value + "";
                export(synseq);
            }
            else
            {
                MessageBox.Show("请点击选择您要导出的数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void export(String synseq)
        {
            String info = "";
            
            String tasknum = "",palletnum="", cuscode = "", cusname = "", itemno = "", itemname = "", quantity = "", address = "", regioncode = "", orderdate = "", companyname = "";
            String sql = "select a.companyname,trunc(plantime)as orderdate,a.tasknum,a.customercode,a.customername," +
                         "c.CONTACTADDRESS,b.cigarettecode,b.cigarettename,b.quantity,a.regioncode,a.palletnum from " +
                         "t_produce_task a,t_produce_taskline b,t_produce_customer c " +
                         "where a.tasknum=b.tasknum and a.customercode=c.CODE and a.exportnum='L01' and a.synseq='"+synseq+"' order by tasknum";
            Db.Open();
            DataTable table = Db.Query(sql);
            int len = table.Rows.Count;
            String[] infostr = new String[len];
            if (len > 0)
            {
                //DataRow row = new DataRow();
                for (int i = 0; i < len; i++)
                {
                    DataRow row = table.Rows[i];

                    tasknum = row["TASKNUM"].ToString();
                    cuscode = row["CUSTOMERCODE"].ToString();
                    cusname = row["CUSTOMERNAME"].ToString();
                    itemno = row["CIGARETTECODE"].ToString();
                    itemname = row["CIGARETTENAME"].ToString();
                    quantity = row["QUANTITY"].ToString();
                    regioncode = row["REGIONCODE"].ToString();
                    orderdate = row["ORDERDATE"].ToString();
                    orderdate = orderdate.Replace("/", "-");
                    companyname = row["COMPANYNAME"].ToString();
                    address = row["CONTACTADDRESS"].ToString();
                    palletnum = row["PALLETNUM"].ToString();

                    //infostr[i] = tasknum + taskseq + "," + tasknum + "," + cuscode + "," + cusname + "," + itemno + "," + itemname + "," + quantity + ",2," + seq + "," + regioncode + "," + regioncode + "," + orderdate + "," + orderdate + ",S1001,1";
                    info = info + companyname + ",," + orderdate + ",," + tasknum + ",," + cuscode + ",," + cusname + ",," + address + ",," + itemno + ",," + itemname + ",," + quantity + ",," + regioncode + ",,"+palletnum+"\r\n";

                }

                DateTime dt = DateTime.Now;
                String time = string.Format("{0:yyyyMMddHHmm}", dt);
                String filename = "L"+synseq+ time;
                StreamWriter sw = new StreamWriter("E:\\" + filename + ".txt", false, Encoding.UTF8);
               
                sw.WriteLine(info.Substring(0, info.Length - 1));
                sw.Close();//写入
                //CompressFile("E:\\" + filename + ".Order");
                MessageBox.Show("数据导出成功，文件为" + "E:\\" + filename + ".txt", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
