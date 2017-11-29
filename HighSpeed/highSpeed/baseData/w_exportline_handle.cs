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
    public partial class win_exportline_handle : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        String handle_sign = "", id = "";
        public win_exportline_handle(String sign,String amend_id)
        {
            InitializeComponent();
            handle_sign = sign;
            id = amend_id;
            init(handle_sign, id);
            if (handle_sign == "0") this.Text = "出口信息--新增";
            else this.Text = "出口信息--修改";

        }

        public void init(String sign, String amend_id)
        {
            //初始化分拣线选择下拉框
            Db.Open();
            String sql = "select exportnum as num,exportnum||'('||exportdesc||')' as lineinfo from highspeed.t_produce_export t where exportlevel=1 ORDER BY exportnum";
            DataTable dt = Db.Query(sql);

            this.box_parentnum.DataSource = dt;
            this.box_parentnum.DisplayMember = "lineinfo";
            this.box_parentnum.ValueMember = "num";
            this.box_parentnum.SelectedIndex = 0;

            //修改状态
            if (sign == "1")
            {
                sql = "select * from t_produce_export where exportnum='" + amend_id +"'";
                DataRow row = Db.Query(sql).Rows[0];
                this.txt_exportnum.Text = row[0].ToString();
                this.txt_exportdesc.Text = row[1].ToString();
                this.txt_machinenum.Text = row[2].ToString();
                this.txt_exportseq.Text = row[4].ToString();

                this.txt_exportstate.Text = row[6].ToString();
                this.txt_taskstate.Text = row[7].ToString();
                this.txt_taskarived.Text = row[8].ToString();
                this.txt_taskfinish.Text = row[9].ToString();
                this.txt_taskconfirm.Text = row[10].ToString();
                this.txt_taskstorage.Text = row[11].ToString();

                String troughid = row[3].ToString();

                box_parentnum.SelectedValue = troughid;
            }

            Db.Close();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
            Db.Close();
        }

        private void txt_exportseq_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = false;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            String parentnum = this.box_parentnum.SelectedValue.ToString();
            String exportnum = this.txt_exportnum.Text.Trim();
            String exportdesc = this.txt_exportdesc.Text.Trim();
            String exportseq = this.txt_exportseq.Text.Trim();

            String machinenum = this.txt_machinenum.Text.Trim();
            String exportstate = this.txt_exportstate.Text.Trim();
            String taskstate = this.txt_taskstate.Text.Trim();
            String taskarived = this.txt_taskarived.Text.Trim();
            String taskfinish = this.txt_taskfinish.Text.Trim();
            String taskconfirm = this.txt_taskconfirm.Text.Trim();
            String taskstorage = this.txt_taskstorage.Text.Trim();

            if (exportnum == "")
            {
                MessageBox.Show("请填写出口编号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (exportdesc == "")
            {
                MessageBox.Show("请填写出口描述!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (exportseq == "")
            {
                MessageBox.Show("请填写出口顺序!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (machinenum == "")
            {
                MessageBox.Show("请填写出口机器编号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                Db.Open();
                if (handle_sign == "0")
                {

                    String sql = "insert into highspeed.t_produce_export(exportnum,exportdesc,machinenum,parentnum,exportseq," +
                                  "state,exportstate,taskstate,taskarived,taskfinish,taskconfirm,taskstorage,exportlevel)values('" +
                                  exportnum + "','" + exportdesc + "','" + machinenum + "','" + parentnum + "','" + exportseq + "','0','" + exportstate
                                  + "','" + taskstate + "','" + taskarived + "','" + taskfinish + "','" + taskconfirm + "','" + taskstorage + "','2')";
                    int len = Db.ExecuteNonQuery(sql);
                    if (len != 0) MessageBox.Show("出口创建成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    String sql = "update highspeed.t_produce_export set exportnum='" + exportnum + "',exportdesc='" + exportdesc + "',machinenum='" + machinenum + "',parentnum='" + parentnum + "',exportseq='" + exportseq + "'," +
  			                     "exportstate='"+exportstate+"',taskstate='"+taskstate+"',taskarived='"+taskarived+"',taskfinish='"+taskfinish+"',taskconfirm='"+taskconfirm+"',taskstorage='"+taskstorage+"' where exportnum='"+id+"'";
                    //MessageBox.Show(sql);
                    int len = Db.ExecuteNonQuery(sql);
                    if (len != 0) MessageBox.Show("出口信息信息修改成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException se)
            {
                MessageBox.Show(se.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Db.Close();

                this.Close();
            }
        }
    }
}
