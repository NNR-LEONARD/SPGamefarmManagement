using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GamefarmManagemet
{
    public partial class HandlerManagement : Form
    {
        private Label titleLabel;
        private DataGridView handlersGrid;
        private Button btnAddHandler;
        private Button btnEditHandler;
        private Button btnDeleteHandler;
        private Button btnBackToMenu;

        private string connectionString = "server=localhost;database=ex_db;uid=root;pwd=Leonard010504.";

        public HandlerManagement()
        {
            InitializeLayout();
            ApplyDarkMode();
            LoadHandlers();
        }

        private void InitializeLayout()
        {
            this.Size = new Size(1000, 800);
            this.Text = "Handlers Management";
            this.StartPosition = FormStartPosition.CenterScreen;

            titleLabel = new Label()
            {
                Text = "Handlers Management",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            int buttonY = 70;
            int spacing = 140;

            btnAddHandler = new Button()
            {
                Text = "Add Handler",
                Location = new Point(20, buttonY),
                Size = new Size(120, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            btnEditHandler = new Button()
            {
                Text = "Edit Handler",
                Location = new Point(20 + spacing, buttonY),
                Size = new Size(120, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            btnDeleteHandler = new Button()
            {
                Text = "Delete Handler",
                Location = new Point(20 + spacing * 2, buttonY),
                Size = new Size(120, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            btnBackToMenu = new Button()
            {
                Text = "Back to Menu",
                Location = new Point(20 + spacing * 3, buttonY),
                Size = new Size(120, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            handlersGrid = new DataGridView()
            {
                Location = new Point(20, 120),
                Size = new Size(940, 620),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            handlersGrid.Columns.Add("HandlerID", "Handler ID");
            handlersGrid.Columns.Add("HandlerName", "Handler Name");
            handlersGrid.Columns.Add("HandlerRole", "Handler Role");
            handlersGrid.Columns.Add("HandlerSalary", "Handler Salary");

            btnAddHandler.Click += BtnAddHandler_Click;
            btnEditHandler.Click += BtnEditHandler_Click;
            btnDeleteHandler.Click += BtnDeleteHandler_Click;
            btnBackToMenu.Click += BtnBackToMenu_Click;

            this.Controls.Add(titleLabel);
            this.Controls.Add(btnAddHandler);
            this.Controls.Add(btnEditHandler);
            this.Controls.Add(btnDeleteHandler);
            this.Controls.Add(btnBackToMenu);
            this.Controls.Add(handlersGrid);
        }

        private void LoadHandlers()
        {
            handlersGrid.Rows.Clear();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Handler_ID, Handler_Name, Handler_Role, Handler_Salary FROM handlermanagement";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        handlersGrid.Rows.Add(
                            reader["Handler_ID"].ToString(),
                            reader["Handler_Name"].ToString(),
                            reader["Handler_Role"].ToString(),
                            Convert.ToDecimal(reader["Handler_Salary"]).ToString("C"));
                    }
                }
            }
        }

        private void BtnAddHandler_Click(object sender, EventArgs e)
        {
            string name = Prompt.ShowDialog("Enter name:", "Add Handler");
            string role = Prompt.ShowDialog("Enter role:", "Add Handler");
            string salaryText = Prompt.ShowDialog("Enter salary:", "Add Handler");

            if (decimal.TryParse(salaryText, out decimal salary))
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("sp_add_handler", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_name", name);
                        cmd.Parameters.AddWithValue("@p_role", role);
                        cmd.Parameters.AddWithValue("@p_salary", salary);

                        try
                        {
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Handler added successfully!");
                            LoadHandlers();
                        }
                        catch (MySqlException ex)
                        {
                            MessageBox.Show("Error: " + ex.Message); 
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid salary.");
            }
        }

        private void BtnEditHandler_Click(object sender, EventArgs e)
        {
            if (handlersGrid.SelectedRows.Count > 0)
            {
                DataGridViewRow row = handlersGrid.SelectedRows[0];
                string id = row.Cells["HandlerID"].Value.ToString();
                string currentName = row.Cells["HandlerName"].Value.ToString();
                string currentRole = row.Cells["HandlerRole"].Value.ToString();
                string currentSalary = row.Cells["HandlerSalary"].Value.ToString().Replace("₱", "").Trim();

                string newName = Prompt.ShowDialog("Edit name:", "Edit Handler", currentName);
                string newRole = Prompt.ShowDialog("Edit role:", "Edit Handler", currentRole);
                string newSalaryText = Prompt.ShowDialog("Edit salary:", "Edit Handler", currentSalary);

                if (decimal.TryParse(newSalaryText, out decimal newSalary))
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "UPDATE handlermanagement SET Handler_Name=@Handler_Name, Handler_Role=@Handler_Role, Handler_Salary=@Handler_Salary WHERE Handler_ID=@id";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Handler_Name", newName);
                            cmd.Parameters.AddWithValue("@Handler_Role", newRole);
                            cmd.Parameters.AddWithValue("@Handler_Salary", newSalary);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    LoadHandlers();
                }
                else
                {
                    MessageBox.Show("Invalid salary.");
                }
            }
            else
            {
                MessageBox.Show("Please select a handler to edit.");
            }
        }

        private void BtnDeleteHandler_Click(object sender, EventArgs e)
        {
            if (handlersGrid.SelectedRows.Count > 0)
            {
                string id = handlersGrid.SelectedRows[0].Cells["HandlerID"].Value.ToString();
                DialogResult result = MessageBox.Show($"Are you sure you want to delete handler ID: {id}?",
                                                      "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM handlermanagement WHERE Handler_ID = @id";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    LoadHandlers();
                }
            }
            else
            {
                MessageBox.Show("Please select a handler to delete.");
            }
        }

        private void BtnBackToMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 menuForm = new Form2();
            menuForm.Show();
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

            handlersGrid.BackgroundColor = Color.FromArgb(40, 40, 40);
            handlersGrid.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            handlersGrid.DefaultCellStyle.ForeColor = Color.White;
            handlersGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            handlersGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            handlersGrid.EnableHeadersVisualStyles = false;
        }

        private void HandlerManagement_Load(object sender, EventArgs e) { }
    }

    // Helper class for prompting user input
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption, string defaultText = "")
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.Gainsboro
            };

            Label textLabel = new Label() { Left = 20, Top = 20, Text = text, Width = 340 };
            TextBox inputBox = new TextBox() { Left = 20, Top = 50, Width = 340, Text = defaultText };
            Button confirmation = new Button() { Text = "OK", Left = 270, Width = 90, Top = 90, DialogResult = DialogResult.OK };

            confirmation.FlatStyle = FlatStyle.Flat;
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : "";
        }
    }
}
