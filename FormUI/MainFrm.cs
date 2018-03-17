using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FormUI.TooL;

namespace MainUI
{
    public partial class MainFrm : Form
    {
        String sourceAdd=System.Configuration.ConfigurationManager.AppSettings["SourceAdd"];
        List<int?> menuInfo = null;
        public MainFrm()
        {
            InitializeComponent();
            menuInfo = InBound.Business.SysUserService.getUserMenu(Constant.userName);
            //menuInfo.Add(1000);
            //menuInfo.Add(1001);
            //menuInfo.Add(1003);
            initMenu();
        }


        private void MainFrm_Load(object sender, EventArgs e)
        {
            ButtonClick(btnInBound, null);
            listView1.Alignment = ListViewAlignment.Left;
            listView1.SmallImageList = imageListZip;
            listView1.StateImageList = imageListZip;
            listView1.LargeImageList = imageListZip;
        }

        public void initMenu()
        {
            if (!menuInfo.Contains(1000))
            {
                btnInBound.Visible = false;
                入库管理ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(2000))
            {
                btnOutBound.Visible = false;
                出库管理ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(3000))
            {
                btnReport.Visible = false;
                统计报表ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(4000))
            {
                btnStorage.Visible = false;
                仓库管理ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(5000))
            {
                btnFenjian.Visible = false;
                分拣管理ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(6000))
            {
                btnChaiduo.Visible = false;
                人工拆垛ToolStripMenuItem1.Visible = false;
            }
            if (!menuInfo.Contains(1001))
            {
                入库单入库ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(1002))
            {
                托盘入库ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(1003))
            {
                成品入库ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(1004))
            {
                移库入库ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(1005))
            {
               入库ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(1006))
            {
                人工入库ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(2001))
            {
               出库ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(2002))
            {
                自动补货出库ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(3001))
            {
                库存统计ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(3002))
            {
                储位明细ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(3003))
            {
                出入库查询ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(3004))
            {
                异常查询ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(4001))
            {
                巷道管理ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(4002))
            {
                设备管理ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(4003))
            {
                可疑储位ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(5001))
            {
                分拣预补ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(6001))
            {
                人工拆垛ToolStripMenuItem1.Visible = false;
            }
        }
        private void CreateFList()
        {
            listView1.Items.Clear();

            if (menuInfo.Contains(1001))
            {
                listView1.Items.Add("入库单入库", 0);
            }
            if (menuInfo.Contains(1002))
            {
                listView1.Items.Add("托盘入库", 0);
            }
            if (menuInfo.Contains(1003))
            {
                listView1.Items.Add("人工入库", 0);
            }
            if (menuInfo.Contains(1004))
            {
                listView1.Items.Add("成品入库", 0);
            }
            if (menuInfo.Contains(1005))
            {
                listView1.Items.Add("移库入库", 0);
            }
            if (menuInfo.Contains(1006))
            {
                listView1.Items.Add("返库", 0);
            }
           // listView1.Items.Add("空托盘入库", 0);
           // listView1.Items.Add("指定货位入库", 0);
            
        }
        private void CreateSList()
        {
            listView1.Items.Clear();

            if (menuInfo.Contains(2001))
            {
                listView1.Items.Add("出库", 1);
            }
            if (menuInfo.Contains(2002))
            {
                listView1.Items.Add("自动补货出库", 1);
            }
         
        }
        private void CreateManual()
        {
            listView1.Items.Clear();
            if (menuInfo.Contains(6001))
            {
                listView1.Items.Add("人工拆垛", 0);
            }
        }
        private void CreateTList()
        {
            listView1.Items.Clear();
            if (menuInfo.Contains(3001))
            {
                listView1.Items.Add("库存统计", 0);
            }
            if (menuInfo.Contains(3002))
            {
                listView1.Items.Add("储位明细", 1);
            }
            if (menuInfo.Contains(3003))
            {
                listView1.Items.Add("出入库查询", 2);
            }
            if (menuInfo.Contains(3004))
            {
                listView1.Items.Add("异常查询", 3);
            }
            if (menuInfo.Contains(3005))
            {
                listView1.Items.Add("单据查询", 3);
            }
            if (menuInfo.Contains(3006))
            {
                listView1.Items.Add("巷道使用率", 3);
            }
        }

