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
    public partial class InOutForm : Form
    {
        public InOutForm()
        {
            InitializeComponent();
        }

        private void Btn_Start_Click(object sender, EventArgs e)
        {
            HandleDelegate task1 = GetListBoxItems1;
            task1.BeginInvoke(null, null);
            HandleDelegate task2 = GetListBoxItems2;
            task2.BeginInvoke(null, null);
            HandleDelegate task3 = GetListBoxItems3;
            task3.BeginInvoke(null, null);
            HandleDelegate task4 = GetListBoxItems4;
            task4.BeginInvoke(null, null); 
        }


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

        private delegate void HandleDelegate();


        /// <summary>
        /// 获取读取的DB块集合与故障信息  电机
        /// </summary>
        private void GetListBoxItems1()
        {
            updateListBox(System.DateTime.Now.ToString() + "开始自检", listBox1);
            updateListBox("自检中,请等待......", listBox1);
            AllSystemStart ass1 = new AllSystemStart();
            List<ErrorInfo> li = ass1.ReadDBinfo_inout(1);
 
            foreach (var item in li)
            {
                if (item.Value!="0")
                {
                    updateListBox(item.DBAdress + "  " + item.ErrorMsg, listBox1);
                } 
            }
            updateListBox("本次自检结束......", listBox1); 
        }

        /// <summary>
        /// 获取读取的DB块集合与故障信息  输送线
        /// </summary>
        private void GetListBoxItems2()
        {
            updateListBox(System.DateTime.Now.ToString() + "开始自检", listBox2);
            updateListBox("自检中,请等待......", listBox2);
            AllSystemStart ass2 = new AllSystemStart();
            List<ErrorInfo> li = ass2.ReadDBinfo_inout(2);

            foreach (var item in li)
            {
                if (item.Value != "0")
                {
                    updateListBox(item.DBAdress + "  " + item.ErrorMsg, listBox2);
                }
            }
            updateListBox("本次自检结束......", listBox2);
        }


        /// <summary>
        /// 获取读取的DB块集合与故障信息  码分机
        /// </summary>
        private void GetListBoxItems3()
        {
            updateListBox(System.DateTime.Now.ToString() + "开始自检", listBox3);
            updateListBox("自检中,请等待......", listBox3);
            AllSystemStart ass3 = new AllSystemStart();
            List<ErrorInfo> li = ass3.ReadDBinfo_inout(3);

            foreach (var item in li)
            {
                if (item.Value != "0")
                {
                    updateListBox(item.DBAdress + "  " + item.ErrorMsg, listBox3);
                }
            }
            updateListBox("本次自检结束......", listBox3);
        }


        /// <summary>
        /// 获取读取的DB块集合与故障信息  入库队列
        /// </summary>
        private void GetListBoxItems4()
        {
            updateListBox(System.DateTime.Now.ToString() + "开始自检", listBox4);
            updateListBox("自检中,请等待......", listBox4);
            AllSystemStart ass4 = new AllSystemStart();
            List<ErrorInfo> li = ass4.ReadDBinfo_inout(4);

            foreach (var item in li)
            {
                if (item.Value != "0")
                {
                    updateListBox(item.DBAdress + "  " + item.ErrorMsg, listBox4);
                }
            }
            updateListBox("本次自检结束......", listBox4);
        }

        private void InOutForm_Load(object sender, EventArgs e)
        {
            if (InOutData.InOutDataList1 != null)
            {
                foreach (var item in InOutData.InOutDataList1)
                {
                    updateListBox(item.ErrorMsg, listBox1);
                }
            }
            if (InOutData.InOutDataList2 != null)
            {
                foreach (var item in InOutData.InOutDataList2)
                {
                    updateListBox(item.ErrorMsg, listBox2);
                }
            }
            if (InOutData.InOutDataList3 != null)
            {
                foreach (var item in InOutData.InOutDataList3)
                {
                    updateListBox(item.ErrorMsg, listBox3);
                }
            }
            if (InOutData.InOutDataList4 != null)
            {
                foreach (var item in InOutData.InOutDataList4)
                {
                    updateListBox(item.ErrorMsg, listBox4);
                }

            } 
        }
    }
}
