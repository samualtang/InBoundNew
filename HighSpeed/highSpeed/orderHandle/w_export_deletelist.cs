using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using highSpeed.PubFunc;
using System.Net.Sockets;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Net;
using System.IO.Compression;
using InBound;
using System.Data.OracleClient;

namespace highSpeed.orderHandle
{
    public partial class w_export_deletelist : Form
    {
        DataSet ds1 = new DataSet();
        public WriteLog writeLog = WriteLog.GetLog();
        w_main wm = new w_main();
        public delegate void HandleScheduleing(int falge, bool isOrnot);
        HandleScheduleing handleschedule;
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        static bool isScheduleing = false;

        DataSet ds2 = new DataSet();
        DataBase Db = new DataBase();
        Socket socketClient;
        public w_export_deletelist()
        {
            InitializeComponent();
            seek1();
            seek2();
            cmb_package.SelectedIndex = 0;
        }
        private void btn_ReDateBing_Click(object sender, EventArgs e)
        {
            seek2();
        }
        private void seek2()
        {
            //string time = this.orderdate.Text;
            //time=DateTime.Parse(time,"yyyy-MM-dd");

            String strsql = "SELECT batchcode, sum(t.taskquantity) as qty, COUNT(*) as cuscount, t.synseq, count(distinct regioncode) as regioncodecount from t_return_task t group BY t.batchcode, t.synseq order by synseq ";
            //MessageBox.Show(strsql);
            Bind2(strsql);
        }

        #region 查询
        /// <summary>
        /// 绑定DataGridView1
        /// </summary>
        /// <param name="sql">要查询的sql</param>
        private void Bind2(string sql)
        {
            try
            {
                ds2.Clear();
                //da.SelectCommand = new OracleCommand(sql, cn);
                //da.Fill(ds, "TB_Inpatient_info");


                ds2 = Db.QueryDs(sql);


                this.orderdata2.DataSource = ds2.Tables[0];
                this.orderdata2.AutoGenerateColumns = false;

                string columnwidths = pub.IniReadValue(this.Name, this.orderdata2.Name);
                if (columnwidths != "")
                {
                    string[] columns = columnwidths.Split(',');
                    int j = 0;
                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (orderdata2.Columns[i].Visible == true)
                        {
                            orderdata2.Columns[j].Width = Convert.ToInt32(columns[i]);
                            j = j + 1;
                        }
                    }
                }
                orderdata2.ClearSelection();

            }
            catch (Exception ex)
            {
                // throw ex;
            }
        }
        #endregion

