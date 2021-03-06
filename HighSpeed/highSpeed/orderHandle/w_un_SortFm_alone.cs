﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.OracleClient;
using System.Threading;
using highSpeed.PubFunc;
using InBound;
using InBound.Business;



namespace highSpeed.orderHandle
{
    public partial class w_un_SortFm_alone : Form
    {

        DataSet ds = new DataSet();
        w_main wm = new w_main();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public WriteLog writeLog = WriteLog.GetLog();
        static string sqlhwere = " where state = 0 ";
        static string sql = " select  REGIONCODE 车组号,Sum(TASKQUANTITY)  任务数 ,STATE 状态,LINENUM 分拣线,ORDERDATE 订单日期 from t_un_task  " + sqlhwere + " group by REGIONCODE,STATE,LINENUM,ORDERDATE  order by REGIONCODE";
        public delegate void HandleIsSorting(int flage, bool isOrnot);
        HandleIsSorting handlesort;
        int times = 1;//排程时间记录

        bool isSort = false;//正在排程标识符
        public w_un_SortFm_alone()
        {
            InitializeComponent();
            //this.pager1.PageChanged += new WHC.Pager.WinControl.PageChangedEventHandler(pager1_PageChanged);
            //this.pager1.ExportCurrent += new WHC.Pager.WinControl.ExportCurrentEventHandler(pager1_ExportCurrent);
            //this.pager1.ExportAll += new WHC.Pager.WinControl.ExportAllEventHandler(pager1_ExportAll);
            handlesort += wm.GetSonFormState;
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            
            dgvSortInfo.AllowUserToAddRows = false;
            //panel3.VisibleChanged += new EventHandler(panel3_VisibleChanged);
            //this.pager1.GetChildAtPoint(7).Visible = false;
            seek();
            // pager1.Width = dgvSortInfo.Width;
            writeLog.Write("排程启动");
            dgvSortInfo.AllowUserToAddRows = false;
        }

        void panel3_VisibleChanged(object sender, EventArgs e)
        {

        }
        void pager1_PageChanged(object sender, EventArgs e)
        {

        }

        void pager1_ExportCurrent(object sender, EventArgs e)
        {
        }

        void pager1_ExportAll(object sender, EventArgs e)
        {
        }

        void seek()
        {
            try
            {

                //int total = int.Parse(DataPublic.ExecuteScalar("select Count(*) from t_produce_task").ToString());
                dgvSortInfo.Visible = true;
                DgvBind(sql);
            }
            catch (NullReferenceException nullre)
            {
                writeLog.Write("seek()空引用:" + nullre.Message);
            }
            catch (IndexOutOfRangeException iore)
            {
                writeLog.Write("seek()报错索引越界:" + iore.Message);
            }
            catch (Exception e)
            {
                writeLog.Write("seek()报错:" + e.Message);
            }
        }
        private void w_SortFm_Load(object sender, EventArgs e)
        {
            int pokecount = UnionTaskInfoService.GetPokeCount();
            int pokeseqcount = UnionTaskInfoService.GetPokeSEQCount();
            if (pokecount == pokeseqcount)
            {
                btnPokeSeq.Enabled = false;
            }
            else
            {
                btnPokeSeq.Enabled = true;
            }
            lblInFO.Text = "分拣车组信息:";
        }
        void DgvBind(string sql)
        {
            Db.Open();
            ds.Clear();
            ds = Db.QueryDs(sql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                panel2.Visible = true;
                label2.Visible = true;
                progressBar1.Visible = false;
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
                lblTime.Visible = false;//时间

                dgvSortInfo.DataSource = null;//处理IndexOutOfRangeException异常
                try
                {
                    this.dgvSortInfo.AllowUserToAddRows = false;
                    this.dgvSortInfo.DataSource = ds.Tables[0];
                }
                catch (Exception e)
                {
                    writeLog.Write("Dgv异常：----"+e.Message);
                }
                //dgvSortInfo.Sort(dgvSortInfo.Columns[0], ListSortDirection.Ascending);//默认车组排序
                //this.dgvSortInfo.AutoGenerateColumns = false;


                string columnwidths = pub.IniReadValue(this.Name, this.dgvSortInfo.Name);
                if (columnwidths != "")
                {
                    string[] columns = columnwidths.Split(',');
                    int j = 0;
                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (dgvSortInfo.Columns[i].Visible == true)
                        {
                            dgvSortInfo.Columns[j].Width = Convert.ToInt32(columns[i]);
                            j = j + 1;
                        }
                    }
                }
                dgvSortInfo.ClearSelection();
                Db.Close();
            }
        }
        delegate void HandleSort();

