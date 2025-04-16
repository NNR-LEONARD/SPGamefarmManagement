using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GamefarmManagemet
{
    public partial class TrainingLogs : Form
    {
        private Label titleLabel;
        private DataGridView trainingGrid;
        private Button btnBack;

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

            trainingGrid = new DataGridView()
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

            trainingGrid.Columns.Add("Training_ID", "Training_ID");
            trainingGrid.Columns.Add("GameFowl_ID", "GameFowl_ID");
            trainingGrid.Columns.Add("TrainingType", "Training Type");
            trainingGrid.Columns.Add("SessionDate", "Session Date");
            trainingGrid.Columns.Add("Remarks", "Remarks");

            this.Controls.Add(titleLabel);
            this.Controls.Add(btnBack);
            this.Controls.Add(trainingGrid);
        }

        private void LoadTrainingLogs()
        {
            var logs = new List<(int trainingID, int gamefowlID, string type, string date, string remarks)>
            {
                (1, 101, "Running", "2025-02-01", "Updated Pass"),
                (2, 102, "Sparring", "2025-02-01", "Updated Pass"),
                (3, 103, "Running", "2025-02-01", "Pass"),
                (4, 104, "Sparring", "2025-02-01", "Pass"),
                (5, 105, "Sparring", "2025-02-01", "Pass"),
                (6, 106, "Sparring", "2025-02-01", "Updated Pass"),
                (7, 107, "Running", "2025-02-01", "Pass"),
                (8, 108, "Sparring", "2025-02-01", "Fail"),
                (9, 109, "Sparring", "2025-02-01", "Fail"),
                (10, 110, "Running", "2025-02-01", "Pass")
            };

            foreach (var log in logs)
            {
                trainingGrid.Rows.Add(log.trainingID, log.gamefowlID, log.type, log.date, log.remarks);
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

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Hide(); // Or this.Close();
            Form2 mainMenu = new Form2();
            mainMenu.Show();
        }

        private void TrainingLogs_Load(object sender, EventArgs e)
        {

        }
    }
}
