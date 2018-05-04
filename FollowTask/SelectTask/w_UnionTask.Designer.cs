namespace FollowTask
{
    partial class w_UnionTask
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
            this.dgvTask = new System.Windows.Forms.DataGridView();
            this.lblSelect = new System.Windows.Forms.Label();
            this.txtInfo2 = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.cmbSelectC = new System.Windows.Forms.ComboBox();
            this.lblNo1 = new System.Windows.Forms.Label();
            this.txtinfo1 = new System.Windows.Forms.TextBox();
            this.lblNo2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTask)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTask
            // 
            this.dgvTask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTask.Location = new System.Drawing.Point(9, 71);
            this.dgvTask.Name = "dgvTask";
            this.dgvTask.RowTemplate.Height = 23;
            this.dgvTask.Size = new System.Drawing.Size(878, 559);
            this.dgvTask.TabIndex = 9;
            // 
            // lblSelect
            // 
            this.lblSelect.AutoSize = true;
            this.lblSelect.Font = new System.Drawing.Font("宋体", 11F);
            this.lblSelect.Location = new System.Drawing.Point(40, 27);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(75, 15);
            this.lblSelect.TabIndex = 8;
            this.lblSelect.Text = "查询条件:";
            // 
            // txtInfo2
            // 
            this.txtInfo2.Font = new System.Drawing.Font("宋体", 11F);
            this.txtInfo2.Location = new System.Drawing.Point(630, 23);
            this.txtInfo2.Name = "txtInfo2";
            this.txtInfo2.Size = new System.Drawing.Size(142, 24);
            this.txtInfo2.TabIndex = 7;
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("宋体", 11F);
            this.btnOk.Location = new System.Drawing.Point(812, 23);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "查 询";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cmbSelectC
            // 
            this.cmbSelectC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectC.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbSelectC.FormattingEnabled = true;
            this.cmbSelectC.Location = new System.Drawing.Point(121, 24);
            this.cmbSelectC.Name = "cmbSelectC";
            this.cmbSelectC.Size = new System.Drawing.Size(140, 23);
            this.cmbSelectC.TabIndex = 5;
            this.cmbSelectC.SelectedIndexChanged += new System.EventHandler(this.cmbSelectC_SelectedIndexChanged);
            // 
            // lblNo1
            // 
            this.lblNo1.AutoSize = true;
            this.lblNo1.Font = new System.Drawing.Font("宋体", 11F);
            this.lblNo1.Location = new System.Drawing.Point(276, 27);
            this.lblNo1.Name = "lblNo1";
            this.lblNo1.Size = new System.Drawing.Size(55, 15);
            this.lblNo1.TabIndex = 21;
            this.lblNo1.Text = "label1";
            // 
            // txtinfo1
            // 
            this.txtinfo1.Font = new System.Drawing.Font("宋体", 11F);
            this.txtinfo1.Location = new System.Drawing.Point(369, 24);
            this.txtinfo1.Name = "txtinfo1";
            this.txtinfo1.Size = new System.Drawing.Size(142, 24);
            this.txtinfo1.TabIndex = 17;
            // 
            // lblNo2
            // 
            this.lblNo2.AutoSize = true;
            this.lblNo2.Font = new System.Drawing.Font("宋体", 11F);
            this.lblNo2.Location = new System.Drawing.Point(554, 27);
            this.lblNo2.Name = "lblNo2";
            this.lblNo2.Size = new System.Drawing.Size(55, 15);
            this.lblNo2.TabIndex = 21;
            this.lblNo2.Text = "label1";
            // 
            // w_UnionTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 652);
            this.Controls.Add(this.lblNo2);
            this.Controls.Add(this.lblNo1);
            this.Controls.Add(this.txtinfo1);
            this.Controls.Add(this.dgvTask);
            this.Controls.Add(this.lblSelect);
            this.Controls.Add(this.txtInfo2);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cmbSelectC);
            this.Name = "w_UnionTask";
            this.Text = "w_UnionTask";
            this.Load += new System.EventHandler(this.w_UnionTask_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTask)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTask;
        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.TextBox txtInfo2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox cmbSelectC;
        private System.Windows.Forms.Label lblNo1;
        private System.Windows.Forms.TextBox txtinfo1;
        private System.Windows.Forms.Label lblNo2;
    }
}