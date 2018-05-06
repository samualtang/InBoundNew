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
        /// <summary>
        /// treeV隐藏标志
        /// </summary>
        bool click = true;
        const string btmpathLeft = @" D:\InBoundNew\FollowTask\Resources\5255\4.ico";
        const string btmpathRight = @" D:\InBoundNew\FollowTask\Resources\5255\7.ico";
        public Fm_Mian()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
         
        }
        private void Fm_Mian_Load(object sender, EventArgs e)
        {
            BitmapRegion.CreateControlRegion(btnLeft, new Bitmap(btmpathLeft));//创建Button图片
        }


        private void treeV_AfterSelect(object sender, TreeViewEventArgs e)
        { 
            string nodeselect = treeV.SelectedNode.Name;//获取选择name 
            switch (nodeselect)
            {
                #region 机械手  
                case "MachineGroup1":
                    ShowMchineForm("机械手,第1组");
                    txtMainInfo.Clear();
                    txtMainInfo.Text  = "第1组的机械手信息"; 
                    break;
                case "MachineGroup2":
                    ShowMchineForm("机械手,第2组");
                    txtMainInfo.Clear();
                    txtMainInfo.Text  = "第2组的机械手信息"; 
                    break;
                case "MachineGroup3":
                    ShowMchineForm("机械手,第3组");
                    txtMainInfo.Clear();
                    txtMainInfo.Text  = "第3组的机械手信息"; 
                    break;
                case "MachineGroup4":
                    ShowMchineForm("机械手,第4组");
                    txtMainInfo.Clear();
                    txtMainInfo.Text  = "第4组的机械手信息"; 
                    break;
                case "MachineGroup5":
                    ShowMchineForm("机械手,第5组");
                    txtMainInfo.Clear();
                    txtMainInfo.Text  = "第5组的机械手信息"; 
                    break;
                case "MachineGroup6":
                    ShowMchineForm("机械手,第6组");
                    txtMainInfo.Clear();
                    txtMainInfo.Text  = "第6组的机械手信息"; 
                    break;
                case "MachineGroup7":
                    ShowMchineForm("机械手,第7组");
                    txtMainInfo.Clear();
                    txtMainInfo.Text  = "第7组的机械手信息"; 
                    break;
                case "MachineGroup8":
                    ShowMchineForm("机械手,第8组");
                    txtMainInfo.Clear();
                    txtMainInfo.Text  = "第8组的机械手信息"; 
                    break;
                #endregion 
                #region 预分拣
                case "fjBigGroup1":
                 ShowSortingForm("预分拣,第1组");
                 break;
                case "fjBigGroup2":
                 ShowSortingForm("预分拣,第2组");
                 break;
                case "fjBigGroup3":
                 ShowSortingForm("预分拣,第3组");
                 break;
                case "fjBigGroup4":
                 ShowSortingForm("预分拣,第4组");
                 break;
                case "fjBigGroup5":
                 ShowSortingForm("预分拣,第5组");
                 break;
                case "fjBigGroup6":
                 ShowSortingForm("预分拣,第6组");
                 break;
                case "fjBigGroup7":
                 ShowSortingForm("预分拣,第7组");
                 break;
                case "fjBigGroup8":
                 ShowSortingForm("预分拣,第8组");
                 break;
                #endregion 
                #region 合流
                case "UinonBelt1":
                 ShowUinionFrom("合流,第1根");
                 break;
                case "UinonBelt2":
                 ShowUinionFrom("合流,第2根");
                 break;
                case "UinonBelt3":
                 ShowUinionFrom("合流,第3根");
                 break;
                case "UinonBelt4":
                 ShowUinionFrom("合流,第4根");
                 break;
                #endregion

            }

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


        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (click)
            {
                treeV.Visible = false;//隐藏
                btnLeft.Location = new Point(0, this.Size.Height / 2); 
                BitmapRegion.CreateControlRegion(btnLeft, new Bitmap(btmpathRight));
                click = false;
            }
            else
            {
                treeV.Visible = true;
                btnLeft.Dock = DockStyle.None;
                btnLeft.Location = new Point(166, this.Size.Height / 2); 
                BitmapRegion.CreateControlRegion(btnLeft, new Bitmap(btmpathLeft));
                click = true;
            }
          

        }

        private void Fm_Mian_SizeChanged(object sender, EventArgs e)
        {
            if (!click)
            {
                btnLeft.Location = new Point(0, this.Size.Height / 2);
            }
            else
            {
                btnLeft.Location = new Point(166, this.Size.Height / 2);
            }
        }

        private void btnLeft_MouseMove(object sender, MouseEventArgs e)
        {
            if (click)
            {
                TxtBoxMianInFo( "隐藏树状菜单"); 
            }
            else
            {
                TxtBoxMianInFo( "还原树状菜单"); 
            }
        }

        private void btnLeft_MouseLeave(object sender, EventArgs e)
        {
            TxtBoxMianInFo( "信息:");   
        }
        private void 退出EToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult MsgBoxResult = MessageBox.Show("确定要退出程序?",//对话框的显示内容 
                                                           "操作提示",//对话框的标题  
                                                           MessageBoxButtons.YesNo,//定义对话框的按钮，这里定义了YSE和NO两个按钮 
                                                           MessageBoxIcon.Question,//定义对话框内的图表式样，这里是一个黄色三角型内加一个感叹号 
                                                           MessageBoxDefaultButton.Button2);//定义对话框的按钮式样
            //Console.WriteLine(MsgBoxResult);
            if (MsgBoxResult == DialogResult.Yes)
            {
                System.Environment.Exit(System.Environment.ExitCode);

                this.Dispose();
                this.Close();
            }
            else
            {
                return;
            }

        }
        /// <summary>
        /// 主窗体信息提示
        /// </summary>
        /// <param name="text"></param>
        public void TxtBoxMianInFo(string text)
        {
            txtMainInfo.Text = text;

        }

        private void txtMainInfo_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void 机械手MToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_MachineTask fm = new w_MachineTask("机械手");
            if (CheckExist(fm) == true)
            {
                fm.Dispose();
                fm = null;
                return;
            }
            fm.MdiParent = this;
            fm.Show();
        }

        private void 预分拣YToolStripMenuItem_Click(object sender, EventArgs e)
        {
            W_FenJTask fm = new W_FenJTask();
            if (CheckExist(fm) == true)
            {
                fm.Dispose();
                fm = null;
                return;
            }
            fm.MdiParent = this;
            fm.Show(); 
        }

        private void 合流UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_UnionTask fm = new w_UnionTask();
            if (CheckExist(fm) == true)
            {
                fm.Dispose();
                fm = null;
                return;
            }
            fm.MdiParent = this;
            fm.Show();
        }

        private void 异形烟NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w_UnNormal fm = new w_UnNormal();
            if (CheckExist(fm) == true)
            {
                fm.Dispose();
                fm = null;
                return;
            }
            fm.MdiParent = this;
            fm.Show();
        }
    }
}
