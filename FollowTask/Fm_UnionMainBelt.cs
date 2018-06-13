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
using InBound;

namespace FollowTask
{
    public partial class Fm_UnionMainBelt : Form
    {

        AutoSizeFormClass asc = new AutoSizeFormClass();//自适应窗体
        Fm_FollowTaskMachineDetail fm_machinedetails ;
        public Fm_UnionMainBelt()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(Fm_UnionMainBelt_FormClosing);

            //  addPanel(400, "芙蓉王(硬)",6,false); 

            //addPanel(10, "1360151", "金圣(硬滕王阁)", 6, false);

            fm_machinedetails = new Fm_FollowTaskMachineDetail(label1.Text);
        
        }
        /// <summary>
        /// 获取卷烟图片
        /// </summary>
        /// <param name="cigraCode">卷烟编码</param>
        /// <returns>卷烟图片</returns>
        Bitmap GetImg(string cigraCode)
        {
            Bitmap cigreImg = (Bitmap)Properties.Resources.ResourceManager.GetObject("_"+cigraCode);
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
        /// <param name="cigarecode">卷烟名称</param>
        /// <param name="qty">卷烟条数</param>
        /// <param name="aOrb">true是之后 false是之前</param>
        public void addPanel(int count, string cigarecode, string cigareName, int qty, bool aOrb)
        { 
            PictureBox img = new PictureBox();
            Label lbl = new Label();
            lbl.Text = qty.ToString();
            lbl.BackColor = Color.Transparent;
            lbl.Font = new System.Drawing.Font("宋体", 10, FontStyle.Regular);
            img.Name = "ImgName" + Guid.NewGuid().ToString();
            img.Size = new System.Drawing.Size(20, 80);
            img.AccessibleName = cigareName + "|" + qty + "|" + cigarecode;//卷烟名称 和 QTY
            img.BackgroundImage = GetImg(cigarecode);
            img.SizeMode = PictureBoxSizeMode.Zoom;
            img.BorderStyle = BorderStyle.FixedSingle;
            img.MouseEnter += new EventHandler(img_MouseEnter);

            if (aOrb)
            {
                img.Location = new Point(count * img.Width + 10 * count, 0);
                lbl.Location = new Point(img.Width / 2 - 4, 10);
                panelafter.Controls.Add(img);//之后
                img.Controls.Add(lbl);
            }
            else
            {
                img.Location = new Point(count * img.Width + 10 * count, 0);
                lbl.Location = new Point(img.Width / 2 - 4, -2);
                img.Controls.Add(lbl);
                panelbefore.Controls.Add(img);//之前
            }


        }
        ToolTip p = new ToolTip();
        void img_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = ((PictureBox)sender);
            p.AutoPopDelay = 24000;
            p.ShowAlways = true;
            string[] strCigaNameAndQty = new string[3]; 
            strCigaNameAndQty =    pb.AccessibleName.Split('|');
            p.SetToolTip(pb, "卷烟名称:" + strCigaNameAndQty[0] + "\r\n" + "卷烟编号:" + strCigaNameAndQty[2] + "\r\n" + "总数：" + strCigaNameAndQty[1]);
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
        static int xynum = -1;//吸烟数量 来自DB块的值
        static decimal sortnum = -1;//任务号 来自db块的值
        /// <summary>
        /// 
        /// </summary>
        /// <param name="machineno">当前机械手号</param>
        /// <param name="after">之后List</param>
        /// <param name="before">之前List</param>
        /// <param name="xyNum">吸烟数量</param>
        /// <param name="Sortnum">任务号</param>
        public void GetNeedInfo(int machineno, List<UnionTaskInfo> after, List<UnionTaskInfo> before,int xyNum,decimal Sortnum)
        {
            try
            {
                listViewAfter.Items.Clear();
                listViewBefore.Items.Clear();
                label1.Text = machineno + "";
                gbpanelBefore.Text = machineno + "号机械手之前卷烟摆放";
                gbpanelAfter.Text = machineno + "号机械手之后卷烟摆放";
                groupBoxAfter.Text = machineno + "号机械手之后卷烟数据";
                groupBoxBefore.Text = machineno + "号机械手之前卷烟数据";
                xynum = xyNum;
                sortnum = Sortnum;
                if (after != null || before != null)
                {
                    listAfterBind(after);

                    listBeforeBind(before);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           
        }
        /// <summary>
        /// 机械手之后皮带
        /// </summary>
        /// <param name="after"></param>
        void listAfterBind(List<UnionTaskInfo> after)
        {

            for (int i = 0; i < after.Count; i++)
            {
                ListViewItem lv = new ListViewItem();
                var mod = after[i];
                lv.SubItems[0].Text = label1.Text + "机械手";
                lv.SubItems.Add(sortnum.ToString());
                lv.SubItems.Add(mod.MainBelt.ToString());
                lv.SubItems.Add(mod.CIGARETTDECODE.ToString());
                lv.SubItems.Add(mod.CIGARETTDENAME.ToString());
                addPanel(i, mod.CIGARETTDECODE, mod.CIGARETTDENAME, (int)mod.qty,true);
                lv.SubItems.Add(mod.qty.ToString());
                lv.SubItems.Add(xynum.ToString());
                lv.SubItems.Add("");
                listViewAfter.Items.Add(lv);


            }

        }
        /// <summary>  
        /// 机械手之前皮带
        /// </summary>
        /// <param name="before"></param>
        void listBeforeBind(List<UnionTaskInfo> before)
        {

            for (int i = 0; i < before.Count; i++)
            {
                ListViewItem lv = new ListViewItem();
                var mod = before[i];
                lv.SubItems[0].Text = label1.Text + "机械手";
                lv.SubItems.Add(sortnum.ToString());
                lv.SubItems.Add(mod.MainBelt.ToString());
                lv.SubItems.Add(mod.CIGARETTDECODE.ToString());
                lv.SubItems.Add(mod.CIGARETTDENAME.ToString());
                addPanel(i, mod.CIGARETTDECODE, mod.CIGARETTDENAME, (int)mod.qty,false);
                lv.SubItems.Add(mod.qty.ToString());
                lv.SubItems.Add(xynum.ToString());
                lv.SubItems.Add("");
                listViewBefore.Items.Add(lv);
            }


        }

      

        private void pbMachine1_Click(object sender, EventArgs e)
        {
           
            fm_machinedetails.Show();
            fm_machinedetails.Activate();
            fm_machinedetails.TopLevel = true;
        }

        private void Fm_UnionMainBelt_Load(object sender, EventArgs e)
        {
            asc.controllInitializeSize(this);
        }

        private void Fm_UnionMainBelt_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        private void listViewBefore_SizeChanged(object sender, EventArgs e)
        {
             
        }

     

        
    }
}
