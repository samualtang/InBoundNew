﻿using System;
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
namespace SpecialShapeSmoke
{
    public partial class MainScreen1 : Form
    {
        WriteLog writeLog = new WriteLog();
        List<GroupBox> panelList = new List<GroupBox>();
        int topHeight = 57;
        int padding = 10;
        int bottom = 100;
        int labelCount = 15;
        int labelHeight = 50;
        int boxTop = 40;
        int boxBottom = 10;
        bool stop = false;
        String lineNum = "0";
        Label chezu = new Label();
        public string[] boxText = null;
        public decimal[] troughno = null;
        public MainScreen1()
        {
            InitializeComponent();
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Hide();
            this.Show();
            Panel p = new Panel();
            //lineNum = ConfigurationManager.AppSettings["LineNum"].ToString();

            boxText =  ConfigurationManager.AppSettings["troughList"].ToString().Split(',');
            lineNum = boxText.Length.ToString();//改为了通道个数
            troughno = new decimal[boxText.Length];
            for (int i = 0; i < boxText.Length; i++)
            {
                troughno[i] =Convert.ToDecimal( boxText[i]) ;
            }

            //if (lineNum == "1")
            //{
            //    boxText = new String[] { "1059", "1060", "1061", };
            //    troughno = new decimal[] { 1059, 1060, 1061 };
            //}
            //else if(lineNum =="2")
            //{
            //   //动态添加
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
            search.Width = 2*topHeight;
            search.Height = topHeight;
            search.BackColor = Color.Silver;
            search.Font = new Font("宋体", 25, FontStyle.Bold);
            search.Text = "刷新";
            search.Click += Refresh;
            search.Location = new Point(p.Width - 4*topHeight, 0);
            p.Controls.Add(search);

           

            //Button refresh = new Button();
            //refresh.Width = 2 * topHeight;
            //refresh.Height = topHeight;
            //refresh.BackColor = Color.Silver;
            //refresh.Font = new Font("宋体", 25, FontStyle.Bold);
            //refresh.Text = "刷新";
            //refresh.Click += Refresh;
            //refresh.Location = new Point(p.Width - 7 * topHeight, 0);
            //p.Controls.Add(refresh);

            addGroupBox(int.Parse( lineNum));
            Thread thread = new Thread(ConnectServer);
            thread.Start();
            
            


        }
       
        public List<HUNHEVIEW> GroupList(List<HUNHEVIEW> list)
        {
            if (list != null)
            {
                List<HUNHEVIEW> temp = new List<HUNHEVIEW>();
                HUNHEVIEW tempview =null;
                int count =0;
                foreach(var item in list)
                {
                    count++;
                    if (tempview == null)
                    {
                        tempview = item;
                    }
                    else if (item.CIGARETTECODE != tempview.CIGARETTECODE)
                    {
                        temp.Add(tempview);
                        tempview = item;
                        
                    }
                    else
                    {
                        tempview.QUANTITY += item.QUANTITY;
                    }
                    if (count == list.Count)
                    {
                        temp.Add(tempview);
                    }
                }
                return temp;
            }
            else
            {

                return null;
            }
        }
        void ConnectServer()
        {

            while (true)
            {
                getData();
                Thread.Sleep(2000);
            }
            //socket = new ClientSocket(ipaddress, PORT);
            //socket.method+=getData;
            //socket.startListen();
        }

        HunHeService service = new HunHeService();
        //List<HUNHEVIEW> through1 = new List<HUNHEVIEW>();
        //List<HUNHEVIEW> through2 = new List<HUNHEVIEW>();
        //List<HUNHEVIEW> through3 = new List<HUNHEVIEW>();

