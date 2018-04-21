using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using highSpeed.PubFunc;
using System.Data.OracleClient;
using System.Data;
using System.Runtime.Remoting.Messaging;

namespace highSpeed
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
            //w_main fw = new w_main();
            //Application.Run(fw);
            if (args.Length == 0)
            {
                if (PublicFun.start())
                {

                }
                else
                {
                    MessageBox.Show("数据库配置文件错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    w_database_set w_database_set = new w_database_set();
                    w_database_set.ShowDialog();
                    if (w_database_set.isCancel)
                    {

                    }
                    else
                    {
                        return;
                    }
                }


                //异步调用
                // Application.Run(form1);
                //getCustomerHandler gchandler = new getCustomerHandler(DataPublic.getCustomer);
                //IAsyncResult result = gchandler.BeginInvoke( new AsyncCallback(CallBackMethod), null);
                //异步调用结束

                //DataPublic.getCustomer();
                w_user users = new w_user();
                users.ShowDialog();
                if (users.DialogResult == DialogResult.OK)
                {
                    w_main form1 = new w_main();
                    form1.WindowState = FormWindowState.Maximized;
                    Application.Run(form1);
                }
                else
                {
                    return;
                }


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
