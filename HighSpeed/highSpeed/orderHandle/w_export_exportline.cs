using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using highSpeed.PubFunc;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;

namespace highSpeed.orderHandle
{
    public partial class win_export_exportline : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public win_export_exportline()
        {
            InitializeComponent();
        }

        /**
         * bz=0,则到处左侧出口数据exportnum='L01'
         * bz=1,则到处左侧出口数据exportnum='R02'
         * 
         */
        private void exportTxt(String bz) {
            int taskseq = 0, seq = 1; String info = "",exportnum="";
            if (bz == "0") exportnum = "L01";
            else exportnum = "R02";
            String tasknum = "", cuscode = "", cusname = "", itemno = "", itemname = "", quantity = "", address="",regioncode = "", orderdate = "", companyname = "";
            String sql = "select a.companyname,trunc(plantime)as orderdate,a.tasknum,a.customercode,a.customername,"+
                         "c.CONTACTADDRESS,b.cigarettecode,b.cigarettename,b.quantity,a.regioncode from "+
                         "t_produce_task a,t_produce_taskline b,t_produce_customer c "+
                         "where a.tasknum=b.tasknum and a.customercode=c.CODE and a.exportnum='"+exportnum+"' order by tasknum";
            Db.Open();
            DataTable table = Db.Query(sql);
            int len = table.Rows.Count;
            String[] infostr = new String[len];
            if (len > 0)
            {
                //DataRow row = new DataRow();
                for (int i = 0; i < len; i++)
                {
                    DataRow row = table.Rows[i];

                    tasknum = row["TASKNUM"].ToString();
                    cuscode = row["CUSTOMERCODE"].ToString();
                    cusname = row["CUSTOMERNAME"].ToString();
                    itemno = row["CIGARETTECODE"].ToString();
                    itemname = row["CIGARETTENAME"].ToString();
                    quantity = row["QUANTITY"].ToString();
                    regioncode = row["REGIONCODE"].ToString();
                    orderdate = row["ORDERDATE"].ToString();
                    orderdate = orderdate.Replace("/", "-");
                    companyname = row["COMPANYNAME"].ToString();
                    address=row["CONTACTADDRESS"].ToString();
                    

                    //infostr[i] = tasknum + taskseq + "," + tasknum + "," + cuscode + "," + cusname + "," + itemno + "," + itemname + "," + quantity + ",2," + seq + "," + regioncode + "," + regioncode + "," + orderdate + "," + orderdate + ",S1001,1";
                    info = info + companyname + ",,"+orderdate+",," + tasknum + ",," + cuscode + ",," + cusname + ",,"+address+",," + itemno + ",," + itemname + ",," + quantity + ",," + regioncode + "\r\n";

                }

                DateTime dt = DateTime.Now;
                String time = string.Format("{0:yyyyMMddHHmm}", dt);
                String filename = "FHX" + time;
                StreamWriter sw = new StreamWriter("E:\\" + filename + ".txt", false);
                sw.WriteLine(info.Substring(0, info.Length - 1));
                sw.Close();//写入
                //CompressFile("E:\\" + filename + ".Order");
                MessageBox.Show("数据导出成功，文件为" + "E:\\" + filename + ".txt", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
