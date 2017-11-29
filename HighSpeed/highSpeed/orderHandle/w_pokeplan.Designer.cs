namespace highSpeed.orderHandle
{
    partial class win_pokeplan
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
            this.lab_showinfo = new System.Windows.Forms.Label();
            this.btn_poke = new System.Windows.Forms.Button();
            this.taskdata = new System.Windows.Forms.DataGridView();
            this.tasknum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.billcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.batchcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customercode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customername = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskquantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regioncode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btn_search = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.taskdata)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_search);
            this.panel1.Controls.Add(this.lab_showinfo);
            this.panel1.Controls.Add(this.btn_poke);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(877, 45);
            this.panel1.TabIndex = 0;
            // 
            // lab_showinfo
            // 
            this.lab_showinfo.AutoSize = true;
            this.lab_showinfo.Location = new System.Drawing.Point(197, 17);
            this.lab_showinfo.Name = "lab_showinfo";
            this.lab_showinfo.Size = new System.Drawing.Size(125, 12);
            this.lab_showinfo.TabIndex = 1;
            this.lab_showinfo.Text = "点击按钮生成拨烟计划";
            // 
            // btn_poke
            // 
            this.btn_poke.Location = new System.Drawing.Point(100, 12);
            this.btn_poke.Name = "btn_poke";
            this.btn_poke.Size = new System.Drawing.Size(75, 23);
            this.btn_poke.TabIndex = 0;
            this.btn_poke.Text = "生成拨烟计划";
            this.btn_poke.UseVisualStyleBackColor = true;
            this.btn_poke.Click += new System.EventHandler(this.btn_poke_Click);
            // 
            // taskdata
            // 
            this.taskdata.AllowUserToAddRows = false;
            this.taskdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.taskdata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tasknum,
            this.billcode,
            this.batchcode,
            this.customercode,
            this.customername,
            this.taskquantity,
            this.regioncode});
            this.taskdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskdata.Location = new System.Drawing.Point(0, 45);
            this.taskdata.Name = "taskdata";
            this.taskdata.ReadOnly = true;
            this.taskdata.RowTemplate.Height = 23;
            this.taskdata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.taskdata.Size = new System.Drawing.Size(877, 217);
            this.taskdata.TabIndex = 5;
            // 
            // tasknum
            // 
            this.tasknum.DataPropertyName = "tasknum";
            this.tasknum.HeaderText = "任务编号";
            this.tasknum.Name = "tasknum";
            this.tasknum.ReadOnly = true;
            this.tasknum.Width = 80;
            // 
            // billcode
            // 
            this.billcode.DataPropertyName = "billcode";
            this.billcode.HeaderText = "订单编号";
            this.billcode.Name = "billcode";
            this.billcode.ReadOnly = true;
            // 
            // batchcode
            // 
            this.batchcode.DataPropertyName = "batchcode";
            this.batchcode.HeaderText = "批次";
            this.batchcode.Name = "batchcode";
            this.batchcode.ReadOnly = true;
            // 
            // customercode
            // 
            this.customercode.DataPropertyName = "customercode";
            this.customercode.HeaderText = "专卖证号";
            this.customercode.Name = "customercode";
            this.customercode.ReadOnly = true;
            // 
            // customername
            // 
            this.customername.DataPropertyName = "customername";
            this.customername.HeaderText = "零售户";
            this.customername.Name = "customername";
            this.customername.ReadOnly = true;
            this.customername.Width = 200;
            // 
            // taskquantity
            // 
            this.taskquantity.DataPropertyName = "taskquantity";
            this.taskquantity.HeaderText = "任务数量";
            this.taskquantity.Name = "taskquantity";
            this.taskquantity.ReadOnly = true;
            // 
            // regioncode
            // 
            this.regioncode.DataPropertyName = "regioncode";
            this.regioncode.HeaderText = "配送车组";
            this.regioncode.Name = "regioncode";
            this.regioncode.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Location = new System.Drawing.Point(25, 87);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(826, 89);
            this.panel2.TabIndex = 6;
            this.panel2.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "正在生成拨烟计划...";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(33, 42);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(741, 23);
            this.progressBar1.TabIndex = 0;
            this.progressBar1.Visible = false;
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(19, 12);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 23);
            this.btn_search.TabIndex = 2;
            this.btn_search.Text = "查询";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // win_pokeplan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 262);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.taskdata);
            this.Controls.Add(this.panel1);
            this.Name = "win_pokeplan";
            this.Text = "拨烟计划";
            this.Load += new System.EventHandler(this.win_pokeplan_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.taskdata)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_poke;
        private System.Windows.Forms.Label lab_showinfo;
        private System.Windows.Forms.DataGridView taskdata;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridViewTextBoxColumn tasknum;
        private System.Windows.Forms.DataGridViewTextBoxColumn billcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn batchcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn customercode;
        private System.Windows.Forms.DataGridViewTextBoxColumn customername;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskquantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn regioncode;
        private System.Windows.Forms.Button btn_search;
    }
}