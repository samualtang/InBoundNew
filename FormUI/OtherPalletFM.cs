﻿using System;
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
    public partial class OtherPalletFM : Form
    {
        public OtherPalletFM()
        {
            InitializeComponent();
        }
        List<String> address = new List<string>() { "1222", "1232", "1217","1415" };
        private void InBoundFM_Load(object sender, EventArgs e)
        {
           
            search();
            searchTask();
        }
        
       
        private void search()
        {
            if (dateTimePicker1.Value.Date <= dateTimePicker2.Value.Date)
            {
                List<InBound.T_WMS_INBOUND> itemlist = InBoundService.GetItem(dateTimePicker1.Value.Date, dateTimePicker2.Value.Date);
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
            //if (tbNum.InvokeRequired)
            //{
            //    tbNum.BeginInvoke(new gv(initText
            //       ));
            //}
            //else
            //{
            //    tbNum.Text = "";
            //}
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

           
            if (tbChooseName.Tag== null )
            {
                MessageBox.Show("请选择相关品牌");
            }
            else if (tbRfid.Text.Equals(""))
            {
                MessageBox.Show("请填入正确的RFID");
            }
            else
            {

                int lockNum =int.Parse( dataGridView2.SelectedRows[0].Cells[4].Value.ToString());
                int platNum = int.Parse(dataGridView2.SelectedRows[0].Cells[3].Value.ToString());
                if (platNum - (lockNum + int.Parse(tbNum.Text)) < 0)
                {
                    MessageBox.Show("任务数量超出入库单数量,请修正.");
                }
                else
                {
                    Thread thread = new Thread(new ThreadStart(startAutoInBound));
                    thread.Start();
                }
            }
        }
       delegate void UpdateTextBox(String text);
       public void Uptb(String text)
       {
           tbChooseName.Text = text;
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
        int selectIndex = 0;
        delegate void getIndex();
        //void getCBSelectIndex()
        //{

           
        //        selectIndex= CBAddress.SelectedIndex;
         
        //}
        // 发送堆垛机命令

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
            // if (num > (entity.BOXQTY - entity.LOCKQTY))
            //{
            //    MessageBox.Show("任务数量超出入库单数量,请修正.");
            //    return;
            //}
            INF_JOBDOWNLOAD job = new INF_JOBDOWNLOAD();
           // CBAddress.BeginInvoke(new getIndex(getCBSelectIndex));
            T_WMS_ITEM item = ItemService.GetItemByBarCode(((List<String>)tbChooseName.Tag)[0].ToString());
            job.SOURCE = "1217";// address[selectIndex];
            job.FULLCOUNT = item.FULLCOUNT;
            job.PILETYPE =decimal.Parse( item.DXTYPE);
            job.TARGET = AtsCellInService.getCellNo(((List<String>)tbChooseName.Tag)[0].ToString());//储位地址
            if (job.TARGET == "")
            {
                MessageBox.Show("获取储位信息失败");
                return;
            }
            job.PLANQTY = num;
            job.JOBTYPE = 20;//入库任务
            job.PRIORITY = 50;
            job.CREATEDATE = DateTime.Now;
            job.BRANDID = ((List<String>)tbChooseName.Tag)[0].ToString();
            job.TUTYPE = 4;
            job.INPUTTYPE = 10;
            job.INBOUNDNO = int.Parse(((List<String>)tbChooseName.Tag)[1].ToString());
            String palletNo = RefRFIDPalletService.GetPallet(tbRfid.Text);
            if (palletNo == "E")
            {
                MessageBox.Show("该托盘已在使用,请确认Rfid是否输入正确");
                return;
            }
            job.BARCODE = palletNo;
            if (cbcDuo.Checked)
                {
                    job.CDTYPE=10;
                }
                else
                {
                    job.CDTYPE= 0;
                }
            InfJobDownLoadService.InsertEntity(job); //插入任务
            InBoundLineService.Update(job.INBOUNDNO??0, 0, job.PLANQTY??0);
            T_WMS_ATSCELLINFO info = new T_WMS_ATSCELLINFO();
            info.PALLETNO = job.BARCODE;
            // info.DISMANTLE = 1;
            info.CELLNO = job.TARGET;
            info.STATUS = 10;//上架
            info.CREATETIME = DateTime.Now;
            info.INBOUNDID = job.INBOUNDNO;

            info.DISMANTLE = 10;

            AtsCellInfoService.InsertAtsCellInfo(info);

            T_WMS_ATSCELLINFO_DETAIL detail = new T_WMS_ATSCELLINFO_DETAIL();
            detail.CIGARETTECODE = ItemService.GetItemByBarCode(job.BRANDID).ITEMNO;
            detail.CIGARETTENAME = ItemService.GetItemByBarCode(job.BRANDID).ITEMNAME;
            detail.BARCODE = job.BRANDID;
            detail.QTY = job.PLANQTY;
            detail.CELLNO = info.CELLNO;
            AtsCellInfoDetailService.InsertAtsCellInfo(detail);
            MessageBox.Show("任务已下达");
            WriteLog.GetLog().Write(job.JOBID + "号任务已下达;入口地址:" + job.SOURCE);
            searchTask();
            initText();
        }
delegate void SearchHanlder();

        public void checkProcess()
        { 
        }
        public void insertMoveAction()
        {
          //  T_WMS_STORAGEAREA_INOUT entity = new T_WMS_STORAGEAREA_INOUT();
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
                list.Add(dataGridView2.SelectedRows[0].Cells[2].Value.ToString());
                list.Add(dataGridView2.SelectedRows[0].Cells[6].Value.ToString());
               
                tbChooseName.Tag = list;
               // tbNum.Text = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();

            }
        }

       
    
      

     

       

       

       

       
        
    }
}
