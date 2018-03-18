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
using ICSharpCode.SharpZipLib.Zip;


namespace highSpeed.orderHandle
{
    public partial class win_export : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public win_export()
        {
            InitializeComponent();
            seek();
        }

        private void seek()
        {
            //string time = this.orderdate.Text;
            //time=DateTime.Parse(time,"yyyy-MM-dd");

            String strsql = "SELECT batchcode,sum(t.taskquantity) as qty,COUNT(*)as cuscount,t.synseq,count(distinct regioncode) as regioncodecount from t_produce_task t group BY t.batchcode,t.synseq order by synseq ";
            //MessageBox.Show(strsql);
            Bind(strsql);
        }

        #region 查询
        /// <summary>
        /// 绑定DataGridView1
        /// </summary>
        /// <param name="sql">要查询的sql</param>
        private void Bind(string sql)
        {
            try
            {
                ds.Clear();
                //da.SelectCommand = new OracleCommand(sql, cn);
                //da.Fill(ds, "TB_Inpatient_info");


                ds = Db.QueryDs(sql);


                this.orderdata.DataSource = ds.Tables[0];
                this.orderdata.AutoGenerateColumns = false;

                string columnwidths = pub.IniReadValue(this.Name, this.orderdata.Name);
                if (columnwidths != "")
                {
                    string[] columns = columnwidths.Split(',');
                    int j = 0;
                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (orderdata.Columns[i].Visible == true)
                        {
                            orderdata.Columns[j].Width = Convert.ToInt32(columns[i]);
                            j = j + 1;
                        }
                    }
                }
                orderdata.ClearSelection();

            }
            catch (Exception ex)
            {
               // throw ex;
            }
        }
        #endregion

        private void win_export_Load(object sender, EventArgs e)
        {
            orderdata.ClearSelection();
        }
        //多条打码机的数据可以写到一个文件上发送过去，不过每次发送的明细不能超过一万条
        private void export(string synseq, string lineno,string groupno,string linetype)//lineno打码机编号
        {
            int taskseq = 0, seq = 1; String info = "";
            String tasknum = "", cuscode = "", cusname = "", itemno = "", itemname = "", quantity = "", regioncode = "", orderdate = "", cuscodetmp = "";
            //task表中不知道大品牌拆到了哪几组，所以数据只能从poke表中获取
            /*String sql = "SELECT a.tasknum,a.customercode,a.customername,b.cigarettecode,b.cigarettename,b.quantity,a.regioncode,to_char(c.enterdate,'yyyy-mm-dd') AS enterdate " +
                         "FROM highspeed.t_produce_task a,highspeed.t_produce_taskline b,dbm.twms_excpo c WHERE a.tasknum=b.tasknum " +
                         " and a.billcode=c.extnum and a.synseq='" + synseq + "' and a.exportnum='" + "" + "' ORDER BY a.tasknum";
            */
            String sql = "select p.sortnum ,t.customercode,t.customername,h.cigarettecode,h.cigarettename ,p.pokenum as quantity,to_char(t.orderdate,'yyyy-mm-dd') as odate,t.regioncode,r.sortname " +
                        " from t_produce_task t,t_produce_poke p,t_produce_sorttrough h,t_produce_sortorder r "+
                        " where t.tasknum = p.tasknum and p.troughnum = h.troughnum and h.state=10 and r.groupno=p.groupno and t.synseq="+synseq+
                        " order by p.sortnum,p.groupno ";
           /**
            String sql = SELECT a.tasknum,a.customercode,a.customername,b.cigarettecode,b.cigarettename,c.pokenum,a.regioncode,to_char(a.orderdate,'yyyy-mm-dd') AS enterdate 
                         FROM t_produce_task a,t_produce_sorttrough b,t_produce_poke c 
                         WHERE a.tasknum=c.tasknum  and b.machineseq=c.machineseq and b.troughtype=10 and b.cigarettetype=20 and b.state='10' 
                         and a.synseq='1' and c.groupno=1 ORDER BY c.sortnum;
            
            string unsqlk=    
                            SELECT a.tasknum,a.customercode,a.customername,b.cigarettecode,b.cigarettename,c.pokenum,a.regioncode,to_char(a.orderdate,'yyyy-mm-dd') AS enterdate 
                         FROM t_un_task a,t_produce_sorttrough b,t_un_poke c 
                         WHERE a.tasknum=c.tasknum  and b.troughnum=c.troughnum and b.troughtype=10 and b.cigarettetype in (30,40) and b.state='10' 
                         and a.synseq='1' and c.linenum=2 
                         ORDER BY c.sortnum,c.secsortnum;
            * **/


            //取批次号
            String batchcodesql = "select SEQ_ONEHAOGONGCHENG.Nextval from dual";

            

            Db.Open();
            DataTable dtseq = Db.Query(batchcodesql);
            String onesynseq = dtseq.Rows[0][0].ToString();//一号工程批次号


            DataTable table = Db.Query(sql);
            int len = table.Rows.Count;
            String[] infostr = new String[len];
           
            if (len > 0)
            {
                //DataRow row = new DataRow();
                for (int i = 0; i < len; i++)
                {
                    DataRow row = table.Rows[i];

                    tasknum = row["SORTNUM"].ToString();
                    cuscode = row["CUSTOMERCODE"].ToString();
                    cusname = row["CUSTOMERNAME"].ToString();
                    itemno = row["CIGARETTECODE"].ToString();
                    itemname = row["CIGARETTENAME"].ToString();
                    quantity = row["QUANTITY"].ToString();
                    regioncode = row["REGIONCODE"].ToString();
                    orderdate = row["ODATE"].ToString();                    
                    lineno = row["SORTNAME"].ToString();
                    taskseq++;
                    if (i + 1 < len) cuscodetmp = table.Rows[i + 1]["CUSTOMERCODE"].ToString();
                    else cuscodetmp = "";

                    //infostr[i] = tasknum + taskseq + "," + tasknum + "," + cuscode + "," + cusname + "," + itemno + "," + itemname + "," + quantity + ",2," + seq + "," + regioncode + "," + regioncode + "," + orderdate + "," + orderdate + ",S1001,1";
                    info = info + tasknum + "," + tasknum + "," + cuscode + "," + cusname + "," + itemno + "," + itemname + "," + quantity + "," + onesynseq + "," + seq + "," + regioncode + "," + regioncode + "," + orderdate + "," + orderdate + "," + lineno + ",1;\r\n";

                    if (cuscode != cuscodetmp)
                    {
                        taskseq = 0;
                        seq++;
                    }
                }
                String folder = "HighSpeedExportData";
                if (!Directory.Exists("D:\\" + folder))
                {
                    Directory.CreateDirectory("D:\\" + folder);
                }
                DateTime dt = DateTime.Now;
                String time = string.Format("{0:yyyyMMddHHmmssfff}", dt);
                String filename =  "RetailerOrder" + time;
                StreamWriter sw = new StreamWriter("D:\\HighSpeedExportData\\" + filename + ".Order", false, Encoding.UTF8);
                sw.WriteLine(info.Substring(0, info.Length - 1));
                sw.Close();//写入
                //CompressFile("D:\\" + filename + ".Order");
                GetFileToZip("D:\\HighSpeedExportData\\" + filename + ".Order", "D:\\HighSpeedExportData\\" + filename + ".zip", filename + ".Order");
                //发送数据

                

                MessageBox.Show("数据导出成功，文件为" + "D:\\HighSpeedExportData\\" + filename + ".Order", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            int count = this.orderdata.SelectedRows.Count;
            if(count>0){
                String synseq = this.orderdata.SelectedRows[0].Cells["synseq"].Value + "";
               // String exportnum = this.orderdata.SelectedRows[0].Cells["exportnum"].Value + "";
                //取页面参数

                export(synseq, "01", "1", "2");



               // export(synseq,exportnum);
            }else{
                MessageBox.Show("请点击选择您要导出的数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void CompressFile(string path)
        {

            FileStream sourceFile = File.OpenRead(path);

            //String newfile = path.Substring(0, path.Length - 6);
            FileStream destinationFile = File.Create(path + ".zip");
            


            byte[] buffer = new byte[sourceFile.Length];

            sourceFile.Read(buffer, 0, buffer.Length);



            using (GZipStream output = new GZipStream(destinationFile,

                CompressionMode.Compress))
            {
                
                Console.WriteLine("Compressing {0} to {1}.", sourceFile.Name,

                    destinationFile.Name, false);



                output.Write(buffer, 0, buffer.Length);

            }



            // Close the files.

            sourceFile.Close();

            destinationFile.Close();

            String[]destination=path.Split('.');
            //MessageBox.Show(destination[0] + ".zip");
            File.Move(path + ".zip", destination[0] + ".zip");

        }

        private void GetFileToZip(string filepath, string zippath,String entryname)
        {

            FileStream fs = File.OpenRead(filepath);
            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
            fs.Close();

            FileStream ZipFile = File.Create(zippath);
            ZipOutputStream ZipStream = new ZipOutputStream(ZipFile);
            ZipEntry ZipEntry = new ZipEntry(entryname);
            ZipStream.PutNextEntry(ZipEntry);
            ZipStream.SetLevel(6);

            ZipStream.Write(buffer, 0, buffer.Length);
            ZipStream.Finish();
            ZipStream.Close();
        }
        

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
            Db.Close();
        }
    }
}
