using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FollowTask.Modle;

using FollowTask.DataModle;
using InBound;
using System.Threading;
namespace FollowTask.ErrorStart
{
    public partial class ErrorStart_Main : Form
    {
        public ErrorStart_Main()
        {
            InitializeComponent();
        }

        /*
       internal const string SERVER_NAME = "OPC.SimaticNET";
       internal const int LOCALE_ID = 0x409;          
       List<string> FJConnectionGroup1, FJConnectionGroup2, FJConnectionGroup3, FJConnectionGroup4;
       IOPCServer pIOPCServer;     */
        SortForm sort;

        InOutForm Inout;

        public void OnDataChange(int group, int[] clientId, object[] values)
        {
            if (group == 1)//一号主皮带八个机械手
            {

            }
            if (group == 2)//二号主皮带八个机械手
            {

            }
            if (group == 3)//三号主皮带八个机械手
            {

            }
            if (group == 4)//四号主皮带八个机械手
            {

            }
        }
       

        Group FJPlcAdress;
        Thread td;
        private void Btn_start_Click(object sender, EventArgs e)
        {  
            b2.BackColor = Color.Yellow;
            b3.BackColor = Color.Yellow;
            b4.BackColor = Color.Yellow;
            b5.BackColor = Color.Yellow;
            b6.BackColor = Color.Yellow;
            b7.BackColor = Color.Yellow;
            b8.BackColor = Color.Yellow;

            //开始自检
            HandleDelegate task = Start_Click;
            task.BeginInvoke(null, Btn_start);

            listBox1.Items.Clear();

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
                list.Items.Insert(0, time + " " + info);
            }
        }





