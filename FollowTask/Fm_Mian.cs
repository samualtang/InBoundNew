using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FollowTask
{
    public partial class Fm_Mian : Form
    {
        public Fm_Mian()
        {
            InitializeComponent();
         
        }
        #region 常用方法
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
                    else if (frm.Text == "")
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
        /// <summary>
        /// MachineShow方法
        /// </summary> 
        /// <param text="fm">第几组</param>
        void ShowMchineForm(string text)
        {
            fm_Machine fm = new fm_Machine(text);
            if (CheckExist(fm) == true)
            {
                fm.Dispose();
                fm = null;
                return;
            }
            fm.MdiParent = this;
            fm.WindowState = FormWindowState.Maximized;
            fm.Show();
        }
        /// <summary>
        /// SortingShow方法
        /// </summary>
        /// <param name="text">第几组</param>
        void ShowSortingForm(string text)
        {
            Fm_FollowTaskSorting fm = new Fm_FollowTaskSorting(text);
            if (CheckExist(fm) == true)
            {
                fm.Dispose();
                fm = null;
                return;
            }
            fm.MdiParent = this;
            fm.WindowState = FormWindowState.Maximized;
            fm.Show();
        }
        /// <summary>
        /// UinionShow方法
        /// </summary>
        /// <param name="text">第几根</param>
        void ShowUinionFrom(string text)
        {
            Fm_FollowTaskUnion fm = new Fm_FollowTaskUnion(text);
            if (CheckExist(fm) == true)
            {
                fm.Dispose();
                fm = null;
                return;
            }
            fm.MdiParent = this;
            fm.WindowState = FormWindowState.Maximized;
            fm.Show();

        }
        #endregion
        private void pictureBox1_Click(object sender, EventArgs e)
        {
           


        }
        void TreeViewBind()
        {
            

        }

        private void treeV_AfterSelect(object sender, TreeViewEventArgs e)
        { 
            string nodeselect = treeV.SelectedNode.Name;//获取选择name 
            switch (nodeselect)
            {
                #region 机械手  
                case "group1":
                    ShowMchineForm(    "机械手,第一组");
                    txtMainInfo.Clear();
                    txtMainInfo.Text  = "第一组的机械手信息"; 
                    break;
                case "group2":
                    ShowMchineForm(    "机械手,第二组");
                    txtMainInfo.Clear();
                    txtMainInfo.Text  = "第二组的机械手信息"; 
                    break;
                case "group3":
                    ShowMchineForm(    "机械手,第三组");
                    txtMainInfo.Clear();
                    txtMainInfo.Text  = "第三组的机械手信息"; 
                    break;
                case "group4":
                    ShowMchineForm(    "机械手,第四组");
                    txtMainInfo.Clear();
                    txtMainInfo.Text  = "第四组的机械手信息"; 
                    break;
                case "group5":
                    ShowMchineForm(    "机械手,第五组");
                    txtMainInfo.Clear();
                    txtMainInfo.Text  = "第五组的机械手信息"; 
                    break;
                case "group6":
                    ShowMchineForm(    "机械手,第六组");
                    txtMainInfo.Clear();
                    txtMainInfo.Text  = "第六组的机械手信息"; 
                    break;
                case "group7":
                    ShowMchineForm(    "机械手,第七组");
                    txtMainInfo.Clear();
                    txtMainInfo.Text  = "第七组的机械手信息"; 
                    break;
                case "group8":
                    ShowMchineForm(    "机械手,第八组");
                    txtMainInfo.Clear();
                    txtMainInfo.Text  = "第八组的机械手信息"; 
                    break;
                #endregion 
                #region 预分拣
                case "fjGourp1":
                    ShowSortingForm("预分拣,第一组");
                 break;
                case "fjGourp2":
                 ShowSortingForm("预分拣,第二组");
                 break;
                case "fjGroup3":
                 ShowSortingForm("预分拣,第三组");
                 break;
                case "fjGroup4":
                 ShowSortingForm("预分拣,第四组");
                 break;
                case "fjGroup5":
                 ShowSortingForm("预分拣,第五组");
                 break;
                case "fjGroup6":
                 ShowSortingForm("预分拣,第六组");
                 break;
                case "fjGroup7":
                 ShowSortingForm("预分拣,第七组");
                 break;
                case "fjGroup8":
                 ShowSortingForm("预分拣,第八组");
                 break;
                #endregion 
                #region 合流
                case "UinonBelt1":
                 ShowUinionFrom("合流,第一根");
                 break;
                case "UinonBelt2":
                 ShowUinionFrom("合流,第二根");
                 break;
                case "UinonBelt3":
                 ShowUinionFrom("合流,第三根");
                 break;
                case "UinonBelt4":
                 ShowUinionFrom("合流,第四根");
                 break;
                #endregion

            }

        }
    }
}
