using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FollowTask
{
    public partial class Fm_FollowTaskSorting : Form
    {
        public Fm_FollowTaskSorting()
        {
            InitializeComponent();
        }

        public Fm_FollowTaskSorting(string text)
        { 
            InitializeComponent();
            this.Text = text;
        }

        private void Fm_FollowTaskSorting_Load(object sender, EventArgs e)
        {
            BitmapRegion BitmapRegion = new BitmapRegion();//此为生成不规则窗体和控件的类
            BitmapRegion.CreateControlRegion(this, new Bitmap("HMlogin.bmp"));
        }
    }
}
