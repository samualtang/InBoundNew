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
using InBound.Business;
using FormUI.TooL;
using InBound;
using InBound.Model;
using InBound.Pub;

namespace FormUI
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
          
            //List<T_PRODUCE_POKE> list= TaskService.getList(1, 1);
         //  String list= AtsCellInService.getCellNoCode("1111111");
          
            //String str = "12";
            try
            {
              // throw(new Exception());

            
                string ls_pass;
              
                ls_pass = textBox2.Text;
               ls_pass= Security.MD5Encrypt(ls_pass).ToLower();
                  if (ls_pass == "")
                {
                    MessageBox.Show("请输入密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (SysUserService.Login(txtUserName.Text, ls_pass))
                {
                    Constant.userName = txtUserName.Text.Trim();
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("用户名密码不对,请重新输入!");
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
            //MessageBox.Show("错误");
        }

    }
}