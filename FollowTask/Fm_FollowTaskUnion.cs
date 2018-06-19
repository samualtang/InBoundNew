using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound;
using OpcRcw.Da;
using Machine;

using FollowTask.Modle;
using InBound.Business;
using InBound.Model;
using System.Threading;
 

namespace FollowTask
{
    public partial class Fm_FollowTaskUnion : Form
    {
        public Fm_FollowTaskUnion()
        {
            InitializeComponent();
            list_data.Items.Clear();
        }
      
     
        AutoSizeFormClass asc = new AutoSizeFormClass();
 
        #region   变量
        public WriteLog writeLog = WriteLog.GetLog();
        DeviceStateManager stateManager = new DeviceStateManager();
        Alarms alarms = new Alarms();
        /// <summary>
        /// 主皮带
        /// </summary>
        static int mainbelt ;
        /// <summary>
        /// 组号
        /// </summary>
        static int groupno; 
        /// <summary>
        /// 任务号
        /// </summary>
        static decimal SortNum;
        /// <summary>
        /// 吸烟数量
        /// </summary>
        static decimal[] xyNum = new decimal[8];

        /// <summary>
        /// 当前机械手之后的烟
        /// </summary>
        List<UnionTaskInfo> listafter = new List<UnionTaskInfo>();

        /// <summary>
        /// 当前机械手之前的烟
        /// </summary>
        List<UnionTaskInfo> listbefore = new List<UnionTaskInfo>();

        /// <summary>
        /// 存放参数
        /// </summary>
        List<object> listPrament = new List<object>();
        #region 图片
        /// <summary>
        /// A线机械手
        /// </summary>
        Bitmap Amachine = (Bitmap)Properties.Resources.ResourceManager.GetObject("A线机械手");
        /// <summary>
        /// B线机械手
        /// </summary>
        Bitmap Bmachine = (Bitmap)Properties.Resources.ResourceManager.GetObject("B线机械手");
        /// <summary>
        /// 合流故障机械手
        /// </summary>
        Bitmap UnionErrormachine = (Bitmap)Properties.Resources.ResourceManager.GetObject("合流故障机械手");
        #endregion

        public delegate void GetNeedInfo(int machineno,List<UnionTaskInfo> after ,List<UnionTaskInfo> before,List<object> listparm);
        public GetNeedInfo getInfo;

        //Fm_UnionMainBelt fm_UnionDetail = new Fm_UnionMainBelt();
        List<Group> listgroup = new List<Group>();
        #endregion
    

        List<Group> listuinongroup = new List<Group>();
       
        public void GetMainInfo(string text, List<Group> listgroup, bool isonline)
        {

            IsOnLine = isonline;
            listuinongroup = listgroup;
            if (IsOnLine)
            {
                updateListBox(text + "主皮带,服务器连接成功,应用程序启动");
                writeLog.Write(text + "主皮带,服务器连接成功,应用程序启动");
                
            
                CheckForIllegalCrossThreadCalls = false;
                asc.controllInitializeSize(this); 

            }
            else
            {
                updateListBox(text + "主皮带,服务器连接失败,请检查网络连接");
                writeLog.Write(text + "主皮带,服务器连接失败,请检查网络连接");
            }
        }
 
        private void Fm_FollowTaskUnion_Load(object sender, EventArgs e)
        {
            try
            {
                lblGourpText.Text = this.Text + "主皮带";
                BindLabelName(); 
            }
            catch (Exception ex)
            { 
                updateListBox("异常错误：" + ex.Message);
                writeLog.Write("异常错误：" + ex.Message);
            }
        }
        Fm_UnionMainBelt fm_un = null;
        #region listBox显示
       

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
     
