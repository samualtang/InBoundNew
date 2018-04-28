using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound;

namespace FollowTask
{
    public partial class Fm_FollowTaskUnion : Form
    {
        public Fm_FollowTaskUnion()
        {
            InitializeComponent();
        }
        public Fm_FollowTaskUnion(string text)
        {
            InitializeComponent();
            this.dgvBetlTaskInfo.DoubleBufferedDataGirdView(true);
            this.dgvMachineTaks.DoubleBufferedDataGirdView(true);
            Text = text;
        } 
        private void Fm_FollowTaskUnion_Load(object sender, EventArgs e)
        {
            lblGourpText.Text = this.Text+"主皮带";
            BtnText(this.Text);
        }

        private void Machine1_Click(object sender, EventArgs e)
        {
            Button btn = ((Button)sender);
            gbMachine.Text = btn.Text + "机械手信息"; 
            Fm_FollowTaskMachineDetail ftmd = new Fm_FollowTaskMachineDetail("第"+Text+"皮带"+btn.Text);
            ftmd.Show();
        }

        /// <summary>
        /// 机械手根据组变更名 
        /// </summary>
        /// <param name="groupText">组名 </param>
        public void BtnText(string groupText)
        { 
            if (groupText.Contains("一") )
            {
                int j = 1;
                for (int i = 1; i <= 8; i++)
                {
                    string btnNmae = "Machine" + j;
                    Control control = Controls.Find(btnNmae, true)[0];
                    control.Text = i + "号";
                    j++;
                }
            }
           if (groupText.Contains("二")) // || groupText.Contains("五") || groupText.Contains("七"
            {
                int j = 1;
                for (int i = 9; i <= 16; i++)
                {
                    string btnNmae = "Machine" + j;
                    Control control = Controls.Find(btnNmae, true)[0];
                    control.Text = i + "号";
                    j++;
                }
            }
            if (groupText.Contains("三"))
            {
                int j = 1;
                for (int i = 17; i <= 25; i++)
                {
                    string btnNmae = "Machine" + j;
                    Control control = Controls.Find(btnNmae, true)[0];
                    control.Text = i + "号";
                    j++;
                }
            }
            if (groupText.Contains("四"))
            {
                int j = 1;
                for (int i = 25; i <= 32; i++)
                {
                    string btnNmae = "Machine" + j;
                    Control control = Controls.Find(btnNmae, true)[0];
                    control.Text = i + "号";
                    j++;
                }
            }

        }

        private void btnhuancun1_Click(object sender, EventArgs e)
        {
            Button btn = ((Button)sender);
            string btnNmae = "Machine" + btn.Name.Substring(10);
            Control control = Controls.Find(btnNmae, true)[0];
            Fm_UinonCache uc = new Fm_UinonCache(Text+control.Text);
            uc.Show();
        }
    }
}
