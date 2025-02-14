using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TrackingFitness
{
    public partial class Member_register : Form
    {
        public static int w;//to carry weight value for Member_monitor
        public Member_register()
        {
            InitializeComponent();
            PersonID();

            dtMember = Member_obj.GetData();
            Mem_dgv.DataSource = dtMember;
            Mem_dgv.Refresh();//for update dgv
        }

        FitnessdataTableAdapters.MemberTBTableAdapter Member_obj = new FitnessdataTableAdapters.MemberTBTableAdapter();//table to be utilized in this form
        DataTable dtMember = new DataTable();
        
        public void PersonID()
        {
            DataTable IDdt = new DataTable();
            IDdt = Member_obj.GetData();// get the data from the table MemberTB

            if (IDdt.Rows.Count == 0)// if no IDs yet
            {
                Mem_ID.Text = "M-001";
            }
            else
            {
                int size = IDdt.Rows.Count - 1;
                string lastID = IDdt.Rows[size][0].ToString();
                int currentnumID = Convert.ToInt32(lastID.Substring(2, 3));//convert numbers containing in the current existing ID to integer

                if (currentnumID >= 1 && currentnumID < 9)//if ID plused one, it should remain single digit
                {
                    Mem_ID.Text = "M-00" + (currentnumID + 1);// Usual format concatenate with number(added one to the previous ID number)
                }                                            //so if M-003(current)--> M-004 inserted in the text box(Mem_ID.Text)
                else if (currentnumID >= 9 && currentnumID < 99)
                {
                    Mem_ID.Text = "M-0" + (currentnumID + 1);
                }
                else if (currentnumID >= 99 && currentnumID < 999)// if ID plused one, it should remain triple digit
                {
                    Mem_ID.Text = "M-" + (currentnumID + 1);
                }
            }
        }

        public void clear_all()
        {
            Mem_age.Text = "";//inserting blank space to clear
            Mem_dob.Text = "";
            
            Mem_height.Text = "";
            Mem_name.Text = "";
            Mem_num.Text = "";
            Mem_password.Text = "";
            Mem_weight.Clear();
            Mem_email.Text = "";
            Mem_female.Checked = false;
            Mem_male.Checked = false;
            Mem_confpass.Text = "";
        }


        private void clearbtn_Click(object sender, EventArgs e)
        {
            clear_all();
        }


        private void registerbtn_Click_1(object sender, EventArgs e)
        {
            string password = Mem_password.Text;
            string gender = null;
            int weight = Convert.ToInt32(Mem_weight.Text.ToString());//convert usual string format to int
            int height = Convert.ToInt32(Mem_height.Text.ToString());
            string name = Mem_name.Text;
            DateTime dob = Mem_dob.Value;
            string confpassword = Mem_confpass.Text;
            string phoneno = Mem_num.Text;

            if (Mem_female.Checked == true)
            {
                gender = "Female";
            }
            if (Mem_male.Checked == true)
            {
                gender = "Male";
            }
            //validation for null, and potential invalid entries
            if (Mem_female.Checked == false && Mem_male.Checked == false)// when none of the radio buttons are selected
            {
                MessageBox.Show("Please select your gender.");
            }
            if (Mem_name.Text == "")
            {
                MessageBox.Show("Please enter your registered member name");
                Mem_name.Focus();
            }
            else if (!((name.All(char.IsLetter) || name.All(char.IsDigit) || name.All(char.IsLetterOrDigit))))// three different possiblities in having only letters and numbers
            {//so if neither of them:
                MessageBox.Show("Username can only be letters and digits");
                Mem_name.Focus();
            }
            else if (Mem_num.Text == "")
            {
                MessageBox.Show("Please enter your registered phone number");
                Mem_num.Focus();
            }
            else if (!phoneno.All(char.IsDigit))// if not all the input data in phone number is digit
            {
                MessageBox.Show("Invalid Phone Number Entry.");// Warning Message
                Mem_num.Focus();// Put back cursor on phone number to readily re-enter
            }
            else if (Mem_age.Text == "")
            {
                MessageBox.Show("Please enter your age");
                Mem_age.Focus();
            }
            else if (Mem_height.Text == null)
            {
                MessageBox.Show("Please enter your height");
                Mem_height.Focus();//replace the cursor onto the height's text box
            }
            else if (Mem_weight.Text == null)
            {
                MessageBox.Show("Please enter your weight");
                Mem_weight.Focus();
            }
            else if (Mem_dob.Text == "")
            {
                MessageBox.Show("Please enter your date of birth");
                Mem_dob.Focus();
            }
            else if (Mem_password.Text == "")
            {
                MessageBox.Show("Please enter your password");
                Mem_password.Focus();
            }
            else if (!password.Any(char.IsDigit))//none is digit in password
            {
                MessageBox.Show("Password must have at least one digit.");
                Mem_password.Focus();
            }
            else if (password.Length != 12)// not of length 12
            {
                MessageBox.Show("Password must have TWELVE characters.");
                Mem_password.Focus();
            }
            else if (!password.Any(char.IsUpper))//none is upper case in password
            {
                MessageBox.Show("Password must contain at least ONE uppercase. Please Re-enter.");
                Mem_password.Focus();
            }
            else if (!password.Any(char.IsLower))// none is lower case in password
            {
                MessageBox.Show("Password must contain at least ONE lowercase letter. Please Re-enter.");
                Mem_password.Focus();
            }
            else if (Mem_confpass.Text == "")
            {
                MessageBox.Show("Please enter to verify password.");
                Mem_confpass.Focus();
            }
            else if (Mem_confpass.Text != Mem_password.Text)
            {
                MessageBox.Show("The password and confirmation password are not equal. Please re-enter.");
                Mem_confpass.Text = "";
                Mem_confpass.Focus();
            }

            else
            {
                //use of encapsulation
                Member_class Mc = new Member_class();
                Mc.memID = Mem_ID.Text;
                Mc.mempassword = password;
                Mc.memgender = gender;
                Mc.memage = Mem_age.Text;
                Mc.memweight = weight;
                Mc.memheight = height;
                Mc.memnum = Mem_num.Text;
                Mc.mememail = Mem_email.Text;

                Member_obj.Insert(Mc.memID, Mem_name.Text, Mc.mempassword, Mc.memgender, dob, Mc.memage, Mc.memweight, Mc.memheight, Mc.memnum, Mc.mememail);
                dtMember = Member_obj.GetData();

                MessageBox.Show("Automatically registered.");
                w = weight;
                Member_login Ml = new Member_login();
                Ml.Show();//to make the form (Member login) appear
                this.Hide();//to make the current form disappear
            }

        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void backbtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Member_login Ml = new Member_login();//
            Ml.Show();//to make the form appear
            this.Hide();

        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            if (starting_page.usertype=="Mem")//to identify between Members and Admins
            {
                MessageBox.Show("Sorry, only authorized individuals are allowed to utilize the button.");
            }
            else
            {
                string password = Mem_password.Text;
                string gender = null;
                int weight = Convert.ToInt32(Mem_weight.Text.ToString());//convert usual string format to int
                int height = Convert.ToInt32(Mem_height.Text.ToString());
                string name = Mem_name.Text;
                
                string confpassword = Mem_confpass.Text;
                string phoneno = Mem_num.Text;

                if (Mem_female.Checked == true)//if female chosen in the radio button
                {
                    gender = "Female";
                }
                if (Mem_male.Checked == true)
                {
                    gender = "Male";
                }
                if (Mem_female.Checked == false && Mem_male.Checked == false)
                {
                    MessageBox.Show("Please select your gender.");
                }
                else if (Mem_name.Text == "")
                {
                    MessageBox.Show("Please enter your registered member name");
                    Mem_name.Focus();
                }
                else if (Mem_email.Text == "")
                {
                    MessageBox.Show("Please enter your email.");
                    Mem_email.Focus();
                }
                else if (!((name.All(char.IsLetter) || name.All(char.IsDigit) || name.All(char.IsLetterOrDigit))))// three different possiblities in having only letters and numbers
                {//so if not either of them:
                    MessageBox.Show("Username can only be letters and digits");
                    Mem_name.Focus();
                }
                else if (Mem_num.Text == "")
                {
                    MessageBox.Show("Please enter your registered phone number");
                    Mem_num.Focus();
                }
                else if (!phoneno.All(char.IsDigit))// if not all the input data in phone number is digit
                {
                    MessageBox.Show("Invalid Phone Number Entry..");// warning message
                    Mem_num.Focus();
                }
                else if (Mem_password.Text == "")
                {
                    MessageBox.Show("Please enter your password");
                    Mem_password.Focus();
                }
                else if (Mem_age.Text == "")
                {
                    MessageBox.Show("Please enter your age");
                    Mem_age.Focus();
                }
                else if (Mem_dob.Text == "")
                {
                    MessageBox.Show("Please enter your date of birth");
                    Mem_dob.Focus();
                }
                else if (!password.Any(char.IsDigit))
                {
                    MessageBox.Show("Password must have at least one digit.");
                    Mem_password.Focus();
                }
                else if (password.Length != 12)
                {
                    MessageBox.Show("Password must have TWELVE characters.");
                    Mem_password.Focus();
                }
                else if (!password.Any(char.IsUpper))
                {
                    MessageBox.Show("Password must contain at least ONE uppercase. Please Re-enter.");
                    Mem_password.Focus();
                }
                else if (!password.Any(char.IsLower))
                {
                    MessageBox.Show("Password must contain at least ONE lowercase letter. Please Re-enter.");
                    Mem_password.Focus();
                }
                else if (Mem_confpass.Text == "")
                {
                    MessageBox.Show("Please enter to verify password.");
                    Mem_confpass.Focus();
                }
                else if (Mem_confpass.Text != Mem_password.Text)
                {
                    MessageBox.Show("The password and confirmation password are not equal. Please re-enter.");
                    Mem_confpass.Text = "";
                    Mem_confpass.Focus();
                }
                else
                {
                    Member_class Mc = new Member_class();
                    Mc.memID = Mem_ID.Text;
                    Member_obj.MemberUpdate(Mem_name.Text, password, gender, Mem_dob.Text, Mem_age.Text, weight, height, Mem_num.Text, Mem_email.Text, Mem_ID.Text);
                    MessageBox.Show("Updated Successfully!!");

                    dtMember = Member_obj.GetData();
                    Mem_dgv.DataSource = dtMember;
                    Mem_dgv.Refresh();
                   
                    PersonID();//to get ready for registration for another person
                    clear_all();
                    updatebtn.Enabled = false;
                    registerbtn.Enabled = true;
                    clearbtn.Enabled = true;
                }
            }
        }

        private void Mem_dgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = this.Mem_dgv.Rows[e.RowIndex];
            Mem_ID.Text = row.Cells[0].Value.ToString();
            Mem_name.Text = row.Cells[1].Value.ToString();
            Mem_password.Text = row.Cells[2].Value.ToString();
            Mem_dob.Text= row.Cells[4].Value.ToString();
            Mem_age.Text = row.Cells[5].Value.ToString();
            Mem_weight.Text = row.Cells[6].Value.ToString();
            Mem_height.Text = row.Cells[7].Value.ToString();
            Mem_num.Text = row.Cells[8].Value.ToString();
            Mem_email.Text = row.Cells[9].Value.ToString();
            Mem_confpass.Text = Mem_password.Text;
            updatebtn.Enabled = true;
        }

        private void Member_register_Load(object sender, EventArgs e)
        {
            if (starting_page.usertype == "Mem")//to identify between Members and Admins
            {
                Mem_dgv.Visible = false;//Members shouldn't be able to view other member's personal information
            }
        }


    }
}

