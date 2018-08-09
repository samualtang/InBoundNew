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
using InBound.Business;
using System.Configuration;
namespace FollowTask
{
    public partial class FM_Device : Form
    {
        public FM_Device()
        {
            InitializeComponent();
        }

        decimal DeviceNum = decimal.Parse(ConfigurationManager.AppSettings["DeviceNum"].ToString());//总设备数
        decimal Section = decimal.Parse(ConfigurationManager.AppSettings["Section"].ToString());//一个设备段之间的距离DeviceName
        string DeviceName = ConfigurationManager.AppSettings["UnionMainBeltDeviceName"].ToString();
        /// <summary>
        /// 主皮带信息集合
        /// </summary>
        List<MainBeltInfo> ListmbInfo = new List<MainBeltInfo>();

        /// <summary>
        /// LIST DB块
        /// </summary>
        List<Group> listMainBelt = new List<Group>();
         
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

        public void GetMainInfo(int mainbelt, List<Group> list, bool isonline)
        {
            if (isonline)
            {
                Text = mainbelt + "号主皮带";
                listMainBelt = list;
                ReadDBinfo(mainbelt);
            }
            else
            {
                lblErorr.Visible = true;
                lblErorr.Text = "错误信息:服务器连接失败";
            }

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
            int i = 0;
            foreach (var item in before)
            {
                PictureBox img = new PictureBox();
                Label lbl = new Label();
                lbl.Text = item.POKENUM.ToString();
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
            decimal Sortnum = 0;
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
                    info.Quantity = Convert.ToDecimal(listMainBelt[mainbelt - 1].ReadD((ReadIndex + 2)).CastTo<int>(-1));//数量
                    info.mainbelt = mainbelt.ToString();//主皮带
                    info.SortNumList = SortNumList;//机械手任务号
                    info.QuantityList = QuantityList;//机械手放烟数量
                    info.DeviceName = ""; 
                    ListmbInfo.Add(info);
                }
                ReadIndex = ReadIndex + 3;
            }
            MainBeltInfoService.GetMainBeltInfo(ListmbInfo); //填充完成之后传进方法 计算 ，
            ListmbInfo = ListmbInfo.OrderBy(x => x.Place).ToList();// ListmbInfo.OrderBy(a => a.Place).ThenBy(a => a.SortNum).ToList();//对距离任务号进行排序
            GetDviceName(ListmbInfo, DeviceNum, DeviceName);
        }
        /// <summary>
        /// 获取设备名
        /// </summary>
        /// <param name="list">数据集</param>
        /// <param name="deviceNum">设备总数</param> 
        /// /// <param name="devicename">设备名称</param>
        void GetDviceName(List<InBound.Model.MainBeltInfo> list, decimal deviceNum, string devicename)
        {
            List<decimal> listcount = new List<decimal>();//设备和设备之间的间距 
            List<string> listDerviceName = new List<string>();//设备名
            for (int i = 1; i <= deviceNum; i++)
            {
                listDerviceName.Add(devicename + i);
            }
            for (int i = 1; i <= deviceNum; i++)//取得一设备与设备之间的间距
            {
                listcount.Add(((deviceNum * i) - deviceNum));
                listcount.Add((deviceNum * i));
            }
            for (int i = 0; i < listcount.Count; i++)//这个任务位置所在哪个设备号中间
            {
                if (listcount[i] > list[i].Place && list[i].Place < listcount[i + 1])
                {
                    list[i].DeviceName = listDerviceName[i];
                }
            }
            
        }


        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtDeviceNo.Text))
            {
                List<UnionTaskInfo> listUnionInfo = new List<UnionTaskInfo>();
                var deviceInfo = ListmbInfo.Where(a => a.DeviceName == txtDeviceNo.Text).ToList();
                if (deviceInfo.Count > 0)
                {
                    dgvMainBeltInfo.DataSource = null;
                    listUnionInfo.Clear();
                    foreach (var item in deviceInfo)
                    { 
                        listUnionInfo.AddRange(item.taskInfo);
                    }
                    addPanel(listUnionInfo);//添加数据图片至panel控件上面
                    lblDeviceNo.Text = "设备编号:" + txtDeviceNo.Text;
                    dgvMainBeltInfo.DataSource = listUnionInfo.Where(x=> x.IsOnMainBelt ==1).Select(x => new
                    {
                        CIGARETTECODE = x.CIGARETTDECODE,
                        CIGARETTNAME = x.CIGARETTDENAME,
                        QTY = x.POKENUM,
                        MAINBELT = x.MainBelt,
                        SORTNUM = x.SortNum,
                        PACKAGEMACHINE = x.PACKAGEMACHINE,
                        PLACE = x.Place + "米", 
                    }).ToList();//根据索引读取相对应数据   
                    DgvBind();
                }
                else
                {
                    MessageBox.Show("当前设备号没有数据!");
                }
            }
            else
            {
                MessageBox.Show("请输入设备号!");
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
                dgvMainBeltInfo.Columns[5].HeaderCell.Value = "包装机"; 
                dgvMainBeltInfo.Columns[6].HeaderCell.Value = "位置";

            }
            catch (ArgumentOutOfRangeException ex)
            {


            }
        }

        
    }
}
