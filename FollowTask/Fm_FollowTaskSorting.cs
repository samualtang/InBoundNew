using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text; 
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;
using InBound;
using OpcRcw.Da;
using Machine;
using FollowTask.Modle;
using InBound.Business;
using InBound.Model;


namespace FollowTask
{
    public partial class Fm_FollowTaskSorting : Form
    {

        public Fm_FollowTaskSorting()
        {
            InitializeComponent();
          
        }

        public WriteLog writeLog = WriteLog.GetLog();

      
        List<Group> ListSort = new List<Group>();
        List<MainBeltInfo> ListmbInfo = new List<MainBeltInfo>();
      
   
        bool isOnLine = false;//服务器连接标识符
        decimal Sortnum;//任务号
   
        decimal groupno;//组号
        /// <summary>
        /// 读取索引
        /// </summary>
        static int ReadIndex = 0;
        public void GetSoringBeltInfo(string text, List<Group> list, bool isonline)
        {
            String OpcFJConnectionService = "S7:[FJCONNECTIONGROUP";//OPC服务器名
            groupno = Convert.ToDecimal(System.Text.RegularExpressions.Regex.Replace(text, @"[^0-9]+", ""));
            if (list[0] != null && list[1] != null)//在每次添加OPC Item之前清空 
            {
           
            }
            if (isonline)
            {
                OpcFJConnectionService = OpcFJConnectionService + GroupnotoBigg(groupno) + "]";
                list[0].addItem(ItemCollection.GetASortingItem(OpcFJConnectionService));
                list[1].addItem(ItemCollection.GetBSortingItem(OpcFJConnectionService));
                ListSort = list;
                isOnLine = isonline;

                if (groupno == 1 || groupno == 3 || groupno == 5 || groupno == 7)
                {

                    ReadDBinfo(1, groupno);

                }
                else
                {
                    ReadDBinfo(2, groupno);
                }
                ReadListInfo(0);
                Text = groupno + "组预分拣";
                txtTitle.Text = Text;
              
                //lblSortnum.Text = "任务号： 0";
                //lblNum.Text = "数量：0"; 
            }
            else
            {

                lblErorr.Text ="与服务器断开连接....";
            }
        }
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
                img.Controls.Add(lbl);
                panelCig.Controls.Add(img);//之后 
            }
            //倒着来
            //int index = before.Count;
            //for (int i = 0; i < before.Count; i++)
            //{
                
