using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using InBound;
using InBound.Model;
using InBound.Business;
//using System.Text.RegularExpressions;
using OpcRcw.Da;
using OpcRcw.Comn;
using SpecialShapeSmoke.Model;
using System.Runtime.InteropServices;
using System.IO.Ports;

namespace SpecialShapeSmoke
{
    public partial class MainScreen1_new : Form
    {

        WriteLog writeLog = WriteLog.GetLog();
        List<GroupBox> panelList = new List<GroupBox>();
        List<T_UN_POKE> listShape = new List<T_UN_POKE>();
        internal const string SERVER_NAME = "OPC.SimaticNET";       // local server name

        internal const string GROUP_NAME = "grp1";                  // Group name 
        internal const int LOCALE_ID = 0x409;                       // LOCALE FOR ENGLISH. 
        IOPCServer pIOPCServer;  //定义opcServer对象.
        /// <summary>
        /// 存储DB块上的 TsakNum  完成号
        /// </summary>
        decimal dbMesg = 0;
        /// <summary>
        /// 存储DB块上的 TsakNum  完成号
        /// </summary>
        decimal dbMesg2 = 0;// 
        /// <summary>
        /// 混合烟道
        /// </summary>
        Group ShapeGroup;

        int topHeight = 57;
        int padding = 10;
        int bottom = 100;
        int labelCount = 15;
        int labelHeight = 50;
        int boxTop = 40;
        int boxBottom = 10;
        bool stop = false;
        bool falge = false;//单通多显标志位
        static bool UpOrDn = false;
        //String lineNum = "0";
        Control control;
        HunHeService_new service = new HunHeService_new();
        Label chezu = new Label();
        System.Drawing.Color labelcolor;
        static int UnPullLabelNum;//待放烟的显示条目
        static int HavePullLabelNum;//已放烟的显示条目
        decimal[] PackMachineSeq;
        string plcvalvestag;
        /// <summary>
        ///  通道编号
        /// </summary>
        public string[] boxText = null;
        //  public decimal[] troughno =  new decimal[9]  ;

        static Boolean isInit = false;
        //通道集合
        List<HUNHEVIEW>[] throughList;
        /// <summary>
        /// 通道对应DB块索引
        /// </summary>
        decimal[] dbIndex = new decimal[] { -1, -1, -1, -1 };//通道对应DB块索引
        // Dictionary<string, int>  rgDic = new Dictionary<string, int>();//存放通道对应值

        TextBox txtbox1;
        TextBox txtbox2;

