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
    public partial class win_replenish_handle : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        String handle_sign = "", id = "";
        public win_replenish_handle(String sign,String amend_id)
        {
            InitializeComponent();
            handle_sign = sign;
            id = amend_id;
            init(handle_sign, id);
            if (handle_sign == "0") this.Text = "补货通道--新增";
            else this.Text = "补货通道--修改";

        }

        public void init(String sign, String amend_id)
        {
            //初始化分拣线选择下拉框
            Db.Open();
            String sql = "select id,troughnum||'('||troughdesc||')'as troughinfo from highspeed.t_produce_sorttrough t ORDER BY to_number(troughnum)";
            DataTable dt = Db.Query(sql);

            this.box_troughid.DataSource = dt;
            this.box_troughid.DisplayMember = "troughinfo";
            this.box_troughid.ValueMember = "id";
            this.box_troughid.SelectedIndex = 0;

            //修改状态
            if (sign == "1")
            {
                sql = "select troughid,replenishnum,replenishdesc,replenishseq from t_produce_replenish where id=" + amend_id;
                DataRow row = Db.Query(sql).Rows[0];
                this.txt_replenishnum.Text = row[1].ToString();
                this.txt_replenishdesc.Text = row[2].ToString();
                this.txt_replenishseq.Text = row[3].ToString();

                String troughid = row[0].ToString();

                box_troughid.SelectedValue = troughid;
            }

            Db.Close();

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
            Db.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            String troughid = this.box_troughid.SelectedValue.ToString();
            String num = this.txt_replenishnum.Text.Trim();
            String desc = this.txt_replenishdesc.Text.Trim();
            String seq = this.txt_replenishseq.Text;

            if (num == "")
            {
                MessageBox.Show("请填写补货通道编号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (desc == "")
            {
                MessageBox.Show("请填写补货通道描述!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (seq == "")
            {
                MessageBox.Show("请填写补货通道顺序!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                Db.Open();
                if (handle_sign == "0")
                {

                    String sql = "insert into highspeed.T_PRODUCE_REPLENISH(id,troughid,replenishnum,replenishdesc,replenishseq)" +
                                 "values(highspeed.s_produce_replenish.nextval," + troughid + ",'" + num + "','" + desc + "'," + seq + ")";
                    int len = Db.ExecuteNonQuery(sql);
                    if (len != 0) MessageBox.Show("补货通道创建成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    String sql = "update highspeed.T_PRODUCE_REPLENISH set replenishnum='" + num + "',replenishdesc='" + desc +
                                 "',replenishseq=" + seq + ",troughid="+troughid+" where id=" + id;
                    int len = Db.ExecuteNonQuery(sql);
                    if (len != 0) MessageBox.Show("补货通道信息修改成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void txt_replenishseq_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = false;
            }
        }
    }
}
