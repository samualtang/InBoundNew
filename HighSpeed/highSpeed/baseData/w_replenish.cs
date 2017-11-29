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
    public partial class win_replenish : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public string sign;
        public string amend_id;
        public win_replenish()
        {
            InitializeComponent();
            init();
            seek();
        }

        public void init()
        {
            //初始化分拣线选择下拉框

            Db.Open();
            String sql = "select id,troughnum||'('||troughdesc||')'as troughinfo from t_produce_sorttrough t ORDER BY to_number(troughnum)";
            DataTable dt = Db.Query(sql);

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("id", typeof(int));
            dt2.Columns.Add("troughinfo", typeof(string));
            DataRow dr3 = dt2.NewRow();
            dr3["id"] = "0";
            dr3["troughinfo"] = "全部";
            dt2.Rows.Add(dr3);
            dt2.Merge(dt);

            this.box_trough.DataSource = dt2;
            this.box_trough.DisplayMember = "troughinfo";
            this.box_trough.ValueMember = "id";
            this.box_trough.SelectedIndex = 0;
            Db.Close();

            //初始化查询条件下拉框
            DataTable conditiontable = new DataTable();
            conditiontable.Columns.Add("value", typeof(string));
            conditiontable.Columns.Add("txtvalue", typeof(string));

            DataRow dr = conditiontable.NewRow();
            dr["value"] = "cigarettecode";
            dr["txtvalue"] = "品牌代码";

            DataRow dr1 = conditiontable.NewRow();
            dr1["value"] = "cigarettename";
            dr1["txtvalue"] = "品牌名称";

            conditiontable.Rows.Add(dr);
            conditiontable.Rows.Add(dr1);

            this.box_types.DataSource = conditiontable;
            this.box_types.DisplayMember = "txtvalue";
            this.box_types.ValueMember = "value";
            this.box_types.SelectedIndex = 0;

            
        }

        private void seek()
        {
            string troughid = this.box_trough.SelectedValue + "";
            string types = this.box_types.SelectedValue + "";
            string keywd = this.txt_keywd.Text.Trim();
            String tmp = "";
            if(troughid!=null&&troughid!="0")
            {
                tmp = tmp + " and a.troughid=" + troughid;
            }
            if (types != null && types != "" && keywd != null && keywd != "")
            {
                tmp = tmp + " and " + types + " like'%" + keywd + "%'";
            }
            String strsql = "SELECT rownum,tmp.* from(SELECT a.id,troughnum||'('||troughdesc||')'as troughinfo,a.replenishnum,a.replenishdesc,a.replenishseq,"+
                            "b.cigarettecode,b.cigarettename,a.state,decode(a.state,'0','正常','1','停用') AS statue FROM t_produce_replenish a,t_produce_sorttrough b "+
                            "WHERE a.troughid=b.id "+tmp+" ORDER BY to_number(b.troughnum),a.replenishseq)tmp";
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

                this.replenishdata.DataSource = ds.Tables[0];
                this.replenishdata.AutoGenerateColumns = false;

                string columnwidths = pub.IniReadValue(this.Name, this.replenishdata.Name);
                if (columnwidths != "")
                {
                    string[] columns = columnwidths.Split(',');
                    int j = 0;
                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (replenishdata.Columns[i].Visible == true)
                        {
                            replenishdata.Columns[j].Width = Convert.ToInt32(columns[i]);
                            j = j + 1;
                        }
                    }
                }
                replenishdata.ClearSelection();
                //初始化启停按钮状态

                this.btn_qy.Enabled = false;
                this.btn_jy.Enabled = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void win_replenish_Load(object sender, EventArgs e)
        {
            replenishdata.ClearSelection();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            seek();
        }

        private void txt_keywd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                seek();
            }
        }

        private void replenishdata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                String status = this.replenishdata.CurrentRow.Cells[8].Value + "";

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

        private void box_types_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txt_keywd.Focus();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            dgVprint1.MainTitle = "补货通道信息";
            //dgVprint1.SubTitle = "这是子标题，当然也可以不设的";
            dgVprint1.PaperLandscape = true;//用横向打印，默认是纵向

            dgVprint1.Print(replenishdata);
        }

        private void btn_toexcel_Click(object sender, EventArgs e)
        {
            dgVprint1.ExportDGVToExcel2(this.replenishdata, "补货通道信息", "replenishInfo.xls", true);
        }

        private void btn_qy_Click(object sender, EventArgs e)
        {
            int count = this.replenishdata.SelectedRows.Count;

            if (count > 0)
            {
                String id = this.replenishdata.SelectedRows[0].Cells[1].Value.ToString();
                String desc = this.replenishdata.SelectedRows[0].Cells[4].Value.ToString();
                DialogResult MsgBoxResult = MessageBox.Show("确定启用【" + desc + "】吗？",//对话框的显示内容 
                                                            "提示",//对话框的标题 
                                                            MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                            MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                            MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
                if (MsgBoxResult == DialogResult.Yes)
                {
                    String updatesql = "update t_produce_replenish set state='0' where id=" + id;
                    try
                    {
                        Db.Open();
                        Db.ExecuteNonQuery(updatesql);
                        MessageBox.Show("描述为【" + desc + "】的补货通道已启用!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        seek();

                    }
                    catch (SqlException se)
                    {
                        MessageBox.Show(se.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Db.Close();
                    }
                }
            }
        }

        private void btn_jy_Click(object sender, EventArgs e)
        {
            int count = this.replenishdata.SelectedRows.Count;

            if (count > 0)
            {
                String id = this.replenishdata.SelectedRows[0].Cells[1].Value.ToString();
                String desc = this.replenishdata.SelectedRows[0].Cells[4].Value.ToString();
                DialogResult MsgBoxResult = MessageBox.Show("确定停用【" + desc + "】吗？",//对话框的显示内容 
                                                            "提示",//对话框的标题 
                                                            MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                            MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                            MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
                if (MsgBoxResult == DialogResult.Yes)
                {
                    String updatesql = "update t_produce_replenish set state='1' where id=" + id;
                    try
                    {
                        Db.Open();
                        Db.ExecuteNonQuery(updatesql);
                        MessageBox.Show("描述为【" + desc + "】的补货通道已停用!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        seek();

                    }
                    catch (SqlException se)
                    {
                        MessageBox.Show(se.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Db.Close();
                    }
                }
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            sign = "0";
            win_replenish_handle repleish_handle = new win_replenish_handle(sign, "");
            repleish_handle.WindowState = FormWindowState.Normal;
            repleish_handle.StartPosition = FormStartPosition.CenterScreen;
            repleish_handle.ShowDialog();
            seek();
        }

        private void btn_amend_Click(object sender, EventArgs e)
        {
            int count = this.replenishdata.SelectedRows.Count;
            if (count > 0)
            {
                amend_id = this.replenishdata.SelectedRows[0].Cells[1].Value.ToString();
                sign = "1";
                win_replenish_handle repleish_handle = new win_replenish_handle(sign, amend_id);
                repleish_handle.WindowState = FormWindowState.Normal;
                repleish_handle.StartPosition = FormStartPosition.CenterScreen;
                repleish_handle.ShowDialog();
                seek();
            }
            else
            {
                MessageBox.Show("请点击选择您要修改的补货通道!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
