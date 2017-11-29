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
    public partial class AutoInBoundFM : Form
    {
        public AutoInBoundFM()
        {
            InitializeComponent();
        }
        List<String> address = new List<string>() { "1001", "1011", "1021" };
        private void InBoundFM_Load(object sender, EventArgs e)
        {
            searchTask();
        }
        
       
        private void search()
        {
           List<InBound.T_WMS_INBOUND_LINE> itemlist = InBoundLineService.GetItem(tbCode.Text, tbName.Text);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = itemlist;
           
        }

        delegate void gv();
        private void searchTask()
        {
            List<InBound.T_WMS_STORAGEAREA_INOUT> list = StroageInOutService.GetDetail(20, 1, 10);
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

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count>0)
            {
                tbChooseName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                List<String> list = new List<String>();
                list.Add(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                list.Add(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
                tbChooseName.Tag =list;
                tbNum.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CBAddress.SelectedIndex == -1)
            {
                MessageBox.Show("请选择一个正确的入口地址");
            }
            else if (dataGridView1.SelectedRows == null || tbChooseName.Tag==null)
            {
                MessageBox.Show("请选择出库品牌");
            }
            else
            {
                Thread thread = new Thread(new ThreadStart(startAutoInBound));
                thread.Start();
            }
        }
       delegate void UpdateTextBox(String text);
       public void Uptb(String text)
       {
           tbCode.Text = text;
       }

        bool isRun = true;
        public void startAutoInBound()
        {

            sendTask();
           // 发送堆垛机命令
            while (isRun)
            {
                checkProcess();
                Thread.Sleep(2000);
            }
         
        }
        int i = 0;
        // 发送堆垛机命令
        public void sendTask()
        {
            //tbCode.BeginInvoke(new UpdateTextBox(Uptb), i+"");
            //i++;

            T_WMS_INBOUND_LINE entity = InBoundLineService.GetItemByID(int.Parse(((List<String>)tbChooseName.Tag)[1].ToString()));
            decimal? num = 0;
            num = decimal.Parse(tbNum.Text);
            INF_JOBDOWNLOAD job = new INF_JOBDOWNLOAD();
            job.SOURCE = address[CBAddress.SelectedIndex];
            job.PLANQTY = num;
            job.JOBTYPE = 1;//入库任务  
            job.PRIORITY = 50;
            job.CREATEDATE = DateTime.Now;
            job.BRANDID = ((List<String>)tbChooseName.Tag)[0].ToString();
            job.TUTYPE = 1;
            using (TransactionScope ts = new TransactionScope())
            {
                InfJobDownLoadService.InsertEntity(job);
                InBoundLineService.Update(entity.INBOUNDDETAILID,  0,num??0);
                ts.Complete();
            }
          //if (entity.BOXQTY - entity.ABOXQTY > 30)
          //{
          //    num = 30;
          //}
          //else
          //{
          //    num = entity.BOXQTY - entity.ABOXQTY;
          //}

          //T_WMS_INOUTBOUND_TASK task = new T_WMS_INOUTBOUND_TASK();
          //task.SOURCEADD = "";
          //task.TARGETADD = "";
          //task.TASKTYPE = 10;
          //task.CIGARETTECODE = entity.CIGARETTECODE;
          //task.CIGARETTENAME = entity.CIGARETTENAME;
          //task.QTY = num;
          //task.INBOUNDLINEID = entity.INBOUNDDETAILID;
          //using (TransactionScope ts = new TransactionScope())
          //{
          //    InOutBoundTaskService.Insert(task);
          //    InBoundLineService.Update(entity.INBOUNDDETAILID, num ?? 0);
          //    ts.Complete();
          //}
            this.BeginInvoke(new SearchHanlder(search));
            searchTask();
        }
delegate void SearchHanlder();

        public void checkProcess()
        { 
        }
        public void insertMoveAction()
        {
            T_WMS_STORAGEAREA_INOUT entity = new T_WMS_STORAGEAREA_INOUT();
            entity.BOXQTY = int.Parse(tbNum.Text);
            entity.CIGARETTECODE = ((List<String>)tbChooseName.Tag)[0];
            entity.CIGARETTENAME = tbChooseName.Text.ToString();
            entity.AREAID = 1;
            entity.INOUTTYPE = 20;
            StroageInOutService.InsertEntity(entity);
            //searchTask();
        }
        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                

                e.Value = "正在入库......";
             
            }

        }

        private void AutoInBoundFM_FormClosed(object sender, FormClosedEventArgs e)
        {
            isRun = false;
        }

    
      

     

       

       

       

       
        
    }
}
