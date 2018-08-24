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
namespace SpecialShapeSmoke
{
    public partial class MainScreen2 : Form
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
        HunHeService service = new HunHeService();
        Label chezu = new Label();
        System.Drawing.Color labelcolor;

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
        decimal[] dbIndex = new decimal[] { -1, -1 };//通道对应DB块索引
        // Dictionary<string, int>  rgDic = new Dictionary<string, int>();//存放通道对应值
        public MainScreen2()
        {
            InitializeComponent();

            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Hide();
            this.Show();
            Panel p = new Panel();
            // lineNum = ConfigurationManager.AppSettings["LineNum"].ToString();
            boxText = ConfigurationManager.AppSettings["troughList"].ToString().Replace(" ", "").Split(',');//通道编号 
            RefreshTime = ConfigurationManager.AppSettings["RefreshTime"].ToString().Replace(" ", "").CastTo<int>(20);//刷新时间
            // troughno = new decimal[boxText.Length];
            if (boxText.Length == 1)//根据通道编号查找DB对应值
            {
                dbIndex[0] = DicBind(boxText[0]);
            }
            else if (boxText.Length == 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    dbIndex[i] = DicBind(boxText[i]);
                }
            }
            if (CheckTrough())
            {
                falge = true;
                addGroupBoxByNew(2);
            }
            else
            {
                addGroupBoxByNew(boxText.Length);
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

            labelcolor = Color.Green;


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
            btnView.Width = 4 * topHeight;
            btnView.Height = topHeight;
            btnView.BackColor = Color.Silver;
            btnView.Font = new Font("宋体", 25, FontStyle.Bold);
            btnView.Text = "进度查看";
            btnView.Click += GetNowView;
            btnView.Location = new Point(p.Width - 8 * topHeight, 0);
            p.Controls.Add(btnView);


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

            Thread thread = new Thread(ConnectServer);
            thread.Start();
        }
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
                throughList[j] = GroupList(service.GetTroughCigarette(Convert.ToDecimal(boxText[j]), finishNo[j], 300));//第二个 
                //initText(panelList[j], throughList[j]);
                initTextUpOrDn(panelList[j], throughList[j], UpOrDn);
            }
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
        decimal DicBind(string troughno)
        {
            decimal positiong = 0;
            if (troughno == "1001")
            {
                positiong = 0;
            }
            else if (troughno == "1002")
            {
                positiong = 1;
            }
            else if (troughno == "1059")
            {
                positiong = 2;
            }
            else if (troughno == "1060")
            {
                positiong = 3;
            }
            else if (troughno == "1061")
            {
                positiong = 4;
            }
            else if (troughno == "2001")
            {
                positiong = 5;
            }
            else if (troughno == "2002")
            {
                positiong = 6;
            }
            else if (troughno == "2059")
            {
                positiong = 7;
            }
            else if (troughno == "2060")
            {
                positiong = 8;
            }
            else if (troughno == "2061")
            {
                positiong = 9;
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
                ShapeGroup.addItem(ItemCollection.GetTaskStatusByShapeItem());
                //ShapeGroup.callback += OnDataChange;

                if (checkConnection()) //连接服务器成功 
                {
                    while (true)
                    {
                        getData();
                        Thread.Sleep(RefreshTime * 500);//每20秒刷新一次
                    }
                }
            }
            catch (Exception e)
            {
                writeLog.Write(e.Message);
            }

            //socket = new ClientSocket(ipaddress, PORT);
            //socket.method+=getData;
            //socket.startListen();
        }

        public bool checkConnection()
        {
            int flag = ShapeGroup.Read(0).CastTo<int>(-1);
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
            // 数据清空
            //try
            //{


            throughList = new List<HUNHEVIEW>[boxText.Length];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < panelList[i].Controls.Count; j++)
                {
                    Label lbl = (Label)panelList[i].Controls[j];
                    updateLabel("", lbl,falg:1);
                }
            }
            //}
            //catch (Exception e)
            //{

            //    writeLog.Write("clearAllText():" + e.Message + "/r/n" + e.Source);
            //}

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


            //writeLog.Write("Receive Resend Data:"+data);
            //try
            //{ 
                
                int countnum = 0;
                decimal[] finishNo = new decimal[2];//完成信号 (taskNum)
                //finishNo[0] = -1;
                //finishNo[1] = -1;

                string Log = "";

                #region  读取DB
                if (dbIndex[1] == -1)//  是1061 和2061 单个通道？
                {
                    finishNo[0] = ShapeGroup.Read((int)dbIndex[0]).CastTo<int>(-1);//根据通道 读取DB块  Read  
                    countnum = 1;

                }
                else
                {
                    finishNo[0] = ShapeGroup.Read((int)dbIndex[0]).CastTo<int>(-1); //两个通道
                    finishNo[1] = ShapeGroup.Read((int)dbIndex[1]).CastTo<int>(-1);
                    countnum = 2;
                }

                #endregion

                if (finishNo[0] != -1 || finishNo[1] != -1|| Refresh)
                {
                    if (befoerFinishNo.Sum() != finishNo.Sum())
                    {
                        for (int i = 0; i < boxText.Length; i++)
                        {
                            Log += "通道 " + boxText[i] + " 接收DB块值:" + finishNo[i] + "\r\n";
                        }
                        writeLog.Write(Log);
                       

                        if ((finishNo.Sum() >= befoerFinishNo.Sum()) || Refresh)
                        { 
                            clearAllText();
                            throughList = new List<HUNHEVIEW>[boxText.Length];
                            //// if (CheckTrough()) { countnum = 1; } else { countnum = 2; }
                            for (int j = 0; j < countnum; j++)//数据获取核心
                            {
                                throughList[j] = GroupList(service.GetTroughCigarette(Convert.ToDecimal(boxText[j]), finishNo[j], 300));//第二个 
                                //initText(panelList[j], throughList[j]);
                                initTextUpOrDn(panelList[j], throughList[j], true);
                            }

                            if (throughList[0].Count <= 0) //根据不同通道完成来显示完成任务 
                            {
                                Label lbl2 = (Label)Controls.Find("orBox" + 0, true)[0].Controls[0];
                                updateLabel("分拣任务完成!分拣结束!", lbl2);

                            }
                            if (boxText.Length == 2)
                            {
                                if (throughList[1].Count <= 0 && !CheckTrough())
                                {
                                    Label lbl2 = (Label)Controls.Find("orBox" + 1, true)[0].Controls[0];
                                    updateLabel("分拣任务完成!分拣结束!", lbl2);
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

        }
        NowView fNowView;
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
            //fNowView = new NowView(machineseq1, machineseq2, finishNo);/*暂 注*/

            fNowView.Show();
            fNowView.Activate();

            SearchWinForm(fNowView);
          

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

        //public void t1_Tick(object sender, EventArgs e)
        //{
        //    finishNo[0] = 244;
        //    finishNo[1] = 0;
        //    t1.Stop();
        //    //t2.Interval = 10000;
        //    //t2.Start();
        //}
        //public void t2_Tick(object sender, EventArgs e)
        //{
        //    finishNo[0] = 205;
        //    finishNo[1] = 214;
        //    t2.Stop();
        //}


        public void Refresh(object sender, EventArgs e)//刷新
        {
           
            getData(true);
        }
        public void initText(GroupBox box, List<HUNHEVIEW> list)
        {
            if (box != null && list != null)
            {
                int i = 14;
                var newlist = list.Skip(15).Take(1000).ToList(); //获取多于15
                int no = 1;
                //int z = newlist.Count();//取剩余烟数

                //try
                //{
                    foreach (var item in list)
                    {
                        decimal count = item.QUANTITY ?? 0;
                        if (count >= 1 && i >= 0 && no <= 15)
                        {
                            Label lbl = (Label)box.Controls[i];
                            Label lab = (Label)box.Controls[i + 15];
                            i--;
                            updateLabel(no + ":  " + item.CIGARETTENAME + ":" + count + "条", lbl);
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
                                    updateLabel((newno + 15) + ":  " + item2.CIGARETTENAME + ":" + count2 + "条", lbl2);
                                    newno++;
                                }
                            }
                            falge = false;
                        }
                    }

                    if (CheckTrough()) { falge = true; }
                //}
                //catch (Exception e)
                //{
                //    writeLog.Write("initText():" + e.Message);
                //}
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="box"></param>
        /// <param name="list"></param>
        /// <param name="isUpOrDn">默认为真，真则从上往下显示，反亦之</param>
        public void initTextUpOrDn(GroupBox box, List<HUNHEVIEW> list, bool isUpOrDn = false)
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
                    singleIndex = 29;
                    multiIdnex = 29;
                }
                //try
                //{
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

                        int index ;

                        if (isUpOrDn)
                        {
                            index = 28 - singleIndex;
                            singleIndex += 2;
                        }
                        else
                        {
                            index = 29 - singleIndex;
                            singleIndex -= 2;
                        }
                        Label lbl = (Label)box.Controls[index];
                        Label lab = (Label)box.Controls[index + 1];
                        //查询是否已经放烟，有则变色
                        if (HunHeService.GetTag(pokes))
                        {
                            updateLabel(singleNo + ":  " + item.CIGARETTENAME + ":" + count + "条", lbl, true);
                        }
                        else
                        {
                            updateLabel(singleNo + ":  " + item.CIGARETTENAME + ":" + count + "条", lbl);
                        }
                        //绑定pokeid
                        updateLabe2(item.POKEIDLIST, lab);
                        

                        singleNo++;
                    }
                }
                if (falge && newlist.Count > 0)//用于单通道多显示2061 and  1061
                {
                    int multiNo = 1;
                    foreach (var item in newlist)
                    {
                        
                        decimal count = item.QUANTITY ?? 0;
                        if (count >= 1 && multiIdnex >= 0 && multiIdnex < 30)
                        {
                            int index;
                            if (isUpOrDn)
                            {
                                index = 28 - multiIdnex;
                                multiIdnex += 2;
                            }
                            else
                            {
                                index = 29 - multiIdnex;
                                multiIdnex -= 2;
                            } 
                            Label lbl = (Label)Controls.Find("orBox" + 1, true)[0].Controls[index];
                            Label lab = (Label)Controls.Find("orBox" + 1, true)[0].Controls[index+1];
                            //取pokeid
                            int PokeCount = item.POKEIDLIST.Count;
                            string[] pokes = new string[PokeCount];
                            int no = 0;
                            foreach (var it in item.POKEIDLIST)
                            {
                                pokes[no] = it;
                                no++;
                            }
                            
                            //查询是否已经放烟，有则变色
                            if (HunHeService.GetTag(pokes))
                            {
                                updateLabel(multNo + ":  " + item.CIGARETTENAME + ":" + count + "条", lbl, true);
                            }
                            else
                            {
                                updateLabel(multNo + ":  " + item.CIGARETTENAME + ":" + count + "条", lbl);
                            }
                            //绑定pokeid
                            updateLabe2(item.POKEIDLIST, lab);
                            if (isUpOrDn)
                            {
                                singleIndex++;
                            }
                            else
                            {
                                singleIndex -= 2;
                            }

                            multNo++;
                        }
                    }


                    falge = false;

                }

                    if (CheckTrough()) { falge = true; }
                //}
                //catch (Exception e)
                //{
                 //   writeLog.Write("initText():" + e.Message);
               // }
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

        void addGroupBoxByNew(int panelCount)//2061 1061
        {
            int panelWidth = (Screen.PrimaryScreen.Bounds.Width - (padding * (2 + 1))) / 2;
            for (var i = 0; i < panelCount; i++)
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
                if (boxText[0] == "1061" || boxText[0] == "2061")
                {
                    if (i == 0)
                    {
                        // box.Location = new Point(panelWidth * i + (padding * (i + 50)), topHeight + padding);
                        box.Text = "通道:" + boxText[0];
                    }
                    if (i == 1)
                    {
                        box.Text = "接上 ";
                        box.Name = "orBox1";
                    }
                }
                else
                {
                    box.Text = "通道" + boxText[i];
                    box.Name = "orBox" + i;
                }
                this.Controls.Add(box);
                panelList.Add(box);
            }
            addLabel(labelCount);
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
        void addLabel(int labelCount)
        {
            foreach (var item in panelList)
            {
                for (var i = 0; i < labelCount; i++)
                {
                    Label lbl = new Label();
                    lbl.Width = item.Width - 2 * padding;
                    lbl.BackColor = Color.White;
                    lbl.Height = (item.Height - 2 * labelCount - boxTop - boxBottom) / labelCount;
                    lbl.ForeColor = Color.Black;
                    //lbl.BorderStyle
                    lbl.TextAlign = ContentAlignment.MiddleLeft;
                    lbl.Text = "";
                    lbl.Name = "lbl" + i;
                    lbl.Font = new Font("宋体", 25, FontStyle.Bold);//
                    lbl.Click += new EventHandler(lbl_Click);//点击label事件 
                    lbl.Location = new Point(padding, boxTop + (lbl.Height + 2) * i);
                    item.Controls.Add(lbl);

                    Label lab = new Label();
                    lab.Location = new Point(lbl.Width, boxTop + (lbl.Height + 2) * i);
                    lab.BackColor = Color.Red;
                    lab.Name = "lab" + i;
                    lab.Visible = false;
                    lab.Enabled = false;
                    item.Controls.Add(lab);
                }
            }

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
             

        }
        private delegate void HandleDelegate1(string info, Label label, bool tag,int falg);
        /// <summary>
        /// 放烟标志
        /// </summary>
        /// <param name="info"></param>
        /// <param name="label"></param>
        /// <param name="tag">true为已放烟</param>
        public void updateLabel(string info, Label label, bool tag = false,int falg=0)
        {
            if (falg==1)
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
                label.Invoke(new HandleDelegate1(updateLabel), new Object[] { info, label,tag = false,falg=0 });
            }
            else
            {
                label.Text = info;
            }
        }
        private delegate void HandleDelegate2(List<string> info, Label label, bool tag);
        public void updateLabe2(List<string> info, Label label, bool tag = false)
        {
            String time = DateTime.Now.ToLongTimeString();
            if (tag)
            {
                label.BackColor = Color.Red;
            }
            if (label.InvokeRequired)
            {
                label.Invoke(new HandleDelegate2(updateLabe2), new Object[] { info, label,tag = false });
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


    }
}
