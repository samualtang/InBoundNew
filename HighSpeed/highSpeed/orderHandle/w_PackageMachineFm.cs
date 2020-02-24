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
        List<PackageSeq> list = new List<PackageSeq>(); 
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
            PackageSeq txt = new PackageSeq();
            if (e.ColumnIndex == 0)
            {
                if (orderdata.RowCount > 0)
                {
                    bool obj = (bool)this.orderdata.CurrentRow.Cells[0].EditedFormattedValue;
                    txt.synseqNO = Convert.ToInt32(this.orderdata.CurrentRow.Cells[1].Value.ToString());
                    txt.packageNO = Convert.ToInt32(this.orderdata.CurrentRow.Cells[2].Value.ToString());
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
                            if (list[i].packageNO == txt.packageNO && list[i].synseqNO == txt.synseqNO)
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
                PackageSeq txt = new PackageSeq();
                orderdata.Rows[i].Cells[0].Value = "true";

                txt.synseqNO = Convert.ToInt32(orderdata.Rows[i].Cells[1].Value.ToString());
                txt.packageNO = Convert.ToInt32(orderdata.Rows[i].Cells[2].Value.ToString());
                list.Add(txt);
            }
        }
        void PackageSort()
        {
            packagelist.Clear();
            dateps = System.DateTime.Now;
            var packageNo = list.Select(x => x.packageNO).Distinct().ToList();
            List<int> dd = packageNo;

            Thread thread1 = new Thread(new ThreadStart(delegate()
            {
                GetPackageResult(1, 1);
            }));
            thread1.Start();
            Thread thread2 = new Thread(new ThreadStart(delegate()
            {
                GetPackageResult(2, 2);
            }));
            thread2.Start();
            Thread thread3 = new Thread(new ThreadStart(delegate()
            {
                GetPackageResult(3, 3);
            }));
            thread3.Start();
            Thread thread4 = new Thread(new ThreadStart(delegate()
            {
                GetPackageResult(4, 4);
            }));
            thread4.Start();
            Thread thread5 = new Thread(new ThreadStart(delegate()
            {
                GetPackageResult(5, 5);
            }));
            thread5.Start();
            Thread thread6 = new Thread(new ThreadStart(delegate()
            {
                GetPackageResult(6, 6);
            }));
            thread6.Start();
            Thread thread7 = new Thread(new ThreadStart(delegate()
            {
                GetPackageResult(7, 7);
            }));
            thread7.Start();
            Thread thread8 = new Thread(new ThreadStart(delegate()
            {
                GetPackageResult(8, 8);
            }));
            thread8.Start();

        }
        public int TopHight, PushLocation_1, PushLocation_2, PushLocation_3, PushLocation_4;
        /// <summary>
        /// 包装机数据生成
        /// </summary>
        /// <param name="tmpno">包装机号</param>
        /// <param name="threadno">线程号</param>
        private void GetPackageResult(int tmpno, int threadno)
        {
            var tmplist = list.Where(x => x.packageNO == tmpno).OrderBy(x => x.synseqNO).ToList();
            string text = "线程" + threadno + "包装机号" + tmpno + "\r\n";
            foreach (var item in tmplist)
            {
                PackageService p = new PackageService();
                p.packageTHeight = TopHight;
                if (tmpno == 1 || tmpno == 2)
                {
                    p.MaxnormalHight = PushLocation_1;
                }
                else if (tmpno == 3 || tmpno == 4)
                {
                    p.MaxnormalHight = PushLocation_2;
                }
                else if (tmpno == 5 || tmpno == 6)
                {
                    p.MaxnormalHight = PushLocation_3;
                }
                else if (tmpno == 7 || tmpno == 8)
                {
                    p.MaxnormalHight = PushLocation_4;
                }
                string tmptext = text + "批次：" + item.synseqNO + "包装机" + item.packageNO + "\r\n";
                p.GetAllOrder(Convert.ToDecimal(item.packageNO), Convert.ToDecimal(item.synseqNO));
                finishPackage(Convert.ToDecimal(item.packageNO));
                writeLog.Write(tmptext);
            }
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
        List<decimal> packagelist = new List<decimal>();

        public void finished(int pac)
        {
            paclist.Add(pac);

            if (paclist.Count==8)
            {
                var date2 = System.DateTime.Now;
                var date3 = System.DateTime.Now;
                UpPackageData.UpPackageSeq();
                var date4 = System.DateTime.Now;
                MessageBox.Show("贴标机数据生成成功!\r\n耗时：" + Math.Ceiling((date2 - date1).TotalSeconds) + " 秒\r\n订单拆分整理耗时" + Math.Ceiling((date4 - date3).TotalSeconds) + "秒");
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
