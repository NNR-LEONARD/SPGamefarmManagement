using System;
using System.Security.AccessControl;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GamefarmManagemet 
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FightRecords fightrecordsForm = new FightRecords();
            fightrecordsForm.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                clickedButton.BackColor = Color.Red; // turns red after clicked
            }

            Attendance attendanceForm = new Attendance();
            attendanceForm.Show();
            this.Hide();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide(); // hide the current menu
            HandlerManagement handlerForm = new HandlerManagement();
            handlerForm.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
        private void button10_Click(object sender, EventArgs e)
        {
            // Create an instance of the target form
            Form targetForm = new FightRecords();

            // Optionally, if you want to hide the current form:
            this.Hide();

            // Show the new form
            targetForm.Show();
        }


        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_Load_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            GamefowlsRecords recordsForm = new GamefowlsRecords();
            recordsForm.Show();
            this.Hide(); // Optional: Hide main menu
        }


        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Breeding menuForm = new Breeding(); // Replace with your actual form
            menuForm.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            MedicalRecords menuForm = new MedicalRecords(); // Replace with your actual form
            menuForm.Show();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            TrainingLogs menuForm = new TrainingLogs(); // Replace with your actual form
            menuForm.Show();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            AuditLogs menuForm = new AuditLogs(); // Replace with your actual form
            menuForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
