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
using highSpeed.PubFunc;

namespace highSpeed.baseData
{
    public partial class w_jianyanreportprint : Form
    {
        public string sign;
        public string amend_id;
        public w_jianyanreportprint()
        {
            InitializeComponent();
            init();
            //seek();
        }

        public void init()
        {
            //初始化分拣线选择下拉框
            
        }

        private void seek()
        {
            
            String type="";
            if(radioButton1.Checked)
            {
                type="1";
            }
            else
            {
                type="2";
            }
            String sSql = "select t.regioncode,r.id,rownum,r.cigarettecode,r.cigarettename,r.replenishqty,r.tasknum  from highspeed.t_produce_replenishplan r left join t_produce_task  t  on  r.tasknum=t.tasknum where r.transportationline=" + type + " and status='0' order by r.seq desc, r.id";
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
                String cigarettecode = "", tasknum = "", regioncode = "", cigarename = "", quantity = "", taskquantity = "";
                while (myread != null && myread.Read())
                {
                    cigarettecode = myread["cigarettecode"].ToString().Trim();
                    cigarename = myread["cigarettename"].ToString().Trim();
                    quantity = myread["replenishqty"].ToString().Trim();
                    tasknum = myread["tasknum"].ToString().Trim();
                    regioncode = myread["regioncode"].ToString().Trim();
                    int index = this.task_data.Rows.Add();
                    this.task_data.Rows[index].Cells[0].Value = cigarettecode;
                    this.task_data.Rows[index].Cells[1].Value = cigarename;
                    this.task_data.Rows[index].Cells[2].Value = quantity;
                    this.task_data.Rows[index].Cells[3].Value = tasknum;
                    this.task_data.Rows[index].Cells[4].Value = regioncode;
                }
               

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
            dgVprint1.MainTitle = "件烟补货计划";
            //dgVprint1.SubTitle = "这是子标题，当然也可以不设的";
           // dgVprint1.PaperLandscape = true;//用横向打印，默认是纵向

            dgVprint1.Print(task_data);
        }

       

      

       
    }
}
