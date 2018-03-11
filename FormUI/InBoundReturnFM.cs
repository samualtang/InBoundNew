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
    public partial class InBoundReturnFM : Form
    {
        public InBoundReturnFM()
        {
            InitializeComponent();
        }

        private void InBoundFM_Load(object sender, EventArgs e)
        {
            search();
            cbOutType.SelectedIndex = 0;
        }
        List<InBound.T_WMS_ATSCELL_OUT> list;
        private void search()
        {
            int type = 10;
            if (cbOutType.SelectedIndex == 5)
            {
                type = 100;

            }
            else if (cbOutType.SelectedIndex >= 0)
            {
                type = (cbOutType.SelectedIndex + 1) * 10;
            }
            else
            {
                type = -1;
            }
            list = AtsCellOutService.getList(type, tbCode.Text);
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
        String cigaretteCode = "";
        decimal taskNo = 0;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count>0)
            {
                tbChooseName.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                tbChooseName.Tag = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                cigaretteCode = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                taskNo=decimal.Parse( dataGridView1.SelectedRows[0].Cells[8].Value.ToString());
                tbNum.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            }
        }
        List<String> address = new List<string>() { "1221", "1231", "1412" };
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
            decimal fullCount = ItemService.GetItemByCode(cigaretteCode).FULLCOUNT??0;
            if (decimal.Parse(tbNum.Text) > fullCount)
            {
                MessageBox.Show("入库数量不能大于满托盘数量");
                return;
            }
            using (TransactionScope ts = new TransactionScope())
            {
                INF_JOBDOWNLOAD job = new INF_JOBDOWNLOAD();

                job.SOURCE = address[cbAddress.SelectedIndex];
                job.TARGET = AtsCellInService.getCellNoCode(cigaretteCode);//储位地址
                if (job.TARGET == "")
                {
                    MessageBox.Show("获取储位信息失败,请联系管理员");
                    return;
                }
                job.PLANQTY = int.Parse(tbNum.Text);
                job.JOBTYPE = 42;//返库
                job.PRIORITY = 50;
                job.CREATEDATE = DateTime.Now;
                job.BRANDID = tbChooseName.Tag.ToString();
                job.TUTYPE = 4;
                job.INPUTTYPE = 10;
                job.TASKNO = taskNo;
                //job.EXTATTR1 = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                String palletNo = RefRFIDPalletService.GetPallet(tbRfid.Text);
                if (palletNo == "E")
                {
                    MessageBox.Show("该托盘已在使用,请确认Rfid是否输入正确");
                    return;
                }
                job.BARCODE = palletNo;
                if (cbcDuo.Checked)
                {
                    job.CDTYPE = 10;
                }
                else
                {
                    job.CDTYPE = 0;
                }
                InfJobDownLoadService.InsertEntity(job);
                AtsCellService.UpdateAtsCell(job.TARGET, 30);

                T_WMS_ATSCELLINFO info = new T_WMS_ATSCELLINFO();
                info.PALLETNO = job.BARCODE;


                info.CELLNO = job.TARGET;
                info.STATUS = 10;//组盘
                info.CREATETIME = DateTime.Now;
           
                info.DISMANTLE = job.CDTYPE;

                AtsCellInfoService.InsertAtsCellInfo(info);
                AtsCellOutService.UpdateObjectThd(taskNo, job.PLANQTY??0);
                T_WMS_ATSCELLINFO_DETAIL detail = new T_WMS_ATSCELLINFO_DETAIL();
                detail.BARCODE = job.BRANDID;

                T_WMS_ITEM item = ItemService.GetItemByBarCode(detail.BARCODE);

                detail.CIGARETTECODE = item.ITEMNO;
                detail.CIGARETTENAME = item.ITEMNAME;
                detail.QTY = job.PLANQTY;
                detail.CELLNO = info.CELLNO;
                AtsCellInfoDetailService.InsertAtsCellInfo(detail);
                ts.Complete();
                MessageBox.Show("入库任务下达完成");
              
            }
            search();
            tbChooseName.Text = "";
            tbChooseName.Tag = null;
          
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 6 && e.Value!=null)
            {
                if (e.Value.ToString() == "10")
                {
                    e.Value = "调拨出库";

                }
                else if (e.Value.ToString() == "20")
                {
                    e.Value = "抽检出库";
                }
                else if (e.Value.ToString() == "30")
                {
                    e.Value = "补货出库";
                }
                else if (e.Value.ToString() == "40")
                {
                    e.Value = "盘点出库";
                }
                else if (e.Value.ToString() == "100")
                {
                    e.Value = "其他出库";
                }
            }
        }

        private void cbOutType_SelectedIndexChanged(object sender, EventArgs e)
        {
            search();
        }

        bool ischeck = false;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)//如果单击列表头，全选．
            {
                if (ischeck)
                {
                    ischeck = false;
                }
                else
                { ischeck = true; }
                int i;
                for (i = 0; i < this.dataGridView1.RowCount; i++)
                {
                    this.dataGridView1.Rows[i].Cells[0].Value = ischeck;//如果为true则为选中,false未选中
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<int> indexList=new List<int>();
            int requestQty = 0;
            String code = "";
            for (var i = 0; i < this.dataGridView1.RowCount; i++)
            {
                if (this.dataGridView1.Rows[i].Cells[0].Value!=null && this.dataGridView1.Rows[i].Cells[0].Value.ToString().ToLower() == "true")
                {
                    indexList.Add(i);
                    if (code != "")
                    {
                        if (code != this.dataGridView1.Rows[i].Cells[1].Value.ToString())
                        {
                            MessageBox.Show("同一品牌才能合盘");
                            return;

                        }
                    }
                    else
                    {
                        code = this.dataGridView1.Rows[i].Cells[1].Value.ToString();
                    }
                    requestQty += int.Parse(this.dataGridView1.Rows[i].Cells[5].Value.ToString());
                  decimal fullcount=  ItemService.GetItemByBarCode(code).FULLCOUNT??0;
                  if (requestQty > fullcount)
                  {
                      MessageBox.Show("合盘后的返库数量不能大于满托盘数量");
                      return;
                  }
                }
            }
            if (indexList.Count > 0)
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    T_WMS_ATSCELL_OUT newEntity = null;
                    for (int i = 0; i < indexList.Count; i++)
                    {
                        T_WMS_ATSCELL_OUT temp = list[indexList[i]];
                        if (newEntity == null)
                        {
                            newEntity = new T_WMS_ATSCELL_OUT();
                            newEntity.ACTRETURNQTY = temp.ACTRETURNQTY;
                            newEntity.ACTQTY = temp.ACTQTY;
                            newEntity.BARCODE = temp.BARCODE;
                            newEntity.CIGARETTECODE = temp.CIGARETTECODE;
                            newEntity.CIGARETTENAME = temp.CIGARETTENAME;
                            newEntity.CREATETIME = DateTime.Now;
                            newEntity.RETURNQTY = temp.RETURNQTY;
                            newEntity.REQUESTQTY = temp.REQUESTQTY;
                            newEntity.OUTTYPE = temp.OUTTYPE;
                            newEntity.REMAK = temp.ID + ";";
                        }
                        else
                        {
                            newEntity.RETURNQTY += temp.RETURNQTY;
                            newEntity.ACTQTY += temp.ACTQTY;
                            newEntity.REMAK += temp.ID + ";";
                        }
                        AtsCellOutService.deleteById(temp.ID);
                    }
                    decimal id = BaseService.GetSeq("select S_ATSCELL_OUT.nextval from dual");
                    newEntity.ID = id;
                    AtsCellOutService.InsertObject(newEntity);
                    ts.Complete();
                   // search();
                }
                search();
            }
        }

    }
}
