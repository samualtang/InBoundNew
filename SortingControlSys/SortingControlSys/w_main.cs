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
            w_SortingControlMain.MdiParent = this;
            //w_SortingControlMain.WindowState = FormWindowState.Maximized;
            w_SortingControlMain.FormBorderStyle = FormBorderStyle.None;
            w_SortingControlMain.Width = 1920;
            w_SortingControlMain.Height = 700;
            w_SortingControlMain.Show();
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
    }
}
