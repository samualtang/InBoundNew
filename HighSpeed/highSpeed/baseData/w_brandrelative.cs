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
    public partial class win_brandrelative : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        String sign = "0";
        public win_brandrelative()
        {
            InitializeComponent();
            init();
            seek();
        }

        public void init()
        {
            

            //初始化查询条件下拉框
            DataTable conditiontable = new DataTable();
            conditiontable.Columns.Add("value", typeof(string));
            conditiontable.Columns.Add("txtvalue", typeof(string));

            DataRow dr = conditiontable.NewRow();
            dr["value"] = "itemno";
            dr["txtvalue"] = "品牌代码";

            DataRow dr1 = conditiontable.NewRow();
            dr1["value"] = "itemname";
            dr1["txtvalue"] = "品牌名称";

            conditiontable.Rows.Add(dr);
            conditiontable.Rows.Add(dr1);

            this.box_type.DataSource = conditiontable;
            this.box_type.DisplayMember = "txtvalue";
            this.box_type.ValueMember = "value";
            this.box_type.SelectedIndex = 0;

        }

        private void seek()
        {
            string types = this.box_type.SelectedValue + "";
            string keywd = this.txt_keywd.Text.Trim();
            String tmp = "";
            sign = "0";
            if (types != null && types != "" && keywd != null && keywd != "")
            {
                tmp = " and " + types + " like'%" + keywd + "%'";
                sign = "1";
            }
            String strsql = "SELECT rownum,itemno,itemname,bigbox_bar " +
                            "from t_wms_item where 1=1 " +tmp;
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
            
            int rowcount = this.codedata.SelectedCells.Count;
            if (rowcount > 0)
            {
                //MessageBox.Show("--");
                String itemno = this.codedata.CurrentRow.Cells[1].Value.ToString();
                String barcode = this.codedata.CurrentRow.Cells[3].Value.ToString();

                try
                {
                    Db.Open();
                    String sql = "update t_wms_item set bigbox_bar='" + barcode + "' where itemno='" + itemno + "'";
                    String batchcodesql = "select count(*) from highspeed.t_produce_brandcoderelative where cigarettecode='" + itemno + "'";
                   
                    int len = Db.ExecuteNonQuery(sql);
                    if (len != 0) MessageBox.Show("卷烟品牌对应条码修改成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (SqlException se)
                {
                    MessageBox.Show(se.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Db.Close();
                    //this.Close();
                }
            }
        }
    }
}
