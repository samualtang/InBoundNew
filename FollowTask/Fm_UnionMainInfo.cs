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
using System.Threading;
namespace FollowTask
{
    public partial class Fm_UnionMainInfo : Form
    {
        public Fm_UnionMainInfo()
        {
            InitializeComponent();
            dgvMainBeltInfo.DoubleBufferedDataGirdView(true);
            CheckForIllegalCrossThreadCalls = false;
            WindowState = FormWindowState.Maximized;
            pbLoading.VisibleChanged += new EventHandler(pbLoading_VisibleChanged);
        }
        /// <summary>
        /// 主皮带信息集合
        /// </summary>
        List<MainBeltInfo> ListmbInfo = new List<MainBeltInfo>();
      
        /// <summary>
        /// 读取索引
        /// </summary>
        static int ReadIndexByBtn = 0;
        
        /// <summary>
        /// LIST DB块
        /// </summary>
      static   List<Group> listMainBelt = new List<Group>();
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
            //for (int i = 0; i < before.Count; i++)
            //{ 
              
            //    PictureBox img = new PictureBox();
            //    Label lbl = new Label();
            //    lbl.Text = before[i].qty.ToString();
            //    lbl.BackColor = Color.Transparent;
            //    lbl.Font = new System.Drawing.Font("宋体", 10, FontStyle.Regular);
            //    img.Name = "ImgName" + Guid.NewGuid().ToString();
            //    img.Size = new System.Drawing.Size(20, 80);
            //    img.AccessibleName = before[i].CIGARETTDENAME + "|" + before[i].qty + "|" + before[i].CIGARETTDECODE;//卷烟名称 和 QTY
            //    img.BackgroundImage = GetImg(before[i].CIGARETTDECODE);
            //    img.SizeMode = PictureBoxSizeMode.Zoom;
            //    img.BorderStyle = BorderStyle.FixedSingle;
            //    img.MouseEnter += new EventHandler(img_MouseEnter); 
            //    img.Location = new Point(i * img.Width + 10 * i, 0);
            //    lbl.Location = new Point(img.Width / 2 - 4, 0);
               
