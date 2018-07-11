using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FollowTask.Modle;
using InBound;
using InBound.Model;
using InBound.Business;
namespace FollowTask
{
    public partial class Fm_UnionMainInfo : Form
    {
        public Fm_UnionMainInfo()
        {
            InitializeComponent();
         
        }
        /// <summary>
        /// 主皮带信息集合
        /// </summary>
        List<MainBeltInfo> ListmbInfo = new List<MainBeltInfo>();
      
        /// <summary>
        /// 读取索引
        /// </summary>
        static int ReadIndex = 0;
        
        /// <summary>
        /// LIST DB块
        /// </summary>
        List<Group> listMainBelt = new List<Group>();
        /// <summary>
        /// 主皮带号
        /// </summary>
        static int MainBelt = 0;
        /// <summary>
        ///  任务号 
        /// </summary>
        static decimal Sortnum ;

        /// <summary>
        /// 获取卷烟图片
        /// </summary>
        /// <param name="cigraCode">卷烟编码</param>
        /// <returns>卷烟图片</returns>
        Bitmap GetImg(string cigraCode)
        {
            Bitmap cigreImg = (Bitmap)Properties.Resources.ResourceManager.GetObject("_" + cigraCode);
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
        public void addPanel(List<UnionTaskInfo> before)
        {
            panelCig.Controls.Clear();
            for (int i = 0; i < before.Count; i++)
            { 
                PictureBox img = new PictureBox();
                Label lbl = new Label();
                lbl.Text = before[i].qty.ToString();
                lbl.BackColor = Color.Transparent;
                lbl.Font = new System.Drawing.Font("宋体", 10, FontStyle.Regular);
                img.Name = "ImgName" + Guid.NewGuid().ToString();
                img.Size = new System.Drawing.Size(20, 80);
                img.AccessibleName = before[i].CIGARETTDENAME + "|" + before[i].qty + "|" + before[i].CIGARETTDECODE;//卷烟名称 和 QTY
                img.BackgroundImage = GetImg(before[i].CIGARETTDECODE);
                img.SizeMode = PictureBoxSizeMode.Zoom;
                img.BorderStyle = BorderStyle.FixedSingle;
                img.MouseEnter += new EventHandler(img_MouseEnter); 
                img.Location = new Point(i * img.Width + 10 * i, 0);
                lbl.Location = new Point(img.Width / 2 - 4, 0);
                panelCig.Controls.Add(img);//之后
                img.Controls.Add(lbl);
            }
        }
        ToolTip p = new ToolTip();
        void img_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = ((PictureBox)sender);
            p.AutoPopDelay = 24000;
            p.ShowAlways = true;
            string[] strCigaNameAndQty = new string[3];
            strCigaNameAndQty = pb.AccessibleName.Split('|');
            p.SetToolTip(pb, "卷烟名称:" + strCigaNameAndQty[0] + "\r\n" + "卷烟编号:" + strCigaNameAndQty[2] + "\r\n" + "总数：" + strCigaNameAndQty[1]);
        }
        public void GetMainInfo(int mainbelt, List<Group> list)
        {
            Text = mainbelt + "号主皮带";
            MainBelt = mainbelt;
            listMainBelt = list;

        }
        double[] nowplace = new double[140];
        /// <summary>
        /// 读取DB块上的任务号，位置，数量
        /// </summary>
        /// <param name="index">读取索引</param>
        /// <param name="mainbelt">主皮带</param>
        void ReadDBinfo(int mainbelt)
        {
            ListmbInfo.Clear(); //清空list
            int ReadIndex = 0;
        
            for (int i = 0; i < 40; i++)//从电控读取数据 填充 listmbinfo
            {
                Sortnum = listMainBelt[mainbelt - 1].ReadD(ReadIndex).CastTo<int>(0);//任务号
             
              
                if (Sortnum > 0)//任务号不为0
                {
                    MainBeltInfo info = new MainBeltInfo();
                    nowplace[i] = (listMainBelt[mainbelt - 1].ReadD((ReadIndex + 1)).CastTo<int>(-1) / 1000);//位置(米)
                    info.SortNum = Sortnum;//任务号
                    info.Place = Convert.ToDecimal(nowplace[i]);//(listMainBelt[mainbelt - 1].ReadD((ReadIndex + 1)).CastTo<int>(-1) / 1000000);//位置(米)
                    info.Quantity =Convert.ToDecimal( listMainBelt[mainbelt - 1].ReadD((ReadIndex + 2)).CastTo<int>(-1));//数量
                    info.mainbelt = mainbelt.ToString();//主皮带
                    ListmbInfo.Add(info);
                }
                ReadIndex = ReadIndex + 3;
            }
            MainBeltInfoService.GetMainBeltInfo(ListmbInfo); //填充完成之后传进方法 计算 ，
        }
    
