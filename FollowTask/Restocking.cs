using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using System.Configuration;
using InBound.Business;
using InBound.Model;

namespace FollowTask
{
    public partial class Restocking : Form
    {
        public Restocking()
        {
            InitializeComponent();
        }
        public InBound.WriteLog writeLog = InBound.WriteLog.GetLog();

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = FolloTaskService.Restocking(textBox1.Text);  
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox_jym.Text = dataGridView1.SelectedCells[2].Value.ToString(); 
        }

        List<InBound.Model.TroughNumList> cmb1;
        List<InBound.Model.TroughNumList> cmb2;
        bool falg = false;
        private void Restocking_Load(object sender, EventArgs e)
        {
            writeLog.Write("手动补货数据录入界面打开！");
            comboBox1_zlhj.SelectedIndex = -1;
            cmb1 = FolloTaskService.GetHJTroughNum();
            comboBox1_zlhj.DataSource = cmb1;
            comboBox1_zlhj.DisplayMember = "troughnun";
            comboBox1_zlhj.ValueMember = "troughnun";
            comboBox1_zlhj.SelectedIndex = 0;

            comboBox2_yg.SelectedIndex = -1;
            cmb2 = FolloTaskService.GetYGTroughNum();
            comboBox2_yg.DataSource = cmb2;
            comboBox2_yg.DisplayMember = "troughnun";
            comboBox2_yg.ValueMember = "troughnun";
            comboBox2_yg.SelectedIndex = 0;


            falg = true;
        }
        private bool check1()
        {
            int tip1 = 0;
            int tip2 = 0;
            foreach (var item in cmb1)
            {
                if (item.troughnun == comboBox1_zlhj.Text)
                {
                    tip1 = 1;
                } 
            }
            foreach (var item in cmb2)
            {
                if (item.troughnun == comboBox2_yg.Text)
                {
                    tip2 = 2;
                }
            }


            if (tip1 != 0 && tip2 != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        
        }
        private bool check2()
        {
            
                string cid1 = "1";
                string cid2 = "2";

                foreach (var item in cmb1)
                {
                    if (item.troughnun == comboBox1_zlhj.SelectedValue.ToString())
                    {
                        cid1 = item.Cid;

                    }
                }
                foreach (var item in cmb2)
                {
                    if (item.troughnun == comboBox2_yg.SelectedValue.ToString())
                    {
                        cid2 = item.Cid;
                    }
                }
                if (cid1 == cid2 && cid1 != "1" && cid2 != "2")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(textBox_num.Text) <= 0)
                {
                    MessageBox.Show("数量不能小于0!");
                    return;
                }
            }
            catch (Exception)
            { 
                MessageBox.Show("数量格式有误！");
                return;
            }
           
