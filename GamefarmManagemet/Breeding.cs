using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GamefarmManagemet
{
    public partial class Breeding : Form
    {
        private Label titleLabel;
        private DataGridView breedingGrid;
        private Button btnBack;
        private Button btnAdd;
        private Button btnDelete;

        private string connectionString = "server=localhost;uid=root;pwd=Leonard010504.;database=ex_db;";

        public Breeding()
        {
            InitializeComponent();
            InitializeLayout();
            ApplyDarkMode();
            LoadBreedingData();
        }

        private void InitializeLayout()
        {
            this.Size = new Size(800, 600);
            this.Text = "Breeding Records";
            this.StartPosition = FormStartPosition.CenterScreen;

            titleLabel = new Label()
            {
                Text = "Breeding Records",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            btnBack = new Button()
            {
                Text = "Back to Menu",
                Location = new Point(650, 25),
                Size = new Size(120, 30)
            };
            btnBack.Click += BtnBack_Click;

            btnAdd = new Button()
            {
                Text = "Add Breeding",
                Location = new Point(20, 530),
                Size = new Size(120, 30)
            };
            btnAdd.Click += BtnAdd_Click;

            btnDelete = new Button()
            {
                Text = "Delete Selected",
                Location = new Point(160, 530),
                Size = new Size(120, 30)
            };
            btnDelete.Click += BtnDelete_Click;

            breedingGrid = new DataGridView()
            {
                Location = new Point(20, 70),
                Size = new Size(750, 450),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            breedingGrid.Columns.Add("Breeding_ID", "Breeding ID");
            breedingGrid.Columns.Add("Father_ID", "Father ID");
            breedingGrid.Columns.Add("Mother_ID", "Mother ID");
            breedingGrid.Columns.Add("Label", "Label");

            this.Controls.Add(titleLabel);
            this.Controls.Add(btnBack);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnDelete);
            this.Controls.Add(breedingGrid);
        }

        private void LoadBreedingData()
        {
            string query = "SELECT Breeding_ID, Father_ID, Mother_ID, Label FROM breeding ORDER BY Breeding_ID DESC";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        breedingGrid.Rows.Clear();

                        while (reader.Read())
                        {
                            int breedingID = reader.GetInt32("Breeding_ID");
                            int fatherID = reader.GetInt32("Father_ID");
                            int motherID = reader.GetInt32("Mother_ID");
                            string label = reader.GetString("Label");

                            breedingGrid.Rows.Add(breedingID, fatherID, motherID, label);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading breeding data: " + ex.Message);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // Show a simple input form/dialog to get new breeding info
            using (Form addForm = new Form())
            {
                addForm.Text = "Add New Breeding";
                addForm.Size = new Size(300, 250);
                addForm.StartPosition = FormStartPosition.CenterParent;

                Label lblFather = new Label() { Text = "Father ID:", Location = new Point(10, 20) };
                TextBox txtFather = new TextBox() { Location = new Point(100, 20), Width = 150 };

                Label lblMother = new Label() { Text = "Mother ID:", Location = new Point(10, 60) };
                TextBox txtMother = new TextBox() { Location = new Point(100, 60), Width = 150 };

                Label lblLabel = new Label() { Text = "Label:", Location = new Point(10, 100) };
                ComboBox cmbLabel = new ComboBox()
                {
                    Location = new Point(100, 100),
                    Width = 150,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                cmbLabel.Items.AddRange(new string[] { "Local", "Early-Bird", "Late-Born", "International" });
                cmbLabel.SelectedIndex = 0;

                Button btnSubmit = new Button() { Text = "Add", Location = new Point(100, 150), Width = 80 };
                btnSubmit.Click += (s, ea) =>
                {
                    if (int.TryParse(txtFather.Text.Trim(), out int fatherId) &&
                        int.TryParse(txtMother.Text.Trim(), out int motherId) &&
                        cmbLabel.SelectedItem != null)
                    {
                        AddBreedingRecord(fatherId, motherId, cmbLabel.SelectedItem.ToString());
                        addForm.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please enter valid data.");
                    }
                };

                addForm.Controls.Add(lblFather);
                addForm.Controls.Add(txtFather);
                addForm.Controls.Add(lblMother);
                addForm.Controls.Add(txtMother);
                addForm.Controls.Add(lblLabel);
                addForm.Controls.Add(cmbLabel);
                addForm.Controls.Add(btnSubmit);

                addForm.ShowDialog();
            }
        }

        private void AddBreedingRecord(int fatherID, int motherID, string label)
        {
            string query = "INSERT INTO breeding (Father_ID, Mother_ID, Label) VALUES (@father, @mother, @label)";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@father", fatherID);
                        cmd.Parameters.AddWithValue("@mother", motherID);
                        cmd.Parameters.AddWithValue("@label", label);

                        cmd.ExecuteNonQuery();
                    }
                }

                LoadBreedingData(); // Refresh grid
                MessageBox.Show("New breeding record added.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding breeding record: " + ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (breedingGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record to delete.");
                return;
            }

            int breedingID = Convert.ToInt32(breedingGrid.SelectedRows[0].Cells["Breeding_ID"].Value);

            var confirm = MessageBox.Show("Are you sure you want to delete the selected breeding record?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                DeleteBreedingRecord(breedingID);
            }
        }

        private void DeleteBreedingRecord(int breedingID)
        {
            string query = "DELETE FROM breeding WHERE Breeding_ID = @id";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", breedingID);
                        cmd.ExecuteNonQuery();
                    }
                }

                LoadBreedingData(); // Refresh grid
                MessageBox.Show("Breeding record deleted.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting breeding record: " + ex.Message);
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
    }
}
