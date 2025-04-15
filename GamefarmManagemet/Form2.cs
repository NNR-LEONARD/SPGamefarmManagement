using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GamefarmManagemet  // Still has a typo: should be GamefarmManagement if you want to fix that
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Go back to Form1
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
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
    }
}
