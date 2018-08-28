namespace highSpeed.orderHandle
{
    partial class win_order_Union
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.cmb_line = new System.Windows.Forms.ComboBox();
            this.btn_recieve = new System.Windows.Forms.Button();
            this.btnVli = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCigerType = new System.Windows.Forms.ComboBox();
            this.btn_all = new System.Windows.Forms.Button();
            this.txt_codestr = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.lab_showinfo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.datePick = new System.Windows.Forms.DateTimePicker();
            this.orderdata = new System.Windows.Forms.DataGridView();
            this.checkbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dpid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.count_hs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dgvUnionOrderINfo = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderdata)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnionOrderINfo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.cmb_line);
            this.panel1.Controls.Add(this.btn_recieve);
            this.panel1.Controls.Add(this.btnVli);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cmbCigerType);
            this.panel1.Controls.Add(this.btn_all);
            this.panel1.Controls.Add(this.txt_codestr);
            this.panel1.Controls.Add(this.btn_search);
            this.panel1.Controls.Add(this.lab_showinfo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.datePick);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1190, 60);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(779, 15);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 22);
            this.button1.TabIndex = 16;
            this.button1.Text = "打  印";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cmb_line
            // 
            this.cmb_line.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_line.FormattingEnabled = true;
            this.cmb_line.Items.AddRange(new object[] {
            "常规烟",
            "异型烟 1线",
            "异型烟 2线"});
            this.cmb_line.Location = new System.Drawing.Point(542, 14);
            this.cmb_line.Name = "cmb_line";
            this.cmb_line.Size = new System.Drawing.Size(109, 20);
            this.cmb_line.TabIndex = 15;
            // 
            // btn_recieve
            // 
            this.btn_recieve.Location = new System.Drawing.Point(657, 13);
            this.btn_recieve.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_recieve.Name = "btn_recieve";
            this.btn_recieve.Size = new System.Drawing.Size(75, 22);
            this.btn_recieve.TabIndex = 14;
            this.btn_recieve.Text = "汇  总";
            this.btn_recieve.UseVisualStyleBackColor = true;
            this.btn_recieve.Click += new System.EventHandler(this.btn_recieve_Click);
            // 
            // btnVli
            // 
            this.btnVli.Location = new System.Drawing.Point(979, 14);
            this.btnVli.Name = "btnVli";
            this.btnVli.Size = new System.Drawing.Size(75, 23);
            this.btnVli.TabIndex = 12;
            this.btnVli.Text = "校验长宽";
            this.btnVli.UseVisualStyleBackColor = true;
            this.btnVli.Click += new System.EventHandler(this.btnVli_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(495, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "选择：";
            // 
            // cmbCigerType
            // 
            this.cmbCigerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCigerType.FormattingEnabled = true;
            this.cmbCigerType.Items.AddRange(new object[] {
            "全部",
            "常规烟",
            "异形烟"});
            this.cmbCigerType.Location = new System.Drawing.Point(879, 34);
            this.cmbCigerType.Name = "cmbCigerType";
            this.cmbCigerType.Size = new System.Drawing.Size(94, 20);
            this.cmbCigerType.TabIndex = 10;
            this.cmbCigerType.Visible = false;
            this.cmbCigerType.SelectedIndexChanged += new System.EventHandler(this.cmbCigerType_SelectedIndexChanged);
            // 
            // btn_all
            // 
            this.btn_all.Location = new System.Drawing.Point(381, 13);
            this.btn_all.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_all.Name = "btn_all";
            this.btn_all.Size = new System.Drawing.Size(75, 22);
            this.btn_all.TabIndex = 9;
            this.btn_all.Text = "全选";
            this.btn_all.UseVisualStyleBackColor = true;
            this.btn_all.Click += new System.EventHandler(this.btn_all_Click);
            // 
            // txt_codestr
            // 
            this.txt_codestr.Enabled = false;
            this.txt_codestr.Location = new System.Drawing.Point(1126, 8);
            this.txt_codestr.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_codestr.Name = "txt_codestr";
            this.txt_codestr.Size = new System.Drawing.Size(60, 21);
            this.txt_codestr.TabIndex = 8;
            this.txt_codestr.Visible = false;
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(186, 12);
            this.btn_search.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 22);
            this.btn_search.TabIndex = 3;
            this.btn_search.Text = "查询";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // lab_showinfo
            // 
            this.lab_showinfo.AutoSize = true;
            this.lab_showinfo.Location = new System.Drawing.Point(3, 45);
            this.lab_showinfo.Name = "lab_showinfo";
            this.lab_showinfo.Size = new System.Drawing.Size(365, 12);
            this.lab_showinfo.TabIndex = 2;
            this.lab_showinfo.Text = "勾选要接收的订单数据，点击“汇总”按钮，进行订单查询操作。  ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "订单日期";
            // 
            // datePick
            // 
            this.datePick.CustomFormat = "yyyy-MM-dd";
            this.datePick.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePick.Location = new System.Drawing.Point(70, 13);
            this.datePick.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.datePick.Name = "datePick";
            this.datePick.Size = new System.Drawing.Size(97, 21);
            this.datePick.TabIndex = 0;
            this.datePick.Value = new System.DateTime(2013, 10, 24, 0, 0, 0, 0);
            // 
            // orderdata
            // 
            this.orderdata.AllowUserToAddRows = false;
            this.orderdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.orderdata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkbox,
            this.Column1,
            this.dpid,
            this.count_hs,
            this.order_qty});
            this.orderdata.Dock = System.Windows.Forms.DockStyle.Left;
            this.orderdata.Location = new System.Drawing.Point(0, 60);
            this.orderdata.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.orderdata.Name = "orderdata";
            this.orderdata.RowTemplate.Height = 23;
            this.orderdata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.orderdata.Size = new System.Drawing.Size(553, 202);
            this.orderdata.TabIndex = 1;
            this.orderdata.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.orderdata_CellEndEdit);
            // 
            // checkbox
            // 
            this.checkbox.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
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
            // dpid
            // 
            this.dpid.DataPropertyName = "routecode";
            this.dpid.HeaderText = "车组信息";
            this.dpid.Name = "dpid";
            this.dpid.ReadOnly = true;
            // 
            // count_hs
            // 
            this.count_hs.DataPropertyName = "count_hs";
            this.count_hs.HeaderText = "订货户数";
            this.count_hs.Name = "count_hs";
            this.count_hs.ReadOnly = true;
            // 
            // order_qty
            // 
            this.order_qty.DataPropertyName = "order_qty";
            this.order_qty.HeaderText = "订货量";
            this.order_qty.Name = "order_qty";
            this.order_qty.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Location = new System.Drawing.Point(70, 116);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(826, 90);
            this.panel2.TabIndex = 2;
            this.panel2.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "正在读取数据...";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(33, 42);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(741, 22);
            this.progressBar1.TabIndex = 0;
            this.progressBar1.Visible = false;
            // 
            // dgvUnionOrderINfo
            // 
            this.dgvUnionOrderINfo.AllowUserToAddRows = false;
            this.dgvUnionOrderINfo.AllowUserToDeleteRows = false;
            this.dgvUnionOrderINfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnionOrderINfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUnionOrderINfo.Location = new System.Drawing.Point(553, 60);
            this.dgvUnionOrderINfo.Name = "dgvUnionOrderINfo";
            this.dgvUnionOrderINfo.ReadOnly = true;
            this.dgvUnionOrderINfo.RowTemplate.Height = 23;
            this.dgvUnionOrderINfo.Size = new System.Drawing.Size(637, 202);
            this.dgvUnionOrderINfo.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(659, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "总量：";
            this.label4.Visible = false;
            // 
            // win_order_Union
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1190, 262);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dgvUnionOrderINfo);
            this.Controls.Add(this.orderdata);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "win_order_Union";
            this.Text = "订单接收";
            this.Activated += new System.EventHandler(this.win_order_Union_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.win_order_recieve_FormClosing);
            this.Load += new System.EventHandler(this.win_order_Union_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderdata)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnionOrderINfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Label lab_showinfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker datePick;
        private System.Windows.Forms.DataGridView orderdata;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox txt_codestr;
        private System.Windows.Forms.Button btn_all;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkbox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dpid;
        private System.Windows.Forms.DataGridViewTextBoxColumn count_hs;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_qty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbCigerType;
        private System.Windows.Forms.DataGridView dgvUnionOrderINfo;
        private System.Windows.Forms.Button btnVli;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmb_line;
        private System.Windows.Forms.Button btn_recieve;
        private System.Windows.Forms.Label label4;
    }
}