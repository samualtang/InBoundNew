namespace highSpeed.baseData
{
    partial class win_package
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
            this.btn_new = new System.Windows.Forms.Button();
            this.btn_amend = new System.Windows.Forms.Button();
            this.btn_del = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.packagedata = new System.Windows.Forms.DataGridView();
            this.rownum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packageid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packagedesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packageval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.packagedata)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_close);
            this.panel1.Controls.Add(this.btn_del);
            this.panel1.Controls.Add(this.btn_amend);
            this.panel1.Controls.Add(this.btn_new);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(717, 45);
            this.panel1.TabIndex = 0;
            // 
            // btn_new
            // 
            this.btn_new.Location = new System.Drawing.Point(12, 11);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(75, 23);
            this.btn_new.TabIndex = 0;
            this.btn_new.Text = "新增";
            this.btn_new.UseVisualStyleBackColor = true;
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // btn_amend
            // 
            this.btn_amend.Location = new System.Drawing.Point(93, 11);
            this.btn_amend.Name = "btn_amend";
            this.btn_amend.Size = new System.Drawing.Size(75, 23);
            this.btn_amend.TabIndex = 1;
            this.btn_amend.Text = "修改";
            this.btn_amend.UseVisualStyleBackColor = true;
            this.btn_amend.Click += new System.EventHandler(this.btn_amend_Click);
            // 
            // btn_del
            // 
            this.btn_del.Location = new System.Drawing.Point(174, 11);
            this.btn_del.Name = "btn_del";
            this.btn_del.Size = new System.Drawing.Size(75, 23);
            this.btn_del.TabIndex = 2;
            this.btn_del.Text = "删除";
            this.btn_del.UseVisualStyleBackColor = true;
            this.btn_del.Click += new System.EventHandler(this.btn_del_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(255, 11);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 3;
            this.btn_close.Text = "关闭";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // packagedata
            // 
            this.packagedata.AllowUserToAddRows = false;
            this.packagedata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.packagedata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rownum,
            this.id,
            this.packageid,
            this.packagedesc,
            this.packageval});
            this.packagedata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packagedata.Location = new System.Drawing.Point(0, 45);
            this.packagedata.Name = "packagedata";
            this.packagedata.ReadOnly = true;
            this.packagedata.RowTemplate.Height = 23;
            this.packagedata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.packagedata.Size = new System.Drawing.Size(717, 217);
            this.packagedata.TabIndex = 1;
            this.packagedata.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.packagedata_CellClick);
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
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // packageid
            // 
            this.packageid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.packageid.DataPropertyName = "packageid";
            this.packageid.HeaderText = "类型编号";
            this.packageid.Name = "packageid";
            this.packageid.ReadOnly = true;
            this.packageid.Width = 78;
            // 
            // packagedesc
            // 
            this.packagedesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.packagedesc.DataPropertyName = "packagedesc";
            this.packagedesc.HeaderText = "类型描述";
            this.packagedesc.Name = "packagedesc";
            this.packagedesc.ReadOnly = true;
            this.packagedesc.Width = 78;
            // 
            // packageval
            // 
            this.packageval.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.packageval.DataPropertyName = "packageval";
            this.packageval.HeaderText = "包装量";
            this.packageval.Name = "packageval";
            this.packageval.ReadOnly = true;
            this.packageval.Width = 66;
            // 
            // win_package
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 262);
            this.Controls.Add(this.packagedata);
            this.Controls.Add(this.panel1);
            this.Name = "win_package";
            this.Text = "包装类型";
            this.Load += new System.EventHandler(this.win_package_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.packagedata)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_del;
        private System.Windows.Forms.Button btn_amend;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.DataGridView packagedata;
        private System.Windows.Forms.DataGridViewTextBoxColumn rownum;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn packageid;
        private System.Windows.Forms.DataGridViewTextBoxColumn packagedesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn packageval;
    }
}