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
using System.Net;
using System.Net.Sockets;


namespace highSpeed.orderHandle
{
    public partial class w_export_DIY : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        Socket socketClient;
        public w_export_DIY()
        {
            InitializeComponent();
            seek();
        }

        private void seek()
        {
            //string time = this.orderdate.Text;
            //time=DateTime.Parse(time,"yyyy-MM-dd");

            String strsql = "SELECT batchcode,sum(t.taskquantity) as qty,COUNT(*)as cuscount,t.synseq,count(distinct regioncode) as regioncodecount from T_UN_DIY_TASK t where t.state=15 group BY t.batchcode,t.synseq order by synseq ";
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
            this.btn_export.Enabled = false;
            panel2.Visible = true;
            label2.Visible = true;
            progressBar1.Visible = true;
            label2.Text = "数据查询中，准备进行数据导出...";
            label2.Refresh();
            int taskseq = 0, seq = 1; 
            String tasknum = "", cuscode = "", cusname = "", itemno = "", itemname = "", quantity = "", regioncode = "", orderdate = "", cuscodetmp = "";
            //task表中不知道大品牌拆到了哪几组，所以数据只能从poke表中获取
            /*String sql = "SELECT a.tasknum,a.customercode,a.customername,b.cigarettecode,b.cigarettename,b.quantity,a.regioncode,to_char(c.enterdate,'yyyy-mm-dd') AS enterdate " +
                         "FROM highspeed.t_produce_task a,highspeed.t_produce_taskline b,dbm.twms_excpo c WHERE a.tasknum=b.tasknum " +
                         " and a.billcode=c.extnum and a.synseq='" + synseq + "' and a.exportnum='" + "" + "' ORDER BY a.tasknum";
            */
            //String sql = "select * from (" +//原来的SQL
            //            " select p.sortnum ,t.customercode,t.customername,h.cigarettecode,h.cigarettename ,p.pokenum as quantity,to_char(t.orderdate,'yyyy-mm-dd') as odate,t.regioncode,r.sortname " +
            //            " from t_produce_task t,t_produce_poke p,t_produce_sorttrough h,t_produce_sortlinename r " +
            //            " where t.tasknum = p.tasknum and p.troughnum = h.troughnum and h.troughtype=10 and h.cigarettetype=20 and h.state=10 and r.groupno=p.packagemachine and r.ctype=1 and t.synseq=" + synseq +
            //            " union all " +
            //            " SELECT aa.sortnum,aa.customercode,aa.customername,hh.cigarettecode,hh.cigarettename,pp.pokenum as quantity,to_char(aa.orderdate,'yyyy-mm-dd') as odate,aa.regioncode,rr.sortname " +
            //            " FROM t_un_task aa,t_produce_sorttrough hh,t_un_poke pp, t_produce_sortlinename rr " +
            //            " WHERE aa.tasknum=pp.tasknum  and rr.groupno=pp.linenum and pp.troughnum=hh.troughnum and hh.troughtype=10 and hh.cigarettetype in (30,40) and hh.state='10' " +
            //            " and aa.synseq=" + synseq + " and rr.ctype=2 )" +
            //            " order by sortnum,sortname ";

            //String sql = " select * from ( " +
            //            " SELECT aa.sortnum,aa.customercode,aa.customername,pp.machineseq,hh.cigarettecode,hh.cigarettename,pp.pokenum as quantity,to_char(aa.orderdate,'yyyy-mm-dd') as odate,aa.regioncode,rr.sortname " +
            //            " FROM t_un_task aa,t_produce_sorttrough hh,t_un_poke pp, t_produce_sortlinename rr " +
            //            " WHERE aa.tasknum=pp.tasknum  and rr.groupno=aa.mainbelt and pp.troughnum=hh.troughnum and hh.troughtype=10 and hh.cigarettetype in (30,40) and hh.state='10' " +
            //            " and aa.synseq=" + synseq + " and rr.ctype=2 order by sortnum,sortname,machineseq ) ";
            String sql ="select * from ( " +
                        " SELECT aa.tasknum,aa.customercode,aa.customername,5001 as machineseq   ,pp.cigarettecode,pp.cigarettename,pp.quantity as quantity,to_char(aa.orderdate,'yyyy-mm-dd') as odate,aa.regioncode,rr.sortname  " +
                        " FROM T_UN_DIY_TASK aa ,T_UN_DIY_TASKLINE pp , t_produce_sortlinename rr "+
                        " WHERE aa.tasknum=pp.tasknum    and aa.linenum = rr.groupno and rr.ctype = 2 "+
                        " and aa.synseq=" + synseq + "    order by tasknum,sortname,machineseq ) ";
                        

            
            //取批次号
            String batchcodesql = "select SEQ_ONEHAOGONGCHENG.Nextval from dual";

            

            Db.Open();
            DataTable dtseq = Db.Query(batchcodesql);
            String onesynseq = dtseq.Rows[0][0].ToString();//一号工程批次号


            DataTable table = Db.Query(sql);
            int len = table.Rows.Count;
            //String[] infostr = new String[len];
            
            if (len > 0)
            {
                InitSocket();
                //DataRow row = new DataRow();
                //创建到处目录
                String folder = "HighSpeedExportData";
                if (!Directory.Exists("D:\\" + folder))
                {
                    Directory.CreateDirectory("D:\\" + folder);
                }
                int count = 0,fileSeq=1,rowCcount=0,bz=0,succCount=0;
                String fileNameStr = ""; String info = "",tmpInfo="",tempCode="",unSuccFile="";
                for (int i = 0; i < len; i++)
                {
                    progressBar1.Value = ((i + 1) * 100 / len);
                    progressBar1.Refresh();

                    DataRow row = table.Rows[i];

                    tasknum = row["TASKNUM"].ToString();
                    cuscode = row["CUSTOMERCODE"].ToString();
                    cusname = row["CUSTOMERNAME"].ToString();
                    itemno = row["CIGARETTECODE"].ToString();
                    itemname = row["CIGARETTENAME"].ToString();
                    quantity = row["QUANTITY"].ToString();
                    regioncode = row["REGIONCODE"].ToString();
                    orderdate = row["ODATE"].ToString();                    
                    lineno = row["SORTNAME"].ToString();
                    taskseq++;
                    rowCcount = rowCcount + 1;

                    label2.Text = "正在导出第" + fileSeq + "个文件...";
                    label2.Refresh();
                    //取下条记录比较车组与零售户
                    if (i + 1 < len)
                    {
                        cuscodetmp = table.Rows[i + 1]["CUSTOMERCODE"].ToString();
                        tempCode = table.Rows[i + 1]["REGIONCODE"].ToString();
                    }
                    else {
                        cuscodetmp = "";
                        tempCode = "";
                    }
                    //infostr[i] = tasknum + taskseq + "," + tasknum + "," + cuscode + "," + cusname + "," + itemno + "," + itemname + "," + quantity + ",2," + seq + "," + regioncode + "," + regioncode + "," + orderdate + "," + orderdate + ",S1001,1";
                    tmpInfo = tmpInfo + tasknum + "," + tasknum + "," + cuscode + "," + cusname + "," + itemno + "," + itemname + "," + quantity + "," + onesynseq + "," + seq + "," + regioncode + "," + regioncode + "," + orderdate + "," + DateTime.Now.Date.ToString("yyyy-MM-dd") + "," + lineno + ",1;\r\n";

                    if (cuscode != cuscodetmp)
                    {
                        taskseq = 0;
                        seq++;
                    }
                    //当下个车组与当前不同时
                    if (!tempCode.Equals(regioncode))
                    {
                        //判断累计记录数是否大于10000，大于则将之前的记录数导出，否则将记录数合并
                        if (rowCcount + count > 1000000000)
                        {
                            label2.Text = "正在压缩第" + fileSeq + "个文件...";
                            label2.Refresh();
                            //将之前的记录信息导出
                            DateTime dt = DateTime.Now;
                            String time = string.Format("{0:yyyyMMddHHmmssfff}", dt);
                            String filename = "RetailerOrder-" + time + "-" + synseq + "-" + fileSeq;
                            fileNameStr = fileNameStr + "," + filename + ".zip";
                            StreamWriter sw = new StreamWriter("D:\\HighSpeedExportData\\" + filename + ".Order", false, Encoding.UTF8);
                            sw.WriteLine(info.Substring(0, info.Length - 1));
                            sw.Close();//写入
                            //CompressFile("D:\\" + filename + ".Order");
                            GetFileToZip("D:\\HighSpeedExportData\\" + filename + ".Order", "D:\\HighSpeedExportData\\" + filename + ".zip", filename + ".Order");
                            //发送数据
                            label2.Text = "正在发送第" + fileSeq + "个文件";
                            label2.Refresh();
                            bz=sendFile("D:\\HighSpeedExportData\\" + filename + ".zip");
                            //记录发送成功数量和失败文件信息
                            if (bz == 0)
                            {
                                succCount = succCount + 1;
                                label2.Text = "第" + fileSeq + "个文件发送完毕!";
                            }
                            else {
                                unSuccFile = unSuccFile + "," + filename + ".zip";
                                label2.Text = "第" + fileSeq + "个文件发送失败!";
                            } 
                            
                            label2.Refresh();
                            //记录新车组的信息
                            fileSeq = fileSeq + 1;
                            count = rowCcount;
                            info = tmpInfo;
                        }
                        else {
                            count = rowCcount + count;
                            info = info + tmpInfo;
                        }

                        //判断循环是否完成（是否为最后一条记录）,如果是最后一条，则将剩余记录导出
                        if ("".Equals(tempCode)) {
                            //fileSeq = fileSeq + 1;
                            label2.Text = "正在压缩第" + fileSeq + "个文件";
                            label2.Refresh();
                            DateTime dt = DateTime.Now;
                            String time = string.Format("{0:yyyyMMddHHmmssfff}", dt);
                            String filename = "RetailerOrder-" + time + "-" + synseq + "-" + fileSeq;
                            fileNameStr = fileNameStr + "," + filename + ".zip";
                            StreamWriter sw = new StreamWriter("D:\\HighSpeedExportData\\" + filename + ".Order", false, Encoding.UTF8);
                            sw.WriteLine(info.Substring(0, info.Length - 1));
                            sw.Close();//写入
                            //CompressFile("D:\\" + filename + ".Order");
                            GetFileToZip("D:\\HighSpeedExportData\\" + filename + ".Order", "D:\\HighSpeedExportData\\" + filename + ".zip", filename + ".Order");
                            //发送数据
                            //sendFile("D:\\HighSpeedExportData\\" + filename + ".zip");
                            label2.Text = "正在发送第" + fileSeq + "个文件";
                            label2.Refresh();
                            bz = sendFile("D:\\HighSpeedExportData\\" + filename + ".zip");
                            //记录发送成功数量和失败文件信息
                            if (bz == 0)
                            {
                                succCount = succCount + 1;
                                label2.Text = "第" + fileSeq + "个文件发送完毕!";
                            }
                            else
                            {
                                unSuccFile = unSuccFile + "," + filename + ".zip";
                                label2.Text = "第" + fileSeq + "个文件发送失败!";
                            }

                            label2.Refresh();
                        }
                        tmpInfo = "";
                        rowCcount = 0;
                    }
                    
                }
                //在弹窗前关闭控件
                panel2.Visible = false;
                label2.Visible = false;
                progressBar1.Visible = false;

                String msg = "成功导出" + fileSeq + "个文件，成功发送" + succCount + "个文件！";
                //导出的文件的所有文件名
                if (!"".Equals(fileNameStr)) fileNameStr = fileNameStr.Substring(1);
                //发送失败的所有文件名
                if (!"".Equals(unSuccFile))
                {
                    unSuccFile = unSuccFile.Substring(1);
                    msg = msg + "其中发送失败文件为(" + unSuccFile + ")！";
                }
                else {
                    msg = msg + "文件名为(" + fileNameStr + ")！";
                } 

                MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                socketClient.Disconnect(false);
                socketClient.Close();
            }
            this.btn_export.Enabled = true;
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
            /*InitSocket();
            int i = sendFile("D:\\HighSpeedExportData\\RetailerOrder-20180326111028259-1-2.zip");
            if (i == 0) MessageBox.Show("文件发送成功!");
            else MessageBox.Show("文件发送失败!");*/
        }

