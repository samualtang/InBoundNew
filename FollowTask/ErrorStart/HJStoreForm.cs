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
    public partial class HJStoreForm : Form
    {
        public HJStoreForm()
        {
            InitializeComponent();
        }

        private void Btn_Start_Click(object sender, EventArgs e)
        {
            HandleDelegate task1 = GetListBoxItems1;
            task1.BeginInvoke(null, null);
            HandleDelegate task2 = GetListBoxItems2;
            task2.BeginInvoke(null, null);
           


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


       



        private void HJStoragetForm_Load(object sender, EventArgs e)
        {
            if (HJStorage.HJStorageList1 != null)
            {
                foreach (var item in HJStorage.HJStorageList2)
                {
                    updateListBox(item.ErrorMsg, listBox1);
                }
            }
            if (HJStorage.HJStorageList2 != null)
            {
                foreach (var item in HJStorage.HJStorageList2)
                {
                    updateListBox(item.ErrorMsg, listBox2);
                }
            }
           
        }

        /// <summary>
        /// 一组重力式货架
        /// </summary>
        private void GetListBoxItems1()
        {
            updateListBox(System.DateTime.Now.ToString() + "开始自检", listBox1);
            updateListBox("自检中,请等待......", listBox1);
            AllSystemStart ass1 = new AllSystemStart();

            try
            {
                List<ErrorInfo> li = ass1.ReadDBinfo_Storage(1); 
                foreach (var item in li)
                {
                    if (item.Value != "0")
                    {
                        updateListBox(item.ErrorMsg, listBox1);
                    }
                }
                updateListBox("本次自检结束......", listBox1);
            }
            catch (Exception)
            {
                updateListBox("未能成功连接PLC......", listBox2);
            }

        }


        /// <summary>
        /// 二组重力式货架
        /// </summary>
        private void GetListBoxItems2()
        {
            updateListBox(System.DateTime.Now.ToString() + "开始自检", listBox2);
            updateListBox("自检中,请等待......", listBox2);
            AllSystemStart ass2 = new AllSystemStart();
            try
            {
                List<ErrorInfo> li = ass2.ReadDBinfo_Storage(2);
                foreach (var item in li)
                {
                    if (item.Value != "0")
                    {
                        updateListBox(item.ErrorMsg, listBox2);
                    }
                }
                updateListBox("本次自检结束......", listBox2);
            }
            catch (Exception)
            {
                updateListBox("未能成功连接PLC......", listBox2);
            }
            
        }
    }
}
