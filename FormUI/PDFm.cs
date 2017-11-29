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
    public partial class PDFm : Form
    {
        public PDFm()
        {
            InitializeComponent();
        }
        private void search()
        {
            List<InBound.T_WMS_ITEM> list = ItemService.GetItem(tbCode.Text, tbName.Text);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = list;

        }
        private void tbCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                search();
            }
        }
        private void OutBoundFm_Load(object sender, EventArgs e)
        {
          // this.routeLB.DataSource = RouteInfoService.GetList();
           //this.routeLB.DisplayMember = "RouteCode";
           //this.routeLB.ValueMember = "RouteCode";
        }
    }
}