        AllSystemStart ass = new AllSystemStart();
        private delegate void HandleDelegate();
        //HandleDelegate di ;
        //private void Btn()
        //{
        //    Btn_start.Enabled = true;
        //}
#region
        /// <summary>
        /// 开始分拣1组
        /// </summary>
        private void Start_FJ1()
        {
            b1_f1.BackColor = Color.Yellow; 
            List<string> sortlist =new List<string>();
            AllSystemStart ass1 = new AllSystemStart();
            sortlist = ass1.ReadDBinfo(1);
            List<ErrorDates> Errorlist = new List<ErrorDates>();
            int falg = 1;
            foreach (var item in sortlist)
            {
                if (item == "-1")
                {
                    ErrorDates ed = new ErrorDates();
                    ed.FJIndex = falg;
                    using (Entities et = new Entities())
                    {
                        var FJErrors = et.T_WMS_ABNORMALLIST.Where(x => x.AREAPLC == "S7:[FJConnectionGroup-]" && x.PLCINDEX == falg).Select(x => x.ERRORMSG).FirstOrDefault();
                        ed.FJError = FJErrors;
                        ed.ErrorTime = DateTime.Now.ToShortTimeString();
                    }
                    ed.FJValue = item;
                    Errorlist.Add(ed);
                }
                falg++;
            } 
            SortData.FJList1 = Errorlist;
            if (Errorlist.Count > 0)
            {
                b1_f1.BackColor = Color.Red;
                updateListBox("预分拣第一组存在故障", listBox1);
            }
            else
            {
                b1_f1.BackColor = Color.Green;
            }
          //  GetState();
        }
        /// <summary>
        /// 开始分拣2组
        /// </summary>
        private void Start_FJ2()
        {
            b1_f2.BackColor = Color.Yellow;

            List<string> sortlist = new List<string>();
            AllSystemStart ass2 = new AllSystemStart();
            sortlist = ass2.ReadDBinfo(2);
            List<ErrorDates> Errorlist = new List<ErrorDates>();
            int falg = 1;
            foreach (var item in sortlist)
            {
                if (item == "-1")
                {
                    ErrorDates ed = new ErrorDates();
                    ed.FJIndex = falg;
                    using (Entities et = new Entities())
                    {
                        var FJErrors = et.T_WMS_ABNORMALLIST.Where(x => x.AREAPLC == "S7:[FJConnectionGroup-]" && x.PLCINDEX == falg).Select(x => x.ERRORMSG).FirstOrDefault();
                        ed.FJError = FJErrors;
                        ed.ErrorTime = DateTime.Now.ToShortTimeString();
                    }
                    ed.FJValue = item;
                    Errorlist.Add(ed);
                }
                falg++;
            }
            SortData.FJList2 = Errorlist;
            if (Errorlist.Count > 0)
            {
                b1_f2.BackColor = Color.Red;
                updateListBox("预分拣第二组存在故障", listBox1);
            }
            else
            {
                b1_f2.BackColor = Color.Green;
            }
          //  GetState();
        }
        /// <summary>
        /// 开始分拣3组
        /// </summary>
        private void Start_FJ3()
        {
            b1_f3.BackColor = Color.Yellow;

            List<string> sortlist = new List<string>();
            AllSystemStart ass3 = new AllSystemStart();
            sortlist = ass3.ReadDBinfo(3);
            List<ErrorDates> Errorlist = new List<ErrorDates>();
            int falg = 1;
            foreach (var item in sortlist)
            {
                if (item == "-1")
                {
                    ErrorDates ed = new ErrorDates();
                    ed.FJIndex = falg;
                    using (Entities et = new Entities())
                    {
                        var FJErrors = et.T_WMS_ABNORMALLIST.Where(x => x.AREAPLC == "S7:[FJConnectionGroup-]" && x.PLCINDEX == falg).Select(x => x.ERRORMSG).FirstOrDefault();
                        ed.FJError = FJErrors;
                        ed.ErrorTime = DateTime.Now.ToShortTimeString();
                    }
                    ed.FJValue = item;
                    Errorlist.Add(ed);
                }
                falg++;
            }
            SortData.FJList3 = Errorlist;
            if (Errorlist.Count > 0)
            {
                b1_f3.BackColor = Color.Red;
                updateListBox("预分拣第三组存在故障", listBox1);
            }
            else
            {
                b1_f3.BackColor = Color.Green;
            }
            //GetState();
        }
        /// <summary>
        /// 开始分拣4组
        /// </summary>
        private void Start_FJ4()
        {
            b1_f4.BackColor = Color.Yellow;

            List<string> sortlist = new List<string>();
            AllSystemStart ass4 = new AllSystemStart();
            sortlist = ass4.ReadDBinfo(4);
            List<ErrorDates> Errorlist = new List<ErrorDates>();
            int falg = 1;
            foreach (var item in sortlist)
            {
                if (item == "-1")
                {
                    ErrorDates ed = new ErrorDates();
                    ed.FJIndex = falg;
                    using (Entities et = new Entities())
                    {
                        var FJErrors = et.T_WMS_ABNORMALLIST.Where(x => x.AREAPLC == "S7:[FJConnectionGroup-]" && x.PLCINDEX == falg).Select(x => x.ERRORMSG).FirstOrDefault();
                        ed.FJError = FJErrors;
                        ed.ErrorTime = DateTime.Now.ToShortTimeString();
                    }
                    ed.FJValue = item;
                    Errorlist.Add(ed);
                }
                falg++;
            }
            SortData.FJList4 = Errorlist;
            if (Errorlist.Count > 0)
            {
                b1_f4.BackColor = Color.Red;
                updateListBox("预分拣第四组存在故障", listBox1);
            }
            else
            {
                b1_f4.BackColor = Color.Green;
            }
           // GetState();
        }
#endregion
        private void Start_Click()
        {
            try
            {
                HandleDelegate task1 = Start_FJ1;
                task1.BeginInvoke(null, null);

                HandleDelegate task2 = Start_FJ2;
                task2.BeginInvoke(null, null);

                HandleDelegate task3 = Start_FJ3;
                task3.BeginInvoke(null, null);

                HandleDelegate task4 = Start_FJ4;
                task4.BeginInvoke(null, null);
            }
            catch (Exception)
            {
                MessageBox.Show("连接PLC失败！");
            }
          
        }