        /// <summary>
        /// 读取list
        /// </summary>
        /// <param name="index">任务号的索引</param>
        void ReadListInfo(int index)
        {
            panelCig.Controls.Clear();
            dgbMainBeltInfo.DataSource = null;//重置数据显示控件
        
            if (ReadIndex < ListmbInfo.Count)
            {
            
                if (ListmbInfo[index].taskInfo != null && ListmbInfo[index].taskInfo.Count > 0)//当数据不为空
                {
                    var list= ListmbInfo[index].taskInfo.Select(x => new
                    {
                        CIGARETTECODE = x.CIGARETTDECODE,
                        CIGARETTNAME = x.CIGARETTDENAME,
                        QTY = x.qty,
                        MAINBELT = x.MainBelt, 
                        SORTNUM = x.SortNum, 
                        
                    }).ToList();//根据索引读取相对应数据
                    dgbMainBeltInfo.DataSource = list;
                    DgvBind();
                    addPanel(ListmbInfo[index].taskInfo);
                    lblSortnum.Text = "任务号：" + ListmbInfo[index].SortNum;
                    lblNum.Text = "数量：" + ListmbInfo[index].Quantity;
                }
            }
          
        }

        void DgvBind()
        {
            dgbMainBeltInfo.Columns[0].HeaderCell.Value = "香烟编号";
            dgbMainBeltInfo.Columns[1].HeaderCell.Value = "香烟名称";
            dgbMainBeltInfo.Columns[2].HeaderCell.Value = "数量";
            dgbMainBeltInfo.Columns[3].HeaderCell.Value = "主皮带";
            dgbMainBeltInfo.Columns[4].HeaderCell.Value = "任务号";
            //dgbMainBeltInfo.Columns[4].HeaderCell.Value = "数量";
            //dgbMainBeltInfo.Columns[5].HeaderCell.Value = "组号";
            //dgbMainBeltInfo.Columns[6].HeaderCell.Value = "物理通道号";
            //dgbMainBeltInfo.Columns[6].HeaderCell.Value = "客户名称"; 
            //dgbMainBeltInfo.Columns[6].HeaderCell.Value = "客户编号"; 
            //dgbMainBeltInfo.Columns[6].HeaderCell.Value = "排序号"; 
        }
       
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (ReadIndex == 0)
            {
                MessageBox.Show("最上面了");
                ReadListInfo(0);
                return;
            }
            else
            {
                ReadIndex = ReadIndex - 1;
                ReadListInfo(ReadIndex);
                lblPlace.Text = "当前位置：" + nowplace[ReadIndex] + "米";
               // MessageBox.Show("当前位置"+nowplace);
            }
          

        }
        private void btnNext_Click(object sender, EventArgs e)//点击下一个
        {
            if (ReadIndex == ListmbInfo.Count)
            {
                ReadListInfo(ListmbInfo.Count);
                MessageBox.Show("最下面了"); 
                return;
            }
            else
            {
                ReadIndex = ReadIndex + 1;
                ReadListInfo(ReadIndex);
                lblPlace.Text = "当前位置：" + nowplace[ReadIndex] + "米";
                //MessageBox.Show("当前位置" + nowplace);
                
            }
         
        }

        private void btnAllInfo_Click(object sender, EventArgs e)
        { 
            List<UnionTaskInfo> listunion = new List<UnionTaskInfo>(); 
            for (int i = 0; i < ListmbInfo.Count; i++)
            {
                if (ListmbInfo[i].taskInfo != null && ListmbInfo[i].taskInfo.Count > 0)//当数据不为空
                {
                    UnionTaskInfo un = new UnionTaskInfo();
                    un.CIGARETTDECODE = ListmbInfo[i].taskInfo[i].CIGARETTDECODE;
                    un.CIGARETTDENAME = ListmbInfo[i].taskInfo[i].CIGARETTDENAME;
                    un.qty = ListmbInfo[i].taskInfo[i].qty;
                    un.MainBelt = ListmbInfo[i].taskInfo[i].MainBelt;
                    un.SortNum = ListmbInfo[i].taskInfo[i].SortNum;
                    listunion.Add(un);
                }
                DgvBind();
                dgbMainBeltInfo.DataSource = listunion;
            }  
        }
        private void Fm_UnionMainInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is Form)
                {
                    //fm_machinedetails.Close();
                    return;
                }
            }
            this.Close();
        }

        private void Fm_UnionMainInfo_Load(object sender, EventArgs e)
        {
            lblSortnum.Text = "任务号：0";
            lblNum.Text = "数量：0";
            ReadDBinfo(MainBelt);
            ReadListInfo(0);
            lblPlace.Text = "当前位置：" + nowplace[0]+"米"; 
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }
       
       
    }
}
