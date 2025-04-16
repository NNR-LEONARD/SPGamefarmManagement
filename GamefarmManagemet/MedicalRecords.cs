using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GamefarmManagemet
{
    public partial class MedicalRecords : Form
    {
        private Label titleLabel;
        private DataGridView recordsGrid;
        private Button btnBack;

        public MedicalRecords()
        {
            InitializeComponent();
            InitializeLayout();
            ApplyDarkMode();
            LoadMedicalRecords();
        }

        private void InitializeLayout()
        {
            this.Size = new Size(1000, 800);
            this.Text = "Medical Records";
            this.StartPosition = FormStartPosition.CenterScreen;

            titleLabel = new Label()
            {
                Text = "Medical Records",
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

            recordsGrid = new DataGridView()
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

            recordsGrid.Columns.Add("MedicalRecord_ID", "MedicalRecord_ID");
            recordsGrid.Columns.Add("GameFowl_ID", "GameFowl_ID");
            recordsGrid.Columns.Add("Diagnosis", "Diagnosis");
            recordsGrid.Columns.Add("Treatment", "Treatment");
            recordsGrid.Columns.Add("CheckUp_Date", "CheckUp Date");
            recordsGrid.Columns.Add("Cost", "Cost");

            this.Controls.Add(titleLabel);
            this.Controls.Add(btnBack);
            this.Controls.Add(recordsGrid);
        }

        private void LoadMedicalRecords()
        {
            var records = new List<(int recordID, int gamefowlID, string diagnosis, string treatment, string date, decimal cost)>
            {
                (1, 100, "Wing Injury", "Antibiotics", "2025-01-11", 500),
                (2, 101, "Leg Injury", "Antibiotics", "2025-01-11", 1500),
                (3, 102, "Wing Injury", "Antibiotics", "2025-01-11", 0),
                (4, 103, "Leg Injury", "Antibiotics", "2025-01-11", 0),
                (5, 104, "Wing Injury", "Antibiotics", "2025-01-11", 0),
                (6, 105, "Wing Injury", "Antibiotics", "2025-01-11", 0),
                (7, 106, "Wing Injury", "Antibiotics", "2025-01-11", 0),
                (8, 107, "Leg Injury", "Antibiotics", "2025-01-11", 0),
                (9, 108, "Wing Injury", "Antibiotics", "2025-01-11", 0),
                (10, 109, "Wing Injury", "Antibiotics", "2025-01-11", 0)
            };

            foreach (var r in records)
            {
                recordsGrid.Rows.Add(r.recordID, r.gamefowlID, r.diagnosis, r.treatment, r.date, r.cost.ToString("F2"));
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

            recordsGrid.BackgroundColor = Color.FromArgb(40, 40, 40);
            recordsGrid.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            recordsGrid.DefaultCellStyle.ForeColor = Color.White;
            recordsGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            recordsGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            recordsGrid.EnableHeadersVisualStyles = false;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Hide(); // Or this.Close();
            Form2 mainMenu = new Form2();
            mainMenu.Show();
        }
        private void MedicalRecords_Load(object sender, EventArgs e)
        {
           
        }
    }
}
