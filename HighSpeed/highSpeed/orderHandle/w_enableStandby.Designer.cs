namespace highSpeed.orderHandle
{
    partial class w_enableStandby
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_GroupList = new System.Windows.Forms.ComboBox();
            this.cbciagrettcode = new System.Windows.Forms.Label();
            this.cbStandby = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbsource = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEnableStandby = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmb_GroupList);
            this.groupBox1.Controls.Add(this.cbciagrettcode);
            this.groupBox1.Controls.Add(this.cbStandby);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbsource);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnEnableStandby);
            this.groupBox1.Location = new System.Drawing.Point(13, 22);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(715, 184);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "启用备用通道";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(138, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "组";
            // 
            // cmb_GroupList
            // 
            this.cmb_GroupList.FormattingEnabled = true;
            this.cmb_GroupList.Location = new System.Drawing.Point(73, 38);
            this.cmb_GroupList.Margin = new System.Windows.Forms.Padding(4);
            this.cmb_GroupList.Name = "cmb_GroupList";
            this.cmb_GroupList.Size = new System.Drawing.Size(61, 23);
            this.cmb_GroupList.TabIndex = 9;
            this.cmb_GroupList.SelectedIndexChanged += new System.EventHandler(this.cmb_GroupList_SelectedIndexChanged);
            // 
            // cbciagrettcode
            // 
            this.cbciagrettcode.AutoSize = true;
            this.cbciagrettcode.Location = new System.Drawing.Point(323, 42);
            this.cbciagrettcode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cbciagrettcode.Name = "cbciagrettcode";
            this.cbciagrettcode.Size = new System.Drawing.Size(55, 15);
            this.cbciagrettcode.TabIndex = 7;
            this.cbciagrettcode.Text = "label4";
            // 
            // cbStandby
            // 
            this.cbStandby.FormattingEnabled = true;
            this.cbStandby.Location = new System.Drawing.Point(94, 107);
            this.cbStandby.Margin = new System.Windows.Forms.Padding(4);
            this.cbStandby.Name = "cbStandby";
            this.cbStandby.Size = new System.Drawing.Size(210, 23);
            this.cbStandby.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 110);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "备用通道:";
            // 
            // cbsource
            // 
            this.cbsource.FormattingEnabled = true;
            this.cbsource.Location = new System.Drawing.Point(178, 38);
            this.cbsource.Margin = new System.Windows.Forms.Padding(4);
            this.cbsource.Name = "cbsource";
            this.cbsource.Size = new System.Drawing.Size(126, 23);
            this.cbsource.TabIndex = 4;
            this.cbsource.SelectedIndexChanged += new System.EventHandler(this.cbsource_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "源通道:";
            // 
            // btnEnableStandby
            // 
            this.btnEnableStandby.Location = new System.Drawing.Point(575, 71);
            this.btnEnableStandby.Margin = new System.Windows.Forms.Padding(4);
            this.btnEnableStandby.Name = "btnEnableStandby";
            this.btnEnableStandby.Size = new System.Drawing.Size(100, 36);
            this.btnEnableStandby.TabIndex = 2;
            this.btnEnableStandby.Text = "启用";
            this.btnEnableStandby.UseVisualStyleBackColor = true;
            this.btnEnableStandby.Click += new System.EventHandler(this.btnEnableStandby_Click);
            // 
            // w_enableStandby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 644);
            this.Controls.Add(this.groupBox1);
            this.Name = "w_enableStandby";
            this.Text = "启用备用";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label cbciagrettcode;
        private System.Windows.Forms.ComboBox cbStandby;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbsource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEnableStandby;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_GroupList;
    }
}