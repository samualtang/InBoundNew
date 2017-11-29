using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;

namespace highSpeed.pubFunc
{

    ///
    /// 实现DataGridView的打印
    ///
    public class PrintDataGridView
    {
        private static List<DataGridViewCellPrint> CellPrintList = new List<DataGridViewCellPrint>();//DataGridViewCellPrint

        private static int printRowCount = 0;

        private static bool IsPrint = true;
        private static bool IsRole = true;
        private static int PoXTmp = 0;
        private static int PoYTmp = 0;
        private static int WidthTmp = 0;
        private static int HeightTmp = 0;
        private static int RowIndex = 0;


        ///
        /// 打印DataGridView控件
        ///
        /// DataGridView控件
        /// 是否包括列标题
        /// 为 System.Drawing.Printing.PrintDocument.PrintPage 事件提供数据。
        /// 起始X坐标
        /// 起始Y坐标
        public static void Print(DataGridView dataGridView, bool includeColumnText, PrintPageEventArgs e, ref int PoX, ref int PoY)
        {
            try
            {
                if (PrintDataGridView.IsPrint)
                {
                    PrintDataGridView.printRowCount = 0;
                    PrintDataGridView.IsPrint = false;
                    PrintDataGridView.DataGridViewCellVsList(dataGridView, includeColumnText);
                    if (0 == PrintDataGridView.CellPrintList.Count)
                        return;
                    if (PoX > e.MarginBounds.Left)
                        PrintDataGridView.IsRole = true;
                    else
                        PrintDataGridView.IsRole = false;
                    PrintDataGridView.PoXTmp = PoX;
                    PrintDataGridView.PoYTmp = PoY;
                    PrintDataGridView.RowIndex = 0;
                    WidthTmp = 0;
                    HeightTmp = 0;
                }
                if (0 != PrintDataGridView.printRowCount)
                {
                    if (IsRole)
                    {
                        PoX = PoXTmp = e.MarginBounds.Left;
                        PoY = PoYTmp = e.MarginBounds.Top;
                    }
                    else
                    {
                        PoX = PoXTmp;
                        PoY = PoYTmp;
                    }
                }
                while (PrintDataGridView.printRowCount < PrintDataGridView.CellPrintList.Count)
                {
                    DataGridViewCellPrint CellPrint = CellPrintList[PrintDataGridView.printRowCount];
                    if (RowIndex == CellPrint.RowIndex)
                        PoX = PoX + WidthTmp;
                    else
                    {
                        PoX = PoXTmp;
                        PoY = PoY + HeightTmp;
                        if (PoY + HeightTmp > e.MarginBounds.Bottom)
                        {
                            HeightTmp = 0;
                            e.HasMorePages = true;
                            return;
                        }
                    }
                    using (SolidBrush solidBrush = new SolidBrush(CellPrint.BackColor))
                    {
                        RectangleF rectF1 = new RectangleF(PoX, PoY, CellPrint.Width, CellPrint.Height);
                        e.Graphics.FillRectangle(solidBrush, rectF1);
                        using (Pen pen = new Pen(Color.Black, 1))
                            e.Graphics.DrawRectangle(pen, Rectangle.Round(rectF1));
                        solidBrush.Color = CellPrint.ForeColor;
                        e.Graphics.DrawString(CellPrint.FormattedValue, CellPrint.Font, solidBrush, new Point(PoX + 2, PoY + 3));
                    }
                    WidthTmp = CellPrint.Width;
                    HeightTmp = CellPrint.Height;
                    RowIndex = CellPrint.RowIndex;
                    PrintDataGridView.printRowCount++;
                }
                PoY = PoY + HeightTmp;
                e.HasMorePages = false;
                PrintDataGridView.IsPrint = true;
            }
            catch
            {
                e.HasMorePages = false;
                PrintDataGridView.IsPrint = true;
                throw;
            }

        }


