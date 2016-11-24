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

        public DlgLogin(MainForm main_form)
        {
            InitializeComponent();
            this.main_form = main_form;
        }

        private void DlgLogin_Load(object sender, EventArgs e)
        {

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
                member m = member_db.member.Where(mb => mb.membercode.Trim() == this.member_code.Trim()).FirstOrDefault();
                if (m != null)
                {
                    this.member_info = m;

                    scuser s = m.scuser.Where(u => u.username.Trim() == this.user_name.Trim() && u.passwordhash.Trim() == this.user_password.Trim()).FirstOrDefault();
                    if(s != null)
                    {
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
    }
}