        //通道集合
        List<HUNHEVIEW>[] throughList;
        public void clearAllText()
        {
            throughList =  new List<HUNHEVIEW>[Convert.ToInt32(lineNum)];

            for (int i = 0; i < throughList.Length; i++)
            {
                for (int j = 0; j < panelList[i].Controls.Count; j++)
                {
                    Label lbl = (Label)panelList[i].Controls[j];
                    updateLabel("", lbl);
                }
            }

          
        }
        public void getData()
        {
               // writeLog.Write("Receive Resend Data:"+data);
                clearAllText();
                try
                {
                    for (int i = 0; i < throughList.Length; i++)
                    {
                        throughList[i]= GroupList(service.GetTroughCigarette(troughno[i], 300));
                        initText(panelList[i], throughList[i]);
                    }
                    
                    //var item = service.GetBeginTask();
                    //if (item != null && item.Count > 0)
                    //{
                    //    updateLabel("当前车组号：" + item[0].REGIONCODE, chezu);
                    //}
                }
                catch (Exception e)
                {
                    writeLog.Write(e.Message);
                }
                //MessageBox.Show(data);

        }

        public void Refresh(object sender, EventArgs e)
        {
            getData();
        }
        public void initText(GroupBox box, List<HUNHEVIEW> list)
        {
            if (box != null && list != null)
            {
                int i = labelCount-1;
                try
                {
                    foreach (var item in list)
                    {
                        decimal count = item.QUANTITY??0;
                        if (count >= 1)
                        {
                            Label lbl = (Label)box.Controls[i];
                            i--;
                            updateLabel(item.CIGARETTENAME+":"+count+"条", lbl);
                            //for (var item2 = 0; item2 < count; item2++)
                            //{
                               
                            //    updateLabel(item.CIGARETTENAME , lbl);
                            //}
                        }
                       
                    }
                }
                catch
                { }
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
                 //System.Environment.Exit(System.Environment.ExitCode);
                 this.Dispose();
                this.Close();
               
                
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
      
        void addGroupBox(int panelCount)
        {
            int panelWidth = (Screen.PrimaryScreen.Bounds.Width-(padding*(panelCount+1))) / panelCount;
            
            for (var i = 0; i < panelCount; i++)
            {
                GroupBox box = new GroupBox();
                //box.Paint += groupBox_Paint;
                box.Width = panelWidth;
                box.BackColor = Color.Transparent;
                box.Text = "通道" + boxText[i];
                box.Font = new Font("宋体", 25, FontStyle.Bold);
                box.ForeColor = Color.Red;
                box.Height = Screen.PrimaryScreen.Bounds.Height - topHeight - bottom;
                //PaintPanelBorder(p, Color.Red, 5, ButtonBorderStyle.Solid);
                box.Location = new Point(panelWidth * i+(padding*(i+1)), topHeight+padding);
                this.Controls.Add(box);
                panelList.Add(box);
            }
            addLabel(labelCount);
        }

        void addLabel(int labelCount)
        {
            foreach (var item in panelList)
            {
                for (var i = 0; i < labelCount; i++)
                {
                    Label lbl = new Label();
                    lbl.Width = item.Width-2*padding;
                    lbl.BackColor = Color.BlueViolet;
                    lbl.Height = (item.Height - 2 * labelCount - boxTop-boxBottom) / labelCount;
                    lbl.ForeColor = Color.Black;
                    //lbl.BorderStyle
                    lbl.TextAlign = ContentAlignment.MiddleLeft;
                    lbl.Text = "";
                    lbl.Font = new Font("宋体", 25, FontStyle.Bold);//
                    lbl.Location = new Point(padding, boxTop + (lbl.Height + 2) * i);
                    item.Controls.Add(lbl);
                }
            }

        }
        private delegate void HandleDelegate1(string info, Label label);
        public void updateLabel(string info, Label label)
        {
            String time = DateTime.Now.ToLongTimeString();
            if (label.InvokeRequired)
            {
                label.Invoke(new HandleDelegate1(updateLabel), new Object[] { info, label });
            }
            else
            {
                label.Text = info;

            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e); 
            stop = true;
           
        }

    }
}
