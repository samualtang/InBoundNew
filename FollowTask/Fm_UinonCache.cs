using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound;
using InBound.Business;
using System.Security.Cryptography;
using InBound.Model;
 

namespace FollowTask
{
    public partial class Fm_UinonCache : Form
    {
        public Fm_UinonCache()
        {
            InitializeComponent(); 
        }
        decimal groupno, mainbelt;//组号和主皮带号

        decimal machineSate =1;//对应机械手状态 1有抓烟  0是无抓烟
        decimal machineTaskExcuting = 347;//正在执行任务号
        decimal machinePokeNum =1;//机械手正在执行的第几抓
        List<FollowTaskDeail> list = new List<FollowTaskDeail>();
        public Fm_UinonCache(string machineText,string machineno) 
        {
            InitializeComponent();
            mainbelt = Convert.ToDecimal(machineText.Substring(4,1)).CastTo(-1);//获取主皮带号
            groupno = GetGroupNo(Convert.ToInt32(machineno));//获取组号
         //   MessageBox.Show(machineno + "    " + groupno);
            this.listViewUnionCache.DoubleBufferedListView(true); 
            Text = machineText;
            this.StartPosition = FormStartPosition.CenterScreen;
            lblCacheText.Text = machineText.Substring(6) + "机械手缓存区香烟排序";
        }

        private void Fm_UinonCache_Load(object sender, EventArgs e)
        {

          
             
        }
        /// <summary>
        /// 获取组号
        /// </summary>
        /// <param name="machineNo">机械手号</param>
        /// <returns></returns>4   
        decimal GetGroupNo(int machineNo)
        { 
            if (machineNo >= 8)
            {
                groupno = machineNo % 8;// Convert.ToDecimal(Math.IEEERemainder(machineNo, 8));//取余获得组号
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
        List<FollowTaskDeail> list1;
        private void btnPokeTime_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtPokenum.Text) && !string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    machinePokeNum = Convert.ToDecimal(txtPokenum.Text);
                    machineTaskExcuting = Convert.ToDecimal(textBox1.Text);
                    btnPokeTime.Text = "第" + machinePokeNum + "抓";
                    listViewUnionCache.Items.Clear();
                    list1 = FolloTaskService.getUnionCache(groupno, mainbelt, machineTaskExcuting, machinePokeNum);
                    ListViewBind(list1);
                }
            }
            catch (Exception ex)
            { 
                MessageBox.Show("该任务号未找到订单!!" + "\r\n" + "错误信息:" + ex.Message + "\r\n");
            } 
        }
        /// <summary>
        /// LV绑定
        /// </summary>
        /// <param name="list"></param>
        private void ListViewBind(List<FollowTaskDeail> list)
        {
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    ListViewItem lv = new ListViewItem();
                    var mod = list[i];
                    lv.SubItems[0].Text = mod.CIGARETTDECODE;
                    lv.SubItems.Add(mod.CIGARETTDENAME);
                    lv.SubItems.Add(mod.SortNum.ToString());
                    lv.SubItems.Add(mod.POKENUM.ToString());
                    lv.SubItems.Add(mod.MainBelt.ToString());
                    lv.SubItems.Add(mod.Machineseq.ToString());
                    listViewUnionCache.Items.Add(lv);
                }
            }
        }

        private void btnPokeTime_MouseEnter(object sender, EventArgs e)
        {
          
         
           

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ListViewBind(list1);
        }

         
    }
}
