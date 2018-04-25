namespace FollowTask
{
    partial class Fm_Mian
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("组一");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("组二");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("第一大组", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("组三");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("组四");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("第二大组", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("组五");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("组六");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("第三大组", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("组七");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("组八");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("第四大组", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("机械手", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode6,
            treeNode9,
            treeNode12});
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("第一组");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("第二组");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("第一大组", new System.Windows.Forms.TreeNode[] {
            treeNode14,
            treeNode15});
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("第三组");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("第四组");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("第二大组", new System.Windows.Forms.TreeNode[] {
            treeNode17,
            treeNode18});
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("第五组");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("第六组");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("第三大组", new System.Windows.Forms.TreeNode[] {
            treeNode20,
            treeNode21});
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("第七组");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("第八组");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("第四大组", new System.Windows.Forms.TreeNode[] {
            treeNode23,
            treeNode24});
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("预分拣", new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode19,
            treeNode22,
            treeNode25});
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("一号主皮带");
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("二号主皮带");
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("三号主皮带");
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("四号主皮带");
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("合流", new System.Windows.Forms.TreeNode[] {
            treeNode27,
            treeNode28,
            treeNode29,
            treeNode30});
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtMainInfo = new System.Windows.Forms.TextBox();
            this.treeV = new System.Windows.Forms.TreeView();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1004, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 系统ToolStripMenuItem
            // 
            this.系统ToolStripMenuItem.Name = "系统ToolStripMenuItem";
            this.系统ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.系统ToolStripMenuItem.Text = "系统";
            // 
            // txtMainInfo
            // 
            this.txtMainInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMainInfo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMainInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtMainInfo.Enabled = false;
            this.txtMainInfo.Location = new System.Drawing.Point(0, 560);
            this.txtMainInfo.Name = "txtMainInfo";
            this.txtMainInfo.ReadOnly = true;
            this.txtMainInfo.Size = new System.Drawing.Size(1004, 21);
            this.txtMainInfo.TabIndex = 3;
            this.txtMainInfo.Text = "信息:";
            // 
            // treeV
            // 
            this.treeV.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeV.Font = new System.Drawing.Font("宋体", 13F);
            this.treeV.Location = new System.Drawing.Point(0, 25);
            this.treeV.Name = "treeV";
            treeNode1.Name = "group1";
            treeNode1.Text = "组一";
            treeNode2.Name = "group2";
            treeNode2.Text = "组二";
            treeNode3.Name = "BigGroup1";
            treeNode3.Text = "第一大组";
            treeNode4.Name = "group3";
            treeNode4.Text = "组三";
            treeNode5.Name = "group4";
            treeNode5.Text = "组四";
            treeNode6.Name = "BigGroup2";
            treeNode6.Text = "第二大组";
            treeNode7.Name = "group5";
            treeNode7.Text = "组五";
            treeNode8.Name = "group6";
            treeNode8.Text = "组六";
            treeNode9.Name = "BigGroup3";
            treeNode9.Text = "第三大组";
            treeNode10.Name = "group7";
            treeNode10.Text = "组七";
            treeNode11.Name = "group8";
            treeNode11.Text = "组八";
            treeNode12.Name = "BigGroup4";
            treeNode12.Text = "第四大组";
            treeNode13.Name = "Machine";
            treeNode13.Text = "机械手";
            treeNode14.Name = "fjGourp1";
            treeNode14.Text = "第一组";
            treeNode15.Name = "fjGroup2";
            treeNode15.Text = "第二组";
            treeNode16.Name = "fjBigGroup1";
            treeNode16.Text = "第一大组";
            treeNode17.Name = "fjGroup3";
            treeNode17.Text = "第三组";
            treeNode18.Name = "fjGroup4";
            treeNode18.Text = "第四组";
            treeNode19.Name = "fjBigGroup2";
            treeNode19.Text = "第二大组";
            treeNode20.Name = "fjGroup5";
            treeNode20.Text = "第五组";
            treeNode21.Name = "fjGroup6";
            treeNode21.Text = "第六组";
            treeNode22.Name = "fjBigGroup3";
            treeNode22.Text = "第三大组";
            treeNode23.Name = "fjGroup7";
            treeNode23.Text = "第七组";
            treeNode24.Name = "fjGroup8";
            treeNode24.Text = "第八组";
            treeNode25.Name = "fjBigGroup4";
            treeNode25.Text = "第四大组";
            treeNode26.Name = "readyTASK";
            treeNode26.Text = "预分拣";
            treeNode27.Name = "UinonBelt1";
            treeNode27.Text = "一号主皮带";
            treeNode28.Name = "UinonBelt2";
            treeNode28.Text = "二号主皮带";
            treeNode29.Name = "UinonBelt3";
            treeNode29.Text = "三号主皮带";
            treeNode30.Name = "UinonBelt4";
            treeNode30.Text = "四号主皮带";
            treeNode31.Name = "UinonTask";
            treeNode31.Text = "合流";
            this.treeV.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode26,
            treeNode31});
            this.treeV.ShowNodeToolTips = true;
            this.treeV.Size = new System.Drawing.Size(156, 535);
            this.treeV.TabIndex = 6;
            this.treeV.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeV_AfterSelect);
            // 
            // Fm_Mian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 581);
            this.Controls.Add(this.treeV);
            this.Controls.Add(this.txtMainInfo);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.Name = "Fm_Mian";
            this.Text = "任务追踪";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 系统ToolStripMenuItem;
        private System.Windows.Forms.TextBox txtMainInfo;
        private System.Windows.Forms.TreeView treeV;

    }
}

