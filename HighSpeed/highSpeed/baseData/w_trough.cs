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

namespace highSpeed.baseData
{
    public partial class win_trough : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public string sign;
        public string amend_id;
        public win_trough()
        {
            InitializeComponent();
            init();
            this.pager1.PageChanged += new WHC.Pager.WinControl.PageChangedEventHandler(pager1_PageChanged);
            this.pager1.ExportCurrent += new WHC.Pager.WinControl.ExportCurrentEventHandler(pager1_ExportCurrent);
            this.pager1.ExportAll += new WHC.Pager.WinControl.ExportAllEventHandler(pager1_ExportAll);
            //this.pager1.GetChildAtPoint(7).Visible = false;
            pager1.PageSize = 200;
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
            //初始化分拣线选择下拉框

            //Db.Open();
            //String sql = "select linenum,linedesc from t_produce_sortline order by linenum";
            //DataTable dt= Db.Query(sql);
            //this.box_sortline.DataSource = dt;
            //this.box_sortline.DisplayMember = "linedesc";
            //this.box_sortline.ValueMember = "linenum";
            //this.box_sortline.SelectedIndex = 0;
            //Db.Close();

            //初始化查询条件下拉框
            DataTable conditiontable = new DataTable();
            conditiontable.Columns.Add("value",typeof(string));
            conditiontable.Columns.Add("txtvalue",typeof(string));

            DataRow dr = conditiontable.NewRow();
            dr["value"] = "cigarettecode";
            dr["txtvalue"] = "品牌代码";
            
            DataRow dr1 = conditiontable.NewRow();
            dr1["value"] = "cigarettename";
            dr1["txtvalue"] = "品牌名称";

            conditiontable.Rows.Add(dr);
            conditiontable.Rows.Add(dr1);

            this.box_condition.DataSource = conditiontable;
            this.box_condition.DisplayMember = "txtvalue";
            this.box_condition.ValueMember = "value";
            this.box_condition.SelectedIndex = 0;

            //初始化启停按钮状态
            this.cbtype.SelectedIndex = 0;
            this.cbctype.SelectedIndex = 0;
            this.btn_qy.Enabled = false;
            this.btn_jy.Enabled = false;
        }

        private void seek()
        {
           // string linenum = this.box_sortline.SelectedValue+"";
            string types = this.box_condition.SelectedValue+"";
            string keywd = this.txt_keywd.Text.Trim();
            int type = cbtype.SelectedIndex;
            int ctype = cbctype.SelectedIndex;
            if (ctype == 1)
            {
                ctype = 20;
            }
            else if (ctype == 2)
            {
                ctype = 30;
            }
            else if (ctype == 3)
            {
                ctype = 40;
            }

            if (type == 0)
            {
                type = 10;
            }
            else if (type == 1)
            {
                type =20;
            }
            else if (type == 2)
            {
                type = 30;
            }
            else if (type == 3)
            {
                type = 40;
            }
            String tmp="";
            if (types != null && types != "" && keywd != null && keywd != "") {
                tmp = " and " + types + " like'%" + keywd + "%'";
            }
            if (type != 0)
            {
                tmp += "and troughtype=" + type;
            }
            if (ctype != 0)
            {
                tmp += "and cigarettetype=" + ctype;
            }
            String strsql = "SELECT tmp.* FROM(select  rownum as num, troughnum,id," +
                            "cigarettecode,cigarettename,state,troughtype as type,cigarettetype as ctype,decode(cigarettetype,'10','混合','20','标准','30','异型','异型混合')as cigarettetype,decode(state,'10','正常','0','禁用')as status, " +
                            "decode(troughtype,10,'分拣',20,'重力式货架',30,'皮带机',40,'分拣出口')as troughtype from t_produce_sorttrough t where  1=1" + tmp +
                            " ORDER BY troughnum)tmp where  tmp.num>" + (pager1.CurrentPageIndex - 1) * pager1.PageSize + " and tmp.num<=" + pager1.CurrentPageIndex * pager1.PageSize + " order by to_number(tmp.troughnum)";
            //MessageBox.Show(strsql);
            int total = int.Parse(DataPublic.ExecuteScalar("SELECT count(*) FROM t_produce_sorttrough where 1=1 and cigarettetype='20'" + tmp).ToString());
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

                this.troughdata.DataSource = ds.Tables[0];
                this.troughdata.AutoGenerateColumns = false;

                string columnwidths = pub.IniReadValue(this.Name, this.troughdata.Name);
                if (columnwidths != "")
                {
                    string[] columns = columnwidths.Split(',');
                    int j = 0;
                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (troughdata.Columns[i].Visible == true)
                        {
                            troughdata.Columns[j].Width = Convert.ToInt32(columns[i]);
                            j = j + 1;
                        }
                    }
                }
                troughdata.ClearSelection();

            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        #endregion

        private void btn_new_Click(object sender, EventArgs e)
        {
            sign = "0";
            win_trough_handle trough_handle = new win_trough_handle(sign,"","20","");
            trough_handle.WindowState = FormWindowState.Normal;
            trough_handle.StartPosition = FormStartPosition.CenterScreen;
            trough_handle.ShowDialog();
            seek();
        }

        private void win_trough_Load(object sender, EventArgs e)
        {
            
            
            
            
            
            
            
            troughdata.ClearSelection();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            seek();
        }

