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

                if (troughtype == "20" || troughtype == "30")
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
            String sql = "select distinct machineseq from t_produce_sorttrough where cigarettetype='" + type + "' and troughtype ='" + troughtype + "'order by machineseq";
            if (type.Equals("40"))
            {
                //sql = "select 1096 machineseq from dual union select 2096 machineseq from dual";

                //不对状态位进行过滤
                sql = @"select distinct machineseq 
                        from t_produce_sorttrough h 
                        where h.troughtype=10 and h.cigarettetype=40  
                        order by  machineseq";
            }
            DataTable dt = Db.Query(sql);
            this.cbthroughnum.DataSource = dt;
            this.cbthroughnum.DisplayMember = "machineseq";
            this.cbthroughnum.ValueMember = "machineseq";
            this.cbthroughnum.SelectedIndex = 0;
            //判断修改类型
            if (troughtype.Equals("10") )//分拣通道
            {
                if (type.Equals("30"))
                {
                    lbltype.Text = "异型";
                    lbllineNum.Text = "异形烟分拣线";
                    cbthroughnum.Enabled = false; 
                }
                else if (type.Equals("40"))
                {
                    lbltype.Text = "异型";
                    lbllineNum.Text = "异形混合烟分拣";
                }
                else if (type.Equals("20")) 
                {
                    lbltype.Text = "常规";
                    lbllineNum.Text = "常规烟分拣";
                    cbthroughnum.Enabled = false; 
                }
            }
            else if (troughtype.Equals("20"))
            {
                if (type.Equals("20"))
                {
                    lbltype.Text = "常规";
                    label6.Text = "通道类型";
                    lbllineNum.Text = "重力式货架";
                    cbthroughnum.Enabled = false; 
                } 
            }
            else if (troughtype.Equals("30"))
            {
                label1.Visible = false;
                label6.Text = "通道类型";
                lbltype.Text = "主皮带";
                cbthroughnum.Enabled = false; 
            }
            else if (troughtype.Equals("40"))
            {
                label1.Visible = false;
                label6.Text = "通道类型";
                lbltype.Text = "包装机";
                cbthroughnum.Enabled = false; 
            }
            

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

            if (sign == "1")
            {
                sql = "select linenum,troughnum,troughdesc,machineseq,cigarettecode,cigarettename,cigarettetype,replenishline,transportationline,actcount,machineseq "+
                      " from t_produce_sorttrough where id=" + amend_id ;
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
            String machineseq = this.cbthroughnum.Text.ToString();//设备物理号编号:获取选择下拉框内的通道编号
            //String machineseq = this.cbthroughnum.SelectedValue.ToString();//设备物理号编号
            String troughdesc = this.txt_troughdesc.Text.Trim();
            String itemno = this.txt_itemno.Text;
            String itemname = this.txt_itemname.Text;
            String radioval = machineseq;
            String groupno = "1";
            if (machineseq == "")
            {
                MessageBox.Show("请选择通道编号!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }            
            if (itemno == "")
            {
                MessageBox.Show("请选择卷烟品牌!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (machineseq != null && machineseq.Length > 0)
            {
                if (machineseq.Substring(0, 1).Equals("2"))//第二组异形烟
                {
                    groupno = "2";
                }
            }
           // MessageBox.Show("设备编号：" + troughnum + ",通道描述：" + troughdesc + ",品牌号" + itemno + ",品牌名称：" + itemname + ",组号为：" + groupno + ",handle_sign为" + handle_sign, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //String actcount = this.box_actcount.SelectedValue == null ? "" : this.box_actcount.SelectedValue.ToString();
            //String replenishline = this.box_replenishline.SelectedValue == null ? "" : this.box_replenishline.SelectedValue.ToString();
            //String transportationline = this.box_transportationline.SelectedValue == null ? "" : this.box_transportationline.SelectedValue.ToString();
            // String machineseq = this.box_machinenum.SelectedValue.ToString();
            String sql ="";
            try
            {
                Db.Open();
                if (handle_sign == "0")//新增
                {
                    //先判断该品牌是否在异形烟混合道中存在
                    String selectSql = "select count(1) from t_produce_sorttrough h " +
                                      "where h.troughtype=10 and h.cigarettetype=40 and state=10 and h.cigarettecode=" + itemno + " and h.groupno=" + groupno;
                    int count = int.Parse(Db.ExecuteScalar(selectSql).ToString());


                    if (count == 0)//增加
                    {
                        //MessageBox.Show(sql);
                        sql = "insert into t_produce_sorttrough(id,linenum,troughnum,machineseq,cigarettecode,cigarettename," +
                                                              "state,mantissa,cigarettetype,troughtype,replenishline,groupno,seq)" +
                                      "values(s_produce_sorttrough.nextval," + 0 + ",s_produce_sorttrough.nextval," + radioval + ",'" + itemno + "','" + itemname + "'," +
                                      "'10',0," + type + ",10,3," + groupno + ",2)";//异形烟对应第三个件烟补货口，六个异形烟柜对应第四个件烟补货口
                        String errorMsg = "";
                        int len = Db.ExecuteNonQuery(sql,out errorMsg);
                        //if (errorMsg != "")
                        //{
                        //    MessageBox.Show(errorMsg);
                        //    return;
                        //}
                        if (len != 0) MessageBox.Show("异形烟混合通道-" + itemname + "创建成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else//如果该品牌在异形烟混合道中存在，预计主要是原来有，后面被禁用了，采用update的方式
                    {
                        //    sql = "update t_produce_sorttrough set machineseq=" + radioval +
                        //                 ",state=10   where troughtype=10 and h.cigarettetype=40 and cigarettecode=" + itemno; ;
                        //    int len = Db.ExecuteNonQuery(sql);
                        //    if (len != 0) MessageBox.Show("异形烟混合道中-" + itemname + "已经存在，修改状态位及通道号成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        MessageBox.Show("该品牌在混合道中已经存在-" + itemname, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                { 
                    sql = "update t_produce_sorttrough set cigarettecode='" + itemno + "',cigarettename='" + itemname + "',machineseq=" + radioval+"groupno="+groupno+ " where id=" + id; 
              
                    int len = Db.ExecuteNonQuery(sql);
                    if (len != 0) MessageBox.Show("分拣通道信息修改成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception se)
            {
                MessageBox.Show("出现异常："+se.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
