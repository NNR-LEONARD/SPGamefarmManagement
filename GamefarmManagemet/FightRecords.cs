using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GamefarmManagemet
{
    public partial class FightRecords : Form
    {
        private DataGridView fightGrid;
        private TextBox txtGameFowlID, txtOpponentBreed, txtLocation;
        private ComboBox cmbResult;
        private DateTimePicker dtpFightDate;
        private CheckBox chkInjured;
        private Button btnAdd, btnBack;

        string connectionString = "server=localhost;database=ex_db;uid=root;pwd=Leonard010504.;";

        public FightRecords()
        {
            InitializeComponent();
            InitializeLayout();
            ApplyDarkMode();
            LoadFightRecords();
        }

        private void InitializeLayout()
        {
            this.Size = new Size(1000, 700);
            this.Text = "Fight Records";
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label()
            {
                Text = "Fight Records",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            fightGrid = new DataGridView()
            {
                Location = new Point(20, 70),
                Size = new Size(940, 300),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(fightGrid);

            // Input Fields
            Label lblGameFowlID = new Label() { Text = "GameFowl ID:", Location = new Point(20, 390), AutoSize = true };
            txtGameFowlID = new TextBox() { Location = new Point(130, 385), Width = 100 };

            Label lblOpponent = new Label() { Text = "Opponent:", Location = new Point(250, 390), AutoSize = true };
            txtOpponentBreed = new TextBox() { Location = new Point(330, 385), Width = 120 };

            Label lblDate = new Label() { Text = "Date:", Location = new Point(470, 390), AutoSize = true };
            dtpFightDate = new DateTimePicker() { Location = new Point(520, 385), Format = DateTimePickerFormat.Short };

            Label lblLocation = new Label() { Text = "Location:", Location = new Point(660, 390), AutoSize = true };
            txtLocation = new TextBox() { Location = new Point(730, 385), Width = 100 };

            Label lblResult = new Label() { Text = "Result:", Location = new Point(20, 430), AutoSize = true };
            cmbResult = new ComboBox() { Location = new Point(80, 425), Width = 100 };
            cmbResult.Items.AddRange(new[] { "Win", "Loss" });
            cmbResult.DropDownStyle = ComboBoxStyle.DropDownList;

            chkInjured = new CheckBox() { Text = "Injured", Location = new Point(200, 427) };

            btnAdd = new Button()
            {
                Text = "Add Fight",
                Location = new Point(320, 425),
                Width = 100
            };
            btnAdd.Click += BtnAdd_Click;

            btnBack = new Button()
            {
                Text = "Back to Menu",
                Location = new Point(830, 600),
                Width = 120
            };
            btnBack.Click += (s, e) => { this.Close(); new Form2().Show(); };

            this.Controls.AddRange(new Control[] {
                lblGameFowlID, txtGameFowlID,
                lblOpponent, txtOpponentBreed,
                lblDate, dtpFightDate,
                lblLocation, txtLocation,
                lblResult, cmbResult,
                chkInjured, btnAdd, btnBack
            });
        }

        private void LoadFightRecords()
        {
            fightGrid.Columns.Clear();
            fightGrid.Rows.Clear();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM fightrecord";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                fightGrid.Columns.Add("Fight_ID", "Fight ID");
                fightGrid.Columns.Add("GameFowl_ID", "GameFowl ID");
                fightGrid.Columns.Add("OpponentBreed", "Opponent");
                fightGrid.Columns.Add("Fight_Date", "Date");
                fightGrid.Columns.Add("Location", "Location");
                fightGrid.Columns.Add("Result", "Result");
                fightGrid.Columns.Add("Injured", "Injured");

                while (reader.Read())
                {
                    fightGrid.Rows.Add(
                        reader["Fight_ID"].ToString(),
                        reader["GameFowl_ID"].ToString(),
                        reader["OpponentBreed"].ToString(),
                        Convert.ToDateTime(reader["Fight_Date"]).ToString("yyyy-MM-dd"),
                        reader["Location"].ToString(),
                        reader["Result"].ToString(),
                        reader["Injured"].ToString()
                    );
                }

                conn.Close();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (txtGameFowlID.Text == "" || txtOpponentBreed.Text == "" || txtLocation.Text == "" || cmbResult.SelectedItem == null)
            {
                MessageBox.Show("Please fill all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string insertQuery = @"INSERT INTO fightrecord
                    (GameFowl_ID, OpponentBreed, Fight_Date, Location, Result, Injured)
                    VALUES (@GameFowl_ID, @OpponentBreed, @Fight_Date, @Location, @Result, @Injured)";
                MySqlCommand cmd = new MySqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@GameFowl_ID", txtGameFowlID.Text);
                cmd.Parameters.AddWithValue("@OpponentBreed", txtOpponentBreed.Text);
                cmd.Parameters.AddWithValue("@Fight_Date", dtpFightDate.Value.Date);
                cmd.Parameters.AddWithValue("@Location", txtLocation.Text);
                cmd.Parameters.AddWithValue("@Result", cmbResult.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Injured", chkInjured.Checked ? 1 : 0);

                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Fight record added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadFightRecords();
            }
        }

        private void ApplyDarkMode()
        {
            this.BackColor = Color.FromArgb(30, 30, 30);
            foreach (Control ctrl in this.Controls)
            {
                ctrl.ForeColor = Color.White;
                ctrl.BackColor = ctrl is Button ? Color.FromArgb(45, 45, 45) : Color.FromArgb(30, 30, 30);
            }

            fightGrid.BackgroundColor = Color.FromArgb(40, 40, 40);
            fightGrid.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            fightGrid.DefaultCellStyle.ForeColor = Color.White;
            fightGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            fightGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            fightGrid.EnableHeadersVisualStyles = false;
        }

        private void FightRecords_Load(object sender, EventArgs e)
        {

        }
    }
}
