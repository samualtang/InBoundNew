using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FollowTask
{
    public partial class Fm_UinonCache : Form
    {
        public Fm_UinonCache()
        {
            InitializeComponent();
        }
        public Fm_UinonCache(string machineText)
        {
            InitializeComponent();
            Text = machineText;
            this.StartPosition = FormStartPosition.CenterScreen;
            lblCacheText.Text = machineText.Substring(6) + "机械手缓存区香烟排序";
        }

 
    }
}
