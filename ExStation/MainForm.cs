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
using System.Xml.Serialization;
using System.IO;

namespace ExStation
{
    public partial class MainForm : Form
    {
        public member member_info;
        public scuser user_info;
        public sccomp comp_info;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //XmlSerializer xml = new XmlSerializer(typeof(List<XMenu>));
            //TextReader text_reader = new StreamReader(@"mnu.xml");
            //List<XMenu> menu = (List<XMenu>)xml.Deserialize(text_reader);
            //text_reader.Close();
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
    }
}
