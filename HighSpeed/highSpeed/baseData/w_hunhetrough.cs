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
    public partial class w_hunhetrough : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public string sign;
        public string amend_id;
        public w_hunhetrough()
        {
            InitializeComponent();
            init();
            this.pager1.PageChanged += new WHC.Pager.WinControl.PageChangedEventHandler(pager1_PageChanged);
            this.pager1.ExportCurrent += new WHC.Pager.WinControl.ExportCurrentEventHandler(pager1_ExportCurrent);
            this.pager1.ExportAll += new WHC.Pager.WinControl.ExportAllEventHandler(pager1_ExportAll);
            //this.pager1.GetChildAtPoint(7).Visible = false;
            pager1.PageSize = 100;
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

            this.btn_qy.Enabled = false;
            this.btn_jy.Enabled = false;
        }

        private void seek()
        {
           // string linenum = this.box_sortline.SelectedValue+"";
            string types = this.box_condition.SelectedValue+"";
            string keywd = this.txt_keywd.Text.Trim();
            String tmp="";
            if (types != null && types != "" && keywd != null && keywd != "") {
                tmp = " and " + types + " like'%" + keywd + "%'";
            }
            String strsql = "SELECT tmp.* FROM(select rownum as num, id,linenum,troughnum,troughdesc,decode(sign(machineseq-4),-1,'混合道','单独道')as troughtype" +
                            ",actcount,cigarettecode,state,cigarettename,decode(cigarettetype,'10','混合','20','正常','异型')as cigarettetype,replenishline,transportationline,decode(state,'0','正常','1','禁用')as status " +
                            "from highspeed.t_produce_sorttrough t where  cigarettetype='10'" + tmp +
                            " ORDER BY to_number(troughnum))tmp where  tmp.num>" + (pager1.CurrentPageIndex - 1) * pager1.PageSize + " and tmp.num<=" + pager1.CurrentPageIndex * pager1.PageSize + " order by to_number(tmp.troughnum)";
            //MessageBox.Show(strsql);
            int total = int.Parse(DataPublic.ExecuteScalar("SELECT count(*) FROM t_produce_sorttrough where 1=1 and cigarettetype='10'" + tmp).ToString());
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
            win_trough_handle trough_handle = new win_trough_handle(sign,"","10","");
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
                String id = this.troughdata.SelectedRows[0].Cells[1].Value.ToString();
                String desc = this.troughdata.SelectedRows[0].Cells[4].Value.ToString();
                DialogResult MsgBoxResult = MessageBox.Show("确定启用编号为【" + desc + "】的分拣通道吗？",//对话框的显示内容 
                                                            "提示",//对话框的标题 
                                                            MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                            MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                            MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
                if (MsgBoxResult == DialogResult.Yes)
                {
                    String updatesql = "update t_produce_sorttrough set state='0' where id=" + id;
                    try
                    {
                        Db.Open();
                        Db.ExecuteNonQuery(updatesql);
                        MessageBox.Show("描述为【" + desc + "】的分拣通道已启用!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                String id = this.troughdata.SelectedRows[0].Cells[1].Value.ToString();
                String desc = this.troughdata.SelectedRows[0].Cells[4].Value.ToString();
                DialogResult MsgBoxResult = MessageBox.Show("确定禁用编号为【" + desc + "】的分拣通道吗？",//对话框的显示内容 
                                                            "提示",//对话框的标题 
                                                            MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                            MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                            MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
                if (MsgBoxResult == DialogResult.Yes)
                {
                    String updatesql = "update t_produce_sorttrough set state='1' where id=" + id;
                    try
                    {
                        Db.Open();
                        Db.ExecuteNonQuery(updatesql);
                        MessageBox.Show("描述为【" + desc + "】的分拣通道已禁用!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            dgVprint1.PaperLandscape = false;//用横向打印，默认是纵向

            dgVprint1.Print(troughdata);
        }

        private void btn_amend_Click(object sender, EventArgs e)
        {
            int count = this.troughdata.SelectedRows.Count;
            if (count > 0)
            {
                amend_id = this.troughdata.SelectedRows[0].Cells[1].Value.ToString();
                sign = "1";
                win_trough_handle trough_handle = new win_trough_handle(sign,amend_id,"10","");
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

                //MessageBox.Show("===" + troughdata.RowCount);
                if (status == "0")
                {
                    this.btn_qy.Enabled = false;
                    this.btn_jy.Enabled = true;
                }
                else
                {
                    this.btn_qy.Enabled = true;
                    this.btn_jy.Enabled = false;
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
                    sqlpara[0] = new OracleParameter("p_bz", "10");
                    sqlpara[1] = new OracleParameter("p_ErrCode", OracleType.VarChar,300);
                    sqlpara[2] = new OracleParameter("p_ErrMsg", OracleType.VarChar,1000);

                    sqlpara[1].Direction = ParameterDirection.Output;
                    sqlpara[2].Direction = ParameterDirection.Output;

                    Db.ExecuteNonQueryWithProc("P_PRODUCE_Validation", sqlpara);
                    //MessageBox.Show(date);
                    //MessageBox.Show(code[i]+"订单数据接收完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    string errcode = sqlpara[1].Value == null ? "" : sqlpara[1].Value.ToString();
                    string errmsg = sqlpara[2].Value==null ? "" : sqlpara[2].Value.ToString();
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
            int count = this.troughdata.SelectedRows.Count;
            if (count > 0)
            {
                if (MessageBox.Show("确定删除" + this.troughdata.SelectedRows[0].Cells[9].Value.ToString() + "?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    try
                    {
                        Db.Open();

                        amend_id = this.troughdata.SelectedRows[0].Cells[3].Value.ToString();
                        //string sql = "select machineseq from t_produce_sorttrough where id=" + amend_id;
                        //int machineSeq = int.Parse(Db.ExecuteScalar(sql).ToString());
                        //sql = "select count(1) from t_produce_sorttrough  where machineseq=" + machineSeq;

                        //int counts = int.Parse(Db.ExecuteScalar(sql).ToString());
                        //if (counts > 1)
                        //{
                        Db.ExecuteNonQuery("delete from t_produce_sorttrough where id=" + amend_id);
                        Db.Commit();
                        seek();
                        MessageBox.Show("删除成功");
                        //}
                        //else
                        //{
                        //    MessageBox.Show("该数据无法删除");
                        //}

                    }
                    catch (Exception ex)
                    { }
                    finally
                    {
                        Db.Close();
                    }
                }

            }
            else
            {
                MessageBox.Show("请点击选择您要删除的数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
