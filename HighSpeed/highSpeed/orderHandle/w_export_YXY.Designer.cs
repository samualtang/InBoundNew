namespace highSpeed.orderHandle
{
    partial class w_export_YXY
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
            this.button1 = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_export = new System.Windows.Forms.Button();
            this.lab_showinfo = new System.Windows.Forms.Label();
            this.orderdata = new System.Windows.Forms.DataGridView();
            this.batchcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cuscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderdata)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btn_close);
            this.panel1.Controls.Add(this.btn_export);
            this.panel1.Controls.Add(this.lab_showinfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1144, 52);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1042, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "刷新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(874, 18);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 7;
            this.btn_close.Text = "关闭";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_export
            // 
            this.btn_export.Location = new System.Drawing.Point(793, 18);
            this.btn_export.Name = "btn_export";
            this.btn_export.Size = new System.Drawing.Size(75, 23);
            this.btn_export.TabIndex = 6;
            this.btn_export.Text = "发送";
            this.btn_export.UseVisualStyleBackColor = true;
            this.btn_export.Click += new System.EventHandler(this.btn_export_Click);
            // 
            // lab_showinfo
            // 
            this.lab_showinfo.AutoSize = true;
            this.lab_showinfo.Location = new System.Drawing.Point(12, 23);
            this.lab_showinfo.Name = "lab_showinfo";
            this.lab_showinfo.Size = new System.Drawing.Size(533, 12);
            this.lab_showinfo.TabIndex = 3;
            this.lab_showinfo.Text = "选择批次订单数据，点击“发送”按钮，可以将批次(异型烟)订单信息导出并自动发送到一号工程。";
            // 
            // orderdata
            // 
            this.orderdata.AllowUserToAddRows = false;
            this.orderdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.orderdata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.batchcode,
            this.cuscount,
            this.qty});
            this.orderdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderdata.Location = new System.Drawing.Point(0, 52);
            this.orderdata.MultiSelect = false;
            this.orderdata.Name = "orderdata";
            this.orderdata.RowTemplate.Height = 23;
            this.orderdata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.orderdata.Size = new System.Drawing.Size(1144, 210);
            this.orderdata.TabIndex = 1;
            // 
            // batchcode
            // 
            this.batchcode.DataPropertyName = "batchcode";
            this.batchcode.HeaderText = "批次编号";
            this.batchcode.Name = "batchcode";
            this.batchcode.ReadOnly = true;
            this.batchcode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.batchcode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cuscount
            // 
            this.cuscount.DataPropertyName = "cuscount";
            this.cuscount.HeaderText = "订货户数";
            this.cuscount.Name = "cuscount";
            this.cuscount.ReadOnly = true;
            this.cuscount.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cuscount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // qty
            // 
            this.qty.DataPropertyName = "qty";
            this.qty.HeaderText = "订货数量";
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            this.qty.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.qty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Location = new System.Drawing.Point(44, 179);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(663, 89);
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
            this.progressBar1.Size = new System.Drawing.Size(597, 23);
            this.progressBar1.TabIndex = 0;
            this.progressBar1.Visible = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "";
            this.dateTimePicker1.Location = new System.Drawing.Point(623, 18);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(121, 21);
            this.dateTimePicker1.TabIndex = 12;
            this.dateTimePicker1.Value = new System.DateTime(2019, 8, 8, 21, 8, 16, 0);
            // 
            // w_export_YXY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 262);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.orderdata);
            this.Controls.Add(this.panel1);
            this.Name = "w_export_YXY";
            this.Text = "异形烟_任务导出";
            this.Load += new System.EventHandler(this.win_export_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderdata)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView orderdata;
        private System.Windows.Forms.Label lab_showinfo;
        private System.Windows.Forms.Button btn_export;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.DataGridViewTextBoxColumn batchcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn cuscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}