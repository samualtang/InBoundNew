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
    public partial class FOutBoundFM : Form
    {
        List<String> address = new List<string>() { "1222", "1232", "1412" };
        public FOutBoundFM()
        {
            InitializeComponent();
        }

        private void InBoundFM_Load(object sender, EventArgs e)
        {
           
        }
        private void search()
        {
            List<InBound.OutBound> list = AtsCellInfoDetailService.GetDetail(tbCode.Text, tbName.Text);
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
            try
            {
                if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count > 0)
                {
                    tbChooseName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    List<String> list = new List<string>();
                    list.Add(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
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
            INF_JOBDOWNLOAD job = new INF_JOBDOWNLOAD();

            job.TARGET = address[cbAdress.SelectedIndex];
            job.SOURCE =((List<String>)tbChooseName.Tag)[2].ToString();
            job.BARCODE = AtsCellInfoService.GetCellInfo(job.SOURCE).PALLETNO;
            job.PLANQTY = int.Parse(((List<String>)tbChooseName.Tag)[1].ToString());
            job.JOBTYPE = 50;//搬运任务
            job.PRIORITY = 50;
            job.CREATEDATE = DateTime.Now;
            job.BRANDID = ((List<String>)tbChooseName.Tag)[0].ToString();
            job.TUTYPE = 4;
            job.INPUTTYPE = 10;
            using (TransactionScope ts = new TransactionScope())
            {
                InfJobDownLoadService.InsertEntity(job);

                T_WMS_ATSCELL_CJ_HIS his = new T_WMS_ATSCELL_CJ_HIS();
                his.CELLNO = job.SOURCE;
                his.QTY = job.PLANQTY;
                his.INBOUNDTIME = AtsCellInfoService.GetCellInfo(his.CELLNO).CREATETIME;
                his.CIGARETTECODE = job.BRANDID;
                his.CIGARETTENAME = ItemService.GetItemByCode(his.CIGARETTECODE).ITEMNAME;
                his.INBOUNDID = AtsCellInfoService.GetCellInfo(his.CELLNO).INBOUNDID;
                his.BARCODE = ItemService.GetItemByCode(his.CIGARETTECODE).BIGBOX_BAR;
                AtsCellCJService.InsertEntity(his);
                AtsCellService.UpdateAtsCell(job.SOURCE,30);//更新cellno状态
                ts.Complete();
            }
            MessageBox.Show("出库完成");
        }

    }
}
