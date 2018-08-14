namespace FollowTask.ErrorStart
{
    partial class ErrorStartMain_From
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
            this.Btn_start = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_start
            // 
            this.Btn_start.Location = new System.Drawing.Point(29, 29);
            this.Btn_start.Name = "Btn_start";
            this.Btn_start.Size = new System.Drawing.Size(169, 35);
            this.Btn_start.TabIndex = 0;
            this.Btn_start.Text = "button1";
            this.Btn_start.UseVisualStyleBackColor = true;
            this.Btn_start.Click += new System.EventHandler(this.Btn_start_Click);
            // 
            // ErrorStartMain_From
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 429);
            this.Controls.Add(this.Btn_start);
            this.Name = "ErrorStartMain_From";
            this.Text = "ErrorStartMain_From";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_start;
    }
}