using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SortingControlSys.PubFunc;
using System.Data.OracleClient;
using System.Data;
using System.Runtime.Remoting.Messaging;
using SortingControlSys.SortingControl;

namespace SortingControlSys
{
    static class Program
    {
       


        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length == 0)
            {
                //if (PublicFun.start())
                //{

                //}
                //else
                //{
                //    MessageBox.Show("数据库配置文件错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    w_database_set w_database_set = new w_database_set();
                //    w_database_set.ShowDialog();
                //    if (w_database_set.isCancel)
                //    {

                //    }
                //    else
                //    {
                //        return;
                //    }
                //}


                /*
                     test form1 = new test();
                     form1.WindowState = FormWindowState.Maximized;
                     Application.Run(form1);
               
               
                w_main form1 = new w_main();
                 form1.WindowState = FormWindowState.Maximized;
                 Application.Run(form1);
                 
                w_SortingControlMain w_SortingControlMain = new w_SortingControlMain();
                w_SortingControlMain.WindowState = FormWindowState.Maximized;
                Application.Run(w_SortingControlMain);
                 * */
                try
                {
                    w_SortingControlMain form1 = new w_SortingControlMain();
                    form1.WindowState = FormWindowState.Maximized;
                    Application.Run(form1);
                }
                catch (Exception ex)
                { }
            }


           

            /**
            Form1 form1 = new Form1();
            form1.WindowState = FormWindowState.Maximized;
            Application.Run(form1);
             */
        }

        private static void CallBackMethod(IAsyncResult result)
        {
            getCustomerHandler gchandler = (getCustomerHandler)((AsyncResult)result).AsyncDelegate;
            //DataPublic.dataTable = (DataTable)handler.EndInvoke(result);
            gchandler.EndInvoke(result);
        }


    }
}
