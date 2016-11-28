using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExStation.SubForm
{
    public partial class DlgFindSimple : Form
    {
        private string keyword;
        public string Keyword
        {
            get
            {
                return this.keyword;
            }
        }

        public DlgFindSimple(string keyword = "")
        {
            InitializeComponent();
            this.keyword = keyword;
        }

        private void DlgFindSimple_Load(object sender, EventArgs e)
        {
            this.txtKeyword.Focus();
            this.txtKeyword.Text = this.keyword;
            this.txtKeyword.SelectionStart = this.keyword.Length;
        }

        private void txtKeyword_TextChanged(object sender, EventArgs e)
        {
            this.keyword = ((TextBox)sender).Text.Trim();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter && this.txtKeyword.Focused)
            {
                this.btnOK.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
