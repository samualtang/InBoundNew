using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound.Model;
using FollowTask.Modle;

namespace FollowTask
{
    public partial class Fm_UnionMainBelt : Form
    {


        Fm_FollowTaskMachineDetail fm_machinedetails ;
        public Fm_UnionMainBelt()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Fm_UnionMainBelt_FormClosing);

            addPanel(30, "芙蓉王(硬)",2,true);

            fm_machinedetails = new Fm_FollowTaskMachineDetail(label1.Text);
        
        }
        /// <summary>
        /// 获取卷烟图片
        /// </summary>
        /// <param name="cigraNmae">卷烟名称</param>
        /// <returns>卷烟图片</returns>
        Bitmap GetImg(string cigraNmae)
        {
            Bitmap cigreImg = (Bitmap)Properties.Resources.ResourceManager.GetObject(cigraNmae);
            if (cigreImg == null)
            {
                cigreImg = (Bitmap)Properties.Resources.ResourceManager.GetObject("默认卷烟");
            }
            return cigreImg;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="count">当前第几次</param>
        /// <param name="cigarename">卷烟名称</param>
        /// <param name="aOrb">true是之前 false是之后</param>
        public void addPanel(int  count,string cigarename,int qty ,bool aOrb)
        {
            for (int i = 0; i < count; i++)
            { 
                PictureBox img = new PictureBox();
                Label lb = new Label();
                lb.Text = qty+"";
                lb.BackColor = Color.Transparent;
                img.Name = "ImgName" + Guid.NewGuid().ToString();
                img.Size = new System.Drawing.Size(20, 80);
                img.BackgroundImage = GetImg(cigarename);
                img.SizeMode = PictureBoxSizeMode.StretchImage;
                //img.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
                if (aOrb)
                {
                    lb.Location = new Point(i * img.Width + 10 * i, panelafter.Size.Height / 2);
                    img.Location = new Point(i * img.Width+10*i, 0);
                 
                    panelafter.Controls.Add(img);
                    panelafter.Controls.Add(lb);
                }
                else
                {
                    img.Location = new Point(1268 - (i * img.Width )-20, 0);
                   
                    panelbefore.Controls.Add(img);
                }
            }
            
        }
        void Fm_UnionMainBelt_FormClosing(object sender, FormClosingEventArgs e)
        {
            listViewAfter.Items.Clear();
            listViewBefore.Items.Clear();
            e.Cancel =true;
            this.Hide();
            fm_machinedetails.Close();
            return;

        }
        public void GetNeedInfo(int machineno, List<UnionTaskInfo> after, List<UnionTaskInfo> before)
        {
            try
            {
                listViewAfter.Items.Clear();
                listViewBefore.Items.Clear();
                label1.Text = machineno + "";
                groupBox1.Text = machineno + "号机械手之后卷烟摆放";
                groupBox2.Text = machineno + "号机械手之前卷烟摆放";
                groupBoxAfter.Text = machineno + "号机械手之前卷烟摆放";
                groupBoxBefore.Text = machineno + "号机械手之前卷烟摆放"; 

                listAfterBind(after);

                listBeforeBind(before);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           
        }
        /// <summary>
        /// 机械手之前皮带
        /// </summary>
        /// <param name="after"></param>
        void listAfterBind(List<UnionTaskInfo> after)
        {
            if (after != null && after.Count != 0)
            {
                bool aOrb = true;
                for (int i = 0; i < after.Count; i++)
                {
                 
                    ListViewItem lv = new ListViewItem();
                    var mod = after[i];
                    lv.SubItems[0].Text = label1.Text + "机械手";
                    lv.SubItems.Add(mod.SortNum.ToString());
                    lv.SubItems.Add(mod.MainBelt.ToString());
                    lv.SubItems.Add(mod.CIGARETTDECODE.ToString());
                    lv.SubItems.Add(mod.CIGARETTDENAME.ToString());
                    //addPanel(i, mod.CIGARETTDENAME, aOrb);
                    lv.SubItems.Add(mod.qty.ToString());
                    lv.SubItems.Add("");
                    listViewAfter.Items.Add(lv);
                    
                  
                }
            }
        }
        /// <summary>  
        /// 机械手之后皮带
        /// </summary>
        /// <param name="before"></param>
        void listBeforeBind(List<UnionTaskInfo> before)
        {
            if (before != null && before.Count != 0)
            {
                bool aOrb = false;
                for (int i = 0; i < before.Count; i++)
                {
                    ListViewItem lv = new ListViewItem();
                    var mod = before[i];
                    lv.SubItems[0].Text = label1.Text + "机械手";
                    lv.SubItems.Add(mod.SortNum.ToString());
                    lv.SubItems.Add(mod.MainBelt.ToString());
                    lv.SubItems.Add(mod.CIGARETTDECODE.ToString());
                    lv.SubItems.Add(mod.CIGARETTDENAME.ToString());
                    //addPanel(i, mod.CIGARETTDENAME, aOrb);
                    lv.SubItems.Add(mod.qty.ToString());
                    lv.SubItems.Add(""); 
                    listViewBefore.Items.Add(lv);
                }
            }
           
        }

      

        private void pbMachine1_Click(object sender, EventArgs e)
        {
           
            fm_machinedetails.Show();
            fm_machinedetails.Activate();
            fm_machinedetails.TopLevel = true;
        }

        
    }
}
