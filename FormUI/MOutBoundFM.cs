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
    public partial class MOutBoundFM : Form
    {
        List<String> address = new List<string>() { "1231", "1355" };
        public MOutBoundFM()
        {
            InitializeComponent();
        }

        private void InBoundFM_Load(object sender, EventArgs e)
        {
           
        }
        private void search()
        {
            List<InBound.OutBound> list = AtsCellInfoDetailService.GetGroupDetail(tbCode.Text, tbName.Text);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = list;
            List<InBound.OutBound> list1 = AtsCellInfoDetailService.GetDetail(tbCode.Text, tbName.Text);
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = list1;
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
            try
            {
                if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count > 0)
                {
                    tbChooseName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    List<String> list = new List<string>();
                    list.Add(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    list.Add(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                    list.Add(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
                    list.Add(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
                    tbChooseName.Tag = list;
                }
            }
            catch(Exception ex)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbAdress.SelectedIndex == -1)
            {
                MessageBox.Show("请选择出库口");
                return;
            }
            if (tbChooseName.Tag == null)
            {
                MessageBox.Show("请选择出库品牌");
                return;
            }
            if (cbType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择出库类型");
                return;
            }
            if (tbPlanQty.Text=="")
            {
                MessageBox.Show("请输入计划数量");
                return;
            }
            int planQty = int.Parse(tbPlanQty.Text);
         //   String barCode = ((List<String>)tbChooseName.Tag)[2].ToString();
            String cigaretteName = ((List<String>)tbChooseName.Tag)[1].ToString();
            String cigaretteCode=((List<String>)tbChooseName.Tag)[0].ToString();
            int totalQty = int.Parse(((List<String>)tbChooseName.Tag)[3].ToString());
            T_WMS_ITEM itemDetail = ItemService.GetItemByCode(cigaretteCode);
            if (planQty > totalQty)
            {
                MessageBox.Show("计划出库数量大于库存,请修改出库数量");
                return;
            }
            //10 调拨出库 20 抽检出库 30 补货出库 40 盘点出库 100 其他
            if (cbType.SelectedIndex != 0)//非调拨出库
            {
                if (planQty > itemDetail.FULLCOUNT)
                {
                    MessageBox.Show("计划数量不能大于满盘数量");
                    return;
                }
                else
                {

                    INF_JOBDOWNLOAD job = new INF_JOBDOWNLOAD();
                    decimal id = BaseService.GetSeq("select S_ATSCELL_OUT.nextval from dual");
                    job.TARGET = address[cbAdress.SelectedIndex];

                    if (cbType.SelectedIndex == 4)
                    {
                        job.SOURCE = ((List<String>)tbChooseName.Tag)[2].ToString();
                    AtsCellOutService.UpdateCellOutStatus(job.SOURCE, planQty);
                    }
                    else
                    {
                        if (cbType.SelectedIndex == 1)
                        {
                            job.SOURCE = AtsCellOutService.getCellNoBig(cigaretteCode, planQty);//先进先出
                        }
                        else
                        {
                            job.SOURCE = AtsCellOutService.getCellNoMath(cigaretteCode, planQty);//先进先出

                        }
                    }
                    if (job.SOURCE == "")
                    {
                        MessageBox.Show("下载任务失败,请修改任务");
                        return;
                    }
                    else
                    {
                     //   AtsCellOutService.UpdateCellOutStatus(job.SOURCE,planQty);
                    }
                    job.BARCODE = AtsCellInfoService.GetCellInfo(job.SOURCE).PALLETNO;//托盘号
                    job.PLANQTY = planQty;
                    job.JOBTYPE = 52;//
                    job.PRIORITY = 50;
                    job.CREATEDATE = DateTime.Now;
                    job.BRANDID = itemDetail.BIGBOX_BAR;
                    if (cbAdress.SelectedIndex == 1)
                    {
                        job.TUTYPE = 3;//二楼人工口
                    }
                    else
                    {
                        job.TUTYPE = 4;
                    }
                    job.INPUTTYPE = 10;
                    job.TASKNO = id;
                    job.TARGET = address[cbAdress.SelectedIndex];
                    using (TransactionScope ts = new TransactionScope())
                    {
                        InfJobDownLoadService.InsertEntity(job);

                        T_WMS_ATSCELL_OUT outcell = new T_WMS_ATSCELL_OUT();
                        outcell.REQUESTQTY = job.PLANQTY;
                        outcell.OUTTARGET = job.TARGET;
                        //10 调拨出库 20 抽检出库 30 补货出库 40 盘点出库 100 其他
                        if (cbType.SelectedIndex == 5)
                        {
                            outcell.OUTTYPE = 100;
                        }
                        else
                        {
                            outcell.OUTTYPE = (cbType.SelectedIndex + 1) * 10;
                        }
                        outcell.CREATETIME = DateTime.Now;
                        outcell.BARCODE = job.BRANDID;
                        outcell.CIGARETTECODE = cigaretteCode;
                        outcell.STATUS = 20;
                        outcell.CIGARETTENAME =cigaretteName;
                        outcell.ID = id;
                        outcell.RETURNQTY = 0;
                        outcell.LOCKRETURNQTY = 0;
                        outcell.ACTQTY = 0;
                        outcell.ACTRETURNQTY = 0;
                        AtsCellOutService.InsertObject(outcell);
                        ts.Complete();
                    }

                }
            }
            else
            {

                using (TransactionScope ts = new TransactionScope())
                {
                    decimal id = BaseService.GetSeq("select S_ATSCELL_OUT.nextval from dual");

                    T_WMS_ATSCELL_OUT outcell = new T_WMS_ATSCELL_OUT();
                    outcell.REQUESTQTY = planQty;
                    outcell.OUTTARGET = address[cbAdress.SelectedIndex];
                    outcell.OUTTYPE =  10;
                    outcell.STATUS = 20;
                    outcell.CREATETIME = DateTime.Now;
                    outcell.BARCODE = itemDetail.BIGBOX_BAR;
                    outcell.CIGARETTECODE = cigaretteCode;
                    outcell.CIGARETTENAME = cigaretteName;
                    outcell.RETURNQTY = 0;
                    outcell.LOCKRETURNQTY = 0;
                    outcell.ACTQTY = 0;
                    outcell.ACTRETURNQTY = 0;
                    outcell.ID = id;
                    AtsCellOutService.InsertObject(outcell);
                   
                    while (planQty > 0)
                    {
                        int tempQty = 0;
                        INF_JOBDOWNLOAD job = new INF_JOBDOWNLOAD();
                       
                        job.TARGET = address[cbAdress.SelectedIndex];
                        job.SOURCE = AtsCellOutService.getCellNoByTime(cigaretteCode);
                       
                        if (job.SOURCE == "")
                        {
                            MessageBox.Show("任务下载失败,请修改任务");
                            break;
                            
                        }
                        else
                        {
                            T_WMS_ATSCELLINFO_DETAIL detail = AtsCellInfoDetailService.GetDetail(job.SOURCE);
                            if (detail.QTY >= planQty)
                            {
                                AtsCellOutService.UpdateCellOutStatus(job.SOURCE, planQty);
                                tempQty = planQty;
                                planQty = 0;

                            }
                            else
                            {
                                AtsCellOutService.UpdateCellOutStatus(job.SOURCE, int.Parse(detail.QTY.ToString()));
                                planQty -= int.Parse(detail.QTY.ToString());
                                tempQty = int.Parse(detail.QTY.ToString());
                            }
                        }
                        job.BARCODE = AtsCellInfoService.GetCellInfo(job.SOURCE).PALLETNO;//托盘号
                        job.PLANQTY = tempQty;
                        job.JOBTYPE = 52;//一楼出库
                        job.PRIORITY = 50;
                        job.CREATEDATE = DateTime.Now;
                        job.BRANDID = itemDetail.BIGBOX_BAR;
                        job.TUTYPE = 4;
                        job.INPUTTYPE = 10;
                        job.TASKNO = id;
                        InfJobDownLoadService.InsertEntity(job);
                    }
                    ts.Complete();
                }
               
            }
            tbChooseName.Tag = null;
            tbChooseName.Text = "";
            search();
            MessageBox.Show("出库任务已下达");
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dataGridView1.Visible = false;
                dataGridView2.Visible = true;
                cbType.SelectedIndex = 4;
            }
            else
            {
                dataGridView1.Visible = true;
                dataGridView2.Visible = false;
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
         
            try
            {
                if (dataGridView2.SelectedRows != null && dataGridView2.SelectedRows.Count > 0)
                {
                    tbChooseName.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                    List<String> list = new List<string>();
                    list.Add(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                    list.Add(dataGridView2.SelectedRows[0].Cells[1].Value.ToString());
                    list.Add(dataGridView2.SelectedRows[0].Cells[2].Value.ToString());
                    list.Add(dataGridView2.SelectedRows[0].Cells[3].Value.ToString());
                    tbChooseName.Tag = list;
                }
            }
            catch(Exception ex)
            {
            }
        
        }

    }
}
