namespace highSpeed.baseData
{
    partial class w_cigarette_handle
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
            this.lblName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbws = new System.Windows.Forms.TextBox();
            this.tbhjws = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbyz = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cbClearUp = new System.Windows.Forms.CheckBox();
            this.cbless = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "卷烟名称：";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(110, 24);
            this.lblName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(0, 12);
            this.lblName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(316, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "卷烟编码:";
            // 
            // lblNo
            // 
            this.lblNo.AutoSize = true;
            this.lblNo.Location = new System.Drawing.Point(387, 24);
            this.lblNo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(0, 12);
            this.lblNo.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 57);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "尾数:";
            // 
            // tbws
            // 
            this.tbws.Location = new System.Drawing.Point(112, 51);
            this.tbws.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbws.Name = "tbws";
            this.tbws.Size = new System.Drawing.Size(99, 21);
            this.tbws.TabIndex = 5;
            this.tbws.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_KeyPress);
            this.tbws.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_KeyUp);
            // 
            // tbhjws
            // 
            this.tbhjws.Location = new System.Drawing.Point(379, 51);
            this.tbhjws.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbhjws.Name = "tbhjws";
            this.tbhjws.Size = new System.Drawing.Size(110, 21);
            this.tbhjws.TabIndex = 7;
            this.tbhjws.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_KeyPress);
            this.tbhjws.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_KeyUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(340, 57);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "阀值:";
            // 
            // tbyz
            // 
            this.tbyz.Location = new System.Drawing.Point(112, 85);
            this.tbyz.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tbyz.Name = "tbyz";
            this.tbyz.Size = new System.Drawing.Size(99, 21);
            this.tbyz.TabIndex = 9;
            this.tbyz.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_KeyPress);
            this.tbyz.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_KeyUp);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(112, 143);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 28);
            this.button1.TabIndex = 12;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(320, 143);
            this.button2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 28);
            this.button2.TabIndex = 13;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 87);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "托盘出库数:";
            // 
            // cbClearUp
            // 
            this.cbClearUp.AutoSize = true;
            this.cbClearUp.Location = new System.Drawing.Point(275, 90);
            this.cbClearUp.Name = "cbClearUp";
            this.cbClearUp.Size = new System.Drawing.Size(96, 16);
            this.cbClearUp.TabIndex = 14;
            this.cbClearUp.Text = "上层烟柜清空";
            this.cbClearUp.UseVisualStyleBackColor = true;
            // 
            // cbless
            // 
            this.cbless.AutoSize = true;
            this.cbless.Location = new System.Drawing.Point(389, 90);
            this.cbless.Name = "cbless";
            this.cbless.Size = new System.Drawing.Size(84, 16);
            this.cbless.TabIndex = 15;
            this.cbless.Text = "烟柜量最少";
            this.cbless.UseVisualStyleBackColor = true;
            // 
            // w_cigarette_handle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 226);
            this.Controls.Add(this.cbless);
            this.Controls.Add(this.cbClearUp);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbyz);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbhjws);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbws);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "w_cigarette_handle";
            this.Text = "卷烟基础设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbws;
        private System.Windows.Forms.TextBox tbhjws;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbyz;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbClearUp;
        private System.Windows.Forms.CheckBox cbless;
    }
}