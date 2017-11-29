namespace highSpeed.baseData
{
    partial class win_replenish_handle
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.box_troughid = new System.Windows.Forms.ComboBox();
            this.txt_replenishnum = new System.Windows.Forms.TextBox();
            this.txt_replenishseq = new System.Windows.Forms.TextBox();
            this.txt_replenishdesc = new System.Windows.Forms.TextBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "所属烟道";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(259, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "补货通道编号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "补货通道描述";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(259, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "补货通道顺序";
            // 
            // box_troughid
            // 
            this.box_troughid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.box_troughid.FormattingEnabled = true;
            this.box_troughid.Location = new System.Drawing.Point(83, 32);
            this.box_troughid.Name = "box_troughid";
            this.box_troughid.Size = new System.Drawing.Size(121, 20);
            this.box_troughid.TabIndex = 4;
            // 
            // txt_replenishnum
            // 
            this.txt_replenishnum.Location = new System.Drawing.Point(342, 32);
            this.txt_replenishnum.Name = "txt_replenishnum";
            this.txt_replenishnum.Size = new System.Drawing.Size(100, 21);
            this.txt_replenishnum.TabIndex = 5;
            // 
            // txt_replenishseq
            // 
            this.txt_replenishseq.Location = new System.Drawing.Point(342, 84);
            this.txt_replenishseq.Name = "txt_replenishseq";
            this.txt_replenishseq.Size = new System.Drawing.Size(100, 21);
            this.txt_replenishseq.TabIndex = 6;
            this.txt_replenishseq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_replenishseq_KeyPress);
            // 
            // txt_replenishdesc
            // 
            this.txt_replenishdesc.Location = new System.Drawing.Point(83, 78);
            this.txt_replenishdesc.Name = "txt_replenishdesc";
            this.txt_replenishdesc.Size = new System.Drawing.Size(121, 21);
            this.txt_replenishdesc.TabIndex = 7;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(129, 139);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 8;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(283, 139);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 9;
            this.btn_close.Text = "关闭";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // win_replenish_handle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 200);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.txt_replenishdesc);
            this.Controls.Add(this.txt_replenishseq);
            this.Controls.Add(this.txt_replenishnum);
            this.Controls.Add(this.box_troughid);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "win_replenish_handle";
            this.Text = "w_replenish_handle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox box_troughid;
        private System.Windows.Forms.TextBox txt_replenishnum;
        private System.Windows.Forms.TextBox txt_replenishseq;
        private System.Windows.Forms.TextBox txt_replenishdesc;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_close;
    }
}