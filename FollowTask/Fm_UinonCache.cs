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
using FollowTask.Modle;
 

namespace FollowTask
{
    public partial class Fm_UinonCache : Form
    {
        public Fm_UinonCache()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.listViewUnionCache.DoubleBufferedListView(true); 
        }
        decimal groupno ;//组号   
        int MainBelt;//主皮带
        int MachineNo;//机械手号 
          /// <summary>
        /// 存放当前任务号 和当前吸烟数量
        /// </summary>
        decimal[] sortnumAndXYnum = new decimal[2]; 
        public Fm_UinonCache(string machineText,string machineno) 
        {
            InitializeComponent();
            MainBelt =  Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(machineText, @"[^0-9]+", ""));//获取主皮带号 
            lblCacheText.Text = machineText.Substring(6) + "机械手缓存区香烟排序";
        }
        /// <summary>
        /// 存放opc组
        /// </summary>
        List<Group> listUnionMachine = new List<Group>();

        public void GetUnionNowMachineTask(int machineno,List<Group> list)
        {
            Text = machineno + "号机械手";
            MachineNo = machineno;
            groupno = GetGroupNo(machineno);//获取组号
            MainBelt = (int)Math.Ceiling(((double)machineno / 8));//获取主皮带 
            lblCacheText.Text = machineno + "号机械手缓存区香烟排序";
            listUnionMachine = list;
        }
     
        /// <summary>
        /// 读取DB当前任务号和当前抓数
        /// </summary>
        /// <param name="mainbelt">主皮带号</param>
        /// <param name="machineno">当前机械手吸烟数量</param>
        /// <returns>一个数组[0]是当前任务号 [1]是当前吸烟数量</returns>
       void ReadDbInFo(int mainbelt, int machineno)
        { 
            if (machineno == 1)
            {
                machineno = 0;
            } 
            sortnumAndXYnum[0] = listUnionMachine[5].ReadD((machineno * 2)).CastTo<int>(-1);//当前任务号
            sortnumAndXYnum[1] = listUnionMachine[5].ReadD(((machineno * 2) + 1)).CastTo<int>(-1);//当前吸烟数量 
            
        }
       #region 暂时无用
       /// <summary>
       /// 任务号
       /// </summary>
       decimal[] SortNumG = new decimal[8];
       /// <summary>
       /// 吸烟数量
       /// </summary>
       decimal[] xyNum = new decimal[8];
       /// <summary>
        /// 读取指定皮带DB块
        /// </summary>
        /// <param name="group">OPC组名</param>
        void ReadDBInfo(Group group, int mainbelt)
        {
            int index = -1;
            if (mainbelt == 1)
            {
                index = 0;
            }
            if (mainbelt == 2)
            {
                index = 16;
            }
            if (mainbelt == 3)
            {
                index = 32;
            }
            if (mainbelt == 4)
            {
                index = 48;
            }
            //SortNum = group.ReadD(0).CastTo<int>(-1);//当前任务号
            if (index != -1 && group.ReadD(0).CastTo<int>(-1) != -1)
            {
                SortNumG[0] = group.ReadD(index).CastTo<int>(-1);
                SortNumG[1] = group.ReadD(index + 2).CastTo<int>(-1);
                SortNumG[2] = group.ReadD(index + 4).CastTo<int>(-1);
                SortNumG[3] = group.ReadD(index + 6).CastTo<int>(-1);
                SortNumG[4] = group.ReadD(index + 8).CastTo<int>(-1);
                SortNumG[5] = group.ReadD(index + 10).CastTo<int>(-1);
                SortNumG[6] = group.ReadD(index + 12).CastTo<int>(-1);
                SortNumG[7] = group.ReadD(index + 14).CastTo<int>(-1);

                //八个机械手吸烟数量
                xyNum[0] = group.ReadD(index + 1).CastTo<int>(-1);
                xyNum[1] = group.ReadD(index + 3).CastTo<int>(-1);
                xyNum[2] = group.ReadD(index + 5).CastTo<int>(-1);
                xyNum[3] = group.ReadD(index + 7).CastTo<int>(-1);
                xyNum[4] = group.ReadD(index + 9).CastTo<int>(-1);
                xyNum[5] = group.ReadD(index + 11).CastTo<int>(-1);
                xyNum[6] = group.ReadD(index + 13).CastTo<int>(-1);
                xyNum[7] = group.ReadD(index + 15).CastTo<int>(-1);
            }
        }
       #endregion
        private void Fm_UinonCache_Load(object sender, EventArgs e)
        {
            ReadDbInFo(MainBelt, MachineNo);
            if (sortnumAndXYnum.Count() != 0)
            {
                //ReadDBInfo(listUnionMachine[5], MainBelt);
                textBox1.Text = sortnumAndXYnum[0] + "";
                txtPokenum.Text = sortnumAndXYnum[1] + "";
                list1 = FolloTaskService.getUnionCache(groupno, MainBelt, sortnumAndXYnum[0], sortnumAndXYnum[1]);//获取数据
                btnRefresh_Click(null, null);
            }
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
        List<FollowTaskDeail> list1 = new List<FollowTaskDeail>();
        private void btnPokeTime_Click(object sender, EventArgs e)
        {
           
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
            return machineNo - 1;
        }
        /// <summary>
        /// LV绑定
        /// </summary>
        /// <param name="list"></param>
        private void ListViewBind(List<FollowTaskDeail> list)
        { 
            try
            {
                if (list != null)
                {
                    int Nums = 1;//序号
                    foreach (var row in list)
                    {
                        int index = this.dgvUnionCache.Rows.Add();

                        this.dgvUnionCache.Rows[index].Cells[0].Value = Nums++; //序号
                        this.dgvUnionCache.Rows[index].Cells[1].Value =  row.MainBelt;//主皮带
                        this.dgvUnionCache.Rows[index].Cells[2].Value = row.SortNum;//任务号
                        this.dgvUnionCache.Rows[index].Cells[3].Value = row.GroupNO;//组号  
                        this.dgvUnionCache.Rows[index].Cells[4].Value = row.POKENUM;//数量
                        this.dgvUnionCache.Rows[index].Cells[5].Value =row.CIGARETTDECODE;//卷烟编码
                        this.dgvUnionCache.Rows[index].Cells[6].Value = row.CIGARETTDENAME;//卷烟名称
                    }

                }
                //listViewUnionCache.Items.Clear();

                //ListViewBind(list1);
                //if (list != null)
                //{
                //    for (int i = 0; i < list.Count; i++)
                //    {
                //        ListViewItem lv = new ListViewItem();
                //        var mod = list[i];
                //        lv.SubItems[0].Text = mod.CIGARETTDECODE;
                //        lv.SubItems.Add(mod.CIGARETTDENAME);
                //        lv.SubItems.Add(mod.SortNum.ToString());
                //        lv.SubItems.Add(mod.POKENUM.ToString());
                //        lv.SubItems.Add(mod.MainBelt.ToString());
                //        lv.SubItems.Add(mod.Machineseq.ToString());
                //        listViewUnionCache.Items.Add(lv);
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("该任务号未找到订单!!" + "\r\n" + "错误信息:" + ex.Message + "\r\n");
            } 
        }
        
        private void btnPokeTime_MouseEnter(object sender, EventArgs e)
        {
         
        } 
     
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ListViewBind(list1);
        }

        private void listViewUnionCache_SizeChanged(object sender, EventArgs e)
        {

            int _Count = listViewUnionCache.Columns.Count;
            int _Width = listViewUnionCache.Width;
            foreach (ColumnHeader ch in listViewUnionCache.Columns)
            {
                ch.Width = _Width / _Count - 1;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

           
                dgVprint1.MainTitle = MachineNo+ "号机械手缓存表";
                //dgVprint1.SubTitle = "这是子标题，当然也可以不设的";
                // dgVprint1.PaperLandscape = true;//用横向打印，默认是纵向

                dgVprint1.Print(dgvUnionCache);
            
        }

      

         
    }
}