            //    panelCig.Controls.Add(img);//之后
            //    img.Controls.Add(lbl);
            //}
            int i = 0;
            foreach (var item in before)
            {
                PictureBox img = new PictureBox();
                Label lbl = new Label();
                lbl.Text =item.POKENUM.ToString();
                lbl.BackColor = Color.Transparent;
                lbl.Font = new System.Drawing.Font("宋体", 10, FontStyle.Regular);
                img.Name = "ImgName" + Guid.NewGuid().ToString();
                img.Size = new System.Drawing.Size(20, 80);
                img.AccessibleName = item.CIGARETTDENAME + "|" + item.POKENUM + "|" + item.CIGARETTDECODE;//卷烟名称 和 QTY
                img.BackgroundImage = GetImg(item.CIGARETTDECODE);
                img.SizeMode = PictureBoxSizeMode.Zoom;
                img.BorderStyle = BorderStyle.FixedSingle;
                img.MouseEnter += new EventHandler(img_MouseEnter);
                img.Location = new Point(i * img.Width + 10 * i, 0);
                lbl.Location = new Point(img.Width / 2 - 4, 0);
                if (item.IsOnMainBelt == 0)//不在皮带上
                {
                   // img.BorderStyle = BorderStyle.Fixed3D;
                    lbl.BackColor = Color.Red;
                    //img.BackColor = Color.Red;
                }
                panelCig.Controls.Add(img);
                img.Controls.Add(lbl);
                i++;
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
        delegate void Handledelegate();
        public void GetMainInfo(int mainbelt, List<Group> list,bool isonline)
        {
            try
            { 
                if (isonline)
                {
                    Text = mainbelt + "号主皮带";
                    txtTitle.Text = "合流" + mainbelt + "号主皮带";
                    MainBelt = mainbelt;
                    listMainBelt = list;
                    lblSortnum.Text = "任务号：0";
                    lblNum.Text = "数量：0";
                    Handledelegate task = ThreadRead;
                    task.BeginInvoke(null, null);
                }
                else
                {
                    lblErorr.Visible = true;
                    lblErorr.Text = "错误信息:服务器连接失败";
                }
            }
            catch (Exception ex)
            {
                lblErorr.Visible = true;
                lblErorr.Text = "错误信息:服务器连接失败" + ex.Message;
               
            }

        }
        void ThreadRead()
        { 
            ReadIndexByBtn = 0;
            ReadDBinfo(MainBelt);//读取DB块上的值 
        }



        /// <summary>
        /// 读取DB块上的任务号，位置，数量,机械手任务号，机械手放烟数量
        /// </summary>
        /// <param name="index">读取索引</param>
        /// <param name="mainbelt">主皮带</param>
        void ReadDBinfo(int mainbelt)
        {
            ListmbInfo.Clear(); //清空list
            dgvMainBeltInfo.DataSource = null;
            panelCig.Controls.Clear();
            int ReadIndex = 0;//读取DB索引
            List<decimal> SortNumList = new List<decimal>();
            List<decimal> QuantityList = new List<decimal>();
            for (int i = (mainbelt - 1) * 8; i < mainbelt * 8; i++)
            {
                SortNumList.Add(listMainBelt[6].ReadD(i).CastTo<decimal>(0));//任务号
                QuantityList.Add(listMainBelt[6].ReadD(32 + i).CastTo<decimal>(0));//放烟数量
            }
            for (int i = 0; i < 40; i++)//从电控读取数据 填充 listmbinfo
            {
                Sortnum = listMainBelt[mainbelt - 1].ReadD(ReadIndex).CastTo<int>(0);//任务号 
                if (Sortnum > 0)//任务号不为0
                {
                    MainBeltInfo info = new MainBeltInfo(); 
                    info.SortNum = Sortnum;//任务号
                    info.Place = (listMainBelt[mainbelt - 1].ReadD((ReadIndex + 1)).CastTo<decimal>(-1) / 1000);//位置(米)
                    info.Quantity =Convert.ToDecimal( listMainBelt[mainbelt - 1].ReadD((ReadIndex + 2)).CastTo<int>(-1));//数量
                    info.mainbelt = mainbelt.ToString();//主皮带
                    info.SortNumList = SortNumList;//机械手任务号
                    info.QuantityList = QuantityList;//机械手放烟数量
                    ListmbInfo.Add(info);
                }
                ReadIndex = ReadIndex + 3;
            }
            MainBeltInfoService.GetMainBeltInfo(ListmbInfo); //填充完成之后传进方法 计算 ，
            ListmbInfo = ListmbInfo.OrderBy(x=>x.Place).ToList();// ListmbInfo.OrderBy(a => a.Place).ThenBy(a => a.SortNum).ToList();//对距离任务号进行排序
            pbLoading.Visible = false; 
        }
    
        /// <summary>
        /// 读取list
        /// </summary>
        /// <param name="index">任务号的索引</param>
        void ReadListInfo(int index)
        {
            if (index <= ListmbInfo.Count && ListmbInfo.Count >0)
            {
                groupBoxUnionInfo.Visible = true;
                panelCig.Controls.Clear();//清空panel控件数据
                dgvMainBeltInfo.DataSource = null;//重置数据显示控件
              
                if (ListmbInfo[index].taskInfo != null && ListmbInfo[index].taskInfo.Count > 0)//当数据不为空
                {
                    if (!string.IsNullOrWhiteSpace(ListmbInfo[index].MsgCode))//如有错误信息
                    {
                        lblErorr.Visible = true;
                        lblErorr.Text = "错误代码：" + ListmbInfo[index].MsgCode + "错误信息：" + ListmbInfo[index].ErrorMsg;
                    }
                    else
                    {
                        lblErorr.Visible = false;
                    }
                    dgvMainBeltInfo.DataSource = ListmbInfo[index].taskInfo.Select(x => new
                    {
                        CIGARETTECODE = x.CIGARETTDECODE,
                        CIGARETTNAME = x.CIGARETTDENAME,
                        QTY = x.POKENUM,
                        MAINBELT = x.MainBelt,
                        SORTNUM = x.SortNum,
                        IsOnBelt = x.IsOnMainBelt, 
                        MACHINESEQ = x.MainBelt +((x.MainBelt -1) *8),
                        PLACE = ListmbInfo[index].Place +"米",
                    }).ToList();//根据索引读取相对应数据   
                    DgvBind();
                    addPanel(ListmbInfo[index].taskInfo);//往panel控件增添当前数据
                    txtSortnum.Text = ListmbInfo[index].SortNum.ToString();
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
                dgvMainBeltInfo.Columns[5].HeaderCell.Value = "是否在主皮带";
                dgvMainBeltInfo.Columns[6].HeaderCell.Value = "机械手号";
                dgvMainBeltInfo.Columns[7].HeaderCell.Value = "位置";

            }
            catch (ArgumentOutOfRangeException ex)
            {

                 
            }
        }
       
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (ReadIndexByBtn == 0)
            {
                //ReadListInfo(0);
                MessageBox.Show("是第一批了"); 
                return;
            }
            else
            {
                ReadIndexByBtn = ReadIndexByBtn - 1;
                ReadListInfo(ReadIndexByBtn);
             
             
            }
          

        }
        private void btnNext_Click(object sender, EventArgs e)//点击下一个
        {
          
            if (ReadIndexByBtn >= (ListmbInfo.Count -1))
            {
               // ReadListInfo(ListmbInfo.Count);  
                MessageBox.Show("最后一批了");
                return;
            }
            else
            {
                ReadIndexByBtn = ReadIndexByBtn + 1;
                ReadListInfo(ReadIndexByBtn); 
            }
         
        }

        private void btnAllInfo_Click(object sender, EventArgs e)
        { 
            List<UnionTaskInfo> listunion = new List<UnionTaskInfo>();
            if (btnAllInfo.Text == "所 有")
            {
                if (ListmbInfo.Count > 0)
                {

                    groupBoxUnionInfo.Visible = false;
                    for (int i = 0; i < ListmbInfo.Count; i++)
                    {

                        for (int j = 0; j < ListmbInfo[i].taskInfo.Count; j++)
                        {
                            UnionTaskInfo un = new UnionTaskInfo();
                            if (ListmbInfo[i].taskInfo != null && ListmbInfo[i].taskInfo.Count > 0)//当数据不为空
                            {
                                if (ListmbInfo[i].taskInfo[j].IsOnMainBelt == 1)
                                {
                                    un.CIGARETTDECODE = ListmbInfo[i].taskInfo[j].CIGARETTDECODE;
                                    un.CIGARETTDENAME = ListmbInfo[i].taskInfo[j].CIGARETTDENAME;
                                    un.POKENUM = ListmbInfo[i].taskInfo[j].POKENUM;
                                    un.MainBelt = ListmbInfo[i].taskInfo[j].MainBelt;
                                    un.SortNum = ListmbInfo[i].taskInfo[j].SortNum;
                                    un.IsOnMainBelt = ListmbInfo[i].taskInfo[j].IsOnMainBelt;
                                    un.Place = ListmbInfo[i].Place;
                                    listunion.Add(un);
                                }
                            }
                        }


                    }
                   
                    dgvMainBeltInfo.DataSource = listunion.Select(x => new
                    {
                        CIGARETTECODE = x.CIGARETTDECODE,
                        CIGARETTNAME = x.CIGARETTDENAME,
                        QTY = x.POKENUM,
                        MAINBELT = x.MainBelt,
                        SORTNUM = x.SortNum,
                        IsOnBelt = x.IsOnMainBelt, 
                        Place = x.Place+"米",
                    }).ToList();//根据索引读取相对应数据  
                    DgvBind();
                    btnAllInfo.Text = "返 回";
                }
                else
                {
                    MessageBox.Show("当前没有数据");
                }
            }
            else
            {
                ReadListInfo(ReadIndexByBtn);
                btnAllInfo.Text = "所 有";
            }
        }
        private void Fm_UnionMainInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            //foreach (Form frm in Application.OpenForms)
            //{
            //    if (frm is Form)
            //    {
            //        //fm_machinedetails.Close();
            //        return;
            //    }
            //}
            //this.Close();
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

        private void btnCx_Click(object sender, EventArgs e)
        {
            if (btnCx.Text == "查询")
            {
                if (string.IsNullOrWhiteSpace(txtSortnum.Text))
                {
                    MessageBox.Show("不能为空");
                }
                else
                {
                    groupBoxUnionInfo.Visible = false;
                    dgvMainBeltInfo.DataSource = UnionTaskInfoService.GetUnionTaskInfo(Convert.ToDecimal(txtSortnum.Text))
                    .Select(x => new
                    {
                        CIGARETTECODE = x.CIGARETTDECODE,
                        CIGARETTNAME = x.CIGARETTDENAME,
                        QTY = x.POKENUM,
                        MAINBELT = x.MainBelt,
                        SORTNUM = x.SortNum,
                       MACHINESEQ = x.groupno + (( x.MainBelt -1) * 8),
                       ISONMAINBELT =  x.IsOnMainBelt > 0 ? "是":"否" ,
                      //  IsOnBelt = x.IsOnMainBelt,
                    }).ToList();//根据索引读取相对应数据  
                     // DgvBind();
                   DgvBind2();
                    btnCx.Text = "返回";
                }
            }
            else
            {
                ReadListInfo(ReadIndexByBtn);
                btnCx.Text = "查询";
            }
        }

        /// <summary>
        /// 绑定Dgv列头显示
        /// </summary>
        void DgvBind2()
        {
            try
            {
                dgvMainBeltInfo.Columns[0].HeaderCell.Value = "香烟编号";
                dgvMainBeltInfo.Columns[1].HeaderCell.Value = "香烟名称";
                dgvMainBeltInfo.Columns[2].HeaderCell.Value = "数量";
                dgvMainBeltInfo.Columns[3].HeaderCell.Value = "主皮带";
                dgvMainBeltInfo.Columns[4].HeaderCell.Value = "任务号";
                dgvMainBeltInfo.Columns[5].HeaderCell.Value = "机械手号";
                dgvMainBeltInfo.Columns[6].HeaderCell.Value = "是否在主皮带";
               // dgvMainBeltInfo.Columns[10].HeaderCell.Value = "是否在皮带";
            }
            catch (ArgumentOutOfRangeException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void txtSortnum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSortnum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCx_Click(sender, e);
            }
        }

        private void dgvMainBeltInfo_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                String statusText = "";
                switch (e.Value.ToString())
                {
                    case "1":
                        statusText = "是";
                        break;
                    case "0":
                        statusText = "否";
                        break;

                }
                e.Value = statusText;
            }
        }

       

        void pbLoading_VisibleChanged(object sender, EventArgs e)
        {
            if (IsHandleCreated)
            {
                this.Invoke(new EventHandler(delegate { ReadListInfo(0); }));
            }
        }

      
 
 
       
       
    }
}
