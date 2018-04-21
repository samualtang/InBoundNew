namespace highSpeed
{
    partial class W_FenJTask
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
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.cmbSelectC = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblNo1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTask)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTask
            // 
            this.dgvTask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTask.Location = new System.Drawing.Point(12, 78);
            this.dgvTask.Name = "dgvTask";
            this.dgvTask.RowTemplate.Height = 23;
            this.dgvTask.Size = new System.Drawing.Size(865, 549);
            this.dgvTask.TabIndex = 9;
            // 
            // lblSelect
            // 
            this.lblSelect.AutoSize = true;
            this.lblSelect.Font = new System.Drawing.Font("宋体", 11F);
            this.lblSelect.Location = new System.Drawing.Point(30, 24);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(75, 15);
            this.lblSelect.TabIndex = 8;
            this.lblSelect.Text = "查询条件:";
            // 
            // txtInfo
            // 
            this.txtInfo.Font = new System.Drawing.Font("宋体", 11F);
            this.txtInfo.Location = new System.Drawing.Point(334, 22);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(142, 24);
            this.txtInfo.TabIndex = 7;
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("宋体", 11F);
            this.btnOk.Location = new System.Drawing.Point(791, 23);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "查 询";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // cmbSelectC
            // 
            this.cmbSelectC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectC.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbSelectC.FormattingEnabled = true;
            this.cmbSelectC.Location = new System.Drawing.Point(111, 21);
            this.cmbSelectC.Name = "cmbSelectC";
            this.cmbSelectC.Size = new System.Drawing.Size(140, 23);
            this.cmbSelectC.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 11F);
            this.textBox1.Location = new System.Drawing.Point(564, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(161, 24);
            this.textBox1.TabIndex = 10;
            // 
            // lblNo1
            // 
            this.lblNo1.AutoSize = true;
            this.lblNo1.Location = new System.Drawing.Point(287, 27);
            this.lblNo1.Name = "lblNo1";
            this.lblNo1.Size = new System.Drawing.Size(41, 12);
            this.lblNo1.TabIndex = 11;
            this.lblNo1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(517, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "label1";
            // 
            // W_FenJTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 646);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblNo1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dgvTask);
            this.Controls.Add(this.lblSelect);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cmbSelectC);
            this.Name = "W_FenJTask";
            this.Text = "W_FenJTask";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTask)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTask;
        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox cmbSelectC;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblNo1;
        private System.Windows.Forms.Label label2;
    }
}