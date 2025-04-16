using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GamefarmManagemet
{
    public partial class Breeding : Form
    {
        private Label titleLabel;
        private DataGridView breedingGrid;
        private Button btnBack;

        public Breeding()
        {
            InitializeComponent();
            InitializeLayout();
            ApplyDarkMode();
            LoadBreedingData();
        }

        private void InitializeLayout()
        {
            this.Size = new Size(1000, 800);
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
                Location = new Point(800, 30),
                Size = new Size(120, 30)
            };
            btnBack.Click += BtnBack_Click;

            breedingGrid = new DataGridView()
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

            breedingGrid.Columns.Add("Breeding_ID", "Breeding_ID");
            breedingGrid.Columns.Add("GameFowl_ID", "GameFowl_ID");
            breedingGrid.Columns.Add("Father_ID", "Father_ID");
            breedingGrid.Columns.Add("Mother_ID", "Mother_ID");
            breedingGrid.Columns.Add("OffSpring_Count", "OffSpring_Count");

            this.Controls.Add(titleLabel);
            this.Controls.Add(btnBack);
            this.Controls.Add(breedingGrid);
        }

        private void LoadBreedingData()
        {
            var breedingRecords = new List<(int breedingID, int gamefowlID, int fatherID, int motherID, int offspringCount)>
            {
                (1, 101, 1010, 1111, 20),
                (2, 102, 1011, 1112, 20),
                (3, 103, 1012, 1113, 25),
                (4, 104, 1013, 1114, 15),
                (5, 105, 1014, 1115, 10),
                (6, 106, 1015, 1116, 20),
                (7, 107, 1016, 1117, 15),
                (8, 108, 1017, 1118, 10),
                (9, 109, 1018, 1119, 20),
                (10, 110, 1019, 1120, 10)
            };

            foreach (var b in breedingRecords)
            {
                breedingGrid.Rows.Add(b.breedingID, b.gamefowlID, b.fatherID, b.motherID, b.offspringCount);
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
            this.Hide(); // Or this.Close();
            Form2 mainMenu = new Form2();
            mainMenu.Show();
        }

        private void Breeding_Load(object sender, EventArgs e)
        {
            // Optional: for future logic on load
        }
    }
}
