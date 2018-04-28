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
            lblFormText.Text = this.Text +"烟柜";
            BtnText(this.Text); 
           
        }
 

        private void Machine1_Click(object sender, EventArgs e)
        {
            Button name = ((Button)sender);
            Fm_FollowTaskMachineDetail ftmd = new Fm_FollowTaskMachineDetail(Text+ name.Text);
            ftmd.Show();
            gbMachine.Text = name.Text+"机械手信息";
        }

        /// <summary>
        /// 机械手根据组变更
        /// </summary>
        /// <param name="groupText">组名 </param>
        public void BtnText(string groupText)
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
                int j = 1;
                for (int i = 1; i < 13; i++)
                {
                    string btnNmae = "Machine" + j;
                    Control control = Controls.Find(btnNmae, true)[0];
                    control.Text = j + "号";
                    j++;
                }
            }
            else if (group == 2)
            {
                int j = 1;
                for (int i = 12; i < 23; i++)
                {
                    string btnNmae = "Machine" + j;
                    Control control = Controls.Find(btnNmae, true)[0];
                    control.Text = i + "号";
                    j++;
                }
            }

        }
    }
}
