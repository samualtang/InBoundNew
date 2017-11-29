using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using highSpeed.PubFunc;

namespace highSpeed.baseData
{
    public partial class w_cigarette_handle : Form
    {
        String itemno, itenName;
        String machineseq, troughtype;
        DataBase Db = new DataBase();
        public w_cigarette_handle(String itemno, String itemname, String ws, String fz, String pcount, String troughtype,String machineseq)
        {
            this.itemno = itemno;
            this.itenName = itemname;
      
            InitializeComponent();
            lblName.Text = itemname;
            lblNo.Text = itemno;
            this.tbws.Text = ws;
            this.tbhjws.Text = fz;
            this.tbyz.Text = pcount;
            this.machineseq = machineseq;
            this.troughtype = troughtype;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String sql = @"update t_produce_sorttrough set MANTISSA=" + tbws.Text + ",THRESHOLD=" + tbhjws.Text + ",BOXCOUNT=" +tbyz.Text+
                 " where machineseq="+this.machineseq+" and troughtype="+this.troughtype +" and cigarettetype=20";
            Db.Open();
            Db.ExecuteNonQuery(sql);
            Db.Close();
            MessageBox.Show("修改成功");
            this.Close();
        }
        private void tb_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键  
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字  
                {
                    e.Handled = true;
                }
            }
            
        }  
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tb_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.ToString() == "")
            {
                tb.Text = "0";
            }

        }
    }
}
