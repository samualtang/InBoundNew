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

namespace FormUI
{
    public partial class YBFM : Form
    {
        public YBFM()
        {
            InitializeComponent();
        }
        Boolean spfirst = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                spfirst = true;
            }
            Thread thread = new Thread(new ThreadStart(startAutoInBound));
            thread.Start();
        }
        bool isRun = true;
        public void startAutoInBound()
        {


            // 发送堆垛机命令
            

                DateTime now = DateTime.Now;
                InBoundService.PreUpdateInOut(spfirst,null);
                DateTime end = DateTime.Now;
                TimeSpan span = end.Subtract(now);
               
            

        }
    }
}
