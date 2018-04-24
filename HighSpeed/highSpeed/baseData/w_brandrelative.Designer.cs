namespace highSpeed.baseData
{
    partial class win_brandrelative
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
            this.txt_keywd = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.box_type = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.codedata = new System.Windows.Forms.DataGridView();
            this.rownum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bigbox_bar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.codedata)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txt_keywd);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.box_type);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(567, 39);
            this.panel1.TabIndex = 0;
            // 
            // txt_keywd
            // 
            this.txt_keywd.Location = new System.Drawing.Point(175, 9);
            this.txt_keywd.Name = "txt_keywd";
            this.txt_keywd.Size = new System.Drawing.Size(100, 21);
            this.txt_keywd.TabIndex = 1;
            this.txt_keywd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_keywd_KeyDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(281, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // box_type
            // 
            this.box_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.box_type.FormattingEnabled = true;
            this.box_type.Location = new System.Drawing.Point(71, 10);
            this.box_type.Name = "box_type";
            this.box_type.Size = new System.Drawing.Size(98, 20);
            this.box_type.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "条件选择";
            // 
            // codedata
            // 
            this.codedata.AllowUserToAddRows = false;
            this.codedata.AllowUserToDeleteRows = false;
            this.codedata.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.codedata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.codedata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rownum,
            this.itemno,
            this.itemname,
            this.bigbox_bar});
            this.codedata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codedata.Location = new System.Drawing.Point(0, 39);
            this.codedata.Name = "codedata";
            this.codedata.RowTemplate.Height = 23;
            this.codedata.Size = new System.Drawing.Size(567, 223);
            this.codedata.TabIndex = 1;
            this.codedata.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.codedata_CellValueChanged);
            // 
            // rownum
            // 
            this.rownum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.rownum.DataPropertyName = "rownum";
            this.rownum.HeaderText = "序号";
            this.rownum.Name = "rownum";
            this.rownum.ReadOnly = true;
            this.rownum.Width = 54;
            // 
            // itemno
            // 
            this.itemno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.itemno.DataPropertyName = "itemno";
            this.itemno.HeaderText = "品牌代码";
            this.itemno.Name = "itemno";
            this.itemno.ReadOnly = true;
            this.itemno.Width = 78;
            // 
            // itemname
            // 
            this.itemname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.itemname.DataPropertyName = "itemname";
            this.itemname.HeaderText = "品牌名称";
            this.itemname.Name = "itemname";
            this.itemname.ReadOnly = true;
            this.itemname.Width = 78;
            // 
            // bigbox_bar
            // 
            this.bigbox_bar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.bigbox_bar.DataPropertyName = "bigbox_bar";
            this.bigbox_bar.HeaderText = "条码信息";
            this.bigbox_bar.Name = "bigbox_bar";
            this.bigbox_bar.Width = 78;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Location = new System.Drawing.Point(156, 107);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(411, 91);
            this.panel2.TabIndex = 2;
            this.panel2.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "正在读取数据...";
            this.label2.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(19, 49);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(380, 23);
            this.progressBar1.TabIndex = 0;
            this.progressBar1.Visible = false;
            // 
            // win_brandrelative
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 262);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.codedata);
            this.Controls.Add(this.panel1);
            this.Name = "win_brandrelative";
            this.Text = "品牌与条码挂接";
            this.Load += new System.EventHandler(this.win_brandrelative_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.codedata)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_keywd;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox box_type;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView codedata;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn rownum;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemno;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemname;
        private System.Windows.Forms.DataGridViewTextBoxColumn bigbox_bar;
    }
}