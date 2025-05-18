using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GamefarmManagemet
{
    public partial class AuditLogs : Form
    {
        private Label titleLabel;
        private DataGridView auditGrid;
        private Button btnBack;

        // 🔑 Replace with your actual DB credentials
        private string connectionString = "server=localhost;user id=root;password=Leonard010504.;database=ex_db";

        public AuditLogs()
        {
            InitializeComponent();
            InitializeLayout();
            ApplyDarkMode();
            LoadAuditLogs();
        }

        private void InitializeLayout()
        {
            this.Size = new Size(1000, 800);
            this.Text = "Audit Logs";
            this.StartPosition = FormStartPosition.CenterScreen;

            titleLabel = new Label()
            {
                Text = "Audit Logs",
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

            auditGrid = new DataGridView()
            {
                Location = new Point(20, 80),
                Size = new Size(940, 650),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            auditGrid.Columns.Add("Log_ID", "Log ID");
            auditGrid.Columns.Add("Handler_ID", "Handler ID");
            auditGrid.Columns.Add("Action_Type", "Action Type");
            auditGrid.Columns.Add("Old_Value", "Old Salary");
            auditGrid.Columns.Add("New_Value", "New Salary");
            auditGrid.Columns.Add("Log_Time", "Timestamp");

            this.Controls.Add(titleLabel);
            this.Controls.Add(btnBack);
            this.Controls.Add(auditGrid);
        }

        private void LoadAuditLogs()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT log_id, handler_id, action_type, old_value, new_value, log_time FROM audit_log ORDER BY log_time DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        auditGrid.Rows.Clear();

                        while (reader.Read())
                        {
                            auditGrid.Rows.Add(
                                reader["log_id"].ToString(),
                                reader["handler_id"].ToString(),
                                reader["action_type"].ToString(),
                                reader["old_value"].ToString(),
                                reader["new_value"].ToString(),
                                Convert.ToDateTime(reader["log_time"]).ToString("yyyy-MM-dd HH:mm:ss")
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading audit logs:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            auditGrid.BackgroundColor = Color.FromArgb(40, 40, 40);
            auditGrid.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            auditGrid.DefaultCellStyle.ForeColor = Color.White;
            auditGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            auditGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            auditGrid.EnableHeadersVisualStyles = false;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Hide(); // or this.Close();
            Form2 mainMenu = new Form2();
            mainMenu.Show();
        }

        private void AuditLogs_Load(object sender, EventArgs e)
        {
            // Optional: auto refresh on load
            LoadAuditLogs();
        }
    }
}
