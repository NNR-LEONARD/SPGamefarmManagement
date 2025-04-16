using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GamefarmManagemet
{
    public partial class AuditLogs : Form
    {
        private Label titleLabel;
        private DataGridView auditGrid;
        private Button btnBack;

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

            auditGrid.Columns.Add("Log_ID", "Log_ID");
            auditGrid.Columns.Add("User", "User");
            auditGrid.Columns.Add("Action", "Action");
            auditGrid.Columns.Add("Timestamp", "Timestamp");

            this.Controls.Add(titleLabel);
            this.Controls.Add(btnBack);
            this.Controls.Add(auditGrid);
        }

        private void LoadAuditLogs()
        {
            // Sample data — replace with your actual audit logs
            var logs = new List<(int logID, string user, string action, string timestamp)>
            {
                (1, "Training", "Update", "2025-04-15 08:00:00"),
                (2, "Training", "Update", "2025-04-15 08:05:00"),
                (3, "Training", "Update", "2025-04-15 08:30:00"),
                (4, "Training", "Update", "2025-04-15 09:00:00")
            };

            foreach (var log in logs)
            {
                auditGrid.Rows.Add(log.logID, log.user, log.action, log.timestamp);
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
            this.Hide(); // Or this.Close();
            Form2 mainMenu = new Form2();
            mainMenu.Show();
        }

        private void AuditLogs_Load(object sender, EventArgs e)
        {

        }
    }
}
