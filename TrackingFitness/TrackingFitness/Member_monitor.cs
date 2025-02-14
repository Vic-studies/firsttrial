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
    public partial class Member_monitor : Form
    {
        FitnessdataTableAdapters.ActTBTableAdapter Activity_obj = new FitnessdataTableAdapters.ActTBTableAdapter();
        DataTable adt = new DataTable();
        FitnessdataTableAdapters.MonitorTBTableAdapter Monitor_obj = new FitnessdataTableAdapters.MonitorTBTableAdapter();
        DataTable mdt = new DataTable();
        FitnessdataTableAdapters.Member_GoalTBTableAdapter Goal_obj = new FitnessdataTableAdapters.Member_GoalTBTableAdapter();
        DataTable gdt = new DataTable();

        DataTable chosenadt = new DataTable();//for activity chosen in form
        DataTable checkact = new DataTable();//collection items for activity combo box
        string ID = Member_login.MemID;

        string status = "";
        double burned_cal, tol_burned_cal, weight,subtraction;
        int i_tolcal;
        int desiredcalorie;
        string intensity;

        public Member_monitor()
        {
            InitializeComponent();
            //for progress (individually)
            mdt = Monitor_obj.ProgressIndividually(ID);
            dgvmonitorform.DataSource = mdt;
            dgvmonitorform.Refresh();
            //for each activity (individually)
            adt = Activity_obj.ActivityIndividual(ID);
            dgvcalculation.DataSource = adt;
            dgvcalculation.Refresh();
            //for goal (individually)
            gdt = Goal_obj.GoalIndividually(ID);
            dgvgoalform.DataSource = gdt;
            dgvgoalform.Refresh();

            if (gdt.Rows.Count > 0)
            {
                goaltimelbl.Visible = true;
                goaltimelbl.Text = "Setted to be ended on:" + dgvgoalform[3, 0].Value.ToString();
            }
            cal_losslbl.Text = "0";
            //public variables
        }
               
        private void Member_monitor_Load(object sender, EventArgs e)
        {
            //for display(taken from previously entered forms)
            titlelbl.Text = Member_login.MemName+"'s " + titlelbl.Text; //Concatenate the current title(Progress) with the member's name(gotten from login form)
            
            //desiredcalorie = Convert.ToInt32( dgvgoalform[0, 5].Value.ToString());// take from goal
            
            //activity choices
            checkact = Activity_obj.ActivityIndividual(ID);
            dgv_individualact.DataSource = checkact;
            dgv_individualact.Refresh();
            if (checkact.Rows.Count > 0)
            {
                for (int counter = 0; counter < checkact.Rows.Count; counter++)
                {
                    actcbo.Items.Add(adt.Rows[counter][1].ToString());
                }
            }
            else
            {
                MessageBox.Show("Please go to '+Add Activity' in home form first");
            }
        }

        public void achievement_status()
        {
            DataTable goal_cal = new DataTable();
            //take desired calorie from Goal form
            goal_cal = Goal_obj.GoalIndividually(ID);
            dgvdesirecal.DataSource = goal_cal;
            dgvdesirecal.Refresh();
            desiredcalorie = Convert.ToInt32(dgvdesirecal[5,0].Value.ToString());

            tolcallbl.Text = Convert.ToString(tol_burned_cal);
            double calorie_compare = Convert.ToDouble(desiredcalorie);
            subtraction = calorie_compare - tol_burned_cal;
   
            if (tol_burned_cal < calorie_compare)
            {
                status = "Goal not acheived, yet." + Convert.ToString(subtraction) + "\n"+" calories left.";
            }
            else if (tol_burned_cal > calorie_compare)
             {
                 status = "Goal achieved with flying colours; " + Convert.ToString(subtraction * -1) + "\n" + " more calories loss in addition to originial goal:" + Convert.ToString(calorie_compare);
             }
            else if (tol_burned_cal == calorie_compare)
             {
                 status = "Goal achieved perfectly; with exactly " + Convert.ToString(calorie_compare) + "\n" + " calories, that was setted in goal.";
             }
            achievelbl.Text = status;
        }

        private void calbtn_Click(object sender, EventArgs e)
        {
            if (toltimetxt.Text == "")
            {
                MessageBox.Show("Please enter how long you've been exercising.");
                toltimetxt.Focus();
            }
            else if (actcbo.SelectedIndex<0)
            {
                MessageBox.Show("Please select the activity to be calculated.");
            }
            else if ( gdt.Rows.Count > 0)// activity checked in null, cuz without activity no activities will be able to be chosen in combo box
            {
                string activity = actcbo.SelectedItem.ToString();
                double met=0;

                if (activity == "Walking")
                {
                    if (intensity == "Light")
                    {
                        met = 2.3;
                    }
                    if (intensity == "Moderate")
                    {
                        met = 4.5;
                    }
                    if (intensity == "Vigorous")
                    {
                        met = 6;
                    }
                    
  //estimated average MET according to research
                }
                else if (activity == "Swimming")
                {
                    if (intensity == "Light")
                    {
                        met = 4.8;
                    }
                    if (intensity == "Moderate")
                    {
                        met = 5.8;
                    }
                    if (intensity == "Vigorous")
                    {
                        met = 10;
                    }
                }
                else if (activity == "Pilates")
                {
                    if (intensity == "Light")
                    {
                        met = 3;
                    }
                    if (intensity == "Moderate")
                    {
                        met = 3.5;
                    }
                    if (intensity == "Vigorous")
                    {
                        met = 3.7;
                    }
                }   
                else if (activity == "Push-ups")
                {
                    if (intensity == "Light")
                    {
                        met = 3.8;
                    }
                    if (intensity == "Moderate")
                    {
                        met = 5.9;
                    }
                    if (intensity == "Vigorous")
                    {
                        met = 8;
                    }
                }
                else if (activity == "Squats")
                {
                    if (intensity == "Light")
                    {
                        met = 3.5;
                    }
                    if (intensity == "Moderate")
                    {
                        met = 5.5;
                    }
                    if (intensity == "vigorous")
                    {
                        met = 8;
                    }
                }
                else
                {
                    if (intensity == "Light")
                    {
                        met = 4.1;
                    }
                    if (intensity == "Moderate")
                    {
                        met = 5;
                    }
                    if (intensity == "Vigorous")
                    {
                        met = 6.2;
                    }
                }
                double minutes_taken = Convert.ToDouble(toltimetxt.Text);
                burned_cal = ((met * weight * 3.5 / 200 * minutes_taken));//for currently selected (one) activity
                cal_losslbl.Text = Convert.ToString(burned_cal);

                //total calorie, will be added on as the user select the activity they want to calculate and the time they have exercised
                tol_burned_cal = tol_burned_cal + burned_cal;
                tolcallbl.Text = Convert.ToString(tol_burned_cal);
                achievement_status();
                MessageBox.Show("Successfully Calculated!!");
                recordbtn.Enabled = true;
            }
            else
            {
                MessageBox.Show("Please fill out 'Set Goal' in prior to resuming this form.");
            }
        }

        private void recordbtn_Click(object sender, EventArgs e)
        {
            i_tolcal = Convert.ToInt32(tol_burned_cal);

                int monidata= Monitor_obj.ProgressInsert(ID, toltimetxt.Text, achievelbl.Text, i_tolcal);

                MessageBox.Show("Successfully Recorded.");
                //show newly recorded data into dgv

                 mdt = Monitor_obj.ProgressIndividually(ID);
                 dgvmonitorform.DataSource = mdt;
                 dgvmonitorform.Refresh();
        }

        private void actcbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string activity= actcbo.Text;
            DataTable forcal = new DataTable();
            if (activity != "")//for extra measures (incase of null errors)
            {
                forcal = Activity_obj.EachAct_info(actcbo.SelectedItem.ToString(), ID);//taken in information provided previously that fits the activity selected
                dgvcalculation.DataSource = forcal;
                dgvcalculation.Refresh();
                weight = Convert.ToDouble(dgvcalculation[2, 0].Value.ToString());//take weight from activity

                intensitylbl.Text = dgvcalculation[4, 0].Value.ToString();//take intensity from activity
                intensity = intensitylbl.Text;// for easier calculation
            }
        }
    }
}
