namespace FollowTask
{
    partial class Fm_UnionMainInfo
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
            this.panelOption = new System.Windows.Forms.Panel();
            this.panelThoery = new System.Windows.Forms.Panel();
            this.btnCx = new System.Windows.Forms.Button();
            this.txtSortnum = new System.Windows.Forms.TextBox();
            this.lblTheory = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblNum = new System.Windows.Forms.Label();
            this.lblSortnum = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblOption = new System.Windows.Forms.Label();
            this.btnAllInfo = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.lblPlace = new System.Windows.Forms.Label();
            this.groupBoxUnionInfo = new System.Windows.Forms.GroupBox();
            this.lblErorr = new System.Windows.Forms.Label();
            this.lblGOto = new System.Windows.Forms.Label();
            this.lblNowcOUNT = new System.Windows.Forms.Label();
            this.lblCOunt = new System.Windows.Forms.Label();
            this.panelCig = new System.Windows.Forms.Panel();
            this.dgvMainBeltInfo = new System.Windows.Forms.DataGridView();
            this.dgVprint1 = new VBprinter.DGVprint(this.components);
            this.panelOption.SuspendLayout();
            this.panelThoery.SuspendLayout();
            this.groupBoxUnionInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMainBeltInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelOption
            // 
            this.panelOption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOption.Controls.Add(this.panelThoery);
            this.panelOption.Controls.Add(this.txtTitle);
            this.panelOption.Controls.Add(this.lblNum);
            this.panelOption.Controls.Add(this.lblSortnum);
            this.panelOption.Controls.Add(this.btnPrint);
            this.panelOption.Controls.Add(this.lblOption);
            this.panelOption.Controls.Add(this.btnAllInfo);
            this.panelOption.Controls.Add(this.btnNext);
            this.panelOption.Controls.Add(this.btnLast);
            this.panelOption.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOption.Location = new System.Drawing.Point(0, 0);
            this.panelOption.Name = "panelOption";
            this.panelOption.Size = new System.Drawing.Size(879, 58);
            this.panelOption.TabIndex = 1;
            // 
            // panelThoery
            // 
            this.panelThoery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelThoery.Controls.Add(this.btnCx);
            this.panelThoery.Controls.Add(this.txtSortnum);
            this.panelThoery.Controls.Add(this.lblTheory);
            this.panelThoery.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelThoery.Location = new System.Drawing.Point(622, 0);
            this.panelThoery.Name = "panelThoery";
            this.panelThoery.Size = new System.Drawing.Size(255, 56);
            this.panelThoery.TabIndex = 25;
            // 
            // btnCx
            // 
            this.btnCx.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCx.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCx.Location = new System.Drawing.Point(121, 26);
            this.btnCx.Name = "btnCx";
            this.btnCx.Size = new System.Drawing.Size(62, 23);
            this.btnCx.TabIndex = 31;
            this.btnCx.Text = "查询";
            this.btnCx.UseVisualStyleBackColor = true;
            this.btnCx.Click += new System.EventHandler(this.btnCx_Click);
            // 
            // txtSortnum
            // 
            this.txtSortnum.Location = new System.Drawing.Point(18, 28);
            this.txtSortnum.Name = "txtSortnum";
            this.txtSortnum.Size = new System.Drawing.Size(81, 21);
            this.txtSortnum.TabIndex = 30;
            this.txtSortnum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSortnum_KeyDown);
            this.txtSortnum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSortnum_KeyPress);
            // 
            // lblTheory
            // 
            this.lblTheory.AutoSize = true;
            this.lblTheory.Location = new System.Drawing.Point(16, 7);
            this.lblTheory.Name = "lblTheory";
            this.lblTheory.Size = new System.Drawing.Size(125, 12);
            this.lblTheory.TabIndex = 29;
            this.lblTheory.Text = "查询任务号理论数据：";
            // 
            // txtTitle
            // 
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitle.Enabled = false;
            this.txtTitle.Location = new System.Drawing.Point(11, 3);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ReadOnly = true;
            this.txtTitle.Size = new System.Drawing.Size(100, 21);
            this.txtTitle.TabIndex = 24;
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.Location = new System.Drawing.Point(387, 32);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(47, 12);
            this.lblNum.TabIndex = 23;
            this.lblNum.Text = "数量：0";
            // 
            // lblSortnum
            // 
            this.lblSortnum.AutoSize = true;
            this.lblSortnum.Location = new System.Drawing.Point(285, 32);
            this.lblSortnum.Name = "lblSortnum";
            this.lblSortnum.Size = new System.Drawing.Size(59, 12);
            this.lblSortnum.TabIndex = 22;
            this.lblSortnum.Text = "任务号：0";
            // 
            // btnPrint
            // 
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrint.Location = new System.Drawing.Point(541, 27);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 19;
            this.btnPrint.Text = "打 印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lblOption
            // 
            this.lblOption.AutoSize = true;
            this.lblOption.Location = new System.Drawing.Point(3, 32);
            this.lblOption.Name = "lblOption";
            this.lblOption.Size = new System.Drawing.Size(47, 12);
            this.lblOption.TabIndex = 5;
            this.lblOption.Text = "操 作：";
            // 
            // btnAllInfo
            // 
            this.btnAllInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAllInfo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAllInfo.Location = new System.Drawing.Point(449, 27);
            this.btnAllInfo.Name = "btnAllInfo";
            this.btnAllInfo.Size = new System.Drawing.Size(75, 23);
            this.btnAllInfo.TabIndex = 4;
            this.btnAllInfo.Text = "所 有";
            this.btnAllInfo.UseVisualStyleBackColor = true;
            this.btnAllInfo.Click += new System.EventHandler(this.btnAllInfo_Click);
            // 
            // btnNext
            // 
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNext.Location = new System.Drawing.Point(167, 27);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "下一批";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLast.Location = new System.Drawing.Point(63, 27);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(75, 23);
            this.btnLast.TabIndex = 3;
            this.btnLast.Text = "上一批";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // lblPlace
            // 
            this.lblPlace.AutoSize = true;
            this.lblPlace.Location = new System.Drawing.Point(4, 124);
            this.lblPlace.Name = "lblPlace";
            this.lblPlace.Size = new System.Drawing.Size(71, 12);
            this.lblPlace.TabIndex = 6;
            this.lblPlace.Text = "当前位置：0";
            // 
            // groupBoxUnionInfo
            // 
            this.groupBoxUnionInfo.Controls.Add(this.lblErorr);
            this.groupBoxUnionInfo.Controls.Add(this.lblGOto);
            this.groupBoxUnionInfo.Controls.Add(this.lblNowcOUNT);
            this.groupBoxUnionInfo.Controls.Add(this.lblCOunt);
            this.groupBoxUnionInfo.Controls.Add(this.lblPlace);
            this.groupBoxUnionInfo.Controls.Add(this.panelCig);
            this.groupBoxUnionInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxUnionInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBoxUnionInfo.Location = new System.Drawing.Point(0, 58);
            this.groupBoxUnionInfo.Name = "groupBoxUnionInfo";
            this.groupBoxUnionInfo.Size = new System.Drawing.Size(879, 141);
            this.groupBoxUnionInfo.TabIndex = 2;
            this.groupBoxUnionInfo.TabStop = false;
            this.groupBoxUnionInfo.Text = "皮带";
            // 
            // lblErorr
            // 
            this.lblErorr.AutoSize = true;
            this.lblErorr.Location = new System.Drawing.Point(394, 123);
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
            this.lblGOto.Location = new System.Drawing.Point(754, 116);
            this.lblGOto.Name = "lblGOto";
            this.lblGOto.Size = new System.Drawing.Size(122, 15);
            this.lblGOto.TabIndex = 9;
            this.lblGOto.Text = "--前往包装机-->";
            // 
            // lblNowcOUNT
            // 
            this.lblNowcOUNT.AutoSize = true;
            this.lblNowcOUNT.Location = new System.Drawing.Point(162, 124);
            this.lblNowcOUNT.Name = "lblNowcOUNT";
            this.lblNowcOUNT.Size = new System.Drawing.Size(77, 12);
            this.lblNowcOUNT.TabIndex = 8;
            this.lblNowcOUNT.Text = "当前批次:0/0";
            // 
            // lblCOunt
            // 
            this.lblCOunt.AutoSize = true;
            this.lblCOunt.Location = new System.Drawing.Point(286, 123);
            this.lblCOunt.Name = "lblCOunt";
            this.lblCOunt.Size = new System.Drawing.Size(53, 12);
            this.lblCOunt.TabIndex = 7;
            this.lblCOunt.Text = "总批次:0";
            // 
            // panelCig
            // 
            this.panelCig.AutoScroll = true;
            this.panelCig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCig.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCig.Location = new System.Drawing.Point(3, 17);
            this.panelCig.Name = "panelCig";
            this.panelCig.Size = new System.Drawing.Size(873, 99);
            this.panelCig.TabIndex = 2;
            // 
            // dgvMainBeltInfo
            // 
            this.dgvMainBeltInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMainBeltInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMainBeltInfo.Location = new System.Drawing.Point(0, 199);
            this.dgvMainBeltInfo.Name = "dgvMainBeltInfo";
            this.dgvMainBeltInfo.RowTemplate.Height = 23;
            this.dgvMainBeltInfo.Size = new System.Drawing.Size(879, 215);
            this.dgvMainBeltInfo.TabIndex = 3;
            this.dgvMainBeltInfo.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvMainBeltInfo_CellFormatting);
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
            // Fm_UnionMainInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 414);
            this.Controls.Add(this.dgvMainBeltInfo);
            this.Controls.Add(this.groupBoxUnionInfo);
            this.Controls.Add(this.panelOption);
            this.Name = "Fm_UnionMainInfo";
            this.Text = "合流主皮带";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Fm_UnionMainInfo_FormClosing);
            this.SizeChanged += new System.EventHandler(this.Fm_UnionMainInfo_SizeChanged);
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

        private System.Windows.Forms.Panel panelOption;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.GroupBox groupBoxUnionInfo;
        private System.Windows.Forms.Panel panelCig;
        private System.Windows.Forms.Label lblOption;
        private System.Windows.Forms.Button btnAllInfo;
        private System.Windows.Forms.DataGridView dgvMainBeltInfo;
        private System.Windows.Forms.Label lblPlace;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.Label lblSortnum;
        private VBprinter.DGVprint dgVprint1;
        private System.Windows.Forms.Label lblNowcOUNT;
        private System.Windows.Forms.Label lblCOunt;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblGOto;
        private System.Windows.Forms.Label lblErorr;
        private System.Windows.Forms.Panel panelThoery;
        private System.Windows.Forms.Button btnCx;
        private System.Windows.Forms.TextBox txtSortnum;
        private System.Windows.Forms.Label lblTheory;
    }
}