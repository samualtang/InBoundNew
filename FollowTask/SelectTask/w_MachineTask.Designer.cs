namespace FollowTask
{
    partial class w_MachineTask
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
            this.cmbSelectC = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.lblSelect = new System.Windows.Forms.Label();
            this.dgvTask = new System.Windows.Forms.DataGridView();
            this.lblNo2 = new System.Windows.Forms.Label();
            this.lblNo1 = new System.Windows.Forms.Label();
            this.txtinfo2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTask)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbSelectC
            // 
            this.cmbSelectC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectC.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbSelectC.FormattingEnabled = true;
            this.cmbSelectC.Location = new System.Drawing.Point(96, 28);
            this.cmbSelectC.Name = "cmbSelectC";
            this.cmbSelectC.Size = new System.Drawing.Size(140, 23);
            this.cmbSelectC.TabIndex = 0;
            this.cmbSelectC.SelectedIndexChanged += new System.EventHandler(this.cmbSelectC_SelectedIndexChanged);
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("宋体", 11F);
            this.btnOk.Location = new System.Drawing.Point(815, 30);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "查 询";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtInfo
            // 
            this.txtInfo.Font = new System.Drawing.Font("宋体", 11F);
            this.txtInfo.Location = new System.Drawing.Point(329, 27);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(142, 24);
            this.txtInfo.TabIndex = 2;
            // 
            // lblSelect
            // 
            this.lblSelect.AutoSize = true;
            this.lblSelect.Font = new System.Drawing.Font("宋体", 11F);
            this.lblSelect.Location = new System.Drawing.Point(15, 31);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(75, 15);
            this.lblSelect.TabIndex = 3;
            this.lblSelect.Text = "查询条件:";
            // 
            // dgvTask
            // 
            this.dgvTask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTask.Location = new System.Drawing.Point(12, 90);
            this.dgvTask.Name = "dgvTask";
            this.dgvTask.RowTemplate.Height = 23;
            this.dgvTask.Size = new System.Drawing.Size(878, 547);
            this.dgvTask.TabIndex = 4;
            // 
            // lblNo2
            // 
            this.lblNo2.AutoSize = true;
            this.lblNo2.Font = new System.Drawing.Font("宋体", 11F);
            this.lblNo2.Location = new System.Drawing.Point(510, 30);
            this.lblNo2.Name = "lblNo2";
            this.lblNo2.Size = new System.Drawing.Size(55, 15);
            this.lblNo2.TabIndex = 14;
            this.lblNo2.Text = "label1";
            // 
            // lblNo1
            // 
            this.lblNo1.AutoSize = true;
            this.lblNo1.Font = new System.Drawing.Font("宋体", 11F);
            this.lblNo1.Location = new System.Drawing.Point(260, 34);
            this.lblNo1.Name = "lblNo1";
            this.lblNo1.Size = new System.Drawing.Size(55, 15);
            this.lblNo1.TabIndex = 15;
            this.lblNo1.Text = "label1";
            // 
            // txtinfo2
            // 
            this.txtinfo2.Font = new System.Drawing.Font("宋体", 11F);
            this.txtinfo2.Location = new System.Drawing.Point(602, 27);
            this.txtinfo2.Name = "txtinfo2";
            this.txtinfo2.Size = new System.Drawing.Size(161, 24);
            this.txtinfo2.TabIndex = 13;
            // 
            // w_MachineTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 651);
            this.Controls.Add(this.lblNo2);
            this.Controls.Add(this.lblNo1);
            this.Controls.Add(this.txtinfo2);
            this.Controls.Add(this.dgvTask);
            this.Controls.Add(this.lblSelect);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cmbSelectC);
            this.Name = "w_MachineTask";
            this.Load += new System.EventHandler(this.w_FollowTask_Load);
            this.SizeChanged += new System.EventHandler(this.w_MachineTask_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTask)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSelectC;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.DataGridView dgvTask;
        private System.Windows.Forms.Label lblNo2;
        private System.Windows.Forms.Label lblNo1;
        private System.Windows.Forms.TextBox txtinfo2;
    }
}