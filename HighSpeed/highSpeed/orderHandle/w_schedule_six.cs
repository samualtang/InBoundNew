using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using highSpeed.PubFunc;
using System.IO;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip;
using System.Net.Sockets;
using System.Net;

namespace highSpeed.orderHandle
{
    public partial class w_schedule_six : Form
    {
        Socket socketClient;
        public w_schedule_six()
        {
            InitializeComponent(); 
        }

        private void btn_refresh_sgx_Click(object sender, EventArgs e)
        {
            databinding_sgx();
            this.txt_codestr.Text = "";
        }
        DataBase Db = new DataBase();
        DataSet ds1 = new DataSet();
        DataSet ds2 = new DataSet();
        public void databinding_sgx()
        {
            String sql = "select  REGIONCODE as 车组号 ,count(*) as 户数,Sum(TASKQUANTITY) as 数量 ,decode(STATE,0,'新增') as 状态,ORDERDATE  as 订单日期 from t_un_diy_task where state = 0  group by REGIONCODE,STATE,ORDERDATE  order by REGIONCODE";

            Db.Open();
            ds1.Clear();
            ds1 = Db.QueryDs(sql);   
            int rcounts = ds1.Tables[0].Rows.Count;
            dgv_sgx.DataSource = null;//处理IndexOutOfRangeException异常

            this.dgv_sgx.DataSource = ds1.Tables[0];  
            Db.Close();
        }

        private void btn_schedule_Click(object sender, EventArgs e)
        {
            if (txt_codestr.Text.Length>0)
            {
                String[] codelist = this.txt_codestr.Text.Substring(1).Split(',');

                if (InBound.Business.SixSchedule.sixorderschedule(codelist))
                {
                    MessageBox.Show("六三六订单手工线，排程成功！");
                    txt_codestr.Text = "";
                    databinding_sgx();
                }
                else
                {
                    MessageBox.Show("六三六订单手失败，排程失败！");
                }
            }
            
        }

        private void dgv_sgx_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (dgv_sgx.RowCount > 0)
                {
                    bool obj = (bool)this.dgv_sgx.CurrentRow.Cells[0].EditedFormattedValue;

                    String czcode = this.dgv_sgx.CurrentRow.Cells[1].Value + ""; 
                    String czcodestr = this.txt_codestr.Text;
                    if (obj)
                    {
                        if (!czcodestr.Contains(czcode))
                        {
                            czcodestr = czcodestr + "," + czcode;
                        }
                    }
                    else
                    {
                        czcodestr = czcodestr.Replace("," + czcode, "");
                    }
                    this.txt_codestr.Text = czcodestr;
                }
            }
        }

        public void databinding_yhgc()
        {
            string sql = "SELECT batchcode as 批次编码,sum(t.taskquantity) as 数量,COUNT(*) as 户数,t.synseq 批次号,count(distinct regioncode) as 车组数 from t_un_diy_task t  where t.state=15 group BY t.batchcode,t.synseq order by synseq ";
            Db.Open();
            ds2.Clear();
            ds2 = Db.QueryDs(sql);
            int rcounts = ds2.Tables[0].Rows.Count;
            dgc_yhgc.DataSource = null;//处理IndexOutOfRangeException异常

            this.dgc_yhgc.DataSource = ds2.Tables[0];
            Db.Close();
        }
        private void btn_refresh_yhgc_Click(object sender, EventArgs e)
        {
            databinding_yhgc();
        }

        private void SendTask_yhgc_Click(object sender, EventArgs e)
        {
            int count = this.dgc_yhgc.SelectedRows.Count;
            if (count > 0)
            {
                String synseq = this.dgc_yhgc.SelectedRows[0].Cells[3].Value + ""; 

                export(synseq, "01", "1", "2");

                // export(synseq,exportnum);
            }
            else
            {
                MessageBox.Show("请点击选择您要导出的数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //多条打码机的数据可以写到一个文件上发送过去，不过每次发送的明细不能超过一万条
        private void export(string synseq, string lineno, string groupno, string linetype)//lineno打码机编号
        {
            this.SendTask_yhgc.Enabled = false;
            panel2.Visible = true;
            label2.Visible = true;
            progressBar1.Visible = true;
            label2.Text = "数据查询中，准备进行数据导出...";
            label2.Refresh();
            int taskseq = 0, seq = 1;
            String tasknum = "", cuscode = "", cusname = "", itemno = "", itemname = "", quantity = "", regioncode = "", orderdate = "", cuscodetmp = "";
            String sql = "select * from ( " +
                        " SELECT aa.tasknum,aa.customercode,aa.customername,5001 as machineseq   ,pp.cigarettecode,pp.cigarettename,pp.quantity as quantity,to_char(aa.orderdate,'yyyy-mm-dd') as odate,aa.regioncode,rr.sortname  " +
                        " FROM T_UN_DIY_TASK aa ,T_UN_DIY_TASKLINE pp , t_produce_sortlinename rr " +
                        " WHERE aa.tasknum=pp.tasknum    and aa.linenum = rr.groupno and rr.ctype = 2 " +
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
                int count = 0, fileSeq = 1, rowCcount = 0, bz = 0, succCount = 0;
                String fileNameStr = ""; String info = "", tmpInfo = "", tempCode = "", unSuccFile = "";
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
                    else
                    {
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
                            //记录新车组的信息
                            fileSeq = fileSeq + 1;
                            count = rowCcount;
                            info = tmpInfo;
                        }
                        else
                        {
                            count = rowCcount + count;
                            info = info + tmpInfo;
                        }

                        //判断循环是否完成（是否为最后一条记录）,如果是最后一条，则将剩余记录导出
                        if ("".Equals(tempCode))
                        {
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
                else
                {
                    msg = msg + "文件名为(" + fileNameStr + ")！";
                }

                MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                socketClient.Disconnect(false);
                socketClient.Close();
            }
            this.SendTask_yhgc.Enabled = true;
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

        private int sendFile(String filePath)
        {
            int i = SocketClientConnector.SendFile(socketClient, filePath, 102400, 1);
            //if (i == 0) MessageBox.Show("文件  " + filePath + "  发送失败!");
            Byte[] bytes = new Byte[1024];
            int len = socketClient.Receive(bytes);
            //String result=System.Text.UTF8Encoding.UTF8.GetString(bytes);
            String result = Encoding.Default.GetString(bytes, 0, len);
            if (!"".Equals(result))
            {
                //result = result.Substring(8, result.Length-1);
                String[] msg = System.Text.RegularExpressions.Regex.Split(result, "\\r\\n");

                if (msg.Length == 2)
                {
                    MessageBox.Show("文件解析成功！");
                    i = 0;
                }
                else
                {
                    MessageBox.Show("文件解析失败！错误信息：" + msg[1]);
                    i = -1;
                }
            }
            //socketClient.Disconnect(false);
            //socketClient.Close();
            return i;
        }
        private void GetFileToZip(string filepath, string zippath, String entryname)
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

        private void w_schedule_six_Load(object sender, EventArgs e)
        {
            databinding_sgx();
            databinding_yhgc(); 
        }

        private void btn_all_Click(object sender, EventArgs e)
        {
            String czcodestr = "";
            for (int i = 0; i < this.dgv_sgx.RowCount; i++)
            {
                dgv_sgx.Rows[i].Cells[0].Value = "true";
                czcodestr = czcodestr + "," + dgv_sgx.Rows[i].Cells[1].Value + "";
            }
            this.txt_codestr.Text = czcodestr;
        }
         
    }
}
