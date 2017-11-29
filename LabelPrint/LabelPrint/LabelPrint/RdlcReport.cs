using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LabelPrintBusiness;

namespace RdlcPro
{
    public partial class RdlcReport : Form
    {
        String code;
        int num;
        int model = 0;
        public RdlcReport(String code,int num)
        {
            this.code = code;
            this.num = num;
            InitializeComponent();
            System.Drawing.Printing.PageSettings ps = reportViewer1.GetPageSettings();// new System.Drawing.Printing.PageSettings();
            ps.Landscape=false;
            reportViewer1.SetPageSettings(ps);
        }
        public RdlcReport(String code, int num,int model)
        {
           this.model = 1;
            this.code = code;
            this.num = num;
            InitializeComponent();
            System.Drawing.Printing.PageSettings ps = reportViewer1.GetPageSettings();// new System.Drawing.Printing.PageSettings();
            ps.Landscape = false;
            reportViewer1.SetPageSettings(ps);
        }
        List<T_WMS_ITEM> GetList(String code,int num)
        {
            List<T_WMS_ITEM> list = new List<T_WMS_ITEM>();
            WmsService service=new WmsService();
            T_WMS_ITEM   item=service.GetItemByCode(code);
            if (item != null)
            {
                item.BIGBOX_BAR = "(91)" + item.BIGBOX_BAR + string.Format("{0:yyyyMMddHHmmss}", DateTime.Now)+"0000000000"; 

 
            }
            for(int i=0;i<num;i++)
            {
                list.Add(item);
            }
            return list;
        }
        List<T_WMS_ITEM> GetList()
        {
            List<T_WMS_ITEM> list = new List<T_WMS_ITEM>();

            T_WMS_ITEM item = new T_WMS_ITEM();
            item.BIGBOX_BAR = code;
            for (int i = 0; i < num; i++)
            {
                list.Add(item);
            }
            return list;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (model == 1)
            {
                this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("BARSource", GetList()));
                this.reportViewer1.RefreshReport();
            }
            else
            {
                this.reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("BARSource", GetList(code, num)));
                this.reportViewer1.RefreshReport();
            }
        }
    }
}