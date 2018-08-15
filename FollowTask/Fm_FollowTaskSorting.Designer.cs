namespace FollowTask
{
    partial class Fm_FollowTaskSorting
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
            this.groupBoxprogramINfo = new System.Windows.Forms.GroupBox();
            this.list_data = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDevice1 = new System.Windows.Forms.Button();
            this.btnZoom = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblNum = new System.Windows.Forms.Label();
            this.lblSortnum = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnAllInfo = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.groupBoxUnionInfo = new System.Windows.Forms.GroupBox();
            this.lblErorr = new System.Windows.Forms.Label();
            this.lblGOto = new System.Windows.Forms.Label();
            this.lblCOunt = new System.Windows.Forms.Label();
            this.lblNowcOUNT = new System.Windows.Forms.Label();
            this.lblPlace = new System.Windows.Forms.Label();
            this.panelCig = new System.Windows.Forms.Panel();
            this.dgvSortingBeltInfo = new System.Windows.Forms.DataGridView();
            this.dgVprint1 = new VBprinter.DGVprint(this.components);
            this.groupBoxprogramINfo.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBoxUnionInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSortingBeltInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxprogramINfo
            // 
            this.groupBoxprogramINfo.Controls.Add(this.list_data);
            this.groupBoxprogramINfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxprogramINfo.Font = new System.Drawing.Font("宋体", 11F);
            this.groupBoxprogramINfo.Location = new System.Drawing.Point(0, 555);
            this.groupBoxprogramINfo.Name = "groupBoxprogramINfo";
            this.groupBoxprogramINfo.Size = new System.Drawing.Size(953, 18);
            this.groupBoxprogramINfo.TabIndex = 12;
            this.groupBoxprogramINfo.TabStop = false;
            this.groupBoxprogramINfo.Text = "程序信息";
            this.groupBoxprogramINfo.Visible = false;
            // 
            // list_data
            // 
            this.list_data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_data.FormattingEnabled = true;
            this.list_data.ItemHeight = 15;
            this.list_data.Location = new System.Drawing.Point(3, 20);
            this.list_data.Name = "list_data";
            this.list_data.Size = new System.Drawing.Size(947, 0);
            this.list_data.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnDevice1);
            this.panel1.Controls.Add(this.btnZoom);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.txtTitle);
            this.panel1.Controls.Add(this.lblNum);
            this.panel1.Controls.Add(this.lblSortnum);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnAllInfo);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnLast);
            this.panel1.Controls.Add(this.lblInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(953, 55);
            this.panel1.TabIndex = 13;
            this.panel1.SizeChanged += new System.EventHandler(this.panel1_SizeChanged);
            // 
            // btnDevice1
            // 
            this.btnDevice1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDevice1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDevice1.Font = new System.Drawing.Font("宋体", 9F);
            this.btnDevice1.Location = new System.Drawing.Point(720, 26);
            this.btnDevice1.Name = "btnDevice1";
            this.btnDevice1.Size = new System.Drawing.Size(81, 23);
            this.btnDevice1.TabIndex = 79;
            this.btnDevice1.Text = "设 备 查 询";
            this.btnDevice1.UseVisualStyleBackColor = true;
            this.btnDevice1.Click += new System.EventHandler(this.btnDevice1_Click);
            // 
            // btnZoom
            // 
            this.btnZoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoom.Location = new System.Drawing.Point(907, 1);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(19, 20);
            this.btnZoom.TabIndex = 28;
            this.btnZoom.Text = "口";
            this.btnZoom.UseVisualStyleBackColor = true;
            this.btnZoom.Visible = false;
            this.btnZoom.Click += new System.EventHandler(this.btnZoom_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(929, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(19, 20);
            this.btnClose.TabIndex = 27;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Visible = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(537, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 26;
            this.button1.Text = "刷新";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // txtTitle
            // 
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitle.Enabled = false;
            this.txtTitle.Location = new System.Drawing.Point(8, 3);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.ReadOnly = true;
            this.txtTitle.Size = new System.Drawing.Size(100, 21);
            this.txtTitle.TabIndex = 25;
            // 
            // lblNum
            // 
            this.lblNum.AutoSize = true;
            this.lblNum.Location = new System.Drawing.Point(352, 31);
            this.lblNum.Name = "lblNum";
            this.lblNum.Size = new System.Drawing.Size(47, 12);
            this.lblNum.TabIndex = 21;
            this.lblNum.Text = "数量：0";
            // 
            // lblSortnum
            // 
            this.lblSortnum.AutoSize = true;
            this.lblSortnum.Location = new System.Drawing.Point(250, 31);
            this.lblSortnum.Name = "lblSortnum";
            this.lblSortnum.Size = new System.Drawing.Size(59, 12);
            this.lblSortnum.TabIndex = 20;
            this.lblSortnum.Text = "任务号：0";
            // 
            // btnPrint
            // 
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrint.Location = new System.Drawing.Point(627, 26);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 18;
            this.btnPrint.Text = "打 印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnAllInfo
            // 
            this.btnAllInfo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAllInfo.Location = new System.Drawing.Point(438, 26);
            this.btnAllInfo.Name = "btnAllInfo";
            this.btnAllInfo.Size = new System.Drawing.Size(75, 23);
            this.btnAllInfo.TabIndex = 6;
            this.btnAllInfo.Text = "所 有";
            this.btnAllInfo.UseVisualStyleBackColor = true;
            this.btnAllInfo.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnNext
            // 
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNext.Location = new System.Drawing.Point(133, 26);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 7;
            this.btnNext.Text = "下一批";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLast.Location = new System.Drawing.Point(50, 26);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(75, 23);
            this.btnLast.TabIndex = 5;
            this.btnLast.Text = "上一批";
            this.btnLast.UseVisualStyleBackColor = true;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(8, 31);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(41, 12);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "操作：";
            // 
            // groupBoxUnionInfo
            // 
            this.groupBoxUnionInfo.Controls.Add(this.lblErorr);
            this.groupBoxUnionInfo.Controls.Add(this.lblGOto);
            this.groupBoxUnionInfo.Controls.Add(this.lblCOunt);
            this.groupBoxUnionInfo.Controls.Add(this.lblNowcOUNT);
            this.groupBoxUnionInfo.Controls.Add(this.lblPlace);
            this.groupBoxUnionInfo.Controls.Add(this.panelCig);
            this.groupBoxUnionInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxUnionInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBoxUnionInfo.Location = new System.Drawing.Point(0, 55);
            this.groupBoxUnionInfo.Name = "groupBoxUnionInfo";
            this.groupBoxUnionInfo.Size = new System.Drawing.Size(953, 142);
            this.groupBoxUnionInfo.TabIndex = 14;
            this.groupBoxUnionInfo.TabStop = false;
            this.groupBoxUnionInfo.Text = "皮带";
            // 
            // lblErorr
            // 
            this.lblErorr.AutoSize = true;
            this.lblErorr.Location = new System.Drawing.Point(394, 127);
            this.lblErorr.Name = "lblErorr";
            this.lblErorr.Size = new System.Drawing.Size(65, 12);
            this.lblErorr.TabIndex = 12;
            this.lblErorr.Text = "错误信息：";
            this.lblErorr.Visible = false;
            // 
            // lblGOto
            // 
            this.lblGOto.AutoSize = true;
            this.lblGOto.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblGOto.Font = new System.Drawing.Font("宋体", 11F);
            this.lblGOto.Location = new System.Drawing.Point(798, 124);
            this.lblGOto.Name = "lblGOto";
            this.lblGOto.Size = new System.Drawing.Size(152, 15);
            this.lblGOto.TabIndex = 11;
            this.lblGOto.Text = "--前往合流主皮带-->";
            // 
            // lblCOunt
            // 
            this.lblCOunt.AutoSize = true;
            this.lblCOunt.Location = new System.Drawing.Point(292, 127);
            this.lblCOunt.Name = "lblCOunt";
            this.lblCOunt.Size = new System.Drawing.Size(53, 12);
            this.lblCOunt.TabIndex = 9;
            this.lblCOunt.Text = "总批次:0";
            // 
            // lblNowcOUNT
            // 
            this.lblNowcOUNT.AutoSize = true;
            this.lblNowcOUNT.Location = new System.Drawing.Point(173, 127);
            this.lblNowcOUNT.Name = "lblNowcOUNT";
            this.lblNowcOUNT.Size = new System.Drawing.Size(77, 12);
            this.lblNowcOUNT.TabIndex = 10;
            this.lblNowcOUNT.Text = "当前批次:0/0";
            // 
            // lblPlace
            // 
            this.lblPlace.AutoSize = true;
            this.lblPlace.Location = new System.Drawing.Point(6, 127);
            this.lblPlace.Name = "lblPlace";
            this.lblPlace.Size = new System.Drawing.Size(71, 12);
            this.lblPlace.TabIndex = 7;
            this.lblPlace.Text = "当前位置：0";
            // 
            // panelCig
            // 
            this.panelCig.AutoScroll = true;
            this.panelCig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCig.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCig.Location = new System.Drawing.Point(3, 17);
            this.panelCig.Name = "panelCig";
            this.panelCig.Size = new System.Drawing.Size(947, 107);
            this.panelCig.TabIndex = 2;
            // 
            // dgvSortingBeltInfo
            // 
            this.dgvSortingBeltInfo.BackgroundColor = System.Drawing.Color.Honeydew;
            this.dgvSortingBeltInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSortingBeltInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSortingBeltInfo.Location = new System.Drawing.Point(0, 197);
            this.dgvSortingBeltInfo.Name = "dgvSortingBeltInfo";
            this.dgvSortingBeltInfo.RowTemplate.Height = 23;
            this.dgvSortingBeltInfo.Size = new System.Drawing.Size(953, 358);
            this.dgvSortingBeltInfo.TabIndex = 15;
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
            // Fm_FollowTaskSorting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 573);
            this.Controls.Add(this.dgvSortingBeltInfo);
            this.Controls.Add(this.groupBoxUnionInfo);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxprogramINfo);
            this.Name = "Fm_FollowTaskSorting";
            this.Text = "Fm_Sorting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Fm_FollowTaskSorting_FormClosing);
            this.Load += new System.EventHandler(this.Fm_FollowTaskSorting_Load);
            this.SizeChanged += new System.EventHandler(this.Fm_FollowTaskSorting_SizeChanged);
            this.groupBoxprogramINfo.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBoxUnionInfo.ResumeLayout(false);
            this.groupBoxUnionInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSortingBeltInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxprogramINfo;
        private System.Windows.Forms.ListBox list_data;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnAllInfo;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.GroupBox groupBoxUnionInfo;
        private System.Windows.Forms.Panel panelCig;
        private System.Windows.Forms.DataGridView dgvSortingBeltInfo;
        private System.Windows.Forms.Label lblPlace;
        private System.Windows.Forms.Button btnPrint;
        private VBprinter.DGVprint dgVprint1;
        private System.Windows.Forms.Label lblNum;
        private System.Windows.Forms.Label lblSortnum;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblCOunt;
        private System.Windows.Forms.Label lblNowcOUNT;
        private System.Windows.Forms.Label lblGOto;
        private System.Windows.Forms.Label lblErorr;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnZoom;
        private System.Windows.Forms.Button btnDevice1;


    }
}