        private void InitSocket()
        {
            //IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            IPAddress address = IPAddress.Parse("10.75.142.1");
            IPEndPoint endpoint = new IPEndPoint(address, 9050);
            //创建服务端负责监听的套接字，参数（使用IPV4协议，使用流式连接，使用Tcp协议传输数据）
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketClient.Connect(endpoint);
        }

        private int sendFile(String filePath) {
            int i = SocketClientConnector.SendFile(socketClient, filePath, 102400, 1);
            //if (i == 0) MessageBox.Show("文件  " + filePath + "  发送失败!");
            Byte[] bytes = new Byte[1024];
            int len=socketClient.Receive(bytes);
            //String result=System.Text.UTF8Encoding.UTF8.GetString(bytes);
            String result = Encoding.Default.GetString(bytes,0,len);
            if(!"".Equals(result)){
                //result = result.Substring(8, result.Length-1);
                String[] msg = System.Text.RegularExpressions.Regex.Split(result,"\\r\\n");

                if (msg.Length == 2)
                {
                    MessageBox.Show("文件解析成功！");
                    i = 0;
                }
                else {
                    MessageBox.Show("文件解析失败！错误信息："+msg[1]);
                    i = -1;
                }
            }
            //socketClient.Disconnect(false);
            //socketClient.Close();
            return i;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            seek();
        }


    }
}