        private void btnSort_Click(object sender, EventArgs e)
        {
            
            this.btnSort.Enabled = false;//防止点击多下  
            isSort = true;
            handlesort(3, true);
            progressBar1.Maximum = 100;
            label2.Text = "正在读取数据...";
            progressBar1.Value = 0;
           
            label2.Visible = true;
            panel2.Visible = true;
            lblTime.Visible = true;
            progressBar1.Visible = true;
            times = 1;//时间重置 
            TimerByTime.Start();// = true;//启动时间记录
            //Thread thread = new Thread(new ThreadStart(Sort));//旧的
            //thread.Start();

            HandleSort task = Sort; //新的
            task.BeginInvoke(null, null);
            label2.Text = "正在对分拣车组任务数据进重新排序";

        }

        void Sort()
        {
            try
            {

                progressBar1.Value = (progressBar1.Maximum / 2);
                OracleParameter[] sqlpara;
                sqlpara = new OracleParameter[2];
                sqlpara[0] = new OracleParameter("p_ErrCode", OracleType.VarChar, 30);
                sqlpara[1] = new OracleParameter("p_ErrMsg", OracleType.VarChar, 100);
                sqlpara[0].Direction = ParameterDirection.Output;
                sqlpara[1].Direction = ParameterDirection.Output;
          
                Db.Open();
                Db.ExecuteNonQueryWithProc("P_UN_SCHEDULE_ALONE", sqlpara);//修改前的存储过程 P_PRODUCE_updatesortnum 
                Db.Close();
                //MessageBox.Show(date);
                //MessageBox.Show(code[i]+"订单数据接收完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                String errcode = sqlpara[0].Value.ToString();
                String errmsg = sqlpara[1].Value.ToString();
                if (errcode == "1")
                {
                    btnPokeSeq.Enabled = true;
                    progressBar1.Value = progressBar1.Maximum;
                    TimerByTime.Stop();// 计时结束;
                    btnSort.Enabled = true;
                    lblInFO.Text = "分拣车组任务排序成功！" + "\r\n" + "所用时间:" + times + "秒";
                    MessageBox.Show("分拣车组任务排序成功！" + "\r\n" + "所用时间:" + times + "秒", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    writeLog.Write("分拣车组任务排序成功！" + "\r\n" + "所用时间:" + times + "秒");
                    updateControl(btnSort, true, true);
                    // 
                    //  DgvBind(sql);//排程成功后刷新 


                }
                else
                {
                    panel2.Visible = false;
                    TimerByTime.Stop();// 计时结束;
                    btnSort.Enabled = true;
                    MessageBox.Show(errmsg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    updateControl(btnSort, true, true);
                }
                updateControl(btnSort, true, true);
                //  panel2.Visible = false;
                updateControl(panel2, false, true);
                //  label2.Visible = false;
                updateControl(label2, false, true);
                //  progressBar1.Visible = false;
                updateControl(progressBar1, false, true);

                updateControl(lblTime, false, true);

            }
            catch (DataException dataex)
            {
                writeLog.Write(this.Text+"排程异常:" + dataex.Message);
            }
            catch (NullReferenceException nullex)
            {
                MessageBox.Show("排程OK:" + nullex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                writeLog.Write(this.Text + "排程异常:" + nullex.Message);
            }
            catch (IndexOutOfRangeException iore)
            {
                MessageBox.Show("排程OK:" + iore.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                writeLog.Write(this.Text + "排程异常索引越界:" + iore.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show("排程OK:" + e.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                writeLog.Write(this.Text + "排程异常:" + e.Message);
            }
            finally
            { 
                times = 1;
                seek();
                panel2.Visible = false;
                TimerByTime.Stop();// 计时结束;
                btnSort.Enabled = true;
                handlesort(3, false);//告诉父窗体任务结束
                isSort = false;
            }

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

        private void dgvSortInfo_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {

                if (dgvSortInfo.DataSource != null)
                {
                    if (e.ColumnIndex == 2)
                    {
                        String statusText = "";
                        switch (e.Value.ToString())
                        {
                            case "0":
                                statusText = "新增";
                                break;

                        }
                        e.Value = statusText;
                    }
                }
            }
            catch (NullReferenceException nullref)
            {
                writeLog.Write(this.Text + "排程异常NullReferenceException:" + nullref.Message);
            }
            catch (Exception ex)
            {

                writeLog.Write(this.Text + "排程异常Exception:" + ex.Message);
            }
        }

        private void btnRef_Click(object sender, EventArgs e)
        {
            seek();

        }

        private void dgvSortInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 0)
            //{
            //    Dgvcheck.Selected = true;

            //}
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            lblTime.Text = "已用时间:" + times++.ToString() + "秒";





        }

        private void w_SortFm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isSort)
            {
                e.Cancel = true;
                MessageBox.Show("正在后台执行任务！请等待任务结束....");
                return;
            }
        }

        delegate void HandleSortPokeseq();
        private void btnPokeSeq_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            HandleSort task1 = datacheck; //新的
            task1.BeginInvoke(null, null);
        } 

        private void datacheck()
        {
            Db.Open();
            OracleParameter[] sqlpara;
            sqlpara = new OracleParameter[2];

            sqlpara[0] = new OracleParameter("p_ErrCode", OracleType.VarChar, 1000);
            sqlpara[1] = new OracleParameter("p_ErrMsg", OracleType.VarChar, 2000);

            sqlpara[0].Direction = ParameterDirection.Output;
            sqlpara[1].Direction = ParameterDirection.Output;

            Db.ExecuteNonQueryWithProc("P_UN_SCHEDULEVALIDATION_ALONE", sqlpara);

            string errcode = sqlpara[0].Value == null ? "" : sqlpara[0].Value.ToString();
            string errmsg = sqlpara[1].Value == null ? "" : sqlpara[1].Value.ToString();
            Db.Close();
            panel4.Visible = false;
            if (errcode == "1")
            {
                MessageBox.Show("异型烟排程数据正常,点击确认后将开放数据给下游获取");
                ScheduleService.InsertSynseqInfo_UnNormalAlone();
            }
            else
            {
                MessageBox.Show(errmsg);
            } 
        }






        private void dgvSortInfo_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            dgvSortInfo.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int list = UnionTaskInfoService.GetPokeCount();

            MessageBox.Show(list.ToString());
        }

        private void dgvSortInfo_DataError_1(object sender, DataGridViewDataErrorEventArgs e)
        {
            writeLog.Write(this.Text + "Dgv异常" + e.ToString());
            if (e.Exception.InnerException is System.IndexOutOfRangeException)
            {
               // MessageBox.Show("出现异常，请查看后台日志，该异常不影响排程！");
                writeLog.Write(this.Text + "Dgv:System.IndexOutOfRangeException异常");
            }
        }

        private void btnVli_Click(object sender, EventArgs e)
        {
            string Str = InBound.Business.UnPokeService.GetNullLWSomke();
            MessageBox.Show(Str); 
        }

        private void btnTransfor_Click(object sender, EventArgs e)
        {
            w_order_Split wos = new w_order_Split();
            wos.ShowDialog();
        }

     


    }
}

