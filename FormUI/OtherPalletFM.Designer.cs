﻿namespace FormUI
{
    partial class OtherPalletFM
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
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.入库单号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.准运证号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.合同号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.货主 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.供应商 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.任务号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品牌名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.品牌代码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.计划数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.锁定数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.入库数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INBOUNDDETAILID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbcDuo = new System.Windows.Forms.CheckBox();
            this.tbRfid = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbChooseName = new System.Windows.Forms.TextBox();
            this.tbNum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.logList = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(795, 49);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(364, 17);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(50, 20);
            this.button2.TabIndex = 7;
            this.button2.Text = "查询";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(247, 16);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(114, 21);
            this.dateTimePicker2.TabIndex = 6;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(67, 15);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(114, 21);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "结束时间:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "开始时间:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Location = new System.Drawing.Point(8, 73);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(808, 165);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "入库单信息";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.入库单号,
            this.准运证号,
            this.合同号,
            this.货主,
            this.供应商,
            this.数量});
            this.dataGridView1.Location = new System.Drawing.Point(2, 16);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(802, 145);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // 入库单号
            // 
            this.入库单号.DataPropertyName = "INBOUNDID";
            this.入库单号.HeaderText = "入库单号";
            this.入库单号.Name = "入库单号";
            // 
            // 准运证号
            // 
            this.准运证号.DataPropertyName = "NAVICERT";
            this.准运证号.HeaderText = "准运证号";
            this.准运证号.Name = "准运证号";
            // 
            // 合同号
            // 
            this.合同号.DataPropertyName = "CONTRACTNO";
            this.合同号.HeaderText = "合同号";
            this.合同号.Name = "合同号";
            // 
            // 货主
            // 
            this.货主.DataPropertyName = "CONSIGNSOR";
            this.货主.HeaderText = "货主";
            this.货主.Name = "货主";
            // 
            // 供应商
            // 
            this.供应商.DataPropertyName = "SUPPLIER";
            this.供应商.HeaderText = "供应商";
            this.供应商.Name = "供应商";
            // 
            // 数量
            // 
            this.数量.DataPropertyName = "QTY";
            this.数量.HeaderText = "数量";
            this.数量.Name = "数量";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.任务号,
            this.品牌名称,
            this.品牌代码,
            this.计划数量,
            this.锁定数量,
            this.入库数量,
            this.INBOUNDDETAILID});
            this.dataGridView2.Location = new System.Drawing.Point(0, 18);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 30;
            this.dataGridView2.Size = new System.Drawing.Size(808, 205);
            this.dataGridView2.TabIndex = 3;
            this.dataGridView2.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView2_CellFormatting);
            this.dataGridView2.SelectionChanged += new System.EventHandler(this.dataGridView2_SelectionChanged);
            // 
            // 任务号
            // 
            this.任务号.DataPropertyName = "INBOUNDID";
            this.任务号.HeaderText = "入库单号";
            this.任务号.Name = "任务号";
            // 
            // 品牌名称
            // 
            this.品牌名称.DataPropertyName = "cigarettename";
            this.品牌名称.HeaderText = "品牌名称";
            this.品牌名称.Name = "品牌名称";
            // 
            // 品牌代码
            // 
            this.品牌代码.DataPropertyName = "barcode";
            this.品牌代码.HeaderText = "件烟码";
            this.品牌代码.Name = "品牌代码";
            // 
            // 计划数量
            // 
            this.计划数量.DataPropertyName = "boxqty";
            this.计划数量.HeaderText = "计划数量";
            this.计划数量.Name = "计划数量";
            // 
            // 锁定数量
            // 
            this.锁定数量.DataPropertyName = "lockqty";
            this.锁定数量.HeaderText = "锁定数量";
            this.锁定数量.Name = "锁定数量";
            // 
            // 入库数量
            // 
            this.入库数量.DataPropertyName = "aboxqty";
            this.入库数量.HeaderText = "入库数量";
            this.入库数量.Name = "入库数量";
            // 
            // INBOUNDDETAILID
            // 
            this.INBOUNDDETAILID.DataPropertyName = "INBOUNDDETAILID";
            this.INBOUNDDETAILID.HeaderText = "INBOUNDDETAILID";
            this.INBOUNDDETAILID.Name = "INBOUNDDETAILID";
            this.INBOUNDDETAILID.Visible = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dataGridView2);
            this.groupBox4.Location = new System.Drawing.Point(8, 295);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(812, 228);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "入库详细信息";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbcDuo);
            this.groupBox2.Controls.Add(this.tbRfid);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tbChooseName);
            this.groupBox2.Controls.Add(this.tbNum);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(10, 243);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(806, 49);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "入库操作";
            // 
            // cbcDuo
            // 
            this.cbcDuo.AutoSize = true;
            this.cbcDuo.Checked = true;
            this.cbcDuo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbcDuo.Location = new System.Drawing.Point(443, 13);
            this.cbcDuo.Margin = new System.Windows.Forms.Padding(2);
            this.cbcDuo.Name = "cbcDuo";
            this.cbcDuo.Size = new System.Drawing.Size(72, 16);
            this.cbcDuo.TabIndex = 15;
            this.cbcDuo.Text = "自动拆垛";
            this.cbcDuo.UseVisualStyleBackColor = true;
            // 
            // tbRfid
            // 
            this.tbRfid.Location = new System.Drawing.Point(278, 13);
            this.tbRfid.Margin = new System.Windows.Forms.Padding(2);
            this.tbRfid.Name = "tbRfid";
            this.tbRfid.Size = new System.Drawing.Size(75, 21);
            this.tbRfid.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(239, 17);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "RFID:";
            // 
            // tbChooseName
            // 
            this.tbChooseName.Location = new System.Drawing.Point(115, 15);
            this.tbChooseName.Margin = new System.Windows.Forms.Padding(2);
            this.tbChooseName.Name = "tbChooseName";
            this.tbChooseName.Size = new System.Drawing.Size(115, 21);
            this.tbChooseName.TabIndex = 9;
            // 
            // tbNum
            // 
            this.tbNum.ForeColor = System.Drawing.Color.Red;
            this.tbNum.Location = new System.Drawing.Point(75, 14);
            this.tbNum.Margin = new System.Windows.Forms.Padding(2);
            this.tbNum.Name = "tbNum";
            this.tbNum.Size = new System.Drawing.Size(37, 21);
            this.tbNum.TabIndex = 8;
            this.tbNum.Text = "30";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(12, 19);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "入库数量:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(365, 10);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 22);
            this.button1.TabIndex = 2;
            this.button1.Text = "入库";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // logList
            // 
            this.logList.FormattingEnabled = true;
            this.logList.ItemHeight = 12;
            this.logList.Location = new System.Drawing.Point(8, 528);
            this.logList.Name = "logList";
            this.logList.Size = new System.Drawing.Size(808, 88);
            this.logList.TabIndex = 6;
            // 
            // OtherPalletFM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 633);
            this.Controls.Add(this.logList);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "OtherPalletFM";
            this.Text = "托盘入库";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AutoInBoundFM_FormClosed);
            this.Load += new System.EventHandler(this.InBoundFM_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 入库单号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 准运证号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 合同号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 货主;
        private System.Windows.Forms.DataGridViewTextBoxColumn 供应商;
        private System.Windows.Forms.DataGridViewTextBoxColumn 数量;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbChooseName;
        private System.Windows.Forms.TextBox tbNum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbRfid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbcDuo;
        private System.Windows.Forms.DataGridViewTextBoxColumn 任务号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品牌名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品牌代码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 计划数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 锁定数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 入库数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn INBOUNDDETAILID;
        private System.Windows.Forms.ListBox logList;
    }
}