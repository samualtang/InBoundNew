namespace SortingControlSys
{
    partial class Fm_SelectedInfoEX
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbSelect = new System.Windows.Forms.ComboBox();
            this.task_data = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.卷烟代码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.卷烟名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.物理通道号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.txtMachine = new System.Windows.Forms.TextBox();
            this.lblmachine = new System.Windows.Forms.Label();
            this.txtsortnum = new System.Windows.Forms.TextBox();
            this.lblsortnum = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.task_data)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmbSelect);
            this.groupBox2.Controls.Add(this.task_data);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.txtMachine);
            this.groupBox2.Controls.Add(this.lblmachine);
            this.groupBox2.Controls.Add(this.txtsortnum);
            this.groupBox2.Controls.Add(this.lblsortnum);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1053, 575);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "任务查询";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 18;
            this.label5.Text = "查询条件:";
            // 
            // cmbSelect
            // 
            this.cmbSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelect.FormattingEnabled = true;
            this.cmbSelect.Location = new System.Drawing.Point(77, 24);
            this.cmbSelect.Name = "cmbSelect";
            this.cmbSelect.Size = new System.Drawing.Size(121, 20);
            this.cmbSelect.TabIndex = 17;
            this.cmbSelect.SelectedIndexChanged += new System.EventHandler(this.cmbSelect_SelectedIndexChanged);
            // 
            // task_data
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.task_data.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle17;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.task_data.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.task_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.task_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.卷烟代码,
            this.卷烟名称,
            this.物理通道号,
            this.Column3,
            this.Column5,
            this.Column4});
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.task_data.DefaultCellStyle = dataGridViewCellStyle19;
            this.task_data.Location = new System.Drawing.Point(0, 59);
            this.task_data.Name = "task_data";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.task_data.RowsDefaultCellStyle = dataGridViewCellStyle20;
            this.task_data.RowTemplate.Height = 23;
            this.task_data.Size = new System.Drawing.Size(1048, 511);
            this.task_data.TabIndex = 16;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "SortNum";
            this.Column1.HeaderText = "任务号";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "billcode";
            this.Column2.HeaderText = "订单号";
            this.Column2.Name = "Column2";
            this.Column2.Width = 200;
            // 
            // 卷烟代码
            // 
            this.卷烟代码.HeaderText = "卷烟代码";
            this.卷烟代码.Name = "卷烟代码";
            // 
            // 卷烟名称
            // 
            this.卷烟名称.HeaderText = "卷烟名称";
            this.卷烟名称.Name = "卷烟名称";
            // 
            // 物理通道号
            // 
            this.物理通道号.HeaderText = "物理通道号";
            this.物理通道号.Name = "物理通道号";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "pokenum";
            this.Column3.HeaderText = "拨烟数量";
            this.Column3.Name = "Column3";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "放烟位置";
            this.Column5.Name = "Column5";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "sortstate";
            this.Column4.HeaderText = "状态位";
            this.Column4.Name = "Column4";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(953, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtMachine
            // 
            this.txtMachine.Location = new System.Drawing.Point(749, 24);
            this.txtMachine.Name = "txtMachine";
            this.txtMachine.Size = new System.Drawing.Size(86, 21);
            this.txtMachine.TabIndex = 14;
            // 
            // lblmachine
            // 
            this.lblmachine.AutoSize = true;
            this.lblmachine.Location = new System.Drawing.Point(600, 29);
            this.lblmachine.Name = "lblmachine";
            this.lblmachine.Size = new System.Drawing.Size(131, 12);
            this.lblmachine.TabIndex = 13;
            this.lblmachine.Text = "请输入设备号进行查询:";
            // 
            // txtsortnum
            // 
            this.txtsortnum.Location = new System.Drawing.Point(486, 24);
            this.txtsortnum.Name = "txtsortnum";
            this.txtsortnum.Size = new System.Drawing.Size(86, 21);
            this.txtsortnum.TabIndex = 14;
            // 
            // lblsortnum
            // 
            this.lblsortnum.AutoSize = true;
            this.lblsortnum.Location = new System.Drawing.Point(348, 29);
            this.lblsortnum.Name = "lblsortnum";
            this.lblsortnum.Size = new System.Drawing.Size(131, 12);
            this.lblsortnum.TabIndex = 13;
            this.lblsortnum.Text = "请输入任务号进行查询:";
            // 
            // Fm_SelectedInfoEX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 588);
            this.Controls.Add(this.groupBox2);
            this.Name = "Fm_SelectedInfoEX";
            this.Text = "Fm_SelectedInfoEX";
            this.Load += new System.EventHandler(this.Fm_SelectedInfoEX_Load);
            this.SizeChanged += new System.EventHandler(this.Fm_SelectedInfoEX_SizeChanged);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.task_data)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbSelect;
        private System.Windows.Forms.DataGridView task_data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 卷烟代码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 卷烟名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物理通道号;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtMachine;
        private System.Windows.Forms.Label lblmachine;
        private System.Windows.Forms.TextBox txtsortnum;
        private System.Windows.Forms.Label lblsortnum;
    }
}