namespace highSpeed.orderHandle
{
    partial class ReHihgSpeedForm
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
            this.regioncode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cuscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_codestr1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_produce = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.txt_codestr2 = new System.Windows.Forms.TextBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_un = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // regioncode
            // 
            this.regioncode.DataPropertyName = "regioncode";
            this.regioncode.HeaderText = "车组CODE";
            this.regioncode.Name = "regioncode";
            this.regioncode.ReadOnly = true;
            // 
            // qty
            // 
            this.qty.DataPropertyName = "qty";
            this.qty.HeaderText = "订货数量";
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            // 
            // cuscount
            // 
            this.cuscount.DataPropertyName = "cuscount";
            this.cuscount.HeaderText = "订货户数";
            this.cuscount.Name = "cuscount";
            this.cuscount.ReadOnly = true;
            // 
            // state
            // 
            this.state.DataPropertyName = "state";
            this.state.HeaderText = "分拣状态";
            this.state.Name = "state";
            this.state.ReadOnly = true;
            this.state.Width = 80;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.ItemSize = new System.Drawing.Size(200, 18);
            this.tabControl1.Location = new System.Drawing.Point(1, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(893, 433);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.txt_codestr1);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.btn_produce);
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(885, 407);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "       常规烟重排       ";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(768, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 35);
            this.button1.TabIndex = 7;
            this.button1.Text = "刷  新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_codestr1
            // 
            this.txt_codestr1.Location = new System.Drawing.Point(761, 383);
            this.txt_codestr1.Name = "txt_codestr1";
            this.txt_codestr1.Size = new System.Drawing.Size(118, 21);
            this.txt_codestr1.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(833, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "常规烟";
            // 
            // btn_produce
            // 
            this.btn_produce.Location = new System.Drawing.Point(768, 280);
            this.btn_produce.Name = "btn_produce";
            this.btn_produce.Size = new System.Drawing.Size(111, 35);
            this.btn_produce.TabIndex = 3;
            this.btn_produce.Text = "确  认";
            this.btn_produce.UseVisualStyleBackColor = true;
            this.btn_produce.Click += new System.EventHandler(this.btn_produce_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column1,
            this.Column5,
            this.Column6});
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(752, 401);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "选择";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 50;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "regioncode";
            this.Column2.HeaderText = "车组号";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "status";
            this.Column3.HeaderText = "分拣状态";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "allqty";
            this.Column4.HeaderText = "订单烟总量";
            this.Column4.Name = "Column4";
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "untask";
            this.Column1.HeaderText = "未排程";
            this.Column1.Name = "Column1";
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "unpoke";
            this.Column5.HeaderText = "未分拣";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "havepoke";
            this.Column6.HeaderText = "已分拣";
            this.Column6.Name = "Column6";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button4);
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.txt_codestr2);
            this.tabPage3.Controls.Add(this.dataGridView2);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.btn_un);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(885, 407);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "       异型烟重排       ";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(768, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 35);
            this.button2.TabIndex = 9;
            this.button2.Text = "刷  新";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txt_codestr2
            // 
            this.txt_codestr2.Location = new System.Drawing.Point(761, 383);
            this.txt_codestr2.Name = "txt_codestr2";
            this.txt_codestr2.Size = new System.Drawing.Size(118, 21);
            this.txt_codestr2.TabIndex = 8;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn2,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.dataGridView2.Location = new System.Drawing.Point(3, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(752, 401);
            this.dataGridView2.TabIndex = 5;
            this.dataGridView2.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellEndEdit);
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.HeaderText = "选择";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.Width = 50;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "regioncode";
            this.dataGridViewTextBoxColumn1.HeaderText = "车组号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "status";
            this.dataGridViewTextBoxColumn2.HeaderText = "分拣状态";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "allqty";
            this.dataGridViewTextBoxColumn3.HeaderText = "订单烟总量";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "untask";
            this.dataGridViewTextBoxColumn4.HeaderText = "未排程";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "unpoke";
            this.dataGridViewTextBoxColumn5.HeaderText = "未分拣";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "havepoke";
            this.dataGridViewTextBoxColumn6.HeaderText = "已分拣";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(833, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "异型烟";
            // 
            // btn_un
            // 
            this.btn_un.Location = new System.Drawing.Point(768, 280);
            this.btn_un.Name = "btn_un";
            this.btn_un.Size = new System.Drawing.Size(111, 35);
            this.btn_un.TabIndex = 3;
            this.btn_un.Text = "确  认";
            this.btn_un.UseVisualStyleBackColor = true;
            this.btn_un.Click += new System.EventHandler(this.btn_un_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(768, 90);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(111, 35);
            this.button3.TabIndex = 8;
            this.button3.Text = "全  选";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(768, 90);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(111, 35);
            this.button4.TabIndex = 10;
            this.button4.Text = "全  选";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // ReHihgSpeedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 436);
            this.Controls.Add(this.tabControl1);
            this.Name = "ReHihgSpeedForm";
            this.Text = "车组重排";
            this.Load += new System.EventHandler(this.ReHihgSpeedForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn regioncode;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn cuscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn state;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_produce;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_un;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.TextBox txt_codestr1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txt_codestr2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;

    }
}