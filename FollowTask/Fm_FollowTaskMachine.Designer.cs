namespace FollowTask
{
    partial class fm_Machine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fm_Machine));
            this.btnBelt = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listViewMchineBelt = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblFormText = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Machine8 = new System.Windows.Forms.Button();
            this.Machine4 = new System.Windows.Forms.Button();
            this.Machine11 = new System.Windows.Forms.Button();
            this.Machine7 = new System.Windows.Forms.Button();
            this.Machine10 = new System.Windows.Forms.Button();
            this.Machine6 = new System.Windows.Forms.Button();
            this.Machine9 = new System.Windows.Forms.Button();
            this.Machine3 = new System.Windows.Forms.Button();
            this.Machine5 = new System.Windows.Forms.Button();
            this.Machine2 = new System.Windows.Forms.Button();
            this.Machine1 = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.list_data = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBelt
            // 
            this.btnBelt.Location = new System.Drawing.Point(43, 179);
            this.btnBelt.Name = "btnBelt";
            this.btnBelt.Size = new System.Drawing.Size(958, 30);
            this.btnBelt.TabIndex = 41;
            this.btnBelt.Text = "皮                       带";
            this.btnBelt.UseVisualStyleBackColor = true;
            this.btnBelt.Click += new System.EventHandler(this.btnBelt_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listViewMchineBelt);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 13F);
            this.groupBox1.Location = new System.Drawing.Point(43, 220);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(618, 400);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "皮带信息";
            // 
            // listViewMchineBelt
            // 
            this.listViewMchineBelt.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listViewMchineBelt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewMchineBelt.GridLines = true;
            this.listViewMchineBelt.Location = new System.Drawing.Point(3, 23);
            this.listViewMchineBelt.Name = "listViewMchineBelt";
            this.listViewMchineBelt.Size = new System.Drawing.Size(612, 374);
            this.listViewMchineBelt.TabIndex = 0;
            this.listViewMchineBelt.UseCompatibleStateImageBehavior = false;
            this.listViewMchineBelt.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "订单号";
            this.columnHeader1.Width = 110;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "主皮带号";
            this.columnHeader2.Width = 81;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "香烟名称";
            this.columnHeader3.Width = 129;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "香烟编号";
            this.columnHeader4.Width = 157;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "排序号";
            this.columnHeader5.Width = 66;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "合流号";
            this.columnHeader6.Width = 65;
            // 
            // lblFormText
            // 
            this.lblFormText.AutoSize = true;
            this.lblFormText.Font = new System.Drawing.Font("宋体", 14F);
            this.lblFormText.Location = new System.Drawing.Point(432, 21);
            this.lblFormText.Name = "lblFormText";
            this.lblFormText.Size = new System.Drawing.Size(69, 19);
            this.lblFormText.TabIndex = 47;
            this.lblFormText.Text = "label1";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Machine8);
            this.groupBox4.Controls.Add(this.Machine4);
            this.groupBox4.Controls.Add(this.Machine11);
            this.groupBox4.Controls.Add(this.Machine7);
            this.groupBox4.Controls.Add(this.Machine10);
            this.groupBox4.Controls.Add(this.Machine6);
            this.groupBox4.Controls.Add(this.Machine9);
            this.groupBox4.Controls.Add(this.Machine3);
            this.groupBox4.Controls.Add(this.Machine5);
            this.groupBox4.Controls.Add(this.Machine2);
            this.groupBox4.Controls.Add(this.Machine1);
            this.groupBox4.Font = new System.Drawing.Font("宋体", 13F);
            this.groupBox4.Location = new System.Drawing.Point(43, 57);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(958, 100);
            this.groupBox4.TabIndex = 48;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "机 械 手 :";
            // 
            // Machine8
            // 
            this.Machine8.Location = new System.Drawing.Point(660, 44);
            this.Machine8.Name = "Machine8";
            this.Machine8.Size = new System.Drawing.Size(41, 50);
            this.Machine8.TabIndex = 47;
            this.Machine8.Text = "8号";
            this.Machine8.UseVisualStyleBackColor = true;
            this.Machine8.Click += new System.EventHandler(this.Machine1_Click);
            this.Machine8.MouseEnter += new System.EventHandler(this.Machine1_MouseEnter);
            // 
            // Machine4
            // 
            this.Machine4.Location = new System.Drawing.Point(287, 44);
            this.Machine4.Name = "Machine4";
            this.Machine4.Size = new System.Drawing.Size(41, 50);
            this.Machine4.TabIndex = 49;
            this.Machine4.Text = "4号";
            this.Machine4.UseVisualStyleBackColor = true;
            this.Machine4.Click += new System.EventHandler(this.Machine1_Click);
            this.Machine4.MouseEnter += new System.EventHandler(this.Machine1_MouseEnter);
            // 
            // Machine11
            // 
            this.Machine11.Location = new System.Drawing.Point(911, 44);
            this.Machine11.Name = "Machine11";
            this.Machine11.Size = new System.Drawing.Size(41, 50);
            this.Machine11.TabIndex = 51;
            this.Machine11.Text = "11号";
            this.Machine11.UseVisualStyleBackColor = true;
            this.Machine11.Click += new System.EventHandler(this.Machine1_Click);
            this.Machine11.MouseEnter += new System.EventHandler(this.Machine1_MouseEnter);
            // 
            // Machine7
            // 
            this.Machine7.Location = new System.Drawing.Point(565, 44);
            this.Machine7.Name = "Machine7";
            this.Machine7.Size = new System.Drawing.Size(41, 50);
            this.Machine7.TabIndex = 50;
            this.Machine7.Text = "7号";
            this.Machine7.UseVisualStyleBackColor = true;
            this.Machine7.Click += new System.EventHandler(this.Machine1_Click);
            this.Machine7.MouseEnter += new System.EventHandler(this.Machine1_MouseEnter);
            // 
            // Machine10
            // 
            this.Machine10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Machine10.Location = new System.Drawing.Point(824, 44);
            this.Machine10.Name = "Machine10";
            this.Machine10.Size = new System.Drawing.Size(41, 50);
            this.Machine10.TabIndex = 48;
            this.Machine10.Text = "10号";
            this.Machine10.UseVisualStyleBackColor = true;
            this.Machine10.Click += new System.EventHandler(this.Machine1_Click);
            this.Machine10.MouseEnter += new System.EventHandler(this.Machine1_MouseEnter);
            // 
            // Machine6
            // 
            this.Machine6.Location = new System.Drawing.Point(468, 44);
            this.Machine6.Name = "Machine6";
            this.Machine6.Size = new System.Drawing.Size(41, 50);
            this.Machine6.TabIndex = 42;
            this.Machine6.Text = "6号";
            this.Machine6.UseVisualStyleBackColor = true;
            this.Machine6.Click += new System.EventHandler(this.Machine1_Click);
            this.Machine6.MouseEnter += new System.EventHandler(this.Machine1_MouseEnter);
            // 
            // Machine9
            // 
            this.Machine9.Location = new System.Drawing.Point(747, 44);
            this.Machine9.Name = "Machine9";
            this.Machine9.Size = new System.Drawing.Size(41, 50);
            this.Machine9.TabIndex = 41;
            this.Machine9.Text = "9号";
            this.Machine9.UseVisualStyleBackColor = true;
            this.Machine9.Click += new System.EventHandler(this.Machine1_Click);
            this.Machine9.MouseEnter += new System.EventHandler(this.Machine1_MouseEnter);
            // 
            // Machine3
            // 
            this.Machine3.Location = new System.Drawing.Point(185, 44);
            this.Machine3.Name = "Machine3";
            this.Machine3.Size = new System.Drawing.Size(41, 50);
            this.Machine3.TabIndex = 43;
            this.Machine3.Text = "3号";
            this.Machine3.UseVisualStyleBackColor = true;
            this.Machine3.Click += new System.EventHandler(this.Machine1_Click);
            this.Machine3.MouseEnter += new System.EventHandler(this.Machine1_MouseEnter);
            // 
            // Machine5
            // 
            this.Machine5.Location = new System.Drawing.Point(375, 44);
            this.Machine5.Name = "Machine5";
            this.Machine5.Size = new System.Drawing.Size(41, 50);
            this.Machine5.TabIndex = 45;
            this.Machine5.Text = "5号";
            this.Machine5.UseVisualStyleBackColor = true;
            this.Machine5.Click += new System.EventHandler(this.Machine1_Click);
            this.Machine5.MouseEnter += new System.EventHandler(this.Machine1_MouseEnter);
            // 
            // Machine2
            // 
            this.Machine2.Location = new System.Drawing.Point(96, 44);
            this.Machine2.Name = "Machine2";
            this.Machine2.Size = new System.Drawing.Size(41, 50);
            this.Machine2.TabIndex = 44;
            this.Machine2.Text = "2号";
            this.Machine2.UseVisualStyleBackColor = true;
            this.Machine2.Click += new System.EventHandler(this.Machine1_Click);
            this.Machine2.MouseEnter += new System.EventHandler(this.Machine1_MouseEnter);
            // 
            // Machine1
            // 
            this.Machine1.Location = new System.Drawing.Point(6, 44);
            this.Machine1.Name = "Machine1";
            this.Machine1.Size = new System.Drawing.Size(41, 50);
            this.Machine1.TabIndex = 46;
            this.Machine1.Text = "1号";
            this.Machine1.UseVisualStyleBackColor = true;
            this.Machine1.Click += new System.EventHandler(this.Machine1_Click);
            this.Machine1.MouseEnter += new System.EventHandler(this.Machine1_MouseEnter);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(926, 40);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 49;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // list_data
            // 
            this.list_data.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.list_data.FormattingEnabled = true;
            this.list_data.ItemHeight = 15;
            this.list_data.Location = new System.Drawing.Point(3, 18);
            this.list_data.Name = "list_data";
            this.list_data.Size = new System.Drawing.Size(318, 379);
            this.list_data.TabIndex = 50;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.list_data);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 11F);
            this.groupBox2.Location = new System.Drawing.Point(677, 220);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(324, 400);
            this.groupBox2.TabIndex = 51;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "程序状态";
            // 
            // fm_Machine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1098, 652);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.lblFormText);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnBelt);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fm_Machine";
            this.Text = "机械手";
            this.Load += new System.EventHandler(this.fm_Machine_Load);
            this.SizeChanged += new System.EventHandler(this.fm_Machine_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBelt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblFormText;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button Machine8;
        private System.Windows.Forms.Button Machine4;
        private System.Windows.Forms.Button Machine11;
        private System.Windows.Forms.Button Machine7;
        private System.Windows.Forms.Button Machine10;
        private System.Windows.Forms.Button Machine6;
        private System.Windows.Forms.Button Machine9;
        private System.Windows.Forms.Button Machine3;
        private System.Windows.Forms.Button Machine5;
        private System.Windows.Forms.Button Machine2;
        private System.Windows.Forms.Button Machine1;
        private System.Windows.Forms.ListView listViewMchineBelt;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ListBox list_data;
        private System.Windows.Forms.GroupBox groupBox2;



    }
}