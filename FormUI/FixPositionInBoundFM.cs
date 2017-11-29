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
namespace FormUI
{
    public partial class FixPositionInBoundFM : Form
    {
        public FixPositionInBoundFM()
        {
            InitializeComponent();
        }

        private void InBoundFM_Load(object sender, EventArgs e)
        {
            searchTask();
        }
        private void search()
        {
            List<InBound.T_WMS_ITEM> list = ItemService.GetItem(tbCode.Text, tbName.Text);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = list;
           
        }
        private void searchTask()
        {
            List<InBound.T_WMS_STORAGEAREA_INOUT> list = StroageInOutService.GetDetail(20, 1, 10);
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = list;

        }
        private void tbCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                search();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count>0)
            {
                tbChooseName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                tbChooseName.Tag = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            T_WMS_STORAGEAREA_INOUT entity = new T_WMS_STORAGEAREA_INOUT();
            entity.BOXQTY = int.Parse(tbNum.Text);
            entity.CIGARETTECODE = tbChooseName.Tag.ToString();
            entity.CIGARETTENAME = tbChooseName.Text.ToString();
            entity.AREAID = 1;
            entity.INOUTTYPE = 20;
            StroageInOutService.InsertEntity(entity);
            searchTask();
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                int value;

                e.Value = "正在入库......";
             
            }

        }

    
      

     

       

       

       

       
        
    }
}
