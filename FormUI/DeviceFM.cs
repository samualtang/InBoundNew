using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InBound.Business;
namespace FormUI
{
    public partial class DeviceFM : Form
    {
        public DeviceFM()
        {
            InitializeComponent();
        }
        #region
        //private void Btn_Click(object sender, EventArgs e)
        //{
        //    if (dataGridView1.SelectedRows.Count < 1)
        //    {
        //        MessageBox.Show("请选择一行进行操作");
        //    }
        //    else
        //    {

        //        String code = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        //        int status = 0;
        //        switch (((Button)sender).Name)
        //        { 
        //            case "btnStart":
        //                status = 10;
        //                break;
        //            case "btnDisable":
        //                status = 0; 
        //                break; 

        //        }
        //        DeviceService.UpdateEntity(code, status);
        //        search();
        //        //
        //    }
        //}
        #endregion
        private void LaneWayFM_Load(object sender, EventArgs e)
        {
            cmbSelectDeviceName.SelectedIndex = 0;
            search(); 
        }
        void search()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = DeviceService.GetList();
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = true;
            dataGridView1.Columns[6].Visible = true; 
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                String statusText = "";
                switch (e.Value.ToString())
                {
                    case "10":
                        statusText = "启用";
                        break;
                    case "0":
                        statusText = "禁用";
                        break;
                 
                }
                e.Value = statusText; 
            }
        }
        void DgvBind()
        {
            if (cmbSelectDeviceName.SelectedIndex == 1)
            {
                dataGridView1.Columns[3].Visible = true;
                dataGridView1.Columns[4].Visible = true;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false; 
                dataGridView1.Columns[3].HeaderCell.Value = "堆垛机对应出库最大任务数";
                dataGridView1.Columns[4].HeaderCell.Value = "堆垛机对应入库的最大数量";
            }
            else if (cmbSelectDeviceName.SelectedIndex == 2)
            {

                dataGridView1.Columns[3].Visible = true;
                dataGridView1.Columns[4].Visible = true;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false; 
                dataGridView1.Columns[4].Width = 250;
                dataGridView1.Columns[3].HeaderCell.Value = "最大任务数";
                dataGridView1.Columns[4].HeaderCell.Value = "开箱机对应烟柜通道";
            }
        }
        private void btnChange_Click(object sender, EventArgs e)
        { 
            if (dataGridView1.SelectedRows.Count < 1)
            {
                MessageBox.Show("请选择一行进行操作");
            }
            else if (cmbSelectDeviceName.SelectedIndex == 1)
            { 
                String code = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
           
                String OutMaxTaskNum = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();//最大任务数(堆垛机对应出库最大任务数)
                String InMaxTaskNum = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();//开箱机对应烟柜通道 和 堆垛机对应入库的最大数量
                //MessageBox.Show(code +"  "+ OutMaxTaskNum + "  " + InMaxTaskNum);
                DeviceService.UpdateDevice(code, OutMaxTaskNum, InMaxTaskNum); 
             
               // search();
            }
            else if (cmbSelectDeviceName.SelectedIndex == 2)
            {
                String code = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                String MaxTaskNum = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();//最大任务数(堆垛机对应出库最大任务数)
                String TroughNum = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();//开箱机对应烟柜通道   
                //MessageBox.Show(code + "  " + MaxTaskNum + "  " + TroughNum);
                DeviceService.UpdateDevice(code, TroughNum ,Convert.ToInt32(MaxTaskNum)); 
            }
        }

        private void cmbSelectDeviceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSelectDeviceName.SelectedIndex == 0)
            {
                search();  
            }
            else if (cmbSelectDeviceName.SelectedIndex == 1)
            {
                dataGridView1.DataSource = DeviceService.GetList().Where(a => Convert.ToInt32(a.DEVICENO) <= 3005 && Convert.ToInt32(a.DEVICENO) > 3000).ToList();

                DgvBind();

            }
            else if (cmbSelectDeviceName.SelectedIndex == 2)
            {
                dataGridView1.DataSource = DeviceService.GetList().Where(a => Convert.ToInt32(a.DEVICENO) <= 6000 && Convert.ToInt32(a.DEVICENO) > 5000).ToList();
                DgvBind();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (cmbSelectDeviceName.SelectedIndex == 0)
            {
                int Cindex = e.ColumnIndex;
                int status = 0;
                String code = dataGridView1[0, e.RowIndex].Value.ToString();
                //String code = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                if (Cindex == 5)//启用
                {
                    status = 10;
                }
                else if (Cindex == 6)//禁用
                {
                    status = 0;
                }
                // MessageBox.Show(status + ""+code);
                // DeviceService.UpdateEntity(code, status);
                search();
            }
        }
        

      
        
    }
}
