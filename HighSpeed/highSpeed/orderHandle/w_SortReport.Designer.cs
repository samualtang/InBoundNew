namespace highSpeed.orderHandle
{
    partial class w_SortReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnExport = new System.Windows.Forms.Button();
            this.CmbAddress = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnSearch = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.DGVData = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgVprint2 = new VBprinter.DGVprint(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVData)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.BtnExport);
            this.panel1.Controls.Add(this.CmbAddress);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.BtnSearch);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(956, 45);
            this.panel1.TabIndex = 0;
            // 
            // BtnExport
            // 
            this.BtnExport.Location = new System.Drawing.Point(347, 11);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(75, 23);
            this.BtnExport.TabIndex = 0;
            this.BtnExport.Text = "导出";
            this.BtnExport.UseVisualStyleBackColor = true;
            this.BtnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // CmbAddress
            // 
            this.CmbAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbAddress.FormattingEnabled = true;
            this.CmbAddress.Items.AddRange(new object[] {
            "全部",
            "长沙",
            "株洲",
            "湘潭"});
            this.CmbAddress.Location = new System.Drawing.Point(78, 13);
            this.CmbAddress.Name = "CmbAddress";
            this.CmbAddress.Size = new System.Drawing.Size(121, 20);
            this.CmbAddress.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "地区：";
            // 
            // BtnSearch
            // 
            this.BtnSearch.Location = new System.Drawing.Point(217, 11);
            this.BtnSearch.Name = "BtnSearch";
            this.BtnSearch.Size = new System.Drawing.Size(75, 23);
            this.BtnSearch.TabIndex = 0;
            this.BtnSearch.Text = "查询";
            this.BtnSearch.UseVisualStyleBackColor = true;
            this.BtnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.DGVData);
            this.panel2.Location = new System.Drawing.Point(0, 52);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(956, 314);
            this.panel2.TabIndex = 1;
            // 
            // DGVData
            // 
            this.DGVData.AllowUserToAddRows = false;
            this.DGVData.AllowUserToDeleteRows = false;
            this.DGVData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGVData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGVData.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGVData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGVData.Location = new System.Drawing.Point(0, 0);
            this.DGVData.Name = "DGVData";
            this.DGVData.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVData.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DGVData.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DGVData.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.DGVData.RowTemplate.Height = 23;
            this.DGVData.Size = new System.Drawing.Size(956, 314);
            this.DGVData.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "品牌";
            this.Column1.HeaderText = "品牌";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "类型";
            this.Column2.HeaderText = "类型";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "数量";
            this.Column3.HeaderText = "数量";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
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
            // w_SortReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 369);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "w_SortReport";
            this.Text = "今日分拣品牌报表";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGVData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnExport;
        private System.Windows.Forms.ComboBox CmbAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnSearch;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView DGVData;
        private VBprinter.DGVprint dgVprint2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;

    }
}