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
    public partial class starting_page : Form
    {
        public static string usertype;
        public starting_page()
        {
            InitializeComponent();
        }

        private void usercbo_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (usercbo.SelectedIndex == 1)
            {
                Member_login Ml = new Member_login();
                Ml.Show();// to make the login form appear
                this.Hide();//to make the starting page disappear
                usertype="Mem";
            }
            else if (usercbo.SelectedIndex==0)
            {
                Admin_login Al = new Admin_login();
                Al.Show();
                this.Hide();
                usertype = "Ad";
            }
        }
    }
}
