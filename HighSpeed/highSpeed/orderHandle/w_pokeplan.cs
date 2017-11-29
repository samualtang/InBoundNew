using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using highSpeed.PubFunc;
using System.Data.OracleClient;

namespace highSpeed.orderHandle
{
    public partial class win_pokeplan : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public win_pokeplan()
        {
            InitializeComponent();
            seek();
        }

        private void seek()
        {
            //string time = this.orderdate.Text;
            //time=DateTime.Parse(time,"yyyy-MM-dd");

            String strsql = "SELECT tasknum,billcode,batchcode,customercode,customername,taskquantity,regioncode from highspeed.t_produce_task t WHERE state='新增'order BY tasknum,sortseq";
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

                this.taskdata.DataSource = ds.Tables[0];
                this.taskdata.AutoGenerateColumns = false;

                string columnwidths = pub.IniReadValue(this.Name, this.taskdata.Name);
                if (columnwidths != "")
                {
                    string[] columns = columnwidths.Split(',');
                    int j = 0;
                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (taskdata.Columns[i].Visible == true)
                        {
                            taskdata.Columns[j].Width = Convert.ToInt32(columns[i]);
                            j = j + 1;
                        }
                    }
                }
                taskdata.ClearSelection();
                Db.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void btn_poke_Click(object sender, EventArgs e)
        {
            String strsql = "SELECT tasknum from highspeed.t_produce_task t WHERE state='新增' order by tasknum";
            ds.Clear();
            ds = Db.QueryDs(strsql);
            int rcounts = ds.Tables[0].Rows.Count;
            OracleParameter[] sqlpara = new OracleParameter[1];
            if (rcounts != 0)
            {
                Db.Open();
                panel2.Visible = true;
                label2.Visible = true;
                progressBar1.Visible = true;
                progressBar1.Value = 0;
                MessageBox.Show(rcounts+"");
                String tasknum = "";
                for (int i = 0; i < rcounts; i++)
                {
                    //显示进度条
                    Application.DoEvents();
                    tasknum = ds.Tables[0].Rows[i]["tasknum"].ToString();
                    //MessageBox.Show(tasknum);
                    sqlpara = new OracleParameter[1]; ;
                    sqlpara[0] = new OracleParameter("p_tasknum", tasknum);
                    Db.ExecuteNonQueryWithProc("P_PRODUCE_POKE", sqlpara);
                    progressBar1.Value = ((i + 1) * 100 / rcounts);
                    progressBar1.Refresh();
                    label2.Text = "正在生成拨烟计划..." + ((i + 1) * 100 / rcounts).ToString() + "%";
                    label2.Refresh();

                    

                }
                panel2.Visible = false;
                label2.Visible = false;
                progressBar1.Visible = false;
                this.lab_showinfo.Text = "拨烟计划完成!";
                MessageBox.Show("拨烟计划已生成!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
               

            }
            else
            {
                MessageBox.Show("暂无新的分拣任务，点击查询按钮查看新的分拣任务!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void win_pokeplan_Load(object sender, EventArgs e)
        {
            this.taskdata.ClearSelection();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            seek();
            this.lab_showinfo.Text = "点击按钮生成拨烟计划";
        }
    }
}
