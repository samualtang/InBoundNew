using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.IO;
using InBound.Business;
using FormUI.TooL;
using InBound;
using InBound.Model;
using InBound.Pub;
using System.IO.Ports;

namespace FormUI
{
    public partial class w_user : Form
    {
    
        private bool iscancel = false;
     
        public w_user()
        {
            InitializeComponent();
            

        }

      
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            MessageBox.Show("ok！");
        }
       


        #region Form_Load
        private void w_user_Load(object sender, EventArgs e)
        {
           

           
            try
            {
                this.CenterToScreen();
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           

        }
        #endregion

        #region 取消
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                iscancel = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        SerialPort sp = new SerialPort();
        SerialPort sp1 = new SerialPort();
        public void OpenSerialPort1()
        {

            sp1.PortName = "COM4";
            if (!sp1.IsOpen)
            {
                try
                {
                    sp1.ReadBufferSize = 32;
                    sp1.BaudRate = 9600;
                    sp1.Open();
                    sp1.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived1);
                }
                catch
                {
                    //if (sp!=null && sp.IsOpen)
                    //{
                    //    sp.Close();
                    //}
                    //Thread.Sleep(5000);
                    //OpenSerialPort();
                }
            }

        }
        public void OpenSerialPort()
        {

            sp.PortName = "COM3";
            if (!sp.IsOpen)
            {
                try
                {
                    sp.ReadBufferSize = 32;
                    sp.BaudRate = 9600;
                    sp.Open();
                    sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
                }
                catch
                {
                    //if (sp!=null && sp.IsOpen)
                    //{
                    //    sp.Close();
                    //}
                    //Thread.Sleep(5000);
                    //OpenSerialPort();
                }
            }

        }
        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
             SerialPort sp = sender as SerialPort;
            String tempCode = sp.ReadExisting();
            MessageBox.Show("d1"+tempCode);
        }
        void sp_DataReceived1(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = sender as SerialPort;
            String tempCode = sp.ReadExisting();
            MessageBox.Show("d2"+tempCode);
        }
        #region btn_OK
        private void button1_Click(object sender, EventArgs e)
        {

            Mainbelt2.MainBeltClient client = new Mainbelt2.MainBeltClient();
            String s = client.GetMainBelt(1);
            //List<T_PRODUCE_POKE> list = TaskService.getList(1, 1);
            //String list = AtsCellInService.getCellNoCode("1111111");
            List<MainBeltInfo> list = new List<MainBeltInfo>();
            MainBeltInfo info = new MainBeltInfo();
            info.SortNum = 72345;
            info.Quantity = 20;
            info.GroupNO = 1;
            info.mainbelt = "1";
            list.Add(info);
            List<decimal> list1 = new List<decimal>();
            list1.Add(72370);
            list1.Add(72369);
            list1.Add(72362);
            list1.Add(72361);
            list1.Add(72360);
            list1.Add(72355);
            list1.Add(72352);
            list1.Add(72352);

            List<decimal> list2 = new List<decimal>();
            list2.Add(0);
            list2.Add(0);
            list2.Add(0);
            list2.Add(0);
            list2.Add(0);
            list2.Add(0);
            list2.Add(10);
            list2.Add(0);
            info.SortNumList = list1;
            info.QuantityList = list2;
            MainBeltInfoService.GetMainBeltInfo(list);
          //  String str = "1203".Substring(0,2);


            List<MainBeltInfo> infolist = new List<MainBeltInfo>();
            MainBeltInfo info1 = new MainBeltInfo();
            info1.SortNum = 71069;
           // info.GroupNO = 1;
            info1.Quantity = 1;
            info1.mainbelt = "2";
            infolist.Add(info);
            MainBeltInfoService.GetMainBeltInfo(infolist);
           // OpenSerialPort();
          //  OpenSerialPort1();
            if (info1.Quantity == 1)
            {
                return;
            }
            try
            {
              // throw(new Exception());

            
                string ls_pass;
              
                ls_pass = textBox2.Text;
               ls_pass= Security.MD5Encrypt(ls_pass).ToLower();
                  if (ls_pass == "")
                {
                    MessageBox.Show("请输入密码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (SysUserService.Login(txtUserName.Text, ls_pass))
                {
                    Constant.userName = txtUserName.Text.Trim();
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("用户名密码不对,请重新输入!");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 回车
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        public bool isCancel
        {
            get
            {
                return iscancel;
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textBox2.Focus();
        }

        private void w_user_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                button2_Click(sender, e);
            }
        }
        #endregion

        private void w_user_FormClosing(object sender, FormClosingEventArgs e)
        {
            //MessageBox.Show("错误");
        }

    }
}