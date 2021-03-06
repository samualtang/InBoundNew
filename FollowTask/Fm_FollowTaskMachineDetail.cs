﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound;
//using Machine;
using OpcRcw.Da;
using FollowTask.Modle;

namespace FollowTask
{
    public partial class Fm_FollowTaskMachineDetail : Form
    {
        public Fm_FollowTaskMachineDetail()
        {
            InitializeComponent();
            this.listViewMachineDetails.DoubleBufferedListView(true);
            this.WindowState = FormWindowState.Normal;
        }
        #region 图片 
          //string imgZhuaQu = Application.StartupPath + @" \Resources\抓取.bmp";
          //string imgYouYan = Application.StartupPath + @"  \Resources\有烟.bmp";
          //string imgWuYan = Application.StartupPath + @" \Resources\无烟.bmp";
          //string imgGuZhang = Application.StartupPath + @" \Resources\故障.bmp";
          Bitmap imgZhuaQu = (Bitmap)Properties.Resources.ResourceManager.GetObject("抓取1");
          Bitmap imgYouYan = (Bitmap)Properties.Resources.ResourceManager.GetObject("有烟1");
          Bitmap imgWuYan = (Bitmap)Properties.Resources.ResourceManager.GetObject("无烟1");
          Bitmap imgGuZhang = (Bitmap)Properties.Resources.ResourceManager.GetObject("故障1");

        #endregion 
          
        /// <summary>
        /// 预分拣机械手
        /// </summary>
        /// <param name="machineNo"></param>
        /// <param name="listgroup"></param>
        public Fm_FollowTaskMachineDetail(string machineNo ,List<Group> listgroup)
        {
            InitializeComponent();
             this.listViewMachineDetails.DoubleBufferedListView(true);
   
            #region 初始化
            if (machineNo.Substring(0,3) =="机械手")//烟柜机械手
            {
                lblCigreName.Text = "香烟";//预分拣机械手
                for (int i = 1; i <= 10; i++)
                {
                    string lblName = "lblCig" + i;
                    Control contr = (Label)Controls.Find(lblName, true)[0];
                    contr.Visible = false;
                }
            }
         
            string machiname = System.Text.RegularExpressions.Regex.Replace(machineNo,  @"[^0-9]+", "");
            Text ="预分拣("+machiname +"号机械手)"; 
            this.StartPosition = FormStartPosition.CenterScreen;
            lblMachineNo.Text = machiname + "号机械手";
            #endregion 
        }
     
        /// <summary>
        /// 合流机械手
        /// </summary>
        /// <param name="machineNo"></param>
        //public Fm_FollowTaskMachineDetail(string machineNo)
        //{
        //    InitializeComponent();
        //    this.listViewMachineDetails.DoubleBufferedListView(true);
        //    lblCigreName.Visible = false;//合流机械手
        //    for (int i = 1; i <= 10; i++)
        //    {
        //        string lblName = "lblCig" + i;
        //        Control contr = (Label)Controls.Find(lblName, true)[0];
        //        contr.Visible = true;
        //    } 
        //    Text ="合流("+ machineNo + "号机械手)";
        //    this.StartPosition = FormStartPosition.CenterScreen;
        //    lblMachineNo.Text = machineNo + "号机械手";
        //}
        int MainBelt;//主皮带
        int MachineNo;//机械收号
        int GroupNo;//组号
        bool isOnLine;//是否连线
        List<Group> Listmachine = new List<Group>();
        public void GetUnionMachineDetails(int  machineno,int mainbelt, List<Group> listMachine,bool isonline)
        {
            isOnLine = isonline;
            if (isOnLine)
            {
                this.listViewMachineDetails.DoubleBufferedListView(true);
                lblCigreName.Visible = false;//合流机械手 
                MainBelt = mainbelt;
                MachineNo = machineno;
                GroupNo = GetGroupNo(machineno);
                Text = "合流( " + machineno + "  号机械手)";
                this.StartPosition = FormStartPosition.CenterScreen;
                lblMachineNo.Text = "合流(" + machineno + "号机械手)";
                Listmachine = listMachine;
                Bind();
            }
            else
            {
                MessageBox.Show("服务器尚未连接！");
                this.Close();
            }
        }
        /// <summary>
        /// 获取组号
        /// </summary>
        /// <param name="machineNo">机械手号</param>
        /// <returns></returns>4   
        int GetGroupNo(int machineNo)
        {
            if (machineNo >= 8)
            {
                GroupNo = machineNo % 8;// Convert.ToDecimal(Math.IEEERemainder(machineNo, 8));//取余获得组号
            }
            else
            {
                GroupNo = machineNo;
            }
            if (GroupNo == 0)
            {
                GroupNo = 8;
            }
            return GroupNo;
        }
        private void Fm_FollowTaskMachineDetail_Load(object sender, EventArgs e)
        {
            //if (isOnLine)
            //{

            //    Bind();
            //}
            //else
            //{
            //    MessageBox.Show("服务器尚未连接！");
            //    this.Close();
            //}
        }
        void Bind()
        {
            decimal[] all = ReadDbInFo(MainBelt, MachineNo);//合流机械手 任务信息
            decimal sortnumAll = all[0];//当前任务号
            decimal xynumAll = all[1];   //总抓数  
            decimal xynum = all[2];//当前已抓数 
            decimal Dropxynum = all[3];//累计已放烟数量
            txtPokenum.Text = xynum + "";
            txtSortnum.Text = sortnumAll+"";
            int lablindex = 1;
            if (Dropxynum ==  xynumAll  )
            {
                lblDropORnot.Text = "已放下";
                BindXipan(0);
            }
            else
            {
                lblDropORnot.Text = "正抓取";
                BindXipan(0);
                List<InBound.Model.FollowTaskDeail> list = InBound.Business.FolloTaskService.GetUnionMachineInfo(sortnumAll, MainBelt, GroupNo, xynumAll, xynum);
                if (list != null)
                {
                    list = list.Take(10).ToList();
                    foreach (var item in list)
                    {

                        string lblName = "lblCig" + lablindex;
                        Control contr = (Label)Controls.Find(lblName, true)[0];
                        contr.Text = item.CIGARETTDENAME;
                        lablindex++;
                    }
                    BindXipan(list.Count);
                    // ListBind(list);
                }
                else
                {
                    BindXipan(0);
                }
            }
        }
        void BindXipan(int count)
        {
            if (count == 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    string lblName = "lblCig" + i;
                    Control contr = (Label)Controls.Find(lblName, true)[0];
                    contr.Text = ""; 
                } 
           
            }
            
