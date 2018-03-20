using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound;
using InBound.Business;
using System.Threading;
using System.Transactions;
namespace FormUI
{
    public partial class InBoundNoFM : Form
    {
        public InBoundNoFM()
        {
            InitializeComponent();
        }
        List<String> address = new List<string>() { "1001", "1011", "1021" };
        private void InBoundFM_Load(object sender, EventArgs e)
        {

            search();
            searchTask();
        }
        
       
        private void search()
        {
            if (dateTimePicker1.Value.Date <= dateTimePicker2.Value.Date)
            {
                List<InBound.T_WMS_INBOUND> itemlist = InBoundService.GetItem(dateTimePicker1.Value.Date, dateTimePicker2.Value.Date.AddDays(1));
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = itemlist;
                if (itemlist != null && itemlist.Count > 0)
                {
                    inboundid = itemlist[0].INBOUNDID;
                }
            }
            else
            {
                MessageBox.Show("开始时间必须小于结束时间");
            }
           
        }

        delegate void gv();
        private void initText()
        {
            if (tbChooseName.InvokeRequired)
            {
                tbChooseName.BeginInvoke(new gv(initText
                    ));
            }
            else
            {
                tbChooseName.Text = "";
                tbChooseName.Tag = null;
            }
            if (tbNum.InvokeRequired)
            {
                tbNum.BeginInvoke(new gv(initText
                   ));
            }
            else
            {
                tbNum.Text = "";
            }
        }
        private void searchTask()
        {
            List<InBound.T_WMS_INBOUND_LINE> list = InBoundLineService.GetItem(inboundid);
            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.BeginInvoke( new gv(searchTask));
            }
            else
            {
                dataGridView2.AutoGenerateColumns = false;
                dataGridView2.DataSource = list;
            }

        }
        private void tbCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                search();
            }
        }
        decimal inboundid = 0;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count>0)
            {
                inboundid = decimal.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                searchTask();
                //tbChooseName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                //List<String> list = new List<String>();
                //list.Add(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                //list.Add(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
                //tbChooseName.Tag =list;
                //tbNum.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CBAddress.SelectedIndex == -1)
            {
                MessageBox.Show("请选择一个正确的入口地址");
            }
            else if (tbChooseName.Tag== null )
            {
                MessageBox.Show("请选择相关品牌");
            }
            
            else
            {

                startAutoInBound();
                //Thread thread = new Thread(new ThreadStart(startAutoInBound));
                //thread.Start();
            }
        }
       delegate void UpdateTextBox(String text);
       public void Uptb(String text)
       {
           tbChooseName.Text = text;
       }
       delegate void HandleDelegate(String text);
       public void updateListBox(string info)
       {
           String time = DateTime.Now.ToLongTimeString();
           if (this.logList.InvokeRequired)
           {


               this.logList.Invoke(new HandleDelegate(updateListBox), info);
           }
           else
           {
               this.logList.Items.Insert(0, time + "    " + info);

           }
       }

        bool isRun = true;
        public void startAutoInBound()
        {

            sendTask();
           // 发送堆垛机命令
            //while (isRun)
            //{
            //    checkProcess();
            //    Thread.Sleep(2000);
            //}
         
        }
        int i = 0;
      public static  int selectIndex = 0;
        delegate void getIndex();
        void getCBSelectIndex()
        {
                selectIndex= CBAddress.SelectedIndex;
         
        }
        // 发送堆垛机命令
        public void sendTask()
        {
            //List<T_WMS_INBOUND_LINE> list = InBoundLineService.GetItem(inboundid);
            //if (list != null && list.Count > 0)
            //{
            //    using (TransactionScope ts = new TransactionScope())
            //    {
            //        foreach (var item in list)
            //        {
            //            INF_JOBDOWNLOAD job = new INF_JOBDOWNLOAD();
            //            CBAddress.BeginInvoke(new getIndex(getCBSelectIndex));
            //            job.SOURCE = address[selectIndex];
            //            job.PLANQTY = item.BOXQTY;
            //            job.JOBTYPE = 1;//入库任务
            //            job.PRIORITY = 50;
            //            job.CREATEDATE = DateTime.Now;
            //            job.BRANDID = decimal.Parse(item.CIGARETTECODE);
            //            job.TUTYPE = 1;

            //            InfJobDownLoadService.InsertEntity(job);
            //            InBoundLineService.Update(item.INBOUNDDETAILID, 0, item.BOXQTY??0);
            //        }
            //        InBoundService.Update(inboundid, "20");
            //        ts.Complete();
            //    }
            //}
            T_WMS_INBOUND_LINE entity = InBoundLineService.GetItemByID(int.Parse(((List<String>)tbChooseName.Tag)[0].ToString()));
            decimal? num = 0;
            num = decimal.Parse(tbNum.Text);
            if (num > (entity.BOXQTY-entity.LOCKQTY))
            {
                MessageBox.Show("任务数量超出入库单数量,请修正.");
                return;
            }
            
            INF_JOBDOWNLOAD job = new INF_JOBDOWNLOAD();
            T_WMS_ITEM item=ItemService.GetItemByBarCode(((List<String>)tbChooseName.Tag)[1].ToString());
            CBAddress.BeginInvoke(new getIndex(getCBSelectIndex));
            job.SOURCE = address[CBAddress.SelectedIndex];
            job.TARGET = "A1";
            job.PLANQTY = num;
            job.JOBTYPE = 10;//码垛任务
            job.PRIORITY = 50;
            job.CREATEDATE = DateTime.Now;
            job.FULLCOUNT = item.FULLCOUNT;
            job.PILETYPE = decimal.Parse(item.DXTYPE);
            job.BRANDID = ((List<String>)tbChooseName.Tag)[1].ToString();
            job.TUTYPE = 1;//件箱
            job.INPUTTYPE = 10;
            job.INBOUNDNO = int.Parse(((List<String>)tbChooseName.Tag)[0].ToString());
            if (InfJobDownLoadService.CheckJobExist(job.INBOUNDNO??0))
            {
                MessageBox.Show("任务已存在");
                return;
            }
            using (TransactionScope ts = new TransactionScope())
            {
                InfJobDownLoadService.InsertEntity(job);
                ts.Complete();
            }
            this.BeginInvoke(new SearchHanlder(search));
            searchTask();
            initText();
            MessageBox.Show("任务已下达");
            updateListBox(job.JOBID + "号任务已下达;入口地址:"+job.SOURCE);
            WriteLog.GetLog().Write(job.JOBID + "号任务已下达;入口地址:" + job.SOURCE);
        }
        delegate void SearchHanlder();

        public void checkProcess()
        { 
        }
        public void insertMoveAction()
        {
            //T_WMS_STORAGEAREA_INOUT entity = new T_WMS_STORAGEAREA_INOUT();
            //entity.BOXQTY = int.Parse(tbNum.Text);
            //entity.CIGARETTECODE = ((List<String>)tbChooseName.Tag)[0];
            //entity.CIGARETTENAME = tbChooseName.Text.ToString();
            //entity.AREAID = 1;
            //entity.INOUTTYPE = 20;
            //StroageInOutService.InsertEntity(entity);
            //searchTask();
        }
        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
        }

        private void AutoInBoundFM_FormClosed(object sender, FormClosedEventArgs e)
        {
            isRun = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            search();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            searchTask();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows != null && dataGridView2.SelectedRows.Count > 0)
            {
               
                tbChooseName.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                List<String> list = new List<String>();
                list.Add(dataGridView2.SelectedRows[0].Cells[6].Value.ToString());
                list.Add(dataGridView2.SelectedRows[0].Cells[2].Value.ToString());
                tbChooseName.Tag = list;
                tbNum.Text = int.Parse(dataGridView2.SelectedRows[0].Cells[3].Value.ToString()) - int.Parse(dataGridView2.SelectedRows[0].Cells[4].Value.ToString())+"";

            }
        }

        private void CBAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
         //   int i = 0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            searchTask();
        }

    }
}
