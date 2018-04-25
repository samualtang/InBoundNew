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
    public partial class fm_Machine : Form
    {
        public fm_Machine()
        {
            InitializeComponent();
        }

        public fm_Machine(string text)
        {
            InitializeComponent();
            this.dgvBetlTaskInfo.DoubleBufferedDataGirdView(true);
            this.dgvMachineTaks.DoubleBufferedDataGirdView(true);
            this.Text = text;
        }

        private void fm_Machine_Load(object sender, EventArgs e)
        {
           // BtnText(this.Text);
            lblFormText.Text = this.Text +"烟柜";
        }
        void BtnText(string groupText)
        {
            int group = 0;
            if (groupText.Contains("一") || groupText.Contains("三") || groupText.Contains("五") || groupText.Contains("七"))
            {
                group = 1;//1-11
            }
            else
            {
                group = 2;//12-22
            }
            if (group == 1)
            {
                for (int i = 1; i < 12; i++)
                {

                    foreach (Control control in this.Controls)
                    {
                        if (control.Text != btnBelt.Text.Replace(" ", ""))
                        {
                            if (control is Button)
                            {
                                //遍历后的操作...
                                control.Text = i.ToString() + "号";
                            }
                        }
                    }
                }
              
               //for(int i=0;i<this.Controls.Count;i++)
               // {
               //    if(this.Controls[i] is Button)
               //    {
               //      this.Controls[i] as Button).Text = i.ToString();    //是要将按钮标签设为0-9数字么？
               //    }
               //}
                 
            }
            else
            {
              
                for (int i = 12; i < 23; i++)
                { 
                    foreach (Control control in this.Controls)
                    {
                       
                        if (control is Button)
                        {
                            if (control.Text.Replace(" ", "") != "皮带")
                            {
                                MessageBox.Show(control.Text);
                                //遍历后的操作...
                                control.Text = i.ToString() + "号";
                            }
                        } 
                    }
                }
              
            }

        }

        private void Machine1_Click(object sender, EventArgs e)
        {
            Button name = ((Button)sender);
            gbMachine.Text = name.Text+"机械手信息";
        }
    }
}
