namespace SortingControlSys
{
    partial class FM_MainCaChe
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
            this.btn_Search = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_groupno = new System.Windows.Forms.TextBox();
            this.txt_mainbelt = new System.Windows.Forms.TextBox();
            this.btn_update = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_cachesize = new System.Windows.Forms.TextBox();
            this.txt_dispatchesize = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_dispatchenum = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmb_state = new System.Windows.Forms.ComboBox();
            this.txt_mainbeltno = new System.Windows.Forms.TextBox();
            this.txt_groupnono = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox_num = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(718, 10);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(75, 23);
            this.btn_Search.TabIndex = 0;
            this.btn_Search.Text = "查  询";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(316, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "主皮带";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(511, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "组号";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dataGridView1.Location = new System.Drawing.Point(77, 39);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(740, 286);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "id";
            this.Column7.HeaderText = "编号";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 60;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "mainbelt";
            this.Column1.HeaderText = "主皮带";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "groupno";
            this.Column2.HeaderText = "分拣组";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "cachesize";
            this.Column3.HeaderText = "缓存量上限";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "dispatchenum";
            this.Column4.HeaderText = "缓存空余量";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "dispatchesize";
            this.Column5.HeaderText = "每次订单量上限";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 130;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "state";
            this.Column6.HeaderText = "缓存带状态";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(75, 380);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "分拣组";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(75, 346);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "主皮带";
            // 
            // txt_groupno
            // 
            this.txt_groupno.Location = new System.Drawing.Point(122, 377);
            this.txt_groupno.Name = "txt_groupno";
            this.txt_groupno.ReadOnly = true;
            this.txt_groupno.Size = new System.Drawing.Size(100, 21);
            this.txt_groupno.TabIndex = 8;
            // 
            // txt_mainbelt
            // 
            this.txt_mainbelt.Location = new System.Drawing.Point(122, 343);
            this.txt_mainbelt.Name = "txt_mainbelt";
            this.txt_mainbelt.ReadOnly = true;
            this.txt_mainbelt.Size = new System.Drawing.Size(100, 21);
            this.txt_mainbelt.TabIndex = 7;
            // 
            // btn_update
            // 
            this.btn_update.Location = new System.Drawing.Point(742, 369);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(75, 23);
            this.btn_update.TabIndex = 6;
            this.btn_update.Text = "修  改";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(242, 350);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "缓存量上限";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(464, 350);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "每次订单量上限";
            // 
            // txt_cachesize
            // 
            this.txt_cachesize.Location = new System.Drawing.Point(313, 343);
            this.txt_cachesize.Name = "txt_cachesize";
            this.txt_cachesize.Size = new System.Drawing.Size(100, 21);
            this.txt_cachesize.TabIndex = 13;
            // 
            // txt_dispatchesize
            // 
            this.txt_dispatchesize.Location = new System.Drawing.Point(559, 345);
            this.txt_dispatchesize.Name = "txt_dispatchesize";
            this.txt_dispatchesize.Size = new System.Drawing.Size(100, 21);
            this.txt_dispatchesize.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(242, 382);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "缓存空余量";
            // 
            // txt_dispatchenum
            // 
            this.txt_dispatchenum.Location = new System.Drawing.Point(313, 379);
            this.txt_dispatchenum.Name = "txt_dispatchenum";
            this.txt_dispatchenum.Size = new System.Drawing.Size(100, 21);
            this.txt_dispatchenum.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(488, 382);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "缓存带状态";
            // 
            // cmb_state
            // 
            this.cmb_state.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_state.FormattingEnabled = true;
            this.cmb_state.Items.AddRange(new object[] {
            "启用",
            "禁用"});
            this.cmb_state.Location = new System.Drawing.Point(559, 379);
            this.cmb_state.Name = "cmb_state";
            this.cmb_state.Size = new System.Drawing.Size(100, 20);
            this.cmb_state.TabIndex = 20;
            // 
            // txt_mainbeltno
            // 
            this.txt_mainbeltno.Location = new System.Drawing.Point(378, 12);
            this.txt_mainbeltno.Name = "txt_mainbeltno";
            this.txt_mainbeltno.Size = new System.Drawing.Size(100, 21);
            this.txt_mainbeltno.TabIndex = 23;
            // 
            // txt_groupnono
            // 
            this.txt_groupnono.Location = new System.Drawing.Point(562, 12);
            this.txt_groupnono.Name = "txt_groupnono";
            this.txt_groupnono.Size = new System.Drawing.Size(100, 21);
            this.txt_groupnono.TabIndex = 24;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(879, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 116);
            this.button1.TabIndex = 25;
            this.button1.Text = "全   部   减 ";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(879, 209);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(26, 116);
            this.button2.TabIndex = 26;
            this.button2.Text = "全   部   加";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBox_num
            // 
            this.textBox_num.Location = new System.Drawing.Point(869, 171);
            this.textBox_num.Name = "textBox_num";
            this.textBox_num.ReadOnly = true;
            this.textBox_num.Size = new System.Drawing.Size(43, 21);
            this.textBox_num.TabIndex = 27;
            // 
            // FM_MainCaChe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 415);
            this.Controls.Add(this.textBox_num);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_groupnono);
            this.Controls.Add(this.txt_mainbeltno);
            this.Controls.Add(this.cmb_state);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_dispatchenum);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_cachesize);
            this.Controls.Add(this.txt_dispatchesize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_groupno);
            this.Controls.Add(this.txt_mainbelt);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Search);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FM_MainCaChe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "合流缓存带设置";
            this.Load += new System.EventHandler(this.FM_MainCaChe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_groupno;
        private System.Windows.Forms.TextBox txt_mainbelt;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_cachesize;
        private System.Windows.Forms.TextBox txt_dispatchesize;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_dispatchenum;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmb_state;
        private System.Windows.Forms.TextBox txt_mainbeltno;
        private System.Windows.Forms.TextBox txt_groupnono;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox_num;
    }
}