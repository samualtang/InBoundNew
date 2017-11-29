using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms;
using highSpeed.PubFunc;

namespace highSpeed.statement
{
    public partial class win_binningSummary : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        public win_binningSummary()
        {
            InitializeComponent();
            init();
            seek();
        }

        public void init()
        {
            

            //初始化查询条件下拉框
            DataTable conditiontable = new DataTable();
            conditiontable.Columns.Add("value", typeof(string));
            conditiontable.Columns.Add("txtvalue", typeof(string));

            DataRow dr = conditiontable.NewRow();
            dr["value"] = "10";
            dr["txtvalue"] = "循环分拣线";

            DataRow dr1 = conditiontable.NewRow();
            dr1["value"] = "20";
            dr1["txtvalue"] = "新立式分拣线";

            DataRow dr2 = conditiontable.NewRow();
            dr2["value"] = "30";
            dr2["txtvalue"] = "老立式分拣线";

            conditiontable.Rows.Add(dr);
            conditiontable.Rows.Add(dr1);
            conditiontable.Rows.Add(dr2);

            this.box_sortline.DataSource = conditiontable;
            this.box_sortline.DisplayMember = "txtvalue";
            this.box_sortline.ValueMember = "value";
            this.box_sortline.SelectedIndex = 0;

        }

        private void seek()
        {
            String date = this.dateTimePick.Value.ToShortDateString();
            DateTime dt = this.dateTimePick.Value;
            String date2 = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString();

            String sortline = this.box_sortline.SelectedValue + "";
            String strsql = "";

            //MessageBox.Show(date+"==="+date2);
            if(sortline=="10")
            {
                strsql="select sum(ceil(h.taskquantity/30)) as sl,h.regioncode as czxx,h.packagedesc FROM HTKADMIN.T_TASK h where  h.tasknum like '"+date2+"%' group by h.regioncode,h.packagedesc";
            }
            if(sortline=="20")
            {
                strsql="select sum(ceil(s.taskquantity/36))as sl,s.regioncode as czxx,s.packagedesc FROM SORTADMIN.T_TASK S where  s.tasknum like '"+date2+"%' group by s.regioncode,s.packagedesc";
            }
            if(sortline=="30")
            {
                strsql="select sum(ceil(quantity/30))as sl,dpid,decode(abnormity,'分拣','30条箱',abnormity) packagedesc "+
			            "from ( "+
  			            "		select sum(t.quality) quantity,m.sjobnum,t.abnormity,m.dpid "+
  			            "		from pick.pick_task_detail t,pick.pick_task_master m  "+
  			            "		where m.makebatch='"+date+"' and m.sjobnum=t.sjobnum "+
  			            "		group by m.sjobnum,t.abnormity,m.dpid "+
  			            "	 ) "+
			            "group by dpid,abnormity "+
			            "order by dpid,packagedesc";
            }
            //MessageBox.Show(strsql);
            //Bind(strsql);
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
               
                this.binningdata.DataSource = ds.Tables[0];
                this.binningdata.AutoGenerateColumns = false;

                string columnwidths = pub.IniReadValue(this.Name, this.binningdata.Name);
                if (columnwidths != "")
                {
                    string[] columns = columnwidths.Split(',');
                    int j = 0;
                    for (int i = 0; i < columns.Length; i++)
                    {
                        if (binningdata.Columns[i].Visible == true)
                        {
                            binningdata.Columns[j].Width = Convert.ToInt32(columns[i]);
                            j = j + 1;
                        }
                    }
                }
                binningdata.ClearSelection();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        private void w_binningSummary_Load(object sender, EventArgs e)
        {
            this.binningdata.ClearSelection();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            seek();
        }

        private void box_sortline_SelectedIndexChanged(object sender, EventArgs e)
        {
            seek();
        }
    }
}
