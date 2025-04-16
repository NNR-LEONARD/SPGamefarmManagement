using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GamefarmManagemet
{
    public partial class FightRecords : Form
    {
        private Label titleLabel;
        private DataGridView fightGrid;
        private Button btnBack;

        public FightRecords()
        {
            InitializeComponent();
            InitializeLayout();
            ApplyDarkMode();
            // The Load event is connected in the designer, so no need to call it manually here
        }

        private void InitializeLayout()
        {
            this.Size = new Size(1000, 800);
            this.Text = "Fight Records";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Resize += FightRecords_Resize;

            titleLabel = new Label()
            {
                Text = "Fight Records",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            fightGrid = new DataGridView()
            {
                Location = new Point(20, 70),
                Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 150),
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

            fightGrid.Columns.Add("Fight_ID", "Fight ID");
            fightGrid.Columns.Add("GameFowl_ID", "GameFowl ID");
            fightGrid.Columns.Add("OpponentBreed", "Opponent Breed");
            fightGrid.Columns.Add("Fight_Date", "Fight Date");
            fightGrid.Columns.Add("Location", "Location");
            fightGrid.Columns.Add("Result", "Result");
            fightGrid.Columns.Add("Injured", "Injured");

            btnBack = new Button()
            {
                Text = "Back to Menu",
                Size = new Size(120, 30),
                Location = new Point(this.ClientSize.Width - 140, this.ClientSize.Height - 60),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            btnBack.Click += BtnBack_Click;

            this.Controls.Add(titleLabel);
            this.Controls.Add(fightGrid);
            this.Controls.Add(btnBack);
        }

        private void FightRecords_Resize(object sender, EventArgs e)
        {
            fightGrid.Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 150);
            btnBack.Location = new Point(this.ClientSize.Width - 140, this.ClientSize.Height - 60);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            Form2 form2 = new Form2();
            form2.Show();
        }

        // This is the missing FightRecords_Load method
        private void FightRecords_Load(object sender, EventArgs e)
        {
            LoadFightRecords();
        }

        private void LoadFightRecords()
        {
            // Sample static records (can be loaded from database in real app)
            var fightRecords = new List<string[]>
            {
                new string[] { "1", "100", "Kelso", "2025-01-05", "Arena1", "Win", "0" },
                new string[] { "2", "101", "Sweater", "2025-01-05", "Arena1", "Loss", "1" },
                new string[] { "3", "102", "Sweater", "2025-01-05", "Arena1", "Win", "0" },
                new string[] { "4", "103", "Claret", "2025-01-05", "Arena1", "Win", "1" },
                new string[] { "5", "104", "Kelso", "2025-01-05", "Arena1", "Win", "0" },
                new string[] { "6", "105", "Sweater", "2025-01-10", "Arena2", "Loss", "1" },
                new string[] { "7", "106", "Kelso", "2025-01-10", "Arena2", "Win", "0" },
                new string[] { "8", "107", "Sweater", "2025-01-10", "Arena2", "Win", "0" },
                new string[] { "9", "108", "Claret", "2025-01-10", "Arena2", "Win", "0" },
                new string[] { "10", "109", "Sweater", "2025-01-10", "Arena2", "Win", "0" },
                new string[] { "11", "201", "Kelso", "2024-02-01", "Arena1", "Win", "0" }
            };

            foreach (var record in fightRecords)
            {
                fightGrid.Rows.Add(record);
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

            fightGrid.BackgroundColor = Color.FromArgb(40, 40, 40);
            fightGrid.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            fightGrid.DefaultCellStyle.ForeColor = Color.White;
            fightGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            fightGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            fightGrid.EnableHeadersVisualStyles = false;
        }
    }
}