            if (!check1())
            {
                MessageBox.Show("重力式货架或烟柜编号输入有误！");
                return;
            }
            if (!check2())
            {
                DialogResult res = MessageBox.Show("重力式货架与烟柜品牌不符合，是否继续？", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (res == DialogResult.Cancel)
                {
                    return;
                }
            }

            DialogResult re = MessageBox.Show("确认下达补货任务？\r\n \r\n" + comboBox1_zlhj.SelectedValue + " " + labelcmb1_jymc.Text + " 到 " + comboBox2_yg.SelectedValue + " " + labelcmb2_jymc.Text + "\r\n烟件数为：" + textBox_num.Text + "\r\n垛形为：" + textBox_dx.Text + "\r\n件烟码为：" + textBox_jym.Text + "\r\n实物品牌为：" + textBox_jymc.Text, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (re == DialogResult.OK)
            {

                string str = "";
                try
                {
                    bool falg = FolloTaskService.InsertRestocking(comboBox1_zlhj.SelectedValue.ToString(), comboBox2_yg.SelectedValue.ToString(), textBox_jym.Text, textBox_jybm.Text, Convert.ToInt32(textBox_num.Text), Convert.ToDecimal(textBox_dx.Text));
                    //bool troughfalg = FolloTaskService.TroughMantissaChange(comboBox1.Text, Convert.ToDecimal(textBox_num.Text));
                    if (falg)
                    {
                        MessageBox.Show("任务下达成功！");
                        str = "下达成功";
                    }
                    else
                    {
                        MessageBox.Show("任务下达失败！");
                        str = "下达成功";
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("输入数据有误！");
                    str = "数据有误,下达失败！";
                }
                finally
                {
                    writeLog.Write("下达补货任务：" + comboBox1_zlhj.SelectedValue + " " + labelcmb1_jymc.Text + " 到 " + comboBox2_yg.SelectedValue + " " + labelcmb2_jymc.Text + "  ,烟件数为：" + textBox_num.Text + "  ,垛形为：" + textBox_dx.Text + "  ,件烟码为：" + textBox_jym.Text + "  ,实物品牌为：" + textBox_jymc.Text);
                }
            }
        }
    
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in cmb1)
            {
                if (item == comboBox1_zlhj.SelectedValue)
                {
                    labelcmb1_jymc.Text = item.cname;
                    bindtext(item); 
                }
                else if (item.troughnun == comboBox1_zlhj.SelectedValue.ToString())
                {
                    if (item.cname == null)
                    {
                        labelcmb1_jymc.Text = "暂无品牌！！！";
                    }
                    else
                    {
                        labelcmb1_jymc.Text = item.cname;
                    }
                    bindtext(item); 
                }
            }
            comboBox1_zlhj.SelectAll();
        }
        public void bindtext(TroughNumList list)
        {
            RestockingData rt = FolloTaskService.RestockingOrDefult(list.Cid);
            if (rt != null)
            {
                textBox_jym.Text = rt.bigbox_bar;
                textBox_jybm.Text = rt.cid;
                textBox_jymc.Text = rt.cname;
                textBox_dx.Text = rt.dxtype;
            }
            else
            {
                textBox_jym.Text = "";
                textBox_jybm.Text = "";
                textBox_jymc.Text = "";
                textBox_dx.Text = "";
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in cmb2)
            {
                if (item == comboBox2_yg.SelectedValue)
                {
                    labelcmb2_jymc.Text = item.cname;
                }
                else if (item.troughnun == comboBox2_yg.SelectedValue.ToString())
                {
                    labelcmb2_jymc.Text = item.cname;
                }
            }
            comboBox2_yg.SelectAll();

        }
 
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (falg)
            {
                foreach (var item in cmb1)
                {
                    labelcmb1_jymc.Text = "";                    
                    if (item.troughnun == comboBox1_zlhj.Text)
                    {
                        comboBox1_zlhj.SelectedIndex = comboBox1_zlhj.Items.IndexOf(item);
                        labelcmb1_jymc.Text = item.cname;
                        return;
                    }
                  

                }
            } 
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            if (falg)
            {
                foreach (var item in cmb2)
                {
                    labelcmb2_jymc.Text = "";
                    if (item.troughnun == comboBox2_yg.Text)
                    {
                        comboBox2_yg.SelectedIndex = comboBox2_yg.Items.IndexOf(item);
                        labelcmb2_jymc.Text = item.cname;
                        return;
                    }
                }
            }
        }

        private void textBox_jym_TextChanged(object sender, EventArgs e)
        {
            RestockingData rt = FolloTaskService.RestockingByDx(textBox_jym.Text);
            if (rt != null)
            {
                textBox_jym.Text = rt.bigbox_bar;
                textBox_jybm.Text = rt.cid;
                textBox_jymc.Text = rt.cname;
                textBox_dx.Text = rt.dxtype; 
            }
            else
            {
                textBox_jybm.Text = "";
                textBox_jymc.Text = "";
                textBox_dx.Text = "";
            }
        }

     


    } 
}

