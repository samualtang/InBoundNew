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
using System.Threading;

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
        void PackageSort()
        {
            var date1 = System.DateTime.Now;
            foreach (var item in list)
            {
                ps.GetAllOrder(Convert.ToDecimal(item[1]), Convert.ToDecimal(item[0]));
            }
            var date2 = System.DateTime.Now;

            MessageBox.Show("包装机数据生成成功!\r\n耗时：" + Math.Ceiling((date2 - date1).TotalSeconds) + " 秒");
            InBound.WriteLog.GetLog().Write("包装机数据生成成功!\r\n耗时：" + Math.Ceiling((date2 - date1).TotalSeconds) + " 秒");
            updateControl(orderdata, true, true);
        }
        DateTime date1 = System.DateTime.Now;
        void PackageCallback()
        {
            paclist.Clear();
             date1 = System.DateTime.Now;
           //foreach (var item in ts.foreachdata())
           // {
           //    Thread  thread=new Thread(()=>
           //     ts.CallBackTBJ2(item));
           //    thread.Start();
           // } 
            Thread thread = new Thread(() =>
            {
                TBJDataSchdule ts = new TBJDataSchdule();
                ts.CallBackTBJ2(1);
                finished(1);
            });
            thread.Start();
            Thread thread2 = new Thread(() =>
            {
                TBJDataSchdule ts1 = new TBJDataSchdule();
                ts1.CallBackTBJ2(2);
                finished(2);
            });
            thread2.Start();
            Thread thread3 = new Thread(() =>
            {
                TBJDataSchdule ts2 = new TBJDataSchdule();
                ts2.CallBackTBJ2(3);
                finished(3);
            });
            thread3.Start();
            Thread thread4 = new Thread(() =>
            {
                TBJDataSchdule ts4 = new TBJDataSchdule();
                ts4.CallBackTBJ2(4);
                finished(4);
            });
            thread4.Start();
            Thread thread5 = new Thread(() =>
            {
                TBJDataSchdule ts5 = new TBJDataSchdule();
                ts5.CallBackTBJ2(5);
                finished(5);
            });
            thread5.Start();
            Thread thread6 = new Thread(() =>
              {
                  TBJDataSchdule ts6 = new TBJDataSchdule();
                  ts6.CallBackTBJ2(6);
                  finished(6);
              });
            thread6.Start();
            Thread thread7 = new Thread(() =>
              {
                  TBJDataSchdule ts7 = new TBJDataSchdule();
                  ts7.CallBackTBJ2(7);
                 finished(7);});
            thread7.Start();
            Thread thread8 = new Thread(() =>
              {
                  TBJDataSchdule ts8 = new TBJDataSchdule();
                  ts8.CallBackTBJ2(8);
                  finished(8);
              });
            thread8.Start();
           // var date2 = System.DateTime.Now;

           
        }

        List<int> paclist = new List<int>();
        public void finished(int pac)
        {
            paclist.Add(pac);

            if (paclist.Count==8)
            {
                var date2 = System.DateTime.Now;
                MessageBox.Show("贴标机数据生成成功!\r\n耗时：" + Math.Ceiling((date2 - date1).TotalSeconds) + " 秒");
                InBound.WriteLog.GetLog().Write("贴标机数据生成成功!\r\n耗时：" + Math.Ceiling((date2 - date1).TotalSeconds) + " 秒");
                updateControl(button_TBJ, true);
            }
            else
            {
       
            }
        }


        PackageService ps = new PackageService();
        TBJDataSchdule ts = new TBJDataSchdule();
        delegate void HandlePackageSort();
        delegate void HandleCallbackSort();
        private void button_datacomplte_Click(object sender, EventArgs e)
        {
            if (list.Count <= 0)
            {
                MessageBox.Show("请选择需要排程的数据！");
                return;
            }
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
            InBound.WriteLog.GetLog().Write("按\r\n" + str + "\r\n顺序生成包装机数据");
            button_query.Enabled = false;
            button_all.Enabled = false;
            orderdata.Enabled = false;
            panel3.Visible = true;
            label1.Text = "包装机数据生成中......";
            HandlePackageSort task = PackageSort; //新的
            task.BeginInvoke(null, null); 
        }
        private delegate void HandleDelegate1(Control control, bool isvisible, bool isenable);
        private delegate void HandleDelegate2(Control control, bool isenable);

        public void updateControl(Control control, bool isvisible, bool isenable)
        {

            if (control.InvokeRequired)
            {
                control.Invoke(new HandleDelegate1(updateControl), new Object[] { control, isvisible, isenable });
            }
            else
            {

                label1.Text = "数据生成完成\r\n重新加载批次数据中...";
                dataget();
                button_query.Enabled = true;
                button_all.Enabled = true;
                panel3.Visible = false;
                control.Visible = isvisible;
                control.Enabled = isenable;
            }
        }
        public void updateControl(Control control, bool isenable)
        {

            if (control.InvokeRequired)
            {
                control.Invoke(new HandleDelegate2(updateControl), new Object[] { control, isenable });
            }
            else
            {
                panel3.Visible = false;
                control.Enabled = isenable;
            }
        }
        private void button_TBJ_Click(object sender, EventArgs e)
        {
            button_TBJ.Enabled = false;
            panel3.Visible = true;
            label1.Text = "贴标机数据生成中......";
            HandleCallbackSort task = PackageCallback;
            task.BeginInvoke(null, null); 
        }

        private void button_CheckData_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ts.CheckData());
        }
    }
}
