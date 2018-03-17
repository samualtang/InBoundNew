namespace FormUI
{
    partial class LaneWayFMReport
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.巷道 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.储位总数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.使用个数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.使用率 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(28, 11);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(803, 427);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "详情";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.巷道,
            this.储位总数,
            this.使用个数,
            this.使用率});
            this.dataGridView1.Location = new System.Drawing.Point(4, 18);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(795, 251);
            this.dataGridView1.TabIndex = 0;
            // 
            // 巷道
            // 
            this.巷道.DataPropertyName = "KeyName1";
            this.巷道.HeaderText = "巷道";
            this.巷道.Name = "巷道";
            // 
            // 储位总数
            // 
            this.储位总数.DataPropertyName = "TOTAL";
            this.储位总数.HeaderText = "储位总数";
            this.储位总数.Name = "储位总数";
            // 
            // 使用个数
            // 
            this.使用个数.DataPropertyName = "USETOTAL";
            this.使用个数.HeaderText = "使用个数";
            this.使用个数.Name = "使用个数";
            // 
            // 使用率
            // 
            this.使用率.DataPropertyName = "usage";
            this.使用率.HeaderText = "使用率%";
            this.使用率.Name = "使用率";
            // 
            // LaneWayFMReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 495);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LaneWayFMReport";
            this.Text = "巷道使用率";
            this.Load += new System.EventHandler(this.QueryForm_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 巷道;
        private System.Windows.Forms.DataGridViewTextBoxColumn 储位总数;
        private System.Windows.Forms.DataGridViewTextBoxColumn 使用个数;
        private System.Windows.Forms.DataGridViewTextBoxColumn 使用率;
    }
}