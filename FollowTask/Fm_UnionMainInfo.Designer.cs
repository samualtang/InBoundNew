namespace FollowTask
{
    partial class Fm_UnionMainInfo
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
            this.panelOption = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblOption = new System.Windows.Forms.Label();
            this.btnAllInfo = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.lblPlace = new System.Windows.Forms.Label();
            this.groupBoxUnionInfo = new System.Windows.Forms.GroupBox();
            this.panelCig = new System.Windows.Forms.Panel();
            this.dgbMainBeltInfo = new System.Windows.Forms.DataGridView();
            this.lblNum = new System.Windows.Forms.Label();
            this.lblSortnum = new System.Windows.Forms.Label();
            this.panelOption.SuspendLayout();
            this.groupBoxUnionInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgbMainBeltInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelOption
            // 
            this.panelOption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOption.Controls.Add(this.lblNum);
            this.panelOption.Controls.Add(this.lblSortnum);
            this.panelOption.Controls.Add(this.btnPrint);
            this.panelOption.Controls.Add(this.lblOption);
            this.panelOption.Controls.Add(this.btnAllInfo);
            this.panelOption.Controls.Add(this.btnNext);
            this.panelOption.Controls.Add(this.btnLast);
            this.panelOption.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOption.Location = new System.Drawing.Point(0, 0);
            this.panelOption.Name = "panelOption";
            this.panelOption.Size = new System.Drawing.Size(713, 47);
            this.panelOption.TabIndex = 1;
            // 
            // btnPrint
            // 
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrint.Location = new System.Drawing.Point(612, 11);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 19;
            this.btnPrint.Text = "打 印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lblOption
            // 
            this.lblOption.AutoSize = true;
            this.lblOption.Location = new System.Drawing.Point(11, 16);
            this.lblOption.Name = "lblOption";
            this.lblOption.Size = new System.Drawing.Size(47, 12);
            this.lblOption.TabIndex = 5;
            this.lblOption.Text = "操 作：";
            // 
            // btnAllInfo
            // 
            this.btnAllInfo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAllInfo.Location = new System.Drawing.Point(496, 11);
            this.btnAllInfo.Name = "btnAllInfo";
            this.btnAllInfo.Size = new System.Drawing.Size(75, 23);
            this.btnAllInfo.TabIndex = 4;
            this.btnAllInfo.Text = "所 有";
            this.btnAllInfo.UseVisualStyleBackColor = true;
            this.btnAllInfo.Click += new System.EventHandler(this.btnAllInfo_Click);
            // 
            // btnNext
            // 
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNext.Location = new System.Drawing.Point(165, 11);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "下一批";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLast.Location = new System.Drawing.Point(61, 11);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(75, 23);
            this.btnLast.TabIndex = 3;
            this.btnLast.Text = "上一批";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // lblPlace
            // 
            this.lblPlace.AutoSize = true;
            this.lblPlace.Location = new System.Drawing.Point(6, 104);
            this.lblPlace.Name = "lblPlace";
            this.lblPlace.Size = new System.Drawing.Size(65, 12);
            this.lblPlace.TabIndex = 6;
            this.lblPlace.Text = "当前位置：";
            // 
            // groupBoxUnionInfo
            // 
            this.groupBoxUnionInfo.Controls.Add(this.lblPlace);
            this.groupBoxUnionInfo.Controls.Add(this.panelCig);
            this.groupBoxUnionInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxUnionInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBoxUnionInfo.Location = new System.Drawing.Point(0, 47);
            this.groupBoxUnionInfo.Name = "groupBoxUnionInfo";
            this.groupBoxUnionInfo.Size = new System.Drawing.Size(713, 123);
            this.groupBoxUnionInfo.TabIndex = 2;
            this.groupBoxUnionInfo.TabStop = false;
            this.groupBoxUnionInfo.Text = "皮带";
            // 
            // panelCig
            // 
            this.panelCig.AutoScroll = true;
            this.panelCig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCig.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCig.Location = new System.Drawing.Point(3, 17);
            this.panelCig.Name = "panelCig";
            this.panelCig.Size = new System.Drawing.Size(707, 84);
            this.panelCig.TabIndex = 2;
            // 
            // dgbMainBeltInfo
            // 
            this.dgbMainBeltInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgbMainBeltInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgbMainBeltInfo.Location = new System.Drawing.Point(0, 170);
            this.dgbMainBeltInfo.Name = "dgbMainBeltInfo";
            this.dgbMainBeltInfo.RowTemplate.Height = 23;
            this.dgbMainBeltInfo.Size = new System.Drawing.Size(713, 237);
            this.dgbMainBeltInfo.TabIndex = 3;
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.Location = new System.Drawing.Point(385, 16);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(41, 12);
            this.lblNum.TabIndex = 23;
            this.lblNum.Text = "数量：";
            // 
            // lblSortnum
            // 
            this.lblSortnum.AutoSize = true;
            this.lblSortnum.Location = new System.Drawing.Point(283, 16);
            this.lblSortnum.Name = "lblSortnum";
            this.lblSortnum.Size = new System.Drawing.Size(53, 12);
            this.lblSortnum.TabIndex = 22;
            this.lblSortnum.Text = "任务号：";
            // 
            // Fm_UnionMainInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 407);
            this.Controls.Add(this.dgbMainBeltInfo);
            this.Controls.Add(this.groupBoxUnionInfo);
            this.Controls.Add(this.panelOption);
            this.Name = "Fm_UnionMainInfo";
            this.Text = "合流主皮带";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Fm_UnionMainInfo_FormClosing);
            this.Load += new System.EventHandler(this.Fm_UnionMainInfo_Load);
            this.panelOption.ResumeLayout(false);
            this.panelOption.PerformLayout();
            this.groupBoxUnionInfo.ResumeLayout(false);
            this.groupBoxUnionInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgbMainBeltInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelOption;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.GroupBox groupBoxUnionInfo;
        private System.Windows.Forms.Panel panelCig;
        private System.Windows.Forms.Label lblOption;
        private System.Windows.Forms.Button btnAllInfo;
        private System.Windows.Forms.DataGridView dgbMainBeltInfo;
        private System.Windows.Forms.Label lblPlace;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.Label lblSortnum;
    }
}