        private void CreateFoList()
        {
            listView1.Items.Clear();
            if (menuInfo.Contains(4001))
            {
                listView1.Items.Add("巷道管理", 0);
            }
            if (menuInfo.Contains(4002))
            {
                listView1.Items.Add("设备管理", 1);
            }
            if (menuInfo.Contains(4003))
            {
                listView1.Items.Add("可疑储位", 0);
            }
            if (menuInfo.Contains(4004))
            {
                listView1.Items.Add("储位管理", 0);
            }
        }


        private void CreateFiList()
        {
            listView1.Items.Clear();
          
            listView1.Items.Add("出入库查询", 0);
            listView1.Items.Add("异常查询", 0);

        }
        private void CreateFenJianList()
        {
            listView1.Items.Clear();
            if (menuInfo.Contains(5001))
            {
                listView1.Items.Add("分拣预补", 0);
            }

        }
        void ButtonClick(object sender, System.EventArgs e)
        {
            // Get the clicked button...
            Button clickedButton = (Button)sender;
            // ... and it's tabindex
            int clickedButtonTabIndex = clickedButton.TabIndex;

            // Send each button to top or bottom as appropriate
            foreach (Control ctl in panel1.Controls)
            {
                if (ctl is Button)
                {
                    Button btn = (Button)ctl;
                    if (btn.TabIndex > clickedButtonTabIndex)
                    {
                        if (btn.Dock != DockStyle.Bottom)
                        {
                            btn.Dock = DockStyle.Bottom;
                          
                            btn.BringToFront();
                        }
                    }
                    else
                    {
                        if (btn.Dock != DockStyle.Top)
                        {
                            btn.Dock = DockStyle.Top;
                           
                            btn.BringToFront();
                        }
                    }
                }
            }
            switch (clickedButton.Name)
            {
                case "btnInBound":
                    CreateFList();
                    break;
                case "btnOutBound":
                    CreateSList();
                    break;
                case "btnReport":
                    CreateTList();
                    break;
                case "btnStorage":
                    CreateFoList();
                    break;
                case "btnQuery":
                    CreateFiList();
                    break;
                case "btnFenjian":
                    CreateFenJianList();
                    break;
                case "btnChaiduo":
                    CreateManual();
                    break;
            }
           listView1.BringToFront();  // Without this, the buttons will hide the items.
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedIndices.Count > 0)
            {
                switch (this.listView1.SelectedItems[0].Text)
                {
                    case "入库单入库":
                        FormUI.InBoundNoFM autofm = new FormUI.InBoundNoFM();
                        autofm.StartPosition = FormStartPosition.CenterScreen;
                        autofm.Show();
                        break;
                    case "托盘入库":
                        FormUI.PalletFM pm = new FormUI.PalletFM();
                        pm.StartPosition = FormStartPosition.CenterScreen;
                        pm.Show();
                        break;
                    case "人工入库":
                        FormUI.OtherPalletFM manualpm = new FormUI.OtherPalletFM();
                        manualpm.StartPosition = FormStartPosition.CenterScreen;
                        manualpm.Show();
                        break;
                    case "成品入库":
                        FormUI.InBoundFM fm = new FormUI.InBoundFM();
                        fm.StartPosition = FormStartPosition.CenterScreen;
                        fm.Show();
                        break;
                       
                    case "补货反库":
                        FormUI.InBoundFM fm1 = new FormUI.InBoundFM();
                        fm1.StartPosition = FormStartPosition.CenterScreen;
                        fm1.Show();
                        break;
                    case "移库入库":

                         FormUI.InBoundFM movefm = new FormUI.InBoundFM();
                         movefm.StartPosition = FormStartPosition.CenterScreen;
                         movefm.Show();
                        break;
                    case "指定货位入库":
                        FormUI.FixPositionInBoundFM fm2 = new FormUI.FixPositionInBoundFM();
                        fm2.StartPosition = FormStartPosition.CenterScreen;
                        fm2.Show();
                        break;
                    case "抽检出库":
                        FormUI.FOutBoundFM outfm = new FormUI.FOutBoundFM();
                        outfm.StartPosition = FormStartPosition.CenterScreen;
                        outfm.Show();
                        break;
                    case "空托盘出库":
                        break;
                    case "库存统计":
                        FormUI.ReportForm rptFm = new FormUI.ReportForm();
                        rptFm.StartPosition = FormStartPosition.CenterScreen;
                        rptFm.Show();
                        break;
                    case "储位明细":
                        FormUI.ReportDetailForm rptDetailFm = new FormUI.ReportDetailForm();
                        rptDetailFm.StartPosition = FormStartPosition.CenterScreen;
                        rptDetailFm.Show();
                        break;
                    case "巷道管理":
                        FormUI.LaneWayFM LaneFm = new FormUI.LaneWayFM();
                        LaneFm.StartPosition = FormStartPosition.CenterScreen;
                        LaneFm.Show();
                        break;
                    case "设备管理":
                        FormUI.DeviceFM LaneFm1 = new FormUI.DeviceFM();
                        LaneFm1.StartPosition = FormStartPosition.CenterScreen;
                        LaneFm1.Show();
                        break;
                    case "出入库查询":
                        FormUI.QueryForm qfm = new FormUI.QueryForm();
                        qfm.StartPosition = FormStartPosition.CenterScreen;
                        qfm.Show();
                        break;
                    case "人工拆垛":
                        FormUI.ManualForm manualfm = new FormUI.ManualForm();
                        manualfm.StartPosition = FormStartPosition.CenterScreen;
                        manualfm.Show();
                        break;
                    case "异常查询":
                        FormUI.ErrorForm errorFm = new FormUI.ErrorForm();
                        errorFm.StartPosition = FormStartPosition.CenterScreen;
                        errorFm.Show();
                        break;
                    case "分拣预补":
                        FormUI.YBFM ybFm = new FormUI.YBFM();
                        ybFm.StartPosition = FormStartPosition.CenterScreen;
                        ybFm.Show();
                        break;
                    case "可疑储位":
                        FormUI.DubiousForm dFm = new FormUI.DubiousForm();
                        dFm.StartPosition = FormStartPosition.CenterScreen;
                        dFm.Show();
                        break;
                    case "出库":
                        FormUI.MOutBoundFM moutFm = new FormUI.MOutBoundFM();
                        moutFm.StartPosition = FormStartPosition.CenterScreen;
                        moutFm.Show();
                        break;
                    case "自动补货出库":
                        FormUI.AutoOutBoundFM autoOutFm = new FormUI.AutoOutBoundFM();
                        autoOutFm.StartPosition = FormStartPosition.CenterScreen;
                        autoOutFm.Show();
                        break;
                    case "返库":
                          FormUI.InBoundReturnFM rkfm = new FormUI.InBoundReturnFM();
                          rkfm.StartPosition = FormStartPosition.CenterScreen;
                          rkfm.Show();
                        break;

                    case "储位管理":
                        FormUI.CellForm cfm = new FormUI.CellForm();
                        cfm.StartPosition = FormStartPosition.CenterScreen;
                        cfm.Show();
                        break;
                    case "单据查询":
                        FormUI.InBoundNoReport inBoundReport = new FormUI.InBoundNoReport();
                        inBoundReport.StartPosition = FormStartPosition.CenterScreen;
                        inBoundReport.Show();
                        break;
                    case "巷道使用率":
                        FormUI.LaneWayFMReport laneWayReport = new FormUI.LaneWayFMReport();
                        laneWayReport.StartPosition = FormStartPosition.CenterScreen;
                        laneWayReport.Show();
                        break;

                }
            }
        }

