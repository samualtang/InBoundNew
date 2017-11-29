using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Data.Odbc;
using System.Threading;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using SortingControlSys.PubFunc;

namespace SortingControlSys
{
    public partial class w_database_set : Form
    {
       // SqlConnection cn = new SqlConnection(); 
        OracleConnection cn = new OracleConnection(); 
        private bool iscancel = false;
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\hospital.ini");
        private const string sSecretKey = "Password";

        public w_database_set()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {

        }


        private void w_database_set_Load(object sender, EventArgs e)
        {
            try
            {
                this.CenterToParent();
                //this.server_server.Text = PublicFun.ServerName;
                //this.server_database.Text = PublicFun.DataName;
                //this.server_uid.Text = PublicFun.userId;
                //this.server_pwd.Text = PublicFun.sPwd;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                iscancel = false;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (server_server.Text.Trim() == "" || server_database.Text.Trim() == "" || server_uid.Text.Trim() == "")
                {
                    MessageBox.Show("服务器名、数据库名和用户名都不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //cn = new SqlConnection("server =" + server_server.Text + ";uid =" + server_uid.Text + ";pwd =" + server_pwd.Text + ";database =" + server_database.Text);
                String connectString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + server_server.Text + ")(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=" + server_database.Text + ")));User Id=" + server_uid.Text + ";Password=" + server_pwd.Text + ";";
                cn = new OracleConnection();
                cn.ConnectionString = connectString;
                cn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("接口数据库连接失败    " + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (cn != null) { if (cn.State != ConnectionState.Closed) { cn.Close(); } }
                return;
            }
            MessageBox.Show("连接成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (cn != null) { if (cn.State != ConnectionState.Closed) { cn.Close(); } }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (server_server.Text.Trim() == "" || server_database.Text.Trim() == "" || server_uid.Text.Trim() == "")
                {
                    MessageBox.Show("服务器名、数据库名和用户名都不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //PublicFun.DecryptFile(System.IO.Directory.GetCurrentDirectory().ToString() + "\\hospital_encrypt.ini", System.IO.Directory.GetCurrentDirectory().ToString() + "\\hospital.ini", sSecretKey);
                pub.IniWriteValue("Database", "Data Source", this.server_server.Text.ToString());
                pub.IniWriteValue("Database", "Initial Catalog", this.server_database.Text.ToString());
                pub.IniWriteValue("Database", "User ID", this.server_uid.Text.ToString());
                pub.IniWriteValue("Database", "Password", this.server_pwd.Text.ToString());
                if (File.Exists(System.IO.Directory.GetCurrentDirectory().ToString() + "\\hospital_encrypt.ini"))
                {
                    File.Delete(System.IO.Directory.GetCurrentDirectory().ToString() + "\\hospital_encrypt.ini");
                }
                PublicFun.EncryptFile(System.IO.Directory.GetCurrentDirectory().ToString() + "\\hospital.ini", System.IO.Directory.GetCurrentDirectory().ToString() + "\\hospital_encrypt.ini", sSecretKey);
                PublicFun.start();
                MessageBox.Show("数据保存完毕", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool isCancel
        {
            get
            {
                return iscancel;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (PublicFun.start())
                {
                    iscancel = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("数据库配置错误，请重设或退出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
