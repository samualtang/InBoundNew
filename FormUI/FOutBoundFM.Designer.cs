﻿namespace FormUI
{
    partial class FOutBoundFM
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
            this.tbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbAdress = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbChooseName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.卷烟编码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.卷烟名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.储位编号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.创建时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(580, 73);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(397, 31);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(162, 28);
            this.tbName.TabIndex = 3;
            this.tbName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbCode_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(301, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "卷烟名称:";
            // 
            // tbCode
            // 
            this.tbCode.Location = new System.Drawing.Point(105, 32);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(173, 28);
            this.tbCode.TabIndex = 1;
            this.tbCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbCode_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "卷烟编号:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbAdress);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tbChooseName);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(627, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(603, 73);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "出库操作";
            // 
            // cbAdress
            // 
            this.cbAdress.FormattingEnabled = true;
            this.cbAdress.Items.AddRange(new object[] {
            "一楼一号出口",
            "一楼二号出口",
            "二楼一号出口"});
            this.cbAdress.Location = new System.Drawing.Point(97, 23);
            this.cbAdress.Name = "cbAdress";
            this.cbAdress.Size = new System.Drawing.Size(121, 26);
            this.cbAdress.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "出库口:";
            // 
            // tbChooseName
            // 
            this.tbChooseName.Location = new System.Drawing.Point(354, 27);
            this.tbChooseName.Name = "tbChooseName";
            this.tbChooseName.Size = new System.Drawing.Size(243, 28);
            this.tbChooseName.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(264, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 33);
            this.button1.TabIndex = 2;
            this.button1.Text = "出库";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Location = new System.Drawing.Point(12, 109);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1218, 306);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "卷烟信息";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.卷烟编码,
            this.卷烟名称,
            this.数量,
            this.储位编号,
            this.创建时间});
            this.dataGridView1.Location = new System.Drawing.Point(3, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(1209, 276);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // 卷烟编码
            // 
            this.卷烟编码.DataPropertyName = "cigarettecode";
            this.卷烟编码.HeaderText = "卷烟编码";
            this.卷烟编码.Name = "卷烟编码";
            // 
            // 卷烟名称
            // 
            this.卷烟名称.DataPropertyName = "cigarettename";
            this.卷烟名称.HeaderText = "卷烟名称";
            this.卷烟名称.Name = "卷烟名称";
            // 
            // 数量
            // 
            this.数量.DataPropertyName = "QTY";
            this.数量.HeaderText = "数量";
            this.数量.Name = "数量";
            // 
            // 储位编号
            // 
            this.储位编号.DataPropertyName = "CELLNO";
            this.储位编号.HeaderText = "储位编号";
            this.储位编号.Name = "储位编号";
            // 
            // 创建时间
            // 
            this.创建时间.DataPropertyName = "CREATETIME";
            this.创建时间.HeaderText = "创建时间";
            this.创建时间.Name = "创建时间";
            // 
            // FOutBoundFM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1260, 790);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FOutBoundFM";
            this.Text = "抽检出库";
            this.Load += new System.EventHandler(this.InBoundFM_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbChooseName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 卷烟编码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 卷烟名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 储位编号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 创建时间;
        private System.Windows.Forms.ComboBox cbAdress;
        private System.Windows.Forms.Label label3;
    }
}