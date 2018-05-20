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
using InBound.Business;
using InBound.Model;

namespace FollowTask
{
    public partial class fm_Machine : Form
    {
        
        public fm_Machine()
        {
            InitializeComponent();
        }

        public WriteLog writeLog = WriteLog.GetLog();
        AutoSizeFormClass asc = new AutoSizeFormClass();//自适应窗体
        public fm_Machine(string text)//窗体初始化
        {
            InitializeComponent();
            asc.controllInitializeSize(this);
            this.listViewMchineBelt.DoubleBufferedListView(true); //双缓存 减少闪烁
            listViewMchineBelt.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.Text = text;
          
            updateListBox(text+"应用程序启动");
            writeLog.Write(text+"应用程序启动");
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


        private void fm_Machine_Load(object sender, EventArgs e)
        {  
           
            lblFormText.Text = this.Text +"烟柜";
            BtnText(this.Text); 
        }
   


        private void Machine1_Click(object sender, EventArgs e)//机械手单击事件
        {
            Button name = ((Button)sender);//获取当前单击按钮的所有实例
            updateListBox("查看" + Text.Substring(4) +"的"+ name.Text + "机械手");
            //writeLog.Write("查看" + Text.Substring(4) +"的"+ name.Text + "机械手");
            Fm_FollowTaskMachineDetail ftmd = new Fm_FollowTaskMachineDetail(Text+ name.Text);
            ftmd.Show();
             
        } 
        /// <summary>
        /// 机械手根据组变更
        /// </summary>
        /// <param name="groupText">组名 </param>
        public void BtnText(string groupText)
        {
            int group = 0;
            if (groupText.Contains("1") || groupText.Contains("3") || groupText.Contains("5") || groupText.Contains("7"))
            {
                group = 1;//1-11
            }
            else
            {
                group = 2;//12-22
            }
            if (group == 1)
            {
                int j = 1;
                for (int i = 1; i < 12; i++)
                {
                    string btnNmae = "Machine" + j;
                    Control control = Controls.Find(btnNmae, true)[0];
                    control.Text = j + "号";
                    j++;
                }
            }
            else if (group == 2)
            {
                int j = 1;
                for (int i = 12; i < 23; i++)
                {
                    string btnNmae = "Machine" + j;
                    Control control = Controls.Find(btnNmae, true)[0];
                    control.Text = i + "号";
                    j++;
                }
            }

        }
        
        private void Machine1_MouseEnter(object sender, EventArgs e)
        {
            Button btn = ((Button)sender);
            ToolTip p = new ToolTip();
            p.ShowAlways = true;
            p.SetToolTip(btn,btn.Text+ "机械详细信息");
        }

        private void btnBelt_Click(object sender, EventArgs e)//皮带单击事件
        {
            Button name = ((Button)sender);//获取当前单击按钮的所有实例
            updateListBox("查看" + Text.Substring(4 ) + "的皮带");
            ListViewBind(FolloTaskService.GetMachineBeltInfo(Convert.ToDecimal(Text.Substring(5, 1))));
        }
        /// <summary>
        /// lv绑定
        /// </summary>
        void ListViewBind(List<FollowTaskDeail> list)
        {
            if (list != null)
            {

                for (int i = 0; i < list.Count; i++)
                {
                    ListViewItem lv = new ListViewItem();
                    var mod = list[i];
                    lv.SubItems[0].Text = mod.Billcode;
                    lv.SubItems.Add(mod.MainBelt.ToString());
                    lv.SubItems.Add(mod.CIGARETTDENAME.ToString());
                    lv.SubItems.Add(mod.CIGARETTDECODE.ToString());
                    lv.SubItems.Add(mod.SortNum.ToString());
                    lv.SubItems.Add(mod.UnionTasknum.ToString());
                    lv.SubItems.Add(mod.Machineseq.ToString());
                     listViewMchineBelt  .Items.Add(lv);
                } 
            }

        }
        private void fm_Machine_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }
        int times;
        private void timer1_Tick(object sender, EventArgs e)
        {
            listViewMchineBelt.Items.Clear();
            ListViewBind(FolloTaskService.GetMachineBeltInfo(Convert.ToDecimal(Text.Substring(5, 1))));
            timer1.Interval = (times * 1000);
        }


       
     

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            times =Convert.ToInt32( txtTimes.Text);
            timer1.Start();
        }

        private void listViewMchineBelt_SizeChanged(object sender, EventArgs e)
        {

            int _Count = listViewMchineBelt.Columns.Count;
            int _Width = listViewMchineBelt.Width;
            foreach (ColumnHeader ch in listViewMchineBelt.Columns)
            {
                ch.Width = _Width / _Count -1;
            }
        }
    }
}
