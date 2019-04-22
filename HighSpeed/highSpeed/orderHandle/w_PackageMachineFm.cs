using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using highSpeed.PubFunc;
using InBound.Business;

namespace highSpeed.orderHandle
{
    public partial class w_PackageMachineFm : Form
    {
        public w_PackageMachineFm()
        {
            InitializeComponent();

            comboBox_querylist.SelectedIndex = 0;
            dataget();
        }
        DataBase db = new DataBase();
        DataSet ds = new DataSet();
        string sql;
        List<string[]> list = new List<string[]>();
        private void button_query_Click(object sender, EventArgs e)
        {
            dataget();
        }

        public void dataget()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox_querytext.Text))
                {
                    sql = "select a.synseq,a.EXPORT,count(distinct a.REGIONCODE) COUNTREGIONCODE,count(distinct a.BILLCODE) COUNTBILLCODE,sum(a.QUANTITY) COUNTQUANTITY from v_produce_packageinfo a ,t_produce_synseq b where a.SYNSEQ =b.synseq and a.EXPORT = b.packageno and b.pmstate = 1 and b.orderdate =(seleCt  max(orderdate) from t_produce_order) group by a.synseq,a.EXPORT order by a.synseq,a.EXPORT";
                }
                else if (comboBox_querylist.SelectedIndex == 0)
                {
                    decimal SYNSEQ = Convert.ToDecimal(textBox_querytext.Text);
                    sql = "select a.synseq,a.EXPORT,count(distinct a.REGIONCODE) COUNTREGIONCODE,count(distinct a.BILLCODE) COUNTBILLCODE,sum(a.QUANTITY) COUNTQUANTITY from v_produce_packageinfo a ,t_produce_synseq b where a.SYNSEQ =b.synseq and a.EXPORT = b.packageno and b.pmstate = 1 and b.orderdate =(seleCt  max(orderdate) from t_produce_order) and a.SYNSEQ = " + SYNSEQ + " group by a.synseq,a.EXPORT order by a.synseq,a.EXPORT";
                }
                else
                {
                    decimal EXPORT = Convert.ToDecimal(textBox_querytext.Text);
                    sql = "select a.synseq,a.EXPORT,count(distinct a.REGIONCODE) COUNTREGIONCODE,count(distinct a.BILLCODE) COUNTBILLCODE,sum(a.QUANTITY) COUNTQUANTITY from v_produce_packageinfo a ,t_produce_synseq b where a.SYNSEQ =b.synseq and a.EXPORT = b.packageno and b.pmstate = 1 and b.orderdate =(seleCt  max(orderdate) from t_produce_order) and a.EXPORT = " + EXPORT + " group by a.synseq,a.EXPORT order by a.synseq,a.EXPORT";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("输入有误，请确认后再查询！");
                return;
            }


            ds = db.QueryDs(sql);
            orderdata.DataSource = ds.Tables[0];
            list.Clear();
        }

        private void orderdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string[] txt = new string[2];
            if (e.ColumnIndex == 0)
            {
                if (orderdata.RowCount > 0)
                {
                    bool obj = (bool)this.orderdata.CurrentRow.Cells[0].EditedFormattedValue;
                    txt[0] = this.orderdata.CurrentRow.Cells[1].Value.ToString();
                    txt[1] = this.orderdata.CurrentRow.Cells[2].Value.ToString();
                    if (list.Count == 0)
                    {
                        list.Add(txt);                        
                    }
                    else
                    {
                        for (int i = 0; i <= list.Count; i++)
                        {
                            if ( i == list.Count)
                            {
                                list.Add(txt);
                                break;
                            }
                            if (list[i][0] == txt[0] && list[i][1] == txt[1])
                            {
                                //list.Remove(txt);
                                list.RemoveAt(i);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void button_all_Click(object sender, EventArgs e)
        {
            list.Clear();
            for (int i = 0; i < this.orderdata.RowCount; i++)
            {
                string[] txt = new string[2];
                orderdata.Rows[i].Cells[0].Value = "true";
                txt[0] = orderdata.Rows[i].Cells[1].Value.ToString();
                txt[1] = orderdata.Rows[i].Cells[2].Value.ToString();
                list.Add(txt);
            }
        }
        PackageService ps = new PackageService();
        TBJDataSchdule ts = new TBJDataSchdule();
        private void button_datacomplte_Click(object sender, EventArgs e)
        {
            string str = "";
            foreach (var item in list)
            {
                str += "批次:" + item[0] + "、" + "包装机:" + item[1] + ";  ";
            }
            DialogResult re = MessageBox.Show("是否按\r\n" + str + "\r\n顺序生成包装机数据", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (re == DialogResult.Cancel)
            {
                return;
            }
            var date1 = System.DateTime.Now;
            foreach (var item in list)
            {
                ps.GetAllOrder(Convert.ToDecimal(item[1]), Convert.ToDecimal(item[0]));
            }
            var date2 = System.DateTime.Now;

            MessageBox.Show("包装机数据生成成功!\r\n耗时：" + Math.Ceiling((date2 - date1).TotalSeconds) + " 秒");
            dataget();
        }

        private void button_TBJ_Click(object sender, EventArgs e)
        {
            var date1 = System.DateTime.Now;
            foreach (var item in ts.foreachdata())
            {
                ts.CallBackTBJ(item);
            }
            var date2 = System.DateTime.Now;

            MessageBox.Show("贴标机数据生成成功!\r\n耗时：" + Math.Ceiling((date2 - date1).TotalSeconds) + " 秒");
        }
    }
}
