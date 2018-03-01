namespace MainUI
{
    partial class MainFrm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.入库管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.入库单入库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.托盘入库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.成品入库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移库入库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.入库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.人工入库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.出库管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.出库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自动补货出库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.统计报表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.库存统计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.储位明细ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.出入库查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.异常查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.仓库管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.巷道管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设备管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.可疑储位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分拣管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分拣预补ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.人工拆垛ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.人工拆垛ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.btnReport = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnChaiduo = new System.Windows.Forms.Button();
            this.btnFenjian = new System.Windows.Forms.Button();
            this.btnStorage = new System.Windows.Forms.Button();
            this.btnOutBound = new System.Windows.Forms.Button();
            this.btnInBound = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.imageListZip = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.入库管理ToolStripMenuItem,
            this.出库管理ToolStripMenuItem,
            this.统计报表ToolStripMenuItem,
            this.仓库管理ToolStripMenuItem,
            this.分拣管理ToolStripMenuItem,
            this.人工拆垛ToolStripMenuItem1});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1312, 25);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // 入库管理ToolStripMenuItem
            // 
            this.入库管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.入库单入库ToolStripMenuItem,
            this.托盘入库ToolStripMenuItem,
            this.成品入库ToolStripMenuItem,
            this.移库入库ToolStripMenuItem,
            this.入库ToolStripMenuItem,
            this.人工入库ToolStripMenuItem});
            this.入库管理ToolStripMenuItem.Name = "入库管理ToolStripMenuItem";
            this.入库管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.入库管理ToolStripMenuItem.Text = "入库管理";
            // 
            // 入库单入库ToolStripMenuItem
            // 
            this.入库单入库ToolStripMenuItem.Name = "入库单入库ToolStripMenuItem";
            this.入库单入库ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.入库单入库ToolStripMenuItem.Text = "入库单入库";
            this.入库单入库ToolStripMenuItem.Click += new System.EventHandler(this.入库单入库ToolStripMenuItem_Click);
            // 
            // 托盘入库ToolStripMenuItem
            // 
            this.托盘入库ToolStripMenuItem.Name = "托盘入库ToolStripMenuItem";
            this.托盘入库ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.托盘入库ToolStripMenuItem.Text = "托盘入库";
            this.托盘入库ToolStripMenuItem.Click += new System.EventHandler(this.托盘入库ToolStripMenuItem_Click);
            // 
            // 成品入库ToolStripMenuItem
            // 
            this.成品入库ToolStripMenuItem.Name = "成品入库ToolStripMenuItem";
            this.成品入库ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.成品入库ToolStripMenuItem.Text = "成品入库";
            this.成品入库ToolStripMenuItem.Click += new System.EventHandler(this.成品入库ToolStripMenuItem_Click);
            // 
            // 移库入库ToolStripMenuItem
            // 
            this.移库入库ToolStripMenuItem.Name = "移库入库ToolStripMenuItem";
            this.移库入库ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.移库入库ToolStripMenuItem.Text = "移库入库";
            this.移库入库ToolStripMenuItem.Click += new System.EventHandler(this.移库入库ToolStripMenuItem_Click);
            // 
            // 入库ToolStripMenuItem
            // 
            this.入库ToolStripMenuItem.Name = "入库ToolStripMenuItem";
            this.入库ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.入库ToolStripMenuItem.Text = "入库";
            // 
            // 人工入库ToolStripMenuItem
            // 
            this.人工入库ToolStripMenuItem.Name = "人工入库ToolStripMenuItem";
            this.人工入库ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.人工入库ToolStripMenuItem.Text = "人工入库";
            this.人工入库ToolStripMenuItem.Click += new System.EventHandler(this.人工入库ToolStripMenuItem_Click);
            // 
            // 出库管理ToolStripMenuItem
            // 
            this.出库管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.出库ToolStripMenuItem,
            this.自动补货出库ToolStripMenuItem});
            this.出库管理ToolStripMenuItem.Name = "出库管理ToolStripMenuItem";
            this.出库管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.出库管理ToolStripMenuItem.Text = "出库管理";
            // 
            // 出库ToolStripMenuItem
            // 
            this.出库ToolStripMenuItem.Name = "出库ToolStripMenuItem";
            this.出库ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.出库ToolStripMenuItem.Text = "出库";
            // 
            // 自动补货出库ToolStripMenuItem
            // 
            this.自动补货出库ToolStripMenuItem.Name = "自动补货出库ToolStripMenuItem";
            this.自动补货出库ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.自动补货出库ToolStripMenuItem.Text = "自动补货出库";
            this.自动补货出库ToolStripMenuItem.Click += new System.EventHandler(this.自动补货出库ToolStripMenuItem_Click);
            // 
            // 统计报表ToolStripMenuItem
            // 
            this.统计报表ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.库存统计ToolStripMenuItem,
            this.储位明细ToolStripMenuItem,
            this.出入库查询ToolStripMenuItem,
            this.异常查询ToolStripMenuItem});
            this.统计报表ToolStripMenuItem.Name = "统计报表ToolStripMenuItem";
            this.统计报表ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.统计报表ToolStripMenuItem.Text = "统计报表";
            // 
            // 库存统计ToolStripMenuItem
            // 
            this.库存统计ToolStripMenuItem.Name = "库存统计ToolStripMenuItem";
            this.库存统计ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.库存统计ToolStripMenuItem.Text = "库存统计";
            this.库存统计ToolStripMenuItem.Click += new System.EventHandler(this.库存统计ToolStripMenuItem_Click);
            // 
            // 储位明细ToolStripMenuItem
            // 
            this.储位明细ToolStripMenuItem.Name = "储位明细ToolStripMenuItem";
            this.储位明细ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.储位明细ToolStripMenuItem.Text = "储位明细";
            this.储位明细ToolStripMenuItem.Click += new System.EventHandler(this.储位明细ToolStripMenuItem_Click);
            // 
            // 出入库查询ToolStripMenuItem
            // 
            this.出入库查询ToolStripMenuItem.Name = "出入库查询ToolStripMenuItem";
            this.出入库查询ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.出入库查询ToolStripMenuItem.Text = "出入库查询";
            this.出入库查询ToolStripMenuItem.Click += new System.EventHandler(this.出入库查询ToolStripMenuItem_Click_1);
            // 
            // 异常查询ToolStripMenuItem
            // 
            this.异常查询ToolStripMenuItem.Name = "异常查询ToolStripMenuItem";
            this.异常查询ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.异常查询ToolStripMenuItem.Text = "异常查询";
            this.异常查询ToolStripMenuItem.Click += new System.EventHandler(this.异常查询ToolStripMenuItem_Click_1);
            // 
            // 仓库管理ToolStripMenuItem
            // 
            this.仓库管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.巷道管理ToolStripMenuItem,
            this.设备管理ToolStripMenuItem,
            this.可疑储位ToolStripMenuItem});
            this.仓库管理ToolStripMenuItem.Name = "仓库管理ToolStripMenuItem";
            this.仓库管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.仓库管理ToolStripMenuItem.Text = "仓库管理";
            // 
            // 巷道管理ToolStripMenuItem
            // 
            this.巷道管理ToolStripMenuItem.Name = "巷道管理ToolStripMenuItem";
            this.巷道管理ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.巷道管理ToolStripMenuItem.Text = "巷道管理";
            this.巷道管理ToolStripMenuItem.Click += new System.EventHandler(this.巷道管理ToolStripMenuItem_Click);
            // 
            // 设备管理ToolStripMenuItem
            // 
            this.设备管理ToolStripMenuItem.Name = "设备管理ToolStripMenuItem";
            this.设备管理ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.设备管理ToolStripMenuItem.Text = "设备管理";
            this.设备管理ToolStripMenuItem.Click += new System.EventHandler(this.设备管理ToolStripMenuItem_Click);
            // 
            // 可疑储位ToolStripMenuItem
            // 
            this.可疑储位ToolStripMenuItem.Name = "可疑储位ToolStripMenuItem";
            this.可疑储位ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.可疑储位ToolStripMenuItem.Text = "可疑储位";
            this.可疑储位ToolStripMenuItem.Click += new System.EventHandler(this.可疑储位ToolStripMenuItem_Click);
            // 
            // 分拣管理ToolStripMenuItem
            // 
            this.分拣管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.分拣预补ToolStripMenuItem});
            this.分拣管理ToolStripMenuItem.Name = "分拣管理ToolStripMenuItem";
            this.分拣管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.分拣管理ToolStripMenuItem.Text = "分拣管理";
            // 
            // 分拣预补ToolStripMenuItem
            // 
            this.分拣预补ToolStripMenuItem.Name = "分拣预补ToolStripMenuItem";
            this.分拣预补ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.分拣预补ToolStripMenuItem.Text = "分拣预补";
            this.分拣预补ToolStripMenuItem.Click += new System.EventHandler(this.分拣预补ToolStripMenuItem_Click);
            // 
            // 人工拆垛ToolStripMenuItem1
            // 
            this.人工拆垛ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.人工拆垛ToolStripMenuItem2});
            this.人工拆垛ToolStripMenuItem1.Name = "人工拆垛ToolStripMenuItem1";
            this.人工拆垛ToolStripMenuItem1.Size = new System.Drawing.Size(68, 21);
            this.人工拆垛ToolStripMenuItem1.Text = "人工拆垛";
            // 
            // 人工拆垛ToolStripMenuItem2
            // 
            this.人工拆垛ToolStripMenuItem2.Name = "人工拆垛ToolStripMenuItem2";
            this.人工拆垛ToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.人工拆垛ToolStripMenuItem2.Text = "人工拆垛";
            this.人工拆垛ToolStripMenuItem2.Click += new System.EventHandler(this.人工拆垛ToolStripMenuItem2_Click);
            // 
            // listView1
            // 
            this.listView1.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.listView1.AllowColumnReorder = true;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listView1.Location = new System.Drawing.Point(0, 92);
            this.listView1.Margin = new System.Windows.Forms.Padding(5);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(81, 380);
            this.listView1.TabIndex = 0;
            this.listView1.TileSize = new System.Drawing.Size(100, 28);
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.Click += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.SystemColors.Control;
            this.btnReport.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReport.Location = new System.Drawing.Point(0, 60);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(81, 32);
            this.btnReport.TabIndex = 3;
            this.btnReport.Text = "统计报表";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.ButtonClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnChaiduo);
            this.panel1.Controls.Add(this.btnFenjian);
            this.panel1.Controls.Add(this.btnStorage);
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Controls.Add(this.btnReport);
            this.panel1.Controls.Add(this.btnOutBound);
            this.panel1.Controls.Add(this.btnInBound);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(85, 476);
            this.panel1.TabIndex = 0;
            // 
            // btnChaiduo
            // 
            this.btnChaiduo.BackColor = System.Drawing.SystemColors.Control;
            this.btnChaiduo.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnChaiduo.Location = new System.Drawing.Point(0, 151);
            this.btnChaiduo.Name = "btnChaiduo";
            this.btnChaiduo.Size = new System.Drawing.Size(81, 30);
            this.btnChaiduo.TabIndex = 10;
            this.btnChaiduo.Text = "人工拆垛";
            this.btnChaiduo.UseVisualStyleBackColor = false;
            this.btnChaiduo.Click += new System.EventHandler(this.ButtonClick);
            // 
            // btnFenjian
            // 
            this.btnFenjian.BackColor = System.Drawing.SystemColors.Control;
            this.btnFenjian.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFenjian.Location = new System.Drawing.Point(0, 121);
            this.btnFenjian.Name = "btnFenjian";
            this.btnFenjian.Size = new System.Drawing.Size(81, 30);
            this.btnFenjian.TabIndex = 9;
            this.btnFenjian.Text = "分拣管理";
            this.btnFenjian.UseVisualStyleBackColor = false;
            this.btnFenjian.Click += new System.EventHandler(this.ButtonClick);
            // 
            // btnStorage
            // 
            this.btnStorage.BackColor = System.Drawing.SystemColors.Control;
            this.btnStorage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnStorage.Location = new System.Drawing.Point(0, 92);
            this.btnStorage.Name = "btnStorage";
            this.btnStorage.Size = new System.Drawing.Size(81, 29);
            this.btnStorage.TabIndex = 7;
            this.btnStorage.Text = "仓库管理";
            this.btnStorage.UseVisualStyleBackColor = false;
            this.btnStorage.Click += new System.EventHandler(this.ButtonClick);
            // 
            // btnOutBound
            // 
            this.btnOutBound.BackColor = System.Drawing.SystemColors.Control;
            this.btnOutBound.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnOutBound.Location = new System.Drawing.Point(0, 30);
            this.btnOutBound.Name = "btnOutBound";
            this.btnOutBound.Size = new System.Drawing.Size(81, 30);
            this.btnOutBound.TabIndex = 2;
            this.btnOutBound.Text = "出库管理";
            this.btnOutBound.UseVisualStyleBackColor = false;
            this.btnOutBound.Click += new System.EventHandler(this.ButtonClick);
            // 
            // btnInBound
            // 
            this.btnInBound.BackColor = System.Drawing.SystemColors.Control;
            this.btnInBound.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnInBound.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnInBound.Location = new System.Drawing.Point(0, 0);
            this.btnInBound.Name = "btnInBound";
            this.btnInBound.Size = new System.Drawing.Size(81, 30);
            this.btnInBound.TabIndex = 1;
            this.btnInBound.Text = "入库管理";
            this.btnInBound.UseVisualStyleBackColor = false;
            this.btnInBound.Click += new System.EventHandler(this.ButtonClick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(85, 25);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 20;
            this.dataGridView1.Size = new System.Drawing.Size(1227, 476);
            this.dataGridView1.TabIndex = 1;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(85, 25);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 476);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // imageListZip
            // 
            this.imageListZip.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListZip.ImageStream")));
            this.imageListZip.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListZip.Images.SetKeyName(0, "arr_icon.gif");
            this.imageListZip.Images.SetKeyName(1, "dian2.gif");
            this.imageListZip.Images.SetKeyName(2, "bat02_02.gif");
            this.imageListZip.Images.SetKeyName(3, "bat01_03.gif");
            this.imageListZip.Images.SetKeyName(4, "bat01_02.gif");
            this.imageListZip.Images.SetKeyName(5, "bat02_04.gif");
            this.imageListZip.Images.SetKeyName(6, "bg04.jpg");
            // 
            // MainFrm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(1312, 501);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainFrm";
            this.Text = "长株潭仓库管理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem 入库管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 出库管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 统计报表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 仓库管理ToolStripMenuItem;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOutBound;
        private System.Windows.Forms.Button btnInBound;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ImageList imageListZip;
        private System.Windows.Forms.Button btnStorage;
        private System.Windows.Forms.Button btnFenjian;
        private System.Windows.Forms.ToolStripMenuItem 分拣管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 入库单入库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 托盘入库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 成品入库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移库入库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 库存统计ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 巷道管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设备管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 可疑储位ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 分拣预补ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 入库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 出库ToolStripMenuItem;
        private System.Windows.Forms.Button btnChaiduo;
        private System.Windows.Forms.ToolStripMenuItem 人工拆垛ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 人工拆垛ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem 自动补货出库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 储位明细ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 人工入库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 出入库查询ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 异常查询ToolStripMenuItem;



    }
}