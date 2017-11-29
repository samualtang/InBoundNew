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
    public partial class w_hunhesort : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        String sign = "0";
        public w_hunhesort()
        {
            InitializeComponent();
            init();
            this.pager1.PageChanged += new WHC.Pager.WinControl.PageChangedEventHandler(pager1_PageChanged);
            this.pager1.ExportCurrent += new WHC.Pager.WinControl.ExportCurrentEventHandler(pager1_ExportCurrent);
            this.pager1.ExportAll += new WHC.Pager.WinControl.ExportAllEventHandler(pager1_ExportAll);
            //this.pager1.GetChildAtPoint(7).Visible = false;
            pager1.PageSize = 500;
            seek();


        }
        void pager1_PageChanged(object sender, EventArgs e)
        {
            seek();
        }

        void pager1_ExportCurrent(object sender, EventArgs e)
        {
        }

        void pager1_ExportAll(object sender, EventArgs e)
        {
        }    

        public void init()
        {
            

            ////初始化查询条件下拉框
            //DataTable conditiontable = new DataTable();
            //conditiontable.Columns.Add("value", typeof(string));
            //conditiontable.Columns.Add("txtvalue", typeof(string));

            //DataRow dr = conditiontable.NewRow();
            //dr["value"] = "itemno";
            //dr["txtvalue"] = "品牌代码";

            //DataRow dr1 = conditiontable.NewRow();
            //dr1["value"] = "itemname";
            //dr1["txtvalue"] = "品牌名称";

            //conditiontable.Rows.Add(dr);
            //conditiontable.Rows.Add(dr1);

            //this.box_type.DataSource = conditiontable;
            //this.box_type.DisplayMember = "txtvalue";
            //this.box_type.ValueMember = "value";
            //this.box_type.SelectedIndex = 0;
            seek();
        }

        private void seek()
        {

            String strsql = "SELECT  cigarettecode,cigarettename,sl from(select t.* ,rownum num from(select cigarettecode,cigarettename,sum(pokenum)sl from t_produce_poke p,t_produce_sorttrough s " + 
                "where p.troughnum=s.troughnum and p.machineseq in(43,44,45)group by cigarettecode,cigarettename order by sl desc)t"+
                " )tmp where  tmp.num>" + (pager1.CurrentPageIndex - 1) * pager1.PageSize + " and tmp.num<=" + pager1.CurrentPageIndex * pager1.PageSize +" order by tmp.num";
            //MessageBox.Show(strsql);
            int total = int.Parse(DataPublic.ExecuteScalar("SELECT count(*) FROM t_produce_poke p ,t_produce_sorttrough s where p.machineseq in(43,44,45) and p.troughnum=s.troughnum group by cigarettecode,cigarettename").ToString());
            this.
            Bind(strsql);
            pager1.RecordCount = total;
            this.pager1.InitPageInfo();
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
                if (sign == "0")
                {
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
                }

                this.codedata.DataSource = ds.Tables[0];
                this.codedata.AutoGenerateColumns = false;

                string columnwidths = pub.IniReadValue(this.Name, this.codedata.Name);
                if (columnwidths != "")
                {
                    string[] columns = columnwidths.Split(',');
                    int j = 0;
                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (codedata.Columns[i].Visible == true)
                        {
                            codedata.Columns[j].Width = Convert.ToInt32(columns[i]);
                            j = j + 1;
                        }
                    }
                }
                codedata.ClearSelection();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void win_brandrelative_Load(object sender, EventArgs e)
        {
            codedata.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            seek();
        }

        private void txt_keywd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                seek();
            }
        }

        private void codedata_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
            //int rowcount = this.codedata.SelectedCells.Count;
            //if (rowcount > 0)
            //{
            //    //MessageBox.Show("--");
            //    String cigarettecode = this.codedata.CurrentRow.Cells[1].Value.ToString();
            //    String count = this.codedata.CurrentRow.Cells[3].Value.ToString();
            //    try
            //    {
            //        if(count!="")
            //        int.Parse(count);
            //    }
            //    catch
            //    {
            //        MessageBox.Show("请输入整数");
            //    }
            //    try
            //    {
            //        Db.Open();
            //        String sql = "update highspeed.t_produce_sorttrough set mantissa='" + count + "' where cigarettecode='" + cigarettecode + "'";
            //        //String batchcodesql = "select count(*) from highspeed.t_produce_brandcoderelative where cigarettecode='" + itemno + "'";
            //        //DataTable dt = Db.Query(batchcodesql);
            //        //String count = dt.Rows[0][0].ToString();

            //        //if (count != "1")
            //        //{
            //        //    sql = "insert into highspeed.t_produce_brandcoderelative(cigarettecode,barcode)values('" + itemno + "','" + barcode + "')";
            //        //}
            //        int len = Db.ExecuteNonQuery(sql);
            //         MessageBox.Show("卷烟品牌对应尾数修改成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    }
            //    catch (SqlException se)
            //    {
            //        MessageBox.Show(se.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    finally
            //    {
            //        Db.Close();
            //        //this.Close();
            //    }
            //}
        }

        private void 打印_Click(object sender, EventArgs e)
        {
            dgVprint1.MainTitle = "混合道补货计划表";
            //dgVprint1.SubTitle = "这是子标题，当然也可以不设的";
           // dgVprint1.PaperLandscape = true;//用横向打印，默认是纵向

            dgVprint1.Print(codedata);
        }

        
    }
}
