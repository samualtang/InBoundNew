using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.IO;
using highSpeed.PubFunc;
using InBound.Business;
using InBound;

namespace highSpeed
{
    public partial class w_user : Form
    {
        OracleConnection cnn = new OracleConnection(PublicFun.connect);
        private bool iscancel = false;
        PublicFun pub;

        public w_user()
        {
            InitializeComponent();
          
        }

      
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            MessageBox.Show("ok！");
        }
       


        #region Form_Load
        private void w_user_Load(object sender, EventArgs e)
        {
           

           
            try
            {
                this.CenterToScreen();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           

        }
        #endregion

        #region 取消
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                iscancel = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region btn_OK
        private void button1_Click(object sender, EventArgs e)
        {
           
            try
            {
                string ls_pass;
                int intCount;
                ls_pass = textBox2.Text;
                if (ls_pass == "")
                {
                    MessageBox.Show("请输入密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string strSql = "select count(*) from t_sys_user  where usercode = '" + txtUserName.Text + "' and userpsw ='" + ls_pass + "' ";
                OracleCommand cmmd = new OracleCommand(strSql, cnn);
                cnn.Open();
                intCount = Convert.ToInt32(cmmd.ExecuteScalar());
                cnn.Close();
                if (intCount != 0)
                {
                    String comBoxText = txtUserName.Text.Trim();
                    PublicFun.PubStrusername = comBoxText;
                    iscancel = false;

                }
                else
                {
                    MessageBox.Show("用户名和密码不符，请重输", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Text = "";
                    textBox2.Focus();
                    return;
                }
                strSql = "select username from t_sys_user  where usercode = '" + txtUserName.Text + "' and userpsw ='" + ls_pass + "' ";
                cmmd = cnn.CreateCommand();
                cmmd.CommandText = strSql;
                cnn.Open();
                OracleDataReader da = cmmd.ExecuteReader();
                while (da.Read())
                {
                    String username =da["username"].ToString();;
                    PublicFun.PubStruserempname = username;
                }
                cnn.Close();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 回车
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        public bool isCancel
        {
            get
            {
                return iscancel;
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textBox2.Focus();
        }

        private void w_user_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                button2_Click(sender, e);
            }
        }
        #endregion

        private void w_user_FormClosing(object sender, FormClosingEventArgs e)
        {
          
        }

    }
}