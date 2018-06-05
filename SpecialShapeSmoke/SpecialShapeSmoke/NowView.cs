using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound.Business;
using System.Threading;

namespace SpecialShapeSmoke
{
    public partial class NowView : Form
    {
        int machineseq1;
        int machineseq2;
        static int NowMachineseq;
        decimal[] nowpokeids;
        System.Windows.Forms.Timer t1 = new System.Windows.Forms.Timer();
        decimal[] lastpokeids = new decimal[] { -1, -1 };
        decimal nowpokeid;
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
            if (labMachineSeq.Text == machineseq1.ToString())
            {
                nowpokeid = nowpokeids[0]; 
            }
            else
            {
                nowpokeid = nowpokeids[1]; 
            }
            NowPoke(nowpokeids,true);
            NowMachineseq = machineseq1;
            t1.Tick  +=new EventHandler(t1_Tick);
            t1.Interval = 500;
            t1.Start();
          
             
        } 


        private void NowView_Load(object sender, EventArgs e)
        {
            DateBind(machineseq1, nowpokeids[0].ToString());
            NowPoke(nowpokeids, true);
        }

        //通道1
        private void btnMachineSeq1_Click(object sender, EventArgs e)
        {
            nowpokeid = nowpokeids[0] ;
            NowMachineseq = machineseq1;
            DateBind(machineseq1, nowpokeids[0].ToString());
            labMachineSeq.Text = machineseq1 + "混合道";
            NowPoke(nowpokeids, true); 
        }
        //通道2
        private void btnMachineSeq2_Click(object sender, EventArgs e)
        {
            nowpokeid = nowpokeids[1] ;
            NowMachineseq = machineseq2;
            DateBind(machineseq2, nowpokeids[1].ToString());
            labMachineSeq.Text = machineseq2 + "混合道"; 
            NowPoke(nowpokeids, true);
        }
        private void t1_Tick(object sender, EventArgs e)
        {
            string pokeid = Convert.ToInt32(labMachineSeq.Text.Substring(0, 4)) == machineseq1 ? nowpokeids[0].ToString() : nowpokeids[1].ToString();
            NowPoke(nowpokeids,false);
            
        }
        //定位当前
        private void btnNowPoke_Click(object sender, EventArgs e)
        {
            string pokeid =Convert.ToInt32(labMachineSeq.Text.Substring(0,4))==machineseq1?nowpokeids[0].ToString():nowpokeids[1].ToString();
            DateBind(Convert.ToDecimal(labMachineSeq.Text.Substring(0, 4)), pokeid);
            NowPoke(nowpokeids,true);
            
        }


        string lastpokeid;
        /// <summary>
        /// 定位当前条目
        /// </summary>
        /// <param name="nowpokeids"></param>
        private void NowPoke(decimal[] nowpokeids,bool falg)
        {
            
            string pokeid; 
            int machineseq;
            if(labMachineSeq.Text.Substring(0,4)==machineseq1.ToString())
            {
                pokeid = nowpokeids[0].ToString(); 
                lastpokeid=lastpokeids[0].ToString();
                machineseq=machineseq1;
            }
            else
            {
                pokeid = nowpokeids[1].ToString();
                lastpokeid=lastpokeids[1].ToString();
                machineseq=machineseq2; 
            }
            //若两次的pokeid不同 重置标志位 重选行
            if (lastpokeid != pokeid)
            {
                falg = true;
            }
            //判断当前pokeid是否等于上一个
            if (!string.IsNullOrEmpty(pokeid) &&( lastpokeid != pokeid || falg))
            { 
                 
                 DateBind(machineseq);

                for (int i = 0; i < DgvNowView.RowCount; i++)
                {
                    if (DgvNowView.Rows[i].Cells["PokeId"].Value.ToString().Trim() == pokeid)
                    { 
                        foreach (DataGridViewRow row in DgvNowView.Rows)
                        {
                            row.Selected = false;
                        }
                        DgvNowView.Rows[i].Selected = true;
                        DgvNowView.FirstDisplayedScrollingRowIndex = i;
                        
                        break;
                    }
                }
                if (labMachineSeq.Text.Substring(0, 4) == machineseq1.ToString())
                {
                    lastpokeids[0] = Convert.ToDecimal(pokeid);
                }
                else
                {
                    lastpokeids[1] = Convert.ToDecimal(pokeid); 
                }
                falg = false;
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="Seq"></param>
        /// <param name="pokeid"></param>

        public void DateBind(decimal Seq, string pokeid = null)
        {
            HunHeService HunHeNowCigarette = new HunHeService();
            List<HUNHENOWVIEW> hunhelist=HunHeNowCigarette.GetALLCigarette(Seq);
            if(hunhelist.Count < 1)
            {
                labMachineSeq.Text = Seq + "通道没有分拣数据，请选择其他通道！";
            }
            DgvNowView.DataSource = hunhelist;

            
        }

        private void NowView_Deactivate(object sender, EventArgs e)
        {
            t1.Stop();
            this.Dispose();
            this.Close();
                
        }
        /// <summary>
        /// 改变单元格显示值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvNowView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        { 
            //重绘状态单元格显示
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
                        foreach (DataGridViewRow item in DgvNowView.Rows)
                        {
                            if (Convert.ToDecimal(item.Cells[8].Value) < Convert.ToDecimal(Convert.ToInt32(labMachineSeq.Text.Substring(0, 4)) == machineseq1 ? nowpokeids[0].ToString() : nowpokeids[1].ToString()))
                            {
                                Status = "分拣完成";
                            }
                        } 
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
