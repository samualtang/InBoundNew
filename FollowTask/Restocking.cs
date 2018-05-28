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

namespace FollowTask
{
    public partial class Restocking : Form
    {
        public Restocking()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = FolloTaskService.Restocking(textBox1.Text);  
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView1.SelectedCells[2].Value.ToString();
            textBox3.Text = dataGridView1.SelectedCells[0].Value.ToString();
            textBox4.Text = dataGridView1.SelectedCells[1].Value.ToString();
            textBox5.Text = dataGridView1.SelectedCells[3].Value.ToString();

        }

        List<InBound.Business.TroughNumList> cmb1;
        List<InBound.Business.TroughNumList> cmb2;
        bool falg = false;
        private void Restocking_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            cmb1 = FolloTaskService.GetHJTroughNum();
            comboBox1.DataSource = cmb1;
            comboBox1.DisplayMember = "troughnun";
            comboBox1.ValueMember = "troughnun";
            comboBox1.SelectedIndex = 0;

            comboBox2.SelectedIndex = -1;
            cmb2 = FolloTaskService.GetYGTroughNum();
            comboBox2.DataSource = cmb2;
            comboBox2.DisplayMember = "troughnun";
            comboBox2.ValueMember = "troughnun";
            comboBox2.SelectedIndex = 0;


            falg = true;
        }
        private bool check1()
        {
            int tip1 = 0;
            int tip2 = 0;
            foreach (var item in cmb1)
            {
                if (item.troughnun == comboBox1.Text)
                {
                    tip1 = 1;
                } 
            }
            foreach (var item in cmb2)
            {
                if (item.troughnun == comboBox2.Text)
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
                    if (item.troughnun == comboBox1.SelectedValue.ToString())
                    {
                        cid1 = item.Cid;

                    }
                }
                foreach (var item in cmb2)
                {
                    if (item.troughnun == comboBox2.SelectedValue.ToString())
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
                    MessageBox.Show("数量不能为0!");
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

            DialogResult re = MessageBox.Show("确认下达补货任务？\r\n" + comboBox1.SelectedValue + " " + labelcmb1.Text + " 到 " + comboBox2.SelectedValue + " " + labelcmb2.Text + "\r\n烟件数为：" + textBox_num.Text + "\r\n垛形为：" + textBox5.Text + "\r\n件烟码为：" + textBox2.Text, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (re == DialogResult.OK)
            {
                try
                {
                    bool falg = FolloTaskService.InsertRestocking(comboBox1.SelectedValue.ToString(), comboBox2.SelectedValue.ToString(), textBox2.Text, textBox3.Text, Convert.ToInt32(textBox_num.Text), Convert.ToDecimal(textBox5.Text));
                    if (falg)
                    {
                        MessageBox.Show("任务下达成功！");
                    }
                    else
                    {
                        MessageBox.Show("任务下达失败！");

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("输入数据有误！");
                }

            }
        }
    
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in cmb1)
            {
                if (item == comboBox1.SelectedValue)
                {
                    labelcmb1.Text = item.cname;
                    bindtext(item); 
                }
                else if (item.troughnun == comboBox1.SelectedValue.ToString())
                {
                    if (item.cname == null)
                    {
                        labelcmb1.Text = "暂无品牌！！！";
                    }
                    else
                    {
                        labelcmb1.Text = item.cname;
                    }
                    bindtext(item); 
                }
            }
            comboBox1.SelectAll();
        }
        public void bindtext(TroughNumList list)
        {
            RestockingData rt = FolloTaskService.RestockingOrDefult(list.Cid);
            if (rt != null)
            {
                textBox2.Text = rt.bigbox_bar;
                textBox3.Text = rt.cid;
                textBox4.Text = rt.cname;
                textBox5.Text = rt.dxtype;
            }
            else
            {
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in cmb2)
            {
                if (item == comboBox2.SelectedValue)
                {
                    labelcmb2.Text = item.cname;
                }
                else if (item.troughnun == comboBox2.SelectedValue.ToString())
                {
                    labelcmb2.Text = item.cname;
                }
            }
            comboBox2.SelectAll();

        }
 
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (falg)
            {
                foreach (var item in cmb1)
                {
                    labelcmb1.Text = "";                    
                    if (item.troughnun == comboBox1.Text)
                    {
                        comboBox1.SelectedIndex = comboBox1.Items.IndexOf(item);
                        //MessageBox.Show("当前文本：" + item.cname + "     " + item.Cid + "\r\n comboBox1选择值为：" + comboBox1.SelectedValue.ToString());
                        labelcmb1.Text = item.cname;
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
                    labelcmb2.Text = "";
                    if (item.troughnun == comboBox2.Text)
                    {
                        comboBox2.SelectedIndex = comboBox2.Items.IndexOf(item);
                        //MessageBox.Show("当前文本：" + item.cname + "     " + item.Cid + "\r\n comboBox1选择值为：" + comboBox1.SelectedValue.ToString());
                        labelcmb2.Text = item.cname;
                        return;
                    }
                }
            } 
        }

     


    } 
}

