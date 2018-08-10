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
    public partial class w_UnNormal : Form
    {
        public w_UnNormal()
        {
            InitializeComponent();

            dgvMainBeltInfo.DoubleBufferedDataGirdView(true);
            this.Text = "异形烟";
        }

        private void txtDeviceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            InBound.Business.UnPokeService.GetUnTaskInfo(1, 22, 10);
        }
    }
}
