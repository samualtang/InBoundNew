﻿namespace highSpeed.orderHandle
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblInFO = new System.Windows.Forms.Label();
            this.btnSort = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dgvSortInfo = new System.Windows.Forms.DataGridView();
            this.pager1 = new WHC.Pager.WinControl.Pager();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSortInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblInFO);
            this.panel1.Controls.Add(this.btnSort);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(921, 48);
            this.panel1.TabIndex = 0;
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
            this.btnSort.Location = new System.Drawing.Point(724, 11);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(75, 23);
            this.btnSort.TabIndex = 0;
            this.btnSort.Text = "排  程";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Location = new System.Drawing.Point(12, 115);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(797, 89);
            this.panel2.TabIndex = 4;
            this.panel2.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "正在读取数据...";
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
            this.dgvSortInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSortInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSortInfo.Location = new System.Drawing.Point(0, 48);
            this.dgvSortInfo.Name = "dgvSortInfo";
            this.dgvSortInfo.RowTemplate.Height = 23;
            this.dgvSortInfo.Size = new System.Drawing.Size(921, 618);
            this.dgvSortInfo.TabIndex = 5;
            // 
            // pager1
            // 
            this.pager1.CurrentPageIndex = 1;
            this.pager1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pager1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pager1.Location = new System.Drawing.Point(0, 621);
            this.pager1.Name = "pager1";
            this.pager1.RecordCount = 0;
            this.pager1.Size = new System.Drawing.Size(921, 45);
            this.pager1.TabIndex = 6;
            this.pager1.Visible = false;
            // 
            // w_SortFm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 666);
            this.Controls.Add(this.pager1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dgvSortInfo);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "w_SortFm";
            this.Text = "任务排序";
            this.Load += new System.EventHandler(this.w_SortFm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSortInfo)).EndInit();
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
        private WHC.Pager.WinControl.Pager pager1;
    }
}