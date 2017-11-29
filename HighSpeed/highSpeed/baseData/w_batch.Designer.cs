namespace highSpeed.baseData
{
    partial class win_batch
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
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_new = new System.Windows.Forms.Button();
            this.batchdata = new System.Windows.Forms.DataGridView();
            this.cbunnormal = new System.Windows.Forms.CheckBox();
            this.cbnormal = new System.Windows.Forms.CheckBox();
            this.sequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.batchcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.starttime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endtime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.类型 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.batchdata)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbnormal);
            this.panel1.Controls.Add(this.cbunnormal);
            this.panel1.Controls.Add(this.btn_close);
            this.panel1.Controls.Add(this.btn_new);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1232, 72);
            this.panel1.TabIndex = 0;
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(507, 18);
            this.btn_close.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(112, 34);
            this.btn_close.TabIndex = 1;
            this.btn_close.Text = "关闭批次";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_new
            // 
            this.btn_new.Location = new System.Drawing.Point(336, 18);
            this.btn_new.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(112, 34);
            this.btn_new.TabIndex = 0;
            this.btn_new.Text = "创建批次";
            this.btn_new.UseCompatibleTextRendering = true;
            this.btn_new.UseVisualStyleBackColor = true;
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // batchdata
            // 
            this.batchdata.AllowUserToAddRows = false;
            this.batchdata.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.batchdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.batchdata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sequence,
            this.batchcode,
            this.starttime,
            this.endtime,
            this.类型,
            this.status});
            this.batchdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.batchdata.Location = new System.Drawing.Point(0, 72);
            this.batchdata.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.batchdata.MultiSelect = false;
            this.batchdata.Name = "batchdata";
            this.batchdata.ReadOnly = true;
            this.batchdata.RowTemplate.Height = 23;
            this.batchdata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.batchdata.Size = new System.Drawing.Size(1232, 406);
            this.batchdata.TabIndex = 1;
            // 
            // cbunnormal
            // 
            this.cbunnormal.AutoSize = true;
            this.cbunnormal.Checked = true;
            this.cbunnormal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbunnormal.Location = new System.Drawing.Point(192, 30);
            this.cbunnormal.Name = "cbunnormal";
            this.cbunnormal.Size = new System.Drawing.Size(88, 22);
            this.cbunnormal.TabIndex = 2;
            this.cbunnormal.Text = "异型烟";
            this.cbunnormal.UseVisualStyleBackColor = true;
            // 
            // cbnormal
            // 
            this.cbnormal.AutoSize = true;
            this.cbnormal.Checked = true;
            this.cbnormal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbnormal.Location = new System.Drawing.Point(27, 30);
            this.cbnormal.Name = "cbnormal";
            this.cbnormal.Size = new System.Drawing.Size(88, 22);
            this.cbnormal.TabIndex = 3;
            this.cbnormal.Text = "标准烟";
            this.cbnormal.UseVisualStyleBackColor = true;
            // 
            // sequence
            // 
            this.sequence.DataPropertyName = "rownum";
            this.sequence.HeaderText = "序号";
            this.sequence.Name = "sequence";
            this.sequence.ReadOnly = true;
            this.sequence.Width = 60;
            // 
            // batchcode
            // 
            this.batchcode.DataPropertyName = "batchcode";
            this.batchcode.HeaderText = "批次编号";
            this.batchcode.Name = "batchcode";
            this.batchcode.ReadOnly = true;
            // 
            // starttime
            // 
            this.starttime.DataPropertyName = "starttime";
            this.starttime.HeaderText = "创建时间";
            this.starttime.Name = "starttime";
            this.starttime.ReadOnly = true;
            this.starttime.Width = 150;
            // 
            // endtime
            // 
            this.endtime.DataPropertyName = "endtime";
            this.endtime.HeaderText = "关闭时间";
            this.endtime.Name = "endtime";
            this.endtime.ReadOnly = true;
            this.endtime.Width = 150;
            // 
            // 类型
            // 
            this.类型.DataPropertyName = "batchtype";
            this.类型.HeaderText = "类型";
            this.类型.Name = "类型";
            this.类型.ReadOnly = true;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "批次状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // win_batch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 478);
            this.Controls.Add(this.batchdata);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "win_batch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "分拣批次管理";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.batchdata)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_new;
        public System.Windows.Forms.DataGridView batchdata;
        private System.Windows.Forms.CheckBox cbnormal;
        private System.Windows.Forms.CheckBox cbunnormal;
        private System.Windows.Forms.DataGridViewTextBoxColumn sequence;
        private System.Windows.Forms.DataGridViewTextBoxColumn batchcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn starttime;
        private System.Windows.Forms.DataGridViewTextBoxColumn endtime;
        private System.Windows.Forms.DataGridViewTextBoxColumn 类型;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
    }
}