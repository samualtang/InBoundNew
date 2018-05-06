namespace FollowTask
{
    partial class Fm_SortDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Fm_SortDetails));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxYaobaiDetails = new System.Windows.Forms.GroupBox();
            this.listViewYaobaiDetails = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRefresh = new System.Windows.Forms.Button();
            this.groupBoxYaobaiDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 25F);
            this.label1.Location = new System.Drawing.Point(197, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(253, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "摇摆前订单查询";
            // 
            // groupBoxYaobaiDetails
            // 
            this.groupBoxYaobaiDetails.Controls.Add(this.listViewYaobaiDetails);
            this.groupBoxYaobaiDetails.Font = new System.Drawing.Font("宋体", 11F);
            this.groupBoxYaobaiDetails.Location = new System.Drawing.Point(28, 85);
            this.groupBoxYaobaiDetails.Name = "groupBoxYaobaiDetails";
            this.groupBoxYaobaiDetails.Size = new System.Drawing.Size(613, 385);
            this.groupBoxYaobaiDetails.TabIndex = 1;
            this.groupBoxYaobaiDetails.TabStop = false;
            this.groupBoxYaobaiDetails.Text = "摇摆详细信息";
            // 
            // listViewYaobaiDetails
            // 
            this.listViewYaobaiDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader6,
            this.columnHeader5});
            this.listViewYaobaiDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewYaobaiDetails.GridLines = true;
            this.listViewYaobaiDetails.Location = new System.Drawing.Point(3, 20);
            this.listViewYaobaiDetails.Name = "listViewYaobaiDetails";
            this.listViewYaobaiDetails.Size = new System.Drawing.Size(607, 362);
            this.listViewYaobaiDetails.TabIndex = 0;
            this.listViewYaobaiDetails.UseCompatibleStateImageBehavior = false;
            this.listViewYaobaiDetails.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "订单号";
            this.columnHeader1.Width = 124;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "排序号";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "合流号";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "主皮带号";
            this.columnHeader4.Width = 73;
            // 
            // columnHeader5
            // 
            this.columnHeader5.DisplayIndex = 4;
            this.columnHeader5.Text = "香烟编号";
            this.columnHeader5.Width = 142;
            // 
            // columnHeader6
            // 
            this.columnHeader6.DisplayIndex = 5;
            this.columnHeader6.Text = "香烟名称";
            this.columnHeader6.Width = 133;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(563, 56);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // Fm_SortDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 504);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.groupBoxYaobaiDetails);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Fm_SortDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "预分拣信息";
            this.Load += new System.EventHandler(this.Fm_SortDetails_Load);
            this.groupBoxYaobaiDetails.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxYaobaiDetails;
        private System.Windows.Forms.ListView listViewYaobaiDetails;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button btnRefresh;
    }
}