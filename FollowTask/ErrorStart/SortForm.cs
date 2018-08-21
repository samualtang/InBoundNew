using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpcRcw.Da;
using FollowTask.Modle;
using FollowTask.DataModle;


namespace FollowTask.ErrorStart
{
    public partial class SortForm : Form
    {
        public SortForm()
        {
            ass.Connction();
            InitializeComponent();

        }

        
        internal const string SERVER_NAME = "OPC.SimaticNET";
        internal const int LOCALE_ID = 0x409;          
        List<string> FJConnectionGroup1, FJConnectionGroup2, FJConnectionGroup3, FJConnectionGroup4;
        IOPCServer pIOPCServer;     
        Group FJPlcAdress;

        private void Btn_Start_Click(object sender, EventArgs e)
        {
            //HandleDelegate task = Start_Click;
            //task.BeginInvoke(null, null);   

            HandleDelegate task1 = GetListBoxItems1;
            task1.BeginInvoke(null, null);
            HandleDelegate task2 = GetListBoxItems2;
            task2.BeginInvoke(null, null);
            HandleDelegate task3 = GetListBoxItems3;
            task3.BeginInvoke(null, null);
            HandleDelegate task4 = GetListBoxItems4;
            task4.BeginInvoke(null, null); 
        }
        private delegate void HandleDelegate();
        private void Start_Click()
        { 
            GetListBoxItems1();
            GetListBoxItems2();
            GetListBoxItems3();
            GetListBoxItems4();
        }


        List<string> str1;
        List<string> str2;
        List<string> str3;
        List<string> str4;
        int j = 2;

        
        #region
        private void GetListBoxItems1()
        {
            updateListBox(System.DateTime.Now.ToString() + "开始自检", listBox1);
            updateListBox("自检中,请等待......", listBox1);
            AllSystemStart ass1 = new AllSystemStart();
            List<Abnormallists> li = ass1.GetFJOpcServerItem();
            str1 = ass1.ReadDBinfo(1);

            int i = 0;

            foreach (var item in str1)
            {
                if (item != "0")
                {
                    updateListBox( li[i].ERRORMSG , listBox1);
                }
                i++;
            }
            updateListBox("本次自检结束......", listBox1);
            str1.Clear(); 
        }
        private void GetListBoxItems2()
        {
            updateListBox(System.DateTime.Now.ToString() + "开始自检", listBox2);
            updateListBox("自检中,请等待......", listBox2);
            AllSystemStart ass2 = new AllSystemStart();
            List<Abnormallists> li = ass2.GetFJOpcServerItem();
            str2 = ass2.ReadDBinfo(2);

            int i = 0;

            foreach (var item in str2)
            {
                if (item != "0")
                {
                    updateListBox(li[i].ERRORMSG,listBox2);
                }
                i++;
            }
            updateListBox("本次自检结束......", listBox2);
            str2.Clear();
        }
        private void GetListBoxItems3()
        {
            updateListBox(System.DateTime.Now.ToString() + "开始自检", listBox3);
            updateListBox("自检中,请等待......", listBox3);
            AllSystemStart ass3 = new AllSystemStart();
            List<Abnormallists> li = ass3.GetFJOpcServerItem();
            str3 = ass3.ReadDBinfo(3);

            int i = 0;

            foreach (var item in str3)
            {
                if (item != "0")
                {
                    updateListBox(  li[i].ERRORMSG , listBox3);
                }
                i++;
            }
            updateListBox("本次自检结束......", listBox3);
            str3.Clear();
        }
        private void GetListBoxItems4()
        {
            updateListBox(System.DateTime.Now.ToString() + "开始自检", listBox4);
            updateListBox("自检中,请等待......", listBox4);
            AllSystemStart ass4 = new AllSystemStart();
            List<Abnormallists> li = ass4.GetFJOpcServerItem();
            str4 = ass4.ReadDBinfo(4);

            int i = 0;

            foreach (var item in str4)
            {
                if (item != "0")
                {
                    updateListBox(  li[i].ERRORMSG , listBox4);
                }
                i++;
            }
            updateListBox("本次自检结束......", listBox4);
            str4.Clear();
        }
#endregion

        AllSystemStart ass = new AllSystemStart();



        public void GetSoringBeltInfo(List<Group> list, int i)
        {
            FJPlcAdress.addItem(ass.GetFJPlcAdress(i));
        }

        private void GetDate()
        {
            foreach (var item in ass.GetFJOpcServerItem())
            {
                updateListBox(item.AREANAME + "第一组：" + item.ERRORMSG, listBox1);
            }
            foreach (var item in ass.GetFJOpcServerItem())
            {
                updateListBox(item.AREANAME + "第二组：" + item.ERRORMSG, listBox1);
            }
            foreach (var item in ass.GetFJOpcServerItem())
            {
                updateListBox(item.AREANAME + "第三组：" + item.ERRORMSG, listBox1);
            }
            foreach (var item in ass.GetFJOpcServerItem())
            {
                updateListBox(item.AREANAME + "第四组：" + item.ERRORMSG, listBox1);
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (var item in ass.GetFJPlcAdress(4))
            {
                updateListBox(item, listBox1);
            }
        }

        private void SortForm_Load(object sender, EventArgs e)
        {
            if (SortData.FJList1 != null )
            { 
                foreach (var item in SortData.FJList1)
                {
                    updateListBox(item.ErrorMsg, listBox1);
                }
            }
            if (SortData.FJList2 != null )
	        {
		        foreach (var item in SortData.FJList2)
                    {
                        updateListBox(item.ErrorMsg, listBox2);
                    }
	        }
            if (SortData.FJList3 != null)
	        { 
                foreach (var item in SortData.FJList3)
                {
                    updateListBox(item.ErrorMsg, listBox3);
                }
	        }
            if (SortData.FJList4 != null)
	        {
		        foreach (var item in SortData.FJList4)
                    {
                        updateListBox(item.ErrorMsg, listBox4);
                    }

	        } 
            
        }

    }
}
