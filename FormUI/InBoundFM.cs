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
using System.Transactions;
namespace FormUI
{
    public partial class InBoundFM : Form
    {
        public InBoundFM()
        {
            InitializeComponent();
        }

        private void InBoundFM_Load(object sender, EventArgs e)
        {
            searchTask();
        }
        private void search()
        {
            List<InBound.T_WMS_ITEM> list = ItemService.GetItem(tbCode.Text, tbName.Text);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = list;
           
        }
        private void searchTask()
        {
           

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
                tbChooseName.Tag = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }
        }
        List<String> address = new List<string>() { "1221", "1231", "1415" };
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
        private void button1_Click(object sender, EventArgs e)
        {


            if (cbAddress.SelectedIndex == -1)
            {
                MessageBox.Show("请选择入口地址");
                return;
            }
            if (tbChooseName.Tag == null)
            {
                MessageBox.Show("请选择品牌");
                return;
            }
            if (tbRfid.Text == "")
            {
                MessageBox.Show("请输入Rfid");
                return;
            }
            INF_JOBDOWNLOAD job = new INF_JOBDOWNLOAD();
         
            job.SOURCE = address[cbAddress.SelectedIndex];
            job.TARGET = AtsCellInService.getCellNoCode(tbChooseName.Tag.ToString());//储位地址
            if (job.TARGET == "")
            {
                MessageBox.Show("获取储位信息失败");
                return;
            }
            job.PLANQTY = int.Parse(tbNum.Text);
            job.JOBTYPE =30;//成品入库
            job.PRIORITY = 50;
            job.CREATEDATE = DateTime.Now;
            job.BRANDID = tbChooseName.Tag.ToString();
            job.TUTYPE = 4;
            job.INPUTTYPE = 10;
            String palletNo = RefRFIDPalletService.GetPallet(tbRfid.Text);
            job.BARCODE = palletNo;
            if (palletNo == "E")
            {
                MessageBox.Show("该托盘已在使用,请确认Rfid是否输入正确");
                return;
            }
            if (cbcDuo.Checked)
            {
                job.CDTYPE = 10;
            }
            else
            {
                job.CDTYPE = 0;
            }

          //  InfJobDownLoadService.InsertEntity(job);
            using (TransactionScope ts = new TransactionScope())
            {
                InfJobDownLoadService.InsertEntity(job);

                //InBoundLineService.Update(entity.INBOUNDDETAILID, 0, num ?? 0);
                AtsCellService.UpdateAtsCell(job.TARGET, 30);//更新cellno状态
                T_WMS_ATSCELLINFO info = new T_WMS_ATSCELLINFO();
                info.PALLETNO = palletNo;
                info.DISMANTLE = 1;
                info.CELLNO = job.TARGET;
                info.STATUS = 10;//组盘
                info.CREATETIME = DateTime.Now;
                //info.INBOUNDID = inboundid;
                if (cbcDuo.Checked)
                {
                    info.DISMANTLE = 10;
                }
                else
                {
                    info.DISMANTLE = 0;
                }
                AtsCellInfoService.InsertAtsCellInfo(info);

                T_WMS_ATSCELLINFO_DETAIL detail = new T_WMS_ATSCELLINFO_DETAIL();
                detail.BARCODE = tbChooseName.Tag.ToString();
                detail.CIGARETTECODE = ItemService.GetItemByBarCode(detail.BARCODE).ITEMNO;
                detail.CIGARETTENAME = tbChooseName.Text;
                detail.QTY = decimal.Parse(tbNum.Text);
                detail.CELLNO = info.CELLNO;
               
                AtsCellInfoDetailService.InsertAtsCellInfo(detail);
                ts.Complete();
            }
            //MessageBox.Show("入库完成");

            //InfJobDownLoadService.InsertEntity(job);
            //using (TransactionScope ts = new TransactionScope())
            //{
            //    InfJobDownLoadService.InsertEntity(job);
               
            //    //InBoundLineService.Update(entity.INBOUNDDETAILID, 0, num ?? 0);
            //    AtsCellService.UpdateAtsCell(job.TARGET,30);//更新cellno状态
            //    T_WMS_ATSCELLINFO info = new T_WMS_ATSCELLINFO();
            //    info.PALLETNO = palletNo;
            //    info.DISMANTLE = 1;
            //    info.CELLNO = job.TARGET;
            //    info.STATUS = 10;//组盘
            //    info.CREATETIME = DateTime.Now;
            //    //info.INBOUNDID = inboundid;
            //    if (cbcDuo.Checked)
            //    {
            //        info.DISMANTLE = 10;
            //    }
            //    else
            //    {
            //        info.DISMANTLE = 0;
            //    }
            //    AtsCellInfoService.InsertAtsCellInfo(info);

            //    T_WMS_ATSCELLINFO_DETAIL detail = new T_WMS_ATSCELLINFO_DETAIL();
            //    detail.CIGARETTECODE = tbChooseName.Tag.ToString();
            //    detail.CIGARETTENAME = tbChooseName.Text;
            //    detail.QTY = decimal.Parse(tbNum.Text);
            //    detail.PALLETNO = info.PALLETNO;
            //    AtsCellInfoDetailService.InsertAtsCellInfo(detail);
            //    ts.Complete();
            //}
            MessageBox.Show("任务已下达");
            updateListBox(job.JOBID + "号任务已下达;入口地址:" + job.SOURCE);
            WriteLog.GetLog().Write(job.JOBID + "号任务已下达;入口地址:" + job.SOURCE);
            tbChooseName.Text = "";
            tbChooseName.Tag = null;
          
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                int value;

                e.Value = "正在入库......";
             
            }

        }

    
      

     

       

       

       

       
        
    }
}
