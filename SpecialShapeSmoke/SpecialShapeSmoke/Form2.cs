using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace SpecialShapeSmoke
{
    public partial class Form2 : Form
    { 
        string str;
        public Form2( )
        {
            InitializeComponent();
            OpenSerialPort();
            OpenSerialPort1();
           
 
        }
         

        
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
            str = tempCode.Split('\r').First(); 
            TextboxFZ3(str);
            //MessageBox.Show("d1" + tempCode);
        }
        void sp_DataReceived1(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = sender as SerialPort;
            String tempCode = sp.ReadExisting();
            str = tempCode.Split('\r').First(); 
            TextboxFZ3(str);
            //MessageBox.Show("d2" + tempCode);
        }

        private void TextboxFZ3(  string str)
        {
            if (this.txt_packbar.InvokeRequired)
            {
                FlushClient fc = new FlushClient(TextboxFZ3);
                this.Invoke(fc, str); //通过代理调用刷新方法
            }
            else
            { 
                    this.txt_packbar.Text = str; 
            }
        }
        private delegate void FlushClient(  string a);


       

        private void button2_Click(object sender, EventArgs e)
        {
           InBound.Business.HunHeService.InsertAllPack_bar(lbl_itemno.Text,txt_packbar.Text);
           redate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            redate();
        }

        public void redate()
        {
            if (txt_search.Text.Length <= 0)
            {
                dataGridView1.DataSource = InBound.Business.HunHeService.GetAllPack_bar();
            }
            else
            {
                dataGridView1.DataSource = InBound.Business.HunHeService.GetAllPack_bar(txt_search.Text);
            } 
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                lbl_itemno.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txt_itemname.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (dataGridView1.Rows[e.RowIndex].Cells[2].Value ==null)
                {
                    str = "";
                   
                }
                else
                {
                    str = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                }
                TextboxFZ3(str);
            }
        }

        private void Form2_Deactivate(object sender, EventArgs e)
        {
            //this.Dispose();
            //this.Close(); 
        }

       

    }
}
