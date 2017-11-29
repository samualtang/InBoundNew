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
    public partial class LaneWayFM : Form
    {
        public LaneWayFM()
        {
            InitializeComponent();
        }

        private void LaneWayFM_Load(object sender, EventArgs e)
        {
            search();
        }
        void search()
        {
            dataGridView1.DataSource = LaneWayService.GetList();
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
                        status = 40;

                        break;
                    case "btnIn":
                        status = 20;

                        break;
                    case "btnOut":
                        status = 30;

                        break;


                }
                LaneWayService.UpdateEntity(code, status);
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
                    case "20":
                        statusText = "禁入";
                        break;
                    case "30":
                        statusText = "禁出";
                        break;
                    case "40":
                        statusText = "禁用";
                        break;
                }
                e.Value = statusText;

            }
        }

      
        
    }
}
