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

        private MySqlConnection GetConnection()
        {
            string connStr = "server=localhost;user=root;database=your_db;port=3306;password=Leonard010504.";
            return new MySqlConnection(connStr);
        }

        // ✅ Test if DB connection works
        private void TestDatabaseConnection()
        {
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    MessageBox.Show("✅ Connected to database successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Connection failed:\n" + ex.Message);
                }
            }
        }

        // ✅ Test reading data
        private void ReadSampleData()
        {
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM your_table LIMIT 1"; // change "your_table" to real table name
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        MessageBox.Show("✅ Data found: " + reader[0].ToString());
                    }
                    else
                    {
                        MessageBox.Show("⚠️ Connected, but no data found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("❌ Error reading data:\n" + ex.Message);
                }
            }
        }

        // Example: Hook up these to buttons (e.g. button10 and button11)
        private void button6_Click(object sender, EventArgs e)
        {
            TestDatabaseConnection();
        }

    }
}
