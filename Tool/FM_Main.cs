using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Encrypt;
using InBound;

namespace Tool
{
    public partial class FM_Main : Form
    {
        public FM_Main()
        {
            InitializeComponent();
            asc.controllInitializeSize(this); 
        }
        AutoSizeFormClass asc = new AutoSizeFormClass();
        private void btnEn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(rtbH.Text))
            {
                string _strInfo = rtbH.Text;
                try
                {
                    rtbF.Text = new SymmetricMethod().Encrypto(_strInfo);
                }
                catch (Exception ex)
                {

                    MessageBox.Show("加密失败,错误信息：" + ex.Message);
                } 
            }
            else
            {
                MessageBox.Show("请输入加密的字符串");
            }
        }

        private void btnDe_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(rtbH.Text))
            {
                string _strInfo = rtbH.Text;
                try
                {
                    rtbF.Text = new SymmetricMethod().Decrypto(_strInfo);
                }
                catch (Exception ex)
                {

                    MessageBox.Show("解密失败,错误信息：" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("请输入解密的字符串");
            }
        }

 

        private void btnClear_Click(object sender, EventArgs e)
        {
            rtbH.Clear();
            rtbF.Clear();
        }

        private void FM_Main_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }
    }
}