        private void win_export_Load(object sender, EventArgs e)
        {
            orderdata2.ClearSelection();
        }
        //多条打码机的数据可以写到一个文件上发送过去，不过每次发送的明细不能超过一万条
        private void export(string synseq, string lineno, string groupno, string linetype)//lineno打码机编号
        {
            this.btn_export.Enabled = false;
            panel2.Visible = true;
            label2.Visible = true;
            progressBar1.Visible = true;
            label2.Text = "数据查询中，准备进行数据导出...";
            label2.Refresh();
            int taskseq = 0, seq = 1;
            String tasknum = "", cuscode = "", cusname = "", itemno = "", itemname = "", quantity = "", regioncode = "", orderdate = "", cuscodetmp = "";


            String sql = "select k.tasknum as sortnum,k.customercode,k.customername,e.cigarettecode,e.cigarettename,e.quantity,k.orderdate as ODATE,k.regioncode,k.packageid as sortname  from t_return_task k join t_return_taskline e on k.tasknum = e.tasknum";


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
                    tmpInfo = tmpInfo + tasknum + "," + tasknum + "," + cuscode + "," + cusname + "," + itemno + "," + itemname + "," + quantity + "," + onesynseq + "," + seq + "," + regioncode + "," + regioncode + "," + orderdate + "," + orderdate + "," + lineno + ",1;\r\n";

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
            this.btn_export.Enabled = true;
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            int count = this.orderdata2.SelectedRows.Count;
            if (count > 0)
            {
                String synseq = this.orderdata2.SelectedRows[0].Cells["synseq"].Value + "";
                // String exportnum = this.orderdata.SelectedRows[0].Cells["exportnum"].Value + "";
                //取页面参数

                export(synseq, "01", "1", "2");

                // export(synseq,exportnum);
            }
            else
            {
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

            String[] destination = path.Split('.');
            //MessageBox.Show(destination[0] + ".zip");
            File.Move(path + ".zip", destination[0] + ".zip");

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

        ///////////////////////////*****************************************************///////////////////////////////



        private void seek1()
        {
            Entities et = new Entities(); 
            this.txt_codestr.Text = "";
            String str = "select r.routecode as regioncode, sum(r.totalqty) as qty, count(distinct r.orderno) as cuscount from t_wms_shiporder r where r.orderdate in (select distinct o.orderdate from t_produce_order o) and r.routecode in (select distinct o.regioncode from t_produce_order o) and r.orderno in (select distinct t.orderno from t_wms_deletelist t where t.state='接收') group by r.routecode";

            String strsql = "with lst as (" + str + ") select rownum,lst.* from lst";
            Bind1(strsql);

            this.txt_codestr.Text = "";
        }

        #region 查询
        /// <summary>
        /// 绑定DataGridView1
        /// </summary>
        /// <param name="sql">要查询的sql</param>
        private void Bind1(string sql)
        {
            try
            {
                Db = new DataBase();
                ds1.Clear();
                //da.SelectCommand = new OracleCommand(sql, cn);
                //da.Fill(ds, "TB_Inpatient_info");


                ds1 = Db.QueryDs(sql);

                //进度条显示



                panel2.Visible = true;
                label2.Visible = true;
                progressBar1.Visible = true;
                int rcounts = ds1.Tables[0].Rows.Count;
                progressBar1.Value = 0;
                for (int i = 0; i < rcounts; i++)
                {
                    Application.DoEvents();
                    progressBar1.Value = ((i + 1) * 100 / rcounts);
                    progressBar1.Refresh();
                    label2.Text = "正在读取数据..." + ((i + 1) * 100 / rcounts).ToString() + "%";
                    label2.Refresh();
                }
                panel2.Visible = false;
                label2.Visible = false;
                progressBar1.Visible = false;

                this.orderdata1.DataSource = ds1.Tables[0];
                orderdata1.Sort(orderdata1.Columns[1], ListSortDirection.Ascending);//默认车组排序
                this.orderdata1.AutoGenerateColumns = false;

                string columnwidths = pub.IniReadValue(this.Name, this.orderdata1.Name);
                if (columnwidths != "")
                {
                    string[] columns = columnwidths.Split(',');
                    int j = 0;
                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (orderdata1.Columns[i].Visible == true)
                        {
                            orderdata1.Columns[j].Width = Convert.ToInt32(columns[i]);
                            j = j + 1;
                        }
                    }
                }
                orderdata1.ClearSelection();
                Db.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void win_schedule_Load(object sender, EventArgs e)
        {
            orderdata1.ClearSelection();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            DialogResult MsgBoxResult = MessageBox.Show("是否确定重置排程顺序？",//对话框的显示内容 
                                                            "提示",//对话框的标题 
                                                            MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                            MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                            MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
            if (MsgBoxResult == DialogResult.Yes)
            {
                this.txt_codestr.Text = "";
                int count = this.orderdata1.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    this.orderdata1.Rows[i].Cells[0].Value = "false";
                }
            }
        }

        private void orderdata1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (orderdata1.RowCount > 0)
                {
                    bool obj = (bool)this.orderdata1.CurrentRow.Cells[0].EditedFormattedValue;
                    //MessageBox.Show(obj);

                    String czcode = this.orderdata1.CurrentRow.Cells[2].Value + "";
                    //MessageBox.Show(obj.ToString());
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

        private void btn_schedule_Click(object sender, EventArgs e)
        {
            try
            {
                Entities et = new Entities();
                string synseq = et.T_RETURN_TASK.Max(x => x.SYNSEQ).ToString();
                if (synseq!="")
                {
                    synseq = (Convert.ToInt32(synseq) + 1).ToString();
                }
                else
                {
                    synseq = "1000";
                }
                isScheduleing = true;
                Db.Open();
                String codestr = this.txt_codestr.Text.Trim();
                //DateTime time = DateTime.Parse(this.datePick.Value.ToString());
                //String date = string.Format("{0:d}", time);
                OracleParameter[] sqlpara = new OracleParameter[1];
                string hasBatchcode = getBatchcode();
                string errcode = "", errmsg = ""; string indexstr = "";
                string package = cmb_package.Text.ToString();
                if (hasBatchcode != "0")
                {
                    if (codestr != "")
                    {
                        DialogResult MsgBoxResult = MessageBox.Show("车组排程顺序为【" + codestr.Substring(1) + "】，是否确定按照该顺序进行手工线排程？",//对话框的显示内容 
                                                                    "提示",//对话框的标题 
                                                                    MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                                    MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                                    MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
                        if (MsgBoxResult == DialogResult.Yes)
                        {
                            btn_schedule.Enabled = false;
                            Db.Open();
                            String[] code = codestr.Substring(1).Split(',');
                            int len = code.Length;
                            string splitval = "1000";// this.txt_splitval.Text.Trim();
                            for (int i = 0; i < len; i++)
                            {
                                panel2.Visible = true;
                                label2.Visible = true;
                                progressBar1.Visible = true;
                                int rcounts = ds1.Tables[0].Rows.Count;
                                progressBar1.Value = 0;
                                Application.DoEvents();
                                if (i == 0) label2.Text = "正在对" + code[i] + "车组订单数据进行手工线排程...";
                                //MessageBox.Show(label2.Text);
                                sqlpara = new OracleParameter[5];
                                //sqlpara[0] = new OracleParameter("p_time", date);
                                sqlpara[0] = new OracleParameter("p_code", code[i]);
                                sqlpara[1] = new OracleParameter("p_package", package);
                                sqlpara[2] = new OracleParameter("p_synseq", synseq);
                                sqlpara[3] = new OracleParameter("p_ErrCode", OracleType.VarChar, 30);
                                sqlpara[4] = new OracleParameter("p_ErrMsg", OracleType.VarChar, 100);

                                sqlpara[3].Direction = ParameterDirection.Output;
                                sqlpara[4].Direction = ParameterDirection.Output;
                                Db.ExecuteNonQueryWithProc("P_RETURN_SCHEDULE", sqlpara);
                                errcode = sqlpara[3].Value.ToString();
                                errmsg = sqlpara[4].Value.ToString();
                                //进度条显示 
                                progressBar1.Value = ((i + 1) * 100 / len);
                                progressBar1.Refresh();
                                String tmpstr = "";
                                if (errcode == "1")
                                {
                                    if (i + 1 < len) tmpstr = "正在对" + code[i + 1] + "车组订单数据进行手工线排程...";
                                    else tmpstr = "";
                                    label2.Text = code[i] + "车组订单数据预排程结束..." + tmpstr;
                                    label2.Refresh();
                                    indexstr = indexstr + "," + code[i];
                                }
                                else
                                {
                                    label2.Text = errmsg;
                                    MessageBox.Show(errmsg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    label2.Refresh();
                                    break;
                                }
                            }
                            panel2.Visible = false;
                            label2.Visible = false;
                            progressBar1.Visible = false;
                            this.lab_showinfo.Text = errmsg;
                            MessageBox.Show(errmsg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.txt_codestr.Text = "";


                            if (indexstr != "")
                            {
                                indexstr = indexstr.Substring(1);
                                DataTable dt_new = ds1.Tables[0];
                                DataRowCollection drc = dt_new.Rows;
                                String[] indexArr = indexstr.Split(',');
                                for (int j = 0; j < indexArr.Length; j++)
                                {
                                    Console.WriteLine(indexArr[indexArr.Length - 1 - j]);

                                    drc.RemoveAt(Convert.ToInt32(indexArr.Length - 1 - j));

                                    //MessageBox.Show(indexArr[indexArr.Length - 1 - j]);
                                }

                                this.orderdata1.DataSource = dt_new;
                                this.orderdata1.AutoGenerateColumns = false;
                            }
                        }

                        seek1();
                    }
                    else
                    {
                        MessageBox.Show("请至少选择一个要手工线排程的车组!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("请添加一个新的批次,再进行预排程操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                btn_schedule.Enabled = true;
            }
            catch (Exception ex)
            {
                writeLog.Write("排程异常：" + ex.Message);

            }
            finally
            {
                //handleschedule(2, false);
                isScheduleing = false;
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            seek1();
        }

        private void txt_splitval_Leave(object sender, EventArgs e)
        {
            string str = "10000";// this.txt_splitval.Text.Trim();
            if (str == "" || str == "0")
            {
                //this.txt_splitval.Text = "1000";
            }
        }

        private string getBatchcode()
        {
            string str = "";
            string sql = "SELECT count(*)  FROM t_produce_batch WHERE state=10 and batchtype=10";
            DataTable dt = Db.Query(sql);
            str = dt.Rows[0][0].ToString();
            return str;
        }


        void Sort()
        {



            OracleParameter[] sqlpara;
            sqlpara = new OracleParameter[2];
            sqlpara[0] = new OracleParameter("p_ErrCode", OracleType.VarChar, 30);
            sqlpara[1] = new OracleParameter("p_ErrMsg", OracleType.VarChar, 100);
            sqlpara[0].Direction = ParameterDirection.Output;
            sqlpara[1].Direction = ParameterDirection.Output;
            Db.Open();
            Db.ExecuteNonQueryWithProc("P_PRODUCE_SCHEDULE", sqlpara);//更改前存储过程  P_PRODUCE_updatesortnum
            //MessageBox.Show(date);
            //MessageBox.Show(code[i]+"订单数据接收完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            String errcode = sqlpara[0].Value.ToString();
            String errmsg = sqlpara[1].Value.ToString();
            if (errcode == "1")
            {
                MessageBox.Show("分拣车组任务排序成功！");
            }

            else
            {
                MessageBox.Show(errmsg);
            }

            //  panel2.Visible = false;
            updateControl(panel2, false, true);
            //  label2.Visible = false;
            updateControl(label2, false, true);
            //  progressBar1.Visible = false;
            updateControl(progressBar1, false, true);
        }

        private delegate void HandleDelegate1(Control control, bool isvisible, bool isenable);
        public void updateControl(Control control, bool isvisible, bool isenable)
        {

            if (control.InvokeRequired)
            {
                //   this.txtreceive.BeginInvoke(new ShowDelegate(Show), strshow);//这个也可以

                control.Invoke(new HandleDelegate1(updateControl), new Object[] { control, isvisible, isenable });
            }
            else
            {
                control.Visible = isvisible;
                control.Enabled = isenable;
            }
        }
        private void btn_all_Click(object sender, EventArgs e)
        {
            String czcodestr = "";
            for (int i = 0; i < this.orderdata1.RowCount; i++)
            {
                orderdata1.Rows[i].Cells[0].Value = "true";
                czcodestr = czcodestr + "," + orderdata1.Rows[i].Cells[2].Value + "";
            }
            this.txt_codestr.Text = czcodestr;
        }

        private void win_schedule_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isScheduleing)
            {
                e.Cancel = true;
                MessageBox.Show("正在预排程！请等待预排程结束！");
                return;
            }
        } 
    }


}
