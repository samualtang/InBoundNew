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



        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            DialogResult re = MessageBox.Show("确认下达补货任务？\r\n" + comboBox1.SelectedValue + labelcmb1.Text + " 到 " + comboBox2.SelectedValue + labelcmb2.Text + "\r\n烟件数为：" + textBox_num.Text  +"\r\n垛形为：" + textBox5.Text + "\r\n件烟码为：" + textBox2.Text, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
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
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            
            foreach (var item in cmb1)
            {
                if (item.troughnun == comboBox1.SelectedValue.ToString())
                {
                    labelcmb1.Text = item.cname;
                }
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
        }

       
    }
}
