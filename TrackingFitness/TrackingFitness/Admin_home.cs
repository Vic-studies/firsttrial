using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrackingFitness
{
    public partial class Admin_home : Form
    {
        public Admin_home()
        {
            InitializeComponent();
        }

        private void Ad_home_Load(object sender, EventArgs e)
        {
            namelbl.Text = namelbl.Text + " "+ Admin_login.AdName + "!";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Admin_register Ar = new Admin_register();
            Ar.Show();
        }

        private void manageMemberProfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Member_register Mr = new Member_register();
            Mr.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