        private void Machine1_Click(object sender, EventArgs e)
        {
            Fm_UnionMainBelt fm_UiMainbelt = new Fm_UnionMainBelt();
            fm_un = fm_UiMainbelt;
            getInfo += fm_UiMainbelt.GetNeedInfo;
            if (IsOnLine)
            {
                try
                {
                    PictureBox btn = ((PictureBox)sender);//获取当前单击的实例
                    listPrament.Clear();
                    int machineno = Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(btn.Name, @"[^0-9]+", "")); //获取机械手
                    mainbelt = (int)Math.Ceiling(((double)machineno / 8));//获取主皮带
                    if (mainbelt == 1)
                    {
                        ReadDBInfo(listuinongroup[0]);
                    }
                    if (mainbelt == 2)
                    {
                        ReadDBInfo(listuinongroup[1]);
                    }
                    if (mainbelt == 3)
                    {
                        ReadDBInfo(listuinongroup[2]);
                    }
                    if (mainbelt == 4)
                    {
                        ReadDBInfo(listuinongroup[3]);
                    }

                    if (SortNum != -1)
                    {
                        groupno = GetGroupNo(machineno);//获取组号
                        if (xyNum[GetXyNumIndex(machineno)] != 0)
                        {
                            listafter = UnionTaskInfoService.GetUnionTaskInfoAfter(mainbelt, groupno, SortNum, xyNum[GetXyNumIndex(machineno)]);//机械手之后
                            listbefore = UnionTaskInfoService.GetUnionTaskInfoBefore(mainbelt, groupno, SortNum, xyNum[GetXyNumIndex(machineno)]);//机械手之前

                            listPrament.Add(mainbelt);//主皮带
                            listPrament.Add(groupno);//组号
                            listPrament.Add(SortNum);//任务号
                            listPrament.Add(xyNum[GetXyNumIndex(machineno)]);//吸烟数量

                            getInfo(machineno, listafter, listbefore, listPrament);
                            fm_UiMainbelt.Show();
                            SearchWinForm(fm_UiMainbelt);
                        }
                        else
                        {
                            listPrament.Add(-1);//主皮带
                            listPrament.Add(-1);//组号
                            listPrament.Add(-1);//任务号
                            listPrament.Add(-1);//吸烟数量
                            getInfo(machineno, listafter, listbefore, listPrament);
                            fm_UiMainbelt.Show();
                            SearchWinForm(fm_UiMainbelt);
                        }
                    }
                    else
                    {
                        MessageBox.Show("服务器连接失败!请检查网络连接!");
                    }

                }
                catch (Exception ex)
                {
                    updateListBox("与服务器断开连接!");
                    writeLog.Write("单击机械手皮带信息错误:" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("尚未连接服务器,请稍候再试！");
            }


        }
        /// <summary>
        /// 读取指定皮带DB块
        /// </summary>
        /// <param name="group">OPC组名</param>
        void ReadDBInfo(Group group)
        {
         
            SortNum = group.ReadD(0).CastTo<int>(-1);//当前任务号
            if (SortNum != -1)
            {
                //八个机械手吸烟数量
                xyNum[0] = group.ReadD(4).CastTo<int>(-1);
                xyNum[1] = group.ReadD(5).CastTo<int>(-1);
                xyNum[2] = group.ReadD(6).CastTo<int>(-1);
                xyNum[3] = group.ReadD(7).CastTo<int>(-1);
                xyNum[4] = group.ReadD(8).CastTo<int>(-1);
                xyNum[5] = group.ReadD(9).CastTo<int>(-1);
                xyNum[6] = group.ReadD(10).CastTo<int>(-1);
                xyNum[7] = group.ReadD(11).CastTo<int>(-1);
            }
        }
       
        /// <summary>
        /// 连接标识符
        /// </summary>
        bool IsOnLine =false;
        /// <summary>
        /// 获取组号
        /// </summary>
        /// <param name="machineNo">机械手号</param>
        /// <returns></returns>4   
        int GetGroupNo(int machineNo)
        {
            if (machineNo >= 8)
            {
                groupno = machineNo % 8;// Convert.ToDecimal(Math.IEEERemainder(machineNo, 8));//获得组号
            }
            else
            {
                groupno = machineNo;
            }
            if (groupno == 0)
            {
                groupno = 8;
            }
            return groupno;
        }
        /// <summary>
        /// 吸烟数量索引
        /// </summary>
        /// <param name="machineNo">机械手号</param>
        /// <returns></returns>
        int GetXyNumIndex(int machineNo)
        { 
            if (machineNo >= 9 && machineNo <= 16)
            {
                return (machineNo - 8) - 1;
            }
            if (machineNo >= 17 && machineNo <= 24)
            {
                return (machineNo - 8 * 2) - 1;
            }
            if (machineNo >= 25 && machineNo <= 32)
            {
                return (machineNo - 8 * 3) - 1;
            }
            return machineNo -1;
        }
        public void SearchWinForm(Form fname)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is Form)
                {
                    //fname.TopMost = true;
                    fname.Activate();

                    return;
                }
            }
            fname.Show();
            fname.Activate();
        }

       

        
        #region  暂时无用
        ///// <summary>
       ///// 获取当前主皮带机械手号
       ///// </summary>
       ///// <returns></returns>
       // decimal[] GetMachineNos()
       // {
       //     machinenos[0] = mainbelt * 8 - 7;
       //     machinenos[1] = mainbelt * 8 - 6;
       //     machinenos[2] = mainbelt * 8 - 5;
       //     machinenos[3] = mainbelt * 8 - 4;
       //     machinenos[4] = mainbelt * 8 - 3;
       //     machinenos[5] = mainbelt * 8 - 2;
       //     machinenos[6] = mainbelt * 8 - 1;
       //     machinenos[7] = mainbelt * 8 - 0;
       //     return machinenos; 
       // }
       // /// <summary>
       // /// 获取当前皮带组号
       // /// </summary>
       // /// <returns></returns>
       // decimal[] GetGroupNos()
       // {
       //     for (int i = 0; i < machinenos.Length; i++)
       //     {
       //        groupnos[i] =   GetGroupNo(machinenos[i]);
       //     }
       //     return groupnos; 
        // }
        #endregion
        bool errorMachine = false;
        
        void BindLabelName()
        {
            int j = 1;
            for (int i = 1; i <= 32; i++)
            {
                string labelName = "label" + j;
                Control label = Controls.Find(labelName, true)[0];

                string pbName = "pbMachine" + j;
                Control pbox = Controls.Find(pbName, true)[0];
                PictureBox picutB = (PictureBox)pbox;
              
               // label.Parent = picutB;
                label.BackColor = Color.Transparent;
                label.BringToFront();
                if (Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(pbName,  @"[^0-9]+", "")) % 2 != 0)//A线
                {
                    label.BackColor = Color.FromArgb(234, 255, 0);
                    picutB.Image = Amachine;  
                }
                else//B线
                {
                    picutB.Image = Bmachine;
                    label.BackColor = Color.FromArgb(0, 255, 255);
                }
                if (errorMachine)//机械手报警
                {
                    picutB.Image = UnionErrormachine;
                    lblGourpText.BackColor = Color.FromArgb(255, 0, 0);
                }
                j++;
            }
            #region
            //if (Text.Contains("1"))
            //{
            //    int j = 1;
            //    for (int i = 1; i <= 8; i++)
            //    {

            //        string labelName = "label" + j;
            //        Control control2 = Controls.Find(labelName, true)[0];
            //        control2.Text = i + "";

            //        string pbName = "pbMachine" + j;
            //        Control control1 = Controls.Find(pbName, true)[0];
            //        control1.Name = "pbMachine" + i;
            //        j++;
            //    }
            //    //th2.Abort();
            //}
            //if (Text.Contains("2")) // || groupText.Contains("五") || groupText.Contains("七"
            //{
            //    int j = 1;
            //    for (int i = 9; i <= 16; i++)
            //    {
            //        string labelName = "label" + j;
            //        Control control2 = Controls.Find(labelName, true)[0];
            //        control2.Text = i + "";


            //        string pbName = "pbMachine" + j;
            //        Control control1 = Controls.Find(pbName, true)[0]; 
            //        control1.Name =  "pbMachine"+i ;
            //        j++;
            //    }
            //    //th2.Abort();
            //}
            //if (Text.Contains("3"))
            //{
            //    int j = 1;
            //    for (int i = 17; i <= 24; i++)
            //    {
            //        string labelName = "label" + j;
            //        Control control2 = Controls.Find(labelName, true)[0];
            //        control2.Text = i + "";

            //        string pbName = "pbMachine" + j;
            //        Control control1 = Controls.Find(pbName, true)[0];
            //        control1.Name = "pbMachine" + i;
            //        j++;
            //    }
            //    //th2.Abort();
            //}
            //if (Text.Contains("4"))
            //{
            //    int j = 1;
            //    for (int i = 25; i <= 32; i++)
            //    {
            //        string labelName = "label" + j;
            //        Control control2 = Controls.Find(labelName, true)[0];
            //        control2.Text = i + "";

            //        string pbName = "pbMachine" + j;
            //        Control control1 = Controls.Find(pbName, true)[0];
            //        control1.Name = "pbMachine" + i;
            //        j++;
            //    }
            //    //th2.Abort();
            //}
            #endregion
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        //public void BtnText()
        //{
        //    if (Text.Contains("1"))
        //    {
        //        int j = 1;
        //        for (int i = 1; i <= 8; i++)
        //        {
        //            string labelName = "label" + j;
        //            Control control2 = Controls.Find(labelName, true)[0];

        //            bindDate(control2, j);
        //            j++;
        //        }
        //        th.Abort();
        //    }
        //    if (Text.Contains("2")) // || groupText.Contains("五") || groupText.Contains("七"
        //    {
        //        int j = 1;
        //        for (int i = 9; i <= 16; i++)
        //        {
        //            string labelName = "label" + j;
        //            Control control2 = Controls.Find(labelName, true)[0];
        //            bindDate(control2, j);
        //            j++;
        //        }
        //        th.Abort();
        //    }
        //    if (Text.Contains("3"))
        //    {
        //        int j = 1;
        //        for (int i = 17; i <= 24; i++)
        //        {
        //            string labelName = "label" + j;
        //            Control control2 = Controls.Find(labelName, true)[0];
        //            bindDate(control2, j);
        //            j++;
        //        }
        //        th.Abort();
        //    }
        //    if (Text.Contains("4"))
        //    {
        //        int j = 1;
        //        for (int i = 25; i <= 32; i++)
        //        {
        //            string labelName = "label" + j;
        //            Control control2 = Controls.Find(labelName, true)[0];
        //            bindDate(control2, j);
        //            j++;
        //        }
        //        th.Abort();
        //    }

        //}

        private void btnhuancun1_Click(object sender, EventArgs e)
        {
            if (IsOnLine)
            {
                Button btn = ((Button)sender);//获取当前单击按钮的所有实例
                string btnNmae = "pbMachine" + btn.Name.Substring(10);
                Control control = Controls.Find(btnNmae, true)[0];
                String machineno = System.Text.RegularExpressions.Regex.Replace(control.Text, @"[^0-9]+", "");
                Fm_UinonCache uc = new Fm_UinonCache(Text + control.Text, machineno);//二号 主皮带  四号机械手
                uc.Show();
            }
            else
            {
                MessageBox.Show("尚未连接服务器,请稍候再试！");
            }
        }
        #region
        ToolTip p = new ToolTip();
        private void Machine1_MouseEnter(object sender, EventArgs e)
        {
          
        }
        List<UnionTaskInfo> listUnion = new List<UnionTaskInfo>();
        /// <summary>
        ///  数据获取
        /// </summary>
        /// <param name="control"></param>
        /// <param name="machineIndex">当前机械手</param>
        //void bindDate(Control control,int machineIndex)
        //{
        //    try
        //    {
        //        Label btn = ((Label)control);
              

        //        int btnmachineno = Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(btn.Text, @"[^0-9]+", "")); //当前选定机械手号
        //        int groupno = GetGroupNo(btnmachineno);//获取组号
        //        listafter = UnionTaskInfoService.GetUnionTaskInfoAfter(mainbelt, groupno,SortNum, xyNum[machineIndex-1]);//机械手之前
        //        listbefore = UnionTaskInfoService.GetUnionTaskInfoBefore(mainbelt, groupno, SortNum, xyNum[machineIndex-1]);//机械手之后

        //        var union = listafter.ToList();//两个list合并 //.Union(listbefore)


        //        //for (int i = 0; i < union.Count; i++)
        //        //{
        //        //    ListViewItem lv = new ListViewItem();
        //        //    var mod = union[i];
        //        //    lv.SubItems[0].Text = btn.Text + "机械手";
        //        //    lv.SubItems.Add(mod.SortNum.ToString());
        //        //    lv.SubItems.Add(mod.MainBelt.ToString());
        //        //    lv.SubItems.Add(mod.CIGARETTDECODE.ToString());
        //        //    lv.SubItems.Add(mod.CIGARETTDENAME.ToString());
        //        //    lv.SubItems.Add("");
        //        //    listViewUnion.Items.Add(lv);
        //        //}


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("错误信息:" + ex.Message);
        //    }
        //}
        
        private void btnhuancun8_MouseEnter(object sender, EventArgs e)
        {
            Button btn = ((Button)sender);//获取当前单击按钮的所有实例
       
            p.ShowAlways = true;
            p.SetToolTip(btn, btn.Text + "缓存详细信息");
        }
  
        private void Fm_FollowTaskUnion_SizeChanged(object sender, EventArgs e)
        {
           //asc.controlAutoSize(this);
        }

        private void listViewUnion_SizeChanged(object sender, EventArgs e)
        {
           
        }
        #endregion

        private void listViewUnion_SelectedIndexChanged(object sender, EventArgs e)
        { 
            //if (listViewUnion.SelectedIndices.Count > 0)
            //{
            //    ClaerColor();
            //    string type = listViewUnion.SelectedItems[0].SubItems[0].Text;
             
            //    for (int i = 0; i < listViewUnion.Items.Count; i++)
            //    {
            //        ListViewItem item = listViewUnion.Items[i];
            //        for (int j = 0; j < item.SubItems.Count; j++)
            //        {
            //            if (type == item.SubItems[j].Text)
            //            { 
            //                item.ForeColor = Color.Red; 
            //            }
            //        }
            //    } 
            //}
             
        }
        /// <summary>
        /// 清除listview颜色
        /// </summary>
        void ClaerColor()
        {
            //for (int i = 0; i < listViewUnion.Items.Count; i++)
            //{
            //    ListViewItem item = listViewUnion.Items[i];
            //    for (int j = 0; j < item.SubItems.Count; j++)
            //    { 
            //        item.ForeColor = Color.Black ; 
            //    }
            //}
        }

      

        private void pbMachine1_MouseEnter(object sender, EventArgs e)
        {
           
            PictureBox picBox = ((PictureBox)sender);
            p.AutoPopDelay = 24000;
            p.ShowAlways = true;
            p.SetToolTip(picBox,System.Text.RegularExpressions.Regex.Replace( picBox.Name ,@"[^0-9]+","") +"号机械手皮带上烟的摆放和详细信息");
        }

        private void Fm_FollowTaskUnion_FormClosing(object sender, FormClosingEventArgs e)
        {
           // e.Cancel = true;
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is Form)
                {
                    //fname.TopMost = true;
                    if (fm_un != null)
                    {
                        fm_un.Close();
                    }
                    return;
                }
            } 
            this.Close();
            //return;
        }



    }
}
