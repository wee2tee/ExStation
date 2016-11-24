using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExStation.Models;
using ExStation.SubForm;

namespace ExStation
{
    public partial class MainForm : Form
    {
        public member member_info;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (this.member_info == null)
            {
                DlgLogin login = new DlgLogin(this);
                if (login.ShowDialog() == DialogResult.OK)
                {
                    this.member_info = login.member_info;
                    this.toolStripStatusLabel1.Text = this.member_info.prename + " " + this.member_info.name;
                }
                else
                {
                    this.Close();
                }
            }
        }
    }
}
