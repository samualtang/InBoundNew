﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using InBound;
namespace FormUI
{
    
    static class Program
    {
        
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {


          //  w_user users = new w_user();
            //users.ShowDialog();
           // if (users.DialogResult == DialogResult.OK)
            if
                (1==1)
            {
                MainUI.MainFrm dFm = new MainUI.MainFrm();
                dFm.WindowState = FormWindowState.Maximized;
                Application.Run(dFm);
            }
            else
            {
                return;
            }
        }
    }
}
