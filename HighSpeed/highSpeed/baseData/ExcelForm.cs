using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace highSpeed.baseData
{
    public partial class ExcelForm : Form
    {    
        public static Microsoft.Office.Interop.Excel.Range range = null; 
        public static Microsoft.Office.Interop.Excel.Workbook wbb = null;
       
        public void OpenExcel(string strFilePathAndFileName)
        {
          //  webBrowser1.Navigate(strFilePathAndFileName); 
        }
        public ExcelForm()
        {
            InitializeComponent();
            //axFramerControl1.
            this.axFramerControl1.Open(@"F:\\7月28日尾数.xls");
        }
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //Object refmissing = System.Reflection.Missing.Value;
            //object[] args = new object[4];
            //args[0] = SHDocVw.OLECMDID.OLECMDID_HIDETOOLBARS;
            //args[1] = SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER;
            //args[2] = refmissing;
            //args[3] = refmissing;
            //object axWebBrowser = this.webBrowser1.ActiveXInstance;
            //axWebBrowser.GetType().InvokeMember("ExecWB", BindingFlags.InvokeMethod, null, axWebBrowser, args);
            //object oApplication = axWebBrowser.GetType().InvokeMember("Document", BindingFlags.GetProperty, null, axWebBrowser, null);
            //wbb = (Microsoft.Office.Interop.Excel.Workbook)oApplication;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.axFramerControl1.Open(@"F:\\7月28日尾数.xls");
        }

    }
}
