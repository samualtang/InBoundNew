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
    public partial class win_un_batch : Form
    {
        public win_un_batch_new batch_new = null;
        DataSet ds = new DataSet();
        //DataSet detail_ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public win_un_batch()
        {
            InitializeComponent(); 
            this.pager1.PageChanged += new WHC.Pager.WinControl.PageChangedEventHandler(pager1_PageChanged);
            this.pager1.ExportCurrent += new WHC.Pager.WinControl.ExportCurrentEventHandler(pager1_ExportCurrent);
            this.pager1.ExportAll += new WHC.Pager.WinControl.ExportAllEventHandler(pager1_ExportAll);
            //this.pager1.GetChildAtPoint(7).Visible = false;
            pager1.PageSize = 25;
            pager1.CurrentPageIndex = 1;
            
            seek();
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

        private void seek()
        {
            String strsql = "SELECT tmp.* FROM" +
                            "(SELECT rownum as num,batchcode,starttime,endtime,decode(state,'0','关闭','1','正常')AS status FROM T_UN_BATCH  ORDER BY batchcode DESC)tmp where tmp.num>" + (pager1.CurrentPageIndex - 1) * pager1.PageSize + " and tmp.num<=" + pager1.CurrentPageIndex * pager1.PageSize + "order by tmp.batchcode desc";

            int total = int.Parse(DataPublic.ExecuteScalar("SELECT count(*) FROM T_UN_BATCH").ToString());
            Bind(strsql);
            pager1.RecordCount = total;
            this.pager1.InitPageInfo();
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
            string str=isCompleted();
            if (str == "0")
            {
                if (batch_new == null || batch_new.IsDisposed)
                {
                    batch_new = new win_un_batch_new();
                    //batch_new.ba
                    batch_new.WindowState = FormWindowState.Normal;
                    batch_new.ShowDialog();
                    seek();
                }
            }
            else {
                MessageBox.Show("当前还有未完成的分拣任务,不能创建新的批次!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private string isCompleted()
        {
            string str = "";
            string sql = "SELECT count(*)  FROM highspeed.t_un_task WHERE state<>'30'";
            DataTable dt = Db.Query(sql);
            str = dt.Rows[0][0].ToString();
            return str;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            String batchcode = this.batchdata.SelectedRows[0].Cells[1].Value + "";
            String status = this.batchdata.SelectedRows[0].Cells[4].Value + "";
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

                    String updatesql = "update highspeed.T_UN_BATCH set state='0',endtime=to_date('" + time + "','yyyy-mm-dd HH24:MI:SS') where batchcode='" + batchcode + "'";

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
