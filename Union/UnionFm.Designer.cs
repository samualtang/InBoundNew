﻿namespace SortingControlSys.SortingControl
{
    partial class UnionFm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnionFm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.task_data = new System.Windows.Forms.DataGridView();
            this.regioncode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regiondesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.boxcount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cuscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.finishqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.percent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.list_data = new System.Windows.Forms.ListBox();
            this.button12 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.task_data)).BeginInit();
            this.panel2.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(1038, 43);
            this.panel1.TabIndex = 35;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
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
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(300, 4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(101, 33);
            this.button6.TabIndex = 9;
            this.button6.Text = "修改状态";
            this.button6.UseVisualStyleBackColor = true;
          
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
            this.textBox1.Size = new System.Drawing.Size(100, 28);
            this.textBox1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(484, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
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
            // task_data
            // 
            this.task_data.AllowUserToAddRows = false;
            this.task_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.task_data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.regioncode,
            this.regiondesc,
            this.boxcount,
            this.cuscount,
            this.finishqty,
            this.percent});
            this.task_data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.task_data.Location = new System.Drawing.Point(0, 43);
            this.task_data.Name = "task_data";
            this.task_data.ReadOnly = true;
            this.task_data.RowTemplate.Height = 23;
            this.task_data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.task_data.Size = new System.Drawing.Size(1038, 376);
            this.task_data.TabIndex = 30;
            // 
            // regioncode
            // 
            this.regioncode.HeaderText = "线路编号";
            this.regioncode.Name = "regioncode";
            this.regioncode.ReadOnly = true;
            // 
            // regiondesc
            // 
            this.regiondesc.HeaderText = "线路名称";
            this.regiondesc.Name = "regiondesc";
            this.regiondesc.ReadOnly = true;
            // 
            // boxcount
            // 
            this.boxcount.HeaderText = "箱数";
            this.boxcount.Name = "boxcount";
            this.boxcount.ReadOnly = true;
            this.boxcount.Width = 200;
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
            this.panel2.Location = new System.Drawing.Point(0, 400);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1038, 19);
            this.panel2.TabIndex = 37;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(825, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 18);
            this.label5.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "提示信息";
            // 
            // list_data
            // 
            this.list_data.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.list_data.FormattingEnabled = true;
            this.list_data.ItemHeight = 18;
            this.list_data.Location = new System.Drawing.Point(0, 288);
            this.list_data.Name = "list_data";
            this.list_data.Size = new System.Drawing.Size(1038, 112);
            this.list_data.TabIndex = 39;
            // 
            // button12
            // 
            this.button12.BackgroundImage = global::Union.Properties.Resources.stop;
            this.button12.Location = new System.Drawing.Point(97, 0);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(99, 43);
            this.button12.TabIndex = 8;
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button11
            // 
            this.button11.BackgroundImage = global::Union.Properties.Resources.rfresh;
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
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 43);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // UnionFm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1038, 419);
            this.ControlBox = false;
            this.Controls.Add(this.list_data);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.task_data);
            this.Controls.Add(this.panel1);
            this.Name = "UnionFm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "卷烟信息";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.w_SortingControlMain_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.task_data)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private System.Windows.Forms.DataGridViewTextBoxColumn regioncode;
        private System.Windows.Forms.DataGridViewTextBoxColumn regiondesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn boxcount;
        private System.Windows.Forms.DataGridViewTextBoxColumn cuscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn finishqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn percent;
        private System.Windows.Forms.Button button6;
    }
}