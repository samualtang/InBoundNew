using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using InBound.Business;

namespace FormUI
{
    public partial class ReplishementFm : Form
    {
        public ReplishementFm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(StartReplishement));
            thread.Start();
        }

        public void StartReplishement()
        {
            InBoundService.PreUpdateInOut(true,null);
        }
    }
}
