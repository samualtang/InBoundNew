﻿namespace LabelPrint
{
    partial class QueryForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pager1 = new WHC.Pager.WinControl.Pager();
            this.ItemList = new System.Windows.Forms.DataGridView();
            this.iTEMNODataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iTEMNAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sHORTNAMEDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bIGBOXBARDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operate = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tWMSITEMBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tbCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbjyCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbNum = new System.Windows.Forms.TextBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tWMSITEMBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.tbNum);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tbjyCode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbCode);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.tbName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(28, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1055, 139);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "请输入查询条件";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(469, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "卷烟编码:";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(844, 29);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 36);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "搜索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(136, 35);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(296, 28);
            this.tbName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "卷烟名称:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pager1);
            this.groupBox2.Controls.Add(this.ItemList);
            this.groupBox2.Location = new System.Drawing.Point(28, 157);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1055, 800);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "结果集";
            // 
            // pager1
            // 
            this.pager1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pager1.CurrentPageIndex = 1;
            this.pager1.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.pager1.Location = new System.Drawing.Point(6, 585);
            this.pager1.Name = "pager1";
            this.pager1.RecordCount = 0;
            this.pager1.Size = new System.Drawing.Size(970, 65);
            this.pager1.TabIndex = 1;
            this.pager1.UseWaitCursor = true;
            this.pager1.Load += new System.EventHandler(this.pager1_Load);
            // 
            // ItemList
            // 
            this.ItemList.AutoGenerateColumns = false;
            this.ItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ItemList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iTEMNODataGridViewTextBoxColumn,
            this.iTEMNAMEDataGridViewTextBoxColumn,
            this.sHORTNAMEDataGridViewTextBoxColumn,
            this.bIGBOXBARDataGridViewTextBoxColumn,
            this.ItemCount,
            this.Operate});
            this.ItemList.DataSource = this.tWMSITEMBindingSource;
            this.ItemList.Location = new System.Drawing.Point(6, 42);
            this.ItemList.Name = "ItemList";
            this.ItemList.RowTemplate.Height = 30;
            this.ItemList.Size = new System.Drawing.Size(970, 537);
            this.ItemList.TabIndex = 0;
            this.ItemList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemList_CellContentClick);
            // 
            // iTEMNODataGridViewTextBoxColumn
            // 
            this.iTEMNODataGridViewTextBoxColumn.DataPropertyName = "ITEMNO";
            this.iTEMNODataGridViewTextBoxColumn.HeaderText = "商品编码";
            this.iTEMNODataGridViewTextBoxColumn.Name = "iTEMNODataGridViewTextBoxColumn";
            this.iTEMNODataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iTEMNAMEDataGridViewTextBoxColumn
            // 
            this.iTEMNAMEDataGridViewTextBoxColumn.DataPropertyName = "ITEMNAME";
            this.iTEMNAMEDataGridViewTextBoxColumn.HeaderText = "商品名称";
            this.iTEMNAMEDataGridViewTextBoxColumn.Name = "iTEMNAMEDataGridViewTextBoxColumn";
            this.iTEMNAMEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // sHORTNAMEDataGridViewTextBoxColumn
            // 
            this.sHORTNAMEDataGridViewTextBoxColumn.DataPropertyName = "SHORTNAME";
            this.sHORTNAMEDataGridViewTextBoxColumn.HeaderText = "简称";
            this.sHORTNAMEDataGridViewTextBoxColumn.Name = "sHORTNAMEDataGridViewTextBoxColumn";
            this.sHORTNAMEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bIGBOXBARDataGridViewTextBoxColumn
            // 
            this.bIGBOXBARDataGridViewTextBoxColumn.DataPropertyName = "BIGBOX_BAR";
            this.bIGBOXBARDataGridViewTextBoxColumn.HeaderText = "件烟码";
            this.bIGBOXBARDataGridViewTextBoxColumn.Name = "bIGBOXBARDataGridViewTextBoxColumn";
            this.bIGBOXBARDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ItemCount
            // 
            dataGridViewCellStyle3.NullValue = "1";
            this.ItemCount.DefaultCellStyle = dataGridViewCellStyle3;
            this.ItemCount.HeaderText = "打印份数";
            this.ItemCount.Name = "ItemCount";
            this.ItemCount.ToolTipText = "请输入打印份数";
            // 
            // Operate
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.NullValue = "打印";
            this.Operate.DefaultCellStyle = dataGridViewCellStyle4;
            this.Operate.HeaderText = "打印";
            this.Operate.Name = "Operate";
            this.Operate.Text = "打印";
            this.Operate.UseColumnTextForButtonValue = true;
            // 
            // tWMSITEMBindingSource
            // 
            this.tWMSITEMBindingSource.DataSource = typeof(LabelPrintBusiness.T_WMS_ITEM);
            // 
            // tbCode
            // 
            this.tbCode.Location = new System.Drawing.Point(606, 35);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(165, 28);
            this.tbCode.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "件烟编码:";
            // 
            // tbjyCode
            // 
            this.tbjyCode.Location = new System.Drawing.Point(136, 87);
            this.tbjyCode.Name = "tbjyCode";
            this.tbjyCode.Size = new System.Drawing.Size(296, 28);
            this.tbjyCode.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(505, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "数量:";
            // 
            // tbNum
            // 
            this.tbNum.Location = new System.Drawing.Point(606, 79);
            this.tbNum.Name = "tbNum";
            this.tbNum.Size = new System.Drawing.Size(165, 28);
            this.tbNum.TabIndex = 8;
            this.tbNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tBox_KeyPress);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(844, 87);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 36);
            this.btnPrint.TabIndex = 9;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // QueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1343, 944);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "QueryForm";
            this.Text = "件烟条码打印";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ItemList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tWMSITEMBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView ItemList;
        private System.Windows.Forms.BindingSource tWMSITEMBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNODataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iTEMNAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sHORTNAMEDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bIGBOXBARDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCount;
        private System.Windows.Forms.DataGridViewButtonColumn Operate;
        private WHC.Pager.WinControl.Pager pager1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TextBox tbNum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbjyCode;
        private System.Windows.Forms.Label label3;
    }
}

