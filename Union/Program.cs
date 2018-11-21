using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SortingControlSys;
using System.Diagnostics;

namespace Union
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Process[] localNmae = Process.GetProcessesByName("Union");
            if (localNmae.Length > 1)
            {
                MessageBox.Show("合流程序已经打开,请关闭程序后再打开!");

            }
            else
            {
                Application.Run(new w_user());
            }
        }
    }
}