        TextBox txtbox3;
        TextBox txtbox4;
        public MainScreen1_new()
        {
            InitializeComponent();


            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Hide();
            this.Show();
            Panel p = new Panel();
            try
            {
                // lineNum = ConfigurationManager.AppSettings["LineNum"].ToString();
                boxText = ConfigurationManager.AppSettings["troughList"].ToString().Replace(" ", "").Split(',');//通道编号 
                RefreshTime = ConfigurationManager.AppSettings["RefreshTime"].ToString().Replace(" ", "").CastTo<int>(20);//刷新时间
                // troughno = new decimal[boxText.Length];
                UnPullLabelNum = Convert.ToInt32(ConfigurationManager.AppSettings["UnPullLabelNum"].ToString());// 待放烟条目
                HavePullLabelNum = Convert.ToInt32(ConfigurationManager.AppSettings["HavePullLabelNum"].ToString());//已放烟条目
                string[] packmachine = ConfigurationManager.AppSettings["PackMachineSeq"].ToString().Replace(" ", "").Split(',');
                PackMachineSeq = new decimal[packmachine.Count()];
                for (int i = 0; i < packmachine.Count(); i++)
                {
                    PackMachineSeq[i] = Convert.ToDecimal(packmachine[i]);
                } 
                sp_name = ConfigurationManager.AppSettings["sp_name"].ToString();//扫码端口1
                sp1_name = ConfigurationManager.AppSettings["sp1_name"].ToString();////扫码端口2

                plcvalvestag = ConfigurationManager.AppSettings["plcvalves"].ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("配置文件读取出现异常，请检查配置文件格式是否正确！");
            }
        

            OpenSerialPort();
            OpenSerialPort1();

            if (boxText.Length == 1)//根据通道编号查找DB对应值
            {
                dbIndex = DicBind(boxText[0],PackMachineSeq);
            }
            //else if (boxText.Length == 2)
            //{
            //    for (int i = 0; i < 2; i++)
            //    {
            //        dbIndex[i] = DicBind(boxText[i]);
            //    }
            //}
            if (CheckTrough())
            {
                falge = true;
                addGroupBoxByNew(2);
            }
            else
            {
                addGroupBoxByNew(4);
                //addGroupBoxByNew(boxText.Length);
            }

            //if (lineNum == "1")
            //{
            //    boxText = new String[] { "1059", "1060", "1061", };
            //    troughno = new decimal[] { 1059, 1060, 1061 };
            //} 
            //else 
            //{
            //     boxText = new String[] { "2059", "2060", "2061" };
            //    troughno = new decimal[] { 2059, 2060, 2061 };
            //}
            p.Width = Screen.PrimaryScreen.Bounds.Width;
            p.Height = topHeight;
            p.BackgroundImage = global::SpecialShapeSmoke.Properties.Resources.topfj;
            p.BackgroundImageLayout = ImageLayout.Stretch;
            p.Location = new Point(0, 0);
            this.Controls.Add(p);
            this.BackgroundImage = global::SpecialShapeSmoke.Properties.Resources.mainbj;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            //颜色绑定
            labelcolor = Color.LightGreen;
            string GetColor = ConfigurationManager.AppSettings["color"].ToString();
            switch (GetColor)
            {
                case "LightGreen":
                    labelcolor = Color.LightGreen;
                    break;
                case "LightCyan":
                    labelcolor = Color.LightCyan;
                    break;
                case "LightBlue":
                    labelcolor = Color.LightBlue;
                    break;
                case "Yellow":
                    labelcolor = Color.Yellow;
                    break;
                case "Green":
                    labelcolor = Color.Green;
                    break;
                case "Lime":
                     labelcolor = Color.Lime;
                    break;
                default:
                    labelcolor = Color.White;
                    break;
            }
            //关闭按钮
            Button close = new Button();
            close.Width = topHeight;
            close.Height = topHeight;
            close.BackgroundImage = global::SpecialShapeSmoke.Properties.Resources.icon_exit;
            close.BackgroundImageLayout = ImageLayout.Stretch;
            close.Click += closeForm;
            close.Location = new Point(p.Width - topHeight, 0);
            p.Controls.Add(close);

            //Label chezu = new Label();
            chezu.Width = 300;
            chezu.Height = 40;
            chezu.BackColor = Color.Transparent;
            chezu.Font = new Font("宋体", 25, FontStyle.Bold);
            chezu.Text = "";
            chezu.Location = new Point(Screen.PrimaryScreen.Bounds.Width - 300, Screen.PrimaryScreen.Bounds.Height - 50);
            this.Controls.Add(chezu);

            Button search = new Button();
            search.Width = 2 * topHeight;
            search.Height = topHeight;
            search.BackColor = Color.Silver;
            search.Font = new Font("宋体", 25, FontStyle.Bold);
            search.Text = "刷新";
            search.Click += Refresh;
            search.Location = new Point(p.Width - 4 * topHeight, 0);
            p.Controls.Add(search);

            Button btnView = new Button();
            btnView.Width = 3 * topHeight;
            btnView.Height = topHeight;
            btnView.BackColor = Color.Silver;
            btnView.Font = new Font("宋体", 25, FontStyle.Bold);
            btnView.Text = "出烟顺序";
            btnView.Click += GetNowView;
            btnView.Location = new Point(p.Width - 7 * topHeight, 0);
            p.Controls.Add(btnView);

            Button btnSearch = new Button();
            btnSearch.Width = 3 * topHeight;
            btnSearch.Height = topHeight;
            btnSearch.BackColor = Color.Silver;
            btnSearch.Font = new Font("宋体", 25, FontStyle.Bold);
            btnSearch.Text = "条烟定位";
            btnSearch.Click += FScView;
            btnSearch.Location = new Point(p.Width - 10 * topHeight, 0);
            p.Controls.Add(btnSearch);

            Button SortSet = new Button();
            SortSet.Width = 2 * topHeight;
            SortSet.Height = topHeight;
            SortSet.BackColor = Color.Silver;
            SortSet.Font = new Font("宋体", 25, FontStyle.Bold);
            SortSet.Name = "btnSortSet";
            SortSet.Text = "排序显示";
            SortSet.Click += SetUpOrDn;
            SortSet.Location = new Point(p.Width - 10 * topHeight, 0);
            // p.Controls.Add(SortSet);


            txtbox1 = new TextBox();
            txtbox1.Width = 120;
            txtbox1.Height = 20;
            txtbox1.Location = new Point(340, 30);
            txtbox1.Visible = false;
            p.Controls.Add(txtbox1);

            Button btn1 = new Button();
            btn1.Width = 60;
            btn1.Height = 20;
            btn1.Location = new Point(470, 30);
            btn1.Text = "1放烟";
            btn1.Click += btn1_Click;
            btn1.Visible = false;
            p.Controls.Add(btn1);

            txtbox2 = new TextBox();
            txtbox2.Width = 120;
            txtbox2.Height = 20;
            txtbox2.Location = new Point(560, 30);
            txtbox2.Visible = false;
            p.Controls.Add(txtbox2);

            Button btn2 = new Button();
            btn2.Width = 60;
            btn2.Height = 20;
            btn2.Location = new Point(690, 30);
            btn2.Text = "2放烟";
            btn2.Click += btn2_Click;
            btn2.Visible = false;
            p.Controls.Add(btn2);

            Thread thread = new Thread(ConnectServer);
            thread.Start();

            lblpack = new Label();
            lblpack.Width = 2 * topHeight;
            lblpack.Height = topHeight;
            lblpack.BackColor = Color.Transparent;
            lblpack.Font = new Font("宋体", 25, FontStyle.Bold); 
            
            lblpack.Name = "lblpack";
            lblpack.Text = PackMachineSeq[0].ToString() + "、" + PackMachineSeq[1].ToString() + "包装机补烟顺序";
            lblpack.Location = new Point(Convert.ToInt32(p.Width *(0.17)), 10);
            lblpack.Size =new Size(350,40);
            p.Controls.Add(lblpack);
            //lblpack.ForeColor = Color.Red;

            txtbox3 = new TextBox();
            txtbox3.Width = 120;
            txtbox3.Height = 20;
            txtbox3.Location = new Point(690, 30); 
            p.Controls.Add(txtbox3);
            txtbox3.Text = "0";
           // txtbox3.Visible = false;


            txtbox4 = new TextBox();
            txtbox4.Width = 120;
            txtbox4.Height = 20;
            txtbox4.Location = new Point(690,5);
            p.Controls.Add(txtbox4);
            txtbox4.Text = "0";
           // txtbox4.Visible = false;
            if (plcvalvestag == "1")
            {
                txtbox4.Visible = true;
                txtbox3.Visible = true;
            }
            else
            {
                txtbox4.Visible = false;
                txtbox3.Visible = false;
            }
            


            timer1.Start();
            timer1.Interval = RefreshTime * 1000;
        }
        Label lblpack;

        public void SetUpOrDn(object sender, EventArgs e)//修改显示方向
        {
            if (UpOrDn)
            {
                UpOrDn = false;
            }
            else
            {
                UpOrDn = true;
            }
            clearAllText();
            for (int j = 0; j < boxText.Length; j++)//数据获取核心
            {
                throughList[j] = GroupList(service.GetTroughCigarette(Convert.ToDecimal(boxText[j]),finishNo, 300,PackMachineSeq, true));//第二个 
                //initText(panelList[j], throughList[j]);
                initTextUpOrDn(panelList[j], throughList[j], 15, UpOrDn);
            }
            MessageBox.Show(UnPullLabelNum + "" + HavePullLabelNum);
        }



