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
        }

        private void Machine1_Click(object sender, EventArgs e)
        {
            Button btn = ((Button)sender);
            gbMachine.Text = btn.Text + "机械手信息";
        }
    }
}
