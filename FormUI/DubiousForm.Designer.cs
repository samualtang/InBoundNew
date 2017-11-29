namespace FormUI
{
    partial class DubiousForm
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
            this.储位编号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.层 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.列 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.巷道 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工作状态 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(28, 68);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(803, 327);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "详情";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.储位编号,
            this.层,
            this.列,
            this.巷道,
            this.工作状态});
            this.dataGridView1.Location = new System.Drawing.Point(4, 18);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(795, 251);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // 储位编号
            // 
            this.储位编号.DataPropertyName = "cellno";
            this.储位编号.HeaderText = "储位编号";
            this.储位编号.Name = "储位编号";
            // 
            // 层
            // 
            this.层.DataPropertyName = "floor";
            this.层.HeaderText = "层";
            this.层.Name = "层";
            // 
            // 列
            // 
            this.列.DataPropertyName = "col";
            this.列.HeaderText = "列";
            this.列.Name = "列";
            // 
            // 巷道
            // 
            this.巷道.DataPropertyName = "lanewayno";
            this.巷道.HeaderText = "巷道";
            this.巷道.Name = "巷道";
            // 
            // 工作状态
            // 
            this.工作状态.DataPropertyName = "workstatus";
            this.工作状态.HeaderText = "工作状态";
            this.工作状态.Name = "工作状态";
            // 
            // DubiousForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 403);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DubiousForm";
            this.Text = "可疑储位";
            this.Load += new System.EventHandler(this.QueryForm_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 储位编号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 层;
        private System.Windows.Forms.DataGridViewTextBoxColumn 列;
        private System.Windows.Forms.DataGridViewTextBoxColumn 巷道;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工作状态;
    }
}