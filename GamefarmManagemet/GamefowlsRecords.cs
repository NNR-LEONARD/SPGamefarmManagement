using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GamefarmManagemet
{
    public partial class GamefowlsRecords : Form
    {
        private Label titleLabel;
        private DataGridView gamefowlGrid;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnBack;

        public GamefowlsRecords()
        {
            InitializeComponent();
            InitializeLayout();
            ApplyDarkMode();
            LoadGamefowlsData();
        }
        private void GamefowlsRecords_Load(object sender, EventArgs e)
        {
            // You can leave it empty or call something like LoadGamefowlsData();
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

            btnEdit = new Button()
            {
                Text = "Edit",
                Location = new Point(20 + spacing, buttonY),
                Size = new Size(120, 30)
            };

            btnDelete = new Button()
            {
                Text = "Delete",
                Location = new Point(20 + spacing * 2, buttonY),
                Size = new Size(120, 30)
            };

            btnBack = new Button()
            {
                Text = "Back to Menu",
                Location = new Point(20 + spacing * 3, buttonY),
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
            gamefowlGrid.Columns.Add("Status", "Status");

            this.Controls.Add(titleLabel);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnEdit);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnBack);
            this.Controls.Add(gamefowlGrid);
        }

        private void LoadGamefowlsData()
        {
            var gamefowls = new List<(int id, string bloodline, string hatched, string status)>
            {
                (1, "Asil", "2024-01-01", "Breeding"),
                (2, "SuperSweater", "2024-01-01", "Breeding"),
                (100, "SBR", "2024-01-22", "BabyStag"),
                (101, "Claret x Gull", "2024-01-22", "BroodCock"),
                (102, "Sweater", "2024-01-22", "BroodCock"),
                (103, "White Kelso", "2024-01-22", "BroodCock"),
                (104, "Claret", "2024-01-22", "BroodCock"),
                (105, "SBR", "2024-01-22", "BroodCock"),
                (106, "Melsims", "2024-01-22", "BroodCock"),
                (107, "Sweater", "2024-01-22", "Culling"),
                (108, "Gilmore", "2024-01-22", "BroodCock"),
                (109, "Gilmore", "2024-01-22", "BroodCock"),
                (200, "TestBloodline", "2024-01-01", "Available"),
                (201, "ClaretGull", "2024-01-01", "Cock")
            };

            foreach (var g in gamefowls)
            {
                gamefowlGrid.Rows.Add(g.id, g.bloodline, g.hatched, g.status);
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
            this.Hide(); // or this.Close(); if you don't plan to return
            Form2 mainMenu = new Form2(); // make sure Form1 exists and is imported
            mainMenu.Show();
        }

    }
}
