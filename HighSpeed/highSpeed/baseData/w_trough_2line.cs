using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using highSpeed.PubFunc;

namespace highSpeed.baseData
{
    public partial class w_trough_2line : Form
    {
        public w_trough_2line()
        {
            InitializeComponent();
            textBox1.Text = "请选择目标线路";
        }
        PubFunc.DataBase Db = new DataBase();

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Length <= 0)
            {
                MessageBox.Show("请选择目标线路");
                return;
            }

            Db.Open();
            OracleParameter[] sqlpara;
            sqlpara = new OracleParameter[3];

            sqlpara[0] = new OracleParameter("var_Line", comboBox1.Text);
            sqlpara[1] = new OracleParameter("p_ErrCode", OracleType.VarChar, 2000);
            sqlpara[2] = new OracleParameter("p_ErrMsg", OracleType.VarChar, 2000);

            sqlpara[0].Direction = ParameterDirection.Input;
            sqlpara[1].Direction = ParameterDirection.Output;
            sqlpara[2].Direction = ParameterDirection.Output;

           
            Db.ExecuteNonQueryWithProc("P_PRODUCE_SORTTROUGH_UNLINE", sqlpara);

         
            string Line = sqlpara[0].Value == null ? "" : sqlpara[0].Value.ToString();
            string errcode = sqlpara[1].Value == null ? "" : sqlpara[1].Value.ToString();
            string errmsg = sqlpara[2].Value == null ? "" : sqlpara[2].Value.ToString();
            Db.Close();
            
            MessageBox.Show(errmsg);
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex==0)
            {
                textBox1.Text = comboBox1.Items[1].ToString();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                textBox1.Text = comboBox1.Items[0].ToString();
            }
             
        }


    }
}
