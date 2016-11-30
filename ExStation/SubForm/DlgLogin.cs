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
    public partial class DlgLogin : Form
    {
        private MainForm main_form;
        private string member_code = string.Empty;
        private string user_name = string.Empty;
        private string user_password = string.Empty;

        public member member_info = null;
        public scuser user_info = null;

        public DlgLogin(MainForm main_form)
        {
            InitializeComponent();
            this.main_form = main_form;
        }

        private void DlgLogin_Load(object sender, EventArgs e)
        {
            this.txtMemberCode.Text = "S000001";
            this.txtUserName.Text = "BIT9";
            this.txtPassword.Text = "BIT9";
        }

        private void txtMemberCode_TextChanged(object sender, EventArgs e)
        {
            this.member_code = ((TextBox)sender).Text;
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            this.user_name = ((TextBox)sender).Text;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            this.user_password = ((TextBox)sender).Text;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            using (mEntities member_db = DBX.GetMemberDB(DBX.member_server, DBX.member_db_uid, DBX.member_db_pwd, DBX.member_db_name))
            {
                member m = member_db.member.Include("scusergroup").Include("scuser").Include("sccompgroup").Include("sccomp").Include("scacclv").Where(mb => mb.membercode.Trim() == this.member_code.Trim()).FirstOrDefault();
                if (m != null)
                {
                    this.member_info = m;

                    scuser s = member_db.scuser.Include("scacclv").Include("sccomp").Where(u => u.username.Trim() == this.user_name.Trim() && u.passwordhash.Trim() == this.user_password.Trim()).FirstOrDefault();

                    if (s != null)
                    {
                        this.user_info = s;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("User/Password is invalid.");
                    }
                }
                else
                {
                    MessageBox.Show("Member code not found.");
                }

            }

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.Enter && !(this.btnOK.Focused || this.btnCancel.Focused))
            {
                SendKeys.Send("{TAB}");
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
