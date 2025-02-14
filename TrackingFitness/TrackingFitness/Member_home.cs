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
    public partial class Member_home : Form
    {
        public Member_home()
        {
            InitializeComponent();

        }

        private void home_Load(object sender, EventArgs e)
        {
            namelbl.Text = namelbl.Text + Member_login.MemName + "!";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void setGoalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Member_goal Mg = new Member_goal();
            Mg.Show();
        }

        private void progressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Member_monitor Mm = new Member_monitor();
            Mm.Show();
        }

        private void addActivityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Member_activity Ma = new Member_activity();
            Ma.Show();
        }

        private void aboutFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProgramInfo PI = new ProgramInfo();
            PI.Show();
            // program for a guide for members
        }
    }
}
