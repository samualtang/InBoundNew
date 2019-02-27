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
using InBound.Business;
using InBound.Model;

namespace highSpeed.baseData
{
    public partial class w_trough_2line : Form
    {
        public w_trough_2line()
        {
            InitializeComponent();
            comboBox_template.Text = "请选择目标线路";

            textBox_hunhe.Text = speacilname;
        }
        PubFunc.DataBase Db = new DataBase();

        static string speacilname = "";
        static string speaciltrough ="";

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox_target.Text.Length <= 0)
            {
                MessageBox.Show("请选择目标线路");
                return;
            }

            Db.Open();
            OracleParameter[] sqlpara;
            sqlpara = new OracleParameter[3];

            sqlpara[0] = new OracleParameter("var_Line", comboBox_target.Text);
            sqlpara[1] = new OracleParameter("p_ErrCode", OracleType.VarChar, 2000);
            sqlpara[2] = new OracleParameter("p_ErrMsg", OracleType.VarChar, 2000);

            sqlpara[0].Direction = ParameterDirection.Input;
            sqlpara[1].Direction = ParameterDirection.Output;
            sqlpara[2].Direction = ParameterDirection.Output;

           
            Db.ExecuteNonQueryWithProc("P_PRODUCE_SORTTROUGH_UNLINE", sqlpara);

         
            string Line = sqlpara[0].Value == null ? "" : sqlpara[0].Value.ToString();
            string errcode = sqlpara[1].Value == null ? "" : sqlpara[1].Value.ToString();
            string errmsg = sqlpara[2].Value == null ? "" : sqlpara[2].Value.ToString();
            Db.Close();
            databinding();
            MessageBox.Show(errmsg);
            
        } 

        //异型烟烟仓
        List<HUNHETROUGH2> troughlist = new List<HUNHETROUGH2>();
        //特异型烟混合道
        List<HUNHETROUGH2> specialtroughlist = new List<HUNHETROUGH2>();
        //分拣线
        decimal[] linenums = null;
        private void w_trough_2line_Load(object sender, EventArgs e)
        {
            comboBox_linenums.SelectedIndex = -1;
            databinding();
            comboBox_linenums.SelectedIndexChanged += new System.EventHandler(comboBox_linenums_SelectedIndexChanged);
        } 
        /// <summary>
        /// 数据绑定tab1
        /// </summary>
        private void databinding()
        {
            linenums = SpecialSmoke.getalllinenum();
            //绑定分拣线  
            if (comboBox_linenums.Items.Count <= 0)
            {
                comboBox_linenums.DataSource = linenums;
                comboBox_linenums.SelectedIndex = 0;
            }

            troughlist = SpecialSmoke.getalltrough(Convert.ToDecimal(comboBox_linenums.SelectedItem));
            specialtroughlist = SpecialSmoke.spcialgetalltrough(Convert.ToDecimal(comboBox_linenums.SelectedItem));

            //comboBox_template.DataSource = linenums.Select(x => x).ToList(); //以一线为模板同步其他线 模板线不允许选择
            //comboBox_template.SelectedIndex = 0;
            comboBox_target.DataSource = linenums.Select(x => x).ToList();

            comboBox_yc1.DataSource = troughlist.Select(x => x.troughnum).ToList();
            comboBox_yc2.DataSource = troughlist.Select(x => x.troughnum).ToList();
            comboBox_yct.DataSource = troughlist.Select(x => x.troughnum).ToList();
            comboBox_retohuhe.DataSource = troughlist.Where(x => x.status == "10").Select(x => x.troughnum).ToList();
            comboBox_spaceyc.DataSource = troughlist.Where(x => x.status == "0").Select(x => x.troughnum).ToList();
             
        }
        private void databinding_comboBox_yc1()
        {
            troughlist = SpecialSmoke.getalltrough(Convert.ToDecimal(comboBox_linenums.SelectedItem));
            comboBox_yc1.DataSource = troughlist.Select(x => x.troughnum).ToList();
        }
        private void databinding_comboBox_yc2()
        {
            troughlist = SpecialSmoke.getalltrough(Convert.ToDecimal(comboBox_linenums.SelectedItem));
            comboBox_yc2.DataSource = troughlist.Select(x => x.troughnum).ToList();
        }


    

        private void comboBox_yc1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string num = comboBox_yc1.SelectedValue.ToString();
                label_ycpp1.Text = troughlist.Where(x => x.troughnum == num).Select(x => x.cigarettename).FirstOrDefault();
                btn_ycstate1.Text = troughlist.Where(x => x.troughnum == num).Select(x => x.status).FirstOrDefault() == "10" ? "已启用" : "已禁用";
            }
            catch
            {
                label_ycpp1.Text = "数据获取失败，请检查数据库连接";
            }
            
        }

        private void comboBox_yc2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { 
                string num = comboBox_yc2.SelectedValue.ToString();
                label_ycpp2.Text = troughlist.Where(x => x.troughnum == num).Select(x => x.cigarettename).FirstOrDefault();
                btn_ycstate2.Text = troughlist.Where(x => x.troughnum == num).Select(x => x.status).FirstOrDefault() == "10" ? "已启用" : "已禁用";
            }
            catch
            {
                label_ycpp2.Text = "数据获取失败，请检查数据库连接";
            }
        }

        private void comboBox_yct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string num = comboBox_yct.SelectedValue.ToString();
                label_ycppt.Text = troughlist.Where(x => x.troughnum == num).Select(x => x.cigarettename).FirstOrDefault();
            }
            catch
            {
                label_ycppt.Text = "数据获取失败，请检查数据库连接";
            }
        }
         
        //选择品牌
        private void btn_search_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox_searchbox.Text;
                List<HUNHETROUGH2> list = specialtroughlist.Where(x => x.cigarettename.IndexOf(name) >= 0).ToList();
                w_trough_pecial fm = new w_trough_pecial(speacilname,speaciltrough,list);

                fm.ShowDialog();
                textBox_hunhe.Text = fm.Controls["textBox_name"].Text;
                label_hhdppt.Text = fm.Controls["label_troughnum"].Text;

                fm.Dispose(); 
            }
            catch (Exception)
            {
                MessageBox.Show("选择失败，请重新选择");
            }
           
        }
 
       
        //互换烟仓
        private void button_huhuan_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确认进行互换:\r\n" + comboBox_yc1.Text + " " + label_ycpp1.Text + "\r\n" + comboBox_yc2.Text + " " + label_ycpp2.Text, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result==DialogResult.OK)
            {
                HUNHETROUGH2 obj1 = new HUNHETROUGH2();
                HUNHETROUGH2 obj2 = new HUNHETROUGH2();

                obj1.troughnum = comboBox_yc1.Text;
                obj2.troughnum = comboBox_yc2.Text;

                obj1.cigarettecode = troughlist.Where(x => x.troughnum == comboBox_yc1.Text).Select(x => x.cigarettecode).FirstOrDefault();
                obj1.cigarettename = troughlist.Where(x => x.troughnum == comboBox_yc1.Text).Select(x => x.cigarettename).FirstOrDefault();
                obj1.machineseq = troughlist.Where(x => x.troughnum == comboBox_yc1.Text).Select(x => x.machineseq).FirstOrDefault();
                obj1.status = troughlist.Where(x => x.troughnum == comboBox_yc1.Text).Select(x => x.status).FirstOrDefault();

                obj2.cigarettecode = troughlist.Where(x => x.troughnum == comboBox_yc2.Text).Select(x => x.cigarettecode).FirstOrDefault();
                obj2.cigarettename = troughlist.Where(x => x.troughnum == comboBox_yc2.Text).Select(x => x.cigarettename).FirstOrDefault();
                obj2.machineseq = troughlist.Where(x => x.troughnum == comboBox_yc2.Text).Select(x => x.machineseq).FirstOrDefault();
                obj2.status = troughlist.Where(x => x.troughnum == comboBox_yc2.Text).Select(x => x.status).FirstOrDefault();

                if (SpecialSmoke.HuHuanYc(obj1, obj2, Convert.ToDecimal(comboBox_linenums.SelectedItem)))
                {
                    MessageBox.Show("互换成功！");
                }
                else
                {
                    MessageBox.Show("互换失败！");
                }
                databinding();
            } 
        }
        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="btn">状态修改按钮</param>
        /// <param name="cmb">通道号下拉框</param>
        /// <returns>0无操作，1成功，2失败</returns>
        private string YCStatusChange(Button btn,ComboBox cmb)
        {
            string newstatevalue;
            string newstatename;
            if (btn.Text == "已启用")
            {
                newstatevalue = "0";
                newstatename = "已禁用";
            }
            else
            {
                newstatevalue = "10";
                newstatename = "已启用";
            }
            DialogResult dia = MessageBox.Show("确当修改状态为" + newstatename + "?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dia == DialogResult.OK)
            {
                HUNHETROUGH2 obj = new HUNHETROUGH2();

                obj.troughnum = cmb.Text;
                obj.status = newstatevalue;
                databinding(); 
                return SpecialSmoke.StatusChange(obj,Convert.ToDecimal(comboBox_linenums.SelectedItem)); 
            }
            else
            {
                return "取消操作！";
            }
        } 
        //烟仓1 设置状态
        private void btn_ycstate1_Click(object sender, EventArgs e)
        {
            string str = YCStatusChange(btn_ycstate1, comboBox_yc1);
            if (str != "取消操作！")
            {
                MessageBox.Show(str);
            }
            databinding();
            //databinding_comboBox_yc1();
            //databinding_comboBox_yc2();
        }
        //烟仓2 设置状态
        private void btn_ycstate2_Click(object sender, EventArgs e)
        {
            string str = YCStatusChange(btn_ycstate2, comboBox_yc2); ;
            if (str != "取消操作！")
            {
                MessageBox.Show(str);
            }
            databinding();
            //databinding_comboBox_yc1();
            //databinding_comboBox_yc2();

        }
         
        private void button_tohunhe_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定进行烟仓与混合道品牌互换？","", MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (result==DialogResult.Cancel)
            {
                label_hhdppt.Text = null;
                textBox_hunhe.Text = null;
                return;
            }
            if (label_hhdppt.Text.Length>0 && textBox_hunhe.Text.Length>0)
            {
                HUNHETROUGH2 hunhe = new HUNHETROUGH2();
                HUNHETROUGH2 yc = new HUNHETROUGH2();

                hunhe.troughnum = label_hhdppt.Text;
                yc.troughnum = comboBox_yct.Text;

                hunhe.cigarettecode = specialtroughlist.Where(x => x.troughnum == label_hhdppt.Text).Select(x => x.cigarettecode).FirstOrDefault();
                hunhe.cigarettename = specialtroughlist.Where(x => x.troughnum == label_hhdppt.Text).Select(x => x.cigarettename).FirstOrDefault();
                hunhe.machineseq = specialtroughlist.Where(x => x.troughnum == label_hhdppt.Text).Select(x => x.machineseq).FirstOrDefault();
                hunhe.status = specialtroughlist.Where(x => x.troughnum == label_hhdppt.Text).Select(x => x.status).FirstOrDefault();

                yc.cigarettecode = troughlist.Where(x => x.troughnum == comboBox_yct.Text).Select(x => x.cigarettecode).FirstOrDefault();
                yc.cigarettename = troughlist.Where(x => x.troughnum == comboBox_yct.Text).Select(x => x.cigarettename).FirstOrDefault();
                yc.machineseq = troughlist.Where(x => x.troughnum == comboBox_yct.Text).Select(x => x.machineseq).FirstOrDefault();
                yc.status = troughlist.Where(x => x.troughnum == comboBox_yct.Text).Select(x => x.status).FirstOrDefault();

                if (SpecialSmoke.HuHuanHunHeDao(hunhe, yc,Convert.ToDecimal(comboBox_linenums.SelectedItem)))
                {
                    MessageBox.Show("互换成功！");
                }
                else
                {
                    MessageBox.Show("互换失败！");
                }
                databinding();
                label_hhdppt.Text = null;
                textBox_hunhe.Text = null;
            }
            else
            {
                MessageBox.Show("请选择要参与互换的混合道品牌");
            }
        }

        private void comboBox_target_SelectedValueChanged(object sender, EventArgs e)
        {
            if(comboBox_target.SelectedValue.ToString()=="1")
            {
                btn_template.Text = "2";
            }
            if (comboBox_target.SelectedValue.ToString() == "2")
            {
                btn_template.Text = "1";
            }
            if (comboBox_target.SelectedValue.ToString() == "3")
            {
                btn_template.Text = "4";
            }
            if (comboBox_target.SelectedValue.ToString() == "4")
            {
                btn_template.Text = "3";
            }
        }

        private void comboBox_linenums_SelectedIndexChanged(object sender, EventArgs e)
        {
            label_hhdppt.Text = null;
            textBox_hunhe.Text = null;
            label_hunhebh.Text = null;
            label_hunhepp.Text = null;
            label_hunhebm.Text = null;
            databinding();
        }

        private void button_searchhunhe_Click(object sender, EventArgs e)
        {
            //查询当前分拣线上的混合道信息
            try
            {
                string name = textBox_searchhnhe.Text;
                List<HUNHETROUGH2> list = specialtroughlist.Where(x => x.cigarettename.IndexOf(name) >= 0).ToList();
                w_trough_pecial fm = new w_trough_pecial(speacilname, speaciltrough, list);

                fm.ShowDialog();
                label_hunhepp.Text = fm.Controls["textBox_name"].Text == "" ? "-" : fm.Controls["textBox_name"].Text;
                label_hunhebh.Text = fm.Controls["label_troughnum"].Text == "品牌通道号" ? "-" : fm.Controls["label_troughnum"].Text;
                label_hunhebm.Text = fm.Controls["label_code"].Text == "品牌编码" ? "-" : fm.Controls["label_code"].Text;
                fm.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("选择失败，请重新选择");
            } 
        }

        private void button_retohunhe_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("确定将混合道品牌移入烟仓？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            //禁用烟仓 新增混合道  
            string groupno = comboBox_retohuhe.SelectedItem.ToString().Substring(0, 1);
            string radioval = comboBox_retohuhe.SelectedItem.ToString();
            string machineseq = groupno + "061";
            string cicode = label_retohuhecode.Text;
            string ciname = label_retohuhename.Text;
            String sql = "";
            String sql2 = "";
            try
            {
                Db.Open();
                //先判断该品牌是否在异形烟混合道中存在
                String selectSql = "select count(1) from t_produce_sorttrough h " +
                                    "where h.troughtype=10 and h.cigarettetype=40 and state=10 and h.cigarettecode=" + cicode + " and h.groupno=" + groupno;
                int count = int.Parse(Db.ExecuteScalar(selectSql).ToString());
                if (count == 0)//增加
                {
                    sql = "insert into t_produce_sorttrough(id,linenum,troughnum,machineseq,cigarettecode,cigarettename," +
                                                            "state,mantissa,cigarettetype,troughtype,replenishline,groupno,seq)" +
                                    "values(s_produce_sorttrough.nextval," + 0 + ",s_produce_sorttrough.nextval," + machineseq + ",'" + cicode + "','" + ciname + "'," +
                                    "'10',0,40,10,3," + groupno + ",2)";
                    String errorMsg = "";
                    int len = Db.ExecuteNonQuery(sql, out errorMsg);

                    sql2 = "update t_produce_sorttrough set state = 0 where cigarettetype = 30 and cigarettecode = " + cicode;
                    String errorMsg2 = "";
                    int len2 = Db.ExecuteNonQuery(sql2, out errorMsg2);
                    if (len != 0 && len2 != 0) MessageBox.Show("异形烟混合通道-" + ciname + "创建成功!源通道禁用", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else 
                {
                    MessageBox.Show("该品牌在混合道中已经存在-" + ciname, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                } 
            }
            catch (Exception se)
            {
                MessageBox.Show("出现异常：" + se.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Db.Close();
                databinding();
            }
        }

        private void comboBox_retohuhe_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string num = comboBox_retohuhe.SelectedValue.ToString();
                label_retohuhename.Text = troughlist.Where(x => x.troughnum == num).Select(x => x.cigarettename).FirstOrDefault();
                label_retohuhecode.Text = troughlist.Where(x => x.troughnum == num).Select(x => x.cigarettecode).FirstOrDefault();
            }
            catch
            {
                label_retohuhename.Text = "数据获取失败，请检查数据库连接";
            }
        }

        private void button_retoyc_Click(object sender, EventArgs e)
        {
            if (label_hunhebh.Text.Count() <= 1 && label_hunhebm.Text.Count() <= 1 && label_hunhepp.Text.Count() <= 1)
            {
                MessageBox.Show("请选择混合道品牌！");
                return;
            }
            //禁用烟仓 新增混合道  
            if (comboBox_spaceyc.Items.Count <= 0)
            {
                MessageBox.Show("请选择目标烟仓！");
                return;
            } 
            string groupno = comboBox_spaceyc.SelectedItem.ToString().Substring(0, 1);
            string troughnum = label_hunhebh.Text.ToString();
            string machineseq = comboBox_spaceyc.SelectedItem.ToString();
            string cicode = label_hunhebm.Text;
            string ciname = label_hunhepp.Text;
            DialogResult result = MessageBox.Show("确定将品牌：" + ciname + "移入烟仓？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            { 
                return;
            }
            String sql = "";
            String sql2 = "";
            try
            {
                Db.Open(); 
                String selectSql ="select count(1) from t_produce_sorttrough h " +
                                    "where h.troughtype=10 and h.cigarettetype=30 and state=10 and h.cigarettecode=" + cicode + " and h.groupno=" + groupno;
                int count = int.Parse(Db.ExecuteScalar(selectSql).ToString());
                if (count == 0)//如果烟仓是未启用的 //先禁用混合道 然后换烟仓品牌
                {
                    sql = "update t_produce_sorttrough set state = 0 where cigarettetype = 40 and troughnum = " + troughnum; 
                    String errorMsg = "";
                    int len = Db.ExecuteNonQuery(sql, out errorMsg);

                    sql2 = "update t_produce_sorttrough set state = 10 ,cigarettecode = " + cicode + ",cigarettename = '" + ciname + "' where cigarettetype = 30 and machineseq = " + machineseq;
                    String errorMsg2 = "";
                    int len2 = Db.ExecuteNonQuery(sql2, out errorMsg2);
                    if (len != 0 && len2 != 0) MessageBox.Show("异形烟烟仓-" + ciname + "创建成功!源混合道禁用", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("该烟仓中已经存在启用的品牌-" + ciname+",请先禁用烟仓！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception se)
            {
                MessageBox.Show("出现异常：" + se.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Db.Close();
                label_hunhebh.Text = "-";
                label_hunhebm.Text = "-";
                label_hunhepp.Text = "-";
                databinding();
            }
        }

   
      
        

  
    }
}