        ///
        /// 将DataGridView控件内容转变到 CellPrintList
        ///
        /// DataGridView控件
        /// 是否包括列标题
        private static void DataGridViewCellVsList(DataGridView dataGridView, bool includeColumnText)
        {
            CellPrintList.Clear();
            try
            {
                int rowsCount = dataGridView.Rows.Count;
                int colsCount = dataGridView.Columns.Count;

                //最后一行是供输入的行时，不用读数据。
                if (dataGridView.Rows[rowsCount - 1].IsNewRow)
                    rowsCount--;
                //包括列标题
                if (includeColumnText)
                {
                    for (int columnsIndex = 0; columnsIndex < colsCount; columnsIndex++)
                    {
                        if (dataGridView.Columns[columnsIndex].Visible)
                        {
                            DataGridViewCellPrint CellPrint = new DataGridViewCellPrint();
                            CellPrint.FormattedValue = dataGridView.Columns[columnsIndex].HeaderText;
                            CellPrint.RowIndex = 0;
                            CellPrint.ColumnIndex = columnsIndex;
                            CellPrint.Font = dataGridView.Columns[columnsIndex].HeaderCell.Style.Font;
                            CellPrint.BackColor = dataGridView.ColumnHeadersDefaultCellStyle.BackColor;
                            CellPrint.ForeColor = dataGridView.ColumnHeadersDefaultCellStyle.ForeColor;
                            CellPrint.Width = dataGridView.Columns[columnsIndex].Width;
                            CellPrint.Height = dataGridView.ColumnHeadersHeight;
                            CellPrintList.Add(CellPrint);
                        }
                    }
                }
                //读取单元格数据
                for (int rowIndex = 0; rowIndex < rowsCount; rowIndex++)
                {
                    for (int columnsIndex = 0; columnsIndex < colsCount; columnsIndex++)
                    {
                        if (dataGridView.Columns[columnsIndex].Visible)
                        {
                            DataGridViewCellPrint CellPrint = new DataGridViewCellPrint();
                            CellPrint.FormattedValue = dataGridView.Rows[rowIndex].Cells[columnsIndex].FormattedValue.ToString();
                            if (includeColumnText)
                                CellPrint.RowIndex = rowIndex + 1;//假如包括列标题则从行号1开始
                            else
                                CellPrint.RowIndex = rowIndex;
                            CellPrint.ColumnIndex = columnsIndex;
                            CellPrint.Font = dataGridView.Rows[rowIndex].Cells[columnsIndex].Style.Font;
                            System.Drawing.Color TmpColor = System.Drawing.Color.Empty;
                            if (System.Drawing.Color.Empty != dataGridView.Rows[rowIndex].Cells[columnsIndex].Style.BackColor)
                                TmpColor = dataGridView.Rows[rowIndex].Cells[columnsIndex].Style.BackColor;
                            else if (System.Drawing.Color.Empty != dataGridView.Rows[rowIndex].DefaultCellStyle.BackColor)
                                TmpColor = dataGridView.Rows[rowIndex].DefaultCellStyle.BackColor;
                            else
                                TmpColor = dataGridView.DefaultCellStyle.BackColor;
                            CellPrint.BackColor = TmpColor;
                            TmpColor = System.Drawing.Color.Empty;
                            if (System.Drawing.Color.Empty != dataGridView.Rows[rowIndex].Cells[columnsIndex].Style.ForeColor)
                                TmpColor = dataGridView.Rows[rowIndex].Cells[columnsIndex].Style.ForeColor;
                            else if (System.Drawing.Color.Empty != dataGridView.Rows[rowIndex].DefaultCellStyle.ForeColor)
                                TmpColor = dataGridView.Rows[rowIndex].DefaultCellStyle.ForeColor;
                            else
                                TmpColor = dataGridView.DefaultCellStyle.ForeColor;
                            CellPrint.ForeColor = TmpColor;
                            CellPrint.Width = dataGridView.Columns[columnsIndex].Width;
                            CellPrint.Height = dataGridView.Rows[rowIndex].Height;
                            CellPrintList.Add(CellPrint);
                        }
                    }
                }
            }
            catch { throw; }
        }

        private class DataGridViewCellPrint
        {
            private string _FormattedValue = "";
            private int _RowIndex = -1;
            private int _ColumnIndex = -1;
            private System.Drawing.Color _ForeColor = System.Drawing.Color.Black;
            private System.Drawing.Color _BackColor = System.Drawing.Color.White;
            private int _Width = 100;
            private int _Height = 23;
            private System.Drawing.Font _Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            ///
            /// 获取或设置单元格的字体。
            ///
            public System.Drawing.Font Font
            {
                set { if (null != value) _Font = value; }
                get { return _Font; }
            }

            ///
            /// 获取为显示进行格式化的单元格的值。
            ///
            public string FormattedValue
            {
                set { _FormattedValue = value; }
                get { return _FormattedValue; }
            }

            ///
            /// 获取或设置列的当前宽度 （以像素为单位）。默认值为 100。
            ///
            public int Width
            {
                set { _Width = value; }
                get { return _Width; }
            }

            ///
            /// 获取或设置列标题行的高度（以像素为单位）。默认值为 23。
            ///
            public int Height
            {
                set { _Height = value; }
                get { return _Height; }
            }

            ///
            /// 获取或设置行号。
            ///
            public int RowIndex
            {
                set { _RowIndex = value; }
                get { return _RowIndex; }
            }

            ///
            /// 获取或设置列号。
            ///
            public int ColumnIndex
            {
                set { _ColumnIndex = value; }
                get { return _ColumnIndex; }
            }

            ///
            /// 获取或设置前景色。
            ///
            public System.Drawing.Color ForeColor
            {
                set { _ForeColor = value; }
                get { return _ForeColor; }
            }

            ///
            /// 获取或设置背景色。
            ///
            public System.Drawing.Color BackColor
            {
                set { _BackColor = value; }
                get { return _BackColor; }
            }

        }

    }
}