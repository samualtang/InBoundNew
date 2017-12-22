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
    public partial class win_trough_handle : Form
    {
        DataSet ds = new DataSet();
        PublicFun pub = new PublicFun(System.IO.Directory.GetCurrentDirectory().ToString() + "\\interface.ini");
        DataBase Db = new DataBase();
        String handle_sign = "", id = "";
        string type = "", troughtype = "";
        public List<string> List = new List<string>();
        public win_trough_handle(String sign, String amend_id, string type, string troughtype)
        {
            InitializeComponent();
            this.type = type;
            this.troughtype = troughtype;
            handle_sign = sign;
            init(handle_sign, amend_id);
            if (handle_sign == "0") this.Text = "分拣通道--新增";
            else
            {
                this.Text = "分拣通道--修改";

                txt_troughdesc.Enabled = false;
                box_replenishline.Enabled = false;
                box_transportationline.Enabled = false;

                if (type == "20" || type == "30")
                {
                    cbthroughnum.Enabled = false;
                }
                this.box_actcount.Enabled = false;
                //radio_fix.Enabled = false;
                //this.radio_single.Enabled = false;

            }

            id = amend_id;
        }

        public void init(String sign, String amend_id)
        {
            Db.Open();
            String sql = "select distinct machineseq from t_produce_sorttrough where cigarettetype='" + type + "'order by machineseq";
            if (type.Equals("10"))
            {
                sql = "select 43 machineseq from dual union select 44 machineseq from dual union select 45 machineseq from dual";
            }
            else if (type.Equals("40"))
            {
                //sql = "select 1096 machineseq from dual union select 2096 machineseq from dual";
                sql = @" select 1058 machineseq,1 groupNo from dual
                            union 
                            select 1059 machineseq,1 groupNo from dual
                            union 
                            select 1060 machineseq,1 groupNo from dual
                            union 
                            select 1061 machineseq,1 groupNo from dual
                            union
                            select 2058 machineseq,2 groupNo from dual
                            union 
                            select 2059 machineseq,2 groupNo from dual
                            union 
                            select 2060 machineseq,2 groupNo from dual
                            union 
                            select 2061 machineseq,2 groupNo from dual";
            }
            DataTable dt = Db.Query(sql);
            this.cbthroughnum.DataSource = dt;
            this.cbthroughnum.DisplayMember = "machineseq";
            this.cbthroughnum.ValueMember = "machineseq";
            this.cbthroughnum.SelectedIndex = 0;

            if (type == "10")
            {
                lbltype.Text = "混合";
                lbllineNum.Text = "高速线";
                txt_troughdesc.Text = "混合道";
                txt_troughdesc.Enabled = false;
                box_replenishline.Visible = false;
                box_transportationline.Visible = false;
                box_actcount.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
            }
            else if (type == "20")
            {
                lbltype.Text = "正常";
                lbllineNum.Text = "高速线";
            }
            else
            {
                lbltype.Text = "异型";
                lbllineNum.Text = "异形烟线";

                txt_troughdesc.Visible = false;
                box_replenishline.Visible = false;
                box_transportationline.Visible = false;
                box_actcount.Visible = false;
                //cbthroughnum.Visible = false;

                label7.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                // label2.Visible = false;
                label3.Visible = false;
            }


            //初始化补货通道下拉框

            DataTable dt_replenishline = new DataTable();
            dt_replenishline.Columns.Add("replenishvalue", typeof(string));
            dt_replenishline.Columns.Add("replenishtxt", typeof(string));

            DataRow dr3 = dt_replenishline.NewRow();
            dr3["replenishvalue"] = "1";
            dr3["replenishtxt"] = "1";

            DataRow dr4 = dt_replenishline.NewRow();
            dr4["replenishvalue"] = "2";
            dr4["replenishtxt"] = "2";

            DataRow dr5 = dt_replenishline.NewRow();
            dr5["replenishvalue"] = "3";
            dr5["replenishtxt"] = "3";

            DataRow dr6 = dt_replenishline.NewRow();
            dr6["replenishvalue"] = "4";
            dr6["replenishtxt"] = "4";

            DataRow dr7 = dt_replenishline.NewRow();
            dr7["replenishvalue"] = "5";
            dr7["replenishtxt"] = "5";

            dt_replenishline.Rows.Add(dr3);
            dt_replenishline.Rows.Add(dr4);
            dt_replenishline.Rows.Add(dr5);
            dt_replenishline.Rows.Add(dr6);
            dt_replenishline.Rows.Add(dr7);

            this.box_replenishline.DataSource = dt_replenishline;
            this.box_replenishline.DisplayMember = "replenishtxt";
            this.box_replenishline.ValueMember = "replenishvalue";
            this.box_replenishline.SelectedIndex = 0;

            //初始化上货通道下拉框

            DataTable dt_transportationline = new DataTable();
            dt_transportationline.Columns.Add("transportationvalue", typeof(string));
            dt_transportationline.Columns.Add("transportationtxt", typeof(string));

            DataRow dr8 = dt_transportationline.NewRow();
            dr8["transportationvalue"] = "1";
            dr8["transportationtxt"] = "1";

            DataRow dr9 = dt_transportationline.NewRow();
            dr9["transportationvalue"] = "2";
            dr9["transportationtxt"] = "2";

            dt_transportationline.Rows.Add(dr8);
            dt_transportationline.Rows.Add(dr9);

            this.box_transportationline.DataSource = dt_transportationline;
            this.box_transportationline.DisplayMember = "transportationtxt";
            this.box_transportationline.ValueMember = "transportationvalue";
            this.box_transportationline.SelectedIndex = 0;

            //初始化烟道状态下拉框
            DataTable dt_actcount = new DataTable();
            dt_actcount.Columns.Add("troughvalue", typeof(string));
            dt_actcount.Columns.Add("troughtxt", typeof(string));

            DataRow dr10 = dt_actcount.NewRow();
            dr10["troughvalue"] = "1";
            dr10["troughtxt"] = "1";

            DataRow dr11 = dt_actcount.NewRow();
            dr11["troughvalue"] = "2";
            dr11["troughtxt"] = "2";

            DataRow dr12 = dt_actcount.NewRow();
            dr12["troughvalue"] = "5";
            dr12["troughtxt"] = "5";

            dt_actcount.Rows.Add(dr12);
            dt_actcount.Rows.Add(dr11);
            dt_actcount.Rows.Add(dr10);

            this.box_actcount.DataSource = dt_actcount;
            this.box_actcount.DisplayMember = "troughtxt";
            this.box_actcount.ValueMember = "troughvalue";
            this.box_actcount.SelectedIndex = 0;



            //修改状态

            if (sign == "1")
            {
                sql = "select linenum,troughnum,troughdesc,machineseq,cigarettecode,cigarettename,cigarettetype,replenishline,transportationline,actcount,machineseq from t_produce_sorttrough where id=" + amend_id + " and cigarettetype=" + type + " and troughtype=" + troughtype;
                DataRow row = Db.Query(sql).Rows[0];
                cbthroughnum.SelectedValue = row[3].ToString();
                this.txt_troughdesc.Text = row[2].ToString();
                this.txt_itemno.Text = row[4].ToString();
                this.txt_itemname.Text = row[5].ToString();
                this.txt_iteminfo.Text = row[5] + "（" + row[4] + "）";

                String linenum = row[0].ToString();
                String radioval = row[3].ToString();
                String cigarettetype = row[6].ToString();

                bool flag = int.Parse(radioval) <= 3;

                box_replenishline.SelectedValue = row[7].ToString();
                box_transportationline.SelectedValue = row[8].ToString();
                box_actcount.SelectedValue = row[9].ToString();

                if (flag)
                {
                    // this.box_machinenum.Visible = true;
                    //  box_machinenum.SelectedValue = row[10].ToString();
                }
            }

            Db.Close();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
            Db.Close();
        }

        private void btn_choose_Click(object sender, EventArgs e)
        {
            win_cigarette_choose choose = new win_cigarette_choose(this, List);
            choose.WindowState = FormWindowState.Normal;
            choose.ShowDialog();
            if (choose.DialogResult == DialogResult.OK)
            {
                List = choose.returnObj;
                if (this.List.Count == 2)
                {
                    this.txt_itemno.Text = List[0];
                    this.txt_itemname.Text = List[1];
                    this.txt_iteminfo.Text = List[1] + "（" + List[0] + "）";
                }
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            String linenum = "";
            if (type != "30")
            {
                linenum = "BS01";
            }
            else
            {
                linenum = "BS02";
            }
            String troughnum = this.cbthroughnum.SelectedValue.ToString();
            String troughdesc = this.txt_troughdesc.Text.Trim();
            String itemno = this.txt_itemno.Text;
            String itemname = this.txt_itemname.Text;
            String radioval = troughnum;
            String groupno = "1";


            List<string> troughnums = (cbthroughnum.DataSource as DataTable).Columns.Count > 1 ?
                (cbthroughnum.DataSource as DataTable).Select("groupno=1").Select(s => s[0].ToString()).ToList() :
                new List<string>();
            //if (troughnum == "2058" || troughnum == "2059" || troughnum == "2060" || troughnum == "2061")
            if (troughnums.Contains(troughnum))
            {
                groupno = "2";

            }
            else if (troughnum == "1096")
            {

            }
            //String cigarettetype = this.box_cigarettetype.SelectedValue.ToString();
            String actcount = this.box_actcount.SelectedValue == null ? "" : this.box_actcount.SelectedValue.ToString();
            String replenishline = this.box_replenishline.SelectedValue == null ? "" : this.box_replenishline.SelectedValue.ToString();
            String transportationline = this.box_transportationline.SelectedValue == null ? "" : this.box_transportationline.SelectedValue.ToString();
            // String machineseq = this.box_machinenum.SelectedValue.ToString();
            if (type == "10")
            {
                actcount = "1";
                replenishline = "5";
                transportationline = "2";
            }
            else if (type == "30")
            {
                actcount = "1";
                replenishline = "1";
            }
            // if (this.radio_fix.Checked) radioval = machineseq;

            //if (troughnum=="")
            //{
            //    MessageBox.Show("请填写通道编号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //if (troughdesc == "")
            //{
            //    MessageBox.Show("请填写通道描述!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            if (itemno == "")
            {
                MessageBox.Show("请选择卷烟品牌!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                Db.Open();
                if (handle_sign == "0")//增加
                {

                    String sql = "insert into t_produce_sorttrough(id,troughnum,machineseq,cigarettecode,cigarettename,state,mantissa,cigarettetype,troughtype,groupno)" +
                                 "values(s_produce_sorttrough.nextval,'" + troughnum + "'," + radioval + ",'" + itemno + "','" + itemname + "','10',0," + type + ",10," + groupno + ")";
                    //MessageBox.Show(sql);
                    //if (troughnum == "2058" || troughnum == "2059" || troughnum == "2060" || troughnum == "2061"|| troughnum == "1058" || troughnum == "1059" || troughnum == "1060" || troughnum == "1061")
                    if ((cbthroughnum.DataSource as DataTable).Select("1=1").Select(s => s[0].ToString()).ToList().Contains(troughnum))
                    {
                        sql = "insert into t_produce_sorttrough(id,troughnum,machineseq,cigarettecode,cigarettename,state,mantissa,cigarettetype,troughtype,groupno)" +
                                  "values(s_produce_sorttrough.nextval,s_produce_sorttrough.nextval," + radioval + ",'" + itemno + "','" + itemname + "','10',0," + type + ",10," + groupno + ")";


                    }
                    if (type == "10")
                    {
                        sql = "insert into t_produce_sorttrough(id,linenum,troughnum,troughdesc,machineseq,cigarettecode,cigarettename,state,mantissa,cigarettetype,replenishline,transportationline,actcount)" +
                                                      "values(s_produce_sorttrough.nextval,'" + linenum + "',highspeed.s_produce_sorttrough.nextval,'" + troughdesc + "'," + radioval + ",'" + itemno + "','" + itemname + "','0',0," + type + "," + replenishline + "," + transportationline + "," + actcount + ")";
                    }
                    else if (type == "30")
                    {
                        sql = "insert into t_produce_sorttrough(id,linenum,troughnum,troughdesc,machineseq,cigarettecode,cigarettename,state,mantissa,cigarettetype,replenishline,transportationline,actcount)" +
                                                      "values(s_produce_sorttrough.nextval,'" + linenum + "',highspeed.s_produce_sorttrough.nextval,'" + troughdesc + "'," + radioval + ",'" + itemno + "','" + itemname + "','0',0," + type + "," + replenishline + "," + transportationline + "," + actcount + ")";
                    }
                    int len = Db.ExecuteNonQuery(sql);
                    if (len != 0) MessageBox.Show("分拣通道创建成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    String sql = "update t_produce_sorttrough set  troughnum=" + radioval + ", machineseq=" + radioval +
                                 ",cigarettecode='" + itemno + "',cigarettename='" + itemname + "' where id=" + id + " and cigarettetype=" + type + " and troughtype=" + troughtype;
                    int len = Db.ExecuteNonQuery(sql);
                    if (len != 0) MessageBox.Show("分拣通道信息修改成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (SqlException se)
            {
                MessageBox.Show(se.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Db.Close();

                this.Close();
            }
        }

        private void txt_troughnum_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }



        //private void radio_fix_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (this.radio_fix.Checked == true)
        //    {
        //        this.box_machinenum.Visible = true;
        //    }
        //    else 
        //    {
        //        this.box_machinenum.Visible = false;
        //    }
        //}

        private void txt_troughnum_Enter(object sender, EventArgs e)
        {
            //this.txt_troughnum.Text = "";
        }

        private void win_trough_handle_Load(object sender, EventArgs e)
        {

        }

    }
}
