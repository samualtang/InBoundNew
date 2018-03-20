namespace FormUI
{
    partial class ManualForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.任务编号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.任务类型 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.源地址 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.目标地址 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品牌编码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.计划数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.返库数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Location = new System.Drawing.Point(28, 27);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(803, 37);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(734, 12);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(65, 21);
            this.button2.TabIndex = 4;
            this.button2.Text = "完成拆垛";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            this.任务编号,
            this.任务类型,
            this.源地址,
            this.目标地址,
            this.品牌编码,
            this.计划数量,
            this.返库数量});
            this.dataGridView1.Location = new System.Drawing.Point(4, 18);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(795, 251);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // 任务编号
            // 
            this.任务编号.DataPropertyName = "jobid";
            this.任务编号.HeaderText = "任务编号";
            this.任务编号.Name = "任务编号";
            // 
            // 任务类型
            // 
            this.任务类型.DataPropertyName = "jobtype";
            this.任务类型.HeaderText = "任务类型";
            this.任务类型.Name = "任务类型";
            // 
            // 源地址
            // 
            this.源地址.DataPropertyName = "source";
            this.源地址.HeaderText = "源地址";
            this.源地址.Name = "源地址";
            // 
            // 目标地址
            // 
            this.目标地址.DataPropertyName = "target";
            this.目标地址.HeaderText = "目标地址";
            this.目标地址.Name = "目标地址";
            // 
            // 品牌编码
            // 
            this.品牌编码.DataPropertyName = "brandid";
            this.品牌编码.HeaderText = "品牌编码";
            this.品牌编码.Name = "品牌编码";
            // 
            // 计划数量
            // 
            this.计划数量.DataPropertyName = "planqty";
            this.计划数量.HeaderText = "计划数量";
            this.计划数量.Name = "计划数量";
            // 
            // 返库数量
            // 
            this.返库数量.HeaderText = "返库数量";
            this.返库数量.Name = "返库数量";
            // 
            // ManualForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 403);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ManualForm";
            this.Text = "人工拆垛";
            this.Load += new System.EventHandler(this.QueryForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 任务编号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 任务类型;
        private System.Windows.Forms.DataGridViewTextBoxColumn 源地址;
        private System.Windows.Forms.DataGridViewTextBoxColumn 目标地址;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品牌编码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 计划数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 返库数量;
    }
}