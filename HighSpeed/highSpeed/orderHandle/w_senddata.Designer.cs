namespace highSpeed.orderHandle
{
    partial class w_senddata
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
            this.btn_send = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExp = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cblist = new System.Windows.Forms.CheckedListBox();
            this.gvdata = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvdata)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_send
            // 
            this.btn_send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_send.Location = new System.Drawing.Point(724, 7);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(91, 45);
            this.btn_send.TabIndex = 0;
            this.btn_send.Text = "发   送";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExp);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cblist);
            this.panel1.Controls.Add(this.btn_send);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(853, 61);
            this.panel1.TabIndex = 4;
            // 
            // btnExp
            // 
            this.btnExp.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnExp.Location = new System.Drawing.Point(609, 7);
            this.btnExp.Name = "btnExp";
            this.btnExp.Size = new System.Drawing.Size(91, 47);
            this.btnExp.TabIndex = 5;
            this.btnExp.Text = "导   出";
            this.btnExp.UseVisualStyleBackColor = true;
            this.btnExp.Click += new System.EventHandler(this.btnExp_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(14, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "一号工程编号：";
            // 
            // cblist
            // 
            this.cblist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cblist.BackColor = System.Drawing.SystemColors.Control;
            this.cblist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cblist.CheckOnClick = true;
            this.cblist.ColumnWidth = 80;
            this.cblist.FormattingEnabled = true;
            this.cblist.HorizontalScrollbar = true;
            this.cblist.Location = new System.Drawing.Point(176, 21);
            this.cblist.MultiColumn = true;
            this.cblist.Name = "cblist";
            this.cblist.Size = new System.Drawing.Size(307, 20);
            this.cblist.TabIndex = 3; 
            // 
            // gvdata
            // 
            this.gvdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvdata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.gvdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvdata.Location = new System.Drawing.Point(0, 61);
            this.gvdata.Name = "gvdata";
            this.gvdata.RowTemplate.Height = 27;
            this.gvdata.Size = new System.Drawing.Size(853, 413);
            this.gvdata.TabIndex = 5;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "";
            this.Column5.Name = "Column5";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "批次编码";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "订单数量";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "任务数量";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "批次号";
            this.Column4.Name = "Column4";
            // 
            // w_senddata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 474);
            this.Controls.Add(this.gvdata);
            this.Controls.Add(this.panel1);
            this.Name = "w_senddata";
            this.Text = "数据发送";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvdata)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gvdata;
        private System.Windows.Forms.CheckedListBox cblist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExp;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}