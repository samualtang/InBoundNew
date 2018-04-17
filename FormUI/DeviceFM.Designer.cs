namespace FormUI
{
    partial class DeviceFM
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnChange = new System.Windows.Forms.Button();
            this.cmbSelectDeviceName = new System.Windows.Forms.ComboBox();
            this.设备编码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.设备名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.状态 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.最大任务数 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.入库最大数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.启用 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.操作 = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.设备编码,
            this.设备名称,
            this.状态,
            this.最大任务数,
            this.入库最大数量,
            this.启用,
            this.操作});
            this.dataGridView1.Location = new System.Drawing.Point(14, 65);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(722, 500);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbSelectDeviceName);
            this.groupBox1.Controls.Add(this.btnChange);
            this.groupBox1.Location = new System.Drawing.Point(14, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(722, 52);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作";
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(662, 12);
            this.btnChange.Margin = new System.Windows.Forms.Padding(2);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(56, 29);
            this.btnChange.TabIndex = 1;
            this.btnChange.Text = "修改";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // cmbSelectDeviceName
            // 
            this.cmbSelectDeviceName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbSelectDeviceName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectDeviceName.Items.AddRange(new object[] {
            "全部",
            "堆垛机",
            "开箱机"});
            this.cmbSelectDeviceName.Location = new System.Drawing.Point(39, 17);
            this.cmbSelectDeviceName.Name = "cmbSelectDeviceName";
            this.cmbSelectDeviceName.Size = new System.Drawing.Size(121, 20);
            this.cmbSelectDeviceName.TabIndex = 0;
            this.cmbSelectDeviceName.SelectedIndexChanged += new System.EventHandler(this.cmbSelectDeviceName_SelectedIndexChanged);
            // 
            // 设备编码
            // 
            this.设备编码.DataPropertyName = "deviceno";
            this.设备编码.HeaderText = "设备编码";
            this.设备编码.Name = "设备编码";
            // 
            // 设备名称
            // 
            this.设备名称.DataPropertyName = "devicename";
            this.设备名称.HeaderText = "设备名称";
            this.设备名称.Name = "设备名称";
            // 
            // 状态
            // 
            this.状态.DataPropertyName = "status";
            this.状态.HeaderText = "状态";
            this.状态.Name = "状态";
            // 
            // 最大任务数
            // 
            this.最大任务数.DataPropertyName = "maxtasknum";
            this.最大任务数.HeaderText = "最大任务数";
            this.最大任务数.Name = "最大任务数";
            // 
            // 入库最大数量
            // 
            this.入库最大数量.DataPropertyName = "troughnum";
            this.入库最大数量.HeaderText = "入库最大数量";
            this.入库最大数量.Name = "入库最大数量";
            // 
            // 启用
            // 
            this.启用.HeaderText = "操作";
            this.启用.Name = "启用";
            this.启用.Text = "启用";
            this.启用.UseColumnTextForButtonValue = true;
            // 
            // 操作
            // 
            this.操作.HeaderText = "操作";
            this.操作.Name = "操作";
            this.操作.Text = "禁用";
            this.操作.UseColumnTextForButtonValue = true;
            // 
            // DeviceFM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 604);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DeviceFM";
            this.Text = "设备管理";
            this.Load += new System.EventHandler(this.LaneWayFM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.ComboBox cmbSelectDeviceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn 设备编码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 设备名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 状态;
        private System.Windows.Forms.DataGridViewTextBoxColumn 最大任务数;
        private System.Windows.Forms.DataGridViewTextBoxColumn 入库最大数量;
        private System.Windows.Forms.DataGridViewButtonColumn 启用;
        private System.Windows.Forms.DataGridViewButtonColumn 操作;
    }
}