using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound.Business;
using InBound;
using System.Transactions;

namespace FormUI
{
    public partial class ManualForm : Form
    {
        public ManualForm()
        {
            InitializeComponent();
        }
        InBound.INF_JOBDOWNLOAD load = null;
        List<InBound.INF_JOBDOWNLOAD> list = new List<InBound.INF_JOBDOWNLOAD>();
        public void search()
        {

            
            dataGridView1.AutoGenerateColumns = false;
            
            load = InfJobDownLoadService.QueryManual();
            if (load != null)
            {
                list.Add(load);
            }
            dataGridView1.DataSource =list;
            
        }
        private void QueryForm_Load(object sender, EventArgs e)
        {
            search();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            search();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (jobid != null && jobid != "0")
            {
                //InBound.INF_JOBFEEDBACK request = new InBound.INF_JOBFEEDBACK();
                ////request.ID = Guid.NewGuid().ToString("N");
                //request.JOBID = jobid;
                //request.FEEDBACKSTATUS = 99;
                //request.ERRORCODE = "OK";
                //request.ENTERDATE = DateTime.Now;
                ////  request.STATUS = 0;
                //InfFeedBackService.InsertEntity(request);


                //T_WMS_ATSCELLINFO cellInfo = AtsCellInfoService.CheckPalletExist(temptask.BARCODE);//检查托盘号是否存在
                //T_WMS_ATSCELLINFO_DETAIL detail = AtsCellInfoDetailService.GetDetail(cellInfo.CELLNO);
                //if (detail.REQUESTQTY == detail.QTY) //&& (cellInfo.DISMANTLE==0)
                //{
                //    INF_JOBDOWNLOAD task1 = new INF_JOBDOWNLOAD();
                //    task1.JOBID = dataEntity.ExecuteStoreQuery<decimal>("select s_inf_jobdownload.nextval from dual").First() + "";
                //    task1.ID = task1.JOBID;
                //    task1.BRANDID = temptask.BRANDID;
                //    task1.BARCODE = cellInfo.PALLETNO;
                //    task1.SOURCE = temptask.EQUIPMENTID;
                //    task1.PLANQTY = 1;
                //    task1.INPUTTYPE = 10;
                //    task1.JOBTYPE = 100;//空托盘回收任务
                //    task1.TARGET = "1422";//空托盘指定地址
                //    task1.TUTYPE = 2;
                //    task1.CREATEDATE = DateTime.Now;
                //    task1.PRIORITY = 50;
                //    dataEntity.INF_JOBDOWNLOAD.AddObject(task1);

                //}
               // INF_EQUIPMENTREQUEST request = InfEquipmentRequestService.GetEquipMentRequest(jobid);
                INF_JOBDOWNLOAD download = InfJobDownLoadService.GetDetail(jobid);
                 T_WMS_ATSCELLINFO cellInfo = AtsCellInfoService.GetCellInfoByBarCode(download.BARCODE);
                T_WMS_ATSCELLINFO_DETAIL detail = AtsCellInfoDetailService.GetDetail(cellInfo.CELLNO);
               
                using (TransactionScope ts = new TransactionScope())
                {
                    if (detail.REQUESTQTY != detail.QTY)
                    {

                        INF_JOBDOWNLOAD task1 = new INF_JOBDOWNLOAD();
                        // task1.JOBID = dataEntity.ExecuteStoreQuery<decimal>("select s_inf_jobdownload.nextval from dual").First() + "";
                        // task1.ID = task.JOBID;
                        task1.BRANDID = detail.BARCODE;
                        task1.BARCODE = cellInfo.PALLETNO;
                        task1.SOURCE = "1415";
                        task1.PLANQTY = detail.QTY - detail.REQUESTQTY;
                        task1.INPUTTYPE = 10;
                        task1.JOBTYPE = 40;//返库任务
                        task1.TARGET = AtsCellInService.getCellNoCode(download.BRANDID + "");
                        task1.TUTYPE = 4;
                        task1.PRIORITY = 50;
                        task1.STATUS = 0;
                        task1.CREATEDATE = DateTime.Now;
                        InfJobDownLoadService.InsertEntity(task1);

                        T_WMS_ATSCELLINFO info = new T_WMS_ATSCELLINFO();
                        info.PALLETNO = cellInfo.PALLETNO;
                        // info.DISMANTLE = 1;
                        info.CELLNO = task1.TARGET;
                        info.STATUS = 10;//组盘
                        info.CREATETIME = cellInfo.CREATETIME;
                        info.INBOUNDID = cellInfo.INBOUNDID;
                        //task.TUTYPE = 4;
                        info.DISMANTLE = 0;

                        AtsCellInfoService.InsertAtsCellInfo(info);

                        T_WMS_ATSCELLINFO_DETAIL details = new T_WMS_ATSCELLINFO_DETAIL();
                        details.BARCODE = detail.BARCODE;
                        T_WMS_ITEM item = ItemService.GetItemByBarCode(details.BARCODE);
                        details.CIGARETTECODE = item.ITEMNO;
                        details.CIGARETTENAME = item.ITEMNAME;
                        details.QTY = task1.PLANQTY;
                        details.CELLNO = info.CELLNO;
                        AtsCellInfoDetailService.InsertAtsCellInfo(details);
                    }
                    AtsCellService.UpdateAtsCell(detail.CELLNO, 10);
                    AtsCellInfoService.delete(detail.CELLNO);
                    AtsCellInfoDetailService.delete(detail.CELLNO);
                    InfJobDownLoadService.UpdateJopDownLoad(jobid, 3);
                  //  InfEquipmentRequestService.UpdateEquipMentRequest(jobid, 1);
                    ts.Complete();
                   
                }
                search();
            }
            else
            {
                MessageBox.Show("请选择记录");
            }
        }
        String jobid = "0";
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count > 0)
            {

                jobid = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
               
            }
        }



    }
}
