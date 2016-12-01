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
using ExStation.Misc;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ExStation
{
    public partial class MainForm : Form
    {
        public member member_info;
        public scuser user_info;
        public sccomp comp_info;
        public List<TextMessage> msg;

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
                    DlgCompanySelect comp = new DlgCompanySelect(login.member_info, login.user_info);
                    if(comp.ShowDialog() == DialogResult.OK)
                    {
                        this.member_info = login.member_info;
                        this.user_info = login.user_info;
                        this.comp_info = comp.selected_comp;
                        
                        this.lblDbName.Text = this.comp_info.dbname + " [ " + this.comp_info.compname + " ]";

                        this.msg = MessageManager.LoadMessage(this.user_info.language);
                        this.SetMenuText(this.mainMenu);
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void SetMenuText(Component menu)
        {
            if(menu is MenuStrip)
            {
                foreach (ToolStripMenuItem item in ((MenuStrip)menu).Items)
                {
                    item.Text = (this.msg.Get(item.Name) != null ? this.msg.Get(item.Name) : item.Text);

                    if(item.DropDownItems.Count > 0)
                    {
                        this.SetMenuText(item);
                    }
                }
            }
            
            if(menu is ToolStripMenuItem)
            {
                foreach (ToolStripMenuItem item in ((ToolStripMenuItem)menu).DropDownItems)
                {
                    item.Text = (this.msg.Get(item.Name) != null ? this.msg.Get(item.Name) : item.Text);

                    if (item.DropDownItems.Count > 0)
                    {
                        this.SetMenuText(item);
                    }
                }
            }
        }
    }
}
