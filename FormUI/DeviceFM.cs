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

        private void LaneWayFM_Load(object sender, EventArgs e)
        {
            search();
        }
        void search()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = DeviceService.GetList();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
            {
                MessageBox.Show("请选择一行进行操作");
            }
            else
            {

                String code = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                int status = 0;
                switch (((Button)sender).Name)
                { 
                    case "btnStart":
                        status = 10;
                        break;
                    case "btnDisable":
                        status = 0;

                        break;
                   

                       


                }
                DeviceService.UpdateEntity(code, status);
                search();
                //
            }
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

      
        
    }
}
