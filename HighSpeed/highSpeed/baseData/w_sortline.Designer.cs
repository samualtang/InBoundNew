namespace highSpeed.baseData
{
    partial class win_sortline
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
            this.sortlinedata = new System.Windows.Forms.DataGridView();
            this.rownum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linenum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.linedesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.troughcount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sortlinedata)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_close);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(789, 47);
            this.panel1.TabIndex = 0;
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(13, 13);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 0;
            this.btn_close.Text = "关闭";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // sortlinedata
            // 
            this.sortlinedata.AllowUserToAddRows = false;
            this.sortlinedata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sortlinedata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rownum,
            this.linenum,
            this.linedesc,
            this.troughcount});
            this.sortlinedata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sortlinedata.Location = new System.Drawing.Point(0, 47);
            this.sortlinedata.MultiSelect = false;
            this.sortlinedata.Name = "sortlinedata";
            this.sortlinedata.ReadOnly = true;
            this.sortlinedata.RowTemplate.Height = 23;
            this.sortlinedata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.sortlinedata.Size = new System.Drawing.Size(789, 215);
            this.sortlinedata.TabIndex = 1;
            // 
            // rownum
            // 
            this.rownum.DataPropertyName = "rownum";
            this.rownum.HeaderText = "序号";
            this.rownum.Name = "rownum";
            this.rownum.ReadOnly = true;
            // 
            // linenum
            // 
            this.linenum.DataPropertyName = "linenum";
            this.linenum.HeaderText = "分拣线编号";
            this.linenum.Name = "linenum";
            this.linenum.ReadOnly = true;
            // 
            // linedesc
            // 
            this.linedesc.DataPropertyName = "linedesc";
            this.linedesc.HeaderText = "分拣线描述";
            this.linedesc.Name = "linedesc";
            this.linedesc.ReadOnly = true;
            // 
            // troughcount
            // 
            this.troughcount.DataPropertyName = "troughcount";
            this.troughcount.HeaderText = "通道数量";
            this.troughcount.Name = "troughcount";
            this.troughcount.ReadOnly = true;
            // 
            // win_sortline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 262);
            this.Controls.Add(this.sortlinedata);
            this.Controls.Add(this.panel1);
            this.Name = "win_sortline";
            this.Text = "分拣线信息";
            this.Load += new System.EventHandler(this.win_sortline_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sortlinedata)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView sortlinedata;
        private System.Windows.Forms.DataGridViewTextBoxColumn rownum;
        private System.Windows.Forms.DataGridViewTextBoxColumn linenum;
        private System.Windows.Forms.DataGridViewTextBoxColumn linedesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn troughcount;
        private System.Windows.Forms.Button btn_close;
    }
}