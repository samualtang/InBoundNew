namespace Tool
{
    partial class FM_Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FM_Main));
            this.rtbH = new System.Windows.Forms.RichTextBox();
            this.rtbF = new System.Windows.Forms.RichTextBox();
            this.btnEn = new System.Windows.Forms.Button();
            this.btnDe = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbH
            // 
            this.rtbH.Location = new System.Drawing.Point(21, 12);
            this.rtbH.Name = "rtbH";
            this.rtbH.Size = new System.Drawing.Size(352, 402);
            this.rtbH.TabIndex = 0;
            this.rtbH.Text = "";
            // 
            // rtbF
            // 
            this.rtbF.Location = new System.Drawing.Point(419, 12);
            this.rtbF.Name = "rtbF";
            this.rtbF.Size = new System.Drawing.Size(405, 402);
            this.rtbF.TabIndex = 0;
            this.rtbF.Text = "";
            // 
            // btnEn
            // 
            this.btnEn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEn.Location = new System.Drawing.Point(80, 15);
            this.btnEn.Name = "btnEn";
            this.btnEn.Size = new System.Drawing.Size(59, 26);
            this.btnEn.TabIndex = 1;
            this.btnEn.Text = "加密";
            this.btnEn.UseVisualStyleBackColor = true;
            this.btnEn.Click += new System.EventHandler(this.btnEn_Click);
            // 
            // btnDe
            // 
            this.btnDe.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDe.Location = new System.Drawing.Point(208, 15);
            this.btnDe.Name = "btnDe";
            this.btnDe.Size = new System.Drawing.Size(59, 26);
            this.btnDe.TabIndex = 1;
            this.btnDe.Text = "解密";
            this.btnDe.UseVisualStyleBackColor = true;
            this.btnDe.Click += new System.EventHandler(this.btnDe_Click);
            // 
            // btnClear
            // 
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClear.Location = new System.Drawing.Point(685, 18);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(65, 23);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnEn);
            this.panel1.Controls.Add(this.btnDe);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 436);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(836, 56);
            this.panel1.TabIndex = 4;
            // 
            // FM_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 492);
            this.Controls.Add(this.rtbF);
            this.Controls.Add(this.rtbH);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FM_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "小工具";
            this.SizeChanged += new System.EventHandler(this.FM_Main_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbH;
        private System.Windows.Forms.RichTextBox rtbF;
        private System.Windows.Forms.Button btnEn;
        private System.Windows.Forms.Button btnDe;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel panel1;
    }
}

