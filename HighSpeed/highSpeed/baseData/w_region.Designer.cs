namespace highSpeed.baseData
{
    partial class win_region
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
            this.regiondata = new System.Windows.Forms.DataGridView();
            this.sequence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.routeno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.routename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mastername = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.regiondata)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1498, 64);
            this.panel1.TabIndex = 0;
            // 
            // regiondata
            // 
            this.regiondata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.regiondata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sequence,
            this.routeno,
            this.routename,
            this.mastername});
            this.regiondata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.regiondata.Location = new System.Drawing.Point(0, 64);
            this.regiondata.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.regiondata.Name = "regiondata";
            this.regiondata.ReadOnly = true;
            this.regiondata.RowTemplate.Height = 23;
            this.regiondata.Size = new System.Drawing.Size(1498, 518);
            this.regiondata.TabIndex = 1;
            // 
            // sequence
            // 
            this.sequence.DataPropertyName = "rownum";
            this.sequence.HeaderText = "序号";
            this.sequence.Name = "sequence";
            this.sequence.ReadOnly = true;
            // 
            // routeno
            // 
            this.routeno.DataPropertyName = "routecode";
            this.routeno.HeaderText = "车组CODE";
            this.routeno.Name = "routeno";
            this.routeno.ReadOnly = true;
            // 
            // routename
            // 
            this.routename.DataPropertyName = "routename";
            this.routename.HeaderText = "车组描述";
            this.routename.Name = "routename";
            this.routename.ReadOnly = true;
            // 
            // mastername
            // 
            this.mastername.DataPropertyName = "deptname";
            this.mastername.HeaderText = "所属单位";
            this.mastername.Name = "mastername";
            this.mastername.ReadOnly = true;
            // 
            // win_region
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1498, 582);
            this.Controls.Add(this.regiondata);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "win_region";
            this.Text = "w_region";
            ((System.ComponentModel.ISupportInitialize)(this.regiondata)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView regiondata;
        private System.Windows.Forms.DataGridViewTextBoxColumn sequence;
        private System.Windows.Forms.DataGridViewTextBoxColumn routeno;
        private System.Windows.Forms.DataGridViewTextBoxColumn routename;
        private System.Windows.Forms.DataGridViewTextBoxColumn mastername;
    }
}