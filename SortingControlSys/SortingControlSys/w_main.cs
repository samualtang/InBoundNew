using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SortingControlSys.PubFunc;
using SortingControlSys.SortingControl;
using System.Configuration;

namespace SortingControlSys
{
    public partial class w_main : Form
    {
        public w_main()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "长株潭物流分拣中心";
            toolStripStatusLabel2.Text = "当前用户：";
            toolStripStatusLabel3.Text = "登录时间："+System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            toolStripStatusLabel4.Text = "";

            w_SortingControlMain w_SortingControlMain = new w_SortingControlMain();
            if (CheckExist(w_SortingControlMain) == true)
            {
                w_SortingControlMain.Dispose();
                w_SortingControlMain = null;
                return;
            } 
         decimal   sortgroupno2 = decimal.Parse(ConfigurationManager.AppSettings["Group2"].ToString());
         this.Text = "长株潭烟草公司分拣系统-组" + sortgroupno2 / 2;
           // w_SortingControlMain.MdiParent = this;
            //w_SortingControlMain.WindowState = FormWindowState.Maximized;
            w_SortingControlMain.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            w_SortingControlMain.Width = 1920;
            w_SortingControlMain.Height = 700;
            w_SortingControlMain.Show();
            this.Close();
        }


        #region 查找是否已经打开
        /// <summary>
        /// 查找是否已经打开
        /// </summary>
        /// <param name="frm"></param>
        /// <returns></returns>
        private bool CheckExist(Form frm)
        {
            bool blResult = false;
            for (int i = 0; i < MdiChildren.Length; i++)
            {
                if (MdiChildren[i].GetType().Name == frm.GetType().Name)
                {
                    Form tmpFrm = MdiChildren[i];
                    if (tmpFrm.Text == frm.Text)
                    {
                        blResult = true;
                        tmpFrm.Activate();
                    }
                    else if (frm.GetType().Name.ToLower() == "w_export_new")
                    {
                        blResult = true;
                        tmpFrm.Activate();
                    }
                }
            }
            return blResult;
        }
        #endregion

        private void w_main_FormClosing(object sender, FormClosingEventArgs e)
        {
        //    DialogResult MsgBoxResult = MessageBox.Show("确定要退出程序?",//对话框的显示内容 
        //                                                     "操作提示",//对话框的标题  
        //                                                     MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
        //                                                     MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
        //                                                     MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
        //    //Console.WriteLine(MsgBoxResult);
        //    if (MsgBoxResult == DialogResult.Yes)
        //    {
        //        System.Environment.Exit(System.Environment.ExitCode);
        //        this.Dispose();
        //        this.Close();
        //    }
        //    else
        //    {
        //        e.Cancel = true;
        //    }
        //    MessageBox.Show("close......");
        }

        private void w_main_FormClosed(object sender, FormClosedEventArgs e)
        {
            //DialogResult MsgBoxResult = MessageBox.Show("确定要退出程序?",//对话框的显示内容 
            //                                                "操作提示",//对话框的标题  
            //                                                MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
            //                                                MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
            //                                                MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
            //MessageBox.Show("close......");
        }
    }
}
