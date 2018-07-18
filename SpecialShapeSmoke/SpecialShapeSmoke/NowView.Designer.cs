namespace SpecialShapeSmoke
{
    partial class NowView
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
            this.TaskNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SortNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TroughNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIGARETTENAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PokeNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PokeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.TaskNum,
            this.SortNum,
            this.Code,
            this.TroughNum,
            this.CustomerName,
            this.CIGARETTENAME,
            this.PokeNum,
            this.status,
            this.PokeId});
            this.DgvNowView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvNowView.Location = new System.Drawing.Point(3, 17);
            this.DgvNowView.Name = "DgvNowView";
            this.DgvNowView.ReadOnly = true;
            this.DgvNowView.RowTemplate.Height = 23;
            this.DgvNowView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvNowView.Size = new System.Drawing.Size(1010, 451);
            this.DgvNowView.TabIndex = 0;
            this.DgvNowView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgvNowView_CellFormatting);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DgvNowView);
            this.groupBox1.Location = new System.Drawing.Point(4, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1016, 471);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btnNowPoke
            // 
            this.btnNowPoke.Location = new System.Drawing.Point(909, 7);
            this.btnNowPoke.Name = "btnNowPoke";
            this.btnNowPoke.Size = new System.Drawing.Size(75, 23);
            this.btnNowPoke.TabIndex = 2;
            this.btnNowPoke.Text = "定位当前";
            this.btnNowPoke.UseVisualStyleBackColor = true;
            this.btnNowPoke.Click += new System.EventHandler(this.btnNowPoke_Click);
            // 
            // btnMachineSeq1
            // 
            this.btnMachineSeq1.Location = new System.Drawing.Point(4, 7);
            this.btnMachineSeq1.Name = "btnMachineSeq1";
            this.btnMachineSeq1.Size = new System.Drawing.Size(75, 23);
            this.btnMachineSeq1.TabIndex = 3;
            this.btnMachineSeq1.Text = "通道1";
            this.btnMachineSeq1.UseVisualStyleBackColor = true;
            this.btnMachineSeq1.Click += new System.EventHandler(this.btnMachineSeq1_Click);
            // 
            // btnMachineSeq2
            // 
            this.btnMachineSeq2.Location = new System.Drawing.Point(85, 7);
            this.btnMachineSeq2.Name = "btnMachineSeq2";
            this.btnMachineSeq2.Size = new System.Drawing.Size(75, 23);
            this.btnMachineSeq2.TabIndex = 4;
            this.btnMachineSeq2.Text = "通道2";
            this.btnMachineSeq2.UseVisualStyleBackColor = true;
            this.btnMachineSeq2.Click += new System.EventHandler(this.btnMachineSeq2_Click);
            // 
            // labMachineSeq
            // 
            this.labMachineSeq.AutoSize = true;
            this.labMachineSeq.Location = new System.Drawing.Point(452, 12);
            this.labMachineSeq.Name = "labMachineSeq";
            this.labMachineSeq.Size = new System.Drawing.Size(35, 12);
            this.labMachineSeq.TabIndex = 5;
            this.labMachineSeq.Text = "通道1";
            // 
            // TaskNum
            // 
            this.TaskNum.DataPropertyName = "TaskNum";
            this.TaskNum.HeaderText = "订单任务号";
            this.TaskNum.Name = "TaskNum";
            this.TaskNum.ReadOnly = true;
            this.TaskNum.Width = 90;
            // 
            // SortNum
            // 
            this.SortNum.DataPropertyName = "SortNum";
            this.SortNum.HeaderText = "分拣任务号";
            this.SortNum.Name = "SortNum";
            this.SortNum.ReadOnly = true;
            this.SortNum.Width = 90;
            // 
            // Code
            // 
            this.Code.DataPropertyName = "regioncode";
            this.Code.HeaderText = "车组";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.Width = 60;
            // 
            // TroughNum
            // 
            this.TroughNum.DataPropertyName = "TroughNum";
            this.TroughNum.HeaderText = "通道号";
            this.TroughNum.Name = "TroughNum";
            this.TroughNum.ReadOnly = true;
            this.TroughNum.Width = 70;
            // 
            // CustomerName
            // 
            this.CustomerName.DataPropertyName = "CustomerName";
            this.CustomerName.HeaderText = "零售户";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            this.CustomerName.Width = 230;
            // 
            // CIGARETTENAME
            // 
            this.CIGARETTENAME.DataPropertyName = "CIGARETTENAME";
            this.CIGARETTENAME.HeaderText = "品牌名称";
            this.CIGARETTENAME.Name = "CIGARETTENAME";
            this.CIGARETTENAME.ReadOnly = true;
            this.CIGARETTENAME.Width = 150;
            // 
            // PokeNum
            // 
            this.PokeNum.DataPropertyName = "PokeNum";
            this.PokeNum.HeaderText = "数量";
            this.PokeNum.Name = "PokeNum";
            this.PokeNum.ReadOnly = true;
            this.PokeNum.Width = 60;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "分拣状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // PokeId
            // 
            this.PokeId.DataPropertyName = "PokeId";
            this.PokeId.HeaderText = "流水号";
            this.PokeId.Name = "PokeId";
            this.PokeId.ReadOnly = true;
            // 
            // NowView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 499);
            this.Controls.Add(this.labMachineSeq);
            this.Controls.Add(this.btnMachineSeq2);
            this.Controls.Add(this.btnMachineSeq1);
            this.Controls.Add(this.btnNowPoke);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "NowView";
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
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn SortNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn TroughNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIGARETTENAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn PokeNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn PokeId;
    }
}