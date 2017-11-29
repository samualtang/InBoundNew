namespace highSpeed.baseData
{
    partial class win_package_handle
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
            this.btn_save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_packageid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_packagedesc = new System.Windows.Forms.TextBox();
            this.txt_packageval = new System.Windows.Forms.TextBox();
            this.btn_close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(141, 124);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 0;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "包装类型编号";
            // 
            // txt_packageid
            // 
            this.txt_packageid.Location = new System.Drawing.Point(116, 22);
            this.txt_packageid.Name = "txt_packageid";
            this.txt_packageid.Size = new System.Drawing.Size(100, 21);
            this.txt_packageid.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(301, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "包装类似描述";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(69, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "包装量";
            // 
            // txt_packagedesc
            // 
            this.txt_packagedesc.Location = new System.Drawing.Point(384, 22);
            this.txt_packagedesc.Name = "txt_packagedesc";
            this.txt_packagedesc.Size = new System.Drawing.Size(100, 21);
            this.txt_packagedesc.TabIndex = 5;
            // 
            // txt_packageval
            // 
            this.txt_packageval.Location = new System.Drawing.Point(116, 70);
            this.txt_packageval.Name = "txt_packageval";
            this.txt_packageval.Size = new System.Drawing.Size(100, 21);
            this.txt_packageval.TabIndex = 6;
            this.txt_packageval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_packageval_KeyPress);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(303, 124);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 7;
            this.btn_close.Text = "关闭";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // win_package_handle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 173);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.txt_packageval);
            this.Controls.Add(this.txt_packagedesc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_packageid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_save);
            this.Name = "win_package_handle";
            this.Text = "w_packagehandle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_packageid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_packagedesc;
        private System.Windows.Forms.TextBox txt_packageval;
        private System.Windows.Forms.Button btn_close;
    }
}