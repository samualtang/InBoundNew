namespace highSpeed.orderHandle
{
    partial class w_SortFm
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPokeSeq = new System.Windows.Forms.Button();
            this.btnRef = new System.Windows.Forms.Button();
            this.rdbUnUnionDan = new System.Windows.Forms.RadioButton();
            this.rdbUnionDan = new System.Windows.Forms.RadioButton();
            this.lblInFO = new System.Windows.Forms.Label();
            this.btnSort = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dgvSortInfo = new System.Windows.Forms.DataGridView();
            this.TimerByTime = new System.Windows.Forms.Timer(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblproseer = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.lblPokeSeq = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSortInfo)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnPokeSeq);
            this.panel1.Controls.Add(this.btnRef);
            this.panel1.Controls.Add(this.rdbUnUnionDan);
            this.panel1.Controls.Add(this.rdbUnionDan);
            this.panel1.Controls.Add(this.lblInFO);
            this.panel1.Controls.Add(this.btnSort);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(921, 48);
            this.panel1.TabIndex = 0;
            // 
            // btnPokeSeq
            // 
            this.btnPokeSeq.Location = new System.Drawing.Point(756, 12);
            this.btnPokeSeq.Name = "btnPokeSeq";
            this.btnPokeSeq.Size = new System.Drawing.Size(75, 23);
            this.btnPokeSeq.TabIndex = 4;
            this.btnPokeSeq.Text = "条烟顺序";
            this.btnPokeSeq.UseVisualStyleBackColor = true;
            this.btnPokeSeq.Click += new System.EventHandler(this.btnPokeSeq_Click);
            // 
            // btnRef
            // 
            this.btnRef.Location = new System.Drawing.Point(561, 12);
            this.btnRef.Name = "btnRef";
            this.btnRef.Size = new System.Drawing.Size(75, 23);
            this.btnRef.TabIndex = 3;
            this.btnRef.Text = "刷 新";
            this.btnRef.UseVisualStyleBackColor = true;
            this.btnRef.Click += new System.EventHandler(this.btnRef_Click);
            // 
            // rdbUnUnionDan
            // 
            this.rdbUnUnionDan.AutoSize = true;
            this.rdbUnUnionDan.Location = new System.Drawing.Point(484, 15);
            this.rdbUnUnionDan.Name = "rdbUnUnionDan";
            this.rdbUnUnionDan.Size = new System.Drawing.Size(59, 16);
            this.rdbUnUnionDan.TabIndex = 2;
            this.rdbUnUnionDan.TabStop = true;
            this.rdbUnUnionDan.Text = "不合单";
            this.rdbUnUnionDan.UseVisualStyleBackColor = true;
            // 
            // rdbUnionDan
            // 
            this.rdbUnionDan.AutoSize = true;
            this.rdbUnionDan.Location = new System.Drawing.Point(394, 15);
            this.rdbUnionDan.Name = "rdbUnionDan";
            this.rdbUnionDan.Size = new System.Drawing.Size(47, 16);
            this.rdbUnionDan.TabIndex = 2;
            this.rdbUnionDan.TabStop = true;
            this.rdbUnionDan.Text = "合单";
            this.rdbUnionDan.UseVisualStyleBackColor = true;
            // 
            // lblInFO
            // 
            this.lblInFO.AutoSize = true;
            this.lblInFO.Font = new System.Drawing.Font("宋体", 9F);
            this.lblInFO.Location = new System.Drawing.Point(12, 12);
            this.lblInFO.Name = "lblInFO";
            this.lblInFO.Size = new System.Drawing.Size(41, 12);
            this.lblInFO.TabIndex = 1;
            this.lblInFO.Text = "label1";
            // 
            // btnSort
            // 
            this.btnSort.Font = new System.Drawing.Font("宋体", 9F);
            this.btnSort.Location = new System.Drawing.Point(661, 12);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(75, 23);
            this.btnSort.TabIndex = 0;
            this.btnSort.Text = "排  程";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblTime);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Location = new System.Drawing.Point(12, 115);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(797, 89);
            this.panel2.TabIndex = 4;
            this.panel2.Visible = false;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("宋体", 11F);
            this.lblTime.Location = new System.Drawing.Point(31, 14);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(75, 15);
            this.lblTime.TabIndex = 2;
            this.lblTime.Text = "已用时间:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.Location = new System.Drawing.Point(184, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "正在排程......";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(33, 42);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(741, 23);
            this.progressBar1.TabIndex = 0;
            this.progressBar1.Visible = false;
            // 
            // dgvSortInfo
            // 
            this.dgvSortInfo.AllowUserToAddRows = false;
            this.dgvSortInfo.AllowUserToDeleteRows = false;
            this.dgvSortInfo.AllowUserToResizeColumns = false;
            this.dgvSortInfo.AllowUserToResizeRows = false;
            this.dgvSortInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvSortInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSortInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSortInfo.Location = new System.Drawing.Point(0, 48);
            this.dgvSortInfo.MultiSelect = false;
            this.dgvSortInfo.Name = "dgvSortInfo";
            this.dgvSortInfo.ReadOnly = true;
            this.dgvSortInfo.RowTemplate.Height = 23;
            this.dgvSortInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSortInfo.Size = new System.Drawing.Size(921, 618);
            this.dgvSortInfo.TabIndex = 5;
            this.dgvSortInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSortInfo_CellContentClick);
            this.dgvSortInfo.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvSortInfo_CellFormatting);
            this.dgvSortInfo.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvSortInfo_DataError);
            // 
            // TimerByTime
            // 
            this.TimerByTime.Interval = 1000;
            this.TimerByTime.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.lblproseer);
            this.panel3.Controls.Add(this.progressBar2);
            this.panel3.Controls.Add(this.lblPokeSeq);
            this.panel3.Font = new System.Drawing.Font("宋体", 11F);
            this.panel3.Location = new System.Drawing.Point(46, 69);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(359, 100);
            this.panel3.TabIndex = 6;
            this.panel3.Visible = false;
            // 
            // lblproseer
            // 
            this.lblproseer.AutoSize = true;
            this.lblproseer.Location = new System.Drawing.Point(11, 68);
            this.lblproseer.Name = "lblproseer";
            this.lblproseer.Size = new System.Drawing.Size(67, 15);
            this.lblproseer.TabIndex = 2;
            this.lblproseer.Text = "完成进度";
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(12, 34);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(293, 23);
            this.progressBar2.TabIndex = 1;
            // 
            // lblPokeSeq
            // 
            this.lblPokeSeq.AutoSize = true;
            this.lblPokeSeq.Location = new System.Drawing.Point(13, 8);
            this.lblPokeSeq.Name = "lblPokeSeq";
            this.lblPokeSeq.Size = new System.Drawing.Size(97, 15);
            this.lblPokeSeq.TabIndex = 0;
            this.lblPokeSeq.Text = "条烟顺序进度";
            // 
            // w_SortFm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 666);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dgvSortInfo);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "w_SortFm";
            this.Text = "任务排序";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.w_SortFm_FormClosing);
            this.Load += new System.EventHandler(this.w_SortFm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSortInfo)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblInFO;
        private System.Windows.Forms.DataGridView dgvSortInfo;
        private System.Windows.Forms.RadioButton rdbUnUnionDan;
        private System.Windows.Forms.RadioButton rdbUnionDan;
        private System.Windows.Forms.Button btnRef;
        private System.Windows.Forms.Timer TimerByTime;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnPokeSeq;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Label lblPokeSeq;
        private System.Windows.Forms.Label lblproseer;
    }
}