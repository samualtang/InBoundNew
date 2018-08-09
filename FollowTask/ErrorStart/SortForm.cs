using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpcRcw.Da;
using SortingControlSys.Model;


namespace FollowTask.ErrorStart
{
    public partial class SortForm : Form
    {
        public SortForm()
        {
            ass.Connction();
            InitializeComponent();
            
        }
        private 

        internal const string SERVER_NAME = "OPC.SimaticNET";
        internal const int LOCALE_ID = 0x409;          
        List<string> FJConnectionGroup1, FJConnectionGroup2, FJConnectionGroup3, FJConnectionGroup4;
        IOPCServer pIOPCServer;
        Group FJPlcAdress;

        List<string> str1;
        List<string> str2;
        List<string> str3;
        List<string> str4;
        private void Btn_Start_Click(object sender, EventArgs e)
        {

           // updateListBox(System.DateTime.Now.ToString()+"开始自检", listBox1);
           // updateListBox("自检中,请等待......", listBox1);
           // updateListBox(System.DateTime.Now.ToString() + "开始自检", listBox2);
           // updateListBox("自检中,请等待......", listBox2);
           // updateListBox(System.DateTime.Now.ToString() + "开始自检", listBox3);
           // updateListBox("自检中,请等待......", listBox3);
            updateListBox(System.DateTime.Now.ToString() + "开始自检", listBox4);
            updateListBox("自检中,请等待......", listBox4);
            List<Abnormallists> li =  ass.GetFJOpcServerItem();
           // str1 = ass.ReadDBinfo(1);
          //  str2 = ass.ReadDBinfo(2);
           // str3 = ass.ReadDBinfo(3);
            str4 = ass.ReadDBinfo(4);
            
            int i = 0;
            /*
            foreach (var item in str1)
            {
                if (item!="0")
                {
                    updateListBox(li[i].AREANAME + li[i].ERRORMSG + li[i].DECICENO + ",值" + item, listBox1);
                }
                i++; 
            }
            i = 0;
            foreach (var item in str2)
            {
                if (item != "0")
                {
                    updateListBox(li[i].AREANAME + li[i].ERRORMSG + li[i].DECICENO + ",值" + item, listBox2);
                }
                i++;
            }
            i = 0;
            foreach (var item in str3)
            {
                if (item != "0")
                {
                    updateListBox(li[i].AREANAME + li[i].ERRORMSG + li[i].DECICENO + ",值" + item, listBox3);
                }
                i++;
            }
             
            i = 0;
             */
            foreach (var item in str4)
            {
                if (item != "0")
                {
                    updateListBox(li[i].AREANAME + li[i].ERRORMSG + li[i].DECICENO + ",值" + item, listBox4);
                }
                i++;
            }
            updateListBox("本次自检结束......", listBox1);
            //str1.Clear();
            //str2.Clear(); 
            //str3.Clear(); 
            str4.Clear();
             
        }
   
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
    }
}