        private void Start_Click1()
        {
            //循环读取4个分拣plc 
            #region
            for (int i = 1; i < 5; i++)
            {
                List<string> sortlist = ass.ReadDBinfo(i);
                List<ErrorDates> Errorlist = new List<ErrorDates>();
                int falg = 1;
                foreach (var item in sortlist)
                {
                    if (item == "-1")
                    {
                        ErrorDates ed = new ErrorDates();
                        ed.FJIndex = falg;
                        using (Entities et = new Entities())
                        {
                            var FJErrors = et.T_WMS_ABNORMALLIST.Where(x => x.AREAPLC == "S7:[FJConnectionGroup-]" && x.PLCINDEX == falg).Select(x => x.ERRORMSG).FirstOrDefault();
                            ed.FJError = FJErrors;
                            ed.ErrorTime = DateTime.Now.ToShortTimeString();
                        }
                        ed.FJValue = item;
                        Errorlist.Add(ed);
                    }
                    falg++;
                }
                switch (i)
                {
                    case 1:
                        SortData.FJList1 = Errorlist;
                        if (Errorlist.Count > 0)
                        {
                            b1_f1.BackColor = Color.Red;
                            updateListBox("预分拣第一组存在故障", listBox1);
                        }
                        else
                        {
                            b1_f1.BackColor = Color.Green;
                        }
                        break;
                    case 2:
                        SortData.FJList2 = Errorlist;
                        if (Errorlist.Count > 0)
                        {
                            b1_f2.BackColor = Color.Red;
                            updateListBox("预分拣第二组存在故障", listBox1);
                        }
                        else
                        {
                            b1_f2.BackColor = Color.Green;
                        }
                        break;
                    case 3:
                        SortData.FJList3 = Errorlist;
                        if (Errorlist.Count > 0)
                        {
                            b1_f3.BackColor = Color.Red;
                            updateListBox("预分拣第三组存在故障", listBox1);
                        }
                        else
                        {
                            b1_f3.BackColor = Color.Green;
                        }
                        break;
                    case 4:
                        SortData.FJList4 = Errorlist;
                        if (Errorlist.Count > 0)
                        {
                            b1_f4.BackColor = Color.Red;
                            updateListBox("预分拣第四组存在故障", listBox1);
                        }
                        else
                        {
                            b1_f4.BackColor = Color.Green;
                        }
                        break;
                }

            }
            //GetState();
            #endregion

        }

        private void btn_sort_Click(object sender, EventArgs e)
        { 
            if (Controls.Contains(sort))
            {
                sort.WindowState = FormWindowState.Maximized;
                sort.Show();
            }
            else
            {
                sort = new SortForm();
                sort.TopLevel = false;
                sort.Parent = splitContainer1.Panel2;
                //sort.FormBorderStyle = FormBorderStyle.None;
                sort.WindowState = FormWindowState.Maximized;
                sort.Show();
            }



        }
        
        private void ErrorStart_Main_Load(object sender, EventArgs e)
        {
            
        }

        private void GetState()
        { 
            if (AllPlcState.FJState1 == 0)
            {
                updateListBox("预分拣第一组plc未连接", listBox1);
            }
            if (AllPlcState.FJState2 == 0)
            {
                updateListBox("预分拣第二组plc未连接", listBox1);
            }
            if (AllPlcState.FJState3 == 0)
            {
                updateListBox("预分拣第三组plc未连接", listBox1);
            }
            if (AllPlcState.FJState4 == 0)
            {
                updateListBox("预分拣第四组plc未连接", listBox1);
            }  
        }

        ErrorStartMain_From fm;
        private void button1_Click(object sender, EventArgs e)
        {
            if (!ReadFormDictionary(fm))
            {
                fm = new ErrorStartMain_From();
                WriteFormDictionary(fm);
            } 
            

        }
         
        /// <summary>
        /// 字典加入窗体
        /// </summary>
        /// <param name="fm"></param>
        private void WriteFormDictionary(Form fm)
        {
            DicForm.DicFormList.Add(fm, true);
            fm.TopLevel = false;
            //指定父容器
            fm.Parent = splitContainer1.Panel2;
            fm.WindowState = FormWindowState.Maximized;
            fm.Show();
        }
        /// <summary>
        /// 字典读取窗体
        /// </summary>
        /// <param name="fm">窗体对象</param>
        /// <returns>是否存在窗体</returns>
        private bool ReadFormDictionary(Form fm)
        {
            if (DicForm.DicFormList.Count() > 0)
            {
                //如果存在窗体
                if (DicForm.DicFormList.ContainsKey(fm))
                {
                    //如果为开启状态
                    if (DicForm.DicFormList.ContainsValue(true))
                    {
                        fm.Activate();
                    }
                    else
                    {
                        fm = new ErrorStartMain_From();
                        fm.Show();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            foreach (var item in DicForm.DicFormList)
            {
                MessageBox.Show(item.Key + "" + item.Value);                
            }
        }

        private void btn_inout_Click(object sender, EventArgs e)
        {
            if (Controls.Contains(Inout))
            {
                Inout.WindowState = FormWindowState.Maximized;
                Inout.Show();
            }
            else
            {
                Inout = new InOutForm();
                Inout.TopLevel = false;
                Inout.Parent = splitContainer1.Panel2; 
                Inout.WindowState = FormWindowState.Maximized;
                Inout.Show();
            }

        }



    }
}