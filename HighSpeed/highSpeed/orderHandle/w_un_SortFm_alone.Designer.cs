namespace highSpeed.orderHandle
{
    partial class w_un_SortFm_alone
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
            this.dgvSortInfo = new System.Windows.Forms.DataGridView();
            this.btnPokeSeq = new System.Windows.Forms.Button();
            this.btnRef = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnVli = new System.Windows.Forms.Button();
            this.lblInFO = new System.Windows.Forms.Label();
            this.btnSort = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.TimerByTime = new System.Windows.Forms.Timer(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnTransfor = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSortInfo)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSortInfo
            // 
            this.dgvSortInfo.AllowUserToAddRows = false;
            this.dgvSortInfo.AllowUserToDeleteRows = false;
            this.dgvSortInfo.AllowUserToResizeColumns = false;
            this.dgvSortInfo.AllowUserToResizeRows = false;
            this.dgvSortInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSortInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvSortInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSortInfo.Location = new System.Drawing.Point(0, 54);
            this.dgvSortInfo.MultiSelect = false;
            this.dgvSortInfo.Name = "dgvSortInfo";
            this.dgvSortInfo.ReadOnly = true;
            this.dgvSortInfo.RowTemplate.Height = 23;
            this.dgvSortInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSortInfo.Size = new System.Drawing.Size(982, 383);
            this.dgvSortInfo.TabIndex = 13;
            this.dgvSortInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSortInfo_CellContentClick);
            this.dgvSortInfo.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvSortInfo_DataError_1);
            // 
            // btnPokeSeq
            // 
            this.btnPokeSeq.Location = new System.Drawing.Point(863, 13);
            this.btnPokeSeq.Name = "btnPokeSeq";
            this.btnPokeSeq.Size = new System.Drawing.Size(75, 23);
            this.btnPokeSeq.TabIndex = 4;
            this.btnPokeSeq.Text = "数据检验";
            this.btnPokeSeq.UseVisualStyleBackColor = true;
            this.btnPokeSeq.Click += new System.EventHandler(this.btnPokeSeq_Click);
            // 
            // btnRef
            // 
            this.btnRef.Location = new System.Drawing.Point(668, 13);
            this.btnRef.Name = "btnRef";
            this.btnRef.Size = new System.Drawing.Size(75, 23);
            this.btnRef.TabIndex = 3;
            this.btnRef.Text = "刷 新";
            this.btnRef.UseVisualStyleBackColor = true;
            this.btnRef.Click += new System.EventHandler(this.btnRef_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnTransfor);
            this.panel1.Controls.Add(this.btnVli);
            this.panel1.Controls.Add(this.btnPokeSeq);
            this.panel1.Controls.Add(this.btnRef);
            this.panel1.Controls.Add(this.lblInFO);
            this.panel1.Controls.Add(this.btnSort);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(982, 48);
            this.panel1.TabIndex = 11;
            // 
            // btnVli
            // 
            this.btnVli.Location = new System.Drawing.Point(352, 13);
            this.btnVli.Name = "btnVli";
            this.btnVli.Size = new System.Drawing.Size(75, 23);
            this.btnVli.TabIndex = 13;
            this.btnVli.Text = "校验长宽";
            this.btnVli.UseVisualStyleBackColor = true;
            this.btnVli.Click += new System.EventHandler(this.btnVli_Click);
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
            this.lblInFO.Visible = false;
            // 
            // btnSort
            // 
            this.btnSort.Font = new System.Drawing.Font("宋体", 9F);
            this.btnSort.Location = new System.Drawing.Point(768, 13);
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
            this.panel2.Location = new System.Drawing.Point(14, 75);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(797, 89);
            this.panel2.TabIndex = 12;
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
            // TimerByTime
            // 
            this.TimerByTime.Interval = 1000;
            this.TimerByTime.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Font = new System.Drawing.Font("宋体", 11F);
            this.panel4.Location = new System.Drawing.Point(526, 61);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(359, 100);
            this.panel4.TabIndex = 14;
            this.panel4.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(210, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 15);
            this.label3.TabIndex = 28;
            this.label3.Text = "数据校验中.....";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::highSpeed.Properties.Resources.loading;
            this.pictureBox1.Location = new System.Drawing.Point(-2, -2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(206, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // btnTransfor
            // 
            this.btnTransfor.Location = new System.Drawing.Point(454, 13);
            this.btnTransfor.Name = "btnTransfor";
            this.btnTransfor.Size = new System.Drawing.Size(75, 23);
            this.btnTransfor.TabIndex = 14;
            this.btnTransfor.Text = "订单拆分";
            this.btnTransfor.UseVisualStyleBackColor = true;
            this.btnTransfor.Click += new System.EventHandler(this.btnTransfor_Click);
            // 
            // w_un_SortFm_alone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 437);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvSortInfo);
            this.Name = "w_un_SortFm_alone";
            this.Text = "异型烟单独排程";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSortInfo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSortInfo;
        private System.Windows.Forms.Button btnPokeSeq;
        private System.Windows.Forms.Button btnRef;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblInFO;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer TimerByTime;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnVli;
        private System.Windows.Forms.Button btnTransfor;

    }
}