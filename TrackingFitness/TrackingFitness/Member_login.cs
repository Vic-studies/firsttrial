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
    public partial class Member_login : Form
    {
        FitnessdataTableAdapters.MemberTBTableAdapter MT = new FitnessdataTableAdapters.MemberTBTableAdapter();

        private int Logincount = 0;//to count the times logged in
        public static string MemID, MemName;

        public Member_login()
        {
            InitializeComponent();
        }

        private void registerlnk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();//the object of the class should disappear
            Member_register register = new Member_register();
            register.Show();// to make the object, 'register', appear
        }

       

        private void loginbto_Click(object sender, EventArgs e)
        {
            string password = Mem_logpassword.Text;
            if (Logincount == 3)
            {
                MessageBox.Show("Login fail three times! You have reached your limit.");
            }
            else if (Mem_logname.Text == "")
            {
                MessageBox.Show("Please enter your name");
            }
            else if (Mem_logpassword.Text == "")
            {
                MessageBox.Show("Please enter your password");
            }
            else if (!password.Any(char.IsUpper))//none is upper case in password
            {
                Logincount += 1;
                MessageBox.Show("Invalid Login! Failed attempt:" + Logincount);
            }
            else if (!password.Any(char.IsLower))// none is lower case in password
            {
                Logincount += 1;
                MessageBox.Show("Invalid Login! Failed attempt:" + Logincount);
            }
            else
            {
                DataTable dtable = new DataTable();

                dtable = MT.LoginData(Mem_logname.Text, Mem_logpassword.Text);
                if (dtable.Rows.Count == 1)
                {
                    MessageBox.Show("Login Successful");
                    dgvlogin.DataSource = dtable;
                    MemName = dgvlogin[1, 0].Value.ToString();
                    MemID = dgvlogin[0, 0].Value.ToString();

                    Member_home H = new Member_home();
                    H.Show();
                    this.Hide();
                }
                else
                {
                    Logincount += 1;
                    MessageBox.Show("Invalid Login! Failed attempt:" + Logincount);
                }
            }
        }

        private void backlnk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            starting_page Sp = new starting_page();
            Sp.Show();
            this.Hide();
        }

        private void closebto_Click(object sender, EventArgs e)
        {
            Close();
        }

 

       
    }
}
