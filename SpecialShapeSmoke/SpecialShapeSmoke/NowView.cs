using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound.Business;

namespace SpecialShapeSmoke
{
    public partial class NowView : Form
    {
        int machineseq1;
        int machineseq2;
        decimal[] nowpokeids;
        Timer t1 = new Timer();
        decimal[] lastpokeids = new decimal[] { -1, -1 };
        public NowView(int machineseq1,int machineseq2,decimal[] nowpokeids)
        {

            InitializeComponent();
            this.machineseq1 = machineseq1;
            this.machineseq2 = machineseq2;
            this.nowpokeids = nowpokeids;  

            btnMachineSeq1.Text = machineseq1 + "混合道";
            btnMachineSeq2.Text = machineseq2 + "混合道";
            labMachineSeq.Text = machineseq1 + "混合道";

            if (machineseq1 == machineseq2)
            {
                btnMachineSeq2.Visible = false;
            }

            t1.Tick  +=new EventHandler(t1_Tick);
            t1.Interval = 2000;
            t1.Start();
        }

         
        private void NowView_Load(object sender, EventArgs e)
        {
            DateBind(machineseq1, nowpokeids[0].ToString());
        }


        private void btnMachineSeq1_Click(object sender, EventArgs e)
        {
            DateBind(machineseq1, nowpokeids[0].ToString());
            labMachineSeq.Text = machineseq1 + "混合道";
        }
        private void btnMachineSeq2_Click(object sender, EventArgs e)
        {
            DateBind(machineseq2, nowpokeids[1].ToString());
            labMachineSeq.Text = machineseq2 + "混合道";
        }
        private void t1_Tick(object sender, EventArgs e)
        {
            string pokeid = Convert.ToInt32(labMachineSeq.Text.Substring(0, 4)) == machineseq1 ? nowpokeids[0].ToString() : nowpokeids[1].ToString();
            NowPoke(nowpokeids);
            
        }
        private void btnNowPoke_Click(object sender, EventArgs e)
        {
            string pokeid =Convert.ToInt32(labMachineSeq.Text.Substring(0,4))==machineseq1?nowpokeids[0].ToString():nowpokeids[1].ToString();
            DateBind(Convert.ToDecimal(labMachineSeq.Text.Substring(0, 4)), pokeid);
            NowPoke(nowpokeids);
            
        }
        private void NowPoke(decimal[] nowpokeids )
        {
            string pokeid;
            string lastpokeid;
            if(labMachineSeq.Text.Substring(0,4)==machineseq1.ToString())
            {
                pokeid = nowpokeids[0].ToString(); 
                lastpokeid=lastpokeids[0].ToString();
            }
            else
            {
                pokeid = nowpokeids[1].ToString();
                lastpokeid=lastpokeids[1].ToString();
            }
            if (!string.IsNullOrEmpty(pokeid) && lastpokeid != pokeid)
            { 
                for (int i = 0; i < DgvNowView.RowCount; i++)
                {
                    if (DgvNowView.Rows[i].Cells["PokeId"].Value.ToString().Trim() == pokeid)
                    {
                        DgvNowView.Rows[0].Selected = false;
                        DgvNowView.Rows[i].Selected = true;
                        DgvNowView.FirstDisplayedScrollingRowIndex = i;
                        break;
                    }
                }
            }
        }

        
        public void DateBind(decimal Seq, string pokeid = null)
        {
            HunHeService HunHeNowCigarette = new HunHeService();
            List<HUNHENOWVIEW> hunhelist=HunHeNowCigarette.GetALLCigarette(Seq);
            if(hunhelist.Count < 1)
            {
                labMachineSeq.Text = Seq + "通道没有分拣数据，请选择其他通道！";
            }
            else
            { 
                DgvNowView.DataSource = hunhelist;
            } 
        }

        private void NowView_Deactivate(object sender, EventArgs e)
        {
            t1.Stop();
            this.Dispose();
            this.Close();
                
        }

        private void DgvNowView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        { 
            if(e.ColumnIndex == 7)
            {
            string Status = "";
            switch (e.Value.ToString())
            {
                case "10":
                    Status = "待分拣";
                    break;
                case "15":
                    Status = "正在分拣";
                    break;
                case "20":
                    Status = "分拣完成";
                    break;
            }
            e.Value = Status;
            }
        }
        
      

      

    }
}
