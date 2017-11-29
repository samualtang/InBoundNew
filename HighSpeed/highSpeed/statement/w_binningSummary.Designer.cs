namespace highSpeed.statement
{
    partial class win_binningSummary
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
            this.btn_search = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePick = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.box_sortline = new System.Windows.Forms.ComboBox();
            this.binningdata = new System.Windows.Forms.DataGridView();
            this.czxx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packagedesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.binningdata)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_search);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.dateTimePick);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.box_sortline);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(609, 45);
            this.panel1.TabIndex = 0;
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(325, 10);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 23);
            this.btn_search.TabIndex = 4;
            this.btn_search.Text = "查询";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "日期";
            // 
            // dateTimePick
            // 
            this.dateTimePick.CustomFormat = "yyyy-MM-dd";
            this.dateTimePick.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePick.Location = new System.Drawing.Point(48, 11);
            this.dateTimePick.Name = "dateTimePick";
            this.dateTimePick.Size = new System.Drawing.Size(105, 21);
            this.dateTimePick.TabIndex = 2;
            this.dateTimePick.Value = new System.DateTime(2013, 10, 29, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(159, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "分拣线";
            // 
            // box_sortline
            // 
            this.box_sortline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.box_sortline.FormattingEnabled = true;
            this.box_sortline.Location = new System.Drawing.Point(206, 12);
            this.box_sortline.Name = "box_sortline";
            this.box_sortline.Size = new System.Drawing.Size(103, 20);
            this.box_sortline.TabIndex = 0;
            this.box_sortline.SelectedIndexChanged += new System.EventHandler(this.box_sortline_SelectedIndexChanged);
            // 
            // binningdata
            // 
            this.binningdata.AllowUserToAddRows = false;
            this.binningdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.binningdata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.czxx,
            this.packagedesc,
            this.sl});
            this.binningdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.binningdata.Location = new System.Drawing.Point(0, 45);
            this.binningdata.MultiSelect = false;
            this.binningdata.Name = "binningdata";
            this.binningdata.ReadOnly = true;
            this.binningdata.RowTemplate.Height = 23;
            this.binningdata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.binningdata.Size = new System.Drawing.Size(609, 217);
            this.binningdata.TabIndex = 1;
            // 
            // czxx
            // 
            this.czxx.DataPropertyName = "czxx";
            this.czxx.HeaderText = "配送线路号";
            this.czxx.Name = "czxx";
            this.czxx.ReadOnly = true;
            // 
            // packagedesc
            // 
            this.packagedesc.DataPropertyName = "packagedesc";
            this.packagedesc.HeaderText = "周转箱类型";
            this.packagedesc.Name = "packagedesc";
            this.packagedesc.ReadOnly = true;
            // 
            // sl
            // 
            this.sl.DataPropertyName = "sl";
            this.sl.HeaderText = "数量";
            this.sl.Name = "sl";
            this.sl.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Location = new System.Drawing.Point(99, 86);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(411, 91);
            this.panel2.TabIndex = 3;
            this.panel2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "正在读取数据...";
            this.label3.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(19, 49);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(380, 23);
            this.progressBar1.TabIndex = 0;
            this.progressBar1.Visible = false;
            // 
            // win_binningSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 262);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.binningdata);
            this.Controls.Add(this.panel1);
            this.Name = "win_binningSummary";
            this.Text = "装箱汇总";
            this.Load += new System.EventHandler(this.w_binningSummary_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.binningdata)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView binningdata;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox box_sortline;
        private System.Windows.Forms.DateTimePicker dateTimePick;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.DataGridViewTextBoxColumn czxx;
        private System.Windows.Forms.DataGridViewTextBoxColumn packagedesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn sl;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}