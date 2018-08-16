using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FollowTask.Modle;
using InBound.Model;
using InBound;
using InBound.Business;
using System.IO;


namespace FollowTask
{
    public partial class FM_MainbeltDevice : Form
    {
        public FM_MainbeltDevice()
        {
            InitializeComponent();
            // rgp (36,255,0)
         //   BackColor = Color.FromArgb(0, 255, 234);

            foreach (Control  item in panel1.Controls )
            {
                if (item.Name.Contains("A"))
                {
                    if (item is Button)
                    {
                        item.BackColor = Color.FromArgb(234, 255, 0);
                    }

                }
                else
                {
                    if (item is Button)
                    {
                        item.BackColor = Color.FromArgb(0, 255, 255);
                    }  

                }
            }
            panel3.Visible = true;
        }
        
        decimal groupno = 0;

        List<Group> ListSort = new List<Group>();
        List<MainBeltInfo> ListmbInfoA = new List<MainBeltInfo>();
        List<MainBeltInfo> ListmbInfoB = new List<MainBeltInfo>();
        bool isOnLine;
 

        delegate void HandleRead(decimal groupno);
        public void GetSoringBeltInfo(string text, List<Group> list, bool isonline)
        {
            try
            {


                String OpcFJConnectionService = "S7:[FJCONNECTIONGROUP";//OPC服务器名
                groupno = Convert.ToDecimal(System.Text.RegularExpressions.Regex.Replace(text, @"[^0-9]+", ""));
                list[0].RemovedItem();//在给OPCItem添加Item之前清空
                list[1].RemovedItem();


                if (isonline)
                {
                    groupno = GroupnotoBigg(groupno);
                    OpcFJConnectionService = OpcFJConnectionService + groupno + "]";
                    list[0].addItem(ItemCollection.GetASortingItem(OpcFJConnectionService));
                    list[1].addItem(ItemCollection.GetBSortingItem(OpcFJConnectionService));
                    ListSort = list;
                    isOnLine = isonline;
                    lblyaobai.Text = groupno + "组预分拣";
                    Text = groupno + "组预分拣";
                    HandleRead task = ReadDBinfo;
                    task.BeginInvoke(groupno, null, null);

                }
                else
                {

                    lblErorr.Text = "与服务器断开连接....";
                }
            }
            catch (Exception ex )
            {
                lblloading.Text = "服务器连接失败，请检查网络！"+ ex.Message; 
            }
        }
        decimal Sortnum;
        /// <summary>
        /// 读取预分拣DB块
        /// </summary>
        /// <param name="index"></param>
        /// <param name="groupno"></param>
        void ReadDBinfo( decimal groupno)
        { 
            ListmbInfoA.Clear(); //清空list
            ListmbInfoB.Clear();
            for (int j = 0; j < 1; j++)
            { 

                // dgvSortingBeltInfo = null;
                panebelt.Controls.Clear();
                int ReadIndex = 0;

                for (int i = 0; i < 40; i++)//从电控读取数据 填充 listmbinfo
                {
                    Sortnum = ListSort[j].ReadD(ReadIndex).CastTo<int>(0);//任务号

                    if (Sortnum > 0)//任务号不为0
                    {
                        MainBeltInfo info = new MainBeltInfo();
                        info.SortNum = Sortnum;//任务号
                        info.Place = (ListSort[j].ReadD((ReadIndex + 1)).CastTo<decimal>(-1) / 1000);//位置(米)
                        info.Quantity = ListSort[j].ReadD((ReadIndex + 2)).CastTo<int>(-1);//数量
                        info.GroupNO = groupno;//组号
                        info.DeviceName = ""; 
                        if (j == 0)
                        {
                            ListmbInfoA.Add(info);
                        }
                        else
                        {
                            ListmbInfoB.Add(info);
                        }
                    }
                    ReadIndex = ReadIndex + 4;
                }
                if (j == 0)
                {
                    MainBeltInfoService.GetSortMainBeltInfo(ListmbInfoA); //填充完成之后传进方法 计算 ，
                    ListmbInfoA = ListmbInfoA.OrderBy(a => a.Place).ToList();//对距离任务号进行排序
                    GetDviceName(ListmbInfoA, "btnA");
                }
                else
                {
                    MainBeltInfoService.GetSortMainBeltInfo(ListmbInfoB); //填充完成之后传进方法 计算 ，
                    ListmbInfoB = ListmbInfoA.OrderBy(a => a.Place).ToList();//对距离任务号进行排序
                    GetDviceName(ListmbInfoB, "btnB");
                } 
            }
            if (isOnLine)
            {
                panel3.Visible = false;
            }
            else
            {
                lblloading.Text = "服务器连接失败!请检查网络";
            }
        }
        /// <summary>
        /// 获取设备名
        /// </summary>
        /// <param name="list">数据集</param>
        /// <param name="deviceNum">设备总数</param> 
        /// /// <param name="devicename">设备名称</param>
        void GetDviceName(List<InBound.Model.MainBeltInfo> list, string line )
        {
            if (list.Count > 0)
            {
               
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Place > 30)
                    {
                        for (int j = 0; j < list[i].taskInfo.Count; j++)
                        {
                            if (list[i].taskInfo[j].MainBelt == 1)
                            {
                                list[i].DeviceName = line + "1";
                                break;
                            }
                            else if (list[i].taskInfo[j].MainBelt == 2)
                            {
                                list[i].DeviceName = line + "2";
                                break;
                            }
                            else if (list[i].taskInfo[j].MainBelt == 3)
                            {
                                list[i].DeviceName = line + "3";
                                break;
                            }
                            else if (list[i].taskInfo[j].MainBelt == 4)
                            {
                                list[i].DeviceName = line + "4";
                                break;
                            }
                        }
                    }
                    else
                    {
                        list[i].DeviceName = line + "0";

                    }
                }
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
            panebelt.Controls.Clear();
            for (int i = 0; i < before.Count; i++)
            {
                PictureBox img = new PictureBox();
                Label lbl = new Label();
                lbl.Text = before[i].POKENUM.ToString();
                lbl.BackColor = Color.Transparent;
                lbl.Font = new System.Drawing.Font("宋体", 10, FontStyle.Regular);
                img.Name = "ImgName" + Guid.NewGuid().ToString();
                img.Size = new System.Drawing.Size(20, 80);
                img.AccessibleName = before[i].CIGARETTDENAME + "|" + before[i].POKENUM + "|" + before[i].CIGARETTDECODE;//卷烟名称 和 QTY
                img.BackgroundImage = GetImg(before[i].CIGARETTDECODE);
                img.SizeMode = PictureBoxSizeMode.Zoom;
                img.BorderStyle = BorderStyle.FixedSingle;
                img.MouseEnter += new EventHandler(img_MouseEnter);
                img.Location = new Point(i * img.Width + 10 * i, 0);
                lbl.Location = new Point(img.Width / 2 - 4, 0);
                img.Controls.Add(lbl);
                panebelt.Controls.Add(img);//之后 
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
            if (group == 2)
            {
                return 1;
            }
            if (group == 3 || group == 4)
            {
                return 2;
            }
            if (group == 5 || group == 6)
            {
                return 3;
            }
            if (group == 7 || group == 8)
            {
                return 4;
            }
            return 1;
        }
        private void btnB04_Click(object sender, EventArgs e)
        {
            Button btn = ((Button)sender);

            panebelt.Controls.Clear();            if (ListmbInfoB.Count > 0 || ListmbInfoA.Count > 0)
            {
                string devicename = btn.Name;
              //  decimal mainbelt =Convert.ToDecimal( System.Text.RegularExpressions.Regex.Replace(btn.Name, @"[^0-9]+", ""));
                if (devicename.Contains("A"))
                {
                    dgvMainBeltInfo.DataSource = null;
                    GetInfoBind(ListmbInfoA, devicename);
                }
                else if (devicename.Contains("B"))
                {
                    dgvMainBeltInfo.DataSource = null;
                    GetInfoBind(ListmbInfoB, devicename);
                }
            }
            else
            {
                if (ListmbInfoA.Count == 0)
                {
                    MessageBox.Show("当前" + groupno + "组A线暂无任务，请稍候再试！");
                    
                }
                if (ListmbInfoA.Count == 0)
                {
                    MessageBox.Show("当前" + groupno + "组B线暂无任务，请稍候再试！");
                   
                } 
            }
        }
        void GetInfoBind(List<MainBeltInfo> list, string devicename)
        {
            List<UnionTaskInfo> listUnionInfo = new List<UnionTaskInfo>();
            List<MainBeltInfo> deviceInfo = new List<MainBeltInfo>();

            deviceInfo = list.Where(a => a.DeviceName == devicename).ToList();



            if (deviceInfo.Count > 0)
            {
                foreach (var item in deviceInfo)
                {
                    // item.taskInfo[0].Place = deviceInfo[0].Place;
                    listUnionInfo.AddRange(item.taskInfo);
                }

                addPanel(listUnionInfo);//添加数据图片至panel控件上面 
                dgvMainBeltInfo.DataSource = listUnionInfo.Select(x => new
                {
                    CIGARETTECODE = x.CIGARETTDECODE,
                    CIGARETTNAME = x.CIGARETTDENAME,
                    QTY = x.POKENUM,
                    MAINBELT = x.MainBelt,
                    SORTNUM = x.SortNum,
                    //PACKAGEMACHINE = x.PACKAGEMACHINE,
                    //PLACE = x.Place + "米",
                }).OrderByDescending(a=> a.SORTNUM).ToList();//根据索引读取相对应数据   
                DgvBind();
            }

        }

        void DgvBind()
        {
            try
            {
                dgvMainBeltInfo.Columns[0].HeaderCell.Value = "香烟编号";
                dgvMainBeltInfo.Columns[1].HeaderCell.Value = "香烟名称";
                dgvMainBeltInfo.Columns[2].HeaderCell.Value = "数量";
                dgvMainBeltInfo.Columns[3].HeaderCell.Value = "主皮带";
                dgvMainBeltInfo.Columns[4].HeaderCell.Value = "任务号";
                //dgvMainBeltInfo.Columns[5].HeaderCell.Value = "包装机";
                //dgvMainBeltInfo.Columns[6].HeaderCell.Value = "位置";

            }
            catch (ArgumentOutOfRangeException ex)
            {


            }
        }
 
       


       
       
    }
}
