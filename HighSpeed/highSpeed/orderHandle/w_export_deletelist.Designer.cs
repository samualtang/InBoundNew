namespace highSpeed.orderHandle
{
    partial class w_export_deletelist
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_paicheng = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.orderdata1 = new System.Windows.Forms.DataGridView();
            this.checkbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regioncode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lab_showinfo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_package = new System.Windows.Forms.ComboBox();
            this.btn_all = new System.Windows.Forms.Button();
            this.btn_search = new System.Windows.Forms.Button();
            this.txt_codestr = new System.Windows.Forms.TextBox();
            this.btn_schedule = new System.Windows.Forms.Button();
            this.tabPage_export = new System.Windows.Forms.TabPage();
            this.orderdata2 = new System.Windows.Forms.DataGridView();
            this.batchcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cuscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_ReDateBing = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_export = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage_paicheng.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderdata1)).BeginInit();
            this.panel4.SuspendLayout();
            this.tabPage_export.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderdata2)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage_paicheng);
            this.tabControl1.Controls.Add(this.tabPage_export);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1213, 458);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage_paicheng
            // 
            this.tabPage_paicheng.AutoScroll = true;
            this.tabPage_paicheng.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_paicheng.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage_paicheng.Controls.Add(this.panel3);
            this.tabPage_paicheng.Controls.Add(this.orderdata1);
            this.tabPage_paicheng.Controls.Add(this.panel4);
            this.tabPage_paicheng.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabPage_paicheng.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage_paicheng.Location = new System.Drawing.Point(4, 25);
            this.tabPage_paicheng.Name = "tabPage_paicheng";
            this.tabPage_paicheng.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_paicheng.Size = new System.Drawing.Size(1205, 429);
            this.tabPage_paicheng.TabIndex = 0;
            this.tabPage_paicheng.Text = "     数 据 排 程     ";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.progressBar2);
            this.panel3.Location = new System.Drawing.Point(14, 81);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(826, 89);
            this.panel3.TabIndex = 6;
            this.panel3.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "正在读取数据...";
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(33, 42);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(741, 23);
            this.progressBar2.TabIndex = 0;
            this.progressBar2.Visible = false;
            // 
            // orderdata1
            // 
            this.orderdata1.AllowUserToAddRows = false;
            this.orderdata1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.orderdata1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkbox,
            this.Column1,
            this.regioncode,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.orderdata1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderdata1.Location = new System.Drawing.Point(3, 50);
            this.orderdata1.MultiSelect = false;
            this.orderdata1.Name = "orderdata1";
            this.orderdata1.RowTemplate.Height = 23;
            this.orderdata1.Size = new System.Drawing.Size(1197, 374);
            this.orderdata1.TabIndex = 5;
            this.orderdata1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.orderdata1_CellContentClick);
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
            // Column1
            // 
            this.Column1.DataPropertyName = "rownum";
            this.Column1.HeaderText = "序号";
            this.Column1.Name = "Column1";
            this.Column1.Width = 80;
            // 
            // regioncode
            // 
            this.regioncode.DataPropertyName = "regioncode";
            this.regioncode.HeaderText = "车组CODE";
            this.regioncode.Name = "regioncode";
            this.regioncode.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "cuscount";
            this.dataGridViewTextBoxColumn1.HeaderText = "订货户数";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "qty";
            this.dataGridViewTextBoxColumn2.HeaderText = "订货数量";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lab_showinfo);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.cmb_package);
            this.panel4.Controls.Add(this.btn_all);
            this.panel4.Controls.Add(this.btn_search);
            this.panel4.Controls.Add(this.txt_codestr);
            this.panel4.Controls.Add(this.btn_schedule);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1197, 47);
            this.panel4.TabIndex = 4;
            // 
            // lab_showinfo
            // 
            this.lab_showinfo.AutoSize = true;
            this.lab_showinfo.Location = new System.Drawing.Point(509, 19);
            this.lab_showinfo.Name = "lab_showinfo";
            this.lab_showinfo.Size = new System.Drawing.Size(509, 12);
            this.lab_showinfo.TabIndex = 19;
            this.lab_showinfo.Text = "勾选要排程的订单数据，点击“排程”按钮，进行排程操作。排程的先后顺序由勾选顺序决定。";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(236, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "包装机：";
            // 
            // cmb_package
            // 
            this.cmb_package.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_package.FormattingEnabled = true;
            this.cmb_package.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cmb_package.Location = new System.Drawing.Point(291, 15);
            this.cmb_package.Name = "cmb_package";
            this.cmb_package.Size = new System.Drawing.Size(121, 20);
            this.cmb_package.TabIndex = 15;
            // 
            // btn_all
            // 
            this.btn_all.Location = new System.Drawing.Point(137, 13);
            this.btn_all.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_all.Name = "btn_all";
            this.btn_all.Size = new System.Drawing.Size(75, 22);
            this.btn_all.TabIndex = 14;
            this.btn_all.Text = "全选";
            this.btn_all.UseVisualStyleBackColor = true;
            this.btn_all.Click += new System.EventHandler(this.btn_all_Click);
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(44, 12);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 23);
            this.btn_search.TabIndex = 10;
            this.btn_search.Text = "查询";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // txt_codestr
            // 
            this.txt_codestr.Location = new System.Drawing.Point(1055, 13);
            this.txt_codestr.Name = "txt_codestr";
            this.txt_codestr.ReadOnly = true;
            this.txt_codestr.Size = new System.Drawing.Size(60, 21);
            this.txt_codestr.TabIndex = 9;
            this.txt_codestr.Visible = false;
            // 
            // btn_schedule
            // 
            this.btn_schedule.Location = new System.Drawing.Point(428, 13);
            this.btn_schedule.Name = "btn_schedule";
            this.btn_schedule.Size = new System.Drawing.Size(75, 23);
            this.btn_schedule.TabIndex = 5;
            this.btn_schedule.Text = "排程";
            this.btn_schedule.UseVisualStyleBackColor = true;
            this.btn_schedule.Click += new System.EventHandler(this.btn_schedule_Click);
            // 
            // tabPage_export
            // 
            this.tabPage_export.AutoScroll = true;
            this.tabPage_export.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage_export.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage_export.Controls.Add(this.orderdata2);
            this.tabPage_export.Controls.Add(this.panel2);
            this.tabPage_export.Controls.Add(this.panel1);
            this.tabPage_export.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabPage_export.Location = new System.Drawing.Point(4, 25);
            this.tabPage_export.Name = "tabPage_export";
            this.tabPage_export.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_export.Size = new System.Drawing.Size(1205, 429);
            this.tabPage_export.TabIndex = 1;
            this.tabPage_export.Text = "     任 务 发 送     ";
            // 
            // orderdata2
            // 
            this.orderdata2.AllowUserToAddRows = false;
            this.orderdata2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.orderdata2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.batchcode,
            this.cuscount,
            this.qty});
            this.orderdata2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderdata2.Location = new System.Drawing.Point(3, 55);
            this.orderdata2.MultiSelect = false;
            this.orderdata2.Name = "orderdata2";
            this.orderdata2.RowTemplate.Height = 23;
            this.orderdata2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.orderdata2.Size = new System.Drawing.Size(1197, 369);
            this.orderdata2.TabIndex = 9;
            // 
            // batchcode
            // 
            this.batchcode.DataPropertyName = "batchcode";
            this.batchcode.HeaderText = "批次编号";
            this.batchcode.Name = "batchcode";
            this.batchcode.ReadOnly = true;
            this.batchcode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.batchcode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cuscount
            // 
            this.cuscount.DataPropertyName = "cuscount";
            this.cuscount.HeaderText = "订货户数";
            this.cuscount.Name = "cuscount";
            this.cuscount.ReadOnly = true;
            this.cuscount.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cuscount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // qty
            // 
            this.qty.DataPropertyName = "qty";
            this.qty.HeaderText = "订货数量";
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            this.qty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Location = new System.Drawing.Point(44, 179);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(663, 89);
            this.panel2.TabIndex = 10;
            this.panel2.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 15);
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
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_ReDateBing);
            this.panel1.Controls.Add(this.btn_close);
            this.panel1.Controls.Add(this.btn_export);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1197, 52);
            this.panel1.TabIndex = 8;
            // 
            // btn_ReDateBing
            // 
            this.btn_ReDateBing.Location = new System.Drawing.Point(12, 18);
            this.btn_ReDateBing.Name = "btn_ReDateBing";
            this.btn_ReDateBing.Size = new System.Drawing.Size(75, 23);
            this.btn_ReDateBing.TabIndex = 8;
            this.btn_ReDateBing.Text = "刷新";
            this.btn_ReDateBing.UseVisualStyleBackColor = true;
            this.btn_ReDateBing.Click += new System.EventHandler(this.btn_ReDateBing_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(1103, 18);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 7;
            this.btn_close.Text = "关闭";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_export
            // 
            this.btn_export.Location = new System.Drawing.Point(583, 18);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(75, 23);
            this.btn_export.TabIndex = 6;
            this.btn_export.Text = "发送";
            this.btn_export.UseVisualStyleBackColor = true;
            this.btn_export.Click += new System.EventHandler(this.btn_export_Click);
            // 
            // w_export_deletelist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1213, 458);
            this.Controls.Add(this.tabControl1);
            this.Name = "w_export_deletelist";
            this.Text = "批量退货数据";
            this.Click += new System.EventHandler(this.win_export_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage_paicheng.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderdata1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tabPage_export.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.orderdata2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_paicheng;
        private System.Windows.Forms.TabPage tabPage_export;
        private System.Windows.Forms.DataGridView orderdata2;
        private System.Windows.Forms.DataGridViewTextBoxColumn batchcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn cuscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_ReDateBing;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_export;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.DataGridView orderdata1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkbox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn regioncode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btn_all;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.TextBox txt_codestr;
        private System.Windows.Forms.Button btn_schedule;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_package;
        private System.Windows.Forms.Label lab_showinfo;

    }
}