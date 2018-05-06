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

namespace FollowTask
{
    public partial class Fm_SortDetails : Form
    {
        public Fm_SortDetails()
        {
            InitializeComponent();
            this.listViewYaobaiDetails.DoubleBufferedListView(true);
             
        }

        public Fm_SortDetails(string  storText)
        {
            InitializeComponent();
            Text = storText+"区域";
        }

        private void Fm_SortDetails_Load(object sender, EventArgs e)
        {
          
        }
    }
}
