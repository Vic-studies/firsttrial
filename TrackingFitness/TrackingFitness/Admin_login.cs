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
    public partial class 
    Admin_login : Form
    {
        FitnessdataTableAdapters.AdminTBTableAdapter AT = new FitnessdataTableAdapters.AdminTBTableAdapter();

        private int Logincount = 0;
        public static string AdID, AdName;
        public Admin_login()
        {
            InitializeComponent();
            
        }
        private void ad_loginbtn_Click(object sender, EventArgs e)
        {
            string password = Ad_logpassword.Text;
            if (Logincount == 3)// if failed login is 3 times already
            {
                MessageBox.Show("Login fail three times! You have reached the limit.");
            }
            else if (Ad_logname.Text == "")
            {
                MessageBox.Show("Please enter your name");
            }
            else if (Ad_logpassword.Text == "")
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

                dtable = AT.LoginData(Ad_logname.Text,Ad_logpassword.Text);
                if (dtable.Rows.Count == 1)
                {
                    MessageBox.Show("Login Successful");
                    Ad_dgvlogin.DataSource = dtable;
                    AdName = Ad_dgvlogin[1, 0].Value.ToString();//to carry on the value as login page is inevitably one of the start ups
                    AdID = Ad_dgvlogin[0, 0].Value.ToString();//(so ID, and username can be known from this form)

                    Admin_home H = new Admin_home();
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

        private void ad_closebtn_Click(object sender, EventArgs e)
        {
            Close(); 
        }
        
    }
}
