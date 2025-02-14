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
    public partial class Admin_register : Form
    {
        FitnessdataTableAdapters.AdminTBTableAdapter Ad_obj = new FitnessdataTableAdapters.AdminTBTableAdapter();
        DataTable dtAd = new DataTable();

        public Admin_register()
        {
            InitializeComponent();
            PersonID();

            dtAd = Ad_obj.GetData();
            Ad_dgv.DataSource = dtAd;
            Ad_dgv.Refresh();
        }
        public void PersonID()
        {
            DataTable IDdt = new DataTable();
            IDdt = Ad_obj.GetData();// get the data from the table dtMember

            if (IDdt.Rows.Count == 0)// if no IDs yet
            {
                Ad_ID.Text = "A-001";
            }
            else
            {
                int size = IDdt.Rows.Count - 1;
                string lastID = IDdt.Rows[size][0].ToString();
                int currentnumID = Convert.ToInt32(lastID.Substring(2, 3));//convert numbers to integer

                if (currentnumID >= 1 && currentnumID < 9)
                {
                    Ad_ID.Text = "A-00" + (currentnumID + 1);
                }
                else if (currentnumID >= 9 && currentnumID < 99)
                {
                    Ad_ID.Text = "A-0" + (currentnumID + 1);
                }
                else if (currentnumID >= 99 && currentnumID < 999)
                {
                    Ad_ID.Text = "A-" + (currentnumID + 1);
                }
            }
        }

        private void ad_backlnk_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Admin_login Al = new Admin_login();
            Al.Show();// allow the form (admin login) to appear
            this.Hide();// to make the current form(admin register) disappear
        }
        public void clear_all()
        {
            Ad_age.Text = "";
            Ad_dob.Text = "";
            Ad_name.Text = "";
            Ad_num.Text = "";
            Ad_password.Text = "";
            Ad_confpass.Text = "";
            Ad_email.Text = "";
            Ad_female.Checked = false;
            Ad_male.Checked = false;
        }
        private void ad_clearbtn_Click(object sender, EventArgs e)
        {
            clear_all();
        }

        private void Ad_dgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = this.Ad_dgv.Rows[e.RowIndex];
            Ad_ID.Text = row.Cells[0].Value.ToString();
            Ad_name.Text = row.Cells[1].Value.ToString();
            Ad_password.Text = row.Cells[2].Value.ToString();
            Ad_dob.Text = row.Cells[4].Value.ToString();
            Ad_age.Text = row.Cells[5].Value.ToString();
            Ad_num.Text = row.Cells[6].Value.ToString();
            Ad_email.Text = row.Cells[7].Value.ToString();
            Ad_confpass.Text = Ad_password.Text;
            Updatebtn.Enabled = true;
        }
        private void Updatebtn_Click(object sender, EventArgs e)
        {
            string email = Ad_email.Text;
            ad_clearbtn.Enabled = false;
            string password = Ad_password.Text;
            string gender = null;
            string name = Ad_name.Text;
            string confpassword = Ad_confpass.Text;
            string phoneno = Ad_num.Text;

            if (Ad_female.Checked == true)//if female chosen in the radio button
            {
                gender = "Female";
            }
            if (Ad_male.Checked == true)
            {
                gender = "Male";
            }
            if (Ad_female.Checked == false && Ad_male.Checked == false)
            {
                MessageBox.Show("Please select your gender.");
            }
            else if (Ad_name.Text == "")
            {
                MessageBox.Show("Please enter your registered member name");
                Ad_name.Focus();
            }
            else if (Ad_email.Text == "")
            {
                MessageBox.Show("Please enter your email.");
                Ad_email.Focus();
            }
            else if (!((name.All(char.IsLetter) || name.All(char.IsDigit) || name.All(char.IsLetterOrDigit))))// three different possiblities in having only letters and numbers
            {//so if not either of them:
                MessageBox.Show("Username can only be letters and digits");
                Ad_name.Focus();
            }
            else if (Ad_num.Text == "")
            {
                MessageBox.Show("Please enter your registered phone number");
                Ad_num.Focus();
            }
            else if (!phoneno.All(char.IsDigit))// if not all the input data in phone number is digit
            {
                MessageBox.Show("Invalid Phone Number Entry..");// warning message
                Ad_num.Focus();
            }
            else if (Ad_password.Text == "")
            {
                MessageBox.Show("Please enter your password");
                Ad_password.Focus();
            }
            else if (Ad_age.Text == "")
            {
                MessageBox.Show("Please enter your age");
                Ad_age.Focus();
            }
            else if (Ad_dob.Text == "")
            {
                MessageBox.Show("Please enter your date of birth");
                Ad_dob.Focus();
            }
            else if (!password.Any(char.IsDigit))
            {
                MessageBox.Show("Password must have at least one digit.");
                Ad_password.Focus();
            }
            else if (password.Length != 12)
            {
                MessageBox.Show("Password must have TWELVE characters.");
                Ad_password.Focus();
            }
            else if (!password.Any(char.IsUpper))
            {
                MessageBox.Show("Password must contain at least ONE uppercase. Please Re-enter.");
                Ad_password.Focus();
            }
            else if (!password.Any(char.IsLower))
            {
                MessageBox.Show("Password must contain at least ONE lowercase letter. Please Re-enter.");
                Ad_password.Focus();
            }
            else if (Ad_confpass.Text == "")
            {
                MessageBox.Show("Please enter to verify password.");
                Ad_confpass.Focus();
            }
            else if (Ad_confpass.Text != Ad_password.Text)
            {
                MessageBox.Show("The password and confirmation password are not equal. Please re-enter.");
                Ad_confpass.Text = "";
                Ad_confpass.Focus();
            }
            else
            {
                Ad_obj.AdminUpdate(Ad_name.Text, password, gender, Ad_dob.Text, Ad_age.Text, Ad_num.Text, Ad_email.Text, Ad_ID.Text);
                MessageBox.Show("Updated Successfully!!");

                dtAd = Ad_obj.GetData();
                Ad_dgv.DataSource = dtAd;
                Ad_dgv.Refresh();
                PersonID();
                clear_all();
                Updatebtn.Enabled = false;
                ad_registerbtn.Enabled = true;
                ad_clearbtn.Enabled = true;
            }
        }

        private void ad_closebtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ad_registerbtn_Click(object sender, EventArgs e)
        {
            string password = Ad_password.Text;
            string gender = null;
            DateTime dob = Ad_dob.Value;
            string confpassword = Ad_confpass.Text;
            string name = Ad_name.Text;
            string phoneno = Ad_num.Text;
            if (Ad_female.Checked == true)
            {
                gender = "Female";
            }
            if (Ad_male.Checked == true)
            {
                gender = "Male";
            }
            if (Ad_female.Checked == false && Ad_male.Checked == false)
            {
                MessageBox.Show("Please select your gender.");
            }
            else if (Ad_name.Text == "")
            {
                MessageBox.Show("Please enter your registered member name");
                Ad_name.Focus();
            }
            else if (!((name.All(char.IsLetter) || name.All(char.IsDigit) || name.All(char.IsLetterOrDigit))))// three possible combination when it comes to containing only letters and numbers
            {
                MessageBox.Show("Username can only be letters and digits");
                Ad_name.Focus();
            }
            else if (Ad_num.Text == "")
            {
                MessageBox.Show("Please enter your registered phone number");
                Ad_num.Focus();
            }
            else if (!phoneno.All(char.IsDigit))// if not all the input data in phone number is digit
            {
                MessageBox.Show("Invalid Phone Number Entry..");// warning message
                Ad_num.Focus();
            }
            else if (Ad_password.Text == "")
            {
                MessageBox.Show("Please enter your password");
                Ad_password.Focus();
            }
            else if (Ad_age.Text == "")
            {
                MessageBox.Show("Please enter your age");
                Ad_age.Focus();
            }
            else if (Ad_dob.Text == "")
            {
                MessageBox.Show("Please enter your date of birth");
                Ad_dob.Focus();
            }
            else if (!password.Any(char.IsDigit))
            {
                MessageBox.Show("Password must have at least one digit.");
                Ad_password.Focus();
            }
            else if (password.Length != 12)
            {
                MessageBox.Show("Password must have TWELVE characters.");
                Ad_password.Focus();
            }
            else if (!password.Any(char.IsUpper))
            {
                MessageBox.Show("Password must contain at least ONE uppercase. Please Re-enter.");
                Ad_password.Focus();
            }
            else if (!password.Any(char.IsLower))
            {
                MessageBox.Show("Password must contain at least ONE lowercase letter. Please Re-enter.");
                Ad_password.Focus();
            }
            else if (Ad_confpass.Text == "")
            {
                MessageBox.Show("Please enter to verify password.");
                Ad_confpass.Focus();
            }
            else if (Ad_confpass.Text != Ad_password.Text)
            {
                MessageBox.Show("The password and confirmation password are not equal. Please re-enter.");
                Ad_confpass.Text = "";
                Ad_confpass.Focus();
            }

            else
            {
                Admin_class Ad = new Admin_class();
                Ad.adID = Ad_ID.Text;
                Ad.adname = Ad_name.Text;
                Ad.adpassword = password;
                Ad.adgen = gender;
                Ad.adage = Ad_age.Text;
                Ad.adnum = Ad_num.Text;
                Ad.ademail = Ad_email.Text;

                Ad_obj.Insert(Ad.adID, Ad.adname, Ad.adpassword, Ad.adgen, dob, Ad.adage, Ad.adnum, Ad.ademail);
                dtAd = Ad_obj.GetData();

                MessageBox.Show("Automatically saved.");

                dtAd = Ad_obj.GetData();
                Ad_dgv.DataSource = dtAd;
                Ad_dgv.Refresh();
                PersonID();
                clear_all();
                
            }
        }

    }
}

