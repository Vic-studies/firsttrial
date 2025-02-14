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
    public partial class Member_activity : Form
    {
        FitnessdataTableAdapters.ActTBTableAdapter Activity_obj = new FitnessdataTableAdapters.ActTBTableAdapter();
        DataTable adt = new DataTable();
        DataTable individualdt = new DataTable();

        public string a_ID;
        string ID = Member_login.MemID;
        int weight;

        public Member_activity()
        {
            InitializeComponent();
            removebtn.Enabled = false;

            individualdt = Activity_obj.ActivityIndividual(ID);
            dgvdata.DataSource = individualdt;
            dgvdata.Refresh();

            adt = Activity_obj.GetData();
            dgvID.DataSource = adt;
            dgvID.Refresh();
            

        }

        public void PersonID()
        {

            adt = Activity_obj.GetData();// get the data from the table dtMember

            if (adt.Rows.Count == 0)// if no IDs yet
            {
                Act_ID.Text = "a-001";
            }
            else
            {
                int size = adt.Rows.Count - 1;
                string lastID = adt.Rows[size][0].ToString();
                int currentnumID = Convert.ToInt32(lastID.Substring(2, 3));//convert numbers to integer

                if (currentnumID >= 1 && currentnumID < 9)
                {
                    Act_ID.Text = "a-00" + (currentnumID + 1);
                }
                else if (currentnumID >= 9 && currentnumID < 99)
                {
                    Act_ID.Text = "a-0" + (currentnumID + 1);
                }
                else if (currentnumID >= 99 && currentnumID < 999)
                {
                    Act_ID.Text = "a-" + (currentnumID + 1);
                }
            }
        }

       
        private void recordbtn_Click(object sender, EventArgs e)
        {
            int flag = -1;
            for (int counter = 0; counter < dgvdata.Rows.Count; counter++)//go thourgh all rows
            {
                if (Convert.ToString(dgvdata.Rows[counter].Cells["Activity_name"].Value) == actnamecbo.Text)//check the one entered in textbox and the one in dgv, current row shown
                {
                    flag = counter;//to stop looping as duplicated activity is already found
                    break;
                }
            }

            if (actnamecbo.SelectedIndex == -1)
            {
                MessageBox.Show("Please select your activity");
            }
            else if (intensitycbo.SelectedIndex==-1)
            {
                MessageBox.Show("Please select your level of intensity.");
            }
            else if (weighttxt.Text == "")
            {
                MessageBox.Show("Please enter your weight");
            }
            else if (flag!=-1)
            {
                MessageBox.Show("Duplicate Entry Found. Please Re-select a new activity.");//to not allow user to enter the same activity twice

            }
            else
            {
                clearbtn.Enabled = true;
                weight = Convert.ToInt32(weighttxt.Text.ToString());//weight for insert, as in sql it is selected as int type
                int adata = Activity_obj.ActivityInsert(Act_ID.Text, actnamecbo.SelectedItem.ToString(), weight, ID, intensitycbo.SelectedItem.ToString());
                
                MessageBox.Show("Successfully recorded, thank you.");
                PersonID();
                individualdt = Activity_obj.ActivityIndividual(ID);
                dgvdata.DataSource = individualdt;
                dgvdata.Refresh();
                Clearallboxes();
            }
        }


        private void Member_activity_Load(object sender, EventArgs e)
        {
            PersonID();
        }
        public void Clearallboxes()
        {
            weighttxt.Text = "";
            intensitycbo.SelectedItem = null;
            actnamecbo.SelectedItem=null;
        }
        private void clearbtn_Click(object sender, EventArgs e)
        {
            Clearallboxes();
        }
        
        private void dgvdata_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //when a cell from the dgv is clicked the values will start to correspond with the txt box and cbo box
            DataGridViewRow row = this.dgvdata.Rows[e.RowIndex];
            Act_ID.Text = row.Cells[0].Value.ToString();
            actnamecbo.Text = row.Cells[1].Value.ToString();
            weighttxt.Text = row.Cells[2].Value.ToString();
            intensitycbo.Text = row.Cells[4].Value.ToString();

            removebtn.Enabled = true;
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            DialogResult checkmessa = MessageBox.Show("Are you sure you want to delete this activity from your list of activity?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (checkmessa.Equals(DialogResult.OK))
            {
                Activity_obj.ActivityDelete(Act_ID.Text);
                MessageBox.Show("Successfully deleted");

                Clearallboxes();
                PersonID();
                removebtn.Enabled = false;
                recordbtn.Enabled = true;

                individualdt = Activity_obj.ActivityIndividual(ID);
                dgvdata.DataSource = individualdt;
                dgvdata.Refresh();// to update the table display


            }
        }
    }
}
