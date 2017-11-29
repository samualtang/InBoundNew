namespace highSpeed.orderHandle
{
    partial class w_tuopanbuhuoplan
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
            this.btn_poke = new System.Windows.Forms.Button();
            this.taskdata = new System.Windows.Forms.DataGridView();
            this.code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.taskdata)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_poke);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(877, 45);
            this.panel1.TabIndex = 0;
            // 
            // btn_poke
            // 
            this.btn_poke.Location = new System.Drawing.Point(0, 17);
            this.btn_poke.Name = "btn_poke";
            this.btn_poke.Size = new System.Drawing.Size(119, 23);
            this.btn_poke.TabIndex = 0;
            this.btn_poke.Text = "生成托盘补货计划";
            this.btn_poke.UseVisualStyleBackColor = true;
            this.btn_poke.Click += new System.EventHandler(this.btn_poke_Click);
            // 
            // taskdata
            // 
            this.taskdata.AllowUserToAddRows = false;
            this.taskdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.taskdata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.code,
            this.Column1,
            this.Column2});
            this.taskdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.taskdata.Location = new System.Drawing.Point(0, 45);
            this.taskdata.Name = "taskdata";
            this.taskdata.ReadOnly = true;
            this.taskdata.RowTemplate.Height = 23;
            this.taskdata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.taskdata.Size = new System.Drawing.Size(877, 217);
            this.taskdata.TabIndex = 5;
            // 
            // code
            // 
            this.code.DataPropertyName = "cigarettecode";
            this.code.HeaderText = "品牌编码";
            this.code.Name = "code";
            this.code.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "cigarettename";
            this.Column1.HeaderText = "品牌名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "replenishqty";
            this.Column2.HeaderText = "数量";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // w_tuopanbuhuoplan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 262);
            this.Controls.Add(this.taskdata);
            this.Controls.Add(this.panel1);
            this.Name = "w_tuopanbuhuoplan";
            this.Text = "拨烟计划";
            this.Load += new System.EventHandler(this.win_pokeplan_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.taskdata)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_poke;
        private System.Windows.Forms.DataGridView taskdata;
        private System.Windows.Forms.DataGridViewTextBoxColumn code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}