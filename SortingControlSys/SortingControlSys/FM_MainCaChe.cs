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
            dataGridView1.DataSource = UnionCache.GetAllData();
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
            dataGridView1.DataSource = UnionCache.GetSearchData(mainbeltno, groupnono);
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
               if (UnionCache.UpdateData(ce)) 
               {
                   MessageBox.Show("修改成功！");
                   dataGridView1.DataSource = UnionCache.GetAllData();
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

        //private void button3_Click(object sender, EventArgs e)
        //{

        //     InBound.Model.UnionCaChe ce = new InBound.Model.UnionCaChe();

        //     ce.mainbelt = 5;
        //     ce.groupno = 9;
        //     ce.cachesize = Convert.ToDecimal(txt_cachesize.Text);
        //     ce.dispatchenum = Convert.ToDecimal(txt_dispatchenum.Text);
        //     ce.dispatchesize = Convert.ToDecimal(txt_dispatchesize.Text);
        //     decimal states = 5;
        //     switch (cmb_state.SelectedItem.ToString())
        //     {
        //         case "启用":
        //             states = 10;
        //             break;
        //         case "禁用":
        //             states = 0;
        //             break;
        //     }


        //     ce.state = states;
        //     try
        //     {
        //         UnionCache.ceshi(ce);
        //         MessageBox.Show("新增成功！");
        //         binding();
        //     }
        //     catch (Exception ex)
        //     {
        //         MessageBox.Show("新增失败！！"+ex.ToString());
                 
        //     }
             
           
               
        //}
    }
}