        private void btn_qy_Click(object sender, EventArgs e)
        {
            int count = this.troughdata.SelectedRows.Count;
            
            if (count>0)
            {
                String id = this.troughdata.SelectedRows[0].Cells["id"].Value.ToString();
                String desc = this.troughdata.SelectedRows[0].Cells[9].Value.ToString();
                String ctype = this.troughdata.CurrentRow.Cells["ctype"].Value + "";
                String type = this.troughdata.CurrentRow.Cells["type"].Value + "";
                DialogResult MsgBoxResult = MessageBox.Show("确定启用"+desc+"【"+id+"】通道吗？",//对话框的显示内容 
                                                            "提示",//对话框的标题 
                                                            MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                            MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                            MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
                if (MsgBoxResult == DialogResult.Yes)
                {
                    String updatesql = "update t_produce_sorttrough set state='10' where id=" + id + " and cigarettetype=" + ctype + " and troughtype=" + type;
                    try
                    {
                        Db.Open();
                        Db.ExecuteNonQuery(updatesql);
                        MessageBox.Show(desc+"通道号【" + id + "】的通道已启用!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        seek();

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
            else
            {
                MessageBox.Show("请点击选择您要启用的分拣通道!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void troughdata_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void btn_jy_Click(object sender, EventArgs e)
        {
            int count=this.troughdata.SelectedRows.Count;
            
            if (count>0)
            {
                String id = this.troughdata.SelectedRows[0].Cells["id"].Value.ToString();
                String desc = this.troughdata.SelectedRows[0].Cells[9].Value.ToString();
               String ctype= this.troughdata.CurrentRow.Cells["ctype"].Value+"";
               String type = this.troughdata.CurrentRow.Cells["type"].Value + "";
                DialogResult MsgBoxResult = MessageBox.Show("确定禁用"+desc+"【"+id+"】通道吗？",//对话框的显示内容 
                                                            "提示",//对话框的标题 
                                                            MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                            MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                            MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
                if (MsgBoxResult == DialogResult.Yes)
                {
                    String updatesql = "update t_produce_sorttrough set state='0' where id=" + id + " and cigarettetype=" + ctype + " and troughtype=" + type ;
                    try
                    {
                        Db.Open();
                        Db.ExecuteNonQuery(updatesql);
                        MessageBox.Show(desc + "【" + id + "】的通道已禁用!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        seek();

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
            else
            {
                MessageBox.Show("请点击选择您要禁用的分拣通道!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            dgVprint1.MainTitle = "分拣通道信息";
            //dgVprint1.SubTitle = "这是子标题，当然也可以不设的";
            dgVprint1.PaperLandscape = true;//用横向打印，默认是纵向

            dgVprint1.Print(troughdata);
        }

        private void btn_amend_Click(object sender, EventArgs e)
        {
            String type = this.troughdata.CurrentRow.Cells["type"].Value + "";
            if (type.Equals("30") || type.Equals("40"))
            {
                return;
            }
            int count = this.troughdata.SelectedRows.Count;
            if (count > 0)
            {
                amend_id = this.troughdata.SelectedRows[0].Cells["id"].Value.ToString();
                sign = "1";
                String cigarettetype = this.troughdata.CurrentRow.Cells["ctype"].Value + "";
                String type1 = this.troughdata.CurrentRow.Cells["type"].Value + "";
                
                win_trough_handle trough_handle = new win_trough_handle(sign, amend_id, cigarettetype,type1);
                trough_handle.WindowState = FormWindowState.Normal;
                trough_handle.StartPosition = FormStartPosition.CenterScreen;
                trough_handle.ShowDialog();
                seek();
            }
            else {
                MessageBox.Show("请点击选择您要修改的分拣通道!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void troughdata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                String status = this.troughdata.CurrentRow.Cells["state"].Value + "";
                String type = this.troughdata.CurrentRow.Cells["type"].Value + "";
                //MessageBox.Show("===" + troughdata.RowCount);
                if (status == "10")
                {
                    this.btn_qy.Enabled = false;
                    this.btn_jy.Enabled = true;
                }
                else
                {
                    this.btn_qy.Enabled = true;
                    this.btn_jy.Enabled = false;
                }
                if (type == "30" || type == "40")
                {
                    this.btn_amend.Enabled = false;
                }
                else
                {
                    this.btn_amend.Enabled = true;
                }
            }
        }

        private void box_condition_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txt_keywd.Focus();
        }

        private void txt_keywd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                seek();
            }
        }

        private void btn_toexcel_Click(object sender, EventArgs e)
        {
            dgVprint1.ExportDGVToExcel2(this.troughdata, "分拣通道信息", "sorttroughInfo.xls", true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Db.Open();
            OracleParameter[] sqlpara;
            sqlpara = new OracleParameter[3];
            sqlpara[0] = new OracleParameter("p_bz", "20");
            sqlpara[1] = new OracleParameter("p_ErrCode", OracleType.VarChar, 300);
            sqlpara[2] = new OracleParameter("p_ErrMsg", OracleType.VarChar, 1000);

            sqlpara[1].Direction = ParameterDirection.Output;
            sqlpara[2].Direction = ParameterDirection.Output;

            Db.ExecuteNonQueryWithProc("P_PRODUCE_Validation", sqlpara);
            //MessageBox.Show(date);
            //MessageBox.Show(code[i]+"订单数据接收完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


            string errcode = sqlpara[1].Value == null ? "" : sqlpara[1].Value.ToString();
            string errmsg = sqlpara[2].Value == null ? "" : sqlpara[2].Value.ToString();
            Db.Close();
            if (errcode == "1")
            {
                MessageBox.Show("设置正常");
            }
            else
            {
                MessageBox.Show(errmsg);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            win_trough_handle trough_handle = new win_trough_handle("0", "", "40","10");
            trough_handle.WindowState = FormWindowState.Normal;
            trough_handle.StartPosition = FormStartPosition.CenterScreen;
            trough_handle.ShowDialog();
        }
    }
}
