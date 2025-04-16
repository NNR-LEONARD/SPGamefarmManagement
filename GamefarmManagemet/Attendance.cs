using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GamefarmManagemet
{
    public partial class Attendance : Form
    {
        private Label titleLabel;
        private DataGridView attendanceGrid;
        private DateTimePicker datePicker;
        private Button btnMarkPresent;
        private Button btnMarkAbsent;
        private Button btnBack;

        public Attendance()
        {
            InitializeComponent();
            InitializeLayout();
            ApplyDarkMode();
            LoadHandlers();
        }

        private void Attendance_Load(object sender, EventArgs e)
        {
        }

        private void InitializeLayout()
        {
            this.Size = new Size(1000, 800);
            this.Text = "Attendance";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Resize += Attendance_Resize;

            titleLabel = new Label()
            {
                Text = "Attendance Sheet",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };

            datePicker = new DateTimePicker()
            {
                Location = new Point(this.ClientSize.Width - 180, 30),
                Width = 160,
                Format = DateTimePickerFormat.Short,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            attendanceGrid = new DataGridView()
            {
                Location = new Point(20, 70),
                Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 500),
                AllowUserToAddRows = false,
                ReadOnly = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

            attendanceGrid.Columns.Add("HandlerID", "Handler ID");
            attendanceGrid.Columns.Add("HandlerName", "Handler Name");
            attendanceGrid.Columns.Add("Date", "Date");
            attendanceGrid.Columns.Add("Status", "Status");

            btnMarkPresent = new Button()
            {
                Text = "Mark Present",
                Location = new Point(20, this.ClientSize.Height - 80),
                Size = new Size(120, 30),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            btnMarkAbsent = new Button()
            {
                Text = "Mark Absent",
                Location = new Point(160, this.ClientSize.Height - 80),
                Size = new Size(120, 30),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            btnBack = new Button()
            {
                Text = "Back to Menu",
                Location = new Point(this.ClientSize.Width - 140, this.ClientSize.Height - 80),
                Size = new Size(120, 30),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            btnMarkPresent.Click += BtnMarkPresent_Click;
            btnMarkAbsent.Click += BtnMarkAbsent_Click;
            btnBack.Click += BtnBack_Click;

            this.Controls.Add(titleLabel);
            this.Controls.Add(datePicker);
            this.Controls.Add(attendanceGrid);
            this.Controls.Add(btnMarkPresent);
            this.Controls.Add(btnMarkAbsent);
            this.Controls.Add(btnBack);
        }

        private void Attendance_Resize(object sender, EventArgs e)
        {
            // Resize the grid and reposition buttons
            attendanceGrid.Size = new Size(this.ClientSize.Width - 40, this.ClientSize.Height - 500);
            datePicker.Location = new Point(this.ClientSize.Width - 180, 30);
            btnMarkPresent.Location = new Point(20, this.ClientSize.Height - 80);
            btnMarkAbsent.Location = new Point(160, this.ClientSize.Height - 80);
            btnBack.Location = new Point(this.ClientSize.Width - 140, this.ClientSize.Height - 80);
        }

        private void BtnMarkPresent_Click(object sender, EventArgs e)
        {
            UpdateSelectedRowStatus("Present");
        }

        private void BtnMarkAbsent_Click(object sender, EventArgs e)
        {
            UpdateSelectedRowStatus("Absent");
        }

        private void UpdateSelectedRowStatus(string status)
        {
            if (attendanceGrid.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = attendanceGrid.SelectedRows[0];
                selectedRow.Cells["Date"].Value = datePicker.Value.ToShortDateString();
                selectedRow.Cells["Status"].Value = status;
            }
            else
            {
                MessageBox.Show("Please select a handler row first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadHandlers()
        {
            var handlers = new List<(string id, string name)>
            {
                ("H001", "Juan Dela Cruz"),
                ("H002", "Maria Santos"),
                ("H003", "Pedro Reyes"),
                ("H004", "Ana Lopez"),
                ("H005", "Jose Garcia")
            };

            foreach (var handler in handlers)
            {
                attendanceGrid.Rows.Add(handler.id, handler.name, "", "");
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void ApplyDarkMode()
        {
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ForeColor = Color.Gainsboro;

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Label || ctrl is DateTimePicker || ctrl is Button)
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

            attendanceGrid.BackgroundColor = Color.FromArgb(40, 40, 40);
            attendanceGrid.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            attendanceGrid.DefaultCellStyle.ForeColor = Color.White;
            attendanceGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
            attendanceGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            attendanceGrid.EnableHeadersVisualStyles = false;
        }
    }
}
