using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GamefarmManagemet
{
    public partial class GamefowlsRecords : Form
    {
        private Label titleLabel;
        private DataGridView gamefowlGrid;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnBack;

        private string connectionString = "server=localhost;uid=root;pwd=Leonard010504.;database=ex_db;";

        public GamefowlsRecords()
        {
            InitializeComponent();
            InitializeLayout();
            ApplyDarkMode();
            LoadGamefowlsData();
        }

        private void InitializeLayout()
        {
            this.Size = new Size(1000, 800);
            this.Text = "Gamefowls Records";
            this.StartPosition = FormStartPosition.CenterScreen;

            titleLabel = new Label()
            {
                Text = "Gamefowls Records",
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

            gamefowlGrid = new DataGridView()
            {
                Location = new Point(20, 120),
                Size = new Size(940, 620),
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            gamefowlGrid.Columns.Add("GameFowl_ID", "GameFowl ID");
            gamefowlGrid.Columns.Add("Bloodline", "Bloodline");
            gamefowlGrid.Columns.Add("Date_Hatched", "Date Hatched");
            gamefowlGrid.Columns.Add("Band_Number", "Band Number");

            this.Controls.Add(titleLabel);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnBack);
            this.Controls.Add(gamefowlGrid);
        }

        private void LoadGamefowlsData()
        {
            string query = "SELECT GameFowl_ID, Bloodline, Date_Hatched, Band_Number FROM gamefowls";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            gamefowlGrid.Rows.Clear();

                            while (reader.Read())
                            {
                                int id = reader.GetInt32("GameFowl_ID");
                                string bloodline = reader.GetString("Bloodline");
                                DateTime dateHatched = reader.GetDateTime("Date_Hatched");
                                string bandNumber = reader.GetString("Band_Number");

                                gamefowlGrid.Rows.Add(id, bloodline, dateHatched.ToString("yyyy-MM-dd"), bandNumber);
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
            string bloodline = Microsoft.VisualBasic.Interaction.InputBox("Enter Bloodline:", "Add Gamefowl", "");
            string dateHatched = Microsoft.VisualBasic.Interaction.InputBox("Enter Date Hatched (yyyy-MM-dd):", "Add Gamefowl", DateTime.Today.ToString("yyyy-MM-dd"));

            if (!string.IsNullOrEmpty(bloodline) && !string.IsNullOrEmpty(dateHatched))
            {
                using (BandNumberDialog bandNumberDialog = new BandNumberDialog())
                {
                    if (bandNumberDialog.ShowDialog() == DialogResult.OK)
                    {
                        string bandNumber = bandNumberDialog.SelectedBandNumber;

                        try
                        {
                            using (MySqlConnection conn = new MySqlConnection(connectionString))
                            {
                                conn.Open();
                                string insertQuery = "INSERT INTO gamefowls (Bloodline, Date_Hatched, Band_Number) VALUES (@bloodline, @dateHatched, @bandNumber)";
                                using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                                {
                                    cmd.Parameters.AddWithValue("@bloodline", bloodline);
                                    cmd.Parameters.AddWithValue("@dateHatched", DateTime.Parse(dateHatched));
                                    cmd.Parameters.AddWithValue("@bandNumber", bandNumber);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            MessageBox.Show("Gamefowl added successfully.");
                            LoadGamefowlsData();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error adding data: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Bloodline and Date Hatched are required.");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (gamefowlGrid.SelectedRows.Count > 0)
            {
                int selectedID = Convert.ToInt32(gamefowlGrid.SelectedRows[0].Cells["GameFowl_ID"].Value);

                var confirmResult = MessageBox.Show("Are you sure you want to delete this gamefowl?",
                    "Confirm Delete", MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();
                            string deleteQuery = "DELETE FROM gamefowls WHERE GameFowl_ID = @id";
                            using (MySqlCommand cmd = new MySqlCommand(deleteQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@id", selectedID);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Gamefowl deleted.");
                        LoadGamefowlsData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting gamefowl: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a gamefowl to delete.");
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

            gamefowlGrid.BackgroundColor = Color.FromArgb(40, 40, 40);
            gamefowlGrid.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            gamefowlGrid.DefaultCellStyle.ForeColor = Color.White;
            gamefowlGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            gamefowlGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gamefowlGrid.EnableHeadersVisualStyles = false;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 mainMenu = new Form2();
            mainMenu.Show();
        }

        // Inner class for Band Number input dialog
        private class BandNumberDialog : Form
        {
            private TextBox textBox;
            private Button btnOK;
            private Button btnCancel;

            public string SelectedBandNumber { get; private set; }

            public BandNumberDialog()
            {
                this.StartPosition = FormStartPosition.CenterParent;
                this.Text = "Enter Band Number";
                this.ClientSize = new Size(240, 90);
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.ShowInTaskbar = false;

                textBox = new TextBox()
                {
                    Location = new Point(15, 15),
                    Width = 200
                };

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

                this.Controls.Add(textBox);
                this.Controls.Add(btnOK);
                this.Controls.Add(btnCancel);

                this.AcceptButton = btnOK;
                this.CancelButton = btnCancel;

                btnOK.Click += (s, e) =>
                {
                    if (!string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        SelectedBandNumber = textBox.Text.Trim();
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid band number.");
                        DialogResult = DialogResult.None; // Prevent dialog from closing
                    }
                };
            }
        }

        private void GamefowlsRecords_Load(object sender, EventArgs e)
        {

        }
    }
}