namespace highSpeed.orderHandle
{
    partial class w_uniondata
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
            this.orderdata = new System.Windows.Forms.DataGridView();
            this.btn_print = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.dgVprint2 = new VBprinter.DGVprint(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtSortNum = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.CUSTOMERNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.billcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.任务号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.组号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cigarettecode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cigarettename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pokenum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.orderdata)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // orderdata
            // 
            this.orderdata.AllowUserToAddRows = false;
            this.orderdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.orderdata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CUSTOMERNAME,
            this.billcode,
            this.任务号,
            this.组号,
            this.ccount,
            this.cigarettecode,
            this.cigarettename,
            this.pokenum});
            this.orderdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.orderdata.Location = new System.Drawing.Point(0, 43);
            this.orderdata.Name = "orderdata";
            this.orderdata.ReadOnly = true;
            this.orderdata.RowTemplate.Height = 23;
            this.orderdata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.orderdata.Size = new System.Drawing.Size(1017, 283);
            this.orderdata.TabIndex = 1;
            // 
            // btn_print
            // 
            this.btn_print.Location = new System.Drawing.Point(804, 12);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(75, 23);
            this.btn_print.TabIndex = 0;
            this.btn_print.Text = "打印";
            this.btn_print.UseVisualStyleBackColor = true;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtSortNum);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btn_print);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1017, 43);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(905, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "导出";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgVprint2
            // 
            this.dgVprint2.Alignment = System.Drawing.StringAlignment.Center;
            this.dgVprint2.AutoFormat = false;
            this.dgVprint2.AutoResizeRowHeight = false;
            this.dgVprint2.Border = "1111";
            this.dgVprint2.CanEditPrintSettings = true;
            this.dgVprint2.Columns = 2;
            this.dgVprint2.ColumnSpace = 50F;
            this.dgVprint2.DefaultColor = System.Drawing.Color.Black;
            this.dgVprint2.DocuMentName = "DataGridView打印控件";
            this.dgVprint2.DoubleLineSpace = 10.16F;
            this.dgVprint2.EnableChangeGroup = true;
            this.dgVprint2.EnableChangeHeaderAndFooter = true;
            this.dgVprint2.EnableChangePageSettings = true;
            this.dgVprint2.EnableChangeSum = true;
            this.dgVprint2.EnableChangeTableSettings = true;
            this.dgVprint2.EnableChangeTableStyle = true;
            this.dgVprint2.EnableChangeTitle = true;
            this.dgVprint2.EnableChangeWaterMark = true;
            this.dgVprint2.EnableChangeZDX = true;
            this.dgVprint2.EnabledPrint = true;
            this.dgVprint2.FixedCols = 1;
            this.dgVprint2.GridColor = System.Drawing.Color.Black;
            this.dgVprint2.GroupColumn = "";
            this.dgVprint2.GroupNewPage = false;
            this.dgVprint2.IsAddRowID = false;
            this.dgVprint2.IsAutoAddEmptyRow = false;
            this.dgVprint2.IsDGVCellValignmentCenter = true;
            this.dgVprint2.IsDrawmargin = true;
            this.dgVprint2.IsDrawPageFooterLine = false;
            this.dgVprint2.IsDrawPageHeaderLine = false;
            this.dgVprint2.IsDrawTableFooterEveryPage = false;
            this.dgVprint2.IsDrawZDX = false;
            this.dgVprint2.IsGroupNewRowID = false;
            this.dgVprint2.IsImmediatePrint = false;
            this.dgVprint2.IsImmediatePrintShowPrintDialog = true;
            this.dgVprint2.IsPrintRowHeaderColumn = false;
            this.dgVprint2.IsShowAboutPage = true;
            this.dgVprint2.IsShowUnvisibleColum = true;
            this.dgVprint2.IsUseAPIprintDialog = false;
            this.dgVprint2.IsUseDoubleLine = false;
            this.dgVprint2.LastPageMode = true;
            this.dgVprint2.LineSpace = 50F;
            this.dgVprint2.MainTitle = "表格主标题";
            this.dgVprint2.MainTitleFont = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.dgVprint2.MinFontSize = 6F;
            this.dgVprint2.OuterBorder = false;
            this.dgVprint2.OuterBorderColor = System.Drawing.Color.Black;
            this.dgVprint2.OuterBorderWidth = 5.08F;
            this.dgVprint2.PageFooterColor = System.Drawing.Color.Black;
            this.dgVprint2.PageFooterFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dgVprint2.PageFooterLeft = null;
            this.dgVprint2.PageFooterMiddle = "共[总页数]页 第[页码]页";
            this.dgVprint2.PageFooterRight = null;
            this.dgVprint2.PageHeaderColor = System.Drawing.Color.Black;
            this.dgVprint2.PageHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dgVprint2.PageHeaderLeft = null;
            this.dgVprint2.PageHeaderMiddle = null;
            this.dgVprint2.PageHeaderRight = null;
            this.dgVprint2.PaperHeight = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.dgVprint2.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.dgVprint2.PaperLandscape = false;
            this.dgVprint2.PaperMargins = new System.Drawing.Printing.Margins(254, 254, 254, 254);
            this.dgVprint2.PaperName = "";
            this.dgVprint2.PaperWidth = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.dgVprint2.PrintBackColor = true;
            this.dgVprint2.PrinterName = "";
            this.dgVprint2.PrintRange = VBprinter.DGVprint.DGVPrintRange.AllVisibleRowsAndColumns;
            this.dgVprint2.PrintTitlePerPage = true;
            this.dgVprint2.PrintType = VBprinter.DGVprint.mytype.GeneralPrint;
            this.dgVprint2.PrintZero = false;
            this.dgVprint2.RowHeight = 0F;
            this.dgVprint2.ShapeDepth = 18;
            this.dgVprint2.SortColumn = "";
            this.dgVprint2.SortMode = System.ComponentModel.ListSortDirection.Ascending;
            this.dgVprint2.SubTitle = "";
            this.dgVprint2.SubTitleFont = new System.Drawing.Font("宋体", 12F);
            this.dgVprint2.SubTitleStyle = 0;
            this.dgVprint2.SumBackColor = System.Drawing.Color.Empty;
            this.dgVprint2.SumColumns = "";
            this.dgVprint2.SumFont = null;
            this.dgVprint2.SumForeColor = System.Drawing.Color.Empty;
            this.dgVprint2.SumNumberAlign = System.Drawing.StringAlignment.Center;
            this.dgVprint2.TableBottomLeftTitleAlign = System.Drawing.StringAlignment.Near;
            this.dgVprint2.TableBottomMiddleTitleAlign = System.Drawing.StringAlignment.Center;
            this.dgVprint2.TableBottomRightTitleAlign = System.Drawing.StringAlignment.Far;
            this.dgVprint2.TableFooterFont = new System.Drawing.Font("宋体", 10F);
            this.dgVprint2.TableFooterLeft = null;
            this.dgVprint2.TableFooterMiddle = null;
            this.dgVprint2.TableFooterRight = null;
            this.dgVprint2.TableHeaderFont = new System.Drawing.Font("宋体", 10F);
            this.dgVprint2.TableHeaderLeft = null;
            this.dgVprint2.TableHeaderMiddle = null;
            this.dgVprint2.TableHeaderRight = null;
            this.dgVprint2.TableTopLeftTitleAlign = System.Drawing.StringAlignment.Near;
            this.dgVprint2.TableTopMiddleTitleAlign = System.Drawing.StringAlignment.Center;
            this.dgVprint2.TableTopRightTitleAlign = System.Drawing.StringAlignment.Far;
            this.dgVprint2.TitleTextStyle = 0;
            this.dgVprint2.WaterMarkColor = System.Drawing.Color.Red;
            this.dgVprint2.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 80F, System.Drawing.FontStyle.Bold);
            this.dgVprint2.WaterMarkLandscape = true;
            this.dgVprint2.WaterMarkOpacity = ((byte)(128));
            this.dgVprint2.WaterMarkText = "";
            this.dgVprint2.WindowTitle = "打印预览结果";
            this.dgVprint2.ZDXFont = new System.Drawing.Font("宋体", 9F);
            this.dgVprint2.ZDXLinecoLor = System.Drawing.Color.Black;
            this.dgVprint2.ZDXLineStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.dgVprint2.ZDXPosition = 0F;
            this.dgVprint2.ZDXText = "装订线";
            this.dgVprint2.ZDXTextColor = System.Drawing.Color.Black;
            this.dgVprint2.ZDXType = VBprinter.DGVprint.TheZDXTYPE.LEFT;
            this.dgVprint2.ZoomToPaperWidth = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "任务号:";
            // 
            // txtSortNum
            // 
            this.txtSortNum.Location = new System.Drawing.Point(63, 15);
            this.txtSortNum.Name = "txtSortNum";
            this.txtSortNum.Size = new System.Drawing.Size(100, 21);
            this.txtSortNum.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(173, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 30);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // CUSTOMERNAME
            // 
            this.CUSTOMERNAME.DataPropertyName = "CUSTOMERNAME";
            this.CUSTOMERNAME.HeaderText = "客户名称";
            this.CUSTOMERNAME.Name = "CUSTOMERNAME";
            this.CUSTOMERNAME.ReadOnly = true;
            // 
            // billcode
            // 
            this.billcode.DataPropertyName = "BILLCODE";
            this.billcode.HeaderText = "订单号";
            this.billcode.Name = "billcode";
            this.billcode.ReadOnly = true;
            // 
            // 任务号
            // 
            this.任务号.DataPropertyName = "SortNum";
            this.任务号.HeaderText = "任务号";
            this.任务号.Name = "任务号";
            this.任务号.ReadOnly = true;
            // 
            // 组号
            // 
            this.组号.DataPropertyName = "groupno";
            this.组号.HeaderText = "组号";
            this.组号.Name = "组号";
            this.组号.ReadOnly = true;
            // 
            // ccount
            // 
            this.ccount.DataPropertyName = "sortseq";
            this.ccount.HeaderText = "送货顺序";
            this.ccount.Name = "ccount";
            this.ccount.ReadOnly = true;
            // 
            // cigarettecode
            // 
            this.cigarettecode.DataPropertyName = "CIGARETTDECODE";
            this.cigarettecode.HeaderText = "品牌代码";
            this.cigarettecode.Name = "cigarettecode";
            this.cigarettecode.ReadOnly = true;
            // 
            // cigarettename
            // 
            this.cigarettename.DataPropertyName = "CIGARETTDENAME";
            this.cigarettename.HeaderText = "品牌名称";
            this.cigarettename.Name = "cigarettename";
            this.cigarettename.ReadOnly = true;
            // 
            // pokenum
            // 
            this.pokenum.DataPropertyName = "qty";
            this.pokenum.HeaderText = "数量";
            this.pokenum.Name = "pokenum";
            this.pokenum.ReadOnly = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(275, 11);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(102, 27);
            this.button2.TabIndex = 5;
            this.button2.Text = "同时查询前后单";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // w_uniondata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 326);
            this.Controls.Add(this.orderdata);
            this.Controls.Add(this.panel1);
            this.Name = "w_uniondata";
            this.Text = "合流信息还原";
            ((System.ComponentModel.ISupportInitialize)(this.orderdata)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView orderdata;
        private VBprinter.DGVprint dgVprint1;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Panel panel1;
        private VBprinter.DGVprint dgVprint2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSortNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUSTOMERNAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn billcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn 任务号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 组号;
        private System.Windows.Forms.DataGridViewTextBoxColumn ccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn cigarettecode;
        private System.Windows.Forms.DataGridViewTextBoxColumn cigarettename;
        private System.Windows.Forms.DataGridViewTextBoxColumn pokenum;
        private System.Windows.Forms.Button button2;
    }
}