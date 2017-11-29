namespace highSpeed.baseData
{
    partial class win_exportline
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
            this.exportlinedata = new System.Windows.Forms.DataGridView();
            this.btn_new = new System.Windows.Forms.Button();
            this.btn_print = new System.Windows.Forms.Button();
            this.btn_jy = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_toexcel = new System.Windows.Forms.Button();
            this.btn_qy = new System.Windows.Forms.Button();
            this.btn_amend = new System.Windows.Forms.Button();
            this.dgVprint1 = new VBprinter.DGVprint(this.components);
            this.rownum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exportnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exportdesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.machinenum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parentnum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exportseq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exportlevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exportstate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskstate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskarived = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskfinish = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskconfirm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskstorage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exportlinedata)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_amend);
            this.panel1.Controls.Add(this.btn_qy);
            this.panel1.Controls.Add(this.btn_toexcel);
            this.panel1.Controls.Add(this.btn_close);
            this.panel1.Controls.Add(this.btn_jy);
            this.panel1.Controls.Add(this.btn_print);
            this.panel1.Controls.Add(this.btn_new);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1014, 47);
            this.panel1.TabIndex = 0;
            // 
            // exportlinedata
            // 
            this.exportlinedata.AllowUserToAddRows = false;
            this.exportlinedata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.exportlinedata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rownum,
            this.id,
            this.exportnum,
            this.exportdesc,
            this.machinenum,
            this.parentnum,
            this.exportseq,
            this.state,
            this.exportlevel,
            this.status,
            this.exportstate,
            this.taskstate,
            this.taskarived,
            this.taskfinish,
            this.taskconfirm,
            this.taskstorage});
            this.exportlinedata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exportlinedata.Location = new System.Drawing.Point(0, 47);
            this.exportlinedata.MultiSelect = false;
            this.exportlinedata.Name = "exportlinedata";
            this.exportlinedata.ReadOnly = true;
            this.exportlinedata.RowTemplate.Height = 23;
            this.exportlinedata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.exportlinedata.Size = new System.Drawing.Size(1014, 215);
            this.exportlinedata.TabIndex = 1;
            this.exportlinedata.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.exportlinedata_CellClick);
            // 
            // btn_new
            // 
            this.btn_new.Location = new System.Drawing.Point(12, 12);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(75, 23);
            this.btn_new.TabIndex = 0;
            this.btn_new.Text = "新增";
            this.btn_new.UseVisualStyleBackColor = true;
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // btn_print
            // 
            this.btn_print.Location = new System.Drawing.Point(336, 12);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(75, 23);
            this.btn_print.TabIndex = 1;
            this.btn_print.Text = "打印";
            this.btn_print.UseVisualStyleBackColor = true;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // btn_jy
            // 
            this.btn_jy.Location = new System.Drawing.Point(255, 12);
            this.btn_jy.Name = "btn_jy";
            this.btn_jy.Size = new System.Drawing.Size(75, 23);
            this.btn_jy.TabIndex = 2;
            this.btn_jy.Text = "停用";
            this.btn_jy.UseVisualStyleBackColor = true;
            this.btn_jy.Click += new System.EventHandler(this.btn_jy_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(498, 12);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 3;
            this.btn_close.Text = "关闭";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_toexcel
            // 
            this.btn_toexcel.Location = new System.Drawing.Point(417, 12);
            this.btn_toexcel.Name = "btn_toexcel";
            this.btn_toexcel.Size = new System.Drawing.Size(75, 23);
            this.btn_toexcel.TabIndex = 4;
            this.btn_toexcel.Text = "导出";
            this.btn_toexcel.UseVisualStyleBackColor = true;
            this.btn_toexcel.Click += new System.EventHandler(this.btn_toexcel_Click);
            // 
            // btn_qy
            // 
            this.btn_qy.Location = new System.Drawing.Point(174, 12);
            this.btn_qy.Name = "btn_qy";
            this.btn_qy.Size = new System.Drawing.Size(75, 23);
            this.btn_qy.TabIndex = 5;
            this.btn_qy.Text = "启用";
            this.btn_qy.UseVisualStyleBackColor = true;
            this.btn_qy.Click += new System.EventHandler(this.btn_qy_Click);
            // 
            // btn_amend
            // 
            this.btn_amend.Location = new System.Drawing.Point(93, 12);
            this.btn_amend.Name = "btn_amend";
            this.btn_amend.Size = new System.Drawing.Size(75, 23);
            this.btn_amend.TabIndex = 6;
            this.btn_amend.Text = "修改";
            this.btn_amend.UseVisualStyleBackColor = true;
            this.btn_amend.Click += new System.EventHandler(this.btn_amend_Click);
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
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // exportnum
            // 
            this.exportnum.DataPropertyName = "exportnum";
            this.exportnum.HeaderText = "出口编号";
            this.exportnum.Name = "exportnum";
            this.exportnum.ReadOnly = true;
            // 
            // exportdesc
            // 
            this.exportdesc.DataPropertyName = "exportdesc";
            this.exportdesc.HeaderText = "出口描述";
            this.exportdesc.Name = "exportdesc";
            this.exportdesc.ReadOnly = true;
            // 
            // machinenum
            // 
            this.machinenum.DataPropertyName = "machinenum";
            this.machinenum.HeaderText = "机器编号";
            this.machinenum.Name = "machinenum";
            this.machinenum.ReadOnly = true;
            // 
            // parentnum
            // 
            this.parentnum.DataPropertyName = "parentnum";
            this.parentnum.HeaderText = "上级编号";
            this.parentnum.Name = "parentnum";
            this.parentnum.ReadOnly = true;
            // 
            // exportseq
            // 
            this.exportseq.DataPropertyName = "exportseq";
            this.exportseq.HeaderText = "出口顺序";
            this.exportseq.Name = "exportseq";
            this.exportseq.ReadOnly = true;
            // 
            // state
            // 
            this.state.DataPropertyName = "state";
            this.state.HeaderText = "state";
            this.state.Name = "state";
            this.state.ReadOnly = true;
            this.state.Visible = false;
            // 
            // exportlevel
            // 
            this.exportlevel.DataPropertyName = "exportlevel";
            this.exportlevel.HeaderText = "exportlevel";
            this.exportlevel.Name = "exportlevel";
            this.exportlevel.ReadOnly = true;
            this.exportlevel.Visible = false;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "出口状态";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // exportstate
            // 
            this.exportstate.DataPropertyName = "exportstate";
            this.exportstate.HeaderText = "使用状态";
            this.exportstate.Name = "exportstate";
            this.exportstate.ReadOnly = true;
            // 
            // taskstate
            // 
            this.taskstate.DataPropertyName = "taskstate";
            this.taskstate.HeaderText = "任务状态";
            this.taskstate.Name = "taskstate";
            this.taskstate.ReadOnly = true;
            // 
            // taskarived
            // 
            this.taskarived.DataPropertyName = "taskarived";
            this.taskarived.HeaderText = "任务接受";
            this.taskarived.Name = "taskarived";
            this.taskarived.ReadOnly = true;
            // 
            // taskfinish
            // 
            this.taskfinish.DataPropertyName = "taskfinish";
            this.taskfinish.HeaderText = "任务完成";
            this.taskfinish.Name = "taskfinish";
            this.taskfinish.ReadOnly = true;
            // 
            // taskconfirm
            // 
            this.taskconfirm.DataPropertyName = "taskconfirm";
            this.taskconfirm.HeaderText = "任务确认";
            this.taskconfirm.Name = "taskconfirm";
            this.taskconfirm.ReadOnly = true;
            // 
            // taskstorage
            // 
            this.taskstorage.DataPropertyName = "taskstorage";
            this.taskstorage.HeaderText = "任务存储";
            this.taskstorage.Name = "taskstorage";
            this.taskstorage.ReadOnly = true;
            // 
            // win_exportline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 262);
            this.Controls.Add(this.exportlinedata);
            this.Controls.Add(this.panel1);
            this.Name = "win_exportline";
            this.Text = "出口信息";
            this.Load += new System.EventHandler(this.win_exportline_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.exportlinedata)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_amend;
        private System.Windows.Forms.Button btn_qy;
        private System.Windows.Forms.Button btn_toexcel;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_jy;
        private System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.DataGridView exportlinedata;
        private VBprinter.DGVprint dgVprint1;
        private System.Windows.Forms.DataGridViewTextBoxColumn rownum;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn exportnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn exportdesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn machinenum;
        private System.Windows.Forms.DataGridViewTextBoxColumn parentnum;
        private System.Windows.Forms.DataGridViewTextBoxColumn exportseq;
        private System.Windows.Forms.DataGridViewTextBoxColumn state;
        private System.Windows.Forms.DataGridViewTextBoxColumn exportlevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn exportstate;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskstate;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskarived;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskfinish;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskconfirm;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskstorage;
    }
}