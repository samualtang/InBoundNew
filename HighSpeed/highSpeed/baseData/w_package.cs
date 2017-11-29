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
    public partial class win_package : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public string sign;
        public string amend_id;
        public win_package()
        {
            InitializeComponent();
            init();
            seek();
        }

        public void init()
        {
            //初始化按钮状态
            this.btn_amend.Enabled = false;
            this.btn_del.Enabled = false;
        }

        private void seek()
        {
            String strsql = "select rownum,id,packageid,packagedesc,packageval from highspeed.t_produce_package t where status='0' ORDER BY packageid";
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

                this.packagedata.DataSource = ds.Tables[0];
                this.packagedata.AutoGenerateColumns = false;

                string columnwidths = pub.IniReadValue(this.Name, this.packagedata.Name);
                if (columnwidths != "")
                {
                    string[] columns = columnwidths.Split(',');
                    int j = 0;
                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (packagedata.Columns[i].Visible == true)
                        {
                            packagedata.Columns[j].Width = Convert.ToInt32(columns[i]);
                            j = j + 1;
                        }
                    }
                }
                packagedata.ClearSelection();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
            Db.Close();
        }

        private void win_package_Load(object sender, EventArgs e)
        {
            packagedata.ClearSelection();
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            sign = "0";
            win_package_handle package_handle = new win_package_handle(sign, "");
            package_handle.WindowState = FormWindowState.Normal;
            package_handle.StartPosition = FormStartPosition.CenterScreen;
            package_handle.ShowDialog();
            seek();
            init();
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            int count = this.packagedata.SelectedRows.Count;

            if (count > 0)
            {
                String id = this.packagedata.CurrentRow.Cells[1].Value.ToString();
                String desc = this.packagedata.CurrentRow.Cells[2].Value.ToString();
                DialogResult MsgBoxResult = MessageBox.Show("确定删除类型编号为【" + desc + "】包装类型吗？",//对话框的显示内容 
                                                            "提示",//对话框的标题 
                                                            MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                            MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                            MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
                if (MsgBoxResult == DialogResult.Yes)
                {
                    String updatesql = "update t_produce_package set status='1' where id=" + id;
                    try
                    {
                        Db.Open();
                        Db.ExecuteNonQuery(updatesql);
                        MessageBox.Show("描述为【" + desc + "】的包装类型已删除!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        seek();
                        init();

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
            else
            {
                MessageBox.Show("请点击选择您要删除的包装类型!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void packagedata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                this.btn_amend.Enabled = true;
                this.btn_del.Enabled = true;
            }
        }

        private void btn_amend_Click(object sender, EventArgs e)
        {
            int count = this.packagedata.SelectedRows.Count;
            if (count > 0)
            {
                amend_id = this.packagedata.CurrentRow.Cells[1].Value.ToString();
                sign = "1";
                win_package_handle package_handle = new win_package_handle(sign, amend_id);
                package_handle.WindowState = FormWindowState.Normal;
                package_handle.StartPosition = FormStartPosition.CenterScreen;
                package_handle.ShowDialog();
                seek();
                init();
            }
            else
            {
                MessageBox.Show("请点击选择您要修改的包装类型!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