            int[] pan = new int[10] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            int[] state = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] zhua = new int[10] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

            for (int i = 0; i < count; i++)
            {
                state[i] = 0;
                zhua[i] = 0;
            }
            pbBind(pan, state, zhua); 
        }
        void ListBind(List<InBound.Model.FollowTaskDeail> list)
        {
            if (list != null && list.Count > 0)
            {

                for (int i = 0; i < list.Count; i++)
                {
                    ListViewItem lv = new ListViewItem();
                    var mod = list[i];
                    lv.SubItems[0].Text = mod.SortNum.ToString();//任务号
                    lv.SubItems.Add(mod.qty.ToString());//数量 
                    listViewMachineDetails.Items.Add(lv);
                }
            }

        }
        
        #region 吸盘
        /// <summary >
        /// 吸盘信息绑定
        /// </summary>
        /// <param name="xipan">吸盘编号</param>
        /// <param name="xipanSate">吸盘状态</param>
        /// <param name="zhuayan">吸盘抓烟状态</param>
        void pbBind(int[] xipan, int[] xipanSate, int[] zhuayan)
        {
            for (int i = 0; i < xipan.Length; i++)
            {
                if (xipan[i] != -1)
                {
                    if (xipanSate[i] == 0)//0为运行 有烟或者无烟 
                    {
                        pbStateBind(i, imgZhuaQu);
                        if (zhuayan[i] == 0)//有烟
                        {
                            pbSomkeBind(i, imgYouYan);
                        }
                        else if (zhuayan[i] == 1)//无烟
                        {
                            pbSomkeBind(i, imgWuYan);
                        }
                    }
                    else if (xipanSate[i] == 1)//  否则故障一定无烟
                    {
                        pbStateBind(i, imgGuZhang);
                        pbSomkeBind(i, imgWuYan); 
                    }  
                }
            } 
        }

        /// <summary>
        /// 吸盘抓烟信息图片显示
        /// </summary>
        /// <param name="index">索引(多少号图片)</param>
        /// <param name="imgpath"> 图片路径 </param>
        void pbSomkeBind(int index, Bitmap bitmap)
        {
            string btnName = "pbSokme" + (index+1);
            Control contrl = (PictureBox)Controls.Find(btnName, true)[0];
            contrl.BackgroundImage = bitmap;
            contrl.BackgroundImageLayout = ImageLayout.Stretch;

        }

        /// <summary>
        /// 吸盘状态信息图片显示
        /// </summary>
        /// <param name="index">索引(多少号图片)</param>
        /// <param name="imgpath"> 图片路径 </param>
        void pbStateBind(int index, Bitmap bitmap)
        {

            string btnName = "pbState" + (index+1);
            Control contrl = (PictureBox)Controls.Find(btnName, true)[0];
            contrl.BackgroundImage = bitmap;
            contrl.BackgroundImageLayout = ImageLayout.Stretch;
        }
        #endregion

        private void listViewMachineDetails_SizeChanged(object sender, EventArgs e)
        {

            int _Count = listViewMachineDetails.Columns.Count;
            int _Width = listViewMachineDetails.Width;
            foreach (ColumnHeader ch in listViewMachineDetails.Columns)
            {
                ch.Width = _Width / _Count - 1;
            }
        }
        /// <summary>
        /// 读取DB当前任务号和当前抓数
        /// </summary>
        /// <param name="mainbelt">主皮带</param>
        /// <param name="machineno">机械手号</param>
        /// <returns></returns>
        decimal[] ReadDbInFo(int mainbelt, int machineno)
        {
            decimal[] sortnumAndXYnum = new decimal[4];

            sortnumAndXYnum[0] = Listmachine[4].ReadD(((2 * machineno) - 2)).CastTo<int>(-1);//机械手的任务号  
            sortnumAndXYnum[1] = Listmachine[4].ReadD(((2 * machineno) - 1)).CastTo<int>(-1);//总抓数 
            sortnumAndXYnum[2] = Listmachine[5].ReadD(((2 * machineno) - 1)).CastTo<int>(-1);//当前抓数 
            sortnumAndXYnum[3] = Listmachine[6].ReadD( machineno  + 31).CastTo<int>(-1);//当前机械手累计放烟数量
            return sortnumAndXYnum;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Bind();
        }

      
    }
}