            //    PictureBox img = new PictureBox();
            //    Label lbl = new Label();
            //    lbl.Text = before[index].CIGARETTDECODE;
            //    lbl.BackColor = Color.Transparent;
            //    lbl.Font = new System.Drawing.Font("宋体", 10, FontStyle.Regular);
            //    img.Name = "ImgName" + Guid.NewGuid().ToString();
            //    img.Size = new System.Drawing.Size(20, 80);
            //    img.AccessibleName = before[index].CIGARETTDENAME + "|" + before[index].qty + "|" + before[index].CIGARETTDECODE;//卷烟名称 和 QTY
            //    img.BackgroundImage = GetImg(before[index].CIGARETTDECODE);
            //    img.SizeMode = PictureBoxSizeMode.Zoom;
            //    img.BorderStyle = BorderStyle.FixedSingle;
            //    img.MouseEnter += new EventHandler(img_MouseEnter);
            //    img.Location = new Point(i * img.Width + 10 * i, 0);
            //    lbl.Location = new Point(img.Width / 2 - 4, 0);
            //    img.Controls.Add(lbl);
            //    panelCig.Controls.Add(img);//之后 
            //    index--;
            //}
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
        /// <summary>
        /// 小组号变大组号
        /// </summary>
        /// <param name="group">小组</param>
        /// <returns></returns>
        decimal GroupnotoBigg(decimal group)
        {
            if (group ==2)
            {
                return 1;
            }
            if (group == 3 || group ==4)
            {
                return 2;
            }
            if (group == 5 || group == 6)
            {
                return 3;
            }
            if (group == 7|| group ==8)
            {
                return 4;
            }
            return 1;
        }
        /// <summary>
        /// 读取预分拣DB块
        /// </summary>
        /// <param name="index"></param>
        /// <param name="groupno"></param>
        void ReadDBinfo(int index,decimal groupno)
        {
            ListmbInfo.Clear(); //清空list
           // dgvSortingBeltInfo = null;
            panelCig.Controls.Clear();
            int ReadIndex = 0; 
            
            for (int i = 0; i < 40; i++)//从电控读取数据 填充 listmbinfo
            {
                Sortnum = ListSort[index - 1].ReadD(ReadIndex).CastTo<int>(0);//任务号
              
                if (Sortnum > 0)//任务号不为0
                {
                    MainBeltInfo info = new MainBeltInfo();
                    info.SortNum = Sortnum;//任务号
                    info.Place = (ListSort[index - 1].ReadD((ReadIndex + 1)).CastTo<int>(-1) / 1000);//位置(米)
                    info.Quantity = ListSort[index - 1].ReadD((ReadIndex + 2)).CastTo<int>(-1);//数量
                    info.GroupNO = groupno;//组号
                    ListmbInfo.Add(info);
                }
                ReadIndex = ReadIndex + 4;
            }
            MainBeltInfoService.GetSortMainBeltInfo(ListmbInfo); //填充完成之后传进方法 计算 ，
        }
        /// <summary>
        /// 读取List
        /// </summary>
        /// <param name="index">索引</param>
        void ReadListInfo(int index)
        {
            if (index <= ListmbInfo.Count && ListmbInfo.Count > 0)
            {
                groupBoxUnionInfo.Visible = true;
                panelCig.Controls.Clear();
                dgvSortingBeltInfo.DataSource = null;//重置数据显示控件
                ListmbInfo = ListmbInfo.OrderBy(a => a.Place).ThenBy(a => a.SortNum).ToList();//对距离任务号进行排序
                if (ListmbInfo[index].taskInfo != null && ListmbInfo[index].taskInfo.Count > 0)//当数据不为空
                {
                    if (!string.IsNullOrWhiteSpace(ListmbInfo[index].MsgCode))
                    {
                        lblErorr.Visible = true;
                        lblErorr.Text = "错误代码：" + ListmbInfo[index].MsgCode + "错误信息：" + ListmbInfo[index].ErrorMsg;
                    }
                    else
                    {
                        lblErorr.Visible = false;
                    }
                    dgvSortingBeltInfo.DataSource = ListmbInfo[index].taskInfo.Select(x => new
                   {
                       CIGARETTECODE = x.CIGARETTDECODE,
                       CIGARETTNAME = x.CIGARETTDENAME,
                       MAINBELT = x.MainBelt,
                       QTY = x.qty,
                       GroupNo = x.groupno,
                       MEACHINESEQ = x.machineseq,
                       SORTNUM = x.SortNum,
                   }).ToList();//根据索引读取相对应数据 
                    DgvBind();
                    addPanel(ListmbInfo[index].taskInfo);//根据当前卷烟信息 往panel控件添加数据
                    lblSortnum.Text = "任务号：" + ListmbInfo[index].SortNum;
                    lblNum.Text = "数量：" + ListmbInfo[index].Quantity;
                    lblPlace.Text = "当前位置：" + ListmbInfo[index].Place + "米";
                }
                lblCOunt.Text = "总批次：" + ListmbInfo.Count;
                lblNowcOUNT.Text = "当前批次:" + (index + 1) + "/" + ListmbInfo.Count;
            }
            else
            {
                lblSortnum.Text = "任务号：";
                lblNum.Text = "数量：" ;
                lblPlace.Text = "当前位置：0米" ;
                   lblCOunt.Text = "总批次：" ;
                lblNowcOUNT.Text = "当前批次:" + 0 + "/" +0;
                dgvSortingBeltInfo.DataSource = null;
            }
            
        }
        void DgvBind()
        {
            try
            { 
                dgvSortingBeltInfo.Columns[0].HeaderCell.Value = "香烟编号";
                dgvSortingBeltInfo.Columns[1].HeaderCell.Value = "香烟名称";
                dgvSortingBeltInfo.Columns[2].HeaderCell.Value = "主皮带";
                dgvSortingBeltInfo.Columns[3].HeaderCell.Value = "数量";
                dgvSortingBeltInfo.Columns[4].HeaderCell.Value = "组号";
                dgvSortingBeltInfo.Columns[5].HeaderCell.Value = "物理通道号";
                dgvSortingBeltInfo.Columns[6].HeaderCell.Value = "任务号";
                //dgbMainBeltInfo.Columns[4].HeaderCell.Value = "数量";
                //dgbMainBeltInfo.Columns[5].HeaderCell.Value = "组号";
                //dgbMainBeltInfo.Columns[6].HeaderCell.Value = "物理通道号";
                //dgbMainBeltInfo.Columns[6].HeaderCell.Value = "客户名称"; 
                //dgbMainBeltInfo.Columns[6].HeaderCell.Value = "客户编号"; 
                //dgbMainBeltInfo.Columns[6].HeaderCell.Value = "排序号"; 
            }
            catch (ArgumentOutOfRangeException aoore)
            { 

            }
        }
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (isOnLine)
            {
                if (ReadIndex == 0)
                {
                   // ReadListInfo(0);
                    MessageBox.Show("最上面了"); 
                    return;
                }
                else
                {
                    ReadIndex = ReadIndex - 1;
                    ReadListInfo(ReadIndex);
                
                }
            }
            else
            {
                updateListBox("与服务器断开连接....");
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (isOnLine)
            {
                if (ReadIndex >= (ListmbInfo.Count - 1))
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
            else
            {
                updateListBox("与服务器断开连接....");
            }
        }

        private void Fm_FollowTaskSorting_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isOnLine)
            {
                if (btnAllInfo.Text == "所 有")
                {
                    if (ListmbInfo.Count > 0)
                    {
                        groupBoxUnionInfo.Visible = false;
                        List<UnionTaskInfo> listunion = new List<UnionTaskInfo>();
                        for (int i = 0; i < ListmbInfo.Count; i++)
                        {

                            for (int j = 0; j < ListmbInfo[i].taskInfo.Count; j++)
                            {
                                UnionTaskInfo un = new UnionTaskInfo();
                                if (ListmbInfo[i].taskInfo != null && ListmbInfo[i].taskInfo.Count > 0)//当数据不为空
                                {
                                    un.CIGARETTDECODE = ListmbInfo[i].taskInfo[j].CIGARETTDECODE;
                                    un.CIGARETTDENAME = ListmbInfo[i].taskInfo[j].CIGARETTDENAME;
                                    un.qty = ListmbInfo[i].taskInfo[j].qty;
                                    un.MainBelt = ListmbInfo[i].taskInfo[j].MainBelt;
                                    un.SortNum = ListmbInfo[i].taskInfo[j].SortNum;
                                    listunion.Add(un);
                                }
                            }
                        }
                        DgvBind();
                        dgvSortingBeltInfo.DataSource = listunion.Select(x => new
                        {
                            CIGARETTECODE = x.CIGARETTDECODE,
                            CIGARETTNAME = x.CIGARETTDENAME,
                            MAINBELT = x.MainBelt,
                            QTY = x.qty,
                            GroupNo = x.groupno,
                            MEACHINESEQ = x.machineseq,
                            SORTNUM = x.SortNum,
                        }).ToList();//根据索引读取相对应数据 ;
                        btnAllInfo.Text = "返回";
                    }
                    else
                    {
                        MessageBox.Show("当前没有数据");
                    }
                }
                else
                {
                    ReadListInfo(0);

                }
               
            }
            else
            {
                updateListBox("与服务器断开连接....");
            }
        }

      

      

     

        private void Fm_FollowTaskSorting_SizeChanged(object sender, EventArgs e)
        {
            Location = new Point(0, 0); 
        }

        #region listBox显示
        public void writeListBox(string info)
        {
            String time = DateTime.Now.ToLongTimeString();
            this.list_data.Items.Add(time + "    " + info);
        }

        private delegate void HandleDelegate(string strshow);

        public void updateListBox(string info)
        {
            String time = DateTime.Now.ToLongTimeString();

            if (this.list_data.InvokeRequired)
            {

                this.list_data.Invoke(new HandleDelegate(updateListBox), info);
            }
            else
            {
                this.list_data.Items.Insert(0, time + "    " + info);

            }
        }
        #endregion

        private void btnPrint_Click(object sender, EventArgs e)
        {
            dgVprint1.MainTitle = "预分拣第" + groupno + "组皮带表";
            //dgVprint1.SubTitle = "这是子标题，当然也可以不设的";
            // dgVprint1.PaperLandscape = true;//用横向打印，默认是纵向

            dgVprint1.Print(dgvSortingBeltInfo);
        }
      
        private void Fm_FollowTaskSorting_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DialogResult MsgBoxResult = MessageBox.Show("确定要退出程序?",//对话框的显示内容 
            //                                                 "操作提示",//对话框的标题  
            //                                                 MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
            //                                                 MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
            //                                                MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
            //Console.WriteLine(MsgBoxResult);
            if (this.DialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                this.Hide();
                e.Cancel = false;
            }
            else { e.Cancel = true; }
         
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (groupno == 1 || groupno == 3 || groupno == 5 || groupno == 7)
            {

                ReadDBinfo(1, groupno);

            }
            else
            {
                ReadDBinfo(2, groupno);
            }
            ReadListInfo(0);
            Text = groupno + "组预分拣";
            txtTitle.Text = Text;
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            btnClose.Location = new Point(this.Width-25, 0);
            btnZoom.Location = new Point(this.Width - 55, 0);
          
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Hide ();
            this.DialogResult = System.Windows.Forms.DialogResult.None;
             
        }
        bool maxormin = true;
        private void btnZoom_Click(object sender, EventArgs e)
        {
            if (maxormin)
            {
                this.WindowState = FormWindowState.Maximized;
                maxormin = false;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                maxormin = true;
            }
        }


      
    }
}
