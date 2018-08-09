namespace FollowTask.ErrorStart
{
    partial class ErrorStart_Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("开机自检");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("入库区");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("立库区");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("出库区");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("备烟区");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("预分拣区");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("异型烟区");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("合流区");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("包装机区");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("区域查看", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9});
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Size = new System.Drawing.Size(631, 395);
            this.splitContainer1.SplitterDistance = 152;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.Font = new System.Drawing.Font("宋体", 14F);
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Checked = true;
            treeNode1.Name = "SortForm";
            treeNode1.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            treeNode1.Text = "开机自检";
            treeNode2.Name = "节点2";
            treeNode2.Text = "入库区";
            treeNode3.Name = "节点3";
            treeNode3.Text = "立库区";
            treeNode4.Name = "节点4";
            treeNode4.Text = "出库区";
            treeNode5.Name = "节点5";
            treeNode5.Text = "备烟区";
            treeNode6.Name = "节点6";
            treeNode6.Text = "预分拣区";
            treeNode7.Name = "节点10";
            treeNode7.Text = "异型烟区";
            treeNode8.Name = "节点9";
            treeNode8.Text = "合流区";
            treeNode9.Name = "节点11";
            treeNode9.Text = "包装机区";
            treeNode10.Name = "节点1";
            treeNode10.NodeFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            treeNode10.Text = "区域查看";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode10});
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(151, 398);
            this.treeView1.TabIndex = 1;
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // ErrorStart_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 395);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ErrorStart_Main";
            this.Text = "ErrorStart_Main";
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;

    }
}