        public List<HUNHEVIEW> GroupList(List<HUNHEVIEW> list)
        {
            if (list != null)
            {
                List<HUNHEVIEW> temp = new List<HUNHEVIEW>();
                HUNHEVIEW tempview = null;
                int count = 0;
                //定义pokelist的长度为混合道poke的个数
                List<string> pokeidlist = new List<string>();
                foreach (var item in list)//遍历取到的数据（名称、编码、通道号）
                {
                    count++;

                    if (tempview == null)//如果tempview没有数据 赋值当前遍历的值
                    {

                        tempview = item;
                        pokeidlist.Add(item.POKEID.ToString());
                        tempview.POKEIDLIST = pokeidlist;
                    }
                    else if (item.CIGARETTECODE != tempview.CIGARETTECODE)//如果当前遍历的数据的香烟编码不等于上一次遍历
                    {
                        temp.Add(tempview);
                        tempview = new HUNHEVIEW();
                        //存pokeid的集合
                        pokeidlist = new List<string>();
                        pokeidlist.Add(item.POKEID.ToString());
                        tempview.CIGARETTENAME = item.CIGARETTENAME;
                        tempview.QUANTITY = item.QUANTITY;
                        tempview.MACHINESEQ = item.MACHINESEQ;
                        tempview.CIGARETTECODE = item.CIGARETTECODE;
                        tempview.POKEIDLIST = pokeidlist;

                    }
                    else
                    {
                        pokeidlist.Add(item.POKEID.ToString());
                        tempview.QUANTITY += item.QUANTITY; //数量相加
                        // tempview.TROUGHNUM += item.TROUGHNUM;//将编码拼接

                    }
                    if (count == list.Count)
                    {
                        temp.Add(tempview);
                        tempview.POKEIDLIST = pokeidlist;
                    }
                }
                return temp;
            }
            else
            {

                return null;
            }
        }
        /// <summary>
        /// 绑定通道号对应值
        /// </summary>
        decimal[] DicBind(string troughno,decimal[] packmachine)
        {
           decimal[] positiong = { 0, 0 };
           decimal[] packmachine1 = { 1, 2 };
           decimal[] packmachine2 = { 3, 4 };
           decimal[] packmachine3 = { 5, 6 };
           decimal[] packmachine4 = { 7, 8 };

           if (troughno == "1061" && packmachine[0] == packmachine1[0] && packmachine[1] == packmachine1[1])
            {
                positiong[0] = 0;
                positiong[1] = 1;
            }
           else if (troughno == "1061" && packmachine[0] == packmachine2[0] && packmachine[1] == packmachine2[1])
           {
               positiong[0] = 2;
               positiong[1] = 3;
           }
           else if (troughno == "2061" && packmachine[0] == packmachine3[0] && packmachine[1] == packmachine3[1])
           {
               positiong[0] = 4;
               positiong[1] = 5;
           }
           else if (troughno == "2061" && packmachine[0] == packmachine4[0] && packmachine[1] == packmachine4[1])
           {
               positiong[0] = 6;
               positiong[1] = 7;
           }
           
           
            return positiong;
        }
        static int RefreshTime;
        void ConnectServer()
        {
            try
            {
                Type svrComponenttyp;
                Guid iidRequiredInterface = typeof(IOPCItemMgt).GUID;
                svrComponenttyp = Type.GetTypeFromProgID(SERVER_NAME);
                pIOPCServer = (IOPCServer)Activator.CreateInstance(svrComponenttyp);

                ShapeGroup = new Group(pIOPCServer, 1, "group1", 1, LOCALE_ID);
                ShapeGroup.addItem(ItemCollection_new.GetTaskStatusByShapeItem());
                //ShapeGroup.callback += OnDataChange;

                if (checkConnection()) //连接服务器成功 
                {
                   
                    //while (true)
                    //{
                    //    getData(true);
                    //    Thread.Sleep(RefreshTime * 1000);//配置秒数
                    //}
                }
            }
            catch (Exception e)
            {
                if (pIOPCServer == null)
                {
                    MessageBox.Show("PLC连接失败，请检查PLC连接，再重新打开程序!");
                }
                writeLog.Write("PLC连接失败" + e.Message);
            }

            //socket = new ClientSocket(ipaddress, PORT);
            //socket.method+=getData;
            //socket.startListen();
        }

        public bool checkConnection()
        {
            int flag = ShapeGroup.ReadD(0).CastTo<int>(-1);
            if (flag == -1)
            {
                MessageBox.Show("连接服务器失败,请检查网络");
                writeLog.Write(" 连接服务器失败,请检查网络.");
                return false;
            }
            else
            {
                writeLog.Write(" 连接服务器成功......");
                return true;
                //isInit = true;
            }
        }


        /// <summary>
        /// 数据清空
        /// </summary>
        public void clearAllText()
        {
            //数据清空
            try
            {


            throughList = new List<HUNHEVIEW>[boxText.Length];
            for (int i = 0; i < (throughList.Length * 2); i++)
            {
                for (int j = 0; j < panelList[i].Controls.Count; j++)
                {
                    Label lbl = (Label)panelList[i].Controls[j];
                    updateLabel("", lbl, falg: 1);
                }
            }
            }
            catch (Exception e)
            {
                MessageBox.Show("放烟错误3");
                writeLog.Write("调用方法：clearAllText():时发生异常，" + e.Message + "/r/n异常源为：" + e.Source);
            }

        }
        #region
        //public void OnDataChange(int group, int[] clientId, object[] values )//DB块的值发生变化
        //{

        //if (group == 1)   //混合道 1001 1059 1061  2002 2060
        //{
        //    int[] tempvalue = new int[10] ;
        //    int temp = ShapeGroup.Read((int)dbIndex[0]).CastTo<int>(-1);
        //    for (int i = 0; i < clientId.Length; i++)
        //    { 
        //       tempvalue[i] = int.Parse((values[i].ToString())); 
        //        if (tempvalue[i] >= 1)
        //        {
        //            writeLog.Write("从通道：" + boxText[0] + "DB块读取值为:" + tempvalue[i] + "    " + temp +"    "+ dbIndex[0]);
        //            dbMesg = temp;

        //        }
        //        getData();
        //    } 
        //}
        //else if (group == 2)  //混合道2 1002 1060 2001  2059 2061 
        //{
        //    int[] tempvalue = new int[10];
        //    for (int i = 0; i < clientId.Length; i++)
        //    {
        //      int temp = ShapeGroup.Read((int)dbIndex[1]).CastTo<int>(-1);
        //        tempvalue[i] = int.Parse((values[i].ToString())); 
        //        if (tempvalue[i] >= 1)
        //        {
        //            if (boxText.Length > 2)
        //            {
        //                writeLog.Write("从通道：" + boxText[1] + "DB块读取值为:" + tempvalue[i] + "     " + temp+"     " + dbIndex[1]);
        //                dbMesg2 = temp;
        //            }
        //              //ShapeGroup.Read((int)dbIndex[0]).CastTo<int>(-1).ToString(); 
        //        }
        //        getData();
        //    }
        //}
        //else
        //if (group == 3)   
        //{
        //    int temp = -1;
        //    int[] tempvalue = new int[10];
        //    for (int i = 0; i < clientId.Length; i++)
        //    {
        //        for (int k = 0; k< 2; k++)
        //        {
        //            temp = ShapeGroup3.Read((int)dbIndex[k]).CastTo<int>(-1);
        //        }

        //        tempvalue[i] = int.Parse((values[i].ToString()));
        //        if (tempvalue[i] >= 1)
        //        {
        //            if (boxText.Length > 2)
        //            {
        //                writeLog.Write("从通道：" + boxText[1] + "DB块读取值为:" + tempvalue[i] + "     " + temp + "     " + dbIndex[1]);
        //                dbMesg2 = temp;
        //            }
        //            //ShapeGroup.Read((int)dbIndex[0]).CastTo<int>(-1).ToString(); 
        //        }
        //        getData();
        //    }
        //}
        //}
        #endregion
        // bool flag = true;//初始化

