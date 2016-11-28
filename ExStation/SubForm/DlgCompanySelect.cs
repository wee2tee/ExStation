using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExStation.Models;
using ExStation.Models.ViewModels;
using ExStation.Misc;

namespace ExStation.SubForm
{
    public partial class DlgCompanySelect : Form
    {
        private BindingSource bs;
        private List<SccompVM> company_list = new List<SccompVM>();
        private member member;
        private scuser user;
        private bool sort_asc;
        private int sort_column_index;

        public sccomp selected_comp;

        public DlgCompanySelect(member member, scuser user)
        {
            InitializeComponent();
            this.member = member;
            this.user = user;
        }

        private void DlgCompanySelect_Load(object sender, EventArgs e)
        {
            this.bs = new BindingSource();
            this.bs.DataSource = this.company_list;
            this.dgv.DataSource = this.bs;

            this.company_list = this.member.sccomp.OrderBy(s => s.compname).ToList().ToViewModel();
            this.bs.ResetBindings(true);
            this.bs.DataSource = this.company_list;

            this.sort_asc = true;
            this.sort_column_index = this.dgv.Columns["col_compname"].Index;
        }

        private void dgv_CurrentCellChanged(object sender, EventArgs e)
        {
            if (((DataGridView)sender).CurrentCell == null)
                return;

            this.selected_comp = (sccomp)((DataGridView)sender).Rows[((DataGridView)sender).CurrentCell.RowIndex].Cells["col_sccomp"].Value;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            Rectangle dlg_rect = this.Bounds;
            
            DlgFindSimple find = new DlgFindSimple();
            find.SetBounds(dlg_rect.X + 8, dlg_rect.Y + (dlg_rect.Height - find.Height) - 8, find.Width, find.Height);
            if(find.ShowDialog() == DialogResult.OK)
            {
                DataGridViewRow target_row = this.dgv.Rows.Cast<DataGridViewRow>()
                   .Where(r => ((sccomp)r.Cells["col_sccomp"].Value).compname.Length >= find.Keyword.Length)
                   .Where(r => ((sccomp)r.Cells["col_sccomp"].Value).compname.Substring(0, find.Keyword.Length) == find.Keyword).FirstOrDefault();

                if (target_row == null)
                {
                    MessageBox.Show("ค้นหาชื่อข้อมูล " + find.Keyword + " ไม่พบ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.dgv.CurrentCell = target_row.Cells["col_compname"];
            }
        }

        private void dgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                DataGridViewColumn col = ((DataGridView)sender).Columns[e.ColumnIndex];
                this.dgv.SetSortColumnInfo(col.Index, true);

                if(col.Index == this.sort_column_index)
                {
                    this.sort_asc = !this.sort_asc;
                }
                else
                {
                    this.sort_asc = true;
                    this.sort_column_index = col.Index;
                }

                string field_name = ((DataGridView)sender).Columns[e.ColumnIndex].DataPropertyName;

                if (this.sort_asc)
                {
                    this.company_list = this.company_list.OrderBy(s => s.GetType().GetProperty(field_name).GetValue(s, null)).ToList();
                }
                else
                {
                    this.company_list = this.company_list.OrderByDescending(s => s.GetType().GetProperty(field_name).GetValue(s, null)).ToList();
                }

                this.bs.ResetBindings(true);
                this.bs.DataSource = this.company_list;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter)
            {
                this.btnOK.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void DlgCompanySelect_KeyPress(object sender, KeyPressEventArgs e)
        {
            Rectangle dlg_rect = this.Bounds;

            DlgFindSimple find = new DlgFindSimple(e.KeyChar.ToString());
            find.SetBounds(dlg_rect.X + 8, dlg_rect.Y + (dlg_rect.Height - find.Height) - 8, find.Width, find.Height);

            if (find.ShowDialog() == DialogResult.OK)
            {
                DataGridViewRow target_row = this.dgv.Rows.Cast<DataGridViewRow>()
                    .Where(r => ((sccomp)r.Cells["col_sccomp"].Value).compname.Length >= find.Keyword.Length)
                    .Where(r => ((sccomp)r.Cells["col_sccomp"].Value).compname.Substring(0, find.Keyword.Length) == find.Keyword).FirstOrDefault();

                if(target_row == null)
                {
                    MessageBox.Show("ค้นหาชื่อข้อมูล " + find.Keyword + " ไม่พบ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.dgv.CurrentCell = target_row.Cells["col_compname"];
            }
        }
    }
}
