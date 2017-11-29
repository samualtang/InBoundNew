using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OracleClient;
using SortingControlSys.PubFunc;

namespace SortingControlSys.SortingControl
{
    public partial class w_reportprint : Form
    {
        public string sign;
        public string amend_id;
        public w_reportprint()
        {
            InitializeComponent();
            init();
            seek();
        }

        public void init()
        {
            //初始化分拣线选择下拉框

            
        }

        private void seek()
        {
            string from=textBox1.Text;
            string to=textBox2.Text;
            if (to.Equals("")) 
            {
                to = from;
            }
            String type="";
            if(radioButton1.Checked)
            {
                type="R02";
            }
            else
            {
                type="L01";
            }
            String sSql = "select t.tasknum,t.customername,t.exportnum,l.cigarettename,l.quantity,t.taskquantity " +
   "from t_produce_task t,t_produce_taskline l where t.tasknum=l.tasknum and T.tasknum>=" + from + " and t.tasknum<=" + to +
   "and t.exportnum='" + type + "'order by t.tasknum";
            Bind(sSql);
        }

        #region 查询
        /// <summary>
        /// 绑定DataGridView1
        /// </summary>
        /// <param name="sql">要查询的sql</param>
        private void Bind(string sql)
        {
            OracleDataReader myread = DataPublic.ReadDb(sql);
            task_data.Rows.Clear();
            try
            {
                String tasknum = "", cusname = "", cigarename = "", quantity = "", taskquantity = "";
                while (myread != null && myread.Read())
                {
                    tasknum = myread["tasknum"].ToString().Trim();
                    cusname = myread["customername"].ToString().Trim();
                    cigarename = myread["cigarettename"].ToString().Trim();
                    quantity = myread["quantity"].ToString().Trim();
                    taskquantity = myread["taskquantity"].ToString().Trim();

                    int index = this.task_data.Rows.Add();
                    this.task_data.Rows[index].Cells[0].Value = tasknum;
                    this.task_data.Rows[index].Cells[1].Value = cusname;
                    this.task_data.Rows[index].Cells[2].Value = cigarename;
                    this.task_data.Rows[index].Cells[3].Value = quantity;
                    this.task_data.Rows[index].Cells[4].Value = taskquantity;
                   
                }
                //MessageBox.Show(sFun.getCzcode("950"));
                /*int indexj=0;
                for (int i = 0; i < task_data.RowCount;i++ )
                {
                    if(this.task_data.Rows[i].Cells[0].Value.ToString()=="0204")
                    {
                         indexj=i;
                         break;
                    }
                }
                for (int i = 0; i < 10;i++ )
                {
                    double per=double.Parse(this.task_data.Rows[indexj].Cells[5].Value.ToString());
                    per = per + i;
                    this.task_data.Rows[indexj].Cells[5].Value = per;
                }
                */

            }
            finally
            {
                if (myread != null)
                {
                    myread.Close();
                    myread.Dispose();
                }
            }
           
        }
        #endregion

        private void btn_search_Click(object sender, EventArgs e)
        {
            seek();
        }

        private void txt_keywd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                seek();
            }
        }

       

       

        private void btn_print_Click(object sender, EventArgs e)
        {
            dgVprint1.MainTitle = "补货通道信息";
            //dgVprint1.SubTitle = "这是子标题，当然也可以不设的";
            dgVprint1.PaperLandscape = true;//用横向打印，默认是纵向

            dgVprint1.Print(task_data);
        }

       

      

       
    }
}
