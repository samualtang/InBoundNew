namespace highSpeed.orderHandle
{
    partial class w_PackageMachineFm
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
            this.button_datacomplte = new System.Windows.Forms.Button();
            this.orderdata = new System.Windows.Forms.DataGridView();
            this.checkbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_query = new System.Windows.Forms.Button();
            this.textBox_querytext = new System.Windows.Forms.TextBox();
            this.comboBox_querylist = new System.Windows.Forms.ComboBox();
            this.button_all = new System.Windows.Forms.Button();
            this.button_TBJ = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pbLoading = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.orderdata)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // button_datacomplte
            // 
            this.button_datacomplte.Location = new System.Drawing.Point(721, 16);
            this.button_datacomplte.Name = "button_datacomplte";
            this.button_datacomplte.Size = new System.Drawing.Size(122, 32);
            this.button_datacomplte.TabIndex = 1;
            this.button_datacomplte.Text = "生成包装机数据";
            this.button_datacomplte.UseVisualStyleBackColor = true;
            this.button_datacomplte.Click += new System.EventHandler(this.button_datacomplte_Click);
            // 
            // orderdata
            // 
            this.orderdata.AllowUserToAddRows = false;
            this.orderdata.AllowUserToDeleteRows = false;
            this.orderdata.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.orderdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.orderdata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkbox,
            this.Column2,
            this.Column1,
            this.Column3,
            this.Column4,
            this.Column5});
            this.orderdata.Location = new System.Drawing.Point(12, 60);
            this.orderdata.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.orderdata.Name = "orderdata";
            this.orderdata.RowTemplate.Height = 23;
            this.orderdata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.orderdata.Size = new System.Drawing.Size(983, 361);
            this.orderdata.TabIndex = 3;
            this.orderdata.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.orderdata_CellContentClick);
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
            // Column2
            // 
            this.Column2.DataPropertyName = "synseq";
            this.Column2.HeaderText = "批次号";
            this.Column2.Name = "Column2";
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "EXPORT";
            this.Column1.HeaderText = "包装机号";
            this.Column1.Name = "Column1";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "COUNTREGIONCODE";
            this.Column3.HeaderText = "车组数";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "COUNTBILLCODE";
            this.Column4.HeaderText = "订单数";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "COUNTQUANTITY";
            this.Column5.HeaderText = "数量";
            this.Column5.Name = "Column5";
            // 
            // button_query
            // 
            this.button_query.Location = new System.Drawing.Point(427, 19);
            this.button_query.Name = "button_query";
            this.button_query.Size = new System.Drawing.Size(75, 27);
            this.button_query.TabIndex = 4;
            this.button_query.Text = "查询";
            this.button_query.UseVisualStyleBackColor = true;
            this.button_query.Click += new System.EventHandler(this.button_query_Click);
            // 
            // textBox_querytext
            // 
            this.textBox_querytext.Location = new System.Drawing.Point(150, 23);
            this.textBox_querytext.Name = "textBox_querytext";
            this.textBox_querytext.Size = new System.Drawing.Size(256, 21);
            this.textBox_querytext.TabIndex = 5;
            // 
            // comboBox_querylist
            // 
            this.comboBox_querylist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_querylist.FormattingEnabled = true;
            this.comboBox_querylist.Items.AddRange(new object[] {
            "批次号",
            "包装机号"});
            this.comboBox_querylist.Location = new System.Drawing.Point(29, 23);
            this.comboBox_querylist.Name = "comboBox_querylist";
            this.comboBox_querylist.Size = new System.Drawing.Size(115, 20);
            this.comboBox_querylist.TabIndex = 6;
            // 
            // button_all
            // 
            this.button_all.Location = new System.Drawing.Point(529, 19);
            this.button_all.Name = "button_all";
            this.button_all.Size = new System.Drawing.Size(75, 27);
            this.button_all.TabIndex = 7;
            this.button_all.Text = "全选";
            this.button_all.UseVisualStyleBackColor = true;
            this.button_all.Click += new System.EventHandler(this.button_all_Click);
            // 
            // button_TBJ
            // 
            this.button_TBJ.Location = new System.Drawing.Point(873, 16);
            this.button_TBJ.Name = "button_TBJ";
            this.button_TBJ.Size = new System.Drawing.Size(122, 32);
            this.button_TBJ.TabIndex = 8;
            this.button_TBJ.Text = "生成贴标机数据";
            this.button_TBJ.UseVisualStyleBackColor = true;
            this.button_TBJ.Click += new System.EventHandler(this.button_TBJ_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.pbLoading);
            this.panel3.Font = new System.Drawing.Font("宋体", 11F);
            this.panel3.Location = new System.Drawing.Point(322, 121);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(399, 100);
            this.panel3.TabIndex = 11;
            this.panel3.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(210, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 15);
            this.label1.TabIndex = 28;
            this.label1.Text = "包装机数据生成中......";
            // 
            // pbLoading
            // 
            this.pbLoading.Image = global::highSpeed.Properties.Resources.loading;
            this.pbLoading.Location = new System.Drawing.Point(-2, -2);
            this.pbLoading.Name = "pbLoading";
            this.pbLoading.Size = new System.Drawing.Size(206, 100);
            this.pbLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLoading.TabIndex = 27;
            this.pbLoading.TabStop = false;
            // 
            // w_PackageMachineFm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 432);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.button_TBJ);
            this.Controls.Add(this.button_all);
            this.Controls.Add(this.comboBox_querylist);
            this.Controls.Add(this.textBox_querytext);
            this.Controls.Add(this.button_query);
            this.Controls.Add(this.orderdata);
            this.Controls.Add(this.button_datacomplte);
            this.Name = "w_PackageMachineFm";
            this.Text = "包装机数据排程";
            ((System.ComponentModel.ISupportInitialize)(this.orderdata)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_datacomplte;
        private System.Windows.Forms.DataGridView orderdata;
        private System.Windows.Forms.Button button_query;
        private System.Windows.Forms.TextBox textBox_querytext;
        private System.Windows.Forms.ComboBox comboBox_querylist;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkbox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Button button_all;
        private System.Windows.Forms.Button button_TBJ;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbLoading;
    }
}