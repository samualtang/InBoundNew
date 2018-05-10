namespace SortingControlSys.SortingControl
{
    partial class w_SortingControlMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(w_SortingControlMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnClearB = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.task_data = new System.Windows.Forms.DataGridView();
            this.nums = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.machine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exponum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mainbelt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pagemachine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cuscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.finishqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.percent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.list_data = new System.Windows.Forms.ListBox();
            this.listError = new System.Windows.Forms.ListBox();
            this.Timerinitdata = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.task_data)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1038, 48);
            this.panel1.TabIndex = 35;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel3.Controls.Add(this.btnSelect);
            this.panel3.Controls.Add(this.btnClearB);
            this.panel3.Controls.Add(this.btnClear);
            this.panel3.Controls.Add(this.button6);
            this.panel3.Controls.Add(this.button12);
            this.panel3.Controls.Add(this.button11);
            this.panel3.Controls.Add(this.button10);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1038, 43);
            this.panel3.TabIndex = 36;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(616, 0);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(101, 43);
            this.btnSelect.TabIndex = 13;
            this.btnSelect.Text = "查 询";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnClearB
            // 
            this.btnClearB.BackgroundImage = global::SortingControlSys.Properties.Resources.stoptwo;
            this.btnClearB.Location = new System.Drawing.Point(404, -1);
            this.btnClearB.Name = "btnClearB";
            this.btnClearB.Size = new System.Drawing.Size(99, 44);
            this.btnClearB.TabIndex = 12;
            this.btnClearB.UseVisualStyleBackColor = true;
            this.btnClearB.Click += new System.EventHandler(this.btnClearB_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackgroundImage = global::SortingControlSys.Properties.Resources.stopone;
            this.btnClear.Location = new System.Drawing.Point(299, -1);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(99, 43);
            this.btnClear.TabIndex = 11;
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // button6
            // 
            this.button6.BackgroundImage = global::SortingControlSys.Properties.Resources.edit;
            this.button6.Location = new System.Drawing.Point(509, 0);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(101, 42);
            this.button6.TabIndex = 9;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // button12
            // 
            this.button12.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button12.BackgroundImage")));
            this.button12.Location = new System.Drawing.Point(97, 0);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(99, 43);
            this.button12.TabIndex = 8;
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button11
            // 
            this.button11.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button11.BackgroundImage")));
            this.button11.Location = new System.Drawing.Point(194, 0);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(99, 43);
            this.button11.TabIndex = 7;
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button10
            // 
            this.button10.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button10.BackgroundImage")));
            this.button10.Location = new System.Drawing.Point(0, 0);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(99, 43);
            this.button10.TabIndex = 0;
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(953, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 6;
            this.button5.Text = "导出";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(782, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "打印";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(692, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "清空条件";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(531, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(484, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "品名：";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(872, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 43);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // task_data
            // 
            this.task_data.AllowUserToAddRows = false;
            this.task_data.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 11F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.task_data.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.task_data.ColumnHeadersHeight = 35;
            this.task_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nums,
            this.groupno,
            this.machine,
            this.exponum,
            this.mainbelt,
            this.pagemachine,
            this.cuscount,
            this.finishqty,
            this.percent});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 11F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.task_data.DefaultCellStyle = dataGridViewCellStyle2;
            this.task_data.Location = new System.Drawing.Point(0, 48);
            this.task_data.Name = "task_data";
            this.task_data.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 11F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.task_data.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.task_data.RowTemplate.Height = 40;
            this.task_data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.task_data.Size = new System.Drawing.Size(592, 237);
            this.task_data.TabIndex = 30;
            // 
            // nums
            // 
            this.nums.HeaderText = "序号";
            this.nums.Name = "nums";
            this.nums.ReadOnly = true;
            // 
            // groupno
            // 
            this.groupno.HeaderText = "组号";
            this.groupno.Name = "groupno";
            this.groupno.ReadOnly = true;
            // 
            // machine
            // 
            this.machine.HeaderText = "烟柜数";
            this.machine.Name = "machine";
            this.machine.ReadOnly = true;
            // 
            // exponum
            // 
            this.exponum.HeaderText = "出口号";
            this.exponum.Name = "exponum";
            this.exponum.ReadOnly = true;
            // 
            // mainbelt
            // 
            this.mainbelt.HeaderText = "主皮带";
            this.mainbelt.Name = "mainbelt";
            this.mainbelt.ReadOnly = true;
            // 
            // pagemachine
            // 
            this.pagemachine.HeaderText = "包装机";
            this.pagemachine.Name = "pagemachine";
            this.pagemachine.ReadOnly = true;
            // 
            // cuscount
            // 
            this.cuscount.HeaderText = "客户数";
            this.cuscount.Name = "cuscount";
            this.cuscount.ReadOnly = true;
            this.cuscount.Width = 200;
            // 
            // finishqty
            // 
            this.finishqty.HeaderText = "完成量";
            this.finishqty.Name = "finishqty";
            this.finishqty.ReadOnly = true;
            this.finishqty.Width = 200;
            // 
            // percent
            // 
            this.percent.HeaderText = "完成百分比";
            this.percent.Name = "percent";
            this.percent.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 391);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1038, 28);
            this.panel2.TabIndex = 37;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(825, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 12);
            this.label5.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "提示信息";
            // 
            // list_data
            // 
            this.list_data.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.list_data.FormattingEnabled = true;
            this.list_data.ItemHeight = 12;
            this.list_data.Location = new System.Drawing.Point(0, 291);
            this.list_data.Name = "list_data";
            this.list_data.Size = new System.Drawing.Size(1038, 100);
            this.list_data.TabIndex = 39;
            // 
            // listError
            // 
            this.listError.BackColor = System.Drawing.SystemColors.Window;
            this.listError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listError.FormattingEnabled = true;
            this.listError.ItemHeight = 15;
            this.listError.Location = new System.Drawing.Point(3, 20);
            this.listError.Name = "listError";
            this.listError.Size = new System.Drawing.Size(434, 220);
            this.listError.TabIndex = 40;
            this.listError.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listError_DrawItem);
            this.listError.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.listError_MeasureItem);
            // 
            // Timerinitdata
            // 
            this.Timerinitdata.Interval = 1000;
            this.Timerinitdata.Tick += new System.EventHandler(this.Timerinitdata_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listError);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 11F);
            this.groupBox1.Location = new System.Drawing.Point(598, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(440, 243);
            this.groupBox1.TabIndex = 41;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "故障信息";
            // 
            // w_SortingControlMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1038, 419);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.list_data);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.task_data);
            this.Controls.Add(this.panel1);
            this.Name = "w_SortingControlMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "卷烟信息";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.w_SortingControlMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.w_SortingControlMain_FormClosed);
            this.SizeChanged += new System.EventHandler(this.w_SortingControlMain_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.task_data)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView task_data;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox list_data;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClearB;
        private System.Windows.Forms.ListBox listError;
        private System.Windows.Forms.Timer Timerinitdata;
        private System.Windows.Forms.DataGridViewTextBoxColumn nums;
        private System.Windows.Forms.DataGridViewTextBoxColumn groupno;
        private System.Windows.Forms.DataGridViewTextBoxColumn machine;
        private System.Windows.Forms.DataGridViewTextBoxColumn exponum;
        private System.Windows.Forms.DataGridViewTextBoxColumn mainbelt;
        private System.Windows.Forms.DataGridViewTextBoxColumn pagemachine;
        private System.Windows.Forms.DataGridViewTextBoxColumn cuscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn finishqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn percent;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSelect;
    }
}