namespace highSpeed.baseData
{
    partial class win_trough
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnsyncData = new System.Windows.Forms.Button();
            this.cbctype = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbtype = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_toexcel = new System.Windows.Forms.Button();
            this.btn_print = new System.Windows.Forms.Button();
            this.btn_amend = new System.Windows.Forms.Button();
            this.btn_qy = new System.Windows.Forms.Button();
            this.btn_jy = new System.Windows.Forms.Button();
            this.btn_search = new System.Windows.Forms.Button();
            this.txt_keywd = new System.Windows.Forms.TextBox();
            this.box_condition = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.troughdata = new System.Windows.Forms.DataGridView();
            this.num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.troughnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.machineseq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cigarettecode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cigarettename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cigarettetype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.通道类型 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgVprint1 = new VBprinter.DGVprint(this.components);
            this.pager1 = new WHC.Pager.WinControl.Pager();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.troughdata)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.cbctype);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cbtype);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btn_toexcel);
            this.panel1.Controls.Add(this.btn_print);
            this.panel1.Controls.Add(this.btn_amend);
            this.panel1.Controls.Add(this.btn_qy);
            this.panel1.Controls.Add(this.btn_jy);
            this.panel1.Controls.Add(this.btn_search);
            this.panel1.Controls.Add(this.txt_keywd);
            this.panel1.Controls.Add(this.box_condition);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1252, 69);
            this.panel1.TabIndex = 0;
            // 
            // btnsyncData
            // 
            this.btnsyncData.Location = new System.Drawing.Point(20, 23);
            this.btnsyncData.Name = "btnsyncData";
            this.btnsyncData.Size = new System.Drawing.Size(48, 23);
            this.btnsyncData.TabIndex = 18;
            this.btnsyncData.Text = "烟柜";
            this.btnsyncData.UseVisualStyleBackColor = true;
            this.btnsyncData.Click += new System.EventHandler(this.btnsyncData_Click);
            // 
            // cbctype
            // 
            this.cbctype.FormattingEnabled = true;
            this.cbctype.Items.AddRange(new object[] {
            "所有",
            "标准烟",
            "异型烟",
            "异型混合"});
            this.cbctype.Location = new System.Drawing.Point(504, 26);
            this.cbctype.Margin = new System.Windows.Forms.Padding(2);
            this.cbctype.Name = "cbctype";
            this.cbctype.Size = new System.Drawing.Size(102, 20);
            this.cbctype.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(441, 31);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "卷烟类型:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(814, 23);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(51, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "增加";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(269, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "通道类型:";
            // 
            // cbtype
            // 
            this.cbtype.FormattingEnabled = true;
            this.cbtype.Items.AddRange(new object[] {
            "分拣",
            "重力式货架",
            "皮带机",
            "分拣出口"});
            this.cbtype.Location = new System.Drawing.Point(340, 27);
            this.cbtype.Margin = new System.Windows.Forms.Padding(2);
            this.cbtype.Name = "cbtype";
            this.cbtype.Size = new System.Drawing.Size(97, 20);
            this.cbtype.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1052, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(49, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "验证";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_toexcel
            // 
            this.btn_toexcel.Location = new System.Drawing.Point(998, 23);
            this.btn_toexcel.Name = "btn_toexcel";
            this.btn_toexcel.Size = new System.Drawing.Size(44, 23);
            this.btn_toexcel.TabIndex = 11;
            this.btn_toexcel.Text = "导出";
            this.btn_toexcel.UseVisualStyleBackColor = true;
            this.btn_toexcel.Click += new System.EventHandler(this.btn_toexcel_Click);
            // 
            // btn_print
            // 
            this.btn_print.Location = new System.Drawing.Point(940, 23);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(51, 23);
            this.btn_print.TabIndex = 10;
            this.btn_print.Text = "打印";
            this.btn_print.UseVisualStyleBackColor = true;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // btn_amend
            // 
            this.btn_amend.Location = new System.Drawing.Point(872, 23);
            this.btn_amend.Name = "btn_amend";
            this.btn_amend.Size = new System.Drawing.Size(54, 23);
            this.btn_amend.TabIndex = 8;
            this.btn_amend.Text = "修改";
            this.btn_amend.UseVisualStyleBackColor = true;
            this.btn_amend.Click += new System.EventHandler(this.btn_amend_Click);
            // 
            // btn_qy
            // 
            this.btn_qy.Location = new System.Drawing.Point(687, 23);
            this.btn_qy.Name = "btn_qy";
            this.btn_qy.Size = new System.Drawing.Size(56, 23);
            this.btn_qy.TabIndex = 7;
            this.btn_qy.Text = "启用";
            this.btn_qy.UseVisualStyleBackColor = true;
            this.btn_qy.Click += new System.EventHandler(this.btn_qy_Click);
            // 
            // btn_jy
            // 
            this.btn_jy.Location = new System.Drawing.Point(750, 23);
            this.btn_jy.Name = "btn_jy";
            this.btn_jy.Size = new System.Drawing.Size(56, 23);
            this.btn_jy.TabIndex = 6;
            this.btn_jy.Text = "禁用";
            this.btn_jy.UseVisualStyleBackColor = true;
            this.btn_jy.Click += new System.EventHandler(this.btn_jy_Click);
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(631, 23);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(51, 23);
            this.btn_search.TabIndex = 5;
            this.btn_search.Text = "查询";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // txt_keywd
            // 
            this.txt_keywd.Location = new System.Drawing.Point(165, 25);
            this.txt_keywd.Name = "txt_keywd";
            this.txt_keywd.Size = new System.Drawing.Size(100, 21);
            this.txt_keywd.TabIndex = 4;
            this.txt_keywd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_keywd_KeyDown);
            // 
            // box_condition
            // 
            this.box_condition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.box_condition.FormattingEnabled = true;
            this.box_condition.Location = new System.Drawing.Point(59, 26);
            this.box_condition.Name = "box_condition";
            this.box_condition.Size = new System.Drawing.Size(100, 20);
            this.box_condition.TabIndex = 3;
            this.box_condition.SelectedIndexChanged += new System.EventHandler(this.box_condition_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "查询条件";
            // 
            // troughdata
            // 
            this.troughdata.AllowUserToAddRows = false;
            this.troughdata.AllowUserToDeleteRows = false;
            this.troughdata.AllowUserToOrderColumns = true;
            this.troughdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.troughdata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.num,
            this.troughnum,
            this.machineseq,
            this.cigarettecode,
            this.cigarettename,
            this.cigarettetype,
            this.通道类型,
            this.status,
            this.state,
            this.type,
            this.ctype,
            this.id,
            this.groupno});
            this.troughdata.Location = new System.Drawing.Point(0, 68);
            this.troughdata.MultiSelect = false;
            this.troughdata.Name = "troughdata";
            this.troughdata.ReadOnly = true;
            this.troughdata.RowTemplate.Height = 23;
            this.troughdata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.troughdata.Size = new System.Drawing.Size(1252, 576);
            this.troughdata.TabIndex = 0;
            this.troughdata.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.troughdata_CellClick);
            this.troughdata.MouseClick += new System.Windows.Forms.MouseEventHandler(this.troughdata_MouseClick);
            // 
            // num
            // 
            this.num.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.num.DataPropertyName = "num";
            this.num.HeaderText = "序号";
            this.num.Name = "num";
            this.num.ReadOnly = true;
            this.num.Visible = false;
            // 
            // troughnum
            // 
            this.troughnum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.troughnum.DataPropertyName = "troughnum";
            this.troughnum.HeaderText = "烟道编号";
            this.troughnum.Name = "troughnum";
            this.troughnum.ReadOnly = true;
            this.troughnum.Width = 78;
            // 
            // machineseq
            // 
            this.machineseq.DataPropertyName = "machineseq";
            this.machineseq.HeaderText = "设备编号";
            this.machineseq.Name = "machineseq";
            this.machineseq.ReadOnly = true;
            // 
            // cigarettecode
            // 
            this.cigarettecode.DataPropertyName = "cigarettecode";
            this.cigarettecode.HeaderText = "品牌代码";
            this.cigarettecode.Name = "cigarettecode";
            this.cigarettecode.ReadOnly = true;
            // 
            // cigarettename
            // 
            this.cigarettename.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.cigarettename.DataPropertyName = "cigarettename";
            this.cigarettename.HeaderText = "品牌名称";
            this.cigarettename.Name = "cigarettename";
            this.cigarettename.ReadOnly = true;
            this.cigarettename.Width = 78;
            // 
            // cigarettetype
            // 
            this.cigarettetype.DataPropertyName = "cigarettetype";
            this.cigarettetype.HeaderText = "品牌类型";
            this.cigarettetype.Name = "cigarettetype";
            this.cigarettetype.ReadOnly = true;
            // 
            // 通道类型
            // 
            this.通道类型.DataPropertyName = "troughtypes";
            this.通道类型.HeaderText = "通道类型";
            this.通道类型.Name = "通道类型";
            this.通道类型.ReadOnly = true;
            // 
            // status
            // 
            this.status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "使用状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 78;
            // 
            // state
            // 
            this.state.DataPropertyName = "state";
            this.state.HeaderText = "state";
            this.state.Name = "state";
            this.state.ReadOnly = true;
            this.state.Visible = false;
            // 
            // type
            // 
            this.type.DataPropertyName = "type";
            this.type.HeaderText = "type";
            this.type.Name = "type";
            this.type.ReadOnly = true;
            this.type.Visible = false;
            // 
            // ctype
            // 
            this.ctype.DataPropertyName = "ctype";
            this.ctype.HeaderText = "ctype";
            this.ctype.Name = "ctype";
            this.ctype.ReadOnly = true;
            this.ctype.Visible = false;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // groupno
            // 
            this.groupno.DataPropertyName = "groupno";
            this.groupno.HeaderText = "groupno";
            this.groupno.Name = "groupno";
            this.groupno.ReadOnly = true;
            this.groupno.Visible = false;
            // 
            // dgVprint1
            // 
            this.dgVprint1.Alignment = System.Drawing.StringAlignment.Center;
            this.dgVprint1.AutoFormat = false;
            this.dgVprint1.AutoResizeRowHeight = false;
            this.dgVprint1.Border = "1111";
            this.dgVprint1.CanEditPrintSettings = true;
            this.dgVprint1.Columns = 2;
            this.dgVprint1.ColumnSpace = 50F;
            this.dgVprint1.DefaultColor = System.Drawing.Color.Black;
            this.dgVprint1.DocuMentName = "DataGridView打印控件";
            this.dgVprint1.DoubleLineSpace = 10.16F;
            this.dgVprint1.EnableChangeGroup = true;
            this.dgVprint1.EnableChangeHeaderAndFooter = true;
            this.dgVprint1.EnableChangePageSettings = true;
            this.dgVprint1.EnableChangeSum = true;
            this.dgVprint1.EnableChangeTableSettings = true;
            this.dgVprint1.EnableChangeTableStyle = true;
            this.dgVprint1.EnableChangeTitle = true;
            this.dgVprint1.EnableChangeWaterMark = true;
            this.dgVprint1.EnableChangeZDX = true;
            this.dgVprint1.EnabledPrint = true;
            this.dgVprint1.FixedCols = 1;
            this.dgVprint1.GridColor = System.Drawing.Color.Black;
            this.dgVprint1.GroupColumn = "";
            this.dgVprint1.GroupNewPage = false;
            this.dgVprint1.IsAddRowID = false;
            this.dgVprint1.IsAutoAddEmptyRow = false;
            this.dgVprint1.IsDGVCellValignmentCenter = true;
            this.dgVprint1.IsDrawmargin = true;
            this.dgVprint1.IsDrawPageFooterLine = false;
            this.dgVprint1.IsDrawPageHeaderLine = false;
            this.dgVprint1.IsDrawTableFooterEveryPage = false;
            this.dgVprint1.IsDrawZDX = false;
            this.dgVprint1.IsGroupNewRowID = false;
            this.dgVprint1.IsImmediatePrint = false;
            this.dgVprint1.IsImmediatePrintShowPrintDialog = true;
            this.dgVprint1.IsPrintRowHeaderColumn = false;
            this.dgVprint1.IsShowAboutPage = true;
            this.dgVprint1.IsShowUnvisibleColum = true;
            this.dgVprint1.IsUseAPIprintDialog = false;
            this.dgVprint1.IsUseDoubleLine = false;
            this.dgVprint1.LastPageMode = true;
            this.dgVprint1.LineSpace = 50F;
            this.dgVprint1.MainTitle = "表格主标题";
            this.dgVprint1.MainTitleFont = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.dgVprint1.MinFontSize = 6F;
            this.dgVprint1.OuterBorder = false;
            this.dgVprint1.OuterBorderColor = System.Drawing.Color.Black;
            this.dgVprint1.OuterBorderWidth = 5.08F;
            this.dgVprint1.PageFooterColor = System.Drawing.Color.Black;
            this.dgVprint1.PageFooterFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dgVprint1.PageFooterLeft = null;
            this.dgVprint1.PageFooterMiddle = "共[总页数]页 第[页码]页";
            this.dgVprint1.PageFooterRight = null;
            this.dgVprint1.PageHeaderColor = System.Drawing.Color.Black;
            this.dgVprint1.PageHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dgVprint1.PageHeaderLeft = null;
            this.dgVprint1.PageHeaderMiddle = null;
            this.dgVprint1.PageHeaderRight = null;
            this.dgVprint1.PaperHeight = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.dgVprint1.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.dgVprint1.PaperLandscape = false;
            this.dgVprint1.PaperMargins = new System.Drawing.Printing.Margins(254, 254, 254, 254);
            this.dgVprint1.PaperName = "";
            this.dgVprint1.PaperWidth = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.dgVprint1.PrintBackColor = true;
            this.dgVprint1.PrinterName = "";
            this.dgVprint1.PrintRange = VBprinter.DGVprint.DGVPrintRange.AllVisibleRowsAndColumns;
            this.dgVprint1.PrintTitlePerPage = true;
            this.dgVprint1.PrintType = VBprinter.DGVprint.mytype.GeneralPrint;
            this.dgVprint1.PrintZero = false;
            this.dgVprint1.RowHeight = 0F;
            this.dgVprint1.ShapeDepth = 18;
            this.dgVprint1.SortColumn = "";
            this.dgVprint1.SortMode = System.ComponentModel.ListSortDirection.Ascending;
            this.dgVprint1.SubTitle = "";
            this.dgVprint1.SubTitleFont = new System.Drawing.Font("宋体", 12F);
            this.dgVprint1.SubTitleStyle = 0;
            this.dgVprint1.SumBackColor = System.Drawing.Color.Empty;
            this.dgVprint1.SumColumns = "";
            this.dgVprint1.SumFont = null;
            this.dgVprint1.SumForeColor = System.Drawing.Color.Empty;
            this.dgVprint1.SumNumberAlign = System.Drawing.StringAlignment.Center;
            this.dgVprint1.TableBottomLeftTitleAlign = System.Drawing.StringAlignment.Near;
            this.dgVprint1.TableBottomMiddleTitleAlign = System.Drawing.StringAlignment.Center;
            this.dgVprint1.TableBottomRightTitleAlign = System.Drawing.StringAlignment.Far;
            this.dgVprint1.TableFooterFont = new System.Drawing.Font("宋体", 10F);
            this.dgVprint1.TableFooterLeft = null;
            this.dgVprint1.TableFooterMiddle = null;
            this.dgVprint1.TableFooterRight = null;
            this.dgVprint1.TableHeaderFont = new System.Drawing.Font("宋体", 10F);
            this.dgVprint1.TableHeaderLeft = null;
            this.dgVprint1.TableHeaderMiddle = null;
            this.dgVprint1.TableHeaderRight = null;
            this.dgVprint1.TableTopLeftTitleAlign = System.Drawing.StringAlignment.Near;
            this.dgVprint1.TableTopMiddleTitleAlign = System.Drawing.StringAlignment.Center;
            this.dgVprint1.TableTopRightTitleAlign = System.Drawing.StringAlignment.Far;
            this.dgVprint1.TitleTextStyle = 0;
            this.dgVprint1.WaterMarkColor = System.Drawing.Color.Red;
            this.dgVprint1.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 80F, System.Drawing.FontStyle.Bold);
            this.dgVprint1.WaterMarkLandscape = true;
            this.dgVprint1.WaterMarkOpacity = ((byte)(128));
            this.dgVprint1.WaterMarkText = "";
            this.dgVprint1.WindowTitle = "打印预览结果";
            this.dgVprint1.ZDXFont = new System.Drawing.Font("宋体", 9F);
            this.dgVprint1.ZDXLinecoLor = System.Drawing.Color.Black;
            this.dgVprint1.ZDXLineStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.dgVprint1.ZDXPosition = 0F;
            this.dgVprint1.ZDXText = "装订线";
            this.dgVprint1.ZDXTextColor = System.Drawing.Color.Black;
            this.dgVprint1.ZDXType = VBprinter.DGVprint.TheZDXTYPE.LEFT;
            this.dgVprint1.ZoomToPaperWidth = true;
            // 
            // pager1
            // 
            this.pager1.CurrentPageIndex = 1;
            this.pager1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pager1.Location = new System.Drawing.Point(0, 645);
            this.pager1.Name = "pager1";
            this.pager1.RecordCount = 0;
            this.pager1.Size = new System.Drawing.Size(1099, 45);
            this.pager1.TabIndex = 1;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(80, 23);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(48, 23);
            this.button3.TabIndex = 19;
            this.button3.Text = "烟仓";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnsyncData);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Location = new System.Drawing.Point(1106, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(143, 64);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "同步";
            // 
            // win_trough
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 559);
            this.Controls.Add(this.pager1);
            this.Controls.Add(this.troughdata);
            this.Controls.Add(this.panel1);
            this.Name = "win_trough";
            this.Text = "分拣通道管理";
            this.Load += new System.EventHandler(this.win_trough_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.troughdata)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView troughdata;
        private System.Windows.Forms.ComboBox box_condition;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_keywd;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Button btn_qy;
        private System.Windows.Forms.Button btn_jy;
        private System.Windows.Forms.Button btn_amend;
        private System.Windows.Forms.Button btn_print;
        private VBprinter.DGVprint dgVprint1;
        private System.Windows.Forms.Button btn_toexcel;
        private WHC.Pager.WinControl.Pager pager1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cbtype;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox cbctype;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnsyncData;
        private System.Windows.Forms.DataGridViewTextBoxColumn num;
        private System.Windows.Forms.DataGridViewTextBoxColumn troughnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn machineseq;
        private System.Windows.Forms.DataGridViewTextBoxColumn cigarettecode;
        private System.Windows.Forms.DataGridViewTextBoxColumn cigarettename;
        private System.Windows.Forms.DataGridViewTextBoxColumn cigarettetype;
        private System.Windows.Forms.DataGridViewTextBoxColumn 通道类型;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn state;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn ctype;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn groupno;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}