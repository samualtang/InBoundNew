namespace FollowTask
{
    partial class w_UnionTask
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
            this.dgvTask = new System.Windows.Forms.DataGridView();
            this.lblSelect = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.cmbSelectC = new System.Windows.Forms.ComboBox();
            this.lblNo1 = new System.Windows.Forms.Label();
            this.txtinfo1 = new System.Windows.Forms.TextBox();
            this.dgVprint1 = new VBprinter.DGVprint(this.components);
            this.btnPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTask)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTask
            // 
            this.dgvTask.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTask.Location = new System.Drawing.Point(9, 71);
            this.dgvTask.Name = "dgvTask";
            this.dgvTask.RowTemplate.Height = 23;
            this.dgvTask.Size = new System.Drawing.Size(878, 559);
            this.dgvTask.TabIndex = 9;
            this.dgvTask.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvTask_CellFormatting);
            // 
            // lblSelect
            // 
            this.lblSelect.AutoSize = true;
            this.lblSelect.Font = new System.Drawing.Font("宋体", 11F);
            this.lblSelect.Location = new System.Drawing.Point(35, 25);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(75, 15);
            this.lblSelect.TabIndex = 8;
            this.lblSelect.Text = "查询条件:";
            // 
            // btnOk
            // 
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Font = new System.Drawing.Font("宋体", 9F);
            this.btnOk.Location = new System.Drawing.Point(593, 24);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(64, 20);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "查 询";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cmbSelectC
            // 
            this.cmbSelectC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectC.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbSelectC.FormattingEnabled = true;
            this.cmbSelectC.Location = new System.Drawing.Point(116, 22);
            this.cmbSelectC.Name = "cmbSelectC";
            this.cmbSelectC.Size = new System.Drawing.Size(140, 23);
            this.cmbSelectC.TabIndex = 5;
            this.cmbSelectC.SelectedIndexChanged += new System.EventHandler(this.cmbSelectC_SelectedIndexChanged);
            // 
            // lblNo1
            // 
            this.lblNo1.AutoSize = true;
            this.lblNo1.Font = new System.Drawing.Font("宋体", 11F);
            this.lblNo1.Location = new System.Drawing.Point(271, 25);
            this.lblNo1.Name = "lblNo1";
            this.lblNo1.Size = new System.Drawing.Size(55, 15);
            this.lblNo1.TabIndex = 21;
            this.lblNo1.Text = "label1";
            // 
            // txtinfo1
            // 
            this.txtinfo1.Font = new System.Drawing.Font("宋体", 11F);
            this.txtinfo1.Location = new System.Drawing.Point(343, 22);
            this.txtinfo1.Name = "txtinfo1";
            this.txtinfo1.Size = new System.Drawing.Size(142, 24);
            this.txtinfo1.TabIndex = 17;
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
            this.btnPrint.Location = new System.Drawing.Point(510, 24);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(64, 20);
            this.btnPrint.TabIndex = 34;
            this.btnPrint.Text = "打 印";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // w_UnionTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 652);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lblNo1);
            this.Controls.Add(this.txtinfo1);
            this.Controls.Add(this.dgvTask);
            this.Controls.Add(this.lblSelect);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cmbSelectC);
            this.Name = "w_UnionTask";
            this.Text = "w_UnionTask";
            this.Load += new System.EventHandler(this.w_UnionTask_Load);
            this.SizeChanged += new System.EventHandler(this.w_UnionTask_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTask)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTask;
        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox cmbSelectC;
        private System.Windows.Forms.Label lblNo1;
        private System.Windows.Forms.TextBox txtinfo1;
        private VBprinter.DGVprint dgVprint1;
        private System.Windows.Forms.Button btnPrint;
    }
}