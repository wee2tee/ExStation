using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CC
{
    public partial class XDatagrid : DataGridView
    {
        private bool row_border_redline;
        public bool FocusedRowBorderRedLine {
            get {
                return this.row_border_redline;
            }
            set
            {
                this.row_border_redline = value;
            }
        }
        private bool allow_sort_by_column_header_clicked;
        public bool AllowSortByColumnHeaderClicked
        {
            get
            {
                return this.allow_sort_by_column_header_clicked;
            }
            set
            {
                this.allow_sort_by_column_header_clicked = value;
            }
        }
        private int sort_column = -1;
        public int SortColumn
        {
            get
            {
                return this.sort_column;
            }
        }
        private bool sort_asc = true;
        public bool SortASC
        {
            get
            {
                return this.sort_asc;
            }
        }

        public XDatagrid()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AllowUserToOrderColumns = false;
            this.AllowUserToResizeColumns = false;
            this.AllowUserToResizeRows = false;

            this.ColumnHeadersDefaultCellStyle.BackColor = Color.PeachPuff;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ColumnHeadersHeight = 28;

            if (this.row_border_redline)
            {
                this.DefaultCellStyle.BackColor = Color.White;
                this.DefaultCellStyle.ForeColor = Color.Black;
                this.DefaultCellStyle.SelectionBackColor = Color.White;
                this.DefaultCellStyle.SelectionForeColor = Color.Black;
            }
            using (Font tahoma = new Font("tahoma", 9.75f))
            {
                this.DefaultCellStyle.Font = tahoma;
            }

            this.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.EnableHeadersVisualStyles = false;

            this.MultiSelect = false;
            this.ReadOnly = true;
            this.RowHeadersVisible = false;
            this.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.StandardTab = true;

            this.AttachEventHandler();
        }

        private void AttachEventHandler()
        {
            if (this.row_border_redline)
            {
                this.Paint += new PaintEventHandler(this.DrawRowBorder);
            }
            if (this.allow_sort_by_column_header_clicked)
            {
                this.CellPainting += new DataGridViewCellPaintingEventHandler(this.DrawColumnHeaderSortSign);
            }
        }

        private void DrawColumnHeaderSortSign(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex == this.SortColumn)
            {
                e.Graphics.DrawPolygon(new Pen(Color.Red), new Point[] { new Point(e.CellBounds.X + 5, e.CellBounds.Y + 5), new Point(e.CellBounds.X + 11, e.CellBounds.Y + 5), new Point(e.CellBounds.X + 8, e.CellBounds.Y + 11) });
                e.Handled = true;
            }
        }

        private void DrawRowBorder(object sender, PaintEventArgs e)
        {
            Rectangle rect = this.GetRowDisplayRectangle(this.CurrentCell.RowIndex, true);

            using (Pen p = new Pen(Color.Red))
            {
                e.Graphics.DrawLine(p, new Point(rect.X, rect.Y), new Point(rect.X + rect.Width, rect.Y));
                e.Graphics.DrawLine(p, new Point(rect.X, rect.Y + rect.Height - 2), new Point(rect.X + rect.Width, rect.Y + rect.Height - 2));
            }
        }

        public void SetSortColumnInfo(int sort_column, bool sort_asc)
        {
            this.sort_column = sort_column;
            this.sort_asc = sort_asc;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
