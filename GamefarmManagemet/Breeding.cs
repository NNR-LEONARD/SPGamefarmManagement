using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GamefarmManagemet
{
    public partial class BreedingRecords : Form
    {
        private Label titleLabel;
        private DataGridView breedingGrid;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnBack;

        private string connectionString = "server=localhost;uid=root;pwd=Leonard010504.;database=ex_db;";

        public BreedingRecords()
        {
            // Manually initialize components to avoid CS8618
            titleLabel = new Label();
            breedingGrid = new DataGridView();
            btnAdd = new Button();
            btnDelete = new Button();
            btnBack = new Button();

            InitializeLayout();
            ApplyDarkMode();
            LoadBreedingData();
        }

        private void InitializeLayout()
        {
            this.Size = new Size(1000, 800);
            this.Text = "Breeding Records";
            this.StartPosition = FormStartPosition.CenterScreen;

            titleLabel = new Label()
            {
                Text = "Breeding Records",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            int buttonY = 70;
            int spacing = 140;

            btnAdd = new Button()
            {
                Text = "Add",
                Location = new Point(20, buttonY),
                Size = new Size(120, 30)
            };
            btnAdd.Click += BtnAdd_Click;

            btnDelete = new Button()
            {
                Text = "Delete",
                Location = new Point(20 + spacing, buttonY),
                Size = new Size(120, 30)
            };
            btnDelete.Click += BtnDelete_Click;

            btnBack = new Button()
            {
                Text = "Back to Menu",
                Location = new Point(20 + spacing * 2, buttonY),
                Size = new Size(120, 30)
            };
            btnBack.Click += BtnBack_Click;

            breedingGrid = new DataGridView()
            {
                Location = new Point(20, 120),
                Size = new Size(940, 620),
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            breedingGrid.Columns.Add("Breeding_ID", "Breeding ID");
            breedingGrid.Columns.Add("Gamefowl_ID", "Gamefowl ID");
            breedingGrid.Columns.Add("Band_Number", "Band Number");
            breedingGrid.Columns.Add("Date_Breeding", "Date of Breeding");
            breedingGrid.Columns.Add("Event", "Event");

            this.Controls.Add(titleLabel);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnBack);
            this.Controls.Add(breedingGrid);
        }

        private void LoadBreedingData()
        {
            string query = "SELECT Breeding_ID, Gamefowl_ID, Band_Number, Date_Breeding, Event FROM breeding_records";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            breedingGrid.Rows.Clear();

                            while (reader.Read())
                            {
                                int breedingId = reader.GetInt32("Breeding_ID");
                                int gamefowlId = reader.GetInt32("Gamefowl_ID");
                                string bandNumber = reader.GetString("Band_Number");
                                DateTime dateBreeding = reader.GetDateTime("Date_Breeding");
                                string eventType = reader.GetString("Event");

                                breedingGrid.Rows.Add(breedingId, gamefowlId, bandNumber, dateBreeding.ToString("yyyy-MM-dd"), eventType);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string gamefowlId = Microsoft.VisualBasic.Interaction.InputBox("Enter Gamefowl ID:", "Add Breeding Record", "");
            if (string.IsNullOrEmpty(gamefowlId) || !int.TryParse(gamefowlId, out int parsedGamefowlId))
            {
                MessageBox.Show("Please enter a valid Gamefowl ID.");
                return;
            }

            // Validate Gamefowl_ID exists in gamefowls table and get associated Band_Number
            string expectedBandNumber = null;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string checkQuery = "SELECT Band_Number FROM gamefowls WHERE GameFowl_ID = @id";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@id", parsedGamefowlId);
                        var result = checkCmd.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show("Gamefowl ID does not exist.");
                            return;
                        }
                        expectedBandNumber = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error validating Gamefowl ID: " + ex.Message);
                return;
            }

            string bandNumber = Microsoft.VisualBasic.Interaction.InputBox("Enter Band Number:", "Add Breeding Record", expectedBandNumber);
            if (string.IsNullOrEmpty(bandNumber))
            {
                MessageBox.Show("Band Number is required.");
                return;
            }

            // Validate Band_Number matches the one in gamefowls table
            if (bandNumber != expectedBandNumber)
            {
                MessageBox.Show($"Band Number must match the Gamefowl ID's Band Number: {expectedBandNumber}");
                return;
            }

            string dateBreeding = Microsoft.VisualBasic.Interaction.InputBox("Enter Date of Breeding (yyyy-MM-dd):", "Add Breeding Record", DateTime.Today.ToString("yyyy-MM-dd"));
            if (string.IsNullOrEmpty(dateBreeding))
            {
                MessageBox.Show("Date of Breeding is required.");
                return;
            }

            using (EventDialog eventDialog = new EventDialog())
            {
                if (eventDialog.ShowDialog() == DialogResult.OK)
                {
                    string eventType = eventDialog.SelectedEvent;

                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();
                            string insertQuery = "INSERT INTO breeding_records (Gamefowl_ID, Band_Number, Date_Breeding, Event) VALUES (@gamefowlId, @bandNumber, @dateBreeding, @event)";
                            using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@gamefowlId", parsedGamefowlId);
                                cmd.Parameters.AddWithValue("@bandNumber", bandNumber);
                                cmd.Parameters.AddWithValue("@dateBreeding", DateTime.Parse(dateBreeding));
                                cmd.Parameters.AddWithValue("@event", eventType);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Breeding record added successfully.");
                        LoadBreedingData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error adding data: " + ex.Message);
                    }
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (breedingGrid.SelectedRows.Count > 0)
            {
                int selectedID = Convert.ToInt32(breedingGrid.SelectedRows[0].Cells["Breeding_ID"].Value);

                var confirmResult = MessageBox.Show("Are you sure you want to delete this breeding record?",
                    "Confirm Delete", MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();
                            string deleteQuery = "DELETE FROM breeding_records WHERE Breeding_ID = @id";
                            using (MySqlCommand cmd = new MySqlCommand(deleteQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@id", selectedID);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Breeding record deleted.");
                        LoadBreedingData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting breeding record: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a breeding record to delete.");
            }
        }

        private void ApplyDarkMode()
        {
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ForeColor = Color.Gainsboro;

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Label || ctrl is Button)
                {
                    ctrl.ForeColor = Color.Gainsboro;
                    ctrl.BackColor = Color.FromArgb(45, 45, 45);
                }

                if (ctrl is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = Color.DimGray;
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 60, 60);
                }
            }

            breedingGrid.BackgroundColor = Color.FromArgb(40, 40, 40);
            breedingGrid.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            breedingGrid.DefaultCellStyle.ForeColor = Color.White;
            breedingGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            breedingGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            breedingGrid.EnableHeadersVisualStyles = false;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 mainMenu = new Form2();
            mainMenu.Show();
        }

        // Inner class for Event selection dialog
        private class EventDialog : Form
        {
            private ComboBox comboBox;
            private Button btnOK;
            private Button btnCancel;

            public string SelectedEvent { get; private set; }

            public EventDialog()
            {
                this.StartPosition = FormStartPosition.CenterParent;
                this.Text = "Select Event";
                this.ClientSize = new Size(240, 90);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.ShowInTaskbar = false;

                comboBox = new ComboBox()
                {
                    Location = new Point(15, 15),
                    Width = 200,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                };

                comboBox.Items.Add("Local-born");
                comboBox.Items.Add("Early-bird");
                comboBox.Items.Add("International");
                comboBox.SelectedIndex = 0;

                btnOK = new Button()
                {
                    Text = "OK",
                    DialogResult = DialogResult.OK,
                    Location = new Point(15, 50),
                    Width = 90,
                };

                btnCancel = new Button()
                {
                    Text = "Cancel",
                    DialogResult = DialogResult.Cancel,
                    Location = new Point(125, 50),
                    Width = 90,
                };

                this.Controls.Add(comboBox);
                this.Controls.Add(btnOK);
                this.Controls.Add(btnCancel);

                this.AcceptButton = btnOK;
                this.CancelButton = btnCancel;

                btnOK.Click += (s, e) =>
                {
                    SelectedEvent = comboBox.SelectedItem.ToString();
                };
            }
        }

        private void BreedingRecords_Load(object sender, EventArgs e)
        {

        }
    }
}