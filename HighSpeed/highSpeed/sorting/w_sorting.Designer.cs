namespace highSpeed.sorting
{
    partial class win_sorting
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
            this.list_data = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_test = new System.Windows.Forms.Button();
            this.taskdata = new System.Windows.Forms.DataGridView();
            this.xlmc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bzxx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.percent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.taskdata)).BeginInit();
            this.SuspendLayout();
            // 
            // list_data
            // 
            this.list_data.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.list_data.FormattingEnabled = true;
            this.list_data.ItemHeight = 12;
            this.list_data.Location = new System.Drawing.Point(0, 162);
            this.list_data.Name = "list_data";
            this.list_data.Size = new System.Drawing.Size(941, 100);
            this.list_data.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_test);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(941, 46);
            this.panel1.TabIndex = 1;
            // 
            // btn_test
            // 
            this.btn_test.Location = new System.Drawing.Point(52, 12);
            this.btn_test.Name = "btn_test";
            this.btn_test.Size = new System.Drawing.Size(75, 23);
            this.btn_test.TabIndex = 0;
            this.btn_test.Text = "测试";
            this.btn_test.UseVisualStyleBackColor = true;
            this.btn_test.Click += new System.EventHandler(this.btn_test_Click);
            // 
            // taskdata
            // 
            this.taskdata.AllowUserToAddRows = false;
            this.taskdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.taskdata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.xlmc,
            this.bzxx,
            this.percent});
            this.taskdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskdata.Location = new System.Drawing.Point(0, 46);
            this.taskdata.Name = "taskdata";
            this.taskdata.RowTemplate.Height = 23;
            this.taskdata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.taskdata.Size = new System.Drawing.Size(941, 116);
            this.taskdata.TabIndex = 2;
            // 
            // xlmc
            // 
            this.xlmc.DataPropertyName = "xlmc";
            this.xlmc.HeaderText = "线路CODE";
            this.xlmc.Name = "xlmc";
            // 
            // bzxx
            // 
            this.bzxx.DataPropertyName = "bzxx";
            this.bzxx.HeaderText = "线路名称";
            this.bzxx.Name = "bzxx";
            // 
            // percent
            // 
            this.percent.HeaderText = "完成情况";
            this.percent.Name = "percent";
            // 
            // win_sorting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 262);
            this.Controls.Add(this.taskdata);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.list_data);
            this.Name = "win_sorting";
            this.Text = "高速分拣机分拣情况";
            this.Load += new System.EventHandler(this.w_sorting_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.taskdata)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox list_data;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_test;
        private System.Windows.Forms.DataGridView taskdata;
        private System.Windows.Forms.DataGridViewTextBoxColumn xlmc;
        private System.Windows.Forms.DataGridViewTextBoxColumn bzxx;
        private System.Windows.Forms.DataGridViewTextBoxColumn percent;
    }
}