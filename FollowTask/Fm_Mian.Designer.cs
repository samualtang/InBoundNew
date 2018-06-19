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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("第一组");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("第二组");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("第三组");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("第四组");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("第五组");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("第六组");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("第七组");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("第八组");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("机械手", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("第一组");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("第二组");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("第三组");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("第四组");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("第五组");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("第六组");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("第七组");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("第八组");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("预分拣", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode16,
            treeNode17});
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("合流");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fm_Mian));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询任务sToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.机械手MToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.预分拣YToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.合流UToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.异形烟NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.补货任务ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtMainInfo = new System.Windows.Forms.TextBox();
            this.treeV = new System.Windows.Forms.TreeView();
            this.btnLeft = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统ToolStripMenuItem,
            this.查询任务sToolStripMenuItem,
            this.补货任务ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1181, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 系统ToolStripMenuItem
            // 
            this.系统ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出EToolStripMenuItem});
            this.系统ToolStripMenuItem.Name = "系统ToolStripMenuItem";
            this.系统ToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.系统ToolStripMenuItem.Text = "系统(&S)";
            // 
            // 退出EToolStripMenuItem
            // 
            this.退出EToolStripMenuItem.Name = "退出EToolStripMenuItem";
            this.退出EToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.退出EToolStripMenuItem.Text = "退出(&E)";
            this.退出EToolStripMenuItem.Click += new System.EventHandler(this.退出EToolStripMenuItem_Click);
            // 
            // 查询任务sToolStripMenuItem
            // 
            this.查询任务sToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.机械手MToolStripMenuItem,
            this.预分拣YToolStripMenuItem,
            this.合流UToolStripMenuItem,
            this.异形烟NToolStripMenuItem});
            this.查询任务sToolStripMenuItem.Name = "查询任务sToolStripMenuItem";
            this.查询任务sToolStripMenuItem.Size = new System.Drawing.Size(82, 21);
            this.查询任务sToolStripMenuItem.Text = "查询任务(&F)";
            this.查询任务sToolStripMenuItem.Click += new System.EventHandler(this.查询任务sToolStripMenuItem_Click);
            // 
            // 机械手MToolStripMenuItem
            // 
            this.机械手MToolStripMenuItem.Name = "机械手MToolStripMenuItem";
            this.机械手MToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.机械手MToolStripMenuItem.Text = "机械手(&M)";
            this.机械手MToolStripMenuItem.Click += new System.EventHandler(this.机械手MToolStripMenuItem_Click);
            // 
            // 预分拣YToolStripMenuItem
            // 
            this.预分拣YToolStripMenuItem.Name = "预分拣YToolStripMenuItem";
            this.预分拣YToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.预分拣YToolStripMenuItem.Text = "预分拣(&Y)";
            this.预分拣YToolStripMenuItem.Click += new System.EventHandler(this.预分拣YToolStripMenuItem_Click);
            // 
            // 合流UToolStripMenuItem
            // 
            this.合流UToolStripMenuItem.Name = "合流UToolStripMenuItem";
            this.合流UToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.合流UToolStripMenuItem.Text = "合流(&U)";
            this.合流UToolStripMenuItem.Click += new System.EventHandler(this.合流UToolStripMenuItem_Click);
            // 
            // 异形烟NToolStripMenuItem
            // 
            this.异形烟NToolStripMenuItem.Name = "异形烟NToolStripMenuItem";
            this.异形烟NToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.异形烟NToolStripMenuItem.Text = "异形烟(&N)";
            this.异形烟NToolStripMenuItem.Click += new System.EventHandler(this.异形烟NToolStripMenuItem_Click);
            // 
            // 补货任务ToolStripMenuItem
            // 
            this.补货任务ToolStripMenuItem.Name = "补货任务ToolStripMenuItem";
            this.补货任务ToolStripMenuItem.Size = new System.Drawing.Size(108, 21);
            this.补货任务ToolStripMenuItem.Text = "烟柜补货任务(&X)";
            this.补货任务ToolStripMenuItem.Visible = false;
            this.补货任务ToolStripMenuItem.Click += new System.EventHandler(this.补货任务ToolStripMenuItem_Click);
            // 
            // txtMainInfo
            // 
            this.txtMainInfo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMainInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtMainInfo.Enabled = false;
            this.txtMainInfo.Font = new System.Drawing.Font("宋体", 11F);
            this.txtMainInfo.Location = new System.Drawing.Point(0, 722);
            this.txtMainInfo.Name = "txtMainInfo";
            this.txtMainInfo.ReadOnly = true;
            this.txtMainInfo.Size = new System.Drawing.Size(1181, 24);
            this.txtMainInfo.TabIndex = 3;
            this.txtMainInfo.Text = "信息:";
            this.txtMainInfo.TextChanged += new System.EventHandler(this.txtMainInfo_TextChanged);
            // 
            // treeV
            // 
            this.treeV.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeV.Font = new System.Drawing.Font("宋体", 13F);
            this.treeV.HotTracking = true;
            this.treeV.ImeMode = System.Windows.Forms.ImeMode.On;
            this.treeV.Indent = 20;
            this.treeV.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.treeV.Location = new System.Drawing.Point(0, 25);
            this.treeV.Name = "treeV";
            treeNode1.Name = "MachineGroup1";
            treeNode1.Text = "第一组";
            treeNode2.Name = "MachineGroup2";
            treeNode2.Text = "第二组";
            treeNode3.Name = "MachineGroup3";
            treeNode3.Text = "第三组";
            treeNode4.Name = "MachineGroup4";
            treeNode4.Text = "第四组";
            treeNode5.Name = "MachineGroup5";
            treeNode5.Text = "第五组";
            treeNode6.Name = "MachineGroup6";
            treeNode6.Text = "第六组";
            treeNode7.Name = "MachineGroup7";
            treeNode7.Text = "第七组";
            treeNode8.Name = "MachineGroup8";
            treeNode8.Text = "第八组";
            treeNode9.Name = "Machine";
            treeNode9.Text = "机械手";
            treeNode10.Name = "fjBigGroup1";
            treeNode10.Text = "第一组";
            treeNode11.Name = "fjBigGroup2";
            treeNode11.Text = "第二组";
            treeNode12.Name = "fjBigGroup3";
            treeNode12.Text = "第三组";
            treeNode13.Name = "fjBigGroup4";
            treeNode13.Text = "第四组";
            treeNode14.Name = "fjBigGroup5";
            treeNode14.Text = "第五组";
            treeNode15.Name = "fjBigGroup6";
            treeNode15.Text = "第六组";
            treeNode16.Name = "fjBigGroup7";
            treeNode16.Text = "第七组";
            treeNode17.Name = "fjBigGroup8";
            treeNode17.Text = "第八组";
            treeNode18.Name = "readyTASK";
            treeNode18.Text = "预分拣";
            treeNode19.Name = "UinonTask";
            treeNode19.Text = "合流";
            this.treeV.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode18,
            treeNode19});
            this.treeV.ShowNodeToolTips = true;
            this.treeV.Size = new System.Drawing.Size(174, 697);
            this.treeV.TabIndex = 6;
            this.treeV.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeV_AfterSelect);
            // 
            // btnLeft
            // 
            this.btnLeft.Location = new System.Drawing.Point(166, 264);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(21, 23);
            this.btnLeft.TabIndex = 8;
            this.btnLeft.Text = "<-";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            this.btnLeft.MouseLeave += new System.EventHandler(this.btnLeft_MouseLeave);
            this.btnLeft.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnLeft_MouseMove);
            // 
            // Fm_Mian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 746);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.treeV);
            this.Controls.Add(this.txtMainInfo);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "Fm_Mian";
            this.Text = "任务还原";
            this.TransparencyKey = System.Drawing.Color.White;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Fm_Mian_FormClosing);
            this.Load += new System.EventHandler(this.Fm_Mian_Load);
            this.SizeChanged += new System.EventHandler(this.Fm_Mian_SizeChanged);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 系统ToolStripMenuItem;
        private System.Windows.Forms.TreeView treeV;
        private System.Windows.Forms.ToolStripMenuItem 退出EToolStripMenuItem;
        private System.Windows.Forms.Button btnLeft;
        public System.Windows.Forms.TextBox txtMainInfo;
        private System.Windows.Forms.ToolStripMenuItem 查询任务sToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 机械手MToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 预分拣YToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 合流UToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 异形烟NToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 补货任务ToolStripMenuItem;

    }
}

