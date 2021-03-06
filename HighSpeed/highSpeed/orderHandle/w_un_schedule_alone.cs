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
using InBound;

namespace highSpeed.orderHandle
{
    public partial class w_un_schedule_alone : Form
    {
        public WriteLog writeLog = WriteLog.GetLog();
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public w_un_schedule_alone()
        {
            InitializeComponent();
            orderdata.AllowUserToAddRows = false;
            seek();
            cmb_Linelist();
        }

        private void seek()
        {
            this.txt_codestr.Text = "";
            String str = "select regioncode,sum(e.quantity) as qty,count(distinct e.billcode)as cuscount from t_produce_orderline e join  t_produce_order r on r.billcode=e.billcode where r.unstate='新增' and e.cigarettecode in (select h.cigarettecode from t_produce_sorttrough h where h.cigarettetype in (30,40) and h.troughtype=10 and h.state=10)group by r.regioncode";
            Bind(str);
            this.txt_codestr.Text = "";
        }
        private void cmb_Linelist()
        {
            try
            {
                Entities et = new Entities();
                var linelist = et.T_PRODUCE_SORTTROUGH.Where(x => x.CIGARETTETYPE == 30 && x.SEQ == 2).GroupBy(x => x.GROUPNO).Select(x => x.Key).ToList();
                foreach (var item in linelist)
                {
                    cmb_Line.Items.Add(item);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("分拣线读取失败！");
            }
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

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

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

                    this.orderdata.AllowUserToAddRows = false;
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
                    Db.Close();
                }
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
                int count=this.orderdata.Rows.Count;
                for (int i = 0; i < count;i++ )
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

                    String czcode = this.orderdata.CurrentRow.Cells[1].Value + "";
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
            int sortline;
            if (cmb_Line.SelectedIndex < 0)
            {
                MessageBox.Show("请选择排程线路！");
                return;
            }
            else
            {
                sortline = Convert.ToInt32(cmb_Line.SelectedItem.ToString());       
            }
            String codestr = this.txt_codestr.Text.Trim();
            //DateTime time = DateTime.Parse(this.datePick.Value.ToString());
            //String date = string.Format("{0:d}", time);
            OracleParameter[] sqlpara = new OracleParameter[1] ;
            string hasBatchcode = getBatchcode();
            string errcode = "", errmsg = "";string indexstr = "";
            if (hasBatchcode != "0")
            {
                if (codestr != "")
                {
                    DialogResult MsgBoxResult = MessageBox.Show("车组排程顺序为【" + codestr.Substring(1) + "】，是否确定按照该顺序进行排程？",//对话框的显示内容 
                                                                "提示",//对话框的标题 
                                                                MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                                MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                                MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
                    if (MsgBoxResult == DialogResult.Yes)
                    {
                        Db.Open();
                        String[] code = codestr.Substring(1).Split(',');
                        int len = code.Length;
                        string splitval = this.txt_splitval.Text.Trim();
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
                            sqlpara[1] = new OracleParameter("p_sortline", sortline);
                            sqlpara[2] = new OracleParameter("p_ErrCode", OracleType.VarChar,30);
                            sqlpara[3] = new OracleParameter("p_ErrMsg", OracleType.VarChar,100);

                            sqlpara[2].Direction = ParameterDirection.Output;
                            sqlpara[3].Direction = ParameterDirection.Output;
                            Db.ExecuteNonQueryWithProc("P_UN_PRE_SCHEDULE_ALONE", sqlpara);
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
                                if (i + 1 < len) tmpstr = "正在对" + code[i + 1] + "车组订单数据进行排程...";
                                else tmpstr = "";
                                label2.Text = code[i] + "车组订单数据排程结束..." + tmpstr;
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
                    MessageBox.Show("请至少选择一个要排程的车组!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else {
                MessageBox.Show("请添加一个新的批次,再进行排程操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            seek();
        }

        private void txt_splitval_Leave(object sender, EventArgs e)
        {
            string str = this.txt_splitval.Text.Trim();
            if (str == "" || str == "0")
            {
                this.txt_splitval.Text = "1000";
            }
        }

        private string getBatchcode() 
        {
            string str = "";
            string sql = "SELECT count(*)  FROM t_produce_batch WHERE state=10 and batchtype=20";
            DataTable dt = Db.Query(sql);
            str = dt.Rows[0][0].ToString();
            return str;
        }

        private void btn_all_Click(object sender, EventArgs e)
        {
            String czcodestr = "";
            for (int i = 0; i < this.orderdata.RowCount; i++)
            {
                orderdata.Rows[i].Cells[0].Value = "true";
                czcodestr = czcodestr + "," + orderdata.Rows[i].Cells[1].Value + "";
            }
            this.txt_codestr.Text = czcodestr;
        }

        private void orderdata_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            writeLog.Write(this.Text + "Dgv异常" + e.ToString());
            if (e.Exception.InnerException is System.IndexOutOfRangeException)
            {
                // MessageBox.Show("出现异常，请查看后台日志，该异常不影响排程！");
                writeLog.Write(this.Text + "Dgv:System.IndexOutOfRangeException异常");
            }
        }

        private void btn_sixschedule_Click(object sender, EventArgs e)
        {
            try
            {
                //查询禁用的包装机和主皮带
                int sortline;
                if (cmb_Line.SelectedIndex < 0)
                {
                    MessageBox.Show("请选择排程线路！");
                    return;
                }
                else
                {
                    sortline = Convert.ToInt32(cmb_Line.SelectedItem.ToString());
                }
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
                                sqlpara[2] = new OracleParameter("p_flag", 2);//2是异标分离
                                sqlpara[3] = new OracleParameter("p_linenum", sortline);//2是异标分离,由于和常规烟分离,需要线路来指定包装机
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

            }
            finally
            {
              
            }
        }

    }
}
