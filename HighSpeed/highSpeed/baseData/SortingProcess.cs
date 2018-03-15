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
    public partial class SortingProcess : Form
    {
        public string sign;
        public string amend_id;
        public SortingProcess()
        {
            InitializeComponent();
            initdata();
            //seek();
        }
        public void initdata()
        {
            String sql = "SELECT rownum as seq ,total.*,finish.finishcuscount,finish.finishqty,round(finishqty/qty*100,2)AS percent FROM " +
                         "(SELECT o.regioncode,COUNT(o.billcode) AS cuscount,SUM(o.taskquantity)AS qty FROM t_produce_task o GROUP BY o.regioncode)total left outer join " +
                         "(SELECT o.regioncode,COUNT(o.billcode) AS finishcuscount,SUM(o.taskquantity)AS finishqty FROM t_produce_task o WHERE state='30' GROUP BY o.regioncode)finish " +
                         "on total.regioncode=finish.regioncode  ORDER BY total.regioncode";
            OracleDataReader myread = DataPublic.ReadDb(sql);
            task_data.Rows.Clear();
            try
            {
                String regioncode = "", cuscount = "", qty = "", finishcuscount = "", finishqty = "", percent = "",seq= "";
                while (myread != null && myread.Read())
                {
                    seq = myread["seq"].ToString().Trim();
                    regioncode = myread["regioncode"].ToString().Trim();
                    cuscount = myread["cuscount"].ToString().Trim();
                    qty = myread["qty"].ToString().Trim();
                    finishcuscount = myread["finishcuscount"].ToString().Trim();
                    if (finishcuscount == "") finishcuscount = "0";
                    finishqty = myread["finishqty"].ToString().Trim();
                    if (finishqty == "") finishqty = "0";
                    percent = myread["percent"].ToString().Trim();
                    if (finishqty == "0") percent = "0";

                    int index = this.task_data.Rows.Add();
                    this.task_data.Rows[index].Cells[0].Value = seq;
                    this.task_data.Rows[index].Cells[1].Value = regioncode;
                    this.task_data.Rows[index].Cells[2].Value = regioncode;
                    
                    this.task_data.Rows[index].Cells[3].Value = finishcuscount + "/" + cuscount;
                    this.task_data.Rows[index].Cells[4].Value = finishqty + "/" + qty;
                    this.task_data.Rows[index].Cells[5].Value = percent + "%";
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

        private void button1_Click(object sender, EventArgs e)
        {
            initdata();
        }
        
    

       
       

       

       
       

      

       
    }
}
