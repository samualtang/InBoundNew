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
    public partial class win_cigarette_choose : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public win_trough_handle win_parent;
        string cigarettecode="", cigarettename="";
        public List<string> infolst=new List<string>();
        public win_cigarette_choose(win_trough_handle form,List<string> lst)
        {
            InitializeComponent();
            init();
            win_parent = form;
            infolst = lst;
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
            this.box_type.SelectedIndex = 1;

            
            this.btn_submit.Enabled = false;
            this.txt_keywd.Focus();
        }

        public List<string> returnObj 
        {
            get { return this.infolst; }
        }

        private void seek()
        {
            string types = this.box_type.SelectedValue + "";
            string keywd = this.txt_keywd.Text.Trim();
            String tmp = "";
            if (types != null && types != "" && keywd != null && keywd != "")
            {
                tmp = " and " + types + " like'%" + keywd + "%'";
                String strsql = "SELECT rownum,itemno,itemname,shortname FROM t_wms_item WHERE length(itemno)=7" + tmp;
                Bind(strsql);
            }
            else 
            {
                MessageBox.Show("请输入查询条件进行查询!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
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

                this.itemdata.DataSource = ds.Tables[0];
                this.itemdata.AutoGenerateColumns = false;

                string columnwidths = pub.IniReadValue(this.Name, this.itemdata.Name);
                if (columnwidths != "")
                {
                    string[] columns = columnwidths.Split(',');
                    int j = 0;
                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (itemdata.Columns[i].Visible == true)
                        {
                            itemdata.Columns[j].Width = Convert.ToInt32(columns[i]);
                            j = j + 1;
                        }
                    }
                }
                itemdata.ClearSelection();

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

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
            Db.Close();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            if (cigarettecode != null && cigarettecode.Trim() != "")
            {
                infolst.Add(cigarettecode);
                infolst.Add(cigarettename);
                this.DialogResult = DialogResult.OK;
                this.Close();
                Db.Close();
            }
            else 
            {
                MessageBox.Show("请先点击选择您要的卷烟品牌，再进行提交！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        private void itemdata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1) 
            {
                if (this.itemdata.CurrentRow != null && this.itemdata.CurrentRow.Index != -1)
                {
                    cigarettecode = this.itemdata.CurrentRow.Cells[1].Value + "";
                    cigarettename = this.itemdata.CurrentRow.Cells[2].Value + "";

                    //MessageBox.Show("===" + troughdata.RowCount);
                    if (cigarettecode != "")
                    {
                        this.btn_submit.Enabled = true;
                        this.txt_itemno.Text = cigarettecode;
                        this.txt_itemname.Text = cigarettename;
                    }
                    else
                    {
                        this.btn_submit.Enabled = false;
                        this.txt_itemno.Text = "";
                        this.txt_itemname.Text = "";
                    }
                }
            }
        }

        private void txt_keywd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                seek();
            }
        }

        private void box_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txt_keywd.Focus();
        }


    }
}
