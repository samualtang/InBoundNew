using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
 
using System.IO;
 
using InBound.Business;
using InBound;
using InBound.Pub;
 
using SortingControlSys.SortingControl;

namespace Union
{
    public partial class w_user : Form
    {
        
        private bool iscancel = false;
       

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
                string ls_pass = textBox2.Text;
                string userName  = txtUserName.Text;
                ls_pass = Security.MD5Encrypt(ls_pass).ToLower();

                if (string.IsNullOrWhiteSpace(userName))
                {
                    MessageBox.Show("请输入帐号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(ls_pass ))
                {
                    MessageBox.Show("请输入密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
               
                if ( UserService.GetUserInfo(userName,ls_pass))
                {
                    UnionFm fm = new UnionFm();
                    fm.Show();
                    fm.WindowState = FormWindowState.Maximized;
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("用户名和密码不符，请重输", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Text = "";
                    textBox2.Focus();
                    return;
                }
             
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