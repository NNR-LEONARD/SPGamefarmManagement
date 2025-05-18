using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GamefarmManagemet
{
    public partial class TrainingLogs : Form
    {
        private Label titleLabel;
        private DataGridView trainingGrid;
        private Button btnBack;
        private Button btnAdd;

        private string connectionString = "server=localhost;uid=root;pwd=Leonard010504.;database=ex_db;";

        public TrainingLogs()
        {
            InitializeComponent();
            InitializeLayout();
            ApplyDarkMode();
            LoadTrainingLogs();
        }

        private void InitializeLayout()
        {
            this.Size = new Size(1000, 800);
            this.Text = "Training Logs";
            this.StartPosition = FormStartPosition.CenterScreen;

            titleLabel = new Label()
            {
                Text = "Training Logs",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            btnBack = new Button()
            {
                Text = "Back to Menu",
                Location = new Point(800, 30),
                Size = new Size(120, 30)
            };
            btnBack.Click += BtnBack_Click;

            btnAdd = new Button()
            {
                Text = "Add Training Log",
                Location = new Point(660, 30),
                Size = new Size(120, 30)
            };
            btnAdd.Click += BtnAdd_Click;

            trainingGrid = new DataGridView()
            {
                Location = new Point(20, 80),
                Size = new Size(940, 650),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                AllowUserToAddRows = false,
                ReadOnly = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            trainingGrid.Columns.Add("Training_ID", "Training ID");
            trainingGrid.Columns.Add("GameFowl_ID", "GameFowl ID");
            trainingGrid.Columns.Add("TrainingType", "Training Type");
            trainingGrid.Columns.Add("SessionDate", "Session Date");

            // Remarks with dropdown
            var remarksColumn = new DataGridViewComboBoxColumn()
            {
                Name = "Remarks",
                HeaderText = "Remarks",
                DataSource = new string[] { "Pass", "Fail", "Updated Pass", "Updated Fail" },
                FlatStyle = FlatStyle.Flat
            };
            trainingGrid.Columns.Add(remarksColumn);

            trainingGrid.CellValueChanged += TrainingGrid_CellValueChanged;

            this.Controls.Add(titleLabel);
            this.Controls.Add(btnBack);
            this.Controls.Add(btnAdd);
            this.Controls.Add(trainingGrid);
        }

        private void LoadTrainingLogs()
        {
            string query = "SELECT Training_ID, GameFowl_ID, TrainingType, SessionDate, Remarks FROM training ORDER BY Training_ID DESC";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        trainingGrid.Rows.Clear();

                        while (reader.Read())
                        {
                            trainingGrid.Rows.Add(
                                reader.GetInt32("Training_ID"),
                                reader.GetInt32("GameFowl_ID"),
                                reader.GetString("TrainingType"),
                                reader.GetDateTime("SessionDate").ToString("yyyy-MM-dd"),
                                reader.IsDBNull(reader.GetOrdinal("Remarks")) ? "" : reader.GetString("Remarks")
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading training logs: " + ex.Message);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form2().Show();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Form addForm = new Form()
            {
                Size = new Size(400, 300),
                Text = "Add Training Log",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog
            };

            var lblGFID = new Label() { Text = "GameFowl ID", Location = new Point(30, 30) };
            var txtGFID = new TextBox() { Location = new Point(150, 30), Width = 200 };

            var lblType = new Label() { Text = "Training Type", Location = new Point(30, 70) };
            var txtType = new TextBox() { Location = new Point(150, 70), Width = 200 };

            var lblDate = new Label() { Text = "Session Date", Location = new Point(30, 110) };
            var dtDate = new DateTimePicker() { Location = new Point(150, 110), Format = DateTimePickerFormat.Short };

            var btnSave = new Button() { Text = "Save", Location = new Point(150, 160), Width = 100 };
            btnSave.Click += (s, ev) =>
            {
                if (int.TryParse(txtGFID.Text, out int gamefowlID) && !string.IsNullOrWhiteSpace(txtType.Text))
                {
                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();
                            string insertQuery = "INSERT INTO training (GameFowl_ID, TrainingType, SessionDate, Remarks) VALUES (@gfid, @type, @date, '')";
                            using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@gfid", gamefowlID);
                                cmd.Parameters.AddWithValue("@type", txtType.Text);
                                cmd.Parameters.AddWithValue("@date", dtDate.Value.Date);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        MessageBox.Show("Training log added!");
                        addForm.Close();
                        LoadTrainingLogs();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error adding log: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter valid inputs.");
                }
            };

            addForm.Controls.AddRange(new Control[] { lblGFID, txtGFID, lblType, txtType, lblDate, dtDate, btnSave });
            addForm.ShowDialog();
        }

        private void TrainingGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (trainingGrid.Columns[e.ColumnIndex].Name == "Remarks")
            {
                int rowIndex = e.RowIndex;
                if (rowIndex >= 0)
                {
                    int trainingId = Convert.ToInt32(trainingGrid.Rows[rowIndex].Cells["Training_ID"].Value);
                    string newRemark = trainingGrid.Rows[rowIndex].Cells["Remarks"].Value?.ToString() ?? "";

                    try
                    {
                        using (MySqlConnection conn = new MySqlConnection(connectionString))
                        {
                            conn.Open();
                            string updateQuery = "UPDATE training SET Remarks = @remark WHERE Training_ID = @id";
                            using (MySqlCommand cmd = new MySqlCommand(updateQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@remark", newRemark);
                                cmd.Parameters.AddWithValue("@id", trainingId);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // 🔽 Replace this block with the one below
                        MessageBox.Show("Error updating remark:\n" + ex.Message +
                                        "\nQuery: UPDATE training SET Remarks = '" + newRemark +
                                        "' WHERE Training_ID = " + trainingId);
                    }
                }
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

            trainingGrid.BackgroundColor = Color.FromArgb(40, 40, 40);
            trainingGrid.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            trainingGrid.DefaultCellStyle.ForeColor = Color.White;
            trainingGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            trainingGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            trainingGrid.EnableHeadersVisualStyles = false;
        }
    }
}
