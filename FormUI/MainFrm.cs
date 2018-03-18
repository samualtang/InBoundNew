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
                ������ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(2000))
            {
                btnOutBound.Visible = false;
                �������ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(3000))
            {
                btnReport.Visible = false;
                ͳ�Ʊ���ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(4000))
            {
                btnStorage.Visible = false;
                �ֿ����ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(5000))
            {
                btnFenjian.Visible = false;
                �ּ����ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(6000))
            {
                btnChaiduo.Visible = false;
                �˹����ToolStripMenuItem1.Visible = false;
            }
            if (!menuInfo.Contains(1001))
            {
                ��ⵥ���ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(1002))
            {
                �������ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(1003))
            {
                ��Ʒ���ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(1004))
            {
                �ƿ����ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(1005))
            {
               ���ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(1006))
            {
                �˹����ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(2001))
            {
               ����ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(2002))
            {
                �Զ���������ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(3001))
            {
                ���ͳ��ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(3002))
            {
                ��λ��ϸToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(3003))
            {
                ������ѯToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(3004))
            {
                �쳣��ѯToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(4001))
            {
                �������ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(4002))
            {
                �豸����ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(4003))
            {
                ���ɴ�λToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(5001))
            {
                �ּ�Ԥ��ToolStripMenuItem.Visible = false;
            }
            if (!menuInfo.Contains(6001))
            {
                �˹����ToolStripMenuItem1.Visible = false;
            }
        }
        private void CreateFList()
        {
            listView1.Items.Clear();

            if (menuInfo.Contains(1001))
            {
                listView1.Items.Add("��ⵥ���", 0);
            }
            if (menuInfo.Contains(1002))
            {
                listView1.Items.Add("�������", 0);
            }
            if (menuInfo.Contains(1003))
            {
                listView1.Items.Add("�˹����", 0);
            }
            if (menuInfo.Contains(1004))
            {
                listView1.Items.Add("��Ʒ���", 0);
            }
            if (menuInfo.Contains(1005))
            {
                listView1.Items.Add("�ƿ����", 0);
            }
            if (menuInfo.Contains(1006))
            {
                listView1.Items.Add("����", 0);
            }
           // listView1.Items.Add("���������", 0);
           // listView1.Items.Add("ָ����λ���", 0);
            
        }
        private void CreateSList()
        {
            listView1.Items.Clear();

            if (menuInfo.Contains(2001))
            {
                listView1.Items.Add("����", 1);
            }
            if (menuInfo.Contains(2002))
            {
                listView1.Items.Add("�Զ���������", 1);
            }
         
        }
        private void CreateManual()
        {
            listView1.Items.Clear();
            if (menuInfo.Contains(6001))
            {
                listView1.Items.Add("�˹����", 0);
            }
        }
        private void CreateTList()
        {
            listView1.Items.Clear();
            if (menuInfo.Contains(3001))
            {
                listView1.Items.Add("���ͳ��", 0);
            }
            if (menuInfo.Contains(3002))
            {
                listView1.Items.Add("��λ��ϸ", 1);
            }
            if (menuInfo.Contains(3003))
            {
                listView1.Items.Add("������ѯ", 2);
            }
            if (menuInfo.Contains(3004))
            {
                listView1.Items.Add("�쳣��ѯ", 3);
            }
            if (menuInfo.Contains(3005))
            {
                listView1.Items.Add("���ݲ�ѯ", 3);
            }
            if (menuInfo.Contains(3006))
            {
                listView1.Items.Add("���ʹ����", 3);
            }
        }

        private void CreateFoList()
        {
            listView1.Items.Clear();
            if (menuInfo.Contains(4001))
            {
                listView1.Items.Add("�������", 0);
            }
            if (menuInfo.Contains(4002))
            {
                listView1.Items.Add("�豸����", 1);
            }
            if (menuInfo.Contains(4003))
            {
                listView1.Items.Add("���ɴ�λ", 0);
            }
            if (menuInfo.Contains(4004))
            {
                listView1.Items.Add("��λ����", 0);
            }
        }


        private void CreateFiList()
        {
            listView1.Items.Clear();
          
            listView1.Items.Add("������ѯ", 0);
            listView1.Items.Add("�쳣��ѯ", 0);

        }
        private void CreateFenJianList()
        {
            listView1.Items.Clear();
            if (menuInfo.Contains(5001))
            {
                listView1.Items.Add("�ּ�Ԥ��", 0);
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
                    case "��ⵥ���":
                        FormUI.InBoundNoFM autofm = new FormUI.InBoundNoFM();
                        autofm.StartPosition = FormStartPosition.CenterScreen;
                        autofm.Show();
                        break;
                    case "�������":
                        FormUI.PalletFM pm = new FormUI.PalletFM();
                        pm.StartPosition = FormStartPosition.CenterScreen;
                        pm.Show();
                        break;
                    case "�˹����":
                        FormUI.OtherPalletFM manualpm = new FormUI.OtherPalletFM();
                        manualpm.StartPosition = FormStartPosition.CenterScreen;
                        manualpm.Show();
                        break;
                    case "��Ʒ���":
                        FormUI.InBoundFM fm = new FormUI.InBoundFM();
                        fm.StartPosition = FormStartPosition.CenterScreen;
                        fm.Show();
                        break;
                       
                    case "��������":
                        FormUI.InBoundFM fm1 = new FormUI.InBoundFM();
                        fm1.StartPosition = FormStartPosition.CenterScreen;
                        fm1.Show();
                        break;
                    case "�ƿ����":

                         FormUI.InBoundFM movefm = new FormUI.InBoundFM();
                         movefm.StartPosition = FormStartPosition.CenterScreen;
                         movefm.Show();
                        break;
                    case "ָ����λ���":
                        FormUI.FixPositionInBoundFM fm2 = new FormUI.FixPositionInBoundFM();
                        fm2.StartPosition = FormStartPosition.CenterScreen;
                        fm2.Show();
                        break;
                    case "������":
                        FormUI.FOutBoundFM outfm = new FormUI.FOutBoundFM();
                        outfm.StartPosition = FormStartPosition.CenterScreen;
                        outfm.Show();
                        break;
                    case "�����̳���":
                        break;
                    case "���ͳ��":
                        FormUI.ReportForm rptFm = new FormUI.ReportForm();
                        rptFm.StartPosition = FormStartPosition.CenterScreen;
                        rptFm.Show();
                        break;
                    case "��λ��ϸ":
                        FormUI.ReportDetailForm rptDetailFm = new FormUI.ReportDetailForm();
                        rptDetailFm.StartPosition = FormStartPosition.CenterScreen;
                        rptDetailFm.Show();
                        break;
                    case "�������":
                        FormUI.LaneWayFM LaneFm = new FormUI.LaneWayFM();
                        LaneFm.StartPosition = FormStartPosition.CenterScreen;
                        LaneFm.Show();
                        break;
                    case "�豸����":
                        FormUI.DeviceFM LaneFm1 = new FormUI.DeviceFM();
                        LaneFm1.StartPosition = FormStartPosition.CenterScreen;
                        LaneFm1.Show();
                        break;
                    case "������ѯ":
                        FormUI.QueryForm qfm = new FormUI.QueryForm();
                        qfm.StartPosition = FormStartPosition.CenterScreen;
                        qfm.Show();
                        break;
                    case "�˹����":
                        FormUI.ManualForm manualfm = new FormUI.ManualForm();
                        manualfm.StartPosition = FormStartPosition.CenterScreen;
                        manualfm.Show();
                        break;
                    case "�쳣��ѯ":
                        FormUI.ErrorForm errorFm = new FormUI.ErrorForm();
                        errorFm.StartPosition = FormStartPosition.CenterScreen;
                        errorFm.Show();
                        break;
                    case "�ּ�Ԥ��":
                        FormUI.YBFM ybFm = new FormUI.YBFM();
                        ybFm.StartPosition = FormStartPosition.CenterScreen;
                        ybFm.Show();
                        break;
                    case "���ɴ�λ":
                        FormUI.DubiousForm dFm = new FormUI.DubiousForm();
                        dFm.StartPosition = FormStartPosition.CenterScreen;
                        dFm.Show();
                        break;
                    case "����":
                        FormUI.MOutBoundFM moutFm = new FormUI.MOutBoundFM();
                        moutFm.StartPosition = FormStartPosition.CenterScreen;
                        moutFm.Show();
                        break;
                    case "�Զ���������":
                        FormUI.AutoOutBoundFM autoOutFm = new FormUI.AutoOutBoundFM();
                        autoOutFm.StartPosition = FormStartPosition.CenterScreen;
                        autoOutFm.Show();
                        break;
                    case "����":
                          FormUI.InBoundReturnFM rkfm = new FormUI.InBoundReturnFM();
                          rkfm.StartPosition = FormStartPosition.CenterScreen;
                          rkfm.Show();
                        break;

                    case "��λ����":
                        FormUI.CellForm cfm = new FormUI.CellForm();
                        cfm.StartPosition = FormStartPosition.CenterScreen;
                        cfm.Show();
                        break;
                    case "���ݲ�ѯ":
                        FormUI.InBoundNoReport inBoundReport = new FormUI.InBoundNoReport();
                        inBoundReport.StartPosition = FormStartPosition.CenterScreen;
                        inBoundReport.Show();
                        break;
                    case "���ʹ����":
                        FormUI.LaneWayFMReport laneWayReport = new FormUI.LaneWayFMReport();
                        laneWayReport.StartPosition = FormStartPosition.CenterScreen;
                        laneWayReport.Show();
                        break;

                }
            }
        }

        private void ��ⵥ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.InBoundNoFM autofm = new FormUI.InBoundNoFM();
            autofm.StartPosition = FormStartPosition.CenterScreen;
            autofm.Show();

        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.PalletFM pm = new FormUI.PalletFM();
            pm.StartPosition = FormStartPosition.CenterScreen;
            pm.Show();
        }

        private void ��Ʒ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.InBoundFM fm = new FormUI.InBoundFM();
            fm.StartPosition = FormStartPosition.CenterScreen;
            fm.Show();

        }

        private void �ƿ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.InBoundFM fm = new FormUI.InBoundFM();
            fm.StartPosition = FormStartPosition.CenterScreen;
            fm.Show();

        }

        private void ������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.FOutBoundFM outfm = new FormUI.FOutBoundFM();
            outfm.StartPosition = FormStartPosition.CenterScreen;
            outfm.Show();
        }

        private void �˹����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.ManualForm manualfm = new FormUI.ManualForm();
            manualfm.StartPosition = FormStartPosition.CenterScreen;
            manualfm.Show();
        }

        private void ���ͳ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.ReportForm rptFm = new FormUI.ReportForm();
            rptFm.StartPosition = FormStartPosition.CenterScreen;
            rptFm.Show();
        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.LaneWayFM LaneFm = new FormUI.LaneWayFM();
            LaneFm.StartPosition = FormStartPosition.CenterScreen;
            LaneFm.Show();
        }

        private void �豸����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.DeviceFM LaneFm1 = new FormUI.DeviceFM();
            LaneFm1.StartPosition = FormStartPosition.CenterScreen;
            LaneFm1.Show();
        }

        private void ���ɴ�λToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.DubiousForm dFm = new FormUI.DubiousForm();
            dFm.StartPosition = FormStartPosition.CenterScreen;
            dFm.Show();
        }

        private void ������ѯToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.QueryForm qfm = new FormUI.QueryForm();
            qfm.StartPosition = FormStartPosition.CenterScreen;
            qfm.Show();
        }

        private void �쳣��ѯToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.ErrorForm errorFm = new FormUI.ErrorForm();
            errorFm.StartPosition = FormStartPosition.CenterScreen;
            errorFm.Show();
        }

        private void �ּ�Ԥ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.YBFM ybFm = new FormUI.YBFM();
            ybFm.StartPosition = FormStartPosition.CenterScreen;
            ybFm.Show();
        }

        

        private void �˹����ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FormUI.ManualForm manualfm = new FormUI.ManualForm();
            manualfm.StartPosition = FormStartPosition.CenterScreen;
            manualfm.Show();
        }

        private void �Զ���������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.AutoOutBoundFM autoOutFm = new FormUI.AutoOutBoundFM();
            autoOutFm.StartPosition = FormStartPosition.CenterScreen;
            autoOutFm.Show();
        }

        private void ��λ��ϸToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.ReportDetailForm rptFm = new FormUI.ReportDetailForm();
            rptFm.StartPosition = FormStartPosition.CenterScreen;
            rptFm.Show();
        }

        private void �˹����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.OtherPalletFM manualpm = new FormUI.OtherPalletFM();
            manualpm.StartPosition = FormStartPosition.CenterScreen;
            manualpm.Show();
            
        }

        private void ������ѯToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FormUI.QueryForm qfm = new FormUI.QueryForm();
            qfm.StartPosition = FormStartPosition.CenterScreen;
            qfm.Show();
        }

        private void �쳣��ѯToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FormUI.ErrorForm errorFm = new FormUI.ErrorForm();
            errorFm.StartPosition = FormStartPosition.CenterScreen;
            errorFm.Show();
        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.MOutBoundFM moutFm = new FormUI.MOutBoundFM();
            moutFm.StartPosition = FormStartPosition.CenterScreen;
            moutFm.Show();
           
        }

        private void ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ���ݲ�ѯToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.InBoundNoReport moutFm = new FormUI.InBoundNoReport();
            moutFm.StartPosition = FormStartPosition.CenterScreen;
            moutFm.Show();
        }

        private void ���ʹ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormUI.LaneWayFMReport moutFm = new FormUI.LaneWayFMReport();
            moutFm.StartPosition = FormStartPosition.CenterScreen;
            moutFm.Show();
        }

        

       
    }
}