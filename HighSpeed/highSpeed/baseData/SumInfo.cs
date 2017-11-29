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
    public partial class SumInfo : Form
    {
        public string sign;
        public string amend_id;
        public SumInfo()
        {
            InitializeComponent();
            initdata();
            //seek();
        }
        public void initdata()
        {
            String sql = "select sum(ceil(h.taskquantity/36)) as sl,h.regioncode as czxx,h.packagedesc "+
                            "FROM highspeed.T_PRODUCE_TASK h "+
                            "group by h.regioncode,h.packagedesc order by h.regioncode,h.packagedesc";
            OracleDataReader myread = DataPublic.ReadDb(sql);
            task_data.Rows.Clear();
            try
            {
                String sl = "", czxx = "", packagedesc = "";
                while (myread != null && myread.Read())
                {
                    sl = myread["sl"].ToString().Trim();
                    czxx = myread["czxx"].ToString().Trim();
                    packagedesc = myread["packagedesc"].ToString().Trim();
                  

                    int index = this.task_data.Rows.Add();
                    this.task_data.Rows[index].Cells[0].Value = czxx;
                    this.task_data.Rows[index].Cells[1].Value = sl;
                    this.task_data.Rows[index].Cells[2].Value = packagedesc;

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
        
    

       
       

       

       
       

      

       
    }
}
