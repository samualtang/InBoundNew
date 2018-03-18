namespace FormUI
{
    partial class InBoundFM
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
            this.cbAddress = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbRfid = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbcDuo = new System.Windows.Forms.CheckBox();
            this.tbChooseName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tbNum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.件烟码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.卷烟名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.卷烟编码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logList = new System.Windows.Forms.ListBox();
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
            this.groupBox1.Location = new System.Drawing.Point(10, 83);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(353, 49);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(225, 21);
            this.tbName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(99, 21);
            this.tbName.TabIndex = 3;
            this.tbName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbCode_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(161, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "卷烟名称:";
            // 
            // tbCode
            // 
            this.tbCode.Location = new System.Drawing.Point(70, 21);
            this.tbCode.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(89, 21);
            this.tbCode.TabIndex = 1;
            this.tbCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbCode_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "卷烟编号:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbAddress);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tbRfid);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbcDuo);
            this.groupBox2.Controls.Add(this.tbChooseName);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.tbNum);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(10, 8);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(754, 49);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "入库操作";
            // 
            // cbAddress
            // 
            this.cbAddress.FormattingEnabled = true;
            this.cbAddress.Items.AddRange(new object[] {
            "楼下一号入口",
            "楼下二号入口",
            "楼上一号入口"});
            this.cbAddress.Location = new System.Drawing.Point(69, 17);
            this.cbAddress.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbAddress.Name = "cbAddress";
            this.cbAddress.Size = new System.Drawing.Size(82, 20);
            this.cbAddress.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 22);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "入口地址:";
            // 
            // tbRfid
            // 
            this.tbRfid.Location = new System.Drawing.Point(277, 19);
            this.tbRfid.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbRfid.Name = "tbRfid";
            this.tbRfid.Size = new System.Drawing.Size(68, 21);
            this.tbRfid.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(247, 22);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "RFID:";
            // 
            // cbcDuo
            // 
            this.cbcDuo.AutoSize = true;
            this.cbcDuo.Location = new System.Drawing.Point(360, 21);
            this.cbcDuo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbcDuo.Name = "cbcDuo";
            this.cbcDuo.Size = new System.Drawing.Size(72, 16);
            this.cbcDuo.TabIndex = 4;
            this.cbcDuo.Text = "能否拆垛";
            this.cbcDuo.UseVisualStyleBackColor = true;
            // 
            // tbChooseName
            // 
            this.tbChooseName.Location = new System.Drawing.Point(489, 17);
            this.tbChooseName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbChooseName.Name = "tbChooseName";
            this.tbChooseName.Size = new System.Drawing.Size(163, 21);
            this.tbChooseName.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(435, 15);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 22);
            this.button1.TabIndex = 2;
            this.button1.Text = "入库";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbNum
            // 
            this.tbNum.ForeColor = System.Drawing.Color.Red;
            this.tbNum.Location = new System.Drawing.Point(209, 19);
            this.tbNum.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbNum.Name = "tbNum";
            this.tbNum.Size = new System.Drawing.Size(35, 21);
            this.tbNum.TabIndex = 1;
            this.tbNum.Text = "30";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(169, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "数量:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Location = new System.Drawing.Point(8, 144);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(812, 204);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "卷烟信息";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.件烟码,
            this.卷烟名称,
            this.卷烟编码});
            this.dataGridView1.Location = new System.Drawing.Point(2, 17);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(806, 184);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // 件烟码
            // 
            this.件烟码.DataPropertyName = "bigbox_bar";
            this.件烟码.HeaderText = "件烟码";
            this.件烟码.Name = "件烟码";
            // 
            // 卷烟名称
            // 
            this.卷烟名称.DataPropertyName = "itemname";
            this.卷烟名称.HeaderText = "卷烟名称";
            this.卷烟名称.Name = "卷烟名称";
            // 
            // 卷烟编码
            // 
            this.卷烟编码.DataPropertyName = "itemno";
            this.卷烟编码.HeaderText = "卷烟编码";
            this.卷烟编码.Name = "卷烟编码";
            // 
            // logList
            // 
            this.logList.FormattingEnabled = true;
            this.logList.ItemHeight = 12;
            this.logList.Location = new System.Drawing.Point(8, 354);
            this.logList.Name = "logList";
            this.logList.Size = new System.Drawing.Size(808, 112);
            this.logList.TabIndex = 3;
            // 
            // InBoundFM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 499);
            this.Controls.Add(this.logList);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "InBoundFM";
            this.Text = "成品入库";
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbChooseName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbNum;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox cbcDuo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbRfid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn 件烟码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 卷烟名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 卷烟编码;
        private System.Windows.Forms.ListBox logList;
    }
}