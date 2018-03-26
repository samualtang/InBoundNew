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
using System.Data.OracleClient;

namespace highSpeed.baseData
{
    public partial class win_batch_new : Form
    {
        //SqlConnection cn = new SqlConnection(PublicFun.connect);
        //SqlCommand cmd = new SqlCommand();
        //PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase db = new DataBase();
        int para = 0;
        public win_batch_new(int para)
        {
            this.para = para;
            InitializeComponent();
            init();
        }
        //初始化界面文本框
        public void init() {
            String batchcode = "";
            String yearmonth = DateTime.Now.ToString("yyyyMMdd");
            String batchcodesql = "select decode(count(*),null,'0','','0',count(*)) from t_produce_batch where batchcode like'%" + yearmonth + "%'";

            DataTable dt = db.Query(batchcodesql);
            String count = dt.Rows[0][0].ToString();
            int seq = System.Int32.Parse(count) + 1;
            if (seq < 10) batchcode = yearmonth + "0" + seq;
            else batchcode = yearmonth + seq;
            this.txt_batchcode.Text = batchcode;

            String time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.starttime.Text = time;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void closeBatch(int bz) {
            db.Open();
            String cond="";
            if(bz!=30)cond=" and batchtype="+bz;
            String sql = "update t_produce_batch set state=0,endtime=sysdate where state=10"+cond;
            db.ExecuteNonQuery(sql);
            db.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            String txt_batchcode = this.txt_batchcode.Text.Trim();
            String txt_starttime = this.starttime.Text;
           
            if (txt_batchcode == null || txt_batchcode == "")
            {
                MessageBox.Show("请填写批次编号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else 
            {
                closeBatch(para);
                String insertsql = "insert into t_produce_batch(batchcode,starttime,state,batchtype)" ;
                                  

                if (para == 10)
                {
                    insertsql+= "select '" + txt_batchcode + "',to_date('" + txt_starttime + "','yyyy-mm-dd HH24:MI:SS'),'10','10' from dual";
                }
                else if (para == 20)
                {
                    insertsql += "select '" + txt_batchcode + "',to_date('" + txt_starttime + "','yyyy-mm-dd HH24:MI:SS'),'10','20' from dual";
                }
                else if (para == 30)
                {
                    insertsql += "select '" + txt_batchcode + "',to_date('" + txt_starttime + "','yyyy-mm-dd HH24:MI:SS'),'10','10' from dual";
                    insertsql += " union select '" + txt_batchcode + "',to_date('" + txt_starttime + "','yyyy-mm-dd HH24:MI:SS'),'10','20' from dual";
                }
                //cn.Open();
                //cmd = new SqlCommand(insertsql, cn);
                try
                {
                    db.Open();
                    db.ExecuteNonQuery(insertsql);
                    MessageBox.Show("批次创建成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //win_batch.

                    this.panel1.Visible = true;
                    this.lab_showinfo.Text = "正在处理历史订单和任务,请稍候......";

                    if (para == 10 || para == 30)
                    {

                        OracleParameter[] sqlpara = new OracleParameter[3];
                        //sqlpara[0] = new OracleParameter("p_time", date);
                        sqlpara[0] = new OracleParameter("bz", "0");
                        sqlpara[1] = new OracleParameter("p_ErrCode", OracleType.VarChar, 30);
                        sqlpara[2] = new OracleParameter("p_ErrMsg", OracleType.VarChar, 1000);

                        sqlpara[1].Direction = ParameterDirection.Output;
                        sqlpara[2].Direction = ParameterDirection.Output;

                        db.ExecuteNonQueryWithProc("P_PRODUCE_REMOVE", sqlpara);
                        if (sqlpara[1].Value.ToString() == "0")
                        {
                            MessageBox.Show(sqlpara[2].Value.ToString());
                            return;
                        }

                    }
                    if (para == 20 || para == 30)
                    {
                        OracleParameter[] sqlpara = new OracleParameter[3];
                        sqlpara[0] = new OracleParameter("bz", "0");
                        sqlpara[1] = new OracleParameter("p_ErrCode", OracleType.VarChar, 30);
                        sqlpara[2] = new OracleParameter("p_ErrMsg", OracleType.VarChar, 1000);

                        sqlpara[1].Direction = ParameterDirection.Output;
                        sqlpara[2].Direction = ParameterDirection.Output;
                        db.ExecuteNonQueryWithProc("P_UN_REMOVE", sqlpara);
                        if (sqlpara[1].Value.ToString() == "0")
                        {
                            MessageBox.Show(sqlpara[2].Value.ToString());
                            return;
                        }
                    }
                    this.lab_showinfo.Text = "处理完成";
                    MessageBox.Show("历史数据处理完成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.panel1.Visible = false;

                }
                catch (SqlException se)
                {
                    MessageBox.Show(se.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    db.Close();

                    this.Close();
                }
            }

        }

    }
}
