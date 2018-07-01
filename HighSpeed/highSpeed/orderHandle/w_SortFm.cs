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

namespace highSpeed.orderHandle
{
    public partial class w_SortFm : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public WriteLog writeLog = WriteLog.GetLog();
        static  string sqlhwere = "where state = " + 0;
        static  string sql = "select  REGIONCODE 车组号,Sum(TASKQUANTITY)  任务数 ,STATE 状态,MAINBELT 主皮带,ORDERDATE 订单日期 from t_produce_task " + sqlhwere + " group by REGIONCODE,STATE,MAINBELT,ORDERDATE  order by REGIONCODE";

        int times = 1;//排程时间记录
        public w_SortFm()
        {
            InitializeComponent();
            this.pager1.PageChanged += new WHC.Pager.WinControl.PageChangedEventHandler(pager1_PageChanged);
            this.pager1.ExportCurrent += new WHC.Pager.WinControl.ExportCurrentEventHandler(pager1_ExportCurrent);
            this.pager1.ExportAll += new WHC.Pager.WinControl.ExportAllEventHandler(pager1_ExportAll);
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false; 
            rdbUnionDan.Checked = true;
            dgvSortInfo.AllowUserToAddRows = false;
            //this.pager1.GetChildAtPoint(7).Visible = false;
            seek();
            pager1.Width = dgvSortInfo.Width;
            writeLog.Write("排程启动");
        }
        void pager1_PageChanged(object sender, EventArgs e)
        {
            seek();
        }

        void pager1_ExportCurrent(object sender, EventArgs e)
        {
        }

        void pager1_ExportAll(object sender, EventArgs e)
        {
        }

        void seek()
        { 
            int total = int.Parse(DataPublic.ExecuteScalar("select Count(*) from t_produce_task").ToString());
            //Console.WriteLine(strsql);
            //  Db.Open();
            DgvBind(sql);
            // Db.Close();
            pager1.RecordCount = total;
            this.pager1.InitPageInfo();
        }
         private void w_SortFm_Load(object sender, EventArgs e)
        {
            lblInFO.Text = "任务列表:";
        }
         void DgvBind(string sql)
         {
             Db.Open();
             ds.Clear(); 
             ds = Db.QueryDs(sql); 
             panel2.Visible = true;
             label2.Visible = true;
             progressBar1.Visible = false;
             int rcounts = ds.Tables[0].Rows.Count;
             progressBar1.Value = 0;
             //for (int i = 0; i < rcounts; i++)
             //{
             //    Application.DoEvents();
             //    progressBar1.Value = ((i + 1) * 100 / rcounts);
             //    progressBar1.Refresh();
             //    label2.Text = "正在读取数据..." + ((i + 1) * 100 / rcounts).ToString() + "%";
             //    label2.Refresh();
             //}
             if (ds.Tables.Count > 0)
             {
                 panel2.Visible = false;
                 label2.Visible = false;
                 progressBar1.Visible = false;
                 lblTime.Visible = false;//时间

                 dgvSortInfo.DataSource = null;//处理IndexOutOfRangeException异常
                 this.dgvSortInfo.DataSource = ds.Tables[0];
                 dgvSortInfo.Sort(dgvSortInfo.Columns[0], ListSortDirection.Ascending);//默认车组排序
                 //this.dgvSortInfo.AutoGenerateColumns = false;


                 //string columnwidths = pub.IniReadValue(this.Name, this.dgvSortInfo.Name);
                 //if (columnwidths != "")
                 //{
                 //    string[] columns = columnwidths.Split(',');
                 //    int j = 0;
                 //    for (int i = 0; i < columns.Length; i++)
                 //    {
                 //        if (dgvSortInfo.Columns[i].Visible == true)
                 //        {
                 //            dgvSortInfo.Columns[j].Width = Convert.ToInt32(columns[i]);
                 //            j = j + 1;
                 //        }
                 //    }
                 //}
                 //dgvSortInfo.ClearSelection(); 
             }
             Db.Close();

         }
        private void btnSort_Click(object sender, EventArgs e)
        { 
            this.btnSort.Enabled = false;//防止点击多下  
            label2.Text = "正在读取数据...";
            progressBar1.Value = times;
            progressBar1.Value = 0; 
            label2.Visible = true;
            panel2.Visible = true;
            lblTime.Visible = true;
            progressBar1.Visible = true;
            times = 1;//时间重置
            TimerByTime.Start();// = true;//启动时间记录
            Thread thread = new Thread(new ThreadStart(Sort));
            thread.Start();
            label2.Text = "正在对分拣车组任务数据进重新排序";  
        }

        void Sort()
        {
            try
            {
                OracleParameter[] sqlpara;
                sqlpara = new OracleParameter[3];
                sqlpara[0] = new OracleParameter("p_ErrCode", OracleType.VarChar, 30);
                sqlpara[1] = new OracleParameter("p_ErrMsg", OracleType.VarChar, 100);
                sqlpara[2] = new OracleParameter("p_UnionStates", OracleType.VarChar, 100);//合单标志位
                sqlpara[0].Direction = ParameterDirection.Output;
                sqlpara[1].Direction = ParameterDirection.Output;
                sqlpara[2].Direction = ParameterDirection.Input;
                if (rdbUnionDan.Checked)//合单
                {
                    sqlpara[2].Value = 1;
                }
                else if (rdbUnUnionDan.Checked)//不合单
                {
                    sqlpara[2].Value = 2;
                }
                else
                {
                    MessageBox.Show("请选择合单或者不合单");
                }
                Db.Open();
                Db.ExecuteNonQueryWithProc("P_PRODUCE_SCHEDULE", sqlpara);//修改前的存储过程 P_PRODUCE_updatesortnum 
                Db.Close();
                //MessageBox.Show(date);
                //MessageBox.Show(code[i]+"订单数据接收完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                String errcode = sqlpara[0].Value.ToString();
                String errmsg = sqlpara[1].Value.ToString();
                if (errcode == "1")
                {
                    TimerByTime.Stop();// 计时结束;
                    MessageBox.Show("分拣车组任务排序成功！" + "\r\n" + "所用时间:" + times);
                    writeLog.Write("分拣车组任务排序成功！" + "\r\n" + "所用时间:" + times);
                    updateControl(btnSort, true, true);
                    //  btnRef_Click(null, null);//排程成功后刷新
                    DgvBind(sql);//排程成功后刷新
                }
                else
                {
                    MessageBox.Show(errmsg);
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
            catch (DataException da)
            {
                MessageBox.Show("异常信息:" + da.Message);
                writeLog.Write("异常信息:" + da.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show("异常信息:" + e.Message);
                writeLog.Write("异常信息:" + e.Message);
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
            lblTime.Text ="已用时间:"+ times++.ToString();
        }


    }
}
