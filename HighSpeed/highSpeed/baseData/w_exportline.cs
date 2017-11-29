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
    public partial class win_exportline : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public string sign;
        public string amend_id;
        public win_exportline()
        {
            InitializeComponent();
            seek();
        }

        private void seek()
        {

            String strsql = "SELECT rownum,tmp.* FROM(select t.*,decode(state,'0','正常','1','停用')as status FROM t_produce_export t ORDER BY exportnum)tmp";
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

                this.exportlinedata.DataSource = ds.Tables[0];
                this.exportlinedata.AutoGenerateColumns = false;

                string columnwidths = pub.IniReadValue(this.Name, this.exportlinedata.Name);
                if (columnwidths != "")
                {
                    string[] columns = columnwidths.Split(',');
                    int j = 0;
                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (exportlinedata.Columns[i].Visible == true)
                        {
                            exportlinedata.Columns[j].Width = Convert.ToInt32(columns[i]);
                            j = j + 1;
                        }
                    }
                }
                exportlinedata.ClearSelection();
                this.btn_amend.Enabled = false;
                this.btn_qy.Enabled = false;
                this.btn_jy.Enabled = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void win_exportline_Load(object sender, EventArgs e)
        {
            exportlinedata.ClearSelection();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
            Db.Close();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            dgVprint1.MainTitle = "出口信息";
            //dgVprint1.SubTitle = "这是子标题，当然也可以不设的";
            dgVprint1.PaperLandscape = true;//用横向打印，默认是纵向

            dgVprint1.Print(exportlinedata);
        }

        private void btn_toexcel_Click(object sender, EventArgs e)
        {
            dgVprint1.ExportDGVToExcel2(this.exportlinedata, "出口信息", "exportlineInfo.xls", true);
        }

        private void btn_qy_Click(object sender, EventArgs e)
        {
            int count = this.exportlinedata.SelectedRows.Count;

            if (count > 0)
            {
                String desc = this.exportlinedata.SelectedRows[0].Cells[2].Value.ToString();
                DialogResult MsgBoxResult = MessageBox.Show("确定启用编号为【" + desc + "】的出口吗？",//对话框的显示内容 
                                                            "提示",//对话框的标题 
                                                            MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                            MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                            MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
                if (MsgBoxResult == DialogResult.Yes)
                {
                    String updatesql = "update highspeed.t_produce_export set state='0' where exportnum='" + desc + "' or parentnum='" + desc + "'";
                    try
                    {
                        Db.Open();
                        int len = Db.ExecuteNonQuery(updatesql);
                        if (len > 0) MessageBox.Show("编号为【" + desc + "】的出口已启用!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            int count = this.exportlinedata.SelectedRows.Count;

            if (count > 0)
            {
                String desc = this.exportlinedata.SelectedRows[0].Cells[2].Value.ToString();
                DialogResult MsgBoxResult = MessageBox.Show("确定停用编号为【" + desc + "】的出口吗？",//对话框的显示内容 
                                                            "提示",//对话框的标题 
                                                            MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                            MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                            MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
                if (MsgBoxResult == DialogResult.Yes)
                {
                    String updatesql = "update highspeed.t_produce_export set state='1' where exportnum='" + desc + "' or parentnum='" + desc + "'";
                    try
                    {
                        Db.Open();
                        int len = Db.ExecuteNonQuery(updatesql);
                        if(len>0)MessageBox.Show("编号为【" + desc + "】的出口已停用!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void exportlinedata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                String status = this.exportlinedata.CurrentRow.Cells[7].Value + "";
                String level=this.exportlinedata.CurrentRow.Cells[14].Value+"";

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
                //MessageBox.Show(level);
                if (level == "1")
                {
                    this.btn_amend.Enabled = false;
                }
                else 
                {
                    this.btn_amend.Enabled = true;
                }
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            sign = "0";
            win_exportline_handle exportline_handle = new win_exportline_handle(sign, "");
            exportline_handle.WindowState = FormWindowState.Normal;
            exportline_handle.StartPosition = FormStartPosition.CenterScreen;
            exportline_handle.ShowDialog();
            seek();
        }

        private void btn_amend_Click(object sender, EventArgs e)
        {
            int count = this.exportlinedata.SelectedRows.Count;
            if (count > 0)
            {
                amend_id = this.exportlinedata.SelectedRows[0].Cells[2].Value.ToString();
                sign = "1";
                win_exportline_handle exportline_handle = new win_exportline_handle(sign, amend_id);
                exportline_handle.WindowState = FormWindowState.Normal;
                exportline_handle.StartPosition = FormStartPosition.CenterScreen;
                exportline_handle.ShowDialog();
                seek();
            }
            else
            {
                MessageBox.Show("请点击选择您要修改的出口信息!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
