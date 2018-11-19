namespace SpecialShapeSmoke
{
    partial class NowView_new
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
            this.DgvNowView = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNowPoke = new System.Windows.Forms.Button();
            this.btnMachineSeq1 = new System.Windows.Forms.Button();
            this.btnMachineSeq2 = new System.Windows.Forms.Button();
            this.labMachineSeq = new System.Windows.Forms.Label();
            this.sendtasknum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tasknum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sortnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customername = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regioncode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TROUGHNUM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIGARETTECODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIGARETTENAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pokenum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pokeid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PULLSTATUS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packagemachine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DgvNowView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DgvNowView
            // 
            this.DgvNowView.AllowUserToAddRows = false;
            this.DgvNowView.AllowUserToDeleteRows = false;
            this.DgvNowView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvNowView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sendtasknum,
            this.tasknum,
            this.sortnum,
            this.customername,
            this.regioncode,
            this.TROUGHNUM,
            this.CIGARETTECODE,
            this.CIGARETTENAME,
            this.pokenum,
            this.status,
            this.pokeid,
            this.PULLSTATUS,
            this.packagemachine});
            this.DgvNowView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvNowView.Location = new System.Drawing.Point(3, 17);
            this.DgvNowView.Name = "DgvNowView";
            this.DgvNowView.ReadOnly = true;
            this.DgvNowView.RowTemplate.Height = 23;
            this.DgvNowView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvNowView.Size = new System.Drawing.Size(1237, 499);
            this.DgvNowView.TabIndex = 0;
            this.DgvNowView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgvNowView_CellFormatting);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.DgvNowView);
            this.groupBox1.Location = new System.Drawing.Point(4, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1243, 519);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btnNowPoke
            // 
            this.btnNowPoke.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNowPoke.Font = new System.Drawing.Font("宋体", 20F);
            this.btnNowPoke.Location = new System.Drawing.Point(933, 12);
            this.btnNowPoke.Name = "btnNowPoke";
            this.btnNowPoke.Size = new System.Drawing.Size(152, 45);
            this.btnNowPoke.TabIndex = 2;
            this.btnNowPoke.Text = "定位当前";
            this.btnNowPoke.UseVisualStyleBackColor = true;
            this.btnNowPoke.Click += new System.EventHandler(this.btnNowPoke_Click);
            // 
            // btnMachineSeq1
            // 
            this.btnMachineSeq1.Font = new System.Drawing.Font("宋体", 18F);
            this.btnMachineSeq1.Location = new System.Drawing.Point(7, 12);
            this.btnMachineSeq1.Name = "btnMachineSeq1";
            this.btnMachineSeq1.Size = new System.Drawing.Size(154, 45);
            this.btnMachineSeq1.TabIndex = 3;
            this.btnMachineSeq1.Text = "通道1";
            this.btnMachineSeq1.UseVisualStyleBackColor = true;
            this.btnMachineSeq1.Visible = false;
            this.btnMachineSeq1.Click += new System.EventHandler(this.btnMachineSeq1_Click);
            // 
            // btnMachineSeq2
            // 
            this.btnMachineSeq2.Font = new System.Drawing.Font("宋体", 18F);
            this.btnMachineSeq2.Location = new System.Drawing.Point(167, 12);
            this.btnMachineSeq2.Name = "btnMachineSeq2";
            this.btnMachineSeq2.Size = new System.Drawing.Size(154, 45);
            this.btnMachineSeq2.TabIndex = 4;
            this.btnMachineSeq2.Text = "通道2";
            this.btnMachineSeq2.UseVisualStyleBackColor = true;
            this.btnMachineSeq2.Visible = false;
            this.btnMachineSeq2.Click += new System.EventHandler(this.btnMachineSeq2_Click);
            // 
            // labMachineSeq
            // 
            this.labMachineSeq.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labMachineSeq.AutoSize = true;
            this.labMachineSeq.Font = new System.Drawing.Font("宋体", 20F);
            this.labMachineSeq.Location = new System.Drawing.Point(432, 21);
            this.labMachineSeq.Name = "labMachineSeq";
            this.labMachineSeq.Size = new System.Drawing.Size(80, 27);
            this.labMachineSeq.TabIndex = 5;
            this.labMachineSeq.Text = "通道1";
            // 
            // sendtasknum
            // 
            this.sendtasknum.DataPropertyName = "sendtasknum";
            this.sendtasknum.HeaderText = "任务包号";
            this.sendtasknum.Name = "sendtasknum";
            this.sendtasknum.ReadOnly = true;
            this.sendtasknum.Width = 80;
            // 
            // tasknum
            // 
            this.tasknum.DataPropertyName = "tasknum";
            this.tasknum.HeaderText = "订单任务号";
            this.tasknum.Name = "tasknum";
            this.tasknum.ReadOnly = true;
            this.tasknum.Width = 90;
            // 
            // sortnum
            // 
            this.sortnum.DataPropertyName = "sortnum";
            this.sortnum.HeaderText = "分拣任务号";
            this.sortnum.Name = "sortnum";
            this.sortnum.ReadOnly = true;
            this.sortnum.Width = 90;
            // 
            // customername
            // 
            this.customername.DataPropertyName = "customername";
            this.customername.HeaderText = "零售户";
            this.customername.Name = "customername";
            this.customername.ReadOnly = true;
            this.customername.Width = 270;
            // 
            // regioncode
            // 
            this.regioncode.DataPropertyName = "regioncode";
            this.regioncode.HeaderText = "车组号";
            this.regioncode.Name = "regioncode";
            this.regioncode.ReadOnly = true;
            this.regioncode.Width = 70;
            // 
            // TROUGHNUM
            // 
            this.TROUGHNUM.DataPropertyName = "TROUGHNUM";
            this.TROUGHNUM.HeaderText = "通道号";
            this.TROUGHNUM.Name = "TROUGHNUM";
            this.TROUGHNUM.ReadOnly = true;
            this.TROUGHNUM.Width = 70;
            // 
            // CIGARETTECODE
            // 
            this.CIGARETTECODE.DataPropertyName = "CIGARETTECODE";
            this.CIGARETTECODE.HeaderText = "卷烟编码";
            this.CIGARETTECODE.Name = "CIGARETTECODE";
            this.CIGARETTECODE.ReadOnly = true;
            this.CIGARETTECODE.Width = 80;
            // 
            // CIGARETTENAME
            // 
            this.CIGARETTENAME.DataPropertyName = "CIGARETTENAME";
            this.CIGARETTENAME.HeaderText = "卷烟名称";
            this.CIGARETTENAME.Name = "CIGARETTENAME";
            this.CIGARETTENAME.ReadOnly = true;
            this.CIGARETTENAME.Width = 120;
            // 
            // pokenum
            // 
            this.pokenum.DataPropertyName = "pokenum";
            this.pokenum.HeaderText = "数量";
            this.pokenum.Name = "pokenum";
            this.pokenum.ReadOnly = true;
            this.pokenum.Width = 40;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "分拣状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 90;
            // 
            // pokeid
            // 
            this.pokeid.DataPropertyName = "pokeid";
            this.pokeid.HeaderText = "流水号";
            this.pokeid.Name = "pokeid";
            this.pokeid.ReadOnly = true;
            this.pokeid.Width = 70;
            // 
            // PULLSTATUS
            // 
            this.PULLSTATUS.DataPropertyName = "PULLSTATUS";
            this.PULLSTATUS.HeaderText = "放烟标志";
            this.PULLSTATUS.Name = "PULLSTATUS";
            this.PULLSTATUS.ReadOnly = true;
            this.PULLSTATUS.Width = 90;
            // 
            // packagemachine
            // 
            this.packagemachine.DataPropertyName = "packmachineseq";
            this.packagemachine.HeaderText = "包装机";
            this.packagemachine.Name = "packagemachine";
            this.packagemachine.ReadOnly = true;
            this.packagemachine.Width = 70;
            // 
            // NowView_new
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1248, 584);
            this.Controls.Add(this.labMachineSeq);
            this.Controls.Add(this.btnMachineSeq2);
            this.Controls.Add(this.btnMachineSeq1);
            this.Controls.Add(this.btnNowPoke);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "NowView_new";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "混合道分拣详情";
            this.Deactivate += new System.EventHandler(this.NowView_Deactivate);
            this.Load += new System.EventHandler(this.NowView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvNowView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DgvNowView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnNowPoke;
        private System.Windows.Forms.Button btnMachineSeq1;
        private System.Windows.Forms.Button btnMachineSeq2;
        private System.Windows.Forms.Label labMachineSeq;
        private System.Windows.Forms.DataGridViewTextBoxColumn sendtasknum;
        private System.Windows.Forms.DataGridViewTextBoxColumn tasknum;
        private System.Windows.Forms.DataGridViewTextBoxColumn sortnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn customername;
        private System.Windows.Forms.DataGridViewTextBoxColumn regioncode;
        private System.Windows.Forms.DataGridViewTextBoxColumn TROUGHNUM;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIGARETTECODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIGARETTENAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn pokenum;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn pokeid;
        private System.Windows.Forms.DataGridViewTextBoxColumn PULLSTATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn packagemachine;
    }
}