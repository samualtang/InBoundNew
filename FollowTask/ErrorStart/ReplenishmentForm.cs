using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FollowTask.DataModle;

namespace FollowTask.ErrorStart
{
    public partial class ReplenishmentForm : Form
    {
        public ReplenishmentForm()
        {
            InitializeComponent();
        }

        private void Btn_Start_Click(object sender, EventArgs e)
        {
            HandleDelegate task1 = GetListBoxItems1;
            task1.BeginInvoke(null, null); 
        }
        private delegate void HandleDelegate();
        private delegate void HandleDelegateError(string strshow, ListBox list);
        /// <summary>
        /// list上写异常
        /// </summary>
        /// <param name="info"></param>
        /// <param name="list"></param>
        public void updateListBox(string info, ListBox list)
        {
            String time = DateTime.Now.ToLongTimeString();
            if (list.InvokeRequired)
            {
                list.Invoke(new HandleDelegateError(updateListBox), info, list);
            }
            else
            {
                list.Items.Insert(0, time + "    " + info);
            }
        }


        /// <summary>
        /// 获取读取的DB块集合与故障信息  大拔杆
        /// </summary>
        private void GetListBoxItems1()
        {
            updateListBox(System.DateTime.Now.ToString() + "开始自检", listBox1);
            updateListBox("自检中,请等待......", listBox1);
            AllSystemStart ass1 = new AllSystemStart();
            List<ErrorInfo> li = ass1.ReadDBinfo_Replenishment(1);

            foreach (var item in li)
            {
                if (item.Value != "0")
                {
                    updateListBox(item.DBAdress + "  " + item.ErrorMsg, listBox1);
                }
            }
            updateListBox("本次自检结束......", listBox1);
        }
       
    }
}
