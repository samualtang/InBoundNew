﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using highSpeed.PubFunc;
using System.Data.SqlClient;
using System.Data.OracleClient;
using InBound.Business;
using System.Threading;
using InBound;

namespace highSpeed.orderHandle
{
    public partial class win_schedule : Form
    {
        DataSet ds = new DataSet();
        public WriteLog writeLog = WriteLog.GetLog();
        w_main wm = new w_main();
        public delegate void HandleScheduleing(int falge,bool isOrnot);
        HandleScheduleing handleschedule;
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        static bool isScheduleing = false;
        public win_schedule()
        {
            InitializeComponent();
            handleschedule += wm.GetSonFormState;
            seek();
        }

        private void seek()
        {
            //string time = this.orderdate.Text;
            //time=DateTime.Parse(time,"yyyy-MM-dd");
            this.txt_codestr.Text = "";
            string sql ="SELECT regioncode, sum(t.orderquantity) as qty, COUNT(*) as cuscount ,(select decode(count (*),0,'',count (*)) from t_produce_order r where r.customername like '%六三六%' and r.regioncode =t.regioncode) sixordercount,(select sum (e.quantity) from t_produce_orderline e where e.allowsort='非标' and e.billcode in(select r.billcode from t_produce_order r where r.customername like '%六三六%' and r.regioncode =t.regioncode)) sixorderqty from t_produce_order t WHERE state = '新增' group BY t.regioncode order by t.regioncode ";
            String strsql = "with lst as (" + sql + ") select rownum,lst.* from lst";
            //MessageBox.Show(strsql);
            Bind(strsql);

            this.txt_codestr.Text = "";
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
                Db = new DataBase();
                ds.Clear();
                //da.SelectCommand = new OracleCommand(sql, cn);
                //da.Fill(ds, "TB_Inpatient_info");


                ds = Db.QueryDs(sql);

                //进度条显示



                panel2.Visible = true;
                label2.Visible = true;
                progressBar1.Visible = true;
                int rcounts = ds.Tables[0].Rows.Count;
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

                this.orderdata.DataSource = ds.Tables[0];
                orderdata.Sort(orderdata.Columns[1], ListSortDirection.Ascending);//默认车组排序
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
            orderdata.ClearSelection();
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
                int count = this.orderdata.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    this.orderdata.Rows[i].Cells[0].Value = "false";
                }
            }
        }

        private void orderdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (orderdata.RowCount > 0)
                {
                    bool obj = (bool)this.orderdata.CurrentRow.Cells[0].EditedFormattedValue;
                    //MessageBox.Show(obj);

                    String czcode = this.orderdata.CurrentRow.Cells[2].Value + "";
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
                //查询禁用的包装机和主皮带
                handleschedule(2,true);
                isScheduleing = true;
                string mainbeltno = "";
                string packmachineno = "";
                Db.Open();
                DataTable dt1 = Db.Query("select troughnum from t_produce_sorttrough h where h.troughtype = 30 and state = 0 order by troughnum");
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        mainbeltno = mainbeltno + " " + dt1.Rows[i][0].ToString();
                    }
                }
                DataTable dt2 = Db.Query("select troughnum from t_produce_sorttrough h where h.troughtype = 40 and state = 0 order by troughnum");
                if (dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        packmachineno = packmachineno + " " + dt2.Rows[i][0].ToString();
                    }
                }
                if (dt1.Rows.Count > 0 || dt2.Rows.Count > 0)
                {
                    DialogResult re = MessageBox.Show("已被禁用主皮带：" + mainbeltno + "\r已被禁用包装机：" + packmachineno, "是否继续预排程？", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (re == DialogResult.Cancel)
                    {
                        return;
                    }
                }


                String codestr = this.txt_codestr.Text.Trim();
                //DateTime time = DateTime.Parse(this.datePick.Value.ToString());
                //String date = string.Format("{0:d}", time);
                OracleParameter[] sqlpara = new OracleParameter[1];
                string hasBatchcode = getBatchcode();
                string errcode = "", errmsg = ""; string indexstr = "";
                if (hasBatchcode != "0")
                {
                    if (codestr != "")
                    {
                        DialogResult MsgBoxResult = MessageBox.Show("车组排程顺序为【" + codestr.Substring(1) + "】，是否确定按照该顺序进行预排程？",//对话框的显示内容 
                                                                    "提示",//对话框的标题 
                                                                    MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                                    MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                                    MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
                        if (MsgBoxResult == DialogResult.Yes)
                        {
                            btn_schedule.Enabled = false;
                            btn_sixschedule.Enabled = false;
                            Db.Open();
                            String[] code = codestr.Substring(1).Split(',');
                            int len = code.Length;
                            string splitval = "1000";// this.txt_splitval.Text.Trim();
                            for (int i = 0; i < len; i++)
                            {
                                panel2.Visible = true;
                                label2.Visible = true;
                                progressBar1.Visible = true;
                                int rcounts = ds.Tables[0].Rows.Count;
                                progressBar1.Value = 0;
                                Application.DoEvents();
                                if (i == 0) label2.Text = "正在对" + code[i] + "车组订单数据进行预排程...";
                                //MessageBox.Show(label2.Text);
                                sqlpara = new OracleParameter[4];
                                //sqlpara[0] = new OracleParameter("p_time", date);
                                sqlpara[0] = new OracleParameter("p_code", code[i]);
                                sqlpara[1] = new OracleParameter("p_splitval", splitval);
                                sqlpara[2] = new OracleParameter("p_ErrCode", OracleType.VarChar, 30);
                                sqlpara[3] = new OracleParameter("p_ErrMsg", OracleType.VarChar, 100);

                                sqlpara[2].Direction = ParameterDirection.Output;
                                sqlpara[3].Direction = ParameterDirection.Output;
                                Db.ExecuteNonQueryWithProc("P_PRODUCE_PRE_SCHEDULE", sqlpara);// 修改前的存储过程 P_PRODUCE_SCHEDULE
                                //MessageBox.Show(date);
                                //MessageBox.Show(code[i]+"订单数据接收完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                errcode = sqlpara[2].Value.ToString();
                                errmsg = sqlpara[3].Value.ToString();
                                //进度条显示

                                progressBar1.Value = ((i + 1) * 100 / len);
                                progressBar1.Refresh();
                                String tmpstr = "";
                                if (errcode == "1")
                                {
                                    if (i + 1 < len) tmpstr = "正在对" + code[i + 1] + "车组订单数据进行预排程...";
                                    else tmpstr = "";
                                    label2.Text = code[i] + "车组订单数据预排程结束..." + tmpstr;
                                    label2.Refresh();
                                    indexstr = indexstr + "," + code[i];
                                }
                                else
                                {
                                    label2.Text = errmsg;
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
                                DataTable dt_new = ds.Tables[0];
                                DataRowCollection drc = dt_new.Rows;
                                String[] indexArr = indexstr.Split(',');
                                for (int j = 0; j < indexArr.Length; j++)
                                {
                                    Console.WriteLine(indexArr[indexArr.Length - 1 - j]);

                                    drc.RemoveAt(Convert.ToInt32(indexArr.Length - 1 - j));

                                    //MessageBox.Show(indexArr[indexArr.Length - 1 - j]);
                                }

                                this.orderdata.DataSource = dt_new;
                                this.orderdata.AutoGenerateColumns = false;
                            }
                        }

                        seek();
                    }
                    else
                    {
                        MessageBox.Show("请至少选择一个要预排程的车组!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("请添加一个新的批次,再进行预排程操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                btn_schedule.Enabled = true;
                btn_sixschedule.Enabled = true;
            }
            catch (Exception ex)
            {
                writeLog.Write("预排程异常：" + ex.Message);

            }
            finally
            {
                handleschedule(2,false);
                isScheduleing = false;
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            seek();
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;//防止点击多下
            panel2.Visible = true;
            label2.Visible = true;
            progressBar1.Visible = true;
            progressBar1.Value = 0;
            label2.Text = "正在对分拣车组任务数据进重新排序";
            Thread thread = new Thread(new ThreadStart(Sort));
            thread.Start();


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
          
            updateControl(button1, true, true);
          //  panel2.Visible = false;
            updateControl(panel2, false,true);
          //  label2.Visible = false;
            updateControl(label2, false, true);
          //  progressBar1.Visible = false;
            updateControl(progressBar1, false, true);
        }

        private delegate void HandleDelegate1(Control control,bool isvisible,bool isenable);
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
            for (int i = 0; i < this.orderdata.RowCount; i++)
            {
                orderdata.Rows[i].Cells[0].Value = "true";
                czcodestr = czcodestr + "," + orderdata.Rows[i].Cells[2].Value + "";
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
        /// <summary>
        /// 六三六剔除预排程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_sixschedule_Click(object sender, EventArgs e)
        {
            try
            {
                //查询禁用的包装机和主皮带
                handleschedule(2, true);
                isScheduleing = true;
                string mainbeltno = "";
                string packmachineno = "";
                Db.Open();
                DataTable dt1 = Db.Query("select troughnum from t_produce_sorttrough h where h.troughtype = 30 and state = 0 order by troughnum");
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        mainbeltno = mainbeltno + " " + dt1.Rows[i][0].ToString();
                    }
                }
                DataTable dt2 = Db.Query("select troughnum from t_produce_sorttrough h where h.troughtype = 40 and state = 0 order by troughnum");
                if (dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        packmachineno = packmachineno + " " + dt2.Rows[i][0].ToString();
                    }
                }
                if (dt1.Rows.Count > 0 || dt2.Rows.Count > 0)
                {
                    DialogResult re = MessageBox.Show("已被禁用主皮带：" + mainbeltno + "\r已被禁用包装机：" + packmachineno, "是否继续预排程？", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (re == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                String codestr = this.txt_codestr.Text.Trim(); 
                OracleParameter[] sqlpara = new OracleParameter[1];
                string hasBatchcode = getBatchcode();
                string errcode = "", errmsg = ""; string indexstr = "";
                if (hasBatchcode != "0")
                {
                    if (codestr != "")
                    {
                        DialogResult MsgBoxResult = MessageBox.Show("车组排程顺序为【" + codestr.Substring(1) + "】，是否确定按照该顺序进行预排程？",//对话框的显示内容 
                                                                    "提示",//对话框的标题 
                                                                    MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                                    MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                                    MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
                        if (MsgBoxResult == DialogResult.Yes)
                        {
                            btn_schedule.Enabled = false;
                            btn_sixschedule.Enabled = false;
                            Db.Open();
                            String[] code = codestr.Substring(1).Split(',');
                            int len = code.Length;
                            string splitval = "1000";// this.txt_splitval.Text.Trim();
                            for (int i = 0; i < len; i++)
                            {
                                panel2.Visible = true;
                                label2.Visible = true;
                                progressBar1.Visible = true;
                                int rcounts = ds.Tables[0].Rows.Count;
                                progressBar1.Value = 0;
                                Application.DoEvents();
                                if (i == 0) label2.Text = "正在对" + code[i] + "车组订单数据进行预排程..."; 
                                sqlpara = new OracleParameter[6];
                                sqlpara[0] = new OracleParameter("p_code", code[i]);//p_linenum
                                sqlpara[1] = new OracleParameter("p_splitval", splitval);
                                sqlpara[2] = new OracleParameter("p_flag", 1);//1是异标合一
                                sqlpara[3] = new OracleParameter("p_linenum", "0");//在异标合一里线路跟随常规烟,所以只需要带入0
                                sqlpara[4] = new OracleParameter("p_ErrCode", OracleType.VarChar, 30);
                                sqlpara[5] = new OracleParameter("p_ErrMsg", OracleType.VarChar, 100);

                                sqlpara[4].Direction = ParameterDirection.Output;
                                sqlpara[5].Direction = ParameterDirection.Output;
                                Db.ExecuteNonQueryWithProc("P_UN_DIY_SCHEDULE_SIX", sqlpara);
                                errcode = sqlpara[4].Value.ToString();
                                errmsg = sqlpara[5].Value.ToString();
                                //进度条显示 
                                progressBar1.Value = ((i + 1) * 100 / len);
                                progressBar1.Refresh();
                                String tmpstr = "";
                                if (errcode == "1")
                                {
                                    if (i + 1 < len) tmpstr = "正在对" + code[i + 1] + "车组订单数据进行预排程...";
                                    else tmpstr = "";
                                    label2.Text = code[i] + "车组订单数据预排程结束..." + tmpstr;
                                    label2.Refresh();
                                    indexstr = indexstr + "," + code[i];
                                }
                                else
                                {
                                    label2.Text = errmsg;
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
                                DataTable dt_new = ds.Tables[0];
                                DataRowCollection drc = dt_new.Rows;
                                String[] indexArr = indexstr.Split(',');
                                for (int j = 0; j < indexArr.Length; j++)
                                {
                                    Console.WriteLine(indexArr[indexArr.Length - 1 - j]);

                                    drc.RemoveAt(Convert.ToInt32(indexArr.Length - 1 - j)); 
                                }

                                this.orderdata.DataSource = dt_new;
                                this.orderdata.AutoGenerateColumns = false;
                            }
                        }

                        seek();
                    }
                    else
                    {
                        MessageBox.Show("请至少选择一个要预排程的车组!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("请添加一个新的批次,再进行预排程操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                btn_schedule.Enabled = true; 
                btn_sixschedule.Enabled = true;
            }
            catch (Exception ex)
            {
                writeLog.Write("预排程异常：" + ex.Message);
                MessageBox.Show("预排程异常：" + ex.Message);

            }
            finally
            {
                handleschedule(2, false);
                isScheduleing = false;
                panel2.Visible = false;
                label2.Visible = false;
                progressBar1.Visible = false;
            }
        }

        private void btn_Reschedule_Click(object sender, EventArgs e)
        {
            try
            {
                //查询禁用的包装机和主皮带
                handleschedule(2, true);
                isScheduleing = true;
                string mainbeltno = "";
                string packmachineno = "";
                Db.Open();
                DataTable dt1 = Db.Query("select troughnum from t_produce_sorttrough h where h.troughtype = 30 and state = 0 order by troughnum");
                if (dt1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        mainbeltno = mainbeltno + " " + dt1.Rows[i][0].ToString();
                    }
                }
                DataTable dt2 = Db.Query("select troughnum from t_produce_sorttrough h where h.troughtype = 40 and state = 0 order by troughnum");
                if (dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        packmachineno = packmachineno + " " + dt2.Rows[i][0].ToString();
                    }
                }
                if (dt1.Rows.Count > 0 || dt2.Rows.Count > 0)
                {
                    DialogResult re = MessageBox.Show("已被禁用主皮带：" + mainbeltno + "\r已被禁用包装机：" + packmachineno, "是否继续预排程？", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (re == DialogResult.Cancel)
                    {
                        return;
                    }
                }

                //string codestr = ScheduleService.GetScheduleCodeList();
                DataTable dt = Db.Query("select ROUTECODE from T_PRODUCE_PRELIMINARYSCHEDULE order by SERIAL,MAINBELT");//第一个排程批次，读取预计划排程车组列表
                string codestr = "";
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        codestr = codestr + "," + dt.Rows[i][0].ToString();
                    }
                } 
                if (codestr.Count()<=0)
                {
                    MessageBox.Show("预计划排程数据表内不存在数据！");
                    return;
                }
                OracleParameter[] sqlpara = new OracleParameter[1];
                string hasBatchcode = getBatchcode();
                string errcode = "", errmsg = ""; string indexstr = "";
                if (hasBatchcode != "0")
                {
                    if (codestr != "")
                    {
                        DialogResult MsgBoxResult = MessageBox.Show("车组排程顺序为【" + codestr.Substring(1) + "】，是否确定按照该顺序进行预排程？",//对话框的显示内容 
                                                                    "提示",//对话框的标题 
                                                                    MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                                    MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                                    MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
                        if (MsgBoxResult == DialogResult.Yes)
                        {
                            btn_schedule.Enabled = false;
                            btn_sixschedule.Enabled = false;
                            Db.Open();
                            String[] code = codestr.Substring(1).Split(',');
                            int len = code.Length;
                            string splitval = "1000";// this.txt_splitval.Text.Trim();
                            for (int i = 0; i < len; i++)
                            {
                                panel2.Visible = true;
                                label2.Visible = true;
                                progressBar1.Visible = true;
                                int rcounts = ds.Tables[0].Rows.Count;
                                progressBar1.Value = 0;
                                Application.DoEvents();
                                if (i == 0) label2.Text = "正在对" + code[i] + "车组订单数据进行预排程...";
                                //MessageBox.Show(label2.Text);
                                sqlpara = new OracleParameter[4];
                                //sqlpara[0] = new OracleParameter("p_time", date);
                                sqlpara[0] = new OracleParameter("p_code", code[i]);
                                sqlpara[1] = new OracleParameter("p_splitval", splitval);
                                sqlpara[2] = new OracleParameter("p_ErrCode", OracleType.VarChar, 30);
                                sqlpara[3] = new OracleParameter("p_ErrMsg", OracleType.VarChar, 100);

                                sqlpara[2].Direction = ParameterDirection.Output;
                                sqlpara[3].Direction = ParameterDirection.Output;
                                Db.ExecuteNonQueryWithProc("P_PRODUCE_PRE_SCHEDULE", sqlpara);// 修改前的存储过程 P_PRODUCE_SCHEDULE
                                //MessageBox.Show(date);
                                //MessageBox.Show(code[i]+"订单数据接收完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                errcode = sqlpara[2].Value.ToString();
                                errmsg = sqlpara[3].Value.ToString();
                                //进度条显示

                                progressBar1.Value = ((i + 1) * 100 / len);
                                progressBar1.Refresh();
                                String tmpstr = "";
                                if (errcode == "1")
                                {
                                    if (i + 1 < len) tmpstr = "正在对" + code[i + 1] + "车组订单数据进行预排程...";
                                    else tmpstr = "";
                                    label2.Text = code[i] + "车组订单数据预排程结束..." + tmpstr;
                                    label2.Refresh();
                                    indexstr = indexstr + "," + code[i];
                                }
                                else
                                {
                                    label2.Text = errmsg;
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
                                DataTable dt_new = ds.Tables[0];
                                DataRowCollection drc = dt_new.Rows;
                                String[] indexArr = indexstr.Split(',');
                                for (int j = 0; j < indexArr.Length; j++)
                                {
                                    Console.WriteLine(indexArr[indexArr.Length - 1 - j]);

                                    drc.RemoveAt(Convert.ToInt32(indexArr.Length - 1 - j));

                                    //MessageBox.Show(indexArr[indexArr.Length - 1 - j]);
                                }

                                this.orderdata.DataSource = dt_new;
                                this.orderdata.AutoGenerateColumns = false;
                            }
                        }

                        seek();
                    }
                    else
                    {
                        MessageBox.Show("请至少选择一个要预排程的车组!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("请添加一个新的批次,再进行预排程操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                btn_schedule.Enabled = true;
                btn_sixschedule.Enabled = true;
            }
            catch (Exception ex)
            {
                writeLog.Write("预排程异常：" + ex.Message);

            }
            finally
            {
                handleschedule(2, false);
                isScheduleing = false;
            }
        }
 


    }
}
