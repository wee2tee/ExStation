using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExStation.Models;

namespace ExStation.SubForm
{
    public partial class DlgCompanySelect : Form
    {
        private BindingSource bs;
        private List<sccomp> company_list = new List<sccomp>();
        private member member;
        private scuser user;

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

            //using (mEntities member_db = DBX.GetMemberDB(DBX.member_server, DBX.member_db_uid, DBX.member_db_pwd, DBX.member_db_name))
            //{
            //    this.company_list = member_db.member.Find(this.member.id).sccomp.ToList();
            //    this.bs.ResetBindings(true);
            //    this.bs.DataSource = this.company_list;
            //}
            this.company_list = this.member.sccomp.ToList();
            this.bs.ResetBindings(true);
            this.bs.DataSource = this.company_list;

        }
    }
}
