﻿namespace FormUI
{
    partial class InBoundNoReport
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
            this.件烟码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.计划数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.锁定数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.入库数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INBOUNDDETAILID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.logList = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox4.SuspendLayout();
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
            this.button2.Location = new System.Drawing.Point(719, 16);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(68, 29);
            this.button2.TabIndex = 7;
            this.button2.Text = "查询";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(324, 16);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(131, 21);
            this.dateTimePicker2.TabIndex = 6;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(131, 15);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(133, 21);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(268, 19);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "结束时间:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 21);
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
            this.groupBox3.Size = new System.Drawing.Size(795, 165);
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
            this.dataGridView1.Size = new System.Drawing.Size(785, 145);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
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
            this.件烟码,
            this.计划数量,
            this.锁定数量,
            this.入库数量,
            this.INBOUNDDETAILID});
            this.dataGridView2.Location = new System.Drawing.Point(0, 18);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 30;
            this.dataGridView2.Size = new System.Drawing.Size(787, 227);
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
            // 件烟码
            // 
            this.件烟码.DataPropertyName = "barcode";
            this.件烟码.HeaderText = "件烟码";
            this.件烟码.Name = "件烟码";
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
            this.groupBox4.Location = new System.Drawing.Point(11, 242);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(795, 228);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "入库详细信息";
            // 
            // logList
            // 
            this.logList.FormattingEnabled = true;
            this.logList.ItemHeight = 12;
            this.logList.Location = new System.Drawing.Point(8, 530);
            this.logList.Name = "logList";
            this.logList.Size = new System.Drawing.Size(787, 112);
            this.logList.TabIndex = 6;
            // 
            // InBoundNoReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 650);
            this.Controls.Add(this.logList);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "InBoundNoReport";
            this.Text = "入库单据查询";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AutoInBoundFM_FormClosed);
            this.Load += new System.EventHandler(this.InBoundFM_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox4.ResumeLayout(false);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn 任务号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 品牌名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 件烟码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 计划数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 锁定数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 入库数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn INBOUNDDETAILID;
        private System.Windows.Forms.DataGridViewTextBoxColumn 入库单号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 准运证号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 合同号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 货主;
        private System.Windows.Forms.DataGridViewTextBoxColumn 供应商;
        private System.Windows.Forms.DataGridViewTextBoxColumn 数量;
        private System.Windows.Forms.ListBox logList;
    }
}