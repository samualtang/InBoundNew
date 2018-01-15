using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using highSpeed.PubFunc;

namespace highSpeed.baseData
{
    public partial class win_customer : Form
    {
       
        DataSet ds = new DataSet();
        DataSet detail_ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public win_customer()
        {
            InitializeComponent();

            
           // ds.Add(dt);



        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
                Db.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            seek();
        }
        private void seek()
        {
            String strsql = "SELECT id as remarks,code,NAME,contactaddress,customerlevel,contact,contactphone FROM t_wms_customer";
            String types = this.typesCombox.Text;
            String keywd = this.keywd.Text.Trim();
            
            if (types != null && types != "" && keywd != null && keywd != "")
            {
                String field="";
                if (types == "专卖证号") field = "id";
                if (types == "零售户CODE") field = "code";
                if (types == "零售户店名") field = "name";
                strsql = strsql + " where "+field+" like '%" + keywd + "%'";
            }

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
               // DataTable dt = DataPublic.dataTable;
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


                this.dataView.DataSource = ds.Tables[0];
                this.dataView.AutoGenerateColumns = false;

                string columnwidths = pub.IniReadValue(this.Name, this.dataView.Name);
                if (columnwidths != "")
                {
                    string[] columns = columnwidths.Split(',');
                    int j = 0;
                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (dataView.Columns[i].Visible == true)
                        {
                            dataView.Columns[j].Width = Convert.ToInt32(columns[i]);
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

        private void btn_print_Click(object sender, EventArgs e)
        {
            dgVprint1.MainTitle = "零售户信息表";
            // dgVprint1.SubTitle = "这是子标题，当然也可以不设的";
            dgVprint1.PaperLandscape = true;//用横向打印，默认是纵向哦
            dgVprint1.Print(dataView);
        }

    }
}
