namespace FollowTask
{
    partial class Fm_UinonCache
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fm_UinonCache));
            this.lblCacheText = new System.Windows.Forms.Label();
            this.listViewUnionCache = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvUnionCache = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnPokeTime = new System.Windows.Forms.Button();
            this.txtPokenum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnionCache)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCacheText
            // 
            this.lblCacheText.AutoSize = true;
            this.lblCacheText.Font = new System.Drawing.Font("宋体", 25F);
            this.lblCacheText.Location = new System.Drawing.Point(20, 9);
            this.lblCacheText.Name = "lblCacheText";
            this.lblCacheText.Size = new System.Drawing.Size(83, 34);
            this.lblCacheText.TabIndex = 0;
            this.lblCacheText.Text = "缓存";
            // 
            // listViewUnionCache
            // 
            this.listViewUnionCache.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listViewUnionCache.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewUnionCache.FullRowSelect = true;
            this.listViewUnionCache.GridLines = true;
            this.listViewUnionCache.Location = new System.Drawing.Point(3, 20);
            this.listViewUnionCache.Name = "listViewUnionCache";
            this.listViewUnionCache.Size = new System.Drawing.Size(549, 333);
            this.listViewUnionCache.TabIndex = 2;
            this.listViewUnionCache.UseCompatibleStateImageBehavior = false;
            this.listViewUnionCache.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "香烟编号";
            this.columnHeader1.Width = 119;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "香烟品牌";
            this.columnHeader2.Width = 145;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "任务号";
            this.columnHeader3.Width = 69;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "数量";
            this.columnHeader4.Width = 57;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "皮带";
            this.columnHeader5.Width = 45;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "通道编号";
            this.columnHeader6.Width = 97;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listViewUnionCache);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 11F);
            this.groupBox1.Location = new System.Drawing.Point(26, 110);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(555, 356);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "缓存信息";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(492, 61);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // dgvUnionCache
            // 
            this.dgvUnionCache.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnionCache.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.dgvUnionCache.Location = new System.Drawing.Point(286, 493);
            this.dgvUnionCache.MultiSelect = false;
            this.dgvUnionCache.Name = "dgvUnionCache";
            this.dgvUnionCache.ReadOnly = true;
            this.dgvUnionCache.RowHeadersVisible = false;
            this.dgvUnionCache.RowTemplate.Height = 23;
            this.dgvUnionCache.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUnionCache.Size = new System.Drawing.Size(232, 279);
            this.dgvUnionCache.TabIndex = 3;
            this.dgvUnionCache.Visible = false;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "cigrcode";
            this.Column1.HeaderText = "香烟编号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "cigrname";
            this.Column2.HeaderText = "香烟名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "pokenum";
            this.Column3.HeaderText = "数量";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 80;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "pokeid";
            this.Column4.HeaderText = "任务号";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "mianbelt";
            this.Column5.HeaderText = "主皮带";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 93;
            // 
            // btnPokeTime
            // 
            this.btnPokeTime.Location = new System.Drawing.Point(388, 61);
            this.btnPokeTime.Name = "btnPokeTime";
            this.btnPokeTime.Size = new System.Drawing.Size(75, 23);
            this.btnPokeTime.TabIndex = 5;
            this.btnPokeTime.Text = "抓";
            this.btnPokeTime.UseVisualStyleBackColor = true;
            this.btnPokeTime.Click += new System.EventHandler(this.btnPokeTime_Click);
            this.btnPokeTime.MouseEnter += new System.EventHandler(this.btnPokeTime_MouseEnter);
            // 
            // txtPokenum
            // 
            this.txtPokenum.Location = new System.Drawing.Point(264, 63);
            this.txtPokenum.Name = "txtPokenum";
            this.txtPokenum.Size = new System.Drawing.Size(100, 21);
            this.txtPokenum.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(205, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "抓数:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(99, 63);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "当前任务号:";
            // 
            // Fm_UinonCache
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 478);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPokenum);
            this.Controls.Add(this.btnPokeTime);
            this.Controls.Add(this.dgvUnionCache);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCacheText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Fm_UinonCache";
            this.Text = "Fm_UinonCache";
            this.Load += new System.EventHandler(this.Fm_UinonCache_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnionCache)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCacheText;
        private System.Windows.Forms.ListView listViewUnionCache;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvUnionCache;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button btnPokeTime;
        private System.Windows.Forms.TextBox txtPokenum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
    }
}