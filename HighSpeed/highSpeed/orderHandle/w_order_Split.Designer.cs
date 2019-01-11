namespace highSpeed.orderHandle
{
    partial class w_order_Split
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
            this.txtNum = new System.Windows.Forms.TextBox();
            this.txtCutName = new System.Windows.Forms.TextBox();
            this.lblNum = new System.Windows.Forms.Label();
            this.lblCutName = new System.Windows.Forms.Label();
            this.lblinfo = new System.Windows.Forms.Label();
            this.btn_search = new System.Windows.Forms.Button();
            this.orderdata = new System.Windows.Forms.DataGridView();
            this.btnCheous = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.lblBcd = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.checkbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dpid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.count_hs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.order_qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.billcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderdata)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblBcd);
            this.panel1.Controls.Add(this.btnSelectAll);
            this.panel1.Controls.Add(this.btnCheous);
            this.panel1.Controls.Add(this.txtNum);
            this.panel1.Controls.Add(this.txtCutName);
            this.panel1.Controls.Add(this.lblNum);
            this.panel1.Controls.Add(this.lblCutName);
            this.panel1.Controls.Add(this.lblinfo);
            this.panel1.Controls.Add(this.btn_search);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(979, 61);
            this.panel1.TabIndex = 1;
            // 
            // txtNum
            // 
            this.txtNum.Location = new System.Drawing.Point(229, 15);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(82, 21);
            this.txtNum.TabIndex = 7;
            this.txtNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNum_KeyPress);
            // 
            // txtCutName
            // 
            this.txtCutName.Location = new System.Drawing.Point(70, 15);
            this.txtCutName.Name = "txtCutName";
            this.txtCutName.Size = new System.Drawing.Size(113, 21);
            this.txtCutName.TabIndex = 7;
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.Location = new System.Drawing.Point(194, 21);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(35, 12);
            this.lblNum.TabIndex = 6;
            this.lblNum.Text = "数量:";
            // 
            // lblCutName
            // 
            this.lblCutName.AutoSize = true;
            this.lblCutName.Location = new System.Drawing.Point(5, 19);
            this.lblCutName.Name = "lblCutName";
            this.lblCutName.Size = new System.Drawing.Size(59, 12);
            this.lblCutName.TabIndex = 5;
            this.lblCutName.Text = "客户名称:";
            // 
            // lblinfo
            // 
            this.lblinfo.AutoSize = true;
            this.lblinfo.Location = new System.Drawing.Point(3, 43);
            this.lblinfo.Name = "lblinfo";
            this.lblinfo.Size = new System.Drawing.Size(419, 12);
            this.lblinfo.TabIndex = 4;
            this.lblinfo.Text = "查询当前排程批次,异型烟数量大于数量的订单,选择需要拆分的订单,单击拆分";
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(334, 15);
            this.btn_search.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 22);
            this.btn_search.TabIndex = 3;
            this.btn_search.Text = "查询";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // orderdata
            // 
            this.orderdata.AllowUserToAddRows = false;
            this.orderdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.orderdata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkbox,
            this.Column1,
            this.dpid,
            this.count_hs,
            this.order_qty,
            this.billcode});
            this.orderdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderdata.Location = new System.Drawing.Point(0, 61);
            this.orderdata.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.orderdata.Name = "orderdata";
            this.orderdata.RowTemplate.Height = 23;
            this.orderdata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.orderdata.Size = new System.Drawing.Size(979, 443);
            this.orderdata.TabIndex = 2;
            this.orderdata.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.orderdata_CellEndEdit);
            // 
            // btnCheous
            // 
            this.btnCheous.Location = new System.Drawing.Point(658, 15);
            this.btnCheous.Name = "btnCheous";
            this.btnCheous.Size = new System.Drawing.Size(75, 23);
            this.btnCheous.TabIndex = 8;
            this.btnCheous.Text = "异型烟拆分";
            this.btnCheous.UseVisualStyleBackColor = true;
            this.btnCheous.Click += new System.EventHandler(this.btnCheous_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(568, 14);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 9;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // lblBcd
            // 
            this.lblBcd.AutoSize = true;
            this.lblBcd.Location = new System.Drawing.Point(566, 43);
            this.lblBcd.Name = "lblBcd";
            this.lblBcd.Size = new System.Drawing.Size(0, 12);
            this.lblBcd.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Location = new System.Drawing.Point(57, 100);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(826, 90);
            this.panel2.TabIndex = 11;
            this.panel2.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "正在读取数据...";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(33, 42);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(741, 22);
            this.progressBar1.TabIndex = 0;
            this.progressBar1.Visible = false;
            // 
            // checkbox
            // 
            this.checkbox.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
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
            // dpid
            // 
            this.dpid.DataPropertyName = "routecode";
            this.dpid.HeaderText = "车组信息";
            this.dpid.Name = "dpid";
            this.dpid.ReadOnly = true;
            // 
            // count_hs
            // 
            this.count_hs.DataPropertyName = "cutname";
            this.count_hs.HeaderText = "客户名称";
            this.count_hs.Name = "count_hs";
            this.count_hs.ReadOnly = true;
            this.count_hs.Width = 250;
            // 
            // order_qty
            // 
            this.order_qty.DataPropertyName = "order_qty";
            this.order_qty.HeaderText = "异形烟量";
            this.order_qty.Name = "order_qty";
            this.order_qty.ReadOnly = true;
            // 
            // billcode
            // 
            this.billcode.HeaderText = "订单编号";
            this.billcode.Name = "billcode";
            // 
            // w_order_Split
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 504);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.orderdata);
            this.Controls.Add(this.panel1);
            this.Name = "w_order_Split";
            this.Text = "订单拆分";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.w_order_Split_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.orderdata)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.TextBox txtCutName;
        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.Label lblCutName;
        private System.Windows.Forms.Label lblinfo;
        private System.Windows.Forms.Button btnCheous;
        private System.Windows.Forms.DataGridView orderdata;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Label lblBcd;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkbox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dpid;
        private System.Windows.Forms.DataGridViewTextBoxColumn count_hs;
        private System.Windows.Forms.DataGridViewTextBoxColumn order_qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn billcode;
    }
}