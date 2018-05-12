using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace SortingControlSys.SortingControl
{
    
    public partial class w_pass : Form
    {
        String secCode = ConfigurationManager.AppSettings.Get("SecCode");
        public w_pass()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(secCode))
            {
                StatusManager form = new StatusManager();
                form.WindowState = FormWindowState.Normal;
                form.StartPosition = FormStartPosition.CenterScreen; 
                form.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("密码输入错误，请重新输入");
            }

        }
    }
}
