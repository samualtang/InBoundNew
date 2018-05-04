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

        public Fm_UinonCache(string machineText,string machineno) 
        {
            InitializeComponent();
            mainbelt = Convert.ToDecimal( machineText.Substring(4,1)).CastTo(-1);//获取主皮带号
            groupno = GetGroupNo(Convert.ToInt32(machineno));//获取组号
            MessageBox.Show(machineno + "    " + groupno);
            this.listViewUnionCache.DoubleBufferedListView(true);
          //  this.dgvUnionCache.DoubleBufferedDataGirdView(true);//双缓存解决dgv闪烁问题
            Text = machineText;
            this.StartPosition = FormStartPosition.CenterScreen;
            lblCacheText.Text = machineText.Substring(6) + "机械手缓存区香烟排序";
        }

        private void Fm_UinonCache_Load(object sender, EventArgs e)
        {

            var data = FolloTaskService.getUnionCache(groupno, mainbelt, machineTaskExcuting, machinePokeNum).Select(a => new
            {
                cigrcode = a.CIGARETTDECODE,
                cigrname = a.CIGARETTDENAME,
                pokenum = a.POKENUM,
                SortNum = a.SortNum,
                mianbelt = a.MainBelt,
                machineseq = a.Machineseq
            }).ToList();

            for (int i = 0; i < data.Count; i++)
            {
                ListViewItem lv = new ListViewItem();
                var mod = data[i];
                lv.SubItems[0].Text =mod.cigrcode;
                lv.SubItems.Add(mod.cigrname);
                lv.SubItems.Add(mod.SortNum.ToString());
                lv.SubItems.Add(mod.pokenum.ToString());
                lv.SubItems.Add(mod.mianbelt.ToString());
                lv.SubItems.Add(mod.machineseq.ToString());
                listViewUnionCache.Items.Add(lv);
            }
             
        }
        public byte[] Modulus = new byte[255];
        /// <summary>
        /// 获取组号
        /// </summary>
        /// <param name="machineNo">机械手号</param>
        /// <returns></returns>
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

         
    }
}
