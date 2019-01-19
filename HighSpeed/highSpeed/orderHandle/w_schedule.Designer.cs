namespace highSpeed.orderHandle
{
    partial class win_schedule
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
            this.btn_sixschedule = new System.Windows.Forms.Button();
            this.btn_all = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_search = new System.Windows.Forms.Button();
            this.txt_codestr = new System.Windows.Forms.TextBox();
            this.btn_schedule = new System.Windows.Forms.Button();
            this.lab_showinfo = new System.Windows.Forms.Label();
            this.orderdata = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.checkbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regioncode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cuscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sixordercount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sixorderqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderdata)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_sixschedule);
            this.panel1.Controls.Add(this.btn_all);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btn_search);
            this.panel1.Controls.Add(this.txt_codestr);
            this.panel1.Controls.Add(this.btn_schedule);
            this.panel1.Controls.Add(this.lab_showinfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1118, 47);
            this.panel1.TabIndex = 0;
            // 
            // btn_sixschedule
            // 
            this.btn_sixschedule.Location = new System.Drawing.Point(824, 11);
            this.btn_sixschedule.Name = "btn_sixschedule";
            this.btn_sixschedule.Size = new System.Drawing.Size(124, 23);
            this.btn_sixschedule.TabIndex = 15;
            this.btn_sixschedule.Text = "六三六拆分预排程";
            this.btn_sixschedule.UseVisualStyleBackColor = true;
            this.btn_sixschedule.Click += new System.EventHandler(this.btn_sixschedule_Click);
            // 
            // btn_all
            // 
            this.btn_all.Location = new System.Drawing.Point(643, 11);
            this.btn_all.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_all.Name = "btn_all";
            this.btn_all.Size = new System.Drawing.Size(75, 22);
            this.btn_all.TabIndex = 14;
            this.btn_all.Text = "全选";
            this.btn_all.UseVisualStyleBackColor = true;
            this.btn_all.Click += new System.EventHandler(this.btn_all_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1031, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "设置排序";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(562, 11);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 23);
            this.btn_search.TabIndex = 10;
            this.btn_search.Text = "查询";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // txt_codestr
            // 
            this.txt_codestr.Location = new System.Drawing.Point(1055, 13);
            this.txt_codestr.Name = "txt_codestr";
            this.txt_codestr.Size = new System.Drawing.Size(60, 21);
            this.txt_codestr.TabIndex = 9;
            this.txt_codestr.Visible = false;
            // 
            // btn_schedule
            // 
            this.btn_schedule.Location = new System.Drawing.Point(724, 11);
            this.btn_schedule.Name = "btn_schedule";
            this.btn_schedule.Size = new System.Drawing.Size(75, 23);
            this.btn_schedule.TabIndex = 5;
            this.btn_schedule.Text = "预排程";
            this.btn_schedule.UseVisualStyleBackColor = true;
            this.btn_schedule.Click += new System.EventHandler(this.btn_schedule_Click);
            // 
            // lab_showinfo
            // 
            this.lab_showinfo.AutoSize = true;
            this.lab_showinfo.Location = new System.Drawing.Point(12, 16);
            this.lab_showinfo.Name = "lab_showinfo";
            this.lab_showinfo.Size = new System.Drawing.Size(509, 12);
            this.lab_showinfo.TabIndex = 2;
            this.lab_showinfo.Text = "勾选要排程的订单数据，点击“排程”按钮，进行排程操作。排程的先后顺序由勾选顺序决定。";
            // 
            // orderdata
            // 
            this.orderdata.AllowUserToAddRows = false;
            this.orderdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.orderdata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkbox,
            this.Column1,
            this.regioncode,
            this.cuscount,
            this.qty,
            this.sixordercount,
            this.sixorderqty});
            this.orderdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderdata.Location = new System.Drawing.Point(0, 47);
            this.orderdata.MultiSelect = false;
            this.orderdata.Name = "orderdata";
            this.orderdata.RowTemplate.Height = 23;
            this.orderdata.Size = new System.Drawing.Size(1118, 215);
            this.orderdata.TabIndex = 1;
            this.orderdata.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.orderdata_CellContentClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Location = new System.Drawing.Point(146, 87);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(826, 89);
            this.panel2.TabIndex = 3;
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
            this.progressBar1.Size = new System.Drawing.Size(741, 23);
            this.progressBar1.TabIndex = 0;
            this.progressBar1.Visible = false;
            // 
            // checkbox
            // 
            this.checkbox.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.checkbox.FalseValue = "false";
            this.checkbox.HeaderText = "选择";
            this.checkbox.Name = "checkbox";
            this.checkbox.TrueValue = "true";
            this.checkbox.Width = 35;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "rownum";
            this.Column1.HeaderText = "序号";
            this.Column1.Name = "Column1";
            this.Column1.Width = 80;
            // 
            // regioncode
            // 
            this.regioncode.DataPropertyName = "regioncode";
            this.regioncode.HeaderText = "车组CODE";
            this.regioncode.Name = "regioncode";
            this.regioncode.ReadOnly = true;
            // 
            // cuscount
            // 
            this.cuscount.DataPropertyName = "cuscount";
            this.cuscount.HeaderText = "订货户数";
            this.cuscount.Name = "cuscount";
            this.cuscount.ReadOnly = true;
            // 
            // qty
            // 
            this.qty.DataPropertyName = "qty";
            this.qty.HeaderText = "订货数量";
            this.qty.Name = "qty";
            this.qty.ReadOnly = true;
            // 
            // sixordercount
            // 
            this.sixordercount.DataPropertyName = "sixordercount";
            this.sixordercount.HeaderText = "六三六户数";
            this.sixordercount.Name = "sixordercount";
            // 
            // sixorderqty
            // 
            this.sixorderqty.DataPropertyName = "sixorderqty";
            this.sixorderqty.HeaderText = "六三六总条数";
            this.sixorderqty.Name = "sixorderqty";
            this.sixorderqty.Width = 120;
            // 
            // win_schedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 262);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.orderdata);
            this.Controls.Add(this.panel1);
            this.Name = "win_schedule";
            this.Text = "预排程";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.win_schedule_FormClosing);
            this.Load += new System.EventHandler(this.win_schedule_Load);
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
        private System.Windows.Forms.Button btn_schedule;
        private System.Windows.Forms.Label lab_showinfo;
        private System.Windows.Forms.TextBox txt_codestr;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_all;
        private System.Windows.Forms.Button btn_sixschedule;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkbox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn regioncode;
        private System.Windows.Forms.DataGridViewTextBoxColumn cuscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn sixordercount;
        private System.Windows.Forms.DataGridViewTextBoxColumn sixorderqty;
    }
}