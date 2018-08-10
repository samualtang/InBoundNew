namespace FollowTask
{
    partial class w_UnNormal
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
            this.dgvMainBeltInfo = new System.Windows.Forms.DataGridView();
            this.lblPackageNo = new System.Windows.Forms.Label();
            this.panelOption = new System.Windows.Forms.Panel();
            this.txtPackageMachine = new System.Windows.Forms.TextBox();
            this.lblTheory = new System.Windows.Forms.Label();
            this.btnEnter = new System.Windows.Forms.Button();
            this.txtDeviceNo = new System.Windows.Forms.TextBox();
            this.dgVprint1 = new VBprinter.DGVprint(this.components);
            this.btnPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMainBeltInfo)).BeginInit();
            this.panelOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMainBeltInfo
            // 
            this.dgvMainBeltInfo.BackgroundColor = System.Drawing.Color.Honeydew;
            this.dgvMainBeltInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMainBeltInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMainBeltInfo.Location = new System.Drawing.Point(0, 58);
            this.dgvMainBeltInfo.Name = "dgvMainBeltInfo";
            this.dgvMainBeltInfo.RowTemplate.Height = 23;
            this.dgvMainBeltInfo.Size = new System.Drawing.Size(756, 407);
            this.dgvMainBeltInfo.TabIndex = 11;
            // 
            // lblPackageNo
            // 
            this.lblPackageNo.AutoSize = true;
            this.lblPackageNo.Font = new System.Drawing.Font("宋体", 11F);
            this.lblPackageNo.Location = new System.Drawing.Point(132, 26);
            this.lblPackageNo.Name = "lblPackageNo";
            this.lblPackageNo.Size = new System.Drawing.Size(97, 15);
            this.lblPackageNo.TabIndex = 4;
            this.lblPackageNo.Text = "包装机编号：";
            // 
            // panelOption
            // 
            this.panelOption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOption.Controls.Add(this.btnPrint);
            this.panelOption.Controls.Add(this.lblTheory);
            this.panelOption.Controls.Add(this.btnEnter);
            this.panelOption.Controls.Add(this.txtDeviceNo);
            this.panelOption.Controls.Add(this.txtPackageMachine);
            this.panelOption.Controls.Add(this.lblPackageNo);
            this.panelOption.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOption.Location = new System.Drawing.Point(0, 0);
            this.panelOption.Name = "panelOption";
            this.panelOption.Size = new System.Drawing.Size(756, 58);
            this.panelOption.TabIndex = 9;
            // 
            // txtPackageMachine
            // 
            this.txtPackageMachine.Location = new System.Drawing.Point(224, 20);
            this.txtPackageMachine.Name = "txtPackageMachine";
            this.txtPackageMachine.Size = new System.Drawing.Size(94, 21);
            this.txtPackageMachine.TabIndex = 27;
            this.txtPackageMachine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDeviceNo_KeyDown);
            // 
            // lblTheory
            // 
            this.lblTheory.AutoSize = true;
            this.lblTheory.Font = new System.Drawing.Font("宋体", 11F);
            this.lblTheory.Location = new System.Drawing.Point(324, 26);
            this.lblTheory.Name = "lblTheory";
            this.lblTheory.Size = new System.Drawing.Size(97, 15);
            this.lblTheory.TabIndex = 32;
            this.lblTheory.Text = "查询任务号：";
            // 
            // btnEnter
            // 
            this.btnEnter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEnter.Location = new System.Drawing.Point(658, 19);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(66, 20);
            this.btnEnter.TabIndex = 31;
            this.btnEnter.Text = "查询";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // txtDeviceNo
            // 
            this.txtDeviceNo.Location = new System.Drawing.Point(427, 20);
            this.txtDeviceNo.Name = "txtDeviceNo";
            this.txtDeviceNo.Size = new System.Drawing.Size(100, 21);
            this.txtDeviceNo.TabIndex = 30;
            this.txtDeviceNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDeviceNo_KeyDown);
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
            // btnPrint
            // 
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrint.Location = new System.Drawing.Point(571, 19);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(64, 20);
            this.btnPrint.TabIndex = 33;
            this.btnPrint.Text = "打 印";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // w_UnNormal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 465);
            this.Controls.Add(this.dgvMainBeltInfo);
            this.Controls.Add(this.panelOption);
            this.Name = "w_UnNormal";
            this.Text = "异型烟包装机缓存查询";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMainBeltInfo)).EndInit();
            this.panelOption.ResumeLayout(false);
            this.panelOption.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMainBeltInfo;
        private System.Windows.Forms.Label lblPackageNo;
        private System.Windows.Forms.Panel panelOption;
        private System.Windows.Forms.Label lblTheory;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.TextBox txtDeviceNo;
        private System.Windows.Forms.TextBox txtPackageMachine;
        private System.Windows.Forms.Button btnPrint;
        private VBprinter.DGVprint dgVprint1;

    }
}