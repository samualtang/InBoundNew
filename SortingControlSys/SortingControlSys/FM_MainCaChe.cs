using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound.Business;

namespace SortingControlSys
{
    public partial class FM_MainCaChe : Form
    {
        public FM_MainCaChe()
        {
            InitializeComponent();

        }

        private void FM_MainCaChe_Load(object sender, EventArgs e)
        {
            binding();


        }
        public void binding()
        {
            dataGridView1.DataSource = UnionCacheServer.GetAllData();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            decimal mainbeltno ;
            decimal groupnono ;
            try
            { 
                if (txt_mainbeltno.Text.Length <= 0)
                { 
                    mainbeltno = 0;
                }
                else
                {
                    mainbeltno = Convert.ToDecimal(txt_mainbeltno.Text);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("主皮带输入格式有误！");
                return;
            }

            try
            {
                if (txt_groupnono.Text.Length <= 0)
                {
                    groupnono = 0; 
                }
                else
                {
                    groupnono = Convert.ToDecimal(txt_groupnono.Text); 
                }
            }
            catch (Exception)
            {
                MessageBox.Show("分拣组号输入格式有误！");
                return;
            } 
            dataGridView1.DataSource = UnionCacheServer.GetSearchData(mainbeltno, groupnono);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>=0)
            {
                txt_mainbelt.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txt_groupno.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txt_cachesize.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txt_dispatchenum.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txt_dispatchesize.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

                switch (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value))
                {
                    case 10:
                        cmb_state.SelectedIndex = 0;
                        break;
                    case 0:
                        cmb_state.SelectedIndex = 1;
                        break;
                }
 
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                string Status = "";
                switch (e.Value.ToString())
                {
                    case "0":
                        Status = "禁用";
                        break;
                    case "10":
                        Status = "启用"; 
                        break; 
                }
                e.Value = Status;
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            InBound.Model.UnionCaChe ce = new InBound.Model.UnionCaChe();
            try
            {
                ce.mainbelt = Convert.ToDecimal(txt_mainbelt.Text);
                ce.groupno = Convert.ToDecimal(txt_groupno.Text);
                ce.cachesize = Convert.ToDecimal(txt_cachesize.Text);
                ce.dispatchenum = Convert.ToDecimal(txt_dispatchenum.Text);
                ce.dispatchesize = Convert.ToDecimal(txt_dispatchesize.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("信息输入格式不正确！");
                return;
            }
            if (ce.cachesize>280)
            {
                MessageBox.Show("输入的缓存量上限过大！");
                return;
            }
            else if (ce.dispatchesize >ce.cachesize)
            {
                MessageBox.Show("每次订单数量超过缓存上限！");
                return;
            }
            else if (ce.dispatchenum > ce.cachesize)
            {
                MessageBox.Show("空余缓存量超过缓存上限！");
                return;
            }
            decimal states = 5;
            switch (cmb_state.SelectedItem.ToString())
            {
                case "启用":
                    states = 10;
                    break;
                case "禁用":
                    states = 0;
                    break;
            }


            ce.state = states;

           DialogResult re =  MessageBox.Show("确定修改 " + ce.mainbelt + "号主皮带上第 " + ce.groupno + "分拣组的信息？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

           if (re==DialogResult.OK)
           {
               if (UnionCacheServer.UpdateData(ce))
               {
                   MessageBox.Show("修改成功！");
                   dataGridView1.DataSource = UnionCacheServer.GetAllData();
               }
               else
               {
                   MessageBox.Show("修改失败！");
               }
           }
           else
           {
               return;
           }
            
        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            decimal num = 0;
            try
            {
                num = Convert.ToDecimal(textBox_num.Text);
                if (num < 0)
                {
                    MessageBox.Show("输入数值必须为非负数！");
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("输入数量格式有误！");
                return;
            }
            if (!Check(num,1))
            {
                return;
            } 
            DialogResult re = MessageBox.Show("确定增加所有合流缓存上限为：" + textBox_num.Text + "  ？", "数据修改", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (re==DialogResult.OK)
            {
                UnionCacheServer.dateplup(num);
                dataGridView1.DataSource = UnionCacheServer.GetAllData();

            }
        }

        private void btn_down_Click(object sender, EventArgs e)
        {
            decimal num = 0;
            try
            {
                num = Convert.ToDecimal(textBox_num.Text);
                if (num<0)
                {
                    MessageBox.Show("输入数值必须为非负数！");
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("输入数量格式有误！");
                return;
            }
            if (!Check(num,2))
            {
                return;
            } 
            DialogResult re = MessageBox.Show("确定减少所有合流缓存上限为：" + textBox_num.Text + "  ？", "数据修改", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (re == DialogResult.OK)
            {
                UnionCacheServer.datepldown(num);
                dataGridView1.DataSource = UnionCacheServer.GetAllData();

            }
        }

        /// <summary>
        /// 检查是否低于0或大于上限
        /// </summary>
        /// <param name="num"></param>
        /// <param name="type">1为增加 2为减少</param>
        public bool Check (decimal num ,int type )
        {
            string listmax = "";
            string listmin = "";
            for (int i =dataGridView1.Rows.Count-1; i >= 0; i--)
            {
                if ( Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value) + num>280)
                {
                    listmax = listmax + " " + (i + 1) + " ";
                } 
                if ( Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value) - num<0)
                {
                    listmin = listmin + " " + (i + 1) + " ";
                } 
            }
            if (listmax != "" && type == 1)
            {
                MessageBox.Show(listmax + "号缓存带超出上限");
                return false;
            }
            if (listmin != "" && type == 2)
            {
                MessageBox.Show(listmax + "号缓存带低于0");
                return false;
            }
            return true;
            
        }

        private void btn_dispatchenum_Click(object sender, EventArgs e)
        {
            decimal num = 0;
            try
            {
                num = Convert.ToDecimal(txt_dispatchenum_pl.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("输入数量格式有误！");
            }
            DialogResult re = MessageBox.Show("确定修改缓存每次空余量为：" + txt_dispatchenum_pl.Text + "  ？", "数据修改", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (re == DialogResult.OK)
            {
                UnionCacheServer.up_dispatchenum(num);
                dataGridView1.DataSource = UnionCacheServer.GetAllData();

            }
        }

        private void btn_dispatchesize_Click(object sender, EventArgs e)
        {
            decimal num = 0;
            try
            {
                num = Convert.ToDecimal(txt_dispatchesize_pl.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("输入数量格式有误！");
            }
            DialogResult re = MessageBox.Show("确定修改每次缓存补烟数为：" + txt_dispatchesize_pl.Text + "  ？", "数据修改", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (re == DialogResult.OK)
            {
                UnionCacheServer.up_dispatchesize(num);
                dataGridView1.DataSource = UnionCacheServer.GetAllData();

            }
        }

       

     

     
    }
}
