using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using InBound.Business;
using InBound;

namespace LabelPrint
{
    public partial class QueryForm : Form
    {

        public int dataType = 1;
        public QueryForm()
        {
            InitializeComponent();
            Rectangle rect = Screen.GetWorkingArea(this);
            this.groupBox1.Width = rect.Width - 50;
            this.groupBox2.Width = rect.Width - 50;
            this.ItemList.Width = rect.Width - 60;
            this.pager1.Width = rect.Width - 60;
            initPager();
        }
        void initPager()
        {
            this.pager1.PageChanged += new WHC.Pager.WinControl.PageChangedEventHandler(pager1_PageChanged);

            ButtonX btnExport = (ButtonX)pager1.Controls[0];
            btnExport.Visible = false;
            ButtonX btnCurrent = (ButtonX)pager1.Controls[1];
            btnCurrent.Visible = false;
            ButtonX btn2 = (ButtonX)pager1.Controls[2];
            ButtonX btn3 = (ButtonX)pager1.Controls[3];
            ButtonX btn4 = (ButtonX)pager1.Controls[4];
            ButtonX btn5 = (ButtonX)pager1.Controls[5];
            PanelEx p = (PanelEx)pager1.Controls[7];
            LabelX lab = (LabelX)p.Controls[0];
            btn2.Height = 20;
            btn3.Height = 20;
            btn4.Height = 20;
            btn5.Height = 20;
            p.Height = 40;
            lab.Height = 30;
            pager1.PageSize = 10;
            pager1_PageChanged(null, null);
        }
        void pager1_PageChanged(object sender, EventArgs e)
        {
            if (dataType == 2)
            {
                button1_Click(null, null);
            }
            else
            {
                WmsService service = new WmsService();
                int total = 0;
                this.ItemList.DataSource = service.GetItemPageList(tbName.Text, tbCode.Text, pager1.CurrentPageIndex - 1, pager1.PageSize, out total);
                pager1.RecordCount = total;
                this.pager1.InitPageInfo();
            }
        }
        private void ItemList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             int CIndex = e.ColumnIndex;
             int rowIndex = e.RowIndex;
            if (CIndex == 5)
            {
                String code = ItemList.Rows[rowIndex].Cells[0].Value.ToString();
                int count=0 ;
                if (ItemList.Rows[rowIndex].Cells[4].Value == null)
                {
                    count = 1;
                }
                else
                {
                    int.TryParse(ItemList.Rows[rowIndex].Cells[4].Value.ToString(), out count);
                }
                RdlcPro.RdlcReport form = new RdlcPro.RdlcReport(code, count);
                form.Show();
            }
        }

        private void pager1_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            pager1.CurrentPageIndex = 1;
            pager1_PageChanged(null, null);
        }
        private void tBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 0x20) e.KeyChar = (char)0;  //禁止空格键  
            if ((e.KeyChar == 0x2D) && (((TextBox)sender).Text.Length == 0)) return;   //处理负数  
            if (e.KeyChar > 0x20)
            {
                try
                {
                    double.Parse(((TextBox)sender).Text + e.KeyChar.ToString());
                }
                catch
                {
                    e.KeyChar = (char)0;   //处理非法字符  
                }
            }
        }  

        private void btnPrint_Click(object sender, EventArgs e)
        {
            String code = tbjyCode.Text;
            int count = 0;
             int.TryParse(tbNum.Text.ToString(), out count);
            RdlcPro.RdlcReport form = new RdlcPro.RdlcReport(code, count,1);
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataType = 2;
          //写数据到inf_jobdownload;
            List<T_WMS_ITEM> list = InBoundLineService.GetInBoundCigarette(tbName.Text, tbCode.Text);
            this.ItemList.DataSource = list;
            pager1.RecordCount = list.Count;
            this.pager1.InitPageInfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataType = 1;
            WmsService service = new WmsService();
            int total = 0;
            this.ItemList.DataSource = service.GetItemPageList(tbName.Text, tbCode.Text, pager1.CurrentPageIndex - 1, pager1.PageSize, out total);
            pager1.RecordCount = total;
            this.pager1.InitPageInfo();
        }       
    }
}
