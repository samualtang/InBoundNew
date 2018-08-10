namespace FollowTask
{
    partial class FM_Device
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
            this.lblDeviceNo = new System.Windows.Forms.Label();
            this.panelOption = new System.Windows.Forms.Panel();
            this.panelThoery = new System.Windows.Forms.Panel();
            this.lblTheory = new System.Windows.Forms.Label();
            this.btnEnter = new System.Windows.Forms.Button();
            this.txtDeviceNo = new System.Windows.Forms.TextBox();
            this.groupBoxUnionInfo = new System.Windows.Forms.GroupBox();
            this.lblErorr = new System.Windows.Forms.Label();
            this.lblGOto = new System.Windows.Forms.Label();
            this.lblPlace = new System.Windows.Forms.Label();
            this.panelCig = new System.Windows.Forms.Panel();
            this.dgvMainBeltInfo = new System.Windows.Forms.DataGridView();
            this.lblDeviceCount = new System.Windows.Forms.Label();
            this.panelOption.SuspendLayout();
            this.panelThoery.SuspendLayout();
            this.groupBoxUnionInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMainBeltInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDeviceNo
            // 
            this.lblDeviceNo.AutoSize = true;
            this.lblDeviceNo.Font = new System.Drawing.Font("宋体", 11F);
            this.lblDeviceNo.Location = new System.Drawing.Point(276, 8);
            this.lblDeviceNo.Name = "lblDeviceNo";
            this.lblDeviceNo.Size = new System.Drawing.Size(67, 15);
            this.lblDeviceNo.TabIndex = 4;
            this.lblDeviceNo.Text = "设备编号";
            // 
            // panelOption
            // 
            this.panelOption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOption.Controls.Add(this.panelThoery);
            this.panelOption.Controls.Add(this.lblDeviceNo);
            this.panelOption.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOption.Location = new System.Drawing.Point(0, 0);
            this.panelOption.Name = "panelOption";
            this.panelOption.Size = new System.Drawing.Size(898, 58);
            this.panelOption.TabIndex = 6;
            // 
            // panelThoery
            // 
            this.panelThoery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelThoery.Controls.Add(this.lblTheory);
            this.panelThoery.Controls.Add(this.btnEnter);
            this.panelThoery.Controls.Add(this.txtDeviceNo);
            this.panelThoery.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelThoery.Location = new System.Drawing.Point(634, 0);
            this.panelThoery.Name = "panelThoery";
            this.panelThoery.Size = new System.Drawing.Size(262, 56);
            this.panelThoery.TabIndex = 26;
            // 
            // lblTheory
            // 
            this.lblTheory.AutoSize = true;
            this.lblTheory.Location = new System.Drawing.Point(17, 7);
            this.lblTheory.Name = "lblTheory";
            this.lblTheory.Size = new System.Drawing.Size(101, 12);
            this.lblTheory.TabIndex = 29;
            this.lblTheory.Text = "查询设备号数据：";
            // 
            // btnEnter
            // 
            this.btnEnter.Location = new System.Drawing.Point(162, 30);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(66, 20);
            this.btnEnter.TabIndex = 6;
            this.btnEnter.Text = "查询";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // txtDeviceNo
            // 
            this.txtDeviceNo.Location = new System.Drawing.Point(18, 29);
            this.txtDeviceNo.Name = "txtDeviceNo";
            this.txtDeviceNo.Size = new System.Drawing.Size(100, 21);
            this.txtDeviceNo.TabIndex = 5;
            this.txtDeviceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDeviceNo_KeyDown);
            // 
            // groupBoxUnionInfo
            // 
            this.groupBoxUnionInfo.Controls.Add(this.lblDeviceCount);
            this.groupBoxUnionInfo.Controls.Add(this.lblErorr);
            this.groupBoxUnionInfo.Controls.Add(this.lblGOto);
            this.groupBoxUnionInfo.Controls.Add(this.lblPlace);
            this.groupBoxUnionInfo.Controls.Add(this.panelCig);
            this.groupBoxUnionInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxUnionInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBoxUnionInfo.Location = new System.Drawing.Point(0, 58);
            this.groupBoxUnionInfo.Name = "groupBoxUnionInfo";
            this.groupBoxUnionInfo.Size = new System.Drawing.Size(898, 141);
            this.groupBoxUnionInfo.TabIndex = 7;
            this.groupBoxUnionInfo.TabStop = false;
            this.groupBoxUnionInfo.Text = "皮带";
            // 
            // lblErorr
            // 
            this.lblErorr.AutoSize = true;
            this.lblErorr.Location = new System.Drawing.Point(361, 123);
            this.lblErorr.Name = "lblErorr";
            this.lblErorr.Size = new System.Drawing.Size(65, 12);
            this.lblErorr.TabIndex = 10;
            this.lblErorr.Text = "错误信息：";
            this.lblErorr.Visible = false;
            // 
            // lblGOto
            // 
            this.lblGOto.AutoSize = true;
            this.lblGOto.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblGOto.Font = new System.Drawing.Font("宋体", 11F);
            this.lblGOto.Location = new System.Drawing.Point(773, 116);
            this.lblGOto.Name = "lblGOto";
            this.lblGOto.Size = new System.Drawing.Size(122, 15);
            this.lblGOto.TabIndex = 9;
            this.lblGOto.Text = "--前往包装机-->";
            // 
            // lblPlace
            // 
            this.lblPlace.AutoSize = true;
            this.lblPlace.Location = new System.Drawing.Point(674, 119);
            this.lblPlace.Name = "lblPlace";
            this.lblPlace.Size = new System.Drawing.Size(71, 12);
            this.lblPlace.TabIndex = 6;
            this.lblPlace.Text = "当前位置：0";
            this.lblPlace.Visible = false;
            // 
            // panelCig
            // 
            this.panelCig.AutoScroll = true;
            this.panelCig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCig.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCig.Location = new System.Drawing.Point(3, 17);
            this.panelCig.Name = "panelCig";
            this.panelCig.Size = new System.Drawing.Size(892, 99);
            this.panelCig.TabIndex = 2;
            // 
            // dgvMainBeltInfo
            // 
            this.dgvMainBeltInfo.BackgroundColor = System.Drawing.Color.Honeydew;
            this.dgvMainBeltInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMainBeltInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMainBeltInfo.Location = new System.Drawing.Point(0, 199);
            this.dgvMainBeltInfo.Name = "dgvMainBeltInfo";
            this.dgvMainBeltInfo.RowTemplate.Height = 23;
            this.dgvMainBeltInfo.Size = new System.Drawing.Size(898, 377);
            this.dgvMainBeltInfo.TabIndex = 8;
            // 
            // lblDeviceCount
            // 
            this.lblDeviceCount.AutoSize = true;
            this.lblDeviceCount.Location = new System.Drawing.Point(12, 123);
            this.lblDeviceCount.Name = "lblDeviceCount";
            this.lblDeviceCount.Size = new System.Drawing.Size(59, 12);
            this.lblDeviceCount.TabIndex = 11;
            this.lblDeviceCount.Text = "设备总数:";
            // 
            // FM_Device
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 576);
            this.Controls.Add(this.dgvMainBeltInfo);
            this.Controls.Add(this.groupBoxUnionInfo);
            this.Controls.Add(this.panelOption);
            this.Name = "FM_Device";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设备";
            this.Load += new System.EventHandler(this.FM_Device_Load);
            this.panelOption.ResumeLayout(false);
            this.panelOption.PerformLayout();
            this.panelThoery.ResumeLayout(false);
            this.panelThoery.PerformLayout();
            this.groupBoxUnionInfo.ResumeLayout(false);
            this.groupBoxUnionInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMainBeltInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDeviceNo;
        private System.Windows.Forms.Panel panelOption;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.TextBox txtDeviceNo;
        private System.Windows.Forms.GroupBox groupBoxUnionInfo;
        private System.Windows.Forms.Label lblErorr;
        private System.Windows.Forms.Label lblGOto;
        private System.Windows.Forms.Label lblPlace;
        private System.Windows.Forms.Panel panelCig;
        private System.Windows.Forms.Panel panelThoery;
        private System.Windows.Forms.Label lblTheory;
        private System.Windows.Forms.DataGridView dgvMainBeltInfo;
        private System.Windows.Forms.Label lblDeviceCount;
    }
}