        static decimal[] finishNo = new decimal[2];//完成信号 (pokeid)
        
        static decimal[] befoerFinishNo = new decimal[2] { -1, -1 };//上一次完成信号(pokeid)

        /// <summary>
        /// 获取数据
        /// </summary>  
        /// 
        public void getData(bool Refresh = false)
        {
            try
            {

                int countnum = 0;
                //decimal[] finishNo = new decimal[2];//完成信号 (taskNum)
               
                string Log = "";

                try
                {
                    #region  读取DB
                    if (dbIndex.Count()==2)//  是1061 和2061 单个通道？
                    {
                        countnum = 2; 
                        finishNo[0] = ShapeGroup.ReadD((int)dbIndex[0]).CastTo<int>(-1);//根据通道 读取DB块  Read  
                        finishNo[1] = ShapeGroup.ReadD((int)dbIndex[1]).CastTo<int>(-1);
                    }
                    else
                    {
                        countnum = 4;
                        finishNo[0] = ShapeGroup.ReadD((int)dbIndex[0]).CastTo<int>(-1);
                        finishNo[1] = ShapeGroup.ReadD((int)dbIndex[1]).CastTo<int>(-1);//两个通道
                        finishNo[2] = ShapeGroup.ReadD((int)dbIndex[2]).CastTo<int>(-1);
                        finishNo[3] = ShapeGroup.ReadD((int)dbIndex[3]).CastTo<int>(-1);
                    } 
                    #endregion
                }
                catch (Exception ex)
                {
                    writeLog.Write("Plc读取完成信号失败！" + ex.Message);
                }

                txtbox4.Text = finishNo[0].ToString();
                txtbox3.Text = finishNo[1].ToString();


                if (finishNo[0] != -1 || finishNo[1] != -1 || Refresh)
                {
                    if (befoerFinishNo.Sum() != finishNo.Sum() || Refresh)
                    {
                        for (int i = 0; i < boxText.Length; i++)
                        {
                            Log += "通道 " + boxText[i] + ";包装机 "+PackMachineSeq[0].ToString()+"、"
                                +PackMachineSeq[1].ToString()+": 接收DB块值:分拣任务号" + finishNo[i]+ ",已出烟"+finishNo[1]+ "条\r\n";
                        }
                        writeLog.Write(Log);


                        if ((finishNo.Sum() >= befoerFinishNo.Sum()) || Refresh)
                        {
                            writeLog.Write("getData()开始获取数据");

                            clearAllText();
                            throughList = new List<HUNHEVIEW>[boxText.Length];
                            //// if (CheckTrough()) { countnum = 1; } else { countnum = 2; }
                            int labelnum;
                            if (true)
                            {
                                labelnum = HavePullLabelNum;
                            }
                            else
                            {
                                labelnum = UnPullLabelNum;
                            }
                            if (boxText.Length == 1)
                            {
                                for (int j = 0; j < countnum; j++)//数据获取核心
                                {
                                    if (j == 0)
                                    {
                                        throughList[j] = GroupList(service.GetTroughCigarette((Convert.ToDecimal(boxText[0])), finishNo, 300, PackMachineSeq,true));
                                        initTextUpOrDn(panelList[j], throughList[j], labelnum, true);
                                    }
                                    else
                                    {
                                        throughList[0] = GroupList(service.GetUnPullCigarette(Convert.ToDecimal(boxText[0]), PackMachineSeq));
                                        initTextUpOrDn(panelList[j], throughList[0], labelnum, true);
                                    }
                                }
                            }
                            else
                            {
                                for (int j = 0; j < countnum + 2; j++)//数据获取核心
                                {
                                    if (j == 0)
                                    {
                                        throughList[j] = GroupList(service.GetTroughCigarette(Convert.ToDecimal(boxText[j]), finishNo, 300, PackMachineSeq, true));
                                        initTextUpOrDn(panelList[j], throughList[j], labelnum, true);
                                    }
                                    else if (j == 2)
                                    {
                                        throughList[j - 1] = GroupList(service.GetTroughCigarette(Convert.ToDecimal(boxText[j - 1]), finishNo, 300, PackMachineSeq, true));
                                        initTextUpOrDn(panelList[j], throughList[j - 1], labelnum, true);
                                    }
                                    else
                                    {
                                        if (j == 1)
                                        {
                                            throughList[0] = GroupList(service.GetUnPullCigarette(Convert.ToDecimal(boxText[0]), PackMachineSeq));
                                            initTextUpOrDn(panelList[j], throughList[0], labelnum, true);
                                        }
                                        else
                                        {
                                            throughList[1] = GroupList(service.GetUnPullCigarette(Convert.ToDecimal(boxText[1]),PackMachineSeq));
                                            initTextUpOrDn(panelList[j], throughList[1], labelnum, true);
                                        }
                                    }
                                }
                                writeLog.Write("getData()数据获取成功！");
                            }
                            if (throughList[0].Count <= 0) //根据不同通道完成来显示完成任务 
                            {
                                Label lbl2 = (Label)Controls.Find("orBox" + 0, true)[0].Controls[0];
                                updateLabel(" 已完成全部出烟任务!", lbl2);
                            }
                            if (boxText.Length == 2)
                            {
                                if (throughList[1].Count <= 0 && !CheckTrough())
                                {
                                    Label lbl2 = (Label)Controls.Find("orBox" + 2, true)[0].Controls[0];
                                    updateLabel(" 已完成全部出烟任务!", lbl2);
                                }
                            }

                            befoerFinishNo = finishNo;
                        }
                    }
                }
                else
                {
                    // if (CheckTrough()) { countGroupBox = 1; } else { countGroupBox = 2; }
                    clearAllText();
                    for (int k = 0; k < boxText.Length; k++)
                    {
                        Label lbl2 = (Label)Controls.Find("orBox" + k, true)[0].Controls[0];
                        updateLabel("服务器断开连接,请重新连接!", lbl2);
                    }
                }
                //writeLog.Write("Receive Resend Data:" + data);
                
            }
            catch(Exception ex)   
            {
                writeLog.Write("getData()数据获取失败！"+ex.Message);
            }
        }
        NowView_new fNowView;
        System.Windows.Forms.Timer t1 = new System.Windows.Forms.Timer();
        //System.Windows.Forms.Timer t2 = new System.Windows.Forms.Timer(); 
        public void GetNowView(object sender, EventArgs e)//获取当前混合道
        {
            int machineseq1 = Convert.ToInt32(boxText[0]);
            int machineseq2;
            if (boxText.Length > 1)
            {
                machineseq2 = Convert.ToInt32(boxText[1]);
            }
            else
            {
                machineseq2 = machineseq1;
            }

            try
            {
                fNowView = new NowView_new(machineseq1, machineseq2, befoerFinishNo, PackMachineSeq); 
                fNowView.Show();
                fNowView.Activate(); 
                SearchWinForm(fNowView);
            
            }
            catch (Exception)
            {
                 MessageBox.Show("数据库连接失败！请检查网络");
                throw;
            }
     

        }
        SearchCustomer fScView;
        public void FScView(object sender, EventArgs e)//扫烟找户
        {
            int machineseq1 = Convert.ToInt32(boxText[0]);
            int machineseq2;
            if (boxText.Length > 1)
            {
                machineseq2 = Convert.ToInt32(boxText[1]);
            }
            else
            {
                machineseq2 = machineseq1;
            }

            try
            {
                fScView = new SearchCustomer();

                fScView.Show();
                fScView.Activate();

                SearchWinForm(fScView);
            }
            catch (Exception)
            {
                MessageBox.Show("数据库连接失败！请检查网络");
                //throw;
            }
        
        }
        public void SearchWinForm(Form fname)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is Form)
                {
                    fname.TopMost = true;
                    fname.Activate();

                    return;
                }
            }
            fname.Show();
            fname.Activate();
        }



        /// <summary>
        /// 手动刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        public void Refresh(object sender, EventArgs e)
        {
            getData(true);
        }
        /// <summary>
        /// 创建已经放烟的数据列表
        /// </summary>
        /// <param name="box"></param>
        /// <param name="list"></param>
        public void initText(GroupBox box, List<HUNHEVIEW> list)
        {
            if (box != null && list != null)
            {
                int i = 14;
                var newlist = list.Skip(15).Take(1000).ToList(); //获取多于15
                int no = 1;
                //int z = newlist.Count();//取剩余烟数

                try
                {
                foreach (var item in list)
                {
                    decimal count = item.QUANTITY ?? 0;
                    if (count >= 1 && i >= 0 && no <= 15)
                    {
                        Label lbl = (Label)box.Controls[i];
                        Label lab = (Label)box.Controls[i + 15];
                        i--;
                        updateLabel(no + ":  " + item.CIGARETTENAME + " " + count + "条", lbl);
                        updateLabel((no + 15).ToString(), lab);
                        no++;
                    }
                    if (falge && newlist.Count > 0)//用于单通道多显示2061 and  1061
                    {
                        int labStart = 14;
                        int newno = 1;
                        foreach (var item2 in newlist)
                        {
                            decimal count2 = item2.QUANTITY ?? 0;
                            if (count2 >= 1 && labStart >= 0 && labStart < 15)
                            {
                                Label lbl2 = (Label)Controls.Find("orBox" + 1, true)[0].Controls[labStart];
                                labStart--;
                                updateLabel((newno + 15) + ":" + item2.CIGARETTENAME + " " + count2 + "条", lbl2);
                                newno++;
                            }
                        }
                        falge = false;
                    }
                }

                if (CheckTrough()) { falge = true; }
                }
                catch (Exception e)
                {
                    MessageBox.Show("放烟错误");
                    writeLog.Write("initText();生成数据集合时发生异常：" + e.Message);
                }
            }
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="box"></param>
        /// <param name="list"></param>
        /// <param name="isUpOrDn">默认为真，真则从上往下显示，反亦之</param>
        public void initTextUpOrDn(GroupBox box, List<HUNHEVIEW> list, int labelnum, bool isUpOrDn = false)
        {
            if (box != null && list != null)
            {
                var newlist = list.Skip(15).Take(1000).ToList(); //获取多于15
                int singleNo = 1;
                int singleIndex;//单通显示
                int multiIdnex; //1061，2061
                int multNo = 1;
                if (isUpOrDn)
                {
                    singleIndex = 0;
                    multiIdnex = 0;
                }
                else
                {
                    singleIndex = labelnum * 2 - 1;
                    multiIdnex = labelnum * 2 - 1;
                }
                try
                {
                    foreach (var item in list)
                    {
                        decimal count = item.QUANTITY ?? 0;
                        if (count >= 1 && singleIndex >= 0 && singleIndex < 30)
                        {
                            //取pokeid
                            int PokeCount = item.POKEIDLIST.Count;
                            string[] pokes = new string[PokeCount];
                            int no = 0;
                            foreach (var it in item.POKEIDLIST)
                            {
                                pokes[no] = it;
                                no++;
                            }

                            int index;

                            if (isUpOrDn)
                            {
                                index = singleIndex;
                                singleIndex += 2;
                            }
                            else
                            {
                                index = (labelnum * 2 - 1) - singleIndex;
                                singleIndex -= 2;
                            }
                            Label lbl = (Label)box.Controls[index];
                            Label lab = (Label)box.Controls[index + 1];
                            //查询是否已经放烟，有则变色
                            if (HunHeService.GetTag(pokes))
                            {
                                updateLabel(singleNo + ":" + item.CIGARETTENAME + " " + count + "条", lbl, true);
                            }
                            else
                            {
                                updateLabel(singleNo + ":" + item.CIGARETTENAME + " " + count + "条", lbl);
                            }
                            //绑定pokeid
                            updateLabe2(item.POKEIDLIST, lab);


                            singleNo++;
                        }
                    }
                 
                    if (CheckTrough()) { falge = true; }
                }
                catch (Exception e)
                {
                    MessageBox.Show("放烟错误");
                    writeLog.Write("initTextUpOrDn()绑定数据时发生异常:" + e.Message);
                }
            }
        }
        /// <summary>
        /// 是否为特殊通道1061and2061
        /// </summary>
        /// <returns></returns>
        bool CheckTrough()
        {
            if (boxText[0] == "1061" || boxText[0] == "2061")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        void OpenWin(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void closeForm(object sender, EventArgs e)
        {
            stop = true;
            DialogResult MsgBoxResult = MessageBox.Show("确认退出系统?",//对话框的显示内容 
                             "操作提示",//对话框的标题 
                             MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                             MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                             MessageBoxDefaultButton.Button2);
            //继续尝试链接
            if (MsgBoxResult == DialogResult.Yes)
            {
                //System.Environment.Exit(0); 
                this.Dispose();
                this.Close();
                System.Environment.Exit(System.Environment.ExitCode);
            }


        }

        private void groupBox_Paint(object sender, PaintEventArgs e)
        {

            GroupBox box = (GroupBox)sender;
            //e.Graphics.Clear(box.BackColor);
            e.Graphics.DrawString(box.Text, box.Font, Brushes.Red, 10, 1);
            e.Graphics.DrawLine(Pens.Red, 1, 7, 8, 7);
            //e.Graphics.DrawLine(Pens.Red, e.Graphics.MeasureString(box.Text, box.Font).Width + 8, 7, box.Width - 2, 7);
            e.Graphics.DrawLine(Pens.Red, 1, 7, 1, box.Height - 2);
            e.Graphics.DrawLine(Pens.Red, 1, box.Height - 2, box.Width - 2, box.Height - 2);
            e.Graphics.DrawLine(Pens.Red, box.Width - 2, 7, box.Width - 2, box.Height - 2);
        }
        /// <summary>
        /// 动态添加label
        /// </summary>
        /// <param name="panelCount"></param>
        void addGroupBoxByNew(int panelCount)//2061 1061
        {

            if (boxText[0] == "1061" || boxText[0] == "2061"||boxText[0] == "1062" || boxText[0] == "2062")
            {
                int panelWidth = (Screen.PrimaryScreen.Bounds.Width - (padding * (2 + 1))) / 2;
                for (var i = 0; i < (panelCount); i++)
                {
                    GroupBox box = new GroupBox();
                    //box.Paint += groupBox_Paint;
                    box.Width = panelWidth;
                    box.BackColor = Color.Transparent;
                    box.Name = "orBox0";
                    box.Font = new Font("宋体", 25, FontStyle.Bold);
                    box.ForeColor = Color.Red;
                    box.Height = Screen.PrimaryScreen.Bounds.Height - topHeight - bottom;
                    //PaintPanelBorder(p, Color.Red, 5, ButtonBorderStyle.Solid);
                    box.Location = new Point(panelWidth * i + (padding * (i + 1)), topHeight + padding);
                    if (i == 0)
                    {
                        // box.Location = new Point(panelWidth * i + (padding * (i + 50)), topHeight + padding);
                        box.Text = "烟道:" + boxText[0];
                        this.Controls.Add(box);
                        panelList.Add(box);
                        addLabel(UnPullLabelNum, i);
                    }
                    if (i == 1)
                    {
                        box.Text = "待补 ";
                        box.Name = "orBox1";
                        this.Controls.Add(box);
                        panelList.Add(box);
                        addLabel(UnPullLabelNum, i, true);
                    }
                }
            }
            else
            {
                int panelWidth = (Screen.PrimaryScreen.Bounds.Width - (padding * (2 + 1))) / 4;
                for (var i = 0; i < (panelCount * 2); i++)
                {
                    GroupBox box = new GroupBox();
                    //box.Paint += groupBox_Paint;
                    box.Width = panelWidth;
                    box.BackColor = Color.Transparent;
                    box.Name = "orBox" + i;
                    box.Font = new Font("宋体", 25, FontStyle.Bold);
                    box.ForeColor = Color.Red;
                    box.Height = Screen.PrimaryScreen.Bounds.Height - topHeight - bottom + 80;
                    //PaintPanelBorder(p, Color.Red, 5, ButtonBorderStyle.Solid);
                    box.Location = new Point(panelWidth * i + (padding * (i + 1)), topHeight + padding);
                    if (i == 0)
                    {
                        box.Text = "通道" + boxText[0];
                        box.Name = "orBox" + i;
                        this.Controls.Add(box);
                        panelList.Add(box);
                        addLabel(HavePullLabelNum, i);

                    }
                    else if (i == 2)
                    {
                        box.Text = "通道" + boxText[1];
                        box.Name = "orBox" + i;
                        this.Controls.Add(box);
                        panelList.Add(box);
                        addLabel(HavePullLabelNum, i);
                    }
                    else
                    {
                        box.Text = "待放";
                        box.Name = "orBox" + i;
                        this.Controls.Add(box);
                        panelList.Add(box);
                        addLabel(UnPullLabelNum, i, true);
                    }
                }
            }
        }

        #region
        //void addGroupBox(int panelCount)
        //{
        //    int panelWidth = (Screen.PrimaryScreen.Bounds.Width - (padding * (panelCount + 1))) / panelCount;

        //    for (var i = 0; i < panelCount; i++)
        //    {
        //        GroupBox box = new GroupBox();
        //        //box.Paint += groupBox_Paint;
        //        box.Width = panelWidth;
        //        box.BackColor = Color.Transparent;
        //        box.Text = "通道" + boxText[i];
        //        box.Name = "orBox" + i;
        //        box.Font = new Font("宋体", 25, FontStyle.Bold);
        //        box.ForeColor = Color.Red;
        //        box.Height = Screen.PrimaryScreen.Bounds.Height - topHeight - bottom;
        //        //PaintPanelBorder(p, Color.Red, 5, ButtonBorderStyle.Solid);
        //        box.Location = new Point(panelWidth * i + (padding * (i + 1)), topHeight + padding);
        //        this.Controls.Add(box);
        //        panelList.Add(box);

        //    }
        //    addLabel(labelCount);
        //}
        #endregion
        void addLabel(int labelCount, int index, bool big = false)
        {
            for (var i = 0; i < labelCount; i++)
            {

                Label lbl = new Label();
                lbl.Width = panelList[index].Width - 2 * padding;
                lbl.BackColor = Color.White;
                if (big)
                {
                    if (i == 0)
                    {
                        lbl.Height = (panelList[index].Height - 2 * labelCount - boxTop - boxBottom) / labelCount + 20;
                        lbl.Location = new Point(padding, boxTop + (lbl.Height + 2) * i - 5);
                        lbl.Font = new Font("宋体", 22, FontStyle.Bold);
                    }
                    else
                    {
                        lbl.Height = (panelList[index].Height - 2 * labelCount - boxTop - boxBottom) / labelCount;
                        lbl.Location = new Point(padding, boxTop + (lbl.Height + 2) * i + 15);
                        lbl.Font = new Font("宋体", 16, FontStyle.Bold);
                    }
                }
                else
                {
                    lbl.Height = (panelList[index].Height - 2 * labelCount - boxTop - boxBottom) / labelCount;
                    lbl.Location = new Point(padding, boxTop + (lbl.Height + 2) * i);
                    lbl.Font = new Font("宋体", 16, FontStyle.Bold);
                }
                //lbl.Height = (panelList[index].Height - 2 * labelCount - boxTop - boxBottom) / labelCount;
                lbl.ForeColor = Color.Black;
                lbl.TextAlign = ContentAlignment.MiddleLeft;
                lbl.Text = "";
                lbl.Name = "lbl" + i;
                //lbl.Font = new Font("宋体", 16, FontStyle.Bold); 
                lbl.Click += new EventHandler(lbl_Click);//点击label事件 
                //lbl.Location = new Point(padding, boxTop + (lbl.Height + 2) * i);


                panelList[index].Controls.Add(lbl);


                Label lab = new Label();
                lab.Location = new Point(lbl.Width, boxTop + (lbl.Height + 2) * i);
                lab.BackColor = Color.Red;
                lab.Name = "lab" + i;
                lab.Visible = false;
                panelList[index].Controls.Add(lab);
            }
        }

        void pullcigarette(string ccode, string no, decimal machineseq)
        { 
            Label lab = (Label)Controls.Find("orBox" + no, true)[0].Controls.Find("lab0", true)[0];
            Label lbl = (Label)Controls.Find("orBox" + no, true)[0].Controls.Find("lbl0", true)[0];
            //MessageBox.Show(lbl.Parent.Name);
            decimal pokeid;
            if (lab.Text.Split('|').Length > 1)
            {
                pokeid = Convert.ToDecimal(lab.Text.Split('|').First());
            }
            else if (lab.Text.Length == 0)
            {
                return;
            }
            else
            {
                pokeid = Convert.ToDecimal(lab.Text);
            }
            //将要放烟的信息
            HUNHEVIEW1 hunhe = HunHeService.GetNextCigarette_bar(pokeid);

            Label lbl2 = (Label)Controls.Find("orBox" + (Convert.ToInt32(no) - 1), true)[0].Controls.Find("lbl14", true)[0];
            Label lab2 = (Label)Controls.Find("orBox" + (Convert.ToInt32(no) - 1), true)[0].Controls.Find("lab14", true)[0];

            //HUNHEVIEW1 hunhe2 =hunhe;
            //if (lab2.Text.Length > 0)
            //{
            //    hunhe = HunHeService.GetHaveCigarette_BAR(Convert.ToDecimal(lab2.Text.Split('|').First()));
            //}

            if (hunhe.PACK_BAR != ccode)
            {
                lbl.BackColor = Color.Red;
                //MessageBox.Show("放烟错误！");
            }
            else
            {
                lbl.BackColor = Color.White;
                HunHeService.PullTag(hunhe.POKEID, machineseq);
                getData(true);
                //数据显示缓存上限
                //if (lbl2.Text == "" || hunhe2 .PACK_BAR== hunhe.PACK_BAR)
                //{
                //    lbl.BackColor = Color.White;
                //    HunHeService.PullTag(hunhe.POKEID, machineseq);
                //    getData(true);
                //}
                //else
                //{
                //    lbl.BackColor = labelcolor;
                //    //MessageBox.Show("数据显示缓存已满！");
                //}

            }
        }

        //点击版
        void pullcigarette_dianji(string ccode, string no, decimal machineseq)
        {

            Label lab = (Label)Controls.Find("orBox" + no, true)[0].Controls.Find("lab0", true)[0];
            Label lbl = (Label)Controls.Find("orBox" + no, true)[0].Controls.Find("lbl0", true)[0];
            decimal pokeid;
            if (lab.Text.Split('|').Length > 1)
            {
                pokeid = Convert.ToDecimal(lab.Text.Split('|').First());
            }
            else if (lab.Text.Length == 0)
            {
                return;
            }
            else
            {
                pokeid = Convert.ToDecimal(lab.Text);
            }

            HUNHEVIEW hunhe = HunHeService.GetNextCigarette(pokeid);

            Label lbl2 = (Label)Controls.Find("orBox" + (Convert.ToInt32(no) - 1), true)[0].Controls.Find("lbl14", true)[0];
            Label lab2 = (Label)Controls.Find("orBox" + (Convert.ToInt32(no) - 1), true)[0].Controls.Find("lab14", true)[0];

            HUNHEVIEW hunhe2 = hunhe;
            if (lab2.Text.Length > 0)
            {
                hunhe = HunHeService.GetHaveCigarette(Convert.ToDecimal(lab2.Text.Split('|').First()));
            }

            if (hunhe2.CIGARETTECODE != ccode)
            {
                lbl.BackColor = Color.Red;
                //MessageBox.Show("放烟错误！");
            }
            else
            {
                HunHeService.PullTag(hunhe2.POKEID, machineseq);
                getData(true);
                //显示上限
                //if (lbl2.Text == "" || hunhe2.CIGARETTECODE == hunhe.CIGARETTECODE)
                //{
                //    HunHeService.PullTag(hunhe2.POKEID, machineseq);
                //    getData(true);
                //}
                //else
                //{
                //    lbl.BackColor = Color.Yellow;
                //    //MessageBox.Show("数据显示缓存已满！");
                //} 
            }
        }

        SerialPort sp = new SerialPort();
        SerialPort sp1 = new SerialPort();

        string sp1_name;
        public void OpenSerialPort1()
        {

            sp1.PortName = sp1_name;
            if (!sp1.IsOpen)
            {
                try
                {
                    sp1.ReadBufferSize = 32;
                    sp1.BaudRate = 9600;
                    sp1.Open();
                    sp1.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived1);
                }
                catch
                {
                    //if (sp!=null && sp.IsOpen)
                    //{
                    //    sp.Close();
                    //}
                    //Thread.Sleep(5000);
                    //OpenSerialPort();
                }
            }

        }


        string sp_name;
        public void OpenSerialPort()
        {

            sp.PortName = sp_name;
            if (!sp.IsOpen)
            {
                try
                {
                    sp.ReadBufferSize = 32;
                    sp.BaudRate = 9600;
                    sp.Open();
                    sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
                }
                catch
                {
                    //if (sp!=null && sp.IsOpen)
                    //{
                    //    sp.Close();
                    //}
                    //Thread.Sleep(5000);
                    //OpenSerialPort();
                }
            }

        }
        static string str;
        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = sender as SerialPort;
            String tempCode = sp.ReadExisting();
            str = tempCode.Split('\r').First();
            TextboxFZ3(1, str);
        }
        void sp_DataReceived1(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = sender as SerialPort;
            String tempCode = sp.ReadExisting();
            str = tempCode.Split('\r').First();
            TextboxFZ3(2, str);

        }

        private void TextboxFZ3(int id, string str)
        {
            try
            {
                if (this.txtbox2.InvokeRequired)
                {
                    FlushClient fc = new FlushClient(TextboxFZ3);
                    this.Invoke(fc, id, str); //通过代理调用刷新方法
                }
                else
                {
                    if (id == 2)
                    {
                        this.txtbox2.Text = str;
                        pullcigarette(txtbox2.Text, "3", Convert.ToDecimal(boxText.First()));
                    }
                    if (id == 1)
                    {
                        this.txtbox1.Text = str;
                        pullcigarette(txtbox1.Text, "1", Convert.ToDecimal(boxText.First()));

                    }
                }
            }
            catch (Exception e)
            {
                Label lab = (Label)Controls.Find("orBox" + (id == 1 ? 1 : 3), true)[0].Controls.Find("lab0", true)[0];
                lab.BackColor = Color.Blue;
                writeLog.Write(id+"号数据区域异常，" + e.Message);
            }
           
        }
        private delegate void FlushClient(int b, string a);



        string packbar_1;
        string packbar_2;

        private void btn1_Click(object sender, EventArgs e)
        {
            pullcigarette(txtbox1.Text, "1", Convert.ToDecimal(boxText.First()));
        }
        private void btn2_Click(object sender, EventArgs e)
        {
            pullcigarette(txtbox2.Text, "3", Convert.ToDecimal(boxText.Last()));
        }
        void lbl_Click(object sender, EventArgs e)
        {
            //父控件的名称
            string ParentControl = ((Label)sender).Parent.Name;


            string lblName = ((Label)sender).Name;
            string labName = "lab" + ((Label)sender).Name.Substring(3);
            string lbltext = ((Label)sender).Text;


            Label lbl = (Label)Controls.Find(ParentControl, true)[0].Controls.Find(lblName, true)[0];
            Label lab = (Label)Controls.Find(ParentControl, true)[0].Controls.Find(labName, true)[0];


            if (lbl.Name != "lbl0")
            {
                return;
            }

            if (ParentControl == "orBox1" || ParentControl == "orBox3")
            {

                if (lab.Text != "")
                {
                    decimal pokeid;
                    if (lab.Text.Split('|').Length > 1)
                    {
                        pokeid = Convert.ToDecimal(lab.Text.Split('|').First());
                    }
                    else
                    {
                        pokeid = Convert.ToDecimal(lab.Text);
                    }
                    HUNHEVIEW hunhe = HunHeService.GetNextCigarette(pokeid);

                    DialogResult dia = MessageBox.Show("确认放烟：" + hunhe.CIGARETTENAME + "一条？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dia == DialogResult.Cancel)
                    {
                        return;
                    }

                    if (ParentControl == "orBox3")
                    {
                        txtbox2.Text = hunhe.CIGARETTECODE;
                        pullcigarette_dianji(txtbox2.Text, "3", Convert.ToDecimal(boxText.First()));//暂时使用 点击版
                    }
                    if (ParentControl == "orBox1")
                    {
                        txtbox1.Text = hunhe.CIGARETTECODE;
                        pullcigarette_dianji(txtbox1.Text, "1", Convert.ToDecimal(boxText.First()));//暂时使用 点击版
                    }

                    //MessageBox.Show(ParentControl + "  " + ((Label)sender).Name + "\r" + lbl.Name + " "
                    //+ lbl.Text + "\r" + lab.Name + " " + lab.Text + "\r" + hunhe.CIGARETTENAME + " " + hunhe.CIGARETTECODE + " pokeid为" + hunhe.POKEID);
                }
            }
            /*
            decimal machineseq;
            if (boxText.Count() == 1)
            {
                machineseq = Convert.ToDecimal(boxText.First());
            }
            else
            {
                machineseq = Convert.ToDecimal(((Label)sender).Parent.Text.Substring(2, 4));
            }
            string[] list = lab.Text.Split('|');
            List<string> pokelist = new List<string>();
            for (int i = 0; i < list.Count(); i++)
            {
                pokelist.Add(list[i]);
            }
            if (lbl.BackColor == Color.White)
            {
                Convert.ToInt32(((Label)sender).Parent.Name.Substring(5, 1));
                if (InBound.Business.HunHeService.PullTag(pokelist, machineseq))
                {
                    lbl.BackColor = labelcolor;
                }
                //string ij = throughList.GetValue(Convert.ToInt32(((Label)sender).Parent.Name.Substring(5, 1))).ToString();
            }
            else if (lbl.BackColor == labelcolor)
            {
                if (InBound.Business.HunHeService.CancelTag(pokelist, machineseq))
                {
                    lbl.BackColor = Color.White;
                }
            }
             */

        }
        private delegate void HandleDelegate1(string info, Label label, bool tag, int falg);
        /// <summary>
        /// 存放烟仓的label
        /// </summary>
        /// <param name="info"></param>
        /// <param name="label"></param>
        /// <param name="tag">true为已放烟</param>
        public void updateLabel(string info, Label label, bool tag = false, int falg = 0)
        {
            if (falg == 1)
            {
                label.BackColor = Color.White;
            }

            String time = DateTime.Now.ToLongTimeString();
            if (tag)
            {
                label.BackColor = labelcolor;
            }
            if (label.InvokeRequired)
            {
                label.Invoke(new HandleDelegate1(updateLabel), new Object[] { info, label, tag = false, falg = 0 });
            }
            else
            {
                label.Text = info;
            }
        }
        private delegate void HandleDelegate2(List<string> info, Label label, bool tag);
        /// <summary>
        /// 存放pokeid的label
        /// </summary>
        /// <param name="info"></param>
        /// <param name="label"></param>
        /// <param name="tag"></param>
        public void updateLabe2(List<string> info, Label label, bool tag = false)
        {
            String time = DateTime.Now.ToLongTimeString();
            if (tag)
            {
                label.BackColor = Color.Red;
            }
            if (label.InvokeRequired)
            {
                label.Invoke(new HandleDelegate2(updateLabe2), new Object[] { info, label, tag = false });
            }
            else
            {
                string pokeid = null;
                for (int i = 0; i < info.Count; i++)
                {
                    if (pokeid == null)
                    {
                        pokeid = info[i];
                    }
                    else
                    {
                        pokeid += "|" + info[i];
                    }
                }
                label.Text = pokeid;
            }
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            stop = true;

        }

        public void Disconnect()
        {


            if (pIOPCServer != null)
            {
                Marshal.ReleaseComObject(pIOPCServer);
                pIOPCServer = null;
            }
            if (ShapeGroup != null)
            {
                ShapeGroup.Release();
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {


            //finishNo[0] = Convert.ToDecimal(txtbox4.Text);
            //finishNo[1] = Convert.ToDecimal(txtbox3.Text);
             
            getData(true);
            txtbox4.Text = finishNo[0].ToString();
            txtbox3.Text = finishNo[1].ToString();

            befoerFinishNo = finishNo;
        }
        

    }
}