using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

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

        // Replace with your actual connection string
        private string connectionString = "server=localhost;database=ex_db;uid=root;pwd=Leonard010504.;";

        public Attendance()
        {
            InitializeComponent();
            InitializeLayout();
            ApplyDarkMode();
            LoadAttendance();
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
                ReadOnly = true,
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
                string handlerID = selectedRow.Cells["HandlerID"].Value.ToString();
                string handlerName = selectedRow.Cells["HandlerName"].Value.ToString();
                string date = datePicker.Value.ToString("yyyy-MM-dd");

                // Insert or update attendance in DB
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if record exists for that handler and date
                    string checkQuery = "SELECT COUNT(*) FROM attendance WHERE HandlerID=@HandlerID AND Date=@Date";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@HandlerID", handlerID);
                        checkCmd.Parameters.AddWithValue("@Date", date);
                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (count > 0)
                        {
                            // Update existing record
                            string updateQuery = "UPDATE attendance SET Status=@Status WHERE HandlerID=@HandlerID AND Date=@Date";
                            using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@Status", status);
                                updateCmd.Parameters.AddWithValue("@HandlerID", handlerID);
                                updateCmd.Parameters.AddWithValue("@Date", date);
                                updateCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Insert new record
                            string insertQuery = "INSERT INTO attendance (HandlerID, Handler_name, Date, Status) VALUES (@HandlerID, @Handler_name, @Date, @Status)";
                            using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@HandlerID", handlerID);
                                insertCmd.Parameters.AddWithValue("@Handler_name", handlerName);
                                insertCmd.Parameters.AddWithValue("@Date", date);
                                insertCmd.Parameters.AddWithValue("@Status", status);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                // Update grid display
                selectedRow.Cells["Date"].Value = date;
                selectedRow.Cells["Status"].Value = status;
            }
            else
            {
                MessageBox.Show("Please select a handler row first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadAttendance()
        {
            attendanceGrid.Rows.Clear();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Get list of all handlers first to display all
                string handlersQuery = "SELECT DISTINCT Handler_ID, Handler_Name FROM handlermanagement ORDER BY Handler_name";
                var handlers = new List<(string id, string name)>();

                using (MySqlCommand cmd = new MySqlCommand(handlersQuery, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Convert Handler_ID (int) to string
                        string id = reader.GetInt32(reader.GetOrdinal("Handler_ID")).ToString();
                        string name = reader.GetString("Handler_Name");
                        handlers.Add((id, name));
                    }
                }


                // For each handler, get attendance record for selected date
                string selectedDate = datePicker.Value.ToString("yyyy-MM-dd");

                foreach (var handler in handlers)
                {
                    string attendanceQuery = "SELECT Status FROM attendance WHERE HandlerID=@HandlerID AND Date=@Date LIMIT 1";
                    string status = "";

                    using (MySqlCommand attendanceCmd = new MySqlCommand(attendanceQuery, conn))
                    {
                        attendanceCmd.Parameters.AddWithValue("@HandlerID", handler.id);
                        attendanceCmd.Parameters.AddWithValue("@Date", selectedDate);

                        object result = attendanceCmd.ExecuteScalar();
                        if (result != null)
                        {
                            status = result.ToString();
                        }
                    }

                    attendanceGrid.Rows.Add(handler.id, handler.name, selectedDate, status);
                }
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

        private void Attendance_Load(object sender, EventArgs e)
        {

        }
    }
}
