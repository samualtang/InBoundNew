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
using System.Threading;
using System.Transactions;
namespace FormUI
{
    public partial class InBoundNoReport : Form
    {
        public InBoundNoReport()
        {
            InitializeComponent();
        }
        List<String> address = new List<string>() { "1001", "1011", "1021" };
        private void InBoundFM_Load(object sender, EventArgs e)
        {

            search();
            searchTask();
        }
        
       
        private void search()
        {
            if (dateTimePicker1.Value.Date <= dateTimePicker2.Value.Date)
            {
                List<InBound.T_WMS_INBOUND> itemlist = InBoundService.GetItemSec(dateTimePicker1.Value.Date, dateTimePicker2.Value.Date.AddDays(1));
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = itemlist;
                if (itemlist != null && itemlist.Count > 0)
                {
                    inboundid = itemlist[0].INBOUNDID;
                }
            }
            else
            {
                MessageBox.Show("开始时间必须小于结束时间");
            }
           
        }

        delegate void gv();
        private void initText()
        {
           
        }
        private void searchTask()
        {
            List<InBound.T_WMS_INBOUND_LINE> list = InBoundLineService.GetItem(inboundid);
            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.BeginInvoke( new gv(searchTask));
            }
            else
            {
                dataGridView2.AutoGenerateColumns = false;
                dataGridView2.DataSource = list;
            }

        }
        private void tbCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                search();
            }
        }
        decimal inboundid = 0;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count>0)
            {
                inboundid = decimal.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                searchTask();
              
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
       delegate void UpdateTextBox(String text);
       
       delegate void HandleDelegate(String text);
       public void updateListBox(string info)
       {
           String time = DateTime.Now.ToLongTimeString();
           if (this.logList.InvokeRequired)
           {


               this.logList.Invoke(new HandleDelegate(updateListBox), info);
           }
           else
           {
               this.logList.Items.Insert(0, time + "    " + info);

           }
       }

        bool isRun = true;
        public void startAutoInBound()
        {

            sendTask();
       
         
        }
        int i = 0;
      public static  int selectIndex = 0;
       
        // 发送堆垛机命令
        public void sendTask()
        {
        }
        delegate void SearchHanlder();

        public void checkProcess()
        { 
        }
        public void insertMoveAction()
        {
            //T_WMS_STORAGEAREA_INOUT entity = new T_WMS_STORAGEAREA_INOUT();
            //entity.BOXQTY = int.Parse(tbNum.Text);
            //entity.CIGARETTECODE = ((List<String>)tbChooseName.Tag)[0];
            //entity.CIGARETTENAME = tbChooseName.Text.ToString();
            //entity.AREAID = 1;
            //entity.INOUTTYPE = 20;
            //StroageInOutService.InsertEntity(entity);
            //searchTask();
        }
        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
        }

        private void AutoInBoundFM_FormClosed(object sender, FormClosedEventArgs e)
        {
            isRun = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            search();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            searchTask();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
           
        }

        private void CBAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
         //   int i = 0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            searchTask();
        }

    }
}
