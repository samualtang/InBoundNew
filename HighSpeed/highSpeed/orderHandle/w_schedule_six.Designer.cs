namespace highSpeed.orderHandle
{
    partial class w_schedule_six
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
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lab_showinfo = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dgc_yhgc = new System.Windows.Forms.DataGridView();
            this.btn_refresh_yhgc = new System.Windows.Forms.Button();
            this.SendTask_yhgc = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_all = new System.Windows.Forms.Button();
            this.txt_codestr = new System.Windows.Forms.TextBox();
            this.dgv_sgx = new System.Windows.Forms.DataGridView();
            this.checkbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btn_refresh_sgx = new System.Windows.Forms.Button();
            this.btn_schedule = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgc_yhgc)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_sgx)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage3
            // 
            this.tabPage3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage3.Controls.Add(this.lab_showinfo);
            this.tabPage3.Controls.Add(this.panel2);
            this.tabPage3.Controls.Add(this.dgc_yhgc);
            this.tabPage3.Controls.Add(this.btn_refresh_yhgc);
            this.tabPage3.Controls.Add(this.SendTask_yhgc);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(758, 395);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "六三六任务导出";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lab_showinfo
            // 
            this.lab_showinfo.AutoSize = true;
            this.lab_showinfo.Location = new System.Drawing.Point(87, 20);
            this.lab_showinfo.Name = "lab_showinfo";
            this.lab_showinfo.Size = new System.Drawing.Size(533, 12);
            this.lab_showinfo.TabIndex = 6;
            this.lab_showinfo.Text = "选择批次订单数据，点击“发送一号工程任务”，可以将批次订单信息导出并自动发送到一号工程。";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Location = new System.Drawing.Point(73, 196);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(663, 89);
            this.panel2.TabIndex = 7;
            this.panel2.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "正在读取数据...";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(33, 42);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(597, 23);
            this.progressBar1.TabIndex = 0;
            this.progressBar1.Visible = false;
            // 
            // dgc_yhgc
            // 
            this.dgc_yhgc.AllowUserToAddRows = false;
            this.dgc_yhgc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgc_yhgc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgc_yhgc.Location = new System.Drawing.Point(0, 51);
            this.dgc_yhgc.MultiSelect = false;
            this.dgc_yhgc.Name = "dgc_yhgc";
            this.dgc_yhgc.RowTemplate.Height = 23;
            this.dgc_yhgc.Size = new System.Drawing.Size(750, 338);
            this.dgc_yhgc.TabIndex = 5;
            // 
            // btn_refresh_yhgc
            // 
            this.btn_refresh_yhgc.Location = new System.Drawing.Point(1, 15);
            this.btn_refresh_yhgc.Name = "btn_refresh_yhgc";
            this.btn_refresh_yhgc.Size = new System.Drawing.Size(75, 23);
            this.btn_refresh_yhgc.TabIndex = 4;
            this.btn_refresh_yhgc.Text = "刷新";
            this.btn_refresh_yhgc.UseVisualStyleBackColor = true;
            this.btn_refresh_yhgc.Click += new System.EventHandler(this.btn_refresh_yhgc_Click);
            // 
            // SendTask_yhgc
            // 
            this.SendTask_yhgc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SendTask_yhgc.Location = new System.Drawing.Point(626, 15);
            this.SendTask_yhgc.Name = "SendTask_yhgc";
            this.SendTask_yhgc.Size = new System.Drawing.Size(122, 23);
            this.SendTask_yhgc.TabIndex = 3;
            this.SendTask_yhgc.Text = "发送一号工程任务";
            this.SendTask_yhgc.UseVisualStyleBackColor = true;
            this.SendTask_yhgc.Click += new System.EventHandler(this.SendTask_yhgc_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage1.Controls.Add(this.btn_all);
            this.tabPage1.Controls.Add(this.txt_codestr);
            this.tabPage1.Controls.Add(this.dgv_sgx);
            this.tabPage1.Controls.Add(this.btn_refresh_sgx);
            this.tabPage1.Controls.Add(this.btn_schedule);
            this.tabPage1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(758, 395);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "排程手工分拣线";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_all
            // 
            this.btn_all.Location = new System.Drawing.Point(98, 15);
            this.btn_all.Name = "btn_all";
            this.btn_all.Size = new System.Drawing.Size(75, 23);
            this.btn_all.TabIndex = 11;
            this.btn_all.Text = "全选";
            this.btn_all.UseVisualStyleBackColor = true;
            this.btn_all.Click += new System.EventHandler(this.btn_all_Click);
            // 
            // txt_codestr
            // 
            this.txt_codestr.Location = new System.Drawing.Point(430, 15);
            this.txt_codestr.Name = "txt_codestr";
            this.txt_codestr.Size = new System.Drawing.Size(188, 21);
            this.txt_codestr.TabIndex = 10;
            this.txt_codestr.Visible = false;
            // 
            // dgv_sgx
            // 
            this.dgv_sgx.AllowUserToAddRows = false;
            this.dgv_sgx.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_sgx.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_sgx.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkbox});
            this.dgv_sgx.Location = new System.Drawing.Point(0, 50);
            this.dgv_sgx.MultiSelect = false;
            this.dgv_sgx.Name = "dgv_sgx";
            this.dgv_sgx.RowTemplate.Height = 23;
            this.dgv_sgx.Size = new System.Drawing.Size(750, 338);
            this.dgv_sgx.TabIndex = 3;
            this.dgv_sgx.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_sgx_CellContentClick);
            // 
            // checkbox
            // 
            this.checkbox.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.checkbox.FalseValue = "false";
            this.checkbox.HeaderText = "选择";
            this.checkbox.Name = "checkbox";
            this.checkbox.TrueValue = "true";
            this.checkbox.Width = 35;
            // 
            // btn_refresh_sgx
            // 
            this.btn_refresh_sgx.Location = new System.Drawing.Point(1, 15);
            this.btn_refresh_sgx.Name = "btn_refresh_sgx";
            this.btn_refresh_sgx.Size = new System.Drawing.Size(75, 23);
            this.btn_refresh_sgx.TabIndex = 2;
            this.btn_refresh_sgx.Text = "刷新";
            this.btn_refresh_sgx.UseVisualStyleBackColor = true;
            this.btn_refresh_sgx.Click += new System.EventHandler(this.btn_refresh_sgx_Click);
            // 
            // btn_schedule
            // 
            this.btn_schedule.Location = new System.Drawing.Point(673, 15);
            this.btn_schedule.Name = "btn_schedule";
            this.btn_schedule.Size = new System.Drawing.Size(75, 23);
            this.btn_schedule.TabIndex = 1;
            this.btn_schedule.Text = "排程";
            this.btn_schedule.UseVisualStyleBackColor = true;
            this.btn_schedule.Click += new System.EventHandler(this.btn_schedule_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(-1, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(766, 424);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            // 
            // w_schedule_six
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 425);
            this.Controls.Add(this.tabControl1);
            this.Name = "w_schedule_six";
            this.Text = "六三六订单";
            this.Load += new System.EventHandler(this.w_schedule_six_Load);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgc_yhgc)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_sgx)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label lab_showinfo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridView dgc_yhgc;
        private System.Windows.Forms.Button btn_refresh_yhgc;
        private System.Windows.Forms.Button SendTask_yhgc;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txt_codestr;
        private System.Windows.Forms.DataGridView dgv_sgx;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkbox;
        private System.Windows.Forms.Button btn_refresh_sgx;
        private System.Windows.Forms.Button btn_schedule;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btn_all;


    }
}