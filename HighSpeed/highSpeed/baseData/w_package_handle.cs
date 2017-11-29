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
    public partial class win_package_handle : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        String handle_sign = "", id = "";
        public win_package_handle(String sign,String amend_id)
        {
            InitializeComponent();
            handle_sign = sign;
            id = amend_id;
            
            if (handle_sign == "0")
            {
                this.Text = "包装类型--新增";
            }
            else 
            {
                this.Text = "包装类型--修改";
                init(handle_sign, id);
            } 
            
        }

        public void init(String sign, String amend_id)
        {
            Db.Open();
            String sql = "select packageid,packagedesc,packageval from t_produce_package where id=" + amend_id;
            DataRow row = Db.Query(sql).Rows[0];
            this.txt_packageid.Text = row[0].ToString();
            this.txt_packagedesc.Text = row[1].ToString();
            this.txt_packageval.Text = row[2].ToString();

            Db.Close();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
            Db.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            String packageid = this.txt_packageid.Text.Trim();
            String packagedesc = this.txt_packagedesc.Text.Trim();
            String packageval = this.txt_packageval.Text.Trim();

            if (packageid == "")
            {
                MessageBox.Show("请填写包装类型编号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (packagedesc == "")
            {
                MessageBox.Show("请填写包装类型描述!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (packageval == "")
            {
                MessageBox.Show("请填写包装类型包装量!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                Db.Open();
                if (handle_sign == "0")
                {

                    String sql = "insert into highspeed.t_produce_package(id,packageid,packagedesc,packageval,status)" +
                                 "values(highspeed.S_PRODUCE_PACKAGE.nextval,'" + packageid + "','" + packagedesc + "','" + packageval + "','0')";
                    int len = Db.ExecuteNonQuery(sql);
                    if (len != 0) MessageBox.Show("包装类型信息创建成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    String sql = "update highspeed.t_produce_package set packageid='" + packageid + "',packagedesc='" + packagedesc + 
                                 "',packageval='" + packageval + "' where id=" + id;
                    int len = Db.ExecuteNonQuery(sql);
                    if (len != 0) MessageBox.Show("包装类型信息修改成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void txt_packageval_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = false;
            }
        }
    }
}