        private void 入库单入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.InBoundNoFM autofm = new FormUI.InBoundNoFM();
            autofm.StartPosition = FormStartPosition.CenterScreen;
            autofm.Show();

        }

        private void 托盘入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.PalletFM pm = new FormUI.PalletFM();
            pm.StartPosition = FormStartPosition.CenterScreen;
            pm.Show();
        }

        private void 成品入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.InBoundFM fm = new FormUI.InBoundFM();
            fm.StartPosition = FormStartPosition.CenterScreen;
            fm.Show();

        }

        private void 移库入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.InBoundFM fm = new FormUI.InBoundFM();
            fm.StartPosition = FormStartPosition.CenterScreen;
            fm.Show();

        }

        private void 抽检出库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.FOutBoundFM outfm = new FormUI.FOutBoundFM();
            outfm.StartPosition = FormStartPosition.CenterScreen;
            outfm.Show();
        }

        private void 人工拆垛ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.ManualForm manualfm = new FormUI.ManualForm();
            manualfm.StartPosition = FormStartPosition.CenterScreen;
            manualfm.Show();
        }

        private void 库存统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.ReportForm rptFm = new FormUI.ReportForm();
            rptFm.StartPosition = FormStartPosition.CenterScreen;
            rptFm.Show();
        }

        private void 巷道管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.LaneWayFM LaneFm = new FormUI.LaneWayFM();
            LaneFm.StartPosition = FormStartPosition.CenterScreen;
            LaneFm.Show();
        }

        private void 设备管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.DeviceFM LaneFm1 = new FormUI.DeviceFM();
            LaneFm1.StartPosition = FormStartPosition.CenterScreen;
            LaneFm1.Show();
        }

        private void 可疑储位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.DubiousForm dFm = new FormUI.DubiousForm();
            dFm.StartPosition = FormStartPosition.CenterScreen;
            dFm.Show();
        }

        private void 出入库查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.QueryForm qfm = new FormUI.QueryForm();
            qfm.StartPosition = FormStartPosition.CenterScreen;
            qfm.Show();
        }

        private void 异常查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.ErrorForm errorFm = new FormUI.ErrorForm();
            errorFm.StartPosition = FormStartPosition.CenterScreen;
            errorFm.Show();
        }

        private void 分拣预补ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.YBFM ybFm = new FormUI.YBFM();
            ybFm.StartPosition = FormStartPosition.CenterScreen;
            ybFm.Show();
        }

        

        private void 人工拆垛ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FormUI.ManualForm manualfm = new FormUI.ManualForm();
            manualfm.StartPosition = FormStartPosition.CenterScreen;
            manualfm.Show();
        }

        private void 自动补货出库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.AutoOutBoundFM autoOutFm = new FormUI.AutoOutBoundFM();
            autoOutFm.StartPosition = FormStartPosition.CenterScreen;
            autoOutFm.Show();
        }

        private void 储位明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.ReportDetailForm rptFm = new FormUI.ReportDetailForm();
            rptFm.StartPosition = FormStartPosition.CenterScreen;
            rptFm.Show();
        }

        private void 人工入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.OtherPalletFM manualpm = new FormUI.OtherPalletFM();
            manualpm.StartPosition = FormStartPosition.CenterScreen;
            manualpm.Show();
            
        }

        private void 出入库查询ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FormUI.QueryForm qfm = new FormUI.QueryForm();
            qfm.StartPosition = FormStartPosition.CenterScreen;
            qfm.Show();
        }

        private void 异常查询ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FormUI.ErrorForm errorFm = new FormUI.ErrorForm();
            errorFm.StartPosition = FormStartPosition.CenterScreen;
            errorFm.Show();
        }

        private void 出库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.MOutBoundFM moutFm = new FormUI.MOutBoundFM();
            moutFm.StartPosition = FormStartPosition.CenterScreen;
            moutFm.Show();
           
        }

        private void 入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 单据查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.InBoundNoReport moutFm = new FormUI.InBoundNoReport();
            moutFm.StartPosition = FormStartPosition.CenterScreen;
            moutFm.Show();
        }

        private void 巷道使用率ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.LaneWayFMReport moutFm = new FormUI.LaneWayFMReport();
            moutFm.StartPosition = FormStartPosition.CenterScreen;
            moutFm.Show();
        }

        

       
    }
}