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
    public partial class w_hunhereportprint : Form
    {
        public string sign;
        public string amend_id;
        public w_hunhereportprint()
        {
            InitializeComponent();
            init();
            //seek();
        }

        public void init()
        {
            //初始化分拣线选择下拉框
            seek();
        }

        private void seek()
        {

            int status = 10;
            if (radioButton2.Checked)
            {
                status = 15;
            }
            if (radioButton3.Checked)
            {
                status = 20;
            }
            String sSql = "select p.sortnum,p.machineseq,h.cigarettecode,h.cigarettename,t.regioncode,p.pokenum,t.customername "+
                          "from t_un_poke p,t_un_task t,t_produce_sorttrough h "+
                          "where p.sortnum=t.sortnum and p.troughnum=h.troughnum and h.troughtype=10 and h.cigarettetype=40 and h.state=10 and p.status="+status+
                          "order by p.linenum,p.sortnum,p.secsortnum,p.machineseq,p.troughnum";
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
                String machineseq = "", cusname = "", cigarename = "", pokenum = "", sortnum = "", regioncode = "";
                while (myread != null && myread.Read())
                {
                    machineseq = myread["machineseq"].ToString().Trim();
                    cusname = myread["customername"].ToString().Trim();
                    cigarename = myread["cigarettename"].ToString().Trim();
                    pokenum = myread["pokenum"].ToString().Trim();
                    sortnum = myread["sortnum"].ToString().Trim();
                    regioncode = myread["regioncode"].ToString().Trim();
                    int index = this.task_data.Rows.Add();
                    this.task_data.Rows[index].Cells[0].Value = machineseq;
                    this.task_data.Rows[index].Cells[1].Value = cusname;
                    this.task_data.Rows[index].Cells[2].Value = cigarename;
                    this.task_data.Rows[index].Cells[3].Value = pokenum;
                    this.task_data.Rows[index].Cells[4].Value = sortnum;
                    this.task_data.Rows[index].Cells[5].Value = regioncode;
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


        private void btn_print_Click(object sender, EventArgs e)
        {
            dgVprint1.MainTitle = "混合道补货顺序";
            //dgVprint1.SubTitle = "这是子标题，当然也可以不设的";
           // dgVprint1.PaperLandscape = true;//用横向打印，默认是纵向

            dgVprint1.Print(task_data);
        }

       

      

       
    }
}
