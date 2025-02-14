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
    public partial class Member_goal : Form
    {
        public Member_goal()
        {
            InitializeComponent();
            PersonID();
            Mem_name.Text = Member_login.MemName;
        }
        public static string uid, gid, uname;
        FitnessdataTableAdapters.Member_GoalTBTableAdapter Goal_obj = new FitnessdataTableAdapters.Member_GoalTBTableAdapter();
        DataTable dtgoal = new DataTable();

        private void enddatedtp_ValueChanged(object sender, EventArgs e)
        {
            DateTime start, end;
            TimeSpan diff;

            start = begindatedtp.Value;
            end = enddatedtp.Value;
            diff = end - start;

            daytaken.Text = diff.TotalDays.ToString();
        }

        public void PersonID()
        {
            DataTable IDdt = new DataTable();
            IDdt = Goal_obj.GetData();// get the data from the table dtGoal

            if (IDdt.Rows.Count == 0)// if no IDs yet
            {
                Goal_ID.Text = "G-001";
            }
            else
            {
                int size = IDdt.Rows.Count - 1;
                string lastID = IDdt.Rows[size][0].ToString();
                int currentnumID = Convert.ToInt32(lastID.Substring(2, 3));//convert, and select numbers in ID to integer

                if (currentnumID >= 1 && currentnumID < 9)
                {
                    Goal_ID.Text = "G-00" + (currentnumID + 1);
                }
                else if (currentnumID >= 9 && currentnumID < 99)
                {
                    Goal_ID.Text = "G-0" + (currentnumID + 1);
                }
                else if (currentnumID >= 99 && currentnumID < 999)
                {
                    Goal_ID.Text = "G-" + (currentnumID + 1);
                }
            }
        }

        private void Member_goal_Load(object sender, EventArgs e)
        {
            uid = Member_login.MemID;
            uname = Member_login.MemName;
        }

        private void setbtn_Click(object sender, EventArgs e)
        {

            if (desirecalories.Text == "")
            {
                MessageBox.Show("Enter the calories that you aim to lose.");
                desirecalories.Focus();
            }
            else if (daytaken.Text =="")
            {
                MessageBox.Show("Enter the dates to begin, and end.");
                desirecalories.Focus();
            }
            else
            {
                int duration = Convert.ToInt32(daytaken.Text.ToString());
                int calorie = Convert.ToInt32(desirecalories.Text.ToString());

                int gdata = Goal_obj.GoalInsert(Goal_ID.Text, uid, begindatedtp.Text, enddatedtp.Text, duration, calorie, uname);

                MessageBox.Show("Successfully set.");
                this.Close();
            }
        }
    }
}
            
        
        
        

    

