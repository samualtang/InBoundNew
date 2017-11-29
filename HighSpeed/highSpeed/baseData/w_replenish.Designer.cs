namespace highSpeed.baseData
{
    partial class win_replenish
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
            this.replenishdata = new System.Windows.Forms.DataGridView();
            this.box_trough = new System.Windows.Forms.ComboBox();
            this.box_types = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_search = new System.Windows.Forms.Button();
            this.txt_keywd = new System.Windows.Forms.TextBox();
            this.btn_new = new System.Windows.Forms.Button();
            this.btn_amend = new System.Windows.Forms.Button();
            this.btn_qy = new System.Windows.Forms.Button();
            this.btn_jy = new System.Windows.Forms.Button();
            this.btn_toexcel = new System.Windows.Forms.Button();
            this.btn_print = new System.Windows.Forms.Button();
            this.dgVprint1 = new VBprinter.DGVprint(this.components);
            this.rownum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.troughinfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.replenishnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.replenishdesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.replenishseq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cigarettecode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cigarettename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.replenishdata)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_print);
            this.panel1.Controls.Add(this.btn_toexcel);
            this.panel1.Controls.Add(this.btn_jy);
            this.panel1.Controls.Add(this.btn_qy);
            this.panel1.Controls.Add(this.btn_amend);
            this.panel1.Controls.Add(this.btn_new);
            this.panel1.Controls.Add(this.txt_keywd);
            this.panel1.Controls.Add(this.btn_search);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.box_types);
            this.panel1.Controls.Add(this.box_trough);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1020, 45);
            this.panel1.TabIndex = 0;
            // 
            // replenishdata
            // 
            this.replenishdata.AllowUserToAddRows = false;
            this.replenishdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.replenishdata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rownum,
            this.id,
            this.troughinfo,
            this.replenishnum,
            this.replenishdesc,
            this.replenishseq,
            this.cigarettecode,
            this.cigarettename,
            this.state,
            this.statue});
            this.replenishdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.replenishdata.Location = new System.Drawing.Point(0, 45);
            this.replenishdata.Name = "replenishdata";
            this.replenishdata.ReadOnly = true;
            this.replenishdata.RowTemplate.Height = 23;
            this.replenishdata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.replenishdata.Size = new System.Drawing.Size(1020, 217);
            this.replenishdata.TabIndex = 1;
            this.replenishdata.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.replenishdata_CellClick);
            // 
            // box_trough
            // 
            this.box_trough.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.box_trough.FormattingEnabled = true;
            this.box_trough.Location = new System.Drawing.Point(62, 12);
            this.box_trough.Name = "box_trough";
            this.box_trough.Size = new System.Drawing.Size(164, 20);
            this.box_trough.TabIndex = 0;
            // 
            // box_types
            // 
            this.box_types.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.box_types.FormattingEnabled = true;
            this.box_types.Location = new System.Drawing.Point(267, 11);
            this.box_types.Name = "box_types";
            this.box_types.Size = new System.Drawing.Size(77, 20);
            this.box_types.TabIndex = 1;
            this.box_types.SelectedIndexChanged += new System.EventHandler(this.box_types_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "烟道选择";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "类型";
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(456, 9);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 23);
            this.btn_search.TabIndex = 4;
            this.btn_search.Text = "查询";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // txt_keywd
            // 
            this.txt_keywd.Location = new System.Drawing.Point(350, 11);
            this.txt_keywd.Name = "txt_keywd";
            this.txt_keywd.Size = new System.Drawing.Size(100, 21);
            this.txt_keywd.TabIndex = 5;
            this.txt_keywd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_keywd_KeyDown);
            // 
            // btn_new
            // 
            this.btn_new.Location = new System.Drawing.Point(537, 9);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(75, 23);
            this.btn_new.TabIndex = 6;
            this.btn_new.Text = "新增";
            this.btn_new.UseVisualStyleBackColor = true;
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // btn_amend
            // 
            this.btn_amend.Location = new System.Drawing.Point(618, 9);
            this.btn_amend.Name = "btn_amend";
            this.btn_amend.Size = new System.Drawing.Size(75, 23);
            this.btn_amend.TabIndex = 7;
            this.btn_amend.Text = "修改";
            this.btn_amend.UseVisualStyleBackColor = true;
            this.btn_amend.Click += new System.EventHandler(this.btn_amend_Click);
            // 
            // btn_qy
            // 
            this.btn_qy.Location = new System.Drawing.Point(699, 9);
            this.btn_qy.Name = "btn_qy";
            this.btn_qy.Size = new System.Drawing.Size(75, 23);
            this.btn_qy.TabIndex = 8;
            this.btn_qy.Text = "启用";
            this.btn_qy.UseVisualStyleBackColor = true;
            this.btn_qy.Click += new System.EventHandler(this.btn_qy_Click);
            // 
            // btn_jy
            // 
            this.btn_jy.Location = new System.Drawing.Point(780, 9);
            this.btn_jy.Name = "btn_jy";
            this.btn_jy.Size = new System.Drawing.Size(75, 23);
            this.btn_jy.TabIndex = 9;
            this.btn_jy.Text = "停用";
            this.btn_jy.UseVisualStyleBackColor = true;
            this.btn_jy.Click += new System.EventHandler(this.btn_jy_Click);
            // 
            // btn_toexcel
            // 
            this.btn_toexcel.Location = new System.Drawing.Point(942, 9);
            this.btn_toexcel.Name = "btn_toexcel";
            this.btn_toexcel.Size = new System.Drawing.Size(75, 23);
            this.btn_toexcel.TabIndex = 10;
            this.btn_toexcel.Text = "导出";
            this.btn_toexcel.UseVisualStyleBackColor = true;
            this.btn_toexcel.Click += new System.EventHandler(this.btn_toexcel_Click);
            // 
            // btn_print
            // 
            this.btn_print.Location = new System.Drawing.Point(861, 9);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(75, 23);
            this.btn_print.TabIndex = 11;
            this.btn_print.Text = "打印";
            this.btn_print.UseVisualStyleBackColor = true;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
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
            this.dgVprint1.MainTitleFont = new System.Drawing.Font("黑体", 16F, System.Drawing.FontStyle.Bold);
            this.dgVprint1.MinFontSize = 6F;
            this.dgVprint1.OuterBorder = false;
            this.dgVprint1.OuterBorderColor = System.Drawing.Color.Black;
            this.dgVprint1.OuterBorderWidth = 5.08F;
            this.dgVprint1.PageFooterColor = System.Drawing.Color.Black;
            this.dgVprint1.PageFooterFont = new System.Drawing.Font("华文行楷", 9F);
            this.dgVprint1.PageFooterLeft = null;
            this.dgVprint1.PageFooterMiddle = "共[总页数]页 第[页码]页";
            this.dgVprint1.PageFooterRight = null;
            this.dgVprint1.PageHeaderColor = System.Drawing.Color.Black;
            this.dgVprint1.PageHeaderFont = new System.Drawing.Font("华文行楷", 9F);
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
            this.dgVprint1.WaterMarkFont = new System.Drawing.Font("华文行楷", 80F, System.Drawing.FontStyle.Bold);
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
            // rownum
            // 
            this.rownum.DataPropertyName = "rownum";
            this.rownum.HeaderText = "序号";
            this.rownum.Name = "rownum";
            this.rownum.ReadOnly = true;
            this.rownum.Width = 54;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // troughinfo
            // 
            this.troughinfo.DataPropertyName = "troughinfo";
            this.troughinfo.HeaderText = "分拣通道信息";
            this.troughinfo.Name = "troughinfo";
            this.troughinfo.ReadOnly = true;
            this.troughinfo.Width = 150;
            // 
            // replenishnum
            // 
            this.replenishnum.DataPropertyName = "replenishnum";
            this.replenishnum.HeaderText = "补货通道编号";
            this.replenishnum.Name = "replenishnum";
            this.replenishnum.ReadOnly = true;
            this.replenishnum.Width = 120;
            // 
            // replenishdesc
            // 
            this.replenishdesc.DataPropertyName = "replenishdesc";
            this.replenishdesc.HeaderText = "补货通道描述";
            this.replenishdesc.Name = "replenishdesc";
            this.replenishdesc.ReadOnly = true;
            this.replenishdesc.Width = 120;
            // 
            // replenishseq
            // 
            this.replenishseq.DataPropertyName = "replenishseq";
            this.replenishseq.HeaderText = "补货通道顺序";
            this.replenishseq.Name = "replenishseq";
            this.replenishseq.ReadOnly = true;
            this.replenishseq.Width = 120;
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
            this.cigarettename.DataPropertyName = "cigarettename";
            this.cigarettename.HeaderText = "品牌名称";
            this.cigarettename.Name = "cigarettename";
            this.cigarettename.ReadOnly = true;
            // 
            // state
            // 
            this.state.DataPropertyName = "state";
            this.state.HeaderText = "state";
            this.state.Name = "state";
            this.state.ReadOnly = true;
            this.state.Visible = false;
            // 
            // statue
            // 
            this.statue.DataPropertyName = "statue";
            this.statue.HeaderText = "补货通道状态";
            this.statue.Name = "statue";
            this.statue.ReadOnly = true;
            // 
            // win_replenish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 262);
            this.Controls.Add(this.replenishdata);
            this.Controls.Add(this.panel1);
            this.Name = "win_replenish";
            this.Text = "补货通道信息";
            this.Load += new System.EventHandler(this.win_replenish_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.replenishdata)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView replenishdata;
        private System.Windows.Forms.TextBox txt_keywd;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox box_types;
        private System.Windows.Forms.ComboBox box_trough;
        private System.Windows.Forms.Button btn_jy;
        private System.Windows.Forms.Button btn_qy;
        private System.Windows.Forms.Button btn_amend;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Button btn_toexcel;
        private VBprinter.DGVprint dgVprint1;
        private System.Windows.Forms.DataGridViewTextBoxColumn rownum;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn troughinfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn replenishnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn replenishdesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn replenishseq;
        private System.Windows.Forms.DataGridViewTextBoxColumn cigarettecode;
        private System.Windows.Forms.DataGridViewTextBoxColumn cigarettename;
        private System.Windows.Forms.DataGridViewTextBoxColumn state;
        private System.Windows.Forms.DataGridViewTextBoxColumn statue;
    }
}