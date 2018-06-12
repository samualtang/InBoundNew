namespace FollowTask
{
    partial class Fm_UnionMainBelt
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
            this.groupBoxAfter = new System.Windows.Forms.GroupBox();
            this.groupBoxBefore = new System.Windows.Forms.GroupBox();
            this.listViewBefore = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewAfter = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxAfter.SuspendLayout();
            this.groupBoxBefore.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxAfter
            // 
            this.groupBoxAfter.Controls.Add(this.listViewAfter);
            this.groupBoxAfter.Font = new System.Drawing.Font("宋体", 11F);
            this.groupBoxAfter.Location = new System.Drawing.Point(657, 106);
            this.groupBoxAfter.Name = "groupBoxAfter";
            this.groupBoxAfter.Size = new System.Drawing.Size(629, 377);
            this.groupBoxAfter.TabIndex = 2;
            this.groupBoxAfter.TabStop = false;
            this.groupBoxAfter.Text = "之前";
            // 
            // groupBoxBefore
            // 
            this.groupBoxBefore.Controls.Add(this.listViewBefore);
            this.groupBoxBefore.Font = new System.Drawing.Font("宋体", 11F);
            this.groupBoxBefore.Location = new System.Drawing.Point(12, 106);
            this.groupBoxBefore.Name = "groupBoxBefore";
            this.groupBoxBefore.Size = new System.Drawing.Size(585, 377);
            this.groupBoxBefore.TabIndex = 3;
            this.groupBoxBefore.TabStop = false;
            this.groupBoxBefore.Text = "之后";
            // 
            // listViewBefore
            // 
            this.listViewBefore.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listViewBefore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewBefore.GridLines = true;
            this.listViewBefore.Location = new System.Drawing.Point(3, 20);
            this.listViewBefore.Name = "listViewBefore";
            this.listViewBefore.Size = new System.Drawing.Size(579, 354);
            this.listViewBefore.TabIndex = 1;
            this.listViewBefore.UseCompatibleStateImageBehavior = false;
            this.listViewBefore.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "机械手号";
            this.columnHeader1.Width = 101;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "任务号";
            this.columnHeader2.Width = 116;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "主皮带号";
            this.columnHeader3.Width = 87;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "香烟编号";
            this.columnHeader4.Width = 125;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "香烟名称";
            this.columnHeader5.Width = 145;
            // 
            // listViewAfter
            // 
            this.listViewAfter.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10});
            this.listViewAfter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewAfter.GridLines = true;
            this.listViewAfter.Location = new System.Drawing.Point(3, 20);
            this.listViewAfter.Name = "listViewAfter";
            this.listViewAfter.Size = new System.Drawing.Size(623, 354);
            this.listViewAfter.TabIndex = 1;
            this.listViewAfter.UseCompatibleStateImageBehavior = false;
            this.listViewAfter.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "机械手号";
            this.columnHeader6.Width = 134;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "任务号";
            this.columnHeader7.Width = 132;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "主皮带号";
            this.columnHeader8.Width = 87;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "香烟编号";
            this.columnHeader9.Width = 125;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "香烟名称";
            this.columnHeader10.Width = 145;
            // 
            // Fm_UnionMainBelt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 495);
            this.Controls.Add(this.groupBoxBefore);
            this.Controls.Add(this.groupBoxAfter);
            this.Name = "Fm_UnionMainBelt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "合流主皮带";
            this.groupBoxAfter.ResumeLayout(false);
            this.groupBoxBefore.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxAfter;
        private System.Windows.Forms.GroupBox groupBoxBefore;
        private System.Windows.Forms.ListView listViewAfter;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ListView listViewBefore;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;

    }
}