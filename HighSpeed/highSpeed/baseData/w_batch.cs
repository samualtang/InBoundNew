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

namespace highSpeed.baseData
{
    public partial class win_batch : Form
    {
        public win_batch_new batch_new = null;
        DataSet ds = new DataSet();
        //DataSet detail_ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public win_batch()
        {
            InitializeComponent(); 
            seek();
        }

        private void seek()
        {
            String strsql = "SELECT rownum,tmp.* FROM"+
                            "(SELECT batchcode,starttime,endtime,decode(batchtype,'10','正常烟','20','异型烟')as batchtype,decode(state,'0','关闭','10','正常')AS status FROM t_produce_batch  ORDER BY batchcode DESC)tmp where rownum<=100";

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

                this.batchdata.DataSource = ds.Tables[0];
                this.batchdata.AutoGenerateColumns = false;

                string columnwidths = pub.IniReadValue(this.Name, this.batchdata.Name);
                if (columnwidths != "")
                {
                    string[] columns = columnwidths.Split(',');
                    int j = 0;
                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (batchdata.Columns[i].Visible == true)
                        {
                            batchdata.Columns[j].Width = Convert.ToInt32(columns[i]);
                            j = j + 1;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void btn_new_Click(object sender, EventArgs e)
        {
            string strunnormal=isCompleted();
            String strnormal = isNCompleted();
            Boolean ispass = false;
            int para = 0;
            if (cbnormal.Checked && cbunnormal.Checked)
            {
                if (strnormal == "0" && strunnormal == "0")
                {
                    ispass = true;
                    para = 30;
                }
                else
                {
                    ispass = false;
                }
            }

            else if (cbnormal.Checked && !cbunnormal.Checked)
            {
                if (strnormal == "0")
                {
                    para = 10;
                    ispass = true;
                }
                else
                {
                    ispass = false;
                }

            }
            else if (cbunnormal.Checked)
            {
                if (strunnormal == "0")
                {
                    para = 20;
                    ispass = true;
                }
                else
                {
                    ispass = false;
                }

            }
            else
            {
                MessageBox.Show("请至少选择一种类型进行批次创建");
                return;
            }
           
                if (ispass)
            {
               // if (batch_new == null || batch_new.IsDisposed)
               // {
                    batch_new = new win_batch_new(para);
                    //batch_new.ba
                    batch_new.WindowState = FormWindowState.Normal;
                    batch_new.ShowDialog();
                    seek();
               // }
            }
            else {
                MessageBox.Show("当前还有未完成的分拣任务,不能创建新的批次!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private string isNCompleted()
        {
            string str = "";
            string sql = "SELECT count(*)  FROM t_produce_task WHERE state<>'30'";
            DataTable dt = Db.Query(sql);
            str = dt.Rows[0][0].ToString();
            return str;
        }
        private string isCompleted()
        {
            string str = "";
            string sql = "SELECT count(*)  FROM t_un_task WHERE state<>'30'";
            DataTable dt = Db.Query(sql);
            str = dt.Rows[0][0].ToString();
            return str;
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            String batchcode = this.batchdata.SelectedRows[0].Cells[1].Value + "";
            String status = this.batchdata.SelectedRows[0].Cells[5].Value + "";

            string type = this.batchdata.SelectedRows[0].Cells[4].Value +"";
            if (type == "异型烟")
            {
                type = "20";
            }
            else
            { type = "10"; }
            if (status == "正常")
            {
                DialogResult MsgBoxResult = MessageBox.Show("是否确定关闭编号为【" + batchcode + "】的批次？",//对话框的显示内容 
                                                            "提示",//对话框的标题 
                                                            MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                            MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                            MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
                if (MsgBoxResult == DialogResult.Yes)
                {
                    String time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    String updatesql = "update t_produce_batch set state='0',endtime=to_date('" + time + "','yyyy-mm-dd HH24:MI:SS') where batchcode='" + batchcode + "' and batchtype="+type;

                    try
                    {
                        Db.Open();
                        Db.ExecuteNonQuery(updatesql);
                        MessageBox.Show("批次关闭成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        seek();

                    }
                    catch (SqlException se)
                    {
                        MessageBox.Show(se.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Db.Close();

                        //this.Close();
                    }

                }
            }
            else 
            {
                MessageBox.Show("您选择的批次已经处于关闭状态!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
