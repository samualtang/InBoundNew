using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FollowTask.ErrorStart
{
    public partial class ErrorStart_Main : Form
    {
        public ErrorStart_Main()
        {
            InitializeComponent();
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((sender as TreeView) != null)
            {

                treeView1.SelectedNode = treeView1.GetNodeAt(e.X, e.Y);
                if (treeView1.SelectedNode != null)
                {
                    string nodeselect = treeView1.SelectedNode.Name;//获取选择name 

                    switch (nodeselect)
                    {
                        case "SortForm":
                            ShowUinionFrom("开机自检");
                            break;
                    }
                }
            }
        }

        SortForm sort;
        /// <summary>
        /// 展示窗口
        /// </summary>
        /// <param name="text"></param>
        void ShowUinionFrom(string text)
        {
            if (CheckExist(sort) == true)
            {
                sort.Show();
                sort.Location = new Point(0, 0);
                sort.MdiParent = this;
             
                return;
            }
            sort = new SortForm();
            sort.Show();

        }
        public void OnDataChange(int group, int[] clientId, object[] values)
        {
            if (group == 1)//一号主皮带八个机械手
            {

            }
            if (group == 2)//二号主皮带八个机械手
            {

            }
            if (group == 3)//三号主皮带八个机械手
            {

            }
            if (group == 4)//四号主皮带八个机械手
            {

            }
        }

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
                        tmpFrm.Show();
                        tmpFrm.Activate();
                    }
                    else if (frm.Text == "")
                    {
                        blResult = true;
                        tmpFrm.Show();
                        tmpFrm.Activate();
                    }
                    else if (frm.GetType().Name.ToLower() == "w_export_new")
                    {
                        blResult = true;
                        tmpFrm.Show();
                        tmpFrm.Activate();
                    }
                }
            }
            return blResult;
        }
    }
}