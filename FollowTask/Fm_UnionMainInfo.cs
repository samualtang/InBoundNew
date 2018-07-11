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
            txtTitle.Text ="合流"+ mainbelt + "号主皮带";
            MainBelt = mainbelt;
            listMainBelt = list;
            

        }
       
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
                    info.SortNum = Sortnum;//任务号
                    info.Place = (listMainBelt[mainbelt - 1].ReadD((ReadIndex + 1)).CastTo<int>(-1) / 1000);//位置(米)
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
            if (index < ListmbInfo.Count)
            {
                panelCig.Controls.Clear();//清空panel控件数据
                dgvMainBeltInfo.DataSource = null;//重置数据显示控件
                ListmbInfo = ListmbInfo.OrderBy(a => a.Place).ToList();//距离从大到小排序
                if (ListmbInfo[index].taskInfo != null && ListmbInfo[index].taskInfo.Count > 0)//当数据不为空
                {
                    dgvMainBeltInfo.DataSource = ListmbInfo[index].taskInfo.Select(x => new
                    {
                        CIGARETTECODE = x.CIGARETTDECODE,
                        CIGARETTNAME = x.CIGARETTDENAME,
                        QTY = x.qty,
                        MAINBELT = x.MainBelt,
                        SORTNUM = x.SortNum,
                    }).ToList();//根据索引读取相对应数据   
                    DgvBind();
                    addPanel(ListmbInfo[index].taskInfo);//往panel控件增添当前数据
                    lblSortnum.Text = "任务号：" + ListmbInfo[index].SortNum;
                    lblNum.Text = "数量：" + ListmbInfo[index].Quantity;
                    lblPlace.Text = "当前位置：" + ListmbInfo[index].Place + "米";
                } 
                lblCOunt.Text = "总批次：" + ListmbInfo.Count;
                lblNowcOUNT.Text = "当前批次:" + (index + 1) + "/" + ListmbInfo.Count;
            }
        }
        /// <summary>
        /// 绑定Dgv列头显示
        /// </summary>
        void DgvBind()
        {
            try
            { 
                dgvMainBeltInfo.Columns[0].HeaderCell.Value = "香烟编号";
                dgvMainBeltInfo.Columns[1].HeaderCell.Value = "香烟名称";
                dgvMainBeltInfo.Columns[2].HeaderCell.Value = "数量";
                dgvMainBeltInfo.Columns[3].HeaderCell.Value = "主皮带";
                dgvMainBeltInfo.Columns[4].HeaderCell.Value = "任务号";
            }
            catch (ArgumentOutOfRangeException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
       
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (ReadIndex == 0)
            {
                ReadListInfo(0);
                MessageBox.Show("是第一批了"); 
                return;
            }
            else
            {
                ReadIndex = ReadIndex - 1;
                ReadListInfo(ReadIndex);
             
             
            }
          

        }
        private void btnNext_Click(object sender, EventArgs e)//点击下一个
        {
            if (ReadIndex > (ListmbInfo.Count -1))
            {
               // ReadListInfo(ListmbInfo.Count);  
                MessageBox.Show("最后一批了");
                return;
            }
            else
            {
                ReadIndex = ReadIndex + 1;
                ReadListInfo(ReadIndex); 
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
                dgvMainBeltInfo.DataSource = listunion;
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
          
           
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            dgVprint1.MainTitle = "合流" + MainBelt + "号主皮带表";
            //dgVprint1.SubTitle = "这是子标题，当然也可以不设的";
            // dgVprint1.PaperLandscape = true;//用横向打印，默认是纵向

            dgVprint1.Print(dgvMainBeltInfo);
        }

        private void Fm_UnionMainInfo_SizeChanged(object sender, EventArgs e)
        {
         
        }
       
       
    }
}
