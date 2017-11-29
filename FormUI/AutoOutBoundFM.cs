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
    public partial class AutoOutBoundFM : Form
    {
        List<String> address = new List<string>() { "1222", "1232", "1412" };
        public AutoOutBoundFM()
        {
            InitializeComponent();
            cbAdress.SelectedIndex = 0;
        }

        private void InBoundFM_Load(object sender, EventArgs e)
        {
           
        }
        private void search()
        {
            List<InBound.T_WMS_ATSCELL_OUT> list = AtsCellOutService.getAutoList( tbCode.Text);
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
        decimal detailId = 0;

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
                    list.Add(dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                    detailId = decimal.Parse(dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                    tbChooseName.Tag = list;
                }
            }
            catch(Exception ex)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (tbChooseName.Tag == null)
            {
                MessageBox.Show("请选择出库品牌");
                return;
            }
            T_WMS_ATSCELL_OUT detail = AtsCellOutService.getDetailById(detailId);

                    INF_JOBDOWNLOAD job = new INF_JOBDOWNLOAD();
                    decimal id = BaseService.GetSeq("select S_ATSCELL_OUT.nextval from dual");
                    job.TARGET = address[cbAdress.SelectedIndex];
                    job.BARCODE = AtsCellInfoService.GetCellInfo(job.SOURCE).PALLETNO;//托盘号
                    job.PLANQTY = detail.REQUESTQTY;
                    job.SOURCE = AtsCellOutService.getCellNoBig(detail.CIGARETTECODE, int.Parse(detail.REQUESTQTY.ToString()));
                    job.JOBTYPE = 52;//
                    job.PRIORITY = 50;
                    job.CREATEDATE = DateTime.Now;
                    job.BRANDID = detail.BARCODE;
                    job.TUTYPE = 4;
                    job.INPUTTYPE = 10;
                    job.TASKNO = id;
                    using (TransactionScope ts = new TransactionScope())
                    {
                        InfJobDownLoadService.InsertEntity(job);

                        AtsCellOutService.updateDetailById(detailId);
                        ts.Complete();
                    }

                }
            
         
         
      }
}
